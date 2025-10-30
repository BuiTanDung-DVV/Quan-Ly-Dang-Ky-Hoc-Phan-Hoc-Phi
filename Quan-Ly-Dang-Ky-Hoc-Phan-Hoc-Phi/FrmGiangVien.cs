using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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

            try
            {
                // ✅ Bước 1: Kiểm tra bảng nào có khóa ngoại tham chiếu đến Lecturers.LecturerID
                string sqlCheckFK = @"
            SELECT 
                fk_tab.name AS ReferencingTable
            FROM sys.foreign_keys fk
            INNER JOIN sys.foreign_key_columns fkc ON fkc.constraint_object_id = fk.object_id
            INNER JOIN sys.tables fk_tab ON fk_tab.object_id = fk.parent_object_id
            INNER JOIN sys.tables pk_tab ON pk_tab.object_id = fk.referenced_object_id
            INNER JOIN sys.columns fk_col ON fkc.parent_object_id = fk_col.object_id AND fkc.parent_column_id = fk_col.column_id
            INNER JOIN sys.columns pk_col ON fkc.referenced_object_id = pk_col.object_id AND fkc.referenced_column_id = pk_col.column_id
            WHERE pk_tab.name = 'Lecturers' AND pk_col.name = 'LecturerID';
        ";

                DataTable fkTables = kn.Lay_DulieuBang(sqlCheckFK);
                List<string> bangLienQuan = new List<string>();

                // ✅ Bước 2: Kiểm tra xem trong các bảng đó có dữ liệu liên quan không
                foreach (DataRow row in fkTables.Rows)
                {
                    string tableName = row["ReferencingTable"].ToString();
                    string sqlCount = $"SELECT COUNT(*) FROM {tableName} WHERE LecturerID = {maGV}";
                    DataTable dtCount = kn.Lay_DulieuBang(sqlCount);

                    if (dtCount.Rows.Count > 0 && Convert.ToInt32(dtCount.Rows[0][0]) > 0)
                    {
                        bangLienQuan.Add(tableName);
                    }
                }

                // ✅ Bước 3: Nếu có bảng liên quan thì cảnh báo
                if (bangLienQuan.Count > 0)
                {
                    string danhSachBang = string.Join(", ", bangLienQuan);
                    DialogResult confirmFK = MessageBox.Show(
                        $"Giảng viên này đang được tham chiếu trong các bảng sau: {danhSachBang}.\n" +
                        "Nếu bạn xóa, dữ liệu liên quan trong các bảng này có thể bị lỗi hoặc mất liên kết.\n\n" +
                        "Bạn có muốn tiếp tục xóa không?",
                        "Cảnh báo ràng buộc khóa ngoại",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirmFK == DialogResult.No)
                        return;
                }

                // ✅ Bước 4: Xác nhận xóa lần cuối
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc muốn xóa giảng viên này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // ✅ Bước 5: Thực thi lệnh xóa
                    string sqlDelete = $"DELETE FROM Lecturers WHERE LecturerID = {maGV}";
                    kn.ThucThiSQL(sqlDelete);

                    // Cập nhật lại DataGridView sau khi xóa
                    Bang_GiangVien();

                    MessageBox.Show("Xóa giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
