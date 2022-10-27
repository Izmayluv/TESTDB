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

        private void dataFilling(int category, int sort, int discount, string search)
        {
            int countAll, index = 0;
            Bitmap bitmap;
            string photo;
            string sumPath;
            double cost, sale, costSale;

            string[,] arrayDiscount = new string[,] 
            {   
                { "0", "100" }, 
                { "0", "10" }, 
                { "10", "15" }, 
                { "15", "100" } 
            };


            connection = new Connection();
            connection.OpenConnection();

            string sqlCount = "SELECT COUNT (*) FROM Items";
            SqlCommand com = new SqlCommand(sqlCount, Connection._connection);
            countAll = (int)com.ExecuteScalar();

            string queryMain = "select * from Items, Manufacturer where Items.productProducer = Manufacturer.producerID ";

            string filterSearch;

            string filterDiscountStr;
            
            string filterSort;

           // queryMain += filterDiscountStr;
            

            if (textBoxSearch.Text != String.Empty)
            {
                filterSearch = " and Items.productName like '%" + textBoxSearch.Text + "%' ";
                queryMain += filterSearch;
            }

            if (discount != -1)
            {
                filterDiscountStr = " and Items.productMaxDiscountAmount between " + arrayDiscount[discount, 0] + " and "
                + arrayDiscount[discount, 1];
                queryMain += filterDiscountStr;
            }

            if (category != 0)
                queryMain += $" and [Items].productCategory = {category} ";

            if (sort == 1)
            {
                filterSort = " order by Items.productCost ASC ";
                queryMain += filterSort;
            }

            if (sort == 0)
            {
                filterSort = " order by Items.productCost DESC ";
                queryMain += filterSort;
            }

            var command = new SqlCommand(queryMain, Connection._connection);
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
                    sale = Convert.ToDouble(dataReader["productMaxDiscountAmount"]);

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

            comboBoxSort.Items.Add("По возрастанию");
            comboBoxSort.Items.Add("По убыванию");

            comboBoxDiscount.Items.Add("Не учитывается");
            comboBoxDiscount.Items.Add("От 0 до 10");
            comboBoxDiscount.Items.Add("От 10 до 15");
            comboBoxDiscount.Items.Add("От 15 до 100");

           /* comboBoxCategories.Text = comboBoxCategories.Items[0].ToString();
            comboBoxDiscount.Text = comboBoxDiscount.Items[0].ToString();
            comboBoxSort.Text = comboBoxSort.Items[0].ToString(); */

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

            dataFilling(Convert.ToInt32(comboBoxCategories.SelectedIndex.ToString()), Convert.ToInt32(comboBoxSort.SelectedIndex.ToString()), Convert.ToInt32(comboBoxDiscount.SelectedIndex.ToString()), textBoxSearch.Text);

        }

        private void comboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataFilling(Convert.ToInt32(comboBoxCategories.SelectedIndex.ToString()),  Convert.ToInt32(comboBoxSort.SelectedIndex.ToString()), Convert.ToInt32(comboBoxDiscount.SelectedIndex.ToString()), textBoxSearch.Text);
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataFilling(Convert.ToInt32(comboBoxCategories.SelectedIndex.ToString()), Convert.ToInt32(comboBoxSort.SelectedIndex.ToString()), Convert.ToInt32(comboBoxDiscount.SelectedIndex.ToString()), textBoxSearch.Text);

        }

        private void comboBoxDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataFilling(Convert.ToInt32(comboBoxCategories.SelectedIndex.ToString()), Convert.ToInt32(comboBoxSort.SelectedIndex.ToString()), Convert.ToInt32(comboBoxDiscount.SelectedIndex.ToString()), textBoxSearch.Text);

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            dataFilling(Convert.ToInt32(comboBoxCategories.SelectedIndex.ToString()), Convert.ToInt32(comboBoxSort.SelectedIndex.ToString()), Convert.ToInt32(comboBoxDiscount.SelectedIndex.ToString()), textBoxSearch.Text);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var authorizationView = new AuthorizationView();
            this.Hide();
            authorizationView.Show();
        }
    }
}
