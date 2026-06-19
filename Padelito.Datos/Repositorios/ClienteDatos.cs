using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class ClienteDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<Cliente> Listar()
        {
            var clientes = new List<Cliente>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT c.IdCliente, c.IdPersona, p.Nombre, p.Apellido, p.Telefono, p.Email,
                         p.Activo, p.FechaAlta
                  FROM Clientes c
                  INNER JOIN Personas p ON p.IdPersona = c.IdPersona
                  ORDER BY p.Apellido, p.Nombre", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        clientes.Add(MapearCliente(lector));
                    }
                }
            }

            return clientes;
        }

        public Cliente ObtenerPorId(int idCliente)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT c.IdCliente, c.IdPersona, p.Nombre, p.Apellido, p.Telefono, p.Email,
                         p.Activo, p.FechaAlta
                  FROM Clientes c
                  INNER JOIN Personas p ON p.IdPersona = c.IdPersona
                  WHERE c.IdCliente = @IdCliente", conexion))
            {
                comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return lector.Read() ? MapearCliente(lector) : null;
                }
            }
        }

        public void Agregar(Cliente cliente)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            {
                conexion.Open();

                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand comandoPersona = new SqlCommand(
                            @"INSERT INTO Personas (Nombre, Apellido, Telefono, Email, Activo)
                              VALUES (@Nombre, @Apellido, @Telefono, @Email, @Activo);
                              SELECT CAST(SCOPE_IDENTITY() AS INT);", conexion, transaccion))
                        {
                            CargarParametrosPersona(comandoPersona, cliente);
                            cliente.IdPersona = Convert.ToInt32(comandoPersona.ExecuteScalar());
                        }

                        using (SqlCommand comandoCliente = new SqlCommand(
                            @"INSERT INTO Clientes (IdPersona)
                              VALUES (@IdPersona)", conexion, transaccion))
                        {
                            comandoCliente.Parameters.Add("@IdPersona", SqlDbType.Int).Value = cliente.IdPersona;
                            comandoCliente.ExecuteNonQuery();
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

        public void Modificar(Cliente cliente)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE p
                  SET Nombre = @Nombre,
                      Apellido = @Apellido,
                      Telefono = @Telefono,
                      Email = @Email,
                      Activo = @Activo
                  FROM Personas p
                  INNER JOIN Clientes c ON c.IdPersona = p.IdPersona
                  WHERE c.IdCliente = @IdCliente", conexion))
            {
                comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = cliente.IdCliente;
                CargarParametrosPersona(comando, cliente);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int idCliente)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE p
                  SET Activo = 0
                  FROM Personas p
                  INNER JOIN Clientes c ON c.IdPersona = p.IdPersona
                  WHERE c.IdCliente = @IdCliente", conexion))
            {
                comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static void CargarParametrosPersona(SqlCommand comando, Cliente cliente)
        {
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = cliente.Nombre;
            comando.Parameters.Add("@Apellido", SqlDbType.VarChar, 50).Value = cliente.Apellido;
            comando.Parameters.Add("@Telefono", SqlDbType.VarChar, 30).Value = (object)cliente.Telefono ?? DBNull.Value;
            comando.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object)cliente.Email ?? DBNull.Value;
            comando.Parameters.Add("@Activo", SqlDbType.Bit).Value = cliente.Activo;
        }

        private static Cliente MapearCliente(SqlDataReader lector)
        {
            return new Cliente
            {
                IdCliente = Convert.ToInt32(lector["IdCliente"]),
                IdPersona = Convert.ToInt32(lector["IdPersona"]),
                Nombre = lector["Nombre"].ToString(),
                Apellido = lector["Apellido"].ToString(),
                Telefono = lector["Telefono"] == DBNull.Value ? string.Empty : lector["Telefono"].ToString(),
                Email = lector["Email"] == DBNull.Value ? string.Empty : lector["Email"].ToString(),
                Activo = Convert.ToBoolean(lector["Activo"]),
                FechaAlta = Convert.ToDateTime(lector["FechaAlta"])
            };
        }
    }
}
