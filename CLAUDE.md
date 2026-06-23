# Portfolio ZoranSimeunovic — Projektna Dokumentacija

## Pravila — obavezno poštovati

> **Projekat mora biti: Clean code**

### Kod
- **Clean code:** Bez komentara koji opisuju ŠTA kod radi. Samo kratki inline komentari za neočigledno ZAŠTO. Bez XML doc blokova. File-scoped namespace u C#.
- **Bez prepisa fajlova:** Uvijek pročitaj fajl, pa uradi ciljani Edit. Nikad ne prepiši cijeli fajl bez eksplicitnog odobrenja.
- **Bez nepotrebnih apstrakcija:** Ne dodavati feature-e, refaktoringe ni apstrakcije koje zadatak ne zahtijeva.

### Brisanje
- **Slike:** Nikad ne brisati slike bez Zoranove eksplicitne saglasnosti — predložiti, ne brisati.
- **Fajlovi/kod:** Provjeri da li je nešto zaista mrtvi kod (grep, čitanje) prije brisanja.

### Cookies
- Zabranjena je upotreba bilo kojeg cookie-a koji nije esencijalan bez izričite saglasnosti vlasnika projekta. Svaki novi cookie mora biti odobren od strane Zorana.

### Komunikacija
- Odgovaraj na srpskom jeziku. Tehnički termini mogu ostati na engleskom.

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
| Baza | MySQL Pomelo 8.0.2 |
| Frontend | Razor Views, Bootstrap 5 (grid only), jQuery, Vanilla JS, CSS3 (dark theme) |
| Design Tool | Figma → CSS export + manual refinement |
| IDE | Visual Studio 2022 |
| VCS | Git (GitHub: zoransimeunovic/Portfolio_ZoranSimeunovic) |

---

## ARHITEKTURA

**Single-project solution** — jednostavna struktura bez API sloja ili Repository pattern-a.

### Ključni Fajlovi
| Fajl | Namena |
|------|--------|
| `Controllers/HomeController.cs` | Index, Contact (POST), UpdateChecklist (POST), SetLanguage, Privacy, Terms, Error |
| `Controllers/AdminController.cs` | Login, admin panel — lista upitnika, detalji, fajlovi, dokumenti |
| `Controllers/QuestionnaireController.cs` | Wizard upitnik — Start, Step1/2/3 (POST), Done, OptOut, upload fajlova |
| `Data/AppDbContext.cs` | EF DbContext — 6 tabela |
| `Models/ContactLead.cs` | Name, Email, Language, OptedOut, OfferSentAt, CreatedAt |
| `Models/Questionnaire.cs` | Token (30 dana), Stage, Step1/2/3Answers (JSON), CompletedAt |
| `Models/Document.cs` | Admin upload — FullName, StoredFileName, ContentType, SizeBytes |
| `Models/QuestionnaireFile.cs` | Fajlovi koje klijent šalje uz upitnik |
| `Models/ChecklistAnswer.cs` | FK → ContactLead, ListKey, ItemText |
| `Models/ClientAction.cs` | Akcija klijenta (token linkovi) |
| `Content/SiteText.cs` | Model klase za sve sekcije portfloija |
| `Content/SiteTextProvider.cs` | BuildEn(), BuildDe(), BuildSr() — tekstovi za 3 jezika |
| `Content/QuestionLabels.cs` | Mapa ključ→labela za prikaz odgovora iz upitnika u admin panelu |
| `MsGraphClient/MsGraphClient.cs` | SendEmailAsync() via Microsoft Graph API |
| `Localization/BrowserCultureProvider.cs` | Accept-Language → kultura; exYu jezici → sr-Latn |
| `Views/Home/Index.cshtml` | Glavna stranica: hero, work, about, process, improve, contact, footer, cookie |
| `Views/Admin/` | Login, Index (lista), Detail (odgovori + fajlovi), Documents |
| `Views/Questionnaire/` | Step1, Step2, Step3, Done, OptOut, Expired |
| `Views/Shared/_Layout.cshtml` | Navbar, footer, Bootstrap + custom CSS |
| `wwwroot/css/site.css` | Dark theme portfolio |
| `wwwroot/css/admin.css` | Admin panel stilovi |
| `wwwroot/js/site.js` | Carousel, checklist scoring, AJAX forma, Figma scale |
| `Program.cs` | Startup: MVC, MySQL, cookie auth, localization, migrations |
| `appsettings.Development.json` | ⚠️ Dev konekcija — u .gitignore |

