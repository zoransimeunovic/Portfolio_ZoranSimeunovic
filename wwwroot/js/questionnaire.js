(function () {
    "use strict";

    var currentStep = 1;

    /* ---------- INIT ---------- */
    document.addEventListener("DOMContentLoaded", function () {
        restoreProgress();
        initConditionals();
        initExclusive();
        initOtherTriggers();
        initRanking();
        initDropzones();
        showStep(1);
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
        if (!validateStep(3)) return;
        if (!validateRanking()) return;
        var btn = document.getElementById("submitBtn");
        var origText = btn.textContent;
        btn.disabled = true;
        btn.textContent = window.QText.btnSending;
        saveStep(3, function () {
            window.location.href = "/";
        }, function () {
            btn.disabled = false;
            btn.textContent = origText;
        });
    };

    function showStep(step) {
        ["step1", "step2", "step3"].forEach(function (id) {
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
        var fill = document.getElementById("progressFill");
        var label = document.getElementById("progressLabel");
        if (!fill || !label) return;
        fill.style.width = (step / 3) * 100 + "%";
        label.textContent = window.QText.progressPattern.replace("{0}", step);
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

    function validateRanking() {
        var selects = document.querySelectorAll(".q-rank-select");
        var values = [];
        selects.forEach(function (s) { if (s.value) values.push(s.value); });
        var errEl = document.getElementById("rankError");
        if (values.length > 0 && (values.length < 3 || new Set(values).size < 3)) {
            if (errEl) errEl.style.display = "block";
            return false;
        }
        if (errEl) errEl.style.display = "none";
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
        var rankErr = document.getElementById("rankError");
        if (rankErr) rankErr.style.display = "none";
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

        stepEl.querySelectorAll("input[type=text], input[type=url], input[type=date], textarea, select").forEach(function (el) {
            if (el.name && el.offsetParent !== null) data[el.name] = el.value;
        });

        var checkGroups = {};
        stepEl.querySelectorAll("input[type=checkbox]:checked").forEach(function (el) {
            if (!el.name) return;
            if (!checkGroups[el.name]) checkGroups[el.name] = [];
            checkGroups[el.name].push(el.value);
        });
        Object.assign(data, checkGroups);

        var radioGroups = {};
        stepEl.querySelectorAll("input[type=radio]:checked").forEach(function (el) {
            if (el.name) radioGroups[el.name] = el.value;
        });
        Object.assign(data, radioGroups);

        return data;
    }

    /* ---------- RESTORE PROGRESS ---------- */
    function restoreProgress() {
        var stage = parseInt(document.getElementById("qForm").dataset.stage || "0", 10);
        if (stage > 0) showStep(Math.min(stage + 1, 3));
    }

    /* ---------- CONDITIONAL LOGIC ---------- */
    function initConditionals() {
        /* radio: show target when this radio selected */
        document.querySelectorAll(".q-trigger-show").forEach(function (el) {
            el.addEventListener("change", function () {
                var target = document.getElementById(el.dataset.show);
                if (target) target.style.display = el.checked ? "block" : "none";
            });
        });

        /* radio: hide target when this radio selected */
        document.querySelectorAll(".q-trigger-hide").forEach(function (el) {
            el.addEventListener("change", function () {
                var target = document.getElementById(el.dataset.hide);
                if (target) target.style.display = "none";
            });
        });

        /* hasWebsite radio: show Q6 (websiteDescWrap) when Da */
        document.querySelectorAll('[name="hasWebsite"]').forEach(function (el) {
            el.addEventListener("change", function () {
                var wrap = document.getElementById("websiteDescWrap");
                if (wrap) wrap.style.display = el.value === "Da" && el.checked ? "block" : "none";
            });
        });

        /* websiteType radio: hide pagesWrap when "Nemam potrebu" */
        document.querySelectorAll('[name="websiteType"]').forEach(function (el) {
            el.addEventListener("change", function () {
                var wrap = document.getElementById("pagesWrap");
                if (!wrap) return;
                wrap.style.display = el.value === "Nemam potrebu za web stranicom" ? "none" : "block";
            });
        });

        /* checkbox show: e.g. Web aplikacija → webAppWrap */
        document.querySelectorAll("input[type=checkbox].q-trigger-show").forEach(function (el) {
            el.addEventListener("change", function () {
                var target = document.getElementById(el.dataset.show);
                if (target) target.style.display = el.checked ? "block" : "none";
            });
        });

        /* customerType checkbox → customerDemoWrap (show if Privatne or I privatne i firme) */
        document.querySelectorAll('[name="customerType"]').forEach(function (el) {
            el.addEventListener("change", updateCustomerDemo);
        });

        /* industry dropdown: show text field for "Ostalo" */
        var industrySelect = document.getElementById("q2");
        var industryOther = document.getElementById("q2other");
        if (industrySelect && industryOther) {
            industrySelect.addEventListener("change", function () {
                industryOther.style.display = industrySelect.value === "__other__" ? "block" : "none";
            });
        }
    }

    function updateCustomerDemo() {
        var checked = Array.from(document.querySelectorAll('[name="customerType"]:checked')).map(function (el) { return el.value; });
        var show = checked.includes("Privatne osobe") || checked.includes("I privatne osobe i firme");
        var wrap = document.getElementById("customerDemoWrap");
        if (wrap) wrap.style.display = show ? "block" : "none";
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
            if (r.ok) {
                ["step1", "step2", "step3", "stepDone"].forEach(function (id) {
                    var el = document.getElementById(id);
                    if (el) el.style.display = "none";
                });
                var wrap = document.querySelector(".q-container");
                if (wrap) {
                    var msg = document.createElement("div");
                    msg.className = "q-opt-out-done";
                    msg.innerHTML = window.QText.optOutDoneHtml;
                    wrap.appendChild(msg);
                }
            } else {
                alert(window.QText.optOutError);
            }
        })
        .catch(function () { alert(window.QText.optOutError); });
    };

    /* ---------- RANKING VALIDATION ---------- */
    function initRanking() {
        document.querySelectorAll(".q-rank-select").forEach(function (sel) {
            sel.addEventListener("change", function () {
                validateRanking();
            });
        });
    }

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
            var nameEl = zone.querySelector(".q-dropzone-filename");
            if (nameEl) nameEl.textContent = data.fileName || file.name;
            setZoneState(zone, "done");
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
