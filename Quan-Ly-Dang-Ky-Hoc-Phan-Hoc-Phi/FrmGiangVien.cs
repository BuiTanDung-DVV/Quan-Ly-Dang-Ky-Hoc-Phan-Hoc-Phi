using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmGiangVien : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();


        public FrmGiangVien()
        {
            InitializeComponent();
        }

        public void Bang_GiangVien()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Lecturers");
            dataKQ.DataSource = dta;
        }

        private void FrmGiangVien_Load(object sender, EventArgs e)
        {
            Bang_GiangVien();
        }

        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmGiangVien_ChinhSua f1 = new FrmGiangVien_ChinhSua();
            f1.ShowDialog();
            Bang_GiangVien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn dòng nào chưa
            if (dataKQ.CurrentRow != null)
            {
                // 2. Lấy ID (MaMonHoc) từ dòng đang chọn
                // (Thay "MaMonHoc" bằng TÊN CỘT ID trong DataGridView của bạn)
                int idGiangVien = Convert.ToInt32(dataKQ.CurrentRow.Cells["LecturerID"].Value);

                // 3. Mở Form chỉnh sửa và "gửi" ID qua
                FrmGiangVien_ChinhSua frm = new FrmGiangVien_ChinhSua(idGiangVien);
                frm.ShowDialog();

                // 4. Tải lại lưới sau khi Form chỉnh sửa đóng
                Bang_GiangVien();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một môn học để sửa!");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                // Nếu không nhập gì thì hiển thị toàn bộ danh sách
                Bang_GiangVien();
            }
            else
            {
                // Tìm kiếm trong bảng Lecturers theo tên hoặc mã giảng viên
                string sql = "SELECT * FROM Lecturers WHERE FullName LIKE N'%" + tuKhoa + "%' OR LecturerID LIKE '%" + tuKhoa + "%'";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (dataKQ.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn giảng viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã giảng viên từ dòng được chọn
            string maGV = dataKQ.CurrentRow.Cells["LecturerID"].Value.ToString();

            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa giảng viên này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Tạo câu lệnh SQL để xóa giảng viên
                    string sql = "DELETE FROM Lecturers WHERE LecturerID = '" + maGV + "'";
                    kn.ThucThiSQL(sql); // Hàm thực thi SQL (không trả dữ liệu)

                    // Cập nhật lại bảng dữ liệu sau khi xóa
                    Bang_GiangVien();

                    MessageBox.Show("Xóa giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
