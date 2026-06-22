# Baza Podataka — Dokumentacija i Rekonstrukcija

**Baza:** `portfolio_zs` (MySQL)  
**ORM:** Entity Framework Core 8.0.10 via Pomelo 8.0.2  
**Migracije:** `db.Database.Migrate()` se pokreće automatski pri startu u `Program.cs`

---

## Konekcija

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Port=3306;Database=portfolio_zs;User=...;Password=...;"
  }
}
```

Dev konekcija: `appsettings.Development.json` — u `.gitignore`, nikad ne commitovati.

---

## Tabele (6)

### contact_leads
Kontakt podaci iz portfolijo forme + status ponude.

| Kolona | Tip | Opis |
|--------|-----|------|
| Id | INT PK AI | |
| Name | VARCHAR(120) NOT NULL | |
| Email | VARCHAR(200) NOT NULL | |
| Language | VARCHAR(10) | en / de / sr-Latn |
| OptedOut | BOOL NOT NULL DEFAULT 0 | Klijent odbio upitnik |
| OfferSentAt | DATETIME NULL | Datum slanja ponude |
| CreatedAt | DATETIME NOT NULL | |

### checklist_answers
Odgovori iz "How to improve" checkliste.

| Kolona | Tip | Opis |
|--------|-----|------|
| Id | INT PK AI | |
| ContactLeadId | INT FK | → contact_leads (CASCADE DELETE) |
| ListKey | VARCHAR(50) NOT NULL | "design" / "website" / "automation" |
| ItemText | VARCHAR(500) NOT NULL | Tekst čekirane stavke |
| CreatedAt | DATETIME NOT NULL | |

### questionnaires
Token-bazirani upitnik (3 koraka), vezan za ContactLead.

| Kolona | Tip | Opis |
|--------|-----|------|
| Id | INT PK AI | |
| ContactLeadId | INT FK | → contact_leads |
| Token | VARCHAR(64) NOT NULL UNIQUE | GUID za pristup bez login-a |
| ExpiresAt | DATETIME NOT NULL | 30 dana od kreiranja |
| Stage | TINYINT NOT NULL DEFAULT 0 | 0=nov, 1=korak1, 2=korak2, 3=završen |
| Step1Answers | TEXT NULL | JSON odgovori koraka 1 |
| Step2Answers | TEXT NULL | JSON odgovori koraka 2 |
| Step3Answers | TEXT NULL | JSON odgovori koraka 3 |
| CompletedAt | DATETIME NULL | |
| CreatedAt | DATETIME NOT NULL | |

### questionnaire_files
Fajlovi koje klijent šalje uz upitnik (Step 3).

| Kolona | Tip | Opis |
|--------|-----|------|
| Id | INT PK AI | |
| QuestionnaireId | INT FK | → questionnaires (CASCADE DELETE) |
| FileLabel | VARCHAR(100) NOT NULL | Oznaka fajla ("logo", "brief"...) |
| OriginalFileName | VARCHAR(260) NOT NULL | |
| StoredFileName | VARCHAR(100) NOT NULL | GUID naziv na disku |
| ContentType | VARCHAR(100) NOT NULL | |
| SizeBytes | BIGINT NOT NULL | |
| UploadedAt | DATETIME NOT NULL | |

Fajlovi se čuvaju u `PrivateUploads/` (van wwwroot, nije javno dostupno).

### documents
Dokumenti koje admin uploaduje i šalje klijentima.

| Kolona | Tip | Opis |
|--------|-----|------|
| Id | INT PK AI | |
| FullName | VARCHAR(200) NOT NULL | Prikazni naziv |
| OriginalFileName | VARCHAR(260) NOT NULL | |
| StoredFileName | VARCHAR(100) NOT NULL | GUID naziv na disku |
| ContentType | VARCHAR(100) NOT NULL | |
| SizeBytes | BIGINT NOT NULL | |
| UploadedAt | DATETIME NOT NULL | |

Fajlovi se čuvaju u `PrivateUploads/` (isti folder kao questionnaire_files).

### client_actions
Log akcija klijenta putem token linkova.

| Kolona | Tip | Opis |
|--------|-----|------|
| Id | INT PK AI | |
| ContactLeadId | INT FK | → contact_leads |
| ActionType | VARCHAR(50) NOT NULL | Tip akcije |
| CreatedAt | DATETIME NOT NULL | |

---

## EF Core Migrations

```
/Migrations/
├── 20260620213237_InitialCreate                    (contact_leads, checklist_answers)
├── 20260622124601_AddQuestionnaireAndClientActions (questionnaires, client_actions)
├── 20260622195855_AddOptedOutAndOfferSent          (contact_leads: OptedOut, OfferSentAt)
├── 20260622201725_AddDocuments                     (documents)
└── 20260622202525_AddQuestionnaireFiles            (questionnaire_files) ← posljednja
```

### Workflow

```bash
# Nova kolona/tabela: promijeni model → kreiraj migraciju → pokreni app
dotnet ef migrations add NazivMigracije

# Pregled SQL prije primjene
dotnet ef migrations script --idempotent

# Ukloni zadnju ako nije commitovana
dotnet ef migrations remove

# Reset baze: dropuj MySQL bazu, pokreni app (Migrate() će je rekonstruisati)
dotnet run
```

---

## Lokalni Setup (MySQL)

1. Instaliraj MySQL Community Server
2. Kreiraj korisnika:
```sql
CREATE USER 'LocalUser'@'localhost' IDENTIFIED BY 'lozinka';
GRANT ALL PRIVILEGES ON portfolio_zs.* TO 'LocalUser'@'localhost';
FLUSH PRIVILEGES;
```
3. Postavi `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=portfolio_zs;User=LocalUser;Password=lozinka;"
  }
}
```
4. `dotnet run` — Migrate() će kreirati bazu i primijeniti sve migracije.
