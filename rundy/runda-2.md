# Runda 2 — Format zapisany słowami

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
> Polskie znaki mają zostać polskimi znakami.”

## 2. Status kompilacji i działania
- **Kompilacja**: Sukces (0 błędów, 0 ostrzeżeń).
- **Działanie**: Aplikacja poprawnie obsługuje zapis do wskazanego pliku Markdown, zachowując najnowszą notatkę na górze i ładując poprzednie notatki przy starcie.

## 3. Powstałe pliki wyjściowe
- Powstał plik: `notatnik/notatki.md` (zgodnie z wymaganiem: pojedynczy plik w folderze projektu).

## 4. Ustalone kodowanie
- **Kodowanie**: UTF-8.
- Polskie znaki diakrytyczne (`ą, ć, ę, ł, ń, ó, ś, ź, ż`) są w pełni zachowane bez encji HTML ani błędnego kodowania ANSI.

## 5. Rzeczywisty wynik w pliku wyjściowym (`notatnik/notatki.md`)
```markdown
2026-10-29 08:19 — Zażółć gęślą jaźń

Test zapisu polskich znaków: ą, ć, ę, ł, ń, ó, ś, ź, ż.

2026-10-29 08:19 — Zakupy

mleko
chleb
```

## 6. Weryfikacja szczegółowych wymagań
1. **Pojedynczy plik zamiast wielu**: Spełnione — powstał dokładnie jeden plik `notatki.md`.
2. **Nagłówki w wymaganym formacie**: Zastosowano format `RRRR-MM-DD GG:MM — Tytuł` z pauzą em-dash (`—`).
3. **Format daty i godziny**: Wygenerowano `2026-10-29 08:19` (`yyyy-MM-dd HH:mm`).
4. **Polskie znaki**: Zachowane bez uszkodzeń w nagłówkach i treści.
5. **Pusta linia między wpisami**: Pomiędzy końcem pierwszej notatki a nagłówkiem drugiej notatki znajduje się pusta linia.
6. **Kolejność notatek**: Najnowsza notatka (`Zażółć...`) znajduje się na samej górze pliku, a starsza (`Zakupy`) poniżej.

## 7. Różnice względem Rundy 1
- Zmiana rozszerzenia i nazwy pliku z `notatki.txt` na `notatki.md`.
- Wprowadzenie znacznika daty i godziny w nagłówku każdego wpisu.
- Zmiana strategii zapisu: z prostego dopisywania na koniec pliku (`File.AppendAllText`) na bezpieczne odczytanie zawartości i dołączenie nowej notatki na początku (`prepend`).
- Precyzyjne zdefiniowanie odstępów (pusta linia między wpisami).
