use assessment;

INSERT INTO paymentmethods
VALUES
('MOB','MOBILE MONEY'),
('CAS', 'CASH'),
('CARD','CARD');

INSERT INTO paymentchannels
VALUES
('MIB', 'MOBILE AND INTERNET BANKING'),
('ATM/POS','ATM OR POINT OF SALE'),
('CBS','OVER THE COUNTER');

INSERT INTO students
VALUES
(1,'Daniel','Kimolo', '1993-07-13','Male','1E');

SELECT * FROM assessment.students;