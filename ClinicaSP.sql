USE DSWI_Clinica
GO

---------------------- CITA ------------------------
-- sp_columns cita

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
           CONCAT(um.nom_usr, SPACE(1), um.ape_usr),
           CONCAT(up.nom_usr, SPACE(1), up.ape_usr),
           pg.mon_pag
    FROM cita AS c
             JOIN medico m ON m.ide_med = c.ide_med
             JOIN usuario um ON um.ide_usr = m.ide_usr
             JOIN paciente p ON p.ide_pac = c.ide_pac
             JOIN usuario up ON up.ide_usr = p.ide_pac
             JOIN pago pg ON c.ide_pag = pg.ide_pag
END
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
    INSERT INTO cita (cal_cit, con_cit, ide_med, ide_pac, ide_pag)
    values (@calendario, @consultorio, @medico,
            (SELECT p.ide_pac
             FROM paciente p
             WHERE p.ide_usr = @paciente), @pago)
END
GO

        sp_agregarCita '2025-04-28 13:00', 2,
        1, 1,
        1
GO

-- Agrega Paciente
CREATE OR ALTER PROC sp_agregarPaciente(
    @cor varchar(100),
    @pwd varchar(150),
    @nom varchar(100),
    @ape varchar(100),
    @ndo varchar(12),
    @fna DATE,
    @doc BIGINT
)
AS
BEGIN
    INSERT INTO usuario (cor_usr, pwd_usr, nom_usr, ape_usr, num_doc, fna_usr, ide_doc, ide_rol)
    VALUES (@cor, @pwd, @nom, @ape,
            @ndo, @fna, @doc, 1);

    INSERT INTO paciente (ide_usr)
    VALUES (scope_identity())
END
GO

        sp_agregarPaciente
        'diego@gmail.com', 'diego1234',
        'Diego Anderson', 'Villena Arias',
        '123456789', '2001-01-30', 2
GO

-- Agrega Medico
CREATE OR ALTER PROC sp_agregarMedico(
    @cor varchar(100),
    @pwd varchar(150),
    @nom varchar(100),
    @ape varchar(100),
    @ndo varchar(12),
    @fna DATE,
    @doc BIGINT,
    @sue SMALLMONEY,
    @esp BIGINT
)
AS
BEGIN
    INSERT INTO usuario (cor_usr, pwd_usr, nom_usr, ape_usr, num_doc, fna_usr, ide_doc, ide_rol)
    VALUES (@cor, @pwd, @nom, @ape,
            @ndo, @fna, @doc, 2);

    INSERT INTO medico (sue_med, ide_esp, ide_usr)
    VALUES (@sue, @esp, scope_identity())
END
GO

        sp_agregarMedico
        'jefferson@bazzini.com', 'jeff12345',
        'Jefferson Guadalup', 'Flores Ramires',
        '3945823421', '2001-11-01',
        2, 2500,
        1
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

-- Lista pacientes para el FrontEnd

CREATE OR ALTER PROC sp_listarPacientesFront
AS
BEGIN
    SELECT p.ide_pac,
           u.nom_usr,
           u.ape_usr,
           u.fna_usr,
           ud.nom_doc,
           u.num_doc,
           r.nom_rol
    FROM paciente AS p
             JOIN usuario u ON u.ide_usr = p.ide_usr
             JOIN user_doc ud ON ud.ide_doc = u.ide_doc
             JOIN roles r ON u.ide_rol = r.ide_rol
END
GO

-- Listar tipos de Pago

CREATE OR ALTER PROC sp_listarPaymentOptions
AS
BEGIN
    SELECT *
    FROM pay_opts
END
GO

-- Verificar Inicio de Sesion

CREATE OR ALTER PROC sp_verificarLogin(
    @correo VARCHAR(100),
    @contraseña VARCHAR(150)
)
AS
BEGIN
    SELECT r.nom_rol
    FROM usuario u
             JOIN roles r ON r.ide_rol = u.ide_rol
    WHERE u.cor_usr = @correo
      AND u.pwd_usr = @contraseña
END
GO

sp_verificarLogin 'joseph@gmail.com', 'Joseph1234'
GO

CREATE OR ALTER PROC sp_obtenerIdUsuario(
    @correo VARCHAR(100)
)
AS
BEGIN
    SELECT u.ide_usr
    FROM usuario u
    WHERE u.cor_usr = @correo
END
GO

sp_obtenerIdUsuario 'joseph@gmail.com'
GO