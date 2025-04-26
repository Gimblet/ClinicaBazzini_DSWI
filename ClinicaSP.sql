USE DSWI_Clinica
GO

-- CREAR PROCEDIMIENTOS ALMACENADOS

-- CITA

-- Lista todas las citas para el BackEnd
CREATE OR ALTER PROC sp_listarCitasBack
AS
BEGIN
    SELECT c.ide_cit,
           c.cal_cit,
           c.con_cit,
           c.ide_med,
           c.ide_pac,
           c.ide_pag
    FROM cita AS c
END
GO

-- Lista todas las citas para el FrontEnd
CREATE OR ALTER PROC sp_listarCitasFront
AS
BEGIN
    SELECT c.ide_cit,
           c.cal_cit,
           c.con_cit,
           CONCAT(m.nom_med, SPACE(1), m.ape_med),
           CONCAT(p.nom_pac, SPACE(1), p.ape_pac),
           p2.mon_pag
    FROM cita AS c
             JOIN dbo.medico m on c.ide_med = m.ide_med
             JOIN dbo.paciente p on c.ide_pac = p.ide_pac
             JOIN dbo.pago p2 on c.ide_pag = p2.ide_pag
END
GO

-- Agrega Paciente
CREATE OR ALTER PROC sp_agregarPaciente(
    @nombre VARCHAR(100),
    @apellido VARCHAR(100),
    @numerodoc VARCHAR(12),
    @tipodoc BIGINT,
    @fechanac DATE,
    @correo VARCHAR(100),
    @contraseña VARCHAR(100)
)
AS
BEGIN
    INSERT INTO paciente
    VALUES (@nombre, @apellido,
            @numerodoc, @tipodoc,
            @fechanac, @correo,
            @contraseña)
END
GO

        sp_agregarPaciente 'Diego Anderson', 'Villena Arias',
        '83212311', 1,
        '2010-03-20', 'diego@gmail.com',
        'diego123'
GO


-- Agrega Medico
CREATE OR ALTER PROC sp_agregarMedico(
    @nombre VARCHAR(100),
    @apellido VARCHAR(100),
    @numerodoc VARCHAR(12),
    @tipodoc BIGINT,
    @fechanac DATE,
    @correo VARCHAR(100),
    @contraseña VARCHAR(100)
)
AS
BEGIN
    INSERT INTO medico
    VALUES (@nombre, @apellido,
            @numerodoc, @tipodoc,
            @fechanac, @correo,
            @contraseña)
END
GO

        sp_agregarMedico 'Jefferson Guadalup', 'Flores Ramires',
        '3945823421', 2,
        '2001-11-01', 'jefferson@bazzini.com',
        'jeff123'
GO

-- Agrega Pago
CREATE OR ALTER PROC sp_agregarPago(
    @hora DATETIME,
    @monto SMALLMONEY,
    @tipopago BIGINT,
    @paciente BIGINT
)
AS
BEGIN
    INSERT INTO pago
    VALUES (@hora, @monto,
            @tipopago, @paciente)
END
GO

        sp_agregarPago '2025-04-25 18:25', 250.0,
        3, 1
GO

-- Agrega Cita
create or alter procedure sp_agregarCita(
    @calendario DATETIME,
    @consultorio INT,
    @medico BIGINT,
    @paciente BIGINT,
    @pago BIGINT
)
AS
BEGIN
    INSERT INTO cita
    VALUES (@calendario, @consultorio,
            @medico, @paciente,
            @pago)
END
GO

        sp_agregarCita '2025-04-28 13:00', 2,
        1, 1,
        1
GO
