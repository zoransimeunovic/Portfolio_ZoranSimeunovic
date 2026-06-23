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
    public QuestionnaireText Questionnaire { get; set; } = new();
    public string LanguageLabel { get; set; } = "Language";
}

public class NavText
{
    public string Home { get; set; } = "";
    public string Work { get; set; } = "";
    public string Process { get; set; } = "";
    public string ContactMe { get; set; } = "";
    public string MyQuestionnaire { get; set; } = "";
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
    public string? Duration { get; set; }
    public string? DurationSub { get; set; }
    public string? DurationUrl { get; set; }
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
    public string Key { get; set; } = "";
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

public class QuestionnaireText
{
    public string PageTitle { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string ProgressPattern { get; set; } = "";
    public string ProgressDone { get; set; } = "";
    public string BtnNext { get; set; } = "";
    public string BtnBack { get; set; } = "";
    public string BtnSubmit { get; set; } = "";
    public string BtnSending { get; set; } = "";
    public string BtnOptOut { get; set; } = "";
    public string OptionalLabel { get; set; } = "";
    public string SelectDefault { get; set; } = "";
    public string OtherOption { get; set; } = "";
    public string DropzoneClickOrDrag { get; set; } = "";
    public string DropzoneUploading { get; set; } = "";
    public string DropzoneError { get; set; } = "";
    public string[] ContentAvailOptions { get; set; } = [];

    public string DoneTitlePattern { get; set; } = "";
    public string DoneText1 { get; set; } = "";
    public string DoneText2 { get; set; } = "";

    public string ExpiredTitle { get; set; } = "";
    public string ExpiredText { get; set; } = "";
    public string CompletedTitle { get; set; } = "";
    public string CompletedText1 { get; set; } = "";
    public string CompletedText2 { get; set; } = "";

    public string CompanyRequired { get; set; } = "";
    public string OptOutConfirmMsg { get; set; } = "";
    public string OptOutDoneHtml { get; set; } = "";
    public string OptOutError { get; set; } = "";

    public QStep1Text S1 { get; set; } = new();
    public QStep2Text S2 { get; set; } = new();
    public QStep3Text S3 { get; set; } = new();
}

public class QStep1Text
{
    public string Title { get; set; } = "";
    public string CompanyLabel { get; set; } = "";
    public string CompanyHint { get; set; } = "";
    public string CompanyPlaceholder { get; set; } = "";
    public string IndustryLabel { get; set; } = "";
    public string[] IndustryOptions { get; set; } = [];
    public string IndustryOtherPlaceholder { get; set; } = "";
    public string TeamSizeLabel { get; set; } = "";
    public string[] TeamSizeOptions { get; set; } = [];
    public string OnlineLabel { get; set; } = "";
    public string[] OnlineOptions { get; set; } = [];
    public string OnlineOtherPlaceholder { get; set; } = "";
    public string OnlineNowhere { get; set; } = "";
    public string HasWebsiteLabel { get; set; } = "";
    public string HasWebsiteYes { get; set; } = "";
    public string HasWebsiteNo { get; set; } = "";
    public string WebsiteUrlPlaceholder { get; set; } = "";
    public string WebsiteDescLabel { get; set; } = "";
    public string[] WebsiteDescOptions { get; set; } = [];
    public string WebsiteDescOtherPlaceholder { get; set; } = "";
}

public class QStep2Text
{
    public string Title { get; set; } = "";
    public string NeedWhatLabel { get; set; } = "";
    public string HintWebsiteLabel { get; set; } = "";
    public string HintWebsiteEm { get; set; } = "";
    public string[] WebsiteTypeOptions { get; set; } = [];
    public string WebsiteTypeNoNeed { get; set; } = "";
    public string HintExtraLabel { get; set; } = "";
    public string HintExtraEm { get; set; } = "";
    public string ProjectExtraWebApp { get; set; } = "";
    public string[] ProjectExtraOptions { get; set; } = [];
    public string ProjectExtraOtherPlaceholder { get; set; } = "";
    public string PagesLabel { get; set; } = "";
    public string[] PagesOptions { get; set; } = [];
    public string PagesOtherPlaceholder { get; set; } = "";
    public string FeaturesLabel { get; set; } = "";
    public string CommHeader { get; set; } = "";
    public string[] CommOptions { get; set; } = [];
    public string SalesHeader { get; set; } = "";
    public string[] SalesOptions { get; set; } = [];
    public string GrowthHeader { get; set; } = "";
    public string[] GrowthOptions { get; set; } = [];
    public string TechHeader { get; set; } = "";
    public string[] TechOptions { get; set; } = [];
    public string FeaturesNone { get; set; } = "";
    public string FeaturesOtherPlaceholder { get; set; } = "";
    public string ContentLabel { get; set; } = "";
    public string TextsHint { get; set; } = "";
    public string TextsDropzoneHint { get; set; } = "";
    public string PhotosHint { get; set; } = "";
    public string PhotosDropzoneHint { get; set; } = "";
    public string AppUsersLabel { get; set; } = "";
    public string[] AppUsersOptions { get; set; } = [];
    public string AppUsersOtherPlaceholder { get; set; } = "";
    public string AppUserCountLabel { get; set; } = "";
    public string[] AppUserCountOptions { get; set; } = [];
}

public class QStep3Text
{
    public string Title { get; set; } = "";
    public string GoalsLabel { get; set; } = "";
    public string WebHeader { get; set; } = "";
    public string[] WebOptions { get; set; } = [];
    public string BusinessHeader { get; set; } = "";
    public string[] BusinessOptions { get; set; } = [];
    public string AutoHeader { get; set; } = "";
    public string[] AutoOptions { get; set; } = [];
    public string GoalsOtherPlaceholder { get; set; } = "";
    public string BiggestProblemLabel { get; set; } = "";
    public string BiggestProblemHint { get; set; } = "";
    public string SuccessCriteriaLabel { get; set; } = "";
    public string SuccessCriteriaHint { get; set; } = "";
    public string CustomersLabel { get; set; } = "";
    public string CustomerTypeHint { get; set; } = "";
    public string CustomerTypePrivate { get; set; } = "";
    public string CustomerTypeBusiness { get; set; } = "";
    public string CustomerTypeBoth { get; set; } = "";
    public string CustomerTypeOtherPlaceholder { get; set; } = "";
    public string AgeHint { get; set; } = "";
    public string[] AgeOptions { get; set; } = [];
    public string GenderHint { get; set; } = "";
    public string[] GenderOptions { get; set; } = [];
    public string LocationHint { get; set; } = "";
    public string[] LocationOptions { get; set; } = [];
    public string BrandingLabel { get; set; } = "";
    public string BrandingHint { get; set; } = "";
    public string LogoTitle { get; set; } = "";
    public string BrandBookTitle { get; set; } = "";
    public string[] BrandingOptions { get; set; } = [];
    public string BrandingOtherPlaceholder { get; set; } = "";
    public string StyleLabel { get; set; } = "";
    public string[] StyleOptions { get; set; } = [];
    public string StyleOtherPlaceholder { get; set; } = "";
    public string ReferenceLinksLabel { get; set; } = "";
    public string ReferenceLinksHint { get; set; } = "";
    public string ExistingSystemsLabel { get; set; } = "";
    public string ExistingSystemsHint { get; set; } = "";
    public string CompetitorLinksLabel { get; set; } = "";
    public string CompetitorLinksHint { get; set; } = "";
    public string StartWhenLabel { get; set; } = "";
    public string[] StartWhenOptions { get; set; } = [];
    public string StartWhenOtherPlaceholder { get; set; } = "";
    public string DeadlineLabel { get; set; } = "";
    public string DeadlineYes { get; set; } = "";
    public string DeadlineNo { get; set; } = "";
    public string DeadlineFlexible { get; set; } = "";
    public string RankingLabel { get; set; } = "";
    public string RankingHintInline { get; set; } = "";
    public string RankSpeedLabel { get; set; } = "";
    public string RankQualityLabel { get; set; } = "";
    public string RankPriceLabel { get; set; } = "";
    public string RankError { get; set; } = "";
    public string SupportLabel { get; set; } = "";
    public string[] SupportOptions { get; set; } = [];
    public string HeardFromLabel { get; set; } = "";
    public string[] HeardFromOptions { get; set; } = [];
    public string HeardFromOtherPlaceholder { get; set; } = "";
    public string AdditionalNotesLabel { get; set; } = "";
    public string AdditionalNotesHint { get; set; } = "";
}
