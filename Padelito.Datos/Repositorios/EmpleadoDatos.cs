using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class EmpleadoDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<Empleado> Listar()
        {
            var empleados = new List<Empleado>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT e.IdEmpleado, e.IdPersona, p.Nombre, p.Apellido, p.DNI, p.Telefono, p.Email,
                         p.Activo, p.FechaAlta
                  FROM Empleados e
                  INNER JOIN Personas p ON p.IdPersona = e.IdPersona
                  ORDER BY p.Apellido, p.Nombre", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        empleados.Add(MapearEmpleado(lector));
                    }
                }
            }

            return empleados;
        }

        public Empleado ObtenerPorId(int idEmpleado)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT e.IdEmpleado, e.IdPersona, p.Nombre, p.Apellido, p.DNI, p.Telefono, p.Email,
                         p.Activo, p.FechaAlta
                  FROM Empleados e
                  INNER JOIN Personas p ON p.IdPersona = e.IdPersona
                  WHERE e.IdEmpleado = @IdEmpleado", conexion))
            {
                comando.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = idEmpleado;
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return lector.Read() ? MapearEmpleado(lector) : null;
                }
            }
        }

        public void Agregar(Empleado empleado)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand comandoPersona = new SqlCommand(
                            @"INSERT INTO Personas (Nombre, Apellido, DNI, Telefono, Email, Activo)
                              VALUES (@Nombre, @Apellido, @DNI, @Telefono, @Email, @Activo);
                              SELECT CAST(SCOPE_IDENTITY() AS INT);", conexion, transaccion))
                        {
                            CargarParametrosPersona(comandoPersona, empleado);
                            empleado.IdPersona = Convert.ToInt32(comandoPersona.ExecuteScalar());
                        }

                        using (SqlCommand comandoEmpleado = new SqlCommand(
                            @"INSERT INTO Empleados (IdPersona)
                              VALUES (@IdPersona)", conexion, transaccion))
                        {
                            comandoEmpleado.Parameters.Add("@IdPersona", SqlDbType.Int).Value = empleado.IdPersona;
                            comandoEmpleado.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Modificar(Empleado empleado)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE p
                  SET Nombre = @Nombre,
                      Apellido = @Apellido,
                      DNI = @DNI,
                      Telefono = @Telefono,
                      Email = @Email,
                      Activo = @Activo
                  FROM Personas p
                  INNER JOIN Empleados e ON e.IdPersona = p.IdPersona
                  WHERE e.IdEmpleado = @IdEmpleado", conexion))
            {
                comando.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = empleado.IdEmpleado;
                CargarParametrosPersona(comando, empleado);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int idEmpleado)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE p
                  SET Activo = 0
                  FROM Personas p
                  INNER JOIN Empleados e ON e.IdPersona = p.IdPersona
                  WHERE e.IdEmpleado = @IdEmpleado", conexion))
            {
                comando.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = idEmpleado;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static void CargarParametrosPersona(SqlCommand comando, Empleado empleado)
        {
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = empleado.Nombre;
            comando.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = empleado.Apellido;
            comando.Parameters.Add("@DNI", SqlDbType.VarChar, 20).Value = empleado.DNI;
            comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 30).Value = (object)empleado.Telefono ?? DBNull.Value;
            comando.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object)empleado.Email ?? DBNull.Value;
            comando.Parameters.Add("@Activo", SqlDbType.Bit).Value = empleado.Activo;
        }

        private static Empleado MapearEmpleado(SqlDataReader lector)
        {
            return new Empleado
            {
                IdEmpleado = Convert.ToInt32(lector["IdEmpleado"]),
                IdPersona = Convert.ToInt32(lector["IdPersona"]),
                Nombre = lector["Nombre"].ToString(),
                Apellido = lector["Apellido"].ToString(),
                DNI = lector["DNI"].ToString(),
                Telefono = lector["Telefono"] == DBNull.Value ? string.Empty : lector["Telefono"].ToString(),
                Email = lector["Email"] == DBNull.Value ? string.Empty : lector["Email"].ToString(),
                Activo = Convert.ToBoolean(lector["Activo"]),
                FechaAlta = Convert.ToDateTime(lector["FechaAlta"])
            };
        }
    }
}
