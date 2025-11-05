using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmEditPassword : Form
    {
        private int _userId; // hoặc truyền Username, tùy thiết kế
        KETNOI_CSDL kn = new KETNOI_CSDL();

        public FrmEditPassword(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string oldPass = txtOldPassword.Text.Trim();
            string newPass = txtNewPassword.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường!");
                return;
            }
            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận không trùng khớp!");
                return;
            }

            // Kiểm tra mật khẩu cũ
            string hashOldPass = oldPass;
            string sqlCheck = $"SELECT COUNT(*) FROM Users WHERE UserID={_userId} AND PasswordHash=N'{hashOldPass}'";
            try
            {
                kn.KetNoi_Dulieu();  // MỞ KẾT NỐI TRƯỚC

                SqlCommand cmd = new SqlCommand(sqlCheck, kn.cnn);
                int exist = (int)cmd.ExecuteScalar();
                if (exist == 0)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!");
                    return;
                }

                // Update mật khẩu mới
                string hashNew = newPass;
                string sqlUpdate = $"UPDATE Users SET PasswordHash=N'{hashNew}' WHERE UserID={_userId}";
                SqlCommand cmdSave = new SqlCommand(sqlUpdate, kn.cnn);
                cmdSave.ExecuteNonQuery();
                MessageBox.Show("Đổi mật khẩu thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                if (kn.cnn.State == System.Data.ConnectionState.Open)
                    kn.cnn.Close();  // ĐÓNG KẾT NỐI SAU KHI XONG
            }
        }
    }
}