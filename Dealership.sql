CREATE DATABASE Dealership
GO

USE Dealership
GO

CREATE TABLE Authorizations(
	ID_Authorizations INT PRIMARY KEY IDENTITY(1,1),
	LoginA VARCHAR(15) NOT NULL UNIQUE,
	PasswordA VARCHAR(15) NOT NULL,
);
GO

INSERT INTO Authorizations(LoginA, PasswordA)
VALUES
	('admin','admin'),
	('employee1','employee1'),
	('employee2','employee2'),
	('employee3','employee3'),
	('employee4','employee4'),
	('user1','user1'),
	('user2','user2'),
	('user3','user3'),
	('user4','user4'),
	('user5','user5')
GO

SELECT * FROM Authorizations

CREATE TABLE Clients(
  ID_Client INT PRIMARY KEY IDENTITY(1,1),
  ClientSurname VARCHAR(30) NOT NULL,
  ClientName VARCHAR(30) NOT NULL,
  ClientMiddlename VARCHAR(30) NOT NULL,
  PhoneNumber VARCHAR(12) NOT NULL UNIQUE,
  Authorizations_ID INT UNIQUE FOREIGN KEY REFERENCES Authorizations(ID_Authorizations)
);
GO

INSERT INTO Clients(ClientSurname, ClientName, ClientMiddlename, PhoneNumber, Authorizations_ID)
VALUES
  ('Филиппова', 'Ксения', 'Кирилловна', '+79153306716', 6),
  ('Никитина', 'Александра', 'Георгиевна', '+79062307090', 7),
  ('Бирюкова', 'Агата', 'Николаевна', '+79982218480', 8),
  ('Евдокимов', 'Михаил', 'Александрович', '+79895872017', 9),
  ('Фокина', 'Виктория', 'Платоновна', '+79275243220', 10)
GO

SELECT * FROM Clients

CREATE TABLE Positions(
  ID_Position INT PRIMARY KEY IDENTITY(1,1),
  NamePosition VARCHAR(25) NOT NULL UNIQUE,
  WorkSchedule VARCHAR(3) NOT NULL,
  Wages DECIMAL(10,2) NOT NULL
);
GO

INSERT INTO Positions(NamePosition, WorkSchedule, Wages)
VALUES
  ('Администратор','2/2','100000.50'),
  ('Продавец-консультант','5/2','70000.25'),
  ('Менеджер по сервису','6/1','120000.75'),
  ('Финансовый менеджер','5/2','90000.83'),
  ('Маркетолог','5/2','80000.92')
GO

SELECT * FROM Positions

CREATE TABLE Employees(
  ID_Employee INT PRIMARY KEY IDENTITY(1,1),
  EmployeeSurname VARCHAR(30) NOT NULL,
  EmployeeName VARCHAR(30) NOT NULL,
  EmployeeMiddlename VARCHAR(30) NOT NULL,
  PhoneNumber VARCHAR(12) NOT NULL UNIQUE,
  Position_ID INT FOREIGN KEY REFERENCES Positions(ID_Position),
  Authorizations_ID INT UNIQUE FOREIGN KEY REFERENCES Authorizations(ID_Authorizations)
);
GO

INSERT INTO Employees(EmployeeSurname, EmployeeName, EmployeeMiddlename,PhoneNumber, Position_ID, Authorizations_ID)
VALUES
  ('Новикова','Софья','Маратовна','+79904395562', 1, 1),
  ('Архипов','Тимофей','Александрович','+79565604238', 2, 2),
  ('Дементьев','Герман','Кириллович','+79581205310', 3, 3),
  ('Орехов','Борис','Георгиевич','+79733517379', 4, 4),
  ('Голубева','Анна','Руслановна','+79804858615', 5, 5)
GO

SELECT * FROM Employees

CREATE TABLE StatusOrders(
  ID_StatusOrder INT PRIMARY KEY IDENTITY(1,1),
  TypeStatus VARCHAR(25) NOT NULL UNIQUE
);
GO

INSERT INTO StatusOrders(TypeStatus)
VALUES
  ('Новый заказ'),
  ('В обработке'),
  ('Подтвержден'),
  ('В процессе выполнения'),
  ('Завершен'),
  ('Возврат'),
  ('Отменен')
GO

SELECT * FROM StatusOrders

CREATE TABLE Dealership(
  ID_Dealership INT PRIMARY KEY IDENTITY(1,1),
  DealershipName VARCHAR(30) NOT NULL UNIQUE,
  Adress VARCHAR(100) NOT NULL UNIQUE
);
GO

