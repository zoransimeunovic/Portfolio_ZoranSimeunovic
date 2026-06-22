# Figma Audit — Historijska Bilješka (22.06.2026)

Ovaj fajl je arhivski zapis jedne iteracije Figma audita. **Ne koristiti kao referensu za trenutno stanje** — aktuelni dizajn je u `site.css` i `Index.cshtml`.

---

## Šta je implementirano (potvrđeno)

- Navbar, Footer, Last CTA — poklapaju se sa Figmom
- About stage — foto, highlights, LinkedIn/Go ikone
- Work kartice (slika overlay, badge, tech tagovi)
- Process kartice (chip na gornjoj liniji, broj u ćošku, 11 koraka)
- Hero stage (prstenovi, foto, floating tagovi, name tag)
- Checklist dinamika i ocjenjivanje (accordion + scoring)

## Dekorativni elementi (dodati u ovoj iteraciji)

- **Work** — lijevi dekorativni krugovi (`.work-circles`)
- **Process** — desni "Service" krugovi (`.process-circles`)
- **Process** — konektor linije između kolona (isprekidane)
- **Improve** — background light (`.improve-glow`)

## Otvorena pitanja (iz perioda audita)

1. **About foto** — `profile.png` ima pozadinu (4.7 MB). Ako se želi savršen kružni rez potreban je cutout PNG bez pozadine. → **Status:** i dalje otvoreno (vidi CLAUDE.md "PROBLEMI")
2. **Dekorativni krugovi** (Work/Process) — pozicije su aproksimacija jer sekcije nisu apsolutne Figma scene. → **Status:** implementirani, prihvaćeni
3. **Suptilnost dekoracija** (#132949 na tamnom) → **Status:** prihvaćeno

## Namjerno nije dirana

- Hero razmak (670), About foto contain, hover scale
- Strelice (glyph), skill tagovi uz dno, Go ikona +20px
- Jezičak lijevo/veći, LinkedIn ikona, card #5 bez plavog okvira
