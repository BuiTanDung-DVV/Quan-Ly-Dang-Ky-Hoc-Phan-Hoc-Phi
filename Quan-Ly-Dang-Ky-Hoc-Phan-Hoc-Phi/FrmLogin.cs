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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        KETNOI_CSDL kn = new KETNOI_CSDL();

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // Set focus to username textbox
            txtDangNhap.Focus();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtDangNhap.Texts))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDangNhap.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Texts))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            try
            {
                // Query to check user credentials (NO HASHING)
                string sql = "SELECT UserID, Username, PasswordHash, Role, LinkedStudentID, LinkedLecturerID " +
                           "FROM Users " +
                           "WHERE Username = '" + txtDangNhap.Texts + "' AND PasswordHash = '" + txtMatKhau.Texts + "'";

                DataTable dt = kn.Lay_DulieuBang(sql);

                if (dt.Rows.Count > 0)
                {
                    // Login successful
                    DataRow row = dt.Rows[0];
                    
                    // Start user session
                    UserSession.StartSession(
                        Convert.ToInt32(row["UserID"]),
                        row["Username"].ToString(),
                        row["Role"].ToString(),
                        row["LinkedStudentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["LinkedStudentID"]),
                        row["LinkedLecturerID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["LinkedLecturerID"])
                    );

                    MessageBox.Show($"Đăng nhập thành công! Chào mừng {UserSession.Username}", "Thành công", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hide login form and show main form
                    this.Hide();
                    FrmMain mainForm = new FrmMain();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi đăng nhập", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.Texts = "";
                    txtDangNhap.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ptbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Optional: Add Enter key press events for better user experience
        private void txtDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtMatKhau.Focus();
            }
        }

        private void txtMatKhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDN_Click(sender, e);
            }
        }
    }
}
