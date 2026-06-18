# Izvještaj — dubinski Figma audit i korekcije

App za pregled: http://localhost:5199 (CSS izmjene su uživo).
Status: **nije commit-ovano ni push-ovano** — čeka tvoj pregled (radno stablo: `site.css`, `Index.cshtml`).

## Dodati vizuelni objekti koji su NEDOSTAJALI
- **Work — lijevi dekorativni krugovi** (Ellipse 20/21/22: 415 / 307.87 / 199.78, stroke 4px rgba(19,41,73,0.6)) → `.work-circles`
- **Process — desni „Service" krugovi** (605 / 448.82 / 291.24, isti stroke) → `.process-circles`
- **Process — konektor linije**: bila jedna, sada dvije isprekidane (kol. 1-2 i 2-3), boja #132949
- **Improve — BG light** (612×612 #156EF6 opacity .1 blur 150, dole-lijevo) → `.improve-glow`

## Korekcije ka Figmi
- **Process light effect**: blur 300→150, rotacija -25→-24.53°, gradijent 251.89deg
- **Improve ocjena-box**: border 2px→1px #156EF6 + radius 2
- **Improve grupni naslovi**: 16→14px, #FFFFFF, capitalize
- **„Start Analysis"**: dodato podvlačenje
- **Process opis koraka**: dodato opacity 0.75
- `.work` dobio overflow:hidden

## Potvrđeno da se već poklapa sa Figmom
Navbar · Footer · Last CTA · About stage · Work kartice (+„Sjena" overlay) · Process kartice (chip na liniji, broj na ćošku).

## Nisam dirao (namjerno)
- Sve čemu si dao saglasnost: hero razmak (670), about foto contain, hover scale, strelice (glyph), skill tagovi uz dno, Go +20px, jezičak lijevo/veći, LinkedIn ikona, card #5 bez plavog okvira.
- Funkcionalno (checklist dinamika/ocjenjivanje, checkbox UX) — rekao si da je OK.

## Za tvoju odluku
1. **About foto** — ako `profile.png` ima pozadinu, `contain` je pokazuje kao pravougaonik. Za savršen rez treba `Bild_bez backgraund.png` (cutout iz Figme) → ubaci u `wwwroot/images` pa ga uvežem.
2. **Pozicije dekorativnih krugova** (Work/Process) su aproksimacija (sekcije nisu apsolutne scene) — javi jesu li previše/premalo vidljive.
3. Dekoracije su suptilne (#132949 na tamnom) — reci ako treba pojačati/utišati.

## Napomena
Poruka „vrs" usred rada izgleda kao slučajan unos — nisam je tretirao kao zadatak.
