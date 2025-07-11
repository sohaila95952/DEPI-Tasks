CREATE DATABASE COMPANY;
CREATE SCHEMA COMPANY;
USE COMPANY;
create table company.employee (
ssn int identity (1,1)primary key ,
birthdate date not null ,
first varchar(255) not null,
last varchar(255) not null ,
gender varchar(1) check  (gender ='f' or gender ='m'),
managerid int ,
foreign key (managerid) references company.employee (ssn),
dnum int
);
create table company.department(
dnum int identity(10,10) primary key,
dname varchar(250) not null,
hiredate date default getdate(),
ssn int,
foreign key (ssn) references company.employee (ssn),
);
create table company.project(
pnum int identity (1,1) primary key,
 pname varchar(250),
 location varchar(250),
 city varchar(250),
 dnum int,
 foreign key (dnum) references company.department (dnum)

);
create table company.emp_manage_dep(
working_hours int ,
essn int ,
pnum int,
primary key (essn,pnum),
foreign key (pnum) references company.project (pnum),
foreign key (essn) references company.employee (ssn),
);
alter table company.employee 
add foreign key (dnum) references company.department (dnum);
create table company.department_locations(
location varchar(250) ,
dnum int,
primary key (location,dnum),
 foreign key (dnum) references company.department (dnum)
);
create table company.dependent (
depname varchar(250) ,
gender varchar(255) not null check  (gender ='f' or gender ='m'),
birthdate date not null ,
essn int ,
primary key (depname,essn),
foreign key (essn) references company.employee (ssn)
);
INSERT INTO company.employee  (birthdate,first,last,gender) VALUES ('2004-05-09', 'sohaila' ,'ahmed','f');
INSERT INTO company.employee  (birthdate,first,last,gender) VALUES ('2004-06-02', 'salma' ,'osama','f');
INSERT INTO company.employee  (birthdate,first,last,gender) VALUES ('2004-07-03', 'ali' ,'mohmmed','m');
INSERT INTO company.employee  (birthdate,first,last,gender) VALUES ('2004-08-04', 'rami' ,'khaled','m');
INSERT INTO company.employee  (birthdate,first,last,gender) VALUES ('2004-09-09', 'ziad' ,'ahmed','m');
INSERT INTO company.department  (dname,hiredate) VALUES ( 'cs','2024-05-09' );
INSERT INTO company.department  (dname,hiredate) VALUES ( 'ai','2024-07-01' );
INSERT INTO company.department  (dname) VALUES ( 'is' );
INSERT INTO company.department  (dname) VALUES ('hr' );
update company.employee set dnum=20 where first='rami' ;
INSERT INTO company.dependent  (depname,essn,gender,birthdate) VALUES ( 'selim',1,'m','2024-10-01' );
INSERT INTO company.dependent  (depname,essn,gender,birthdate) VALUES ( 'adel',2,'m','2024-12-01' );
delete from company.dependent where depname= 'adel';
select * from company.employee where dnum=20;
INSERT INTO company.project (pname) VALUES ('web'),('ai'); 
INSERT INTO company.emp_manage_dep (essn, pnum, working_hours)
VALUES (1, 2, 20),(2,1, 15);
SELECT e.ssn, e.first, e.last, p.pnum, p.pname, ed.working_hours
FROM 
    company.employee e
JOIN 
    company.emp_manage_dep ed ON e.ssn = ed.essn
JOIN 
    company.project p ON p.pnum = ed.pnum;


alter table company.dependent drop constraint FK__dependent__essn__5165187F ;
alter table company.dependent 
add foreign key (essn) references company.employee (ssn) 
on delete cascade on update cascade;
