using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmLopHocPhan : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();


        public FrmLopHocPhan()
        {
            InitializeComponent();
        }

        public void Bang_LopHocPhan()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM ClassSections");
            dataKQ.DataSource = dta;
        }

        private void FrmLopHocPhan_Load(object sender, EventArgs e)
        {
            Bang_LopHocPhan();
        }

        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmLopHocPhan_ChinhSua f1 = new FrmLopHocPhan_ChinhSua();

            f1.ShowDialog();

            Bang_LopHocPhan();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataKQ.CurrentRow != null)
            {
                int idLopHocPhan = Convert.ToInt32(dataKQ.CurrentRow.Cells["SectionID"].Value);
                FrmLopHocPhan_ChinhSua f1 = new FrmLopHocPhan_ChinhSua(idLopHocPhan);
                f1.ShowDialog();
                Bang_LopHocPhan();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một lớp học phần để sửa!");
            }
        }
    }
}
