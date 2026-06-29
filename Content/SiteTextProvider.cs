namespace Portfolio_ZoranSimeunovic.Content;

public static class SiteTextProvider
{
    public static SiteText Get(string culture)
    {
        var c = (culture ?? "en").ToLowerInvariant();
        if (c.StartsWith("de")) return BuildDe();
        if (c.StartsWith("sr")) return BuildSr();
        return BuildEn();
    }

    // ----------------------------------------------------------------- ENGLISH
    private static SiteText BuildEn() => new()
    {
        Culture = "en",
        PageTitle = "Custom Web Development | Zoran Simeunovic",
        LanguageLabel = "Language",
        Nav = new NavText { Home = "HOME", Work = "WORK", Process = "PROCESS", Pricing = "PRICING", ContactMe = "CONTACT ME", MyQuestionnaire = "MY QUESTIONNAIRE" },
        Hero = new HeroText
        {
            Badge = "Full Stack Web Developer",
            HeadingHtml = "<span class=\"accent\">Web solutions</span> that help you grow",
            Subtitle1 = "Your business deserves a professional web presence.",
            Subtitle2 = "Let's build it together.",
            ContactMe = "CONTACT ME",
            MyWork = "MY WORK",
            ProjectsCount = "5+ WEB AND DESKTOP CUSTOMERS",
            Name = "Zoran Simeunović"
        },
        Work = new WorkSection
        {
            Label = "WORK",
            TitleHtml = "Featured <span class=\"accent\">work</span>",
            Subtitle = "Real-world business applications, web platforms and backend systems",
            PrevLabel = "Previous",
            NextLabel = "Next",
            Cards = new List<WorkCard>
            {
                new() { Title = "HRIS/Web", Image = "hris.webp", Duration = "2 years", DurationSub = "at AddWare Solutions", DurationUrl = "https://www.addware.de/urlaubsmanager/",
                    Description = "Development and maintenance of an HRIS (Human Resources Information System) for workforce management, including personnel planning, vacation management, and time tracking. Integration of web services, sync services, and Microsoft 365. Optimization of existing systems and development of new modules.",
                    Techs = new() { "ASP.NET", "C#", "MySQL", "Microsoft Graph" } },
                new() { Title = "Desktop GUI", Image = "desktop-gui.webp", Duration = "3 years", DurationSub = "at AddWare Solutions", DurationUrl = "https://www.addware.de/urlaubsmanager/",
                    Description = "Complete redesign of a desktop GUI focused on performance and scalability. Enabled fast rendering of large employee datasets in grid tables with 12+ months of historical data. Improved business logic, optimized large data handling, and delivered new and enhanced modules.",
                    Techs = new() { "C#", "WPF", "MySQL" } },
                new() { Title = "Zeiterfassung", Image = "zeiterfassung.webp", Duration = "1 year", DurationSub = "at AddWare Solutions", DurationUrl = "https://www.addware.de/zeiterfassung/",
                    Description = "Development and enhancement of desktop, web, and mobile clients for time tracking, providing real-time data visualization and centralized management of working hours, breaks, and absences within a unified system.",
                    Techs = new() { "ASP.NET", "WPF", "Xamarin Forms" } },
                new() { Title = "Personal Portfolio", Image = "personal-portfolio.webp", Duration = null, DurationSub = null,
                    Description = "Website designed and developed from scratch to showcase my work, skills, and experience, using Figma and Webflow.",
                    Techs = new() { "ASP.NET", "MySQL", "JS" } }
            }
        },
        About = new AboutText
        {
            Label = "ABOUT ME",
            Name = "Zoran Simeunović",
            HeadingHtml = "Who am <span class=\"accent\">I?</span>",
            Description = "I build scalable full-stack websites and web applications using Figma, Webflow, ASP.NET and MySQL. I love clean code and fast UIs.",
            Highlight1Html = "<span class=\"accent\">6 Years</span> Enterprise Experience",
            Highlight2Html = "<span class=\"accent\">Web,</span> Desktop & Mobile Applications",
            Highlight3Html = "Websites & <span class=\"accent\">Web</span> Solutions",
            GoPlayer = "Go Player",
            ContactMe = "CONTACT ME",
            DownloadCv = "DOWNLOAD CV"
        },
        Process = new ProcessSection
        {
            Label = "PROCESS",
            TitleHtml = "How we work together",
            ContactMe = "CONTACT ME",
            FindMe = "or find me on LinkedIn, Xing, GitHub or email",
            ShowAll = "Show all processes",
            HideAll = "Hide processes",
            Steps = new List<ProcessStep>
            {
                new() { Tag = "FIRST STEP" },
                new() { Tag = "100% VIA EMAIL", Title = "Project questionnaire", Description = "Your business, Current online presence, Project goals, Type of project, Timeline, Design preferences" },
                new() { Tag = "FREE", Title = "Offer", Description = "I review your answers, ask any follow-up questions, and send a detailed offer." },
                new() { Tag = "BEFORE WORK", Title = "Project agreement", Description = "Clear scope, price and milestones in writing. No surprises." },
                new() { Tag = "YOUR APPROVAL REQUIRED", Title = "Design preview", Description = "See the full design first. Nothing gets built without your approval." },
                new() { Tag = "BUILDING YOUR SITE", Title = "Website", Description = "The approved design becomes a fast, working website. No silence, you'll always know where things stand." },
                new() { Tag = "TIME TO REVIEW", Title = "Website review", Description = "You test the website and request changes if needed." },
                new() { Tag = "YOUR APP", Title = "Web App (optional)", Description = "Website approved. Custom tools built behind the scenes. Portals, bookings, dashboards." },
                new() { Tag = "TIME TO REVIEW", Title = "Web App review (optional)", Description = "You test everything end to end. Request changes if needed." },
                new() { Tag = "GO LIVE", Title = "Launch & handover", Description = "Deploy, docs, and handover. Final invoice on delivery. All bugs resulting from development are fixed free of charge - 30 days for websites, 60 days for web applications." },
                new() { Tag = "FLEXIBLE", Title = "Ongoing support (optional)", Description = "On your terms. Hosting, SSL, backups, bug fixes, or new features - take only what your business needs." }
            }
        },
        Improve = new ImproveSection
        {
            TitleHtml = "How to improve <span class=\"accent\">web presence?</span>",
            Subtitle1 = "Do you need a redesign, a new website, or a web app?",
            Subtitle2 = "Get a quick analysis.",
            StartAnalysis = "Open checklist",
            CloseChecklist = "Close checklist",
            Analyze = "Analyze",
            Lists = new List<ImproveList>
            {
                new()
                {
                    Key = "redesign",
                    HeadingHtml = "Does your website need <span class=\"accent\">redesign</span>?",
                    Groups = new()
                    {
                        new() { Title = "Analysis", Items = new() { "Is the website visually outdated?", "Is the mobile version poor or missing?", "Is the navigation confusing?", "Is it immediately clear what the company does?" } },
                        new() { Title = "Design", Items = new() { "Are the colors and fonts consistent?", "Is there a clear visual hierarchy (headings, sections)?", "Are the CTA buttons visible and clear?", "Is there too much text without enough whitespace?" } },
                        new() { Title = "UX / Functionality", Items = new() { "Is contact information easy to find?", "Does the website load quickly?", "Can users find information they need within 5 sec?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Solid and functional. Redesign likely not needed." },
                        new() { Min = 3, Max = 5, Text = "Some weaknesses. Minor improvements or optimization recommended." },
                        new() { Min = 6, Max = 8, Text = "Outdated or inefficient. Redesign recommended." },
                        new() { Min = 9, Max = 11, Text = "Poor condition. Complete redesign recommended." }
                    }
                },
                new()
                {
                    Key = "website",
                    HeadingHtml = "Do you need a <span class=\"accent\">website</span>?",
                    Groups = new()
                    {
                        new() { Title = "Basics", Items = new() { "Do clients search for your business online?", "Is your web presence limited to Instagram or Facebook?", "Are you losing potential clients because they can't find you on Google?" } },
                        new() { Title = "Need", Items = new() { "Do you receive inquiries through messages instead of a contact form?", "Do you often explain the same information to clients repeatedly?", "Do you offer services or pricing that should be publicly available online?" } },
                        new() { Title = "Readiness", Items = new() { "Would you like your business to have a more professional web presence?", "Would you like clients to find you automatically through Google?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Your current web presence may be sufficient. A website could still help, but it's not urgent." },
                        new() { Min = 3, Max = 5, Text = "Your web presence has gaps. A simple, professional website would help clients find you." },
                        new() { Min = 6, Max = 7, Text = "Clients are likely struggling to find you. A stronger web presence would make a clear difference." },
                        new() { Min = 8, Max = 9, Text = "You're missing out on clients every day. A professional website is the next logical step." }
                    }
                },
                new()
                {
                    Key = "automation",
                    HeadingHtml = "Do you need <span class=\"accent\">automation</span>?",
                    Groups = new()
                    {
                        new() { Title = "Problems", Items = new() { "Do you use Excel or paper to manage your data?", "Do you manually send the same emails or messages?", "Do you have repetitive tasks that take up too much time?" } },
                        new() { Title = "Processes", Items = new() { "Do you schedule appointments manually?", "Do you track customers, orders, or inventory manually?", "Do multiple people work with the same data without system?" } },
                        new() { Title = "Potential For A Web Application", Items = new() { "Would a system save you at least 1-2 hours per day?", "Would automation reduce errors?", "Would you like a central place for all your data?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Your current processes seem manageable. Automation may not be a priority right now." },
                        new() { Min = 3, Max = 5, Text = "Some tasks could be automated. Small improvements would save time and reduce errors." },
                        new() { Min = 6, Max = 7, Text = "Manual work is slowing you down. A web application would streamline your business significantly." },
                        new() { Min = 8, Max = 9, Text = "Your business is losing time and money on manual processes. A custom web app would pay for itself quickly." }
                    }
                }
            }
        },
        Pricing = new PricingSection
        {
            Label = "PRICING",
            Title = "How much does a website cost?",
            Subtitle = "No templates - every website is built specifically for your business.",
            GetStarted = "GET YOUR QUOTE",
            Badges = new() { "Fixed price", "Warranty included" },
            BadgeRecommended = "Recommended",
            CtaHeading = "Can't find what you need?",
            CtaText = "Request a custom quote for desktop apps, mobile apps, hosting, maintenance, and/or projects with many different features.",
            CtaButton = "Request a quote",

            WarrantyUnit = "day warranty",
            PrevLabel = "Previous",
            NextLabel = "Next",
            Cards = new List<PricingCard>
            {
                new() { Name = "Landing Page", Price = "399€", WarrantyDays = 30, Features = new() { "Single page, single goal", "Contact form", "Responsive design", "SEO basics", "GDPR implementation" } },
                new() { Name = "Presentation Website", Price = "799€", WarrantyDays = 30, Features = new() { "Up to 5 pages", "Contact form + location map", "WhatsApp button", "Responsive design", "SEO basics", "GDPR implementation" } },
                new() { Name = "Website with Content Management", Price = "1,199€", WarrantyDays = 30, IsRecommended = true, Features = new() { "Up to 8 pages", "Blog and gallery", "Multilingual support", "Client manages content themselves", "WhatsApp button", "Responsive design", "SEO basics", "GDPR implementation" } },
                new() { Name = "Website + Interactive Tool", Price = "1,599€", WarrantyDays = 60, Features = new() { "Up to 5 pages", "Calculator, configurator or form", "Admin panel", "Responsive design", "SEO basics", "GDPR implementation" } },
                new() { Name = "Website + Bookings / Portal", Price = "2,199€", WarrantyDays = 60, Features = new() { "Up to 5 pages", "Online bookings or client portal", "Automated email messages", "Admin panel", "Responsive design", "SEO basics", "GDPR implementation" } },
                new() { Name = "Web Application", Price = "2,799€", WarrantyDays = 60, Features = new() { "Up to 8 pages", "Client portal", "Online bookings and/or newsletter", "Automated email messages", "Admin panel", "Visitor statistics", "Responsive design", "SEO basics", "GDPR implementation" } },
            }
        },
        Contact = new ContactSection
        {
            TitleHtml = "Take the first step",
            Subtitle1 = "Leave your name and email, I'll send you",
            Subtitle2 = "a short questionnaire to get started",
            NamePlaceholder = "Your name",
            EmailPlaceholder = "Your email address",
            GetStarted = "GET STARTED",
            PrivacyNoteHtml = "By submitting, you agree to our <a href=\"/Home/Privacy\">Privacy Policy</a>. We respect your privacy.",
            SuccessMessage = "Thank you! Your details have been received. I'll be in touch soon.",
            ErrorMessage = "Something went wrong. Please try again later."
        },
        Footer = new FooterText
        {
            Home = "HOME", Work = "WORK", Process = "PROCESS", Pricing = "PRICING",
            Copyright = "© 2026 ZS.dev All rights reserved.",
            PrivacyPolicy = "Privacy Policy", TermsOfService = "Terms of Service", CookieSettings = "Cookie Settings"
        },
        Cookie = new CookieText
        {
            Title = "Cookie Settings",
            Body = "This website does not use any cookies. No tracking, no analytics, no third-party cookies. Your privacy is fully respected.",
            Close = "Got it"
        },
        Legal = new LegalText
        {
            BackHome = "← Back to home",
            PrivacyTitle = "Privacy Policy",
            PrivacyHtml = @"<p>This Privacy Policy explains how Zoran Simeunović / ZS.dev (""I"", ""me"") processes personal data collected through this website.</p>
<h3>Data I collect</h3><p>When you submit the contact form, I collect your name and email address. No other personal data is collected. No tracking pixels, analytics tools, or advertising services are used.</p>
<h3>How I use your data</h3><p>Your name and email are used to: (1) send you a confirmation email to verify your address, (2) send you a short project questionnaire, and (3) follow up on your inquiry. Your data is never sold or shared with third parties, except as described below.</p>
<h3>Email delivery</h3><p>Transactional emails (confirmation, questionnaire link) are sent via <strong>Brevo</strong> (Sendinblue SAS, 55 rue d'Amsterdam, 75008 Paris, France), a GDPR-compliant email delivery service. Your name and email address are transmitted to Brevo solely for the purpose of sending these emails. Brevo's privacy policy: <a href=""https://www.brevo.com/legal/privacypolicy/"">brevo.com/legal/privacypolicy</a>.</p>
<h3>Cookies</h3><p>This website uses one essential cookie (<code>q_ref</code>) to maintain your questionnaire session. This cookie is set only after you open your questionnaire link and expires after 30 days or when you complete the questionnaire. No tracking or advertising cookies are used.</p>
<h3>Storage</h3><p>Your data is stored on a secured server within the EU. It is kept only as long as necessary to handle your request and is deleted upon your request.</p>
<h3>Legal basis</h3><p>Processing is based on your consent given by voluntarily submitting the contact form (Art. 6(1)(a) GDPR).</p>
<h3>Your rights</h3><p>Under GDPR you have the right to access, correct, delete, or restrict processing of your data. To exercise these rights, email <a href=""mailto:zoran.simeunovic@outlook.de"">zoran.simeunovic@outlook.de</a>. You also have the right to lodge a complaint with a supervisory authority.</p>
<p class=""legal-updated"">Last updated: June 2026.</p>",
            TermsTitle = "Terms of Service",
            TermsHtml = @"<p>By using this website you agree to the following terms.</p>
<h3>Purpose</h3><p>This website is a personal portfolio presenting the work and services of Zoran Simeunović (ZS.dev).</p>
<h3>Content</h3><p>All content, text, and images are the property of ZS.dev unless otherwise stated and may not be reused without permission.</p>
<h3>No warranty</h3><p>The website is provided ""as is"" without warranties of any kind. Information may change without notice.</p>
<h3>Contact form</h3><p>By submitting the contact form you confirm the information provided is accurate. Submitting does not create any binding agreement.</p>
<h3>Liability</h3><p>ZS.dev is not liable for any damages arising from the use of this website.</p>
<p class=""legal-updated"">Last updated: June 2026.</p>"
        },
        Questionnaire = BuildEnQuestionnaire()
    };

    // ------------------------------------------------------------------ GERMAN
    private static SiteText BuildDe() => new()
    {
        Culture = "de",
        PageTitle = "Webentwicklung nach Maß | Zoran Simeunovic",
        LanguageLabel = "Sprache",
        Nav = new NavText { Home = "START", Work = "PROJEKTE", Process = "ABLAUF", Pricing = "PREISE", ContactMe = "KONTAKT", MyQuestionnaire = "MEIN FRAGEBOGEN" },
        Hero = new HeroText
        {
            Badge = "Full-Stack Webentwickler",
            HeadingHtml = "<span class=\"accent\">Web-Lösungen</span> für Ihr Wachstum",
            Subtitle1 = "Ihr Unternehmen verdient einen professionellen Webauftritt.",
            Subtitle2 = "Lassen Sie ihn uns gemeinsam aufbauen.",
            ContactMe = "KONTAKT",
            MyWork = "MEINE ARBEIT",
            ProjectsCount = "5+ WEB- UND DESKTOP-KUNDEN",
            Name = "Zoran Simeunović"
        },
        Work = new WorkSection
        {
            Label = "PROJEKTE",
            TitleHtml = "Ausgewählte <span class=\"accent\">Projekte</span>",
            Subtitle = "Praxisnahe Geschäftsanwendungen, Webplattformen und Backend-Systeme",
            PrevLabel = "Zurück",
            NextLabel = "Weiter",
            Cards = new List<WorkCard>
            {
                new() { Title = "HRIS/Web", Image = "hris.webp", Duration = "2 Jahre", DurationSub = "bei AddWare Solutions", DurationUrl = "https://www.addware.de/urlaubsmanager/",
                    Description = "Entwicklung und Wartung eines HRIS (Human Resources Information System) für die Personal­verwaltung, einschließlich Personalplanung, Urlaubsverwaltung und Zeiterfassung. Integration von Webdiensten, Synchronisationsdiensten und Microsoft 365. Optimierung bestehender Systeme und Entwicklung neuer Module.",
                    Techs = new() { "ASP.NET", "C#", "MySQL", "Microsoft Graph" } },
                new() { Title = "Desktop GUI", Image = "desktop-gui.webp", Duration = "3 Jahre", DurationSub = "bei AddWare Solutions", DurationUrl = "https://www.addware.de/urlaubsmanager/",
                    Description = "Komplette Neugestaltung einer Desktop-GUI mit Fokus auf Leistung und Skalierbarkeit. Ermöglichte das schnelle Rendern großer Mitarbeiterdatensätze in Tabellen mit über 12 Monaten Verlaufsdaten. Verbesserte Geschäftslogik, optimierte Verarbeitung großer Datenmengen und Bereitstellung neuer und erweiterter Module.",
                    Techs = new() { "C#", "WPF", "MySQL" } },
                new() { Title = "Zeiterfassung", Image = "zeiterfassung.webp", Duration = "1 Jahr", DurationSub = "bei AddWare Solutions", DurationUrl = "https://www.addware.de/zeiterfassung/",
                    Description = "Entwicklung und Erweiterung von Desktop-, Web- und mobilen Clients zur Zeiterfassung mit Echtzeit-Datenvisualisierung und zentraler Verwaltung von Arbeitszeiten, Pausen und Abwesenheiten in einem einheitlichen System.",
                    Techs = new() { "ASP.NET", "WPF", "Xamarin Forms" } },
                new() { Title = "Personal Portfolio", Image = "personal-portfolio.webp", Duration = null, DurationSub = null,
                    Description = "Von Grund auf gestaltete und entwickelte Website, um meine Arbeit, Fähigkeiten und Erfahrung zu präsentieren – mit Figma und Webflow.",
                    Techs = new() { "ASP.NET", "MySQL", "JS" } }
            }
        },
        About = new AboutText
        {
            Label = "ÜBER MICH",
            Name = "Zoran Simeunović",
            HeadingHtml = "Wer bin <span class=\"accent\">ich?</span>",
            Description = "Ich entwickle skalierbare Full-Stack-Websites und Webanwendungen mit Figma, Webflow, ASP.NET und MySQL. Ich liebe sauberen Code und schnelle Benutzeroberflächen.",
            Highlight1Html = "<span class=\"accent\">6 Jahre</span> Unternehmenserfahrung",
            Highlight2Html = "<span class=\"accent\">Web-,</span> Desktop- & mobile Anwendungen",
            Highlight3Html = "Websites & <span class=\"accent\">Web</span>-Lösungen",
            GoPlayer = "Go-Spieler",
            ContactMe = "KONTAKT",
            DownloadCv = "LEBENSLAUF"
        },
        Process = new ProcessSection
        {
            Label = "ABLAUF",
            TitleHtml = "Wie wir zusammenarbeiten",
            ContactMe = "KONTAKT",
            FindMe = "oder finden Sie mich auf LinkedIn, Xing, GitHub oder per E-Mail",
            ShowAll = "Alle Prozesse anzeigen",
            HideAll = "Prozesse verbergen",
            Steps = new List<ProcessStep>
            {
                new() { Tag = "ERSTER SCHRITT" },
                new() { Tag = "100% PER E-MAIL", Title = "Projekt-Fragebogen", Description = "Ihr Unternehmen, aktuelle Online-Präsenz, Projektziele, Projektart, Zeitrahmen, Designwünsche" },
                new() { Tag = "KOSTENLOS", Title = "Angebot", Description = "Ich prüfe Ihre Antworten, stelle eventuelle Rückfragen und sende ein detailliertes Angebot." },
                new() { Tag = "VOR ARBEITSBEGINN", Title = "Projektvereinbarung", Description = "Klarer Umfang, Preis und Meilensteine schriftlich. Keine Überraschungen." },
                new() { Tag = "IHRE FREIGABE ERFORDERLICH", Title = "Design-Vorschau", Description = "Sehen Sie zuerst das vollständige Design. Es wird nichts ohne Ihre Freigabe gebaut." },
                new() { Tag = "AUFBAU IHRER SEITE", Title = "Website", Description = "Das freigegebene Design wird zu einer schnellen, funktionierenden Website. Kein Schweigen – Sie wissen jederzeit, wo es steht." },
                new() { Tag = "ZEIT ZUM PRÜFEN", Title = "Website-Prüfung", Description = "Sie testen die Website und fordern bei Bedarf Änderungen an." },
                new() { Tag = "IHRE APP", Title = "Web-App (optional)", Description = "Website freigegeben. Maßgeschneiderte Tools im Hintergrund. Portale, Buchungen, Dashboards." },
                new() { Tag = "ZEIT ZUM PRÜFEN", Title = "Web-App-Prüfung (optional)", Description = "Sie testen alles von Anfang bis Ende. Fordern Sie bei Bedarf Änderungen an." },
                new() { Tag = "GO-LIVE", Title = "Launch & Übergabe", Description = "Bereitstellung, Dokumentation und Übergabe. Schlussrechnung bei Lieferung. Alle Fehler, die auf die Entwicklung zurückzuführen sind, werden kostenlos behoben - 30 Tage für Websites, 60 Tage für Web-Anwendungen." },
                new() { Tag = "FLEXIBLE", Title = "Laufender Support (optional)", Description = "Zu Ihren Bedingungen. Hosting, SSL, Backups, Fehlerbehebungen oder neue Funktionen – nur was Ihr Unternehmen braucht." }
            }
        },
        Improve = new ImproveSection
        {
            TitleHtml = "Wie Sie Ihre <span class=\"accent\">Web-Präsenz verbessern?</span>",
            Subtitle1 = "Brauchen Sie ein Redesign, eine neue Website oder eine Web-App?",
            Subtitle2 = "Erhalten Sie eine schnelle Analyse.",
            StartAnalysis = "Checkliste öffnen",
            CloseChecklist = "Checkliste schließen",
            Analyze = "Analysieren",
            Lists = new List<ImproveList>
            {
                new()
                {
                    Key = "redesign",
                    HeadingHtml = "Braucht Ihre Website ein <span class=\"accent\">Redesign</span>?",
                    Groups = new()
                    {
                        new() { Title = "Analyse", Items = new() { "Ist die Website optisch veraltet?", "Ist die mobile Version schlecht oder fehlt sie?", "Ist die Navigation verwirrend?", "Ist sofort klar, was das Unternehmen macht?" } },
                        new() { Title = "Design", Items = new() { "Sind Farben und Schriften einheitlich?", "Gibt es eine klare visuelle Hierarchie (Überschriften, Abschnitte)?", "Sind die CTA-Buttons sichtbar und klar?", "Gibt es zu viel Text ohne genügend Weißraum?" } },
                        new() { Title = "UX / Funktionalität", Items = new() { "Sind die Kontaktinformationen leicht zu finden?", "Lädt die Website schnell?", "Finden Nutzer die benötigten Informationen innerhalb von 5 Sekunden?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Solide und funktional. Ein Redesign ist wahrscheinlich nicht nötig." },
                        new() { Min = 3, Max = 5, Text = "Einige Schwächen. Kleinere Verbesserungen oder Optimierungen empfohlen." },
                        new() { Min = 6, Max = 8, Text = "Veraltet oder ineffizient. Ein Redesign wird empfohlen." },
                        new() { Min = 9, Max = 11, Text = "Schlechter Zustand. Ein komplettes Redesign wird empfohlen." }
                    }
                },
                new()
                {
                    Key = "website",
                    HeadingHtml = "Brauchen Sie eine <span class=\"accent\">Website</span>?",
                    Groups = new()
                    {
                        new() { Title = "Grundlagen", Items = new() { "Suchen Kunden online nach Ihrem Unternehmen?", "Beschränkt sich Ihre Web-Präsenz auf Instagram oder Facebook?", "Verlieren Sie potenzielle Kunden, weil man Sie bei Google nicht findet?" } },
                        new() { Title = "Bedarf", Items = new() { "Erhalten Sie Anfragen über Nachrichten statt über ein Kontaktformular?", "Erklären Sie Kunden oft dieselben Informationen wiederholt?", "Bieten Sie Leistungen oder Preise an, die öffentlich online verfügbar sein sollten?" } },
                        new() { Title = "Bereitschaft", Items = new() { "Möchten Sie, dass Ihr Unternehmen einen professionelleren Webauftritt hat?", "Möchten Sie, dass Kunden Sie automatisch über Google finden?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Ihre aktuelle Web-Präsenz reicht möglicherweise aus. Eine Website könnte helfen, ist aber nicht dringend." },
                        new() { Min = 3, Max = 5, Text = "Ihre Web-Präsenz hat Lücken. Eine einfache, professionelle Website würde Kunden helfen, Sie zu finden." },
                        new() { Min = 6, Max = 7, Text = "Kunden haben wahrscheinlich Mühe, Sie zu finden. Ein stärkerer Webauftritt würde einen klaren Unterschied machen." },
                        new() { Min = 8, Max = 9, Text = "Ihnen entgehen täglich Kunden. Eine professionelle Website ist der nächste logische Schritt." }
                    }
                },
                new()
                {
                    Key = "automation",
                    HeadingHtml = "Brauchen Sie <span class=\"accent\">Automatisierung</span>?",
                    Groups = new()
                    {
                        new() { Title = "Probleme", Items = new() { "Verwalten Sie Ihre Daten mit Excel oder Papier?", "Versenden Sie dieselben E-Mails oder Nachrichten manuell?", "Haben Sie wiederkehrende Aufgaben, die zu viel Zeit kosten?" } },
                        new() { Title = "Prozesse", Items = new() { "Planen Sie Termine manuell?", "Verwalten Sie Kunden, Bestellungen oder Lager manuell?", "Arbeiten mehrere Personen ohne System mit denselben Daten?" } },
                        new() { Title = "Potenzial für eine Web-Anwendung", Items = new() { "Würde ein System Ihnen mindestens 1–2 Stunden pro Tag sparen?", "Würde Automatisierung Fehler reduzieren?", "Möchten Sie einen zentralen Ort für all Ihre Daten?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Ihre aktuellen Prozesse scheinen überschaubar. Automatisierung hat momentan vielleicht keine Priorität." },
                        new() { Min = 3, Max = 5, Text = "Einige Aufgaben könnten automatisiert werden. Kleine Verbesserungen würden Zeit sparen und Fehler reduzieren." },
                        new() { Min = 6, Max = 7, Text = "Manuelle Arbeit bremst Sie aus. Eine Web-Anwendung würde Ihr Geschäft erheblich optimieren." },
                        new() { Min = 8, Max = 9, Text = "Ihr Unternehmen verliert Zeit und Geld durch manuelle Prozesse. Eine maßgeschneiderte Web-App würde sich schnell auszahlen." }
                    }
                }
            }
        },
        Pricing = new PricingSection
        {
            Label = "PREISE",
            Title = "Was kostet eine Website?",
            Subtitle = "Ohne Templates - jede Website wird individuell für Ihr Unternehmen entwickelt.",
            GetStarted = "ANGEBOT ERHALTEN",
            Badges = new() { "Festpreis", "Gewährleistung inklusive" },
            BadgeRecommended = "Empfohlen",
            CtaHeading = "Nichts Passendes gefunden?",
            CtaText = "Fordern Sie ein individuelles Angebot an - für Desktop-Apps, mobile Apps, Hosting, Wartung und/oder Projekte mit vielen verschiedenen Funktionen.",
            CtaButton = "Angebot anfragen",

            WarrantyUnit = "Tage Gewährleistung",
            PrevLabel = "Zurück",
            NextLabel = "Weiter",
            Cards = new List<PricingCard>
            {
                new() { Name = "Landing Page", Price = "399€", WarrantyDays = 30, Features = new() { "Eine Seite, ein Ziel", "Kontaktformular", "Responsives Design", "SEO-Grundlagen", "DSGVO-Implementierung" } },
                new() { Name = "Präsentations-Website", Price = "799€", WarrantyDays = 30, Features = new() { "Bis zu 5 Seiten", "Kontaktformular + Standortkarte", "WhatsApp-Schaltfläche", "Responsives Design", "SEO-Grundlagen", "DSGVO-Implementierung" } },
                new() { Name = "Website mit Content-Verwaltung", Price = "1.199€", WarrantyDays = 30, IsRecommended = true, Features = new() { "Bis zu 8 Seiten", "Blog und Galerie", "Mehrsprachige Unterstützung", "Kunde pflegt Inhalte selbst", "WhatsApp-Schaltfläche", "Responsives Design", "SEO-Grundlagen", "DSGVO-Implementierung" } },
                new() { Name = "Website + interaktives Tool", Price = "1.599€", WarrantyDays = 60, Features = new() { "Bis zu 5 Seiten", "Rechner, Konfigurator oder Formular", "Admin-Panel", "Responsives Design", "SEO-Grundlagen", "DSGVO-Implementierung" } },
                new() { Name = "Website + Buchungen / Portal", Price = "2.199€", WarrantyDays = 60, Features = new() { "Bis zu 5 Seiten", "Online-Buchungen oder Kundenportal", "Automatische E-Mail-Nachrichten", "Admin-Panel", "Responsives Design", "SEO-Grundlagen", "DSGVO-Implementierung" } },
                new() { Name = "Webanwendung", Price = "2.799€", WarrantyDays = 60, Features = new() { "Bis zu 8 Seiten", "Kundenportal", "Online-Buchungen und/oder Newsletter", "Automatische E-Mail-Nachrichten", "Admin-Panel", "Besucherstatistiken", "Responsives Design", "SEO-Grundlagen", "DSGVO-Implementierung" } },
            }
        },
        Contact = new ContactSection
        {
            TitleHtml = "Machen Sie den ersten Schritt",
            Subtitle1 = "Hinterlassen Sie Ihren Namen und Ihre E-Mail, ich sende Ihnen",
            Subtitle2 = "einen kurzen Fragebogen für den Start",
            NamePlaceholder = "Ihr Name",
            EmailPlaceholder = "Ihre E-Mail-Adresse",
            GetStarted = "LOSLEGEN",
            PrivacyNoteHtml = "Mit dem Absenden stimmen Sie unserer <a href=\"/Home/Privacy\">Datenschutzerklärung</a> zu. Wir respektieren Ihre Privatsphäre.",
            SuccessMessage = "Vielen Dank! Ihre Angaben sind eingegangen. Ich melde mich in Kürze.",
            ErrorMessage = "Etwas ist schiefgelaufen. Bitte versuchen Sie es später erneut."
        },
        Footer = new FooterText
        {
            Home = "START", Work = "PROJEKTE", Process = "ABLAUF", Pricing = "PREISE",
            Copyright = "© 2026 ZS.dev Alle Rechte vorbehalten.",
            PrivacyPolicy = "Datenschutz", TermsOfService = "AGB", CookieSettings = "Cookie-Einstellungen"
        },
        Cookie = new CookieText
        {
            Title = "Cookie-Einstellungen",
            Body = "Diese Website verwendet keine Cookies. Kein Tracking, keine Analyse, keine Cookies von Drittanbietern. Ihre Privatsphäre wird vollständig respektiert.",
            Close = "Verstanden"
        },
        Legal = new LegalText
        {
            BackHome = "← Zurück zur Startseite",
            PrivacyTitle = "Datenschutzerklärung",
            PrivacyHtml = @"<p>Diese Datenschutzerklärung erläutert, wie Zoran Simeunović / ZS.dev (""ich"") personenbezogene Daten verarbeitet, die über diese Website erhoben werden.</p>
<h3>Erhobene Daten</h3><p>Wenn Sie das Kontaktformular absenden, erhebe ich Ihren Namen und Ihre E-Mail-Adresse. Weitere personenbezogene Daten werden nicht erhoben. Es werden keine Tracking-Pixel, Analyse-Tools oder Werbedienste verwendet.</p>
<h3>Verwendungszweck</h3><p>Ihr Name und Ihre E-Mail-Adresse werden verwendet, um: (1) eine Bestätigungs-E-Mail zur Verifizierung Ihrer Adresse zu senden, (2) einen kurzen Projektfragebogen zuzusenden und (3) Ihre Anfrage zu beantworten. Ihre Daten werden weder verkauft noch an Dritte weitergegeben, außer wie unten beschrieben.</p>
<h3>E-Mail-Versand</h3><p>Transaktions-E-Mails werden über <strong>Brevo</strong> (Sendinblue SAS, 55 rue d'Amsterdam, 75008 Paris, Frankreich) versendet, einem DSGVO-konformen E-Mail-Dienst. Ihr Name und Ihre E-Mail-Adresse werden ausschließlich zum Versand dieser E-Mails an Brevo übermittelt. Datenschutzerklärung: <a href=""https://www.brevo.com/legal/privacypolicy/"">brevo.com/legal/privacypolicy</a>.</p>
<h3>Cookies</h3><p>Diese Website verwendet ein technisch notwendiges Cookie (<code>q_ref</code>), um Ihre Fragebogen-Sitzung aufrechtzuerhalten. Es wird nur gesetzt, wenn Sie Ihren Fragebogen-Link öffnen, und läuft nach 30 Tagen oder nach Abschluss ab. Es werden keine Tracking- oder Werbe-Cookies verwendet.</p>
<h3>Speicherung</h3><p>Ihre Daten werden auf einem gesicherten Server innerhalb der EU gespeichert und nur so lange aufbewahrt, wie es zur Bearbeitung Ihrer Anfrage erforderlich ist.</p>
<h3>Rechtsgrundlage</h3><p>Die Verarbeitung erfolgt auf Grundlage Ihrer Einwilligung (Art. 6 Abs. 1 lit. a DSGVO).</p>
<h3>Ihre Rechte</h3><p>Sie haben das Recht auf Auskunft, Berichtigung, Löschung oder Einschränkung der Verarbeitung. Kontakt: <a href=""mailto:zoran.simeunovic@outlook.de"">zoran.simeunovic@outlook.de</a>. Sie haben zudem das Recht, sich bei einer Aufsichtsbehörde zu beschweren.</p>
<p class=""legal-updated"">Zuletzt aktualisiert: Juni 2026.</p>",
            TermsTitle = "Allgemeine Geschäftsbedingungen",
            TermsHtml = @"<p>Durch die Nutzung dieser Website stimmen Sie den folgenden Bedingungen zu.</p>
<h3>Zweck</h3><p>Diese Website ist ein persönliches Portfolio, das die Arbeit und Leistungen von Zoran Simeunović (ZS.dev) präsentiert.</p>
<h3>Inhalt</h3><p>Alle Inhalte, Texte und Bilder sind Eigentum von ZS.dev, sofern nicht anders angegeben, und dürfen nicht ohne Genehmigung wiederverwendet werden.</p>
<h3>Keine Gewährleistung</h3><p>Die Website wird ""wie besehen"" ohne jegliche Gewährleistung bereitgestellt. Informationen können sich ohne Vorankündigung ändern.</p>
<h3>Kontaktformular</h3><p>Mit dem Absenden des Kontaktformulars bestätigen Sie, dass die angegebenen Informationen korrekt sind. Das Absenden begründet keine verbindliche Vereinbarung.</p>
<h3>Haftung</h3><p>ZS.dev haftet nicht für Schäden, die aus der Nutzung dieser Website entstehen.</p>
<p class=""legal-updated"">Zuletzt aktualisiert: Juni 2026.</p>"
        },
        Questionnaire = BuildDeQuestionnaire()
    };

    // --------------------------------------------------------- SRPSKI (LATINICA)
    private static SiteText BuildSr() => new()
    {
        Culture = "sr-Latn",
        PageTitle = "Izrada web stranica po mjeri | Zoran Simeunovic",
        LanguageLabel = "Jezik",
        Nav = new NavText { Home = "POČETNA", Work = "RADOVI", Process = "PROCES", Pricing = "CIJENE", ContactMe = "KONTAKT", MyQuestionnaire = "MOJ UPITNIK" },
        Hero = new HeroText
        {
            Badge = "Full Stack Web Developer",
            HeadingHtml = "<span class=\"accent\">Web rešenja</span> za vaš&nbsp;rast",
            Subtitle1 = "Vaše poslovanje zaslužuje profesionalno web prisustvo.",
            Subtitle2 = "Hajde da ga napravimo zajedno.",
            ContactMe = "KONTAKT",
            MyWork = "MOJI RADOVI",
            ProjectsCount = "5+ WEB I DESKTOP KLIJENATA",
            Name = "Zoran Simeunović"
        },
        Work = new WorkSection
        {
            Label = "RADOVI",
            TitleHtml = "Izdvojeni <span class=\"accent\">radovi</span>",
            Subtitle = "Poslovne aplikacije iz prakse, web platforme i backend sistemi",
            PrevLabel = "Prethodno",
            NextLabel = "Sledeće",
            Cards = new List<WorkCard>
            {
                new() { Title = "HRIS/Web", Image = "hris.webp", Duration = "2 godine", DurationSub = "u AddWare Solutions", DurationUrl = "https://www.addware.de/urlaubsmanager/",
                    Description = "Razvoj i održavanje HRIS-a (Human Resources Information System) za upravljanje radnom snagom, uključujući kadrovsko planiranje, upravljanje godišnjim odmorima i evidenciju radnog vremena. Integracija web servisa, sinhronizacionih servisa i Microsoft 365. Optimizacija postojećih sistema i razvoj novih modula.",
                    Techs = new() { "ASP.NET", "C#", "MySQL", "Microsoft Graph" } },
                new() { Title = "Desktop GUI", Image = "desktop-gui.webp", Duration = "3 godine", DurationSub = "u AddWare Solutions", DurationUrl = "https://www.addware.de/urlaubsmanager/",
                    Description = "Potpuni redizajn desktop GUI-ja sa fokusom na performanse i skalabilnost. Omogućeno brzo prikazivanje velikih skupova podataka o zaposlenima u tabelama sa preko 12 mjeseci istorijskih podataka. Poboljšana poslovna logika, optimizovano rukovanje velikim podacima i isporučeni novi i unapređeni moduli.",
                    Techs = new() { "C#", "WPF", "MySQL" } },
                new() { Title = "Zeiterfassung", Image = "zeiterfassung.webp", Duration = "1 godina", DurationSub = "u AddWare Solutions", DurationUrl = "https://www.addware.de/zeiterfassung/",
                    Description = "Razvoj i unapređenje desktop, web i mobilnih klijenata za evidenciju radnog vremena, uz vizuelizaciju podataka u realnom vremenu i centralizovano upravljanje radnim satima, pauzama i odsustvima u okviru jedinstvenog sistema.",
                    Techs = new() { "ASP.NET", "WPF", "Xamarin Forms" } },
                new() { Title = "Personal Portfolio", Image = "personal-portfolio.webp", Duration = null, DurationSub = null,
                    Description = "Sajt osmišljen i razvijen od nule kako bi predstavio moj rad, veštine i iskustvo, koristeći Figma i Webflow.",
                    Techs = new() { "ASP.NET", "MySQL", "JS" } }
            }
        },
        About = new AboutText
        {
            Label = "O MENI",
            Name = "Zoran Simeunović",
            HeadingHtml = "Ko sam <span class=\"accent\">ja?</span>",
            Description = "Pravim skalabilne full-stack sajtove i web aplikacije koristeći Figma, Webflow, ASP.NET i MySQL. Volim čist kod i brze korisničke interfejse.",
            Highlight1Html = "<span class=\"accent\">6 godina</span> iskustva u privredi",
            Highlight2Html = "<span class=\"accent\">Web,</span> desktop i mobilne aplikacije",
            Highlight3Html = "Sajtovi i <span class=\"accent\">web</span> rješenja",
            GoPlayer = "Go igrač",
            ContactMe = "KONTAKT",
            DownloadCv = "PREUZMI CV"
        },
        Process = new ProcessSection
        {
            Label = "PROCES",
            TitleHtml = "Kako sarađujemo",
            ContactMe = "KONTAKT",
            FindMe = "ili me pronađite na LinkedIn, Xing, GitHub ili putem mejla",
            ShowAll = "Prikazi sve procese",
            HideAll = "Sakrij procese",
            Steps = new List<ProcessStep>
            {
                new() { Tag = "PRVI KORAK" },
                new() { Tag = "100% PUTEM MEJLA", Title = "Upitnik o projektu", Description = "Vaše poslovanje, trenutno online prisustvo, ciljevi projekta, tip projekta, rokovi, željeni dizajn" },
                new() { Tag = "BESPLATNO", Title = "Ponuda", Description = "Pregledam vaše odgovore, postavim eventualna dodatna pitanja i šaljem detaljnu ponudu." },
                new() { Tag = "PRIJE POČETKA RADA", Title = "Ugovor o projektu", Description = "Jasan obim, cijena i prekretnice u pisanoj formi. Bez iznenađenja." },
                new() { Tag = "POTREBNO VAŠE ODOBRENJE", Title = "Pregled dizajna", Description = "Prvo vidite kompletan dizajn. Ništa se ne gradi bez vašeg odobrenja." },
                new() { Tag = "IZRADA VAŠEG SAJTA", Title = "Sajt", Description = "Odobreni dizajn postaje brz, funkcionalan sajt. Bez ćutanja - uvijek znate dokle se stiglo." },
                new() { Tag = "VRIJEME ZA PROVJERU", Title = "Pregled sajta", Description = "Testirate sajt i tražite izmjene ako su potrebne." },
                new() { Tag = "VAŠA APLIKACIJA", Title = "Web aplikacija (opciono)", Description = "Sajt odobren. Prilagođeni alati grade se iza scene. Portali, rezervacije, dashboardi." },
                new() { Tag = "VRIJEME ZA PROVJERU", Title = "Pregled web aplikacije (opciono)", Description = "Testirate sve od početka do kraja. Tražite izmjene ako su potrebne." },
                new() { Tag = "PUŠTANJE U RAD", Title = "Lansiranje i predaja", Description = "Postavljanje, dokumentacija i predaja. Konačna faktura pri isporuci. Sve greške nastale kao rezultat razvoja bit će ispravljene besplatno - 30 dana za web stranice, 60 dana za web aplikacije." },
                new() { Tag = "FLEXIBLE", Title = "Kontinuirana podrška (opciono)", Description = "Pod vašim uslovima. Hosting, SSL, rezervne kopije, ispravke grešaka ili nove funkcije - uzimate samo ono što vašem poslovanju treba." }
            }
        },
        Improve = new ImproveSection
        {
            TitleHtml = "Kako poboljšati <span class=\"accent\">web prisustvo?</span>",
            Subtitle1 = "Treba li vam redizajn, novi sajt ili web aplikacija?",
            Subtitle2 = "Dobijte brzu analizu.",
            StartAnalysis = "Otvori listu",
            CloseChecklist = "Zatvori listu",
            Analyze = "Analiziraj",
            Lists = new List<ImproveList>
            {
                new()
                {
                    Key = "redesign",
                    HeadingHtml = "Treba li vašem sajtu <span class=\"accent\">redizajn</span>?",
                    Groups = new()
                    {
                        new() { Title = "Analiza", Items = new() { "Da li je sajt vizuelno zastario?", "Da li je mobilna verzija loša ili je nema?", "Da li je navigacija zbunjujuća?", "Da li je odmah jasno čime se kompanija bavi?" } },
                        new() { Title = "Dizajn", Items = new() { "Da li su boje i fontovi dosljedni?", "Postoji li jasna vizuelna hijerarhija (naslovi, sekcije)?", "Da li su CTA dugmad vidljiva i jasna?", "Ima li previše teksta bez dovoljno praznog prostora?" } },
                        new() { Title = "UX / Funkcionalnost", Items = new() { "Da li se kontakt informacije lako pronalaze?", "Da li se sajt brzo učitava?", "Mogu li korisnici pronaći potrebne informacije u roku od 5 sekundi?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Solidan i funkcionalan. Redizajn vjerovatno nije potreban." },
                        new() { Min = 3, Max = 5, Text = "Postoje slabosti. Preporučuju se manja poboljšanja ili optimizacija." },
                        new() { Min = 6, Max = 8, Text = "Zastareo ili neefikasan. Preporučuje se redizajn." },
                        new() { Min = 9, Max = 11, Text = "Loše stanje. Preporučuje se potpun redizajn." }
                    }
                },
                new()
                {
                    Key = "website",
                    HeadingHtml = "Treba li vam <span class=\"accent\">sajt</span>?",
                    Groups = new()
                    {
                        new() { Title = "Osnove", Items = new() { "Da li klijenti onlajn traže vaše poslovanje?", "Da li je vaše web prisustvo ograničeno na Instagram ili Facebook?", "Da li gubite potencijalne klijente jer vas ne mogu pronaći na Google-u?" } },
                        new() { Title = "Potreba", Items = new() { "Da li primate upite preko poruka umjesto preko kontakt forme?", "Da li klijentima često ponavljate iste informacije?", "Da li nudite usluge ili cijene koje bi trebalo da budu javno dostupne onlajn?" } },
                        new() { Title = "Spremnost", Items = new() { "Želite li da vaše poslovanje ima profesionalnije web prisustvo?", "Želite li da vas klijenti automatski pronalaze putem Google-a?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Vaše trenutno web prisustvo je možda dovoljno. Sajt bi mogao pomoći, ali nije hitno." },
                        new() { Min = 3, Max = 5, Text = "Vaše web prisustvo ima praznine. Jednostavan, profesionalan sajt pomogao bi klijentima da vas pronađu." },
                        new() { Min = 6, Max = 7, Text = "Klijenti vjerovatno teško dolaze do vas. Jače web prisustvo napravilo bi jasnu razliku." },
                        new() { Min = 8, Max = 9, Text = "Svakog dana gubite klijente. Profesionalan sajt je sljedeći logičan korak." }
                    }
                },
                new()
                {
                    Key = "automation",
                    HeadingHtml = "Treba li vam <span class=\"accent\">automatizacija</span>?",
                    Groups = new()
                    {
                        new() { Title = "Problemi", Items = new() { "Da li koristite Excel ili papir za upravljanje podacima?", "Da li ručno šaljete iste mejlove ili poruke?", "Imate li ponavljajuće zadatke koji oduzimaju previše vremena?" } },
                        new() { Title = "Procesi", Items = new() { "Da li ručno zakazujete termine?", "Da li ručno pratite klijente, porudžbine ili zalihe?", "Da li više ljudi radi sa istim podacima bez sistema?" } },
                        new() { Title = "Potencijal za web aplikaciju", Items = new() { "Da li bi vam sistem uštedeo bar 1–2 sata dnevno?", "Da li bi automatizacija smanjila greške?", "Želite li centralno mjesto za sve vaše podatke?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Vaši trenutni procesi djeluju upravljivo. Automatizacija možda trenutno nije prioritet." },
                        new() { Min = 3, Max = 5, Text = "Neki zadaci bi mogli da se automatizuju. Mala poboljšanja uštedila bi vrijeme i smanjila greške." },
                        new() { Min = 6, Max = 7, Text = "Ručni rad vas usporava. Web aplikacija bi značajno pojednostavila vaše poslovanje." },
                        new() { Min = 8, Max = 9, Text = "Vaše poslovanje gubi vrijeme i novac na ručnim procesima. Prilagođena web aplikacija brzo bi se isplatila." }
                    }
                }
            }
        },
        Pricing = new PricingSection
        {
            Label = "CIJENE",
            Title = "Koliko košta web stranica?",
            Subtitle = "Bez gotovih šablona - svaki sajt razvijen posebno za vaš biznis.",
            GetStarted = "DOBIJ PONUDU",
            Badges = new() { "Fiksna cijena", "Garantni rok uključen" },
            BadgeRecommended = "Preporučeno",
            CtaHeading = "Niste pronašli što tražite?",
            CtaText = "Zatražite personalnu ponudu za desktop aplikaciju, mobilnu app, hosting, održavanje, i/ili projekte sa mnogo različitih funkcija.",
            CtaButton = "Zatražite ponudu",

            WarrantyUnit = "dana garantni rok",
            PrevLabel = "Prethodni",
            NextLabel = "Sljedeći",
            Cards = new List<PricingCard>
            {
                new() { Name = "Landing stranica", Price = "399€", WarrantyDays = 30, Features = new() { "Jedna stranica, jedan cilj", "Kontakt forma", "Responzivan dizajn", "SEO osnove", "DSGVO implementacija" } },
                new() { Name = "Prezentacioni sajt", Price = "799€", WarrantyDays = 30, Features = new() { "Do 5 stranica", "Kontakt forma + mapa lokacije", "WhatsApp dugme", "Responzivan dizajn", "SEO osnove", "DSGVO implementacija" } },
                new() { Name = "Sajt koji sam uređuješ", Price = "1.199€", WarrantyDays = 30, IsRecommended = true, Features = new() { "Do 8 stranica", "Blog i galerija", "Višejezična podrška", "Klijent sam uređuje sadržaj", "WhatsApp dugme", "Responzivan dizajn", "SEO osnove", "DSGVO implementacija" } },
                new() { Name = "Sajt + interaktivni alat", Price = "1.599€", WarrantyDays = 60, Features = new() { "Do 5 stranica", "Kalkulator, konfigurator ili obrazac", "Admin panel", "Responzivan dizajn", "SEO osnove", "DSGVO implementacija" } },
                new() { Name = "Sajt + rezervacije / portal", Price = "2.199€", WarrantyDays = 60, Features = new() { "Do 5 stranica", "Online rezervacije ili klijentski portal", "Automatske email poruke", "Admin panel", "Responzivan dizajn", "SEO osnove", "DSGVO implementacija" } },
                new() { Name = "Web aplikacija", Price = "2.799€", WarrantyDays = 60, Features = new() { "Do 8 stranica", "Klijentski portal", "Online rezervacije i/ili newsletter", "Automatske email poruke", "Admin panel", "Statistika posjeta", "Responzivan dizajn", "SEO osnove", "DSGVO implementacija" } },
            }
        },
        Contact = new ContactSection
        {
            TitleHtml = "Napravite prvi korak",
            Subtitle1 = "Ostavite ime i mejl, poslaću vam",
            Subtitle2 = "kratak upitnik za početak",
            NamePlaceholder = "Vaše ime",
            EmailPlaceholder = "Vaša email adresa",
            GetStarted = "ZAPOČNI",
            PrivacyNoteHtml = "Slanjem prihvatate našu <a href=\"/Home/Privacy\">Politiku privatnosti</a>. Poštujemo vašu privatnost.",
            SuccessMessage = "Hvala! Vaši podaci su primljeni. Javiću vam se uskoro.",
            ErrorMessage = "Došlo je do greške. Molimo pokušajte ponovo kasnije."
        },
        Footer = new FooterText
        {
            Home = "POČETNA", Work = "RADOVI", Process = "PROCES", Pricing = "CIJENE",
            Copyright = "© 2026 ZS.dev Sva prava zadržana.",
            PrivacyPolicy = "Politika privatnosti", TermsOfService = "Uslovi korištenja", CookieSettings = "Podešavanja kolačića"
        },
        Cookie = new CookieText
        {
            Title = "Podešavanja kolačića",
            Body = "Ovaj sajt ne koristi kolačiće. Bez praćenja, bez analitike, bez kolačića trećih strana. Vaša privatnost je u potpunosti poštovana.",
            Close = "Razumijem"
        },
        Legal = new LegalText
        {
            BackHome = "← Nazad na početnu",
            PrivacyTitle = "Politika privatnosti",
            PrivacyHtml = @"<p>Ova Politika privatnosti objašnjava kako Zoran Simeunović / ZS.dev (""ja"") obrađuje lične podatke prikupljene putem ovog sajta.</p>
<h3>Podaci koje prikupljam</h3><p>Kada popunite kontakt formu, prikupljam vaše ime i email adresu. Drugi lični podaci se ne prikupljaju. Ne koristim piksel praćenje, alate za analitiku niti reklamne servise.</p>
<h3>Svrha obrade</h3><p>Vaše ime i email adresa koriste se za: (1) slanje email potvrde radi verifikacije adrese, (2) slanje kratkog upitnika o projektu i (3) odgovor na vaš upit. Vaši podaci se ne prodaju niti dijele s trećim stranama, osim kako je opisano ispod.</p>
<h3>Slanje emailova</h3><p>Transakcioni emailovi šalju se putem <strong>Brevo</strong> (Sendinblue SAS, 55 rue d'Amsterdam, 75008 Paris, Francuska), servisa usklađenog sa GDPR-om. Vaše ime i email adresa prosljeđuju se Brevo-u isključivo radi dostave ovih emailova. Politika privatnosti: <a href=""https://www.brevo.com/legal/privacypolicy/"">brevo.com/legal/privacypolicy</a>.</p>
<h3>Kolačići</h3><p>Sajt koristi jedan tehnički neophodan kolačić (<code>q_ref</code>) za održavanje sesije upitnika. Postavlja se samo kada otvorite link za upitnik i ističe nakon 30 dana ili završetka upitnika. Ne koriste se kolačići za praćenje ili oglašavanje.</p>
<h3>Čuvanje podataka</h3><p>Vaši podaci čuvaju se na sigurnom serveru unutar EU i brišu se na vaš zahtjev ili kada više nisu potrebni.</p>
<h3>Pravna osnova</h3><p>Obrada se vrši na osnovu vaše saglasnosti date dobrovoljnim popunjavanjem kontakt forme (čl. 6 st. 1 t. a) GDPR).</p>
<h3>Vaša prava</h3><p>Imate pravo na pristup, ispravku, brisanje ili ograničenje obrade vaših podataka. Kontakt: <a href=""mailto:zoran.simeunovic@outlook.de"">zoran.simeunovic@outlook.de</a>. Imate i pravo na pritužbu nadzornom tijelu.</p>
<p class=""legal-updated"">Posljednje ažuriranje: jun 2026.</p>",
            TermsTitle = "Uslovi korištenja",
            TermsHtml = @"<p>Korištenjem ovog sajta prihvatate sljedeće uslove.</p>
<h3>Svrha</h3><p>Ovaj sajt je lični portfolio koji predstavlja rad i usluge Zorana Simeunovića (ZS.dev).</p>
<h3>Sadržaj</h3><p>Sav sadržaj, tekst i slike vlasništvo su ZS.dev osim ako nije drugačije navedeno i ne smiju se koristiti bez dozvole.</p>
<h3>Bez garancije</h3><p>Sajt se pruža ""takav kakav jeste"" bez bilo kakvih garancija. Informacije se mogu promijeniti bez prethodne najave.</p>
<h3>Kontakt forma</h3><p>Slanjem kontakt forme potvrđujete da su navedeni podaci tačni. Slanje ne stvara nikakav obavezujući sporazum.</p>
<h3>Odgovornost</h3><p>ZS.dev ne snosi odgovornost za bilo kakvu štetu nastalu korišćenjem ovog sajta.</p>
<p class=""legal-updated"">Posljednje ažuriranje: jun 2026.</p>"
        },
        Questionnaire = BuildSrQuestionnaire()
    };

    private static QuestionnaireText BuildEnQuestionnaire() => new()
    {
        PageTitle = "Questionnaire",
        Subtitle = "Your answers help me prepare an offer tailored to your needs. Please answer as many questions as possible.",
        ProgressPattern = "Step {0} of 5",
        ProgressDone = "Complete",
        BtnNext = "Next step →",
        BtnBack = "← Back",
        BtnSubmit = "Submit questionnaire",
        BtnSending = "Sending...",
        BtnOptOut = "Unsubscribe",
        OptionalLabel = "optional",
        SelectDefault = "- Select -",
        OtherOption = "Other: ___",
        DropzoneClickOrDrag = "Drag or click",
        DropzoneUploading = "Uploading...",
        DropzoneError = "Error. Please try again.",
        DoneTitlePattern = "Thank you, {0}!",
        DoneText1 = "I will review your answers and get back to you soon.",
        DoneText2 = "If I have any additional questions, I will contact you via email.",
        ExpiredTitle = "Link has expired",
        ExpiredText = "This questionnaire link is no longer active. Contact me at <a href=\"mailto:zoran.simeunovic@outlook.de\">zoran.simeunovic@outlook.de</a> and I will send you a new link.",
        CompletedTitle = "Questionnaire already completed",
        CompletedText1 = "Your answers have already been received. I will get back to you within 24–48 hours.",
        CompletedText2 = "Have questions? Write to <a href=\"mailto:zoran.simeunovic@outlook.de\">zoran.simeunovic@outlook.de</a>.",
        CompanyRequired = "This field is required.",
        SelectedPackageLabel = "Selected package",
        PackagePriceHint = "Changes to selected features may affect the final price.",
        PackagePriceWarning = "This change may affect the price compared to your selected package. Do you want to continue?",
        PackagePriceWarningConfirm = "Continue",
        PackagePriceWarningCancel = "Cancel",
        OptOutConfirmMsg = "Are you sure you want to unsubscribe?\n\nThis will result in the deletion of your answers and contact information.",
        OptOutDoneHtml = "Your data has been successfully deleted.",
        OptOutError = "Error sending request. Please try again.",
        PagesUnderLimitMsg = "You can still add {0} more page(s) included in your package. Continue or go back to add more.",
        PagesOverLimitMsg = "You selected {0} page(s) more than included in your package, which may affect the price. Continue or go back to reduce your selection.",
        PagesModalContinue = "Continue",
        PagesModalChange = "Change selection",
        SalesRequiredMsg = "Please select online bookings or client portal.",
        S1 = new QStep1Text
        {
            Title = "About your business",
            CompanyLabel = "Company / brand name",
            CompanyHint = "If you don't have a company, enter \"private\".",
            CompanyPlaceholder = "e.g. My Company Ltd.",
            IndustryLabel = "What industry are you in?",
            IndustryOptions = ["Trade / E-commerce", "Health & wellness", "Construction & real estate", "Hospitality & tourism", "Education & training", "IT & technology", "Consulting & services", "Beauty & fashion"],
            IndustryOtherPlaceholder = "Enter your industry",
            TeamSizeLabel = "Size of your team?",
            TeamSizeOptions = ["Just me", "2–10 employees", "11–50 employees", "50+ employees"],
            BrandDescLabel = "How would you describe your brand?",
            BrandDescOptions = ["Accessible and friendly", "Professional and serious", "Premium and exclusive", "Modern and innovative", "Traditional and reliable"],
            BrandDescOtherPlaceholder = "Describe your brand"
        },
        S2 = new QStep2Text
        {
            Title = "Website",
            NeedWhatLabel = "What are you looking for?",
            ResponsiveLabel = "Responsive design - adapted for desktop, tablet and mobile",
            WebsiteTypeOptions = ["Landing page", "Presentation website", "Portfolio website", "Website with gallery / blog", "Product or service catalogue", "Redesign of existing website"],
            WebsiteTypeOtherPlaceholder = "Describe what you need",
            ExistingUrlLabel = "URL of your existing website",
            ExistingUrlPlaceholder = "https://your-website.com",
            CurrentSiteDescLabel = "How would you describe your current website?",
            CurrentSiteDescOptions = ["Visually outdated", "Not mobile-friendly", "Difficult to navigate", "Slow", "Missing important information", "Doesn't appear on Google", "I'm satisfied but need new features"],
            CurrentSiteDescOtherPlaceholder = "Describe",
            WantToChangeLabel = "What do you want to change?",
            WantToChangeOptions = ["Visual appearance", "Structure and navigation", "Content and texts", "Speed and performance", "Mobile version"],
            WantToChangeOtherPlaceholder = "Describe",
            PagesLabel = "Which pages do you need?",
            PagesOptions = ["Home page", "About us", "Services / Products", "Pricing", "Gallery / Portfolio", "Contact", "FAQ", "Team", "Careers", "Reviews - changes via support and maintenance"],
            PagesPortfolioOptions = ["About us", "Works / Portfolio", "Contact", "Blog / News"],
            PagesOtherPlaceholder = "Which pages?",
            ExtrasLabel = "Do you need any of the following?",
            CommHeader = "Contact",
            CommOptions = ["Contact form - clients contact you directly from your website", "WhatsApp button - clients contact you directly via WhatsApp", "Chatbot - automated chat assistant on your website"],
            GrowthHeader = "Visibility & growth",
            GrowthOptions = ["SEO basics - so clients find you more easily on Google", "SEO advanced optimization - keyword research, content strategy, link building", "Google Maps integration - show your company location on the map", "Multilingual support (max. 3 languages)", "Online payment", "Google or Trustpilot reviews - automatic display of ratings from the platform"],
            MultilingualPlaceholder = "E.g. German, English, Swedish...",
            MultilingualExtraPlaceholder = "Write which additional languages you need",
            OnlinePaymentPlaceholder = "E.g. Visa, Mastercard, American Express, PayPal, Klarna, Apple Pay...",
            ReviewsPlatformPlaceholder = "Select platform",
            ExtrasOtherPlaceholder = "What else do you need?"
        },
        S3 = new QStep3Text
        {
            Title = "Web application & automation",
            SectionHint = "Select what applies to your project. If nothing is needed, just click Next.",
            AppTypeLabel = "What do you need?",
            AppTypeOptions = ["New web application", "New desktop application"],
            AppTypeOtherPlaceholder = "Describe",
            AppUsersLabel = "Who will use the application?",
            AppUsersOptions = ["Me / internal team", "My clients"],
            AppUsersOtherPlaceholder = "Who?",
            AppDisabledHint = "Please select an application type above first.",
            AdminPanelHint = "To select this option, you must first select Admin panel under Automation.",
            CommLabel = "Communication & automation",
            CommOptions = ["Newsletter - regularly inform clients about news and offers", "Automated emails - reminders, confirmations, welcome messages, birthday and anniversary greetings, absence notifications"],
            SalesLabel = "Sales & bookings",
            SalesOptions = ["Online bookings - clients schedule appointments themselves, without phone calls", "Client portal - clients log in and view their data, orders or documents", "Online shop - sell products or services directly online"],
            AutoLabel = "Automation & integrations",
            AutoOptions = ["Reduce phone calls and inquiries - the system works for you", "Save time on repetitive tasks - data entry, order tracking, report generation", "Microsoft 365 integration - connect your site with Outlook, Teams, Calendar, OneDrive", "Admin panel for managing data and users - everything in one place", "Analytics dashboard - view visitor count, traffic sources and user behaviour within your site"],
            ContentMgmtLabel = "Content management",
            ContentMgmtOptions = ["I want to update texts and images myself - without technical knowledge", "Blog - add and publish articles yourself", "News section - publish news and updates yourself"],
            ContentMgmtOtherPlaceholder = "What else?"
        },
        S4 = new QStep4Text
        {
            Title = "Design & materials",
            TextsLabel = "Texts for the website",
            TextsHelpCheckbox = "I need help with texts",
            TextsDropzoneHint = "Attach texts - drag or click",
            PhotosLabel = "Photos",
            PhotosHelpCheckbox = "I need help with photos",
            PhotosDropzoneHint = "Attach photos - drag or click",
            BrandingLabel = "Branding materials - Logo / Brand book",
            BrandingHelpCheckbox = "I need help with branding",
            BrandingDropzoneHint = "Attach logo or brand book - drag or click",
            SocialLinksLabel = "Social media",
            SocialLinksHint = "List links to your profiles you want displayed on the website. E.g. LinkedIn, Xing, Facebook, Instagram...",
            StyleLabel = "What style do you prefer?",
            StyleOptions = ["Minimal and clean", "Modern and bold", "Professional and serious", "Warm and friendly", "I'm not sure - I trust your judgment"],
            StyleOtherPlaceholder = "Describe your style",
            ThemeLabel = "Do you prefer a dark or light theme?",
            ThemeDark = "Dark theme",
            ThemeLight = "Light theme",
            ThemeAny = "Either way",
            LikedSitesLabel = "Do you have any websites you like?",
            LikedSitesHint = "Share links - doesn't have to be from your industry.",
            LikedSitesWhatLabel = "What do you like about those websites?",
            LikedSitesWhatHint = "E.g. colours, layout, simplicity, animations...",
            CompetitorSitesLabel = "Do you have competitor websites as a reference?",
            CompetitorSitesHint = "Links to competitor websites.",
            CompetitorGoodLabel = "What does your competition do well?",
            CompetitorGoodHint = "What do you like about competitor websites or services?",
            CompetitorBadLabel = "What does your competition do poorly?",
            CompetitorBadHint = "Where do you see an opportunity to stand out?",
            ColorsLabel = "What colours would you like to use on the website?",
            PrimaryColorLabel = "Primary colour",
            SecondaryColorLabel = "Secondary colour",
            AccentColorLabel = "Accent colour",
            ColorsHint = "Skip if unsure. We can choose colours that suit your brand together."
        },
        S5 = new QStep5Text
        {
            Title = "Goals & timeline",
            GoalsLabel = "What is the main goal of this project?",
            GoalsWebHeader = "Web presence",
            GoalsWebOptions = ["Look more professional and credible", "Be found on Google", "Present my services or products online", "Attract more clients / inquiries"],
            GoalsBusinessHeader = "Business & growth",
            GoalsBusinessOptions = ["Sell products or services online", "Provide better service to existing clients", "Expand business to new markets", "Build brand recognition"],
            GoalsOtherPlaceholder = "What goal?",
            BiggestProblemLabel = "What is currently your biggest challenge?",
            BiggestProblemHint = "Briefly describe what you struggle with most in your business.",
            SuccessCriteriaLabel = "How will you know the project is successful?",
            SuccessCriteriaHint = "What would have to change for you to say - this worked?",
            AdvantagesLabel = "What are your main advantages over your competitors?",
            AdvantagesHint = "What sets you apart from others in your field?",
            CustomersLabel = "Who are your customers / clients?",
            CustomerTypeLabel = "Customer type",
            CustomerTypeOptions = ["Private individuals", "Businesses and companies"],
            CustomerTypeOtherPlaceholder = "Who are your clients?",
            AgeLabel = "Age group",
            AgeOptions = ["Young adults (18–35)", "Middle-aged (35–55)", "Older generation (55+)", "All age groups"],
            GenderLabel = "Gender",
            GenderOptions = ["Predominantly male", "Predominantly female", "Equal / all"],
            LocationLabel = "Customer location",
            LocationOptions = ["Local (same city / region)", "National (entire country)", "International"],
            StartWhenLabel = "When would you like to start?",
            StartWhenOptions = ["As soon as possible", "Within a month", "Within 2–3 months", "Just exploring options"],
            DeadlineLabel = "Do you have a deadline?",
            DeadlineYes = "Yes → When?",
            DeadlineNo = "No fixed deadline",
            SupportLabel = "Are you interested in maintenance and support after the project?",
            SupportOptions = ["Hosting - your website on a reliable server", "SSL certificate - secure connection", "Regular backups - your data automatically saved daily", "Security updates - protection against attacks", "Minor content changes - updating texts, images, prices and reviews", "Technical support"],
            SupportOtherPlaceholder = "What else?",
            HeardFromLabel = "How did you hear about me?",
            HeardFromOptions = ["Friend / colleague recommendation", "Google search", "LinkedIn", "Xing", "Instagram", "Facebook"],
            HeardFromOtherPlaceholder = "Where did you hear?",
            AdditionalNotesLabel = "Is there anything else I should know?",
            AdditionalNotesHint = "Write everything you consider important that we didn't ask.",
            WarrantyHtml = "All bugs resulting from development are fixed free of charge - <strong>30 days</strong> for websites, <strong>60 days</strong> for web applications."
        }
    };

    private static QuestionnaireText BuildDeQuestionnaire() => new()
    {
        PageTitle = "Fragebogen",
        Subtitle = "Ihre Antworten helfen mir, ein auf Ihre Bedürfnisse zugeschnittenes Angebot zu erstellen. Bitte beantworten Sie so viele Fragen wie möglich.",
        ProgressPattern = "Schritt {0} von 5",
        ProgressDone = "Abgeschlossen",
        BtnNext = "Nächster Schritt →",
        BtnBack = "← Zurück",
        BtnSubmit = "Fragebogen absenden",
        BtnSending = "Wird gesendet...",
        BtnOptOut = "Abmelden",
        OptionalLabel = "optional",
        SelectDefault = "- Auswählen -",
        OtherOption = "Sonstiges: ___",
        DropzoneClickOrDrag = "Ziehen oder klicken",
        DropzoneUploading = "Wird hochgeladen...",
        DropzoneError = "Fehler. Bitte erneut versuchen.",
        DoneTitlePattern = "Danke, {0}!",
        DoneText1 = "Ich werde Ihre Antworten prüfen und mich in Kürze bei Ihnen melden.",
        DoneText2 = "Falls ich weitere Fragen habe, kontaktiere ich Sie per E-Mail.",
        ExpiredTitle = "Link ist abgelaufen",
        ExpiredText = "Dieser Fragebogen-Link ist nicht mehr aktiv. Kontaktieren Sie mich unter <a href=\"mailto:zoran.simeunovic@outlook.de\">zoran.simeunovic@outlook.de</a> und ich sende Ihnen einen neuen Link.",
        CompletedTitle = "Fragebogen bereits ausgefüllt",
        CompletedText1 = "Ihre Antworten wurden bereits empfangen. Ich werde mich innerhalb von 24–48 Stunden bei Ihnen melden.",
        CompletedText2 = "Haben Sie Fragen? Schreiben Sie an <a href=\"mailto:zoran.simeunovic@outlook.de\">zoran.simeunovic@outlook.de</a>.",
        CompanyRequired = "Dieses Feld ist erforderlich.",
        SelectedPackageLabel = "Ausgewähltes Paket",
        PackagePriceHint = "Änderungen bei den ausgewählten Funktionen können den Endpreis beeinflussen.",
        PackagePriceWarning = "Diese Änderung kann den Preis im Vergleich zu Ihrem gewählten Paket beeinflussen. Möchten Sie fortfahren?",
        PackagePriceWarningConfirm = "Fortfahren",
        PackagePriceWarningCancel = "Abbrechen",
        OptOutConfirmMsg = "Sind Sie sicher, dass Sie sich abmelden möchten?\n\nDadurch werden Ihre bisherigen Antworten und Kontaktdaten gelöscht.",
        OptOutDoneHtml = "Ihre Daten wurden erfolgreich gelöscht.",
        OptOutError = "Fehler beim Senden der Anfrage. Bitte erneut versuchen.",
        PagesUnderLimitMsg = "Sie können noch {0} Seite(n) aus Ihrem Paket hinzufügen. Weiter oder zurückgehen, um mehr hinzuzufügen.",
        PagesOverLimitMsg = "Sie haben {0} Seite(n) mehr als im Paket enthalten ausgewählt, was den Preis beeinflussen kann. Weiter oder zurückgehen, um die Auswahl zu reduzieren.",
        PagesModalContinue = "Weiter",
        PagesModalChange = "Auswahl ändern",
        SalesRequiredMsg = "Bitte wählen Sie Online-Buchungen oder Kundenportal.",
        S1 = new QStep1Text
        {
            Title = "Über Ihr Unternehmen",
            CompanyLabel = "Firmenname / Markenname",
            CompanyHint = "Falls Sie kein Unternehmen haben, tragen Sie \"privat\" ein.",
            CompanyPlaceholder = "z.B. Meine Firma GmbH",
            IndustryLabel = "In welcher Branche sind Sie tätig?",
            IndustryOptions = ["Handel / E-Commerce", "Gesundheit & Wellness", "Bauwesen & Immobilien", "Gastronomie & Tourismus", "Bildung & Ausbildung", "IT & Technologie", "Beratung & Dienstleistungen", "Beauty & Mode"],
            IndustryOtherPlaceholder = "Branche eingeben",
            TeamSizeLabel = "Größe Ihres Teams?",
            TeamSizeOptions = ["Nur ich", "2–10 Mitarbeiter", "11–50 Mitarbeiter", "50+ Mitarbeiter"],
            BrandDescLabel = "Wie würden Sie Ihre Marke beschreiben?",
            BrandDescOptions = ["Zugänglich und freundlich", "Professionell und seriös", "Premium und exklusiv", "Modern und innovativ", "Traditionell und zuverlässig"],
            BrandDescOtherPlaceholder = "Beschreiben Sie Ihre Marke"
        },
        S2 = new QStep2Text
        {
            Title = "Website",
            NeedWhatLabel = "Was suchen Sie?",
            ResponsiveLabel = "Responsives Design - angepasst für Desktop, Tablet und Mobilgerät",
            WebsiteTypeOptions = ["Landing Page", "Präsentationswebsite", "Portfolio-Website", "Website mit Galerie / Blog", "Produkt- oder Dienstleistungskatalog", "Redesign einer bestehenden Website"],
            WebsiteTypeOtherPlaceholder = "Beschreiben Sie, was Sie benötigen",
            ExistingUrlLabel = "URL Ihrer bestehenden Website",
            ExistingUrlPlaceholder = "https://ihre-website.de",
            CurrentSiteDescLabel = "Wie würden Sie Ihre aktuelle Website beschreiben?",
            CurrentSiteDescOptions = ["Visuell veraltet", "Nicht mobilfreundlich", "Schwer zu navigieren", "Langsam", "Wichtige Informationen fehlen", "Erscheint nicht bei Google", "Ich bin zufrieden, brauche aber neue Funktionen"],
            CurrentSiteDescOtherPlaceholder = "Beschreiben",
            WantToChangeLabel = "Was möchten Sie ändern?",
            WantToChangeOptions = ["Visuelles Erscheinungsbild", "Struktur und Navigation", "Inhalte und Texte", "Geschwindigkeit und Performance", "Mobile Version"],
            WantToChangeOtherPlaceholder = "Beschreiben",
            PagesLabel = "Welche Seiten benötigen Sie?",
            PagesOptions = ["Startseite", "Über uns", "Dienstleistungen / Produkte", "Preisliste", "Galerie / Portfolio", "Kontakt", "FAQ", "Team", "Karriere", "Bewertungen - Änderungen über Support und Wartung"],
            PagesPortfolioOptions = ["Über uns", "Arbeiten / Portfolio", "Kontakt", "Blog / Neuigkeiten"],
            PagesOtherPlaceholder = "Welche Seiten?",
            ExtrasLabel = "Benötigen Sie eines der Folgenden?",
            CommHeader = "Kontakt",
            CommOptions = ["Kontaktformular - Kunden kontaktieren Sie direkt von Ihrer Website", "WhatsApp-Schaltfläche - Kunden kontaktieren Sie direkt über WhatsApp", "Chatbot - automatischer Chat-Assistent auf Ihrer Website"],
            GrowthHeader = "Sichtbarkeit & Wachstum",
            GrowthOptions = ["SEO-Grundlagen - damit Kunden Sie leichter auf Google finden", "SEO-Erweiterte Optimierung - Keyword-Recherche, Content-Strategie, Linkaufbau", "Google Maps-Integration - zeigen Sie den Standort Ihres Unternehmens auf der Karte", "Mehrsprachige Unterstützung (max. 3 Sprachen)", "Online-Zahlung", "Google oder Trustpilot Bewertungen - automatische Anzeige von Bewertungen der Plattform"],
            MultilingualPlaceholder = "Z.B. Deutsch, Englisch, Schwedisch...",
            MultilingualExtraPlaceholder = "Geben Sie an, welche weiteren Sprachen Sie benötigen",
            OnlinePaymentPlaceholder = "Z.B. Visa, Mastercard, American Express, PayPal, Klarna, Apple Pay...",
            ReviewsPlatformPlaceholder = "Plattform auswählen",
            ExtrasOtherPlaceholder = "Was brauchen Sie noch?"
        },
        S3 = new QStep3Text
        {
            Title = "Web-Applikation & Automatisierung",
            SectionHint = "Wählen Sie, was auf Ihr Projekt zutrifft. Wenn nichts davon benötigt wird, klicken Sie einfach auf Weiter.",
            AppTypeLabel = "Was benötigen Sie?",
            AppTypeOptions = ["Neue Web-Anwendung", "Neue Desktop-Anwendung"],
            AppTypeOtherPlaceholder = "Beschreiben",
            AppUsersLabel = "Wer wird die Anwendung nutzen?",
            AppUsersOptions = ["Ich / internes Team", "Meine Kunden"],
            AppUsersOtherPlaceholder = "Wer?",
            AppDisabledHint = "Bitte wählen Sie zuerst oben einen Anwendungstyp aus.",
            AdminPanelHint = "Um diese Option zu wählen, müssen Sie zuerst Admin-Panel unter Automatisierung auswählen.",
            CommLabel = "Kommunikation & Automatisierung",
            CommOptions = ["Newsletter - informieren Sie Kunden regelmäßig über Neuigkeiten und Angebote", "Automatische E-Mails - Erinnerungen, Bestätigungen, Willkommensnachrichten, Geburtstags- und Jubiläumsglückwünsche, Abwesenheitsmeldungen"],
            SalesLabel = "Verkauf & Buchungen",
            SalesOptions = ["Online-Buchungen - Kunden vereinbaren Termine selbst, ohne Anrufe", "Kundenportal - Kunden melden sich an und sehen ihre Daten, Bestellungen oder Dokumente", "Online-Shop - verkaufen Sie Produkte oder Dienstleistungen direkt online"],
            AutoLabel = "Automatisierung & Integrationen",
            AutoOptions = ["Anzahl der Anrufe und Anfragen reduzieren - das System arbeitet für Sie", "Zeit bei Routineaufgaben sparen - Dateneingabe, Auftragsverfolgung, Berichterstellung", "Microsoft 365-Integration - verbinden Sie Ihre Website mit Outlook, Teams, Calendar, OneDrive", "Admin-Panel für Daten- und Benutzerverwaltung - alles an einem Ort", "Analytics-Dashboard - Besucherzahlen, Herkunft und Nutzerverhalten direkt auf Ihrer Website"],
            ContentMgmtLabel = "Inhaltsverwaltung",
            ContentMgmtOptions = ["Ich möchte Texte und Bilder selbst aktualisieren - ohne technische Kenntnisse", "Blog - Artikel selbst hinzufügen und veröffentlichen", "Neuigkeiten-Bereich - Neuigkeiten und Updates selbst veröffentlichen"],
            ContentMgmtOtherPlaceholder = "Was noch?"
        },
        S4 = new QStep4Text
        {
            Title = "Design & Materialien",
            TextsLabel = "Texte für die Website",
            TextsHelpCheckbox = "Ich brauche Hilfe bei den Texten",
            TextsDropzoneHint = "Texte anhängen - ziehen oder klicken",
            PhotosLabel = "Fotos",
            PhotosHelpCheckbox = "Ich brauche Hilfe bei den Fotos",
            PhotosDropzoneHint = "Fotos anhängen - ziehen oder klicken",
            BrandingLabel = "Marken-Materialien - Logo / Brand book",
            BrandingHelpCheckbox = "Ich brauche Hilfe beim Branding",
            BrandingDropzoneHint = "Logo oder Brand book anhängen - ziehen oder klicken",
            SocialLinksLabel = "Social Media",
            SocialLinksHint = "Listen Sie Links zu Ihren Profilen auf, die Sie auf der Website anzeigen möchten. Z.B. LinkedIn, Xing, Facebook, Instagram...",
            StyleLabel = "Welchen Stil bevorzugen Sie?",
            StyleOptions = ["Minimal und sauber", "Modern und auffällig", "Professionell und seriös", "Warm und freundlich", "Ich bin nicht sicher - ich vertraue Ihrem Urteil"],
            StyleOtherPlaceholder = "Beschreiben Sie Ihren Stil",
            ThemeLabel = "Bevorzugen Sie ein dunkles oder helles Design?",
            ThemeDark = "Dunkles Theme",
            ThemeLight = "Helles Theme",
            ThemeAny = "Egal",
            LikedSitesLabel = "Haben Sie Websites, die Ihnen gefallen?",
            LikedSitesHint = "Links teilen - muss nicht aus Ihrer Branche sein.",
            LikedSitesWhatLabel = "Was gefällt Ihnen an diesen Websites?",
            LikedSitesWhatHint = "Z.B. Farben, Layout, Einfachheit, Animationen...",
            CompetitorSitesLabel = "Haben Sie Mitbewerber-Websites als Referenz?",
            CompetitorSitesHint = "Links zu Mitbewerber-Websites.",
            CompetitorGoodLabel = "Was macht Ihre Konkurrenz gut?",
            CompetitorGoodHint = "Was gefällt Ihnen an Mitbewerber-Websites oder -Dienstleistungen?",
            CompetitorBadLabel = "Was macht Ihre Konkurrenz schlecht?",
            CompetitorBadHint = "Wo sehen Sie eine Chance, sich abzuheben?",
            ColorsLabel = "Welche Farben möchten Sie auf der Website verwenden?",
            PrimaryColorLabel = "Primärfarbe",
            SecondaryColorLabel = "Sekundärfarbe",
            AccentColorLabel = "Akzentfarbe",
            ColorsHint = "Überspringen Sie es, wenn Sie unsicher sind. Farben wählen wir gemeinsam."
        },
        S5 = new QStep5Text
        {
            Title = "Ziele & Zeitplan",
            GoalsLabel = "Was ist das Hauptziel dieses Projekts?",
            GoalsWebHeader = "Web-Präsenz",
            GoalsWebOptions = ["Professioneller und glaubwürdiger wirken", "Bei Google gefunden werden", "Meine Dienstleistungen oder Produkte online präsentieren", "Mehr Kunden / Anfragen gewinnen"],
            GoalsBusinessHeader = "Geschäft & Wachstum",
            GoalsBusinessOptions = ["Produkte oder Dienstleistungen online verkaufen", "Bestehenden Kunden einen besseren Service bieten", "Geschäft auf neue Märkte ausweiten", "Markenbekanntheit aufbauen"],
            GoalsOtherPlaceholder = "Welches Ziel?",
            BiggestProblemLabel = "Was ist derzeit Ihre größte Herausforderung?",
            BiggestProblemHint = "Beschreiben Sie kurz, womit Sie im Geschäftsalltag am meisten kämpfen.",
            SuccessCriteriaLabel = "Woran werden Sie erkennen, dass das Projekt erfolgreich ist?",
            SuccessCriteriaHint = "Was müsste sich ändern, damit Sie sagen - das hat funktioniert?",
            AdvantagesLabel = "Was sind Ihre Hauptvorteile gegenüber Mitbewerbern?",
            AdvantagesHint = "Was unterscheidet Sie von anderen in Ihrer Branche?",
            CustomersLabel = "Wer sind Ihre Kunden?",
            CustomerTypeLabel = "Kundentyp",
            CustomerTypeOptions = ["Privatpersonen", "Unternehmen und Firmen"],
            CustomerTypeOtherPlaceholder = "Wer sind Ihre Kunden?",
            AgeLabel = "Altersgruppe",
            AgeOptions = ["Junge Erwachsene (18–35)", "Mittleres Alter (35–55)", "Ältere Generation (55+)", "Alle Altersgruppen"],
            GenderLabel = "Geschlecht",
            GenderOptions = ["Überwiegend männlich", "Überwiegend weiblich", "Gleich / alle"],
            LocationLabel = "Kundenstandort",
            LocationOptions = ["Lokal (gleiche Stadt / Region)", "National (ganzes Land)", "International"],
            StartWhenLabel = "Wann möchten Sie beginnen?",
            StartWhenOptions = ["So bald wie möglich", "Innerhalb eines Monats", "Innerhalb von 2–3 Monaten", "Ich erkunde nur Möglichkeiten"],
            DeadlineLabel = "Haben Sie einen Abgabetermin?",
            DeadlineYes = "Ja → Wann?",
            DeadlineNo = "Kein fester Termin",
            SupportLabel = "Interessieren Sie sich für Wartung und Support nach dem Projekt?",
            SupportOptions = ["Hosting - Ihre Website auf einem zuverlässigen Server", "SSL-Zertifikat - sichere Verbindung", "Regelmäßige Backups - Ihre Daten werden täglich automatisch gesichert", "Sicherheitsupdates - Schutz vor Angriffen", "Kleinere Inhaltsänderungen - Texte, Bilder, Preise und Bewertungen aktualisieren", "Technischer Support"],
            SupportOtherPlaceholder = "Was noch?",
            HeardFromLabel = "Wie haben Sie von mir erfahren?",
            HeardFromOptions = ["Empfehlung von Freund / Kollege", "Google-Suche", "LinkedIn", "Xing", "Instagram", "Facebook"],
            HeardFromOtherPlaceholder = "Wo haben Sie gehört?",
            AdditionalNotesLabel = "Gibt es noch etwas, das ich wissen sollte?",
            AdditionalNotesHint = "Schreiben Sie alles, was Sie für wichtig halten und wir nicht gefragt haben.",
            WarrantyHtml = "Alle Fehler, die auf die Entwicklung zurückzuführen sind, werden kostenlos behoben - <strong>30 Tage</strong> für Websites, <strong>60 Tage</strong> für Web-Anwendungen."
        }
    };

    private static QuestionnaireText BuildSrQuestionnaire() => new()
    {
        PageTitle = "Upitnik",
        Subtitle = "Vaši odgovori mi pomažu da pripremim ponudu prilagođenu Vašim potrebama. Molim Vas odgovorite na što više pitanja.",
        ProgressPattern = "Korak {0} od 5",
        ProgressDone = "Završeno",
        BtnNext = "Sljedeći korak →",
        BtnBack = "← Nazad",
        BtnSubmit = "Pošaljite upitnik",
        BtnSending = "Šalje se...",
        BtnOptOut = "Odjavi se",
        OptionalLabel = "opciono",
        SelectDefault = "- Odaberite -",
        OtherOption = "Ostalo: ___",
        DropzoneClickOrDrag = "Prevucite ili kliknite",
        DropzoneUploading = "Uploaduje se...",
        DropzoneError = "Greška. Pokušajte ponovo.",
        DoneTitlePattern = "Hvala, {0}!",
        DoneText1 = "Pregledat ću Vaše odgovore i javiti Vam se uskoro.",
        DoneText2 = "Ako budem imao dodatnih pitanja, kontaktiraću Vas putem e-maila.",
        ExpiredTitle = "Link je istekao",
        ExpiredText = "Ovaj link za upitnik više nije aktivan. Kontaktirajte me na <a href=\"mailto:zoran.simeunovic@outlook.de\">zoran.simeunovic@outlook.de</a> i poslat ću Vam novi link.",
        CompletedTitle = "Upitnik je već popunjen",
        CompletedText1 = "Vaši odgovori su već primljeni. Javit ću Vam se u roku od 24–48 sati.",
        CompletedText2 = "Imate li pitanja? Pišite na <a href=\"mailto:zoran.simeunovic@outlook.de\">zoran.simeunovic@outlook.de</a>.",
        CompanyRequired = "Ovo polje je obavezno.",
        SelectedPackageLabel = "Odabrani paket",
        PackagePriceHint = "Promjene u odabranim funkcionalnostima mogu uticati na konačnu cijenu.",
        PackagePriceWarning = "Ova promjena može uticati na cijenu u odnosu na odabrani paket. Da li želite nastaviti?",
        PackagePriceWarningConfirm = "Nastavi",
        PackagePriceWarningCancel = "Otkaži",
        OptOutConfirmMsg = "Da li ste sigurni da se želite odjaviti?\n\nOvo će prouzrokovati brisanje Vaših dosadašnjih odgovora i kontakt podataka.",
        OptOutDoneHtml = "Vaši podaci su uspješno obrisani.",
        OptOutError = "Greška pri slanju zahtjeva. Pokušajte ponovo.",
        PagesUnderLimitMsg = "Možete još dodati {0} stranicu/a uključenih u Vaš paket. Nastavite ili se vratite da dodate još.",
        PagesOverLimitMsg = "Odabrali ste {0} stranicu/a više nego što je uključeno u paket, što može uticati na cijenu. Nastavite ili se vratite da smanjite odabir.",
        PagesModalContinue = "Nastavi",
        PagesModalChange = "Promijeni odabir",
        SalesRequiredMsg = "Odaberite online rezervacije ili klijentski portal.",
        S1 = new QStep1Text
        {
            Title = "O Vašem poslovanju",
            CompanyLabel = "Naziv kompanije / brend",
            CompanyHint = "Ako nemate kompaniju, upišite \"privatno\".",
            CompanyPlaceholder = "npr. Moja Firma d.o.o.",
            IndustryLabel = "U kojoj industriji poslujete?",
            IndustryOptions = ["Trgovina / E-commerce", "Zdravlje i wellness", "Građevinarstvo i nekretnine", "Ugostiteljstvo i turizam", "Obrazovanje i obuka", "IT i tehnologija", "Konsalting i usluge", "Ljepota i moda"],
            IndustryOtherPlaceholder = "Unesite industriju",
            TeamSizeLabel = "Veličina tima",
            TeamSizeOptions = ["Samo ja", "2–10 zaposlenih", "11–50 zaposlenih", "50+ zaposlenih"],
            BrandDescLabel = "Kakav je Vaš brend?",
            BrandDescOptions = ["Pristupačan i prijateljski", "Profesionalan i ozbiljan", "Premium i ekskluzivan", "Moderan i inovativan", "Tradicionalan i pouzdan"],
            BrandDescOtherPlaceholder = "Opišite Vaš brend"
        },
        S2 = new QStep2Text
        {
            Title = "Web stranica",
            NeedWhatLabel = "Vrsta projekta",
            ResponsiveLabel = "Responzivan dizajn - prilagođen za desktop, tablet i mobilni",
            WebsiteTypeOptions = ["Landing stranica", "Prezentacioni sajt", "Portfolio sajt", "Sajt s galerijom / blogom", "Katalog proizvoda ili usluga", "Redizajn postojećeg sajta"],
            WebsiteTypeOtherPlaceholder = "Opišite Vašu potrebu",
            ExistingUrlLabel = "URL postojeće web stranice",
            ExistingUrlPlaceholder = "https://vaša-stranica.com",
            CurrentSiteDescLabel = "Kakav je Vaš trenutni sajt?",
            CurrentSiteDescOptions = ["Vizuelno zastarjel", "Nije prilagođen mobilnim uređajima", "Težak za navigaciju", "Spor", "Nedostaju važne informacije", "Ne pojavljuje se na Googleu", "Zadovoljan/na sam, ali trebam nove funkcionalnosti"],
            CurrentSiteDescOtherPlaceholder = "Opišite",
            WantToChangeLabel = "Šta promijeniti?",
            WantToChangeOptions = ["Vizuelni izgled", "Strukturu i navigaciju", "Sadržaj i tekstove", "Brzinu i performanse", "Mobilnu verziju"],
            WantToChangeOtherPlaceholder = "Opišite",
            PagesLabel = "Stranice",
            PagesOptions = ["Početna stranica", "O nama", "Usluge / Proizvodi", "Cjenovnik", "Galerija / Portfolio", "Kontakt", "FAQ / Često postavljena pitanja", "Tim", "Karijere", "Recenzije - izmjene kroz podršku i održavanje"],
            PagesPortfolioOptions = ["O nama", "Radovi / Portfolio", "Kontakt", "Blog / Vijesti"],
            PagesOtherPlaceholder = "Koje stranice?",
            ExtrasLabel = "Dodatne funkcije",
            CommHeader = "Kontakt",
            CommOptions = ["Kontakt forma - klijenti Vas kontaktiraju direktno s Vašeg sajta", "WhatsApp dugme - klijenti Vas kontaktiraju direktno putem WhatsApp-a", "Chatbot - automatski chat asistent na Vašem sajtu"],
            GrowthHeader = "Vidljivost i rast",
            GrowthOptions = ["SEO osnove - da Vas klijenti lakše pronađu na Googleu", "SEO napredna optimizacija - istraživanje ključnih riječi, content strategija, izgradnja linkova", "Google Maps integracija - prikažite lokaciju Vaše firme na mapi", "Višejezična podrška (max. 3 jezika)", "Online plaćanje", "Google ili Trustpilot recenzije - automatski prikaz ocjena s platforme"],
            MultilingualPlaceholder = "Npr. njemački, engleski, švedski...",
            MultilingualExtraPlaceholder = "Napišite koji jezici su vam još dodatno potrebni",
            OnlinePaymentPlaceholder = "Npr. Visa, Mastercard, American Express, PayPal, Klarna, Apple Pay...",
            ReviewsPlatformPlaceholder = "Odaberite platformu",
            ExtrasOtherPlaceholder = "Šta još?"
        },
        S3 = new QStep3Text
        {
            Title = "Web aplikacija i automatizacija",
            SectionHint = "Odaberite tip aplikacije i funkcionalnosti koje Vam trebaju. Pitanja ispod odnose se na odabrani tip.",
            AppTypeLabel = "Šta Vam je potrebno?",
            AppTypeOptions = ["Web aplikacija", "Desktop aplikacija", "Mobilna aplikacija (Android / iOS)"],
            AppTypeOtherPlaceholder = "Opišite",
            AppUsersLabel = "Korisnici aplikacije",
            AppUsersOptions = ["Ja / interni tim", "Moji klijenti"],
            AppUsersOtherPlaceholder = "Ko?",
            AppDisabledHint = "Molimo Vas odaberite bar jednu opciju pod \"Šta Vam je potrebno?\".",
            AdminPanelHint = "Da biste odabrali ovu opciju, morate prvo odabrati Admin panel pod Automatizacijom.",
            CommLabel = "Komunikacija i automatizacija",
            CommOptions = ["Newsletter - redovno obavještavajte klijente o novostima i ponudama", "Automatske email poruke - podsjetnici, potvrde, dobrodošlice, čestitke za rođendane i godišnjice, obavještenja o odsustvu"],
            SalesLabel = "Prodaja i rezervacije",
            SalesOptions = ["Online rezervacije - klijenti sami zakazuju termine, bez telefonskih poziva", "Klijentski portal - klijenti se prijavljuju i vide svoje podatke, narudžbe ili dokumente", "Online prodavnica - prodajte proizvode ili usluge direktno online"],
            AutoLabel = "Automatizacija i integracije",
            AutoOptions = ["Smanjiti broj telefonskih poziva i upita - sistem radi umjesto Vas", "Uštediti vrijeme na ponavljajućim zadacima - unos podataka, praćenje narudžbi, generisanje izvještaja", "Microsoft 365 integracija - povežite sajt s Outlook, Teams, Calendar, OneDrive", "Admin panel za upravljanje podacima i korisnicima - sve na jednom mjestu", "Statistika posjeta - pregledajte broj posjetilaca, izvore prometa i ponašanje korisnika direktno na Vašem sajtu"],
            ContentMgmtLabel = "Upravljanje sadržajem",
            ContentMgmtOptions = ["Želim sam ažurirati tekstove i slike - bez tehničkog znanja", "Blog - sami dodajete i objavljujete članke", "Sekcija vijesti - sami objavljujete vijesti i novosti"],
            ContentMgmtOtherPlaceholder = "Šta još?"
        },
        S4 = new QStep4Text
        {
            Title = "Dizajn i materijali",
            TextsLabel = "Tekstovi za sajt",
            TextsHelpCheckbox = "Potrebna mi je pomoć oko tekstova",
            TextsDropzoneHint = "Priložite tekstove - prevucite ili kliknite",
            PhotosLabel = "Fotografije",
            PhotosHelpCheckbox = "Potrebna mi je pomoć oko fotografija",
            PhotosDropzoneHint = "Priložite fotografije - prevucite ili kliknite",
            BrandingLabel = "Brendirani materijali - Logo / Brand book",
            BrandingHelpCheckbox = "Potrebna mi je pomoć oko brendiranja",
            BrandingDropzoneHint = "Priložite logo ili brand book - prevucite ili kliknite",
            SocialLinksLabel = "Društvene mreže",
            SocialLinksHint = "Navedite linkove ka Vašim profilima koje želite prikazati na sajtu. Npr. LinkedIn, Xing, Facebook, Instagram...",
            StyleLabel = "Preferirani stil",
            StyleOptions = ["Minimalan i čist", "Moderan i upečatljiv", "Profesionalan i ozbiljan", "Topao i prijateljski", "Nisam siguran/na - vjerujem Vašem sudu"],
            StyleOtherPlaceholder = "Opišite željeni stil",
            ThemeLabel = "Tamna ili svijetla tema?",
            ThemeDark = "Tamna tema",
            ThemeLight = "Svijetla tema",
            ThemeAny = "Svejedno",
            LikedSitesLabel = "Sajtovi koji Vam se sviđaju",
            LikedSitesHint = "Podijelite linkove - ne mora biti iz Vaše industrije.",
            LikedSitesWhatLabel = "Šta Vam se sviđa na tim sajtovima?",
            LikedSitesWhatHint = "Npr. boje, raspored, jednostavnost, animacije...",
            CompetitorSitesLabel = "Sajtovi konkurencije",
            CompetitorSitesHint = "Linkovi ka sajtovima konkurencije.",
            CompetitorGoodLabel = "Šta konkurencija radi dobro?",
            CompetitorGoodHint = "Šta Vam se sviđa kod konkurentskih sajtova ili usluga?",
            CompetitorBadLabel = "Šta konkurencija radi loše?",
            CompetitorBadHint = "Gdje vidite priliku da se istaknete?",
            ColorsLabel = "Koje boje biste željeli koristiti na sajtu?",
            PrimaryColorLabel = "Primarna boja",
            SecondaryColorLabel = "Sekundarna boja",
            AccentColorLabel = "Akcent boja",
            ColorsHint = "Ako niste sigurni, preskočite. Možemo zajedno odabrati boje koje odgovaraju Vašem brendu."
        },
        S5 = new QStep5Text
        {
            Title = "Ciljevi i rokovi",
            GoalsLabel = "Ciljevi projekta",
            GoalsWebHeader = "Web prisustvo",
            GoalsWebOptions = ["Izgledati profesionalnije i vjerodostojnije", "Biti pronađen na Googleu", "Predstaviti usluge ili proizvode online", "Privući više klijenata / upita"],
            GoalsBusinessHeader = "Poslovanje i rast",
            GoalsBusinessOptions = ["Prodavati proizvode ili usluge online", "Pružiti bolju uslugu postojećim klijentima", "Proširiti poslovanje na nova tržišta", "Izgraditi prepoznatljivost brenda"],
            GoalsOtherPlaceholder = "Koji cilj?",
            BiggestProblemLabel = "Šta Vam trenutno predstavlja najveći problem?",
            BiggestProblemHint = "Opišite ukratko s čime se najviše borite u poslovanju.",
            SuccessCriteriaLabel = "Kriteriji uspjeha",
            SuccessCriteriaHint = "Šta bi se moralo promijeniti da kažete - ovo je uspjelo?",
            AdvantagesLabel = "Koje su Vaše glavne prednosti u odnosu na konkurenciju?",
            AdvantagesHint = "Šta Vas izdvaja od ostalih u Vašoj branši?",
            CustomersLabel = "Ko su Vaši kupci / klijenti?",
            CustomerTypeLabel = "Tip kupaca",
            CustomerTypeOptions = ["Privatne osobe", "Preduzeća i firme"],
            CustomerTypeOtherPlaceholder = "Ko su Vaši klijenti?",
            AgeLabel = "Starosna grupa",
            AgeOptions = ["Mlađi odrasli (18–35)", "Srednja generacija (35–55)", "Starija generacija (55+)", "Sve starosne grupe"],
            GenderLabel = "Spol",
            GenderOptions = ["Pretežno muški", "Pretežno ženski", "Podjednako / svi"],
            LocationLabel = "Lokacija kupaca",
            LocationOptions = ["Lokalni (isti grad / region)", "Nacionalni (cijela zemlja)", "Međunarodni"],
            StartWhenLabel = "Kada početi?",
            StartWhenOptions = ["Što prije moguće", "U roku od mjesec dana", "U roku od 2–3 mjeseca", "Samo istražujem mogućnosti"],
            DeadlineLabel = "Imate li rok završetka?",
            DeadlineYes = "Da → Kada?",
            DeadlineNo = "Nemam fiksni rok",
            SupportLabel = "Podrška i održavanje",
            SupportOptions = ["Hosting - Vaš sajt na pouzdanom serveru", "SSL certifikat - sigurna veza, zeleni katanac u browseru", "Redovni backupi - Vaši podaci se automatski čuvaju na dnevnoj bazi", "Sigurnosni updates - zaštita od hakerskih napada", "Manje izmjene sadržaja - ažuriranje tekstova, slika, cijena i recenzija", "Tehnička podrška"],
            SupportOtherPlaceholder = "Šta još?",
            HeardFromLabel = "Kako ste čuli za mene?",
            HeardFromOptions = ["Preporuka prijatelja / kolege", "Google pretraga", "LinkedIn", "Xing", "Instagram", "Facebook"],
            HeardFromOtherPlaceholder = "Gdje ste čuli?",
            AdditionalNotesLabel = "Napomene",
            AdditionalNotesHint = "Napišite sve što smatrate važnim a nismo pitali.",
            WarrantyHtml = "Sve greške nastale kao rezultat razvoja biće ispravljene besplatno - <strong>30 dana</strong> za web stranice, <strong>60 dana</strong> za web aplikacije."
        }
    };
}
