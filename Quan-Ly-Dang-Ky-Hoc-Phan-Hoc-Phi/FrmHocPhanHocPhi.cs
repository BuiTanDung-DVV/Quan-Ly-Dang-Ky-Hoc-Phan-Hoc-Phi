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
    public partial class FrmHocPhanHocPhi : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();
        public int StudentID { get; set; }  // property để nhận từ form trước

        public FrmHocPhanHocPhi()
        {
            InitializeComponent();
        }

        private void FrmHocPhanHocPhi_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = $@"
            SELECT
                s.StudentID,
                s.StudentCode,
                s.FullName,
                c.CourseID,
                c.Code AS CourseCode,
                c.Name AS CourseName,
                c.Credits,
                cs.Room,
                cs.Schedule,
                i.InvoiceID,
                i.TotalAmount,
                i.DueDate
            FROM Students s
            INNER JOIN Invoices i ON s.StudentID = i.StudentID
            INNER JOIN InvoiceDetails id ON i.InvoiceID = id.InvoiceID
            INNER JOIN ClassSections cs ON id.SectionID = cs.SectionID
            INNER JOIN Courses c ON cs.CourseID = c.CourseID
            WHERE s.StudentID = {StudentID}
            ORDER BY i.CreatedDate, c.Name";

                DataTable dt = kn.Lay_DulieuBang(sql);

                rptHocPhanHocPhi1.SetDataSource(dt);  // Set nguồn dữ liệu cho Crystal Report
                crystalReportViewer1.ReportSource = rptHocPhanHocPhi1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi load dữ liệu report: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}
