using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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

            try
            {
                // ✅ Bước 1: Lấy TermID từ Code để dùng cho kiểm tra
                string sqlGetID = $"SELECT TermID FROM AcademicTerms WHERE Code = '{maHocKy}'";
                DataTable dtTerm = kn.Lay_DulieuBang(sqlGetID);

                if (dtTerm.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy học kỳ trong cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string termID = dtTerm.Rows[0]["TermID"].ToString();

                // ✅ Bước 2: Kiểm tra bảng nào đang tham chiếu đến AcademicTerms.TermID
                string sqlCheckFK = @"
            SELECT 
                fk_tab.name AS ReferencingTable
            FROM sys.foreign_keys fk
            INNER JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
            INNER JOIN sys.tables fk_tab ON fk_tab.object_id = fk.parent_object_id
            INNER JOIN sys.tables pk_tab ON pk_tab.object_id = fk.referenced_object_id
            INNER JOIN sys.columns fk_col ON fkc.parent_object_id = fk_col.object_id AND fkc.parent_column_id = fk_col.column_id
            INNER JOIN sys.columns pk_col ON fkc.referenced_object_id = pk_col.object_id AND fkc.referenced_column_id = pk_col.column_id
            WHERE pk_tab.name = 'AcademicTerms' AND pk_col.name = 'TermID';
        ";

                DataTable fkTables = kn.Lay_DulieuBang(sqlCheckFK);
                List<string> bangLienQuan = new List<string>();

                // ✅ Bước 3: Kiểm tra từng bảng có dữ liệu liên quan không
                foreach (DataRow row in fkTables.Rows)
                {
                    string tableName = row["ReferencingTable"].ToString();
                    string sqlCount = $"SELECT COUNT(*) FROM {tableName} WHERE TermID = {termID}";
                    DataTable dtCount = kn.Lay_DulieuBang(sqlCount);

                    if (dtCount.Rows.Count > 0 && Convert.ToInt32(dtCount.Rows[0][0]) > 0)
                    {
                        bangLienQuan.Add(tableName);
                    }
                }

                // ✅ Bước 4: Nếu có bảng liên quan → cảnh báo người dùng
                if (bangLienQuan.Count > 0)
                {
                    string danhSachBang = string.Join(", ", bangLienQuan);
                    DialogResult confirmFK = MessageBox.Show(
                        $"Học kỳ này đang được tham chiếu trong các bảng sau: {danhSachBang}.\n" +
                        "Nếu bạn xóa, dữ liệu trong các bảng này có thể bị lỗi hoặc mất liên kết.\n\n" +
                        "Bạn có chắc chắn muốn tiếp tục xóa không?",
                        "Cảnh báo ràng buộc khóa ngoại",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirmFK == DialogResult.No)
                        return;
                }

                // ✅ Bước 5: Xác nhận lần cuối
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa học kỳ này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    string sqlDelete = $"DELETE FROM AcademicTerms WHERE TermID = {termID}";
                    kn.ThucThiSQL(sqlDelete);

                    Bang_HocKi(); // Cập nhật lại DataGridView

                    MessageBox.Show("Xóa học kỳ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
