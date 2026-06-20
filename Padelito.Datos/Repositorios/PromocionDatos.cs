using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class PromocionDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<Promocion> Listar()
        {
            var promociones = new List<Promocion>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdPromocion, Nombre, Descripcion, PorcentajeDescuento,
                         FechaDesde, FechaHasta, Activa
                  FROM Promociones
                  ORDER BY FechaDesde DESC, Nombre", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        promociones.Add(MapearPromocion(lector));
                    }
                }
            }

            return promociones;
        }

        public Promocion ObtenerPorId(int idPromocion)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdPromocion, Nombre, Descripcion, PorcentajeDescuento,
                         FechaDesde, FechaHasta, Activa
                  FROM Promociones
                  WHERE IdPromocion = @IdPromocion", conexion))
            {
                comando.Parameters.Add("@IdPromocion", SqlDbType.Int).Value = idPromocion;
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return lector.Read() ? MapearPromocion(lector) : null;
                }
            }
        }

        public void Agregar(Promocion promocion)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"INSERT INTO Promociones (Nombre, Descripcion, PorcentajeDescuento, FechaDesde, FechaHasta, Activa)
                  VALUES (@Nombre, @Descripcion, @PorcentajeDescuento, @FechaDesde, @FechaHasta, @Activa)", conexion))
            {
                CargarParametros(comando, promocion);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(Promocion promocion)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Promociones
                  SET Nombre = @Nombre,
                      Descripcion = @Descripcion,
                      PorcentajeDescuento = @PorcentajeDescuento,
                      FechaDesde = @FechaDesde,
                      FechaHasta = @FechaHasta,
                      Activa = @Activa
                  WHERE IdPromocion = @IdPromocion", conexion))
            {
                comando.Parameters.Add("@IdPromocion", SqlDbType.Int).Value = promocion.IdPromocion;
                CargarParametros(comando, promocion);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int idPromocion)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE Promociones
                  SET Activa = 0
                  WHERE IdPromocion = @IdPromocion", conexion))
            {
                comando.Parameters.Add("@IdPromocion", SqlDbType.Int).Value = idPromocion;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static void CargarParametros(SqlCommand comando, Promocion promocion)
        {
            comando.Parameters.Add("@Nombre", SqlDbType.VarChar, 80).Value = promocion.Nombre;
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar, 255).Value =
                string.IsNullOrWhiteSpace(promocion.Descripcion) ? (object)DBNull.Value : promocion.Descripcion;
            comando.Parameters.Add("@PorcentajeDescuento", SqlDbType.Decimal).Value = promocion.PorcentajeDescuento;
            comando.Parameters["@PorcentajeDescuento"].Precision = 5;
            comando.Parameters["@PorcentajeDescuento"].Scale = 2;
            comando.Parameters.Add("@FechaDesde", SqlDbType.Date).Value = promocion.FechaDesde.Date;
            comando.Parameters.Add("@FechaHasta", SqlDbType.Date).Value = promocion.FechaHasta.Date;
            comando.Parameters.Add("@Activa", SqlDbType.Bit).Value = promocion.Activa;
        }

        private static Promocion MapearPromocion(SqlDataReader lector)
        {
            return new Promocion
            {
                IdPromocion = Convert.ToInt32(lector["IdPromocion"]),
                Nombre = lector["Nombre"].ToString(),
                Descripcion = lector["Descripcion"] == DBNull.Value ? string.Empty : lector["Descripcion"].ToString(),
                PorcentajeDescuento = Convert.ToDecimal(lector["PorcentajeDescuento"]),
                FechaDesde = Convert.ToDateTime(lector["FechaDesde"]),
                FechaHasta = Convert.ToDateTime(lector["FechaHasta"]),
                Activa = Convert.ToBoolean(lector["Activa"])
            };
        }
    }
}
