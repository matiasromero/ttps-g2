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