using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
namespace CPR
{
    public partial class frmBilling : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        ConnectionString cs = new ConnectionString();
        SqlDataReader rdr;
        bool isMenuclick = false;
        public frmBilling()
        {
            InitializeComponent();
            txtTaxPer.Text = "0";
            txtTaxAmt.Text ="0";
            txtDiscountPer.Text = "0";
            txtDiscountAmount.Text = "0";
            txtSubTotal.Text = "0";
        }
        public frmBilling(bool isMenuClickp)
        {
            InitializeComponent();
            txtTaxPer.Text = "";
            txtTaxAmt.Text = "";
            txtDiscountPer.Text = "0";
            this.isMenuclick = isMenuClickp;
            txtDiscountAmount.Text = "0";
        }
        private void auto()
        {
            txtInvoiceNo.Text = "INV-" + GetUniqueKey(8);

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

        public void FillCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select checkpointid, RTRIM(checkpointname) as CheckpointName from checkpoints";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                Dictionary<string, string> test = new Dictionary<string, string>();
                while (rdr.Read())
                {
                    //cmbCheckpoint.Items.Add(rdr[1]);
                    test.Add(rdr[0].ToString(), rdr[1].ToString());
                    //cmbCheckpoint.SelectedIndex = 0;
                }
                cmbCheckpoint.DataSource = new BindingSource(test, null);
                cmbCheckpoint.DisplayMember = "Value";
                cmbCheckpoint.ValueMember = "Key";

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVehicleId.Text == "")
                {
                    MessageBox.Show("Please retrieve Customer Details", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Button1.Focus();
                    return;
                }

                //if (txtTaxPer.Text == "")
                //{
                //    MessageBox.Show("Please enter tax percentage", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txtTaxPer.Focus();
                //    return;
                //}
                if (txtDiscountPer.Text == "")
                {
                    MessageBox.Show("Please enter discount percentage", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDiscountPer.Focus();
                    return;
                }
                if (txtTotalPayment.Text == "")
                {
                    MessageBox.Show("Please enter total payment", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTotalPayment.Focus();
                    return;
                }

                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("sorry no product added", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                auto();

                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert Into Invoice_Info(InvoiceNo,InvoiceDate,VehicleId,SubTotal,VATPer,VATAmount,DiscountPer,DiscountAmount,GrandTotal,TotalPayment,PaymentDue,Remarks) VALUES ('" + txtInvoiceNo.Text + "','" + dtpInvoiceDate.Text + "','" + txtVehicleId.Text + "'," + txtSubTotal.Text + "," + txtTaxPer.Text + "," + txtTaxAmt.Text + "," + txtDiscountPer.Text + "," + txtDiscountAmount.Text + "," + txtTotal.Text + "," + txtTotalPayment.Text + "," + txtPaymentDue.Text + ",'" + txtRemarks.Text + "')";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                for (int i = 0; i <= ListView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);

                    string cd = "insert Into ProductRepaired(InvoiceNo,CheckpointID,CheckpointName,Quantity,Charges,TotalAmount) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                    cmd = new SqlCommand(cd);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", txtInvoiceNo.Text);
                    cmd.Parameters.AddWithValue("d2", ListView1.Items[i].SubItems[1].Text);
                    cmd.Parameters.AddWithValue("d3", ListView1.Items[i].SubItems[2].Text);
                    cmd.Parameters.AddWithValue("d4", ListView1.Items[i].SubItems[4].Text);
                    cmd.Parameters.AddWithValue("d5", ListView1.Items[i].SubItems[3].Text);
                    cmd.Parameters.AddWithValue("d6", ListView1.Items[i].SubItems[5].Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                try
                {
                    cb = "update Customer set status = 'S' where vehicleid = '" + txtVehicleId.Text + "'";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                    btnUpdate.Enabled = false;
                    MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                con.Close();

                Save.Enabled = false;
                btnPrint.Enabled = true;

                MessageBox.Show("Successfully completed", "Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomersRecord1 frm = new frmCustomersRecord1();
            frm.Visible = true;
        }

        public void Calculate()
        {
            if (!isMenuclick)
            {
                //if (txtTaxPer.Text != "")
                //{
                //    txtTaxAmt.Text = Convert.ToInt32((Convert.ToInt32(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100)).ToString();

                //}
                //if (txtDiscountPer.Text != "")
                //{
                //    txtDiscountAmount.Text = Convert.ToInt32(((Convert.ToInt32(txtSubTotal.Text) + Convert.ToInt32(txtTaxAmt.Text)) * Convert.ToDouble(txtDiscountPer.Text) / 100)).ToString();
                //}
                if (txtDiscountPer.Text != "" && txtDiscountPer.Text != "0")
                {
                    txtDiscountAmount.Text = Convert.ToInt32(((Convert.ToInt32(txtSubTotal.Text)) * Convert.ToDouble(txtDiscountPer.Text) / 100)).ToString();
                }
                int val1 = 0;
                int val2 = 0;
                int val3 = 0;
                int val4 = 0;
                int val5 = 0;
                int.TryParse(txtTaxAmt.Text, out val1);
                int.TryParse(txtSubTotal.Text, out val2);
                int.TryParse(txtDiscountAmount.Text, out val3);
                int.TryParse(txtTotal.Text, out val4);
                int.TryParse(txtTotalPayment.Text, out val5);
                val4 = val1 + val2 - val3;
                txtTotal.Text = val4.ToString();
                int I = (val4 - val5);
                txtPaymentDue.Text = I.ToString();
            }
        }
        private void txtSaleQty_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtCharges.Text, out val1);
            int.TryParse(txtQty.Text, out val2);
            int I = (val1 * val2);
            txtTotalAmount.Text = I.ToString();
        }

        public double subtot()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            i = 0;
            j = 0;
            k = 0;


            try
            {

                j = ListView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    k = k + Convert.ToInt32(ListView1.Items[i].SubItems[5].Text);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return k;

        }
        private void auto1()
        {
            txtCheckpointID.Text = "P-" + GetUniqueKey1(4);
        }
        public static string GetUniqueKey1(int maxSize)
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
        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtVehicleId.Text == "")
                {
                    MessageBox.Show("Please retrieve Customer ID", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtVehicleId.Focus();
                    return;
                }

                if (cmbCheckpoint.Text == "")
                {
                    MessageBox.Show("Please retrieve product name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbCheckpoint.Focus();
                    return;
                }
                if (txtCharges.Text == "")
                {
                    MessageBox.Show("Please enter charges", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCharges.Focus();
                    return;
                }
                if (txtQty.Text == "")
                {
                    MessageBox.Show("Please enter no. of quantity", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQty.Focus();
                    return;
                }
                int Qty = Convert.ToInt32(txtQty.Text);
                if (Qty == 0)
                {
                    MessageBox.Show("no. of quantity can not be zero", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQty.Focus();
                    return;
                }
                //auto1();
                if (ListView1.Items.Count == 0)
                {

                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(((KeyValuePair<string, string>)cmbCheckpoint.SelectedItem).Key.ToString());
                    lst.SubItems.Add(((KeyValuePair<string, string>)cmbCheckpoint.SelectedItem).Value.ToString());
                    lst.SubItems.Add(txtCharges.Text);
                    lst.SubItems.Add(txtQty.Text);
                    lst.SubItems.Add(txtTotalAmount.Text);
                    ListView1.Items.Add(lst);
                    txtSubTotal.Text = subtot().ToString();

                    Calculate();
                    cmbCheckpoint.Text = "";
                    txtCheckpointID.Text = "";
                    txtCharges.Text = "";
                    txtQty.Text = "";
                    txtTotalAmount.Text = "";
                    return;
                }


                ListViewItem lst1 = new ListViewItem();

                lst1.SubItems.Add(((KeyValuePair<string, string>)cmbCheckpoint.SelectedItem).Key.ToString());
                lst1.SubItems.Add(((KeyValuePair<string, string>)cmbCheckpoint.SelectedItem).Value.ToString());
                lst1.SubItems.Add(txtCharges.Text);
                lst1.SubItems.Add(txtQty.Text);
                lst1.SubItems.Add(txtTotalAmount.Text);
                ListView1.Items.Add(lst1);
                txtSubTotal.Text = subtot().ToString();
                Calculate();
                cmbCheckpoint.Text = "";
                txtCheckpointID.Text = "";
                txtCharges.Text = "";
                txtQty.Text = "";
                txtTotalAmount.Text = "";
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListView1.Items.Count == 0)
                {
                    MessageBox.Show("No items to remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int itmCnt = 0;
                    int i = 0;
                    int t = 0;

                    ListView1.FocusedItem.Remove();
                    itmCnt = ListView1.Items.Count;
                    t = 1;

                    for (i = 1; i <= itmCnt + 1; i++)
                    {
                        //Dim lst1 As New ListViewItem(i)
                        //ListView1.Items(i).SubItems(0).Text = t
                        t = t + 1;

                    }
                    txtSubTotal.Text = subtot().ToString();
                    Calculate();
                }

                btnRemove.Enabled = false;
                if (ListView1.Items.Count == 0)
                {
                    txtSubTotal.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTaxPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaxPer.Text))
                {
                    txtTaxAmt.Text = "";
                    txtTotal.Text = "";
                    return;
                }
                Calculate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = true;
        }

        private void Reset()
        {
            txtInvoiceNo.Text = "";
            dtpInvoiceDate.Text = DateTime.Today.ToString();
            txtVehicleId.Text = "";
            txtCustomerName.Text = "";
            cmbCheckpoint.Text = "";
            txtCheckpointID.Text = "";
            txtCharges.Text = "";
            txtQty.Text = "";
            txtTotalAmount.Text = "";
            ListView1.Items.Clear();


            txtSubTotal.Text = "";
            txtTotal.Text = "";
            txtTotalPayment.Text = "";
            txtPaymentDue.Text = "";
            txtRemarks.Text = "";
            Save.Enabled = true;
            Delete.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            btnPrint.Enabled = false;
            ListView1.Enabled = true;
            Button7.Enabled = true;

        }

        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
            Reset();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }
        private void delete_records()
        {

            try
            {

                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq1 = "delete from productRepaired where InvoiceNo='" + txtInvoiceNo.Text + "'";
                cmd = new SqlCommand(cq1);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                con.Close();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Invoice_Info where InvoiceNo='" + txtInvoiceNo.Text + "'";
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
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.lblUser.Text = label6.Text;
            frm.Show();
        }

        private void txtTotalPayment_TextChanged(object sender, EventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtTotal.Text, out val1);
            int.TryParse(txtTotalPayment.Text, out val2);
            int I = (val1 - val2);
            txtPaymentDue.Text = I.ToString();
        }

        private void txtTotalPayment_Validating(object sender, CancelEventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtTotal.Text, out val1);
            int.TryParse(txtTotalPayment.Text, out val2);
            if (val2 > val1)
            {
                MessageBox.Show("Total Payment can't be more than grand total", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPayment.Text = "";
                txtPaymentDue.Text = "";
                txtTotalPayment.Focus();
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptInvoice rpt = new rptInvoice();
                //The report you created.
                cmd = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                CPR_DBDataSet myDS = new CPR_DBDataSet();
                //The DataSet you created.
                con = new SqlConnection(cs.DBConn);
                cmd.Connection = con;
                cmd.CommandText = "SELECT * from invoice_info,ProductRepaired,customer where invoice_info.invoiceno=ProductRepaired.invoiceno and invoice_info.VehicleId=Customer.VehicleId and Invoice_info.invoiceNo='" + txtInvoiceNo.Text + "'";
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS, "Invoice_Info");
                myDA.Fill(myDS, "ProductRepaired");
                myDA.Fill(myDS, "Customer");
                rpt.SetDataSource(myDS);
                frmInvoiceReport frm = new frmInvoiceReport();
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String cb = "update Invoice_info set VehicleId='" + txtVehicleId.Text + "',DiscountPer=" + txtDiscountPer.Text + ",DiscountAmount=" + txtDiscountAmount.Text + ",GrandTotal= " + txtTotal.Text + ",TotalPayment= " + txtTotalPayment.Text + ",PaymentDue= " + txtPaymentDue.Text + ",Remarks='" + txtRemarks.Text + "' where Invoiceno= '" + txtInvoiceNo.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                btnUpdate.Enabled = false;
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBillingRecords frm = new frmBillingRecords();
            frm.DataGridView1.DataSource = null;
            frm.dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            frm.dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            frm.GroupBox3.Visible = false;
            frm.DataGridView3.DataSource = null;
            frm.cmbCustomerName.Text = "";
            frm.GroupBox4.Visible = false;
            frm.DateTimePicker1.Text = DateTime.Today.ToString();
            frm.DateTimePicker2.Text = DateTime.Today.ToString();
            frm.DataGridView2.DataSource = null;
            frm.GroupBox10.Visible = false;
            frm.FillCombo();
            frm.label9.Text = label6.Text;
            frm.Show();
        }

        private void txtSaleQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTotalPayment_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTaxPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtDiscountPer_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void frmProductRepair_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Done");
            cmbStatus.Items.Add("Settle");
            cmbStatus.SelectedText = "Settle";
            FillCombo();
        }

        private void txtCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        }

        private void frmBilling_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}