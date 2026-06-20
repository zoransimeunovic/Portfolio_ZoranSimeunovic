# Rekonstrukcija Baze Podataka — Kompletna SQL Dokumentacija

**Namena:** Ovaj fajl sadrži SQL komande i migracije potrebne za kompletnu rekonstrukciju baze podataka `portfolio_zs`. 

**Preporučeni pristup:** Koristi **EF Core Migrations** — automatski rukuje bazom pri startu aplikacije.  
**Alternativa:** Ručna SQL inicijalizacija (bez migrations).

---

## EF Core Migrations Setup (od 20.06.2026)

Projekat sada koristi **`dotnet ef migrations`** za upravljanje bazom podataka.

### Migracije u Projektu

```
/Migrations/
├── 20260620213237_InitialCreate.cs        (kreira contact_leads i checklist_answers)
├── 20260620213237_InitialCreate.Designer.cs
└── AppDbContextModelSnapshot.cs            (snapshot trenutnog modela)
```

### Kako Migracije Rade

1. **Pri startu aplikacije** (`Program.cs`, redovi 70-81):
   - `db.Database.Migrate()` se pokreće
   - Ako baza ne postoji → kreira se sa `InitialCreate` migracijom
   - Ako migracije nisu primenjene → primene se

2. **Za novu sesiju:**
   - Samo pokreni `dotnet run`
   - Migracije će se automatski primeniti

3. **Za dodavanje novih promena:**
   ```bash
   # 1. Promeni model (npr. dodaj novu kolonu u ContactLead)
   # 2. Kreiraj novu migraciju
   dotnet ef migrations add NazivMigracije
   
   # 3. Pregled migracije (pregleda SQL)
   dotnet ef migrations script --idempotent
   
   # 4. Obrišu migraciju ako nije commitovana
   dotnet ef migrations remove
   ```

---

## SQL — Kompletna Inicijalizacija (za ručnu setup)

Ako trebaš da ručno kreirajš bazu (bez migracija), koristi sledeće komande:

```sql
-- Kreiraj bazu
CREATE DATABASE IF NOT EXISTS portfolio_zs 
  CHARACTER SET utf8mb4 
  COLLATE utf8mb4_unicode_ci;

USE portfolio_zs;

-- Tabela 1: contact_leads
CREATE TABLE contact_leads (
  id INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Jedinstveni identifikator lead-a',
  name VARCHAR(120) NOT NULL COMMENT 'Ime kontakta',
  email VARCHAR(200) NOT NULL COMMENT 'Email adresa',
  language VARCHAR(10) COMMENT 'Jezik sajta pri submission-u (en, de, sr-Latn)',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Vrijeme kreiranja reda',
  
  INDEX idx_email (email),
  INDEX idx_created_at (created_at)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tabela 2: checklist_answers
CREATE TABLE checklist_answers (
  id INT PRIMARY KEY AUTO_INCREMENT COMMENT 'Jedinstveni identifikator odgovora',
  contact_lead_id INT NOT NULL COMMENT 'FK na contact_leads.id',
  list_key VARCHAR(50) NOT NULL COMMENT 'Ključ ček liste (npr: "design", "tech", "marketing")',
  item_text VARCHAR(500) NOT NULL COMMENT 'Tekst stavke iz ček liste',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Vrijeme kreiranja reda',
  
  INDEX idx_contact_lead_id (contact_lead_id),
  INDEX idx_list_key (list_key),
  INDEX idx_created_at (created_at),
  
  CONSTRAINT fk_checklist_answers_contact_lead_id 
    FOREIGN KEY (contact_lead_id) 
    REFERENCES contact_leads(id) 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
```

---

## Što se Promenilo — Migracija sa EnsureCreated na Migrations

**Datum:** 20.06.2026

### Izmene u Kodu

**Program.cs:**
- **Staro:** `db.Database.EnsureCreated()` + raw SQL za checklist_answers
- **Novo:** `db.Database.Migrate()` — čistiji pristup, verzionisano

