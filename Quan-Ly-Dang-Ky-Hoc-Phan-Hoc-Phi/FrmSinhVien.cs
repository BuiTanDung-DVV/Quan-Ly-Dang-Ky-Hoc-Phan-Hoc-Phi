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
            SetupForm();
        }

        private void SetupForm()
        {
            // Thiết lập form
            this.Text = "Quản lý Sinh viên";

            // Thiết lập DataGridView nếu có
            if (dataKQ != null)
            {
                SetupDataGridView();
            }
        }

        private void SetupDataGridView()
        {
            try
            {
                // Thiết lập các thuộc tính cho DataGridView
                dataKQ.AutoGenerateColumns = true;
                dataKQ.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataKQ.MultiSelect = false;
                dataKQ.ReadOnly = true;
                dataKQ.AllowUserToAddRows = false;
                dataKQ.AllowUserToDeleteRows = false;
                dataKQ.RowHeadersVisible = false;

                // Thiết lập alternating row colors
                dataKQ.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dataKQ.DefaultCellStyle.SelectionBackColor = Color.FromArgb(24, 79, 147);
                dataKQ.DefaultCellStyle.SelectionForeColor = Color.White;

                // Thiết lập font
                dataKQ.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                dataKQ.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                dataKQ.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(24, 79, 147);
                dataKQ.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dataKQ.EnableHeadersVisualStyles = false;
                dataKQ.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataKQ.ColumnHeadersHeight = 35;
                dataKQ.RowTemplate.Height = 30;
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi thiết lập DataGridView: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        public void Bang_SinhVien()
        {
            try
            {
                ShowLoading(true);

                string sql = @"
                    SELECT 
                        s.StudentID,
                        s.StudentCode AS [Mã SV],
                        s.FullName AS [Họ và tên],
                        s.Gender AS [Giới tính],
                        FORMAT(s.DateOfBirth, 'dd/MM/yyyy') AS [Ngày sinh],
                        s.Email,
                        s.Phone AS [Điện thoại],
                        s.Address AS [Địa chỉ],
                        d.Name AS [Khoa/Viện],
                        s.AdmissionYear AS [Năm nhập học],
                        s.Status AS [Trạng thái]
                    FROM Students s
                    LEFT JOIN Departments d ON s.DeptID = d.DeptID
                    ORDER BY s.StudentCode";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                // Ẩn cột StudentID
                if (dataKQ.Columns["StudentID"] != null)
                {
                    dataKQ.Columns["StudentID"].Visible = false;
                }

                // Thiết lập độ rộng cột
                ConfigureColumnWidths();

                // Cập nhật số lượng bản ghi
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu sinh viên: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void ConfigureColumnWidths()
        {
            try
            {
                if (dataKQ.Columns.Count > 0)
                {
                    // Thiết lập độ rộng cho các cột
                    var columnWidths = new Dictionary<string, int>
                    {
                        ["Mã SV"] = 100,
                        ["Họ và tên"] = 180,
                        ["Giới tính"] = 80,
                        ["Ngày sinh"] = 100,
                        ["Email"] = 200,
                        ["Điện thoại"] = 120,
                        ["Địa chỉ"] = 250,
                        ["Khoa/Viện"] = 150,
                        ["Năm nhập học"] = 100,
                        ["Trạng thái"] = 100
                    };

                    foreach (DataGridViewColumn column in dataKQ.Columns)
                    {
                        if (columnWidths.ContainsKey(column.Name))
                        {
                            column.Width = columnWidths[column.Name];
                        }

                        // Căn giữa cho một số cột
                        if (column.Name == "Giới tính" || column.Name == "Ngày sinh" ||
                            column.Name == "Năm nhập học" || column.Name == "Trạng thái")
                        {
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi thiết lập cột: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void UpdateRecordCount()
        {
            try
            {
                int count = dataKQ.Rows.Count;
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi cập nhật số lượng: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ShowLoading(bool show)
        {
            try
            {
                if (show)
                {
                    this.Cursor = Cursors.WaitCursor;
                    // Disable buttons
                    if (btnToaMoi != null) btnToaMoi.Enabled = false;
                    if (btnSua != null) btnSua.Enabled = false;
                    if (btnXoa != null) btnXoa.Enabled = false;
                    if (btnTimKiem != null) btnTimKiem.Enabled = false;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    // Enable buttons
                    if (btnToaMoi != null) btnToaMoi.Enabled = true;
                    if (btnSua != null) btnSua.Enabled = true;
                    if (btnXoa != null) btnXoa.Enabled = true;
                    if (btnTimKiem != null) btnTimKiem.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi hiển thị loading: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            try
            {
                // Khởi tạo kết nối database
                if (kn.cnn == null || kn.cnn.State != ConnectionState.Open)
                {
                    kn.KetNoi_Dulieu();
                }

                Bang_SinhVien();

                // Focus vào textbox tìm kiếm nếu có
                if (txtTimKiem != null)
                {
                    txtTimKiem.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi khởi tạo form: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSinhVien_ChinhSua f1 = new FrmSinhVien_ChinhSua();
                if (f1.ShowDialog() == DialogResult.OK)
                {
                    Bang_SinhVien();
                    ShowMessage("Thêm sinh viên thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form thêm mới: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemSinhVien();
        }

        private void TimKiemSinhVien()
        {
            try
            {
                ShowLoading(true);

                string tuKhoa = txtTimKiem?.Text?.Trim() ?? "";

                if (string.IsNullOrEmpty(tuKhoa))
                {
                    Bang_SinhVien();
                    return;
                }

                string sql = @"
                    SELECT 
                        s.StudentID,
                        s.StudentCode AS [Mã SV],
                        s.FullName AS [Họ và tên],
                        s.Gender AS [Giới tính],
                        FORMAT(s.DateOfBirth, 'dd/MM/yyyy') AS [Ngày sinh],
                        s.Email,
                        s.Phone AS [Điện thoại],
                        s.Address AS [Địa chỉ],
                        d.Name AS [Khoa/Viện],
                        s.AdmissionYear AS [Năm nhập học],
                        s.Status AS [Trạng thái]
                    FROM Students s
                    LEFT JOIN Departments d ON s.DeptID = d.DeptID
                    WHERE s.FullName LIKE N'%" + tuKhoa + "%' " +
                          "OR s.StudentCode LIKE '%" + tuKhoa + "%' " +
                          "OR s.Email LIKE '%" + tuKhoa + "%' " +
                          "OR s.Phone LIKE '%" + tuKhoa + "%' " +
                          "OR d.Name LIKE N'%" + tuKhoa + "%' " +
                    "ORDER BY s.StudentCode";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                // Ẩn cột StudentID
                if (dataKQ.Columns["StudentID"] != null)
                {
                    dataKQ.Columns["StudentID"].Visible = false;
                }

                ConfigureColumnWidths();
                UpdateRecordCount();

                if (dta.Rows.Count == 0)
                {
                    ShowMessage($"Không tìm thấy sinh viên nào với từ khóa '{tuKhoa}'",
                              "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataKQ.CurrentRow == null)
                {
                    ShowMessage("Vui lòng chọn sinh viên cần xóa.", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string maSV = dataKQ.CurrentRow.Cells["StudentID"].Value?.ToString();
                string tenSV = dataKQ.CurrentRow.Cells["Họ và tên"].Value?.ToString();
                string maSinhVien = dataKQ.CurrentRow.Cells["Mã SV"].Value?.ToString();

                if (string.IsNullOrEmpty(maSV))
                {
                    ShowMessage("Không thể lấy thông tin sinh viên!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa sinh viên?\n\n" +
                    $"🎓 Mã SV: {maSinhVien}\n" +
                    $"👤 Tên: {tenSV}\n\n" +
                    $"⚠️ Thao tác này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ShowLoading(true);

                    // Xóa tài khoản user liên quan trước
                    string sqlDeleteUser = $"DELETE FROM Users WHERE LinkedStudentID = {maSV}";
                    kn.ThucThiSQL(sqlDeleteUser);

                    // Xóa sinh viên
                    string sqlDeleteStudent = $"DELETE FROM Students WHERE StudentID = {maSV}";
                    kn.ThucThiSQL(sqlDeleteStudent);

                    Bang_SinhVien();
                    ShowMessage("Xóa sinh viên thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi khi xóa sinh viên: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataKQ.CurrentRow == null)
                {
                    ShowMessage("Vui lòng chọn sinh viên cần chỉnh sửa!", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string idValue = dataKQ.CurrentRow.Cells["StudentID"].Value?.ToString();

                if (string.IsNullOrEmpty(idValue) || !int.TryParse(idValue, out int idSinhVien))
                {
                    ShowMessage("Không thể lấy thông tin sinh viên!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                FrmSinhVien_ChinhSua frm = new FrmSinhVien_ChinhSua(idSinhVien);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Bang_SinhVien();
                    ShowMessage("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        // Event handlers cho TextBox tìm kiếm
        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TimKiemSinhVien();
            }
        }

        // Event handler cho double-click trên DataGridView
        private void dataKQ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnSua_Click(sender, e);
            }
        }

        // Cleanup resources
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                if (kn?.cnn != null && kn.cnn.State == ConnectionState.Open)
                {
                    kn.NgatKetNoi();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error closing connection: {ex.Message}");
            }

            base.OnFormClosed(e);
        }
    }
}