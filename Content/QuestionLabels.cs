namespace Portfolio_ZoranSimeunovic.Content;

public static class QuestionLabels
{
    private static readonly Dictionary<string, string> Labels = new()
    {
        // Korak 1
        ["company"]              = "Naziv kompanije / brend",
        ["industry"]             = "Industrija",
        ["industryOther"]        = "Industrija (ostalo)",
        ["teamSize"]             = "Veličina tima",
        ["onlinePresence"]       = "Online prisustvo",
        ["onlinePresenceOther"]  = "Online prisustvo (ostalo)",
        ["hasWebsite"]           = "Ima web stranicu",
        ["websiteUrl"]           = "URL web stranice",
        ["websiteDesc"]          = "Opis web stranice",
        ["websiteDescOther"]     = "Opis web stranice (ostalo)",

        // Korak 2
        ["websiteType"]          = "Tip web stranice",
        ["projectExtra"]         = "Dodatno (web app / desktop...)",
        ["projectExtraOther"]    = "Dodatno (ostalo)",
        ["pages"]                = "Stranice / sekcije",
        ["pagesOther"]           = "Stranice (ostalo)",
        ["features"]             = "Funkcionalne potrebe",
        ["featuresOther"]        = "Funkcionalne potrebe (ostalo)",
        ["contentTexts"]         = "Sadržaj — tekstovi",
        ["contentPhotos"]        = "Sadržaj — fotografije",
        ["appUsers"]             = "Ko koristi aplikaciju",
        ["appUsersOther"]        = "Korisnici aplikacije (ostalo)",
        ["appUserCount"]         = "Broj korisnika",

        // Korak 3
        ["goals"]                = "Ciljevi projekta",
        ["goalsOther"]           = "Ciljevi (ostalo)",
        ["biggestProblem"]       = "Najveći problem",
        ["successCriteria"]      = "Kriterij uspjeha",
        ["customerType"]         = "Tip kupaca",
        ["customerTypeOther"]    = "Tip kupaca (ostalo)",
        ["ageGroup"]             = "Starosna grupa",
        ["gender"]               = "Spol kupaca",
        ["customerLocation"]     = "Lokacija kupaca",
        ["branding"]             = "Brendirani materijali",
        ["brandingOther"]        = "Brendiranje (ostalo)",
        ["style"]                = "Preferirani stil",
        ["styleOther"]           = "Stil (ostalo)",
        ["referenceLinks"]       = "Reference linkovi",
        ["existingSystems"]      = "Postojeći sistemi",
        ["competitorLinks"]      = "Linkovi konkurencije",
        ["startWhen"]            = "Kada početi",
        ["startWhenOther"]       = "Kada početi (ostalo)",
        ["deadline"]             = "Rok završetka",
        ["deadlineDate"]         = "Datum roka",
        ["rankSpeed"]            = "Rang — Brzina isporuke",
        ["rankQuality"]          = "Rang — Kvalitet dizajna",
        ["rankPrice"]            = "Rang — Cijena",
        ["support"]              = "Podrška nakon završetka",
        ["heardFrom"]            = "Kako su čuli za mene",
        ["heardFromOther"]       = "Kako su čuli (ostalo)",
        ["additionalNotes"]      = "Dodatne napomene",
    };

    public static string GetLabel(string key) =>
        Labels.TryGetValue(key, out var label) ? label : key;
}
