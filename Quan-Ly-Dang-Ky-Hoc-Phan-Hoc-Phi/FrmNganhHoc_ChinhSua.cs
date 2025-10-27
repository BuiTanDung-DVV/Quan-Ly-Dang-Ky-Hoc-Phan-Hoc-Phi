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

        public int CornerRadius { get; set; } = 30; // bán kính bo góc mặc định

        KETNOI_CSDL kn = new KETNOI_CSDL();
        public FrmNganhHoc_ChinhSua()
        {
            InitializeComponent();
            _idNganhHoc = null;

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            this.label1.Text = "Thêm Mới Ngành Học";
            this.txtId.Enabled = false;
        }

        public FrmNganhHoc_ChinhSua(int idNganhHoc)
        {
            InitializeComponent();
            _idNganhHoc = idNganhHoc;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            this.label1.Text = "Chỉnh Sửa Ngành Học";
            this.txtId.Enabled = false;
            this.txtMaNganh.Enabled = false;
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
            cboKhoaVien.DataSource = dta;
            cboKhoaVien.DisplayMember = "Name";
            cboKhoaVien.ValueMember = "DeptID";
        }

        private void FrmNganhHoc_ChinhSua_Load(object sender, EventArgs e)
        {
            Bang_KhoaVien();
            Load_DuLieuCanSua();
        }
        private void Load_DuLieuCanSua()
        {
            if (_idNganhHoc == null) return;

            try
            {
                string sql = "SELECT * FROM Majors WHERE MajorID = " + _idNganhHoc.Value;

                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read() == true)
                {
                    // 7. Đổ dữ liệu vào TextBox
                    txtId.Text = doc_dl["MajorID"].ToString();
                    txtMaNganh.Text = doc_dl["Code"].ToString();
                    txtTenNganh.Text = doc_dl["Name"].ToString();
                    cboKhoaVien.SelectedValue = doc_dl["DeptID"];
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_idNganhHoc == null)
            {
                // Thêm mới môn học
                string strKtra = "Select Code from Majors where Code='" + txtMaNganh.Text + "'";
                SqlCommand cmd = new SqlCommand(strKtra, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read() == true)
                {
                    MessageBox.Show("Mã đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNganh.Focus();
                }
                else
                {
                    kn.ThucThiSQL("INSERT INTO Majors (Code, Name, DeptID) VALUES ('" + txtMaNganh.Text + "',N'" + txtTenNganh.Text + "'," + cboKhoaVien.SelectedValue + ")");
                    MessageBox.Show("Lưu dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
    
            }
            else
            {
                // Cập nhật môn học
                string sql_Sua = "UPDATE Majors SET Name=N'" + txtTenNganh.Text + "', DeptID=" + cboKhoaVien.SelectedValue + " WHERE MajorID=" + _idNganhHoc.Value;
                kn.ThucThiSQL(sql_Sua);
                MessageBox.Show("Cập nhật môn học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }
    }
}
