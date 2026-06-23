using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class TurnosDisponiblesDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<TurnosDisponibles> Listar()
        {
            var turnos = new List<TurnosDisponibles>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT td.IdTurnoDisponible, td.IdCancha, c.Nombre AS NombreCancha,
                         td.HoraInicio, td.HoraFin, td.Activo
                  FROM TurnosDisponibles td
                  INNER JOIN Canchas c ON c.IdCancha = td.IdCancha
                  ORDER BY c.Nombre, td.HoraInicio", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        turnos.Add(MapearTurnoDisponible(lector));
                    }
                }
            }

            return turnos;
        }

        public TurnosDisponibles ObtenerPorId(int idTurnoDisponible)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT td.IdTurnoDisponible, td.IdCancha, c.Nombre AS NombreCancha,
                         td.HoraInicio, td.HoraFin, td.Activo
                  FROM TurnosDisponibles td
                  INNER JOIN Canchas c ON c.IdCancha = td.IdCancha
                  WHERE td.IdTurnoDisponible = @IdTurnoDisponible", conexion))
            {
                comando.Parameters.Add("@IdTurnoDisponible", SqlDbType.Int).Value = idTurnoDisponible;
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    return lector.Read() ? MapearTurnoDisponible(lector) : null;
                }
            }
        }

        public decimal ObtenerPrecioCancha(int idTurnoDisponible)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT c.PrecioHora
                  FROM TurnosDisponibles td
                  INNER JOIN Canchas c ON c.IdCancha = td.IdCancha
                  WHERE td.IdTurnoDisponible = @IdTurnoDisponible
                    AND td.Activo = 1
                    AND c.Activa = 1", conexion))
            {
                comando.Parameters.Add("@IdTurnoDisponible", SqlDbType.Int).Value = idTurnoDisponible;
                conexion.Open();

                object precio = comando.ExecuteScalar();

                if (precio == null || precio == DBNull.Value)
                {
                    throw new ArgumentException("El turno seleccionado no existe o no esta activo.");
                }

                return Convert.ToDecimal(precio);
            }
        }

        public void Agregar(TurnosDisponibles turnoDisponible)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"INSERT INTO TurnosDisponibles (IdCancha, HoraInicio, HoraFin, Activo)
                  VALUES (@IdCancha, @HoraInicio, @HoraFin, @Activo)", conexion))
            {
                CargarParametros(comando, turnoDisponible);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void Modificar(TurnosDisponibles turnoDisponible)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE TurnosDisponibles
                  SET IdCancha = @IdCancha,
                      HoraInicio = @HoraInicio,
                      HoraFin = @HoraFin,
                      Activo = @Activo
                  WHERE IdTurnoDisponible = @IdTurnoDisponible", conexion))
            {
                comando.Parameters.Add("@IdTurnoDisponible", SqlDbType.Int).Value = turnoDisponible.IdTurnoDisponible;
                CargarParametros(comando, turnoDisponible);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarLogico(int idTurnoDisponible)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"UPDATE TurnosDisponibles
                  SET Activo = 0
                  WHERE IdTurnoDisponible = @IdTurnoDisponible", conexion))
            {
                comando.Parameters.Add("@IdTurnoDisponible", SqlDbType.Int).Value = idTurnoDisponible;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static void CargarParametros(SqlCommand comando, TurnosDisponibles turnoDisponible)
        {
            comando.Parameters.Add("@IdCancha", SqlDbType.Int).Value = turnoDisponible.IdCancha;
            comando.Parameters.Add("@HoraInicio", SqlDbType.Time).Value = turnoDisponible.HoraInicio;
            comando.Parameters.Add("@HoraFin", SqlDbType.Time).Value = turnoDisponible.HoraFin;
            comando.Parameters.Add("@Activo", SqlDbType.Bit).Value = turnoDisponible.Activo;
        }

        private static TurnosDisponibles MapearTurnoDisponible(SqlDataReader lector)
        {
            return new TurnosDisponibles
            {
                IdTurnoDisponible = Convert.ToInt32(lector["IdTurnoDisponible"]),
                IdCancha = Convert.ToInt32(lector["IdCancha"]),
                NombreCancha = lector["NombreCancha"].ToString(),
                HoraInicio = (TimeSpan)lector["HoraInicio"],
                HoraFin = (TimeSpan)lector["HoraFin"],
                Activo = Convert.ToBoolean(lector["Activo"])
            };
        }
    }
}
