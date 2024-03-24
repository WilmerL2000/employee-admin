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
	public class DepartamentoDL
	{
		public List<Departamento> Lista()
		{
			//Create depart list
			List<Departamento> list = new List<Departamento>();

			//Create a connection
			using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
			{
				//Add the command exec
				SqlCommand cmd = new SqlCommand("select * from fn_departamento()", oConexion);
				cmd.CommandType = CommandType.Text;

				try
				{
					//Open connection
					oConexion.Open();
					
					//Exec the command
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						//Read all data
						while(reader.Read()) {

							//Add to list with its object
							list.Add(new Departamento {
								IdDepartamento = Convert.ToInt32(reader["IdDepartamento"].ToString()),
								Nombre = reader["Nombre"].ToString()
							});
								}
					}

					return list;
				}catch (Exception ex)
				{
					throw ex;
				}
			}
		}
	}
}
