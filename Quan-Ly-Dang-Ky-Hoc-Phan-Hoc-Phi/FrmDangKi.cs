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
    public partial class FrmDangKi : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();
        private int currentStudentID = 0;
        private int selectedTermID = 0;
        private Timer searchTimer;
        private Dictionary<string, Color> statusColors;
        private BackgroundWorker dataLoader;

        public FrmDangKi()
        {
            InitializeComponent();
            InitializeEnhancements();
        }

        private void InitializeEnhancements()
        {
            // Khởi tạo timer cho tìm kiếm real-time
            searchTimer = new Timer();
            searchTimer.Interval = 500; // 500ms delay
            searchTimer.Tick += SearchTimer_Tick;

            // Khởi tạo màu sắc cho trạng thái
            statusColors = new Dictionary<string, Color>
            {
                {"Đang học", Color.FromArgb(46, 204, 113)},
                {"Đã duyệt", Color.FromArgb(52, 152, 219)},
                {"Chờ duyệt", Color.FromArgb(241, 196, 15)},
                {"Đã hủy", Color.FromArgb(231, 76, 60)},
                {"Full", Color.FromArgb(231, 76, 60)},
                {"Available", Color.FromArgb(46, 204, 113)}
            };

            // Khởi tạo BackgroundWorker cho việc load dữ liệu
            dataLoader = new BackgroundWorker();
            dataLoader.WorkerSupportsCancellation = true;
            dataLoader.DoWork += DataLoader_DoWork;
            dataLoader.RunWorkerCompleted += DataLoader_RunWorkerCompleted;

            // Setup enhanced event handlers với null check
            if (txtTimKiem != null)
            {
                txtTimKiem.TextChanged += TxtTimKiem_TextChanged;
                txtTimKiem.KeyDown += TxtTimKiem_KeyDown;
            }
        }

        private void FrmDangKi_Load(object sender, EventArgs e)
        {
            try
            {
                ShowLoadingIndicator(true);
                LoadStudentInfo();
                SetupEnhancedDataGridViews();
                LoadTermsWithIcons();
                LoadDepartmentsWithIcons();
                
                // Load dữ liệu không đồng bộ với error handling
                this.BeginInvoke(new Action(() => {
                    try
                    {
                        LoadAvailableClassesAsync();
                        LoadRegisteredClassesAsync();
                        UpdateEnhancedSummary();
                    }
                    catch (Exception ex)
                    {
                        ShowError("Lỗi load dữ liệu", ex.Message);
                    }
                    finally
                    {
                        ShowLoadingIndicator(false);
                    }
                }));
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khởi tạo form", ex.Message);
                ShowLoadingIndicator(false);
            }
        }

        #region Enhanced Setup Methods
        private void LoadStudentInfo()
        {
            try
            {
                if (UserSession.IsStudent() && UserSession.LinkedStudentID.HasValue)
                {
                    currentStudentID = UserSession.LinkedStudentID.Value;
                    
                    string sql = $@"
                        SELECT s.StudentCode, s.FullName, d.Name as DeptName, s.Email, s.Phone
                        FROM Students s
                        LEFT JOIN Departments d ON s.DeptID = d.DeptID
                        WHERE s.StudentID = {currentStudentID}";

                    DataTable dt = kn.Lay_DulieuBang(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        if (lblSinhVien != null)
                        {
                            lblSinhVien.Text = $"👨‍🎓 {row["StudentCode"]} - {row["FullName"]} ({row["DeptName"]})";
                        }
                        
                        // Cập nhật title với tên sinh viên
                        if (lblTitle != null)
                        {
                            lblTitle.Text = $"ĐĂNG KÝ TÍN CHỈ - {row["FullName"].ToString().ToUpper()}";
                        }
                    }
                    else
                    {
                        throw new Exception("Không tìm thấy thông tin sinh viên");
                    }
                }
                else
                {
                    if (lblSinhVien != null)
                        lblSinhVien.Text = "⚠️ Thông tin sinh viên không hợp lệ";
                    
                    this.Enabled = false;
                    ShowError("Lỗi quyền truy cập", "Bạn không có quyền truy cập chức năng này!");
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi load thông tin sinh viên", ex.Message);
                if (lblSinhVien != null)
                    lblSinhVien.Text = "❌ Lỗi tải thông tin sinh viên";
            }
        }

        private void LoadTermsWithIcons()
        {
            try
            {
                string sql = @"
                    SELECT TermID, Name, StartDate, EndDate,
                           CASE 
                               WHEN GETDATE() BETWEEN StartDate AND EndDate THEN N'📚 ' + Name + N' (Hiện tại)'
                               WHEN GETDATE() < StartDate THEN N'⏳ ' + Name + N' (Sắp tới)'
                               ELSE N'📅 ' + Name
                           END as DisplayName
                    FROM AcademicTerms 
                    ORDER BY StartDate DESC";
                
                DataTable dt = kn.Lay_DulieuBang(sql);
                
                if (cboHocKy != null && dt != null && dt.Rows.Count > 0)
                {
                    cboHocKy.DisplayMember = "DisplayName";
                    cboHocKy.ValueMember = "TermID";
                    cboHocKy.DataSource = dt;
                    
                    // Tự động chọn học kỳ hiện tại
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["DisplayName"].ToString().Contains("(Hiện tại)"))
                        {
                            cboHocKy.SelectedValue = row["TermID"];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi load học kỳ", ex.Message);
            }
        }

        private void LoadDepartmentsWithIcons()
        {
            try
            {
                string sql = @"
                    SELECT DeptID, 
                           CASE 
                               WHEN Name LIKE N'%Công nghệ%' THEN N'💻 ' + Name
                               WHEN Name LIKE N'%Kinh tế%' THEN N'💰 ' + Name
                               WHEN Name LIKE N'%Y%' THEN N'🏥 ' + Name
                               WHEN Name LIKE N'%Ngoại ngữ%' THEN N'🌐 ' + Name
                               ELSE N'🏛️ ' + Name
                           END as DisplayName
                    FROM Departments 
                    ORDER BY Name";
                
                DataTable dt = kn.Lay_DulieuBang(sql);
                
                if (dt != null && cboKhoa != null)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["DeptID"] = 0;
                    newRow["DisplayName"] = "🔄 Tất cả khoa";
                    dt.Rows.InsertAt(newRow, 0);
                    
                    cboKhoa.DisplayMember = "DisplayName";
                    cboKhoa.ValueMember = "DeptID";
                    cboKhoa.DataSource = dt;
                    cboKhoa.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi load khoa", ex.Message);
            }
        }

        private void SetupEnhancedDataGridViews()
        {
            try
            {
                // Enhanced setup cho dgvLopHocPhan với null check
                if (dgvLopHocPhan != null)
                {
                    dgvLopHocPhan.Columns.Clear();
                    dgvLopHocPhan.AllowUserToResizeColumns = true;
                    dgvLopHocPhan.AllowUserToOrderColumns = true;
                    dgvLopHocPhan.EnableHeadersVisualStyles = false;
                    
                    // Tạo columns với styling đẹp hơn
                    var columns = new[]
                    {
                        new { Name = "SectionID", Header = "Mã LHP", Width = 80, Visible = false },
                        new { Name = "CourseCode", Header = "📖 Mã môn", Width = 100, Visible = true },
                        new { Name = "CourseName", Header = "📚 Tên môn học", Width = 200, Visible = true },
                        new { Name = "Credits", Header = "⭐ Tín chỉ", Width = 70, Visible = true },
                        new { Name = "LecturerName", Header = "👨‍🏫 Giảng viên", Width = 150, Visible = true },
                        new { Name = "Schedule", Header = "⏰ Lịch học", Width = 120, Visible = true },
                        new { Name = "Room", Header = "🏢 Phòng", Width = 80, Visible = true },
                        new { Name = "MaxStudents", Header = "👥 Sĩ số", Width = 70, Visible = true },
                        new { Name = "CurrentStudents", Header = "✅ Đã ĐK", Width = 70, Visible = true },
                        new { Name = "TuitionPerCredit", Header = "💰 Phí/TC", Width = 100, Visible = true },
                        new { Name = "TotalTuition", Header = "💵 Tổng phí", Width = 100, Visible = true }
                    };

                    foreach (var col in columns)
                    {
                        var column = new DataGridViewTextBoxColumn
                        {
                            Name = col.Name,
                            HeaderText = col.Header,
                            Width = col.Width,
                            Visible = col.Visible
                        };

                        if (col.Name.Contains("Tuition"))
                        {
                            column.DefaultCellStyle.Format = "#,##0 VNĐ";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            column.DefaultCellStyle.ForeColor = Color.DarkGreen;
                            column.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        }

                        dgvLopHocPhan.Columns.Add(column);
                    }
                }

                // Enhanced setup cho dgvDaDangKy với null check
                if (dgvDaDangKy != null)
                {
                    dgvDaDangKy.Columns.Clear();
                    dgvDaDangKy.EnableHeadersVisualStyles = false;

                    var registeredColumns = new[]
                    {
                        new { Name = "EnrollmentID", Header = "ID", Width = 50, Visible = false },
                        new { Name = "SectionID", Header = "Mã LHP", Width = 80, Visible = false },
                        new { Name = "CourseCode", Header = "📖 Mã môn", Width = 80, Visible = true },
                        new { Name = "CourseName", Header = "📚 Môn học", Width = 150, Visible = true },
                        new { Name = "Credits", Header = "⭐ TC", Width = 40, Visible = true },
                        new { Name = "Schedule", Header = "⏰ Lịch", Width = 100, Visible = true },
                        new { Name = "Room", Header = "🏢 Phòng", Width = 60, Visible = true },
                        new { Name = "Status", Header = "📊 Trạng thái", Width = 100, Visible = true },
                        new { Name = "RegisterDate", Header = "📅 Ngày ĐK", Width = 90, Visible = true }
                    };

                    foreach (var col in registeredColumns)
                    {
                        var column = new DataGridViewTextBoxColumn
                        {
                            Name = col.Name,
                            HeaderText = col.Header,
                            Width = col.Width,
                            Visible = col.Visible
                        };

                        if (col.Name == "RegisterDate")
                        {
                            column.DefaultCellStyle.Format = "dd/MM/yyyy";
                        }

                        dgvDaDangKy.Columns.Add(column);
                    }
                }

                // Style headers với null check
                foreach (DataGridView dgv in new[] { dgvLopHocPhan, dgvDaDangKy })
                {
                    if (dgv != null)
                    {
                        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
                        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgv.ColumnHeadersHeight = 35;
                        dgv.RowTemplate.Height = 30;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi setup DataGridView", ex.Message);
            }
        }
        #endregion

        #region Enhanced Data Loading Methods
        private void LoadAvailableClassesAsync()
        {
            try
            {
                if (dgvLopHocPhan == null || dgvLopHocPhan.Columns.Count == 0 || 
                    cboHocKy == null || cboHocKy.SelectedValue == null) 
                    return;
                
                selectedTermID = Convert.ToInt32(cboHocKy.SelectedValue);
                
                string sql = $@"
                    SELECT 
                        cs.SectionID,
                        c.Code as CourseCode,
                        c.Name as CourseName,
                        c.Credits,
                        l.FullName as LecturerName,
                        cs.Schedule,
                        cs.Room,
                        cs.MaxStudents,
                        ISNULL(enrolled.CurrentStudents, 0) as CurrentStudents,
                        c.TuitionPerCredit,
                        c.Credits * c.TuitionPerCredit as TotalTuition,
                        c.DeptID,
                        CASE 
                            WHEN ISNULL(enrolled.CurrentStudents, 0) >= cs.MaxStudents THEN N'Full'
                            ELSE N'Available'
                        END as AvailabilityStatus
                    FROM ClassSections cs
                    JOIN Courses c ON cs.CourseID = c.CourseID
                    JOIN Lecturers l ON cs.LecturerID = l.LecturerID
                    LEFT JOIN (
                        SELECT SectionID, COUNT(*) as CurrentStudents
                        FROM Enrollments 
                        WHERE Status IN (N'Đang học', N'Đã duyệt')
                        GROUP BY SectionID
                    ) enrolled ON cs.SectionID = enrolled.SectionID
                    WHERE cs.TermID = {selectedTermID}";

                // Áp dụng bộ lọc với null check
                if (cboKhoa != null && cboKhoa.SelectedValue != null && Convert.ToInt32(cboKhoa.SelectedValue) > 0)
                {
                    sql += $" AND c.DeptID = {cboKhoa.SelectedValue}";
                }

                if (txtTimKiem != null && !string.IsNullOrEmpty(txtTimKiem.Text.Trim()))
                {
                    string searchText = txtTimKiem.Text.Trim().Replace("'", "''");
                    sql += $" AND (c.Code LIKE '%{searchText}%' OR c.Name LIKE N'%{searchText}%' OR l.FullName LIKE N'%{searchText}%')";
                }

                sql += $@" AND cs.SectionID NOT IN (
                    SELECT SectionID FROM Enrollments 
                    WHERE StudentID = {currentStudentID} 
                        AND Status IN (N'Đang học', N'Đã duyệt')
                )";

                sql += " ORDER BY c.Code, cs.SectionID";

                DataTable dt = kn.Lay_DulieuBang(sql);
                
                if (dt != null && dgvLopHocPhan != null)
                {
                    dgvLopHocPhan.Rows.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        int rowIndex = dgvLopHocPhan.Rows.Add(
                            row["SectionID"],
                            row["CourseCode"],
                            row["CourseName"],
                            row["Credits"],
                            row["LecturerName"],
                            row["Schedule"],
                            row["Room"],
                            row["MaxStudents"],
                            row["CurrentStudents"],
                            row["TuitionPerCredit"],
                            row["TotalTuition"]
                        );

                        // Enhanced styling dựa trên trạng thái
                        string status = row["AvailabilityStatus"].ToString();
                        DataGridViewRow dgvRow = dgvLopHocPhan.Rows[rowIndex];

                        if (status == "Full")
                        {
                            dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                            dgvRow.DefaultCellStyle.ForeColor = Color.FromArgb(180, 0, 0);
                            dgvRow.Cells["CurrentStudents"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                            dgvRow.Cells["CurrentStudents"].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                            dgvRow.Cells["CurrentStudents"].Style.ForeColor = Color.Green;
                        }

                        // Highlight high tuition
                        decimal totalTuition = Convert.ToDecimal(row["TotalTuition"]);
                        if (totalTuition > 2000000) // > 2 triệu
                        {
                            dgvRow.Cells["TotalTuition"].Style.BackColor = Color.FromArgb(255, 240, 220);
                            dgvRow.Cells["TotalTuition"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        }
                    }

                    // Cập nhật số lượng môn có thể đăng ký
                    UpdateAvailableClassesCount(dt.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi load lớp học phần", ex.Message);
            }
        }

        private void LoadRegisteredClassesAsync()
        {
            try
            {
                if (dgvDaDangKy == null || dgvDaDangKy.Columns.Count == 0 || selectedTermID == 0) 
                    return;

                string sql = $@"
                    SELECT 
                        e.EnrollmentID,
                        e.SectionID,
                        c.Code as CourseCode,
                        c.Name as CourseName,
                        c.Credits,
                        cs.Schedule,
                        cs.Room,
                        e.Status,
                        e.RegisterDate
                    FROM Enrollments e
                    JOIN ClassSections cs ON e.SectionID = cs.SectionID
                    JOIN Courses c ON cs.CourseID = c.CourseID
                    WHERE e.StudentID = {currentStudentID} 
                        AND cs.TermID = {selectedTermID}
                        AND e.Status IN (N'Đang học', N'Đã duyệt', N'Chờ duyệt')
                    ORDER BY e.RegisterDate DESC";

                DataTable dt = kn.Lay_DulieuBang(sql);
                
                if (dt != null)
                {
                    dgvDaDangKy.Rows.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        int rowIndex = dgvDaDangKy.Rows.Add(
                            row["EnrollmentID"],
                            row["SectionID"],
                            row["CourseCode"],
                            row["CourseName"],
                            row["Credits"],
                            row["Schedule"],
                            row["Room"],
                            row["Status"],
                            row["RegisterDate"]
                        );

                        // Enhanced styling dựa trên trạng thái
                        string status = row["Status"].ToString();
                        DataGridViewRow dgvRow = dgvDaDangKy.Rows[rowIndex];

                        if (statusColors.ContainsKey(status))
                        {
                            dgvRow.Cells["Status"].Style.BackColor = statusColors[status];
                            dgvRow.Cells["Status"].Style.ForeColor = Color.White;
                            dgvRow.Cells["Status"].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                        }

                        // Highlight recent registrations
                        DateTime regDate = Convert.ToDateTime(row["RegisterDate"]);
                        if ((DateTime.Now - regDate).TotalDays <= 1)
                        {
                            dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
                            dgvRow.Cells["RegisterDate"].Style.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
                            dgvRow.Cells["RegisterDate"].Style.ForeColor = Color.Blue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi load lớp đã đăng ký", ex.Message);
            }
        }

        private void UpdateEnhancedSummary()
        {
            try
            {
                if (dgvDaDangKy == null) return;

                int totalCredits = 0;
                decimal totalTuition = 0;
                int totalClasses = 0;

                foreach (DataGridViewRow row in dgvDaDangKy.Rows)
                {
                    if (row.Cells["Credits"].Value != null)
                    {
                        totalCredits += Convert.ToInt32(row.Cells["Credits"].Value);
                        totalClasses++;
                    }
                }

                // Tính học phí từ database để đảm bảo chính xác
                string sql = $@"
                    SELECT 
                        ISNULL(SUM(c.Credits * c.TuitionPerCredit), 0) as TotalTuition,
                        COUNT(*) as ClassCount
                    FROM Enrollments e
                    JOIN ClassSections cs ON e.SectionID = cs.SectionID
                    JOIN Courses c ON cs.CourseID = c.CourseID
                    WHERE e.StudentID = {currentStudentID} 
                        AND cs.TermID = {selectedTermID}
                        AND e.Status IN (N'Đang học', N'Đã duyệt')";

                DataTable dt = kn.Lay_DulieuBang(sql);
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["TotalTuition"] != DBNull.Value)
                {
                    totalTuition = Convert.ToDecimal(dt.Rows[0]["TotalTuition"]);
                }

                // Cập nhật labels với emoji và màu sắc
                if (lblTongTinChi != null)
                {
                    lblTongTinChi.Text = $"⭐ Tổng tín chỉ: {totalCredits} TC ({totalClasses} môn)";
                    lblTongTinChi.ForeColor = totalCredits > 22 ? Color.Red : Color.Blue;
                }

                if (lblTongHocPhi != null)
                {
                    lblTongHocPhi.Text = $"💰 Tổng học phí: {totalTuition:N0} VNĐ";
                    lblTongHocPhi.ForeColor = totalTuition > 8000000 ? Color.Red : Color.Green;
                }

                // Hiển thị cảnh báo nếu cần
                ShowWarnings(totalCredits, totalTuition);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi cập nhật thống kê", ex.Message);
            }
        }
        #endregion

        #region Enhanced Event Handlers với Real-time Features
        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Reset timer mỗi khi user gõ
                if (searchTimer != null)
                {
                    searchTimer.Stop();
                    searchTimer.Start();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in TxtTimKiem_TextChanged: " + ex.Message);
            }
        }

        private void TxtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (searchTimer != null)
                        searchTimer.Stop();
                    LoadAvailableClassesAsync();
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in TxtTimKiem_KeyDown: " + ex.Message);
            }
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (searchTimer != null)
                    searchTimer.Stop();
                LoadAvailableClassesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in SearchTimer_Tick: " + ex.Message);
            }
        }

        private void cboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboHocKy != null && cboHocKy.SelectedValue != null)
                {
                    LoadAvailableClassesAsync();
                    LoadRegisteredClassesAsync();
                    UpdateEnhancedSummary();
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi thay đổi học kỳ", ex.Message);
            }
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadAvailableClassesAsync();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi thay đổi khoa", ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadAvailableClassesAsync();
                if (dgvLopHocPhan != null)
                {
                    ShowInfo("Tìm kiếm hoàn tất", $"Tìm thấy {dgvLopHocPhan.Rows.Count} lớp học phần");
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tìm kiếm", ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTimKiem != null)
                    txtTimKiem.Text = "";
                if (cboKhoa != null)
                    cboKhoa.SelectedIndex = 0;
                
                LoadAvailableClassesAsync();
                LoadRegisteredClassesAsync();
                UpdateEnhancedSummary();
                ShowInfo("Làm mới thành công", "Dữ liệu đã được cập nhật");
            }
            catch (Exception ex)
            {
                ShowError("Lỗi làm mới", ex.Message);
            }
        }

        private void dgvLopHocPhan_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvLopHocPhan != null && btnDangKy != null)
                {
                    bool hasSelection = dgvLopHocPhan.CurrentRow != null;
                    btnDangKy.Enabled = hasSelection;
                    
                    if (hasSelection)
                    {
                        ShowClassDetails(dgvLopHocPhan.CurrentRow);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in dgvLopHocPhan_SelectionChanged: " + ex.Message);
            }
        }

        private void dgvDaDangKy_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvDaDangKy != null && btnHuyDangKy != null)
                {
                    bool hasSelection = dgvDaDangKy.CurrentRow != null;
                    btnHuyDangKy.Enabled = hasSelection;
                    
                    if (hasSelection)
                    {
                        string status = dgvDaDangKy.CurrentRow.Cells["Status"].Value?.ToString();
                        btnHuyDangKy.Enabled = status != "Đã hủy";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in dgvDaDangKy_SelectionChanged: " + ex.Message);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLopHocPhan == null || dgvLopHocPhan.CurrentRow == null)
                {
                    ShowWarning("Chưa chọn lớp", "Vui lòng chọn lớp học phần cần đăng ký!");
                    return;
                }

                int sectionID = Convert.ToInt32(dgvLopHocPhan.CurrentRow.Cells["SectionID"].Value);
                string courseName = dgvLopHocPhan.CurrentRow.Cells["CourseName"].Value?.ToString() ?? "";
                string courseCode = dgvLopHocPhan.CurrentRow.Cells["CourseCode"].Value?.ToString() ?? "";
                int credits = Convert.ToInt32(dgvLopHocPhan.CurrentRow.Cells["Credits"].Value);
                decimal tuition = Convert.ToDecimal(dgvLopHocPhan.CurrentRow.Cells["TotalTuition"].Value);

                // Kiểm tra điều kiện đăng ký
                if (!ValidateRegistration(credits, tuition))
                {
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"🎓 Xác nhận đăng ký:\n\n" +
                    $"📖 Môn: {courseCode} - {courseName}\n" +
                    $"⭐ Tín chỉ: {credits}\n" +
                    $"💰 Học phí: {tuition:N0} VNĐ\n\n" +
                    $"Bạn có chắc chắn muốn đăng ký?", 
                    "Xác nhận đăng ký", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    RegisterForClass(sectionID, courseName);
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi đăng ký", ex.Message);
            }
        }

        private void btnHuyDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDaDangKy == null || dgvDaDangKy.CurrentRow == null)
                {
                    ShowWarning("Chưa chọn lớp", "Vui lòng chọn lớp cần hủy đăng ký!");
                    return;
                }

                int enrollmentID = Convert.ToInt32(dgvDaDangKy.CurrentRow.Cells["EnrollmentID"].Value);
                string courseName = dgvDaDangKy.CurrentRow.Cells["CourseName"].Value?.ToString() ?? "";
                string courseCode = dgvDaDangKy.CurrentRow.Cells["CourseCode"].Value?.ToString() ?? "";

                DialogResult result = MessageBox.Show(
                    $"⚠️ Xác nhận hủy đăng ký:\n\n" +
                    $"📖 Môn: {courseCode} - {courseName}\n\n" +
                    $"Bạn có chắc chắn muốn hủy đăng ký?", 
                    "Xác nhận hủy đăng ký", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    UnregisterFromClass(enrollmentID, courseName);
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi hủy đăng ký", ex.Message);
            }
        }

        private void btnXemLichHoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDaDangKy == null || dgvDaDangKy.Rows.Count == 0)
                {
                    ShowInfo("Chưa có lịch học", "Bạn chưa đăng ký lớp học phần nào!");
                    return;
                }

                ShowScheduleDialog();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi xem lịch học", ex.Message);
            }
        }
        #endregion

        #region Enhanced Helper Methods
        private bool ValidateRegistration(int credits, decimal tuition)
        {
            try
            {
                if (dgvDaDangKy == null) return false;

                // Kiểm tra giới hạn tín chỉ
                int currentCredits = 0;
                foreach (DataGridViewRow row in dgvDaDangKy.Rows)
                {
                    if (row.Cells["Credits"].Value != null)
                    {
                        currentCredits += Convert.ToInt32(row.Cells["Credits"].Value);
                    }
                }

                if (currentCredits + credits > 25)
                {
                    ShowWarning("Vượt giới hạn tín chỉ", 
                        $"Bạn đã đăng ký {currentCredits} tín chỉ.\n" +
                        $"Không thể đăng ký thêm {credits} tín chỉ (giới hạn 25 TC/học kỳ)");
                    return false;
                }

                // Kiểm tra học phí
                decimal currentTuition = 0;
                try
                {
                    string sql = $@"
                        SELECT ISNULL(SUM(c.Credits * c.TuitionPerCredit), 0) as TotalTuition
                        FROM Enrollments e
                        JOIN ClassSections cs ON e.SectionID = cs.SectionID
                        JOIN Courses c ON cs.CourseID = c.CourseID
                        WHERE e.StudentID = {currentStudentID} 
                            AND cs.TermID = {selectedTermID}
                            AND e.Status IN (N'Đang học', N'Đã duyệt')";

                    DataTable dt = kn.Lay_DulieuBang(sql);
                    if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["TotalTuition"] != DBNull.Value)
                    {
                        currentTuition = Convert.ToDecimal(dt.Rows[0]["TotalTuition"]);
                    }
                }
                catch { }

                if (currentTuition + tuition > 10000000) // 10 triệu
                {
                    DialogResult result = MessageBox.Show(
                        $"⚠️ Cảnh báo học phí cao:\n\n" +
                        $"Học phí hiện tại: {currentTuition:N0} VNĐ\n" +
                        $"Học phí sau khi đăng ký: {(currentTuition + tuition):N0} VNĐ\n\n" +
                        $"Bạn có muốn tiếp tục?", 
                        "Cảnh báo học phí", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Warning);
                    
                    return result == DialogResult.Yes;
                }

                return true;
            }
            catch (Exception ex)
            {
                ShowError("Lỗi kiểm tra đăng ký", ex.Message);
                return false;
            }
        }

        private void CreateOrUpdateInvoice()
        {
            // Trigger tự động xử lý - không cần code phức tạp
            try
            {
                System.Diagnostics.Debug.WriteLine($"✅ Invoice auto-updated by trigger for Student {currentStudentID}, Term {selectedTermID}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("⚠️ Invoice trigger notification error: " + ex.Message);
            }
        }

        private void RegisterForClass(int sectionID, string courseName)
        {
            try
            {
                ShowLoadingIndicator(true, "Đang đăng ký...");

                // Chỉ cần INSERT vào Enrollments - trigger sẽ tự động xử lý hóa đơn
                string insertSql = $@"
                    INSERT INTO Enrollments (StudentID, SectionID, RegisterDate, Status)
                    VALUES ({currentStudentID}, {sectionID}, GETDATE(), N'Đang học')";

                kn.ThucThiSQL(insertSql);

                ShowSuccess("Đăng ký thành công", 
                    $"✅ Đã đăng ký môn '{courseName}' thành công!\n" +
                    $"💰 Hóa đơn học phí đã được cập nhật tự động.\n" +
                    $"🔄 Vui lòng kiểm tra mục thanh toán để xem chi tiết.");
                
                // Refresh data
                LoadAvailableClassesAsync();
                LoadRegisteredClassesAsync();
                UpdateEnhancedSummary();
                RefreshPaymentFormsIfOpen();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("đã đăng ký"))
                {
                    ShowWarning("Đăng ký thất bại", ex.Message);
                }
                else if (ex.Message.Contains("đầy"))
                {
                    ShowWarning("Lớp đã đầy", ex.Message);
                }
                else if (ex.Message.Contains("trùng"))
                {
                    ShowWarning("Trùng lịch học", ex.Message);
                }
                else
                {
                    ShowError("Lỗi đăng ký", ex.Message);
                }
            }
            finally
            {
                ShowLoadingIndicator(false);
            }
        }

        private void UnregisterFromClass(int enrollmentID, string courseName)
        {
            try
            {
                ShowLoadingIndicator(true, "Đang hủy đăng ký...");

                // Chỉ cần DELETE từ Enrollments - trigger sẽ tự động xử lý hóa đơn
                string sql = $"DELETE FROM Enrollments WHERE EnrollmentID = {enrollmentID}";
                kn.ThucThiSQL(sql);

                ShowSuccess("Hủy đăng ký thành công", 
                    $"✅ Đã hủy đăng ký môn '{courseName}' thành công!\n" +
                    $"💰 Hóa đơn học phí đã được cập nhật tự động.\n" +
                    $"🔄 Vui lòng kiểm tra mục thanh toán để xem chi tiết.");
                
                // Refresh data
                LoadAvailableClassesAsync();
                LoadRegisteredClassesAsync();
                UpdateEnhancedSummary();
                RefreshPaymentFormsIfOpen();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi hủy đăng ký", ex.Message);
            }
            finally
            {
                ShowLoadingIndicator(false);
            }
        }

        private void RefreshPaymentFormsIfOpen()
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form is FrmThanhToan paymentForm && !form.IsDisposed)
                    {
                        var loadMethod = paymentForm.GetType().GetMethod("LoadInvoices", 
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        
                        if (loadMethod != null)
                        {
                            paymentForm.BeginInvoke(new Action(() => {
                                try
                                {
                                    loadMethod.Invoke(paymentForm, null);
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine("Error refreshing payment form: " + ex.Message);
                                }
                            }));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error refreshing payment forms: " + ex.Message);
            }
        }

        private void ShowClassDetails(DataGridViewRow row)
        {
            try
            {
                if (row?.Cells != null && row.Cells.Count > 0)
                {
                    string details = $"📖 {row.Cells["CourseCode"].Value} - {row.Cells["CourseName"].Value}\n" +
                                   $"👨‍🏫 GV: {row.Cells["LecturerName"].Value}\n" +
                                   $"⏰ Lịch: {row.Cells["Schedule"].Value}\n" +
                                   $"🏢 Phòng: {row.Cells["Room"].Value}\n" +
                                   $"👥 Sĩ số: {row.Cells["CurrentStudents"].Value}/{row.Cells["MaxStudents"].Value}\n" +
                                   $"💰 Học phí: {Convert.ToDecimal(row.Cells["TotalTuition"].Value):N0} VNĐ";

                    // Hiển thị trong title bar
                    this.Text = details;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error showing class details: " + ex.Message);
            }
        }

        private void ShowScheduleDialog()
        {
            try
            {
                if (dgvDaDangKy == null) return;

                var scheduleForm = new Form
                {
                    Text = "📅 Lịch học của bạn",
                    Size = new Size(600, 400),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                var richTextBox = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    Font = new Font("Consolas", 10),
                    BackColor = Color.White
                };

                StringBuilder schedule = new StringBuilder();
                schedule.AppendLine("📚 LỊCH HỌC CỦA BẠN");
                schedule.AppendLine("".PadRight(50, '='));
                schedule.AppendLine();

                var groupedSchedule = new Dictionary<string, List<string>>();

                foreach (DataGridViewRow row in dgvDaDangKy.Rows)
                {
                    if (row.Cells["Schedule"].Value != null)
                    {
                        string scheduleText = row.Cells["Schedule"].Value.ToString();
                        string courseInfo = $"📖 {row.Cells["CourseName"].Value} - 🏢 {row.Cells["Room"].Value}";
                        
                        if (!groupedSchedule.ContainsKey(scheduleText))
                            groupedSchedule[scheduleText] = new List<string>();
                        
                        groupedSchedule[scheduleText].Add(courseInfo);
                    }
                }

                foreach (var timeSlot in groupedSchedule.OrderBy(x => x.Key))
                {
                    schedule.AppendLine($"⏰ {timeSlot.Key}:");
                    foreach (var course in timeSlot.Value)
                    {
                        schedule.AppendLine($"   {course}");
                    }
                    schedule.AppendLine();
                }

                if (groupedSchedule.Count == 0)
                {
                    schedule.AppendLine("🔍 Không có lịch học nào được tìm thấy.");
                }

                richTextBox.Text = schedule.ToString();
                scheduleForm.Controls.Add(richTextBox);
                scheduleForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi hiển thị lịch học", ex.Message);
            }
        }
        #endregion

        #region UI Helper Methods
        private void ShowLoadingIndicator(bool show, string message = "Đang tải...")
        {
            try
            {
                if (show)
                {
                    this.Cursor = Cursors.WaitCursor;
                    if (lblSinhVien != null)
                        lblSinhVien.Text = $"⏳ {message}";
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    LoadStudentInfo(); // Restore original text
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in ShowLoadingIndicator: " + ex.Message);
            }
        }

        private void ShowSuccess(string title, string message)
        {
            MessageBox.Show($"✅ {message}", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string title, string message)
        {
            MessageBox.Show($"❌ {message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string title, string message)
        {
            MessageBox.Show($"⚠️ {message}", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowInfo(string title, string message)
        {
            MessageBox.Show($"ℹ️ {message}", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowWarnings(int totalCredits, decimal totalTuition)
        {
            try
            {
                if (lblTongTinChi != null && totalCredits > 22)
                {
                    lblTongTinChi.Text += " ⚠️";
                }
                
                if (lblTongHocPhi != null && totalTuition > 8000000)
                {
                    lblTongHocPhi.Text += " ⚠️";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in ShowWarnings: " + ex.Message);
            }
        }

        private void UpdateAvailableClassesCount(int count)
        {
            try
            {
                if (btnTimKiem != null)
                    btnTimKiem.Text = $"🔍 Tìm kiếm ({count})";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in UpdateAvailableClassesCount: " + ex.Message);
            }
        }
        #endregion

        #region Background Worker Methods
        private void DataLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            // Load data in background if needed
        }

        private void DataLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowLoadingIndicator(false);
        }
        #endregion

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                searchTimer?.Dispose();
                dataLoader?.Dispose();
                kn?.NgatKetNoi();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in OnFormClosed: " + ex.Message);
            }
            
            base.OnFormClosed(e);
        }
    }
}
