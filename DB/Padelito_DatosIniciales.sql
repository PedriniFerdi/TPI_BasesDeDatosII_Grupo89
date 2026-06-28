/* ============================================================
   PADELITO_DB - DATOS INICIALES / DATOS DE PRUEBA
   Ejecutar despues de Padelito_DB.sql, Padelito_Triggers.sql,
   Padelito_Vistas.sql y Padelito_StoredProcedures.sql.

   Este script evita duplicar datos si se ejecuta más de una vez.
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
       PERSONAS, CLIENTES Y EMPLEADOS
       ======================================================== */

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE Email = 'juan.perez@mail.com')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Juan', 'Perez', NULL, '11-2456-7788', 'juan.perez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE Email = 'maria.gomez@mail.com')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Maria', 'Gomez', NULL, '11-3654-2210', 'maria.gomez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE Email = 'lucas.fernandez@mail.com')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Lucas', 'Fernandez', NULL, '11-4890-1133', 'lucas.fernandez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE Email = 'sofia.ramirez@mail.com')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Sofia', 'Ramirez', NULL, '11-6021-9044', 'sofia.ramirez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE Email = 'diego.suarez@mail.com')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Diego', 'Suarez', NULL, '11-7012-3321', 'diego.suarez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE Email = 'carolina.mendez@mail.com')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Carolina', 'Mendez', NULL, '11-5588-1200', 'carolina.mendez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE Email = 'tomas.acosta@mail.com')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Tomas', 'Acosta', NULL, '11-2345-9081', 'tomas.acosta@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE Email = 'valentina.lopez@mail.com')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Valentina', 'Lopez', NULL, '11-6789-2214', 'valentina.lopez@mail.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE DNI = '30111222')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Carlos', 'Benitez', '30111222', '11-4000-1001', 'carlos.benitez@padelito.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE DNI = '32654321')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Andrea', 'Molina', '32654321', '11-4000-1002', 'andrea.molina@padelito.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE DNI = '28444777')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Federico', 'Castro', '28444777', '11-4000-1003', 'federico.castro@padelito.com', 1);

    IF NOT EXISTS (SELECT 1 FROM Personas WHERE DNI = '35123987')
        INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
        VALUES ('Natalia', 'Rojas', '35123987', '11-4000-1004', 'natalia.rojas@padelito.com', 1);

    INSERT INTO Clientes (IdPersona)
    SELECT p.IdPersona
    FROM Personas p
    WHERE p.Email IN (
        'juan.perez@mail.com',
        'maria.gomez@mail.com',
        'lucas.fernandez@mail.com',
        'sofia.ramirez@mail.com',
        'diego.suarez@mail.com',
        'carolina.mendez@mail.com',
        'tomas.acosta@mail.com',
        'valentina.lopez@mail.com'
    )
      AND NOT EXISTS (SELECT 1 FROM Clientes c WHERE c.IdPersona = p.IdPersona);

    INSERT INTO Empleados (IdPersona)
    SELECT p.IdPersona
    FROM Personas p
    WHERE p.DNI IN ('30111222', '32654321', '28444777', '35123987')
      AND NOT EXISTS (SELECT 1 FROM Empleados e WHERE e.IdPersona = p.IdPersona);

    /* ========================================================
       USUARIOS DEL SISTEMA
       ======================================================== */

    DECLARE @IdRolAdministrador INT = (SELECT IdRol FROM Roles WHERE Descripcion = 'Administrador');
    DECLARE @IdRolEmpleado INT = (SELECT IdRol FROM Roles WHERE Descripcion = 'Empleado');
    DECLARE @IdRolRecepcion INT = (SELECT IdRol FROM Roles WHERE Descripcion = 'Recepcion');

    DECLARE @IdEmpleadoCarlos INT = (
        SELECT e.IdEmpleado
        FROM Empleados e
        INNER JOIN Personas p ON e.IdPersona = p.IdPersona
        WHERE p.DNI = '30111222'
    );
    DECLARE @IdEmpleadoAndrea INT = (
        SELECT e.IdEmpleado
        FROM Empleados e
        INNER JOIN Personas p ON e.IdPersona = p.IdPersona
        WHERE p.DNI = '32654321'
    );
    DECLARE @IdEmpleadoFederico INT = (
        SELECT e.IdEmpleado
        FROM Empleados e
        INNER JOIN Personas p ON e.IdPersona = p.IdPersona
        WHERE p.DNI = '28444777'
    );
    DECLARE @IdEmpleadoNatalia INT = (
        SELECT e.IdEmpleado
        FROM Empleados e
        INNER JOIN Personas p ON e.IdPersona = p.IdPersona
        WHERE p.DNI = '35123987'
    );

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

    INSERT INTO TurnosDisponibles (IdCancha, HoraInicio, HoraFin, Activo)
    SELECT c.IdCancha, h.HoraInicio, h.HoraFin, 1
    FROM Canchas c
    CROSS JOIN (
        VALUES
            (CAST('08:00' AS TIME), CAST('09:00' AS TIME)),
            (CAST('09:00' AS TIME), CAST('10:00' AS TIME)),
            (CAST('10:00' AS TIME), CAST('11:00' AS TIME)),
            (CAST('11:00' AS TIME), CAST('12:00' AS TIME)),
            (CAST('17:00' AS TIME), CAST('18:00' AS TIME)),
            (CAST('18:00' AS TIME), CAST('19:00' AS TIME)),
            (CAST('19:00' AS TIME), CAST('20:00' AS TIME)),
            (CAST('20:00' AS TIME), CAST('21:00' AS TIME)),
            (CAST('21:00' AS TIME), CAST('22:00' AS TIME)),
            (CAST('22:00' AS TIME), CAST('23:00' AS TIME))
    ) h(HoraInicio, HoraFin)
    WHERE c.Activa = 1
      AND NOT EXISTS (
          SELECT 1
          FROM TurnosDisponibles td
          WHERE td.IdCancha = c.IdCancha
            AND td.HoraInicio = h.HoraInicio
            AND td.HoraFin = h.HoraFin
      );

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

    DECLARE @IdClienteJuan INT = (
        SELECT c.IdCliente FROM Clientes c INNER JOIN Personas p ON c.IdPersona = p.IdPersona WHERE p.Email = 'juan.perez@mail.com'
    );
    DECLARE @IdClienteMaria INT = (
        SELECT c.IdCliente FROM Clientes c INNER JOIN Personas p ON c.IdPersona = p.IdPersona WHERE p.Email = 'maria.gomez@mail.com'
    );
    DECLARE @IdClienteLucas INT = (
        SELECT c.IdCliente FROM Clientes c INNER JOIN Personas p ON c.IdPersona = p.IdPersona WHERE p.Email = 'lucas.fernandez@mail.com'
    );
    DECLARE @IdClienteSofia INT = (
        SELECT c.IdCliente FROM Clientes c INNER JOIN Personas p ON c.IdPersona = p.IdPersona WHERE p.Email = 'sofia.ramirez@mail.com'
    );
    DECLARE @IdClienteDiego INT = (
        SELECT c.IdCliente FROM Clientes c INNER JOIN Personas p ON c.IdPersona = p.IdPersona WHERE p.Email = 'diego.suarez@mail.com'
    );
    DECLARE @IdClienteCarolina INT = (
        SELECT c.IdCliente FROM Clientes c INNER JOIN Personas p ON c.IdPersona = p.IdPersona WHERE p.Email = 'carolina.mendez@mail.com'
    );
    DECLARE @IdClienteTomas INT = (
        SELECT c.IdCliente FROM Clientes c INNER JOIN Personas p ON c.IdPersona = p.IdPersona WHERE p.Email = 'tomas.acosta@mail.com'
    );
    DECLARE @IdClienteValentina INT = (
        SELECT c.IdCliente FROM Clientes c INNER JOIN Personas p ON c.IdPersona = p.IdPersona WHERE p.Email = 'valentina.lopez@mail.com'
    );

    DECLARE @IdCancha1 INT = (SELECT IdCancha FROM Canchas WHERE Nombre = 'Cancha 1');
    DECLARE @IdCancha2 INT = (SELECT IdCancha FROM Canchas WHERE Nombre = 'Cancha 2');
    DECLARE @IdCancha3 INT = (SELECT IdCancha FROM Canchas WHERE Nombre = 'Cancha 3');
    DECLARE @IdCanchaPremium INT = (SELECT IdCancha FROM Canchas WHERE Nombre = 'Cancha Premium');

    DECLARE @IdTurnoC1_0800 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE IdCancha = @IdCancha1 AND HoraInicio = '08:00' AND HoraFin = '09:00');
    DECLARE @IdTurnoC2_0900 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE IdCancha = @IdCancha2 AND HoraInicio = '09:00' AND HoraFin = '10:00');
    DECLARE @IdTurnoC3_1800 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE IdCancha = @IdCancha3 AND HoraInicio = '18:00' AND HoraFin = '19:00');
    DECLARE @IdTurnoPremium_1900 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE IdCancha = @IdCanchaPremium AND HoraInicio = '19:00' AND HoraFin = '20:00');
    DECLARE @IdTurnoC1_2000 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE IdCancha = @IdCancha1 AND HoraInicio = '20:00' AND HoraFin = '21:00');
    DECLARE @IdTurnoC2_2100 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE IdCancha = @IdCancha2 AND HoraInicio = '21:00' AND HoraFin = '22:00');
    DECLARE @IdTurnoC3_1700 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE IdCancha = @IdCancha3 AND HoraInicio = '17:00' AND HoraFin = '18:00');
    DECLARE @IdTurnoPremium_1000 INT = (SELECT IdTurnoDisponible FROM TurnosDisponibles WHERE IdCancha = @IdCanchaPremium AND HoraInicio = '10:00' AND HoraFin = '11:00');

    DECLARE @IdEstadoPendiente INT = (SELECT IdEstadoReserva FROM EstadosReserva WHERE Descripcion = 'Pendiente');
    DECLARE @IdEstadoConfirmada INT = (SELECT IdEstadoReserva FROM EstadosReserva WHERE Descripcion = 'Confirmada');
    DECLARE @IdEstadoCancelada INT = (SELECT IdEstadoReserva FROM EstadosReserva WHERE Descripcion = 'Cancelada');
    DECLARE @IdEstadoFinalizada INT = (SELECT IdEstadoReserva FROM EstadosReserva WHERE Descripcion = 'Finalizada');

    DECLARE @IdPromoManiana INT = (SELECT IdPromocion FROM Promociones WHERE Nombre = 'Promo Maniana');
    DECLARE @IdPromoSocios INT = (SELECT IdPromocion FROM Promociones WHERE Nombre = 'Socios Frecuentes');
    DECLARE @IdPromoFinSemana INT = (SELECT IdPromocion FROM Promociones WHERE Nombre = 'Fin de Semana');

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE FechaReserva = '2026-06-03' AND IdTurnoDisponible = @IdTurnoC1_0800)
        INSERT INTO Reservas (IdCliente, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteJuan, @IdTurnoC1_0800, @IdEmpleadoAndrea, @IdPromoManiana, '2026-06-03', @IdEstadoConfirmada, 8000.00, 7200.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE FechaReserva = '2026-06-03' AND IdTurnoDisponible = @IdTurnoC2_0900)
        INSERT INTO Reservas (IdCliente, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteMaria, @IdTurnoC2_0900, @IdEmpleadoAndrea, @IdPromoManiana, '2026-06-03', @IdEstadoPendiente, 9500.00, 8550.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE FechaReserva = '2026-06-04' AND IdTurnoDisponible = @IdTurnoC3_1800)
        INSERT INTO Reservas (IdCliente, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteLucas, @IdTurnoC3_1800, @IdEmpleadoFederico, NULL, '2026-06-04', @IdEstadoConfirmada, 11000.00, 11000.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE FechaReserva = '2026-06-04' AND IdTurnoDisponible = @IdTurnoPremium_1900)
        INSERT INTO Reservas (IdCliente, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteSofia, @IdTurnoPremium_1900, @IdEmpleadoNatalia, @IdPromoSocios, '2026-06-04', @IdEstadoConfirmada, 13500.00, 11475.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE FechaReserva = '2026-06-05' AND IdTurnoDisponible = @IdTurnoC1_2000)
        INSERT INTO Reservas (IdCliente, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteDiego, @IdTurnoC1_2000, @IdEmpleadoAndrea, NULL, '2026-06-05', @IdEstadoFinalizada, 8000.00, 8000.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE FechaReserva = '2026-06-05' AND IdTurnoDisponible = @IdTurnoC2_2100)
        INSERT INTO Reservas (IdCliente, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteCarolina, @IdTurnoC2_2100, @IdEmpleadoFederico, NULL, '2026-06-05', @IdEstadoCancelada, 9500.00, 9500.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE FechaReserva = '2026-06-06' AND IdTurnoDisponible = @IdTurnoC3_1700)
        INSERT INTO Reservas (IdCliente, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteTomas, @IdTurnoC3_1700, @IdEmpleadoNatalia, @IdPromoFinSemana, '2026-06-06', @IdEstadoConfirmada, 11000.00, 10450.00);

    IF NOT EXISTS (SELECT 1 FROM Reservas WHERE FechaReserva = '2026-06-07' AND IdTurnoDisponible = @IdTurnoPremium_1000)
        INSERT INTO Reservas (IdCliente, IdTurnoDisponible, IdEmpleado, IdPromocion, FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
        VALUES (@IdClienteValentina, @IdTurnoPremium_1000, @IdEmpleadoAndrea, @IdPromoManiana, '2026-06-07', @IdEstadoPendiente, 13500.00, 12150.00);

    /* ========================================================
       PAGOS
       ======================================================== */

    DECLARE @IdMetodoEfectivo INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Efectivo');
    DECLARE @IdMetodoDebito INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Tarjeta de debito');
    DECLARE @IdMetodoCredito INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Tarjeta de credito');
    DECLARE @IdMetodoTransferencia INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Transferencia');
    DECLARE @IdMetodoMercadoPago INT = (SELECT IdMetodoPago FROM MetodosPago WHERE Descripcion = 'Mercado Pago');

    DECLARE @IdReservaJuan INT = (SELECT IdReserva FROM Reservas WHERE FechaReserva = '2026-06-03' AND IdTurnoDisponible = @IdTurnoC1_0800);
    DECLARE @IdReservaLucas INT = (SELECT IdReserva FROM Reservas WHERE FechaReserva = '2026-06-04' AND IdTurnoDisponible = @IdTurnoC3_1800);
    DECLARE @IdReservaSofia INT = (SELECT IdReserva FROM Reservas WHERE FechaReserva = '2026-06-04' AND IdTurnoDisponible = @IdTurnoPremium_1900);
    DECLARE @IdReservaDiego INT = (SELECT IdReserva FROM Reservas WHERE FechaReserva = '2026-06-05' AND IdTurnoDisponible = @IdTurnoC1_2000);
    DECLARE @IdReservaTomas INT = (SELECT IdReserva FROM Reservas WHERE FechaReserva = '2026-06-06' AND IdTurnoDisponible = @IdTurnoC3_1700);

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

SELECT 'Personas' AS Tabla, COUNT(*) AS Cantidad FROM Personas
UNION ALL SELECT 'Clientes', COUNT(*) FROM Clientes
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
