namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmSinhVien_ChinhSua
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
            this.isCurrent = new Sunny.UI.UIRadioButtonGroup();
            this.optKetThuc = new Sunny.UI.UIRadioButton();
            this.optHienHanh = new Sunny.UI.UIRadioButton();
            this.txtDate2 = new Sunny.UI.UIDatePicker();
            this.txtDate1 = new Sunny.UI.UIDatePicker();
            this.txtId = new Sunny.UI.UITextBox();
            this.btnHuy = new Sunny.UI.UIButton();
            this.btnLuu = new Sunny.UI.UIButton();
            this.txtTenHK = new Sunny.UI.UITextBox();
            this.txtMaHK = new Sunny.UI.UITextBox();
            this.lblDate1 = new System.Windows.Forms.Label();
            this.lblDate2 = new System.Windows.Forms.Label();
            this.lblTenHK = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblMaHK = new System.Windows.Forms.Label();
            this.isCurrent.SuspendLayout();
            this.SuspendLayout();
            // 
            // isCurrent
            // 
            this.isCurrent.Controls.Add(this.optKetThuc);
            this.isCurrent.Controls.Add(this.optHienHanh);
            this.isCurrent.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isCurrent.Location = new System.Drawing.Point(803, 131);
            this.isCurrent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.isCurrent.MinimumSize = new System.Drawing.Size(1, 1);
            this.isCurrent.Name = "isCurrent";
            this.isCurrent.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.isCurrent.Size = new System.Drawing.Size(213, 131);
            this.isCurrent.TabIndex = 64;
            this.isCurrent.Text = "Trạng thái";
            this.isCurrent.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // optKetThuc
            // 
            this.optKetThuc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.optKetThuc.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optKetThuc.Location = new System.Drawing.Point(35, 89);
            this.optKetThuc.MinimumSize = new System.Drawing.Size(1, 1);
            this.optKetThuc.Name = "optKetThuc";
            this.optKetThuc.Size = new System.Drawing.Size(158, 33);
            this.optKetThuc.TabIndex = 1;
            this.optKetThuc.Text = "Kết thúc";
            // 
            // optHienHanh
            // 
            this.optHienHanh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.optHienHanh.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optHienHanh.Location = new System.Drawing.Point(35, 47);
            this.optHienHanh.MinimumSize = new System.Drawing.Size(1, 1);
            this.optHienHanh.Name = "optHienHanh";
            this.optHienHanh.Size = new System.Drawing.Size(158, 36);
            this.optHienHanh.TabIndex = 0;
            this.optHienHanh.Text = "Hiện hành";
            // 
            // txtDate2
            // 
            this.txtDate2.FillColor = System.Drawing.Color.White;
            this.txtDate2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtDate2.Location = new System.Drawing.Point(231, 352);
            this.txtDate2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDate2.MaxLength = 10;
            this.txtDate2.MinimumSize = new System.Drawing.Size(63, 0);
            this.txtDate2.Name = "txtDate2";
            this.txtDate2.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.txtDate2.Radius = 29;
            this.txtDate2.Size = new System.Drawing.Size(539, 46);
            this.txtDate2.SymbolDropDown = 61555;
            this.txtDate2.SymbolNormal = 61555;
            this.txtDate2.SymbolSize = 24;
            this.txtDate2.TabIndex = 63;
            this.txtDate2.Text = "2025-10-27";
            this.txtDate2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDate2.Value = new System.DateTime(2025, 10, 27, 21, 48, 56, 724);
            this.txtDate2.Watermark = "";
            // 
            // txtDate1
            // 
            this.txtDate1.FillColor = System.Drawing.Color.White;
            this.txtDate1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDate1.Location = new System.Drawing.Point(231, 296);
            this.txtDate1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDate1.MaxLength = 10;
            this.txtDate1.MinimumSize = new System.Drawing.Size(63, 0);
            this.txtDate1.Name = "txtDate1";
            this.txtDate1.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.txtDate1.Radius = 30;
            this.txtDate1.Size = new System.Drawing.Size(539, 46);
            this.txtDate1.SymbolDropDown = 61555;
            this.txtDate1.SymbolNormal = 61555;
            this.txtDate1.SymbolSize = 24;
            this.txtDate1.TabIndex = 62;
            this.txtDate1.Text = "2025-10-27";
            this.txtDate1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtDate1.Value = new System.DateTime(2025, 10, 27, 21, 47, 14, 637);
            this.txtDate1.Watermark = "";
            // 
            // txtId
            // 
            this.txtId.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtId.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.Location = new System.Drawing.Point(231, 130);
            this.txtId.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtId.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtId.Name = "txtId";
            this.txtId.Padding = new System.Windows.Forms.Padding(5);
            this.txtId.Radius = 30;
            this.txtId.ShowText = false;
            this.txtId.Size = new System.Drawing.Size(539, 46);
            this.txtId.TabIndex = 61;
            this.txtId.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtId.Watermark = "ID";
            // 
            // btnHuy
            // 
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Location = new System.Drawing.Point(531, 453);
            this.btnHuy.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Radius = 30;
            this.btnHuy.Size = new System.Drawing.Size(239, 49);
            this.btnHuy.TabIndex = 60;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // btnLuu
            // 
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(145, 453);
            this.btnLuu.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Radius = 30;
            this.btnLuu.Size = new System.Drawing.Size(239, 49);
            this.btnLuu.TabIndex = 59;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // txtTenHK
            // 
            this.txtTenHK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenHK.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenHK.Location = new System.Drawing.Point(231, 240);
            this.txtTenHK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenHK.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtTenHK.Name = "txtTenHK";
            this.txtTenHK.Padding = new System.Windows.Forms.Padding(5);
            this.txtTenHK.Radius = 30;
            this.txtTenHK.ShowText = false;
            this.txtTenHK.Size = new System.Drawing.Size(539, 46);
            this.txtTenHK.TabIndex = 58;
            this.txtTenHK.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTenHK.Watermark = "Nhập tên môn học";
            // 
            // txtMaHK
            // 
            this.txtMaHK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaHK.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaHK.Location = new System.Drawing.Point(231, 184);
            this.txtMaHK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaHK.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtMaHK.Name = "txtMaHK";
            this.txtMaHK.Padding = new System.Windows.Forms.Padding(5);
            this.txtMaHK.Radius = 30;
            this.txtMaHK.ShowText = false;
            this.txtMaHK.Size = new System.Drawing.Size(539, 46);
            this.txtMaHK.TabIndex = 57;
            this.txtMaHK.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtMaHK.Watermark = "Nhập mã kì học";
            // 
            // lblDate1
            // 
            this.lblDate1.AutoSize = true;
            this.lblDate1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate1.Location = new System.Drawing.Point(30, 309);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(101, 33);
            this.lblDate1.TabIndex = 53;
            this.lblDate1.Text = "Bắt đầu";
            // 
            // lblDate2
            // 
            this.lblDate2.AutoSize = true;
            this.lblDate2.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate2.Location = new System.Drawing.Point(30, 365);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(109, 33);
            this.lblDate2.TabIndex = 52;
            this.lblDate2.Text = "Kết thúc";
            // 
            // lblTenHK
            // 
            this.lblTenHK.AutoSize = true;
            this.lblTenHK.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenHK.Location = new System.Drawing.Point(30, 253);
            this.lblTenHK.Name = "lblTenHK";
            this.lblTenHK.Size = new System.Drawing.Size(133, 33);
            this.lblTenHK.TabIndex = 51;
            this.lblTenHK.Text = "Tên kì học";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(420, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 36);
            this.label1.TabIndex = 54;
            this.label1.Text = "Thêm Sinh Viên";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(30, 143);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(43, 33);
            this.lblID.TabIndex = 55;
            this.lblID.Text = "ID";
            // 
            // lblMaHK
            // 
            this.lblMaHK.AutoSize = true;
            this.lblMaHK.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaHK.Location = new System.Drawing.Point(30, 197);
            this.lblMaHK.Name = "lblMaHK";
            this.lblMaHK.Size = new System.Drawing.Size(128, 33);
            this.lblMaHK.TabIndex = 56;
            this.lblMaHK.Text = "Mã kì học";
            // 
            // FrmSinhVien_ChinhSua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1063, 516);
            this.Controls.Add(this.isCurrent);
            this.Controls.Add(this.txtDate2);
            this.Controls.Add(this.txtDate1);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtTenHK);
            this.Controls.Add(this.txtMaHK);
            this.Controls.Add(this.lblDate1);
            this.Controls.Add(this.lblDate2);
            this.Controls.Add(this.lblTenHK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblMaHK);
            this.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FrmSinhVien_ChinhSua";
            this.Text = "FrmSinhVien_ChinhSua";
            this.isCurrent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunny.UI.UIRadioButtonGroup isCurrent;
        private Sunny.UI.UIRadioButton optKetThuc;
        private Sunny.UI.UIRadioButton optHienHanh;
        private Sunny.UI.UIDatePicker txtDate2;
        private Sunny.UI.UIDatePicker txtDate1;
        private Sunny.UI.UITextBox txtId;
        private Sunny.UI.UIButton btnHuy;
        private Sunny.UI.UIButton btnLuu;
        private Sunny.UI.UITextBox txtTenHK;
        private Sunny.UI.UITextBox txtMaHK;
        private System.Windows.Forms.Label lblDate1;
        private System.Windows.Forms.Label lblDate2;
        private System.Windows.Forms.Label lblTenHK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblMaHK;
    }
}