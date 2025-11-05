namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmQuenMK
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new Sunny.UI.UIPanel();
            this.groupBoxPassword = new Sunny.UI.UIGroupBox();
            this.lblConfirmPasswordValidation = new System.Windows.Forms.Label();
            this.lblNewPasswordValidation = new System.Windows.Forms.Label();
            this.lblUsernameValidation = new System.Windows.Forms.Label();
            this.btnToggleConfirm = new Sunny.UI.UIButton();
            this.btnToggleNew = new Sunny.UI.UIButton();
            this.txtConfirmPassword = new Sunny.UI.UITextBox();
            this.txtNewPassword = new Sunny.UI.UITextBox();
            this.txtUsername = new Sunny.UI.UITextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new Sunny.UI.UIButton();
            this.btnResetPassword = new Sunny.UI.UIButton();
            this.pnlProgress = new Sunny.UI.UIPanel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.groupBoxPassword.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1035, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(280, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(328, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🔓 KHÔI PHỤC MẬT KHẨU";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.groupBoxPassword);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Controls.Add(this.pnlProgress);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlMain.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.pnlMain.Location = new System.Drawing.Point(0, 60);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMain.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Radius = 0;
            this.pnlMain.RectColor = System.Drawing.Color.Transparent;
            this.pnlMain.Size = new System.Drawing.Size(1035, 540);
            this.pnlMain.TabIndex = 1;
            this.pnlMain.Text = null;
            this.pnlMain.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxPassword
            // 
            this.groupBoxPassword.Controls.Add(this.lblConfirmPasswordValidation);
            this.groupBoxPassword.Controls.Add(this.lblNewPasswordValidation);
            this.groupBoxPassword.Controls.Add(this.lblUsernameValidation);
            this.groupBoxPassword.Controls.Add(this.btnToggleConfirm);
            this.groupBoxPassword.Controls.Add(this.btnToggleNew);
            this.groupBoxPassword.Controls.Add(this.txtConfirmPassword);
            this.groupBoxPassword.Controls.Add(this.txtNewPassword);
            this.groupBoxPassword.Controls.Add(this.txtUsername);
            this.groupBoxPassword.Controls.Add(this.lblConfirmPassword);
            this.groupBoxPassword.Controls.Add(this.lblNewPassword);
            this.groupBoxPassword.Controls.Add(this.lblUsername);
            this.groupBoxPassword.FillColor = System.Drawing.Color.White;
            this.groupBoxPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.groupBoxPassword.Location = new System.Drawing.Point(50, 50);
            this.groupBoxPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxPassword.MinimumSize = new System.Drawing.Size(1, 1);
            this.groupBoxPassword.Name = "groupBoxPassword";
            this.groupBoxPassword.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.groupBoxPassword.Radius = 10;
            this.groupBoxPassword.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.groupBoxPassword.Size = new System.Drawing.Size(700, 320);
            this.groupBoxPassword.TabIndex = 1;
            this.groupBoxPassword.Text = "Khôi phục mật khẩu";
            this.groupBoxPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblConfirmPasswordValidation
            // 
            this.lblConfirmPasswordValidation.AutoSize = true;
            this.lblConfirmPasswordValidation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfirmPasswordValidation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblConfirmPasswordValidation.Location = new System.Drawing.Point(250, 270);
            this.lblConfirmPasswordValidation.Name = "lblConfirmPasswordValidation";
            this.lblConfirmPasswordValidation.Size = new System.Drawing.Size(0, 15);
            this.lblConfirmPasswordValidation.TabIndex = 11;
            // 
            // lblNewPasswordValidation
            // 
            this.lblNewPasswordValidation.AutoSize = true;
            this.lblNewPasswordValidation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNewPasswordValidation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblNewPasswordValidation.Location = new System.Drawing.Point(250, 190);
            this.lblNewPasswordValidation.Name = "lblNewPasswordValidation";
            this.lblNewPasswordValidation.Size = new System.Drawing.Size(0, 15);
            this.lblNewPasswordValidation.TabIndex = 10;
            // 
            // lblUsernameValidation
            // 
            this.lblUsernameValidation.AutoSize = true;
            this.lblUsernameValidation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsernameValidation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblUsernameValidation.Location = new System.Drawing.Point(250, 110);
            this.lblUsernameValidation.Name = "lblUsernameValidation";
            this.lblUsernameValidation.Size = new System.Drawing.Size(0, 15);
            this.lblUsernameValidation.TabIndex = 9;
            // 
            // btnToggleConfirm
            // 
            this.btnToggleConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleConfirm.FillColor = System.Drawing.Color.Transparent;
            this.btnToggleConfirm.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.btnToggleConfirm.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnToggleConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.btnToggleConfirm.Location = new System.Drawing.Point(620, 235);
            this.btnToggleConfirm.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnToggleConfirm.Name = "btnToggleConfirm";
            this.btnToggleConfirm.Radius = 15;
            this.btnToggleConfirm.RectColor = System.Drawing.Color.Transparent;
            this.btnToggleConfirm.Size = new System.Drawing.Size(40, 30);
            this.btnToggleConfirm.TabIndex = 8;
            this.btnToggleConfirm.Text = "👁";
            this.btnToggleConfirm.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnToggleConfirm.Click += new System.EventHandler(this.btnToggleConfirm_Click);
            // 
            // btnToggleNew
            // 
            this.btnToggleNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleNew.FillColor = System.Drawing.Color.Transparent;
            this.btnToggleNew.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.btnToggleNew.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnToggleNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.btnToggleNew.Location = new System.Drawing.Point(620, 155);
            this.btnToggleNew.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnToggleNew.Name = "btnToggleNew";
            this.btnToggleNew.Radius = 15;
            this.btnToggleNew.RectColor = System.Drawing.Color.Transparent;
            this.btnToggleNew.Size = new System.Drawing.Size(40, 30);
            this.btnToggleNew.TabIndex = 7;
            this.btnToggleNew.Text = "👁";
            this.btnToggleNew.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnToggleNew.Click += new System.EventHandler(this.btnToggleNew_Click);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(250, 235);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtConfirmPassword.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Padding = new System.Windows.Forms.Padding(5);
            this.txtConfirmPassword.PasswordChar = '●';
            this.txtConfirmPassword.ShowText = false;
            this.txtConfirmPassword.Size = new System.Drawing.Size(360, 30);
            this.txtConfirmPassword.TabIndex = 5;
            this.txtConfirmPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtConfirmPassword.Watermark = "Nhập lại mật khẩu mới";
            this.txtConfirmPassword.TextChanged += new System.EventHandler(this.txtConfirmPassword_TextChanged);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtNewPassword.Location = new System.Drawing.Point(250, 155);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNewPassword.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Padding = new System.Windows.Forms.Padding(5);
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.ShowText = false;
            this.txtNewPassword.Size = new System.Drawing.Size(360, 30);
            this.txtNewPassword.TabIndex = 4;
            this.txtNewPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtNewPassword.Watermark = "Nhập mật khẩu mới";
            this.txtNewPassword.TextChanged += new System.EventHandler(this.txtNewPassword_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUsername.Location = new System.Drawing.Point(250, 75);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtUsername.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Padding = new System.Windows.Forms.Padding(5);
            this.txtUsername.ShowText = false;
            this.txtUsername.Size = new System.Drawing.Size(360, 30);
            this.txtUsername.TabIndex = 3;
            this.txtUsername.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtUsername.Watermark = "Nhập tên đăng nhập cần khôi phục";
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblConfirmPassword.Location = new System.Drawing.Point(30, 240);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(171, 21);
            this.lblConfirmPassword.TabIndex = 2;
            this.lblConfirmPassword.Text = "🔁 Xác nhận mật khẩu:";
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNewPassword.Location = new System.Drawing.Point(30, 160);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(136, 21);
            this.lblNewPassword.TabIndex = 1;
            this.lblNewPassword.Text = "🔐 Mật khẩu mới:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUsername.Location = new System.Drawing.Point(30, 80);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(143, 21);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "👤 Tên đăng nhập:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnResetPassword);
            this.pnlButtons.Location = new System.Drawing.Point(50, 390);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(700, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(380, 10);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Radius = 8;
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "❌ Hủy bỏ";
            this.btnCancel.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResetPassword.Enabled = false;
            this.btnResetPassword.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnResetPassword.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnResetPassword.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(186)))), ((int)(((byte)(91)))));
            this.btnResetPassword.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(164)))), ((int)(((byte)(69)))));
            this.btnResetPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnResetPassword.Location = new System.Drawing.Point(170, 10);
            this.btnResetPassword.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Radius = 8;
            this.btnResetPassword.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnResetPassword.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnResetPassword.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(186)))), ((int)(((byte)(91)))));
            this.btnResetPassword.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(164)))), ((int)(((byte)(69)))));
            this.btnResetPassword.Size = new System.Drawing.Size(180, 40);
            this.btnResetPassword.TabIndex = 0;
            this.btnResetPassword.Text = "🔓 Khôi phục";
            this.btnResetPassword.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // pnlProgress
            // 
            this.pnlProgress.BackColor = System.Drawing.Color.Transparent;
            this.pnlProgress.Controls.Add(this.progressBar);
            this.pnlProgress.Controls.Add(this.lblProgress);
            this.pnlProgress.FillColor = System.Drawing.Color.Transparent;
            this.pnlProgress.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.pnlProgress.Location = new System.Drawing.Point(50, 470);
            this.pnlProgress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlProgress.MinimumSize = new System.Drawing.Size(1, 1);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Radius = 0;
            this.pnlProgress.RectColor = System.Drawing.Color.Transparent;
            this.pnlProgress.Size = new System.Drawing.Size(700, 40);
            this.pnlProgress.TabIndex = 3;
            this.pnlProgress.Text = null;
            this.pnlProgress.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlProgress.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.progressBar.Location = new System.Drawing.Point(150, 20);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(400, 15);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.lblProgress.Location = new System.Drawing.Point(300, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(83, 19);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.Text = "Đang xử lý...";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmQuenMK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1035, 600);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmQuenMK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Khôi phục mật khẩu";
            this.Load += new System.EventHandler(this.FrmQuenMK_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.groupBoxPassword.ResumeLayout(false);
            this.groupBoxPassword.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Sunny.UI.UIPanel pnlMain;
        private Sunny.UI.UIGroupBox groupBoxPassword;
        private Sunny.UI.UITextBox txtUsername;
        private Sunny.UI.UITextBox txtNewPassword;
        private Sunny.UI.UITextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private Sunny.UI.UIButton btnToggleNew;
        private Sunny.UI.UIButton btnToggleConfirm;
        private System.Windows.Forms.Panel pnlButtons;
        private Sunny.UI.UIButton btnResetPassword;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIPanel pnlProgress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblUsernameValidation;
        private System.Windows.Forms.Label lblNewPasswordValidation;
        private System.Windows.Forms.Label lblConfirmPasswordValidation;
    }
}