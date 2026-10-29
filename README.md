# Zadanie 3.2 — Notatnik i cztery rundy próśb

Aplikacja desktopowa **Notatnik** stworzona w technologii **C# / .NET 10 / Avalonia UI**, zrealizowana w ramach eksperymentu inżynierii promptów z modelem **Gemini 3.8 Flash (High)**.

Repozytorium dokumentuje ewolucję specyfikacji wymagań: od lakonicznego, jednozdaniowego promptu, poprzez specyfikację słowną, podejście one-shot, few-shot (trzy zróżnicowane przykłady), meta-prompting, aż po analizę powtarzalności i ręczną implementację odporności na błędy zapisu.

---

## 1. Cel zadania

Głównym celem zadania było:
1. Zbudowanie w pełni funkcjonalnej aplikacji desktopowej Notatnik w architekturze Avalonia UI.
2. Praktyczne zbadanie, jak stopień uszczegółowienia promptu (minimalny prompt -> format słowny -> 1 przykład -> 3 zróżnicowane przykłady) wpływa na kod i format danych wyjściowych generowanych przez agenta AI.
3. Przeprowadzenie analizy meta-promptu (poproszenie modelu o analizę i zoptymalizowanie pierwotnej prośby).
4. Zbadanie powtarzalności i niedeterministyczności modelu w kontrolowanym eksperymencie.
5. Samodzielne zaimplementowanie obsługi błędów zapisu z blokiem `try/catch` i kontrolką statusu.
6. Rygorystyczna walidacja działania aplikacji (test 5 notatek, kolejność LIFO, kodowanie UTF-8, obsługa polskich znaków).

---

## 2. Użyte technologie i wymagania środowiskowe

- **Język programowania**: C# 13 / .NET 10 SDK (wersja SDK: `10.0.401`)
- **Framework UI**: Avalonia UI (wersja `11.x`, szablon `avalonia.app`)
- **System operacyjny**: Windows 11 (x64)
- **Model AI**: Gemini 3.8 Flash (High) (Google DeepMind)
- **Kontrola wersji**: Git

---

## 3. Instrukcja uruchomienia aplikacji

### Wymagania wstępne
Upewnij się, że masz zainstalowany .NET SDK (wersja 9.0 lub 10.0):
```bash
dotnet --version
```

### Uruchomienie projektu
1. Sklonuj repozytorium:
   ```bash
   git clone https://github.com/maksymiliangorka1/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G.git
   cd 3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G
   ```
2. Zbuduj i uruchom aplikację:
   ```bash
   dotnet run --project notatnik
   ```
3. W oknie aplikacji:
   - Wpisz tytuł w polu **Tytuł:**
   - Wpisz treść (jedno- lub wielowierszową) w polu **Treść:**
   - Kliknij przycisk **Zapisz**
   - Zapisana notatka pojawi się na liście powyżej, a w pliku `notatnik/notatki.md` zostanie umieszczona na samej górze.

---

## 4. Opis formatu pliku `notatki.md`

Plik `notatnik.md` znajduje się w katalogu projektu `notatnik/`. Wpisy są sformatowane jako semantyczne nagłówki poziomu 2 języka Markdown:

```markdown
## RRRR-MM-DD GG:MM — Tytuł notatki

Treść notatki (może składać się z wielu wierszy)

## RRRR-MM-DD GG:MM — Wcześniejsza notatka

Treść wcześniejszej notatki
```

### Reguły formatu:
- **Prefiks nagłówka**: `## ` (nagłówek H2).
- **Format daty i czasu**: `yyyy-MM-dd HH:mm` (np. `2026-10-29 08:31`).
- **Separator nagłówka**: pauza em-dash `—` (kod Unicode `U+2014`) otoczona spacjami.
- **Odstępy**: Dokładnie jedna pusta linia między nagłówkiem a treścią notatki oraz dokładnie jedna pusta linia pomiędzy kolejnymi wpisami.
- **Kolejność wpisów**: **Najnowszy wpis na samej górze** pliku (odwrotna kolejność chronologiczna / LIFO).
- **Kodowanie**: UTF-8 (pełne wsparcie dla polskich znaków: `ą, ć, ę, ł, ń, ó, ś, ź, ż`).

---

## 5. Wyniki rund 1–4 i porównanie rezultatów

