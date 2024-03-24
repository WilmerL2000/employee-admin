using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CRUD.EntityLayer;
using System.Data;
using System.Data.SqlClient;

namespace CRUD.DataLayer
{
	public class EmpleadoDL
	{
		public List<Empleado> Lista()
		{
			List<Empleado> list = new List<Empleado>();

			using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
			{
				SqlCommand cmd = new SqlCommand("select * from fn_empleados()", oConexion);
				cmd.CommandType = CommandType.Text;

				try
				{
					oConexion.Open();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							list.Add(new Empleado
							{
								IdEmpleado = Convert.ToInt32(reader["IdDepartamento"].ToString()),
								NombreCompleto = reader["NombreCompleto"].ToString(),
								Departamento = new Departamento {
									IdDepartamento = Convert.ToInt32(reader["IdDepartamento"].ToString()),
									Nombre = reader["Nombre"].ToString()
								},
								Sueldo = (decimal) reader["Sueldo"],
								FechaContrato = reader["FechaContrato"].ToString(),
							});
						}
					}

					return list;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		public Empleado Obtener(int IdEmpleado)
		{
			//Single employee
			Empleado empleado = new Empleado();

			using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
			{
				SqlCommand cmd = new SqlCommand("select * from fn_empleado(@IdEmpleado)", oConexion);

				//Add parameter to command
				cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
				cmd.CommandType = CommandType.Text;

				try
				{
					oConexion.Open();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						//Is there something to read?
						if(reader.Read())
						{
							//Add values to single employee
							empleado.IdEmpleado = Convert.ToInt32(reader["IdDepartamento"].ToString());
							empleado.NombreCompleto = reader["NombreCompleto"].ToString();
							empleado.Departamento = new Departamento
							{
								IdDepartamento = Convert.ToInt32(reader["IdDepartamento"].ToString()),
								Nombre = reader["Nombre"].ToString()
							};
							empleado.Sueldo = (decimal)reader["Sueldo"];
							empleado.FechaContrato = reader["FechaContrato"].ToString();
								
						}

					}

					return empleado;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}


		public bool Crear(Empleado empleado)
		{
			bool respuesta = false;

			using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
			{
				//Stored procedure
				SqlCommand cmd = new SqlCommand("sp_CrearEmpleado", oConexion);

				//Add parameter to command
				cmd.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
				cmd.Parameters.AddWithValue("@IdDepartamento", empleado.Departamento.IdDepartamento);
				cmd.Parameters.AddWithValue("@Sueldo", empleado.Sueldo);
				cmd.Parameters.AddWithValue("@FechaContrato", empleado.FechaContrato);
				//!!
				cmd.CommandType = CommandType.StoredProcedure;

				try
				{
					oConexion.Open();
					//Affected rows
					int filasAfectadas = cmd.ExecuteNonQuery();

					if (filasAfectadas > 0) respuesta = true;						
							
					return respuesta;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		public bool Editar(Empleado empleado)
		{
			bool respuesta = false;

			using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
			{
				//Stored procedure
				SqlCommand cmd = new SqlCommand("sp_EditarEmpleado", oConexion);

				//Add parameter to command
				cmd.Parameters.AddWithValue("@IdEmpleado", empleado.IdEmpleado);
				cmd.Parameters.AddWithValue("@NombreCompleto", empleado.NombreCompleto);
				cmd.Parameters.AddWithValue("@IdDepartamento", empleado.Departamento.IdDepartamento);
				cmd.Parameters.AddWithValue("@Sueldo", empleado.Sueldo);
				cmd.Parameters.AddWithValue("@FechaContrato", empleado.FechaContrato);
				//!!
				cmd.CommandType = CommandType.StoredProcedure;

				try
				{
					oConexion.Open();
						//Affected rows
					int filasAfectadas = cmd.ExecuteNonQuery();

					if (filasAfectadas > 0) respuesta = true;

					return respuesta;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}

		public bool Eliminar(int IdEmpleado)
		{
			bool respuesta = false;

			using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
			{
				//Stored procedure
				SqlCommand cmd = new SqlCommand("sp_ELiminarEmpleado", oConexion);
				cmd.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);
				cmd.CommandType = CommandType.StoredProcedure;

				try
				{
					oConexion.Open();
					//Affected rows
					int filasAfectadas = cmd.ExecuteNonQuery();

					if (filasAfectadas > 0) respuesta = true;

					return respuesta;
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
	}
}
