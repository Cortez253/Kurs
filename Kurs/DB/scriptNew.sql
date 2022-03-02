
create table Clients 
(
	Id_client int not null identity constraint PK_clients primary key,
	[Surname] nvarchar(50) not null,
	[Name] nvarchar(50) not null,
	Telephone nvarchar(12) not null,
	[E-mail] varchar(max)
)

create table Avto 
(
	Id_avto int not null identity constraint PK_avto primary key,
	Car_brand nvarchar(50) not null,
	Model nvarchar(50) not null,
)

create table [Services]
(
	Id_service int not null identity constraint PK_service primary key,
	Name_service nvarchar(30) not null,													
	Price money not null
)

create table Orders
(
	Id_order int not null identity constraint PK_order primary key,
	Id_client int not null constraint FK_order1 foreign key references Clients(Id_client),
	Id_avto int not null constraint FK_order2 foreign key references Avto(Id_avto),
	Id_service int not null constraint FK_order3 foreign key references [Services](Id_service),
	Date_order date not null,
	Period_of_execution nvarchar(20) not null,		/* Срок выполнения */
	Order_status varchar(30) not null,				/* Статус заказа. Возможные значения: выполняется, завершен */
)

create table Users
(
	Id_user int not null identity constraint PK_user primary key,
	[Login] varchar(30) not null,
	[Password] varchar(30) not null,
	[Access_rights] varchar(10) not null
)