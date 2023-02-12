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
    public partial class Doctor : Form
    {
        public Doctor()
        {
            InitializeComponent();
            DisplayDoc();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\Documents\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayDoc()
        {
            Con.Open();
            String Query = "Select * from DoctorTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DoctorDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void Clear()
        {
            DNameTb.Text = "";
            DocPhoneTb.Text = "";
            DocAddTb.Text = "";
            DocExpTb.Text = "";
            DocPassWordTb.Text = "";
            DocGenCb.SelectedIndex = 0;
            DocSpecCb.SelectedIndex = 0;
            key = 0;
        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (DNameTb.Text == "" || DocPassWordTb.Text == "" || DocPhoneTb.Text == "" || DocAddTb.Text == "" || DocGenCb.SelectedIndex == -1 || DocSpecCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into DoctorTbl(DocName,DOCDOB,DOCGEN,DOCSPEC,DOCEXP,DOCPHONE,DOCADD,DOCPASS)values(@DN,@DD,@DG,@DS,@DE,@DP,@DA,@DPA)", Con);
                    cmd.Parameters.AddWithValue("@DN", DNameTb.Text);
                    cmd.Parameters.AddWithValue("@DD", DocDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@DG", DocGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DS", DocSpecCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DE", DocExpTb.Text);
                    cmd.Parameters.AddWithValue("@DP", DocPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DA", DocAddTb.Text);
                    cmd.Parameters.AddWithValue("@DPA", DocPassWordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Added");


                    Con.Close();
                    DisplayDoc();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        private void DoctorDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DNameTb.Text = DoctorDGV.SelectedRows[0].Cells[1].Value.ToString();
            DocDOB.Text = DoctorDGV.SelectedRows[0].Cells[2].Value.ToString();
            DocGenCb.SelectedItem = DoctorDGV.SelectedRows[0].Cells[3].Value.ToString();
            DocSpecCb.SelectedItem = DoctorDGV.SelectedRows[0].Cells[4].Value.ToString();
            DocExpTb.Text = DoctorDGV.SelectedRows[0].Cells[5].Value.ToString();
            DocPhoneTb.Text = DoctorDGV.SelectedRows[0].Cells[6].Value.ToString();
            DocAddTb.Text = DoctorDGV.SelectedRows[0].Cells[7].Value.ToString();
            DocPassWordTb.Text = DoctorDGV.SelectedRows[0].Cells[8].Value.ToString();
            if (DNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(DoctorDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (DNameTb.Text == "" || DocPassWordTb.Text == "" || DocPhoneTb.Text == "" || DocAddTb.Text == "" || DocGenCb.SelectedIndex == -1 || DocSpecCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update DoctorTbl set DocName=@DN,DOCDOB=@DD,DOCGEN=@DG,DOCSPEC=@DS,DOCEXP=@DE,DOCPHONE=@DP,DOCADD=@DA,DOCPASS=@DPA where DOCID=@Dkey", Con);
                    cmd.Parameters.AddWithValue("@DN", DNameTb.Text);
                    cmd.Parameters.AddWithValue("@DD", DocDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@DG", DocGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DS", DocSpecCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DE", DocExpTb.Text);
                    cmd.Parameters.AddWithValue("@DP", DocPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DA", DocAddTb.Text);
                    cmd.Parameters.AddWithValue("@DPA", DocPassWordTb.Text);
                    cmd.Parameters.AddWithValue("@Dkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Updated");


                    Con.Close();
                    DisplayDoc();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Doctor");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from DoctorTbl where DOCId=@Dkey", Con);

                    cmd.Parameters.AddWithValue("@Dkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Doctor Deleted");


                    Con.Close();
                    DisplayDoc();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void DocDOB_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
