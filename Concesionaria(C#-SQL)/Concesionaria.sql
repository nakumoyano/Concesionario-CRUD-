create database Concesionario
use Concesionario 

create table modelos
(id_modelo int identity,
 modelo varchar (100),
 constraint pk_id_modelo primary key(id_modelo)
)

create table marcas
(id_marca int identity,
 marca varchar(100),
 constraint pk_id_marca primary key(id_marca)
)

create table colores
(id_color int identity,
 color varchar(100),
 constraint pk_id_color primary key(id_color)
)

create table concesionarias
(id_concesionaria int identity,
 id_modelo int,
 id_marca int,
 precio decimal,
 puertas int,
 id_color int,
 constraint pk_id_concesionaria primary key(id_concesionaria),
 constraint fk_id_modelo foreign key(id_modelo) references modelos(id_modelo),
 constraint fk_id_marca foreign key(id_marca) references marcas(id_marca),
 constraint fk_id_color foreign key(id_color) references colores(id_color)
)

insert into modelos values (1960),(1961),(1962),(1963),(1964),(1965),(1966),(1967),(1968),(1969),(1970),(1971),
(1972),(1973),(1974),(1975),(1976),(1977),(1978),(1979),(1980),(1981),(1982),(1983),(1984),(1985),(1986),(1987),(1988),(1989),(1990),(1991),(1992),(1993),(1994),(1995),(1996),(1997),(1998),(1999),(2000),(2001),(2002),(2003),(2004),(2005),(2006),(2007),(2008),(2009),(2010),(2011),(2012),(2013),(2014),(2015),(2016),(2017),(2018),(2019),(2020),(2021)

insert into marcas values ('Abarth'),('Alfa Romeo'), ('Aston Martin'), ('Audi'), ('Bentley'), ('Bmw'), ('Byd'), ('Chevrolet'), ('Citroen'), ('Dacia'), ('Dfsk'), ('Ds'), ('Ferrari'), ('Fiat'), ('Ford'), ('Honda'), ('Hyundai'), ('Infiniti'), ('Isuzu'), ('Jaguar'), ('Jeep'), ('Kia'), ('Lada'), ('Lamborghini'), ('Lancia'), ('Land Rover'), ('Lexus'), ('Mahindra'), ('Maserati'), ('Mazda'), ('Mercedes'), ('Mini'), ('Mitsubishi'),('Morgan'), ('Nissan'), ('Opel'), ('Peugeot'), ('Porsche'), ('Renault'), ('Rolls-Royce'), ('Seat'), ('Skoda'), ('Smart'), ('Subaru'), ('Suzuki'), ('Tesla'), ('Toyota'), ('Volkswagen'), ('Volvo')

insert into colores values ('Azul'), ('Rojo'), ('Negro'), ('Neutro'), ('Fluor'), ('Amarillo'), ('Verde'), ('Naranja'), ('Violeta'), ('Celeste')

insert into concesionarias values (1,20,130000,2,2),
                                  (61,24,5600000,1,3),
								  (62,39,4134300,2,1),
								  (60,1,500000,2,6),
								  (45,30,256000,1,5),
								  (32,19,356230,1,7),
								  (30,21,150000,2,4),
								  (10,32,300000,1,3),
								  (1,15,700000,1,3),
								  (6,15,1050000,2,2)

alter table concesionarias add nombre varchar(100)

select * from concesionarias 

insert into concesionarias (nombre) values ('I-Pace HSE 400'),
										   ('Huracan 2021'),
										   ('Captur'),
										   ('Fiat 500'),
										   ('Suv CX-3'),
										   ('DMAX 2021'),
										   ('Renegade'),
										   ('Ulta-Compact'),
										   ('Mondeo Hibrido'),
										   ('Focus')

alter table concesionarias drop column nombre