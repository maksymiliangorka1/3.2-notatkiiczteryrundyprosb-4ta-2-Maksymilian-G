# Runda 3 — Jeden przykład

## 1. Treść promptu
> „Zrób notatnik: pole na tytuł, pole na treść, przycisk zapisu i lista zapisanych notatek. Notatki mają być zapisywane do pliku.
>
> Notatki zapisuj do jednego pliku notatki.md w folderze projektu.
>
> Format wpisu:
>
> 2026-09-08 14:32 — Tytuł notatki
>
> treść notatki, może mieć kilka linii
>
> Między wpisami pusta linia. Najnowsza notatka na górze pliku.
>
> Polskie znaki mają zostać polskimi znakami.
>
> Plik ma wyglądać dokładnie tak:
>
> 2026-09-08 14:32 — Zakupy
>
> mleko
>
> chleb
>
> 2026-09-07 09:15 — Pomysł na projekt
>
> minutnik z historią sesji”

## 2. Status kompilacji i działania
- **Kompilacja**: Sukces (0 błędów, 0 ostrzeżeń).
- **Działanie**: Poprawne. Aplikacja kompiluje się i zapisuje notatki zgodnie z podanym wzorcem struktury.

## 3. Powstałe pliki wyjściowe
- Powstał plik: `notatnik/notatki.md`.

## 4. Ustalone kodowanie
- **Kodowanie**: UTF-8.

## 5. Rzeczywisty wynik w pliku wyjściowym (`notatnik/notatki.md`)
```markdown
2026-10-29 08:21 — Zażółć gęślą jaźń

Test zapisu polskich znaków: ą, ć, ę, ł, ń, ó, ś, ź, ż.

2026-10-29 08:21 — Zakupy

mleko
chleb
```

## 6. Wpływ pojedynczego przykładu na interpretację modelu
1. **Jednoznaczność odstępów między notatkami**: Przykład jednoznacznie potwierdza konieczność pustej linii (`\n\n`) pomiędzy kolejnymi notatkami.
2. **Brak znaczników Markdown dla nagłówków**: Podany przykład nie używał znaczników Markdown (brak `#` lub `##`), co potwierdziło, że nagłówek ma być czystą linią z datą i pauzą em-dash.
3. **Dwuznaczność pojedynczego przykładu**: W podanym przykładzie wersy `mleko` i `chleb` rozdzielono pustą linią. Z pojedynczego przykładu trudno jednoznacznie wywnioskować, czy każda linijka treści ma być sztucznie rozdzielana pustą linią przez program, czy też zależy to od danych wpisanych przez użytkownika w polu edycji (wielo- vs jednolinijkowa treść).
