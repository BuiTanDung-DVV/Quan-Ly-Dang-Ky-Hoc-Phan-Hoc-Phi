using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmGiangVien : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();


        public FrmGiangVien()
        {
            InitializeComponent();
        }

        public void Bang_GiangVien()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Lecturers");
            dataKQ.DataSource = dta;
        }

        private void FrmGiangVien_Load(object sender, EventArgs e)
        {
            Bang_GiangVien();
        }

        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmGiangVien_ChinhSua f1 = new FrmGiangVien_ChinhSua();
            f1.ShowDialog();
            Bang_GiangVien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn dòng nào chưa
            if (dataKQ.CurrentRow != null)
            {
                // 2. Lấy ID (MaMonHoc) từ dòng đang chọn
                // (Thay "MaMonHoc" bằng TÊN CỘT ID trong DataGridView của bạn)
                int idGiangVien = Convert.ToInt32(dataKQ.CurrentRow.Cells["LecturerID"].Value);

                // 3. Mở Form chỉnh sửa và "gửi" ID qua
                FrmGiangVien_ChinhSua frm = new FrmGiangVien_ChinhSua(idGiangVien);
                frm.ShowDialog();

                // 4. Tải lại lưới sau khi Form chỉnh sửa đóng
                Bang_GiangVien();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một môn học để sửa!");
            }
        }
    }
}
