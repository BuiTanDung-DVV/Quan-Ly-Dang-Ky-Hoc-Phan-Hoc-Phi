using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmMain : Form
    {
        FrmSinhVien SinhVien;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void sataPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sataPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void sataButton2_Click(object sender, EventArgs e)
        {
            if (SinhVien == null || SinhVien.IsDisposed)
            {
                SinhVien = new FrmSinhVien();
                SinhVien.MdiParent = this;
                SinhVien.Show();
            }
            else
            {
                SinhVien.Activate();
            }
        }
    }
}
