using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmLogin : Form
    {
        private bool isPasswordVisible = false;
        public FrmLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            AddFormAnimations();
        }
        
        KETNOI_CSDL kn = new KETNOI_CSDL();

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtDangNhap.Focus();
        }

        private void AddFormAnimations()
        {
            this.Opacity = 0;
            Timer fadeInTimer = new Timer();
            fadeInTimer.Interval = 50;
            fadeInTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1)
                    this.Opacity += 0.1;
                else
                    fadeInTimer.Stop();
            };
            fadeInTimer.Start();
        }


        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            
            if (isPasswordVisible)
            {
                txtMatKhau.PasswordChar = '\0';
                btnShowPassword.Text = "🙈";
                btnShowPassword.TipsText = "Ẩn mật khẩu";
            }
            else
            {
                txtMatKhau.PasswordChar = '●';
                btnShowPassword.Text = "👁";
                btnShowPassword.TipsText = "Hiện mật khẩu";
            }
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
            AnimateButton(btnDN);
            
            if (string.IsNullOrWhiteSpace(txtDangNhap.Text))
            {
                ShowStyledMessage("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxIcon.Warning);
                txtDangNhap.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                ShowStyledMessage("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            try
            {
                ShowLoadingEffect();
                
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

                    ShowStyledMessage($"Đăng nhập thành công! Chào mừng {UserSession.Username}", "Thành công",
                                  MessageBoxIcon.Information);

                    FadeOutAndClose();
                }
                else
                {
                    ShowStyledMessage("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi đăng nhập",
                                  MessageBoxIcon.Error);
                    txtMatKhau.Text = "";
                    txtDangNhap.Focus();

                    ShakeForm();
                }
            }
            catch (Exception ex)
            {
                ShowStyledMessage("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi",
                              MessageBoxIcon.Error);
            }
            finally
            {
                HideLoadingEffect();
            }
        }

        private void AnimateButton(Sunny.UI.UIButton button)
        {
            var originalSize = button.Size;
            button.Size = new Size(originalSize.Width - 5, originalSize.Height - 2);
            
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += (s, e) =>
            {
                button.Size = originalSize;
                timer.Stop();
            };
            timer.Start();
        }

        private void ShowLoadingEffect()
        {
            btnDN.Text = "Đang đăng nhập...";
            btnDN.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
        }

        private void HideLoadingEffect()
        {
            btnDN.Text = "ĐĂNG NHẬP";
            btnDN.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void FadeOutAndClose()
        {
            Timer fadeOutTimer = new Timer();
            fadeOutTimer.Interval = 50;
            fadeOutTimer.Tick += (s, e) =>
            {
                if (this.Opacity > 0)
                    this.Opacity -= 0.1;
                else
                {
                    fadeOutTimer.Stop();
                    this.Hide();
                    FrmMain mainForm = new FrmMain();
                    mainForm.Show();
                }
            };
            fadeOutTimer.Start();
        }

        private void ShakeForm()
        {
            var original = this.Location;
            var rnd = new Random(1337);
            const int shake_amplitude = 10;
            
            for (int i = 0; i < 10; i++)
            {
                this.Location = new Point(original.X + rnd.Next(-shake_amplitude, shake_amplitude),
                                         original.Y + rnd.Next(-shake_amplitude, shake_amplitude));
                
                System.Threading.Thread.Sleep(20);
                Application.DoEvents();
            }
            
            this.Location = original;
        }

        private void ShowStyledMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void linkLabelQuenMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Ẩn form login hiện tại với hiệu ứng fade
                this.Hide();
                
                // Tạo và hiển thị form khôi phục mật khẩu
                FrmQuenMK formQuenMK = new FrmQuenMK();
                formQuenMK.ShowDialog();
                
                // Sau khi đóng form khôi phục mật khẩu, hiển thị lại form login
                this.Show();
            }
            catch (Exception ex)
            {
                ShowStyledMessage($"Lỗi khi mở form khôi phục mật khẩu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }
    }
}
