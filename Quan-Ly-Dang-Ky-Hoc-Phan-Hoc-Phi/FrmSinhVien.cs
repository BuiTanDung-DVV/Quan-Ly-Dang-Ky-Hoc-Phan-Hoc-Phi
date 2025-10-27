using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmSinhVien : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();
        private bool isEditing = false;
        private int currentStudentID = 0;

        public FrmSinhVien()
        {
            InitializeComponent();
        }

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            LoadStudents();
            SetupDataGridView();
            SetButtonState(false);
            ClearForm();
            RenameControls();
        }
        private void RenameControls()
        {
            textBox1.Name = "txtEmail";
            textBox2.Name = "txtPhone";
            
            lblMaLop.Text = "Năm nhập học:";
            lblMaHe.Text = "Trạng thái:";
            lblNoiSinh.Text = "Địa chỉ:";
            
            cboMaLop.Name = "cboAdmissionYear";
            cboMaHe.Name = "cboStatus";
        }

        private void LoadDepartments()
        {
            try
            {
                string sql = "SELECT DeptID, Name FROM Departments ORDER BY Name";
                DataTable dt = kn.Lay_DulieuBang(sql);
                
                cboMaKhoa.DisplayMember = "Name";
                cboMaKhoa.ValueMember = "DeptID";
                cboMaKhoa.DataSource = dt;
                cboMaKhoa.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load khoa: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAdmissionYears()
        {
            cboMaLop.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 10; i <= currentYear + 2; i++)
            {
                cboMaLop.Items.Add(i);
            }
        }

        private void LoadStatus()
        {
            cboMaHe.Items.Clear();
            cboMaHe.Items.Add("Đang học");
            cboMaHe.Items.Add("Nghỉ học");
            cboMaHe.Items.Add("Thôi học");
            cboMaHe.Items.Add("Tốt nghiệp");
            cboMaHe.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
           
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            
            LoadAdmissionYears();
            LoadStatus();
        }

        private void LoadStudents()
        {
            try
            {
                string sql = @"
                    SELECT 
                        s.StudentID,
                        s.StudentCode as [Mã SV],
                        s.FullName as [Họ tên],
                        s.Gender as [Giới tính],
                        s.DateOfBirth as [Ngày sinh],
                        s.Email,
                        s.Phone as [Điện thoại],
                        s.Address as [Địa chỉ],
                        d.Name as [Khoa],
                        s.AdmissionYear as [Năm nhập học],
                        s.Status as [Trạng thái],
                        s.DeptID
                    FROM Students s
                    LEFT JOIN Departments d ON s.DeptID = d.DeptID
                    ORDER BY s.StudentCode";

                DataTable dt = kn.Lay_DulieuBang(sql);
                dataGridView1.DataSource = dt;
                
                if (dataGridView1.Columns["StudentID"] != null)
                    dataGridView1.Columns["StudentID"].Visible = false;
                if (dataGridView1.Columns["DeptID"] != null)
                    dataGridView1.Columns["DeptID"].Visible = false;
                if (dataGridView1.Columns["Email"] != null)
                    dataGridView1.Columns["Email"].Visible = false;

                if (dataGridView1.Columns["Ngày sinh"] != null)
                    dataGridView1.Columns["Ngày sinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetButtonState(bool editing)
        {
            isEditing = editing;
            
            btnTaoMoi.Enabled = !editing;
            btnLuu.Enabled = editing;
            btnSua.Enabled = !editing && dataGridView1.CurrentRow != null;
            btnXoa.Enabled = !editing && dataGridView1.CurrentRow != null;
            
            txtMaSV.Enabled = editing;
            txtName.Enabled = editing;
            optNam.Enabled = editing;
            optNu.Enabled = editing;
            txtDate.Enabled = editing;
            textBox1.Enabled = editing; // Email
            textBox2.Enabled = editing; // Phone
            txtNoiSinh.Enabled = editing; // Address
            cboMaKhoa.Enabled = editing;
            cboMaLop.Enabled = editing; // AdmissionYear
            cboMaHe.Enabled = editing; // Status
        }

        private void ClearForm()
        {
            txtMaSV.Text = "";
            txtName.Text = "";
            optNam.Checked = true;
            txtDate.Value = DateTime.Now.AddYears(-20);
            textBox1.Text = ""; // Email
            textBox2.Text = ""; // Phone
            txtNoiSinh.Text = ""; // Address
            cboMaKhoa.SelectedIndex = -1;
            cboMaLop.SelectedIndex = -1;
            cboMaHe.SelectedIndex = 0;
            currentStudentID = 0;
        }

        private void LoadDataToForm()
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                
                currentStudentID = Convert.ToInt32(row.Cells["StudentID"].Value);
                txtMaSV.Text = row.Cells["Mã SV"].Value?.ToString() ?? "";
                txtName.Text = row.Cells["Họ tên"].Value?.ToString() ?? "";
                
                // Gender
                string gender = row.Cells["Giới tính"].Value?.ToString();
                optNam.Checked = gender == "Nam";
                optNu.Checked = gender == "Nữ";
                
                // Date of Birth
                if (row.Cells["Ngày sinh"].Value != DBNull.Value)
                    txtDate.Value = Convert.ToDateTime(row.Cells["Ngày sinh"].Value);
                
                textBox1.Text = row.Cells["Email"].Value?.ToString() ?? ""; // Email
                textBox2.Text = row.Cells["Điện thoại"].Value?.ToString() ?? ""; // Phone
                txtNoiSinh.Text = row.Cells["Địa chỉ"].Value?.ToString() ?? ""; // Address
                
                // Department
                if (row.Cells["DeptID"].Value != DBNull.Value)
                    cboMaKhoa.SelectedValue = Convert.ToInt32(row.Cells["DeptID"].Value);
                
                // Admission Year
                if (row.Cells["Năm nhập học"].Value != DBNull.Value)
                {
                    int year = Convert.ToInt32(row.Cells["Năm nhập học"].Value);
                    cboMaLop.Text = year.ToString();
                }
                
                // Status
                cboMaHe.Text = row.Cells["Trạng thái"].Value?.ToString() ?? "Đang học";
            }
        }


        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sinh viên!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !IsValidEmail(textBox1.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }

            if (cboMaKhoa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khoa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaKhoa.Focus();
                return false;
            }

            if (cboMaLop.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn năm nhập học!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaLop.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsCodeExists(string code, int excludeId = 0)
        {
            try
            {
                string sql = $"SELECT COUNT(*) FROM Students WHERE StudentCode = '{code}'";
                if (excludeId > 0)
                    sql += $" AND StudentID != {excludeId}";

                DataTable dt = kn.Lay_DulieuBang(sql);
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
            catch
            {
                return false;
            }
        }



        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                LoadDataToForm();
                SetButtonState(false);
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !isEditing)
            {
                btnSua_Click(sender, e);
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetButtonState(true);
            txtMaSV.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            if (IsCodeExists(txtMaSV.Text.Trim(), currentStudentID))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSV.Focus();
                return;
            }

            try
            {
                kn.KetNoi_Dulieu();

                if (currentStudentID == 0) // Thêm mới - sử dụng stored procedure
                {
                    SqlCommand cmd = new SqlCommand("ThemSinhVien", kn.cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@StudentCode", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@FullName", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", optNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@DateOfBirth", txtDate.Value);
                    cmd.Parameters.AddWithValue("@Email", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phone", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtNoiSinh.Text.Trim());
                    cmd.Parameters.AddWithValue("@DeptID", cboMaKhoa.SelectedValue);
                    cmd.Parameters.AddWithValue("@AdmissionYear", Convert.ToInt32(cboMaLop.Text));
                    cmd.Parameters.AddWithValue("@Status", cboMaHe.Text);

                    cmd.ExecuteNonQuery();
                }
                else // Cập nhật - sử dụng stored procedure
                {
                    SqlCommand cmd = new SqlCommand("SuaSinhVien", kn.cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@StudentID", currentStudentID);
                    cmd.Parameters.AddWithValue("@StudentCode", txtMaSV.Text.Trim());
                    cmd.Parameters.AddWithValue("@FullName", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gender", optNam.Checked ? "Nam" : "Nữ");
                    cmd.Parameters.AddWithValue("@DateOfBirth", txtDate.Value);
                    cmd.Parameters.AddWithValue("@Email", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phone", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtNoiSinh.Text.Trim());
                    cmd.Parameters.AddWithValue("@DeptID", cboMaKhoa.SelectedValue);
                    cmd.Parameters.AddWithValue("@AdmissionYear", Convert.ToInt32(cboMaLop.Text));
                    cmd.Parameters.AddWithValue("@Status", cboMaHe.Text);

                    cmd.ExecuteNonQuery();
                }

                kn.NgatKetNoi();

                string message = currentStudentID == 0 ? "Thêm mới thành công!" : "Cập nhật thành công!";
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadStudents();
                SetButtonState(false);
                ClearForm();
            }
            catch (Exception ex)
            {
                kn.NgatKetNoi();
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadDataToForm();
            SetButtonState(true);
            txtName.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenSV = dataGridView1.CurrentRow.Cells["Họ tên"].Value?.ToString();
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sinh viên '{tenSV}'?\n\nLưu ý: Việc xóa có thể ảnh hưởng đến dữ liệu liên quan!", 
                "Xác nhận xóa", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int studentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StudentID"].Value);
                    
                    // Kiểm tra ràng buộc dữ liệu
                    if (HasRelatedData(studentId))
                    {
                        MessageBox.Show("Không thể xóa sinh viên này vì đã có dữ liệu liên quan (đăng ký học, hóa đơn, user...)!", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Sử dụng stored procedure để xóa
                    kn.KetNoi_Dulieu();
                    SqlCommand cmd = new SqlCommand("XoaSinhVien", kn.cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.ExecuteNonQuery();
                    kn.NgatKetNoi();

                    MessageBox.Show("Xóa thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadStudents();
                    ClearForm();
                    SetButtonState(false);
                }
                catch (Exception ex)
                {
                    kn.NgatKetNoi();
                    MessageBox.Show("Lỗi xóa dữ liệu: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool HasRelatedData(int studentId)
        {
            try
            {
                // Kiểm tra trong bảng Enrollments
                string sql1 = $"SELECT COUNT(*) FROM Enrollments WHERE StudentID = {studentId}";
                DataTable dt1 = kn.Lay_DulieuBang(sql1);
                if (Convert.ToInt32(dt1.Rows[0][0]) > 0)
                    return true;

                // Kiểm tra trong bảng Invoices
                string sql2 = $"SELECT COUNT(*) FROM Invoices WHERE StudentID = {studentId}";
                DataTable dt2 = kn.Lay_DulieuBang(sql2);
                if (Convert.ToInt32(dt2.Rows[0][0]) > 0)
                    return true;

                // Kiểm tra trong bảng Users
                string sql3 = $"SELECT COUNT(*) FROM Users WHERE LinkedStudentID = {studentId}";
                DataTable dt3 = kn.Lay_DulieuBang(sql3);
                if (Convert.ToInt32(dt3.Rows[0][0]) > 0)
                    return true;

                return false;
            }
            catch
            {
                return true; // Nếu có lỗi, coi như có dữ liệu liên quan để an toàn
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                DialogResult result = MessageBox.Show(
                    "Bạn đang trong quá trình chỉnh sửa. Bạn có muốn hủy bỏ các thay đổi?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SetButtonState(false);
                    LoadDataToForm();
                }
            }
            else
            {
                Application.Exit();
            } 
        }
      

        public void SearchByStudentCode(string studentCode)
        {
            try
            {
                kn.KetNoi_Dulieu();
                SqlCommand cmd = new SqlCommand("TKTTSinhVien", kn.cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentCode", studentCode);
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                dataGridView1.DataSource = dt;
                kn.NgatKetNoi();
            }
            catch (Exception ex)
            {
                kn.NgatKetNoi();
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SearchByFullName(string fullName)
        {
            try
            {
                kn.KetNoi_Dulieu();
                SqlCommand cmd = new SqlCommand("TKTTSinhVien1", kn.cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FullName", fullName);
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                dataGridView1.DataSource = dt;
                kn.NgatKetNoi();
            }
            catch (Exception ex)
            {
                kn.NgatKetNoi();
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SearchByDepartment(int deptID)
        {
            try
            {
                kn.KetNoi_Dulieu();
                SqlCommand cmd = new SqlCommand("TKTTSinhVien2", kn.cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DeptID", deptID);
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                dataGridView1.DataSource = dt;
                kn.NgatKetNoi();
            }
            catch (Exception ex)
            {
                kn.NgatKetNoi();
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SearchByAdmissionYear(int admissionYear)
        {
            try
            {
                kn.KetNoi_Dulieu();
                SqlCommand cmd = new SqlCommand("TKTTSinhVien3", kn.cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdmissionYear", admissionYear);
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                
                dataGridView1.DataSource = dt;
                kn.NgatKetNoi();
            }
            catch (Exception ex)
            {
                kn.NgatKetNoi();
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FrmSinhVien_Load(this, e);
        }
    }
}
