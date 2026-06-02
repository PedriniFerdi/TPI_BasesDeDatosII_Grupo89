/* ============================================================
   PADELITO_DB - TRIGGERS
   Triggers de auditoria para reservas
   ============================================================ */

USE PADELITO_DB
GO

/* ============================================================
   TRIGGER TRAS UNA INSERCION
   Registra en AuditoriaReservas cuando se crea una reserva.
   ============================================================ */

CREATE TRIGGER TR_Reservas_Insert
ON Reservas
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditoriaReservas (
        IdReserva,
        Accion,
        Descripcion,
        UsuarioSistema
    )
    SELECT
        i.IdReserva,
        'INSERT',
        'Se registro una nueva reserva.',
        SUSER_SNAME()
    FROM inserted i;
END;
GO

/* ============================================================
   TRIGGER TRAS UNA MODIFICACION
   Registra en AuditoriaReservas cuando se modifica una reserva.
   ============================================================ */

CREATE TRIGGER TR_Reservas_Update
ON Reservas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditoriaReservas (
        IdReserva,
        Accion,
        Descripcion,
        UsuarioSistema
    )
    SELECT
        i.IdReserva,
        'UPDATE',
        'Se modifico una reserva.',
        SUSER_SNAME()
    FROM inserted i;
END;
GO

/* ============================================================
   TRIGGER TRAS UNA ELIMINACION
   Registra en AuditoriaReservas cuando se elimina una reserva.
   ============================================================ */

CREATE TRIGGER TR_Reservas_Delete
ON Reservas
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO AuditoriaReservas (
        IdReserva,
        Accion,
        Descripcion,
        UsuarioSistema
    )
    SELECT
        d.IdReserva,
        'DELETE',
        'Se elimino una reserva.',
        SUSER_SNAME()
    FROM deleted d;
END;
GO
