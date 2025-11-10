using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmKhoaVien : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();

        public FrmKhoaVien()
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

                dataKQ.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dataKQ.DefaultCellStyle.SelectionBackColor = Color.FromArgb(24, 79, 147);
                dataKQ.DefaultCellStyle.SelectionForeColor = Color.White;

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

        public void Bang_KhoaVien()
        {
            try
            {
                ShowLoading(true);

                // Lấy danh sách Khoa/Viện
                string sql = @"
                    SELECT 
                        DeptID,
                        Code AS [Mã Khoa],
                        Name AS [Tên Khoa/Viện],
                        Office AS [Phòng/Làm việc]
                    FROM Departments
                    ORDER BY Code";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                if (dataKQ.Columns["DeptID"] != null)
                {
                    dataKQ.Columns["DeptID"].Visible = false;
                }

                ConfigureColumnWidths();
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu khoa/viện: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
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
                        ["Mã Khoa"] = (95, DataGridViewAutoSizeColumnMode.None, 50),
                        ["Tên Khoa/Viện"] = (200, DataGridViewAutoSizeColumnMode.None, 100),
                        ["Phòng/Làm việc"] = (170, DataGridViewAutoSizeColumnMode.None, 90)
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
                        if (column.Name == "Mã Khoa")
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
                        if (col.Visible && col.Name != "DeptID")
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
            catch { }
        }

        private void UpdateRecordCount()
        {
            // Có thể hiển thị số lượng bản ghi ra label nếu muốn
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

        private void FrmKhoaVien_Load(object sender, EventArgs e)
        {
            try
            {
                if (kn.cnn == null || kn.cnn.State != System.Data.ConnectionState.Open)
                {
                    kn.KetNoi_Dulieu();
                }
                if (dataKQ != null)
                    SetupDataGridView();
                Bang_KhoaVien();

                if (txtTimKiem != null)
                    txtTimKiem.Focus();
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
                FrmKhoaVien_ChinhSua f1 = new FrmKhoaVien_ChinhSua();
                if (f1.ShowDialog() == DialogResult.OK)
                {
                    Bang_KhoaVien();
                    ShowMessage("Thêm khoa/viện thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi thêm mới: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemKhoaVien();
        }

        private void TimKiemKhoaVien()
        {
            try
            {
                ShowLoading(true);
                string tuKhoa = txtTimKiem?.Text?.Trim() ?? "";

                if (string.IsNullOrEmpty(tuKhoa))
                {
                    Bang_KhoaVien();
                    return;
                }

                string sql = @"
                    SELECT 
                        DeptID,
                        Code AS [Mã Khoa],
                        Name AS [Tên Khoa/Viện],
                        Office AS [Phòng/Làm việc]
                    FROM Departments
                    WHERE 
                        Name LIKE N'%" + tuKhoa + "%' " +
                    "OR Code LIKE '%" + tuKhoa + "%' " +
                    "OR Office LIKE N'%" + tuKhoa + "%' " +
                    "ORDER BY Code";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                if (dataKQ.Columns["DeptID"] != null)
                {
                    dataKQ.Columns["DeptID"].Visible = false;
                }

                ConfigureColumnWidths();

                if (dta.Rows.Count == 0)
                {
                    ShowMessage($"Không tìm thấy khoa/viện nào với từ khóa '{tuKhoa}'",
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
                    ShowMessage("Vui lòng chọn khoa/viện cần xóa.", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string deptId = dataKQ.CurrentRow.Cells["DeptID"].Value?.ToString();
                string code = dataKQ.CurrentRow.Cells["Mã Khoa"].Value?.ToString();
                string name = dataKQ.CurrentRow.Cells["Tên Khoa/Viện"].Value?.ToString();

                if (string.IsNullOrEmpty(deptId))
                {
                    ShowMessage("Không thể lấy thông tin khoa/viện!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa khoa/viện?\n\n" +
                    $"🏫 Mã Khoa: {code}\n" +
                    $"👤 Tên: {name}\n\n" +
                    $"⚠️ Thao tác này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ShowLoading(true);

                    // Thực hiện xóa
                    string sqlDelete = $"DELETE FROM Departments WHERE DeptID = {deptId}";
                    kn.ThucThiSQL(sqlDelete);

                    Bang_KhoaVien();
                    ShowMessage("Xóa khoa/viện thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi khi xóa khoa/viện: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
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
                    ShowMessage("Vui lòng chọn khoa/viện cần chỉnh sửa!", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string idValue = dataKQ.CurrentRow.Cells["DeptID"].Value?.ToString();

                if (string.IsNullOrEmpty(idValue) || !int.TryParse(idValue, out int deptId))
                {
                    ShowMessage("Không thể lấy thông tin khoa/viện!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                FrmKhoaVien_ChinhSua frm = new FrmKhoaVien_ChinhSua(deptId);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Bang_KhoaVien();
                    ShowMessage("Cập nhật thông tin khoa/viện thành công!", "Thông báo", MessageBoxIcon.Information);
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
                TimKiemKhoaVien();
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
                if (kn?.cnn != null && kn.cnn.State == System.Data.ConnectionState.Open)
                    kn.NgatKetNoi();
            }
            catch { }

            base.OnFormClosed(e);
        }

    }
}