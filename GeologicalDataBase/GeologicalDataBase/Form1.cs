using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeologicalDataBase
{
    public partial class GeoDataBase : Form
    {
        SqlConnection sqlConnection;

        public GeoDataBase()
        {
            InitializeComponent();
        }
        public void MethodForListView()
        {
            //часть кода с Form1_load
            string connection = @"Data Source=DESKTOP-BQTGVGM\DIPLOMDB;Initial Catalog=DIPLOM;Integrated Security=True";
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;

            listView1.Columns.Add("SKV");
            listView1.Columns.Add("X");
            listView1.Columns.Add("Y");
            listView1.Columns.Add("PR");
            listView1.Columns.Add("F5");
            listView1.Columns.Add("F6");
            listView1.Columns.Add("Описание пород");
            listView1.Columns.Add("Зони");
            listView1.Columns.Add("Au");
            listView1.Columns.Add("Ag");
            listView1.Columns.Add("As");
            listView1.Columns.Add("Cu");
            listView1.Columns.Add("Zn");
            listView1.Columns.Add("Pb");
            listView1.Columns.Add("Bi");
            listView1.Columns.Add("Mo");
            listView1.Columns.Add("W");
            listView1.Columns.Add("Ni");
            listView1.Columns.Add("Fe");
            listView1.Columns.Add("Mn");

        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dIPLOMDataSet.Test". При необходимости она может быть перемещена или удалена.
            this.testTableAdapter.Fill(this.dIPLOMDataSet.Test);
            string connection = @"Data Source=DESKTOP-BQTGVGM\DIPLOMDB;Initial Catalog=DIPLOM;Integrated Security=True";
            
            sqlConnection = new SqlConnection(connection);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            
            

            


            await LoadTest();
            //SqlCommand command = new SqlCommand("SELECT * FROM [Test]", sqlConnection);
            //try
            //{
            //    sqlReader = await command.ExecuteReaderAsync();
            //    while (await sqlReader.ReadAsync())
            //    {
            //        listBox1.Items.Add(Convert.ToString(sqlReader["ID"] + " " + sqlReader["SVP"]));
            //        //listBox1.Items.Add(Convert.ToString(sqlReader["ID"]+" "+ sqlReader["SKV"]+" "+ sqlReader["X"]+" "+ sqlReader["Y"]+" "+ sqlReader["PR"]+" "+ sqlReader["FROM"]+" "+ sqlReader["TO"]+" "+ sqlReader["Rock Description"]+" "+ sqlReader["Zones"]+" "+ sqlReader["Au"]+" "+ sqlReader["Ag"]+" "+ sqlReader["As"]+" "+ sqlReader["Cu"]+ " "+sqlReader["Zn"]+" "+ sqlReader["Pb"]+" "+ sqlReader["Bi"]+" "+ sqlReader["Mo"]+" "+ sqlReader["W"]+" "+ sqlReader["Ni"]+" "+ sqlReader["Fe"]+" "+ sqlReader["Mn"]));
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);


            //}
            //finally
            //{
            //    if (sqlReader != null)
            //    {
            //        sqlReader.Close();
            //    }

            //}


        }



        public async Task LoadTest() //SELECT, метод для ListView
        {
            SqlDataReader sqlReader = null;
            SqlCommand getStudentCommand = new SqlCommand("SELECT * FROM [Test] WHERE Au>5", sqlConnection);

            try
            {
                sqlReader = await getStudentCommand.ExecuteReaderAsync();

                while(await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlReader["SKV"]),
                        Convert.ToString(sqlReader["X"]),
                        Convert.ToString(sqlReader["Y"]),
                        Convert.ToString(sqlReader["PR"]),
                        Convert.ToString(sqlReader["F5"]),
                        Convert.ToString(sqlReader["F6"]),
                        Convert.ToString(sqlReader["Описание пород"]),
                        Convert.ToString(sqlReader["Зони"]),
                        Convert.ToString(sqlReader["Au"]),
                        Convert.ToString(sqlReader["Ag"]),
                        Convert.ToString(sqlReader["As"]),
                        Convert.ToString(sqlReader["Cu"]),
                        Convert.ToString(sqlReader["Zn"]),
                        Convert.ToString(sqlReader["Pb"]),
                        Convert.ToString(sqlReader["Bi"]),
                        Convert.ToString(sqlReader["Mo"]),
                        Convert.ToString(sqlReader["W"]),
                        Convert.ToString(sqlReader["Ni"]),
                        Convert.ToString(sqlReader["Fe"]),
                        Convert.ToString(sqlReader["Mn"])


                    });

                    listView1.Items.Add(item);

                    
                }
                

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(sqlReader!=null && sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void закритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sqlConnection!=null & sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private void GeoDataBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null & sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //if (label43.Visible)
            //{
            //    label43.Visible = false;
            //}
            //if (
            //   !string.IsNullOrEmpty(textBox43.Text) && !string.IsNullOrWhiteSpace(textBox43.Text)){


            //    SqlCommand command = new SqlCommand("INSERT INTO [Test] (SVP)VALUES(@SVP)", sqlConnection);
                
            //    command.Parameters.AddWithValue("SVP", textBox43.Text);

            //    await command.ExecuteNonQueryAsync();

            //}
            //else
            //{
            //    label43.Visible = true;
            //    label43.Text = "сука лошара введи нормальные данные";
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox43_TextChanged(object sender, EventArgs e)
        {

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Update();

            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Test]", sqlConnection);
            SqlCommand command1 = new SqlCommand("SELECT * FROM [WeatheringCrust]", sqlConnection);


            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["ID"] + " " + sqlReader["SVP"]));
                    //listBox1.Items.Add(Convert.ToString(sqlReader["ID"]+" "+ sqlReader["SKV"]+" "+ sqlReader["X"]+" "+ sqlReader["Y"]+" "+ sqlReader["PR"]+" "+ sqlReader["FROM"]+" "+ sqlReader["TO"]+" "+ sqlReader["Rock Description"]+" "+ sqlReader["Zones"]+" "+ sqlReader["Au"]+" "+ sqlReader["Ag"]+" "+ sqlReader["As"]+" "+ sqlReader["Cu"]+ " "+sqlReader["Zn"]+" "+ sqlReader["Pb"]+" "+ sqlReader["Bi"]+" "+ sqlReader["Mo"]+" "+ sqlReader["W"]+" "+ sqlReader["Ni"]+" "+ sqlReader["Fe"]+" "+ sqlReader["Mn"]));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }

            }
        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (label46.Visible)
            {
                label46.Visible = false;
            }
            if (
               !string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text)&&
               !string.IsNullOrEmpty(textBox44.Text) && !string.IsNullOrWhiteSpace(textBox44.Text))
            {


                SqlCommand command = new SqlCommand("UPDATE [Test] SET [SVP]=@SVP WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox1.Text);
                command.Parameters.AddWithValue("SVP", textBox44.Text); 

                await command.ExecuteNonQueryAsync();

            }
            else
            {
                label46.Visible = true;
                label46.Text = "сука лошара введи нормальные данные";
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void textBox44_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button5_Click(object sender, EventArgs e)
        {
            
            SqlCommand command = new SqlCommand("DELETE FROM [TEST] WHERE [Id]=@Id", sqlConnection);

            command.Parameters.AddWithValue("Id", textBox45.Text);
            

            await command.ExecuteNonQueryAsync();
        }

        private void textBox45_TextChanged(object sender, EventArgs e)
        {

        }

        private void вибратиЗаВмістомAuToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void вибратиЗаВмістомAuToolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void вибратиЗаВмістомAuToolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void експортВExelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
