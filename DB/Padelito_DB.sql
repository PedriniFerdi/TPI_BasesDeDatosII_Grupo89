/* ============================================================
   PADELITO_DB
   ============================================================ */

USE master;
GO

IF DB_ID(N'PADELITO_DB') IS NOT NULL
BEGIN
    ALTER DATABASE PADELITO_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PADELITO_DB;
END
GO

CREATE DATABASE PADELITO_DB;
GO

USE PADELITO_DB;
GO

/* ============================================================
   TABLAS
   ============================================================ */

CREATE TABLE Clientes(
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Telefono VARCHAR(30),
    Email VARCHAR(100),
    Activo BIT NOT NULL DEFAULT 1,
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE()
);
GO


CREATE TABLE Empleados(
    IdEmpleado INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    DNI VARCHAR(20) NOT NULL UNIQUE,
    Telefono VARCHAR(30),
    Email VARCHAR(100),
    Activo BIT NOT NULL DEFAULT 1,
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE()
);
GO


CREATE TABLE Roles(
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL UNIQUE
);
GO


CREATE TABLE Usuarios(
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasenia VARCHAR(255) NOT NULL,
    IdEmpleado INT NOT NULL UNIQUE,
    IdRol INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaAlta DATETIME NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (IdEmpleado) REFERENCES Empleados(IdEmpleado),
    FOREIGN KEY (IdRol) REFERENCES Roles(IdRol)
);
GO

CREATE TABLE TiposCancha(
    IdTipoCancha INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(80) NOT NULL UNIQUE
);
GO

CREATE TABLE Canchas(
    IdCancha INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(80) NOT NULL UNIQUE,
    IdTipoCancha INT NOT NULL,
    PrecioHora DECIMAL(10,2) NOT NULL CHECK (PrecioHora >= 0),
    Activa BIT NOT NULL DEFAULT 1,

    FOREIGN KEY (IdTipoCancha) REFERENCES TiposCancha(IdTipoCancha)
);
GO

CREATE TABLE TurnosDisponibles(
    IdTurnoDisponible INT IDENTITY(1,1) PRIMARY KEY,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,

    CHECK (HoraFin > HoraInicio),
    UNIQUE (HoraInicio, HoraFin)
);
GO

CREATE TABLE Promociones(
    IdPromocion INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(80) NOT NULL,
    Descripcion VARCHAR(255),
    PorcentajeDescuento DECIMAL(5,2) NOT NULL CHECK (PorcentajeDescuento BETWEEN 0 AND 100),
    FechaDesde DATE NOT NULL,
    FechaHasta DATE NOT NULL,
    Activa BIT NOT NULL DEFAULT 1,

    CHECK (FechaHasta >= FechaDesde)
);
GO

CREATE TABLE EstadosReserva(
    IdEstadoReserva INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE Reservas(
    IdReserva INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente INT NOT NULL,
    IdCancha INT NOT NULL,
    IdTurnoDisponible INT NOT NULL,
    IdEmpleado INT NOT NULL,
    IdPromocion INT NULL,
    FechaReserva DATE NOT NULL,
    IdEstadoReserva INT NOT NULL,
    PrecioBase DECIMAL(10,2) NOT NULL CHECK (PrecioBase >= 0),
    PrecioFinal DECIMAL(10,2) NOT NULL CHECK (PrecioFinal >= 0),
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT UQ_Reservas_Cancha_Fecha_Turno UNIQUE (IdCancha, FechaReserva, IdTurnoDisponible),

    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (IdCancha) REFERENCES Canchas(IdCancha),
    FOREIGN KEY (IdTurnoDisponible) REFERENCES TurnosDisponibles(IdTurnoDisponible),
    FOREIGN KEY (IdEmpleado) REFERENCES Empleados(IdEmpleado),
    FOREIGN KEY (IdPromocion) REFERENCES Promociones(IdPromocion),
    FOREIGN KEY (IdEstadoReserva) REFERENCES EstadosReserva(IdEstadoReserva)
);
GO

CREATE TABLE MetodosPago(
    IdMetodoPago INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE Pagos(
    IdPago INT IDENTITY(1,1) PRIMARY KEY,
    IdReserva INT NOT NULL,
    IdMetodoPago INT NOT NULL,
    Monto DECIMAL(10,2) NOT NULL CHECK (Monto > 0),
    FechaPago DATETIME NOT NULL DEFAULT GETDATE(),
    Observacion VARCHAR(255),

    FOREIGN KEY (IdReserva) REFERENCES Reservas(IdReserva),
    FOREIGN KEY (IdMetodoPago) REFERENCES MetodosPago(IdMetodoPago)
);
GO

CREATE TABLE AuditoriaReservas(
    IdAuditoria INT IDENTITY(1,1) PRIMARY KEY,
    IdReserva INT NOT NULL,
    Accion VARCHAR(20) NOT NULL CHECK (Accion IN ('INSERT', 'UPDATE', 'DELETE')),
    Descripcion VARCHAR(500) NOT NULL,
    FechaAccion DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioSistema VARCHAR(128) NOT NULL
);
GO
