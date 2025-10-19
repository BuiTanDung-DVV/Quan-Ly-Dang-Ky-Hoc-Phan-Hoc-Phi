using System;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmMain : RoundedForm
    {
        FrmSinhVien SinhVien;
        FrmKhoaVien KhoaVien;
        FrmMonHoc MonHoc;
        FrmNganhHoc NganhHoc;
        FrmHocKi HocKi;
        FrmGiangVien GiangVien;
        FrmLopHocPhan LopHocPhan;
        FrmDangKi DangKi;
        FrmThanhToan ThanhToan;
        FrmHoaDon HoaDon;
        FrmLichSuDK LichSuDK;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Bạn cần đăng nhập để sử dụng hệ thống!", "Thông báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                FrmLogin loginForm = new FrmLogin();
                loginForm.ShowDialog();
                return;
            }

            // Setup UI based on user role
            SetupUIByRole();
            ShowWelcomeMessage();
        }

        private void SetupUIByRole()
        {
            // Example: Hide/Show menu items based on role
            if (UserSession.IsStudent())
            {
                // Show only student-related functions
                // Hide admin menus, etc.
            }
            else if (UserSession.IsLecturer())
            {
                // Show lecturer-related functions
            }
            else if (UserSession.IsAdmin())
            {
                // Show all administrative functions
            }
        }

        private void ShowWelcomeMessage()
        {
            string welcomeMessage = $"Xin chào, {UserSession.Username}!";
            
            if (UserSession.IsStudent())
                welcomeMessage += $" (Sinh viên - ID: {UserSession.LinkedStudentID})";
            else if (UserSession.IsLecturer())
                welcomeMessage += $" (Giảng viên - ID: {UserSession.LinkedLecturerID})";
            else if (UserSession.IsAdmin())
                welcomeMessage += " (Quản trị viên)";

            // Display in a label or status bar
            // lblWelcome.Text = welcomeMessage;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", 
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                UserSession.EndSession();
                this.Hide();
                FrmLogin loginForm = new FrmLogin();
                loginForm.Show();
            }
        }

        private void btnQLSV_Click(object sender, EventArgs e)
        {
            btnQLSV.Selected = true;
            if (SinhVien == null || SinhVien.IsDisposed)
            {
                SinhVien = new FrmSinhVien();
                SinhVien.MdiParent = this;
                SinhVien.Dock = DockStyle.Fill;
                SinhVien.Show();
            }
            else
            {
                SinhVien.Activate();
            }
        }

        private void btnQLKV_Click(object sender, EventArgs e)
        {
            btnQLKV.Selected = true;
            if (KhoaVien == null || KhoaVien.IsDisposed)
            {
                KhoaVien = new FrmKhoaVien();
                KhoaVien.MdiParent = this;
                KhoaVien.Dock = DockStyle.Fill;
                KhoaVien.Show();
            }
            else
            {
                KhoaVien.Activate();
            }
        }

        private void btnQLMH_Click(object sender, EventArgs e)
        {
            btnQLMH.Selected = true;
            if (MonHoc == null || MonHoc.IsDisposed)
            {
                MonHoc = new FrmMonHoc();
                MonHoc.MdiParent = this;
                MonHoc.Dock = DockStyle.Fill;
                MonHoc.Show();
            }
            else
            {
                MonHoc.Activate();
            }
        }

        private void btnQLNH_Click(object sender, EventArgs e)
        {
            btnQLNH.Selected = true;
            if (NganhHoc == null || NganhHoc.IsDisposed)
            {
                NganhHoc = new FrmNganhHoc();
                NganhHoc.MdiParent = this;
                NganhHoc.Dock = DockStyle.Fill;
                NganhHoc.Show();
            }
            else
            {
                NganhHoc.Activate();
            }
        }

        private void btnQLHK_Click(object sender, EventArgs e)
        {
            btnQLHK.Selected = true;
            if (HocKi == null || HocKi.IsDisposed)
            {
                HocKi = new FrmHocKi();
                HocKi.MdiParent = this;
                HocKi.Dock = DockStyle.Fill;
                HocKi.Show();
            }
            else
            {
                HocKi.Activate();
            }
        }

        private void btnQLGV_Click(object sender, EventArgs e)
        {
            btnQLGV.Selected = true;
            if (GiangVien == null || GiangVien.IsDisposed)
            {
                GiangVien = new FrmGiangVien();
                GiangVien.MdiParent = this;
                GiangVien.Dock = DockStyle.Fill;
                GiangVien.Show();
            }
            else
            {
                GiangVien.Activate();
            }
        }

        private void btnQLLHP_Click(object sender, EventArgs e)
        {
            btnQLLHP.Selected = true;
            if (LopHocPhan == null || LopHocPhan.IsDisposed)
            {
                LopHocPhan = new FrmLopHocPhan();
                LopHocPhan.MdiParent = this;
                LopHocPhan.Dock = DockStyle.Fill;
                LopHocPhan.Show();
            }
            else
            {
                LopHocPhan.Activate();
            }
        }

        private void btnDK_Click(object sender, EventArgs e)
        {
            btnDK.Selected = true;
            if (DangKi == null || DangKi.IsDisposed)
            {
                DangKi = new FrmDangKi();
                DangKi.MdiParent = this;
                DangKi.Dock = DockStyle.Fill;
                DangKi.Show();
            }
            else
            {
                DangKi.Activate();
            }
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            btnHoaDon.Selected = true;
            if (HoaDon == null || HoaDon.IsDisposed)
            {
                HoaDon = new FrmHoaDon();
                HoaDon.MdiParent = this;
                HoaDon.Dock = DockStyle.Fill;
                HoaDon.Show();
            }
            else
            {
                HoaDon.Activate();
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            btnThanhToan.Selected = true;
            if (ThanhToan == null || ThanhToan.IsDisposed)
            {
                ThanhToan = new FrmThanhToan();
                ThanhToan.MdiParent = this;
                ThanhToan.Dock = DockStyle.Fill;
                ThanhToan.Show();
            }
            else
            {
                ThanhToan.Activate();
            }
        }

        private void btnLichSuDK_Click(object sender, EventArgs e)
        {
            btnLichSuDK.Selected = true;
            if (LichSuDK == null || LichSuDK.IsDisposed)
            {
                LichSuDK = new FrmLichSuDK();
                LichSuDK.MdiParent = this;
                LichSuDK.Dock = DockStyle.Fill;
                LichSuDK.Show();
            }
            else
            {
                LichSuDK.Activate();
            }
        }
    }
}
