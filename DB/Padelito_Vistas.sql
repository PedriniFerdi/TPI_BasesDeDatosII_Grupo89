/* ============================================================
   PADELITO_DB - VISTAS
   Vistas para consultas frecuentes del sistema
   ============================================================ */

USE PADELITO_DB
GO

/* ============================================================
   VISTA 1
   Detalle general de reservas.
   ============================================================ */

CREATE VIEW VW_ReservasDetalle
AS
SELECT
    r.IdReserva,
    r.FechaReserva,
    c.Nombre + ' ' + c.Apellido AS Cliente,
    ca.Nombre AS Cancha,
    tc.Descripcion AS TipoCancha,
    td.HoraInicio,
    td.HoraFin,
    e.Nombre + ' ' + e.Apellido AS Empleado,
    er.Descripcion AS EstadoReserva,
    p.Nombre AS Promocion,
    r.PrecioBase,
    r.PrecioFinal,
    r.FechaCreacion
FROM Reservas r
INNER JOIN Clientes c ON r.IdCliente = c.IdCliente
INNER JOIN Canchas ca ON r.IdCancha = ca.IdCancha
INNER JOIN TiposCancha tc ON ca.IdTipoCancha = tc.IdTipoCancha
INNER JOIN TurnosDisponibles td ON r.IdTurnoDisponible = td.IdTurnoDisponible
INNER JOIN Empleados e ON r.IdEmpleado = e.IdEmpleado
INNER JOIN EstadosReserva er ON r.IdEstadoReserva = er.IdEstadoReserva
LEFT JOIN Promociones p ON r.IdPromocion = p.IdPromocion;
GO

/* ============================================================
   VISTA 2
   Detalle de pagos realizados.
   ============================================================ */

CREATE VIEW VW_PagosDetalle
AS
SELECT
    p.IdPago,
    p.IdReserva,
    c.Nombre + ' ' + c.Apellido AS Cliente,
    ca.Nombre AS Cancha,
    r.FechaReserva,
    mp.Descripcion AS MetodoPago,
    p.Monto,
    p.FechaPago,
    p.Observacion
FROM Pagos p
INNER JOIN Reservas r ON p.IdReserva = r.IdReserva
INNER JOIN Clientes c ON r.IdCliente = c.IdCliente
INNER JOIN Canchas ca ON r.IdCancha = ca.IdCancha
INNER JOIN MetodosPago mp ON p.IdMetodoPago = mp.IdMetodoPago;
GO

/* ============================================================
   VISTA 3
   Canchas activas con su tipo y precio.
   ============================================================ */

CREATE VIEW VW_CanchasActivas
AS
SELECT
    ca.IdCancha,
    ca.Nombre AS Cancha,
    tc.Descripcion AS TipoCancha,
    ca.PrecioHora,
    ca.Activa
FROM Canchas ca
INNER JOIN TiposCancha tc ON ca.IdTipoCancha = tc.IdTipoCancha
WHERE ca.Activa = 1;
GO