```diff
- db.Database.EnsureCreated();
- db.Database.ExecuteSqlRaw(@"CREATE TABLE IF NOT EXISTS checklist_answers (...)")

+ db.Database.Migrate();
```

**Novi Fajlovi:**
- `/Migrations/20260620213237_InitialCreate.cs` — Inicijalna migracija
- `/Migrations/20260620213237_InitialCreate.Designer.cs` — Automatski generisan
- `/Migrations/AppDbContextModelSnapshot.cs` — Snapshot modela

### Prednosti Migrations

✅ **Verzionisanje:** Sve izmene su u git-u sa timestamp-om  
✅ **Reproducibility:** Bilo ko može da rekonstruiše identičnu bazu  
✅ **Production-ready:** Lakše za deployment  
✅ **Rollback:** `dotnet ef migrations remove` ako trebalo  
✅ **Kolaboracija:** Jasno šta se promenilo i kada  

### Kompatibilnost

- **Lokalna MySQL:** Migracije rade bez problema
- **Cloud MySQL:** Migracije rade bez problema
- **SQLite:** Migracije rade bez problema

---

## Tabela 1: contact_leads

### Struktura

| Kolona | Tip | Null | Key | Extra | Opis |
|--------|-----|------|-----|-------|------|
| id | INT | NO | PRI | AUTO_INCREMENT | Jedinstveni identifikator |
| name | VARCHAR(120) | NO | | | Ime kontakta (obavezno) |
| email | VARCHAR(200) | NO | MUL | | Email adresa (obavezno, validiran) |
| language | VARCHAR(10) | YES | | | Jezička postavka pri submission-u |
| created_at | DATETIME | NO | MUL | DEFAULT CURRENT_TIMESTAMP | Vremenska oznaka kreiranja |

### Indeksi

- `PRIMARY KEY (id)` — Jedinstveni identifikator reda
- `INDEX idx_email (email)` — Za brzo pretraživanje po email-u (O(log n))
- `INDEX idx_created_at (created_at)` — Za sortiranje po vremenu kreiranja

### Validacije

- **name:** Obavezno (NOT NULL), max 120 karaktera
- **email:** Obavezno (NOT NULL), max 200 karaktera, validiran u C# prije upisa
- **language:** Opciono (NULLABLE), max 10 karaktera (en, de, sr-Latn)
- **created_at:** Automatski setuje se na CURRENT_TIMESTAMP pri INSERT-u

### Relacije

- **1:N sa checklist_answers** — jedan kontakt može imati više odgovora na ček liste

### C# Model

- `/Models/ContactLead.cs`
- Korišćenje: `HomeController.Contact()` — INSERT pri submission forme

---

## Tabela 2: checklist_answers

### Struktura

| Kolona | Tip | Null | Key | Extra | Opis |
|--------|-----|------|-----|-------|------|
| id | INT | NO | PRI | AUTO_INCREMENT | Jedinstveni identifikator |
| contact_lead_id | INT | NO | MUL, FK | | Referenca na contact_leads(id) |
| list_key | VARCHAR(50) | NO | MUL | | Ključ liste: "design", "tech", "marketing" |
| item_text | VARCHAR(500) | NO | | | Tekst stavke iz ček liste |
| created_at | DATETIME | NO | MUL | DEFAULT CURRENT_TIMESTAMP | Vremenska oznaka kreiranja |

### Indeksi

- `PRIMARY KEY (id)` — Jedinstveni identifikator reda
- `INDEX idx_contact_lead_id (contact_lead_id)` — Za brzo pronalaženje po lead-u
- `INDEX idx_list_key (list_key)` — Za filtriranje po list key-u
- `INDEX idx_created_at (created_at)` — Za sortiranje po vremenu

### Foreign Key Constraint

```
CONSTRAINT fk_checklist_answers_contact_lead_id
  FOREIGN KEY (contact_lead_id)
  REFERENCES contact_leads(id)
  ON DELETE CASCADE
  ON UPDATE CASCADE
```