Szczegółowe raporty z każdego etapu znajdują się w katalogu [`rundy/`](file:///c:/Users/gorka.maksymilian/Documents/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G/rundy/):
- [runda-1.md](file:///c:/Users/gorka.maksymilian/Documents/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G/rundy/runda-1.md)
- [runda-2.md](file:///c:/Users/gorka.maksymilian/Documents/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G/rundy/runda-2.md)
- [runda-3.md](file:///c:/Users/gorka.maksymilian/Documents/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G/rundy/runda-3.md)
- [runda-4.md](file:///c:/Users/gorka.maksymilian/Documents/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G/rundy/runda-4.md)

### Tabela porównawcza rund 1–4:

| Cecha | Runda 1 (Minimalny prompt) | Runda 2 (Format słowny) | Runda 3 (Jeden przykład) | Runda 4 (Trzy przykłady) |
|---|---|---|---|---|
| **Plik wyjściowy** | `notatki.txt` (decyzja agenta) | `notatki.md` | `notatki.md` | `notatki.md` |
| **Znacznik czasu** | Brak (nie podano w prośbie) | `2026-10-29 08:19` | `2026-10-29 08:21` | `2026-10-29 08:23` |
| **Format nagłówka** | `Tytuł: ...` | `2026-10-29 08:19 — ...` | `2026-10-29 08:21 — ...` | `## 2026-10-29 08:23 — ...` |
| **Kolejność wpisów** | Chronologiczna (dopisywanie na końcu, FIFO) | Najnowszy na górze (prepend, LIFO) | Najnowszy na górze (prepend, LIFO) | Najnowszy na górze (prepend, LIFO) |
| **Formatowanie treści** | Etykieta `Treść:` + separator `---` | Tekst oddzielony pustą linią | Wątpliwość co do pustych linii wewnątrz treści | Czyste, naturalne wsparcie dla 1- i wielowierszowości |
| **Jednoznaczność promptu** | Skrajnie niska | Średnia (nieprecyzyjne formatowanie Markdown) | Umiarkowana (możliwe nadinterpretowanie pustych linii) | **Bardzo wysoka (brak niejednoznaczności)** |

---

## 6. Wynik Rundy 5 (meta-prompt) i porównanie z Rundą 2

W Rundzie 5 model otrzymał zadanie analizy promptu z Rundy 1 pod kątem brakujących informacji oraz sformułowania poprawionej specyfikacji ([runda-5.md](file:///c:/Users/gorka.maksymilian/Documents/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G/rundy/runda-5.md)).

### Rezultat meta-analizy:
Model trafnie wskazał 6 krytycznych braków w pierwotnej prośbie:
1. Brak lokalizacji i nazwy pliku.
2. Brak struktury wpisu i braku znacznika czasu.
3. Brak zdefiniowanego porządku wpisów (FIFO vs LIFO).
4. Brak jawnego kodowania znaków (UTF-8).
5. Brak specyfikacji zachowania interfejsu (czyszczenie kontrolek, wczytywanie historii).
6. Brak definicji platformy technologicznej.

### Porównanie z Rundą 2:
- W Rundzie 2 ograniczenia formatu były formułowane ręcznie przez człowieka, co pominęło np. semantykę nagłówków Markdown (`## `) czy zachowanie UI po zapisie.
- Meta-prompt pozwolił modelowi samodzielnie uzupełnić luki architektoniczne bez ulegania tzw. *scope creep* — model nie dodał niepotrzebnych bibliotek zewnętrznych ani zbędnych funkcji (np. usuwania, wyszukiwania), lecz stworzył czystą, jednoznaczną specyfikację inżynierską.

---

## 7. Kontrolowana powtórka i analiza niedeterministyczności

Do kontrolowanej powtórki wybrano **Rundę 4**, uruchamiając ją po raz drugi w identycznym środowisku z **dokładnie tą samą treścią promptu** ([runda-4-powtorka.md](file:///c:/Users/gorka.maksymilian/Documents/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G/rundy/runda-4-powtorka.md)).

### Wyniki porównania:
- **Co pozostało identyczne**: Pełna składnia nagłówka (`## ` + data + pauza em-dash), zachowanie struktury jedno- i wielowierszowej, kodowanie UTF-8, porządek LIFO (najnowsza notatka na górze) oraz separacja pustą linią.
- **Co się różniło**: Jedynie rzeczywisty czas wykonania próby (`08:25` vs `08:23`).

### Analiza niedeterministyczności modeli:
Modele językowe z natury generują odpowiedzi w sposób probabilistyczny (zwłaszcza przy temperaturze > 0). W przypadku luźnych, ogólnych poleceń (jak w Rundzie 1) niedeterministyczność objawia się drastycznymi różnicami w wygenerowanym kodzie i formacie danych. Jednakże, jak wykazał eksperyment w Rundzie 4, zastosowanie techniki **few-shot prompting z trzema zróżnicowanymi przykładami** redukuje przestrzeń interpretacji modelu niemal do zera, skutecznie neutralizując wpływ stochastyczności generowania.

---

## 8. Własnoręczna obsługa błędu zapisu

W ramach Etapu 10 zaimplementowano odporność aplikacji na awarie podczas operacji wejścia-wyjścia:
1. **Lokalizacja operacji**: Metoda `SaveButton_Click` w [MainWindow.axaml.cs](file:///c:/Users/gorka.maksymilian/Documents/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G/notatnik/MainWindow.axaml.cs#L44-L71), wywołująca `File.WriteAllText(FilePath, updated, Encoding.UTF8)`.
2. **Dodanie kontrolki UI**: W [MainWindow.axaml](file:///c:/Users/gorka.maksymilian/Documents/3.2-notatkiiczteryrundyprosb-4ta-2-Maksymilian-G/notatnik/MainWindow.axaml#L16) dodano kontrolkę `TextBlock` o nazwie `StatusText`.
3. **Struktura zabezpieczenia**:
   ```csharp
   try
   {
       File.WriteAllText(FilePath, updated, Encoding.UTF8);
       _notes.Insert(0, newEntry);
       TitleTextBox.Text = string.Empty;
       ContentTextBox.Text = string.Empty;
       StatusText.Text = "Notatka została pomyślnie zapisana.";
   }
   catch (Exception ex)
   {
       StatusText.Text = $"Błąd zapisu: {ex.Message}";
   }
   ```

### Przebieg testów:
- **Przed dodaniem try/catch (plik tylko do odczytu)**: Próba zapisu do pliku z atrybutem `IsReadOnly = $true` powodowała nieobsłużony wyjątek `System.UnauthorizedAccessException: Odmowa dostępu do ścieżki` i natychmiastowe zamknięcie (awarię) aplikacji.
- **Po dodaniu try/catch (plik tylko do odczytu)**: Aplikacja nie uległa awarii. W kontrolce `StatusText` wyświetlił się czytelny komunikat: `Błąd zapisu: Odmowa dostępu do ścieżki...`.
- **Po zdjęciu atrybutu tylko do odczytu**: Operacja zapisu powiodła się, a kontrolka wyświetliła komunikat: `Notatka została pomyślnie zapisana.`.

### Dlaczego łapanie ogólnego `Exception` może być zbyt szerokie?
Przechwytywanie bazowej klasy `System.Exception` maskuje nie tylko spodziewane błędy wejścia/wyjścia (takie jak `IOException`, `UnauthorizedAccessException` czy `DirectoryNotFoundException`), ale również krytyczne błędy programistyczne (np. `NullReferenceException`, błędy alokacji pamięci `OutOfMemoryException`). W środowisku produkcyjnym dobrą praktyką jest selektywne przechwytywanie konkretnych wyjątków I/O, aby nie ukrywać usterek logicznych w kodzie.

---

## 9. Wynik testu pięciu notatek

Przeprowadzono sekwencyjny test zapisu 5 zróżnicowanych notatek testowych:
1. `Zakupy na weekend` (treść wielowierszowa: mleko, chleb, masło)
2. `Zażółć gęślą jaźń` (test polskich znaków diakrytycznych)
3. `Projekt Avalonia: architektura` (wielowierszowa treść techniczna)
4. `Spotkanie zespołu` (krótka notatka organizacyjna)
5. `Wydanie wersji 1.0 — Podsumowanie` (podsumowanie testu)

### Rzeczywista zawartość pliku `notatnik/notatki.md`:
```markdown
## 2026-10-29 08:31 — Wydanie wersji 1.0 — Podsumowanie

Wszystkie 5 notatek zapisane pomyślnie.
Najnowsza notatka znajduje się na samej górze.

## 2026-10-29 08:31 — Spotkanie zespołu

Omówienie wyników 4 rund promptowania.

## 2026-10-29 08:31 — Projekt Avalonia: architektura

Rozdzielenie widoku XAML od kodu C#.
Obsługa zdarzeń i kolekcja ObservableCollection.

## 2026-10-29 08:31 — Zażółć gęślą jaźń

Test polskich znaków: ą, ć, ę, ł, ń, ó, ś, ź, ż.

## 2026-10-29 08:31 — Zakupy na weekend

mleko
chleb
masło
```

### Wyniki weryfikacji:
- **Liczba wpisów**: Dokładnie 5 wpisów w pliku i 5 elementów załadowanych na listę w UI.
- **Kolejność**: Prawidłowa LIFO — notatka 5. znajduje się na szczycie pliku, a najstarsza notatka 1. na samym dole.
- **Bezpieczeństwo danych**: Żadna wcześniejsza notatka nie została utracona przy kolejnych zapisach.
- **Polskie znaki**: Bezbłędnie zachowane we wszystkich wpisach.

---

## 10. Dodatkowa rada z oficjalnej dokumentacji producenta modelu (Google Gemini)

- **Źródło**: Oficjalna dokumentacja Google AI for Developers — *Prompt design strategies*
- **Bezpośredni link**: [https://ai.google.dev/gemini-api/docs/prompting-strategies#consistent-formatting](https://ai.google.dev/gemini-api/docs/prompting-strategies#consistent-formatting)
- **Data odczytania źródła**: `2026-10-29`
- **Cytat / Zalecenie**:
  > *"Make sure that the structure and formatting of few-shot examples are the same to avoid responses with undesired formats. One of the primary objectives of adding few-shot examples in prompts is to show the model the response format. Therefore, it is essential to ensure a consistent format across all examples, especially paying attention to XML tags, white spaces, newlines, and example splitters."*
- **Wyjaśnienie wpływu na aplikację Notatnik**:
  Zalecenie to zwróciło uwagę na krytyczne znaczenie spójności białych znaków i separatorów (nowych linii) pomiędzy przykładami, co w aplikacji Notatnik pozwoliło jednoznacznie odróżnić pojedyncze przejście do nowej linii w treści notatki od podwójnej nowej linii stanowiącej separator kolejnych wpisów.
