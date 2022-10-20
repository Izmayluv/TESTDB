create database Trade
go
use Trade
go
create table StaffRole
(
	RoleID int primary key identity,
	RoleName nvarchar(100) not null
)
go
create table Customer
(
	userID int primary key identity,
	userSurname nvarchar(100) not null,
	userName nvarchar(100) not null,
	userPatronymic nvarchar(100) not null,
	userLogin nvarchar(max) not null,
	userPassword nvarchar(max) not null,
	userRole int foreign key references Role(RoleID) not null
)
