<<<<<<< HEAD
create database CompanyDB
use CompanyDB 


create table Countries(
Id int primary key identity,
Name nvarchar (20),
)


create table Cityes(
Id int primary key identity,
Name nvarchar (20),
CountriId INT FOREIGN KEY REFERENCES Countries(Id)
)


create table Employees(
Id int primary key identity,
Name nvarchar (20) ,
Surname nvarchar (20) ,
Age int ,
Salary decimal(10,2),
Position nvarchar(50),
CityId INT FOREIGN KEY REFERENCES Cityes(Id),
IsDeleted bit
)



=======
create database CompanyDB
use CompanyDB 


create table Countries(
Id int primary key identity,
Name nvarchar (20),
)


create table Cityes(
Id int primary key identity,
Name nvarchar (20),
CountriId INT FOREIGN KEY REFERENCES Countries(Id)
)


create table Employees(
Id int primary key identity,
Name nvarchar (20) ,
Surname nvarchar (20) ,
Age int ,
Salary decimal(10,2),
Position nvarchar(50),
CityId INT FOREIGN KEY REFERENCES Cityes(Id),
IsDeleted bit
)



>>>>>>> c14c9742bc1e765dbec0ffc6e8d063c69789b72c
