using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class UsuarioDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<Usuario> Listar()
        {
            var usuarios = new List<Usuario>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT u.IdUsuario, u.NombreUsuario, u.Contrasenia, u.IdEmpleado,
                         p.Apellido + ', ' + p.Nombre AS EmpleadoNombreCompleto,
                         u.IdRol, r.Descripcion AS RolDescripcion,
                         u.Activo, u.FechaAlta
                  FROM Usuarios u
                  INNER JOIN Empleados e ON e.IdEmpleado = u.IdEmpleado
                  INNER JOIN Personas p ON p.IdPersona = e.IdPersona
                  INNER JOIN Roles r ON r.IdRol = u.IdRol
                  ORDER BY u.NombreUsuario", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        usuarios.Add(MapearUsuario(lector));
                    }
                }
            }

            return usuarios;
        }

        public Usuario ObtenerPorId(int idUsuario)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT u.IdUsuario, u.NombreUsuario, u.Contrasenia, u.IdEmpleado,
                         p.Apellido + ', ' + p.Nombre AS EmpleadoNombreCompleto,
                         u.IdRol, r.Descripcion AS RolDescripcion,
                         u.Activo, u.FechaAlta
                  FROM Usuarios u
                  INNER JOIN Empleados e ON e.IdEmpleado = u.IdEmpleado
                  INNER JOIN Personas p ON p.IdPersona = e.IdPersona
                  INNER JOIN Roles r ON r.IdRol = u.IdRol
                  WHERE u.IdUsuario = @IdUsuario", conexion))
            {
                comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return lector.Read() ? MapearUsuario(lector) : null;
                }
            }
        }

        public List<Empleado> ListarEmpleadosDisponibles(int idEmpleadoActual)
        {
            var empleados = new List<Empleado>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT e.IdEmpleado, e.IdPersona, p.Nombre, p.Apellido, p.DNI, p.Telefono, p.Email,
                         p.Activo, p.FechaAlta
                  FROM Empleados e
                  INNER JOIN Personas p ON p.IdPersona = e.IdPersona
                  LEFT JOIN Usuarios u ON u.IdEmpleado = e.IdEmpleado
                  WHERE p.Activo = 1
                    AND (u.IdUsuario IS NULL OR e.IdEmpleado = @IdEmpleadoActual)
                  ORDER BY p.Apellido, p.Nombre", conexion))
            {
                comando.Parameters.Add("@IdEmpleadoActual", SqlDbType.Int).Value = idEmpleadoActual;
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

        public void Agregar(Usuario usuario)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"INSERT INTO Usuarios (NombreUsuario, Contrasenia, IdEmpleado, IdRol, Activo)
                  VALUES (@NombreUsuario, @Contrasenia, @IdEmpleado, @IdRol, @Activo)", conexion))
            {
                CargarParametros(comando, usuario);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Usuario usuario)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Usuarios
                  SET NombreUsuario = @NombreUsuario,
                      Contrasenia = @Contrasenia,
                      IdEmpleado = @IdEmpleado,
                      IdRol = @IdRol,
                      Activo = @Activo
                  WHERE IdUsuario = @IdUsuario", conexion))
            {
                comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = usuario.IdUsuario;
                CargarParametros(comando, usuario);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int idUsuario)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Usuarios
                  SET Activo = 0
                  WHERE IdUsuario = @IdUsuario", conexion))
            {
                comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = idUsuario;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public bool ExisteNombreUsuario(string nombreUsuario, int idUsuarioActual)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT COUNT(1)
                  FROM Usuarios
                  WHERE NombreUsuario = @NombreUsuario
                    AND IdUsuario <> @IdUsuarioActual", conexion))
            {
                comando.Parameters.Add("@NombreUsuario", SqlDbType.VarChar, 50).Value = nombreUsuario;
                comando.Parameters.Add("@IdUsuarioActual", SqlDbType.Int).Value = idUsuarioActual;
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }

        public bool ExisteEmpleadoAsignado(int idEmpleado, int idUsuarioActual)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT COUNT(1)
                  FROM Usuarios
                  WHERE IdEmpleado = @IdEmpleado
                    AND IdUsuario <> @IdUsuarioActual", conexion))
            {
                comando.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = idEmpleado;
                comando.Parameters.Add("@IdUsuarioActual", SqlDbType.Int).Value = idUsuarioActual;
                conexion.Open();
                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }

        private static void CargarParametros(SqlCommand comando, Usuario usuario)
        {
            comando.Parameters.Add("@NombreUsuario", SqlDbType.VarChar, 20).Value = usuario.NombreUsuario;
            comando.Parameters.Add("@Contrasenia", SqlDbType.VarChar, 15).Value = usuario.Contrasenia;
            comando.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = usuario.IdEmpleado;
            comando.Parameters.Add("@IdRol", SqlDbType.Int).Value = usuario.IdRol;
            comando.Parameters.Add("@Activo", SqlDbType.Bit).Value = usuario.Activo;
        }

        private static Usuario MapearUsuario(SqlDataReader lector)
        {
            return new Usuario
            {
                IdUsuario = Convert.ToInt32(lector["IdUsuario"]),
                NombreUsuario = lector["NombreUsuario"].ToString(),
                Contrasenia = lector["Contrasenia"].ToString(),
                IdEmpleado = Convert.ToInt32(lector["IdEmpleado"]),
                EmpleadoNombreCompleto = lector["EmpleadoNombreCompleto"].ToString(),
                IdRol = Convert.ToInt32(lector["IdRol"]),
                RolDescripcion = lector["RolDescripcion"].ToString(),
                Activo = Convert.ToBoolean(lector["Activo"]),
                FechaAlta = Convert.ToDateTime(lector["FechaAlta"])
            };
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
