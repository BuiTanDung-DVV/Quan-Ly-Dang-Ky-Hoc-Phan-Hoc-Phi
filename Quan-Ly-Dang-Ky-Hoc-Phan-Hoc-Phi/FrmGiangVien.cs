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

        }
    }
}
