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
            this.txtStudentID = new Sunny.UI.UITextBox();
            this.txtStudentCode = new Sunny.UI.UITextBox();
            this.txtFullName = new Sunny.UI.UITextBox();
            this.cboGender = new Sunny.UI.UIComboBox();
            this.dtpDateOfBirth = new Sunny.UI.UIDatePicker();
            this.txtEmail = new Sunny.UI.UITextBox();
            this.txtPhone = new Sunny.UI.UITextBox();
            this.txtAddress = new Sunny.UI.UITextBox();
            this.cboDeptID = new Sunny.UI.UIComboBox();
            this.numAdmissionYear = new Sunny.UI.UIIntegerUpDown();
            this.cboStatus = new Sunny.UI.UIComboBox();
            this.btnSave = new Sunny.UI.UIButton();
            this.btnCancel = new Sunny.UI.UIButton();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStudentID
            // 
            this.txtStudentID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtStudentID.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.txtStudentID.Location = new System.Drawing.Point(41, 58);
            this.txtStudentID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStudentID.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Padding = new System.Windows.Forms.Padding(5);
            this.txtStudentID.ReadOnly = true;
            this.txtStudentID.ShowText = false;
            this.txtStudentID.Size = new System.Drawing.Size(200, 40);
            this.txtStudentID.TabIndex = 0;
            this.txtStudentID.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtStudentID.Watermark = "ID (auto)";
            // 
            // txtStudentCode
            // 
            this.txtStudentCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtStudentCode.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.txtStudentCode.Location = new System.Drawing.Point(41, 108);
            this.txtStudentCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtStudentCode.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtStudentCode.Name = "txtStudentCode";
            this.txtStudentCode.Padding = new System.Windows.Forms.Padding(5);
            this.txtStudentCode.ShowText = false;
            this.txtStudentCode.Size = new System.Drawing.Size(400, 40);
            this.txtStudentCode.TabIndex = 1;
            this.txtStudentCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtStudentCode.Watermark = "Nhập mã sinh viên";
            // 
            // txtFullName
            // 
            this.txtFullName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFullName.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.txtFullName.Location = new System.Drawing.Point(41, 158);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFullName.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Padding = new System.Windows.Forms.Padding(5);
            this.txtFullName.ShowText = false;
            this.txtFullName.Size = new System.Drawing.Size(400, 40);
            this.txtFullName.TabIndex = 2;
            this.txtFullName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtFullName.Watermark = "Nhập họ tên";
            // 
            // cboGender
            // 
            this.cboGender.DataSource = null;
            this.cboGender.FillColor = System.Drawing.Color.White;
            this.cboGender.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.cboGender.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboGender.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.cboGender.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboGender.Location = new System.Drawing.Point(530, 208);
            this.cboGender.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboGender.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboGender.Name = "cboGender";
            this.cboGender.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboGender.Size = new System.Drawing.Size(200, 40);
            this.cboGender.SymbolSize = 24;
            this.cboGender.TabIndex = 3;
            this.cboGender.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboGender.Watermark = "Giới tính";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.FillColor = System.Drawing.Color.White;
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.dtpDateOfBirth.Location = new System.Drawing.Point(41, 208);
            this.dtpDateOfBirth.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDateOfBirth.MaxLength = 10;
            this.dtpDateOfBirth.MinimumSize = new System.Drawing.Size(63, 0);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.dtpDateOfBirth.Size = new System.Drawing.Size(200, 40);
            this.dtpDateOfBirth.SymbolDropDown = 61555;
            this.dtpDateOfBirth.SymbolNormal = 61555;
            this.dtpDateOfBirth.SymbolSize = 24;
            this.dtpDateOfBirth.TabIndex = 4;
            this.dtpDateOfBirth.Text = "2025-11-05";
            this.dtpDateOfBirth.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.dtpDateOfBirth.Value = new System.DateTime(2025, 11, 5, 22, 7, 34, 671);
            this.dtpDateOfBirth.Watermark = "";
            // 
            // txtEmail
            // 
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.txtEmail.Location = new System.Drawing.Point(41, 258);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEmail.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Padding = new System.Windows.Forms.Padding(5);
            this.txtEmail.ShowText = false;
            this.txtEmail.Size = new System.Drawing.Size(400, 40);
            this.txtEmail.TabIndex = 5;
            this.txtEmail.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtEmail.Watermark = "Nhập email";
            // 
            // txtPhone
            // 
            this.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPhone.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.txtPhone.Location = new System.Drawing.Point(41, 308);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPhone.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Padding = new System.Windows.Forms.Padding(5);
            this.txtPhone.ShowText = false;
            this.txtPhone.Size = new System.Drawing.Size(200, 40);
            this.txtPhone.TabIndex = 6;
            this.txtPhone.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPhone.Watermark = "SĐT";
            // 
            // txtAddress
            // 
            this.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAddress.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.txtAddress.Location = new System.Drawing.Point(41, 358);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAddress.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Padding = new System.Windows.Forms.Padding(5);
            this.txtAddress.ShowText = false;
            this.txtAddress.Size = new System.Drawing.Size(400, 40);
            this.txtAddress.TabIndex = 7;
            this.txtAddress.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtAddress.Watermark = "Địa chỉ";
            // 
            // cboDeptID
            // 
            this.cboDeptID.DataSource = null;
            this.cboDeptID.FillColor = System.Drawing.Color.White;
            this.cboDeptID.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.cboDeptID.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboDeptID.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboDeptID.Location = new System.Drawing.Point(530, 58);
            this.cboDeptID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboDeptID.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboDeptID.Name = "cboDeptID";
            this.cboDeptID.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboDeptID.Size = new System.Drawing.Size(200, 40);
            this.cboDeptID.SymbolSize = 24;
            this.cboDeptID.TabIndex = 8;
            this.cboDeptID.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboDeptID.Watermark = "Khoa";
            // 
            // numAdmissionYear
            // 
            this.numAdmissionYear.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.numAdmissionYear.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.numAdmissionYear.Location = new System.Drawing.Point(530, 108);
            this.numAdmissionYear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.numAdmissionYear.Maximum = 2099D;
            this.numAdmissionYear.Minimum = 2000D;
            this.numAdmissionYear.MinimumSize = new System.Drawing.Size(1, 16);
            this.numAdmissionYear.Name = "numAdmissionYear";
            this.numAdmissionYear.Padding = new System.Windows.Forms.Padding(5);
            this.numAdmissionYear.ShowText = false;
            this.numAdmissionYear.Size = new System.Drawing.Size(200, 40);
            this.numAdmissionYear.TabIndex = 9;
            this.numAdmissionYear.Text = "2023";
            this.numAdmissionYear.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.numAdmissionYear.Value = 2023;
            // 
            // cboStatus
            // 
            this.cboStatus.DataSource = null;
            this.cboStatus.FillColor = System.Drawing.Color.White;
            this.cboStatus.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.cboStatus.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboStatus.Items.AddRange(new object[] {
            "Đang học",
            "Tốt nghiệp",
            "Bảo lưu",
            "Thôi học"});
            this.cboStatus.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboStatus.Location = new System.Drawing.Point(530, 158);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboStatus.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboStatus.Size = new System.Drawing.Size(200, 40);
            this.cboStatus.SymbolSize = 24;
            this.cboStatus.TabIndex = 10;
            this.cboStatus.Text = "Đang học";
            this.cboStatus.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboStatus.Watermark = "";
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.btnSave.Location = new System.Drawing.Point(41, 406);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 50);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Lưu";
            this.btnSave.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSave.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.btnCancel.Location = new System.Drawing.Point(231, 406);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 50);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnCancel.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(850, 50);
            this.pnlTop.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(310, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "THÊM SINH VIÊN";
            // 
            // FrmSinhVien_ChinhSua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(850, 503);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.numAdmissionYear);
            this.Controls.Add(this.cboDeptID);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.dtpDateOfBirth);
            this.Controls.Add(this.cboGender);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtStudentCode);
            this.Controls.Add(this.txtStudentID);
            this.Font = new System.Drawing.Font("Times New Roman", 14F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSinhVien_ChinhSua";
            this.Text = "FrmStudent_Editor";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
            this.Load += new System.EventHandler(this.FrmSinhVien_ChinhSua_Load);


        }

        #endregion

        private Sunny.UI.UITextBox txtStudentID;
        private Sunny.UI.UITextBox txtStudentCode;
        private Sunny.UI.UITextBox txtFullName;
        private Sunny.UI.UIComboBox cboGender;
        private Sunny.UI.UIDatePicker dtpDateOfBirth;
        private Sunny.UI.UITextBox txtEmail;
        private Sunny.UI.UITextBox txtPhone;
        private Sunny.UI.UITextBox txtAddress;
        private Sunny.UI.UIComboBox cboDeptID;
        private Sunny.UI.UIIntegerUpDown numAdmissionYear;
        private Sunny.UI.UIComboBox cboStatus;
        private Sunny.UI.UIButton btnSave;
        private Sunny.UI.UIButton btnCancel;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label1;
    }
}