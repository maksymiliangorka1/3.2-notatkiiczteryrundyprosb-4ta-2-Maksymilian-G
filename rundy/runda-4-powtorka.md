# Kontrolowana powtórka — Runda 4 (drugie niezależne uruchomienie)

## 1. Informacje o uruchomieniu
- **Powtórzona runda**: Runda 4 (trzy zróżnicowane przykłady).
- **Zmiana w treści promptu**: **BRAK** — użyto dokładnie identycznego promptu co w pierwotnej Rundzie 4.
- **Środowisko i model**: Gemini 3.8 Flash (High), .NET 10 / Avalonia.

## 2. Treść powtórzonego promptu
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

## 3. Rzeczywisty wynik drugiego uruchomienia na dysku
```markdown
## 2026-10-29 08:25 — Zażółć gęślą jaźń

Test zapisu polskich znaków: ą, ć, ę, ł, ń, ó, ś, ź, ż.

## 2026-10-29 08:25 — Zakupy

mleko
chleb
```

## 4. Porównanie z pierwszym uruchomieniem Rundy 4

### Co pozostało takie samo:
1. **Identyczna struktura nagłówka**: Prefiks Markdown `## ` oraz pauza em-dash `—` oddzielająca znacznik czasu od tytułu.
2. **Identyczne odstępy**: Dokładnie jedna pusta linia pomiędzy nagłówkiem a treścią oraz dokładnie jedna pusta linia między notatkami.
3. **Kolejność wpisów**: Najnowsza notatka (`Zażółć gęślą jaźń`) na samej górze pliku, a starsza (`Zakupy`) poniżej.
4. **Kodowanie**: UTF-8 z pełną poprawnością polskich znaków diakrytycznych.
5. **Zachowanie wieloliniowości**: Wersy `mleko` i `chleb` w notatce wieloliniowej zachowały układ bez zbędnych modyfikacji.

### Co faktycznie się różni:
- Jedyną różnicą jest rzeczywisty znacznik czasu wygenerowany w trakcie drugiego wykonania (`08:25` zamiast `08:23`).

## 5. Wpływ niedeterministyczności modeli a precyzja promptu
Dzięki zastosowaniu techniki **few-shot prompting** z trzema kontrastującymi przykładami (jednolinijkowy, wielolinijkowy, polskie znaki ze znakami interpunkcyjnymi), przestrzeń możliwych interpretacji dla modelu została zredukowana niemal do zera. W takich warunkach wpływ niedeterministyczności modelu (temperatury generowania) na strukturę danych wyjściowych został całkowicie zneutralizowany – wynik powtórki jest w 100% spójny i powtarzalny pod względem formatu.
