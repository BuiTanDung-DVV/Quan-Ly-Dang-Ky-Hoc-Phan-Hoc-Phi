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


        public void Bang_NganhHoc()
        {
            try
            {
                // 1. Hiển thị trạng thái tải
                ShowLoading(true);

                // 2. Truy vấn SQL cho bảng Ngành Học (Majors)
                string sql = @"
            SELECT 
                m.MajorID,
                m.Code AS [Mã Ngành],
                m.Name AS [Tên Ngành],
                d.Name AS [Thuộc Khoa/Viện]
            FROM Majors m
            LEFT JOIN Departments d ON m.DeptID = d.DeptID  -- Lấy tên Khoa/Viện liên quan
            ORDER BY m.Code";

                // 3. Thực thi truy vấn và gán vào DataGridView
                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                // 4. Ẩn cột Khóa chính (MajorID)
                // Cột này được dùng nội bộ nhưng không cần hiển thị
                if (dataKQ.Columns["MajorID"] != null)
                {
                    dataKQ.Columns["MajorID"].Visible = false;
                }

                // 5. Thiết lập độ rộng cột (Cần có phương thức ConfigureColumnWidths() riêng cho bảng này)
                ConfigureColumnWidths();

                // 6. Cập nhật số lượng bản ghi
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                ShowMessage($"Lỗi tải dữ liệu ngành học: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
            finally
            {
                // Kết thúc trạng thái tải (luôn chạy)
                ShowLoading(false);
            }
        }

        private void ConfigureColumnWidths()
        {
            try
            {
                // Đảm bảo dataKQ đã có dữ liệu (có cột)
                if (dataKQ.Columns.Count > 0)
                {
                    // Cho phép người dùng tự thay đổi kích thước cột
                    dataKQ.AllowUserToResizeColumns = true;
                    dataKQ.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

                    // Định nghĩa cấu hình mới cho các cột của bảng Ngành Học
                    var columnConfig = new Dictionary<string, (int width, DataGridViewAutoSizeColumnMode mode, int minWidth)>
                    {
                        // Cột Khóa chính MajorID đã bị ẩn, không cần cấu hình ở đây

                        ["Mã Ngành"] = (100, DataGridViewAutoSizeColumnMode.None, 80),
                        ["Tên Ngành"] = (250, DataGridViewAutoSizeColumnMode.Fill, 150), // Dùng Fill để tự động mở rộng
                        ["Thuộc Khoa/Viện"] = (200, DataGridViewAutoSizeColumnMode.None, 150)
                    };

                    // Lặp qua tất cả các cột để áp dụng cấu hình
                    foreach (DataGridViewColumn column in dataKQ.Columns)
                    {
                        if (columnConfig.ContainsKey(column.Name))
                        {
                            var config = columnConfig[column.Name];
                            column.Width = config.width;
                            column.AutoSizeMode = config.mode;
                            column.MinimumWidth = config.minWidth;
                            column.Resizable = DataGridViewTriState.True; // Cho phép thay đổi kích thước
                        }

                        // Căn giữa nội dung cho các cột chứa mã số hoặc thông tin ngắn gọn
                        if (column.Name == "Mã Ngành")
                        {
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        // Căn trái mặc định cho Tên Ngành và Khoa/Viện
                    }

                    // Gọi phương thức để thêm menu chuột phải (ContextMenu)
                    AddColumnContextMenu();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
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
                        if (col.Visible && col.Name != "MajorID")
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

        private void FrmNganh_Load(object sender, EventArgs e)
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

                Bang_NganhHoc();

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
                FrmNganhHoc_ChinhSua f1 = new FrmNganhHoc_ChinhSua();
                if (f1.ShowDialog() == DialogResult.OK)
                {
                    Bang_NganhHoc();
                    ShowMessage("Thêm thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form thêm mới: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataKQ.CurrentRow == null)
                {
                    ShowMessage("Vui lòng chọn cần chỉnh sửa!", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string idValue = dataKQ.CurrentRow.Cells["MajorID"].Value?.ToString();

                if (string.IsNullOrEmpty(idValue) || !int.TryParse(idValue, out int idNganhHoc))
                {
                    ShowMessage("Không thể lấy thông tin!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                FrmNganhHoc_ChinhSua f1 = new FrmNganhHoc_ChinhSua(idNganhHoc);
                if (f1.ShowDialog() == DialogResult.OK)
                {
                    Bang_NganhHoc();
                    ShowMessage("Cập nhật thông tin thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }
        private void TimKiemNganhHoc()
        {
            try
            {
                // 1. Bật trạng thái tải
                ShowLoading(true);

                // 2. Lấy từ khóa tìm kiếm
                string tuKhoa = txtTimKiem?.Text?.Trim() ?? "";

                // 3. Nếu từ khóa rỗng, hiển thị lại toàn bộ bảng Ngành Học
                if (string.IsNullOrEmpty(tuKhoa))
                {
                    Bang_NganhHoc(); // Phương thức tải toàn bộ bảng Ngành Học
                    return;
                }

                // 4. Xây dựng truy vấn SQL tìm kiếm cho Ngành Học (Majors)
                string sql = @"
            SELECT 
                m.MajorID,
                m.Code AS [Mã Ngành],
                m.Name AS [Tên Ngành],
                d.Name AS [Thuộc Khoa/Viện]
            FROM Majors m
            LEFT JOIN Departments d ON m.DeptID = d.DeptID  -- Lấy tên Khoa/Viện
            
            -- Thêm điều kiện tìm kiếm WHERE
            WHERE m.Code LIKE '%" + tuKhoa + "%' " +           // Tìm theo Mã Ngành
                          "OR m.Name LIKE N'%" + tuKhoa + "%' " +       // Tìm theo Tên Ngành
                          "OR d.Name LIKE N'%" + tuKhoa + "%' " +       // Tìm theo Tên Khoa/Viện
                    "ORDER BY m.Code";

                // 5. Thực thi truy vấn, gán dữ liệu và cấu hình giao diện
                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                // Ẩn cột MajorID (Khóa chính)
                if (dataKQ.Columns["MajorID"] != null)
                {
                    dataKQ.Columns["MajorID"].Visible = false;
                }

                ConfigureColumnWidths();
                UpdateRecordCount();

                // 6. Thông báo nếu không tìm thấy
                if (dta.Rows.Count == 0)
                {
                    ShowMessage($"Không tìm thấy ngành học nào với từ khóa '{tuKhoa}'",
                                "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                ShowMessage($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
            finally
            {
                // Tắt trạng thái tải
                ShowLoading(false);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemNganhHoc();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem người dùng đã chọn dòng nào chưa
                if (dataKQ.CurrentRow == null)
                {
                    ShowMessage("Vui lòng chọn ngành học cần xóa.", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                // 2. Lấy thông tin cần thiết từ dòng được chọn
                // Lấy MajorID (Khóa chính) để thực hiện thao tác DELETE
                string majorID = dataKQ.CurrentRow.Cells["MajorID"].Value?.ToString();
                string maNganh = dataKQ.CurrentRow.Cells["Mã Ngành"].Value?.ToString();
                string tenNganh = dataKQ.CurrentRow.Cells["Tên Ngành"].Value?.ToString();

                if (string.IsNullOrEmpty(majorID))
                {
                    ShowMessage("Không thể lấy thông tin ID Ngành Học!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                // 3. Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa ngành học này?\n\n" +
                    $"🏷️ Mã Ngành: {maNganh}\n" +
                    $"📚 Tên Ngành: {tenNganh}\n\n" +
                    $"⚠️ Cảnh báo: Thao tác này có thể bị lỗi nếu còn sinh viên hoặc môn học thuộc ngành này!",
                    "Xác nhận xóa Ngành Học",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ShowLoading(true);

                    string sqlDeleteMajor = $"DELETE FROM Majors WHERE MajorID = {majorID}";
                    kn.ThucThiSQL(sqlDeleteMajor);

                    // Cập nhật lại danh sách trên giao diện
                    Bang_NganhHoc(); // Giả sử đây là phương thức tải lại toàn bộ bảng Ngành Học
                    ShowMessage("Xóa ngành học thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Thông báo lỗi chi tiết hơn nếu là lỗi Khóa ngoại (Foreign Key)
                string errorMessage = ex.Message;
                if (errorMessage.Contains("REFERENCE constraint") || errorMessage.Contains("foreign key"))
                {
                    ShowMessage($"Lỗi ràng buộc: Không thể xóa ngành học này vì vẫn còn dữ liệu (như sinh viên hoặc môn học) đang liên kết với nó. Vui lòng xóa hoặc gán lại các dữ liệu liên quan trước.", "Lỗi Ràng Buộc", MessageBoxIcon.Error);
                }
                else
                {
                    ShowMessage($"Lỗi khi xóa ngành học: {errorMessage}", "Lỗi", MessageBoxIcon.Error);
                }
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void txtTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TimKiemNganhHoc();
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
