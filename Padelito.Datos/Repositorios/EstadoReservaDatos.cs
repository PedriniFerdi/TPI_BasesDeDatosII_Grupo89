using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class EstadoReservaDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<EstadoReserva> Listar()
        {
            var estados = new List<EstadoReserva>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdEstadoReserva, Descripcion
                  FROM EstadosReserva
                  ORDER BY IdEstadoReserva", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        estados.Add(new EstadoReserva
                        {
                            IdEstadoReserva = Convert.ToInt32(lector["IdEstadoReserva"]),
                            Descripcion = lector["Descripcion"].ToString()
                        });
                    }
                }
            }

            return estados;
        }
    }
}
