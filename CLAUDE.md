# Portfolio ZoranSimeunovic — Projektna Dokumentacija

## Pravila — obavezno poštovati
- **Cookies:** Zabranjena je upotreba bilo kojeg cookie-a koji nije esencijalan bez izričite saglasnosti vlasnika projekta. Svaki novi cookie koji nije esencijalan mora biti odobren od strane Zorana prije implementacije.
- **Clean code:** Bez komentara koji opisuju ŠTA kod radi. Samo kratki inline komentari za neočigledno ZAŠTO. Bez XML doc blokova. File-scoped namespace u C#.

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
| Baza | SQLite (development) / MySQL Pomelo 8.0.2 (production) |
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
| `Controllers/HomeController.cs` | 6 akcija: Index, Contact (POST), UpdateChecklist (POST), SetLanguage, Privacy, Terms, Error |
| `Data/AppDbContext.cs` | EF DbContext — 2 tabele: contact_leads, checklist_answers |
| `Models/ContactLead.cs` | Name, Email, Language, CreatedAt |
| `Models/ChecklistAnswer.cs` | ContactLeadId, ListKey, ItemText, CreatedAt (FK → ContactLead CASCADE) |
| `Models/ErrorViewModel.cs` | RequestId, ShowRequestId |
| `Content/SiteText.cs` | Model klase za sve sekcije (Nav, Hero, Work, About, Process, Improve, Contact, Footer, Cookie, Legal) |
| `Content/SiteTextProvider.cs` | BuildEn(), BuildDe(), BuildSr() — sav tekstualni sadržaj za 3 jezika |
| `Localization/BrowserCultureProvider.cs` | Accept-Language → kultura; exYu jezici → sr-Latn |
| `Views/Home/Index.cshtml` | Glavna stranica: 8 sekcija (hero, work, about, process, improve, contact, footer, cookie modal) |
| `Views/Home/Privacy.cshtml` | Politika privatnosti |
| `Views/Home/Terms.cshtml` | Uslovi korišćenja |
| `Views/Shared/_Layout.cshtml` | Layout: navbar, footer, Bootstrap + custom CSS |
| `wwwroot/css/site.css` | Dark theme (boje, tipografija, layout, responsive) |
| `wwwroot/js/site.js` | Carousel, checklist scoring, AJAX forma, CSS var scaling |
| `Program.cs` | Startup: MVC, SQLite/MySQL, localization, migrations |
| `appsettings.json` | Production config (connection string placeholder) |
| `appsettings.Development.json` | ⚠️ Dev konekcija — u .gitignore |

---

## BAZA PODATAKA

### Konekcija

**Development — SQLite:**
```json
{ "ConnectionStrings": { "DefaultConnection": "Data Source=portfolio.db" } }
```

**Production — MySQL/Cloud:**
```json
{ "ConnectionStrings": { "DefaultConnection": "" } }
```

### Tabele (2)
- **contact_leads** — Kontakt podaci iz "Take the first step" forme
- **checklist_answers** — Odgovori iz "How to improve" sekcije (FK → contact_leads, CASCADE delete)

### EF Core Migrations
- Sistem: `dotnet ef migrations` (od 20.06.2026)
- `Program.cs` pokreće `db.Database.Migrate()` pri startu
- Migracije u `/Migrations/` folderu
- Trenutna: `20260620213237_InitialCreate`

```bash
dotnet ef migrations add NazivMigracije
dotnet ef migrations remove
```

### Status
✅ **FUNKCIONALNA** — SQLite sa migracima, `portfolio.db` se kreira automatski

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
- Font: Google Fonts — Outfit 400/600/700

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

## KONTROLER AKCIJE (HomeController.cs)

| Akcija | Metod | Opis |
|--------|-------|------|
| `Index` | GET | Glavna stranica |
| `Contact` | POST | AJAX kontakt forma → JSON `{success, message, leadId}` |
| `UpdateChecklist` | POST | Sprema checklist odgovore za dati leadId |
| `SetLanguage` | GET | Setuje `lang` cookie; validira na en/de/sr-Latn |
| `Privacy` | GET | Politika privatnosti |
| `Terms` | GET | Uslovi korišćenja |
| `Error` | GET | Error stranica (ResponseCache NoStore) |

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
- ✓ Git repozitorijum — pushovan na GitHub
- ✓ Localization — 3 jezika implementirana
- ✓ Kontakt forma — AJAX + DB persistencija
- ✓ Checklists scoring — dinamički rezultati
- ✓ SQLite migrations — funkcionalno
- ✓ Clean code refaktoring — 21.06.2026

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
Microsoft.EntityFrameworkCore.Sqlite   8.0.10
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

**Napomena:** Dokumentacija verificirana i ažurirana 21.06.2026 nakon clean code refaktoringa.
