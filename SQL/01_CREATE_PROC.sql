CREATE OR ALTER PROCEDURE DEPO.EMPLOYEES_ADD
(
	@EMPLOYEE_NAME NVARCHAR(50),
	@SURNAME NVARCHAR(50),
	@PATRONYMIC NVARCHAR(50),
	@BIRTHDATE DATE,
	@PASSPORT_SERIES NVARCHAR(50),
	@PASSPORT_NUMBER NVARCHAR(50),
	@COMPANY_ID INT = NULL
)
AS BEGIN
	INSERT INTO DEPO.EMPLOYEES(
		EMPLOYEE_NAME,
		SURNAME,
		PATRONYMIC,
		BIRTHDATE,
		PASSPORT_SERIES,
		PASSPORT_NUMBER,
		COMPANY_ID)
VALUES (@EMPLOYEE_NAME, @SURNAME, @PATRONYMIC, @BIRTHDATE, @PASSPORT_SERIES, @PASSPORT_NUMBER, @COMPANY_ID)
END
GO

CREATE OR ALTER PROCEDURE DEPO.COMPANIES_ADD
(
	@COMPANY_NAME NVARCHAR(50),
	@INN NVARCHAR(50),
	@LEGAL_ADRESS NVARCHAR(50),
	@FACT_ADRESS NVARCHAR(50)
)
AS BEGIN
	INSERT INTO DEPO.COMPANIES(
		COMPANY_NAME,
		INN,
		LEGAL_ADRESS,
		FACT_ADRESS)
VALUES (@COMPANY_NAME, @INN, @LEGAL_ADRESS, @FACT_ADRESS)
END
GO

CREATE OR ALTER PROCEDURE DEPO.COMPANIES_GET_ALL
AS BEGIN
	SELECT
		COMPANY_ID,
		COMPANY_NAME,
		INN,
		LEGAL_ADRESS,
		FACT_ADRESS
	FROM DEPO.COMPANIES
END
GO

CREATE OR ALTER PROCEDURE DEPO.COMPANIES_GET_BY_ID
(
	@COMPANY_ID INT
)
AS BEGIN
	SELECT
		COMPANY_NAME,
		INN,
		LEGAL_ADRESS,
		FACT_ADRESS
	FROM DEPO.COMPANIES
	WHERE COMPANY_ID = @COMPANY_ID
END
GO

CREATE OR ALTER PROCEDURE DEPO.EMPLOYEES_GET_BY_COMPANY
(
	@COMPANY_ID INT
)
AS BEGIN
	SELECT
		EMPLOYEE_ID,
		EMPLOYEE_NAME,
		SURNAME,
		PATRONYMIC,
		BIRTHDATE,
		PASSPORT_SERIES,
		PASSPORT_NUMBER
	FROM DEPO.EMPLOYEES
	WHERE ISNULL(COMPANY_ID, 0) = @COMPANY_ID
END
GO

CREATE OR ALTER PROCEDURE DEPO.EMPLOYEES_GET_BY_ID
(
	@EMPLOYEE_ID INT
)
AS BEGIN
	SELECT
		EMPLOYEE_NAME,
		SURNAME,
		PATRONYMIC,
		BIRTHDATE,
		PASSPORT_SERIES,
		PASSPORT_NUMBER
	FROM DEPO.EMPLOYEES
	WHERE EMPLOYEE_ID = @EMPLOYEE_ID
END
GO

