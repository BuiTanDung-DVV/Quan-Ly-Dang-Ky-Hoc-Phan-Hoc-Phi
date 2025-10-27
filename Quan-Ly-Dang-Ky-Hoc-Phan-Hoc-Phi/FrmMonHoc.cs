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
    public partial class FrmMonHoc : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();
        string sql;
        public FrmMonHoc()
        {
            InitializeComponent();
        }
        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            Load_KhoaVien();
            Load_MonHoc();
        }

        private void Load_KhoaVien()
        {
            cboKhoaVien.DataSource = kn.Lay_DulieuBang("SELECT * FROM Departments");
            cboKhoaVien.DisplayMember = "Name";
            cboKhoaVien.ValueMember = "DeptID";
        }

        private void Load_MonHoc()
        {
            sql = "SELECT c.CourseID, c.Code, c.Name, c.Credits, c.TuitionPerCredit, d.Name AS DeptName " +
                  "FROM Courses c JOIN Departments d ON c.DeptID = d.DeptID";
            dataGridView1.DataSource = kn.Lay_DulieuBang(sql);
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
            txtMaMon.Clear();
            txtTenLop.Clear();
            txtSoTin.Clear();
            txtHocPhi.Clear();
            cboKhoaVien.SelectedIndex = -1;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            sql = $"INSERT INTO Courses (Code, Name, Credits, TuitionPerCredit, DeptID) " +
                  $"VALUES ('{txtMaMon.Text}', N'{txtTenLop.Text}', {txtSoTin.Text}, {txtHocPhi.Text}, {cboKhoaVien.SelectedValue})";
            kn.ThucThiSQL(sql);
            Load_MonHoc();
            MessageBox.Show("Đã thêm môn học thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CourseID"].Value);
            sql = $"UPDATE Courses SET Code='{txtMaMon.Text}', Name=N'{txtTenLop.Text}', Credits={txtSoTin.Text}, " +
                  $"TuitionPerCredit={txtHocPhi.Text}, DeptID={cboKhoaVien.SelectedValue} WHERE CourseID={id}";
            kn.ThucThiSQL(sql);
            Load_MonHoc();
            MessageBox.Show("Cập nhật thành công!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CourseID"].Value);
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sql = $"DELETE FROM Courses WHERE CourseID={id}";
                kn.ThucThiSQL(sql);
                Load_MonHoc();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaMon.Text = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                txtTenLop.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                txtSoTin.Text = dataGridView1.Rows[e.RowIndex].Cells["Credits"].Value.ToString();
                txtHocPhi.Text = dataGridView1.Rows[e.RowIndex].Cells["TuitionPerCredit"].Value.ToString();
                cboKhoaVien.Text = dataGridView1.Rows[e.RowIndex].Cells["DeptName"].Value.ToString();
            }
        }

        private void lblTenMon_Click(object sender, EventArgs e)
        {

        }

        private void txtTenLop_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
