using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace CPR
{
    public partial class frmBillingRecords : Form
    {

        DataTable dTable;
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        bool isMenuclick = false;

        public frmBillingRecords()
        {
            InitializeComponent();
        }

        public frmBillingRecords(bool isMenuClickp)
        {
            InitializeComponent();
            this.isMenuclick = isMenuClickp;
        }

        private void frmSalesRecord_Load(object sender, EventArgs e)
        {
            FillCombo();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (DataGridView1.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = DataGridView1.RowCount - 1;
                colsTotal = DataGridView1.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = DataGridView1.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = DataGridView1.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (DataGridView2.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = DataGridView2.RowCount - 1;
                colsTotal = DataGridView2.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = DataGridView2.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = DataGridView2.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (DataGridView3.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = DataGridView3.RowCount - 1;
                colsTotal = DataGridView3.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = DataGridView3.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = DataGridView3.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            DataGridView3.DataSource = null;
            cmbCustomerName.Text = "";
            GroupBox4.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            GroupBox3.Visible = false;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            DateTimePicker1.Text = DateTime.Today.ToString();
            DateTimePicker2.Text = DateTime.Today.ToString();
            DataGridView2.DataSource = null;
            GroupBox10.Visible = false;
        }
        public void FillCombo()
        {

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct CustomerName FROM Invoice_Info,Customer where Invoice_Info.VehicleId=Customer.VehicleID", con);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dTable = ds.Tables[0];
                cmbCustomerName.Items.Clear();
                foreach (DataRow drow in dTable.Rows)
                {
                    cmbCustomerName.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                GroupBox3.Visible = true;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(invoiceNo) as [Invoice No.],RTRIM(InvoiceDate) as [Invoice Date],RTRIM(Invoice_Info.VehicleId) as [Vehicle ID],RTRIM(CustomerName) as [Customer Name],RTRIM(SubTotal) as [SubTotal],RTRIM(DiscountPer) as [Discount %],RTRIM(DiscountAmount) as [Discount Amount],RTRIM(GrandTotal) as [Grand Total],RTRIM(TotalPayment) as [Total Payment],RTRIM(PaymentDue) as [Payment Due],Remarks from Invoice_Info,Customer where Invoice_Info.VehicleId=Customer.VehicleId and InvoiceDate between @d1 and @d2 order by InvoiceDate desc", con);
                cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "InvoiceDate").Value = dtpInvoiceDateFrom.Value.Date;
                cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "InvoiceDate").Value = dtpInvoiceDateTo.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice_Info");
                myDA.Fill(myDataSet, "Customer");
                DataGridView1.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView1.DataSource = myDataSet.Tables["Invoice_Info"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView1.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells["Discount Amount"].Value);
                    Int64 j = Convert.ToInt64(r.Cells["Grand Total"].Value);
                    Int64 k = Convert.ToInt64(r.Cells["Total Payment"].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;

                }
                TextBox1.Text = sum.ToString();
                TextBox2.Text = sum1.ToString();
                TextBox3.Text = sum2.ToString();

                DataGridDesign();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridDesign()
        {
            DataGridView1.BorderStyle = BorderStyle.None;
            DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            DataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            DataGridView1.BackgroundColor = Color.White;
            DataGridView1.EnableHeadersVisualStyles = false;
            DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void DataGridDesign3()
        {
            DataGridView3.BorderStyle = BorderStyle.None;
            DataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            DataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            DataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            DataGridView3.BackgroundColor = Color.White;
            DataGridView3.EnableHeadersVisualStyles = false;
            DataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            DataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void DataGridDesign2()
        {
            DataGridView2.BorderStyle = BorderStyle.None;
            DataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            DataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            DataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            DataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            DataGridView2.BackgroundColor = Color.White;
            DataGridView2.EnableHeadersVisualStyles = false;
            DataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            DataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            DataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GroupBox4.Visible = true;
                cmbCustomerName.Text = cmbCustomerName.Text.Trim();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(invoiceNo) as [Invoice No.],RTRIM(InvoiceDate) as [Invoice Date],RTRIM(Invoice_Info.VehicleId) as [Vehicle ID],RTRIM(CustomerName) as [Customer Name],RTRIM(SubTotal) as [SubTotal],RTRIM(DiscountPer) as [Discount %],RTRIM(DiscountAmount) as [Discount Amount],RTRIM(GrandTotal) as [Grand Total],RTRIM(TotalPayment) as [Total Payment],RTRIM(PaymentDue) as [Payment Due],Remarks from Invoice_Info,Customer where Invoice_Info.VehicleId=Customer.VehicleId and Customername='" + cmbCustomerName.Text.Trim() + "' order by CustomerName,InvoiceDate", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice_Info");
                myDA.Fill(myDataSet, "Customer");
                DataGridView3.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView3.DataSource = myDataSet.Tables["Invoice_Info"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView3.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells["Discount Amount"].Value);
                    Int64 j = Convert.ToInt64(r.Cells["Grand Total"].Value);
                    Int64 k = Convert.ToInt64(r.Cells["Total Payment"].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                TextBox6.Text = sum.ToString();
                TextBox5.Text = sum1.ToString();
                TextBox4.Text = sum2.ToString();
                DataGridDesign3();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                GroupBox10.Visible = true;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(invoiceNo) as [Invoice No.],RTRIM(InvoiceDate) as [Invoice Date],RTRIM(Invoice_Info.VehicleId) as [Vehicle ID],RTRIM(CustomerName) as [Customer Name],RTRIM(SubTotal) as [SubTotal],RTRIM(DiscountPer) as [Discount %],RTRIM(DiscountAmount) as [Discount Amount],RTRIM(GrandTotal) as [Grand Total],RTRIM(TotalPayment) as [Total Payment],RTRIM(PaymentDue) as [Payment Due],Remarks from Invoice_Info,Customer where Invoice_Info.VehicleId=Customer.VehicleId and InvoiceDate between @d1 and @d2 and PaymentDue > 0 order by InvoiceDate desc", con);
                cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "InvoiceDate").Value = DateTimePicker2.Value.Date;
                cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "InvoiceDate").Value = DateTimePicker1.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice_Info");
                myDA.Fill(myDataSet, "Customer");
                DataGridView2.DataSource = myDataSet.Tables["Customer"].DefaultView;
                DataGridView2.DataSource = myDataSet.Tables["Invoice_Info"].DefaultView;
                Int64 sum = 0;
                Int64 sum1 = 0;
                Int64 sum2 = 0;

                foreach (DataGridViewRow r in this.DataGridView2.Rows)
                {
                    Int64 i = Convert.ToInt64(r.Cells["Discount Amount"].Value);
                    Int64 j = Convert.ToInt64(r.Cells["Grand Total"].Value);
                    Int64 k = Convert.ToInt64(r.Cells["Total Payment"].Value);
                    sum = sum + i;
                    sum1 = sum1 + j;
                    sum2 = sum2 + k;
                }
                TextBox12.Text = sum.ToString();
                TextBox11.Text = sum1.ToString();
                TextBox10.Text = sum2.ToString();
                DataGridDesign2();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmSalesRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            if (!isMenuclick)
            {
                frmBilling frm = new frmBilling(isMenuclick);
                frm.label6.Text = label9.Text;
                frm.Show();
            }
        }

        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (!isMenuclick)
                {
                    DataGridViewRow dr = DataGridView1.SelectedRows[0];
                    this.Hide();
                    frmBilling frm = new frmBilling();
                    // or simply use column name instead of index
                    // dr.Cells["id"].Value.ToString();
                    frm.Show();
                    frm.txtInvoiceNo.Text = dr.Cells["Invoice No."].Value.ToString();
                    frm.dtpInvoiceDate.Text = dr.Cells["Invoice Date"].Value.ToString();
                    frm.txtVehicleId.Text = dr.Cells["Vehicle ID"].Value.ToString();
                    frm.txtCustomerName.Text = dr.Cells["Customer Name"].Value.ToString();
                    frm.txtSubTotal.Text = dr.Cells["SubTotal"].Value.ToString();
                    //frm.txtTaxPer.Text = dr.Cells[5].Value.ToString();
                    //frm.txtTaxAmt.Text = dr.Cells[6].Value.ToString();
                    frm.txtDiscountPer.Text = dr.Cells["Discount %"].Value.ToString();
                    frm.txtDiscountAmount.Text = dr.Cells["Discount Amount"].Value.ToString();
                    frm.txtTotal.Text = dr.Cells["Grand Total"].Value.ToString();
                    frm.txtTotalPayment.Text = dr.Cells["Total Payment"].Value.ToString();
                    frm.txtPaymentDue.Text = dr.Cells["Payment Due"].Value.ToString();
                    frm.txtRemarks.Text = dr.Cells["Remarks"].Value.ToString();
                    frm.btnUpdate.Enabled = true;
                    frm.Delete.Enabled = true;
                    frm.btnPrint.Enabled = true;
                    frm.Save.Enabled = false;
                    frm.label6.Text = label9.Text;
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("SELECT CheckpointID,ProductRepaired.CheckpointName,ProductRepaired.Charges,ProductRepaired.Quantity,ProductRepaired.TotalAmount from Invoice_Info,ProductRepaired where Invoice_info.InvoiceNo=ProductRepaired.InvoiceNo and invoice_Info.InvoiceNo='" + dr.Cells[0].Value.ToString() + "'", con);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read() == true)
                    {
                        ListViewItem lst = new ListViewItem();
                        lst.SubItems.Add(rdr[0].ToString().Trim());
                        lst.SubItems.Add(rdr[1].ToString().Trim());
                        lst.SubItems.Add(rdr[2].ToString().Trim());
                        lst.SubItems.Add(rdr[3].ToString().Trim());
                        lst.SubItems.Add(rdr[4].ToString().Trim());
                        frm.ListView1.Items.Add(lst);
                    }
                    //frm.ListView1.Enabled = false;
                    //frm.Button7.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void DataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (!isMenuclick)
                {
                    DataGridViewRow dr = DataGridView3.SelectedRows[0];
                    this.Hide();
                    frmBilling frm = new frmBilling();
                    // or simply use column name instead of index
                    // dr.Cells["id"].Value.ToString();
                    frm.Show();
                    frm.txtInvoiceNo.Text = dr.Cells["Invoice No."].Value.ToString();
                    frm.dtpInvoiceDate.Text = dr.Cells["Invoice Date"].Value.ToString();
                    frm.txtVehicleId.Text = dr.Cells["Vehicle ID"].Value.ToString();
                    frm.txtCustomerName.Text = dr.Cells["Customer Name"].Value.ToString();
                    frm.txtSubTotal.Text = dr.Cells["SubTotal"].Value.ToString();
                    //frm.txtTaxPer.Text = dr.Cells[5].Value.ToString();
                    //frm.txtTaxAmt.Text = dr.Cells[6].Value.ToString();
                    frm.txtDiscountPer.Text = dr.Cells["Discount %"].Value.ToString();
                    frm.txtDiscountAmount.Text = dr.Cells["Discount Amount"].Value.ToString();
                    frm.txtTotal.Text = dr.Cells["Grand Total"].Value.ToString();
                    frm.txtTotalPayment.Text = dr.Cells["Total Payment"].Value.ToString();
                    frm.txtPaymentDue.Text = dr.Cells["Payment Due"].Value.ToString();
                    frm.txtRemarks.Text = dr.Cells["Remarks"].Value.ToString();
                    frm.btnUpdate.Enabled = true;
                    frm.Delete.Enabled = true;
                    frm.btnPrint.Enabled = true;
                    frm.Save.Enabled = false;
                    frm.label6.Text = label9.Text;
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("SELECT CheckpointID,ProductRepaired.CheckpointName,ProductRepaired.Charges,ProductRepaired.Quantity,ProductRepaired.TotalAmount from Invoice_Info,ProductRepaired where Invoice_info.InvoiceNo=ProductRepaired.InvoiceNo and invoice_Info.InvoiceNo='" + dr.Cells[0].Value.ToString() + "'", con);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read() == true)
                    {
                        ListViewItem lst = new ListViewItem();
                        lst.SubItems.Add(rdr[0].ToString().Trim());
                        lst.SubItems.Add(rdr[1].ToString().Trim());
                        lst.SubItems.Add(rdr[2].ToString().Trim());
                        lst.SubItems.Add(rdr[3].ToString().Trim());
                        lst.SubItems.Add(rdr[4].ToString().Trim());
                        frm.ListView1.Items.Add(lst);
                    }
                    //frm.ListView1.Enabled = false;
                    //frm.Button7.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView3.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView3.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void DataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (DataGridView2.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                DataGridView2.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));

        }

        private void DataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (!isMenuclick)
                {
                    DataGridViewRow dr = DataGridView2.SelectedRows[0];
                    this.Hide();
                    frmBilling frm = new frmBilling();
                    // or simply use column name instead of index
                    // dr.Cells["id"].Value.ToString();
                    frm.Show();
                    frm.txtInvoiceNo.Text = dr.Cells["Invoice No."].Value.ToString();
                    frm.dtpInvoiceDate.Text = dr.Cells["Invoice Date"].Value.ToString();
                    frm.txtVehicleId.Text = dr.Cells["Vehicle ID"].Value.ToString();
                    frm.txtCustomerName.Text = dr.Cells["Customer Name"].Value.ToString();
                    frm.txtSubTotal.Text = dr.Cells["SubTotal"].Value.ToString();
                    //frm.txtTaxPer.Text = dr.Cells[5].Value.ToString();
                    //frm.txtTaxAmt.Text = dr.Cells[6].Value.ToString();
                    frm.txtDiscountPer.Text = dr.Cells["Discount %"].Value.ToString();
                    frm.txtDiscountAmount.Text = dr.Cells["Discount Amount"].Value.ToString();
                    frm.txtTotal.Text = dr.Cells["Grand Total"].Value.ToString();
                    frm.txtTotalPayment.Text = dr.Cells["Total Payment"].Value.ToString();
                    frm.txtPaymentDue.Text = dr.Cells["Payment Due"].Value.ToString();
                    frm.txtRemarks.Text = dr.Cells["Remarks"].Value.ToString();
                    frm.btnUpdate.Enabled = true;
                    frm.Delete.Enabled = true;
                    frm.btnPrint.Enabled = true;
                    frm.Save.Enabled = false;
                    frm.label6.Text = label9.Text;
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("SELECT CheckpointID,ProductRepaired.CheckpointName,ProductRepaired.Charges,ProductRepaired.Quantity,ProductRepaired.TotalAmount from Invoice_Info,ProductRepaired where Invoice_info.InvoiceNo=ProductRepaired.InvoiceNo and invoice_Info.InvoiceNo='" + dr.Cells[0].Value.ToString() + "'", con);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read() == true)
                    {
                        ListViewItem lst = new ListViewItem();
                        lst.SubItems.Add(rdr[0].ToString().Trim());
                        lst.SubItems.Add(rdr[1].ToString().Trim());
                        lst.SubItems.Add(rdr[2].ToString().Trim());
                        lst.SubItems.Add(rdr[3].ToString().Trim());
                        lst.SubItems.Add(rdr[4].ToString().Trim());
                        frm.ListView1.Items.Add(lst);
                    }
                    //frm.ListView1.Enabled = false;
                    //frm.Button7.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TabControl1_Click(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            dtpInvoiceDateFrom.Text = DateTime.Today.ToString();
            dtpInvoiceDateTo.Text = DateTime.Today.ToString();
            GroupBox3.Visible = false;
            DataGridView3.DataSource = null;
            cmbCustomerName.Text = "";
            GroupBox4.Visible = false;
            DateTimePicker1.Text = DateTime.Today.ToString();
            DateTimePicker2.Text = DateTime.Today.ToString();
            DataGridView2.DataSource = null;
            GroupBox10.Visible = false;

        }

        private void cmbCustomerName_Format(object sender, ListControlConvertEventArgs e)
        {
            if (object.ReferenceEquals(e.DesiredType, typeof(string)))
            {
                e.Value = e.Value.ToString().Trim();
            }
        }

        private void frmBillingRecords_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }

}
