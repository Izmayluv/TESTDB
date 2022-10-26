using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TESTDB.Classes;
using TESTDB.Views;

namespace TESTDB
{
    public partial class AuthorizationView : Form
    {
        public AuthorizationView()
        {
            InitializeComponent();
        }

        private Commands commands;

        public static string loginText;
        public static string passwordText;

        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            commands = new Commands();

            loginText = login.Text;
            passwordText = password.Text;

            try
            {
                switch (commands.AuthCommand(login.Text, password.Text))
                {
                    case 0:
                        MessageBox.Show("Пароль или логин неверный");
                        break;


                    case 1:
                        //MessageBox.Show("Вы вошли как клиент");

                        var clientView = new ClientView();

                        this.Hide();
                        clientView.Show();

                        break;


                    case 2:
                        //MessageBox.Show("Вы вошли как менеджер");

                        var managerView = new ManagerView();

                        this.Hide();
                        managerView.Show();

                        break;


                    case 3:
                        //MessageBox.Show("Вы вошли как админирстратор");

                        var adminView = new AdministratorView();

                        this.Hide();
                        adminView.Show();

                        break;
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Ошибка");
            }

        }
    }
}
