using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmNganhHoc : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();

        public FrmNganhHoc()
        {
            InitializeComponent();
        }

        public void Bang_NganhHoc()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Majors");
            dataKQ.DataSource = dta;
        }

        private void FrmNganh_Load(object sender, EventArgs e)
        {
            Bang_NganhHoc();
        }

        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmNganhHoc_ChinhSua f1 = new FrmNganhHoc_ChinhSua();

            f1.ShowDialog();

            Bang_NganhHoc();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn dòng nào chưa
            if (dataKQ.CurrentRow != null)
            {
                // 2. Lấy ID (MaMonHoc) từ dòng đang chọn
                // (Thay "MaMonHoc" bằng TÊN CỘT ID trong DataGridView của bạn)
                int idNganhHoc = Convert.ToInt32(dataKQ.CurrentRow.Cells["MajorID"].Value);

                // 3. Mở Form chỉnh sửa và "gửi" ID qua
                FrmNganhHoc_ChinhSua frm = new FrmNganhHoc_ChinhSua(idNganhHoc);
                frm.ShowDialog();

                // 4. Tải lại lưới sau khi Form chỉnh sửa đóng
                Bang_NganhHoc();
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
                Bang_NganhHoc();
            }
            else
            {
                // Tìm kiếm trong bảng Lecturers theo tên hoặc mã giảng viên
                string sql = "SELECT * FROM Majors WHERE Name LIKE N'%" + tuKhoa + "%' OR Code LIKE '%" + tuKhoa + "%'";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (dataKQ.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn ngành học cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã ngành học từ dòng được chọn
            string maNganh = dataKQ.CurrentRow.Cells["MajorID"].Value.ToString();

            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa ngành học này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Câu lệnh SQL để xóa
                    string sql = "DELETE FROM Majors WHERE MajorID = '" + maNganh + "'";
                    kn.ThucThiSQL(sql); // Hàm thực thi câu lệnh SQL (không trả dữ liệu)

                    // Cập nhật lại bảng ngành học
                    Bang_NganhHoc();

                    MessageBox.Show("Xóa ngành học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
