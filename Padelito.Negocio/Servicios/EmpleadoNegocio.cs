using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class EmpleadoNegocio
    {
        private readonly EmpleadoDatos _empleadoDatos = new EmpleadoDatos();

        public List<Empleado> Listar()
        {
            return _empleadoDatos.Listar();
        }

        public Empleado ObtenerPorId(int idEmpleado)
        {
            if (idEmpleado <= 0)
            {
                throw new ArgumentException("El id del empleado no es valido.");
            }

            return _empleadoDatos.ObtenerPorId(idEmpleado);
        }

        public void Guardar(Empleado empleado)
        {
            Validar(empleado);

            if (empleado.IdEmpleado == 0)
            {
                _empleadoDatos.Agregar(empleado);
            }
            else
            {
                _empleadoDatos.Modificar(empleado);
            }
        }

        public void EliminarLogico(int idEmpleado)
        {
            if (idEmpleado <= 0)
            {
                throw new ArgumentException("Debe seleccionar un empleado valido.");
            }

            _empleadoDatos.EliminarLogico(idEmpleado);
        }

        private static void Validar(Empleado empleado)
        {
            if (empleado == null)
            {
                throw new ArgumentNullException("empleado");
            }

            if (string.IsNullOrWhiteSpace(empleado.Nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(empleado.Apellido))
            {
                throw new ArgumentException("El apellido es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(empleado.DNI))
            {
                throw new ArgumentException("El DNI es obligatorio.");
            }

            if (!string.IsNullOrWhiteSpace(empleado.Email) && !empleado.Email.Contains("@"))
            {
                throw new ArgumentException("El email debe tener un formato basico valido.");
            }
        }
    }
}
