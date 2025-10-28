using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmSinhVien : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();
        private bool isEditing = false;
        private int currentStudentID = 0;

        public FrmSinhVien()
        {
            InitializeComponent();
        }

        public void Bang_SinhVien()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Students");
            dataKQ.DataSource = dta;
        }

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            Bang_SinhVien();
        }


        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmSinhVien_ChinhSua f1 = new FrmSinhVien_ChinhSua();
            f1.ShowDialog();
            Bang_SinhVien();
        }
    }
}
