using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmNganhHoc : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();
 
        public FrmNganhHoc()
        {
            InitializeComponent();
        }

        public void Bang_NganhHoc()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Majors");
            dataKQ.DataSource = dta;
        }

        private void FrmNganh_Load(object sender, EventArgs e)
        {
            Bang_NganhHoc();
        }

        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmNganhHoc_ChinhSua f1 = new FrmNganhHoc_ChinhSua();

            f1.ShowDialog();

            Bang_NganhHoc();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn dòng nào chưa
            if (dataKQ.CurrentRow != null)
            {
                // 2. Lấy ID (MaMonHoc) từ dòng đang chọn
                // (Thay "MaMonHoc" bằng TÊN CỘT ID trong DataGridView của bạn)
                int idNganhHoc = Convert.ToInt32(dataKQ.CurrentRow.Cells["MajorID"].Value);

                // 3. Mở Form chỉnh sửa và "gửi" ID qua
                FrmNganhHoc_ChinhSua frm = new FrmNganhHoc_ChinhSua(idNganhHoc);
                frm.ShowDialog();

                // 4. Tải lại lưới sau khi Form chỉnh sửa đóng
                Bang_NganhHoc();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một môn học để sửa!");
            }
        }
    }
}