INSERT INTO Dealership(DealershipName, Adress)
VALUES
  ('Автосалон BMW','Малая Бронная ул., 26, Москва, 123892'),
  ('Автосалон Mercedes','Саринский пр-д, 70, Москва, 170882'),
  ('Автосалон Dodge','Чистый пер., 98, Москва, 148594'),
  ('Автосалон Audi','Хрущевский пер., 71, Москва, 183921'),
  ('Автосалон Lamborghini','Овчинниковская наб., 40 стр1, Москва, 195922')
GO

SELECT * FROM Dealership

CREATE TABLE Brands(
	ID_Brand INT PRIMARY KEY IDENTITY(1,1),
	BrandName VARCHAR(15) NOT NULL UNIQUE
);
GO

INSERT INTO Brands(BrandName)
VALUES
	('BMW'),
	('Mercedes'),
	('Dodge'),
	('Audi'),
	('Lamborghini')
GO

SELECT * FROM Brands

CREATE TABLE Models(
	ID_Model INT PRIMARY KEY IDENTITY(1,1),
	ModelName VARCHAR(15) NOT NULL UNIQUE
);
GO

INSERT INTO Models(ModelName)
VALUES
	('M5 F90'),
	('CLS 63 AMG'),
	('Hellcat'),
	('RS6'),
	('Huracan')
GO

SELECT * FROM Models

CREATE TABLE Specifications(
	ID_Specification INT PRIMARY KEY IDENTITY(1,1),
	EnginePower VARCHAR(10) NOT NULL,
	EngineCapacity VARCHAR(5) NOT NULL,
	MachineDrive VARCHAR(10) NOT NULL
);
GO

INSERT INTO Specifications(EnginePower, EngineCapacity, MachineDrive)
VALUES
	('600 л. с.','4.4 л','AWD'),
	('585 л. с.','5.5 л','AWD'),
	('717 л. с.','6.2 л','RWD'),
	('600 л. с.','4.0 л','AWD'),
	('640 л. с.','5.2 л','AWD')
GO

SELECT * FROM Specifications

CREATE TABLE Cars(
	ID_Car INT PRIMARY KEY IDENTITY(1,1),
	Brand_ID INT UNIQUE FOREIGN KEY REFERENCES Brands(ID_Brand),
	Model_ID INT UNIQUE FOREIGN KEY REFERENCES Models(ID_Model),
	Specification_ID INT FOREIGN KEY REFERENCES Specifications(ID_Specification),
	YearRelease VARCHAR(4) NOT NULL,
	Price DECIMAL(10,2) NOT NULL
);
GO

INSERT INTO Cars(Brand_ID, Model_ID, Specification_ID, YearRelease, Price)
VALUES
	( 1, 1, 1,'2018','7000000.00'),
	( 2, 2, 2,'2014','5200000.00'),
	( 3, 3, 3,'2023','18298269.00'),
	( 4, 4, 4,'2023','22750000.00'),
	( 5, 5, 5,'2020','29000000.00')
GO

SELECT * FROM Cars

CREATE TABLE DealershipCars(
	ID_DealershipCar INT PRIMARY KEY IDENTITY(1,1),
	Dealership_ID INT UNIQUE FOREIGN KEY REFERENCES Dealership(ID_Dealership),
	Car_ID INT UNIQUE FOREIGN KEY REFERENCES Cars(ID_Car)
);
GO

INSERT INTO DealershipCars(Dealership_ID, Car_ID)
VALUES
	( 1, 1),
	( 2, 2),
	( 3, 3),
	( 4, 4),
	( 5, 5)
GO

SELECT * FROM DealershipCars

CREATE TABLE Orders(
  ID_Order INT PRIMARY KEY IDENTITY(1,1),
  DealershipCar_ID INT FOREIGN KEY REFERENCES DealershipCars(ID_DealershipCar),
  Amount INT NOT NULL,
  StatusOrder_ID INT FOREIGN KEY REFERENCES StatusOrders(ID_StatusOrder),
  DateOrder VARCHAR(10) NOT NULL,
);
GO

INSERT INTO Orders(DealershipCar_ID, Amount, StatusOrder_ID, DateOrder)
VALUES
	( 1, 1, 5, '20.10.2022'),
	( 2, 1, 5, '17.10.2023'),
	( 3, 1, 5, '01.01.2024'),
	( 4, 1, 5, '22.02.2023'),
	( 5, 1, 5, '15.03.2024')
GO

SELECT * FROM Orders

