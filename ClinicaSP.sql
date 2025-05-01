USE DSWI_Clinica
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

-- agregar recepcionista 
CREATE OR ALTER PROC sp_agregarRecepcionista(
    @cor VARCHAR(100),
    @pwd VARCHAR(150),
    @nom VARCHAR(100),
    @ape VARCHAR(100),
    @ndo VARCHAR(12),
    @fna DATE,
    @doc BIGINT,
    @sue SMALLMONEY
)
AS
BEGIN
    INSERT INTO usuario (cor_usr, pwd_usr, nom_usr, ape_usr, num_doc, fna_usr, ide_doc, ide_rol)
    VALUES (@cor, @pwd, @nom, @ape, @ndo, @fna, @doc, 3);

    INSERT INTO recepcionista (sue_rep, ide_usr)
    VALUES (@sue, SCOPE_IDENTITY());
END
GO

EXEC sp_agregarRecepcionista
     'ana@gmail.com', 'ana12345',
     'Ana María', 'Zevallos Rojas',
     '78645231', '1994-07-22',
     1, 1800;
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

sp_obtenerIdUsuario 'diego@gmail.com'
GO

------------------- PAGOS ---------------------
-- Sp_columns pago
-- Listar tipos de Pago

CREATE OR ALTER PROC sp_listarPaymentOptions
AS
BEGIN
    SELECT *
    FROM pay_opts
END
GO

-- Agrega Pago
CREATE OR ALTER PROC sp_agregarPago(
    @hora DATETIME,
    @monto SMALLMONEY,
    @tipopago BIGINT,
    @usuario BIGINT
)
AS
BEGIN
    INSERT INTO pago
    VALUES (@hora, @monto,
            @tipopago, (select p.ide_pac
                        FROM paciente p
                        WHERE p.ide_usr = @usuario))
END
GO

        sp_agregarPago '2025-04-25 18:25', 250.0,
        3, 3
GO

CREATE OR ALTER PROC sp_obtenerPagoPorId(
    @id BIGINT
)
AS
BEGIN
    SELECT *
    FROM pago
    WHERE ide_pag = @id
END
GO

sp_obtenerPagoPorId 1

-- Lista Pagos
CREATE OR ALTER PROC sp_listarPagos
AS
BEGIN
    SELECT p.ide_pag,
           p.hor_pag,
           p.mon_pag,
           po.nom_pay,
           u.cor_usr,
           CONCAT(u.nom_usr, SPACE(1), u.ape_usr),
           u.num_doc
    FROM pago p
             JOIN pay_opts po ON po.ide_pay = p.tip_pag
             JOIN paciente pc ON pc.ide_pac = p.ide_pac
             JOIN usuario u ON u.ide_usr = pc.ide_usr
END
GO

sp_listarPagos
GO

-- Lista pagos por el id (Id de Usuario)
CREATE OR ALTER PROC sp_listarPagosPorPaciente(
    @ide BIGINT
)
AS
BEGIN
    SELECT p.ide_pag,
           p.hor_pag,
           p.mon_pag,
           po.nom_pay,
           u.cor_usr,
           CONCAT(u.nom_usr, SPACE(1), u.ape_usr),
           u.num_doc
    FROM pago p
             JOIN pay_opts po ON po.ide_pay = p.tip_pag
             JOIN paciente pc ON pc.ide_pac = p.ide_pac
             JOIN usuario u ON u.ide_usr = pc.ide_usr
    WHERE p.ide_pac = (SELECT pa.ide_pac
                       FROM paciente pa
                       WHERE ide_usr = @ide)
END
GO

sp_listarPagosPorPaciente 1

-- Actualizar pago por Id (Se actualiza todos los campos excepto el idPaciente
-- para evitar incongruencias
CREATE OR ALTER PROC sp_actualizarPago(
    @paciente BIGINT,
    @ide_pag BIGINT,
    @hor_pag DATETIME,
    @mon_pag SMALLMONEY,
    @tip_pag BIGINT
)
AS
BEGIN
    UPDATE pago
    SET hor_pag = @hor_pag,
        mon_pag = @mon_pag,
        tip_pag = @tip_pag,
        ide_pac = @paciente
    WHERE ide_pag = @ide_pag
END
GO

-- sp_actualizarPago 1, '2025-04-20 01:00:00', 200.00, 1

-- Elimina un Pago Realizado por Id
CREATE OR ALTER PROC sp_eliminarPago(
    @id BIGINT
)
AS
BEGIN
    DELETE
    FROM pago
    WHERE ide_pag = @id
END
GO

-- sp_eliminarPago 1

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