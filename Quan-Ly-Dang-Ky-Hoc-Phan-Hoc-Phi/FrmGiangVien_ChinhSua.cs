using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmGiangVien_ChinhSua : Form
    {
        private int? _idGiangVien;

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

        // Constructor thêm mới
        public FrmGiangVien_ChinhSua()
        {
            InitializeComponent();
            _idGiangVien = null;
            InitializeForm();
            this.label1.Text = "👨‍🏫 THÊM MỚI GIẢNG VIÊN";
            this.txtLecturerID.Enabled = false;
        }

        // Constructor chỉnh sửa
        public FrmGiangVien_ChinhSua(int idGiangVien)
        {
            InitializeComponent();
            _idGiangVien = idGiangVien;
            InitializeForm();
            this.label1.Text = "✏️ CHỈNH SỬA GIẢNG VIÊN";
            this.txtLecturerID.Enabled = false;
            this.txtLecturerCode.Enabled = false;
        }

        private void InitializeForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
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
            if (_idGiangVien == null) return;

            try
            {
                if (kn.cnn.State != ConnectionState.Open)
                    kn.KetNoi_Dulieu();

                string sql = "SELECT * FROM Lecturers WHERE LecturerID = " + _idGiangVien.Value;
                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read())
                {
                    txtLecturerID.Text = doc_dl["LecturerID"].ToString();
                    txtLecturerCode.Text = doc_dl["LecturerCode"].ToString();
                    txtFullName.Text = doc_dl["FullName"].ToString();
                    txtEmail.Text = doc_dl["Email"].ToString();

                    if (doc_dl["DeptID"] != DBNull.Value)
                        cboDeptID.SelectedValue = doc_dl["DeptID"];
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho giảng viên này!", "Thông báo",
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
            if (string.IsNullOrWhiteSpace(txtLecturerCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã giảng viên!", "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLecturerCode.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (cboDeptID.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khoa/viện!", "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDeptID.Focus();
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

            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (_idGiangVien == null) // Thêm mới
                {
                    // Kiểm tra trùng mã giảng viên
                    if (kn.cnn.State != ConnectionState.Open)
                        kn.KetNoi_Dulieu();

                    string strKtra = "SELECT LecturerCode FROM Lecturers WHERE LecturerCode=N'" + txtLecturerCode.Text + "'";
                    SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                    SqlDataReader doc_dl = cmd.ExecuteReader();

                    if (doc_dl.Read())
                    {
                        MessageBox.Show("Mã giảng viên đã tồn tại, vui lòng nhập mã khác!", "Thông báo",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLecturerCode.Focus();
                        doc_dl.Close();
                        return;
                    }
                    doc_dl.Close();

                    // Thêm giảng viên và lấy ID
                    string sqlWithIdentity = $"INSERT INTO Lecturers (LecturerCode, FullName, Email, DeptID) " +
                                           $"VALUES (N'{txtLecturerCode.Text}', N'{txtFullName.Text}', " +
                                           $"N'{txtEmail.Text}', {cboDeptID.SelectedValue}); " +
                                           $"SELECT SCOPE_IDENTITY();";

                    if (kn.cnn.State != ConnectionState.Open)
                        kn.KetNoi_Dulieu();

                    SqlCommand cmdInsert = new SqlCommand(sqlWithIdentity, kn.cnn);
                    object oid = cmdInsert.ExecuteScalar();

                    if (oid != null)
                    {
                        int idGiangVienMoi = Convert.ToInt32(oid);
                        string username = txtLecturerCode.Text.Trim();
                        string password = "123456"; // Hoặc có thể để rỗng hoặc tạo logic khác

                        string sqlInsertUser = "INSERT INTO Users (Username, PasswordHash, Role, LinkedLecturerID) " +
                                             $"VALUES (N'{username}', N'{password}', N'Giảng viên', {idGiangVienMoi})";
                        SqlCommand cmdUser = new SqlCommand(sqlInsertUser, kn.cnn);
                        cmdUser.ExecuteNonQuery();

                        MessageBox.Show($"Lưu dữ liệu thành công!\n\n" +
                                      $"👨‍🏫 Giảng viên: {txtFullName.Text}\n" +
                                      $"👤 Tài khoản: {username}\n" +
                                      $"🔑 Mật khẩu: {password}",
                                      "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else // Cập nhật
                {
                    string sql_update = $"UPDATE Lecturers SET " +
                                      $"FullName=N'{txtFullName.Text}', Email=N'{txtEmail.Text}', " +
                                      $"DeptID={cboDeptID.SelectedValue} WHERE LecturerID={_idGiangVien.Value}";
                    kn.ThucThiSQL(sql_update);
                    MessageBox.Show("Cập nhật thông tin giảng viên thành công!", "Thông báo",
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

        private void FrmGiangVien_ChinhSua_Load(object sender, EventArgs e)
        {
            try
            {
                Bang_KhoaVien();

                if (_idGiangVien == null)
                {
                    txtLecturerCode.Focus();
                }
                else
                {
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