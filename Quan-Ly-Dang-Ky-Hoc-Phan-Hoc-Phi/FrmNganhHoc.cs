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
            if (dataKQ.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn ngành học cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNganh = dataKQ.CurrentRow.Cells["MajorID"].Value.ToString();

            try
            {
                // ✅ Bước 1: Tìm các bảng có foreign key tham chiếu đến Majors(MajorID)
                string sqlCheckFK = @"
            SELECT 
                fk_tab.name AS ReferencingTable
            FROM sys.foreign_keys fk
            INNER JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
            INNER JOIN sys.tables fk_tab ON fk_tab.object_id = fk.parent_object_id
            INNER JOIN sys.tables pk_tab ON pk_tab.object_id = fk.referenced_object_id
            INNER JOIN sys.columns fk_col ON fkc.parent_object_id = fk_col.object_id AND fkc.parent_column_id = fk_col.column_id
            INNER JOIN sys.columns pk_col ON fkc.referenced_object_id = pk_col.object_id AND fkc.referenced_column_id = pk_col.column_id
            WHERE pk_tab.name = 'Majors' AND pk_col.name = 'MajorID';
        ";

                DataTable fkTables = kn.Lay_DulieuBang(sqlCheckFK);
                List<string> bangLienQuan = new List<string>();

                foreach (DataRow row in fkTables.Rows)
                {
                    string tableName = row["ReferencingTable"].ToString();
                    string sqlCount = $"SELECT COUNT(*) FROM {tableName} WHERE MajorID = {maNganh}";
                    DataTable dtCount = kn.Lay_DulieuBang(sqlCount);

                    if (dtCount.Rows.Count > 0 && Convert.ToInt32(dtCount.Rows[0][0]) > 0)
                    {
                        bangLienQuan.Add(tableName);
                    }
                }

                // ✅ Bước 2: Nếu có bảng liên quan, hỏi xác nhận
                if (bangLienQuan.Count > 0)
                {
                    string danhSachBang = string.Join(", ", bangLienQuan);
                    DialogResult confirmFK = MessageBox.Show(
                        $"Ngành học này đang được tham chiếu trong các bảng sau: {danhSachBang}.\n" +
                        "Nếu xóa, dữ liệu liên quan trong các bảng này có thể gây lỗi hoặc mất liên kết.\n\n" +
                        "Bạn có chắc chắn muốn tiếp tục xóa không?",
                        "Cảnh báo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirmFK == DialogResult.No)
                        return;
                }

                // ✅ Bước 3: Xác nhận xóa
                DialogResult confirmDelete = MessageBox.Show(
                    "Bạn có chắc muốn xóa ngành học này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmDelete == DialogResult.Yes)
                {
                    string sqlDelete = $"DELETE FROM Majors WHERE MajorID = {maNganh}";
                    kn.ThucThiSQL(sqlDelete);
                    Bang_NganhHoc();
                    MessageBox.Show("Xóa ngành học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
