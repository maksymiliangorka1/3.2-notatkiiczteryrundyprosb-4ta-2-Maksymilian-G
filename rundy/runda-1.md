# Runda 1 — Minimalny prompt

## 1. Treść promptu
> „Zrób notatnik: pole na tytuł, pole na treść, przycisk zapisu i lista zapisanych notatek. Notatki mają być zapisywane do pliku.”

## 2. Status kompilacji i działania
- **Kompilacja**: Sukces (0 błędów, 0 ostrzeżeń).
- **Działanie**: Aplikacja uruchamia się, przyjmuje dane wejściowe z pól tekstowych, zapisuje je na dysku po kliknięciu przycisku i aktualizuje listę zapisanych notatek.

## 3. Powstałe pliki wyjściowe
- Powstał plik: `notatnik/notatki.txt` (w katalogu roboczym aplikacji).

## 4. Ustalone kodowanie
- **Kodowanie**: UTF-8 bez sygnatury BOM.
- **Weryfikacja binarna**: Pierwsze 4 bajty pliku to `84 121 116 117` (znaki ASCII `Tytu`). Brak sekwencji BOM `EF BB BF`. Polskie znaki diakrytyczne (`ą, ć, ę, ł, ń, ó, ś, ź, ż`) zostały zakodowane wielobajtowo zgodnie ze standardem UTF-8.

## 5. Rzeczywisty wynik w pliku wyjściowym (`notatnik/notatki.txt`)
```text
Tytuł: Zakupy
Treść: mleko
chleb
---
Tytuł: Zażółć gęślą jaźń
Treść: Test zapisu polskich znaków: ą, ć, ę, ł, ń, ó, ś, ź, ż.
---
```

## 6. Kolejność wpisów
- **Kolejność**: Chronologiczna (najstarszy wpis na początku, najnowszy na końcu pliku – standardowe dopisywanie `File.AppendAllText`).

## 7. Decyzje formatu podjęte samodzielnie przez agenta
1. **Nazwa i rozszerzenie pliku**: Agent wybrał plik `notatki.txt` (prompt nie określał nazwy pliku ani rozszerzenia).
2. **Struktura wpisu**: Zastosowano etykiety `Tytuł: ...` oraz `Treść: ...` zakończone separatorem `---`.
3. **Brak daty/godziny**: Wpisy nie zawierają znacznika czasu, ponieważ prośba o tym nie wspominała.
4. **Kolejność zapisu**: Zastosowano tryb append (dopisywanie na koniec), co powoduje, że najnowsze wpisy lądują na dole.
5. **Prezentacja listy w UI**: Odczyt pliku i parsowanie po separatorze `---` w celu wyświetlenia w `ListBox`.
