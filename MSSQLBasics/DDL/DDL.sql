create database TripService
use TripService

create table Cities(
Id int primary key identity,
Name nvarchar(20) not null,
CountryCode char(2) not null
)

create table Hotels(
Id int primary key identity,
Name nvarchar(30) not null,
CityId int foreign key references Cities(Id) not null,
EmployeeCount int not null,
BaseRate decimal(15,2)
)

create table Rooms(
Id int primary key identity,
Price decimal(15,2) not null,
Type nvarchar(20) not null,
Beds int not null,
HotelId int foreign key references Hotels(Id) not null
)

create table Trips(
Id int primary key identity,
RoomId int foreign key references Rooms(Id) not null,
BookDate date not null,
ArrivalDate date not null,
ReturnDate date not null,
CancelDate date,

constraint ch_BookDate check (BookDate < ArrivalDate),
constraint ch_ArrivalDate check (ArrivalDate < ReturnDate)
)

create table Accounts(
Id int primary key identity,
FirstName nvarchar(50) not null,
MiddleName nvarchar(20),
LastName nvarchar(50) not null,
CityId int foreign key references Cities(Id) not null,
BirthDate date not null,
Email nvarchar(100) not null unique
)

create table AccountsTrips(
AccountId int foreign key references Accounts(Id) not null,
TripId int foreign key references Trips(Id) not null,
Luggage int not null,

constraint pk_AccountIdTripId primary key(AccountId,TripId),
constraint ch_Luggage check (Luggage >= 0)
)


