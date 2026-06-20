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
        PageTitle = "Zoran Simeunović — Full Stack Web Developer",
        LanguageLabel = "Language",
        Nav = new NavText { Home = "HOME", Work = "WORK", Process = "PROCESS", ContactMe = "CONTACT ME" },
        Hero = new HeroText
        {
            Badge = "Full Stack Web Developer",
            HeadingHtml = "I build <span class=\"accent\">web solutions</span> that help you grow",
            Subtitle1 = "Your business deserves a professional web presence.",
            Subtitle2 = "Let's build it together.",
            ContactMe = "CONTACT ME",
            MyWork = "MY WORK",
            ProjectsCount = "5+ WEB AND DESKTOP PROJECTS",
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
                new() { Title = "HRIS/Web", Image = "hris.jpg", Duration = "2 years", DurationSub = "at AddWare Solutions",
                    Description = "Development and maintenance of an HRIS (Human Resources Information System) for workforce management, including personnel planning, vacation management, and time tracking. Integration of web services, sync services, and Microsoft 365. Optimization of existing systems and development of new modules.",
                    Techs = new() { "ASP.NET", "C#", "MySQL", "Microsoft Graph" } },
                new() { Title = "Desktop GUI", Image = "desktop-gui.png", Duration = "3 years", DurationSub = "at AddWare Solutions",
                    Description = "Complete redesign of a desktop GUI focused on performance and scalability. Enabled fast rendering of large employee datasets in grid tables with 12+ months of historical data. Improved business logic, optimized large data handling, and delivered new and enhanced modules.",
                    Techs = new() { "C#", "WPF", "MySQL" } },
                new() { Title = "Zeiterfassung", Image = "zeiterfassung.png", Duration = "1 year", DurationSub = "at AddWare Solutions",
                    Description = "Development and enhancement of desktop, web, and mobile clients for time tracking, providing real-time data visualization and centralized management of working hours, breaks, and absences within a unified system.",
                    Techs = new() { "ASP.NET", "WPF", "Xamarin Forms" } },
                new() { Title = "Personal Portfolio", Image = "personal-portfolio.png", Duration = null, DurationSub = null,
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
            Steps = new List<ProcessStep>
            {
                new() { Tag = "FIRST STEP" },
                new() { Tag = "100% VIA EMAIL", Title = "Project questionnaire", Description = "Your business, Current online presence, Project goals, Type of project, Timeline, Design preferences" },
                new() { Tag = "FREE", Title = "Quote", Description = "I review your answers, ask any follow-up questions, and send a detailed quote." },
                new() { Tag = "BEFORE WORK", Title = "Project agreement", Description = "Clear scope, price and milestones in writing. No surprises." },
                new() { Tag = "YOUR APPROVAL REQUIRED", Title = "Design preview", Description = "See the full design first. Nothing gets built without your approval." },
                new() { Tag = "BUILDING YOUR SITE", Title = "Website", Description = "The approved design becomes a fast, working website. No silence, you'll always know where things stand." },
                new() { Tag = "TIME TO REVIEW", Title = "Website review", Description = "You test the website and request changes if needed." },
                new() { Tag = "TIME FOR THE ENGINE", Title = "Web App (optional)", Description = "Website approved. Custom tools built behind the scenes. Portals, bookings, dashboards." },
                new() { Tag = "TIME TO REVIEW", Title = "Web App review (optional)", Description = "You test everything end to end. Request changes if needed." },
                new() { Tag = "GO LIVE", Title = "Launch & handover", Description = "Deploy, docs, and handover. Final invoice on delivery." },
                new() { Tag = "OPTIONAL", Title = "Ongoing support", Description = "On your terms. Hosting, SSL, backups, bug fixes, or new features — take only what your business needs." }
            }
        },
        Improve = new ImproveSection
        {
            TitleHtml = "How to improve <span class=\"accent\">web presence?</span>",
            Subtitle1 = "Do you need a redesign, a new website, or a web app?",
            Subtitle2 = "Get a quick analysis.",
            StartAnalysis = "Start Analysis",
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
        Contact = new ContactSection
        {
            TitleHtml = "Take the first step",
            Subtitle1 = "Leave your name and email, I'll send you",
            Subtitle2 = "a short questionnaire to get started",
            NamePlaceholder = "Your name",
            EmailPlaceholder = "Your email address",
            GetStarted = "GET STARTED",
            PrivacyNote = "By submitting, you agree to our Privacy Policy. We respect your privacy.",
            SuccessMessage = "Thank you! Your details have been received. I'll be in touch soon.",
            ErrorMessage = "Something went wrong. Please try again later."
        },
        Footer = new FooterText
        {
            Home = "HOME", Work = "WORK", Process = "PROCESS",
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
            PrivacyHtml = @"<p>This Privacy Policy explains how ZS.dev (""I"", ""me"") handles your information when you visit this website.</p>
<h3>No cookies</h3><p>This website does not use cookies, tracking pixels, or third-party analytics.</p>
<h3>Data I collect</h3><p>The only personal data collected is the name and email address you voluntarily submit through the contact form.</p>
<h3>Purpose</h3><p>This information is used solely to respond to your inquiry and send you a short questionnaire. It is never sold or shared with third parties.</p>
<h3>Storage</h3><p>Your data is stored securely and kept only as long as necessary to handle your request.</p>
<h3>Your rights</h3><p>You may request access to, correction of, or deletion of your data at any time by emailing <a href=""mailto:zoran.simeunovic@outlook.de"">zoran.simeunovic@outlook.de</a>.</p>
<p class=""legal-updated"">Last updated: June 2026.</p>",
            TermsTitle = "Terms of Service",
            TermsHtml = @"<p>By using this website you agree to the following terms.</p>
<h3>Purpose</h3><p>This website is a personal portfolio presenting the work and services of Zoran Simeunović (ZS.dev).</p>
<h3>Content</h3><p>All content, text, and images are the property of ZS.dev unless otherwise stated and may not be reused without permission.</p>
<h3>No warranty</h3><p>The website is provided ""as is"" without warranties of any kind. Information may change without notice.</p>
<h3>Contact form</h3><p>By submitting the contact form you confirm the information provided is accurate. Submitting does not create any binding agreement.</p>
<h3>Liability</h3><p>ZS.dev is not liable for any damages arising from the use of this website.</p>
<p class=""legal-updated"">Last updated: June 2026.</p>"
        }
    };

    // ------------------------------------------------------------------ GERMAN
    private static SiteText BuildDe() => new()
    {
        Culture = "de",
        PageTitle = "Zoran Simeunović — Full-Stack Webentwickler",
        LanguageLabel = "Sprache",
        Nav = new NavText { Home = "START", Work = "PROJEKTE", Process = "ABLAUF", ContactMe = "KONTAKT" },
        Hero = new HeroText
        {
            Badge = "Full-Stack Webentwickler",
            HeadingHtml = "Ich entwickle <span class=\"accent\">Web-Lösungen</span>, die Ihr Wachstum fördern",
            Subtitle1 = "Ihr Unternehmen verdient einen professionellen Webauftritt.",
            Subtitle2 = "Lassen Sie ihn uns gemeinsam aufbauen.",
            ContactMe = "KONTAKT",
            MyWork = "MEINE ARBEIT",
            ProjectsCount = "5+ WEB- UND DESKTOP-PROJEKTE",
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
                new() { Title = "HRIS/Web", Image = "hris.jpg", Duration = "2 Jahre", DurationSub = "bei AddWare Solutions",
                    Description = "Entwicklung und Wartung eines HRIS (Human Resources Information System) für die Personalverwaltung, einschließlich Personalplanung, Urlaubsverwaltung und Zeiterfassung. Integration von Webdiensten, Synchronisationsdiensten und Microsoft 365. Optimierung bestehender Systeme und Entwicklung neuer Module.",
                    Techs = new() { "ASP.NET", "C#", "MySQL", "Microsoft Graph" } },
                new() { Title = "Desktop GUI", Image = "desktop-gui.png", Duration = "3 Jahre", DurationSub = "bei AddWare Solutions",
                    Description = "Komplette Neugestaltung einer Desktop-GUI mit Fokus auf Leistung und Skalierbarkeit. Ermöglichte das schnelle Rendern großer Mitarbeiterdatensätze in Tabellen mit über 12 Monaten Verlaufsdaten. Verbesserte Geschäftslogik, optimierte Verarbeitung großer Datenmengen und Bereitstellung neuer und erweiterter Module.",
                    Techs = new() { "C#", "WPF", "MySQL" } },
                new() { Title = "Zeiterfassung", Image = "zeiterfassung.png", Duration = "1 Jahr", DurationSub = "bei AddWare Solutions",
                    Description = "Entwicklung und Erweiterung von Desktop-, Web- und mobilen Clients zur Zeiterfassung mit Echtzeit-Datenvisualisierung und zentraler Verwaltung von Arbeitszeiten, Pausen und Abwesenheiten in einem einheitlichen System.",
                    Techs = new() { "ASP.NET", "WPF", "Xamarin Forms" } },
                new() { Title = "Personal Portfolio", Image = "personal-portfolio.png", Duration = null, DurationSub = null,
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
            Steps = new List<ProcessStep>
            {
                new() { Tag = "ERSTER SCHRITT" },
                new() { Tag = "100% PER E-MAIL", Title = "Projekt-Fragebogen", Description = "Ihr Unternehmen, aktuelle Online-Präsenz, Projektziele, Projektart, Zeitrahmen, Designwünsche" },
                new() { Tag = "KOSTENLOS", Title = "Angebot", Description = "Ich prüfe Ihre Antworten, stelle eventuelle Rückfragen und sende ein detailliertes Angebot." },
                new() { Tag = "VOR ARBEITSBEGINN", Title = "Projektvereinbarung", Description = "Klarer Umfang, Preis und Meilensteine schriftlich. Keine Überraschungen." },
                new() { Tag = "IHRE FREIGABE ERFORDERLICH", Title = "Design-Vorschau", Description = "Sehen Sie zuerst das vollständige Design. Es wird nichts ohne Ihre Freigabe gebaut." },
                new() { Tag = "AUFBAU IHRER SEITE", Title = "Website", Description = "Das freigegebene Design wird zu einer schnellen, funktionierenden Website. Kein Schweigen – Sie wissen jederzeit, wo es steht." },
                new() { Tag = "ZEIT ZUM PRÜFEN", Title = "Website-Prüfung", Description = "Sie testen die Website und fordern bei Bedarf Änderungen an." },
                new() { Tag = "ZEIT FÜR DEN MOTOR", Title = "Web-App (optional)", Description = "Website freigegeben. Maßgeschneiderte Tools im Hintergrund. Portale, Buchungen, Dashboards." },
                new() { Tag = "ZEIT ZUM PRÜFEN", Title = "Web-App-Prüfung (optional)", Description = "Sie testen alles von Anfang bis Ende. Fordern Sie bei Bedarf Änderungen an." },
                new() { Tag = "GO-LIVE", Title = "Launch & Übergabe", Description = "Bereitstellung, Dokumentation und Übergabe. Schlussrechnung bei Lieferung." },
                new() { Tag = "OPTIONAL", Title = "Laufender Support", Description = "Zu Ihren Bedingungen. Hosting, SSL, Backups, Fehlerbehebungen oder neue Funktionen – nur was Ihr Unternehmen braucht." }
            }
        },
        Improve = new ImproveSection
        {
            TitleHtml = "Wie Sie Ihre <span class=\"accent\">Web-Präsenz verbessern?</span>",
            Subtitle1 = "Brauchen Sie ein Redesign, eine neue Website oder eine Web-App?",
            Subtitle2 = "Erhalten Sie eine schnelle Analyse.",
            StartAnalysis = "Analyse starten",
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
        Contact = new ContactSection
        {
            TitleHtml = "Machen Sie den ersten Schritt",
            Subtitle1 = "Hinterlassen Sie Ihren Namen und Ihre E-Mail, ich sende Ihnen",
            Subtitle2 = "einen kurzen Fragebogen für den Start",
            NamePlaceholder = "Ihr Name",
            EmailPlaceholder = "Ihre E-Mail-Adresse",
            GetStarted = "LOSLEGEN",
            PrivacyNote = "Mit dem Absenden stimmen Sie unserer Datenschutzerklärung zu. Wir respektieren Ihre Privatsphäre.",
            SuccessMessage = "Vielen Dank! Ihre Angaben sind eingegangen. Ich melde mich in Kürze.",
            ErrorMessage = "Etwas ist schiefgelaufen. Bitte versuchen Sie es später erneut."
        },
        Footer = new FooterText
        {
            Home = "START", Work = "PROJEKTE", Process = "ABLAUF",
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
            PrivacyHtml = @"<p>Diese Datenschutzerklärung erklärt, wie ZS.dev (""ich"") mit Ihren Daten umgeht, wenn Sie diese Website besuchen.</p>
<h3>Keine Cookies</h3><p>Diese Website verwendet keine Cookies, Tracking-Pixel oder Analyse-Dienste von Drittanbietern.</p>
<h3>Erhobene Daten</h3><p>Die einzigen erhobenen personenbezogenen Daten sind der Name und die E-Mail-Adresse, die Sie freiwillig über das Kontaktformular übermitteln.</p>
<h3>Zweck</h3><p>Diese Informationen werden ausschließlich verwendet, um auf Ihre Anfrage zu antworten und Ihnen einen kurzen Fragebogen zu senden. Sie werden niemals verkauft oder an Dritte weitergegeben.</p>
<h3>Speicherung</h3><p>Ihre Daten werden sicher gespeichert und nur so lange aufbewahrt, wie es zur Bearbeitung Ihrer Anfrage erforderlich ist.</p>
<h3>Ihre Rechte</h3><p>Sie können jederzeit Auskunft, Berichtigung oder Löschung Ihrer Daten verlangen, per E-Mail an <a href=""mailto:zoran.simeunovic@outlook.de"">zoran.simeunovic@outlook.de</a>.</p>
<p class=""legal-updated"">Zuletzt aktualisiert: Juni 2026.</p>",
            TermsTitle = "Allgemeine Geschäftsbedingungen",
            TermsHtml = @"<p>Durch die Nutzung dieser Website stimmen Sie den folgenden Bedingungen zu.</p>
<h3>Zweck</h3><p>Diese Website ist ein persönliches Portfolio, das die Arbeit und Leistungen von Zoran Simeunović (ZS.dev) präsentiert.</p>
<h3>Inhalt</h3><p>Alle Inhalte, Texte und Bilder sind Eigentum von ZS.dev, sofern nicht anders angegeben, und dürfen nicht ohne Genehmigung wiederverwendet werden.</p>
<h3>Keine Gewährleistung</h3><p>Die Website wird ""wie besehen"" ohne jegliche Gewährleistung bereitgestellt. Informationen können sich ohne Vorankündigung ändern.</p>
<h3>Kontaktformular</h3><p>Mit dem Absenden des Kontaktformulars bestätigen Sie, dass die angegebenen Informationen korrekt sind. Das Absenden begründet keine verbindliche Vereinbarung.</p>
<h3>Haftung</h3><p>ZS.dev haftet nicht für Schäden, die aus der Nutzung dieser Website entstehen.</p>
<p class=""legal-updated"">Zuletzt aktualisiert: Juni 2026.</p>"
        }
    };

    // --------------------------------------------------------- SRPSKI (LATINICA)
    private static SiteText BuildSr() => new()
    {
        Culture = "sr-Latn",
        PageTitle = "Zoran Simeunović — Full Stack Web Developer",
        LanguageLabel = "Jezik",
        Nav = new NavText { Home = "POČETNA", Work = "RADOVI", Process = "PROCES", ContactMe = "KONTAKT" },
        Hero = new HeroText
        {
            Badge = "Full Stack Web Developer",
            HeadingHtml = "Pravim <span class=\"accent\">web rešenja</span> koja pomažu vašem rastu",
            Subtitle1 = "Vaše poslovanje zaslužuje profesionalno web prisustvo.",
            Subtitle2 = "Hajde da ga napravimo zajedno.",
            ContactMe = "KONTAKT",
            MyWork = "MOJI RADOVI",
            ProjectsCount = "5+ WEB I DESKTOP PROJEKATA",
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
                new() { Title = "HRIS/Web", Image = "hris.jpg", Duration = "2 godine", DurationSub = "u AddWare Solutions",
                    Description = "Razvoj i održavanje HRIS-a (Human Resources Information System) za upravljanje radnom snagom, uključujući kadrovsko planiranje, upravljanje godišnjim odmorima i evidenciju radnog vremena. Integracija web servisa, sinhronizacionih servisa i Microsoft 365. Optimizacija postojećih sistema i razvoj novih modula.",
                    Techs = new() { "ASP.NET", "C#", "MySQL", "Microsoft Graph" } },
                new() { Title = "Desktop GUI", Image = "desktop-gui.png", Duration = "3 godine", DurationSub = "u AddWare Solutions",
                    Description = "Potpuni redizajn desktop GUI-ja sa fokusom na performanse i skalabilnost. Omogućeno brzo prikazivanje velikih skupova podataka o zaposlenima u tabelama sa preko 12 meseci istorijskih podataka. Poboljšana poslovna logika, optimizovano rukovanje velikim podacima i isporučeni novi i unapređeni moduli.",
                    Techs = new() { "C#", "WPF", "MySQL" } },
                new() { Title = "Zeiterfassung", Image = "zeiterfassung.png", Duration = "1 godina", DurationSub = "u AddWare Solutions",
                    Description = "Razvoj i unapređenje desktop, web i mobilnih klijenata za evidenciju radnog vremena, uz vizuelizaciju podataka u realnom vremenu i centralizovano upravljanje radnim satima, pauzama i odsustvima u okviru jedinstvenog sistema.",
                    Techs = new() { "ASP.NET", "WPF", "Xamarin Forms" } },
                new() { Title = "Personal Portfolio", Image = "personal-portfolio.png", Duration = null, DurationSub = null,
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
            Highlight3Html = "Sajtovi i <span class=\"accent\">web</span> rešenja",
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
            Steps = new List<ProcessStep>
            {
                new() { Tag = "PRVI KORAK" },
                new() { Tag = "100% PUTEM MEJLA", Title = "Upitnik o projektu", Description = "Vaše poslovanje, trenutno online prisustvo, ciljevi projekta, tip projekta, rokovi, željeni dizajn" },
                new() { Tag = "BESPLATNO", Title = "Ponuda", Description = "Pregledam vaše odgovore, postavim eventualna dodatna pitanja i šaljem detaljnu ponudu." },
                new() { Tag = "PRE POČETKA RADA", Title = "Dogovor o projektu", Description = "Jasan obim, cena i prekretnice u pisanoj formi. Bez iznenađenja." },
                new() { Tag = "POTREBNO VAŠE ODOBRENJE", Title = "Pregled dizajna", Description = "Prvo vidite kompletan dizajn. Ništa se ne gradi bez vašeg odobrenja." },
                new() { Tag = "IZRADA VAŠEG SAJTA", Title = "Sajt", Description = "Odobreni dizajn postaje brz, funkcionalan sajt. Bez ćutanja — uvek znate dokle se stiglo." },
                new() { Tag = "VREME ZA PROVERU", Title = "Pregled sajta", Description = "Testirate sajt i tražite izmene ako su potrebne." },
                new() { Tag = "VREME ZA MOTOR", Title = "Web aplikacija (opciono)", Description = "Sajt odobren. Prilagođeni alati grade se iza scene. Portali, rezervacije, dashboardi." },
                new() { Tag = "VREME ZA PROVERU", Title = "Pregled web aplikacije (opciono)", Description = "Testirate sve od početka do kraja. Tražite izmene ako su potrebne." },
                new() { Tag = "PUŠTANJE U RAD", Title = "Lansiranje i predaja", Description = "Postavljanje, dokumentacija i predaja. Konačna faktura pri isporuci." },
                new() { Tag = "OPCIONO", Title = "Kontinuirana podrška", Description = "Pod vašim uslovima. Hosting, SSL, rezervne kopije, ispravke grešaka ili nove funkcije — uzimate samo ono što vašem poslovanju treba." }
            }
        },
        Improve = new ImproveSection
        {
            TitleHtml = "Kako poboljšati <span class=\"accent\">web prisustvo?</span>",
            Subtitle1 = "Treba li vam redizajn, novi sajt ili web aplikacija?",
            Subtitle2 = "Dobijte brzu analizu.",
            StartAnalysis = "Započni analizu",
            Lists = new List<ImproveList>
            {
                new()
                {
                    Key = "redesign",
                    HeadingHtml = "Treba li vašem sajtu <span class=\"accent\">redizajn</span>?",
                    Groups = new()
                    {
                        new() { Title = "Analiza", Items = new() { "Da li je sajt vizuelno zastareo?", "Da li je mobilna verzija loša ili je nema?", "Da li je navigacija zbunjujuća?", "Da li je odmah jasno čime se kompanija bavi?" } },
                        new() { Title = "Dizajn", Items = new() { "Da li su boje i fontovi dosledni?", "Postoji li jasna vizuelna hijerarhija (naslovi, sekcije)?", "Da li su CTA dugmad vidljiva i jasna?", "Ima li previše teksta bez dovoljno praznog prostora?" } },
                        new() { Title = "UX / Funkcionalnost", Items = new() { "Da li se kontakt informacije lako pronalaze?", "Da li se sajt brzo učitava?", "Mogu li korisnici pronaći potrebne informacije u roku od 5 sekundi?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Solidan i funkcionalan. Redizajn verovatno nije potreban." },
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
                        new() { Title = "Potreba", Items = new() { "Da li primate upite preko poruka umesto preko kontakt forme?", "Da li klijentima često ponavljate iste informacije?", "Da li nudite usluge ili cene koje bi trebalo da budu javno dostupne onlajn?" } },
                        new() { Title = "Spremnost", Items = new() { "Želite li da vaše poslovanje ima profesionalnije web prisustvo?", "Želite li da vas klijenti automatski pronalaze putem Google-a?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Vaše trenutno web prisustvo je možda dovoljno. Sajt bi mogao pomoći, ali nije hitno." },
                        new() { Min = 3, Max = 5, Text = "Vaše web prisustvo ima praznine. Jednostavan, profesionalan sajt pomogao bi klijentima da vas pronađu." },
                        new() { Min = 6, Max = 7, Text = "Klijenti verovatno teško dolaze do vas. Jače web prisustvo napravilo bi jasnu razliku." },
                        new() { Min = 8, Max = 9, Text = "Svakog dana gubite klijente. Profesionalan sajt je sledeći logičan korak." }
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
                        new() { Title = "Potencijal za web aplikaciju", Items = new() { "Da li bi vam sistem uštedeo bar 1–2 sata dnevno?", "Da li bi automatizacija smanjila greške?", "Želite li centralno mesto za sve vaše podatke?" } }
                    },
                    Scores = new()
                    {
                        new() { Min = 0, Max = 2, Text = "Vaši trenutni procesi deluju upravljivo. Automatizacija možda trenutno nije prioritet." },
                        new() { Min = 3, Max = 5, Text = "Neki zadaci bi mogli da se automatizuju. Mala poboljšanja uštedela bi vreme i smanjila greške." },
                        new() { Min = 6, Max = 7, Text = "Ručni rad vas usporava. Web aplikacija bi značajno pojednostavila vaše poslovanje." },
                        new() { Min = 8, Max = 9, Text = "Vaše poslovanje gubi vreme i novac na ručnim procesima. Prilagođena web aplikacija brzo bi se isplatila." }
                    }
                }
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
            PrivacyNote = "Slanjem prihvatate našu Politiku privatnosti. Poštujemo vašu privatnost.",
            SuccessMessage = "Hvala! Vaši podaci su primljeni. Javiću vam se uskoro.",
            ErrorMessage = "Došlo je do greške. Molimo pokušajte ponovo kasnije."
        },
        Footer = new FooterText
        {
            Home = "POČETNA", Work = "RADOVI", Process = "PROCES",
            Copyright = "© 2026 ZS.dev Sva prava zadržana.",
            PrivacyPolicy = "Politika privatnosti", TermsOfService = "Uslovi korišćenja", CookieSettings = "Podešavanja kolačića"
        },
        Cookie = new CookieText
        {
            Title = "Podešavanja kolačića",
            Body = "Ovaj sajt ne koristi kolačiće. Bez praćenja, bez analitike, bez kolačića trećih strana. Vaša privatnost je u potpunosti poštovana.",
            Close = "Razumem"
        },
        Legal = new LegalText
        {
            BackHome = "← Nazad na početnu",
            PrivacyTitle = "Politika privatnosti",
            PrivacyHtml = @"<p>Ova Politika privatnosti objašnjava kako ZS.dev (""ja"") postupa sa vašim podacima kada posetite ovaj sajt.</p>
<h3>Bez kolačića</h3><p>Ovaj sajt ne koristi kolačiće, piksele za praćenje niti analitiku trećih strana.</p>
<h3>Podaci koje prikupljam</h3><p>Jedini lični podaci koji se prikupljaju jesu ime i email adresa koje dobrovoljno unosite putem kontakt forme.</p>
<h3>Svrha</h3><p>Ovi podaci se koriste isključivo za odgovor na vaš upit i slanje kratkog upitnika. Nikada se ne prodaju niti dele sa trećim stranama.</p>
<h3>Čuvanje</h3><p>Vaši podaci se bezbedno čuvaju i samo onoliko dugo koliko je potrebno za obradu vašeg zahteva.</p>
<h3>Vaša prava</h3><p>U svakom trenutku možete zatražiti uvid, ispravku ili brisanje svojih podataka slanjem mejla na <a href=""mailto:zoran.simeunovic@outlook.de"">zoran.simeunovic@outlook.de</a>.</p>
<p class=""legal-updated"">Poslednje ažuriranje: jun 2026.</p>",
            TermsTitle = "Uslovi korišćenja",
            TermsHtml = @"<p>Korišćenjem ovog sajta prihvatate sledeće uslove.</p>
<h3>Svrha</h3><p>Ovaj sajt je lični portfolio koji predstavlja rad i usluge Zorana Simeunovića (ZS.dev).</p>
<h3>Sadržaj</h3><p>Sav sadržaj, tekst i slike vlasništvo su ZS.dev osim ako nije drugačije navedeno i ne smeju se koristiti bez dozvole.</p>
<h3>Bez garancije</h3><p>Sajt se pruža ""takav kakav jeste"" bez bilo kakvih garancija. Informacije se mogu promeniti bez prethodne najave.</p>
<h3>Kontakt forma</h3><p>Slanjem kontakt forme potvrđujete da su navedeni podaci tačni. Slanje ne stvara nikakav obavezujući sporazum.</p>
<h3>Odgovornost</h3><p>ZS.dev ne snosi odgovornost za bilo kakvu štetu nastalu korišćenjem ovog sajta.</p>
<p class=""legal-updated"">Poslednje ažuriranje: jun 2026.</p>"
        }
    };
}