- **Referenca:** `contact_leads(id)`
- **ON DELETE CASCADE:** Ako se obriše lead, automatski se obrišu svi njegovi odgovori
- **ON UPDATE CASCADE:** Ako se ID lead-a ažurira, ažurira se i u svim odgovorima

### Validacije

- **contact_lead_id:** Obavezno (NOT NULL), mora postojati u contact_leads tabeli
- **list_key:** Obavezno (NOT NULL), max 50 karaktera
- **item_text:** Obavezno (NOT NULL), max 500 karaktera
- **created_at:** Automatski setuje se na CURRENT_TIMESTAMP pri INSERT-u

### Relacije

- **N:1 sa contact_leads** — više odgovora pripada jednom kontaktu

### C# Model

- `/Models/ChecklistAnswer.cs`
- Korišćenje: `HomeController.UpdateChecklist()` — INSERT/UPDATE nakon što korisnik kompletira ček liste

---

## Entity Relationship Diagram

```
┌─────────────────────────────────────────────────────────────────┐
│ contact_leads                                                     │
├─────────────────────────────────────────────────────────────────┤
│ [PK] id (INT, AUTO_INCREMENT)                                   │
│      name (VARCHAR(120), NOT NULL)                              │
│      email (VARCHAR(200), NOT NULL)                             │
│      language (VARCHAR(10), NULLABLE)                           │
│      created_at (DATETIME, DEFAULT CURRENT_TIMESTAMP)           │
│                                                                   │
│      Indeksi: idx_email, idx_created_at                         │
└─────────────────────────────────────────────────────────────────┘
              │
              │ 1:N Relacija
              │ contact_lead_id
              │ (FK, CASCADE DELETE)
              │
              ▼
┌─────────────────────────────────────────────────────────────────┐
│ checklist_answers                                                 │
├─────────────────────────────────────────────────────────────────┤
│ [PK] id (INT, AUTO_INCREMENT)                                   │
│ [FK] contact_lead_id (INT, FK → contact_leads.id)              │
│      list_key (VARCHAR(50), NOT NULL)                           │
│      item_text (VARCHAR(500), NOT NULL)                         │
│      created_at (DATETIME, DEFAULT CURRENT_TIMESTAMP)           │
│                                                                   │
│      Indeksi: idx_contact_lead_id, idx_list_key, idx_created_at│
└─────────────────────────────────────────────────────────────────┘
```

---

## Inicijalizacija iz EF Core Koda

Baza se automatski kreira iz C# koda pri startu aplikacije.

**Fajl:** `/Data/AppDbContext.cs` — `OnModelCreating()` metoda  
**Pokretač:** `/Program.cs` (redovi 70-95) — `Database.EnsureCreated()`

```csharp
try
{
    context.Database.EnsureCreated();
}
catch (Exception ex)
{
    logger.LogWarning($"Database initialization failed: {ex.Message}");
}
```

**Ponašanje:**
- Automatski kreira tabele ako ne postoje
- Pokreće se pri startu aplikacije
- Greške su loggovane ali ne zaustavljaju aplikaciju
- Omogućava aplikaciji da radi čak i ako DB nije dostupna

---

## Primeri SQL Query-ja za Rad sa Bazom

### Insert novo kontakt čitanje

```sql
INSERT INTO contact_leads (name, email, language, created_at)
VALUES ('Pera Perić', 'pera@example.com', 'sr-Latn', NOW());
```

### Pronađi sve lead-e po email-u

```sql
SELECT * FROM contact_leads 
WHERE email = 'pera@example.com';
```

### Pronađi sve lead-e iz poslednje 7 dana

```sql
SELECT * FROM contact_leads 
WHERE created_at >= DATE_SUB(NOW(), INTERVAL 7 DAY)
ORDER BY created_at DESC;
```

### Pronađi sve odgovore za određeni lead

```sql
SELECT ca.* FROM checklist_answers ca
INNER JOIN contact_leads cl ON ca.contact_lead_id = cl.id
WHERE cl.email = 'pera@example.com'
ORDER BY ca.list_key, ca.created_at;
```

