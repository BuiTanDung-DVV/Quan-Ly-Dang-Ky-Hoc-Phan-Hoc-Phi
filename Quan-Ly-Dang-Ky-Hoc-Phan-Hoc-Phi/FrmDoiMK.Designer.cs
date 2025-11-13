namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmDoiMK
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
            this.groupBoxInfo = new Sunny.UI.UIGroupBox();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.groupBoxPassword = new Sunny.UI.UIGroupBox();
            this.lblConfirmPasswordValidation = new System.Windows.Forms.Label();
            this.lblNewPasswordValidation = new System.Windows.Forms.Label();
            this.lblCurrentPasswordValidation = new System.Windows.Forms.Label();
            this.btnToggleConfirm = new Sunny.UI.UIButton();
            this.btnToggleNew = new Sunny.UI.UIButton();
            this.btnToggleCurrent = new Sunny.UI.UIButton();
            this.txtConfirmPassword = new Sunny.UI.UITextBox();
            this.txtNewPassword = new Sunny.UI.UITextBox();
            this.txtCurrentPassword = new Sunny.UI.UITextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.lblCurrentPassword = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new Sunny.UI.UIButton();
            this.btnChangePassword = new Sunny.UI.UIButton();
            this.pnlProgress = new Sunny.UI.UIPanel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
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
            this.pnlTop.Size = new System.Drawing.Size(805, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(280, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(235, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🔒 ĐỔI MẬT KHẨU";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.groupBoxInfo);
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
            this.pnlMain.Size = new System.Drawing.Size(805, 610);
            this.pnlMain.TabIndex = 1;
            this.pnlMain.Text = null;
            this.pnlMain.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlMain.Click += new System.EventHandler(this.pnlMain_Click);
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.lblCurrentUser);
            this.groupBoxInfo.Controls.Add(this.lblRole);
            this.groupBoxInfo.Controls.Add(this.lblUserID);
            this.groupBoxInfo.FillColor = System.Drawing.Color.White;
            this.groupBoxInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.groupBoxInfo.Location = new System.Drawing.Point(50, 30);
            this.groupBoxInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxInfo.MinimumSize = new System.Drawing.Size(1, 1);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.groupBoxInfo.Radius = 10;
            this.groupBoxInfo.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.groupBoxInfo.Size = new System.Drawing.Size(700, 120);
            this.groupBoxInfo.TabIndex = 0;
            this.groupBoxInfo.Text = "Thông tin tài khoản";
            this.groupBoxInfo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblCurrentUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCurrentUser.Location = new System.Drawing.Point(77, 79);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(188, 21);
            this.lblCurrentUser.TabIndex = 0;
            this.lblCurrentUser.Text = "👤 Tên đăng nhập: admin";
            // 
            // lblRole
            // 
            this.lblRole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRole.Location = new System.Drawing.Point(246, 32);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(134, 21);
            this.lblRole.TabIndex = 1;
            this.lblRole.Text = "🏷️ Vai trò: Admin";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblUserID
            // 
            this.lblUserID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblUserID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUserID.Location = new System.Drawing.Point(445, 79);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(67, 21);
            this.lblUserID.TabIndex = 2;
            this.lblUserID.Text = "🆔 ID: 1";
            // 
            // groupBoxPassword
            // 
            this.groupBoxPassword.Controls.Add(this.lblConfirmPasswordValidation);
            this.groupBoxPassword.Controls.Add(this.lblNewPasswordValidation);
            this.groupBoxPassword.Controls.Add(this.lblCurrentPasswordValidation);
            this.groupBoxPassword.Controls.Add(this.btnToggleConfirm);
            this.groupBoxPassword.Controls.Add(this.btnToggleNew);
            this.groupBoxPassword.Controls.Add(this.btnToggleCurrent);
            this.groupBoxPassword.Controls.Add(this.txtConfirmPassword);
            this.groupBoxPassword.Controls.Add(this.txtNewPassword);
            this.groupBoxPassword.Controls.Add(this.txtCurrentPassword);
            this.groupBoxPassword.Controls.Add(this.lblConfirmPassword);
            this.groupBoxPassword.Controls.Add(this.lblNewPassword);
            this.groupBoxPassword.Controls.Add(this.lblCurrentPassword);
            this.groupBoxPassword.FillColor = System.Drawing.Color.White;
            this.groupBoxPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.groupBoxPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.groupBoxPassword.Location = new System.Drawing.Point(50, 170);
            this.groupBoxPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxPassword.MinimumSize = new System.Drawing.Size(1, 1);
            this.groupBoxPassword.Name = "groupBoxPassword";
            this.groupBoxPassword.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.groupBoxPassword.Radius = 10;
            this.groupBoxPassword.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.groupBoxPassword.Size = new System.Drawing.Size(700, 300);
            this.groupBoxPassword.TabIndex = 1;
            this.groupBoxPassword.Text = "Thay đổi mật khẩu";
            this.groupBoxPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblConfirmPasswordValidation
            // 
            this.lblConfirmPasswordValidation.AutoSize = true;
            this.lblConfirmPasswordValidation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfirmPasswordValidation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblConfirmPasswordValidation.Location = new System.Drawing.Point(250, 220);
            this.lblConfirmPasswordValidation.Name = "lblConfirmPasswordValidation";
            this.lblConfirmPasswordValidation.Size = new System.Drawing.Size(0, 15);
            this.lblConfirmPasswordValidation.TabIndex = 11;
            // 
            // lblNewPasswordValidation
            // 
            this.lblNewPasswordValidation.AutoSize = true;
            this.lblNewPasswordValidation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNewPasswordValidation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblNewPasswordValidation.Location = new System.Drawing.Point(250, 160);
            this.lblNewPasswordValidation.Name = "lblNewPasswordValidation";
            this.lblNewPasswordValidation.Size = new System.Drawing.Size(0, 15);
            this.lblNewPasswordValidation.TabIndex = 10;
            // 
            // lblCurrentPasswordValidation
            // 
            this.lblCurrentPasswordValidation.AutoSize = true;
            this.lblCurrentPasswordValidation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCurrentPasswordValidation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblCurrentPasswordValidation.Location = new System.Drawing.Point(250, 100);
            this.lblCurrentPasswordValidation.Name = "lblCurrentPasswordValidation";
            this.lblCurrentPasswordValidation.Size = new System.Drawing.Size(0, 15);
            this.lblCurrentPasswordValidation.TabIndex = 9;
            // 
            // btnToggleConfirm
            // 
            this.btnToggleConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleConfirm.FillColor = System.Drawing.Color.Transparent;
            this.btnToggleConfirm.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.btnToggleConfirm.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnToggleConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.btnToggleConfirm.Location = new System.Drawing.Point(620, 185);
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
            this.btnToggleNew.Location = new System.Drawing.Point(620, 125);
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
            // btnToggleCurrent
            // 
            this.btnToggleCurrent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleCurrent.FillColor = System.Drawing.Color.Transparent;
            this.btnToggleCurrent.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.btnToggleCurrent.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnToggleCurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(98)))), ((int)(((byte)(102)))));
            this.btnToggleCurrent.Location = new System.Drawing.Point(620, 65);
            this.btnToggleCurrent.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnToggleCurrent.Name = "btnToggleCurrent";
            this.btnToggleCurrent.Radius = 15;
            this.btnToggleCurrent.RectColor = System.Drawing.Color.Transparent;
            this.btnToggleCurrent.Size = new System.Drawing.Size(40, 30);
            this.btnToggleCurrent.TabIndex = 6;
            this.btnToggleCurrent.Text = "👁";
            this.btnToggleCurrent.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnToggleCurrent.Click += new System.EventHandler(this.btnToggleCurrent_Click);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(250, 185);
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
            this.txtNewPassword.Location = new System.Drawing.Point(250, 125);
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
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtCurrentPassword.Location = new System.Drawing.Point(250, 65);
            this.txtCurrentPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCurrentPassword.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.Padding = new System.Windows.Forms.Padding(5);
            this.txtCurrentPassword.PasswordChar = '●';
            this.txtCurrentPassword.ShowText = false;
            this.txtCurrentPassword.Size = new System.Drawing.Size(360, 30);
            this.txtCurrentPassword.TabIndex = 3;
            this.txtCurrentPassword.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtCurrentPassword.Watermark = "Nhập mật khẩu hiện tại";
            this.txtCurrentPassword.TextChanged += new System.EventHandler(this.txtCurrentPassword_TextChanged);
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblConfirmPassword.Location = new System.Drawing.Point(30, 190);
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
            this.lblNewPassword.Location = new System.Drawing.Point(30, 130);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(136, 21);
            this.lblNewPassword.TabIndex = 1;
            this.lblNewPassword.Text = "🔐 Mật khẩu mới:";
            // 
            // lblCurrentPassword
            // 
            this.lblCurrentPassword.AutoSize = true;
            this.lblCurrentPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblCurrentPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCurrentPassword.Location = new System.Drawing.Point(30, 70);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(159, 21);
            this.lblCurrentPassword.TabIndex = 0;
            this.lblCurrentPassword.Text = "🔓 Mật khẩu hiện tại:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnChangePassword);
            this.pnlButtons.Location = new System.Drawing.Point(50, 490);
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
            // btnChangePassword
            // 
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.Enabled = false;
            this.btnChangePassword.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnChangePassword.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnChangePassword.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(186)))), ((int)(((byte)(91)))));
            this.btnChangePassword.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(164)))), ((int)(((byte)(69)))));
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnChangePassword.Location = new System.Drawing.Point(170, 10);
            this.btnChangePassword.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Radius = 8;
            this.btnChangePassword.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnChangePassword.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnChangePassword.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(186)))), ((int)(((byte)(91)))));
            this.btnChangePassword.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(164)))), ((int)(((byte)(69)))));
            this.btnChangePassword.Size = new System.Drawing.Size(180, 40);
            this.btnChangePassword.TabIndex = 0;
            this.btnChangePassword.Text = "✅ Đổi mật khẩu";
            this.btnChangePassword.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // pnlProgress
            // 
            this.pnlProgress.BackColor = System.Drawing.Color.Transparent;
            this.pnlProgress.Controls.Add(this.progressBar);
            this.pnlProgress.Controls.Add(this.lblProgress);
            this.pnlProgress.FillColor = System.Drawing.Color.Transparent;
            this.pnlProgress.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.pnlProgress.Location = new System.Drawing.Point(50, 560);
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
            // FrmDoiMK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(805, 670);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDoiMK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đổi mật khẩu";
            this.Load += new System.EventHandler(this.FrmQuenMK_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
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
        private Sunny.UI.UIGroupBox groupBoxInfo;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Label lblUserID;
        private Sunny.UI.UIGroupBox groupBoxPassword;
        private Sunny.UI.UITextBox txtCurrentPassword;
        private Sunny.UI.UITextBox txtNewPassword;
        private Sunny.UI.UITextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private Sunny.UI.UIButton btnToggleCurrent;
        private Sunny.UI.UIButton btnToggleNew;
        private Sunny.UI.UIButton btnToggleConfirm;
        private System.Windows.Forms.Panel pnlButtons;
        private Sunny.UI.UIButton btnChangePassword;
        private Sunny.UI.UIButton btnCancel;
        private Sunny.UI.UIPanel pnlProgress;
        private System.Windows.Forms.ProgressBar progressBar;   
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblCurrentPasswordValidation;
        private System.Windows.Forms.Label lblNewPasswordValidation;
        private System.Windows.Forms.Label lblConfirmPasswordValidation;
    }
}