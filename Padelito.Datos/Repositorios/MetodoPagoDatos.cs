using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class MetodoPagoDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<MetodoPago> Listar()
        {
            var metodos = new List<MetodoPago>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdMetodoPago, Descripcion
                  FROM MetodosPago
                  ORDER BY Descripcion", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        metodos.Add(new MetodoPago
                        {
                            IdMetodoPago = Convert.ToInt32(lector["IdMetodoPago"]),
                            Descripcion = lector["Descripcion"].ToString()
                        });
                    }
                }
            }

            return metodos;
        }
    }
}
