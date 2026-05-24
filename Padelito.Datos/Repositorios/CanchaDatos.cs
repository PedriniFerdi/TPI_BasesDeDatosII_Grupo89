using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class CanchaDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<Cancha> Listar()
        {
            var canchas = new List<Cancha>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT c.IdCancha, c.Nombre, c.IdTipoCancha, tc.Descripcion AS TipoCanchaDescripcion,
                         c.PrecioHora, c.Activa
                  FROM Canchas c
                  INNER JOIN TiposCancha tc ON tc.IdTipoCancha = c.IdTipoCancha
                  ORDER BY c.Nombre", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        canchas.Add(MapearCancha(lector));
                    }
                }
            }

            return canchas;
        }

        public Cancha ObtenerPorId(int idCancha)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT c.IdCancha, c.Nombre, c.IdTipoCancha, tc.Descripcion AS TipoCanchaDescripcion,
                         c.PrecioHora, c.Activa
                  FROM Canchas c
                  INNER JOIN TiposCancha tc ON tc.IdTipoCancha = c.IdTipoCancha
                  WHERE c.IdCancha = @IdCancha", conexion))
            {
                comando.Parameters.Add("@IdCancha", SqlDbType.Int).Value = idCancha;
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return lector.Read() ? MapearCancha(lector) : null;
                }
            }
        }

        public void Agregar(Cancha cancha)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"INSERT INTO Canchas (Nombre, IdTipoCancha, PrecioHora, Activa)
                  VALUES (@Nombre, @IdTipoCancha, @PrecioHora, @Activa)", conexion))
            {
                CargarParametros(comando, cancha);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Cancha cancha)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Canchas
                  SET Nombre = @Nombre,
                      IdTipoCancha = @IdTipoCancha,
                      PrecioHora = @PrecioHora,
                      Activa = @Activa
                  WHERE IdCancha = @IdCancha", conexion))
            {
                comando.Parameters.Add("@IdCancha", SqlDbType.Int).Value = cancha.IdCancha;
                CargarParametros(comando, cancha);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int idCancha)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Canchas
                  SET Activa = 0
                  WHERE IdCancha = @IdCancha", conexion))
            {
                comando.Parameters.Add("@IdCancha", SqlDbType.Int).Value = idCancha;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static void CargarParametros(SqlCommand comando, Cancha cancha)
        {
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 80).Value = cancha.Nombre;
            comando.Parameters.Add("@IdTipoCancha", SqlDbType.Int).Value = cancha.IdTipoCancha;
            comando.Parameters.Add("@PrecioHora", SqlDbType.Decimal).Value = cancha.PrecioHora;
            comando.Parameters["@PrecioHora"].Precision = 10;
            comando.Parameters["@PrecioHora"].Scale = 2;
            comando.Parameters.Add("@Activa", SqlDbType.Bit).Value = cancha.Activa;
        }

        private static Cancha MapearCancha(SqlDataReader lector)
        {
            return new Cancha
            {
                IdCancha = Convert.ToInt32(lector["IdCancha"]),
                Nombre = lector["Nombre"].ToString(),
                IdTipoCancha = Convert.ToInt32(lector["IdTipoCancha"]),
                TipoCanchaDescripcion = lector["TipoCanchaDescripcion"].ToString(),
                PrecioHora = Convert.ToDecimal(lector["PrecioHora"]),
                Activa = Convert.ToBoolean(lector["Activa"])
            };
        }
    }
}
