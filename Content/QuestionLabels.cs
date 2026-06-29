namespace Portfolio_ZoranSimeunovic.Content;

public static class QuestionLabels
{
    private static readonly Dictionary<string, string> Labels = new()
    {
        // Korak 1
        ["company"]                  = "Naziv kompanije / brend",
        ["industry"]                 = "Industrija",
        ["industryOther"]            = "Industrija (ostalo)",
        ["teamSize"]                 = "Veličina tima",
        ["brandDesc"]                = "Opis brenda",
        ["brandDescOther"]           = "Opis brenda (ostalo)",

        // Korak 2
        ["projectType"]              = "Tip projekta",
        ["projectTypeOther"]         = "Tip projekta (ostalo)",
        ["existingUrl"]              = "URL postojećeg sajta",
        ["currentSiteDesc"]          = "Opis trenutnog sajta",
        ["currentSiteDescOther"]     = "Opis sajta (ostalo)",
        ["wantToChange"]             = "Šta žele promijeniti",
        ["wantToChangeOther"]        = "Promjene (ostalo)",
        ["pages"]                    = "Stranice",
        ["responsive"]               = "Responzivan dizajn",
        ["pagesOther"]               = "Stranice (ostalo)",
        ["pagesPortfolioOther"]      = "Stranice (ostalo)",
        ["extras"]                   = "Dodatne funkcionalnosti",
        ["extrasOther"]              = "Dodatno (ostalo)",
        ["multilingualDetail"]       = "Višejezičnost - jezici u paketu",
        ["multilingualExtra"]        = "Višejezičnost - dodatni jezici",
        ["onlinePaymentDetail"]      = "Online plaćanje - procesori",
        ["reviewsPlatform"]          = "Platforma za recenzije",

        // Korak 3
        ["appType"]                  = "Tip aplikacije",
        ["appTypeOther"]             = "Tip aplikacije (ostalo)",
        ["appUsers"]                 = "Ko koristi aplikaciju",
        ["appUsersOther"]            = "Korisnici (ostalo)",
        ["commAuto"]                 = "Komunikacija i automatizacija",
        ["salesBooking"]             = "Prodaja i rezervacije",
        ["automation"]               = "Automatizacija i integracije",
        ["ms365"]                    = "Microsoft 365 - odabrane aplikacije",
        ["contentMgmt"]              = "Upravljanje sadržajem",
        ["contentMgmtOther"]         = "Sadržaj (ostalo)",

        // Korak 4
        ["textsHelp"]                = "Pomoć oko tekstova",
        ["photosHelp"]               = "Pomoć oko fotografija",
        ["brandHelp"]                = "Pomoć oko brendiranja",
        ["socialLinks"]              = "Društvene mreže",
        ["style"]                    = "Preferirani stil",
        ["styleOther"]               = "Stil (ostalo)",
        ["primaryColor"]             = "Primarna boja",
        ["secondaryColor"]           = "Sekundarna boja",
        ["accentColor"]              = "Akcent boja",
        ["theme"]                    = "Tema (tamna/svijetla)",
        ["likedSites"]               = "Sajtovi koji se sviđaju",
        ["likedSitesWhat"]           = "Šta im se sviđa",
        ["competitorSites"]          = "Sajtovi konkurencije",
        ["competitorGood"]           = "Šta konkurencija radi dobro",
        ["competitorBad"]            = "Šta konkurencija radi loše",

        // Korak 5
        ["goals"]                    = "Ciljevi projekta",
        ["goalsOther"]               = "Ciljevi (ostalo)",
        ["biggestProblem"]           = "Najveći problem",
        ["successCriteria"]          = "Kriterij uspjeha",
        ["advantages"]               = "Prednosti u odnosu na konkurenciju",
        ["customerType"]             = "Tip kupaca",
        ["customerTypeOther"]        = "Tip kupaca (ostalo)",
        ["ageGroup"]                 = "Starosna grupa",
        ["gender"]                   = "Spol kupaca",
        ["customerLocation"]         = "Lokacija kupaca",
        ["startWhen"]                = "Kada početi",
        ["deadline"]                 = "Rok završetka",
        ["deadlineDate"]             = "Datum roka",
        ["support"]                  = "Podrška i održavanje",
        ["supportOther"]             = "Podrška (ostalo)",
        ["heardFrom"]                = "Kako su čuli za mene",
        ["heardFromOther"]           = "Kako su čuli (ostalo)",
        ["additionalNotes"]          = "Dodatne napomene",
    };

    public static string GetLabel(string key) =>
        Labels.TryGetValue(key, out var label) ? label : key;

    private static readonly Lazy<Dictionary<string, string>> OptionLabels = new(BuildOptionLabels);

    private static Dictionary<string, string> BuildOptionLabels()
    {
        var q = SiteTextProvider.Get("sr-Latn").Questionnaire;
        var d = new Dictionary<string, string>();

        void Add(string id, string[] opts)
        {
            for (var i = 0; i < opts.Length; i++) d[$"{id}_{i}"] = opts[i];
        }

        Add("industry", q.S1.IndustryOptions);
        Add("teamSize", q.S1.TeamSizeOptions);
        Add("brandDesc", q.S1.BrandDescOptions);

        Add("projectType", q.S2.WebsiteTypeOptions);
        Add("currentSiteDesc", q.S2.CurrentSiteDescOptions);
        Add("wantToChange", q.S2.WantToChangeOptions);
        Add("pagesPortfolio", q.S2.PagesPortfolioOptions);
        Add("pagesFull", q.S2.PagesOptions);
        Add("extrasComm", q.S2.CommOptions);
        Add("extrasGrowth", q.S2.GrowthOptions);

        Add("appType", q.S3.AppTypeOptions);
        Add("appUsers", q.S3.AppUsersOptions);
        Add("commAuto", q.S3.CommOptions);
        Add("salesBooking", q.S3.SalesOptions);
        Add("automation", q.S3.AutoOptions);
        Add("contentMgmt", q.S3.ContentMgmtOptions);

        Add("style", q.S4.StyleOptions);

        Add("goalsWeb", q.S5.GoalsWebOptions);
        Add("goalsBiz", q.S5.GoalsBusinessOptions);
        Add("customerType", q.S5.CustomerTypeOptions);
        Add("ageGroup", q.S5.AgeOptions);
        Add("gender", q.S5.GenderOptions);
        Add("customerLocation", q.S5.LocationOptions);
        Add("startWhen", q.S5.StartWhenOptions);
        Add("support", q.S5.SupportOptions);
        Add("heardFrom", q.S5.HeardFromOptions);

        d["theme_dark"] = q.S4.ThemeDark;
        d["theme_light"] = q.S4.ThemeLight;
        d["theme_any"] = q.S4.ThemeAny;
        d["deadline_yes"] = q.S5.DeadlineYes;
        d["deadline_no"] = q.S5.DeadlineNo;
        d["yes"] = "Da";

        return d;
    }

    public static string GetOptionLabel(string value)
    {
        if (string.IsNullOrEmpty(value)) return value;
        if (OptionLabels.Value.TryGetValue(value, out var label)) return label;
        if (value.EndsWith("_other")) return "Ostalo";
        return value;
    }
}
