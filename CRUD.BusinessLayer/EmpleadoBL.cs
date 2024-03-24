using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUD.DataLayer;
using CRUD.EntityLayer;

namespace CRUD.BusinessLayer
{
	public class EmpleadoBL
	{
		EmpleadoDL empleadoDL = new EmpleadoDL();

		public List<Empleado> Lista()
		{
			try
			{
				return empleadoDL.Lista();

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Empleado Obtener(int IdEmpleado)
		{
			try
			{
				return empleadoDL.Obtener(IdEmpleado);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool Crear(Empleado empleado)
		{
			try
			{
				return (empleado.NombreCompleto == "") ?
					throw new OperationCanceledException("El nombre no puede ser vacio") : empleadoDL.Crear(empleado);


			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool Editar(Empleado empleado)
		{
			try
			{
				var encontrado = empleadoDL.Obtener(empleado.IdEmpleado);

				return encontrado == null ? throw new OperationCanceledException("No existe el empleado") : empleadoDL.Editar(empleado);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool Eliminar(int IdEmpleado)
		{
			try
			{
				var encontrado = empleadoDL.Obtener(IdEmpleado);

				return encontrado == null ? throw new OperationCanceledException("No existe el empleado") : empleadoDL.Eliminar(IdEmpleado);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
