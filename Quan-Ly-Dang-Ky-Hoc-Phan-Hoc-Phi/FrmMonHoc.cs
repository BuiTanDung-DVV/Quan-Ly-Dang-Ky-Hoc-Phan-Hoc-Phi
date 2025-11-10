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
using static System.Collections.Specialized.BitVector32;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmMonHoc : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();

        public FrmMonHoc()
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

                // Style
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

        public void Bang_MonHoc()
        {
            try
            {
                ShowLoading(true);

                string sql = @"
                    SELECT 
                        c.CourseID,
                        c.Code AS [Mã môn],
                        c.Name AS [Tên môn học],
                        c.Credits AS [Số tín chỉ],
                        c.TuitionPerCredit AS [Học phí/tín chỉ],
                        d.Name AS [Khoa/Viện]
                    FROM Courses c
                    LEFT JOIN Departments d ON c.DeptID = d.DeptID
                    ORDER BY c.Code";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                if (dataKQ.Columns["CourseID"] != null)
                    dataKQ.Columns["CourseID"].Visible = false;

                ConfigureColumnWidths();
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu môn học: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
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
                        ["Mã môn"] = (80, DataGridViewAutoSizeColumnMode.None, 60),
                        ["Tên môn học"] = (200, DataGridViewAutoSizeColumnMode.None, 100),
                        ["Số tín chỉ"] = (90, DataGridViewAutoSizeColumnMode.None, 60),
                        ["Học phí/tín chỉ"] = (120, DataGridViewAutoSizeColumnMode.None, 100),
                        ["Khoa/Viện"] = (150, DataGridViewAutoSizeColumnMode.None, 100)
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

                        if (column.Name == "Mã môn" || column.Name == "Số tín chỉ")
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

                var autoFitItem = new ToolStripMenuItem("🔧 Tự động điều chỉnh độ rộng");
                autoFitItem.Click += (s, e) =>
                {
                    foreach (DataGridViewColumn col in dataKQ.Columns)
                    {
                        if (col.Visible && col.Name != "CourseID")
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                };

                var resetItem = new ToolStripMenuItem("↺ Khôi phục kích thước mặc định");
                resetItem.Click += (s, e) => ConfigureColumnWidths();

                columnMenu.Items.Add(autoFitItem);
                columnMenu.Items.Add(resetItem);

                dataKQ.ColumnHeaderMouseClick += (s, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                        columnMenu.Show(dataKQ, dataKQ.PointToClient(Cursor.Position));
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
                this.Cursor = show ? Cursors.WaitCursor : Cursors.Default;
                if (btnToaMoi != null) btnToaMoi.Enabled = !show;
                if (btnSua != null) btnSua.Enabled = !show;
                if (btnXoa != null) btnXoa.Enabled = !show;
                if (btnTimKiem != null) btnTimKiem.Enabled = !show;
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

        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            try
            {
                if (kn.cnn == null || kn.cnn.State != ConnectionState.Open)
                    kn.KetNoi_Dulieu();

                if (dataKQ != null)
                    SetupDataGridView();

                Bang_MonHoc();

                txtTimKiem?.Focus();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi khởi tạo form: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnToaMoi_Click_1(object sender, EventArgs e)
        {
            try
            {
                FrmMonHoc_ChinhSua frm = new FrmMonHoc_ChinhSua();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Bang_MonHoc();
                    ShowMessage("Thêm môn học thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form thêm mới: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataKQ.CurrentRow == null)
                {
                    ShowMessage("Vui lòng chọn môn học cần chỉnh sửa!", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string idValue = dataKQ.CurrentRow.Cells["CourseID"].Value?.ToString();

                if (string.IsNullOrEmpty(idValue) || !int.TryParse(idValue, out int idMonHoc))
                {
                    ShowMessage("Không thể lấy thông tin môn học!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                FrmMonHoc_ChinhSua frm = new FrmMonHoc_ChinhSua(idMonHoc);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Bang_MonHoc();
                    ShowMessage("Cập nhật môn học thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemMonHoc();
        }

        private void TimKiemMonHoc()
        {
            try
            {
                ShowLoading(true);
                string tuKhoa = txtTimKiem?.Text?.Trim() ?? "";

                string sql = @"
                    SELECT 
                        c.CourseID,
                        c.Code AS [Mã môn],
                        c.Name AS [Tên môn học],
                        c.Credits AS [Số tín chỉ],
                        c.TuitionPerCredit AS [Học phí/tín chỉ],
                        d.Name AS [Khoa/Viện]
                    FROM Courses c
                    LEFT JOIN Departments d ON c.DeptID = d.DeptID
                    WHERE c.Name LIKE N'%" + tuKhoa + "%' " +
                          "OR c.Code LIKE '%" + tuKhoa + "%' " +
                          "OR d.Name LIKE N'%" + tuKhoa + "%' " +
                    "ORDER BY c.Code";

                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                if (dataKQ.Columns["CourseID"] != null)
                    dataKQ.Columns["CourseID"].Visible = false;

                ConfigureColumnWidths();
                UpdateRecordCount();

                if (dta.Rows.Count == 0)
                {
                    ShowMessage($"Không tìm thấy môn học nào với từ khóa '{tuKhoa}'",
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
                    ShowMessage("Vui lòng chọn môn học cần xóa.", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string id = dataKQ.CurrentRow.Cells["CourseID"].Value?.ToString();
                string tenMon = dataKQ.CurrentRow.Cells["Tên môn học"].Value?.ToString();

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa môn học này?\n\n📘 {tenMon}\n\n⚠️ Thao tác này không thể hoàn tác!",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string sqlDeleteClassSections = $"DELETE FROM ClassSections WHERE CourseID = {id}";
                    kn.ThucThiSQL(sqlDeleteClassSections);

                    string sql = $"DELETE FROM Courses WHERE CourseID = {id}";
                    kn.ThucThiSQL(sql);

                    Bang_MonHoc();
                    ShowMessage("Xóa môn học thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi khi xóa môn học: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }


        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                TimKiemMonHoc();
        }

        private void dataKQ_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnSua_Click_1(sender, e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                if (kn?.cnn != null && kn.cnn.State == ConnectionState.Open)
                    kn.NgatKetNoi();
            }
            catch { }

            base.OnFormClosed(e);
        }
    }
}
