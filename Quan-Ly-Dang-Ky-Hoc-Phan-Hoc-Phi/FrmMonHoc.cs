using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmMonHoc : Form
    {

        public FrmMonHoc()
        {
            InitializeComponent();
        }

        KETNOI_CSDL kn = new KETNOI_CSDL();

        public void Bang_MonHoc()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Courses");
            dataKQ.DataSource = dta;
        }
        private void lblTenMon_Click(object sender, EventArgs e)
        {

        }

        private void txtTenLop_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            Bang_MonHoc();
        }

        private void btnToaMoi_Click_1(object sender, EventArgs e)
        {
            FrmMonHoc_ChinhSua f1 = new FrmMonHoc_ChinhSua();

            f1.ShowDialog();

            Bang_MonHoc();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn dòng nào chưa
            if (dataKQ.CurrentRow != null)
            {
                // 2. Lấy ID (MaMonHoc) từ dòng đang chọn
                // (Thay "MaMonHoc" bằng TÊN CỘT ID trong DataGridView của bạn)
                int idMonHoc = Convert.ToInt32(dataKQ.CurrentRow.Cells["CourseID"].Value);

                // 3. Mở Form chỉnh sửa và "gửi" ID qua
                FrmMonHoc_ChinhSua frm = new FrmMonHoc_ChinhSua(idMonHoc);
                frm.ShowDialog();

                // 4. Tải lại lưới sau khi Form chỉnh sửa đóng
                Bang_MonHoc();
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
                Bang_MonHoc();
            }
            else
            {
                // Tìm kiếm trong bảng Lecturers theo tên hoặc mã giảng viên
                string sql = "SELECT * FROM Courses WHERE Name LIKE N'%" + tuKhoa + "%' OR Code LIKE '%" + tuKhoa + "%'";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (dataKQ.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn môn học cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã môn học từ dòng được chọn
            string maMon = dataKQ.CurrentRow.Cells["CourseID"].Value.ToString();

            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa môn học này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Tạo câu lệnh SQL để xóa môn học
                    string sql = "DELETE FROM Courses WHERE CourseID = '" + maMon + "'";
                    kn.ThucThiSQL(sql); // Gọi hàm thực thi SQL (hàm này trong lớp 'kn' của bạn)

                    // Cập nhật lại DataGridView sau khi xóa
                    Bang_MonHoc();

                    MessageBox.Show("Xóa môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