### Pronađi sve lead-e sa njihovim odgovorima (LEFT JOIN)

```sql
SELECT 
  cl.id,
  cl.name,
  cl.email,
  ca.list_key,
  ca.item_text,
  ca.created_at AS answer_date
FROM contact_leads cl
LEFT JOIN checklist_answers ca ON cl.id = ca.contact_lead_id
ORDER BY cl.created_at DESC, ca.list_key;
```

### Obrišu sve odgovore za određeni lead (CASCADE automatski)

```sql
DELETE FROM contact_leads 
WHERE id = 5;
-- Automatski briše sve redove iz checklist_answers gde je contact_lead_id = 5
```

### StatističkeQuery-je

```sql
-- Broj lead-a po jeziku
SELECT language, COUNT(*) as broj 
FROM contact_leads 
GROUP BY language;

-- Broj odgovora po ček listi
SELECT list_key, COUNT(*) as broj 
FROM checklist_answers 
GROUP BY list_key;

-- Lead-i sa najviše odgovora
SELECT cl.name, COUNT(ca.id) as broj_odgovora
FROM contact_leads cl
LEFT JOIN checklist_answers ca ON cl.id = ca.contact_lead_id
GROUP BY cl.id, cl.name
ORDER BY broj_odgovora DESC;
```

---

## Setup — Korišćenje EF Core Migrations (Preporučeno od 20.06.2026)

### Najjednostavnije — Automatski Setup sa Migracima

1. **Obezbedi connection string u `appsettings.Development.json`:**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3306;Database=portfolio_zs;User=LocalUser;Password=password;"
     }
   }
   ```

2. **Pokreni aplikaciju:**
   ```bash
   dotnet run
   ```

3. **Gotovo!** Migracije će automatski:
   - Kreirati bazu `portfolio_zs` ako ne postoji
   - Primeniti sve migracije iz `/Migrations/` foldera
   - Tabele će biti kreirane sa svim indeksima i foreign key-jima

### Ako trebaš da Resetuješ Bazu

```bash
# 1. Obriši bazu (ili je pusti da ostane, migracije će je resetovati)
# 2. Obriši local.db ako koristiš SQLite
rm portfolio.db

# 3. Pokreni aplikaciju — sve će biti kreirano ponovo
dotnet run
```

### Za Dodavanje Novih Kolona / Tabela

```bash
# 1. Promeni model u C# (npr. `/Models/ContactLead.cs`)
# 2. Kreiraj novu migraciju sa opisnim imenom
dotnet ef migrations add AddNewColumnToContactLead

# 3. Pregleda SQL koji će biti primenjen
dotnet ef migrations script --idempotent

# 4. Pokreni aplikaciju — migracija će biti primenjena
dotnet run
```

### Za Vraćanje Prethodne Migracije (Rollback)

```bash
# Obriši zadnju migraciju
dotnet ef migrations remove

# Ili direktno ažuriraj bazu na specifičnu migraciju
dotnet ef database update 20260620213237_InitialCreate
```

---

## Setup — Lokalna MySQL (Development - Ručna Setup)

### Na Windows-u (MySQL Community Server)

1. Preuzmi i instaliraj MySQL Community Server (https://dev.mysql.com/downloads/mysql/)
2. Pri instalaciji seti root lozinku
3. Otvori MySQL Command Line Client (ili koristi PowerShell)

```bash
mysql -u root -p
```

4. Pokreni sve SQL komande iz sekcije "SQL Kompletna Inicijalizacija" gore

5. Kreiraj dev korisnika (opciono, za bezbednost):

```sql
CREATE USER 'LocalUser'@'localhost' IDENTIFIED BY 'secure_password_123';
GRANT ALL PRIVILEGES ON portfolio_zs.* TO 'LocalUser'@'localhost';
FLUSH PRIVILEGES;
```

6. Ažuriraj `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=portfolio_zs;User=LocalUser;Password=secure_password_123;"
  }
}
```

7. Pokreni aplikaciju:

```bash
dotnet run
```

### Na Linux-u (apt)

```bash
sudo apt-get install mysql-server

