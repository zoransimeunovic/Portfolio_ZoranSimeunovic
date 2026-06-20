# Portfolio ZoranSimeunovic — Projektna Dokumentacija

## Pravila — obavezno poštovati
- **Cookies:** Zabranjena je upotreba bilo kojeg cookie-a koji nije esencijalan bez izričite saglasnosti vlasnika projekta. Svaki novi cookie koji nije esencijalan mora biti odobren od strane Zorana prije implementacije.

---

## TIP PROJEKTA I TEHNOLOGIJE

**Tip:** ASP.NET Core 8.0 MVC Web Application — Lični portfolio sajt  
**Autor:** Zoran Simeunović (Full Stack Web Developer, 6 godina iskustva)  
**Email:** zoransimeunovic@outlook.de  
**Copyright:** © 2026 ZS.dev

### Tehnološki Stack
| Layer | Tehnologija |
|-------|------------|
| Backend | ASP.NET Core MVC 8.0, C# 12, Entity Framework Core 8.0.10 |
| Baza | MySQL (Pomelo.EntityFrameworkCore.MySql 8.0.2) |
| Frontend | Razor Views, Bootstrap 5 (grid only), jQuery, Vanilla JS, CSS3 (dark theme) |
| Design Tool | Figma → CSS export + manual refinement |
| IDE | Visual Studio 2022 |
| VCS | Git (GitHub: zoransimeunovic/Portfolio_ZoranSimeunovic) |

---

## ARHITEKTURA

**Single-project solution** — jednostavna struktura bez API sloja ili Repository pattern-a.

### Ključni Fajlovi
| Fajl | Redova | Namena |
|------|--------|--------|
| `Controllers/HomeController.cs` | 144 | 6 akcija: Index, Contact (POST), UpdateChecklist (POST), SetLanguage, Privacy, Terms |
| `Data/AppDbContext.cs` | — | EF DbContext za MySQL (2 tabele: contact_leads, checklist_answers) |
| `Models/ContactLead.cs` | — | Name, Email, Language, CreatedAt |
| `Models/ChecklistAnswer.cs` | — | ContactLeadId, ListKey, ItemText, CreatedAt |
| `Models/ErrorViewModel.cs` | — | RequestId, ShowRequestId |
| `Content/SiteTextProvider.cs` | 571 | Sav tekstualni sadržaj za 3 jezika (en, de, sr-Latn); metode BuildEn(), BuildDe(), BuildSr() |
| `Content/SiteText.cs` | — | Model klase za sve sekcije (Nav, Hero, Work, About, Process, Improve, Contact, Footer, Cookie, Legal) |
| `Localization/BrowserCultureProvider.cs` | — | Accept-Language → kultura; mapira exYu jezike na sr-Latn |
| `Views/Home/Index.cshtml` | 280+ | Glavna stranica: 8 sekcija (hero, work, about, process, improve, contact, footer, modal) |
| `Views/Home/Privacy.cshtml` | 379 | Politika privatnosti |
| `Views/Home/Terms.cshtml` | 373 | Uslovi korišćenja |
| `Views/Shared/_Layout.cshtml` | 147 | Layout: navbar, footer, Bootstrap + custom CSS |
| `wwwroot/css/site.css` | 860 | Dark theme (boje, tipografija, layout, responsive) |
| `wwwroot/js/site.js` | 197 | Carousel, checklists scoring, AJAX kontakt forma, responsive scaling |
| `Program.cs` | — | Startup: MVC, MySQL, localization (en/de/sr-Latn), HTTPS, DB.EnsureCreated() |
| `appsettings.json` | — | Production config (connection string placeholder) |
| `appsettings.Development.json` | — | ⚠️ HARDCODED LOZINKA (u .gitignore) — vidi "Problemi" ispod |

## BAZA PODATAKA

**⚠️ Detaljnu SQL dokumentaciju vidi: `DATABASE_RECONSTRUCTION.md`**

### Konekcija

**Production (appsettings.json):**
```json
"ConnectionStrings": { "DefaultConnection": "" }
```

**Development (appsettings.Development.json):**
```
Server=173.249.49.4
Port=3306
Database=portfolio_zs
User=RemoteAppUser
Password=SS@vo01SofFija79!Jos_66;
```

### Tabele (2)

- **contact_leads** — Kontakt podaci iz "Take the first step" forme
- **checklist_answers** — Odgovore iz "How to improve" sekcije (FK na contact_leads, CASCADE delete)

### Inicijalizacija — EF Core Migrations

**Sistem:** `dotnet ef migrations` (od 20.06.2026)