---

## BAZA PODATAKA

### Konekcija

**MySQL (development i production):**
```json
{ "ConnectionStrings": { "DefaultConnection": "Server=...;Database=portfolio_zs;User=...;Password=...;" } }
```

### Tabele (6)
- **contact_leads** — Kontakt podaci + OptedOut + OfferSentAt
- **checklist_answers** — Odgovori checkliste (FK → contact_leads, CASCADE)
- **questionnaires** — Token, Stage, Step1/2/3Answers JSON, CompletedAt
- **questionnaire_files** — Fajlovi uz upitnik (FK → questionnaires)
- **documents** — Admin upload dokumenti
- **client_actions** — Token akcije klijenta

### EF Core Migrations
- `Program.cs` pokreće `db.Database.Migrate()` pri startu
- Migracije u `/Migrations/` folderu
- Posljednja: `20260622202525_AddQuestionnaireFiles`

```bash
dotnet ef migrations add NazivMigracije
dotnet ef migrations remove
```

### Status
✅ **FUNKCIONALNA** — MySQL sa EF Core migracijama, `db.Database.Migrate()` pri startu

---

## LOKALIZACIJA

### Podržani Jezici
- **en** (English) — fallback default
- **de** (German)
- **sr-Latn** (Serbian Latin)

### Prioritet rezolucije kulture
1. Cookie (ručni izbor korisnika)
2. Browser Accept-Language header (`BrowserCultureProvider`)
3. Fallback: `en`

### Mapiranje (BrowserCultureProvider.cs)
- `de*` → `de`
- `sr`, `hr`, `bs`, `cnr`, `me`, `mk`, `sh` → `sr-Latn`
- `en*` → `en` | Ostalo → `en`

---

## FRONTEND

### CSS & JavaScript
| Fajl | Opis |
|------|------|
| `wwwroot/css/site.css` | Dark theme — boje: `#030E1F` / `#DDE6F5` / `#156EF6` |
| `wwwroot/js/site.js` | Carousel, checklist scoring, AJAX forma, CSS scale varijable |

### CSS Varijable (Figma scaling)
- `--hero-scale` — skaliranje hero stage (postavljeno u site.js)
- `--fig-scale` — skaliranje about stage (postavljeno u site.js)

### Slike
| Kategorija | Fajlovi | Napomena |
|-----------|---------|---------|
| Profile | `profile.png` | ⚠️ 4.7 MB — trebalo optimizovati |
| Work | `hris.jpg`, `desktop-gui.png`, `zeiterfassung.png`, `personal-portfolio.png` | |
| Avatars | `avatar1–5.png` | Hero proof sekcija |
| Icons (aktivni) | `linkedin.png`, `linkedin_in_about.png`, `Vector_LinekdIn.png`, `go-game.png`, `github.png`, `xing.png`, `email.png`, `my-work.png`, `download-cv.png`, `open.png`, `close.png` | |
| Icons (nekorišćeni) | `language.png`, `vector-linkedin.png`, `linkedin-about.png` | Čuvaju se za eventualnu upotrebu |
| CV | `wwwroot/files/Lebenslauf_ZoranSimeunovic.pdf` | |

### Bootstrap 5
- Grid only (12 kolona, margin 72, gutter 24)
- CDN: jquery.min.js, bootstrap.bundle.min.js
- Font: Google Fonts — Outfit 400/600/700/800

### Sekcije na Index Stranici
1. **Hero** — Badge, heading, CTA, avatar proof, tech floating tags
2. **Work** — Carousel (3/2/1 cards responsive), project details, tech tags
3. **About** — Figma stage: foto, highlights, CV, LinkedIn/Go ikone
4. **Process** — 11-step grid (3 kolone, column-major order)
5. **Improve** — 3 accordion checklists (Design, Website, Automation) + dynamic scoring
6. **Contact** — AJAX form (name + email)
7. **Footer** — Nav, social icons, copyright, cookie modal link
8. **Cookie Modal** — "Ne koristimo cookies" poruka (Bootstrap)

