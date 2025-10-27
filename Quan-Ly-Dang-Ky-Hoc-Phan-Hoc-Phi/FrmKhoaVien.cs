using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmKhoaVien : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();


        public FrmKhoaVien()
        {
            InitializeComponent();
          
        }

        public void Bang_KhoaVien()
        {
            DataTable dta = new DataTable();
            dta = kn.Lay_DulieuBang("SELECT * FROM Departments");
            dataKQ.DataSource = dta;
        }



        private void btnToaMoi_Click(object sender, EventArgs e)
        {
            FrmKhoaVien_ChinhSua f1 = new FrmKhoaVien_ChinhSua();
            f1.ShowDialog();
            Bang_KhoaVien();
        }

        private void FrmKhoaVien_Load(object sender, EventArgs e)
        {
            Bang_KhoaVien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataKQ.CurrentRow != null)
            {
                int idKhoaVien = Convert.ToInt32(dataKQ.CurrentRow.Cells["DeptID"].Value);
                FrmKhoaVien_ChinhSua frm = new FrmKhoaVien_ChinhSua(idKhoaVien);
                frm.ShowDialog();
                Bang_KhoaVien();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khoa viện để sửa!");
            }
        }
    }
}
