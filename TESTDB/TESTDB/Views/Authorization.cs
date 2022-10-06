using System;
using System.Windows.Forms;
using TESTDB.Classes;

namespace TESTDB
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        Connection connection;
        Commands commands;

        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            connection = new Connection();
            commands = new Commands();

            connection.OpenConnection();

            commands.AuthCommand(login.Text, password.Text);

            connection.CloseConnection();
        }
    }
}
