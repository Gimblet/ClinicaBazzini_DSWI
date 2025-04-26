USE master
GO

IF DB_ID('DSWI_Clinica') IS NOT NULL
    DROP DATABASE DSWI_Clinica
GO

CREATE DATABASE DSWI_Clinica
GO

USE DSWI_Clinica
GO

CREATE TABLE user_doc
(
    ide_doc BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    nom_doc VARCHAR(30)
)
GO

INSERT INTO user_doc
VALUES
    ('DNI'),
    ('CEX')
GO

CREATE TABLE medico
(
    ide_med BIGINT IDENTITY(1,1) PRIMARY KEY,
    nom_med VARCHAR(100) NOT NULL, -- Nombre
    ape_med VARCHAR(100) NOT NULL, -- Apellido
    num_doc VARCHAR(12) NOT NULL, -- Numero de Documento
    tip_doc BIGINT NOT NULL, -- Tipo de Documento
    fna_doc DATE NOT NULL, -- Fecha Nacimiento
    cor_med VARCHAR(100) NOT NULL UNIQUE, -- Correo
    con_med VARCHAR(150) NOT NULL, -- Contraseña
    FOREIGN KEY(tip_doc) REFERENCES user_doc(ide_doc)
)
GO

CREATE TABLE paciente
(
    ide_pac BIGINT IDENTITY(1,1) PRIMARY KEY,
    nom_pac VARCHAR(100) NOT NULL, -- Nombre
    ape_pac VARCHAR(100) NOT NULL, -- Apellido
    num_doc VARCHAR(12) NOT NULL, -- Numero de Documento
    tip_doc BIGINT NOT NULL, -- Tipo de documento
    fna_pac DATE NOT NULL, -- Fecha Nacimiento
    cor_pac VARCHAR(100) NOT NULL UNIQUE, -- Correo
    con_pac VARCHAR(150) NOT NULL, -- Contraseña
    FOREIGN KEY(tip_doc) REFERENCES user_doc(ide_doc)
)
GO

CREATE TABLE  pay_opts
(
    ide_pay BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    nom_pay VARCHAR(50)
)
GO

INSERT INTO pay_opts
VALUES 
    ('Efectivo'),
    ('Yape'),
    ('Plin'),
    ('IZI PAY'),
    ('VISA'),
    ('MASTERCARD'),
    ('AMEX'),
    ('DINERS CLUB')
GO

CREATE TABLE pago
(
    ide_pag BIGINT IDENTITY(1,1) PRIMARY KEY,
    hor_pag DATETIME NOT NULL, -- Hora
    mon_pag SMALLMONEY NOT NULL CHECK(mon_pag > 0), -- Monto
    tip_pag BIGINT NOT NULL, -- Tipo de Pago
    ide_pac BIGINT NOT NULL, -- Paciente(Usuario)
    FOREIGN KEY(tip_pag) REFERENCES pay_opts(ide_pay),
    FOREIGN KEY(ide_pac) REFERENCES paciente(ide_pac) 
)
GO

CREATE TABLE cita
( 
    ide_cit BIGINT IDENTITY(1,1) PRIMARY KEY,
    cal_cit DATETIME NOT NULL, -- Calendario(Fecha)
    con_cit INT NOT NULL CHECK(cita.con_cit > 0),-- Consultorio
    ide_med BIGINT NOT NULL, -- Id del Medico
    ide_pac BIGINT NOT NULL, -- Id del Paciente
    ide_pag BIGINT NOT NULL, -- Id del Pago
    FOREIGN KEY(ide_med) REFERENCES medico(ide_med),
    FOREIGN KEY(ide_pac) REFERENCES paciente(ide_pac),
    FOREIGN KEY(ide_pag) REFERENCES pago(ide_pag)
)
GO
