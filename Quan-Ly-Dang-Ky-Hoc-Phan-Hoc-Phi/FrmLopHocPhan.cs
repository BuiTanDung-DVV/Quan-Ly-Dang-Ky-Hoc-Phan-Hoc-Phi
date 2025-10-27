using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmLopHocPhan : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();
        string sql;

        public FrmLopHocPhan()
        {
            InitializeComponent();
        }

        private void FrmLopHocPhan_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboboxes();
        }

        private void LoadData()
        {
            sql = "SELECT s.SectionID, s.SectionCode, c.Name, t.Name, l.FullName, " +
                  "s.Schedule, s.Room, s.MaxStudents " +
                  "FROM ClassSections s " +
                  "JOIN Courses c ON s.CourseID = c.CourseID " +
                  "JOIN AcademicTerms t ON s.TermID = t.TermID " +
                  "JOIN Lecturers l ON s.LecturerID = l.LecturerID";

            DataTable dt = kn.Lay_DulieuBang(sql);
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

        private void LoadComboboxes()
        {
            // Đổ dữ liệu vào combobox khóa học
            cboMonHoc.DataSource = kn.Lay_DulieuBang("SELECT CourseID, Name FROM Courses");
            cboMonHoc.DisplayMember = "Name";
            cboMonHoc.ValueMember = "CourseID";

            // Đổ dữ liệu vào combobox học kỳ
            cboHocKy.DataSource = kn.Lay_DulieuBang("SELECT TermID, Name FROM AcademicTerms");
            cboHocKy.DisplayMember = "Name";
            cboHocKy.ValueMember = "TermID";

            // Đổ dữ liệu vào combobox giảng viên
            cboGiangVien.DataSource = kn.Lay_DulieuBang("SELECT LecturerID, FullName FROM Lecturers");
            cboGiangVien.DisplayMember = "FullName";
            cboGiangVien.ValueMember = "LecturerID";
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaLop.Clear();
            txtPhong.Clear();
            txtLichHoc.Clear();
            txtSoLuong.Clear();
            cboMonHoc.SelectedIndex = -1;
            cboHocKy.SelectedIndex = -1;
            cboGiangVien.SelectedIndex = -1;
            txtMaLop.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng nhập mã lớp học phần!");
                return;
            }

            sql = "INSERT INTO ClassSections (SectionCode, CourseID, TermID, LecturerID, Schedule, Room, MaxStudents) " +
                  "VALUES (N'" + txtMaLop.Text + "', " +
                  cboMonHoc.SelectedValue + ", " +
                  cboHocKy.SelectedValue + ", " +
                  cboGiangVien.SelectedValue + ", " +
                  "N'" + txtLichHoc.Text + "', N'" + txtPhong.Text + "', " + txtSoLuong.Text + ")";

            kn.ThucThiSQL(sql);
            MessageBox.Show("Đã thêm lớp học phần mới!");
            LoadData();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SectionID"].Value);
            sql = "UPDATE ClassSections SET " +
                  "SectionCode = N'" + txtMaLop.Text + "', " +
                  "CourseID = " + cboMonHoc.SelectedValue + ", " +
                  "TermID = " + cboHocKy.SelectedValue + ", " +
                  "LecturerID = " + cboGiangVien.SelectedValue + ", " +
                  "Schedule = N'" + txtLichHoc.Text + "', " +
                  "Room = N'" + txtPhong.Text + "', " +
                  "MaxStudents = " + txtSoLuong.Text + " " +
                  "WHERE SectionID = " + id;

            kn.ThucThiSQL(sql);
            MessageBox.Show("Cập nhật thông tin lớp học phần thành công!");
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SectionID"].Value);
            if (MessageBox.Show("Bạn có chắc muốn xóa lớp học phần này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sql = "DELETE FROM ClassSections WHERE SectionID = " + id;
                kn.ThucThiSQL(sql);
                MessageBox.Show("Đã xóa!");
                LoadData();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            txtMaLop.Text = dataGridView1.CurrentRow.Cells["SectionCode"].Value.ToString();
            cboMonHoc.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            cboHocKy.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            cboGiangVien.Text = dataGridView1.CurrentRow.Cells["FullName"].Value.ToString();
            txtLichHoc.Text = dataGridView1.CurrentRow.Cells["Schedule"].Value.ToString();
            txtPhong.Text = dataGridView1.CurrentRow.Cells["Room"].Value.ToString();
            txtSoLuong.Text = dataGridView1.CurrentRow.Cells["MaxStudents"].Value.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
