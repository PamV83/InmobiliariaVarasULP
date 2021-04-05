using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaVarasULP.Models
{
   public class RepositorioInquilino : RepositorioBase
		{
			public RepositorioInquilino(IConfiguration configuration) : base(configuration)
			{

			}

			public IList<Inquilino> ObtenerTodos()
			{
				IList<Inquilino> res = new List<Inquilino>();
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					string sql = $"SELECT Id, Dni, Nombre, Apellido, DomicilioLaboral, Email, TelefonoInquilino, NombreGarante, DniGarante, TelefonoGarante" +
						$" FROM Inquilino";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = CommandType.Text;
						connection.Open();
						var reader = command.ExecuteReader();
						while (reader.Read())
						{
							Inquilino i = new Inquilino
							{
								Id = reader.GetInt32(0),
								Dni = reader.GetString(1),
								Nombre = reader.GetString(2),
								Apellido = reader.GetString(3),
								DomicilioLaboral = reader.GetString(4),
								Email = reader.GetString(5),
								TelefonoInquilino = reader.GetString(6),
								NombreGarante = reader.GetString(7),
								DniGarante = reader.GetString(8),
								TelefonoGarante = reader.GetString(9),

							};
							res.Add(i);
						}
						connection.Close();
					}
				}
				return res;
			}
			public int Alta(Inquilino i)
			{
				int res = -1;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					string sql = $"INSERT INTO Inquilino ( Dni, Nombre, Apellido, DomicilioLaboral, Email, TelefonoInquilino, NombreGarante, DniGarante, TelefonoGarante) " +
					$"VALUES ('{i.Dni}', '{i.Nombre}','{i.Apellido}', '{i.DomicilioLaboral}', '{i.Email}', '{i.TelefonoInquilino}', '{i.NombreGarante}', '{i.DniGarante}', '{i.TelefonoGarante}')";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.CommandType = System.Data.CommandType.Text;
						connection.Open();
						res = command.ExecuteNonQuery();
						command.CommandText = "SELECT SCOPE_IDENTITY()";
						var id = command.ExecuteScalar();
						i.Id = Convert.ToInt32(id);
						connection.Close();
					}
				}
				return res;
			}
		public Inquilino ObtenerPorId(int id)
		{

			Inquilino i = null;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"SELECT Id, Dni, Nombre, Apellido, DomicilioLaboral, Email, TelefonoInquilino, NombreGarante, DniGarante, TelefonoGarante FROM Inquilino" +
					$" WHERE Id=@id;";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						i= new Inquilino
						{
							Id = reader.GetInt32(0),
							Dni = reader.GetString(1),
							Nombre = reader.GetString(2),
							Apellido = reader.GetString(3),
							DomicilioLaboral = reader.GetString(4),
							Email = reader.GetString(5),
							TelefonoInquilino = reader.GetString(6),
							NombreGarante = reader.GetString(7),
							DniGarante = reader.GetString(8),
							TelefonoGarante = reader.GetString(9),
						};
					}
					connection.Close();
				}
				return i;
			}
		
		}

		public int Baja(int id)
		{
			int res = -1;

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"DELETE FROM Inquilino WHERE {nameof(Inquilino.Id)} = @id;";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}

			}
			return res;
		}
		public int Modificacion(Inquilino i)
		{
			int res = -1;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				string sql = $"UPDATE Inquilino SET Dni='{i.Dni}',Nombre='{i.Nombre}', Apellido='{i.Apellido}', DomicilioLaboral='{i.DomicilioLaboral}', Email='{i.Email}',, TelefonoInquilino='{i.TelefonoInquilino}', NombreGarante='{i.NombreGarante}',DniGarante='{i.DniGarante}', TelefonoGarante='{i.TelefonoGarante}' " +
					$"WHERE Id = {i.Id}";
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
	}
}
