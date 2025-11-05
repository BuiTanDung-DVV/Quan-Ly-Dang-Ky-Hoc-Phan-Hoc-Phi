using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmDoiMK : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();
        private bool isCurrentVisible = false;
        private bool isNewVisible = false;
        private bool isConfirmVisible = false;

        public FrmDoiMK()
        {
            InitializeComponent();
        }

        private void FrmQuenMK_Load(object sender, EventArgs e)
        {
            LoadUserInfo();
            SetupPasswordValidation();
            CenterTitle();
            InitializeValidationLabels();
        }

        private void LoadUserInfo()
        {
            try
            {
                lblCurrentUser.Text = $"👤 Tên đăng nhập: {UserSession.Username}";
                lblRole.Text = $"🏷️ Vai trò: {GetRoleDisplayName()}";
                lblUserID.Text = $"🆔 ID: {UserSession.UserID}";
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi load thông tin: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private string GetRoleDisplayName()
        {
            if (UserSession.IsAdmin()) return "Quản trị viên";
            if (UserSession.IsLecturer()) return "Giảng viên";
            if (UserSession.IsStudent()) return "Sinh viên";
            return UserSession.Role;
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
            // Initialize validation labels to be empty
            lblCurrentPasswordValidation.Text = "";
            lblNewPasswordValidation.Text = "";
            lblConfirmPasswordValidation.Text = "";
        }

        private void SetupPasswordValidation()
        {
            txtCurrentPassword.TextChanged += ValidateForm;
            txtNewPassword.TextChanged += ValidateForm;
            txtConfirmPassword.TextChanged += ValidateForm;
        }

        private void ValidateForm(object sender, EventArgs e)
        {
            bool isValid = !string.IsNullOrWhiteSpace(txtCurrentPassword.Text) &&
                          !string.IsNullOrWhiteSpace(txtNewPassword.Text) &&
                          !string.IsNullOrWhiteSpace(txtConfirmPassword.Text) &&
                          txtNewPassword.Text == txtConfirmPassword.Text &&
                          IsPasswordStrong(txtNewPassword.Text);

            btnChangePassword.Enabled = isValid;
            
            UpdatePasswordFieldColors();
        }

        private void UpdatePasswordFieldColors()
        {
            // Update current password field
            if (!string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                txtCurrentPassword.RectColor = Color.FromArgb(76, 175, 80);
            }
            else
            {
                txtCurrentPassword.RectColor = Color.FromArgb(200, 200, 200);
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

            // At least 6 characters
            bool hasMinLength = password.Length >= 6;
            
            // Has uppercase and lowercase
            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            
            // Has number
            bool hasNumber = Regex.IsMatch(password, @"[0-9]");

            return hasMinLength && (hasUpperCase || hasLowerCase) && hasNumber;
        }

        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateCurrentPassword();
        }

        private void ValidateCurrentPassword()
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                lblCurrentPasswordValidation.Text = "";
                lblCurrentPasswordValidation.ForeColor = Color.FromArgb(244, 67, 54);
            }
            else
            {
                lblCurrentPasswordValidation.Text = "✅ Đã nhập mật khẩu hiện tại";
                lblCurrentPasswordValidation.ForeColor = Color.FromArgb(76, 175, 80);
            }
        }

        private void txtNewPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateNewPassword();
            ValidateConfirmPassword(); // Re-validate confirm password when new password changes
        }

        private void ValidateNewPassword()
        {
            string password = txtNewPassword.Text;
            
            if (string.IsNullOrWhiteSpace(password))
            {
                lblNewPasswordValidation.Text = "";
                txtNewPassword.Watermark = "Nhập mật khẩu mới";
                return;
            }

            var validationMessages = new System.Collections.Generic.List<string>();
            
            // Check minimum length
            if (password.Length < 6)
            {
                validationMessages.Add("ít nhất 6 ký tự");
            }
            
            // Check for uppercase
            bool hasUpperCase = Regex.IsMatch(password, @"[A-Z]");
            if (!hasUpperCase)
            {
                validationMessages.Add("chữ hoa (A-Z)");
            }
            
            // Check for lowercase  
            bool hasLowerCase = Regex.IsMatch(password, @"[a-z]");
            if (!hasLowerCase)
            {
                validationMessages.Add("chữ thường (a-z)");
            }
            
            // Check for number
            bool hasNumber = Regex.IsMatch(password, @"[0-9]");
            if (!hasNumber)
            {
                validationMessages.Add("số (0-9)");
            }

            if (validationMessages.Count == 0)
            {
                lblNewPasswordValidation.Text = "✅ Mật khẩu mạnh";
                lblNewPasswordValidation.ForeColor = Color.FromArgb(76, 175, 80);
                txtNewPassword.Watermark = "✅ Mật khẩu mạnh";
            }
            else
            {
                lblNewPasswordValidation.Text = "❌ Cần: " + string.Join(", ", validationMessages);
                lblNewPasswordValidation.ForeColor = Color.FromArgb(244, 67, 54);
                txtNewPassword.Watermark = "⚠️ Cần thêm: " + string.Join(", ", validationMessages);
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

        private void btnToggleCurrent_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility(txtCurrentPassword, btnToggleCurrent, ref isCurrentVisible);
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

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!ValidatePasswords()) return;

            ShowProgress(true);

            try
            {
                // Verify current password
                if (!VerifyCurrentPassword(txtCurrentPassword.Text))
                {
                    ShowMessage("Mật khẩu hiện tại không đúng!", "Lỗi xác thực", MessageBoxIcon.Error);
                    txtCurrentPassword.Focus();
                    txtCurrentPassword.SelectAll();
                    return;
                }

                // Update password
                if (UpdatePassword(txtNewPassword.Text))
                {
                    ShowMessage("Đổi mật khẩu thành công!\nVui lòng đăng nhập lại với mật khẩu mới.", 
                              "Thành công", MessageBoxIcon.Information);
                    
                    // Log out user
                    UserSession.EndSession();
                    
                    // Return to login
                    Application.Restart();
                }
                else
                {
                    ShowMessage("Có lỗi xảy ra khi đổi mật khẩu!\nVui lòng thử lại.", "Lỗi", MessageBoxIcon.Error);
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
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                ShowMessage("Vui lòng nhập mật khẩu hiện tại!", "Thông báo", MessageBoxIcon.Warning);
                txtCurrentPassword.Focus();
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

            if (txtCurrentPassword.Text == txtNewPassword.Text)
            {
                ShowMessage("Mật khẩu mới phải khác mật khẩu hiện tại!", "Thông báo", MessageBoxIcon.Warning);
                txtNewPassword.Focus();
                return false;
            }

            return true;
        }

        private bool VerifyCurrentPassword(string currentPassword)
        {
            try
            {
                string sql = $"SELECT COUNT(*) FROM Users WHERE UserID = {UserSession.UserID} AND PasswordHash = '{currentPassword}'";
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
                ShowMessage($"Lỗi kiểm tra mật khẩu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                return false;
            }
        }

        private bool UpdatePassword(string newPassword)
        {
            try
            {
                string sql = $"UPDATE Users SET PasswordHash = '{newPassword}' WHERE UserID = {UserSession.UserID}";
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
            btnChangePassword.Enabled = !show;
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
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy bỏ thay đổi mật khẩu?", 
                                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            btnChangePassword.Enabled = false;
            
            txtCurrentPassword.RectColor = Color.FromArgb(200, 200, 200);
            txtNewPassword.RectColor = Color.FromArgb(200, 200, 200);
            txtConfirmPassword.RectColor = Color.FromArgb(200, 200, 200);
            
            // Clear validation messages
            lblCurrentPasswordValidation.Text = "";
            lblNewPasswordValidation.Text = "";
            lblConfirmPasswordValidation.Text = "";
            
            txtCurrentPassword.Focus();
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
            else if (keyData == Keys.Enter && btnChangePassword.Enabled)
            {
                btnChangePassword_Click(this, EventArgs.Empty);
                return true;
            }
            
            return base.ProcessDialogKey(keyData);
        }
    }
}