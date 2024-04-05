# Employees and Companies
## Task
Develop an application in C# with MSSQL database.
Conduct development using git version control system.
The application represents a database of organization employees.

It should include the following capabilities:

* Adding a new organization from the interface (Name, INN, legal address, actual address)
* Adding employees from the interface (Surname, First Name, Patronymic, date of birth, passport series, passport number)
* Ability to import and export from/to a csv file
  
The repository should contain:

* A project created in Visual Studio
* Script for creating and initially populating the database
* Data in csv format for loading
* An example of csv export

## Table DEPO.TECH_DATA
DEPO.TECH_DATA table stores the directory available to the SQL server and the application. This directory processes the uploaded and downloaded files.

## Folder SQL/FileFormat
Schemas for uploaded files are expected in the directory from DEPO.TECH_DATA.

## Folder SQL/FileUpload
CSV files for test uploading to the server.

## Folder SQL/FileDownloaded
Example of downloaded CSV files.

# Сотрудники и Компании

## Задача
Создать приложение на языке C# с БД MSSQL.  
Разработку вести с помощью системы контроля версий git.  
Приложение представляет из себя базу сотрудников организаций.  
  
Должно включать возможность:  
* Заведения новой организации из интерфейса (Наименование, ИНН, юр. адрес, факт. адрес)  
* Заведения сотрудников из интерфейса (Фамилия, Имя, Отчество, дата рождения, паспорт серия, паспорт номер)  
* Иметь возможность импорта и экспорта из\в csv-файл

Репозиторий должен содержать:  
* Проект, выполненный в visual studio
* Скрипт создания и первоначального заполнения базы данных
* Данные в csv для загрузки
* Пример выгрузки csv

## Таблица DEPO.TECH_DATA
В DEPO.TECH_DATA записывается директория, доступная SQL серверу и приложению. В этой директории обрабатываются загружаемые и скачиваемые файлы.
## Папка SQL/FileFormat
В директории из DEPO.TECH_DATA ожидаются схемы для загружаемых файлов, лежащие в FileFormat.
## Папка SQL/FileUpload
Файлы для тестовой загрузки на сервер.
## Папка SQL/FileDownloaded
Файлы тестовой выгрузки с сервера.