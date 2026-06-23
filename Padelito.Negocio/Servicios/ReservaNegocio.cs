using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class ReservaNegocio
    {
        private readonly ReservaDatos _reservaDatos = new ReservaDatos();
        private readonly TurnosDisponiblesDatos _turnosDisponiblesDatos = new TurnosDisponiblesDatos();
        private readonly PromocionDatos _promocionDatos = new PromocionDatos();

        public List<ReservaDetalle> ListarDetalle()
        {
            return _reservaDatos.ListarDetalle();
        }

        public void Guardar(Reserva reserva)
        {
            Validar(reserva);

            if (_reservaDatos.ExisteReservaParaTurno(reserva.FechaReserva, reserva.IdTurnoDisponible))
            {
                throw new ArgumentException("Ya existe una reserva para ese turno en la fecha seleccionada.");
            }

            reserva.PrecioBase = _turnosDisponiblesDatos.ObtenerPrecioCancha(reserva.IdTurnoDisponible);
            reserva.PrecioFinal = CalcularPrecioFinal(reserva);

            _reservaDatos.Agregar(reserva);
        }

        public void CambiarEstado(int idReserva, int idEstadoReserva)
        {
            if (idReserva <= 0)
            {
                throw new ArgumentException("Debe seleccionar una reserva valida.");
            }

            if (idEstadoReserva <= 0)
            {
                throw new ArgumentException("Debe seleccionar un estado valido.");
            }

            _reservaDatos.CambiarEstado(idReserva, idEstadoReserva);
        }

        private decimal CalcularPrecioFinal(Reserva reserva)
        {
            if (!reserva.IdPromocion.HasValue)
            {
                return reserva.PrecioBase;
            }

            Promocion promocion = _promocionDatos.ObtenerPorId(reserva.IdPromocion.Value);

            if (promocion == null)
            {
                throw new ArgumentException("La promocion seleccionada no existe.");
            }

            if (!promocion.Activa)
            {
                throw new ArgumentException("La promocion seleccionada no esta activa.");
            }

            if (reserva.FechaReserva.Date < promocion.FechaDesde.Date || reserva.FechaReserva.Date > promocion.FechaHasta.Date)
            {
                throw new ArgumentException("La promocion seleccionada no esta vigente para la fecha de reserva.");
            }

            decimal descuento = reserva.PrecioBase * promocion.PorcentajeDescuento / 100;
            return reserva.PrecioBase - descuento;
        }

        private static void Validar(Reserva reserva)
        {
            if (reserva == null)
            {
                throw new ArgumentNullException("reserva");
            }

            if (reserva.IdCliente <= 0)
            {
                throw new ArgumentException("Debe seleccionar un cliente.");
            }

            if (reserva.IdTurnoDisponible <= 0)
            {
                throw new ArgumentException("Debe seleccionar un turno disponible.");
            }

            if (reserva.IdEmpleado <= 0)
            {
                throw new ArgumentException("Debe seleccionar un empleado.");
            }

            if (reserva.IdEstadoReserva <= 0)
            {
                throw new ArgumentException("Debe seleccionar un estado.");
            }

            if (reserva.FechaReserva == DateTime.MinValue)
            {
                throw new ArgumentException("Debe ingresar una fecha de reserva valida.");
            }
        }
    }
}
