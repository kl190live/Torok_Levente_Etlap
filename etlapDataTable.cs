using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etlap
{
	public class etlapDataTable
	{
		MySqlConnection connection;
		public etlapDataTable()
		{
			MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
			builder.Server = "localhost";
			builder.Port = 3306;
			builder.UserID = "root";
			builder.Password = "";
			builder.Database = "etlapdb";
			connection = new MySqlConnection(builder.ConnectionString);
		}

		public List<etlapAdatok> GetAll()
		{ 
			List<etlapAdatok> etlapAdatoks = new List<etlapAdatok>();
			OpenConnection();
			string sql = "SELECT * FROM etlap";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			using (MySqlDataReader reader= command.ExecuteReader())
			{
				while (reader.Read())
				{ 
					etlapAdatok etlapAdatok = new etlapAdatok();
					etlapAdatok.Id= reader.GetInt32("id");
					etlapAdatok.Name = reader.GetString("nev");
					etlapAdatok.description = reader.GetString("leiras");
					etlapAdatok.price = reader.GetInt32("ar");
					etlapAdatok.category = reader.GetString("kategoria");
					etlapAdatoks.Add(etlapAdatok);
				}
			}
			CloseConnection();
			return etlapAdatoks;
		}

		public bool Create(etlapAdatok etlapadatok)
		{
			OpenConnection();
			string sql = "INSERT INTO etlap(name,description,price,category) VALUES(@name, @description, @price, @category)";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@name", etlapadatok.Name);
			command.Parameters.AddWithValue("@description", etlapadatok.description);
			command.Parameters.AddWithValue("@price", etlapadatok.price);
			command.Parameters.AddWithValue("@category", etlapadatok.category);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}

		public bool Delete(int id)
		{
			OpenConnection();
			string sql = "DELETE FROM etlap WHERE id=@id";
			MySqlCommand command = connection.CreateCommand();
			command.CommandText = sql;
			command.Parameters.AddWithValue("@id", id);
			int affectedRows = command.ExecuteNonQuery();
			CloseConnection();
			return affectedRows == 1;
		}

		private void OpenConnection()
		{
			if (connection.State != System.Data.ConnectionState.Open)
			{ 
				connection.Open();
			}
		}

		private void CloseConnection()
		{
			if (connection.State == System.Data.ConnectionState.Open)
			{
				connection.Close();
			}
		}

	}
}
