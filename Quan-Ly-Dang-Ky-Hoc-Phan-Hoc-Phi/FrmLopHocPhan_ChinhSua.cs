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

        public int CornerRadius { get; set; } = 30; // bán kính bo góc mặc định

        KETNOI_CSDL kn = new KETNOI_CSDL();
        public FrmLopHocPhan_ChinhSua()
        {
            InitializeComponent();
            _idLopHocPhan = null;

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            this.label1.Text = "Thêm Mới Lớp Học Phần";
            this.txtId.Enabled = false;
        }

        public FrmLopHocPhan_ChinhSua(int idLopHocPhan)
        {
            InitializeComponent();
            _idLopHocPhan = idLopHocPhan;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
            this.label1.Text = "Chỉnh Sửa Lớp Học Phần";
            this.txtId.Enabled = false;
            this.txtMaLop.Enabled = false;
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
            cboMonHoc.DataSource = dta;
            cboMonHoc.DisplayMember = "Name";
            cboMonHoc.ValueMember = "CourseID";
        }

        public void Bang_GiaoVien()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Lecturers");
            cboGV.DataSource = dta;
            cboGV.DisplayMember = "FullName";
            cboGV.ValueMember = "LecturerID";
        }

        public void Bang_HocKy()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM AcademicTerms");
            cboHK.DataSource = dta;
            cboHK.DisplayMember = "Name";
            cboHK.ValueMember = "TermID";
        }

        private void Load_DuLieuCanSua()
        {
            if (_idLopHocPhan == null)
                return;

            try 
            {
                string sql = "SELECT * FROM ClassSections WHERE SectionID = @SectionID";
                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();
                if (doc_dl.Read())
                {
                    txtId.Text = doc_dl["SectionID"].ToString();
                    txtMaLop.Text = doc_dl["SectionCode"].ToString();
                    cboMonHoc.SelectedValue = doc_dl["CourseID"];
                    cboGV.SelectedValue = doc_dl["LecturerID"];
                    cboHK.SelectedValue = doc_dl["TermID"];
                    txtLich.Text = doc_dl["Schedule"].ToString();
                    txtRoom.Text = doc_dl["Room"].ToString();
                    txtMaxSt.Text = doc_dl["MaxStudents"].ToString();
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
        private void FrmLopHocPhan_ChinhSua_Load(object sender, EventArgs e)
        {
            Bang_GiaoVien();
            Bang_MonHoc();
            Bang_HocKy();
            Load_DuLieuCanSua();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //nút lưu
            if (_idLopHocPhan == null)
            {
                // Thêm mới môn học
                string strKtra = "Select SectionCode from ClassSections where SectionCode='" + txtMaLop.Text + "'";
                SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read() == true)
                {
                    MessageBox.Show("Mã đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaLop.Focus();
                }
                else
                {
                    kn.ThucThiSQL(
                        "INSERT INTO Sections (SectionCode, CourseID, TermID, LecturerID, Schedule, Room, MaxStudents) " +
                        "VALUES ('" + txtMaLop.Text + "', "
                               + cboMonHoc.SelectedValue + ", "
                               + cboHK.SelectedValue + ", "
                               + cboGV.SelectedValue + ", N'"
                               + txtLich.Text + "', N'"
                               + txtRoom.Text + "', "
                               + txtMaxSt.Text + ")"
                    );
                    MessageBox.Show("Lưu dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            else
            {
                // Cập nhật môn học
                string sql_Sua = "UPDATE Sections SET " +
                                 "SectionCode = '" + txtMaLop.Text + "', " +
                                 "CourseID = " + cboMonHoc.SelectedValue + ", " +
                                 "TermID = " + cboHK.SelectedValue + ", " +
                                 "LecturerID = " + cboGV.SelectedValue + ", " +
                                 "Schedule = N'" + txtLich.Text + "', " +
                                 "Room = N'" + txtRoom.Text + "', " +
                                 "MaxStudents = " + txtMaxSt.Text + " " +
                                 "WHERE SectionID = " + _idLopHocPhan.Value;
                kn.ThucThiSQL(sql_Sua);
                MessageBox.Show("Cập nhật lớp học phần thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
