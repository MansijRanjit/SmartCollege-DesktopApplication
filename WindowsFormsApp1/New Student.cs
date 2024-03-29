﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class New_Student : Form
    {

        public New_Student()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFullName.Text.Trim() != "" && txtGuardianName.Text.Trim() != "" && txtMobile.Text.Trim() != "" && txtEmail.Text.Trim() != "" && txtSemester.Text.Trim() != "" && txtProgram.Text.Trim() != "" && txtSchoolName.Text.Trim() != "" && txtBatch.Text.Trim() != "" && txtAddress.Text.Trim() != "")
            {
                String name = txtFullName.Text;
                String gname = txtGuardianName.Text;
                String gender = "";
                bool isChecked = radioButtonMale.Checked;
                if (isChecked)
                {
                    gender = radioButtonMale.Text;
                }
                else
                {
                    gender = radioButtonFemale.Text;
                }
                String dob = dateTimePickerDOB.Text;
                Int64 mobile = Int64.Parse(txtMobile.Text);
                String email = txtEmail.Text;
                String semester = txtSemester.Text;
                String program = txtProgram.Text;
                String sname = txtSchoolName.Text;
                String batch = txtBatch.Text;
                String add = txtAddress.Text;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = "data source = MANSIJ\\SQLEXPRESS; database =college; integrated security =True";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "insert into NewStudent (fname,gname,gender,dob,mobile,email,semester,prog,sname,batch,addres) values ('" + name + "','" + gname + "','" + gender + "','" + dob + "'," + mobile + ",'" + email + "','" + semester + "','" + program + "','" + sname + "','" + batch + "','" + add + "')";

                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);
                con.Close();
               if( MessageBox.Show("Data Saved. Remember the Registration ID", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information)== DialogResult.OK)
                {
                    txtFullName.Clear();
                    txtGuardianName.Clear();
                    txtAddress.Clear();
                    radioButtonFemale.Checked = false;
                    radioButtonMale.Checked = false;
                    txtMobile.Clear();
                    txtEmail.Clear();
                    txtProgram.ResetText();
                    txtSemester.ResetText();
                    txtSchoolName.Clear();
                    txtBatch.Clear();

                    SqlConnection con2 = new SqlConnection();
                    con2.ConnectionString = "data source = MANSIJ\\SQLEXPRESS ; database =college; integrated security =True";
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = con2;

                    cmd2.CommandText = "select max(NSID) from NewStudent";

                    SqlDataAdapter DA2 = new SqlDataAdapter(cmd2);
                    DataSet DS2 = new DataSet();
                    DA2.Fill(DS2);

                   Int64 abc = Convert.ToInt64(DS2.Tables[0].Rows[0][0]);
                   label13.Text = (abc + 1).ToString();

                }
            }
            else
            {
                MessageBox.Show("Please Fill All The Information and Submit ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtFullName.Clear();
            txtGuardianName.Clear();
            txtAddress.Clear();
            radioButtonFemale.Checked = false;
            radioButtonMale.Checked = false;
            txtMobile.Clear();
            txtEmail.Clear();
            txtProgram.ResetText();
            txtSemester.ResetText();
            txtSchoolName.Clear();
            txtBatch.Clear();

        }

        private void New_Student_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "data source = MANSIJ\\SQLEXPRESS ; database =college; integrated security =True";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "select max(NSID) from NewStudent";

            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

           Int64 abc = Convert.ToInt64(DS.Tables[0].Rows[0][0]);
           label13.Text = (abc+1).ToString();
           
        }
        //validations
        private void txtMobile_Validating(object sender, CancelEventArgs e)
        {
            if (((txtMobile.TextLength == 10) || (txtMobile.TextLength == 7)) && (txtMobile.Text.Any(char.IsNumber)))
            {
                errorProviderMobile.SetError(txtMobile, "");

            }
            else
            {
                txtMobile.Focus();
                errorProviderMobile.SetError(txtMobile, "Please Enter Valid Phone Number");
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if ((txtEmail.Text != string.Empty) && (txtEmail.Text.Contains("@")) && (txtEmail.Text.Contains(".")))
            {
                errorProviderEmail.SetError(txtEmail, "");
            }
            else
            {
                txtEmail.Focus();
                errorProviderEmail.SetError(txtEmail, "Please enter valid Email");
            }
        }

       

        private void txtProgram_MouseEnter(object sender, EventArgs e)
        {
            txtProgram.DroppedDown = true;
        }

        private void txtSemester_MouseEnter(object sender, EventArgs e)
        {
            txtSemester.DroppedDown = true;
        }

        private void txtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtGuardianName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
