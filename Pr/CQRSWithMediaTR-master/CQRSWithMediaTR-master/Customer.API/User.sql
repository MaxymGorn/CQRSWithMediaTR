 
CREATE LOGIN [Admins]   
    WITH PASSWORD = 'Admins';  
	go
 
CREATE USER [Admins]
	for LOGIN [Admins]
GO

GRANT  SELECT, INSERT, UPDATE,delete on SCHEMA::dbo TO Admins;
