using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmKhoaVien : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();


        public FrmKhoaVien()
        {
            InitializeComponent();
          
        }

        public void Bang_KhoaVien()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Departments");
            dataKQ.DataSource = dta;
        }



        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmKhoaVien_ChinhSua f1 = new FrmKhoaVien_ChinhSua();
            f1.ShowDialog();
            Bang_KhoaVien();
        }

        private void FrmKhoaVien_Load(object sender, EventArgs e)
        {
            Bang_KhoaVien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataKQ.CurrentRow != null)
            {
                int idKhoaVien = Convert.ToInt32(dataKQ.CurrentRow.Cells["DeptID"].Value);
                FrmKhoaVien_ChinhSua frm = new FrmKhoaVien_ChinhSua(idKhoaVien);
                frm.ShowDialog();
                Bang_KhoaVien();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khoa viện để sửa!");
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                // Nếu không nhập gì thì hiển thị toàn bộ danh sách
                Bang_KhoaVien();
            }
            else
            {
                // Tìm kiếm trong bảng Lecturers theo tên hoặc mã giảng viên
                string sql = "SELECT * FROM Departments WHERE Name LIKE N'%" + tuKhoa + "%' OR Code LIKE '%" + tuKhoa + "%'";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (dataKQ.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khoa/viện cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã khoa/viện từ dòng được chọn
            string maKhoaVien = dataKQ.CurrentRow.Cells["Code"].Value.ToString();

            try
            {
                // ✅ Bước 1: Lấy DeptID tương ứng với Code
                string sqlGetID = $"SELECT DeptID FROM Departments WHERE Code = '{maKhoaVien}'";
                DataTable dtDept = kn.Lay_DulieuBang(sqlGetID);

                if (dtDept.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy khoa/viện trong cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int deptId = Convert.ToInt32(dtDept.Rows[0]["DeptID"]);

                // ✅ Bước 2: Kiểm tra bảng nào có dữ liệu liên quan đến DeptID này
                string sqlCheckFK = @"
            SELECT 
                fk_tab.name AS ReferencingTable
            FROM sys.foreign_keys fk
            INNER JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
            INNER JOIN sys.tables fk_tab ON fk_tab.object_id = fk.parent_object_id
            INNER JOIN sys.tables pk_tab ON pk_tab.object_id = fk.referenced_object_id
            INNER JOIN sys.columns fk_col ON fkc.parent_object_id = fk_col.object_id AND fkc.parent_column_id = fk_col.column_id
            INNER JOIN sys.columns pk_col ON fkc.referenced_object_id = pk_col.object_id AND fkc.referenced_column_id = pk_col.column_id
            WHERE pk_tab.name = 'Departments' AND pk_col.name = 'DeptID';
        ";

                DataTable fkTables = kn.Lay_DulieuBang(sqlCheckFK);
                List<string> bangLienQuan = new List<string>();

                foreach (DataRow row in fkTables.Rows)
                {
                    string tableName = row["ReferencingTable"].ToString();
                    string sqlCount = $"SELECT COUNT(*) FROM {tableName} WHERE DeptID = {deptId}";
                    DataTable dtCount = kn.Lay_DulieuBang(sqlCount);

                    if (dtCount.Rows.Count > 0 && Convert.ToInt32(dtCount.Rows[0][0]) > 0)
                    {
                        bangLienQuan.Add(tableName);
                    }
                }

                // ✅ Bước 3: Nếu có bảng liên quan, cảnh báo người dùng
                if (bangLienQuan.Count > 0)
                {
                    string danhSachBang = string.Join(", ", bangLienQuan);
                    DialogResult confirmFK = MessageBox.Show(
                        $"Khoa/Viện này đang được tham chiếu trong các bảng sau: {danhSachBang}.\n" +
                        "Nếu xóa, dữ liệu liên quan trong các bảng này có thể bị lỗi hoặc mất liên kết.\n\n" +
                        "Bạn có chắc chắn muốn tiếp tục xóa không?",
                        "Cảnh báo ràng buộc khóa ngoại",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirmFK == DialogResult.No)
                        return;
                }

                // ✅ Bước 4: Xác nhận lần cuối trước khi xóa
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa khoa/viện này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    string sqlDelete = $"DELETE FROM Departments WHERE DeptID = {deptId}";
                    kn.ThucThiSQL(sqlDelete);

                    Bang_KhoaVien(); // Cập nhật lại DataGridView

                    MessageBox.Show("Xóa khoa/viện thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
