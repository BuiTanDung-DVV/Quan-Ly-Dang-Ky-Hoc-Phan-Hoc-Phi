namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmDangKi
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.ComboBox cboHocKy;
        private System.Windows.Forms.ComboBox cboKhoa;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvLopHocPhan;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.Label lblSinhVien;
        private System.Windows.Forms.Label lblTongTinChi;
        private System.Windows.Forms.Label lblTongHocPhi;
        private System.Windows.Forms.GroupBox gbDaDangKy;
        private System.Windows.Forms.DataGridView dgvDaDangKy;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnHuyDangKy;
        private System.Windows.Forms.Button btnXemLichHoc;

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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.cboHocKy = new System.Windows.Forms.ComboBox();
            this.cboKhoa = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.dgvLopHocPhan = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.lblSinhVien = new System.Windows.Forms.Label();
            this.lblTongTinChi = new System.Windows.Forms.Label();
            this.lblTongHocPhi = new System.Windows.Forms.Label();
            this.gbDaDangKy = new System.Windows.Forms.GroupBox();
            this.dgvDaDangKy = new System.Windows.Forms.DataGridView();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.btnHuyDangKy = new System.Windows.Forms.Button();
            this.btnXemLichHoc = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHocPhan)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            this.gbDaDangKy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaDangKy)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(96)))), ((int)(((byte)(167)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 50);
            this.pnlTop.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(490, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(195, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐĂNG KÝ TÍN CHỈ";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlFilter.Controls.Add(this.cboHocKy);
            this.pnlFilter.Controls.Add(this.cboKhoa);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Controls.Add(this.btnTimKiem);
            this.pnlFilter.Controls.Add(this.btnLamMoi);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 50);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1200, 60);
            this.pnlFilter.TabIndex = 2;
            // 
            // cboHocKy
            // 
            this.cboHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHocKy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHocKy.Location = new System.Drawing.Point(15, 18);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(180, 25);
            this.cboHocKy.TabIndex = 0;
            this.cboHocKy.SelectedIndexChanged += new System.EventHandler(this.cboHocKy_SelectedIndexChanged);
            // 
            // cboKhoa
            // 
            this.cboKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKhoa.Location = new System.Drawing.Point(205, 18);
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Size = new System.Drawing.Size(180, 25);
            this.cboKhoa.TabIndex = 1;
            this.cboKhoa.SelectedIndexChanged += new System.EventHandler(this.cboKhoa_SelectedIndexChanged);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.Location = new System.Drawing.Point(395, 18);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(220, 25);
            this.txtTimKiem.TabIndex = 2;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(625, 18);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(80, 28);
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(715, 18);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 28);
            this.btnLamMoi.TabIndex = 4;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // dgvLopHocPhan
            // 
            this.dgvLopHocPhan.AllowUserToAddRows = false;
            this.dgvLopHocPhan.AllowUserToDeleteRows = false;
            this.dgvLopHocPhan.BackgroundColor = System.Drawing.Color.White;
            this.dgvLopHocPhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLopHocPhan.Location = new System.Drawing.Point(15, 125);
            this.dgvLopHocPhan.MultiSelect = false;
            this.dgvLopHocPhan.Name = "dgvLopHocPhan";
            this.dgvLopHocPhan.ReadOnly = true;
            this.dgvLopHocPhan.RowHeadersWidth = 51;
            this.dgvLopHocPhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLopHocPhan.Size = new System.Drawing.Size(750, 380);
            this.dgvLopHocPhan.TabIndex = 1;
            this.dgvLopHocPhan.SelectionChanged += new System.EventHandler(this.dgvLopHocPhan_SelectionChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRight.Controls.Add(this.gbThongTin);
            this.pnlRight.Controls.Add(this.gbDaDangKy);
            this.pnlRight.Controls.Add(this.btnDangKy);
            this.pnlRight.Controls.Add(this.btnHuyDangKy);
            this.pnlRight.Controls.Add(this.btnXemLichHoc);
            this.pnlRight.Location = new System.Drawing.Point(780, 125);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(405, 520);
            this.pnlRight.TabIndex = 0;
            // 
            // gbThongTin
            // 
            this.gbThongTin.Controls.Add(this.lblSinhVien);
            this.gbThongTin.Controls.Add(this.lblTongTinChi);
            this.gbThongTin.Controls.Add(this.lblTongHocPhi);
            this.gbThongTin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbThongTin.Location = new System.Drawing.Point(8, 8);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(385, 90);
            this.gbThongTin.TabIndex = 0;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "Thông tin sinh viên";
            // 
            // lblSinhVien
            // 
            this.lblSinhVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSinhVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblSinhVien.Location = new System.Drawing.Point(8, 22);
            this.lblSinhVien.Name = "lblSinhVien";
            this.lblSinhVien.Size = new System.Drawing.Size(370, 20);
            this.lblSinhVien.TabIndex = 0;
            this.lblSinhVien.Text = "Sinh viên: ";
            // 
            // lblTongTinChi
            // 
            this.lblTongTinChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTongTinChi.Location = new System.Drawing.Point(8, 45);
            this.lblTongTinChi.Name = "lblTongTinChi";
            this.lblTongTinChi.Size = new System.Drawing.Size(180, 20);
            this.lblTongTinChi.TabIndex = 1;
            this.lblTongTinChi.Text = "Tổng tín chỉ đăng ký: 0";
            // 
            // lblTongHocPhi
            // 
            this.lblTongHocPhi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongHocPhi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblTongHocPhi.Location = new System.Drawing.Point(8, 65);
            this.lblTongHocPhi.Name = "lblTongHocPhi";
            this.lblTongHocPhi.Size = new System.Drawing.Size(370, 20);
            this.lblTongHocPhi.TabIndex = 2;
            this.lblTongHocPhi.Text = "Tổng học phí: 0 VNĐ";
            // 
            // gbDaDangKy
            // 
            this.gbDaDangKy.Controls.Add(this.dgvDaDangKy);
            this.gbDaDangKy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbDaDangKy.Location = new System.Drawing.Point(8, 105);
            this.gbDaDangKy.Name = "gbDaDangKy";
            this.gbDaDangKy.Size = new System.Drawing.Size(385, 320);
            this.gbDaDangKy.TabIndex = 1;
            this.gbDaDangKy.TabStop = false;
            this.gbDaDangKy.Text = "Lớp học phần đã đăng ký";
            // 
            // dgvDaDangKy
            // 
            this.dgvDaDangKy.AllowUserToAddRows = false;
            this.dgvDaDangKy.AllowUserToDeleteRows = false;
            this.dgvDaDangKy.BackgroundColor = System.Drawing.Color.White;
            this.dgvDaDangKy.ColumnHeadersHeight = 29;
            this.dgvDaDangKy.Location = new System.Drawing.Point(8, 22);
            this.dgvDaDangKy.Name = "dgvDaDangKy";
            this.dgvDaDangKy.ReadOnly = true;
            this.dgvDaDangKy.RowHeadersWidth = 51;
            this.dgvDaDangKy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDaDangKy.Size = new System.Drawing.Size(370, 290);
            this.dgvDaDangKy.TabIndex = 0;
            this.dgvDaDangKy.SelectionChanged += new System.EventHandler(this.dgvDaDangKy_SelectionChanged);
            // 
            // btnDangKy
            // 
            this.btnDangKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.Location = new System.Drawing.Point(8, 440);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(120, 35);
            this.btnDangKy.TabIndex = 2;
            this.btnDangKy.Text = "ĐĂNG KÝ";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // btnHuyDangKy
            // 
            this.btnHuyDangKy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnHuyDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyDangKy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuyDangKy.ForeColor = System.Drawing.Color.White;
            this.btnHuyDangKy.Location = new System.Drawing.Point(140, 440);
            this.btnHuyDangKy.Name = "btnHuyDangKy";
            this.btnHuyDangKy.Size = new System.Drawing.Size(120, 35);
            this.btnHuyDangKy.TabIndex = 3;
            this.btnHuyDangKy.Text = "HỦY ĐĂNG KÝ";
            this.btnHuyDangKy.UseVisualStyleBackColor = false;
            this.btnHuyDangKy.Click += new System.EventHandler(this.btnHuyDangKy_Click);
            // 
            // btnXemLichHoc
            // 
            this.btnXemLichHoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnXemLichHoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemLichHoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXemLichHoc.ForeColor = System.Drawing.Color.White;
            this.btnXemLichHoc.Location = new System.Drawing.Point(272, 440);
            this.btnXemLichHoc.Name = "btnXemLichHoc";
            this.btnXemLichHoc.Size = new System.Drawing.Size(120, 35);
            this.btnXemLichHoc.TabIndex = 4;
            this.btnXemLichHoc.Text = "XEM LỊCH HỌC";
            this.btnXemLichHoc.UseVisualStyleBackColor = false;
            this.btnXemLichHoc.Click += new System.EventHandler(this.btnXemLichHoc_Click);
            // 
            // FrmDangKi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 660);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.dgvLopHocPhan);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDangKi";
            this.Text = "Đăng ký tín chỉ";
            this.Load += new System.EventHandler(this.FrmDangKi_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLopHocPhan)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbDaDangKy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDaDangKy)).EndInit();
            this.ResumeLayout(false);
        }
    }
}