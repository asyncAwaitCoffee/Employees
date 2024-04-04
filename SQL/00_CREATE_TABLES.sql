CREATE SCHEMA DEPO;
GO

CREATE TABLE DEPO.EMPLOYEES
(
	EMPLOYEE_ID INT IDENTITY(1,1) PRIMARY KEY,
	EMPLOYEE_NAME NVARCHAR(50) NOT NULL,
	SURNAME NVARCHAR(50) NOT NULL,
	PATRONYMIC NVARCHAR(50) NULL,
	BIRTHDATE DATE NOT NULL,
	PASSPORT_SERIES NVARCHAR(50) NOT NULL,
	PASSPORT_NUMBER NVARCHAR(50) NOT NULL,
	COMPANY_ID INT NULL,

	CONSTRAINT CS_SERIES_NUMBER UNIQUE(PASSPORT_SERIES, PASSPORT_NUMBER)
)
GO

CREATE TABLE DEPO.COMPANIES
(
	COMPANY_ID INT IDENTITY(1,1) PRIMARY KEY,
	COMPANY_NAME NVARCHAR(50) NOT NULL,
	INN NVARCHAR(50) UNIQUE NOT NULL,
	LEGAL_ADRESS NVARCHAR(50) NOT NULL,
	FACT_ADRESS NVARCHAR(50) NOT NULL
)
GO

CREATE TABLE DEPO.ERROR_LOG
(
	ERROR_DATE DATETIME DEFAULT GETDATE(),
	CONTEXT NVARCHAR(100),
	PARAMS NVARCHAR(100),
	ERROR_TEXT NVARCHAR(400)
)

CREATE TABLE DEPO.TECH_DATA
(
	TECH_ID INT IDENTITY(1,1) PRIMARY KEY,
	TECH_NAME NVARCHAR(100),
	TECH_DATA NVARCHAR(100)
)