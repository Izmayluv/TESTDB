
using System.Windows.Forms;
using System.Data.SqlClient;
using TESTDB.Views;

namespace TESTDB.Classes
{
    internal class Commands
    {

        public int AuthCommand(string login, string password)
        {
            int role = 0;
            string query = "select * from Customers where userLogin = '" + login + "' and userPassword = '" + password + "'";
            SqlCommand _sqlCommand = new SqlCommand(query, Connection._connection);
            try
            {
                SqlDataReader reader = _sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                   role = reader.GetInt32(6);
                }

                if (reader.HasRows)
                {
                    reader.Close();
                    return role;
                }
                else
                {
                    reader.Close();
                    return 0;
                }
                
            }
            catch (SqlException)
            {
                return 0;
            }

            
            //try
            //{
            //    SqlDataReader reader = _sqlCommand.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        role = reader.GetInt32(6);
            //    }

            //    if (reader.HasRows)
            //    {
            //        MessageBox.Show("Пароль верный");

            //        if (role.Equals(0))
            //        {
            //            MessageBox.Show("ujdyjjPOFMomF");
            //        }

            //        if (role == 1)
            //        {
            //            MessageBox.Show("Вы вошли как клиент");
            //            ClientView clientView = new ClientView();
            //            clientView.Show();

            //        }

            //        if (role == 2)
            //        {
            //            MessageBox.Show("Вы вошли как менеджер");
            //        }
            //        if (role == 3)
            //        {
            //            MessageBox.Show("Вы вошли как администратор");
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Пароль не верный");
            //    }
            //    reader.Close();
            //}
            //catch (SqlException)
            //{
            //    MessageBox.Show("Выполнить команду не удалось", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

    }
}
