# Padelito 

Proyecto ASP.NET Web Forms con .NET Framework 4.8, SQL Server, ADO.NET y Bootstrap.

## Estado actual

El sistema cuenta con pantallas funcionales para Clientes, Empleados, Tipos de cancha, Canchas, Turnos disponibles, Promociones, Usuarios, Reservas, Pagos, Reportes y Auditoria de reservas.

El panel general muestra clientes activos, canchas activas, reservas del dia, ingresos registrados y ultimas reservas.


## Base de datos 

La base se llama `PADELITO_DB`.

Tablas principales:

- `Clientes`: personas que reservan canchas.
- `Empleados`: personal del club.
- `Usuarios`: credenciales asociadas a empleados.
- `TiposCancha` y `Canchas`: catalogo de canchas y precios.
- `TurnosDisponibles`: horarios disponibles.
- `Promociones`: descuentos vigentes.
- `EstadosReserva` y `Reservas`: nucleo de reservas.
- `MetodosPago` y `Pagos`: pagos asociados a reservas.
- `AuditoriaReservas`: tabla usada por triggers.

Objetos SQL existentes:

- Vistas: `VW_ReservasDetalle`, `VW_PagosDetalle`, `VW_CanchasActivas`.
- Stored procedures: `SP_ReporteReservasPorFecha`, `SP_CambiarEstadoReserva`.
- Triggers: auditoria de `INSERT` y `UPDATE` sobre `Reservas`.

Orden recomendado de ejecucion de scripts:

1. `Padelito_DB.sql`
2. `Padelito_DatosIniciales.sql`
3. `Padelito_Vistas.sql`
4. `Padelito_StoredProcedures.sql`
5. `Padelito_Triggers.sql`

Los scripts dejan cargados datos iniciales suficientes para recorrer la demo sin cargar todos los catalogos desde cero

## Proyectos de la solucion

La solucion `Padelito.sln` tiene cuatro proyectos:

- `Padelito.Web`: capa de presentacion con paginas `.aspx`.
- `Padelito.Negocio`: reglas de negocio, validaciones y coordinacion de operaciones.
- `Padelito.Dominio`: clases que representan entidades de la base de datos.
- `Padelito.Datos`: acceso a SQL Server usando ADO.NET.

## Responsabilidad de cada capa

### Presentacion

Contiene Web Forms, controles ASP.NET, Bootstrap y eventos de pantalla.

No escribe SQL. Su tarea es tomar datos del formulario, llamar a Negocio y mostrar resultados.

### Negocio

Contiene las principales reglas del sistema

- validar campos obligatorios;
- decidir si se inserta o modifica;
- centralizar mensajes de error;
- llamar a la capa Datos.

### Dominio

Contiene clases y sus propiedades para las entidades principales, modelos de detalle y resumenes usados por la interfaz.

### Datos

Contiene ADO.NET clasico:

- `SqlConnection`
- `SqlCommand`
- `SqlDataReader`
- parametros SQL

Esta capa conoce las tablas y las consultas SQL.

## Cadena de conexion

Esta en `Padelito.Web/Web.config`:

```xml
<add name="PadelitoDB"
     connectionString="Server=localhost;Database=PADELITO_DB;Integrated Security=True;TrustServerCertificate=True"
     providerName="System.Data.SqlClient" />
```

Si la instancia local cambia, ajustar solamente `Server`.

## Como ejecutar el proyecto

Requisitos:

- Visual Studio o entorno compatible con ASP.NET Web Forms y .NET Framework 4.8.
- SQL Server local.
- IIS Express.

Pasos:

1. Abrir SQL Server Management Studio o una herramienta equivalente.
2. Ejecutar los scripts SQL en el orden indicado en la seccion Base de datos.
3. Abrir `Padelito.sln`.
4. Verificar en `Padelito.Web/Web.config` que la cadena de conexion apunte a la instancia local correcta.
5. Compilar la solucion.
6. Ejecutar `Padelito.Web` con IIS Express.
7. Ingresar desde `Login.aspx` con un usuario de prueba.
8. Recorrer los modulos desde el menu lateral.



## Decisiones academicas

- Se usa ADO.NET clasico para el acceso a datos.
- La solucion mantiene capas simples: Web, Negocio, Datos y Dominio.
- No se usa Entity Framework
- Las contraseñas de usuarios son datos de prueba academica y no representan un esquema productivo de seguridad.
- No se implementa autenticacion productiva porque el objetivo del trabajo es demostrar modelo de datos, capas, ABM, reservas, pagos, reportes y auditoria.
- El login implementado es academico: valida usuarios activos y roles, pero no incluye hashing, ni recuperación de contraseña

