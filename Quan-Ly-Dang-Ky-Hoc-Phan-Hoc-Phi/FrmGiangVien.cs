using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmGiangVien : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();
        string sql;

        public FrmGiangVien()
        {
            InitializeComponent();
        }

        private void FrmGiangVien_Load(object sender, EventArgs e)
        {
            Load_KhoaVien();
            Load_GiangVien();
        }

        // 🔹 Nạp danh sách khoa vào ComboBox
        private void Load_KhoaVien()
        {
            sql = "SELECT * FROM Departments";
            DataTable dta = kn.Lay_DulieuBang(sql);
            cboKhoaVien.DataSource = dta;
            cboKhoaVien.DisplayMember = "Name";   // hiển thị tên
            cboKhoaVien.ValueMember = "DeptID";   // lấy giá trị DeptID
        }

        // 🔹 Nạp dữ liệu giảng viên ra DataGridView
        private void Load_GiangVien()
        {
            sql = @"SELECT L.LecturerCode AS [Mã GV], 
                           L.FullName AS [Tên GV],
                           L.Email AS [Email],
                           D.Name AS [Khoa]
                    FROM Lecturers L
                    JOIN Departments D ON L.DeptID = D.DeptID";
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

        // 🔹 Nút TẠO MỚI
        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaGV.Clear();
            txtGV.Clear();
            txtEmail.Clear();
            txtSDT.Clear();  // (nếu bạn có thêm cột SDT trong DB thì mới dùng)
            cboKhoaVien.SelectedIndex = -1;
            txtMaGV.Focus();
        }

        // 🔹 Nút LƯU (Thêm mới)
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaGV.Text == "" || txtGV.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            sql = $"INSERT INTO Lecturers (LecturerCode, FullName, Email, DeptID) " +
                  $"VALUES ('{txtMaGV.Text}', N'{txtGV.Text}', '{txtEmail.Text}', {cboKhoaVien.SelectedValue})";
            try
            {
                kn.ThucThiSQL(sql);
                MessageBox.Show("Thêm giảng viên thành công!");
                Load_GiangVien();
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
                txtMaGV.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txtGV.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                cboKhoaVien.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            }
        }

        // 🔹 Nút SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            sql = $"UPDATE Lecturers SET FullName = N'{txtGV.Text}', " +
                  $"Email = '{txtEmail.Text}', DeptID = {cboKhoaVien.SelectedValue} " +
                  $"WHERE LecturerCode = '{txtMaGV.Text}'";
            try
            {
                kn.ThucThiSQL(sql);
                MessageBox.Show("Cập nhật thành công!");
                Load_GiangVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        // 🔹 Nút XÓA
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa giảng viên này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sql = $"DELETE FROM Lecturers WHERE LecturerCode = '{txtMaGV.Text}'";
                try
                {
                    kn.ThucThiSQL(sql);
                    MessageBox.Show("Đã xóa!");
                    Load_GiangVien();
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
