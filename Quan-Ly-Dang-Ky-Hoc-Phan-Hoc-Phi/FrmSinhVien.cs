using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmSinhVien : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();

        public FrmSinhVien()
        {
            InitializeComponent();
        }

        public void Bang_SinhVien()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Students");
            dataKQ.DataSource = dta;
        }

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            Bang_SinhVien();
        }


        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmSinhVien_ChinhSua f1 = new FrmSinhVien_ChinhSua();
            f1.ShowDialog();
            Bang_SinhVien();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                // Nếu không nhập gì thì hiển thị toàn bộ danh sách
                Bang_SinhVien();
            }
            else
            {
                // Tìm kiếm trong bảng Lecturers theo tên hoặc mã giảng viên
                string sql = "SELECT * FROM Students WHERE FullName LIKE N'%" + tuKhoa + "%' OR StudentCode LIKE '%" + tuKhoa + "%'";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (dataKQ.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã giảng viên từ dòng được chọn
            string maSV = dataKQ.CurrentRow.Cells["StudentID"].Value.ToString();

            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Tạo câu lệnh SQL để xóa
                    string sql = "DELETE FROM Students WHERE StudentID = '" + maSV + "'";
                    kn.ThucThiSQL(sql); // Hàm thực thi không trả dữ liệu (bạn có thể có hàm này trong lớp kn)

                    // Cập nhật lại bảng
                    Bang_SinhVien();

                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
