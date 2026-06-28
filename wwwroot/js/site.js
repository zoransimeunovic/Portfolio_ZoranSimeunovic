(function () {
    "use strict";

    // Dijeljeno stanje: postavljeno nakon uspjesnog submit-a kontakt forme
    var state = { leadId: null, form: null };

    document.addEventListener("DOMContentLoaded", function () {
        initHeroScale();
        initCarousel();
        initChecklists();
        initContactForm();
        initMobileMenuClose();
        initHomeLinks();
        initProcessToggle();
    });

    /* Sekcije gradjene kao apsolutne 1440px Figma scene (.hero-stage,
       .fig-stage) skaliraju se proporcionalno prema sirini da izgledaju
       identicno Figmi na svim ekranima. */
    function initHeroScale() {
        var stages = [];
        var hero = document.querySelector(".hero-stage");
        if (hero) stages.push({ el: hero, v: "--hero-scale" });
        document.querySelectorAll(".fig-stage").forEach(function (el) {
            stages.push({ el: el, v: "--fig-scale" });
        });
        if (stages.length === 0) return;

        function apply() {
            stages.forEach(function (s) {
                var scale = Math.min(s.el.parentElement.clientWidth / 1440, 1);
                s.el.style.setProperty(s.v, scale);
            });
        }

        apply();
        var t;
        window.addEventListener("resize", function () {
            clearTimeout(t);
            t = setTimeout(apply, 100);
        });
    }

    function initCarousel() {
        var root = document.querySelector("[data-carousel]");
        if (!root) return;

        var track = root.querySelector("[data-track]");
        var prevBtn = root.querySelector("[data-prev]");
        var nextBtn = root.querySelector("[data-next]");
        if (!track) return;

        var realCards = Array.prototype.slice.call(track.children);
        var n = realCards.length;
        if (n === 0) return;

        // Kloniramo sve kartice na pocetak i kraj — omogucava beskonacni loop bez skoka
        for (var i = n - 1; i >= 0; i--) {
            var pre = realCards[i].cloneNode(true);
            pre.setAttribute("aria-hidden", "true");
            track.insertBefore(pre, track.firstChild);
        }
        realCards.forEach(function (card) {
            var post = card.cloneNode(true);
            post.setAttribute("aria-hidden", "true");
            track.appendChild(post);
        });

        var index = n; // pocinjemo na prvoj pravoj kartici (preskacemo prepended clone-ove)
        var busy = false;

        function perView() {
            var w = window.innerWidth;
            if (w <= 640) return 1;
            if (w <= 991) return 2;
            return 3;
        }

        function step() {
            var style = window.getComputedStyle(track);
            var gap = parseFloat(style.columnGap || style.gap || "0") || 0;
            return realCards[0].getBoundingClientRect().width + gap;
        }

        function setPos(i, animate) {
            if (!animate) {
                track.style.transition = "none";
                track.style.transform = "translateX(" + (-i * step()) + "px)";
                track.getBoundingClientRect();
                track.style.transition = "";
            } else {
                track.style.transform = "translateX(" + (-i * step()) + "px)";
            }
        }

        track.addEventListener("transitionend", function () {
            if (index >= 2 * n) { index -= n; setPos(index, false); }
            if (index < n)      { index += n; setPos(index, false); }
            busy = false;
        });

        function navigate(dir) {
            if (busy) return;
            busy = true;
            index += dir;
            setPos(index, true);
        }

        if (prevBtn) prevBtn.addEventListener("click", function () { navigate(-1); });
        if (nextBtn) nextBtn.addEventListener("click", function () { navigate(1); });

        var touchStartX = 0;
        track.addEventListener("touchstart", function (e) { touchStartX = e.touches[0].clientX; }, { passive: true });
        track.addEventListener("touchend", function (e) {
            var diff = touchStartX - e.changedTouches[0].clientX;
            if (Math.abs(diff) > 50) navigate(diff > 0 ? 1 : -1);
        });

        var resizeTimer;
        window.addEventListener("resize", function () {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(function () { setPos(index, false); }, 150);
        });

        setPos(index, false);
    }

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

    function initChecklists() {
        var lists = document.querySelectorAll("[data-checklist]");
        lists.forEach(function (list) {
            var toggleBtn = list.querySelector("[data-toggle-list]");
            var body = list.querySelector("[data-body]");
            var checks = list.querySelectorAll("[data-check]");
            var bands = list.querySelectorAll(".score-band");
            var scoreText = list.querySelector("[data-score-text]");
            var scoreBox = list.querySelector("[data-score]");
            var analyzeBtn = list.querySelector("[data-analyze]");
            var revealed = false;

            if (toggleBtn && body) {
                var toggleLabel = toggleBtn.querySelector("span:first-child");
                var textOpen = toggleBtn.dataset.open || "";
                var textClose = toggleBtn.dataset.close || "";
                toggleBtn.addEventListener("click", function () {
                    var isHidden = body.hasAttribute("hidden");
                    if (isHidden) {
                        body.removeAttribute("hidden");
                        toggleBtn.classList.add("open");
                        if (toggleLabel && textClose) toggleLabel.textContent = textClose;
                    } else {
                        body.setAttribute("hidden", "");
                        toggleBtn.classList.remove("open");
                        if (toggleLabel && textOpen) toggleLabel.textContent = textOpen;
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
                if (scoreText) scoreText.textContent = text;
            }

            if (analyzeBtn) {
                analyzeBtn.addEventListener("click", function () {
                    revealed = true;
                    updateScore();
                    if (scoreBox) scoreBox.removeAttribute("hidden");
                    saveChecklist();
                });
            }

        });
    }

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

    function initHomeLinks() {
        document.querySelectorAll('a[href$="#home"]').forEach(function (a) {
            a.addEventListener("click", function (e) {
                var linkPath = new URL(a.href, window.location.href).pathname;
                if (linkPath !== window.location.pathname) return;
                e.preventDefault();
                window.scrollTo({ top: 0, behavior: "smooth" });
            });
        });
    }

    function initProcessToggle() {
        var btn = document.querySelector("[data-process-toggle]");
        if (!btn) return;
        var grid = document.querySelector(".process-grid");
        var label = btn.querySelector("span:first-child");
        var textShow = btn.dataset.show || label.textContent;
        var textHide = btn.dataset.hide || label.textContent;
        btn.addEventListener("click", function () {
            var isOpen = btn.classList.contains("open");
            if (isOpen) {
                grid.classList.remove("expanded");
                label.textContent = textShow;
                btn.classList.remove("open");
            } else {
                grid.classList.add("expanded");
                label.textContent = textHide;
                btn.classList.add("open");
            }
        });
    }

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
