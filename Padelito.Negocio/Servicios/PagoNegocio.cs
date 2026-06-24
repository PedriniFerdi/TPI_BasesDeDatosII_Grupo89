using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class PagoNegocio
    {
        private readonly PagoDatos _pagoDatos = new PagoDatos();

        public List<PagoDetalle> ListarDetalle()
        {
            return _pagoDatos.ListarDetalle();
        }

        public void Guardar(Pago pago)
        {
            Validar(pago);
            _pagoDatos.Agregar(pago);
        }

        private static void Validar(Pago pago)
        {
            if (pago == null)
            {
                throw new ArgumentNullException("pago");
            }

            if (pago.IdReserva <= 0)
            {
                throw new ArgumentException("Debe seleccionar una reserva.");
            }

            if (pago.IdMetodoPago <= 0)
            {
                throw new ArgumentException("Debe seleccionar un metodo de pago.");
            }

            if (pago.Monto <= 0)
            {
                throw new ArgumentException("El monto debe ser mayor a 0.");
            }
        }
    }
}
