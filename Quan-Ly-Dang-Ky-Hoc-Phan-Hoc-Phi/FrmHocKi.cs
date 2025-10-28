using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmHocKi : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();

        public FrmHocKi()
        {
            InitializeComponent();
        }

        public void Bang_HocKi()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM AcademicTerms");
            dataKQ.DataSource = dta;
        }

        private void FrmHocKi_Load(object sender, EventArgs e)
        {
            Bang_HocKi();
        }

        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmHocKi_ChinhSua f1 = new FrmHocKi_ChinhSua();
            f1.ShowDialog();
            Bang_HocKi();
        }

        private void btnSua1_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn dòng nào chưa
            if (dataKQ.CurrentRow != null)
            {
                // 2. Lấy ID (MaMonHoc) từ dòng đang chọn
                int idHocKi = Convert.ToInt32(dataKQ.CurrentRow.Cells["TermID"].Value);

                // 3. Mở Form chỉnh sửa và "gửi" ID qua
                FrmHocKi_ChinhSua f1 = new FrmHocKi_ChinhSua(idHocKi);
                f1.ShowDialog();

                // 4. Tải lại lưới sau khi Form chỉnh sửa đóng
                Bang_HocKi();
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
                Bang_HocKi();
            }
            else
            {
                // Tìm kiếm trong bảng Lecturers theo tên hoặc mã giảng viên
                string sql = "SELECT * FROM AcademicTerms WHERE Name LIKE N'%" + tuKhoa + "%' OR Code LIKE '%" + tuKhoa + "%'";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (dataKQ.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn học kỳ cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã học kỳ từ dòng được chọn
            string maHocKy = dataKQ.CurrentRow.Cells["Code"].Value.ToString();

            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa học kỳ này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Tạo câu lệnh SQL để xóa học kỳ
                    string sql = "DELETE FROM AcademicTerms WHERE Code = '" + maHocKy + "'";
                    kn.ThucThiSQL(sql); // Hàm thực thi SQL (không trả dữ liệu)

                    // Cập nhật lại bảng học kỳ
                    Bang_HocKi();

                    MessageBox.Show("Xóa học kỳ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
