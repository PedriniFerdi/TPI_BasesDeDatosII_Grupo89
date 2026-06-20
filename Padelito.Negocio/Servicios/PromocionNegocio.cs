using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class PromocionNegocio
    {
        private readonly PromocionDatos _promocionDatos = new PromocionDatos();

        public List<Promocion> Listar()
        {
            return _promocionDatos.Listar();
        }

        public Promocion ObtenerPorId(int idPromocion)
        {
            if (idPromocion <= 0)
            {
                throw new ArgumentException("El id de la promocion no es valido.");
            }

            return _promocionDatos.ObtenerPorId(idPromocion);
        }

        public void Guardar(Promocion promocion)
        {
            Validar(promocion);

            if (promocion.IdPromocion == 0)
            {
                _promocionDatos.Agregar(promocion);
            }
            else
            {
                _promocionDatos.Modificar(promocion);
            }
        }

        public void EliminarLogico(int idPromocion)
        {
            if (idPromocion <= 0)
            {
                throw new ArgumentException("Debe seleccionar una promocion valida.");
            }

            _promocionDatos.EliminarLogico(idPromocion);
        }

        private static void Validar(Promocion promocion)
        {
            if (promocion == null)
            {
                throw new ArgumentNullException("promocion");
            }

            if (string.IsNullOrWhiteSpace(promocion.Nombre))
            {
                throw new ArgumentException("El nombre de la promocion es obligatorio.");
            }

            if (promocion.Nombre.Length > 80)
            {
                throw new ArgumentException("El nombre no puede superar los 80 caracteres.");
            }

            if (!string.IsNullOrWhiteSpace(promocion.Descripcion) && promocion.Descripcion.Length > 255)
            {
                throw new ArgumentException("La descripcion no puede superar los 255 caracteres.");
            }

            if (promocion.PorcentajeDescuento < 0 || promocion.PorcentajeDescuento > 100)
            {
                throw new ArgumentException("El porcentaje de descuento debe estar entre 0 y 100.");
            }

            if (promocion.FechaDesde == DateTime.MinValue || promocion.FechaHasta == DateTime.MinValue)
            {
                throw new ArgumentException("Debe ingresar la fecha desde y la fecha hasta.");
            }

            if (promocion.FechaHasta < promocion.FechaDesde)
            {
                throw new ArgumentException("La fecha hasta debe ser mayor o igual que la fecha desde.");
            }
        }
    }
}
