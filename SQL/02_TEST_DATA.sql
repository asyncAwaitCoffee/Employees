TRUNCATE TABLE DEPO.TECH_DATA;
INSERT INTO DEPO.TECH_DATA(TECH_NAME, TECH_DATA)
VALUES ('TEMP FILES', 'D:\MyTemp\')

TRUNCATE TABLE DEPO.COMPANIES;
INSERT INTO DEPO.COMPANIES(COMPANY_NAME, INN, LEGAL_ADDRESS, FACT_ADDRESS)
VALUES
('Company A', '1234567890', '123 Main St, Apt 123, City A', '456 Oak St, Apt 456, City A'),
('Company B', '0987654321', '456 Willow St, Apt 456, City B', '789 Pine St, Apt 789, City B'),
('Company C', '1357924680', '789 Maple St, Apt 789, City C', '012 Cedar St, Apt 012, City C'),
('Company D', '2468013579', '234 Birch St, Apt 234, City D', '345 Walnut St, Apt 345, City D'),
('Company E', '9876543210', '567 Oak St, Apt 567, City E', '678 Willow St, Apt 678, City E');

TRUNCATE TABLE DEPO.EMPLOYEES;
INSERT INTO DEPO.EMPLOYEES(EMPLOYEE_NAME, SURNAME, PATRONYMIC, BIRTHDATE, PASSPORT_SERIES, PASSPORT_NUMBER, COMPANY_ID)
VALUES
('John', 'Johnson', 'Johnovich', '1990-05-15', '1234', '123456', NULL),
('Mary', 'Smith', 'Johnovna', '1985-08-20', '5678', '654321', 1),
('Alexey', 'Sidorev', 'Alexeyevich', '1988-12-10', '9012', '987654', NULL),
('Catherine', 'Smith', 'Alexanderovna', '1992-03-25', '3456', '456789', 2),
('Andrew', 'Popov', 'Andreevich', '1995-07-08', '7890', '321654', NULL),
('Olga', 'Novikova', 'Alexanderovna', '1991-10-12', '2345', '987123', 3),
('Sergey', 'Morozov', 'Sergeevich', '1987-04-30', '6789', '456789', NULL),
('Tatiana', 'Volkova', 'Sergeevna', '1989-06-18', '0123', '321987', 4),
('Dmitriy', 'Kozlov', 'Dmitrievich', '1993-09-05', '4567', '789123', NULL),
('Elena', 'Lebedeva', 'Dmitrievna', '1986-02-28', '8901', '654789', 5),
('Natalia', 'Semyonova', 'Nikolaevna', '1994-11-15', '2345', '159753', 1),
('Alexander', 'Egorov', 'Alexanderovich', '1984-07-03', '6789', '357159', 1),
('Victoria', 'Morgunova', 'Vladimirovna', '1990-01-20', '0123', '852963', 2),
('Paul', 'Markov', 'Nikolaevich', '1988-04-12', '4567', '369852', 2),
('Irina', 'Safonova', 'Andreevna', '1992-08-25', '8901', '147258', 3),
('Artem', 'Fedorov', 'Igorevich', '1987-12-30', '2345', '258369', 3),
('Oksana', 'Zhukova', 'Johnovna', '1996-03-10', '6789', '369147', 4),
('Maxim', 'Baranov', 'Petrovich', '1989-05-22', '0123', '951753', 4),
('Julia', 'Komarova', 'Mikhailovna', '1993-10-05', '4567', '753159', 5),
('Vladimir', 'Belyaev', 'Viktorovich', '1985-01-17', '8901', '159357', 5);

