namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmMonHoc_ChinhSua
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
            this.lblSoTin = new System.Windows.Forms.Label();
            this.lblHocPhi = new System.Windows.Forms.Label();
            this.lblKhoaVien = new System.Windows.Forms.Label();
            this.lblTenMon = new System.Windows.Forms.Label();
            this.lblMaMon = new System.Windows.Forms.Label();
            this.txtMaMon = new Sunny.UI.UITextBox();
            this.txtTenMon = new Sunny.UI.UITextBox();
            this.txtSoTC = new Sunny.UI.UITextBox();
            this.txtHocPhi = new Sunny.UI.UITextBox();
            this.cboKhoaVien = new Sunny.UI.UIComboBox();
            this.btnLuu = new Sunny.UI.UIButton();
            this.btnHuy = new Sunny.UI.UIButton();
            this.lblID = new System.Windows.Forms.Label();
            this.txtId = new Sunny.UI.UITextBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSoTin
            // 
            this.lblSoTin.AutoSize = true;
            this.lblSoTin.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoTin.Location = new System.Drawing.Point(12, 280);
            this.lblSoTin.Name = "lblSoTin";
            this.lblSoTin.Size = new System.Drawing.Size(85, 21);
            this.lblSoTin.TabIndex = 23;
            this.lblSoTin.Text = "Số tín chỉ";
            // 
            // lblHocPhi
            // 
            this.lblHocPhi.AutoSize = true;
            this.lblHocPhi.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHocPhi.Location = new System.Drawing.Point(12, 336);
            this.lblHocPhi.Name = "lblHocPhi";
            this.lblHocPhi.Size = new System.Drawing.Size(72, 21);
            this.lblHocPhi.TabIndex = 22;
            this.lblHocPhi.Text = "Học phí";
            // 
            // lblKhoaVien
            // 
            this.lblKhoaVien.AutoSize = true;
            this.lblKhoaVien.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhoaVien.Location = new System.Drawing.Point(12, 392);
            this.lblKhoaVien.Name = "lblKhoaVien";
            this.lblKhoaVien.Size = new System.Drawing.Size(85, 21);
            this.lblKhoaVien.TabIndex = 21;
            this.lblKhoaVien.Text = "Khoa viện";
            // 
            // lblTenMon
            // 
            this.lblTenMon.AutoSize = true;
            this.lblTenMon.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenMon.Location = new System.Drawing.Point(12, 224);
            this.lblTenMon.Name = "lblTenMon";
            this.lblTenMon.Size = new System.Drawing.Size(110, 21);
            this.lblTenMon.TabIndex = 20;
            this.lblTenMon.Text = "Tên môn học";
            // 
            // lblMaMon
            // 
            this.lblMaMon.AutoSize = true;
            this.lblMaMon.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaMon.Location = new System.Drawing.Point(12, 168);
            this.lblMaMon.Name = "lblMaMon";
            this.lblMaMon.Size = new System.Drawing.Size(105, 21);
            this.lblMaMon.TabIndex = 24;
            this.lblMaMon.Text = "Mã môn học";
            // 
            // txtMaMon
            // 
            this.txtMaMon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaMon.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaMon.Location = new System.Drawing.Point(213, 155);
            this.txtMaMon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaMon.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtMaMon.Name = "txtMaMon";
            this.txtMaMon.Padding = new System.Windows.Forms.Padding(5);
            this.txtMaMon.Radius = 30;
            this.txtMaMon.ShowText = false;
            this.txtMaMon.Size = new System.Drawing.Size(539, 46);
            this.txtMaMon.TabIndex = 25;
            this.txtMaMon.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaMon.Watermark = "Nhập mã môn học";
            // 
            // txtTenMon
            // 
            this.txtTenMon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenMon.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenMon.Location = new System.Drawing.Point(213, 211);
            this.txtTenMon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenMon.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtTenMon.Name = "txtTenMon";
            this.txtTenMon.Padding = new System.Windows.Forms.Padding(5);
            this.txtTenMon.Radius = 30;
            this.txtTenMon.ShowText = false;
            this.txtTenMon.Size = new System.Drawing.Size(539, 46);
            this.txtTenMon.TabIndex = 26;
            this.txtTenMon.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTenMon.Watermark = "Nhập tên môn học";
            // 
            // txtSoTC
            // 
            this.txtSoTC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoTC.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoTC.Location = new System.Drawing.Point(213, 267);
            this.txtSoTC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSoTC.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtSoTC.Name = "txtSoTC";
            this.txtSoTC.Padding = new System.Windows.Forms.Padding(5);
            this.txtSoTC.Radius = 30;
            this.txtSoTC.ShowText = false;
            this.txtSoTC.Size = new System.Drawing.Size(539, 46);
            this.txtSoTC.TabIndex = 27;
            this.txtSoTC.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtSoTC.Watermark = "Nhập số tín chỉ";
            // 
            // txtHocPhi
            // 
            this.txtHocPhi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHocPhi.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHocPhi.Location = new System.Drawing.Point(213, 323);
            this.txtHocPhi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtHocPhi.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtHocPhi.Name = "txtHocPhi";
            this.txtHocPhi.Padding = new System.Windows.Forms.Padding(5);
            this.txtHocPhi.Radius = 30;
            this.txtHocPhi.ShowText = false;
            this.txtHocPhi.Size = new System.Drawing.Size(539, 46);
            this.txtHocPhi.TabIndex = 28;
            this.txtHocPhi.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtHocPhi.Watermark = "Nhập học phí";
            // 
            // cboKhoaVien
            // 
            this.cboKhoaVien.DataSource = null;
            this.cboKhoaVien.DisplayMember = "Chọn khoa viện";
            this.cboKhoaVien.FillColor = System.Drawing.Color.White;
            this.cboKhoaVien.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoaVien.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboKhoaVien.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboKhoaVien.Location = new System.Drawing.Point(213, 379);
            this.cboKhoaVien.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboKhoaVien.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboKhoaVien.Name = "cboKhoaVien";
            this.cboKhoaVien.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboKhoaVien.Radius = 30;
            this.cboKhoaVien.Size = new System.Drawing.Size(539, 46);
            this.cboKhoaVien.SymbolSize = 24;
            this.cboKhoaVien.TabIndex = 29;
            this.cboKhoaVien.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboKhoaVien.Watermark = "Chọn khoa viện";
            // 
            // btnLuu
            // 
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(45, 464);
            this.btnLuu.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Radius = 30;
            this.btnLuu.Size = new System.Drawing.Size(239, 49);
            this.btnLuu.TabIndex = 30;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Location = new System.Drawing.Point(390, 464);
            this.btnHuy.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Radius = 30;
            this.btnHuy.Size = new System.Drawing.Size(239, 49);
            this.btnHuy.TabIndex = 31;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(12, 114);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(29, 21);
            this.lblID.TabIndex = 24;
            this.lblID.Text = "ID";
            // 
            // txtId
            // 
            this.txtId.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtId.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(213, 101);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtId.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtId.Name = "txtId";
            this.txtId.Padding = new System.Windows.Forms.Padding(5);
            this.txtId.Radius = 30;
            this.txtId.ShowText = false;
            this.txtId.Size = new System.Drawing.Size(539, 46);
            this.txtId.TabIndex = 32;
            this.txtId.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtId.Watermark = "ID";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(834, 50);
            this.pnlTop.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(304, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "THÊM MÔN HỌC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMonHoc_ChinhSua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(834, 541);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.cboKhoaVien);
            this.Controls.Add(this.txtHocPhi);
            this.Controls.Add(this.txtSoTC);
            this.Controls.Add(this.txtTenMon);
            this.Controls.Add(this.txtMaMon);
            this.Controls.Add(this.lblSoTin);
            this.Controls.Add(this.lblHocPhi);
            this.Controls.Add(this.lblKhoaVien);
            this.Controls.Add(this.lblTenMon);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblMaMon);
            this.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FrmMonHoc_ChinhSua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMonHoc_ChinhSua";
            this.Load += new System.EventHandler(this.FrmMonHoc_ChinhSua_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSoTin;
        private System.Windows.Forms.Label lblHocPhi;
        private System.Windows.Forms.Label lblKhoaVien;
        private System.Windows.Forms.Label lblTenMon;
        private System.Windows.Forms.Label lblMaMon;
        private Sunny.UI.UITextBox txtMaMon;
        private Sunny.UI.UITextBox txtTenMon;
        private Sunny.UI.UITextBox txtSoTC;
        private Sunny.UI.UITextBox txtHocPhi;
        private Sunny.UI.UIComboBox cboKhoaVien;
        private Sunny.UI.UIButton btnLuu;
        private Sunny.UI.UIButton btnHuy;
        private System.Windows.Forms.Label lblID;
        private Sunny.UI.UITextBox txtId;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label1;
    }
}