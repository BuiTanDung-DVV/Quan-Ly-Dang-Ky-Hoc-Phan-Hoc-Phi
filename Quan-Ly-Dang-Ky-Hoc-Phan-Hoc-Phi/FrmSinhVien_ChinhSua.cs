using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

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

        public int CornerRadius { get; set; } = 30;
        KETNOI_CSDL kn = new KETNOI_CSDL();

        public FrmSinhVien_ChinhSua()
        {
            InitializeComponent();
            _idSinhVien = null;

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            this.label1.Text = "Thêm Mới Sinh Viên";
            this.txtStudentID.Enabled = false;
        }

        public FrmSinhVien_ChinhSua(int idSinhVien)
        {
            InitializeComponent();
            _idSinhVien = idSinhVien;

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            this.label1.Text = "Chỉnh Sửa Sinh Viên";
            this.txtStudentID.Enabled = false;
            this.txtStudentCode.Enabled = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
        }

        public void Bang_KhoaVien()
        {
            DataTable dta = kn.Lay_DulieuBang("SELECT * FROM Departments");
            cboDeptID.DataSource = dta;
            cboDeptID.DisplayMember = "Name";
            cboDeptID.ValueMember = "DeptID";
        }

        private void Load_DuLieuCanSua()
        {
            if (_idSinhVien == null) return;

            try
            {
                string sql = "SELECT * FROM Students WHERE StudentID = " + _idSinhVien.Value;
                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read())
                {
                    txtStudentID.Text = doc_dl["StudentID"].ToString();
                    txtStudentCode.Text = doc_dl["StudentCode"].ToString();
                    txtFullName.Text = doc_dl["FullName"].ToString();
                    cboGender.Text = doc_dl["Gender"].ToString();
                    dtpDateOfBirth.Value = doc_dl["DateOfBirth"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(doc_dl["DateOfBirth"]);
                    txtEmail.Text = doc_dl["Email"].ToString();
                    txtPhone.Text = doc_dl["Phone"].ToString();
                    txtAddress.Text = doc_dl["Address"].ToString();
                    cboDeptID.SelectedValue = doc_dl["DeptID"];
                    numAdmissionYear.Value = doc_dl["AdmissionYear"] == DBNull.Value ? DateTime.Now.Year : Convert.ToInt32(doc_dl["AdmissionYear"]);
                    cboStatus.Text = doc_dl["Status"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho sinh viên này!");
                    this.Close();
                }
                doc_dl.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu. Lỗi: " + ex.Message);
                this.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentCode.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Hãy nhập đầy đủ mã sinh viên và họ tên!");
                return;
            }

            if (_idSinhVien == null)
            {
                // Kiểm tra trùng mã sinh viên
                string strKtra = "Select StudentCode from Students where StudentCode=N'" + txtStudentCode.Text + "'";
                SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read())
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStudentCode.Focus();
                    doc_dl.Close();
                }
                else
                {
                    doc_dl.Close();
                    string sql = $"INSERT INTO Students (StudentCode, FullName, Gender, DateOfBirth, Email, Phone, Address, DeptID, AdmissionYear, Status) " +
                        $"VALUES (N'{txtStudentCode.Text}', N'{txtFullName.Text}', N'{cboGender.Text}', '{dtpDateOfBirth.Value:yyyy-MM-dd}', N'{txtEmail.Text}', N'{txtPhone.Text}', N'{txtAddress.Text}', {cboDeptID.SelectedValue}, {numAdmissionYear.Value}, N'{cboStatus.Text}')";
                    kn.ThucThiSQL(sql);

                    // Lấy StudentID vừa thêm (dựa vào StudentCode)
                    string sqlGetId = "SELECT StudentID FROM Students WHERE StudentCode = N'" + txtStudentCode.Text + "'";
                    if (kn.cnn.State != ConnectionState.Open)
                        kn.cnn.Open();
                    SqlCommand cmdGetId = new SqlCommand(sqlGetId, kn.cnn);
                    object oid = cmdGetId.ExecuteScalar();
                    int idSinhVienMoi = -1;
                    if (oid != null)
                    {
                        idSinhVienMoi = Convert.ToInt32(oid);
                        // Thêm user cho sinh viên mới
                        string username = txtStudentCode.Text.Trim();
                        string password = txtPhone.Text.Trim();
                        string hashPassword = password;
                        string sqlInsertUser = "INSERT INTO Users (Username, PasswordHash, Role, LinkedStudentID) " +
                                               "VALUES (N'" + username + "', N'" + hashPassword + "', N'Sinh viên', " + idSinhVienMoi + ")";
                        kn.ThucThiSQL(sqlInsertUser);
                    }
                    MessageBox.Show("Lưu dữ liệu thành công.\nTài khoản: " + txtStudentCode.Text + "\nMật khẩu: " + txtPhone.Text,
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
            else
            {
                string sql_update = $"UPDATE Students SET " +
                    $"FullName=N'{txtFullName.Text}', Gender=N'{cboGender.Text}', DateOfBirth='{dtpDateOfBirth.Value:yyyy-MM-dd}', " +
                    $"Email=N'{txtEmail.Text}', Phone=N'{txtPhone.Text}', Address=N'{txtAddress.Text}', DeptID={cboDeptID.SelectedValue}, " +
                    $"AdmissionYear={numAdmissionYear.Value}, Status=N'{cboStatus.Text}' " +
                    $"WHERE StudentID={_idSinhVien.Value}";
                kn.ThucThiSQL(sql_update);
                MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }


        private void FrmSinhVien_ChinhSua_Load(object sender, EventArgs e)
        {
            Bang_KhoaVien();
            cboGender.Items.AddRange(new string[] { "Nam", "Nữ", "Khác" });
            cboStatus.Items.AddRange(new string[] { "Đang học", "Tốt nghiệp", "Bảo lưu", "Thôi học" });
            numAdmissionYear.Minimum = 2000;
            numAdmissionYear.Maximum = 2099;
            numAdmissionYear.Value = DateTime.Now.Year;

            if (_idSinhVien == null)
            {
                cboStatus.Text = "Đang học";
            }
            Load_DuLieuCanSua();
        }
    }
}