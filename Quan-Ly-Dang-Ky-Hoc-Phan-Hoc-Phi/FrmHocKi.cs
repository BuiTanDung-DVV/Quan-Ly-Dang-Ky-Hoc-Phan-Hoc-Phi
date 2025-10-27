using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmHocKi : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();

        public FrmHocKi()
        {
            InitializeComponent();
        }

        public void Bang_HocKi()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM AcademicTerms");
            dataKQ.DataSource = dta;
        }

        private void FrmHocKi_Load(object sender, EventArgs e)
        {
            Bang_HocKi();
        }

        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmHocKi_ChinhSua f1 = new FrmHocKi_ChinhSua();
            f1.ShowDialog();
            Bang_HocKi();
        }

        private void btnSua1_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn dòng nào chưa
            if (dataKQ.CurrentRow != null)
            {
                // 2. Lấy ID (MaMonHoc) từ dòng đang chọn
                int idHocKi = Convert.ToInt32(dataKQ.CurrentRow.Cells["TermID"].Value);

                // 3. Mở Form chỉnh sửa và "gửi" ID qua
                FrmHocKi_ChinhSua f1 = new FrmHocKi_ChinhSua(idHocKi);
                f1.ShowDialog();

                // 4. Tải lại lưới sau khi Form chỉnh sửa đóng
                Bang_HocKi();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một môn học để sửa!");
            }
        }
    }
}
