# Portfolio ZoranSimeunovic — Projektna Dokumentacija

## Pravila — obavezno poštovati

- **Clean code:** Bez komentara koji opisuju ŠTA. Samo kratki inline ZAŠTO. Bez XML doc blokova. File-scoped namespace u C#.
- **Bez prepisa fajlova:** Uvijek pročitaj fajl, pa uradi ciljani Edit. Nikad ne prepiši bez eksplicitnog odobrenja.
- **Bez nepotrebnih apstrakcija:** Ne dodavati feature-e ni refaktoringe koje zadatak ne zahtijeva.
- **Slike:** Nikad ne brisati bez Zoranove saglasnosti.
- **Cookies:** Zabranjen svaki ne-esencijalni cookie bez Zoranove saglasnosti.
- **Tekstovi:** Nikad em dash "-" — uvijek obična crtica "-".
- **Komunikacija:** Odgovaraj na srpskom. Tehnički termini mogu ostati na engleskom.

---

## PROJEKAT

**ASP.NET Core 8.0 MVC** — lični portfolio sajt. Single-project solution, bez API sloja ni Repository pattern-a.  
Stack: C# 12, EF Core 8.0.10, MySQL Pomelo 8.0.2, Razor Views, Bootstrap 5 (grid only), jQuery, Vanilla JS, CSS3.  
Figma (1440px desktop base) → CSS export + manual refinement. Dark theme.

---

## KLJUČNI FAJLOVI

| Fajl | Namena |
|------|--------|
| `Controllers/HomeController.cs` | Index, Contact, UpdateChecklist, SetLanguage, Privacy, Terms, Error |
| `Controllers/AdminController.cs` | Login, lista upitnika, detalji, fajlovi, dokumenti |
| `Controllers/QuestionnaireController.cs` | Wizard — Start, Step1/2/3, Done, OptOut, upload |
| `Data/AppDbContext.cs` | EF DbContext — 5 tabela |
| `Models/ContactLead.cs` | Kontakt lead, SyncWorker timestamp flagovi |
| `Models/Questionnaire.cs` | Token (30 dana), Stage, Step1/2/3Answers (JSON) |
| `Models/Document.cs` | Admin upload dokumenti |
| `Models/QuestionnaireFile.cs` | Fajlovi koje klijent šalje uz upitnik |
| `Models/ChecklistAnswer.cs` | FK → ContactLead, ListKey, ItemText |
| `Content/SiteTextProvider.cs` | BuildEn(), BuildDe(), BuildSr() — tekstovi za 3 jezika |
| `Content/QuestionLabels.cs` | Mapa ključ→labela za admin prikaz upitnika |
| `Localization/BrowserCultureProvider.cs` | Accept-Language → kultura; exYu jezici → sr-Latn |
| `wwwroot/css/site.css` | Dark theme (1821 linija) |
| `wwwroot/js/site.js` | Carousel, checklist scoring, AJAX forma, Figma scale |
| `Program.cs` | Startup: MVC, MySQL, cookie auth, localization, migrations |
| `appsettings.Development.json` | ⚠️ Dev konekcija — u .gitignore |

---

## BAZA PODATAKA

Šema: `DATABASE_RECONSTRUCTION.md` — jedini pouzdani izvor. Ažurirati pri svakoj migraciji.

- Engine: MySQL, baza `portfolio_zs`
- Migracije: `db.Database.Migrate()` automatski pri startu
- Posljednja migracija: `20260623205314_UseTimestampsForNotificationFlags`
- Tabele: `contact_leads`, `checklist_answers`, `questionnaire`, `questionnaire_files`, `documents`

```bash
dotnet ef migrations add NazivMigracije
```

---

## LOKALIZACIJA

3 jezika: `en` (fallback), `de`, `sr-Latn`. Prioritet: Cookie → Accept-Language → fallback.  
Mapiranje: `de*`→`de` | `sr/hr/bs/cnr/me/mk/sh`→`sr-Latn` | ostalo→`en`

---

## FRONTEND

- `site.css`: boje `--primary #030E1F` / `--secondary #DDE6F5` / `--accent #156EF6` / `--surface #132949` / `--surface-2 #1C2635` / `--muted #8A9BB8`
- `site.js`: `--hero-scale` i `--fig-scale` CSS varijable za Figma stage skaliranje (postavljene u JS)
- Bootstrap 5 grid only (12 kol, margin 72, gutter 24), Font: Outfit 400/600/700/800
- Figma CSS referenca: `Design/figma/` (8 fajlova, `08-...css` ne implementirati — već u JS)
- Nekorišćene slike: `language.png`, `vector-linkedin.png`, `linkedin-about.png` — čuvaju se

### Sekcije Index stranice
Hero → Work (carousel 3/2/1) → About (Figma stage) → Process (11 koraka) → Improve (3 checkliste + scoring) → Contact (AJAX) → Footer → Cookie Modal

---

## KONTROLERI

- **HomeController**: Index, Contact (POST→JSON), UpdateChecklist, SetLanguage, Privacy, Terms, Error
- **AdminController** (`/admin`, cookie auth): Login/Logout, Index, Detail, Delete, OfferSent, Documents CRUD, QFileDownload
- **QuestionnaireController** (`/q`): Start, Step1/2/3 (GET+POST), Done, OptOut, file upload (`PrivateUploads/`)

---

## OTVORENI PROBLEMI

| Problem | Akcija |
|---------|--------|
| DB lozinka hardcoded | Koristiti user secrets |
