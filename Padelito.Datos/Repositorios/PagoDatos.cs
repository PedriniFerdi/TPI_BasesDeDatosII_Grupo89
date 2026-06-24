using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class PagoDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<PagoDetalle> ListarDetalle()
        {
            var pagos = new List<PagoDetalle>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdPago, IdReserva, Cliente, Cancha, FechaReserva,
                         MetodoPago, Monto, FechaPago, Observacion
                  FROM VW_PagosDetalle
                  ORDER BY FechaPago DESC, IdPago DESC", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        pagos.Add(MapearPagoDetalle(lector));
                    }
                }
            }

            return pagos;
        }

        public void Agregar(Pago pago)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"INSERT INTO Pagos
                    (IdReserva, IdMetodoPago, Monto, Observacion)
                  VALUES
                    (@IdReserva, @IdMetodoPago, @Monto, @Observacion)", conexion))
            {
                comando.Parameters.Add("@IdReserva", SqlDbType.Int).Value = pago.IdReserva;
                comando.Parameters.Add("@IdMetodoPago", SqlDbType.Int).Value = pago.IdMetodoPago;
                comando.Parameters.Add("@Monto", SqlDbType.Decimal).Value = pago.Monto;
                comando.Parameters["@Monto"].Precision = 10;
                comando.Parameters["@Monto"].Scale = 2;
                comando.Parameters.Add("@Observacion", SqlDbType.VarChar, 255).Value =
                    string.IsNullOrWhiteSpace(pago.Observacion) ? (object)DBNull.Value : pago.Observacion.Trim();

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static PagoDetalle MapearPagoDetalle(SqlDataReader lector)
        {
            return new PagoDetalle
            {
                IdPago = Convert.ToInt32(lector["IdPago"]),
                IdReserva = Convert.ToInt32(lector["IdReserva"]),
                Cliente = lector["Cliente"].ToString(),
                Cancha = lector["Cancha"].ToString(),
                FechaReserva = Convert.ToDateTime(lector["FechaReserva"]),
                MetodoPago = lector["MetodoPago"].ToString(),
                Monto = Convert.ToDecimal(lector["Monto"]),
                FechaPago = Convert.ToDateTime(lector["FechaPago"]),
                Observacion = lector["Observacion"] == DBNull.Value ? string.Empty : lector["Observacion"].ToString()
            };
        }
    }
}
