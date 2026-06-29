# Database Reconstruction — portfolio_zs

> **Pouzdani izvor podataka o bazi.** Ažurirati odmah pri svakoj promjeni šeme.  
> Izvor: `AppDbContextModelSnapshot.cs` + EF migracije (zadnja: `AddPackageNameToContactLead`)

---

## Konekcija

- **Engine:** MySQL (Pomelo 8.0.2)
- **Database:** `portfolio_zs`
- **Connection string:** `appsettings.Development.json` → `ConnectionStrings:DefaultConnection`  
  (u `.gitignore` — nikad ne commitovati)
- **Migracije:** automatski pri startu — `db.Database.Migrate()` u `Program.cs`

---

## Važno: imenovanje kolona (naming convention)

Postoji **nedosljednost** u projektu koja se mora poštovati radi kompatibilnosti:

| Tabela | Konvencija kolona | Razlog |
|--------|------------------|--------|
| `contact_leads` | **PascalCase** (`Id`, `Name`, `ConfirmationToken`...) | Nema `HasColumnName` u `AppDbContext` za `ContactLead` entitet |
| Sve ostale tabele | **snake_case** (`id`, `token`, `contact_id`...) | Eksplicitni `HasColumnName` u `AppDbContext` |

Ovo važi i za `SyncDbContext` (SyncWorkerService) — on nema mapping za `ContactLead` kolone,
pa se oslanja na iste PascalCase nazive.

**Pravilo za buduće kolone:**
- Nova kolona u `contact_leads` → **ne dodavati** `HasColumnName` (ostati PascalCase)
- Nova kolona u ostalim tabelama → **obavezno** `HasColumnName("snake_case_naziv")`

---

## Tabele (5 aktivnih)

### 1. `contact_leads`

**C# model:** `Models/ContactLead.cs`

| Kolona | MySQL tip | Null | Napomena |
|--------|-----------|------|---------|
| `Id` | `int` | NOT NULL | PK, AUTO_INCREMENT |
| `Name` | `varchar(120)` | NOT NULL | |
| `Email` | `varchar(200)` | NOT NULL | |
| `Language` | `varchar(10)` | NULL | `en` / `de` / `sr-Latn` |
| `CreatedAt` | `datetime(6)` | NOT NULL | UTC |
| `OptedOut` | `tinyint(1)` | NOT NULL | default `0`; klijent odbio upitnik |
| `OfferSentAt` | `datetime(6)` | NULL | admin akcija — datum slanja ponude |
| `ConfirmationToken` | `varchar(64)` | NULL | UNIQUE INDEX; NULL dok SyncWorker ne generise |
| `ConfirmationTokenExpiresAt` | `datetime(6)` | NULL | +48h od kreiranja tokena |
| `EmailConfirmedAt` | `datetime(6)` | NULL | NULL = email nije potvrđen |
| `ConfirmationEmailSentAt` | `datetime(6)` | NULL | SyncWorker flag — NULL = čeka slanje |
| `QuestionnaireEmailSentAt` | `datetime(6)` | NULL | SyncWorker flag — NULL = čeka slanje |
| `RegistrationNotificationSentAt` | `datetime(6)` | NULL | SyncWorker flag — NULL = čeka slanje |
| `OptOutNotificationSentAt` | `datetime(6)` | NULL | SyncWorker flag — NULL = čeka slanje |
| `PackageName` | `varchar(100)` | NULL | Paket odabran na sajtu (pricing sekcija) |

**Indeksi:**
- `PK_contact_leads` → `Id`
- `IX_contact_leads_ConfirmationToken` → `ConfirmationToken` (UNIQUE)

**SyncWorker upiti za ovu tabelu:**
```sql
-- Confirmation email
WHERE ConfirmationToken IS NOT NULL AND ConfirmationEmailSentAt IS NULL

-- Questionnaire pozivnica
WHERE EmailConfirmedAt IS NOT NULL AND QuestionnaireEmailSentAt IS NULL

-- Notifikacija adminu o novoj registraciji
WHERE ConfirmationToken IS NOT NULL AND RegistrationNotificationSentAt IS NULL

-- Notifikacija o opt-out
WHERE OptedOut = 1 AND OptOutNotificationSentAt IS NULL
```

---

### 2. `checklist_answers`

**C# model:** `Models/ChecklistAnswer.cs`

| Kolona | MySQL tip | Null | Napomena |
|--------|-----------|------|---------|
| `id` | `int` | NOT NULL | PK, AUTO_INCREMENT |
| `contact_lead_id` | `int` | NOT NULL | FK → `contact_leads.Id` CASCADE DELETE |
| `list_key` | `varchar(50)` | NOT NULL | `design` / `website` / `automation` |
| `item_text` | `varchar(500)` | NOT NULL | tekst čekirane stavke |
| `created_at` | `datetime(6)` | NOT NULL | UTC |

**Indeksi:**
- `PK_checklist_answers` → `id`
- `IX_checklist_answers_contact_lead_id` → `contact_lead_id`

---

### 3. `questionnaire`

**C# model:** `Models/Questionnaire.cs`  
Napomena: tabela se zove `questionnaire` (jednina), ne `questionnaires`.

