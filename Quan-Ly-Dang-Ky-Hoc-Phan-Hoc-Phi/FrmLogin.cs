using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
        }
        KETNOI_CSDL kn = new KETNOI_CSDL();

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtDangNhap.Focus();
        }

        private void ptbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

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
                btnDN_Click_1(sender, e);
            }
        }

        private void btnDN_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDangNhap.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            try
            {
                string sql = "SELECT UserID, Username, PasswordHash, Role, LinkedStudentID, LinkedLecturerID " +
                           "FROM Users " +
                           "WHERE Username = '" + txtDangNhap.Text + "' AND PasswordHash = '" + txtMatKhau.Text + "'";

                DataTable dt = kn.Lay_DulieuBang(sql);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    UserSession.StartSession(
                        Convert.ToInt32(row["UserID"]),
                        row["Username"].ToString(),
                        row["Role"].ToString(),
                        row["LinkedStudentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["LinkedStudentID"]),
                        row["LinkedLecturerID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["LinkedLecturerID"])
                    );

                    MessageBox.Show($"Đăng nhập thành công! Chào mừng {UserSession.Username}", "Thành công",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    FrmMain mainForm = new FrmMain();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi đăng nhập",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.Text = "";
                    txtDangNhap.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
