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
            // XÓA HOÀN TOÀN SetupForm() call
        }

        // XÓA HOÀN TOÀN method SetupForm()
        // private void SetupForm() { ... } - ĐÃ XÓA

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
                    // ENABLE COLUMN RESIZING
                    dataKQ.AllowUserToResizeColumns = true;
                    dataKQ.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                    // Thiết lập độ rộng ban đầu tối ưu
                    var columnConfig = new Dictionary<string, (int width, DataGridViewAutoSizeColumnMode mode, int minWidth)>
                    {
                        ["Mã SV"] = (75, DataGridViewAutoSizeColumnMode.None, 50),
                        ["Họ và tên"] = (150, DataGridViewAutoSizeColumnMode.None, 70),
                        ["Giới tính"] = (60, DataGridViewAutoSizeColumnMode.None, 30),
                        ["Ngày sinh"] = (120, DataGridViewAutoSizeColumnMode.None, 80),
                        ["Email"] = (150, DataGridViewAutoSizeColumnMode.None, 100),
                        ["Điện thoại"] = (105, DataGridViewAutoSizeColumnMode.None, 90),
                        ["Địa chỉ"] = (100, DataGridViewAutoSizeColumnMode.None, 30),
                        ["Khoa/Viện"] = (125, DataGridViewAutoSizeColumnMode.None, 100),
                        ["Năm nhập học"] = (85, DataGridViewAutoSizeColumnMode.None, 50),
                        ["Trạng thái"] = (85, DataGridViewAutoSizeColumnMode.None, 50)
                    };

                    foreach (DataGridViewColumn column in dataKQ.Columns)
                    {
                        if (columnConfig.ContainsKey(column.Name))
                        {
                            var config = columnConfig[column.Name];

                            column.Width = config.width;
                            column.AutoSizeMode = config.mode;
                            column.MinimumWidth = config.minWidth;
                            column.Resizable = DataGridViewTriState.True;
                        }

                        // Căn giữa cho các cột phù hợp
                        if (column.Name == "Giới tính" || column.Name == "Ngày sinh" ||
                            column.Name == "Năm nhập học" || column.Name == "Trạng thái" || column.Name == "Mã SV")
                        {
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }

                    // Thêm context menu cho columns (bonus)
                    AddColumnContextMenu();
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi thiết lập cột: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void AddColumnContextMenu()
        {
            try
            {
                ContextMenuStrip columnMenu = new ContextMenuStrip();

                ToolStripMenuItem autoFitItem = new ToolStripMenuItem("🔧 Tự động điều chỉnh độ rộng");
                autoFitItem.Click += (s, e) => {
                    foreach (DataGridViewColumn col in dataKQ.Columns)
                    {
                        if (col.Visible && col.Name != "StudentID")
                        {
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }
                    }
                };

                ToolStripMenuItem resetItem = new ToolStripMenuItem("↺ Khôi phục kích thước mặc định");
                resetItem.Click += (s, e) => ConfigureColumnWidths();

                columnMenu.Items.Add(autoFitItem);
                columnMenu.Items.Add(resetItem);

                dataKQ.ColumnHeaderMouseClick += (s, e) => {
                    if (e.Button == MouseButtons.Right)
                    {
                        columnMenu.Show(dataKQ, dataKQ.PointToClient(Cursor.Position));
                    }
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding context menu: {ex.Message}");
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

                if (dataKQ != null)
                {
                    SetupDataGridView();
                }

                Bang_SinhVien();

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