Baza se kreira i ažurira automatski pri startu:
- `Program.cs` (redovi 70-81) pokreće `db.Database.Migrate()`
- Migracije se čuvaju u `/Migrations/` folderu
- Za novu sesiju: samo pokreni `dotnet run` — migracije će se primeniti

**Trenutna Migracija:**
- `20260620213237_InitialCreate` — Kreira contact_leads i checklist_answers tabele

**Za dodavanje novih migracija:**
```bash
dotnet ef migrations add NazivMigracije
dotnet ef migrations remove  # ako trebaš da obrišeš
```

### Trenutni Status

❌ **DATABASE NIJE FUNKCIONALNA** — IP firewall na 173.249.49.4

**Greška:**
```
Access denied for user 'RemoteAppUser'@'217.232.240.42'
```

**Rezultat:**
- Website radi, kontakt forma prima podatke
- Podaci se ne čuvaju u bazu (try/catch glasa grešku)
- Korisnik vidi "success" ali nema persistencije

### 3 Rešenja

1. **Lokalna MySQL** — `DATABASE_RECONSTRUCTION.md` → "Setup — Lokalna MySQL"
2. **Cloud MySQL** — `DATABASE_RECONSTRUCTION.md` → "Setup — Cloud MySQL"
3. **SQLite Development** — `DATABASE_RECONSTRUCTION.md` → "Setup — SQLite"

---

## LOKALIZACIJA

### Podržani Jezici
- **en** (English) — fallback default
- **de** (German)
- **sr-Latn** (Serbian Latin, Cyrillic variant not supported)

### Resurs za Sadržaj
Sav tekstualni sadržaj je **hardcoded u C# klase**, ne u .resx fajlovi:
- `/Content/SiteText.cs` — Modeli za sve sekcije
- `/Content/SiteTextProvider.cs` — BuildEn(), BuildDe(), BuildSr() metode
- **571 linija** — Kompletna trilingvalna podrška

### Rezolucija Kulture
**Redosled prioriteta:**
1. **Cookie** (`lang` cookie) — najviši prioritet
2. **Browser** Accept-Language header (BrowserCultureProvider) — drugi prioritet
3. **Fallback:** en (English)

**Mapiranje** (BrowserCultureProvider.cs):
- `de*` → `de`
- `sr`, `hr`, `bs`, `mk`, `sl` → `sr-Latn` (svi exYu jezici → Serbian Latin)
- `en*` → `en`
- Ostalo → `en`

### Akcije za Lokalizaciju
| Akcija | Ulaz | Efekat |
|--------|------|--------|
| SetLanguage | `culture` + `returnUrl?` | Setuje `lang` cookie; validira kulturu; redirect |
| All Views | — | Pozivaju `CurrentText()` koja učitava SiteTextProvider.Get(CultureInfo.CurrentCulture) |

---

## FRONTEND

