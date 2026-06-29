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
    public PricingSection Pricing { get; set; } = new();
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
    public string Pricing { get; set; } = "";
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
    public string ShowAll { get; set; } = "";
    public string HideAll { get; set; } = "";
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
    public string CloseChecklist { get; set; } = "";
    public string Analyze { get; set; } = "";
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

public class PricingSection
{
    public string Label { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string GetStarted { get; set; } = "";
    public List<string> Badges { get; set; } = new();
    public string BadgeRecommended { get; set; } = "";
    public string CtaHeading { get; set; } = "";
    public string CtaText { get; set; } = "";
    public string CtaButton { get; set; } = "";
    public string Disclaimer { get; set; } = "";
    public string WarrantyUnit { get; set; } = "";
    public string PrevLabel { get; set; } = "Previous";
    public string NextLabel { get; set; } = "Next";
    public List<PricingCard> Cards { get; set; } = new();
}

public class PricingCard
{
    public string Name { get; set; } = "";
    public string Price { get; set; } = "";
    public List<string> Features { get; set; } = new();
    public int WarrantyDays { get; set; } = 30;
    public bool IsRecommended { get; set; } = false;
}

public class ContactSection
{
    public string TitleHtml { get; set; } = "";
    public string Subtitle1 { get; set; } = "";
    public string Subtitle2 { get; set; } = "";
    public string NamePlaceholder { get; set; } = "";
    public string EmailPlaceholder { get; set; } = "";
    public string GetStarted { get; set; } = "";
    public string PrivacyNoteHtml { get; set; } = "";
    public string SuccessMessage { get; set; } = "";
    public string ErrorMessage { get; set; } = "";
}

public class FooterText
{
    public string Home { get; set; } = "";
    public string Work { get; set; } = "";
    public string Process { get; set; } = "";
    public string Pricing { get; set; } = "";
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
    public QStep4Text S4 { get; set; } = new();
    public QStep5Text S5 { get; set; } = new();
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
    public string BrandDescLabel { get; set; } = "";
    public string[] BrandDescOptions { get; set; } = [];
    public string BrandDescOtherPlaceholder { get; set; } = "";
}

public class QStep2Text
{
    public string Title { get; set; } = "";
    public string NeedWhatLabel { get; set; } = "";
    public string[] WebsiteTypeOptions { get; set; } = [];
    public string WebsiteTypeOtherPlaceholder { get; set; } = "";
    public string ExistingUrlLabel { get; set; } = "";
    public string ExistingUrlPlaceholder { get; set; } = "";
    public string CurrentSiteDescLabel { get; set; } = "";
    public string[] CurrentSiteDescOptions { get; set; } = [];
    public string CurrentSiteDescOtherPlaceholder { get; set; } = "";
    public string WantToChangeLabel { get; set; } = "";
    public string[] WantToChangeOptions { get; set; } = [];
    public string WantToChangeOtherPlaceholder { get; set; } = "";
    public string PagesLabel { get; set; } = "";
    public string[] PagesOptions { get; set; } = [];
    public string[] PagesPortfolioOptions { get; set; } = [];
    public string PagesOtherPlaceholder { get; set; } = "";
    public string ExtrasLabel { get; set; } = "";
    public string CommHeader { get; set; } = "";
    public string[] CommOptions { get; set; } = [];
    public string GrowthHeader { get; set; } = "";
    public string[] GrowthOptions { get; set; } = [];
    public string ExtrasOtherPlaceholder { get; set; } = "";
}

public class QStep3Text
{
    public string Title { get; set; } = "";
    public string SectionHint { get; set; } = "";
    public string AppTypeLabel { get; set; } = "";
    public string[] AppTypeOptions { get; set; } = [];
    public string AppTypeOtherPlaceholder { get; set; } = "";
    public string AppUsersLabel { get; set; } = "";
    public string[] AppUsersOptions { get; set; } = [];
    public string AppUsersOtherPlaceholder { get; set; } = "";
    public string AppDisabledHint { get; set; } = "";
    public string AdminPanelHint { get; set; } = "";
    public string CommLabel { get; set; } = "";
    public string[] CommOptions { get; set; } = [];
    public string SalesLabel { get; set; } = "";
    public string[] SalesOptions { get; set; } = [];
    public string AutoLabel { get; set; } = "";
    public string[] AutoOptions { get; set; } = [];
    public string ContentMgmtLabel { get; set; } = "";
    public string[] ContentMgmtOptions { get; set; } = [];
    public string ContentMgmtOtherPlaceholder { get; set; } = "";
}

public class QStep4Text
{
    public string Title { get; set; } = "";
    public string TextsLabel { get; set; } = "";
    public string TextsHelpCheckbox { get; set; } = "";
    public string TextsDropzoneHint { get; set; } = "";
    public string PhotosLabel { get; set; } = "";
    public string PhotosHelpCheckbox { get; set; } = "";
    public string PhotosDropzoneHint { get; set; } = "";
    public string BrandingLabel { get; set; } = "";
    public string BrandingHelpCheckbox { get; set; } = "";
    public string BrandingDropzoneHint { get; set; } = "";
    public string SocialLinksLabel { get; set; } = "";
    public string SocialLinksHint { get; set; } = "";
    public string StyleLabel { get; set; } = "";
    public string[] StyleOptions { get; set; } = [];
    public string StyleOtherPlaceholder { get; set; } = "";
    public string ColorsLabel { get; set; } = "";
    public string PrimaryColorLabel { get; set; } = "";
    public string SecondaryColorLabel { get; set; } = "";
    public string AccentColorLabel { get; set; } = "";
    public string ColorsHint { get; set; } = "";
    public string ThemeLabel { get; set; } = "";
    public string ThemeDark { get; set; } = "";
    public string ThemeLight { get; set; } = "";
    public string ThemeAny { get; set; } = "";
    public string LikedSitesLabel { get; set; } = "";
    public string LikedSitesHint { get; set; } = "";
    public string LikedSitesWhatLabel { get; set; } = "";
    public string LikedSitesWhatHint { get; set; } = "";
    public string CompetitorSitesLabel { get; set; } = "";
    public string CompetitorSitesHint { get; set; } = "";
    public string CompetitorGoodLabel { get; set; } = "";
    public string CompetitorGoodHint { get; set; } = "";
    public string CompetitorBadLabel { get; set; } = "";
    public string CompetitorBadHint { get; set; } = "";
}

public class QStep5Text
{
    public string Title { get; set; } = "";
    public string GoalsLabel { get; set; } = "";
    public string GoalsWebHeader { get; set; } = "";
    public string[] GoalsWebOptions { get; set; } = [];
    public string GoalsBusinessHeader { get; set; } = "";
    public string[] GoalsBusinessOptions { get; set; } = [];
    public string GoalsOtherPlaceholder { get; set; } = "";
    public string BiggestProblemLabel { get; set; } = "";
    public string BiggestProblemHint { get; set; } = "";
    public string SuccessCriteriaLabel { get; set; } = "";
    public string SuccessCriteriaHint { get; set; } = "";
    public string AdvantagesLabel { get; set; } = "";
    public string AdvantagesHint { get; set; } = "";
    public string CustomersLabel { get; set; } = "";
    public string CustomerTypeLabel { get; set; } = "";
    public string[] CustomerTypeOptions { get; set; } = [];
    public string CustomerTypeOtherPlaceholder { get; set; } = "";
    public string AgeLabel { get; set; } = "";
    public string[] AgeOptions { get; set; } = [];
    public string GenderLabel { get; set; } = "";
    public string[] GenderOptions { get; set; } = [];
    public string LocationLabel { get; set; } = "";
    public string[] LocationOptions { get; set; } = [];
    public string StartWhenLabel { get; set; } = "";
    public string[] StartWhenOptions { get; set; } = [];
    public string DeadlineLabel { get; set; } = "";
    public string DeadlineYes { get; set; } = "";
    public string DeadlineNo { get; set; } = "";
    public string SupportLabel { get; set; } = "";
    public string[] SupportOptions { get; set; } = [];
    public string SupportOtherPlaceholder { get; set; } = "";
    public string HeardFromLabel { get; set; } = "";
    public string[] HeardFromOptions { get; set; } = [];
    public string HeardFromOtherPlaceholder { get; set; } = "";
    public string AdditionalNotesLabel { get; set; } = "";
    public string AdditionalNotesHint { get; set; } = "";
    public string WarrantyHtml { get; set; } = "";
}
