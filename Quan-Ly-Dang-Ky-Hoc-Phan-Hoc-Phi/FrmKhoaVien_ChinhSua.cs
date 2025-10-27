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

        public int CornerRadius { get; set; } = 30; // bán kính bo góc mặc định

        KETNOI_CSDL kn = new KETNOI_CSDL();
        public FrmKhoaVien_ChinhSua()
        {
            InitializeComponent();
            _idKhoaVien = null;

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            this.label1.Text = "Thêm Mới Khoa Viên";
            this.txtId.Enabled = false;
        }

        public FrmKhoaVien_ChinhSua(int idKhoaVien)
        {
            InitializeComponent();
            _idKhoaVien = idKhoaVien;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
            this.label1.Text = "Chỉnh Sửa Khoa Viên";
            this.txtId.Enabled = false;
            this.txtMaKhoa.Enabled = false;
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
                string sql = "SELECT * FROM Departments WHERE DeptID = " + _idKhoaVien.Value;

                // SỬA Ở ĐÂY: Dùng hàm Lay_DulieuBang
                DataTable dta = kn.Lay_DulieuBang(sql);

                if (dta.Rows.Count > 0)
                {
                    // Lấy dòng dữ liệu đầu tiên
                    DataRow doc_dl = dta.Rows[0];

                    // 7. Đổ dữ liệu vào TextBox
                    txtId.Text = doc_dl["DeptID"].ToString();
                    txtMaKhoa.Text = doc_dl["Code"].ToString();
                    txtTenKhoa.Text = doc_dl["Name"].ToString();
                    txtDiaChi.Text = doc_dl["Office"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho khoa/viện này!");
                    this.Close();
                }

                // Không cần doc_dl.Close() hay kn.cnn.Close() nữa
                // vì hàm Lay_DulieuBang đã tự quản lý
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải dữ liệu. Lỗi: " + ex.Message);
                this.Close();
            }
        }
        private void FrmKhoaVien_ChinhSua_Load(object sender, EventArgs e)
        {
            Load_DuLieuCanSua();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try // Bọc tất cả trong try...catch
            {
                if (_idKhoaVien == null)
                {
                    // Thêm mới
                    string strKtra = "Select Code from Departments where Code='" + txtMaKhoa.Text + "'";

                    // SỬA Ở ĐÂY: Dùng Lay_DulieuBang để kiểm tra
                    DataTable dtaCheck = kn.Lay_DulieuBang(strKtra);

                    if (dtaCheck.Rows.Count > 0) // Kiểm tra xem có dòng nào trả về không
                    {
                        MessageBox.Show("Mã đã tồn tại, vui lòng nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaKhoa.Focus();
                        // Không đóng form ở đây
                    }
                    else
                    {
                        // Mã không trùng, tiến hành Thêm mới
                        kn.ThucThiSQL(
                            "INSERT INTO Departments (Code, Name, Office) " +
                            "VALUES ('" + txtMaKhoa.Text + "', N'" + txtTenKhoa.Text + "', N'" + txtDiaChi.Text + "')"
                        );
                        MessageBox.Show("Lưu dữ liệu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close(); // Chỉ đóng form sau khi lưu thành công
                    }
                }
                else
                {
                    // Cập nhật (Phần này của bạn đã dùng kn.ThucThiSQL đúng)
                    // (Tôi cũng bỏ phần cập nhật "Code" vì txtMaKhoa đang bị disable)
                    string sql_Sua = "UPDATE Departments SET " +
                                     "Name = N'" + txtTenKhoa.Text + "', " +
                                     "Office = N'" + txtDiaChi.Text + "' " +
                                     "WHERE DeptID = " + _idKhoaVien.Value;
                    kn.ThucThiSQL(sql_Sua);
                    MessageBox.Show("Cập nhật khoa/viện thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi lưu: " + ex.Message);
            }
        }
    }
}