CREATE OR ALTER PROCEDURE DEPO.COMPANIES_LOAD_FROM_FILE
(
	@FILE_PATH NVARCHAR(MAX)
)
AS BEGIN	
	DECLARE @TEMP_PATH NVARCHAR(100) = (SELECT TECH_DATA FROM DEPO.TECH_DATA WHERE TECH_ID = 1)		
	DECLARE @BULK_SQL NVARCHAR(MAX) = 
		'INSERT INTO DEPO.COMPANIES(
			COMPANY_NAME,
			INN,
			LEGAL_ADRESS,
			FACT_ADRESS)
		SELECT
			COMPANY_NAME,
			INN,
			LEGAL_ADRESS,
			FACT_ADRESS
		FROM OPENROWSET (
		BULK ''' + @FILE_PATH + ''',
		FORMATFILE = ''' + @TEMP_PATH + 'companies_format.xml''
		) AS RS'

	EXEC sp_executesql @BULK_SQL

END
GO

CREATE OR ALTER PROCEDURE DEPO.EMPLOYEES_LOAD_FROM_FILE
(
	@FILE_PATH NVARCHAR(MAX)
)
AS BEGIN
	DECLARE @TEMP_PATH NVARCHAR(100) = (SELECT TECH_DATA FROM DEPO.TECH_DATA WHERE TECH_ID = 1)
	DECLARE @BULK_SQL NVARCHAR(MAX) = 
		'INSERT INTO DEPO.EMPLOYEES(
			EMPLOYEE_NAME,
			SURNAME,
			PATRONYMIC,
			BIRTHDATE,
			PASSPORT_SERIES,
			PASSPORT_NUMBER)
		SELECT
			EMPLOYEE_NAME,
			SURNAME,
			PATRONYMIC,
			BIRTHDATE,
			PASSPORT_SERIES,
			PASSPORT_NUMBER
		FROM OPENROWSET (
		BULK ''' + @FILE_PATH + ''',
		FORMATFILE = ''' + @TEMP_PATH + 'employees_format.xml''
		) AS RS'

	EXEC sp_executesql @BULK_SQL

END
GO

CREATE OR ALTER PROCEDURE DEPO.COMPANIES_LOAD_TO_FILE
AS BEGIN
	DECLARE @TEMP_PATH NVARCHAR(100) = (SELECT TECH_DATA FROM DEPO.TECH_DATA WHERE TECH_ID = 1)
	DECLARE @SQL VARCHAR(8000)

	SET @SQL =
	'bcp "SELECT COMPANY_NAME,INN,LEGAL_ADRESS,FACT_ADRESS FROM APPS_DB.DEPO.COMPANIES" queryout ' +
	@TEMP_PATH + 'COMPANIES_full.csv -c -t^| -T -S ' + @@SERVERNAME 

EXEC xp_cmdshell @SQL
END
GO

CREATE OR ALTER PROCEDURE DEPO.EMPLOYEES_LOAD_TO_FILE
AS BEGIN
	DECLARE @TEMP_PATH NVARCHAR(100) = (SELECT TECH_DATA FROM DEPO.TECH_DATA WHERE TECH_ID = 1)
	DECLARE @SQL VARCHAR(8000)

	SET @SQL =
	'bcp "SELECT E.EMPLOYEE_NAME,E.SURNAME,E.PATRONYMIC,E.BIRTHDATE,E.PASSPORT_SERIES,E.PASSPORT_NUMBER,C.COMPANY_NAME FROM APPS_DB.DEPO.EMPLOYEES AS E LEFT JOIN APPS_DB.DEPO.COMPANIES AS C ON E.COMPANY_ID = C.COMPANY_ID" queryout '
	+ @TEMP_PATH + 'EMPLOYEES_full.csv -c -t^| -T -S ' + @@SERVERNAME 

EXEC xp_cmdshell @SQL
END
GO

CREATE OR ALTER PROCEDURE DEPO.LOG_ERROR
(
	@CONTEXT NVARCHAR(100),
	@PARAMS NVARCHAR(100),
	@ERROR_TEXT NVARCHAR(100)
)
AS BEGIN

INSERT INTO DEPO.ERROR_LOG(CONTEXT, PARAMS, ERROR_TEXT)
VALUES (@CONTEXT, @PARAMS, @ERROR_TEXT)

END
GO

CREATE OR ALTER PROCEDURE DEPO.GET_TECH_DATA
(
	@TECH_ID INT
)
AS BEGIN
	SELECT
		TECH_DATA
	FROM DEPO.TECH_DATA
	WHERE TECH_ID = @TECH_ID
END