namespace Portfolio_ZoranSimeunovic.Content;

public class SiteText
{
    public string Culture { get; set; } = "en";
    public string PageTitle { get; set; } = "";

    public NavText Nav { get; set; } = new();
    public HeroText Hero { get; set; } = new();
    public WorkSection Work { get; set; } = new();
    public AboutText About { get; set; } = new();
    public ProcessSection Process { get; set; } = new();
    public ImproveSection Improve { get; set; } = new();
    public ContactSection Contact { get; set; } = new();
    public FooterText Footer { get; set; } = new();
    public CookieText Cookie { get; set; } = new();
    public LegalText Legal { get; set; } = new();
    public string LanguageLabel { get; set; } = "Language";
}

public class NavText
{
    public string Home { get; set; } = "";
    public string Work { get; set; } = "";
    public string Process { get; set; } = "";
    public string ContactMe { get; set; } = "";
}

public class HeroText
{
    public string Badge { get; set; } = "";
    public string HeadingHtml { get; set; } = "";
    public string Subtitle1 { get; set; } = "";
    public string Subtitle2 { get; set; } = "";
    public string ContactMe { get; set; } = "";
    public string MyWork { get; set; } = "";
    public string ProjectsCount { get; set; } = "";
    public string Name { get; set; } = "";
}

public class WorkSection
{
    public string Label { get; set; } = "";
    public string TitleHtml { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string PrevLabel { get; set; } = "Previous";
    public string NextLabel { get; set; } = "Next";
    public List<WorkCard> Cards { get; set; } = new();
}

public class WorkCard
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Image { get; set; } = "";
    public string? Duration { get; set; }     // npr. "2 years"
    public string? DurationSub { get; set; }   // npr. "at AddWare Solutions"
    public List<string> Techs { get; set; } = new();
}

public class AboutText
{
    public string Label { get; set; } = "";
    public string Name { get; set; } = "";
    public string HeadingHtml { get; set; } = "";
    public string Description { get; set; } = "";
    public string Highlight1Html { get; set; } = "";
    public string Highlight2Html { get; set; } = "";
    public string Highlight3Html { get; set; } = "";
    public string GoPlayer { get; set; } = "";
    public string ContactMe { get; set; } = "";
    public string DownloadCv { get; set; } = "";
}

public class ProcessSection
{
    public string Label { get; set; } = "";
    public string TitleHtml { get; set; } = "";
    public string ContactMe { get; set; } = "";
    public string FindMe { get; set; } = "";
    public List<ProcessStep> Steps { get; set; } = new();
}

public class ProcessStep
{
    public string Tag { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}

public class ImproveSection
{
    public string TitleHtml { get; set; } = "";
    public string Subtitle1 { get; set; } = "";
    public string Subtitle2 { get; set; } = "";
    public string StartAnalysis { get; set; } = "";
    public List<ImproveList> Lists { get; set; } = new();
}

public class ImproveList
{
    public string Key { get; set; } = "";          // redesign | website | automation
    public string HeadingHtml { get; set; } = "";
    public List<ImproveGroup> Groups { get; set; } = new();
    public List<ScoreBand> Scores { get; set; } = new();

    public int TotalItems => Groups.Sum(g => g.Items.Count);
}

public class ImproveGroup
{
    public string Title { get; set; } = "";
    public List<string> Items { get; set; } = new();
}

public class ScoreBand
{
    public int Min { get; set; }
    public int Max { get; set; }
    public string Text { get; set; } = "";
}

public class ContactSection
{
    public string TitleHtml { get; set; } = "";
    public string Subtitle1 { get; set; } = "";
    public string Subtitle2 { get; set; } = "";
    public string NamePlaceholder { get; set; } = "";
    public string EmailPlaceholder { get; set; } = "";
    public string GetStarted { get; set; } = "";
    public string PrivacyNote { get; set; } = "";
    public string SuccessMessage { get; set; } = "";
    public string ErrorMessage { get; set; } = "";
}

public class FooterText
{
    public string Home { get; set; } = "";
    public string Work { get; set; } = "";
    public string Process { get; set; } = "";
    public string Copyright { get; set; } = "";
    public string PrivacyPolicy { get; set; } = "";
    public string TermsOfService { get; set; } = "";
    public string CookieSettings { get; set; } = "";
}

public class CookieText
{
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public string Close { get; set; } = "";
}

public class LegalText
{
    public string PrivacyTitle { get; set; } = "";
    public string PrivacyHtml { get; set; } = "";
    public string TermsTitle { get; set; } = "";
    public string TermsHtml { get; set; } = "";
    public string BackHome { get; set; } = "";
}
