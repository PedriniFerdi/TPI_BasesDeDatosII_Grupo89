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
                @"SELECT IdCliente, Nombre, Apellido, Telefono, Email, Activo, FechaAlta
                  FROM Clientes
                  ORDER BY Apellido, Nombre", conexion))
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
                @"SELECT IdCliente, Nombre, Apellido, Telefono, Email, Activo, FechaAlta
                  FROM Clientes
                  WHERE IdCliente = @IdCliente", conexion))
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
            using (SqlCommand comando = new SqlCommand(
                @"INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Activo)
                  VALUES (@Nombre, @Apellido, @Telefono, @Email, @Activo)", conexion))
            {
                CargarParametros(comando, cliente);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Cliente cliente)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Clientes
                  SET Nombre = @Nombre,
                      Apellido = @Apellido,
                      Telefono = @Telefono,
                      Email = @Email,
                      Activo = @Activo
                  WHERE IdCliente = @IdCliente", conexion))
            {
                comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = cliente.IdCliente;
                CargarParametros(comando, cliente);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int idCliente)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Clientes
                  SET Activo = 0
                  WHERE IdCliente = @IdCliente", conexion))
            {
                comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = idCliente;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static void CargarParametros(SqlCommand comando, Cliente cliente)
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
