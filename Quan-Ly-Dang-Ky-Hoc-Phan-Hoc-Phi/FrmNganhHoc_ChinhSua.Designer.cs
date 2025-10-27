namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmNganhHoc_ChinhSua
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
            this.txtTenNganh = new Sunny.UI.UITextBox();
            this.txtMaNganh = new Sunny.UI.UITextBox();
            this.lblKhoaVien = new System.Windows.Forms.Label();
            this.lblTenNganh = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblMaNganh = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtId
            // 
            this.txtId.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtId.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(234, 111);
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
            this.btnHuy.Location = new System.Drawing.Point(412, 458);
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
            this.btnLuu.Location = new System.Drawing.Point(82, 458);
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
            this.cboKhoaVien.Location = new System.Drawing.Point(234, 277);
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
            // txtTenNganh
            // 
            this.txtTenNganh.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenNganh.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenNganh.Location = new System.Drawing.Point(234, 221);
            this.txtTenNganh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenNganh.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtTenNganh.Name = "txtTenNganh";
            this.txtTenNganh.Padding = new System.Windows.Forms.Padding(5);
            this.txtTenNganh.Radius = 30;
            this.txtTenNganh.ShowText = false;
            this.txtTenNganh.Size = new System.Drawing.Size(539, 46);
            this.txtTenNganh.TabIndex = 41;
            this.txtTenNganh.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTenNganh.Watermark = "Nhập tên ngành học";
            // 
            // txtMaNganh
            // 
            this.txtMaNganh.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaNganh.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaNganh.Location = new System.Drawing.Point(234, 165);
            this.txtMaNganh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaNganh.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtMaNganh.Name = "txtMaNganh";
            this.txtMaNganh.Padding = new System.Windows.Forms.Padding(5);
            this.txtMaNganh.Radius = 30;
            this.txtMaNganh.ShowText = false;
            this.txtMaNganh.Size = new System.Drawing.Size(539, 46);
            this.txtMaNganh.TabIndex = 40;
            this.txtMaNganh.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaNganh.Watermark = "Nhập mã ngành học";
            // 
            // lblKhoaVien
            // 
            this.lblKhoaVien.AutoSize = true;
            this.lblKhoaVien.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhoaVien.Location = new System.Drawing.Point(33, 290);
            this.lblKhoaVien.Name = "lblKhoaVien";
            this.lblKhoaVien.Size = new System.Drawing.Size(127, 33);
            this.lblKhoaVien.TabIndex = 34;
            this.lblKhoaVien.Text = "Khoa viện";
            // 
            // lblTenNganh
            // 
            this.lblTenNganh.AutoSize = true;
            this.lblTenNganh.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenNganh.Location = new System.Drawing.Point(33, 234);
            this.lblTenNganh.Name = "lblTenNganh";
            this.lblTenNganh.Size = new System.Drawing.Size(178, 33);
            this.lblTenNganh.TabIndex = 33;
            this.lblTenNganh.Text = "Tên ngành học";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(315, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 36);
            this.label1.TabIndex = 37;
            this.label1.Text = "Thêm ngành học";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(33, 124);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(43, 33);
            this.lblID.TabIndex = 38;
            this.lblID.Text = "ID";
            // 
            // lblMaNganh
            // 
            this.lblMaNganh.AutoSize = true;
            this.lblMaNganh.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaNganh.Location = new System.Drawing.Point(33, 178);
            this.lblMaNganh.Name = "lblMaNganh";
            this.lblMaNganh.Size = new System.Drawing.Size(173, 33);
            this.lblMaNganh.TabIndex = 39;
            this.lblMaNganh.Text = "Mã ngành học";
            // 
            // FrmNganhHoc_ChinhSua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(834, 541);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.cboKhoaVien);
            this.Controls.Add(this.txtTenNganh);
            this.Controls.Add(this.txtMaNganh);
            this.Controls.Add(this.lblKhoaVien);
            this.Controls.Add(this.lblTenNganh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblMaNganh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmNganhHoc_ChinhSua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmNganhHoc_ChinhSua";
            this.Load += new System.EventHandler(this.FrmNganhHoc_ChinhSua_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunny.UI.UITextBox txtId;
        private Sunny.UI.UIButton btnHuy;
        private Sunny.UI.UIButton btnLuu;
        private Sunny.UI.UIComboBox cboKhoaVien;
        private Sunny.UI.UITextBox txtTenNganh;
        private Sunny.UI.UITextBox txtMaNganh;
        private System.Windows.Forms.Label lblKhoaVien;
        private System.Windows.Forms.Label lblTenNganh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblMaNganh;
    }
}