| Kolona | MySQL tip | Null | Napomena |
|--------|-----------|------|---------|
| `id` | `int` | NOT NULL | PK, AUTO_INCREMENT |
| `contact_id` | `int` | NOT NULL | FK → `contact_leads.Id` CASCADE DELETE |
| `token` | `varchar(64)` | NOT NULL | UNIQUE; pristupni link za wizard |
| `token_expires_at` | `datetime(6)` | NOT NULL | +30 dana od kreiranja |
| `stage` | `tinyint unsigned` | NOT NULL | `0`=kreiran `1`=step1 `2`=step2 `3`=step3 |
| `step1_answers` | `longtext` | NULL | JSON string |
| `step2_answers` | `longtext` | NULL | JSON string |
| `step3_answers` | `longtext` | NULL | JSON string |
| `completed_at` | `datetime(6)` | NULL | NULL = nije završen |
| `completion_notification_sent_at` | `datetime(6)` | NULL | SyncWorker flag |
| `created_at` | `datetime(6)` | NOT NULL | UTC |

**Indeksi:**
- `PK_questionnaire` → `id`
- `IX_questionnaire_contact_id` → `contact_id`
- `IX_questionnaire_token` → `token` (UNIQUE)

**SyncWorker upit:**
```sql
WHERE completed_at IS NOT NULL AND completion_notification_sent_at IS NULL
```

---

### 4. `questionnaire_files`

**C# model:** `Models/QuestionnaireFile.cs`  
Fajlovi se čuvaju na disku: `PrivateUploads/` (van `wwwroot`).

| Kolona | MySQL tip | Null | Napomena |
|--------|-----------|------|---------|
| `id` | `int` | NOT NULL | PK, AUTO_INCREMENT |
| `questionnaire_id` | `int` | NOT NULL | FK → `questionnaire.id` CASCADE DELETE |
| `file_label` | `varchar(50)` | NOT NULL | oznaka fajla (tip) |
| `original_file_name` | `varchar(255)` | NOT NULL | originalni naziv |
| `stored_file_name` | `varchar(100)` | NOT NULL | ime na disku |
| `content_type` | `varchar(100)` | NOT NULL | MIME tip |
| `size_bytes` | `bigint` | NOT NULL | |
| `uploaded_at` | `datetime(6)` | NOT NULL | UTC |

**Indeksi:**
- `PK_questionnaire_files` → `id`
- `IX_questionnaire_files_questionnaire_id` → `questionnaire_id`

---

### 5. `documents`

**C# model:** `Models/Document.cs`  
Admin upload dokumenti. Čuvaju se u `PrivateUploads/`.

| Kolona | MySQL tip | Null | Napomena |
|--------|-----------|------|---------|
| `id` | `int` | NOT NULL | PK, AUTO_INCREMENT |
| `full_name` | `varchar(255)` | NOT NULL | prikazni naziv u admin panelu |
| `original_file_name` | `varchar(255)` | NOT NULL | |
| `stored_file_name` | `varchar(100)` | NOT NULL | ime na disku |
| `content_type` | `varchar(100)` | NOT NULL | MIME tip |
| `size_bytes` | `bigint` | NOT NULL | |
| `uploaded_at` | `datetime(6)` | NOT NULL | UTC |

**Indeksi:**
- `PK_documents` → `id`

---

## Relacije

```
contact_leads (1) ──── (N) checklist_answers    [CASCADE DELETE]
contact_leads (1) ──── (N) questionnaire         [CASCADE DELETE]
questionnaire  (1) ──── (N) questionnaire_files  [CASCADE DELETE]
documents — bez FK veza
```

---

## EF Core Migration historija

Tabela `__EFMigrationsHistory` (EF Core interno).

| MigrationId | Šta je urađeno |
|-------------|----------------|
| `20260620213237_InitialCreate` | Kreiran `contact_leads` (Id, Name, Email, Language, CreatedAt) i `checklist_answers` |
| `20260622124601_AddQuestionnaireAndClientActions` | Kreiran `questionnaire`; kreiran `client_actions` (obrisano u kasnijoj migraciji) |
| `20260622195855_AddOptedOutAndOfferSent` | `contact_leads`: dodano `OptedOut`, `OfferSentAt`; MySQL tipovi konvertovani iz TEXT |
| `20260622201725_AddDocuments` | Kreirana `documents` tabela |
| `20260622202525_AddQuestionnaireFiles` | Kreirana `questionnaire_files` tabela |
| `20260623122430_AddEmailConfirmation` | `contact_leads`: dodano `ConfirmationToken`, `ConfirmationTokenExpiresAt`, `EmailConfirmedAt` |
| `20260623204811_AddNotificationFlagsRemoveClientActions` | Obrisana `client_actions`; dodane bool flag kolone (privremeno) |
| `20260623205314_UseTimestampsForNotificationFlags` | Bool flags → `DateTime?` timestamp kolone: `ConfirmationEmailSentAt`, `QuestionnaireEmailSentAt`, `RegistrationNotificationSentAt`, `OptOutNotificationSentAt` u `contact_leads`; `completion_notification_sent_at` u `questionnaire` |

---

## Dodavanje nove migracije

```bash
dotnet ef migrations add NazivMigracije
dotnet ef migrations remove   # poništi ako treba
```

Primjena: automatski pri `dotnet run` (`Program.cs` → `db.Database.Migrate()`).

**Nakon svake migracije — ažurirati ovaj fajl.**

---

## Rekonstrukcija baze od nule

```sql
-- 1. Kreiraj korisnika (MySQL Workbench / shell)
CREATE USER 'LocalUser'@'localhost' IDENTIFIED BY 'lozinka';
GRANT ALL PRIVILEGES ON portfolio_zs.* TO 'LocalUser'@'localhost';
FLUSH PRIVILEGES;
```

```json
// 2. appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=portfolio_zs;User=LocalUser;Password=lozinka;"
  }
}
```

```bash
# 3. Pokretanje — Migrate() kreira bazu i primjenjuje sve migracije
dotnet run
```
