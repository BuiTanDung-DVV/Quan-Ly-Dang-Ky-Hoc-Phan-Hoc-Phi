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

        public FrmDangKi()
        {
            InitializeComponent();
        }

        private void FrmDangKi_Load(object sender, EventArgs e)
        {
            LoadStudentInfo();
            SetupDataGridViews();
            LoadTerms();
            LoadDepartments();
            
            // DELAY để đảm bảo tất cả đã setup xong
            this.BeginInvoke(new Action(() => {
                LoadAvailableClasses();
                LoadRegisteredClasses();
                CalculateTotals();
            }));
        }

        #region Setup Methods
        private void LoadStudentInfo()
        {
            if (UserSession.IsStudent() && UserSession.LinkedStudentID.HasValue)
            {
                currentStudentID = UserSession.LinkedStudentID.Value;
                try
                {
                    string sql = $@"
                        SELECT s.StudentCode, s.FullName, d.Name as DeptName
                        FROM Students s
                        LEFT JOIN Departments d ON s.DeptID = d.DeptID
                        WHERE s.StudentID = {currentStudentID}";

                    DataTable dt = kn.Lay_DulieuBang(sql);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        lblSinhVien.Text = $"Sinh viên: {row["StudentCode"]} - {row["FullName"]} ({row["DeptName"]})";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load thông tin sinh viên: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                lblSinhVien.Text = "Thông tin sinh viên không hợp lệ";
                this.Enabled = false;
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
                
                // Chọn học kỳ hiện tại
                DataRow[] currentTerms = dt.Select("Name LIKE '%hiện tại%' OR Name LIKE '%current%'");
                if (currentTerms.Length == 0 && dt.Rows.Count > 0)
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

        private void LoadDepartments()
        {
            try
            {
                string sql = "SELECT DeptID, Name FROM Departments ORDER BY Name";
                DataTable dt = kn.Lay_DulieuBang(sql);
                
                DataRow newRow = dt.NewRow();
                newRow["DeptID"] = 0;
                newRow["Name"] = "-- Tất cả khoa --";
                dt.Rows.InsertAt(newRow, 0);
                
                cboKhoa.DisplayMember = "Name";
                cboKhoa.ValueMember = "DeptID";
                cboKhoa.DataSource = dt;
                cboKhoa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load khoa: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViews()
        {
            // Setup dgvLopHocPhan
            dgvLopHocPhan.Columns.Clear();
            dgvLopHocPhan.Columns.Add("SectionID", "Mã LHP");
            dgvLopHocPhan.Columns.Add("CourseCode", "Mã môn");
            dgvLopHocPhan.Columns.Add("CourseName", "Tên môn học");
            dgvLopHocPhan.Columns.Add("Credits", "Tín chỉ");
            dgvLopHocPhan.Columns.Add("LecturerName", "Giảng viên");
            dgvLopHocPhan.Columns.Add("Schedule", "Lịch học");
            dgvLopHocPhan.Columns.Add("Room", "Phòng");
            dgvLopHocPhan.Columns.Add("MaxStudents", "Sĩ số");
            dgvLopHocPhan.Columns.Add("CurrentStudents", "Đã đăng ký");
            dgvLopHocPhan.Columns.Add("TuitionPerCredit", "Học phí/TC");
            dgvLopHocPhan.Columns.Add("TotalTuition", "Tổng học phí");

            dgvLopHocPhan.Columns["SectionID"].Visible = false;
            dgvLopHocPhan.Columns["TuitionPerCredit"].DefaultCellStyle.Format = "N0";
            dgvLopHocPhan.Columns["TotalTuition"].DefaultCellStyle.Format = "N0";

            // Setup dgvDaDangKy
            dgvDaDangKy.Columns.Clear();
            dgvDaDangKy.Columns.Add("EnrollmentID", "ID");
            dgvDaDangKy.Columns.Add("SectionID", "Mã LHP");
            dgvDaDangKy.Columns.Add("CourseCode", "Mã môn");
            dgvDaDangKy.Columns.Add("CourseName", "Tên môn học");
            dgvDaDangKy.Columns.Add("Credits", "Tín chỉ");
            dgvDaDangKy.Columns.Add("Schedule", "Lịch học");
            dgvDaDangKy.Columns.Add("Room", "Phòng");
            dgvDaDangKy.Columns.Add("Status", "Trạng thái");
            dgvDaDangKy.Columns.Add("RegisterDate", "Ngày ĐK");

            dgvDaDangKy.Columns["EnrollmentID"].Visible = false;
            dgvDaDangKy.Columns["SectionID"].Visible = false;
            dgvDaDangKy.Columns["RegisterDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        #endregion

        #region Data Loading Methods
        private void LoadAvailableClasses()
        {
            if (dgvLopHocPhan.Columns.Count == 0)
            {
                return;
            }
    
            if (cboHocKy.SelectedValue == null) return;
            
            selectedTermID = Convert.ToInt32(cboHocKy.SelectedValue);
            
            try
            {
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
                        c.DeptID
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


                if (cboKhoa.SelectedValue != null && Convert.ToInt32(cboKhoa.SelectedValue) > 0)
                {
                    sql += $" AND c.DeptID = {cboKhoa.SelectedValue}";
                }

                if (!string.IsNullOrEmpty(txtTimKiem.Text.Trim()))
                {
                    string searchText = txtTimKiem.Text.Trim();
                    sql += $" AND (c.Code LIKE '%{searchText}%' OR c.Name LIKE N'%{searchText}%')";
                }

                sql += $@" AND cs.SectionID NOT IN (
                    SELECT SectionID FROM Enrollments 
                    WHERE StudentID = {currentStudentID} 
                        AND Status IN (N'Đang học', N'Đã duyệt')
                )";

                sql += " ORDER BY c.Code";

                DataTable dt = kn.Lay_DulieuBang(sql);
                dgvLopHocPhan.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvLopHocPhan.Rows.Add(
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

                    // Highlight full classes
                    int maxStudents = Convert.ToInt32(row["MaxStudents"]);
                    int currentStudents = Convert.ToInt32(row["CurrentStudents"]);
                    
                    if (currentStudents >= maxStudents)
                    {
                        dgvLopHocPhan.Rows[dgvLopHocPhan.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightCoral;
                        dgvLopHocPhan.Rows[dgvLopHocPhan.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load lớp học phần: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRegisteredClasses()
        {
            if (dgvDaDangKy.Columns.Count == 0)
            {
                return;
            }
    
            if (selectedTermID == 0) return;

            try
            {
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
                        AND e.Status IN (N'Đang học', N'Đã duyệt')
                    ORDER BY e.RegisterDate DESC";

                DataTable dt = kn.Lay_DulieuBang(sql);
                dgvDaDangKy.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvDaDangKy.Rows.Add(
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load lớp đã đăng ký: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotals()
        {
            int totalCredits = 0;
            decimal totalTuition = 0;

            foreach (DataGridViewRow row in dgvDaDangKy.Rows)
            {
                if (row.Cells["Credits"].Value != null)
                {
                    totalCredits += Convert.ToInt32(row.Cells["Credits"].Value);
                }
            }

            try
            {
                string sql = $@"
                    SELECT SUM(c.Credits * c.TuitionPerCredit) as TotalTuition
                    FROM Enrollments e
                    JOIN ClassSections cs ON e.SectionID = cs.SectionID
                    JOIN Courses c ON cs.CourseID = c.CourseID
                    WHERE e.StudentID = {currentStudentID} 
                        AND cs.TermID = {selectedTermID}
                        AND e.Status IN (N'Đang học', N'Đã duyệt')";

                DataTable dt = kn.Lay_DulieuBang(sql);
                if (dt.Rows.Count > 0 && dt.Rows[0]["TotalTuition"] != DBNull.Value)
                {
                    totalTuition = Convert.ToDecimal(dt.Rows[0]["TotalTuition"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính học phí: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            lblTongTinChi.Text = $"Tổng tín chỉ đăng ký: {totalCredits}";
            lblTongHocPhi.Text = $"Tổng học phí: {totalTuition:N0} VNĐ";
        }
        #endregion

        #region Event Handlers
        private void cboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableClasses();
            LoadRegisteredClasses();
            CalculateTotals();
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableClasses();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadAvailableClasses();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Text = "";
            cboKhoa.SelectedIndex = 0;
            LoadAvailableClasses();
        }

        private void dgvLopHocPhan_SelectionChanged(object sender, EventArgs e)
        {
            btnDangKy.Enabled = dgvLopHocPhan.CurrentRow != null;
        }

        private void dgvDaDangKy_SelectionChanged(object sender, EventArgs e)
        {
            btnHuyDangKy.Enabled = dgvDaDangKy.CurrentRow != null;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (dgvLopHocPhan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lớp học phần cần đăng ký!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int sectionID = Convert.ToInt32(dgvLopHocPhan.CurrentRow.Cells["SectionID"].Value);
            string courseName = dgvLopHocPhan.CurrentRow.Cells["CourseName"].Value.ToString();

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn đăng ký lớp '{courseName}'?", 
                "Xác nhận đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Sử dụng trigger sẽ tự động kiểm tra sĩ số và tạo hóa đơn
                    string sql = $@"
                        INSERT INTO Enrollments (StudentID, SectionID, RegisterDate, Status)
                        VALUES ({currentStudentID}, {sectionID}, GETDATE(), N'Đang học')";

                    kn.ThucThiSQL(sql);

                    MessageBox.Show("Đăng ký thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadAvailableClasses();
                    LoadRegisteredClasses();
                    CalculateTotals();
                }
                catch (Exception ex)
                {
                    // Trigger sẽ throw error nếu vi phạm business rules
                    MessageBox.Show("Lỗi đăng ký: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHuyDangKy_Click(object sender, EventArgs e)
        {
            if (dgvDaDangKy.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lớp cần hủy đăng ký!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int enrollmentID = Convert.ToInt32(dgvDaDangKy.CurrentRow.Cells["EnrollmentID"].Value);
            string courseName = dgvDaDangKy.CurrentRow.Cells["CourseName"].Value.ToString();

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn hủy đăng ký lớp '{courseName}'?", 
                "Xác nhận hủy đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string sql = $"DELETE FROM Enrollments WHERE EnrollmentID = {enrollmentID}";
                    kn.ThucThiSQL(sql);

                    MessageBox.Show("Hủy đăng ký thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadAvailableClasses();
                    LoadRegisteredClasses();
                    CalculateTotals();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hủy đăng ký: " + ex.Message, "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXemLichHoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDaDangKy.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn chưa đăng ký lớp học phần nào!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Tạo và hiển thị lịch học
                string schedule = "LỊCH HỌC CỦA BẠN:\n\n";
                foreach (DataGridViewRow row in dgvDaDangKy.Rows)
                {
                    if (row.Cells["CourseName"].Value != null)
                    {
                        schedule += $"• {row.Cells["CourseName"].Value} - {row.Cells["Schedule"].Value} - Phòng {row.Cells["Room"].Value}\n";
                    }
                }

                MessageBox.Show(schedule, "Lịch học", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xem lịch học: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void CreateInvoiceIfNeeded()
        {
            if (selectedTermID == 0 || currentStudentID == 0) return;
            
            try
            {
                // Kiểm tra xem đã có hóa đơn chưa
                string checkSql = $@"
                    SELECT COUNT(*) FROM Invoices 
                    WHERE StudentID = {currentStudentID} AND TermID = {selectedTermID}";
        
                DataTable dt = kn.Lay_DulieuBang(checkSql);
                if (Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    // Chưa có hóa đơn, tạo mới
                    string createSql = $"EXEC sp_CreateInvoiceForTerm {currentStudentID}, {selectedTermID}";
                    kn.ThucThiSQL(createSql);
                }
            }
            catch (Exception ex)
            {
          
                System.Diagnostics.Debug.WriteLine("Error creating invoice: " + ex.Message);
            }
        }

    }
}
