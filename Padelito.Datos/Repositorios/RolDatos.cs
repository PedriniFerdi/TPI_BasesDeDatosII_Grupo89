using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class RolDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<Rol> Listar()
        {
            var roles = new List<Rol>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdRol, Descripcion
                  FROM Roles
                  ORDER BY Descripcion", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        roles.Add(new Rol
                        {
                            IdRol = Convert.ToInt32(lector["IdRol"]),
                            Descripcion = lector["Descripcion"].ToString()
                        });
                    }
                }
            }

            return roles;
        }
    }
}
