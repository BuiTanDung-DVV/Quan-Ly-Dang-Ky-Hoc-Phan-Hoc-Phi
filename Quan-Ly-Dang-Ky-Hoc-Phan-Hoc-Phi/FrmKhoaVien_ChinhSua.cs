using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmKhoaVien_ChinhSua : Form
    {
        private int? _idKhoaVien;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        public int CornerRadius { get; set; } = 15;
        KETNOI_CSDL kn = new KETNOI_CSDL();

        // Constructor thêm mới
        public FrmKhoaVien_ChinhSua()
        {
            InitializeComponent();
            _idKhoaVien = null;
            InitializeForm();
            this.label1.Text = "🏫 THÊM MỚI KHOA/VIỆN";
            this.txtDeptID.Enabled = false;
        }

        // Constructor chỉnh sửa
        public FrmKhoaVien_ChinhSua(int idKhoaVien)
        {
            InitializeComponent();
            _idKhoaVien = idKhoaVien;
            InitializeForm();
            this.label1.Text = "✏️ CHỈNH SỬA KHOA/VIỆN";
            this.txtDeptID.Enabled = false;
            this.txtCode.Enabled = false;
        }

        private void InitializeForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            try
            {
                if (kn.cnn == null || kn.cnn.State != ConnectionState.Open)
                    kn.KetNoi_Dulieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
        }

        private void Load_DuLieuCanSua()
        {
            if (_idKhoaVien == null) return;

            try
            {
                if (kn.cnn.State != ConnectionState.Open)
                    kn.KetNoi_Dulieu();

                string sql = "SELECT * FROM Departments WHERE DeptID = " + _idKhoaVien.Value;
                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read())
                {
                    txtDeptID.Text = doc_dl["DeptID"].ToString();
                    txtCode.Text = doc_dl["Code"].ToString();
                    txtName.Text = doc_dl["Name"].ToString();
                    txtOffice.Text = doc_dl["Office"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho khoa/viện này!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                doc_dl.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải dữ liệu. Lỗi: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
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

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã khoa/viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khoa/viện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (_idKhoaVien == null) // Thêm mới
                {
                    // Kiểm tra trùng mã khoa/viện
                    if (kn.cnn.State != ConnectionState.Open)
                        kn.KetNoi_Dulieu();

                    string strKtra = "SELECT Code FROM Departments WHERE Code=N'" + txtCode.Text + "'";
                    SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                    SqlDataReader doc_dl = cmd.ExecuteReader();
                    if (doc_dl.Read())
                    {
                        MessageBox.Show("Mã khoa/viện đã tồn tại, vui lòng nhập mã khác!", "Thông báo",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCode.Focus();
                        doc_dl.Close();
                        return;
                    }
                    doc_dl.Close();

                    // Thêm mới
                    string sqlInsert = $"INSERT INTO Departments (Code, Name, Office) " +
                                       $"VALUES (N'{txtCode.Text}', N'{txtName.Text}', N'{txtOffice.Text}')";
                    kn.ThucThiSQL(sqlInsert);

                    MessageBox.Show("Thêm mới khoa/viện thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Cập nhật
                {
                    string sql_update = $"UPDATE Departments SET Name=N'{txtName.Text}', " +
                                      $"Office=N'{txtOffice.Text}' WHERE DeptID={_idKhoaVien.Value}";

                    kn.ThucThiSQL(sql_update);
                    MessageBox.Show("Cập nhật thông tin khoa/viện thành công!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void FrmKhoaVien_ChinhSua_Load(object sender, EventArgs e)
        {
            try
            {
                if (_idKhoaVien == null)
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

        // Xử lý phím tắt
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