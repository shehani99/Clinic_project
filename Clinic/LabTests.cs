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

namespace Clinic
{
    public partial class LabTests : Form
    {
        public LabTests()
        {
            InitializeComponent();
            DisplayTest();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\Documents\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayTest()
        {
            Con.Open();
            String Query = "Select * from TestTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            LabTestDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void Clear()
        {
            LabTestTb.Text = "";
            LabCostTb.Text = "";
            key = 0;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (LabTestTb.Text == "" || LabCostTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into TestTbl(TestName,TestCost)values(@TN,@TC)", Con);
                    cmd.Parameters.AddWithValue("@TN", LabTestTb.Text);
                    cmd.Parameters.AddWithValue("@TC", LabCostTb.Text);
                    

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lab Test Added");


                    Con.Close();
                    DisplayTest();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (LabTestTb.Text == "" || LabCostTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update TestTbl set TestName=@TN,TestCost=@TC where TestNum=@Tkey", Con);
                    cmd.Parameters.AddWithValue("@TN", LabTestTb.Text);
                    cmd.Parameters.AddWithValue("TC", LabCostTb.Text);
                    cmd.Parameters.AddWithValue("@Tkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lab Test Updated");


                    Con.Close();
                    DisplayTest();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void LabTestDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LabTestTb.Text = LabTestDGV.SelectedRows[0].Cells[1].Value.ToString();
            LabCostTb.Text = LabTestDGV.SelectedRows[0].Cells[2].Value.ToString();

            if (LabTestTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(LabTestDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Test");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from TestTbl where TestNum=@Tkey", Con);

                    cmd.Parameters.AddWithValue("@Tkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lab Test Deleted");


                    Con.Close();
                    DisplayTest();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void LabTestTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
