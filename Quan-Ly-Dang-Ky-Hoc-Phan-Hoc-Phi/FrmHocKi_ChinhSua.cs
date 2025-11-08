using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmHocKi_ChinhSua : Form
    {
        private int? _idHocKi;

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
        public FrmHocKi_ChinhSua()
        {
            InitializeComponent();
            _idHocKi = null;
            InitializeForm();
            this.label1.Text = "📅 THÊM MỚI HỌC KỲ";
            this.txtTermID.Enabled = false;
        }

        // Constructor chỉnh sửa
        public FrmHocKi_ChinhSua(int idHocKi)
        {
            InitializeComponent();
            _idHocKi = idHocKi;
            InitializeForm();
            this.label1.Text = "✏️ CHỈNH SỬA HỌC KỲ";
            this.txtTermID.Enabled = false;
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
            if (_idHocKi == null) return;

            try
            {
                if (kn.cnn.State != ConnectionState.Open)
                    kn.KetNoi_Dulieu();

                string sql = "SELECT * FROM AcademicTerms WHERE TermID = " + _idHocKi.Value;
                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read())
                {
                    txtTermID.Text = doc_dl["TermID"].ToString();
                    txtCode.Text = doc_dl["Code"].ToString();
                    txtName.Text = doc_dl["Name"].ToString();
                    dtpStartDate.Value = doc_dl["StartDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(doc_dl["StartDate"]);
                    dtpEndDate.Value = doc_dl["EndDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(doc_dl["EndDate"]);
                    chkIsCurrent.Checked = doc_dl["IsCurrent"] != DBNull.Value && Convert.ToBoolean(doc_dl["IsCurrent"]);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho học kỳ này!", "Thông báo",
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
                MessageBox.Show("Vui lòng nhập mã học kỳ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên học kỳ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpStartDate.Focus();
                return false;
            }
            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (_idHocKi == null) // Thêm mới
                {
                    // Kiểm tra trùng mã học kỳ
                    if (kn.cnn.State != ConnectionState.Open)
                        kn.KetNoi_Dulieu();

                    string strKtra = "SELECT Code FROM AcademicTerms WHERE Code=N'" + txtCode.Text + "'";
                    SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                    SqlDataReader doc_dl = cmd.ExecuteReader();
                    if (doc_dl.Read())
                    {
                        MessageBox.Show("Mã học kỳ đã tồn tại, vui lòng nhập mã khác!", "Thông báo",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCode.Focus();
                        doc_dl.Close();
                        return;
                    }
                    doc_dl.Close();

                    // Thêm mới
                    string sqlInsert = $"INSERT INTO AcademicTerms (Code, Name, StartDate, EndDate, IsCurrent) " +
                                       $"VALUES (N'{txtCode.Text}', N'{txtName.Text}', '{dtpStartDate.Value:yyyy-MM-dd}', '{dtpEndDate.Value:yyyy-MM-dd}', {(chkIsCurrent.Checked ? 1 : 0)})";
                    kn.ThucThiSQL(sqlInsert);

                    MessageBox.Show("Thêm mới học kỳ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Cập nhật
                {
                    string sql_update = $"UPDATE AcademicTerms SET Name=N'{txtName.Text}', " +
                                      $"StartDate='{dtpStartDate.Value:yyyy-MM-dd}', EndDate='{dtpEndDate.Value:yyyy-MM-dd}', IsCurrent={(chkIsCurrent.Checked ? 1 : 0)} " +
                                      $"WHERE TermID={_idHocKi.Value}";

                    kn.ThucThiSQL(sql_update);
                    MessageBox.Show("Cập nhật thông tin học kỳ thành công!", "Thông báo",
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

        private void FrmHocKi_ChinhSua_Load(object sender, EventArgs e)
        {
            try
            {
                // Setup DateTimePicker
                dtpStartDate.MinDate = new DateTime(2000, 1, 1);
                dtpEndDate.MinDate = new DateTime(2000, 1, 1);
                dtpStartDate.MaxDate = new DateTime(2099, 12, 31);
                dtpEndDate.MaxDate = new DateTime(2099, 12, 31);

                if (_idHocKi == null)
                {
                    dtpStartDate.Value = DateTime.Today;
                    dtpEndDate.Value = DateTime.Today.AddMonths(4);
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