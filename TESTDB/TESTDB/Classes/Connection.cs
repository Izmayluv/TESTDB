using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TESTDB.Classes
{
    internal class Connection
    {
        //private static string _connectionString = @"Data Source = DESKTOP-6483D6D\SQLEXPRESS; Initial Catalog = Trade; Integrated Security = True";
        public static SqlConnection _connection;
        private SqlConnectionStringBuilder _builder;


        public void OpenConnection()
        {
            _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = @"DESKTOP-6483D6D\SQLEXPRESS";
            _builder.InitialCatalog = "Trade";
            _builder.IntegratedSecurity = true;

            _connection = new SqlConnection(_builder.ConnectionString);

            try
            {
                _connection.Open();

                if (_connection.State == ConnectionState.Open)
                {
                    //MessageBox.Show("Подключение выполнено успешно", "Готово", MessageBoxButtons.OK);
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Подключение не удалось", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseConnection()
        {
            _connection = new SqlConnection(_builder.ConnectionString);

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
