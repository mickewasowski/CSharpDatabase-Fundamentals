BACKUP DATABASE SoftUni
TO DISK = 'D:\backups\SoftUniDB.bak';

USE master
DROP DATABASE SoftUni

RESTORE DATABASE SoftUni FROM DISK='D:\backups\SoftUniDB.bak';
