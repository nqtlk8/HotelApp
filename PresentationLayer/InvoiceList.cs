using BusinessLogicLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class InvoiceList: Form
    {
        public InvoiceList()
        {
            InitializeComponent();
        }

        private void InvoiceList_Load(object sender, EventArgs e)
        {
            LoadInvoicesToGrid();
        }
        private async void LoadInvoicesToGrid()
        {
            try
            {
                var invoices = await InvoiceBLL.GetAllInvoicesAsync();
                dgvInvoices.DataSource = invoices;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tải danh sách hóa đơn: " + ex.Message);
            }
        }

        private void dgvInvoices_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnReadInvoice_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn.");
                return;
            }

            var selectedRow = dgvInvoices.SelectedRows[0];
            var cellValue = selectedRow.Cells["InvoiceID"].Value;

            if (cellValue == null || cellValue == DBNull.Value)
            {
                MessageBox.Show("Không thể xác định InvoiceID.");
                return;
            }

            if (!int.TryParse(cellValue.ToString(), out int invoiceId))
            {
                MessageBox.Show("InvoiceID không hợp lệ.");
                return;
            }

            // Mở form hiển thị chi tiết hóa đơn
            InvoiceForm invoiceForm = new InvoiceForm(invoiceId);
            invoiceForm.Show();
        }
        }
}
