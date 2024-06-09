use master
create database HMS_ADO
go
--Table 01
use HMS_ADO
create table Users
(
UserID int primary key identity,
Username varchar(30) not null,
Password varchar(40) not null
)

select * from Users
select * from Patients

insert into Users values ('Admin', 'admin123'),('Accounts', 'accounts123'),('Reciptionist', 'reciptionist123')

---------------------------------------------------
use HMS_ADO
create table PatientInfo
(
PatientID int primary key identity (100,1),
Name varchar(30),
Mobile varchar(20),
Age int not null,
Address varchar(100) 
)
select * from PatientInfo

---------------------------------------------------
use HMS_ADO
create table TestList
(
TestID int primary key identity (101,1),
TestName varchar(30)
)
select * from TestList

select TestName from TestList

insert into TestList values ('X-Ray'),('CT'),('RBC')
---------------------------------------------------
use HMS_ADO
create table Invoice
(
invID int primary key identity (1000,1),
invoiceID int,
invDate datetime,
PatientID int foreign key references PatientInfo(PatientID),
TestID int foreign key references TestList(TestID)
)
select * from Invoice

---------------------------------------------------

select * from Invoice where PatientID=104

SELECT
    i.invoiceID,
    p.PatientID,
    p.Name,
    p.Mobile,
    tl.TestName,
    i.invDate AS Date
FROM
    Invoice i
INNER JOIN PatientInfo p ON i.PatientID = p.PatientID
INNER JOIN TestList tl ON i.TestID = tl.TestID
WHERE
    i.invoiceID = 3;

SELECT
    *
FROM
    Invoice i
INNER JOIN PatientInfo p ON i.PatientID = p.PatientID
INNER JOIN TestList tl ON i.TestID = tl.TestID
WHERE
    i.invoiceID = 3;	

