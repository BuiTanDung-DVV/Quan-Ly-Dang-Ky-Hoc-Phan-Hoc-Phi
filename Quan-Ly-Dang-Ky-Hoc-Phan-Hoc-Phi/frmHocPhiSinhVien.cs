using CrystalDecisions.CrystalReports.Engine;
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
    public partial class frmHocPhiSinhVien : Form
    {
        public frmHocPhiSinhVien()
        {
            InitializeComponent();
        }

        private void frmHocPhiSinhVien_Load(object sender, EventArgs e)
        {
            // 1. Khởi tạo lớp kết nối
            KETNOI_CSDL kn = new KETNOI_CSDL();

            // 2. Lấy dữ liệu từ view
            DataTable dt = kn.Lay_DulieuBang("SELECT * FROM vw_PaymentOverview");

            // 3. Khởi tạo report
            ReportDocument rpt = new ReportDocument();
            rpt.Load(@"D:\project\Quan-Ly-Dang-Ky-Hoc-Phan-Hoc-Phi\Quan-Ly-Dang-Ky-Hoc-Phan-Hoc-Phi\rptHocPhiSinhVien.rpt"); // thay bằng đường dẫn thật

            // 4. Gán dữ liệu cho report
            rpt.SetDataSource(dt);

            // 5. Hiển thị trên CrystalReportViewer
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
