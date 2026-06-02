/* ============================================================
   PADELITO_DB - STORED PROCEDURES
   Procedimientos almacenados para reportes y acciones
   ============================================================ */

USE PADELITO_DB
GO

/* ============================================================
   REPORTE PARAMETRIZADO
   Lista reservas dentro de un rango de fechas.
   Permite filtrar opcionalmente por estado de reserva.
   ============================================================ */

CREATE PROCEDURE SP_ReporteReservasPorFecha
    @FechaDesde DATE,
    @FechaHasta DATE,
    @IdEstadoReserva INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        r.IdReserva,
        r.FechaReserva,
        c.Nombre + ' ' + c.Apellido AS Cliente,
        ca.Nombre AS Cancha,
        td.HoraInicio,
        td.HoraFin,
        er.Descripcion AS EstadoReserva,
        r.PrecioBase,
        r.PrecioFinal
    FROM Reservas r
    INNER JOIN Clientes c ON r.IdCliente = c.IdCliente
    INNER JOIN Canchas ca ON r.IdCancha = ca.IdCancha
    INNER JOIN TurnosDisponibles td ON r.IdTurnoDisponible = td.IdTurnoDisponible
    INNER JOIN EstadosReserva er ON r.IdEstadoReserva = er.IdEstadoReserva
    WHERE r.FechaReserva BETWEEN @FechaDesde AND @FechaHasta
      AND (@IdEstadoReserva IS NULL OR r.IdEstadoReserva = @IdEstadoReserva)
    ORDER BY
        r.FechaReserva,
        td.HoraInicio,
        ca.Nombre;
END;
GO

/* ============================================================
   ACCION EN LA BASE DE DATOS
   Cambia el estado de una reserva existente.
   ============================================================ */

CREATE PROCEDURE SP_CambiarEstadoReserva
    @IdReserva INT,
    @IdEstadoReserva INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Reservas
    SET IdEstadoReserva = @IdEstadoReserva
    WHERE IdReserva = @IdReserva;
END;
GO
