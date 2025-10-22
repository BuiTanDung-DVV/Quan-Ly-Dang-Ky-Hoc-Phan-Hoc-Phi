using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmDanhSachDangKi : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();
        private int currentLecturerID = 0;
        private int selectedTermID = 0;
        private int selectedSectionID = 0;

        public FrmDanhSachDangKi()
        {
            InitializeComponent();
        }

        private void FrmDanhSachDangKi_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền trước
            if (!UserSession.IsLecturer() || !UserSession.LinkedLecturerID.HasValue)
            {
                lblGiangVien.Text = "Thông tin giảng viên không hợp lệ";
                this.Enabled = false;
                return;
            }

            try
            {
                // 1. Setup DataGridViews TRƯỚC TIÊN
                SetupDataGridViews();
                
                // 2. Load thông tin giảng viên
                LoadLecturerInfo();
                
                // 3. Load học kỳ
                LoadTerms();
                
                // 4. Load dữ liệu môn học (chỉ khi đã có columns)
                if (dgvMonHoc.Columns.Count > 0)
                {
                    LoadLecturerCourses();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLecturerInfo()
        {
            if (UserSession.IsLecturer() && UserSession.LinkedLecturerID.HasValue)
            {
                currentLecturerID = UserSession.LinkedLecturerID.Value;
                try
                {
                    string sql = $@"
                        SELECT l.LecturerCode, l.FullName, d.Name as DeptName
                        FROM Lecturers l
                        LEFT JOIN Departments d ON l.DeptID = d.DeptID
                        WHERE l.LecturerID = {currentLecturerID}";

                    DataTable dt = kn.Lay_DulieuBang(sql);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        lblGiangVien.Text = $"Giảng viên: {row["LecturerCode"]} - {row["FullName"]} ({row["DeptName"]})";
                    }
                    else
                    {
                        lblGiangVien.Text = "Không tìm thấy thông tin giảng viên";
                        currentLecturerID = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load thông tin giảng viên: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentLecturerID = 0;
                }
            }
            else
            {
                lblGiangVien.Text = "Thông tin giảng viên không hợp lệ";
                currentLecturerID = 0;
            }
        }

        private void LoadTerms()
        {
            try
            {
                string sql = "SELECT TermID, Name FROM AcademicTerms ORDER BY StartDate DESC";
                DataTable dt = kn.Lay_DulieuBang(sql);
                
                cboHocKy.DisplayMember = "Name";
                cboHocKy.ValueMember = "TermID";
                cboHocKy.DataSource = dt;
                
                // Chọn học kỳ mới nhất (index 0)
                if (dt.Rows.Count > 0)
                {
                    cboHocKy.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load học kỳ: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViews()
        {
            try
            {
                // Clear existing columns trước khi thêm mới
                dgvMonHoc.Columns.Clear();
                dgvSinhVien.Columns.Clear();

                // Setup dgvMonHoc với SectionCode
                dgvMonHoc.Columns.Add("SectionID", "ID");
                dgvMonHoc.Columns.Add("SectionCode", "Mã LHP");
                dgvMonHoc.Columns.Add("CourseCode", "Mã môn");
                dgvMonHoc.Columns.Add("CourseName", "Tên môn học");
                dgvMonHoc.Columns.Add("Credits", "Tín chỉ");
                dgvMonHoc.Columns.Add("Schedule", "Lịch học");
                dgvMonHoc.Columns.Add("Room", "Phòng");
                dgvMonHoc.Columns.Add("MaxStudents", "Sĩ số");
                dgvMonHoc.Columns.Add("RegisteredStudents", "Đã ĐK");
                dgvMonHoc.Columns.Add("EmptySlots", "Còn trống");

                // Thiết lập width và style cho dgvMonHoc
                dgvMonHoc.Columns["SectionID"].Visible = false;
                dgvMonHoc.Columns["SectionCode"].Width = 90;
                dgvMonHoc.Columns["CourseCode"].Width = 90;
                dgvMonHoc.Columns["CourseName"].Width = 280;
                dgvMonHoc.Columns["Credits"].Width = 70;
                dgvMonHoc.Columns["Schedule"].Width = 200;
                dgvMonHoc.Columns["Room"].Width = 80;
                dgvMonHoc.Columns["MaxStudents"].Width = 70;
                dgvMonHoc.Columns["RegisteredStudents"].Width = 80;
                dgvMonHoc.Columns["EmptySlots"].Width = 80;

                // Style cho dgvMonHoc
                dgvMonHoc.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                dgvMonHoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                dgvMonHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
                dgvMonHoc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvMonHoc.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

                // Setup dgvSinhVien
                dgvSinhVien.Columns.Add("StudentCode", "Mã sinh viên");
                dgvSinhVien.Columns.Add("FullName", "Họ và tên");
                dgvSinhVien.Columns.Add("Gender", "Giới tính");
                dgvSinhVien.Columns.Add("DateOfBirth", "Ngày sinh");
                dgvSinhVien.Columns.Add("Email", "Email");
                dgvSinhVien.Columns.Add("Phone", "Điện thoại");
                dgvSinhVien.Columns.Add("DeptName", "Khoa");
                dgvSinhVien.Columns.Add("RegisterDate", "Ngày ĐK");
                dgvSinhVien.Columns.Add("Status", "Trạng thái");

                // Thiết lập width cho dgvSinhVien
                dgvSinhVien.Columns["StudentCode"].Width = 120;
                dgvSinhVien.Columns["FullName"].Width = 200;
                dgvSinhVien.Columns["Gender"].Width = 80;
                dgvSinhVien.Columns["DateOfBirth"].Width = 100;
                dgvSinhVien.Columns["Email"].Width = 220;
                dgvSinhVien.Columns["Phone"].Width = 120;
                dgvSinhVien.Columns["DeptName"].Width = 150;
                dgvSinhVien.Columns["RegisterDate"].Width = 100;
                dgvSinhVien.Columns["Status"].Width = 100;

                // Style cho dgvSinhVien
                dgvSinhVien.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                dgvSinhVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                dgvSinhVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(39, 174, 96);
                dgvSinhVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvSinhVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

                // Format dates
                dgvSinhVien.Columns["DateOfBirth"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvSinhVien.Columns["RegisterDate"].DefaultCellStyle.Format = "dd/MM/yyyy";

                // Verify setup
                System.Diagnostics.Debug.WriteLine($"dgvMonHoc columns count: {dgvMonHoc.Columns.Count}");
                System.Diagnostics.Debug.WriteLine($"dgvSinhVien columns count: {dgvSinhVien.Columns.Count}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thiết lập DataGridView: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLecturerCourses()
        {
            // Kiểm tra điều kiện cần thiết
            if (cboHocKy.SelectedValue == null || currentLecturerID == 0) 
            {
                dgvMonHoc.Rows.Clear();
                dgvSinhVien.Rows.Clear();
                lblMonDangChon.Text = "Môn đang chọn: Chưa chọn môn nào";
                lblTongSinhVien.Text = "Tổng sinh viên: 0";
                return;
            }

            // Kiểm tra DataGridView đã có columns chưa
            if (dgvMonHoc.Columns.Count == 0)
            {
                MessageBox.Show("Lỗi: DataGridView chưa được thiết lập columns!", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            selectedTermID = Convert.ToInt32(cboHocKy.SelectedValue);
            
            try
            {
                string sql = $@"
                    SELECT 
                        cs.SectionID,
                        cs.SectionCode,
                        c.Code as CourseCode,
                        c.Name as CourseName,
                        c.Credits,
                        cs.Schedule,
                        cs.Room,
                        cs.MaxStudents,
                        ISNULL(enrolled.RegisteredStudents, 0) as RegisteredStudents,
                        (cs.MaxStudents - ISNULL(enrolled.RegisteredStudents, 0)) as EmptySlots
                    FROM ClassSections cs
                    JOIN Courses c ON cs.CourseID = c.CourseID
                    LEFT JOIN (
                        SELECT 
                            SectionID, 
                            COUNT(*) as RegisteredStudents
                        FROM Enrollments 
                        WHERE Status IN (N'Đang học', N'Đã duyệt')
                        GROUP BY SectionID
                    ) enrolled ON cs.SectionID = enrolled.SectionID
                    WHERE cs.LecturerID = {currentLecturerID} 
                        AND cs.TermID = {selectedTermID}
                    ORDER BY c.Code, cs.SectionCode";

                DataTable dt = kn.Lay_DulieuBang(sql);
                dgvMonHoc.Rows.Clear();

                if (dt.Rows.Count == 0)
                {
                    lblMonDangChon.Text = "Môn đang chọn: Không có lớp nào trong học kỳ này";
                    lblTongSinhVien.Text = "Tổng sinh viên: 0";
                    dgvSinhVien.Rows.Clear();
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    // Kiểm tra SectionCode có tồn tại không
                    string sectionCode = row["SectionCode"]?.ToString() ?? 
                                        $"{row["CourseCode"]}-L{row["SectionID"]}";

                    dgvMonHoc.Rows.Add(
                        row["SectionID"],
                        sectionCode,
                        row["CourseCode"],
                        row["CourseName"],
                        row["Credits"],
                        row["Schedule"],
                        row["Room"],
                        row["MaxStudents"],
                        row["RegisteredStudents"],
                        row["EmptySlots"]
                    );

                    // Highlight rows based on capacity
                    int maxStudents = Convert.ToInt32(row["MaxStudents"]);
                    int registeredStudents = Convert.ToInt32(row["RegisteredStudents"]);
                    
                    DataGridViewRow dgvRow = dgvMonHoc.Rows[dgvMonHoc.Rows.Count - 1];
                    
                    if (registeredStudents >= maxStudents)
                    {
                        // Lớp đầy - màu đỏ nhạt
                        dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
                        dgvRow.DefaultCellStyle.ForeColor = Color.FromArgb(194, 54, 22);
                    }
                    else if (registeredStudents >= maxStudents * 0.8)
                    {
                        // Gần đầy - màu vàng nhạt
                        dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220);
                        dgvRow.DefaultCellStyle.ForeColor = Color.FromArgb(183, 149, 11);
                    }
                    else
                    {
                        // Còn trống nhiều - màu xanh nhạt
                        dgvRow.DefaultCellStyle.BackColor = Color.FromArgb(235, 248, 238);
                        dgvRow.DefaultCellStyle.ForeColor = Color.FromArgb(22, 160, 133);
                    }
                }

                // Clear student list khi load lại môn học
                dgvSinhVien.Rows.Clear();
                lblMonDangChon.Text = "Môn đang chọn: Chưa chọn môn nào";
                lblTongSinhVien.Text = "Tổng sinh viên: 0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách môn học: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Debug thông tin
                MessageBox.Show($"Debug info:\n" +
                               $"- DataGridView Columns Count: {dgvMonHoc.Columns.Count}\n" +
                               $"- TermID: {selectedTermID}\n" +
                               $"- LecturerID: {currentLecturerID}", "Debug");
            }
        }

        private void LoadStudentList()
        {
            if (selectedSectionID == 0) return;

            try
            {
                string sql = $@"
                    SELECT 
                        s.StudentCode,
                        s.FullName,
                        s.Gender,
                        s.DateOfBirth,
                        s.Email,
                        s.Phone,
                        d.Name as DeptName,
                        e.RegisterDate,
                        e.Status
                    FROM Enrollments e
                    JOIN Students s ON e.StudentID = s.StudentID
                    JOIN Departments d ON s.DeptID = d.DeptID
                    WHERE e.SectionID = {selectedSectionID}
                        AND e.Status IN (N'Đang học', N'Đã duyệt')
                    ORDER BY s.StudentCode";

                DataTable dt = kn.Lay_DulieuBang(sql);
                dgvSinhVien.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvSinhVien.Rows.Add(
                        row["StudentCode"],
                        row["FullName"],
                        row["Gender"],
                        row["DateOfBirth"],
                        row["Email"],
                        row["Phone"],
                        row["DeptName"],
                        row["RegisterDate"],
                        row["Status"]
                    );
                }

                lblTongSinhVien.Text = $"Tổng sinh viên: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách sinh viên: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Chỉ load khi DataGridView đã ready
            if (dgvMonHoc.Columns.Count > 0 && currentLecturerID > 0)
            {
                LoadLecturerCourses();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Refresh lại toàn bộ
            if (dgvMonHoc.Columns.Count > 0 && currentLecturerID > 0)
            {
                LoadLecturerCourses();
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin giảng viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvMonHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMonHoc.CurrentRow != null)
            {
                selectedSectionID = Convert.ToInt32(dgvMonHoc.CurrentRow.Cells["SectionID"].Value);
                string sectionCode = dgvMonHoc.CurrentRow.Cells["SectionCode"].Value.ToString();
                string courseName = dgvMonHoc.CurrentRow.Cells["CourseName"].Value.ToString();
                
                lblMonDangChon.Text = $"Môn đang chọn: {sectionCode} - {courseName}";
                
                LoadStudentList();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Lấy thông tin lớp đang chọn
                string className = lblMonDangChon.Text.Replace("Môn đang chọn: ", "");
                
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.FileName = $"DanhSach_{className}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();
                    
                    // Header với thông tin lớp
                    csv.AppendLine($"DANH SÁCH SINH VIÊN LỚP: {className}");
                    csv.AppendLine($"Giảng viên: {lblGiangVien.Text.Replace("Giảng viên: ", "")}");
                    csv.AppendLine($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    csv.AppendLine($"{lblTongSinhVien.Text}");
                    csv.AppendLine(""); // Dòng trống
                    
                    // Header cột
                    csv.AppendLine("STT,Mã sinh viên,Họ và tên,Giới tính,Ngày sinh,Email,Điện thoại,Khoa,Ngày đăng ký,Trạng thái");
                    
                    // Data với số thứ tự
                    int stt = 1;
                    foreach (DataGridViewRow row in dgvSinhVien.Rows)
                    {
                        string[] fields = new string[dgvSinhVien.Columns.Count + 1];
                        fields[0] = stt.ToString();
                        for (int i = 0; i < dgvSinhVien.Columns.Count; i++)
                        {
                            fields[i + 1] = row.Cells[i].Value?.ToString() ?? "";
                        }
                        csv.AppendLine(string.Join(",", fields));
                        stt++;
                    }
                    
                    System.IO.File.WriteAllText(saveFileDialog.FileName, csv.ToString(), Encoding.UTF8);
                    
                    MessageBox.Show("Xuất file thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
