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
    public partial class FrmMonHoc : Form
    {

        public FrmMonHoc()
        {
            InitializeComponent();
        }

        KETNOI_CSDL kn = new KETNOI_CSDL();

        public void Bang_MonHoc()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Courses");
            dataKQ.DataSource = dta;
        }
        private void lblTenMon_Click(object sender, EventArgs e)
        {

        }

        private void txtTenLop_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmMonHoc_Load(object sender, EventArgs e)
        {
            Bang_MonHoc();
        }

        private void btnToaMoi_Click_1(object sender, EventArgs e)
        {
            FrmMonHoc_ChinhSua f1 = new FrmMonHoc_ChinhSua();

            f1.ShowDialog();

            Bang_MonHoc();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn dòng nào chưa
            if (dataKQ.CurrentRow != null)
            {
                // 2. Lấy ID (MaMonHoc) từ dòng đang chọn
                // (Thay "MaMonHoc" bằng TÊN CỘT ID trong DataGridView của bạn)
                int idMonHoc = Convert.ToInt32(dataKQ.CurrentRow.Cells["CourseID"].Value);

                // 3. Mở Form chỉnh sửa và "gửi" ID qua
                FrmMonHoc_ChinhSua frm = new FrmMonHoc_ChinhSua(idMonHoc);
                frm.ShowDialog();

                // 4. Tải lại lưới sau khi Form chỉnh sửa đóng
                Bang_MonHoc();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một môn học để sửa!");
            }
        }
    }
}