sudo mysql_secure_installation  # Postavi root lozinku i bezbednost

sudo mysql -u root -p
```

Zatim pokreni SQL komande kao gore.

---

## Setup — Cloud MySQL (AWS RDS / DigitalOcean)

1. Kreiraj MySQL instancu (npr. AWS RDS, DigitalOcean)
2. Obezbedi connection string sa whitelisted IP-om
3. Preuzmi MySQL client CLI:

```bash
# macOS
brew install mysql-client

# Windows (PowerShell)
choco install mysql
```

4. Povezan na cloud bazu i pokreni SQL komande:

```bash
mysql -h your-db-endpoint.amazonaws.com -u admin -p
# Unesite lozinku
```

5. Ažuriraj `appsettings.Development.json` sa cloud connection string-om:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-db-endpoint.amazonaws.com;Port=3306;Database=portfolio_zs;User=admin;Password=your_password;"
  }
}
```

---

## Setup — SQLite (Development bez Network zavisnosti)

SQLite je idealan za lokalnu razvojnu bazu bez potrebe za vanjskom MySQL instancom.

### Instalacija

1. Dodaj NuGet paket u `.csproj`:

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.10" />
```

2. U `Program.cs`, zameni MySQL konfiguraciju sa SQLite:

```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=portfolio.db";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));
```

3. Obrišu/ignoriši remote MySQL connection string iz `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=portfolio.db"
  }
}
```

4. Baza će se automatski kreirati kao `portfolio.db` fajl pri startu aplikacije

### Prednosti SQLite za Development
- Nema potrebe za vanjskom MySQL instancom
- Brža startup vrijeme
- Lakši za lokalno testiranje
- `portfolio.db` fajl je commit-able (ili ga dodaj u .gitignore ako trebaš čiste okoline)

### Napomena za Production
- SQLite NIJE prikladan za produkciju (nema concurrent write поддршке)
- Za produkciju koristi MySQL ili PostreSQL

---

## Diagnostika i Troubleshooting

### Greška: "Access denied for user 'RemoteAppUser'@'217.232.240.42'"

**Uzrok:** Tvoj IP nije whitelisted na remote MySQL serveru.

**Rešenje:**
1. Koristi lokalnu MySQL umesto remote
2. Zatraži admin da whitelistuje tvoj IP na remote serveru
3. Koristi SSH tunel ako je dostupan

### Greška: "Can't connect to MySQL server on 'localhost:3306'"

**Uzrok:** MySQL server nije pokrenuta lokalno.

**Rešenje:**
- Windows: Pokreni "MySQL80" servis iz Services (services.msc)
- Linux/macOS: `sudo systemctl start mysql`

### Baza je prazna nakon što se aplikacija pokrene

**Uzrok:** Migracije su kreirale tabele ali su prazne (normalno — nova baza).

**Rešenje:** To je očekivano. Kontakt forma će popunjavati redove pri submission-u.

### Greška: "Migration pending: InitialCreate"

**Uzrok:** Neka migracija nije primenjena na bazu.

**Rešenje:**
```bash
# Primeni sve pending migracije
dotnet ef database update

# Ili pokreni aplikaciju
dotnet run  # Automatski će primeniti migracije
```

### Greška: "There is already an open DataReader associated with this connection"

**Uzrok:** Konkurentna konekcija na bazu (DBContext nije pravilno rasporedjena).

**Rešenje:**
- Provjeri da li trebaš `.AsNoTracking()` za query-je
- Koristi `await` pravilno u HomeController.cs

### Trebam da vidim šta je u bazi

```bash
mysql -u LocalUser -p portfolio_zs
mysql> SELECT * FROM contact_leads;
mysql> SELECT * FROM checklist_answers;
```

---

## Napomena

Ovaj fajl je **referentna dokumentacija** za SQL strukture i setup. Za normalan rad vidi `CLAUDE.md`.
