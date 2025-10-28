using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmLopHocPhan : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();


        public FrmLopHocPhan()
        {
            InitializeComponent();
        }

        public void Bang_LopHocPhan()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM ClassSections");
            dataKQ.DataSource = dta;
        }

        private void FrmLopHocPhan_Load(object sender, EventArgs e)
        {
            Bang_LopHocPhan();
        }

        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmLopHocPhan_ChinhSua f1 = new FrmLopHocPhan_ChinhSua();

            f1.ShowDialog();

            Bang_LopHocPhan();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataKQ.CurrentRow != null)
            {
                int idLopHocPhan = Convert.ToInt32(dataKQ.CurrentRow.Cells["SectionID"].Value);
                FrmLopHocPhan_ChinhSua f1 = new FrmLopHocPhan_ChinhSua(idLopHocPhan);
                f1.ShowDialog();
                Bang_LopHocPhan();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp học phần để sửa!");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                // Nếu không nhập gì thì hiển thị toàn bộ danh sách
                Bang_LopHocPhan();
            }
            else
            {
                // Tìm kiếm trong bảng Lecturers theo tên hoặc mã giảng viên
                string sql = "SELECT * FROM ClassSections WHERE Room LIKE N'%" + tuKhoa + "%' OR SectionCode LIKE '%" + tuKhoa + "%'";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (dataKQ.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã lớp học phần từ dòng được chọn
            string maLopHP = dataKQ.CurrentRow.Cells["SectionID"].Value.ToString();

            // Xác nhận trước khi xóa
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa lớp học phần này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Câu lệnh SQL để xóa lớp học phần
                    string sql = "DELETE FROM ClassSections WHERE SectionID = '" + maLopHP + "'";
                    kn.ThucThiSQL(sql); // Hàm thực thi SQL (không trả dữ liệu)

                    // Cập nhật lại danh sách lớp học phần sau khi xóa
                    Bang_LopHocPhan();

                    MessageBox.Show("Xóa lớp học phần thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
