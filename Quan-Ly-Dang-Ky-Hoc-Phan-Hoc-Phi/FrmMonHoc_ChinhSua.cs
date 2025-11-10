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

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmMonHoc_ChinhSua : Form
    {
        private int? _idMonHoc;

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
        public FrmMonHoc_ChinhSua()
        {
            InitializeComponent();
            _idMonHoc = null;

            InitializeForm();

            this.label1.Text = "📚THÊM MÔN HỌC";
            this.txtCourseID.Enabled = false;
        }
        public FrmMonHoc_ChinhSua(int idMonHoc)
        {
            InitializeComponent();
            _idMonHoc = idMonHoc;
            InitializeForm();

            this.label1.Text = "✏️ CHỈNH SỬA MÔN HỌC";
            this.txtCourseID.Enabled = false;
            this.txtCode.Enabled = false;
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

        public void Bang_KhoaVien()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Departments");
            cboDeptID.DataSource = dta;
            cboDeptID.DisplayMember = "Name";
            cboDeptID.ValueMember = "DeptID";
        }

        private void Load_DuLieuCanSua()
        {
            if (_idMonHoc == null) return;

            try
            {
                if (kn.cnn.State != ConnectionState.Open)
                    kn.KetNoi_Dulieu();

                string sql = "SELECT * FROM Courses WHERE CourseID = " + _idMonHoc.Value;

                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read() == true)
                {
                    // 7. Đổ dữ liệu vào TextBox
                    txtCourseID.Text = doc_dl["CourseID"].ToString();
                    txtCode.Text = doc_dl["Code"].ToString();
                    txtName.Text = doc_dl["Name"].ToString();
                    txtCredits.Text = doc_dl["Credits"].ToString();
                    txtTuitionPerCredit.Text = doc_dl["TuitionPerCredit"].ToString();
                    cboDeptID.SelectedValue = doc_dl["DeptID"];
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho môn học này!");
                    this.Close();
                }

                // 8. Phải tự đóng SqlDataReader (rất quan trọng)
                doc_dl.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu. Lỗi: " + ex.Message);
                this.Close();
            }

        }
        private bool ValidateInput()
        {
            // 1. Kiểm tra Mã Môn Học (Code)
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }

            // 2. Kiểm tra Tên Môn Học (Name)
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // 3. Kiểm tra Số Tín Chỉ (Credits)
            // Cần là số nguyên và lớn hơn 0
            if (!int.TryParse(txtCredits.Text, out int credits) || credits <= 0)
            {
                MessageBox.Show("Số tín chỉ không hợp lệ (Phải là số nguyên dương)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCredits.Focus();
                return false;
            }

            // 4. Kiểm tra Học phí mỗi Tín Chỉ (TuitionPerCredit)
            // Cần là số hợp lệ (decimal) và lớn hơn hoặc bằng 0
            if (!decimal.TryParse(txtTuitionPerCredit.Text, out decimal tuition) || tuition < 0)
            {
                MessageBox.Show("Học phí mỗi tín chỉ không hợp lệ (Phải là số dương hoặc bằng 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuitionPerCredit.Focus();
                return false;
            }

            // 5. Kiểm tra ID Khoa/Viện (DeptID)
            // Môn học phải thuộc một Khoa/Viện quản lý
            if (cboDeptID.SelectedValue == null || (int)cboDeptID.SelectedValue <= 0)
            {
                MessageBox.Show("Vui lòng chọn Khoa/Viện quản lý môn học này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboDeptID.Focus();
                return false;
            }

            // Nếu tất cả kiểm tra đều thành công
            return true;
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
                if (_idMonHoc == null)
                {
                    // Thêm mới môn học
                    string strKtra = "Select Code from Courses where Code='" + txtCode.Text + "'";
                    SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                    SqlDataReader doc_dl = cmd.ExecuteReader();

                    if (doc_dl.Read() == true)
                    {
                        MessageBox.Show("Mã đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCode.Focus();
                    }
                    else
                    {
                        kn.ThucThiSQL("INSERT INTO Courses (Code, Name, Credits, TuitionPerCredit, DeptID) VALUES ('" + txtCode.Text + "',N'" + txtName.Text + "'," + txtCredits.Text + "," + txtTuitionPerCredit.Text + "," + cboDeptID.SelectedValue + ")");
                        MessageBox.Show("Lưu dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Cập nhật môn học
                    string sql_Sua = "UPDATE Courses SET Name=N'" + txtName.Text + "', Credits=" + txtCredits.Text + ", TuitionPerCredit=" + txtTuitionPerCredit.Text + ", DeptID=" + cboDeptID.SelectedValue + " WHERE CourseID=" + _idMonHoc.Value;
                    kn.ThucThiSQL(sql_Sua);
                    MessageBox.Show("Cập nhật môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
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

        private void FrmMonHoc_ChinhSua_Load(object sender, EventArgs e)
        {
            try
            {
                Bang_KhoaVien();
                Load_DuLieuCanSua();
                if (_idMonHoc == null)
                {
                    txtCode.Focus();
                }
                else
                {
                    Load_DuLieuCanSua();
                    txtName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
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
