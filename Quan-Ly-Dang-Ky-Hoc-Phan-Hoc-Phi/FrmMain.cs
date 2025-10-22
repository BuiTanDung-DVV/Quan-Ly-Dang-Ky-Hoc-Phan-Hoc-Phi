using System;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmMain : Form
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
        FrmDanhSachDangKi DSDVDK;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Bạn cần đăng nhập để sử dụng hệ thống!", "Thông báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                FrmLogin loginForm = new FrmLogin();
                loginForm.ShowDialog();
                return;
            }
            SetupUIByRole();
            ShowWelcomeMessage();
        }

        private void SetupUIByRole()
        {
            HideAllButtons();

            if (UserSession.IsStudent())
            {
                // Sinh viên: Chỉ hiển thị các chức năng liên quan đến sinh viên
                btnDK.Visible = true;          // Đăng ký tín chỉ
                btnThanhToan.Visible = true;   // Thanh toán
                
                lblChucNang.Text = "Chức năng sinh viên";
            }
            else if (UserSession.IsLecturer())
            {
                // Giảng viên: Chỉ hiển thị các chức năng liên quan đến giảng viên
                btnQLLHP.Visible = true;       // Quản lý lớp học phần (lớp mình dạy)
                btnDSSVDK.Visible = true;          // Xem đăng ký của sinh viên
                
                lblChucNang.Text = "Chức năng giảng viên";
            }
            else if (UserSession.IsAdmin())
            {
                
                ShowAllButtons();
                btnDK.Visible = false;
                btnThanhToan.Visible = false;
                btnDSSVDK.Visible = false;

                lblChucNang.Text = "Chức năng quản trị";
            }
            else
            {
                MessageBox.Show($"Role không được nhận diện: '{UserSession.Role}'", "Debug Error");
            }
        }

        private void HideAllButtons()
        {
            btnDK.Visible = false;
            btnQLSV.Visible = false;
            btnQLGV.Visible = false;
            btnQLLHP.Visible = false;
            btnQLHK.Visible = false;
            btnQLNH.Visible = false;
            btnQLMH.Visible = false;
            btnQLKV.Visible = false;
            btnThanhToan.Visible = false;
            btnDSSVDK.Visible = false;
        }

        private void ShowAllButtons()
        {
            btnDK.Visible = true;
            btnQLSV.Visible = true;
            btnQLGV.Visible = true;
            btnQLLHP.Visible = true;
            btnQLHK.Visible = true;
            btnQLNH.Visible = true;
            btnQLMH.Visible = true;
            btnQLKV.Visible = true;
            btnThanhToan.Visible = true;
            btnDSSVDK.Visible = true;
        }

        private void ShowWelcomeMessage()
        {
            try
            {
                if (UserSession.IsStudent())
                {
                    lblName.Text = GetStudentName(UserSession.LinkedStudentID);
                    lblMa.Text = UserSession.LinkedStudentID?.ToString() ?? "N/A";
                }
                else if (UserSession.IsLecturer())
                {
                    lblName.Text = GetLecturerName(UserSession.LinkedLecturerID);
                    lblMa.Text = UserSession.LinkedLecturerID?.ToString() ?? "N/A";
                }
                else if (UserSession.IsAdmin())
                {
                    lblName.Text = UserSession.Username;
                    lblMa.Text = "ADMIN";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị thông tin: {ex.Message}", "Error");
                lblName.Text = UserSession.Username;
                lblMa.Text = UserSession.Role;
            }
        }

        private string GetStudentName(int? studentId)
        {
            if (studentId == null) return UserSession.Username;
            
            try
            {
                KETNOI_CSDL kn = new KETNOI_CSDL();
                string sql = $"SELECT FullName FROM Students WHERE StudentID = {studentId}";
                var dt = kn.Lay_DulieuBang(sql);
                return dt.Rows.Count > 0 ? dt.Rows[0]["FullName"].ToString() : UserSession.Username;
            }
            catch
            {
                return UserSession.Username;
            }
        }

        private string GetLecturerName(int? lecturerId)
        {
            if (lecturerId == null) return UserSession.Username;
            
            try
            {
                KETNOI_CSDL kn = new KETNOI_CSDL();
                string sql = $"SELECT FullName FROM Lecturers WHERE LecturerID = {lecturerId}";
                var dt = kn.Lay_DulieuBang(sql);
                return dt.Rows.Count > 0 ? dt.Rows[0]["FullName"].ToString() : UserSession.Username;
            }
            catch
            {
                return UserSession.Username;
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

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UserSession.EndSession();
                this.Hide();
                FrmLogin loginForm = new FrmLogin();
                loginForm.Show();
                this.Close();
            }
        }

        private void btnDSDVDK_Click(object sender, EventArgs e)
        {
            btnDSSVDK.Selected = true;
            if (DSDVDK == null || DSDVDK.IsDisposed)
            {
                DSDVDK = new FrmDanhSachDangKi();
                DSDVDK.MdiParent = this;
                DSDVDK.Dock = DockStyle.Fill;
                DSDVDK.Show();
            }
            else
            {
                DSDVDK.Activate();
            }
        }
    }
}
