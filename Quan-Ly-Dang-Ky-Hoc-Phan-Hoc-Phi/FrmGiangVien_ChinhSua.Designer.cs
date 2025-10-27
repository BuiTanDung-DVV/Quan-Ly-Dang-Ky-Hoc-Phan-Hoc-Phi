namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmGiangVien_ChinhSua
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
            this.txtId = new Sunny.UI.UITextBox();
            this.btnHuy = new Sunny.UI.UIButton();
            this.btnLuu = new Sunny.UI.UIButton();
            this.cboKhoaVien = new Sunny.UI.UIComboBox();
            this.txtEmail = new Sunny.UI.UITextBox();
            this.txtTenGV = new Sunny.UI.UITextBox();
            this.txtMaGV = new Sunny.UI.UITextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblKhoaVien = new System.Windows.Forms.Label();
            this.lblTenGV = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblMaGV = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtId
            // 
            this.txtId.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtId.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(242, 101);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtId.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtId.Name = "txtId";
            this.txtId.Padding = new System.Windows.Forms.Padding(5);
            this.txtId.Radius = 30;
            this.txtId.ShowText = false;
            this.txtId.Size = new System.Drawing.Size(539, 46);
            this.txtId.TabIndex = 47;
            this.txtId.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtId.Watermark = "ID";
            // 
            // btnHuy
            // 
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Location = new System.Drawing.Point(419, 464);
            this.btnHuy.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Radius = 30;
            this.btnHuy.Size = new System.Drawing.Size(239, 49);
            this.btnHuy.TabIndex = 46;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(74, 464);
            this.btnLuu.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Radius = 30;
            this.btnLuu.Size = new System.Drawing.Size(239, 49);
            this.btnLuu.TabIndex = 45;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // cboKhoaVien
            // 
            this.cboKhoaVien.DataSource = null;
            this.cboKhoaVien.DisplayMember = "Chọn khoa viện";
            this.cboKhoaVien.FillColor = System.Drawing.Color.White;
            this.cboKhoaVien.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoaVien.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboKhoaVien.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboKhoaVien.Location = new System.Drawing.Point(242, 323);
            this.cboKhoaVien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboKhoaVien.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboKhoaVien.Name = "cboKhoaVien";
            this.cboKhoaVien.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboKhoaVien.Radius = 30;
            this.cboKhoaVien.Size = new System.Drawing.Size(539, 46);
            this.cboKhoaVien.SymbolSize = 24;
            this.cboKhoaVien.TabIndex = 44;
            this.cboKhoaVien.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboKhoaVien.Watermark = "Chọn khoa viện";
            // 
            // txtEmail
            // 
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(242, 267);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEmail.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Padding = new System.Windows.Forms.Padding(5);
            this.txtEmail.Radius = 30;
            this.txtEmail.ShowText = false;
            this.txtEmail.Size = new System.Drawing.Size(539, 46);
            this.txtEmail.TabIndex = 42;
            this.txtEmail.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtEmail.Watermark = "Nhập email";
            // 
            // txtTenGV
            // 
            this.txtTenGV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenGV.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenGV.Location = new System.Drawing.Point(242, 211);
            this.txtTenGV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenGV.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtTenGV.Name = "txtTenGV";
            this.txtTenGV.Padding = new System.Windows.Forms.Padding(5);
            this.txtTenGV.Radius = 30;
            this.txtTenGV.ShowText = false;
            this.txtTenGV.Size = new System.Drawing.Size(539, 46);
            this.txtTenGV.TabIndex = 41;
            this.txtTenGV.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTenGV.Watermark = "Nhập tên giảng viên";
            // 
            // txtMaGV
            // 
            this.txtMaGV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaGV.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaGV.Location = new System.Drawing.Point(242, 155);
            this.txtMaGV.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaGV.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtMaGV.Name = "txtMaGV";
            this.txtMaGV.Padding = new System.Windows.Forms.Padding(5);
            this.txtMaGV.Radius = 30;
            this.txtMaGV.ShowText = false;
            this.txtMaGV.Size = new System.Drawing.Size(539, 46);
            this.txtMaGV.TabIndex = 40;
            this.txtMaGV.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaGV.Watermark = "Nhập mã giảng viên";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(41, 280);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(80, 33);
            this.lblEmail.TabIndex = 36;
            this.lblEmail.Text = "Email";
            // 
            // lblKhoaVien
            // 
            this.lblKhoaVien.AutoSize = true;
            this.lblKhoaVien.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhoaVien.Location = new System.Drawing.Point(41, 336);
            this.lblKhoaVien.Name = "lblKhoaVien";
            this.lblKhoaVien.Size = new System.Drawing.Size(127, 33);
            this.lblKhoaVien.TabIndex = 34;
            this.lblKhoaVien.Text = "Khoa viện";
            // 
            // lblTenGV
            // 
            this.lblTenGV.AutoSize = true;
            this.lblTenGV.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenGV.Location = new System.Drawing.Point(41, 224);
            this.lblTenGV.Name = "lblTenGV";
            this.lblTenGV.Size = new System.Drawing.Size(159, 33);
            this.lblTenGV.TabIndex = 33;
            this.lblTenGV.Text = "Tên môn học";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(323, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 36);
            this.label1.TabIndex = 37;
            this.label1.Text = "Thêm Mới Giảng Viên";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(41, 114);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(43, 33);
            this.lblID.TabIndex = 38;
            this.lblID.Text = "ID";
            // 
            // lblMaGV
            // 
            this.lblMaGV.AutoSize = true;
            this.lblMaGV.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaGV.Location = new System.Drawing.Point(41, 168);
            this.lblMaGV.Name = "lblMaGV";
            this.lblMaGV.Size = new System.Drawing.Size(172, 33);
            this.lblMaGV.TabIndex = 39;
            this.lblMaGV.Text = "Mã giảng viên";
            // 
            // FrmGiangVien_ChinhSua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(834, 541);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.cboKhoaVien);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtTenGV);
            this.Controls.Add(this.txtMaGV);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblKhoaVien);
            this.Controls.Add(this.lblTenGV);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblMaGV);
            this.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmGiangVien_ChinhSua";
            this.Text = "FrmGiangVien_ChinhSua";
            this.Load += new System.EventHandler(this.FrmGiangVien_ChinhSua_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunny.UI.UITextBox txtId;
        private Sunny.UI.UIButton btnHuy;
        private Sunny.UI.UIButton btnLuu;
        private Sunny.UI.UIComboBox cboKhoaVien;
        private Sunny.UI.UITextBox txtEmail;
        private Sunny.UI.UITextBox txtTenGV;
        private Sunny.UI.UITextBox txtMaGV;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblKhoaVien;
        private System.Windows.Forms.Label lblTenGV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblMaGV;
    }
}