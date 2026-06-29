(function () {
    "use strict";

    var currentStep = 1;

    /* ---------- INIT ---------- */
    document.addEventListener("DOMContentLoaded", function () {
        initConditionals();
        initAppTypeGuard();
        initExclusive();
        initOtherTriggers();
        initDropzones();
        initAutoSave();
        initStepper();
        initPriceWarning();
        restoreProgress();
    });

    /* ---------- STEP NAVIGATION ---------- */
    window.nextStep = function (step) {
        if (!validateStep(step)) return;
        saveStep(step, function () {
            showStep(step + 1);
        });
    };

    window.prevStep = function (step) {
        showStep(step - 1);
    };

    window.submitForm = function () {
        if (!validateStep(5)) return;
        var btn = document.getElementById("submitBtn");
        var origText = btn.textContent;
        btn.disabled = true;
        btn.textContent = window.QText.btnSending;
        saveStep(5, function () {
            window.location.href = "/questionnaire/done?token=" + document.getElementById("qToken").value;
        }, function () {
            btn.disabled = false;
            btn.textContent = origText;
        });
    };

    function showStep(step) {
        ["step1", "step2", "step3", "step4", "step5"].forEach(function (id) {
            var el = document.getElementById(id);
            if (el) el.style.display = "none";
        });

        var el = document.getElementById("step" + step);
        if (el) el.style.display = "block";

        currentStep = step;
        updateProgress(step);
        window.scrollTo({ top: 0, behavior: "smooth" });
    }

    function updateProgress(step) {
        document.querySelectorAll(".q-step-btn").forEach(function (btn) {
            var n = parseInt(btn.dataset.step, 10);
            btn.classList.remove("active", "done");
            if (n === step) btn.classList.add("active");
            else if (n < step) btn.classList.add("done");
        });
        document.querySelectorAll(".q-step-line").forEach(function (line, i) {
            line.classList.toggle("done", i < step - 1);
        });
    }

    function initStepper() {
        document.querySelectorAll(".q-step-btn").forEach(function (btn) {
            btn.addEventListener("click", function () {
                var target = parseInt(btn.dataset.step, 10);
                if (target === currentStep) return;
                if (target < currentStep) {
                    showStep(target);
                } else {
                    if (!validateStep(currentStep)) return;
                    saveStep(currentStep, function () { showStep(target); });
                }
            });
        });
    }

    /* ---------- VALIDATION ---------- */
    function validateStep(step) {
        clearErrors();
        if (step === 1) {
            var company = document.querySelector('[name="company"]');
            if (company && !company.value.trim()) {
                showError(company, window.QText.companyRequired);
                return false;
            }
        }
        return true;
    }

    function showError(input, msg) {
        input.classList.add("q-invalid");
        var err = document.createElement("p");
        err.className = "q-error-msg";
        err.textContent = msg;
        input.parentNode.insertBefore(err, input.nextSibling);
        input.scrollIntoView({ behavior: "smooth", block: "center" });
    }

    function clearErrors() {
        document.querySelectorAll(".q-invalid").forEach(function (el) { el.classList.remove("q-invalid"); });
        document.querySelectorAll(".q-error-msg").forEach(function (el) { el.remove(); });
    }

    /* ---------- SAVE STEP ---------- */
    function saveStep(step, onSuccess, onError) {
        var token = document.getElementById("qToken").value;
        var answers = collectStep(step);

        fetch("/questionnaire/save-step", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ token: token, step: step, answers: JSON.stringify(answers) })
        })
        .then(function (r) {
            if (r.ok) { if (onSuccess) onSuccess(); }
            else { if (onError) onError(); }
        })
        .catch(function () { if (onError) onError(); });
    }

    /* ---------- COLLECT ANSWERS ---------- */
    function collectStep(step) {
        var stepEl = document.getElementById("step" + step);
        if (!stepEl) return {};
        var data = {};

        stepEl.querySelectorAll("input[type=text], input[type=url], input[type=date], input[type=color], textarea, select").forEach(function (el) {
            if (!el.name || el.offsetParent === null) return;
            if (el.closest("[data-app-locked]")) return;
            data[el.name] = el.value;
        });

        var checkGroups = {};
        stepEl.querySelectorAll("input[type=checkbox]:checked").forEach(function (el) {
            if (!el.name || el.offsetParent === null) return;
            if (!checkGroups[el.name]) checkGroups[el.name] = [];
            checkGroups[el.name].push(el.value);
        });
        Object.assign(data, checkGroups);

        var radioGroups = {};
        stepEl.querySelectorAll("input[type=radio]:checked").forEach(function (el) {
            if (!el.name || el.offsetParent === null) return;
            radioGroups[el.name] = el.value;
        });
        Object.assign(data, radioGroups);

        return data;
    }

    /* ---------- AUTO SAVE ---------- */
    function initAutoSave() {
        var timer = null;
        function schedule() {
            clearTimeout(timer);
            timer = setTimeout(function () {
                var token = document.getElementById("qToken").value;
                var answers = collectStep(currentStep);
                fetch("/questionnaire/save-step", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ token: token, step: currentStep, answers: JSON.stringify(answers), isAutoSave: true })
                });
            }, 800);
        }
        document.addEventListener("input", schedule);
        document.addEventListener("change", schedule);
    }

    /* ---------- RESTORE PROGRESS ---------- */
    function restoreProgress() {
        var stage = parseInt(document.getElementById("qForm").dataset.stage || "0", 10);

        if (window.QSavedAnswers) {
            for (var s = 1; s <= 5; s++) {
                if (window.QSavedAnswers["step" + s]) {
                    restoreFormData(s, window.QSavedAnswers["step" + s]);
                }
            }
            updateProjectType();
            updateCustomerDemo();
        }

        showStep(1);
    }

    function restoreFormData(stepNum, answers) {
        if (!answers) return;
        if (typeof answers === "string") {
            try { answers = JSON.parse(answers); } catch (e) { return; }
        }
        var stepEl = document.getElementById("step" + stepNum);
        if (!stepEl) return;

        stepEl.querySelectorAll("input[type=text], input[type=url], input[type=date], input[type=color], textarea, select").forEach(function (el) {
            if (!el.name || answers[el.name] === undefined) return;
            el.value = answers[el.name];
        });

        stepEl.querySelectorAll("input[type=checkbox]").forEach(function (el) {
            if (!el.name) return;
            var val = answers[el.name];
            if (!val) return;
            var arr = Array.isArray(val) ? val : [val];
            if (arr.indexOf(el.value) !== -1) {
                el.checked = true;
                if (el.classList.contains("q-trigger-other")) {
                    var otherEl = stepEl.querySelector('[name="' + el.dataset.other + '"]');
                    if (otherEl) otherEl.style.display = "block";
                }
            }
        });

        stepEl.querySelectorAll("input[type=radio]").forEach(function (el) {
            if (!el.name || answers[el.name] === undefined) return;
            if (el.value === answers[el.name]) {
                el.checked = true;
                if (el.classList.contains("q-trigger-other")) {
                    var otherEl = stepEl.querySelector('[name="' + el.dataset.other + '"]');
                    if (otherEl) otherEl.style.display = "block";
                }
                if (el.classList.contains("q-trigger-show")) {
                    var target = document.getElementById(el.dataset.show);
                    if (target) target.style.display = "block";
                }
            }
        });

        var industrySelect = document.getElementById("q2");
        var industryOther = document.getElementById("q2other");
        if (industrySelect && industryOther && industrySelect.value === "__other__") {
            industryOther.style.display = "block";
        }
    }

    /* ---------- PRICE WARNING ---------- */
    function initPriceWarning() {
        if (!window.QText.packageName) return;
        var warned = false;
        var modal = document.getElementById("priceWarningModal");
        var confirmBtn = document.getElementById("priceWarningConfirm");
        var cancelBtn = document.getElementById("priceWarningCancel");
        var textEl = document.getElementById("priceWarningText");
        if (!modal) return;

        textEl.textContent = window.QText.packagePriceWarning;
        confirmBtn.textContent = window.QText.packagePriceWarningConfirm;
        cancelBtn.textContent = window.QText.packagePriceWarningCancel;

        var pendingCb = null;

        document.querySelectorAll("#step2 input[type=checkbox], #step3 input[type=checkbox]").forEach(function (cb) {
            cb.addEventListener("change", function () {
                if (warned) return;
                var self = this;
                var checked = self.checked;
                self.checked = !checked;
                pendingCb = function () { self.checked = checked; };
                modal.style.display = "flex";
            });
        });

        confirmBtn.addEventListener("click", function () {
            warned = true;
            if (pendingCb) { pendingCb(); pendingCb = null; }
            modal.style.display = "none";
        });

        cancelBtn.addEventListener("click", function () {
            pendingCb = null;
            modal.style.display = "none";
        });
    }

    /* ---------- APP TYPE GUARD (Q12–Q16) ---------- */
    function initAppTypeGuard() {
        document.querySelectorAll(".q-app-guarded").forEach(function (field) {
            field.addEventListener("click", function (e) {
                if (document.querySelectorAll('[name="appType"]:checked').length > 0) return;
                e.preventDefault();
                e.stopPropagation();
                window.alert(window.QText.appDisabledHint);
            }, true);
        });
    }

    /* ---------- CLEAR GROUP HELPER ---------- */
    function clearGroup(el) {
        el.querySelectorAll("input[type=checkbox], input[type=radio]").forEach(function (cb) { cb.checked = false; });
        el.querySelectorAll("input[type=text], input[type=url], input[type=date], textarea").forEach(function (inp) { inp.value = ""; });
        el.querySelectorAll("select").forEach(function (sel) { sel.selectedIndex = 0; });
        el.querySelectorAll(".q-other-input").forEach(function (ta) { ta.style.display = "none"; ta.value = ""; });
    }

    /* ---------- CONDITIONAL LOGIC ---------- */
    function initConditionals() {
        /* deadline radio: show/hide date picker */
        document.querySelectorAll("input[type=radio].q-trigger-show").forEach(function (el) {
            el.addEventListener("change", function () {
                var target = document.getElementById(el.dataset.show);
                if (target) target.style.display = el.checked ? "block" : "none";
            });
        });
        document.querySelectorAll(".q-trigger-hide").forEach(function (el) {
            el.addEventListener("change", function () {
                var target = document.getElementById(el.dataset.hide);
                if (target) { clearGroup(target); target.style.display = "none"; }
            });
        });

        /* projectType radio: controls redizajnWrap and pagesWrap sub-groups */
        document.querySelectorAll('[name="projectType"]').forEach(function (el) {
            el.addEventListener("change", updateProjectType);
        });

        /* appType checkbox: auto-clear guarded fields when all unchecked */
        document.querySelectorAll('[name="appType"]').forEach(function (el) {
            el.addEventListener("change", function () {
                if (document.querySelectorAll('[name="appType"]:checked').length === 0) {
                    document.querySelectorAll(".q-app-guarded").forEach(function (field) {
                        clearGroup(field);
                    });
                }
            });
        });

        /* customerType checkbox → customerDemoWrap */
        document.querySelectorAll('[name="customerType"]').forEach(function (el) {
            el.addEventListener("change", updateCustomerDemo);
        });

        /* industry dropdown */
        var industrySelect = document.getElementById("q2");
        var industryOther = document.getElementById("q2other");
        if (industrySelect && industryOther) {
            industrySelect.addEventListener("change", function () {
                industryOther.style.display = industrySelect.value === "__other__" ? "block" : "none";
            });
        }
    }

    function updateProjectType() {
        var selected = "";
        var checked = document.querySelector('[name="projectType"]:checked');
        if (checked) selected = checked.value;

        var redizajnWrap  = document.getElementById("redizajnWrap");
        var pagesWrap     = document.getElementById("pagesWrap");
        var pagesPortfolio = document.getElementById("pagesPortfolio");
        var pagesFull     = document.getElementById("pagesFull");

        /* pozicijski ključevi: 0=landing, 2=portfolio, 5=redizajn (isti redoslijed u svim jezicima) */
        var isLanding   = selected === "projectType_0";
        var isPortfolio = selected === "projectType_2";
        var isRedizajn  = selected === "projectType_5";
        var showFull    = !isRedizajn && !isLanding && !isPortfolio && selected !== "";

        /* redizajnWrap */
        if (redizajnWrap) {
            if (!isRedizajn) clearGroup(redizajnWrap);
            redizajnWrap.style.display = isRedizajn ? "block" : "none";
        }

        /* pagesWrap */
        if (pagesWrap) pagesWrap.style.display = (isRedizajn || isLanding || !selected) ? "none" : "block";

        /* sub-groups */
        if (pagesPortfolio) {
            if (!isPortfolio) clearGroup(pagesPortfolio);
            pagesPortfolio.style.display = isPortfolio ? "" : "none";
        }
        if (pagesFull) {
            if (!showFull) clearGroup(pagesFull);
            pagesFull.style.display = showFull ? "" : "none";
        }
    }

    function updateCustomerDemo() {
        var checked = Array.from(document.querySelectorAll('[name="customerType"]:checked')).map(function (el) { return el.value; });
        var show = checked.indexOf("customerType_0") !== -1;
        var wrap = document.getElementById("customerDemoWrap");
        if (wrap) {
            if (!show) clearGroup(wrap);
            wrap.style.display = show ? "block" : "none";
        }
    }

    /* ---------- "OSTALO" TEXT TRIGGERS ---------- */
    function initOtherTriggers() {
        document.querySelectorAll(".q-trigger-other").forEach(function (el) {
            el.addEventListener("change", function () {
                var otherId = el.dataset.other;
                var otherEl = document.querySelector('[name="' + otherId + '"]');
                if (otherEl) otherEl.style.display = el.checked ? "block" : "none";
            });
        });
    }

    /* ---------- EXCLUSIVE CHECKBOX (Nigdje drugdje / Ništa od navedenog) ---------- */
    function initExclusive() {
        document.querySelectorAll(".q-exclusive-check").forEach(function (exclusiveEl) {
            var group = exclusiveEl.name;

            exclusiveEl.addEventListener("change", function () {
                if (exclusiveEl.checked) {
                    document.querySelectorAll('[name="' + group + '"]:not(.q-exclusive-check)').forEach(function (el) {
                        el.checked = false;
                        /* hide any "ostalo" text fields */
                        var otherId = el.dataset && el.dataset.other;
                        if (otherId) {
                            var otherEl = document.querySelector('[name="' + otherId + '"]');
                            if (otherEl) otherEl.style.display = "none";
                        }
                    });
                }
            });

            document.querySelectorAll('[name="' + group + '"]:not(.q-exclusive-check)').forEach(function (el) {
                el.addEventListener("change", function () {
                    if (el.checked) exclusiveEl.checked = false;
                });
            });
        });
    }

    /* ---------- OPT-OUT ---------- */
    window.optOut = function () {
        var confirmed = window.confirm(window.QText.optOutConfirm);
        if (!confirmed) return;

        var token = document.getElementById("qToken").value;
        fetch("/questionnaire/opt-out", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ token: token })
        })
        .then(function (r) {
            if (r.ok) { window.location.href = "/"; }
            else { alert(window.QText.optOutError); }
        })
        .catch(function () { alert(window.QText.optOutError); });
    };

    /* ---------- FILE DROPZONES ---------- */
    function initDropzones() {
        document.querySelectorAll(".q-dropzone").forEach(function (zone) {
            var input = zone.querySelector(".q-dropzone-input");
            if (!input) return;

            zone.addEventListener("click", function (e) {
                if (e.target === input) return;
                input.click();
            });

            input.addEventListener("change", function () {
                if (input.files && input.files[0]) uploadFile(zone, input.files[0]);
            });

            zone.addEventListener("dragover", function (e) {
                e.preventDefault();
                zone.classList.add("q-dropzone--over");
            });

            zone.addEventListener("dragleave", function () {
                zone.classList.remove("q-dropzone--over");
            });

            zone.addEventListener("drop", function (e) {
                e.preventDefault();
                zone.classList.remove("q-dropzone--over");
                var file = e.dataTransfer && e.dataTransfer.files && e.dataTransfer.files[0];
                if (file) uploadFile(zone, file);
            });
        });
    }

    function uploadFile(zone, file) {
        var token = document.getElementById("qToken").value;
        var label = zone.dataset.label || "file";

        setZoneState(zone, "progress");

        var formData = new FormData();
        formData.append("token", token);
        formData.append("fileLabel", label);
        formData.append("file", file);

        fetch("/questionnaire/upload-file", {
            method: "POST",
            body: formData
        })
        .then(function (r) {
            if (!r.ok) throw new Error("upload_failed");
            return r.json();
        })
        .then(function (data) {
            setZoneState(zone, "idle");
            var input = zone.querySelector(".q-dropzone-input");
            if (input) input.value = "";
            if (data.fileId) {
                var label = encodeURIComponent(data.fileName || file.name).replace(/%20/g, ' ').replace(/%([0-9A-F]{2})/gi, function (m, h) { return String.fromCharCode(parseInt(h, 16)); });
                var item = document.createElement('div');
                item.className = 'q-file-item';
                item.dataset.fileId = data.fileId;
                var span = document.createElement('span');
                span.className = 'q-file-name';
                span.textContent = data.fileName || file.name;
                var btn = document.createElement('button');
                btn.type = 'button';
                btn.className = 'q-file-delete';
                btn.dataset.fileId = data.fileId;
                btn.setAttribute('aria-label', 'Obriši');
                btn.innerHTML = '&#x2715;';
                item.appendChild(span);
                item.appendChild(btn);
                zone.insertAdjacentElement('afterend', item);
            }
        })
        .catch(function () {
            setZoneState(zone, "error");
        });
    }

    function setZoneState(zone, state) {
        var idle = zone.querySelector(".q-dropzone-idle");
        var progress = zone.querySelector(".q-dropzone-progress");
        var done = zone.querySelector(".q-dropzone-done");
        var err = zone.querySelector(".q-dropzone-err");

        if (idle) idle.style.display = state === "idle" ? "" : "none";
        if (progress) progress.style.display = state === "progress" ? "" : "none";
        if (done) done.style.display = state === "done" ? "" : "none";
        if (err) err.style.display = state === "error" ? "" : "none";
    }

})();
