using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmKhoaVien : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();
        string sql;

        public FrmKhoaVien()
        {
            InitializeComponent();
            Load_KhoaVien();
        }

        // 🔹 Nạp dữ liệu Khoa/Viện
        private void Load_KhoaVien()
        {
            sql = "SELECT Code AS [Mã Khoa], Name AS [Tên Khoa], Office AS [Địa Chỉ] FROM Departments";
            dataGridView1.DataSource = kn.Lay_DulieuBang(sql);

            // 🔹 Làm đẹp bảng
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 60, 120);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 240, 250);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // 🔹 Nút TẠO MỚI
        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaKhoa.Clear();
            txtTenKhoa.Clear();
            txtCoVan.Clear(); // txtCoVan đang hiển thị Office
            txtMaKhoa.Focus();
        }

        // 🔹 Nút LƯU (Thêm mới)
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaKhoa.Text == "" || txtTenKhoa.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            sql = $"INSERT INTO Departments (Code, Name, Office) VALUES ('{txtMaKhoa.Text}', N'{txtTenKhoa.Text}', N'{txtCoVan.Text}')";
            try
            {
                kn.ThucThiSQL(sql);
                MessageBox.Show("Thêm khoa/viện thành công!");
                Load_KhoaVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        // 🔹 Khi click vào dòng trong DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMaKhoa.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txtTenKhoa.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txtCoVan.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            }
        }

        // 🔹 Nút SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            sql = $"UPDATE Departments SET Name = N'{txtTenKhoa.Text}', Office = N'{txtCoVan.Text}' WHERE Code = '{txtMaKhoa.Text}'";
            try
            {
                kn.ThucThiSQL(sql);
                MessageBox.Show("Cập nhật thành công!");
                Load_KhoaVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        // 🔹 Nút XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa khoa/viện này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sql = $"DELETE FROM Departments WHERE Code = '{txtMaKhoa.Text}'";
                try
                {
                    kn.ThucThiSQL(sql);
                    MessageBox.Show("Đã xóa!");
                    Load_KhoaVien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        // 🔹 Nút THOÁT
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
