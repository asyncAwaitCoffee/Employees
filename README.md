# Сотрудники и Компании

## Задача
Создать приложение на языке C# с БД MSSQL.  
Разработку вести с помощью системы контроля версий git.  
Приложение представляет из себя базу сотрудников организаций.  
  
Должно включать возможность:  
* Заведения новой организации из интерфейса (Наименование, ИНН, юр. адрес, факт. адрес)  
* Заведения сотрудников из интерфейса (Фамилия, Имя, Отчество, дата рождения, паспорт серия, паспорт номер)  
* Иметь возможность импорта и экспорта из\в csv-файл

## Таблица DEPO.TECH_DATA
В DEPO.TECH_DATA записывается директория, доступная SQL серверу и приложению. В этой директории обрабатываются загружаемые и скачиваемые файлы.
## Папка SQL/FileFormat
В директории из DEPO.TECH_DATA ожидаются схемы для загружаемых файлов, лежащие в FileFormat.
## Папка SQL/FileUpload
Файлы для тестовой загрузки на сервер.