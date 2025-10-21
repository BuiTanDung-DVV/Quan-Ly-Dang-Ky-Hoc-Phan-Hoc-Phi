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
            
            // pnlTop
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(32, 96, 167);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Size = new System.Drawing.Size(1600, 60);
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Text = "ĐĂNG KÝ TÍN CHỈ";
            
            // pnlFilter
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlFilter.Controls.Add(this.cboHocKy);
            this.pnlFilter.Controls.Add(this.cboKhoa);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Controls.Add(this.btnTimKiem);
            this.pnlFilter.Controls.Add(this.btnLamMoi);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 60);
            this.pnlFilter.Size = new System.Drawing.Size(1600, 80);
            
            // cboHocKy
            this.cboHocKy.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboHocKy.Location = new System.Drawing.Point(20, 25);
            this.cboHocKy.Size = new System.Drawing.Size(200, 30);
            this.cboHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHocKy.SelectedIndexChanged += new System.EventHandler(this.cboHocKy_SelectedIndexChanged);
            
            // cboKhoa
            this.cboKhoa.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboKhoa.Location = new System.Drawing.Point(240, 25);
            this.cboKhoa.Size = new System.Drawing.Size(200, 30);
            this.cboKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoa.SelectedIndexChanged += new System.EventHandler(this.cboKhoa_SelectedIndexChanged);
            
            // txtTimKiem
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTimKiem.Location = new System.Drawing.Point(460, 25);
            this.txtTimKiem.Size = new System.Drawing.Size(250, 30);
            
            // btnTimKiem
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(730, 25);
            this.btnTimKiem.Size = new System.Drawing.Size(100, 35);
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            
            // btnLamMoi
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(149, 165, 166);
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(850, 25);
            this.btnLamMoi.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            
            // dgvLopHocPhan
            this.dgvLopHocPhan.AllowUserToAddRows = false;
            this.dgvLopHocPhan.AllowUserToDeleteRows = false;
            this.dgvLopHocPhan.BackgroundColor = System.Drawing.Color.White;
            this.dgvLopHocPhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLopHocPhan.Location = new System.Drawing.Point(20, 160);
            this.dgvLopHocPhan.MultiSelect = false;
            this.dgvLopHocPhan.ReadOnly = true;
            this.dgvLopHocPhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLopHocPhan.Size = new System.Drawing.Size(1000, 500);
            this.dgvLopHocPhan.SelectionChanged += new System.EventHandler(this.dgvLopHocPhan_SelectionChanged);
            
            // pnlRight
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRight.Controls.Add(this.gbThongTin);
            this.pnlRight.Controls.Add(this.gbDaDangKy);
            this.pnlRight.Controls.Add(this.btnDangKy);
            this.pnlRight.Controls.Add(this.btnHuyDangKy);
            this.pnlRight.Controls.Add(this.btnXemLichHoc);
            this.pnlRight.Location = new System.Drawing.Point(1040, 160);
            this.pnlRight.Size = new System.Drawing.Size(520, 700);
            
            // gbThongTin
            this.gbThongTin.Controls.Add(this.lblSinhVien);
            this.gbThongTin.Controls.Add(this.lblTongTinChi);
            this.gbThongTin.Controls.Add(this.lblTongHocPhi);
            this.gbThongTin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.gbThongTin.Location = new System.Drawing.Point(10, 10);
            this.gbThongTin.Size = new System.Drawing.Size(490, 120);
            this.gbThongTin.Text = "Thông tin sinh viên";
            
            // lblSinhVien
            this.lblSinhVien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSinhVien.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.lblSinhVien.Location = new System.Drawing.Point(10, 30);
            this.lblSinhVien.Size = new System.Drawing.Size(470, 25);
            this.lblSinhVien.Text = "Sinh viên: ";
            
            // lblTongTinChi
            this.lblTongTinChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongTinChi.Location = new System.Drawing.Point(10, 60);
            this.lblTongTinChi.Size = new System.Drawing.Size(200, 25);
            this.lblTongTinChi.Text = "Tổng tín chỉ đăng ký: 0";
            
            // lblTongHocPhi
            this.lblTongHocPhi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongHocPhi.ForeColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.lblTongHocPhi.Location = new System.Drawing.Point(10, 85);
            this.lblTongHocPhi.Size = new System.Drawing.Size(470, 25);
            this.lblTongHocPhi.Text = "Tổng học phí: 0 VNĐ";
            
            // gbDaDangKy
            this.gbDaDangKy.Controls.Add(this.dgvDaDangKy);
            this.gbDaDangKy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.gbDaDangKy.Location = new System.Drawing.Point(10, 140);
            this.gbDaDangKy.Size = new System.Drawing.Size(490, 400);
            this.gbDaDangKy.Text = "Lớp học phần đã đăng ký";
            
            // dgvDaDangKy
            this.dgvDaDangKy.AllowUserToAddRows = false;
            this.dgvDaDangKy.AllowUserToDeleteRows = false;
            this.dgvDaDangKy.BackgroundColor = System.Drawing.Color.White;
            this.dgvDaDangKy.Location = new System.Drawing.Point(10, 25);
            this.dgvDaDangKy.ReadOnly = true;
            this.dgvDaDangKy.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDaDangKy.Size = new System.Drawing.Size(470, 360);
            this.dgvDaDangKy.SelectionChanged += new System.EventHandler(this.dgvDaDangKy_SelectionChanged);
            
            // btnDangKy
            this.btnDangKy.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangKy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDangKy.ForeColor = System.Drawing.Color.White;
            this.btnDangKy.Location = new System.Drawing.Point(10, 560);
            this.btnDangKy.Size = new System.Drawing.Size(150, 45);
            this.btnDangKy.Text = "ĐĂNG KÝ";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            
            // btnHuyDangKy
            this.btnHuyDangKy.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
            this.btnHuyDangKy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyDangKy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnHuyDangKy.ForeColor = System.Drawing.Color.White;
            this.btnHuyDangKy.Location = new System.Drawing.Point(180, 560);
            this.btnHuyDangKy.Size = new System.Drawing.Size(150, 45);
            this.btnHuyDangKy.Text = "HỦY ĐĂNG KÝ";
            this.btnHuyDangKy.UseVisualStyleBackColor = false;
            this.btnHuyDangKy.Click += new System.EventHandler(this.btnHuyDangKy_Click);
            
            // btnXemLichHoc
            this.btnXemLichHoc.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.btnXemLichHoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemLichHoc.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXemLichHoc.ForeColor = System.Drawing.Color.White;
            this.btnXemLichHoc.Location = new System.Drawing.Point(350, 560);
            this.btnXemLichHoc.Size = new System.Drawing.Size(150, 45);
            this.btnXemLichHoc.Text = "XEM LỊCH HỌC";
            this.btnXemLichHoc.UseVisualStyleBackColor = false;
            this.btnXemLichHoc.Click += new System.EventHandler(this.btnXemLichHoc_Click);
            
            // FrmDangKi
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1600, 900);
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