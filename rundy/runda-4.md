# Runda 4 — Trzy zróżnicowane przykłady

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
> Przykład 1 — wpis jednolinijkowy:
>
> ## 2026-09-08 14:32 — Zakupy
>
> mleko
>
> Przykład 2 — wpis wielolinijkowy:
>
> ## 2026-09-07 09:15 — Pomysł na projekt
>
> minutnik z historią sesji
>
> oraz eksport wyników
>
> Przykład 3 — polskie znaki i myślnik w tytule:
>
> ## 2026-09-06 18:40 — Ważne: próba zapisu
>
> Zażółć gęślą jaźń. Pamiętaj o ą, ć, ę, ł, ń, ó, ś, ź i ż.”

## 2. Status kompilacji i działania
- **Kompilacja**: Sukces (0 błędów, 0 ostrzeżeń).
- **Działanie**: Aplikacja poprawnie obsługuje nagłówki poziomu 2 (`## `), formatuje wpisy wieloliniowe oraz zapewnia bezpieczne dodawanie nowych notatek na szczycie pliku bez utraty wcześniejszych danych.

## 3. Powstałe pliki wyjściowe
- Powstał plik: `notatnik/notatki.md`.

## 4. Ustalone kodowanie
- **Kodowanie**: UTF-8.

## 5. Rzeczywisty wynik w pliku wyjściowym (`notatnik/notatki.md`)
```markdown
## 2026-10-29 08:23 — Zażółć gęślą jaźń

Test zapisu polskich znaków: ą, ć, ę, ł, ń, ó, ś, ź, ż.

## 2026-10-29 08:23 — Zakupy

mleko
chleb
```

## 6. Wnioski z podania trzech zróżnicowanych przykładów
1. **Wyjaśnienie składni Markdown (`## `)**: Trzy przykłady jednoznacznie usunęły wątpliwości z rund 2 i 3 – każdy nagłówek zaczyna się od `## `, co czyni wpisy semantycznymi nagłówkami poziomu 2 w formacie Markdown.
2. **Elastyczność treści**: Przykłady 1 i 2 pokazały, że treść może być zarówno jednolinijkowa, jak i wielolinijkowa, bez wymuszania sztucznych separatorów między zwykłymi liniami tekstu.
3. **Pewność obsługi znaków specjalnych**: Przykład 3 rozwiał wątpliwości dotyczące dwukropków w tytule oraz polskich znaków diakrytycznych.
4. **Niezawodność formatu**: Zestawienie 3 różnorodnych przykładów (few-shot prompting) stanowi najbardziej precyzyjną i odporną na błędy interpretacji formę specyfikacji wymagań formatowania.
