/* =====================================================================
   ZS.dev — interaktivnost
   - Work carousel (responsive: 3 / 2 / 1 kartica)
   - "How to improve" accordion + dinamicki scoring
   - Kontakt forma (AJAX snimanje u bazu)
   - Automatsko snimanje checklist odgovora pri svakoj promjeni
   - Zatvaranje mobilnog menija na klik
   ===================================================================== */
(function () {
    "use strict";

    // Dijeljeno stanje: postavljeno nakon uspjesnog submit-a kontakt forme
    var state = { leadId: null, form: null };

    document.addEventListener("DOMContentLoaded", function () {
        initCarousel();
        initChecklists();
        initContactForm();
        initMobileMenuClose();
    });

    /* ----------------------------- CAROUSEL ----------------------------- */
    function initCarousel() {
        var root = document.querySelector("[data-carousel]");
        if (!root) return;

        var track = root.querySelector("[data-track]");
        var cards = Array.prototype.slice.call(track.children);
        var prevBtn = root.querySelector("[data-prev]");
        var nextBtn = root.querySelector("[data-next]");
        var dotsWrap = document.querySelector("[data-dots]");
        if (cards.length === 0) return;

        var index = 0;

        function perView() {
            var w = window.innerWidth;
            if (w <= 640) return 1;
            if (w <= 991) return 2;
            return 3;
        }

        function maxIndex() {
            return Math.max(0, cards.length - perView());
        }

        function step() {
            var style = window.getComputedStyle(track);
            var gap = parseFloat(style.columnGap || style.gap || "0") || 0;
            return cards[0].getBoundingClientRect().width + gap;
        }

        function render() {
            if (index > maxIndex()) index = maxIndex();
            if (index < 0) index = 0;
            track.style.transform = "translateX(" + (-index * step()) + "px)";
            if (prevBtn) prevBtn.disabled = index === 0;
            if (nextBtn) nextBtn.disabled = index === maxIndex();
            updateDots();
        }

        function buildDots() {
            if (!dotsWrap) return;
            dotsWrap.innerHTML = "";
            var count = maxIndex() + 1;
            if (count <= 1) return;
            for (var i = 0; i < count; i++) {
                var dot = document.createElement("button");
                dot.className = "dot";
                dot.setAttribute("aria-label", "Slide " + (i + 1));
                (function (i) {
                    dot.addEventListener("click", function () { index = i; render(); });
                })(i);
                dotsWrap.appendChild(dot);
            }
        }

        function updateDots() {
            if (!dotsWrap) return;
            var dots = dotsWrap.querySelectorAll(".dot");
            dots.forEach(function (d, i) {
                d.classList.toggle("active", i === index);
            });
        }

        if (prevBtn) prevBtn.addEventListener("click", function () { index--; render(); });
        if (nextBtn) nextBtn.addEventListener("click", function () { index++; render(); });

        var resizeTimer;
        window.addEventListener("resize", function () {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(function () { buildDots(); render(); }, 150);
        });

        buildDots();
        render();
    }

    /* -------- SHARED: prikupi sve cekirane stavke -------- */
    function collectChecked() {
        var checked = [];
        document.querySelectorAll("[data-checklist]").forEach(function (list) {
            var key = list.getAttribute("data-key") || "";
            list.querySelectorAll("[data-check]:checked").forEach(function (cb) {
                var span = cb.closest("label") && cb.closest("label").querySelector("span");
                if (span) checked.push({ Key: key, Item: span.textContent.trim() });
            });
        });
        return checked;
    }

    /* -------- SHARED: snimi tekuce stanje checkliste na server -------- */
    function saveChecklist() {
        if (!state.leadId || !state.form) return;
        var checked = collectChecked();
        var tokenEl = state.form.querySelector('[name="__RequestVerificationToken"]');
        var params = new URLSearchParams();
        params.append("leadId", state.leadId);
        if (tokenEl) params.append("__RequestVerificationToken", tokenEl.value);
        if (checked.length > 0) params.append("checklistJson", JSON.stringify(checked));
        fetch("/Home/UpdateChecklist", {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded",
                "X-Requested-With": "XMLHttpRequest"
            },
            body: params.toString()
        });
    }

    /* --------------------------- CHECKLISTS ----------------------------- */
    function initChecklists() {
        var lists = document.querySelectorAll("[data-checklist]");
        lists.forEach(function (list) {
            var toggleBtn = list.querySelector("[data-toggle-list]");
            var body = list.querySelector("[data-body]");
            var checks = list.querySelectorAll("[data-check]");
            var bands = list.querySelectorAll(".score-band");
            var scoreText = list.querySelector("[data-score-text]");

            if (toggleBtn && body) {
                toggleBtn.addEventListener("click", function () {
                    var isHidden = body.hasAttribute("hidden");
                    if (isHidden) {
                        body.removeAttribute("hidden");
                        toggleBtn.classList.add("open");
                    } else {
                        body.setAttribute("hidden", "");
                        toggleBtn.classList.remove("open");
                    }
                });
            }

            function updateScore() {
                var count = 0;
                checks.forEach(function (c) { if (c.checked) count++; });
                var text = "";
                bands.forEach(function (b) {
                    var min = parseInt(b.getAttribute("data-min"), 10);
                    var max = parseInt(b.getAttribute("data-max"), 10);
                    if (count >= min && count <= max) text = b.textContent;
                });
                if (text && scoreText) scoreText.textContent = text;
            }

            checks.forEach(function (c) {
                c.addEventListener("change", function () {
                    updateScore();
                    saveChecklist();
                });
            });
            updateScore();
        });
    }

    /* -------------------------- CONTACT FORM ---------------------------- */
    function initContactForm() {
        var form = document.getElementById("contactForm");
        if (!form) return;
        state.form = form;
        var result = form.parentElement.querySelector("[data-result]");

        form.addEventListener("submit", function (e) {
            e.preventDefault();

            var submitBtn = form.querySelector("button[type=submit]");
            if (submitBtn) submitBtn.disabled = true;

            var params = new URLSearchParams();
            params.append("name", (form.querySelector('[name="name"]') || {}).value || "");
            params.append("email", (form.querySelector('[name="email"]') || {}).value || "");
            var tokenEl = form.querySelector('[name="__RequestVerificationToken"]');
            if (tokenEl) params.append("__RequestVerificationToken", tokenEl.value);

            fetch(form.getAttribute("action"), {
                method: "POST",
                headers: {
                    "X-Requested-With": "XMLHttpRequest",
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body: params.toString()
            })
                .then(function (r) { return r.json(); })
                .then(function (res) {
                    showResult(res.success, res.message);
                    if (res.success) {
                        state.leadId = res.leadId;
                        saveChecklist();
                        form.reset();
                    }
                })
                .catch(function () {
                    showResult(false, "Error. Please try again later.");
                })
                .finally(function () {
                    if (submitBtn) submitBtn.disabled = false;
                });
        });

        function showResult(ok, message) {
            if (!result) return;
            result.textContent = message;
            result.classList.remove("ok", "err");
            result.classList.add(ok ? "ok" : "err");
            result.removeAttribute("hidden");
        }
    }

    /* ----------------------- MOBILE MENU CLOSE -------------------------- */
    function initMobileMenuClose() {
        var nav = document.getElementById("mainNav");
        if (!nav) return;
        nav.querySelectorAll(".nav-link, .nav-cta").forEach(function (link) {
            link.addEventListener("click", function () {
                if (nav.classList.contains("show")) {
                    var toggler = document.querySelector(".navbar-toggler");
                    if (toggler) toggler.click();
                }
            });
        });
    }
})();
