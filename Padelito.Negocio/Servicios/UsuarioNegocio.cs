using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class UsuarioNegocio
    {
        private readonly UsuarioDatos _usuarioDatos = new UsuarioDatos();

        public List<Usuario> Listar()
        {
            return _usuarioDatos.Listar();
        }

        public Usuario ObtenerPorId(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                throw new ArgumentException("El id del usuario no es valido.");
            }

            return _usuarioDatos.ObtenerPorId(idUsuario);
        }

        public List<Empleado> ListarEmpleadosDisponibles(int idEmpleadoActual)
        {
            return _usuarioDatos.ListarEmpleadosDisponibles(idEmpleadoActual);
        }

        public void Guardar(Usuario usuario)
        {
            Validar(usuario);

            if (_usuarioDatos.ExisteNombreUsuario(usuario.NombreUsuario, usuario.IdUsuario))
            {
                throw new ArgumentException("Ya existe un usuario con ese nombre.");
            }

            if (_usuarioDatos.ExisteEmpleadoAsignado(usuario.IdEmpleado, usuario.IdUsuario))
            {
                throw new ArgumentException("El empleado seleccionado ya tiene un usuario asignado.");
            }

            if (usuario.IdUsuario == 0)
            {
                _usuarioDatos.Agregar(usuario);
            }
            else
            {
                _usuarioDatos.Modificar(usuario);
            }
        }

        public void EliminarLogico(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                throw new ArgumentException("Debe seleccionar un usuario valido.");
            }

            _usuarioDatos.EliminarLogico(idUsuario);
        }

        private static void Validar(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("usuario");
            }

            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
            {
                throw new ArgumentException("El nombre de usuario es obligatorio.");
            }

            if (usuario.NombreUsuario.Length > 20)
            {
                throw new ArgumentException("El nombre de usuario no puede superar los 20 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Contrasenia))
            {
                throw new ArgumentException("La contrasenia es obligatoria.");
            }

            if (usuario.Contrasenia.Length > 15)
            {
                throw new ArgumentException("La contrasenia no puede superar los 15 caracteres.");
            }

            if (usuario.IdEmpleado <= 0)
            {
                throw new ArgumentException("Debe seleccionar un empleado.");
            }

            if (usuario.IdRol <= 0)
            {
                throw new ArgumentException("Debe seleccionar un rol.");
            }
        }
    }
}
