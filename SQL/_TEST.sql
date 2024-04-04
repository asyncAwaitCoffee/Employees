SELECT * FROM DEPO.COMPANIES

SELECT * FROM DEPO.EMPLOYEES
SELECT * FROM DEPO.ERROR_LOG
/*
declare @sql varchar(8000)

set @sql =
'bcp "select EMPLOYEE_ID,EMPLOYEE_NAME,SURNAME,PATRONYMIC,BIRTHDATE,PASSPORT_SERIES,PASSPORT_NUMBER,COMPANY_ID from APPS_DB.DEPO.EMPLOYEES" queryout D:\MyTemp\TEST_full.csv -c -t^| -T -S ' + @@SERVERNAME 

print @sql

exec master..xp_cmdshell @sql

declare @sql varchar(8000)

set @sql =
'bcp APPS_DB.DEPO.COMPANIES format nul -c -x -f D:\MyTemp\companies_format.xml -t^|, -T'

print @sql

exec master..xp_cmdshell @sql
*/
exec DEPO.COMPANIES_LOAD_FROM_FILE @FILE_PATH=N'D:\MyTemp\ColorPickerPalette.csv_e9fe2e26-554f-4323-8de7-b560fe45d753'

INSERT INTO DEPO.EMPLOYEES(EMPLOYEE_NAME,SURNAME,PATRONYMIC,BIRTHDATE,PASSPORT_SERIES,PASSPORT_NUMBER,COMPANY_ID)
SELECT EMPLOYEE_NAME,SURNAME,PATRONYMIC,BIRTHDATE,PASSPORT_SERIES,PASSPORT_NUMBER,COMPANY_ID FROM OPENROWSET (
BULK 'D:\MyTemp\TEST.csv',
FORMATFILE = 'D:\MyTemp\employees_format.xml',
ERRORFILE = 'D:\MyTemp\errors.txt'
) AS RS

BULK INSERT APPS_DB.DEPO.EMPLOYEES
FROM 'D:\MyTemp\TEST_full.csv'
WITH
(
        FORMAT='CSV',
		FIELDTERMINATOR = '|'
)
GO