CREATE TABLE OrdersChecks(
	ID_OrderCheck INT PRIMARY KEY IDENTITY(1,1),
	Client_ID INT FOREIGN KEY REFERENCES Clients(ID_Client),
	Employee_ID INT FOREIGN KEY REFERENCES Employees(ID_Employee),
	Order_ID INT FOREIGN KEY REFERENCES Orders(ID_Order),
	Price DECIMAL(10,2) NOT NULL
);
GO

INSERT INTO OrdersChecks(Client_ID, Employee_ID, Order_ID, Price)
VALUES
	( 1, 1, 1, '7000000.00'),
	( 2, 2, 2, '5200000.00'),
	( 3, 3, 3, '18298269.00'),
	( 4, 4, 4, '22750000.00'),
	( 5, 5, 5, '29000000.00')
GO

SELECT * FROM OrdersChecks

CREATE PROCEDURE Backup_Dealership
AS
BEGIN
    DECLARE @BackupPath NVARCHAR(500);
    SET @BackupPath = 'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\Backup\Dealership' + '.bak';
    
    BACKUP DATABASE Dealership TO DISK = @BackupPath;
END;
GO

EXEC Backup_Dealership;

SELECT --Клиенты--
	Clients.ID_Client,
	Clients.ClientSurname,
	Clients.ClientName,
	Clients.ClientMiddlename,
	Clients.PhoneNumber,
	Authorizations.LoginA
FROM Authorizations
INNER JOIN Clients ON Clients.Authorizations_ID = Authorizations.ID_Authorizations;

SELECT --Сотрудники--
    Employees.ID_Employee,
    Employees.EmployeeSurname,
    Employees.EmployeeName,
    Employees.EmployeeMiddlename,
    Employees.PhoneNumber,
    Positions.NamePosition,
    Authorizations.LoginA
FROM Employees
INNER JOIN Positions ON Employees.Position_ID = Positions.ID_Position
INNER JOIN Authorizations ON Employees.Authorizations_ID = Authorizations.ID_Authorizations;

SELECT --Машина--
    Cars.ID_Car,
    Brands.BrandName,
    Models.ModelName,
    Specifications.EnginePower,
    Specifications.EngineCapacity,
    Specifications.MachineDrive,
    Cars.YearRelease,
    Cars.Price
FROM Cars
INNER JOIN Brands ON Cars.Brand_ID = Brands.ID_Brand
INNER JOIN Models ON Cars.Model_ID = Models.ID_Model
INNER JOIN Specifications ON Cars.Specification_ID = Specifications.ID_Specification;

SELECT  --Чек--
    OC.ID_OrderCheck,
    C.ClientSurname,
    E.EmployeeSurname,
    D.DealershipName,
    B.BrandName,
    O.Amount,
    O.DateOrder,
	CA.Price
FROM OrdersChecks AS OC
INNER JOIN Clients AS C ON OC.Client_ID = C.ID_Client
INNER JOIN Employees AS E ON OC.Employee_ID = E.ID_Employee
INNER JOIN DealershipCars AS DC ON OC.Order_ID = DC.ID_DealershipCar
INNER JOIN Dealership AS D ON DC.Dealership_ID = D.ID_Dealership
INNER JOIN Cars AS CA ON DC.Car_ID = CA.ID_Car
INNER JOIN Brands AS B ON CA.Brand_ID = B.ID_Brand
INNER JOIN Orders AS O ON OC.Order_ID = O.ID_Order;


SELECT -- Автосалон машина
    DealershipCars.ID_DealershipCar,
    Dealership.DealershipName,
    Brands.BrandName
FROM DealershipCars
INNER JOIN Dealership ON DealershipCars.Dealership_ID = Dealership.ID_Dealership
INNER JOIN Brands ON DealershipCars.Car_ID = Brands.ID_Brand;

SELECT --Заказы--
    Orders.ID_Order,
    Dealership.DealershipName,
    Brands.BrandName,
    Orders.Amount,
    StatusOrders.TypeStatus,
    Orders.DateOrder
FROM Orders
INNER JOIN DealershipCars ON Orders.DealershipCar_ID = DealershipCars.ID_DealershipCar
INNER JOIN Dealership ON DealershipCars.Dealership_ID = Dealership.ID_Dealership
INNER JOIN Cars ON DealershipCars.Car_ID = Cars.ID_Car
INNER JOIN Brands ON Cars.Brand_ID = Brands.ID_Brand
INNER JOIN StatusOrders ON Orders.StatusOrder_ID = StatusOrders.ID_StatusOrder

use master
go

drop database Dealership