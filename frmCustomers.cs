﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
namespace CPR
{
    public partial class frmCustomers : Form
    {

        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectionString cs = new ConnectionString();
        public frmCustomers()
        {
            InitializeComponent();
            txtVehicleID.Focus();            
        }
        private void Reset()
        {
            txtAddress.Text = "";
            txtCity.Text = "";
            txtEmail.Text = "";
            txtCustomerName.Text = "";
            txtContactNo1.Text = "";
            txtNotes.Text = "";
            txtContactNo.Text = "";
            txtVehicleID.Text = "";
            txtVehicleName.Text = "";
            btnSave.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtVehicleID.Focus();
        }
        private void frmCustomers_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Done");
            cmbStatus.Items.Add("Settle");
            cmbStatus.SelectedText = "Pending";
            txtVehicleID.Select();
        }
        private void auto()
        {
            txtVehicleID.Text = "C-" + GetUniqueKey(6);
        }
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtVehicleID.Text == "")
            {
                MessageBox.Show("Please enter vehicle ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVehicleID.Focus();
                return;
            }

            if (txtVehicleName.Text == "")
            {
                MessageBox.Show("Please enter vehicle name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVehicleName.Focus();
                return;
            }

            if (txtCustomerName.Text == "")
            {
                MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerName.Focus();
                return;
            }

            if (txtAddress.Text == "")
            {
                MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return;
            }
            if (txtCity.Text == "")
            {
                MessageBox.Show("Please enter city", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCity.Focus();
                return;
            }

            if (txtContactNo.Text == "")
            {
                MessageBox.Show("Please enter contact no.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtContactNo.Focus();
                return;
            }

            try
            {
                //auto();

                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "INSERT INTO [Customer]([VehicleId],[CustomerName],[Address],[City],[ContactNo],[ContactNo1],[Email],[Notes],[VehicleName],[status]) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10)";

                cmd = new SqlCommand(cb);

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txtVehicleID.Text);
                cmd.Parameters.AddWithValue("@d2", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@d3", txtAddress.Text);
                cmd.Parameters.AddWithValue("@d4", txtCity.Text);
                cmd.Parameters.AddWithValue("@d5", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@d6", txtContactNo1.Text);
                cmd.Parameters.AddWithValue("@d7", txtEmail.Text);
                cmd.Parameters.AddWithValue("@d8", txtNotes.Text);
                cmd.Parameters.AddWithValue("@d9", txtVehicleName.Text);
                cmd.Parameters.AddWithValue("@d10", "P");

                cmd.ExecuteReader();
                MessageBox.Show("Successfully saved", "Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string getFlag(string status)
        {
            if (cmbStatus.SelectedText == "Pending")
                return "P";
            else if (cmbStatus.SelectedText == "Done")
                return "D";
            else
                return "S";
        }

        private void delete_records()
        {

            try
            {

                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Customer where VehicleID='" + txtVehicleID.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;

                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("No record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


                if (MessageBox.Show("Do you really want to delete the record?", "Customer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    delete_records();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVehicleID.Text == "")
                {
                    MessageBox.Show("Please enter vehicle ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtVehicleID.Focus();
                    return;
                }

                if (txtVehicleName.Text == "")
                {
                    MessageBox.Show("Please enter vehicle name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtVehicleName.Focus();
                    return;
                }

                if (txtCustomerName.Text == "")
                {
                    MessageBox.Show("Please enter name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerName.Focus();
                    return;
                }

                if (txtAddress.Text == "")
                {
                    MessageBox.Show("Please enter address", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAddress.Focus();
                    return;
                }
                if (txtCity.Text == "")
                {
                    MessageBox.Show("Please enter city", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCity.Focus();
                    return;
                }

                if (txtContactNo.Text == "")
                {
                    MessageBox.Show("Please enter contact no.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContactNo.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update Customer set Customername=@d2,address=@d3,City=@d4,ContactNo=@d5,ContactNo1=@d6,Email=@d7,Notes=@d8,VehicleName=@d9,Status=@d10 where VehicleID=@d1";

                cmd = new SqlCommand(cb);

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txtVehicleID.Text);
                cmd.Parameters.AddWithValue("@d2", txtCustomerName.Text);
                cmd.Parameters.AddWithValue("@d3", txtAddress.Text);
                cmd.Parameters.AddWithValue("@d4", txtCity.Text);
                cmd.Parameters.AddWithValue("@d5", txtContactNo.Text);
                cmd.Parameters.AddWithValue("@d6", txtContactNo1.Text);
                cmd.Parameters.AddWithValue("@d7", txtEmail.Text);
                cmd.Parameters.AddWithValue("@d8", txtNotes.Text);
                cmd.Parameters.AddWithValue("@d9", txtVehicleName.Text);
                cmd.Parameters.AddWithValue("@d10", "P");
                cmd.ExecuteReader();
                MessageBox.Show("Successfully updated", "Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUpdate.Enabled = false;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtContactNo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomersRecord frm = new frmCustomersRecord();
            frm.Show();
            frm.GetData();
        }

        private void txtVehicleID_Leave(object sender, EventArgs e)
        {
            DataSet myDataSet = new DataSet();
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string str = "SELECT RTRIM(VehicleID)as [Vehicle ID],(VehicleName)as [Vehicle Name],RTRIM(Customername) as [Customer Name],RTRIM(address) as [Address],RTRIM(city) as [City],RTRIM(ContactNo) as [Contact No.],RTRIM(ContactNo1) as [Contact No. 1],(email) as [Email],(notes) as [Notes] from Customer where vehicleId= '" + txtVehicleID.Text + "' order by CustomerName";
                cmd = new SqlCommand(str, con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                myDA.Fill(myDataSet, "Customer");
                con.Close();

                if (myDataSet.Tables["Customer"].Rows.Count != 0)
                {
                    txtVehicleID.Text = myDataSet.Tables[0].Rows[0]["Vehicle Id"].ToString();
                    txtVehicleName.Text = myDataSet.Tables[0].Rows[0]["Vehicle Name"].ToString();
                    txtCustomerName.Text = myDataSet.Tables[0].Rows[0]["Customer Name"].ToString();
                    txtAddress.Text = myDataSet.Tables[0].Rows[0]["Address"].ToString();
                    txtCity.Text = myDataSet.Tables[0].Rows[0]["City"].ToString();
                    txtContactNo.Text = myDataSet.Tables[0].Rows[0]["Contact No."].ToString();
                    txtContactNo1.Text = myDataSet.Tables[0].Rows[0]["Contact No. 1"].ToString();
                    txtEmail.Text = myDataSet.Tables[0].Rows[0]["Email"].ToString();
                    txtNotes.Text = myDataSet.Tables[0].Rows[0]["Notes"].ToString();
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCustomers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
