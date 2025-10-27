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

        public int CornerRadius { get; set; } = 30; // bán kính bo góc mặc định

        KETNOI_CSDL kn = new KETNOI_CSDL();
        public FrmHocKi_ChinhSua()
        {
            InitializeComponent();
            _idHocKi = null;

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));

            this.label1.Text = "Thêm Mới Học Kì";
            this.txtId.Enabled = false;
        }

        public FrmHocKi_ChinhSua(int idHocKi)
        {
            InitializeComponent();
            _idHocKi = idHocKi;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, CornerRadius, CornerRadius));
            this.label1.Text = "Chỉnh Sửa Học Kì";
            this.txtId.Enabled = false;
            this.txtMaHK.Enabled = false;
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
                string sql = "SELECT * FROM AcademicTerms WHERE TermID = " + _idHocKi.Value;

                SqlCommand cmd = new SqlCommand(sql, kn.cnn);
                SqlDataReader doc_dl = cmd.ExecuteReader();

                if (doc_dl.Read() == true)
                {
                    // 7. Đổ dữ liệu vào TextBox
                    txtId.Text = doc_dl["TermID"].ToString();
                    txtMaHK.Text = doc_dl["Code"].ToString();
                    txtTenHK.Text = doc_dl["Name"].ToString();
                    txtDate1.Text = Convert.ToDateTime(doc_dl["StartDate"]).ToShortDateString();
                    txtDate2.Text = Convert.ToDateTime(doc_dl["EndDate"]).ToShortDateString();

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


        private void FrmHocKi_ChinhSua_Load(object sender, EventArgs e)
        {
            Load_DuLieuCanSua();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
