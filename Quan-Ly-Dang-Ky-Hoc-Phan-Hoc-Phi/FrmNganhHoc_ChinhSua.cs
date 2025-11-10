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
    public partial class FrmNganhHoc_ChinhSua : Form
    {
        private int? _idNganhHoc;

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
        public FrmNganhHoc_ChinhSua()
        {
            InitializeComponent();
            _idNganhHoc = null;

            InitializeForm();

            this.label1.Text = "🎓 THÊM MỚI NGÀNH HỌC";
            this.txtMajorID.Enabled = false;
        }

        public FrmNganhHoc_ChinhSua(int idNganhHoc)
        {
            InitializeComponent();
            _idNganhHoc = idNganhHoc;
            InitializeForm();

            this.label1.Text = "✏️ CHỈNH SỬA NGÀNH HỌC";
            this.txtMajorID.Enabled = false;
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

        private void FrmNganhHoc_ChinhSua_Load(object sender, EventArgs e)
        {
            try
            {
                Bang_KhoaVien();
                Load_DuLieuCanSua();
                if (_idNganhHoc == null)
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
        private void Load_DuLieuCanSua()
        {
            if (_idNganhHoc == null) return;

            try
            {
                if (kn.cnn.State != ConnectionState.Open)
                    kn.KetNoi_Dulieu();

                string sql = "SELECT * FROM Majors WHERE MajorID = " + _idNganhHoc.Value;

                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read() == true)
                {
                    // 7. Đổ dữ liệu vào TextBox
                    txtMajorID.Text = doc_dl["MajorID"].ToString();
                    txtCode.Text = doc_dl["Code"].ToString();
                    txtName.Text = doc_dl["Name"].ToString();
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
            // 1. Kiểm tra Mã Ngành (Code)
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã ngành!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }

            // 2. Kiểm tra Tên Ngành (Name)
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên ngành học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // 3. Kiểm tra ID Khoa/Viện (DeptID)
            // Giả sử SelectedValue của ComboBox là DeptID (INT)
            if (cboDeptID.SelectedValue == null || (int)cboDeptID.SelectedValue <= 0)
            {
                MessageBox.Show("Vui lòng chọn Khoa/Viện quản lý ngành này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (_idNganhHoc == null)
                {
                    // Thêm mới môn học
                    string strKtra = "Select Code from Majors where Code='" + txtCode.Text + "'";
                    SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                    SqlDataReader doc_dl = cmd.ExecuteReader();

                    if (doc_dl.Read() == true)
                    {
                        MessageBox.Show("Mã đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCode.Focus();
                    }
                    else
                    {
                        kn.ThucThiSQL("INSERT INTO Majors (Code, Name, DeptID) VALUES ('" + txtCode.Text + "',N'" + txtName.Text + "'," + cboDeptID.SelectedValue + ")");
                        MessageBox.Show("Lưu dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    // Cập nhật môn học
                    string sql_Sua = "UPDATE Majors SET Name=N'" + txtName.Text + "', DeptID=" + cboDeptID.SelectedValue + " WHERE MajorID=" + _idNganhHoc.Value;
                    kn.ThucThiSQL(sql_Sua);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
