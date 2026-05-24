using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class TiposCanchaNegocio
    {
        private readonly TiposCanchaDatos _tiposCanchaDatos = new TiposCanchaDatos();

        public List<TiposCancha> Listar()
        {
            return _tiposCanchaDatos.Listar();
        }

        public TiposCancha ObtenerPorId(int idTipoCancha)
        {
            if (idTipoCancha <= 0)
            {
                throw new ArgumentException("El id del tipo de cancha no es valido.");
            }

            return _tiposCanchaDatos.ObtenerPorId(idTipoCancha);
        }

        public void Guardar(TiposCancha tipoCancha)
        {
            Validar(tipoCancha);

            if (tipoCancha.IdTipoCancha == 0)
            {
                _tiposCanchaDatos.Agregar(tipoCancha);
            }
            else
            {
                _tiposCanchaDatos.Modificar(tipoCancha);
            }
        }

        public void Eliminar(int idTipoCancha)
        {
            if (idTipoCancha <= 0)
            {
                throw new ArgumentException("Debe seleccionar un tipo de cancha valido.");
            }

            _tiposCanchaDatos.Eliminar(idTipoCancha);
        }

        private static void Validar(TiposCancha tipoCancha)
        {
            if (tipoCancha == null)
            {
                throw new ArgumentNullException("tipoCancha");
            }

            if (string.IsNullOrWhiteSpace(tipoCancha.Descripcion))
            {
                throw new ArgumentException("La descripcion es obligatoria.");
            }

            if (tipoCancha.Descripcion.Length > 80)
            {
                throw new ArgumentException("La descripcion no puede superar los 80 caracteres.");
            }
        }
    }
}
