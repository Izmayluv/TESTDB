use Trade
go

create table Category
(
 categoryID int primary key identity,
 categoryName nvarchar(max) not null
);
go
create table Supplier
(
	supplierID int primary key identity,
	supplierName nvarchar(max) not null
);
go
create table Producer
(
	producerID int primary key identity,
	producerName nvarchar(max) not null
);
go
create table Photo
(
	photoID nvarchar(100) primary key,
	photoName nvarchar(max) not null,
	photoImage image not null
);
go
create table Product
(
	productArticleNumber nvarchar(100) primary key not null,
	productName nvarchar(max) not null,
	productDescription nvarchar(max) null,
	productUnit nvarchar(max) null,
	productCost decimal(19,4) not null,
	productDiscountAmount tinyint null,
	productMaxDiscountAmount tinyint null,
	productQuantityInStock int not null,
	productPhoto nvarchar(100) foreign key references Photo(photoID) not null,
	productCategory int foreign key references Category(categoryID) not null,
	productProducer int foreign key references Producer(producerID) not null,
	productSupplier int foreign key references Supplier(supplierID) not null,
	productStatus bit null,
);

