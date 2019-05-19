using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using CPR.Properties;
namespace CPR
{
    public partial class frmMainMenu : Form
    {
        SqlDataReader rdr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectionString cs = new ConnectionString();
        Image refresh = Resources.refresh;

        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomers frm = new frmCustomers();
            frm.Show();
        }

        private void registrationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmRegistration frm = new frmRegistration();
            frm.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.Show();
        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistration frm = new frmRegistration();
            frm.Show();
        }

        private void profileEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomers frm = new frmCustomers();
            frm.Show();
        }


        private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Notepad.exe");
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }

        private void wordpadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Wordpad.exe");
        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("TaskMgr.exe");
        }

        private void mSWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Winword.exe");
        }



        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            ToolStripStatusLabel4.Text = System.DateTime.Now.ToString();

            GetData();
            GetDone();
            GetSettle();
        }

        public void GetData()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select UPPER(VehicleId) as 'VehicleId', CustomerName from Customer where Status='P'", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Customer");
                //dataGridView3.DataSource = myDataSet.Tables["Customer"].DefaultView;
                dataGridPending.Columns[0].Name = "VehicleId";
                dataGridPending.Columns[0].DataPropertyName = "VehicleId";
                dataGridPending.Columns[1].Name = "CustomerName";
                dataGridPending.Columns[1].DataPropertyName = "CustomerName";
                dataGridPending.DataSource = myDataSet.Tables["Customer"];
                con.Close();

                dataGridPending.BorderStyle = BorderStyle.None;
                dataGridPending.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridPending.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridPending.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridPending.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridPending.BackgroundColor = Color.White;
                dataGridPending.EnableHeadersVisualStyles = false;
                dataGridPending.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridPending.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridPending.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridPending.Columns[0].DefaultCellStyle.Font = new Font("Cambria", 14, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetDone()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select UPPER(VehicleId) as 'VehicleId', CustomerName from Customer where Status='D'", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Customer");
                //dataGridView3.DataSource = myDataSet.Tables["Customer"].DefaultView;
                dataGridDone.Columns[0].Name = "VehicleId";
                dataGridDone.Columns[0].DataPropertyName = "VehicleId";
                dataGridDone.Columns[1].Name = "CustomerName";
                dataGridDone.Columns[1].DataPropertyName = "CustomerName";
                dataGridDone.DataSource = myDataSet.Tables["Customer"];
                con.Close();

                dataGridDone.BorderStyle = BorderStyle.None;
                dataGridDone.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridDone.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridDone.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridDone.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridDone.BackgroundColor = Color.White;
                dataGridDone.EnableHeadersVisualStyles = false;
                dataGridDone.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridDone.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridDone.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridDone.Columns[0].DefaultCellStyle.Font = new Font("Cambria", 14, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetSettle()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select UPPER(VehicleId) as 'VehicleId', CustomerName from Customer where Status='S'", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Customer");
                //dataGridView6.DataSource = myDataSet.Tables["Customer"].DefaultView;
                dataGridSettle.Columns[0].Name = "VehicleId";
                dataGridSettle.Columns[0].DataPropertyName = "VehicleId";
                dataGridSettle.Columns[1].Name = "CustomerName";
                dataGridSettle.Columns[1].DataPropertyName = "CustomerName";
                dataGridSettle.DataSource = myDataSet.Tables["Customer"];
                con.Close();

                dataGridSettle.BorderStyle = BorderStyle.None;
                dataGridSettle.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridSettle.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridSettle.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridSettle.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridSettle.BackgroundColor = Color.White;
                dataGridSettle.EnableHeadersVisualStyles = false;
                dataGridSettle.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridSettle.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridSettle.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridSettle.Columns[0].DefaultCellStyle.Font = new Font("Cambria", 14, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ToolStripStatusLabel4.Text = System.DateTime.Now.ToString();
        }





        private void loginDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoginDetails frm = new frmLoginDetails();
            frm.Show();
        }


        private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBilling frm = new frmBilling();
            frm.label6.Text = lblUser.Text;
            frm.Show();
        }

        private void checkpointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCheckpoint frm = new frmCheckpoint();
            frm.Show();
        }

        private void dataGridDone_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want to send the message ?", "Send message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                //Do something
            }
            else
            {
            }
        }

        private void dataGridPending_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure servicing is done ?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();

                    DataGridViewRow dr = dataGridPending.SelectedRows[0];
                    string cb = "update Customer set status = 'D' where vehicleid = '" + dr.Cells[0].Value.ToString() + "'";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GetData();
                    GetDone();
                    GetSettle();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData();
            GetDone();
            GetSettle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
        }

        private void billingRecordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBillingRecords frm = new frmBillingRecords(true);
            frm.Show();
        }

        private void dataGridDone_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Hide();
            frmBilling frm = new frmBilling();
            frm.label6.Text = lblUser.Text;


            try
            {
                DataGridViewRow dr = dataGridDone.SelectedRows[0];
                this.Hide();
                frmBilling frm1 = new frmBilling();
                frm1.Show();
                frm1.txtVehicleId.Text = dr.Cells[0].Value.ToString();
                frm1.txtCustomerName.Text = dr.Cells[1].Value.ToString();
                frm1.label6.Text = label1.Text;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            frm.Show();
        }

        private void dataGridDone_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Hide();
            try
            {
                this.Hide();
                frmBilling frm1 = new frmBilling();

                con.Open();
                cmd = new SqlCommand("select UPPER(VehicleId) as 'VehicleId', CustomerName from Customer where vehicleid='" + dataGridDone.SelectedCells[0].Value + "'", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Customer");

                frm1.txtVehicleId.Text = myDataSet.Tables["Customer"].Rows[0]["VehicleId"].ToString();
                frm1.txtCustomerName.Text = myDataSet.Tables["Customer"].Rows[0]["CustomerName"].ToString();
                frm1.label6.Text = label1.Text;
                frm1.Show();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
