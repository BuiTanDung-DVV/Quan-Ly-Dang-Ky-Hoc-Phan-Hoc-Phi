using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmLopHocPhan_ChinhSua : Form
    {
        private int? _idLopHocPhan;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        public int CornerRadius { get; set; } = 15; // bán kính bo góc mặc định

        KETNOI_CSDL kn = new KETNOI_CSDL();
        public FrmLopHocPhan_ChinhSua()
        {
            InitializeComponent();
            _idLopHocPhan = null;
            InitializeForm();

            this.label1.Text = "📝 THÊM MỚI LỚP HỌC PHẦN";
            this.txtSectionID.Enabled = false;
        }

        public FrmLopHocPhan_ChinhSua(int idLopHocPhan)
        {
            InitializeComponent();
            _idLopHocPhan = idLopHocPhan;
            InitializeForm();

            this.label1.Text = "✏️ CHỈNH SỬA LỚP HỌC PHẦN";
            this.txtSectionID.Enabled = false;
            this.txtSectionCode.Enabled = false;
        }

        private void InitializeForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            // Đảm bảo kết nối database
            try
            {
                if (kn.cnn == null || kn.cnn.State != ConnectionState.Open)
                {
                    kn.KetNoi_Dulieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
        }

        public void Bang_MonHoc()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Courses");
            cboCourseID.DataSource = dta;
            cboCourseID.DisplayMember = "Name";
            cboCourseID.ValueMember = "CourseID";
        }

        public void Bang_GiaoVien()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Lecturers");
            cboLecturerID.DataSource = dta;
            cboLecturerID.DisplayMember = "FullName";
            cboLecturerID.ValueMember = "LecturerID";
        }

        public void Bang_HocKy()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM AcademicTerms");
            cboTermID.DataSource = dta;
            cboTermID.DisplayMember = "Name";
            cboTermID.ValueMember = "TermID";
        }

        private void Load_DuLieuCanSua()
        {
            if (_idLopHocPhan == null)
                return;

            try
            {
                if (kn.cnn.State != ConnectionState.Open)
                    kn.KetNoi_Dulieu();

                string sql = "SELECT * FROM ClassSections WHERE SectionID = " + _idLopHocPhan;
                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();
                if (doc_dl.Read())
                {
                    txtSectionID.Text = doc_dl["SectionID"].ToString();
                    txtSectionCode.Text = doc_dl["SectionCode"].ToString();
                    cboCourseID.SelectedValue = doc_dl["CourseID"];
                    cboLecturerID.SelectedValue = doc_dl["LecturerID"];
                    cboTermID.SelectedValue = doc_dl["TermID"];
                    txtSchedule.Text = doc_dl["Schedule"].ToString();
                    txtRoom.Text = doc_dl["Room"].ToString();
                    txtMaxStudents.Text = doc_dl["MaxStudents"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy lớp học phần với ID đã cho.");
                }
                doc_dl.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private bool ValidateInput()
        {
            // 1. Kiểm tra Mã Lớp Học Phần
            if (string.IsNullOrWhiteSpace(txtSectionCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã lớp học phần!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSectionCode.Focus();
                return false;
            }

            // 2. Kiểm tra Khóa học
            if (cboCourseID.SelectedValue == null || (int)cboCourseID.SelectedValue <= 0)
            {
                MessageBox.Show("Vui lòng chọn môn học/khóa học cho lớp này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCourseID.Focus();
                return false;
            }

            // 3. Kiểm tra Học kỳ
            if (cboTermID.SelectedValue == null || (int)cboTermID.SelectedValue <= 0)
            {
                MessageBox.Show("Vui lòng chọn học kỳ cho lớp này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTermID.Focus();
                return false;
            }

            // 4. Kiểm tra Giảng viên
            if (cboLecturerID.SelectedValue == null || (int)cboLecturerID.SelectedValue <= 0)
            {
                MessageBox.Show("Vui lòng chọn giảng viên phụ trách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLecturerID.Focus();
                return false;
            }

            // 5. Kiểm tra Lịch học
            if (string.IsNullOrWhiteSpace(txtSchedule.Text))
            {
                MessageBox.Show("Vui lòng nhập lịch học (Ví dụ: Thứ 3, Tiết 1-3)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSchedule.Focus();
                return false;
            }

            // 6. Kiểm tra Phòng học
            if (string.IsNullOrWhiteSpace(txtRoom.Text))
            {
                MessageBox.Show("Vui lòng nhập phòng học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoom.Focus();
                return false;
            }

            // 7. Kiểm tra Số lượng SV tối đa (MaxStudents)
            if (!int.TryParse(txtMaxStudents.Text, out int maxStudents) || maxStudents <= 0)
            {
                MessageBox.Show("Số lượng sinh viên tối đa không hợp lệ (Phải là số nguyên dương)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaxStudents.Focus();
                return false;
            }

            // Nếu tất cả kiểm tra đều thành công
            return true;
        }
        private void FrmLopHocPhan_ChinhSua_Load(object sender, EventArgs e)
        {

            try
            {
                Bang_MonHoc();
                Bang_GiaoVien();
                Bang_HocKy();
                if (_idLopHocPhan == null)
                {
                    txtSectionCode.Focus();
                }
                else
                {
                    Load_DuLieuCanSua();
                    txtSchedule.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy bỏ thay đổi?",
                                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;
            //nút lưu

            try
            {
                if (_idLopHocPhan == null)
                {
                    if (kn.cnn.State != ConnectionState.Open)
                        kn.KetNoi_Dulieu();

                    // Thêm mới môn học
                    string strKtra = "Select SectionCode from ClassSections where SectionCode='" + txtSectionCode.Text + "'";
                    SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                    SqlDataReader doc_dl = cmd.ExecuteReader();

                    if (doc_dl.Read() == true)
                    {
                        MessageBox.Show("Mã đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSectionCode.Focus();
                    }
                    else
                    {
                        kn.ThucThiSQL(
                            "INSERT INTO ClassSections (SectionCode, CourseID, TermID, LecturerID, Schedule, Room, MaxStudents) " +
                            "VALUES ('" + txtSectionCode.Text + "', "
                                   + cboCourseID.SelectedValue + ", "
                                   + cboTermID.SelectedValue + ", "
                                   + cboLecturerID.SelectedValue + ", N'"
                                   + txtSchedule.Text + "', N'"
                                   + txtRoom.Text + "', "
                                   + txtMaxStudents.Text + ")"
                        );
                        MessageBox.Show("Lưu dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Cập nhật môn học
                    string sql_Sua = "UPDATE ClassSections SET " +
                                     "SectionCode = '" + txtSectionCode.Text + "', " +
                                     "CourseID = " + cboCourseID.SelectedValue + ", " +
                                     "TermID = " + cboTermID.SelectedValue + ", " +
                                     "LecturerID = " + cboLecturerID.SelectedValue + ", " +
                                     "Schedule = N'" + txtSchedule.Text + "', " +
                                     "Room = N'" + txtRoom.Text + "', " +
                                     "MaxStudents = " + txtMaxStudents.Text + " " +
                                     "WHERE SectionID = " + _idLopHocPhan.Value;
                    kn.ThucThiSQL(sql_Sua);
                    MessageBox.Show("Cập nhật lớp học phần thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (kn.cnn != null && kn.cnn.State == ConnectionState.Open)
                {
                    kn.NgatKetNoi();
                }
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnHuy_Click(this, EventArgs.Empty);
                return true;
            }
            else if (keyData == (Keys.Control | Keys.S))
            {
                btnLuu_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

    }
}
