# Runda 5 — Meta-prompt

## 1. Analiza pierwotnego promptu z Rundy 1

### Treść pierwotnego promptu
> „Zrób notatnik: pole na tytuł, pole na treść, przycisk zapisu i lista zapisanych notatek. Notatki mają być zapisywane do pliku.”

### Czego brakuje w pierwotnej prośbie, aby wynik był w 100% jednoznaczny?
1. **Identyfikacja i lokalizacja pliku**:
   - Brak nazwy pliku i rozszerzenia (czy `notes.txt`, `notatki.md`, czy `data.json`?).
   - Brak jednoznacznej ścieżki (katalog projektu, katalog roboczy, folder użytkownika AppData?).
   - Brak określenia, czy każda notatka tworzy osobny plik, czy wszystkie trafiają do jednego zbiorczego pliku.
2. **Format i struktura danych**:
   - Brak struktury wpisu (surowy tekst, Markdown, JSON, klucz-wartość).
   - Brak określenia, czy dodawać znacznik czasu (data/godzina utworzenia) i w jakim formacie.
   - Brak definicji separatorów między wpisami (pusta linia, kreski `---`).
3. **Kolejność i strategia zapisu**:
   - Brak wskazania porządku wpisów (chronologiczny FIFO z dopisywaniem na końcu pliku vs odwrotny LIFO z najnowszym wpisem na szczycie).
   - Ryzyko nadpisania całego pliku zamiast bezpiecznego dołączania.
4. **Kodowanie znaków**:
   - Brak jawnego wymogu UTF-8, co w środowisku Windows może prowadzić do użycia kodowania ANSI (np. Windows-1250) i utraty znaków na innych systemach.
5. **Zachowanie interfejsu (UX/UI)**:
   - Brak specyfikacji, co dzieje się po zapisie (czyszczenie pól, komunikat sukcesu/błędu).
   - Brak określenia, czy lista ma wczytywać istniejące notatki przy uruchomieniu aplikacji.
   - Brak określenia, co ma być prezentowane na liście (sam tytuł, data z tytułem, czy cała notatka).
6. **Technologia**:
   - W oderwaniu od repozytorium brakowało wskazania platformy (.NET / Avalonia).

---

## 2. Propozycja poprawionego promptu (wynik meta-promptu)

```text
Zbuduj aplikację Notatnik w C# / Avalonia UI.

Interfejs ma zawierać:
- Pole tekstowe na tytuł notatki (TextBox).
- Wielowierszowe pole tekstowe na treść notatki (TextBox z AcceptsReturn="True").
- Przycisk zapisu (Button „Zapisz”).
- Listę zapisanych notatek (ListBox), wczytującą istniejące notatki przy starcie programu.

Zasady zapisu do pliku:
- Wszystkie notatki zapisuj do jednego pliku „notatki.md” zlokalizowanego w folderze projektu.
- Kodowanie pliku: UTF-8 (z poprawną obsługą polskich znaków diakrytycznych).
- Kolejność wpisów: najnowsza notatka ma znajdować się na samej górze pliku.
- Format pojedynczego wpisu:
  ## RRRR-MM-DD GG:MM — [Tytuł]
  [Treść notatki]
- Pomiędzy kolejnymi wpisami notatek w pliku ma znajdować się dokładnie jedna pusta linia.
- Po udanym zapisie wyczyść pola tytułu i treści oraz zaktualizuj listę w interfejsie.
```

---

## 3. Rzeczywiste wykonanie poprawionego promptu

### Weryfikacja pod kątem zbędnych wymagań:
Propozycja modelu:
- NIE zawiera zewnętrznych bibliotek (brak zbędnych pakietów NuGet).
- NIE dodaje niepotrzebnych funkcji (np. usuwania notatek, wyszukiwarki, eksportu do PDF czy chmury).
- Skupia się precyzyjnie na usunięciu niejednoznaczności formatu, ścieżki i kodowania.

### Status kompilacji i działania:
- Kompilacja: Sukces (0 błędów, 0 ostrzeżeń).
- Zapis dwóch notatek testowych:
  1. `Zakupy` (treść: `mleko\nchleb`)
  2. `Zażółć gęślą jaźń` (treść: `Test zapisu polskich znaków: ą, ć, ę, ł, ń, ó, ś, ź, ż.`)

### Rzeczywista zawartość pliku `notatnik/notatki.md`:
```markdown
## 2026-10-29 08:24 — Zażółć gęślą jaźń

Test zapisu polskich znaków: ą, ć, ę, ł, ń, ó, ś, ź, ż.

## 2026-10-29 08:24 — Zakupy

mleko
chleb
```

---

## 4. Porównanie Rundy 5 z Rundą 2

| Cecha | Runda 2 (format opisany słowami) | Runda 5 (meta-prompt) |
|---|---|---|
| **Format nagłówka** | `2026-09-08 14:32 — Tytuł` (zwykły tekst) | `## 2026-10-29 08:24 — Tytuł` (nagłówek Markdown H2) |
| **Pojemność specyfikacji** | Określiła format wpisu i kolejność | Określiła format, zachowanie UI (czyszczenie pól, reload) oraz kodowanie |
| **Ryzyko nadmiarowości** | Brak | Niskie — model precyzyjnie zdefiniował brakujące reguły techniczne bez dodawania bloatware'u |
| **Czy meta-prompt pomógł?** | Wymagał ręcznego formułowania ograniczeń przez człowieka | Model sam zidentyfikował luki specyfikacji i przygotował formalną, precyzyjną instrukcję |

### Wniosek:
Meta-prompt okazał się wysoce skuteczny. Pozwolił przekształcić naiwny, skrajnie niejednoznaczny prompt z Rundy 1 w precyzyjną, profesjonalną specyfikację inżynierską, nie powodując przy tym niepożądanego „scope creep” (rozszerzenia zakresu o zbędne biblioteki czy funkcje).