### CSS & JavaScript
| Fajl | Veličina | Opis |
|------|----------|------|
| `wwwroot/css/site.css` | 41 KB | 860 linija — dark theme (boje #030E1F/#DDE6F5/#156EF6; tipografija; layout; responsive) |
| `wwwroot/js/site.js` | 11 KB | 197 linija — Carousel (slide/dot navigation), checklists scoring, AJAX forma, CSS var scaling |

### CSS Varijable (Responsive Scaling)
```css
--hero-scale: 1.0;    /* Adjusts at breakpoints */
--fig-stage: 1.0;     /* Figma design stage scaling */
```
Setovane u site.js na resize za matching Figma-a na svim rezolucijama.

### Slike (5.9 MB ukupno)
| Kategorija | Fajlovi | Veličina | Napomena |
|-----------|---------|----------|---------|
| Profile | profile.png | **4.7 MB** | ⚠️ TREBALO OPTIMIZOVATI |
| Work Projects | 4 slike | 306 KB | hris.jpg (55K), desktop-gui.png (138K), zeiterfassung.png (20K), portfolio.png (93K) |
| Avatars | 5 × .png | 876 KB | Proof/social sekcija |
| Icons | 12 × .png | 56 KB | LinkedIn (3 varijante), Go, GitHub, Xing, email, language toggle, open/close |
| Ostalo | favicon, CV | 5.4 KB | wwwroot/files/Lebenslauf_ZoranSimeunovic.pdf |

### Bootstrap 5
- **Korišćenje:** Grid only (12 kolona, margin 72, gutter 24)
- **CDN:** jquery.min.js, bootstrap.bundle.min.js
- **Fonts:** Google Fonts (Outfit 400/600/700)

### Sekcije na Index Stranici
1. **Hero** — Badge, heading, CTA buttons, avatar proof, tech tags
2. **Work** — Carousel (3/2/1 cards responsive); project details; tech skills
3. **About** — Profile image, 3 highlights, CV button, social icons
4. **Process** — 11-step grid (3-column, column-major); step cards with numbers
5. **Improve** — 3 accordion checklists (Design, Tech, Marketing) sa dinamičkim scoringom
6. **Contact** — AJAX form (name + email) + privacy notice
7. **Footer** — Nav links, social icons, copyright, cookie modal (Bootstrap)
8. **Cookie Modal** — "Ne koristimo cookies" obaveštenje

---

## DIZAJN (Figma Export Status)

### Figma Frame
- **Veličina:** 1440×5992 px (desktop base)
- **Alat:** Figma → CSS export + ručne izmene

### Boje
| Naziv | Hex | Korišćenje |
|-------|-----|-----------|
| Primarna | #030E1F | Background |
| Sekundarna | #DDE6F5 | Text (light) |
| Tercijarna | #156EF6 | Accent (blue) |
| Tercijarna 2 | #1C2635 | Chips, card headers |
| Sekundarna 2 | #132949 | Card backgrounds |

### Responsive Design
- **Desktop:** 1440px max-width
- **Tablet:** Bootstrap breakpoints (≤991px)
- **Mobile:** 1-column layout (≤640px)
- **Scaling:** site.js CSS varijable za matching Figma-a

### Exported CSS Fajlovi
```
/Design/figma/
├── 01-navbar-hero.css        (0–923px)
├── 02-work.css               (~1003px)
├── 03-about.css              (1931px)
├── 04-process.css            (2775px)
├── 05-improve.css            (3837px)
├── 06-last-cta.css           (4710px)
├── 07-footer.css             (5247px)
└── 08-checklist-evaluation-DO-NOT-IMPLEMENT.css  (reference; already implemented in JS)
```

---

## KONTROLER AKCIJE

### HomeController.cs (6 akcija)

#### 1. GET Index
```csharp
public IActionResult Index()
```
- Vraća `View(CurrentText())` — glavna stranica sa svim sekcijama
- Nema parametara

#### 2. POST Contact
```csharp
public async Task<IActionResult> Contact(string name, string email)
```
- AJAX endpoint za kontakt formu
- **Validacija:** name i email obavezni
- **Akcije:**
  - Kreira ContactLead (name, email, language, created_at)
  - Saveuje u DB (try/catch — greške se glase)
  - Vraća JSON: `{ success: bool, message: string, leadId: int? }`
- **Povratni kod:**
  - 200 (čak i ako DB save fails — see greška handling)
  - 400 Bad Request ako su parametri nedostaju

#### 3. POST UpdateChecklist
```csharp
public async Task<IActionResult> UpdateChecklist(int leadId, string checklistJson)
```
- Sprema odgovore iz ček listi (sekcija "How to improve")
- **Parametri:** 
  - `leadId` — referenca na ContactLead
  - `checklistJson` — JSON array sa item_text vrednostima
- **Akcije:**
  - Kreira ChecklistAnswer redove (contact_lead_id, list_key, item_text)
  - Truncates strings na 500 karaktera
  - Saveuje u DB
- **Povratni kod:** 200 JSON ili 400 ako leadId nevaljani

#### 4. GET SetLanguage
```csharp
public IActionResult SetLanguage(string culture, string returnUrl = null)
```
- Setuje `lang` cookie sa odabranom kulturom
- **Validacija:** culture mora biti u listi podržanih (en, de, sr-Latn)
- **Redirect:** Vraća na returnUrl ili home

#### 5. GET Privacy
```csharp
public IActionResult Privacy()
```
- Vraća `/Views/Home/Privacy.cshtml`
- Pokazuje Legal.PrivacyHtml (trilingvalno iz SiteTextProvider)

#### 6. GET Terms
```csharp
public IActionResult Terms()
```
- Vraća `/Views/Home/Terms.cshtml`
- Pokazuje Legal.TermsHtml (trilingvalno iz SiteTextProvider)

---

## PROBLEMI I STANJE

### KRITIČNI
| Problem | Status | Korijen | Akcija |
|---------|--------|--------|--------|
| ❌ DB konekcija ne radi | Potvrđeno | IP firewall na 173.249.49.4 | Prebaci na lokalnu MySQL ili cloud sa whitelisted IP |
| ⚠️ Lozinka hardcoded | Potvrđeno | appsettings.Development.json | Zameni sa env vars / user secrets |

### VAŽNO
| Problem | Status | Korijen | Akcija |
|---------|--------|--------|--------|
| 📷 profile.png je 4.7 MB | Potvrđeno | Slike nisu optimizovane | Kompajliraj na <1 MB |

### SREDNJE
| Problem | Status | Korijen | Akcija |
|---------|--------|--------|--------|
| 📱 Nema touch/swipe | Potvrđeno | site.js carousel bez touch events | Dodaj touchstart/touchend listeners |

### MANJE
| Problem | Status | Korijen | Akcija |
|---------|--------|--------|--------|
| 📝 Nema README.md | Potvrđeno | Nedostaje root dokumentacija | Kreiraj README sa setup instrukcijama |

### ✓ REŠENO
- ✓ .gitignore — OK (appsettings.Development.json je isključen)
- ✓ Git repozitorijum — postoji i pushovan na GitHub
- ✓ Localization — sva 3 jezika implementirana
- ✓ Kontakt forma — AJAX funkcionira (bez DB persistencije)
- ✓ Checklists scoring — dinamički rezultati

---

## GIT STANJE

### Remote
- **URL:** https://github.com/zoransimeunovic/Portfolio_ZoranSimeunovic.git
- **Branch:** main
- **Status:** Sinhronizovana

### Skoro Commits (4 ukupno)
| Hash | Datum | Poruka |
|------|-------|--------|
| 9edd672 | 18. jun 2026 | Add missing Figma decorative objects and align details |
| 9d40dfa | 18. jun 2026 | Redesign hero, about, CTA and footer to match Figma export |
| 27e649e | 17. jun 2026 | Add checklist answers feature |
| 8a325c9 | 17. jun 2026 | Initial commit |

### Necomitted Izmene (2 fajla)
1. **Views/Home/Index.cshtml** — HTML izmene (ikone, dekoracije, linkovi)
2. **wwwroot/css/site.css** — CSS izmene (navbar opacity, hero positioning, aspect-ratio)

**Preporuka:** Commituj ove izmene sa porukom "Update hero styling and LinkedIn icons"

---

## POKRETANJE LOKALNO

### Portovi
| Profil | HTTP | HTTPS | Napomena |
|--------|------|-------|---------|
| http | 5157 | — | Kestrel HTTP |
| https | 5157 | 7270 | Kestrel HTTPS |
| IIS Express | 22113 | 44344 | Alternativa |

### Setup
1. Postavi `ASPNETCORE_ENVIRONMENT=Development`
2. Ažuriraj `appsettings.Development.json` — MySQL kredencijali
3. `dotnet restore`
4. `dotnet build`
5. `dotnet run` ili F5 u Visual Studio 2022

### Provera
- Otidi na `http://localhost:5157`
- Testiraj sve 3 jezika (en, de, sr-Latn)
- Testiraj kontakt forma
- Otvori DevTools (F12) za console greške

---

## DEPENDENCIES

### NuGet Packages
```xml
<TargetFramework>net8.0</TargetFramework>
<Nullable>enable</Nullable>
<ImplicitUsings>enable</ImplicitUsings>

<!-- Packages -->
Microsoft.EntityFrameworkCore                 8.0.10
Microsoft.EntityFrameworkCore.Design          8.0.10
Pomelo.EntityFrameworkCore.MySql              8.0.2
```

### Klijentske Biblioteke (wwwroot/lib)
- Bootstrap 5 CSS (grid only)
- jQuery (minified)
- Fonts: Google Fonts (Outfit 400/600/700)

---

## AUTORA I KONTAKTI

**Zoran Simeunović**
- Email: zoransimeunovic@outlook.de
- GitHub: zoransimeunovic
- Portfolio: https://zs.dev

**Iskustvo:** HRIS sistemи, Desktop GUI (WPF), Zeiterfassung, Webflow, Figma dizajn, MySQL

---

## SLEDEĆI KORACI ZA NOVU SESIJU

Ova dokumentacija je dizajnirana da novu AI sesiju omogući da počne **bez re-skeniranja projekta**. Ako trebaš:

1. **Raditi na DB problemima** → Vidi "BAZA PODATAKA" sekciju
2. **Dodati novu funkcionalnost** → Vidi "KONTROLER AKCIJE" i "FRONTEND" sekcije
3. **Ažurirati dizajn** → Vidi "DIZAJN" i "FRONTEND" sekcije
4. **Debagovanje** → Vidi "PROBLEMI" i "GIT STANJE"
5. **Lokalizovati nešto novo** → Vidi "LOKALIZACIJA" sekciju

**Napomena za AI:** Sve informacije u ovoj dokumentaciji su verificirane i dobijene 20.06.2026 detaljnom analizom projekta. Ako se suočiš sa drugačitim stanjem, primeni novu analizu i ažuriraj ovu dokumentaciju.
