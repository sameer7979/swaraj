using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CPR
{
    partial class frmCheckpoint : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectionString cs = new ConnectionString();
        public frmCheckpoint()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCheckpoint.Text == "")
            {
                MessageBox.Show("Please enter Checkpoint name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCheckpoint.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "INSERT INTO [Checkpoints]([CheckpointName]) VALUES (@d1)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txtCheckpoint.Text);
                cmd.ExecuteReader();
                LoadCheckpoints();
                MessageBox.Show("Successfully saved", "Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCheckpoint_Load(object sender, EventArgs e)
        {
            LoadCheckpoints();
        }

        private void LoadCheckpoints()
        {
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = new SqlCommand("select CheckpointName, CheckpointId from Checkpoints", con);
            SqlDataAdapter myDA = new SqlDataAdapter(cmd);
            DataSet myDataSet = new DataSet();
            myDA.Fill(myDataSet, "Checkpoints");
            //dataGridView3.DataSource = myDataSet.Tables["Customer"].DefaultView;
            dataGridCheckpoint.Columns[0].Name = "CheckpointName";
            dataGridCheckpoint.Columns[0].DataPropertyName = "CheckpointName";
            dataGridCheckpoint.DataSource = myDataSet.Tables["Checkpoints"];
            con.Close();
        }

        private void dataGridCheckpoint_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridCheckpoint.SelectedRows[0];
                txtCheckpointId.Text = dr.Cells[1].Value.ToString();
                txtCheckpoint.Text = dr.Cells[0].Value.ToString();
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnUpdate.Focus();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCheckpoint.Text == "")
            {
                MessageBox.Show("Please enter Checkpoint name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCheckpoint.Focus();
                return;
            }
            try
            {
                string cb = "update Checkpoints set CheckpointName = @d1 where CheckpointID = '" + txtCheckpointId.Text + "'";
                con.Open();
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txtCheckpoint.Text);
                cmd.ExecuteReader();

                MessageBox.Show("Successfully saved", "Customer Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Focus();
                txtCheckpoint.Text = "";
                LoadCheckpoints();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCheckpoint.Text == "")
            {
                MessageBox.Show("Please select row", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCheckpoint.Focus();
                return;
            }
            try
            {
                string cb = "delete Checkpoints where CheckpointID = '" + txtCheckpointId.Text + "'";
                con.Open();
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                int a = cmd.ExecuteNonQuery();
                if (a == -1)
                {
                    MessageBox.Show("It is used in somewhere else", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Focus();
                txtCheckpoint.Text = "";
                LoadCheckpoints();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                MessageBox.Show("It is used in somewhere else", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCheckpoint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
