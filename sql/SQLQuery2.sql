--CREATE TABLE employee1 (
--    id INT PRIMARY KEY,
--    emp_name VARCHAR(50),
--    job VARCHAR(30),
--    salary INT,
--    hire_date DATE,
--    dept_no INT
--);

INSERT INTO employee1 VALUES (1, 'Afor', 'Manager', 50000, '2023-01-10', 10);
INSERT INTO employee1 VALUES (2, 'Bfor', 'Lead', 40000, '2023-02-15', 20);
INSERT INTO employee1 VALUES (3, 'Cfor', 'DeveOps', 35000, '2023-01-10', 10);
INSERT INTO employee1 VALUES (4, 'Dfor', 'Backend', 30000, '2023-03-20', 30);
INSERT INTO employee1 VALUES (5, 'Efor', 'Frontend', 25000, '2023-01-10', 20);
INSERT INTO employee1 VALUES (6, 'Ffor', 'Tester', 19000, '2023-04-05', 10);
INSERT INTO employee1 VALUES (7, 'Gfor', 'ProdEng', 22000, '2023-02-15', 20);


SELECT job, SUM(salary) AS total_salary
FROM employee1
GROUP BY job;

SELECT hire_date, COUNT(*) AS total_employees
FROM employee1
GROUP BY hire_date
HAVING COUNT(*) >= 3;

SELECT dept_no, SUM(salary) AS total_salary
FROM employee1
GROUP BY dept_no
HAVING COUNT(*) > 2 
   AND SUM(salary) > 29000;