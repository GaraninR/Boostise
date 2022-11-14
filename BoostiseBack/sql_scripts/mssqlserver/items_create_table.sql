create table dbo.items (
	id INT IDENTITY(1,1) PRIMARY KEY,
	firstname VARCHAR(60),
	lastname VARCHAR(60),
	phonenumber VARCHAR(40),
	email VARCHAR(60),
	advtext TEXT,
	age INT,
	priceusd NUMERIC(5,2),
	course NUMERIC(4,2),
	priceuah NUMERIC(11,4)
);

drop table dbo.items;