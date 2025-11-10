using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmLopHocPhan : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();


        public FrmLopHocPhan()
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



        public void Bang_LopHocPhan()
        {
            try
            {
                // 1. Hiển thị trạng thái tải
                ShowLoading(true);

                // 2. Truy vấn SQL cho bảng Lớp Học Phần (ClassSections)
                string sql = @"
            SELECT 
                cs.SectionID,
                cs.SectionCode AS [Mã HP],
                c.Name AS [Tên môn học],
                t.Name AS [Học kỳ],
                l.FullName AS [Giảng viên],
                cs.Schedule AS [Lịch học],
                cs.Room AS [Phòng],
                cs.MaxStudents AS [SV tối đa]
            FROM ClassSections cs
            LEFT JOIN Courses c ON cs.CourseID = c.CourseID          -- Lấy tên môn học
            LEFT JOIN AcademicTerms t ON cs.TermID = t.TermID        -- Lấy tên học kỳ
            LEFT JOIN Lecturers l ON cs.LecturerID = l.LecturerID    -- Lấy tên giảng viên
            ORDER BY cs.SectionCode";

                // 3. Thực thi truy vấn và gán vào DataGridView
                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                // 4. Ẩn cột Khóa chính (SectionID)
                // Cột này được dùng nội bộ nhưng không cần hiển thị
                if (dataKQ.Columns["SectionID"] != null)
                {
                    dataKQ.Columns["SectionID"].Visible = false;
                }

                // 5. Thiết lập độ rộng cột
                ConfigureColumnWidths();

                // 6. Cập nhật số lượng bản ghi
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                ShowMessage($"Lỗi tải dữ liệu lớp học phần: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
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

                    // Định nghĩa cấu hình mới cho các cột của bảng Lớp Học Phần
                    var columnConfig = new Dictionary<string, (int width, DataGridViewAutoSizeColumnMode mode, int minWidth)>
                    {
                        // Cột Khóa chính SectionID đã bị ẩn, không cần cấu hình ở đây

                        ["Mã HP"] = (90, DataGridViewAutoSizeColumnMode.None, 70),
                        ["Tên môn học"] = (250, DataGridViewAutoSizeColumnMode.None, 150), // Cần rộng hơn cho tên môn học dài
                        ["Học kỳ"] = (120, DataGridViewAutoSizeColumnMode.None, 80),
                        ["Giảng viên"] = (150, DataGridViewAutoSizeColumnMode.None, 100),
                        ["Lịch học"] = (100, DataGridViewAutoSizeColumnMode.None, 70),
                        ["Phòng"] = (70, DataGridViewAutoSizeColumnMode.None, 50),
                        ["SV tối đa"] = (70, DataGridViewAutoSizeColumnMode.None, 50)
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

                        // Căn giữa nội dung cho các cột chứa mã số, số lượng hoặc thông tin ngắn gọn
                        if (column.Name == "Mã HP" || column.Name == "Phòng" || column.Name == "SV tối đa" || column.Name == "Lịch học")
                        {
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
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
                        if (col.Visible && col.Name != "ClassSections")
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



        private void FrmLopHocPhan_Load(object sender, EventArgs e)
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

                Bang_LopHocPhan();

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
                FrmLopHocPhan_ChinhSua f1 = new FrmLopHocPhan_ChinhSua();
                if (f1.ShowDialog() == DialogResult.OK)
                {
                    Bang_LopHocPhan();
                    ShowMessage("Thêm thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form thêm mới: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void TimKiemLopHocPhan()
        {
            try
            {
                // 1. Bật trạng thái tải
                ShowLoading(true);

                // 2. Lấy từ khóa tìm kiếm (Sử dụng null-coalescing operator để đảm bảo an toàn)
                string tuKhoa = txtTimKiem?.Text?.Trim() ?? "";

                // 3. Nếu từ khóa rỗng, hiển thị lại toàn bộ bảng
                if (string.IsNullOrEmpty(tuKhoa))
                {
                    Bang_LopHocPhan(); // Giả sử đây là phương thức tải toàn bộ Lớp Học Phần
                    return;
                }

                // 4. Xây dựng truy vấn SQL tìm kiếm cho Lớp Học Phần
                string sql = @"
            SELECT 
                cs.SectionID,
                cs.SectionCode AS [Mã HP],
                c.Name AS [Tên môn học],
                t.Name AS [Học kỳ],
                l.FullName AS [Giảng viên],
                cs.Schedule AS [Lịch học],
                cs.Room AS [Phòng],
                cs.MaxStudents AS [SV tối đa]
            FROM ClassSections cs
            LEFT JOIN Courses c ON cs.CourseID = c.CourseID
            LEFT JOIN AcademicTerms t ON cs.TermID = t.TermID
            LEFT JOIN Lecturers l ON cs.LecturerID = l.LecturerID
            
            -- Thêm điều kiện tìm kiếm WHERE
            WHERE cs.SectionCode LIKE '%" + tuKhoa + "%' " +
                          "OR c.Name LIKE N'%" + tuKhoa + "%' " +           // Tìm theo Tên môn học
                          "OR t.Name LIKE N'%" + tuKhoa + "%' " +           // Tìm theo Học kỳ
                          "OR l.FullName LIKE N'%" + tuKhoa + "%' " +       // Tìm theo Tên giảng viên
                          "OR cs.Room LIKE N'%" + tuKhoa + "%' " +          // Tìm theo Phòng học
                    "ORDER BY cs.SectionCode";

                // 5. Thực thi truy vấn, gán dữ liệu và cấu hình giao diện
                DataTable dta = kn.Lay_DulieuBang(sql);
                dataKQ.DataSource = dta;

                // Ẩn cột SectionID (Khóa chính)
                if (dataKQ.Columns["SectionID"] != null)
                {
                    dataKQ.Columns["SectionID"].Visible = false;
                }

                ConfigureColumnWidths();
                UpdateRecordCount();

                // 6. Thông báo nếu không tìm thấy
                if (dta.Rows.Count == 0)
                {
                    ShowMessage($"Không tìm thấy lớp học phần nào với từ khóa '{tuKhoa}'",
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataKQ.CurrentRow == null)
                {
                    ShowMessage("Vui lòng chọn cần chỉnh sửa!", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                string idValue = dataKQ.CurrentRow.Cells["SectionID"].Value?.ToString();

                if (string.IsNullOrEmpty(idValue) || !int.TryParse(idValue, out int idLopHocPhan))
                {
                    ShowMessage("Không thể lấy thông tin!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                FrmLopHocPhan_ChinhSua f1 = new FrmLopHocPhan_ChinhSua(idLopHocPhan);
                if (f1.ShowDialog() == DialogResult.OK)
                {
                    Bang_LopHocPhan();
                    ShowMessage("Cập nhật thông tin thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemLopHocPhan();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem người dùng đã chọn dòng nào chưa
                if (dataKQ.CurrentRow == null)
                {
                    ShowMessage("Vui lòng chọn lớp học phần cần xóa.", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                // 2. Lấy thông tin cần thiết từ dòng được chọn
                // Lấy SectionID (Khóa chính) để thực hiện thao tác DELETE
                string sectionID = dataKQ.CurrentRow.Cells["SectionID"].Value?.ToString();
                string maHP = dataKQ.CurrentRow.Cells["Mã HP"].Value?.ToString();
                string tenMonHoc = dataKQ.CurrentRow.Cells["Tên môn học"].Value?.ToString();

                if (string.IsNullOrEmpty(sectionID))
                {
                    ShowMessage("Không thể lấy thông tin ID Lớp Học Phần!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                // 3. Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa lớp học phần này?\n\n" +
                    $"🏷️ Mã HP: {maHP}\n" +
                    $"📚 Môn học: {tenMonHoc}\n\n" +
                    $"⚠️ Cảnh báo: Thao tác này sẽ xóa tất cả ghi danh (Enrollments) liên quan và không thể hoàn tác!",
                    "Xác nhận xóa Lớp Học Phần",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ShowLoading(true);

                    string sqlDeleteEnrollments = $"DELETE FROM Enrollments WHERE SectionID = {sectionID}";
                    kn.ThucThiSQL(sqlDeleteEnrollments);

                    string sqlDeleteSection = $"DELETE FROM ClassSections WHERE SectionID = {sectionID}";
                    kn.ThucThiSQL(sqlDeleteSection);

                    // Cập nhật lại danh sách trên giao diện
                    Bang_LopHocPhan(); // Giả sử đây là phương thức tải lại toàn bộ bảng Lớp Học Phần
                    ShowMessage("Xóa lớp học phần thành công!", "Thông báo", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi khi xóa lớp học phần: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
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
                TimKiemLopHocPhan();
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

        private void uiTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
