-- SCRIPT FILE FOR CREATING SAMPLE DB FOR STORING JOBS DATA --

--TO CREATE NEW EMPTY DATABASE
BEGIN
CREATE DATABASE JOBSDB
END
GO

--CREATE DEPARTMENTS TABLE
BEGIN
CREATE TABLE DEPARTMENTS
(
	DEPARTMENTID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	TITLE VARCHAR(50) NOT NULL
)

-- UNCOMMENT FOLLOWING QUERIES TO INSERT SAMPLE DATA IN DB
--INSERT INTO DEPARTMENTS VALUES('Software Development')
--INSERT INTO DEPARTMENTS VALUES('Project Management')
END
GO

--CREATE LOCATIONS TABLE
BEGIN
CREATE TABLE LOCATIONS
(
	LOCATIONID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	TITLE VARCHAR(50) NOT NULL,
	CITY VARCHAR(50) NOT NULL, -- CAN CREATE SEPARATE TABLE TO STORE CITY DATA (Didnt create separate table as this is Sample DB)
	[STATE] VARCHAR(50) NOT NULL, -- CAN CREATE SEPARATE TABLE TO STORE STATE DATA (Didnt create separate table as this is Sample DB)
	COUNTRY VARCHAR(50) NOT NULL, -- CAN CREATE SEPARATE TABLE TO STORE COUNTRY DATA (Didnt create separate table as this is Sample DB)
	ZIP INT NOT NULL -- CHAR DATATYPE MOST PREFERABLE AS THE ZIPCODE MIGHT HAVE ALPHANUMERIC EXTENSIONS IN SOME LOCATIONS (Using int as this is for Sample DB)
)

-- UNCOMMENT FOLLOWING QUERIES TO INSERT SAMPLE DATA IN DB
--INSERT INTO LOCATIONS VALUES('US Office', 'Baltimore', 'MD', 'United States', 21202)
--INSERT INTO LOCATIONS VALUES('India Head Office', 'Bangaluru', 'Karnataka', 'India', 560030)
END
GO

--CREATE JOBS TABLE
BEGIN
CREATE TABLE JOBS
(
	JOBID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	CODE AS ('JOB-' + RIGHT('0000' + CAST(JOBID AS VARCHAR(4)), 4)) PERSISTED NOT NULL UNIQUE,
	TITLE VARCHAR(50) NOT NULL,
	[DESCRIPTION] VARCHAR(200),
	LOCATIONID INT NOT NULL FOREIGN KEY REFERENCES LOCATIONS(LOCATIONID),
	DEPARTMENTID INT NOT NULL FOREIGN KEY REFERENCES DEPARTMENTS(DEPARTMENTID),
	POSTEDDATE AS GETDATE(),
	CLOSINGDATE DATETIME
)

-- UNCOMMENT FOLLOWING QUERIES TO INSERT SAMPLE DATA IN DB
--INSERT INTO JOBS VALUES('Associate Poject Manager', 'Job Description - Associate Poject Manager', 1, 2, GETDATE() + 15)
--INSERT INTO JOBS VALUES('Senior Software Developer', 'Job Description - Senior Software Developer', 2, 1, GETDATE() + 20)
END
GO


