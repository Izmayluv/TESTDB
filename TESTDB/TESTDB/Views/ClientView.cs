using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TESTDB.Views
{
    public partial class ClientView : Form
    {
        public ClientView()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            AuthorizationView authorizationView = new AuthorizationView();
            this.Hide();
            authorizationView.Show();
        }
    }
}
