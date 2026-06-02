/* ============================================================
   PADELITO_DB - DATOS INICIALES / DATOS DE PRUEBA
   Ejecutar despues de Padelito_DB.sql, Padelito_Triggers.sql,
   Padelito_Vistas.sql y Padelito_StoredProcedures.sql.

   Este script evita duplicar registros si se ejecuta
   mas de una vez.
   ============================================================ */

USE PADELITO_DB
GO

SET NOCOUNT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    /* ========================================================
       CATALOGOS BASICOS
       ======================================================== */

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE Descripcion = 'Administrador')
        INSERT INTO Roles (Descripcion) VALUES ('Administrador');

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE Descripcion = 'Empleado')
        INSERT INTO Roles (Descripcion) VALUES ('Empleado');

    IF NOT EXISTS (SELECT 1 FROM Roles WHERE Descripcion = 'Recepcion')
        INSERT INTO Roles (Descripcion) VALUES ('Recepcion');

    IF NOT EXISTS (SELECT 1 FROM EstadosReserva WHERE Descripcion = 'Pendiente')
        INSERT INTO EstadosReserva (Descripcion) VALUES ('Pendiente');

    IF NOT EXISTS (SELECT 1 FROM EstadosReserva WHERE Descripcion = 'Confirmada')
        INSERT INTO EstadosReserva (Descripcion) VALUES ('Confirmada');

    IF NOT EXISTS (SELECT 1 FROM EstadosReserva WHERE Descripcion = 'Cancelada')
        INSERT INTO EstadosReserva (Descripcion) VALUES ('Cancelada');

    IF NOT EXISTS (SELECT 1 FROM EstadosReserva WHERE Descripcion = 'Finalizada')
        INSERT INTO EstadosReserva (Descripcion) VALUES ('Finalizada');

    IF NOT EXISTS (SELECT 1 FROM MetodosPago WHERE Descripcion = 'Efectivo')
        INSERT INTO MetodosPago (Descripcion) VALUES ('Efectivo');

    IF NOT EXISTS (SELECT 1 FROM MetodosPago WHERE Descripcion = 'Tarjeta de debito')
        INSERT INTO MetodosPago (Descripcion) VALUES ('Tarjeta de debito');

    IF NOT EXISTS (SELECT 1 FROM MetodosPago WHERE Descripcion = 'Tarjeta de credito')
        INSERT INTO MetodosPago (Descripcion) VALUES ('Tarjeta de credito');

    IF NOT EXISTS (SELECT 1 FROM MetodosPago WHERE Descripcion = 'Transferencia')
        INSERT INTO MetodosPago (Descripcion) VALUES ('Transferencia');

    IF NOT EXISTS (SELECT 1 FROM MetodosPago WHERE Descripcion = 'Mercado Pago')
        INSERT INTO MetodosPago (Descripcion) VALUES ('Mercado Pago');

    IF NOT EXISTS (SELECT 1 FROM TiposCancha WHERE Descripcion = 'Cemento')
        INSERT INTO TiposCancha (Descripcion) VALUES ('Cemento');

    IF NOT EXISTS (SELECT 1 FROM TiposCancha WHERE Descripcion = 'Sintetico')
        INSERT INTO TiposCancha (Descripcion) VALUES ('Sintetico');

    IF NOT EXISTS (SELECT 1 FROM TiposCancha WHERE Descripcion = 'Indoor')
        INSERT INTO TiposCancha (Descripcion) VALUES ('Indoor');

    IF NOT EXISTS (SELECT 1 FROM TiposCancha WHERE Descripcion = 'Premium')
        INSERT INTO TiposCancha (Descripcion) VALUES ('Premium');

    /* ========================================================
       PERSONAS
       ======================================================== */

    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Email = 'juan.perez@mail.com')
        INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Activo)
        VALUES ('Juan', 'Perez', '11-2456-7788', 'juan.perez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Email = 'maria.gomez@mail.com')
        INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Activo)
        VALUES ('Maria', 'Gomez', '11-3654-2210', 'maria.gomez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Email = 'lucas.fernandez@mail.com')
        INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Activo)
        VALUES ('Lucas', 'Fernandez', '11-4890-1133', 'lucas.fernandez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Email = 'sofia.ramirez@mail.com')
        INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Activo)
        VALUES ('Sofia', 'Ramirez', '11-6021-9044', 'sofia.ramirez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Email = 'diego.suarez@mail.com')
        INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Activo)
        VALUES ('Diego', 'Suarez', '11-7012-3321', 'diego.suarez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Email = 'carolina.mendez@mail.com')
        INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Activo)
        VALUES ('Carolina', 'Mendez', '11-5588-1200', 'carolina.mendez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Email = 'tomas.acosta@mail.com')
        INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Activo)
        VALUES ('Tomas', 'Acosta', '11-2345-9081', 'tomas.acosta@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Clientes WHERE Email = 'valentina.lopez@mail.com')
        INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Activo)
        VALUES ('Valentina', 'Lopez', '11-6789-2214', 'valentina.lopez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Empleados WHERE DNI = '30111222')
        INSERT INTO Empleados (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Carlos', 'Benitez', '30111222', '11-4000-1001', 'carlos.benitez@padelito.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Empleados WHERE DNI = '32654321')
        INSERT INTO Empleados (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Andrea', 'Molina', '32654321', '11-4000-1002', 'andrea.molina@padelito.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Empleados WHERE DNI = '28444777')
        INSERT INTO Empleados (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Federico', 'Castro', '28444777', '11-4000-1003', 'federico.castro@padelito.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Empleados WHERE DNI = '35123987')
        INSERT INTO Empleados (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Natalia', 'Rojas', '35123987', '11-4000-1004', 'natalia.rojas@padelito.com', 1);

    /* ========================================================
       USUARIOS DEL SISTEMA
       ======================================================== */

    DECLARE @IdRolAdministrador INT = (SELECT IdRol FROM Roles WHERE Descripcion = 'Administrador');
    DECLARE @IdRolEmpleado INT = (SELECT IdRol FROM Roles WHERE Descripcion = 'Empleado');
    DECLARE @IdRolRecepcion INT = (SELECT IdRol FROM Roles WHERE Descripcion = 'Recepcion');

    DECLARE @IdEmpleadoCarlos INT = (SELECT IdEmpleado FROM Empleados WHERE DNI = '30111222');
    DECLARE @IdEmpleadoAndrea INT = (SELECT IdEmpleado FROM Empleados WHERE DNI = '32654321');
    DECLARE @IdEmpleadoFederico INT = (SELECT IdEmpleado FROM Empleados WHERE DNI = '28444777');
    DECLARE @IdEmpleadoNatalia INT = (SELECT IdEmpleado FROM Empleados WHERE DNI = '35123987');

    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE NombreUsuario = 'admin')
       AND NOT EXISTS (SELECT 1 FROM Usuarios WHERE IdEmpleado = @IdEmpleadoCarlos)
        INSERT INTO Usuarios (NombreUsuario, Contrasenia, IdEmpleado, IdRol, Activo)
        VALUES ('admin', 'Admin123!', @IdEmpleadoCarlos, @IdRolAdministrador, 1);

    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE NombreUsuario = 'recepcion')
       AND NOT EXISTS (SELECT 1 FROM Usuarios WHERE IdEmpleado = @IdEmpleadoAndrea)
        INSERT INTO Usuarios (NombreUsuario, Contrasenia, IdEmpleado, IdRol, Activo)
        VALUES ('recepcion', 'Recepcion123!', @IdEmpleadoAndrea, @IdRolRecepcion, 1);

    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE NombreUsuario = 'empleado1')
       AND NOT EXISTS (SELECT 1 FROM Usuarios WHERE IdEmpleado = @IdEmpleadoFederico)
        INSERT INTO Usuarios (NombreUsuario, Contrasenia, IdEmpleado, IdRol, Activo)
        VALUES ('empleado1', 'Empleado123!', @IdEmpleadoFederico, @IdRolEmpleado, 1);

    IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE NombreUsuario = 'empleado2')
       AND NOT EXISTS (SELECT 1 FROM Usuarios WHERE IdEmpleado = @IdEmpleadoNatalia)
        INSERT INTO Usuarios (NombreUsuario, Contrasenia, IdEmpleado, IdRol, Activo)
        VALUES ('empleado2', 'Empleado123!', @IdEmpleadoNatalia, @IdRolEmpleado, 1);

    /* ========================================================
       CANCHAS Y TURNOS
       ======================================================== */

    DECLARE @IdTipoCemento INT = (SELECT IdTipoCancha FROM TiposCancha WHERE Descripcion = 'Cemento');
    DECLARE @IdTipoSintetico INT = (SELECT IdTipoCancha FROM TiposCancha WHERE Descripcion = 'Sintetico');
    DECLARE @IdTipoIndoor INT = (SELECT IdTipoCancha FROM TiposCancha WHERE Descripcion = 'Indoor');
    DECLARE @IdTipoPremium INT = (SELECT IdTipoCancha FROM TiposCancha WHERE Descripcion = 'Premium');

    IF NOT EXISTS (SELECT 1 FROM Canchas WHERE Nombre = 'Cancha 1')
        INSERT INTO Canchas (Nombre, IdTipoCancha, PrecioHora, Activa)
        VALUES ('Cancha 1', @IdTipoCemento, 8000.00, 1);

    IF NOT EXISTS (SELECT 1 FROM Canchas WHERE Nombre = 'Cancha 2')
        INSERT INTO Canchas (Nombre, IdTipoCancha, PrecioHora, Activa)
        VALUES ('Cancha 2', @IdTipoSintetico, 9500.00, 1);

    IF NOT EXISTS (SELECT 1 FROM Canchas WHERE Nombre = 'Cancha 3')
        INSERT INTO Canchas (Nombre, IdTipoCancha, PrecioHora, Activa)
        VALUES ('Cancha 3', @IdTipoIndoor, 11000.00, 1);

    IF NOT EXISTS (SELECT 1 FROM Canchas WHERE Nombre = 'Cancha Premium')
        INSERT INTO Canchas (Nombre, IdTipoCancha, PrecioHora, Activa)
        VALUES ('Cancha Premium', @IdTipoPremium, 13500.00, 1);

    IF NOT EXISTS (SELECT 1 FROM Canchas WHERE Nombre = 'Cancha Mantenimiento')
        INSERT INTO Canchas (Nombre, IdTipoCancha, PrecioHora, Activa)
        VALUES ('Cancha Mantenimiento', @IdTipoCemento, 7500.00, 0);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '08:00' AND HoraFin = '09:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('08:00', '09:00', 1);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '09:00' AND HoraFin = '10:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('09:00', '10:00', 1);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '10:00' AND HoraFin = '11:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('10:00', '11:00', 1);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '11:00' AND HoraFin = '12:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('11:00', '12:00', 1);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '17:00' AND HoraFin = '18:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('17:00', '18:00', 1);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '18:00' AND HoraFin = '19:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('18:00', '19:00', 1);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '19:00' AND HoraFin = '20:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('19:00', '20:00', 1);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '20:00' AND HoraFin = '21:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('20:00', '21:00', 1);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '21:00' AND HoraFin = '22:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('21:00', '22:00', 1);

    IF NOT EXISTS (SELECT 1 FROM TurnosDisponibles WHERE HoraInicio = '22:00' AND HoraFin = '23:00')
        INSERT INTO TurnosDisponibles (HoraInicio, HoraFin, Activo) VALUES ('22:00', '23:00', 1);

    /* ========================================================
       PROMOCIONES
       ======================================================== */

    IF NOT EXISTS (SELECT 1 FROM Promociones WHERE Nombre = 'Promo Maniana')
        INSERT INTO Promociones (Nombre, Descripcion, PorcentajeDescuento, FechaDesde, FechaHasta, Activa)
        VALUES ('Promo Maniana', 'Descuento para turnos de 08 a 12 hs.', 10.00, '2026-06-01', '2026-12-31', 1);

    IF NOT EXISTS (SELECT 1 FROM Promociones WHERE Nombre = 'Socios Frecuentes')
        INSERT INTO Promociones (Nombre, Descripcion, PorcentajeDescuento, FechaDesde, FechaHasta, Activa)
        VALUES ('Socios Frecuentes', 'Beneficio para clientes con reservas recurrentes.', 15.00, '2026-06-01', '2026-12-31', 1);

    IF NOT EXISTS (SELECT 1 FROM Promociones WHERE Nombre = 'Fin de Semana')
        INSERT INTO Promociones (Nombre, Descripcion, PorcentajeDescuento, FechaDesde, FechaHasta, Activa)
        VALUES ('Fin de Semana', 'Promocion especial para sabados y domingos.', 5.00, '2026-06-01', '2026-12-31', 1);

    IF NOT EXISTS (SELECT 1 FROM Promociones WHERE Nombre = 'Promo Vencida')
        INSERT INTO Promociones (Nombre, Descripcion, PorcentajeDescuento, FechaDesde, FechaHasta, Activa)
        VALUES ('Promo Vencida', 'Promocion historica inactiva para pruebas.', 20.00, '2025-01-01', '2025-12-31', 0);

    /* ========================================================
       RESERVAS
       ======================================================== */

    DECLARE @IdClienteJuan INT = (SELECT IdCliente FROM Clientes WHERE Email = 'juan.perez@mail.com');
    DECLARE @IdClienteMaria INT = (SELECT IdCliente FROM Clientes WHERE Email = 'maria.gomez@mail.com');
    DECLARE @IdClienteLucas INT = (SELECT IdCliente FROM Clientes WHERE Email = 'lucas.fernandez@mail.com');
    DECLARE @IdClienteSofia INT = (SELECT IdCliente FROM Clientes WHERE Email = 'sofia.ramirez@mail.com');
    DECLARE @IdClienteDiego INT = (SELECT IdCliente FROM Clientes WHERE Email = 'diego.suarez@mail.com');
    DECLARE @IdClienteCarolina INT = (SELECT IdCliente FROM Clientes WHERE Email = 'carolina.mendez@mail.com');
    DECLARE @IdClienteTomas INT = (SELECT IdCliente FROM Clientes WHERE Email = 'tomas.acosta@mail.com');
    DECLARE @IdClienteValentina INT = (SELECT IdCliente FROM Clientes WHERE Email = 'valentina.lopez@mail.com');

    DECLARE @IdCancha1 INT = (SELECT IdCancha FROM Canchas WHERE Nombre = 'Cancha 1');
    DECLARE @IdCancha2 INT = (SELECT IdCancha FROM Canchas WHERE Nombre = 'Cancha 2');
    DECLARE @IdCancha3 INT = (SELECT IdCancha FROM Canchas WHERE Nombre = 'Cancha 3');
    DECLARE @IdCanchaPremium INT = (SELECT IdCancha FROM Canchas WHERE Nombre = 'Cancha Premium');

    DECLARE @IdTurno0800 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE HoraInicio = '08:00' AND HoraFin = '09:00');
    DECLARE @IdTurno0900 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE HoraInicio = '09:00' AND HoraFin = '10:00');
    DECLARE @IdTurno1000 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE HoraInicio = '10:00' AND HoraFin = '11:00');
    DECLARE @IdTurno1700 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE HoraInicio = '17:00' AND HoraFin = '18:00');
    DECLARE @IdTurno1800 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE HoraInicio = '18:00' AND HoraFin = '19:00');
    DECLARE @IdTurno1900 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE HoraInicio = '19:00' AND HoraFin = '20:00');
    DECLARE @IdTurno2000 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE HoraInicio = '20:00' AND HoraFin = '21:00');
    DECLARE @IdTurno2100 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE HoraInicio = '21:00' AND HoraFin = '22:00');

    DECLARE @IdEstadoPendiente INT = (SELECT IdEstadoReserva FROM EstadosReserva WHERE Descripcion = 'Pendiente');
    DECLARE @IdEstadoConfirmada INT = (SELECT IdEstadoReserva FROM EstadosReserva WHERE Descripcion = 'Confirmada');
    DECLARE @IdEstadoCancelada INT = (SELECT IdEstadoReserva FROM EstadosReserva WHERE Descripcion = 'Cancelada');
    DECLARE @IdEstadoFinalizada INT = (SELECT IdEstadoReserva FROM EstadosReserva WHERE Descripcion = 'Finalizada');

    DECLARE @IdPromoManiana INT = (SELECT IdPromocion FROM Promociones WHERE Nombre = 'Promo Maniana');
    DECLARE @IdPromoSocios INT = (SELECT IdPromocion FROM Promociones WHERE Nombre = 'Socios Frecuentes');
    DECLARE @IdPromoFinSemana INT = (SELECT IdPromocion FROM Promociones WHERE Nombre = 'Fin de Semana');

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdCancha = @IdCancha1 AND FechaReserva = '2026-06-03' AND IdTurnoDisponible = @IdTurno0800)
        INSERT INTO Reservas (IdCliente, IdCancha, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteJuan, @IdCancha1, @IdTurno0800, @IdEmpleadoAndrea, @IdPromoManiana, '2026-06-03', @IdEstadoConfirmada, 8000.00, 7200.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdCancha = @IdCancha2 AND FechaReserva = '2026-06-03' AND IdTurnoDisponible = @IdTurno0900)
        INSERT INTO Reservas (IdCliente, IdCancha, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteMaria, @IdCancha2, @IdTurno0900, @IdEmpleadoAndrea, @IdPromoManiana, '2026-06-03', @IdEstadoPendiente, 9500.00, 8550.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdCancha = @IdCancha3 AND FechaReserva = '2026-06-04' AND IdTurnoDisponible = @IdTurno1800)
        INSERT INTO Reservas (IdCliente, IdCancha, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteLucas, @IdCancha3, @IdTurno1800, @IdEmpleadoFederico, NULL, '2026-06-04', @IdEstadoConfirmada, 11000.00, 11000.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdCancha = @IdCanchaPremium AND FechaReserva = '2026-06-04' AND IdTurnoDisponible = @IdTurno1900)
        INSERT INTO Reservas (IdCliente, IdCancha, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteSofia, @IdCanchaPremium, @IdTurno1900, @IdEmpleadoNatalia, @IdPromoSocios, '2026-06-04', @IdEstadoConfirmada, 13500.00, 11475.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdCancha = @IdCancha1 AND FechaReserva = '2026-06-05' AND IdTurnoDisponible = @IdTurno2000)
        INSERT INTO Reservas (IdCliente, IdCancha, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteDiego, @IdCancha1, @IdTurno2000, @IdEmpleadoAndrea, NULL, '2026-06-05', @IdEstadoFinalizada, 8000.00, 8000.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdCancha = @IdCancha2 AND FechaReserva = '2026-06-05' AND IdTurnoDisponible = @IdTurno2100)
        INSERT INTO Reservas (IdCliente, IdCancha, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteCarolina, @IdCancha2, @IdTurno2100, @IdEmpleadoFederico, NULL, '2026-06-05', @IdEstadoCancelada, 9500.00, 9500.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdCancha = @IdCancha3 AND FechaReserva = '2026-06-06' AND IdTurnoDisponible = @IdTurno1700)
        INSERT INTO Reservas (IdCliente, IdCancha, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteTomas, @IdCancha3, @IdTurno1700, @IdEmpleadoNatalia, @IdPromoFinSemana, '2026-06-06', @IdEstadoConfirmada, 11000.00, 10450.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE IdCancha = @IdCanchaPremium AND FechaReserva = '2026-06-07' AND IdTurnoDisponible = @IdTurno1000)
        INSERT INTO Reservas (IdCliente, IdCancha, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteValentina, @IdCanchaPremium, @IdTurno1000, @IdEmpleadoAndrea, @IdPromoManiana, '2026-06-07', @IdEstadoPendiente, 13500.00, 12150.00);

    /* ========================================================
       PAGOS
       ======================================================== */

    DECLARE @IdMetodoEfectivo INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Efectivo');
    DECLARE @IdMetodoDebito INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Tarjeta de debito');
    DECLARE @IdMetodoCredito INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Tarjeta de credito');
    DECLARE @IdMetodoTransferencia INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Transferencia');
    DECLARE @IdMetodoMercadoPago INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Mercado Pago');

    DECLARE @IdReservaJuan INT = (SELECT IdReserva FROM Reservas WHERE IdCancha = @IdCancha1 AND FechaReserva = '2026-06-03' AND IdTurnoDisponible = @IdTurno0800);
    DECLARE @IdReservaLucas INT = (SELECT IdReserva FROM Reservas WHERE IdCancha = @IdCancha3 AND FechaReserva = '2026-06-04' AND IdTurnoDisponible = @IdTurno1800);
    DECLARE @IdReservaSofia INT = (SELECT IdReserva FROM Reservas WHERE IdCancha = @IdCanchaPremium AND FechaReserva = '2026-06-04' AND IdTurnoDisponible = @IdTurno1900);
    DECLARE @IdReservaDiego INT = (SELECT IdReserva FROM Reservas WHERE IdCancha = @IdCancha1 AND FechaReserva = '2026-06-05' AND IdTurnoDisponible = @IdTurno2000);
    DECLARE @IdReservaTomas INT = (SELECT IdReserva FROM Reservas WHERE IdCancha = @IdCancha3 AND FechaReserva = '2026-06-06' AND IdTurnoDisponible = @IdTurno1700);

    IF NOT EXISTS (SELECT 1 FROM Pagos WHERE IdReserva = @IdReservaJuan AND Monto = 7200.00)
        INSERT INTO Pagos (IdReserva, IdMetodoPago, Monto, Observacion)
        VALUES (@IdReservaJuan, @IdMetodoTransferencia, 7200.00, 'Pago total anticipado.');

    IF NOT EXISTS (SELECT 1 FROM Pagos WHERE IdReserva = @IdReservaLucas AND Monto = 5500.00)
        INSERT INTO Pagos (IdReserva, IdMetodoPago, Monto, Observacion)
        VALUES (@IdReservaLucas, @IdMetodoEfectivo, 5500.00, 'Sena del 50%.');

    IF NOT EXISTS (SELECT 1 FROM Pagos WHERE IdReserva = @IdReservaSofia AND Monto = 11475.00)
        INSERT INTO Pagos (IdReserva, IdMetodoPago, Monto, Observacion)
        VALUES (@IdReservaSofia, @IdMetodoMercadoPago, 11475.00, 'Pago total con promocion.');

    IF NOT EXISTS (SELECT 1 FROM Pagos WHERE IdReserva = @IdReservaDiego AND Monto = 8000.00)
        INSERT INTO Pagos (IdReserva, IdMetodoPago, Monto, Observacion)
        VALUES (@IdReservaDiego, @IdMetodoDebito, 8000.00, 'Reserva finalizada y abonada.');

    IF NOT EXISTS (SELECT 1 FROM Pagos WHERE IdReserva = @IdReservaTomas AND Monto = 10450.00)
        INSERT INTO Pagos (IdReserva, IdMetodoPago, Monto, Observacion)
        VALUES (@IdReservaTomas, @IdMetodoCredito, 10450.00, 'Pago con tarjeta de credito.');

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO

/* ============================================================
   CONSULTAS DE CONTROL
   ============================================================ */

SELECT 'Clientes' AS Tabla, COUNT(*) AS Cantidad FROM Clientes
UNION ALL SELECT 'Empleados', COUNT(*) FROM Empleados
UNION ALL SELECT 'Roles', COUNT(*) FROM Roles
UNION ALL SELECT 'Usuarios', COUNT(*) FROM Usuarios
UNION ALL SELECT 'TiposCancha', COUNT(*) FROM TiposCancha
UNION ALL SELECT 'Canchas', COUNT(*) FROM Canchas
UNION ALL SELECT 'TurnosDisponibles', COUNT(*) FROM TurnosDisponibles
UNION ALL SELECT 'Promociones', COUNT(*) FROM Promociones
UNION ALL SELECT 'EstadosReserva', COUNT(*) FROM EstadosReserva
UNION ALL SELECT 'Reservas', COUNT(*) FROM Reservas
UNION ALL SELECT 'Pagos', COUNT(*) FROM Pagos
UNION ALL SELECT 'AuditoriaReservas', COUNT(*) FROM AuditoriaReservas;
GO
