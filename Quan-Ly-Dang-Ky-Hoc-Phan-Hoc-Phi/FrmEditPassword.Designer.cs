namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmEditPassword
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

        private void InitializeComponent()
        {
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtOldPassword = new Sunny.UI.UITextBox();
            this.txtNewPassword = new Sunny.UI.UITextBox();
            this.txtConfirm = new Sunny.UI.UITextBox();
            this.btnOk = new Sunny.UI.UIButton();
            this.btnCancel = new Sunny.UI.UIButton();
            this.lblTitle = new System.Windows.Forms.Label();

            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(32, 96, 167);
            this.lblTitle.Location = new System.Drawing.Point(80, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(248, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐỔI MẬT KHẨU";
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.lblOldPassword.Location = new System.Drawing.Point(32, 80);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(97, 25);
            this.lblOldPassword.TabIndex = 1;
            this.lblOldPassword.Text = "Mật khẩu cũ:";
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.lblNewPassword.Location = new System.Drawing.Point(32, 140);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(104, 25);
            this.lblNewPassword.TabIndex = 2;
            this.lblNewPassword.Text = "Mật khẩu mới:";
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.lblConfirm.Location = new System.Drawing.Point(32, 200);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(143, 25);
            this.lblConfirm.TabIndex = 3;
            this.lblConfirm.Text = "Xác nhận mật khẩu:";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOldPassword.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.txtOldPassword.Location = new System.Drawing.Point(180, 76);
            this.txtOldPassword.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.Padding = new System.Windows.Forms.Padding(5);
            this.txtOldPassword.PasswordChar = '●';
            this.txtOldPassword.Size = new System.Drawing.Size(216, 37);
            this.txtOldPassword.TabIndex = 4;
            this.txtOldPassword.Watermark = "Nhập mật khẩu cũ";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewPassword.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.txtNewPassword.Location = new System.Drawing.Point(180, 136);
            this.txtNewPassword.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Padding = new System.Windows.Forms.Padding(5);
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.Size = new System.Drawing.Size(216, 37);
            this.txtNewPassword.TabIndex = 5;
            this.txtNewPassword.Watermark = "Nhập mật khẩu mới";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirm.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.txtConfirm.Location = new System.Drawing.Point(180, 196);
            this.txtConfirm.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Padding = new System.Windows.Forms.Padding(5);
            this.txtConfirm.PasswordChar = '●';
            this.txtConfirm.Size = new System.Drawing.Size(216, 37);
            this.txtConfirm.TabIndex = 6;
            this.txtConfirm.Watermark = "Xác nhận lại mật khẩu";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btnOk.Location = new System.Drawing.Point(100, 260);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(105, 40);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Đồng ý";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 13F);
            this.btnCancel.Location = new System.Drawing.Point(220, 260);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(105, 40);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmEditPassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(430, 325);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.lblOldPassword);
            this.Controls.Add(this.lblTitle);
            this.Name = "FrmEditPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi mật khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Label lblConfirm;
        private Sunny.UI.UITextBox txtOldPassword;
        private Sunny.UI.UITextBox txtNewPassword;
        private Sunny.UI.UITextBox txtConfirm;
        private Sunny.UI.UIButton btnOk;
        private Sunny.UI.UIButton btnCancel;
    }
}