using System.Data.SqlClient;
using System;


namespace TESTDB.Classes
{
    internal class Commands
    {
        private Connection connection;

        public int AuthCommand(string login, string password)
        {
            connection = new Connection();
            connection.OpenConnection();

            var role = 0;
            var query = "select * from Customer where userLogin = @login and userPassword = @password";

            var _sqlCommand = new SqlCommand(query, Connection._connection);

            _sqlCommand.Parameters.AddWithValue("@login", login);
            _sqlCommand.Parameters.AddWithValue("@password", password);
            
            try
            {
                var reader = _sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                   role = reader.GetInt32(6);
                }

                if (reader.HasRows)
                {
                    reader.Close();
                    connection.CloseConnection();
                    return role;
                }

                reader.Close();
                connection.CloseConnection();
                return 0; 
            }

            catch (SqlException)
            {
                return 0;
            }

            catch (InvalidOperationException)
            {
                return 0;
            }
            
        }

        public string getFIO(string login, string password)
        {
            connection = new Connection();
            connection.OpenConnection();

            var name = "";
            var query = "select * from Customer where userLogin = @login and userPassword = @password";

            var _sqlCommand = new SqlCommand(query, Connection._connection);

            _sqlCommand.Parameters.AddWithValue("@login", login);
            _sqlCommand.Parameters.AddWithValue("@password", password);

            try
            {
                var reader = _sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    name += reader.GetString(1) + " ";
                    name += reader.GetString(2) + " ";
                    name += reader.GetString(3);
                }

                if (reader.HasRows)
                {
                    reader.Close();
                    connection.CloseConnection();
                    return name;
                }

                reader.Close();
                connection.CloseConnection();
                return null;
            }

            catch (SqlException)
            {
                return null;
            }

            catch (InvalidOperationException)
            {
                return null;
            }

        }
    }
}
