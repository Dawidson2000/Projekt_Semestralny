# Projekt_Semestralny

## INICJALIZACJA BAZY DANYCH:

* W folderze z projektem znajduje sie plik o nazwie **KinoRezerwacjeQuery.sql**
* Zawiera kompletną baze danych wraz z danymi
* Ten plik nalezy uruchomić w Visual Studio w SQL Server Object Explorer, a dokładniej w localDB

## OPIS PROGRAMU
* Program składa sie z 3 głownych okien i jednego informacyjnego

### Pierwsze okno
* Znajdują się na nim dwa przyciski
* Pierwszy z nich **Rezerwuj** otwiera nowe okno rezerwacji
* Drugi: **Anuluj Rezerwację** otiwera okno jest rezerwacji

### Okno rezerwacji
* Najpierw z rozwijanego comboboxa wybieramy interesujący nas film
* Następnie wyświetla się lista dostepnych seansów i dokonujemy wyboru
* Kolejnym krokiem jest wybranie miejsc w sali
* Z lewej strony znajduja się pola tekstowe: **Imie**, **Nawisko** oraz **Nr telefonu**
* Ostatnim etapem jest wciśnięcie przycisku: **Rezerwuj**
* Jeżeli wszystkie dane sa poprawne rezerwacja zostanie dokonana, użytkownik dostanie informację zwrotną z numerem dokonanej rezerwacji

### Okno anulowania rezerwacji
* Aby anulować rezerwację należy podac wcześniej pokazany nr rezerwacji oraz skojarzony z nią numer telefonu
* Jeżeli dane są poprawne użytkownik dostanie informację zwrotną, że wszystko sie powiodło, w innym przypadku informację co jest nie tak
