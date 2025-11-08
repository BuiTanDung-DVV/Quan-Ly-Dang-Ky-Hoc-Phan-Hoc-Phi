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
    public partial class FrmGiangVien : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();

        public FrmGiangVien()
        {
            InitializeComponent();
        }

        private void SetupDataGridView()
        {
            try
            {
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

        public void Bang_GiangVien()
        {
            try
            {
                ShowLoading(true);

                string sql = @"
                    SELECT
                        l.LecturerID,
                        l.LecturerCode AS [Mã GV],
                        l.FullName AS [Họ và tên],
                        l.Email,
                        d.Name AS [Khoa/Viện]
                    FROM Lecturers l
                    LEFT JOIN Departments d ON l.DeptID = d.DeptID
                    ORDER BY l.LecturerCode";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                // Ẩn cột LecturerID
                if (dataKQ.Columns["LecturerID"] != null)
                {
                    dataKQ.Columns["LecturerID"].Visible = false;
                }

                ConfigureColumnWidths();
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu giảng viên: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
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
                    dataKQ.AllowUserToResizeColumns = true;
                    dataKQ.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                    var columnConfig = new Dictionary<string, (int width, DataGridViewAutoSizeColumnMode mode, int minWidth)>
                    {
                        ["Mã GV"] = (75, DataGridViewAutoSizeColumnMode.None, 50),
                        ["Họ và tên"] = (150, DataGridViewAutoSizeColumnMode.None, 70),
                        ["Email"] = (170, DataGridViewAutoSizeColumnMode.None, 100),
                        ["Khoa/Viện"] = (125, DataGridViewAutoSizeColumnMode.None, 90)
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
                        if (column.Name == "Mã GV")
                        {
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }

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
                        if (col.Visible && col.Name != "LecturerID")
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
                // Bạn có thể cập nhật label hiển thị số bản ghi nếu cần tại đây
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
                    if (btnThemMoi != null) btnThemMoi.Enabled = false;
                    if (btnSua != null) btnSua.Enabled = false;
                    if (btnXoa != null) btnXoa.Enabled = false;
                    if (btnTimKiem != null) btnTimKiem.Enabled = false;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    if (btnThemMoi != null) btnThemMoi.Enabled = true;
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

        private void FrmGiangVien_Load(object sender, EventArgs e)
        {
            try
            {
                if (kn.cnn == null || kn.cnn.State != ConnectionState.Open)
                {
                    kn.KetNoi_Dulieu();
                }

                if (dataKQ != null)
                {
                    SetupDataGridView();
                }

                Bang_GiangVien();

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

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                FrmGiangVien_ChinhSua f1 = new FrmGiangVien_ChinhSua();
                if (f1.ShowDialog() == DialogResult.OK)
                {
                    Bang_GiangVien();
                    ShowMessage("Thêm giảng viên thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form thêm mới: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemGiangVien();
        }

        private void TimKiemGiangVien()
        {
            try
            {
                ShowLoading(true);

                string tuKhoa = txtTimKiem?.Text?.Trim() ?? "";

                if (string.IsNullOrEmpty(tuKhoa))
                {
                    Bang_GiangVien();
                    return;
                }

                string sql = @"
                    SELECT
                        l.LecturerID,
                        l.LecturerCode AS [Mã GV],
                        l.FullName AS [Họ và tên],
                        l.Email,
                        d.Name AS [Khoa/Viện]
                    FROM Lecturers l
                    LEFT JOIN Departments d ON l.DeptID = d.DeptID
                    WHERE l.FullName LIKE N'%" + tuKhoa + "%' " +
                          "OR l.LecturerCode LIKE '%" + tuKhoa + "%' " +
                          "OR l.Email LIKE '%" + tuKhoa + "%' " +
                          "OR d.Name LIKE N'%" + tuKhoa + "%' " +
                    "ORDER BY l.LecturerCode";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                if (dataKQ.Columns["LecturerID"] != null)
                {
                    dataKQ.Columns["LecturerID"].Visible = false;
                }

                ConfigureColumnWidths();
                UpdateRecordCount();

                if (dta.Rows.Count == 0)
                {
                    ShowMessage($"Không tìm thấy giảng viên nào với từ khóa '{tuKhoa}'",
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
                    ShowMessage("Vui lòng chọn giảng viên cần xóa.", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string lecturerID = dataKQ.CurrentRow.Cells["LecturerID"].Value?.ToString();
                string fullName = dataKQ.CurrentRow.Cells["Họ và tên"].Value?.ToString();
                string lecturerCode = dataKQ.CurrentRow.Cells["Mã GV"].Value?.ToString();

                if (string.IsNullOrEmpty(lecturerID))
                {
                    ShowMessage("Không thể lấy thông tin giảng viên!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa giảng viên?\n\n" +
                    $"👨‍🏫 Mã GV: {lecturerCode}\n" +
                    $"👤 Tên: {fullName}\n\n" +
                    $"⚠️ Thao tác này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ShowLoading(true);

                    // Xóa tài khoản user liên quan trước (nếu có)
                    string sqlDeleteUser = $"DELETE FROM Users WHERE LinkedLecturerID = {lecturerID}";
                    kn.ThucThiSQL(sqlDeleteUser);

                    // Xóa giảng viên
                    string sqlDeleteLecturer = $"DELETE FROM Lecturers WHERE LecturerID = {lecturerID}";
                    kn.ThucThiSQL(sqlDeleteLecturer);

                    Bang_GiangVien();
                    ShowMessage("Xóa giảng viên thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi khi xóa giảng viên: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
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
                    ShowMessage("Vui lòng chọn giảng viên cần chỉnh sửa!", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string idValue = dataKQ.CurrentRow.Cells["LecturerID"].Value?.ToString();

                if (string.IsNullOrEmpty(idValue) || !int.TryParse(idValue, out int idGiangVien))
                {
                    ShowMessage("Không thể lấy thông tin giảng viên!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                FrmGiangVien_ChinhSua frm = new FrmGiangVien_ChinhSua(idGiangVien);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Bang_GiangVien();
                    ShowMessage("Cập nhật thông tin giảng viên thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TimKiemGiangVien();
            }
        }

        private void dataKQ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnSua_Click(sender, e);
            }
        }

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