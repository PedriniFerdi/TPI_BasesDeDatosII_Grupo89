using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class AuditoriaReservaDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<AuditoriaReservaDetalle> Listar()
        {
            var auditorias = new List<AuditoriaReservaDetalle>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdAuditoria, IdReserva, Accion, Descripcion, FechaAccion, UsuarioSistema
                  FROM AuditoriaReservas
                  ORDER BY FechaAccion DESC, IdAuditoria DESC", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        auditorias.Add(MapearAuditoriaReservaDetalle(lector));
                    }
                }
            }

            return auditorias;
        }

        private static AuditoriaReservaDetalle MapearAuditoriaReservaDetalle(SqlDataReader lector)
        {
            return new AuditoriaReservaDetalle
            {
                IdAuditoria = Convert.ToInt32(lector["IdAuditoria"]),
                IdReserva = Convert.ToInt32(lector["IdReserva"]),
                Accion = lector["Accion"].ToString(),
                Descripcion = lector["Descripcion"].ToString(),
                FechaAccion = Convert.ToDateTime(lector["FechaAccion"]),
                UsuarioSistema = lector["UsuarioSistema"].ToString()
            };
        }
    }
}
