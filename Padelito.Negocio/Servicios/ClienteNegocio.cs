using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class ClienteNegocio
    {
        private readonly ClienteDatos _clienteDatos = new ClienteDatos();

        public List<Cliente> Listar()
        {
            return _clienteDatos.Listar();
        }

        public Cliente ObtenerPorId(int idCliente)
        {
            if (idCliente <= 0)
            {
                throw new ArgumentException("El id del cliente no es valido.");
            }

            return _clienteDatos.ObtenerPorId(idCliente);
        }

        public void Guardar(Cliente cliente)
        {
            Validar(cliente);

            if (cliente.IdCliente == 0)
            {
                _clienteDatos.Agregar(cliente);
            }
            else
            {
                _clienteDatos.Modificar(cliente);
            }
        }

        public void EliminarLogico(int idCliente)
        {
            if (idCliente <= 0)
            {
                throw new ArgumentException("Debe seleccionar un cliente valido.");
            }

            _clienteDatos.EliminarLogico(idCliente);
        }

        private static void Validar(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException("cliente");
            }

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(cliente.Apellido))
            {
                throw new ArgumentException("El apellido es obligatorio.");
            }

            if (!string.IsNullOrWhiteSpace(cliente.Email) && !cliente.Email.Contains("@"))
            {
                throw new ArgumentException("El email debe tener un formato basico valido.");
            }
        }
    }
}
