create database KinoRezerwacje
go
use KinoRezerwacje
go
create table sale
(
id_sali int identity(1,1) primary key,
ilosc_miejsc tinyint check(ilosc_miejsc between 30 and 40) not null,
)
go
create table filmy
(
id_filmu int identity(1,1) primary key,
tytul nvarchar(150) not null,
nazwisko_rezysera nvarchar(50) not null,
czas_trwania_minuty int not null,
jezyk nvarchar(9) not null default 'polski' check(jezyk in('polski','angielski','niemiecki')),
)
go
create table seanse
(
id_seansu int identity(1,1) primary key,
id_filmu int not null,
id_sali int not null,
czas_rozpoczecia datetime not null check(czas_rozpoczecia>=getdate()),
CONSTRAINT seanse_filmy FOREIGN KEY(id_filmu)
REFERENCES filmy(id_filmu) on delete no action,
CONSTRAINT seanse_sale FOREIGN KEY(id_sali)
REFERENCES sale(id_sali) on delete no action,
)
go
create table rezerwacje
(
id_rezerwacji int identity(1,1) primary key,
id_seansu int not null,
typ_rezerwacji nvarchar(11) not null check(typ_rezerwacji in('internetowa','stacjonarna')),
imie_klienta nvarchar(50) not null,
nr_telefonu nvarchar(9) not null,
nazwisko_klienta nvarchar(50) not null,
czy_oplacone bit default 0 not null,
data_dokonania_rezerwacji date not null check(data_dokonania_rezerwacji<=getdate()),
CONSTRAINT rezerwacje_seanse FOREIGN KEY(id_seansu)
REFERENCES seanse(id_seansu) on delete no action,
)
go
create table miejsca
(
id_miejsca int identity(1,1) primary key,
rzad tinyint check(rzad between 1 and 8) not null,
numer_miejsca tinyint check(numer_miejsca between 1 and 10) not null,
id_sali int not null,
CONSTRAINT miejsca_sale FOREIGN KEY(id_sali)
REFERENCES sale(id_sali) on delete no action,
)
go
create table zarezerwowane_miejsca
(
id_zar_miej int identity(1,1) primary key,
id_miejsca int not null,
id_rezerwacji int not null,
id_seansu int not null,
CONSTRAINT zarez_miejsca FOREIGN KEY(id_miejsca)
REFERENCES miejsca(id_miejsca) on delete no action,
CONSTRAINT zarez_rezerwacje FOREIGN KEY(id_rezerwacji)
REFERENCES rezerwacje(id_rezerwacji) on delete no action,
CONSTRAINT zarez_seanse FOREIGN KEY(id_seansu)
REFERENCES seanse(id_seansu) on delete no action,
)
go
insert into sale(ilosc_miejsc) values (30)
insert into sale(ilosc_miejsc) values (40)
go
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 1, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 2, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 3, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 4, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 5, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 6, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 7, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 8, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 9, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 10, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 1, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 2, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 3, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 4, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 5, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 6, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 7, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 8, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 9, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 10, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 1, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 2, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 3, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 4, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 5, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 6, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 7, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 8, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 9, 1)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 10, 1)

insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 1, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 2, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 3, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 4, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 5, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 6, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 7, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 8, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 9, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (1, 10, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 1, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 2, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 3, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 4, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 5, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 6, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 7, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 8, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 9, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (2, 10, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 1, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 2, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 3, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 4, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 5, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 6, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 7, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 8, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 9, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (3, 10, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 1, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 2, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 3, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 4, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 5, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 6, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 7, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 8, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 9, 2)
insert into miejsca(rzad, numer_miejsca, id_sali) values (4, 10, 2)

go
insert into filmy(tytul, nazwisko_rezysera, czas_trwania_minuty) values ('Titanic', 'Cameron', 195)
insert into filmy(tytul, nazwisko_rezysera, czas_trwania_minuty, jezyk) values ('Avatar', 'Cameron', 162, 'niemiecki')
insert into filmy(tytul, nazwisko_rezysera, czas_trwania_minuty, jezyk) values ('Gwiezdne wojny: Skywalker. Odrodzenie', 'Abrams', 144, 'angielski')
insert into filmy(tytul, nazwisko_rezysera, czas_trwania_minuty) values ('Spectre', 'Mendes', 148)
go
insert into seanse(czas_rozpoczecia, id_filmu, id_sali) values ('2021-03-15 19:00:00.000', 1 ,1)
insert into seanse(czas_rozpoczecia, id_filmu, id_sali) values ('2021-03-15 19:00:00.000', 1 ,2)
insert into seanse(czas_rozpoczecia, id_filmu, id_sali) values ('2021-03-16 20:00:00.000', 2 ,2)
insert into seanse(czas_rozpoczecia, id_filmu, id_sali) values ('2021-03-17 19:00:00.000', 2 ,1)
insert into seanse(czas_rozpoczecia, id_filmu, id_sali) values ('2021-03-18 19:00:00.000', 3 ,1)
insert into seanse(czas_rozpoczecia, id_filmu, id_sali) values ('2021-03-18 23:00:00.000', 3 ,1)
insert into seanse(czas_rozpoczecia, id_filmu, id_sali) values ('2021-03-19 19:00:00.000', 4 ,2)
insert into seanse(czas_rozpoczecia, id_filmu, id_sali) values ('2021-03-20 20:00:00.000', 4 ,2)
