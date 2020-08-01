CREATE USER proiect_ip IDENTIFIED BY proiectip
DEFAULT TABLESPACE users
TEMPORARY TABLESPACE temp
QUOTA UNLIMITED ON users;
GRANT connect , resource , create view TO proiect_ip;