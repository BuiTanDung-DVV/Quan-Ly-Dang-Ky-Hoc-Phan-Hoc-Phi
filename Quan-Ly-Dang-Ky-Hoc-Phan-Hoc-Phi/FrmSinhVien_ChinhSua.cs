using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmSinhVien_ChinhSua : Form
    {
        private int? _idSinhVien;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        public int CornerRadius { get; set; } = 15;
        KETNOI_CSDL kn = new KETNOI_CSDL();

        // Constructor cho thêm mới
        public FrmSinhVien_ChinhSua()
        {
            InitializeComponent();
            _idSinhVien = null;
            InitializeForm();
            this.label1.Text = "👨‍🎓 THÊM MỚI SINH VIÊN";
            this.txtStudentID.Enabled = false;
        }

        // Constructor cho chỉnh sửa
        public FrmSinhVien_ChinhSua(int idSinhVien)
        {
            InitializeComponent();
            _idSinhVien = idSinhVien;
            InitializeForm();
            this.label1.Text = "✏️ CHỈNH SỬA SINH VIÊN";
            this.txtStudentID.Enabled = false;
            this.txtStudentCode.Enabled = false;
        }

        private void InitializeForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
            
            // Đảm bảo kết nối database
            try
            {
                if (kn.cnn == null || kn.cnn.State != ConnectionState.Open)
                {
                    kn.KetNoi_Dulieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
        }

        private void Bang_KhoaVien()
        {
            try
            {
                DataTable dta = kn.Lay_DulieuBang("SELECT DeptID, Name FROM Departments ORDER BY Name");
                cboDeptID.DataSource = dta;
                cboDeptID.DisplayMember = "Name";
                cboDeptID.ValueMember = "DeptID";
                cboDeptID.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load danh sách khoa/viện: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Load_DuLieuCanSua()
        {
            if (_idSinhVien == null) return;

            try
            {
                if (kn.cnn.State != ConnectionState.Open)
                    kn.KetNoi_Dulieu();

                string sql = "SELECT * FROM Students WHERE StudentID = " + _idSinhVien.Value;
                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read())
                {
                    txtStudentID.Text = doc_dl["StudentID"].ToString();
                    txtStudentCode.Text = doc_dl["StudentCode"].ToString();
                    txtFullName.Text = doc_dl["FullName"].ToString();
                    cboGender.Text = doc_dl["Gender"].ToString();
                    dtpDateOfBirth.Value = doc_dl["DateOfBirth"] == DBNull.Value ? 
                                          DateTime.Now.AddYears(-18) : 
                                          Convert.ToDateTime(doc_dl["DateOfBirth"]);
                    txtEmail.Text = doc_dl["Email"].ToString();
                    txtPhone.Text = doc_dl["Phone"].ToString();
                    txtAddress.Text = doc_dl["Address"].ToString();
                    
                    if (doc_dl["DeptID"] != DBNull.Value)
                        cboDeptID.SelectedValue = doc_dl["DeptID"];
                    
                    numAdmissionYear.Value = doc_dl["AdmissionYear"] == DBNull.Value ? 
                                           DateTime.Now.Year : 
                                           Convert.ToInt32(doc_dl["AdmissionYear"]);
                    cboStatus.Text = doc_dl["Status"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho sinh viên này!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                doc_dl.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải dữ liệu. Lỗi: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy bỏ thay đổi?", 
                                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtStudentCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên!", "Thông báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (cboGender.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGender.Focus();
                return false;
            }

            if (cboDeptID.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khoa/viện!", "Thông báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDeptID.Focus();
                return false;
            }

            if (cboStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái!", "Thông báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboStatus.Focus();
                return false;
            }

            // Validate email format
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, emailPattern))
                {
                    MessageBox.Show("Định dạng email không hợp lệ!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            // Validate phone number
            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                string phonePattern = @"^[0-9]{10,11}$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, phonePattern))
                {
                    MessageBox.Show("Số điện thoại phải có 10-11 chữ số!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (_idSinhVien == null) // Thêm mới
                {
                    // Kiểm tra trùng mã sinh viên
                    if (kn.cnn.State != ConnectionState.Open)
                        kn.KetNoi_Dulieu();

                    string strKtra = "SELECT StudentCode FROM Students WHERE StudentCode=N'" + txtStudentCode.Text + "'";
                    SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                    SqlDataReader doc_dl = cmd.ExecuteReader();

                    if (doc_dl.Read())
                    {
                        MessageBox.Show("Mã sinh viên đã tồn tại, vui lòng nhập mã khác!", "Thông báo", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtStudentCode.Focus();
                        doc_dl.Close();
                        return;
                    }
                    doc_dl.Close();

                    // Thêm sinh viên và lấy ID trong 1 lệnh
                    string sqlWithIdentity = $"INSERT INTO Students (StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status) " +
                                           $"VALUES (N'{txtStudentCode.Text}', N'{txtFullName.Text}', N'{cboGender.Text}', " +
                                           $"'{dtpDateOfBirth.Value:yyyy-MM-dd}', N'{txtEmail.Text}', N'{txtPhone.Text}', " +
                                           $"N'{txtAddress.Text}', {cboDeptID.SelectedValue}, {numAdmissionYear.Value}, N'{cboStatus.Text}'); " +
                                           $"SELECT SCOPE_IDENTITY();";
                    
                    if (kn.cnn.State != ConnectionState.Open)
                        kn.KetNoi_Dulieu();

                    SqlCommand cmdInsert = new SqlCommand(sqlWithIdentity, kn.cnn);
                    object oid = cmdInsert.ExecuteScalar();
                    
                    if (oid != null)
                    {
                        int idSinhVienMoi = Convert.ToInt32(oid);
                        string username = txtStudentCode.Text.Trim();
                        string password = string.IsNullOrWhiteSpace(txtPhone.Text) ? "123456" : txtPhone.Text.Trim();
                        
                        string sqlInsertUser = "INSERT INTO Users (Username, PasswordHash, Role, LinkedStudentID) " +
                                             $"VALUES (N'{username}', N'{password}', N'Sinh viên', {idSinhVienMoi})";
                        
                        SqlCommand cmdUser = new SqlCommand(sqlInsertUser, kn.cnn);
                        cmdUser.ExecuteNonQuery();
                        
                        MessageBox.Show($"Lưu dữ liệu thành công!\n\n" +
                                      $"🎓 Sinh viên: {txtFullName.Text}\n" +
                                      $"👤 Tài khoản: {username}\n" +
                                      $"🔑 Mật khẩu: {password}",
                                      "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else // Cập nhật
                {
                    string sql_update = $"UPDATE Students SET " +
                                      $"FullName=N'{txtFullName.Text}', Gender=N'{cboGender.Text}', " +
                                      $"DateOfBirth='{dtpDateOfBirth.Value:yyyy-MM-dd}', Email=N'{txtEmail.Text}', " +
                                      $"Phone=N'{txtPhone.Text}', Address=N'{txtAddress.Text}', " +
                                      $"DeptID={cboDeptID.SelectedValue}, AdmissionYear={numAdmissionYear.Value}, " +
                                      $"Status=N'{cboStatus.Text}' WHERE StudentID={_idSinhVien.Value}";
                    
                    kn.ThucThiSQL(sql_update);
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (kn.cnn != null && kn.cnn.State == ConnectionState.Open)
                {
                    kn.NgatKetNoi();
                }
            }
        }

        private void FrmSinhVien_ChinhSua_Load(object sender, EventArgs e)
        {
            try
            {
                // Load dữ liệu khoa/viện
                Bang_KhoaVien();
                
                // Setup ComboBox
                cboGender.Items.AddRange(new string[] { "Nam", "Nữ"});
                cboStatus.Items.AddRange(new string[] { "Đang học", "Tốt nghiệp", "Bảo lưu", "Thôi học" });
                
                // Setup NumericUpDown
                numAdmissionYear.Minimum = 2000;
                numAdmissionYear.Maximum = 2099;
                numAdmissionYear.Value = DateTime.Now.Year;

                // Setup DateTimePicker
                dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-15);
                dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-80);
                dtpDateOfBirth.Value = DateTime.Now.AddYears(-18);

                // Thiết lập mặc định cho thêm mới
                if (_idSinhVien == null)
                {
                    cboGender.SelectedIndex = 0; // Nam
                    cboStatus.SelectedIndex = 0; // Đang học
                    txtStudentCode.Focus();
                }
                else
                {
                    // Load dữ liệu cho chỉnh sửa
                    Load_DuLieuCanSua();
                    txtFullName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý phím tắt
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.Control | Keys.S))
            {
                btnLuu_Click(this, EventArgs.Empty);
                return true;
            }
            
            return base.ProcessDialogKey(keyData);
        }
    }
}