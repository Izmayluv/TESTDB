
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TESTDB.Classes
{
    internal class Connection
    {
        private static string _connectionString = @"Data Source = DESKTOP-6483D6D\SQLEXPRESS; Initial Catalog = Trade; Integrated Security = True";
        public static SqlConnection _connection;

        public void OpenConnection()
        {
            _connection = new SqlConnection(_connectionString);

            try
            {
                _connection.Open();

                if (_connection.State == ConnectionState.Open)
                {
                    MessageBox.Show("Подключение выполнено успешно", "Готово", MessageBoxButtons.OK);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Подключение не удалось", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseConnection()
        {
            _connection = new SqlConnection(_connectionString);

            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Отключение не удалось", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
