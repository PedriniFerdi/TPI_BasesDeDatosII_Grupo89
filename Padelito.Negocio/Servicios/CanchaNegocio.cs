using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class CanchaNegocio
    {
        private readonly CanchaDatos _canchaDatos = new CanchaDatos();

        public List<Cancha> Listar()
        {
            return _canchaDatos.Listar();
        }

        public Cancha ObtenerPorId(int idCancha)
        {
            if (idCancha <= 0)
            {
                throw new ArgumentException("El id de la cancha no es valido.");
            }

            return _canchaDatos.ObtenerPorId(idCancha);
        }

        public void Guardar(Cancha cancha)
        {
            Validar(cancha);

            if (cancha.IdCancha == 0)
            {
                _canchaDatos.Agregar(cancha);
            }
            else
            {
                _canchaDatos.Modificar(cancha);
            }
        }

        public void EliminarLogico(int idCancha)
        {
            if (idCancha <= 0)
            {
                throw new ArgumentException("Debe seleccionar una cancha valida.");
            }

            _canchaDatos.EliminarLogico(idCancha);
        }

        private static void Validar(Cancha cancha)
        {
            if (cancha == null)
            {
                throw new ArgumentNullException("cancha");
            }

            if (string.IsNullOrWhiteSpace(cancha.Nombre))
            {
                throw new ArgumentException("El nombre de la cancha es obligatorio.");
            }

            if (cancha.Nombre.Length > 80)
            {
                throw new ArgumentException("El nombre no puede superar los 80 caracteres.");
            }

            if (cancha.IdTipoCancha <= 0)
            {
                throw new ArgumentException("Debe seleccionar un tipo de cancha.");
            }

            if (cancha.PrecioHora < 0)
            {
                throw new ArgumentException("El precio por hora no puede ser negativo.");
            }
        }
    }
}
