using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmQuenMK : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();
        private bool isNewVisible = false;
        private bool isConfirmVisible = false;

        public FrmQuenMK()
        {
            InitializeComponent();
        }

        private void FrmQuenMK_Load(object sender, EventArgs e)
        {
            SetupPasswordValidation();
            CenterTitle();
            InitializeValidationLabels();
        }

        private void CenterTitle()
        {
            if (lblTitle != null && pnlTop != null)
            {
                lblTitle.Location = new Point(
                    (pnlTop.Width - lblTitle.Width) / 2,
                    (pnlTop.Height - lblTitle.Height) / 2
                );
            }
        }

        private void InitializeValidationLabels()
        {
            lblUsernameValidation.Text = "";
            lblNewPasswordValidation.Text = "";
            lblConfirmPasswordValidation.Text = "";
        }

        private void SetupPasswordValidation()
        {
            txtUsername.TextChanged += ValidateForm;
            txtNewPassword.TextChanged += ValidateForm;
            txtConfirmPassword.TextChanged += ValidateForm;
        }

        private void ValidateForm(object sender, EventArgs e)
        {
            bool isValid = !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                          !string.IsNullOrWhiteSpace(txtNewPassword.Text) &&
                          !string.IsNullOrWhiteSpace(txtConfirmPassword.Text) &&
                          txtNewPassword.Text == txtConfirmPassword.Text &&
                          IsPasswordStrong(txtNewPassword.Text);

            btnResetPassword.Enabled = isValid;
            
            UpdatePasswordFieldColors();
        }

        private void UpdatePasswordFieldColors()
        {
            // Update username field
            if (!string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.RectColor = Color.FromArgb(76, 175, 80);
            }
            else
            {
                txtUsername.RectColor = Color.FromArgb(200, 200, 200);
            }

            // Update new password field
            if (!string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                if (IsPasswordStrong(txtNewPassword.Text))
                {
                    txtNewPassword.RectColor = Color.FromArgb(76, 175, 80);
                }
                else
                {
                    txtNewPassword.RectColor = Color.FromArgb(244, 67, 54);
                }
            }
            else
            {
                txtNewPassword.RectColor = Color.FromArgb(200, 200, 200);
            }

            // Update confirm password field
            if (!string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                if (txtNewPassword.Text == txtConfirmPassword.Text && IsPasswordStrong(txtNewPassword.Text))
                {
                    txtConfirmPassword.RectColor = Color.FromArgb(76, 175, 80);
                }
                else
                {
                    txtConfirmPassword.RectColor = Color.FromArgb(244, 67, 54);
                }
            }
            else
            {
                txtConfirmPassword.RectColor = Color.FromArgb(200, 200, 200);
            }
        }

        private bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return false;

            bool hasMinLength = password.Length >= 6;
            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            bool hasNumber = Regex.IsMatch(password, @"[0-9]");

            return hasMinLength && (hasUpperCase || hasLowerCase) && hasNumber;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            ValidateUsername();
        }

        private void ValidateUsername()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblUsernameValidation.Text = "";
                lblUsernameValidation.ForeColor = Color.FromArgb(244, 67, 54);
            }
            else
            {
                lblUsernameValidation.Text = "✅ Đã nhập tên đăng nhập";
                lblUsernameValidation.ForeColor = Color.FromArgb(76, 175, 80);
            }
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateNewPassword();
            ValidateConfirmPassword();
        }

        private void ValidateNewPassword()
        {
            string password = txtNewPassword.Text;
            
            if (string.IsNullOrWhiteSpace(password))
            {
                lblNewPasswordValidation.Text = "";
                return;
            }

            var validationMessages = new System.Collections.Generic.List<string>();
            
            if (password.Length < 6)
            {
                validationMessages.Add("ít nhất 6 ký tự");
            }
            
            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            if (!hasUpperCase)
            {
                validationMessages.Add("chữ hoa (A-Z)");
            }
            
            bool hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            if (!hasLowerCase)
            {
                validationMessages.Add("chữ thường (a-z)");
            }
            
            bool hasNumber = Regex.IsMatch(password, @"[0-9]");
            if (!hasNumber)
            {
                validationMessages.Add("số (0-9)");
            }

            if (validationMessages.Count == 0)
            {
                lblNewPasswordValidation.Text = "✅ Mật khẩu mạnh";
                lblNewPasswordValidation.ForeColor = Color.FromArgb(76, 175, 80);
            }
            else
            {
                lblNewPasswordValidation.Text = "❌ Cần: " + string.Join(", ", validationMessages);
                lblNewPasswordValidation.ForeColor = Color.FromArgb(244, 67, 54);
            }
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateConfirmPassword();
        }

        private void ValidateConfirmPassword()
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                lblConfirmPasswordValidation.Text = "";
                return;
            }

            if (txtNewPassword.Text == txtConfirmPassword.Text)
            {
                if (IsPasswordStrong(txtNewPassword.Text))
                {
                    lblConfirmPasswordValidation.Text = "✅ Mật khẩu khớp và mạnh";
                    lblConfirmPasswordValidation.ForeColor = Color.FromArgb(76, 175, 80);
                }
                else
                {
                    lblConfirmPasswordValidation.Text = "⚠️ Mật khẩu khớp nhưng chưa đủ mạnh";
                    lblConfirmPasswordValidation.ForeColor = Color.FromArgb(255, 152, 0);
                }
            }
            else
            {
                lblConfirmPasswordValidation.Text = "❌ Mật khẩu không khớp";
                lblConfirmPasswordValidation.ForeColor = Color.FromArgb(244, 67, 54);
            }
        }

        private void btnToggleNew_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txtNewPassword, btnToggleNew, ref isNewVisible);
        }

        private void btnToggleConfirm_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txtConfirmPassword, btnToggleConfirm, ref isConfirmVisible);
        }

        private void TogglePasswordVisibility(Sunny.UI.UITextBox textBox, Sunny.UI.UIButton button, ref bool isVisible)
        {
            isVisible = !isVisible;
            
            if (isVisible)
            {
                textBox.PasswordChar = '\0';
                button.Text = "🙈";
                button.TipsText = "Ẩn mật khẩu";
            }
            else
            {
                textBox.PasswordChar = '●';
                button.Text = "👁";
                button.TipsText = "Hiện mật khẩu";
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (!ValidatePasswords()) return;

            ShowProgress(true);

            try
            {
                // Verify username exists
                if (!VerifyUsernameExists(txtUsername.Text))
                {
                    ShowMessage("Tên đăng nhập không tồn tại trong hệ thống!", "Lỗi xác thực", MessageBoxIcon.Error);
                    txtUsername.Focus();
                    txtUsername.SelectAll();
                    return;
                }

                // Update password
                if (UpdatePassword(txtUsername.Text, txtNewPassword.Text))
                {
                    ShowMessage("Khôi phục mật khẩu thành công!\nBạn có thể đăng nhập với mật khẩu mới.", 
                              "Thành công", MessageBoxIcon.Information);
                    
                    // Return to login
                    this.Hide();
                    FrmLogin loginForm = new FrmLogin();
                    loginForm.Show();
                    this.Close();
                }
                else
                {
                    ShowMessage("Có lỗi xảy ra khi khôi phục mật khẩu!\nVui lòng thử lại.", "Lỗi", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
            finally
            {
                ShowProgress(false);
            }
        }

        private bool ValidatePasswords()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                ShowMessage("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                ShowMessage("Vui lòng nhập mật khẩu mới!", "Thông báo", MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return false;
            }

            if (!IsPasswordStrong(txtNewPassword.Text))
            {
                ShowMessage("Mật khẩu mới chưa đủ mạnh!\nCần có ít nhất 6 ký tự, chữ hoa/thường và số.", "Thông báo", MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return false;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                ShowMessage("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            return true;
        }

        private bool VerifyUsernameExists(string username)
        {
            try
            {
                string sql = $"SELECT COUNT(*) FROM Users WHERE Username = '{username}'";
                DataTable dt = kn.Lay_DulieuBang(sql);
                
                if (dt.Rows.Count > 0)
                {
                    int count = Convert.ToInt32(dt.Rows[0][0]);
                    return count > 0;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi kiểm tra tên đăng nhập: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                return false;
            }
        }

        private bool UpdatePassword(string username, string newPassword)
        {
            try
            {
                string sql = $"UPDATE Users SET PasswordHash = '{newPassword}' WHERE Username = '{username}'";
                kn.ThucThiSQL(sql);
                return true;
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi cập nhật mật khẩu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                return false;
            }
        }

        private void ShowProgress(bool show)
        {
            pnlProgress.Visible = show;
            btnResetPassword.Enabled = !show;
            btnCancel.Enabled = !show;
            
            if (show)
            {
                lblProgress.Text = "Đang xử lý...";
                progressBar.Value = 0;
                
                Timer progressTimer = new Timer();
                progressTimer.Interval = 100;
                progressTimer.Tick += (s, e) =>
                {
                    if (progressBar.Value < 100)
                        progressBar.Value += 10;
                    else
                        progressTimer.Stop();
                };
                progressTimer.Start();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy bỏ khôi phục mật khẩu?", 
                                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                this.Hide();
                FrmLogin loginForm = new FrmLogin();
                loginForm.Show();
                this.Close();
            }
        }

        private void ClearForm()
        {
            txtUsername.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            btnResetPassword.Enabled = false;
            
            txtUsername.RectColor = Color.FromArgb(200, 200, 200);
            txtNewPassword.RectColor = Color.FromArgb(200, 200, 200);
            txtConfirmPassword.RectColor = Color.FromArgb(200, 200, 200);
            
            lblUsernameValidation.Text = "";
            lblNewPasswordValidation.Text = "";
            lblConfirmPasswordValidation.Text = "";
            
            txtUsername.Focus();
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnCancel_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == Keys.Enter && btnResetPassword.Enabled)
            {
                btnResetPassword_Click(this, EventArgs.Empty);
                return true;
            }
            
            return base.ProcessDialogKey(keyData);
        }
    }
}
