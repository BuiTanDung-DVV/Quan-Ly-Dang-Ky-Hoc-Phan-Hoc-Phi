using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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

            try
            {
                // ✅ Bước 1: Kiểm tra bảng nào đang tham chiếu đến ClassSections.SectionID
                string sqlCheckFK = @"
            SELECT 
                fk_tab.name AS ReferencingTable
            FROM sys.foreign_keys fk
            INNER JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
            INNER JOIN sys.tables fk_tab ON fk_tab.object_id = fk.parent_object_id
            INNER JOIN sys.tables pk_tab ON pk_tab.object_id = fk.referenced_object_id
            INNER JOIN sys.columns fk_col ON fkc.parent_object_id = fk_col.object_id AND fkc.parent_column_id = fk_col.column_id
            INNER JOIN sys.columns pk_col ON fkc.referenced_object_id = pk_col.object_id AND fkc.referenced_column_id = pk_col.column_id
            WHERE pk_tab.name = 'ClassSections' AND pk_col.name = 'SectionID';
        ";

                DataTable fkTables = kn.Lay_DulieuBang(sqlCheckFK);
                List<string> bangLienQuan = new List<string>();

                // ✅ Kiểm tra từng bảng xem có dữ liệu liên quan không
                foreach (DataRow row in fkTables.Rows)
                {
                    string tableName = row["ReferencingTable"].ToString();
                    string sqlCount = $"SELECT COUNT(*) FROM {tableName} WHERE SectionID = {maLopHP}";
                    DataTable dtCount = kn.Lay_DulieuBang(sqlCount);

                    if (dtCount.Rows.Count > 0 && Convert.ToInt32(dtCount.Rows[0][0]) > 0)
                    {
                        bangLienQuan.Add(tableName);
                    }
                }

                // ✅ Bước 2: Nếu có bảng liên quan, cảnh báo người dùng
                if (bangLienQuan.Count > 0)
                {
                    string danhSachBang = string.Join(", ", bangLienQuan);
                    DialogResult confirmFK = MessageBox.Show(
                        $"Lớp học phần này đang được tham chiếu trong các bảng sau: {danhSachBang}.\n" +
                        "Nếu bạn xóa, dữ liệu trong các bảng này có thể bị lỗi hoặc mất liên kết.\n\n" +
                        "Bạn có muốn tiếp tục xóa không?",
                        "Cảnh báo ràng buộc khóa ngoại",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirmFK == DialogResult.No)
                        return;
                }

                // ✅ Bước 3: Xác nhận lần cuối trước khi xóa
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa lớp học phần này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Câu lệnh SQL để xóa lớp học phần
                    string sql = $"DELETE FROM ClassSections WHERE SectionID = {maLopHP}";
                    kn.ThucThiSQL(sql); // Hàm thực thi SQL (không trả dữ liệu)

                    // Cập nhật lại danh sách lớp học phần sau khi xóa
                    Bang_LopHocPhan();

                    MessageBox.Show("Xóa lớp học phần thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
