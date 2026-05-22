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
                @"SELECT IdEmpleado, Nombre, Apellido, DNI, Telefono, Email, Activo, FechaAlta
                  FROM Empleados
                  ORDER BY Apellido, Nombre", conexion))
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
                @"SELECT IdEmpleado, Nombre, Apellido, DNI, Telefono, Email, Activo, FechaAlta
                  FROM Empleados
                  WHERE IdEmpleado = @IdEmpleado", conexion))
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
            using (SqlCommand comando = new SqlCommand(
                @"INSERT INTO Empleados (Nombre, Apellido, DNI, Telefono, Email, Activo)
                  VALUES (@Nombre, @Apellido, @DNI, @Telefono, @Email, @Activo)", conexion))
            {
                CargarParametros(comando, empleado);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Empleado empleado)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Empleados
                  SET Nombre = @Nombre,
                      Apellido = @Apellido,
                      DNI = @DNI,
                      Telefono = @Telefono,
                      Email = @Email,
                      Activo = @Activo
                  WHERE IdEmpleado = @IdEmpleado", conexion))
            {
                comando.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = empleado.IdEmpleado;
                CargarParametros(comando, empleado);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int idEmpleado)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Empleados
                  SET Activo = 0
                  WHERE IdEmpleado = @IdEmpleado", conexion))
            {
                comando.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = idEmpleado;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static void CargarParametros(SqlCommand comando, Empleado empleado)
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
