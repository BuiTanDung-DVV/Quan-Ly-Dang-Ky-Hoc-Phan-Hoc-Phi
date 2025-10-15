namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnDN = new System.Windows.Forms.Button();
            this.txtMK = new System.Windows.Forms.TextBox();
            this.txtDN = new System.Windows.Forms.TextBox();
            this.lblMK = new System.Windows.Forms.Label();
            this.lblDN = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Location = new System.Drawing.Point(1072, 533);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(256, 103);
            this.btnThoat.TabIndex = 17;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnDN
            // 
            this.btnDN.Font = new System.Drawing.Font("Times New Roman", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDN.Location = new System.Drawing.Point(652, 533);
            this.btnDN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDN.Name = "btnDN";
            this.btnDN.Size = new System.Drawing.Size(256, 103);
            this.btnDN.TabIndex = 16;
            this.btnDN.Text = "Đăng Nhập";
            this.btnDN.UseVisualStyleBackColor = true;
            // 
            // txtMK
            // 
            this.txtMK.Font = new System.Drawing.Font("Times New Roman", 14.26415F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMK.Location = new System.Drawing.Point(774, 331);
            this.txtMK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMK.Name = "txtMK";
            this.txtMK.Size = new System.Drawing.Size(604, 40);
            this.txtMK.TabIndex = 15;
            // 
            // txtDN
            // 
            this.txtDN.Font = new System.Drawing.Font("Times New Roman", 14.26415F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDN.Location = new System.Drawing.Point(774, 243);
            this.txtDN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDN.Name = "txtDN";
            this.txtDN.Size = new System.Drawing.Size(604, 40);
            this.txtDN.TabIndex = 14;
            // 
            // lblMK
            // 
            this.lblMK.AutoSize = true;
            this.lblMK.Font = new System.Drawing.Font("Times New Roman", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMK.Location = new System.Drawing.Point(562, 344);
            this.lblMK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMK.Name = "lblMK";
            this.lblMK.Size = new System.Drawing.Size(144, 32);
            this.lblMK.TabIndex = 19;
            this.lblMK.Text = "Mật khẩu:";
            // 
            // lblDN
            // 
            this.lblDN.AutoSize = true;
            this.lblDN.Font = new System.Drawing.Font("Times New Roman", 14.26415F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDN.Location = new System.Drawing.Point(526, 255);
            this.lblDN.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDN.Name = "lblDN";
            this.lblDN.Size = new System.Drawing.Size(208, 32);
            this.lblDN.TabIndex = 20;
            this.lblDN.Text = "Tên đăng nhập:";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(782, 126);
            this.lbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(459, 51);
            this.lbl1.TabIndex = 18;
            this.lbl1.Text = "ĐĂNG NHẬP HỆ THỐNG";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(2080, 1064);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDN);
            this.Controls.Add(this.txtMK);
            this.Controls.Add(this.txtDN);
            this.Controls.Add(this.lblMK);
            this.Controls.Add(this.lblDN);
            this.Controls.Add(this.lbl1);
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "z";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnDN;
        private System.Windows.Forms.TextBox txtMK;
        private System.Windows.Forms.TextBox txtDN;
        private System.Windows.Forms.Label lblMK;
        private System.Windows.Forms.Label lblDN;
        private System.Windows.Forms.Label lbl1;
    }
}