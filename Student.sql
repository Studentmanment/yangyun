use master;
if exists(select * from sysdatabases where name='StuScoManagement')
drop database  StuScoManagement;
create database StuScoManagement;

 
 use StuScoManagement;
 if exists (select * from sysobjects where name='Course')
 drop table Course;
 create table Course
 (
   ID int identity(1,1),
   Cno char(10) primary key,
   Cname char(20) not null,
   Teano char(10) not null,
   Credit char(4) not null
 );
 
  
 if exists (select * from sysobjects where name='Student')
  drop table Student;
   create table Student
   (
   ID int identity(1,1),
   Sno char(10) primary key,
   [Password] char(10) not null,
   Sname char(10) not null,
   Sgender char(2) not null,
   Sage char(2) not null,
   Depart char(10) not null
   );
  
   
  if exists (select * from sysobjects where name='Teacher')
  drop table Teacher;
  create table Teacher
  (
    ID int identity(1,1),
    Teano char(10) primary key,
    [Password] char(10) not null,
    Tname char(10) not null,
    Tgender char(2) not null,
    Tdepart char(10) not null  
  );
  
  
  if exists (select * from sysobjects where name='SC')
  drop table SC;
  create table SC 
  (
    ID int identity(1,1) primary key,
    Sno char(10) foreign key references Student(Sno),
    Cno char(10) foreign key references Course(Cno),
    Grade smallint,
  );
  
  /*Student表数据插入*/
  insert into Student values
  ( '1108060232',
    '12345678',
     '盛呈呈',
     '男',
     '22',
     'CS'
  );
  insert into Student values
  (
   '1108060231',
    '12345678',
     '叶帅',
     '男',
     '20',
     'CS'
  );
  insert into Student values
  (
   '1108060229',
    '12345678',
     '杨云',
     '女',
     '20',
     'CS'
  );
  insert into Student values
  (
   '1108060230',
    '12345678',
     '乔泽家',
     '男',
     '21',
     'CS'
  );
  
  /*Teacher表数据插入*/
  insert into Teacher values
  (
     '1001',
    '12345678',
     '邓少勋',
     '男',
     'CS'
  );
    insert into Teacher values
  (
     '1002',
    '12345678',
     '李辉',
     '男',
     'CS'
  );
    insert into Teacher values
  (
     '1003',
    '12345678',
     '申玲',
     '女',
     'CS'
  );
    insert into Teacher values
  (
     '1004',
    '12345678',
     '张民选',
     '女',
     'MA'
  );
  
/*Course表数据插入*/
  insert into Course values
  (
     '001',
    '数据库分析',
     '1002',
     '3'
  );
   insert into Course values
  (
     '002',
    '软件工程与计算',
     '1003',
     '2'
  );
   insert into Course values
  (
     '003',
    '线性代数',
     '1004',
     '2'
  );
  insert into Course values
  (
     '004',
    '面向对象',
     '1001',
     '4'
  );
  
  
  /*SC数据插入*/
  insert into SC values
  (
    '1108060232',
    '001',
     '90'
  );
    insert into SC values
  (
    '1108060232',
    '002',
     '91'
  );
    insert into SC values
  (
    '1108060232',
    '003',
     '92'
  );
    insert into SC values
  (
    '1108060232',
    '004',
     '98'
  );
  
    insert into SC values
  (
    '1108060231',
    '001',
     '95'
  );
   
    insert into SC values
  (
    '1108060231',
    '003',
     '97'
  );
   
   insert into SC values
  (
    '1108060231',
    '004',
     '89'
  );
  
  insert into SC values
  (
    '1108060229',
    '002',
     '88'
  );
  insert into SC values
  (
    '1108060229',
    '003',
     '92'
  );
  insert into SC values
  (
    '1108060229',
    '004',
     '95'
  );
  
  insert into SC values
  (
    '1108060230',
    '001',
     '93'
  );
   insert into SC values
  (
    '1108060230',
    '002',
     '86'
  );
   insert into SC values
  (
    '1108060230',
    '004',
     '99'
  );