---

## DIZAJN (Figma)

- **Veličina:** 1440×5992 px (desktop base)
- **About i Hero sekcija:** Figma stage layout (apsolutne koordinate, CSS scale)

### Boje (CSS varijable)
| Varijabla | Hex | Primjena |
|-----------|-----|---------|
| `--primary` | #030E1F | Background |
| `--secondary` | #DDE6F5 | Tekst (light) |
| `--accent` | #156EF6 | Plava akcent |
| `--surface` | #132949 | Kartice |
| `--surface-2` | #1C2635 | Chip headers |
| `--muted` | #8A9BB8 | Prigušeni tekst |

### Exported CSS Fajlovi (referenca)
```
/Design/figma/
├── 01-navbar-hero.css
├── 02-work.css
├── 03-about.css
├── 04-process.css
├── 05-improve.css
├── 06-last-cta.css
├── 07-footer.css
└── 08-checklist-evaluation-DO-NOT-IMPLEMENT.css  (već implementirano u JS)
```

---

## KONTROLER AKCIJE

**HomeController** — portfolio stranica  
Index, Contact (POST → JSON), UpdateChecklist (POST), SetLanguage, Privacy, Terms, Error

**AdminController** (`/admin`) — zaštićen cookie auth  
Login/LoginPost/Logout, Index, Detail, Delete, OfferSent, Documents, DocumentUpload/Download/Delete, QFileDownload

**QuestionnaireController** (`/q`) — token wizard  
Start (kreira token → redirect), Step1/2/3 (GET+POST), Done, OptOut, file upload (PrivateUploads/)

---

## PROBLEMI I STANJE

### VAŽNO
| Problem | Status | Akcija |
|---------|--------|--------|
| 📷 `profile.png` je 4.7 MB | Potvrđeno | Optimizovati na < 1 MB |
| ⚠️ DB lozinka hardcoded | Potvrđeno | appsettings.Development.json — koristiti user secrets |

### SREDNJE
| Problem | Status | Akcija |
|---------|--------|--------|
| 📱 Nema touch/swipe na carousel | Potvrđeno | Dodati touchstart/touchend listeners u site.js |

### ✓ RIJEŠENO
- ✓ .gitignore — appsettings.Development.json isključen
- ✓ Localization — 3 jezika implementirana
- ✓ Kontakt forma — AJAX + DB persistencija
- ✓ Checklists scoring — dinamički rezultati
- ✓ EF Core migrations (MySQL) — funkcionalno
- ✓ Admin panel + upitnik wizard — implementirani 22.06.2026
- ✓ Deep clean code — uklonjen mrtvi kod, GraphMailClient, komentari — 22.06.2026

---

## GIT

- **Remote:** https://github.com/zoransimeunovic/Portfolio_ZoranSimeunovic.git
- **Branch:** main

---

## POKRETANJE LOKALNO

```bash
dotnet restore
dotnet build
dotnet run    # http://localhost:5157
```

Portovi: HTTP `5157`, HTTPS `7270`  
Varijabla: `ASPNETCORE_ENVIRONMENT=Development`

---

## DEPENDENCIES

```xml
Microsoft.EntityFrameworkCore          8.0.10
Microsoft.EntityFrameworkCore.Design   8.0.10
Pomelo.EntityFrameworkCore.MySql       8.0.2
```

---

## AUTOR

**Zoran Simeunović** — zoransimeunovic@outlook.de  
GitHub: zoransimeunovic | Portfolio: https://zoransimeunovic.de

---

## ZA NOVU AI SESIJU

1. **DB problemi** → sekcija "BAZA PODATAKA"
2. **Nova funkcionalnost** → sekcije "KONTROLER AKCIJE" i "FRONTEND"
3. **Dizajn izmjene** → sekcije "DIZAJN" i "FRONTEND"
4. **Lokalizacija** → sekcija "LOKALIZACIJA"
5. **Debugging** → sekcija "PROBLEMI"

**Napomena:** Dokumentacija ažurirana 22.06.2026.
