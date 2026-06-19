using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class TurnosDisponiblesNegocio
    {
        private readonly TurnosDisponiblesDatos _turnosDisponiblesDatos = new TurnosDisponiblesDatos();

        public List<TurnosDisponibles> Listar()
        {
            return _turnosDisponiblesDatos.Listar();
        }

        public TurnosDisponibles ObtenerPorId(int idTurnoDisponible)
        {
            if (idTurnoDisponible <= 0)
            {
                throw new ArgumentException("El id del turno disponible no es valido.");
            }

            return _turnosDisponiblesDatos.ObtenerPorId(idTurnoDisponible);
        }

        public void Guardar(TurnosDisponibles turnoDisponible)
        {
            Validar(turnoDisponible);

            if (turnoDisponible.IdTurnoDisponible == 0)
            {
                _turnosDisponiblesDatos.Agregar(turnoDisponible);
            }
            else
            {
                _turnosDisponiblesDatos.Modificar(turnoDisponible);
            }
        }

        public void EliminarLogico(int idTurnoDisponible)
        {
            if (idTurnoDisponible <= 0)
            {
                throw new ArgumentException("Debe seleccionar un turno disponible valido.");
            }

            _turnosDisponiblesDatos.EliminarLogico(idTurnoDisponible);
        }

        private static void Validar(TurnosDisponibles turnoDisponible)
        {
            if (turnoDisponible == null)
            {
                throw new ArgumentNullException("turnoDisponible");
            }

            if (turnoDisponible.IdCancha <= 0)
            {
                throw new ArgumentException("Debe seleccionar una cancha.");
            }

            if (turnoDisponible.HoraInicio == TimeSpan.Zero && turnoDisponible.HoraFin == TimeSpan.Zero)
            {
                throw new ArgumentException("Debe ingresar la hora de inicio y la hora de fin.");
            }

            if (turnoDisponible.HoraFin <= turnoDisponible.HoraInicio)
            {
                throw new ArgumentException("La hora de fin debe ser mayor que la hora de inicio.");
            }
        }
    }
}
