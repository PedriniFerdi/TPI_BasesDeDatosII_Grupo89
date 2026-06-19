# Padelito 

Proyecto ASP.NET Web Forms con .NET Framework 4.8, SQL Server, ADO.NET y Bootstrap.

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
- Triggers: auditoria de `INSERT`, `UPDATE` y `DELETE` sobre `Reservas`.

## Proyectos de la solucion

La solucion `Padelito.sln` tiene cuatro proyectos:

- `Padelito.Web`: capa de presentacion con paginas `.aspx`.
- `Padelito.Negocio`: reglas simples, validaciones y coordinacion de operaciones.
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

Contiene clases y sus propiedades. En esta base inicial existe:

- `Cliente`

La clase coincide con la tabla `Clientes`.

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


## Flujo del CRUD Clientes

1. `Clientes.aspx` muestra formulario y grilla.
2. `Clientes.aspx.cs` captura eventos ABM
3. `ClienteNegocio` valida datos y decide la operacion.
4. `ClienteDatos` ejecuta SQL parametrizado contra `Clientes`.
5. La grilla se recarga desde la base.



