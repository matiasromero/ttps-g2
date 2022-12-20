create table DFecha
(
    idFecha int not null,
    [year] int,
    mes int,
    dia int,
    PRIMARY KEY (idFecha)
)

create table DLugar
(
    idLugar int not null,
    province nvarchar(80) not null,
    department nvarchar(100) not null,
    PRIMARY KEY (idLugar)
)

create table DVacuna
(
    idVacuna int not null,
    laboratory nvarchar(100) not null,
    [name] nvarchar(50) not null,
    [type] nvarchar(50) not null,
    PRIMARY KEY (idVacuna)

)

create table DVacunado
(
    idVacunado int not null,
    edadanio int,
    edaddecada int,
    edadveintena int,
    dni int,
    sexo nvarchar(20),
    PRIMARY KEY (idVacunado)
)

create table HVencimiento
(
    idVencimiento int not null,
    idFecha int not null,
    idLugar int not null,
    idVacuna int not null
        PRIMARY KEY (idVencimiento),
    FOREIGN KEY (idFecha) REFERENCES DFecha(idFecha),
    FOREIGN KEY (idLugar) REFERENCES DLugar(idLugar),
    FOREIGN KEY (idVacuna) REFERENCES DVacuna(idVacuna)
)

create table HVacunacion
(
    idVacunacion int not null,
    idVacunado int not null,
    idFecha int not null,
    idLugar int not null,
    idVacuna int not null
        PRIMARY KEY (idVacunacion),
    FOREIGN KEY (idVacunado) REFERENCES DVacunado(idVacunado),
    FOREIGN KEY (idFecha) REFERENCES DFecha(idFecha),
    FOREIGN KEY (idLugar) REFERENCES DLugar(idLugar),
    FOREIGN KEY (idVacuna) REFERENCES DVacuna(idVacuna)
)

CREATE INDEX index_dfecha
ON dfecha (year, mes, dia);

CREATE INDEX index_dlugar
ON dlugar (province, department);

CREATE INDEX index_dvacunado
ON dvacunado (dni);

CREATE INDEX index_dvacuna
ON DVacuna (laboratory, [name], [type]);



create table StockHistoryDetails
(
    province nvarchar(80) not null,
    developedVaccine nvarchar(200) not null,
    year int not null,
    month int not null,
    distributedQuantity int not null,
    appliedQuantity int not null,
    currentStock int not null,
    overdueQuantity int not null
)
