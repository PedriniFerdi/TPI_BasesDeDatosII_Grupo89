using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class TiposCanchaDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<TiposCancha> Listar()
        {
            var tiposCancha = new List<TiposCancha>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdTipoCancha, Descripcion
                  FROM TiposCancha
                  ORDER BY Descripcion", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        tiposCancha.Add(MapearTipoCancha(lector));
                    }
                }
            }

            return tiposCancha;
        }

        public TiposCancha ObtenerPorId(int idTipoCancha)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdTipoCancha, Descripcion
                  FROM TiposCancha
                  WHERE IdTipoCancha = @IdTipoCancha", conexion))
            {
                comando.Parameters.Add("@IdTipoCancha", SqlDbType.Int).Value = idTipoCancha;
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return lector.Read() ? MapearTipoCancha(lector) : null;
                }
            }
        }

        public void Agregar(TiposCancha tipoCancha)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"INSERT INTO TiposCancha (Descripcion)
                  VALUES (@Descripcion)", conexion))
            {
                CargarParametros(comando, tipoCancha);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(TiposCancha tipoCancha)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE TiposCancha
                  SET Descripcion = @Descripcion
                  WHERE IdTipoCancha = @IdTipoCancha", conexion))
            {
                comando.Parameters.Add("@IdTipoCancha", SqlDbType.Int).Value = tipoCancha.IdTipoCancha;
                CargarParametros(comando, tipoCancha);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Eliminar(int idTipoCancha)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"DELETE FROM TiposCancha
                  WHERE IdTipoCancha = @IdTipoCancha", conexion))
            {
                comando.Parameters.Add("@IdTipoCancha", SqlDbType.Int).Value = idTipoCancha;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static void CargarParametros(SqlCommand comando, TiposCancha tipoCancha)
        {
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 80).Value = tipoCancha.Descripcion;
        }

        private static TiposCancha MapearTipoCancha(SqlDataReader lector)
        {
            return new TiposCancha
            {
                IdTipoCancha = Convert.ToInt32(lector["IdTipoCancha"]),
                Descripcion = lector["Descripcion"].ToString()
            };
        }
    }
}
