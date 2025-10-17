using System;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmMain : Form
    {
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
    }
}
