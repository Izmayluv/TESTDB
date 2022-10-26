using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TESTDB.Classes;

namespace TESTDB.Views
{
    public partial class ClientView : Form
    {
        public ClientView()
        {
            InitializeComponent();
        }

        private Connection connection;
        private string path = @"C:\Users\pixam\source\repos\TESTDB\TESTDB\Resources\";

        private void dataFilling(int category)
        {
            int countAll, index = 0;
            Bitmap bitmap;
            string photo;
            string sumPath;
            double cost, sale, costSale;


            connection = new Connection();
            connection.OpenConnection();

            string sqlCount = "SELECT COUNT (*) FROM Items";
            SqlCommand com = new SqlCommand(sqlCount, Connection._connection);
            countAll = (int)com.ExecuteScalar();

            string query = "select *from Items, Manufacturer where Items.productProducer = Manufacturer.producerID";

            if (category != 0)
                query += $" and [Items].productCategory = {category}";

            var command = new SqlCommand(query, Connection._connection);
            var dataReader = command.ExecuteReader();

            dataGridViewProd.Rows.Clear();
            
            while (dataReader.Read())
            {
                if (dataReader.HasRows)
                {

                    
                    dataGridViewProd.Rows.Add();
                    dataGridViewProd.Rows[index].Cells[0].Value = dataReader["productArticleNumber"];
                    photo = dataReader["productPhoto"].ToString();

                    if (String.IsNullOrEmpty(photo))
                        bitmap = Properties.Resources.zaglushka;
                    else
                    {
                        sumPath = path + photo;
                        

                        if (File.Exists(sumPath))
             
                            bitmap = new Bitmap(sumPath);
                        
                        else
                            bitmap = Properties.Resources.zaglushka;
                    }

                    dataGridViewProd.Rows [index].Cells[1].Value = bitmap;

                    string description = "Наименование товара: " + dataReader["productName"] + Environment.NewLine;
                    description += "Описание товара: " + dataReader["productDescription"] + Environment.NewLine;
                    description += "Производитель: " + dataReader ["productProducer"] + Environment.NewLine;

                    cost = Convert.ToDouble(dataReader["productCost"]);
                    sale = Convert.ToDouble(dataReader["productDiscountAmount"]);

                    costSale = cost - sale / 100.00 * cost;

                    description += "Цена: " + cost + Environment.NewLine;
                    description += "Скидка: " + sale + Environment.NewLine;
                    description += "Цена со скидкой: " + costSale + Environment.NewLine;

                    dataGridViewProd.Rows[index].Cells[2].Value = description;

                    dataGridViewProd.Rows[index].Height = 140;
                    index++;

                   dataGridViewProd.ClearSelection();
                }
            }

            dataReader.Close();
            connection.CloseConnection();
            labelAmountOfItems.Text = "Количество записей: " + index + " из " + countAll;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            var authorizationView = new AuthorizationView();
            this.Hide();
            authorizationView.Show();
        }



        private void ClientView_Load(object sender, EventArgs e)
        {
            Commands commands = new Commands();
            labelLog.Text = commands.getFIO(AuthorizationView.loginText, AuthorizationView.passwordText);

            comboBoxCategories.Items.Add("Все категории");
            comboBoxCategories.Text = comboBoxCategories.Items[0].ToString();

            connection = new Connection();
            connection.OpenConnection();

            string query = "select *from category";
            SqlCommand com = new SqlCommand(query, Connection._connection);
            var dataReader = com.ExecuteReader();

            while (dataReader.Read())
            {
                if (dataReader.HasRows)
                {
                    comboBoxCategories.Items.Add(dataReader["categoryName"]);
                }
            }
            
            connection.CloseConnection();

            dataFilling(Convert.ToInt32(comboBoxCategories.SelectedIndex.ToString()));

        }

        private void comboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataFilling(Convert.ToInt32(comboBoxCategories.SelectedIndex.ToString()));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var authorizationView = new AuthorizationView();
            this.Hide();
            authorizationView.Show();
        }
    }
}
