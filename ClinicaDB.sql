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

CREATE TABLE roles
(
    ide_rol BIGINT IDENTITY (1,1) PRIMARY KEY,
    nom_rol VARCHAR(15) NOT NULL CHECK(DATALENGTH(nom_rol) >= 4), -- Nombre del Rol (Tiene que ser mayor a 3 letras)
    pri_rol INT NOT NULL CHECK(pri_rol >= 0) -- Prioridad (No puede ser menor que 0)
)

INSERT INTO roles (nom_rol, pri_rol)
VALUES
    ('paciente', 0),
    ('medico', 1),
    ('recepcionista', 2)
GO

CREATE TABLE usuario
(
    ide_usr BIGINT IDENTITY(1,1) PRIMARY KEY,
    cor_usr VARCHAR(100) NOT NULL UNIQUE, -- Correo 
    pwd_usr VARCHAR(150) NOT NULL, -- Contraseña
    nom_usr VARCHAR(100) NOT NULL, -- Nombre
    ape_usr VARCHAR(100) NOT NULL, -- Apellido
    fna_usr DATE NOT NULL, -- Fecha Nacimiento
    num_doc VARCHAR(12) NOT NULL, -- Numero de Documento
    ide_doc BIGINT NOT NULL, -- Tipo de Documento
    ide_rol BIGINT NOT NULL, -- Rol del usuario
    CONSTRAINT pwdLongerThan8Char CHECK(DATALENGTH(pwd_usr) >= 8), -- La contraseña debe ser mayor a 8 caracteres
    CONSTRAINT FK_Document FOREIGN KEY(ide_doc) REFERENCES user_doc(ide_doc),
    CONSTRAINT FK_Roles FOREIGN KEY(ide_rol) REFERENCES roles(ide_rol)
)

CREATE TABLE especialidad
(
    ide_esp BIGINT IDENTITY(1,1) PRIMARY KEY,
    nom_esp VARCHAR(25) NOT NULL
)
GO

INSERT INTO especialidad (nom_esp)
VALUES
    ('Dermatología'),
    ('Pediatría'),
    ('Cirugía'),
    ('Oftalmología'),
    ('Neurología'),
    ('Radiolgía')
GO

CREATE TABLE medico
(
    ide_med BIGINT IDENTITY(1,1) PRIMARY KEY,
    sue_med SMALLMONEY NOT NULL CHECK(sue_med > 0),
    ide_esp BIGINT NOT NULL, -- Id Especialidad
    ide_usr BIGINT NOT NULL UNIQUE, -- Id de usuario(Credenciales)
    FOREIGN KEY(ide_usr) REFERENCES usuario(ide_usr),
    FOREIGN KEY(ide_esp) REFERENCES especialidad(ide_esp)
)
GO

CREATE TABLE paciente
(
    ide_pac BIGINT IDENTITY(1,1) PRIMARY KEY,
    ide_usr BIGINT NOT NULL UNIQUE, -- Id de usuario(Credenciales)
    FOREIGN KEY(ide_usr) REFERENCES usuario(ide_usr)
)
GO

CREATE TABLE recepcionista
(
    ide_rep BIGINT IDENTITY(1,1) PRIMARY KEY,
    sue_rep SMALLMONEY NOT NULL CHECK(sue_rep > 0),
    ide_usr BIGINT NOT NULL UNIQUE, -- Id de usuario(Credenciales)
    FOREIGN KEY(ide_usr) REFERENCES usuario(ide_usr)
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