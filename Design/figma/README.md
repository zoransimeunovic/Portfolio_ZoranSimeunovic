# Figma Export — GLAVNI IZVOR ZA DIZAJN

Razdvojeno po sekcijama radi pouzdanog pretraživanja. **Prije svake dizajn-izmjene
prvo pročitati odgovarajući fajl OVDJE** (tačne koordinate, dimenzije, boje, fontovi) —
ne raditi po sjećanju.

Frame: **1440×5992**, pozadina `#030E1F`. Font: **Outfit**. Koordinate su apsolutne
na cijeli frame → za svaku sekciju oduzeti njen `top` offset.

## Fajlovi (redoslijed po Figma `top`)
| Fajl | Sekcija | Figma top |
|------|---------|-----------|
| `01-navbar-hero.css` | Navbar + Hero | 0–923 |
| `02-work.css` | Featured work (3 kartice) | ~1003 |
| `03-about.css` | About me | 1931 |
| `04-process.css` | How we work together (11 koraka) | 2775 |
| `05-improve.css` | How to improve web presence (3 liste) | 3837 |
| `06-last-cta.css` | Take the first step | 4710 |
| `07-footer.css` | Footer | 5247 |
| `08-checklist-evaluation-DO-NOT-IMPLEMENT.css` | Evaluating checklists | 5501 |

## Paleta
| Naziv | Hex |
|-------|-----|
| Primarna (bg) | `#030E1F` |
| Sekundarna (tekst) | `#DDE6F5` |
| Tercijarna (plava) | `#156EF6` |
| Tercijarna 2 (chip) | `#1C2635` |
| Sekundarna 2 (kartica) | `#132949` |
| Gradijent CTA | `linear-gradient(93.78deg, #156EF6 0%, #113AC2 102.39%)` |

## VAŽNO
- Zadnja sekcija (`08-...DO-NOT-IMPLEMENT`) se **NE implementira** — samo pomoć/ocjenjivanje (već je dobro).
- Koristiti postojeće resurse (`wwwroot/images`) i lokalizovani tekst iz `SiteTextProvider.cs` — ne hardkodirati engleski.
