using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmNganhHoc : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();
        string sql;
        DataTable dt;
        public FrmNganhHoc()
        {
            InitializeComponent();
        }

        private void FrmNganh_Load(object sender, EventArgs e)
        {
            Load_KhoaVien();
            Load_Nganh();
        }

        private void Load_KhoaVien()
        {
            sql = "SELECT DeptID, Name FROM Departments";
            cboKhoaVien.DataSource = kn.Lay_DulieuBang(sql);
            cboKhoaVien.DisplayMember = "Name";
            cboKhoaVien.ValueMember = "DeptID";
        }

        private void Load_Nganh()
        {
            sql = "SELECT m.MajorID, m.Code, m.Name, d.Name AS DeptName " +
                  "FROM Majors m INNER JOIN Departments d ON m.DeptID = d.DeptID";
            dt = kn.Lay_DulieuBang(sql);
            dataGridView1.DataSource = dt;
            // 🔹 Làm đẹp DataGridView
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 60, 120);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 240, 250);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaNganh.Clear();
            txtTenNganh.Clear();
            cboKhoaVien.SelectedIndex = -1;
            txtMaNganh.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNganh.Text == "" || txtTenNganh.Text == "" || cboKhoaVien.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            sql = $"INSERT INTO Majors (Code, Name, DeptID) " +
                  $"VALUES ('{txtMaNganh.Text}', N'{txtTenNganh.Text}', {cboKhoaVien.SelectedValue})";

            kn.ThucThiSQL(sql);
            Load_Nganh();
            MessageBox.Show("Đã thêm mới ngành!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["MajorID"].Value);

            sql = $"UPDATE Majors SET Code='{txtMaNganh.Text}', Name=N'{txtTenNganh.Text}', " +
                  $"DeptID={cboKhoaVien.SelectedValue} WHERE MajorID={id}";
            kn.ThucThiSQL(sql);
            Load_Nganh();
            MessageBox.Show("Đã cập nhật thông tin ngành!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["MajorID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa ngành này?", "Xác nhận",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                sql = $"DELETE FROM Majors WHERE MajorID={id}";
                kn.ThucThiSQL(sql);
                Load_Nganh();
                MessageBox.Show("Đã xóa ngành!");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaNganh.Text = dataGridView1.CurrentRow.Cells["Code"].Value.ToString();
                txtTenNganh.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
                cboKhoaVien.Text = dataGridView1.CurrentRow.Cells["DeptName"].Value.ToString();
            }
        }
    }
}
