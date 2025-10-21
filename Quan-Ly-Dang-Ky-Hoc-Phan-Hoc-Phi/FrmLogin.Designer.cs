namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNEU = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelDN = new Sunny.UI.UIPanel();
            this.btnDN = new Sunny.UI.UIButton();
            this.txtMatKhau = new Sunny.UI.UITextBox();
            this.txtDangNhap = new Sunny.UI.UITextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDN = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelDN.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNEU
            // 
            this.lblNEU.AutoSize = true;
            this.lblNEU.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.lblNEU.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblNEU.Location = new System.Drawing.Point(644, 176);
            this.lblNEU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNEU.Name = "lblNEU";
            this.lblNEU.Size = new System.Drawing.Size(409, 31);
            this.lblNEU.TabIndex = 24;
            this.lblNEU.Text = "ĐẠI HỌC KINH TẾ QUỐC DÂN";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = global::Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi.Properties.Resources.NEU_cau_truc_toa_nha_the_ky;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(562, 629);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(769, 31);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // panelDN
            // 
            this.panelDN.Controls.Add(this.btnDN);
            this.panelDN.Controls.Add(this.txtMatKhau);
            this.panelDN.Controls.Add(this.txtDangNhap);
            this.panelDN.Controls.Add(this.label1);
            this.panelDN.Controls.Add(this.label2);
            this.panelDN.Controls.Add(this.lblDN);
            this.panelDN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.panelDN.Location = new System.Drawing.Point(600, 225);
            this.panelDN.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.panelDN.MinimumSize = new System.Drawing.Size(1, 1);
            this.panelDN.Name = "panelDN";
            this.panelDN.Radius = 25;
            this.panelDN.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.panelDN.Size = new System.Drawing.Size(475, 400);
            this.panelDN.TabIndex = 28;
            this.panelDN.Text = null;
            this.panelDN.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDN
            // 
            this.btnDN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnDN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDN.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.btnDN.Location = new System.Drawing.Point(25, 260);
            this.btnDN.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnDN.Name = "btnDN";
            this.btnDN.Radius = 25;
            this.btnDN.Size = new System.Drawing.Size(330, 50);
            this.btnDN.TabIndex = 3;
            this.btnDN.Text = "ĐĂNG NHẬP";
            this.btnDN.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnDN.Click += new System.EventHandler(this.btnDN_Click_1);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtMatKhau.Location = new System.Drawing.Point(25, 195);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMatKhau.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Padding = new System.Windows.Forms.Padding(5);
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Radius = 25;
            this.txtMatKhau.ShowText = false;
            this.txtMatKhau.Size = new System.Drawing.Size(330, 45);
            this.txtMatKhau.TabIndex = 2;
            this.txtMatKhau.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMatKhau.Watermark = "Nhập mật khẩu";
            this.txtMatKhau.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMatKhau_KeyPress);
            // 
            // txtDangNhap
            // 
            this.txtDangNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.txtDangNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDangNhap.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtDangNhap.Location = new System.Drawing.Point(25, 110);
            this.txtDangNhap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDangNhap.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtDangNhap.Name = "txtDangNhap";
            this.txtDangNhap.Padding = new System.Windows.Forms.Padding(5);
            this.txtDangNhap.Radius = 25;
            this.txtDangNhap.ShowText = false;
            this.txtDangNhap.Size = new System.Drawing.Size(330, 45);
            this.txtDangNhap.TabIndex = 1;
            this.txtDangNhap.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDangNhap.Watermark = "Nhập tên đăng nhập";
            this.txtDangNhap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDangNhap_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label1.Location = new System.Drawing.Point(25, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mật khẩu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label2.Location = new System.Drawing.Point(25, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên đăng nhập";
            // 
            // lblDN
            // 
            this.lblDN.AutoSize = true;
            this.lblDN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.lblDN.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold);
            this.lblDN.ForeColor = System.Drawing.Color.Red;
            this.lblDN.Location = new System.Drawing.Point(25, 25);
            this.lblDN.Name = "lblDN";
            this.lblDN.Size = new System.Drawing.Size(196, 35);
            this.lblDN.TabIndex = 1;
            this.lblDN.Text = "ĐĂNG NHẬP";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1102, 629);
            this.Controls.Add(this.panelDN);
            this.Controls.Add(this.lblNEU);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(1120, 676);
            this.MinimumSize = new System.Drawing.Size(1120, 676);
            this.Name = "FrmLogin";
            this.Text = "Đăng nhập - Hệ thống quản lý đăng ký học phần";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelDN.ResumeLayout(false);
            this.panelDN.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblNEU;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Sunny.UI.UIPanel panelDN;
        private Sunny.UI.UIButton btnDN;
        private Sunny.UI.UITextBox txtMatKhau;
        private Sunny.UI.UITextBox txtDangNhap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDN;
    }
}