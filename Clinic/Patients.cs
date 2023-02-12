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
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
            DisplayPat();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ASUS\Documents\clinicDb.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayPat()
        {
            Con.Open();
            String Query = "Select * from PatientTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PatientsDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void Clear()
        {
            PatNameTb.Text = "";
            PatGenCb.SelectedIndex = 0;
            PatAllTb.Text = "";
            PatAddTb.Text = "";
            PatPhoneTb.Text = "";
            key = 0;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || PatAllTb.Text == "" || PatAddTb.Text == "" || PatPhoneTb.Text == "" || PatGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into PatientTbl(PatName,PatGen,PatDOB,PatAdd,PatPhone,PatAll)values(@PN,@PG,@PD,@PA,@PP,@PAl)", Con);
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@PG", PatGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PA", PatAddTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PAl", PatAllTb.Text);
                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Added");


                    Con.Close();
                    DisplayPat();
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
            if (PatNameTb.Text == "" || PatAllTb.Text == "" || PatAddTb.Text == "" || PatPhoneTb.Text == "" || PatGenCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update DoctorTbl set PatName=@PN,PatGen=@PG,PatDOB=@PD,PatAdd=@PA,PatPhone=@PP,PatAll=@PAl where PatId=@Pkey", Con);
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@PG", PatGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PA", PatAddTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PAl", PatAllTb.Text);
                    cmd.Parameters.AddWithValue("@Pkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Updated");


                    Con.Close();
                    DisplayPat();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void PatientsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PatNameTb.Text = PatientsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PatGenCb.SelectedItem = PatientsDGV.SelectedRows[0].Cells[2].Value.ToString();
            PatDOB.Text = PatientsDGV.SelectedRows[0].Cells[3].Value.ToString();
            PatAddTb.Text = PatientsDGV.SelectedRows[0].Cells[4].Value.ToString();
            PatPhoneTb.Text = PatientsDGV.SelectedRows[0].Cells[5].Value.ToString();
            PatAllTb.Text = PatientsDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (PatNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PatientsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select The Patient");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from PatientTbl where PatId=@Pkey", Con);

                    cmd.Parameters.AddWithValue("@Pkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Deleted");


                    Con.Close();
                    DisplayPat();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        
    }
    
}
