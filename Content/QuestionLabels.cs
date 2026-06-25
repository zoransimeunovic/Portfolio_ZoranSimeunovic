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
        ["pages"]                    = "Stranice / sekcije",
        ["pagesOther"]               = "Stranice (ostalo)",
        ["pagesPortfolioOther"]      = "Stranice (ostalo)",
        ["extras"]                   = "Dodatne funkcionalnosti",
        ["extrasOther"]              = "Dodatno (ostalo)",

        // Korak 3
        ["appType"]                  = "Tip aplikacije",
        ["appTypeOther"]             = "Tip aplikacije (ostalo)",
        ["appUsers"]                 = "Ko koristi aplikaciju",
        ["appUsersOther"]            = "Korisnici (ostalo)",
        ["commAuto"]                 = "Komunikacija i automatizacija",
        ["salesBooking"]             = "Prodaja i rezervacije",
        ["automation"]               = "Automatizacija i integracije",
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
}
