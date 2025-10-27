using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    public partial class FrmHocKi : Form
    {
        KETNOI_CSDL kn = new KETNOI_CSDL();
        string sql;
        DataTable dtTerms;

        public FrmHocKi()
        {
            InitializeComponent();
        }

        private void FrmHocKi_Load(object sender, EventArgs e)
        {
            LoadHocKi();
        }

        private void LoadHocKi()
        {
            sql = "SELECT TermID AS [Mã Kỳ], Code AS [Mã Học Kỳ], Name AS [Tên Học Kỳ], StartDate AS [Ngày Bắt Đầu], EndDate AS [Ngày Kết Thúc], IsCurrent AS [Đang Hiện Hành] FROM AcademicTerms";
            DataTable dt = kn.Lay_DulieuBang(sql);
            dataGridView1.DataSource = dt;
            // 🔹 Làm đẹp DataGridView
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 60, 120);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 240, 250);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            txtMaLop.Clear();      // Code
            txtTenLop.Clear();     // Name
            txtGhiChu.Clear();     // IsCurrent
            txtDate1.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            txtMaLop.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                bool isCurrent = txtGhiChu.Text.Trim() == "1" || txtGhiChu.Text.Trim().ToLower() == "true";

                sql = $"INSERT INTO AcademicTerms (Code, Name, StartDate, EndDate, IsCurrent) " +
                      $"VALUES ('{txtMaLop.Text}', N'{txtTenLop.Text}', " +
                      $"'{txtDate1.Value:yyyy-MM-dd}', '{dateTimePicker1.Value:yyyy-MM-dd}', '{(isCurrent ? 1 : 0)}')";

                kn.ThucThiSQL(sql);
                MessageBox.Show("Đã thêm kì học mới!");
                LoadHocKi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                bool isCurrent = txtGhiChu.Text.Trim() == "1" || txtGhiChu.Text.Trim().ToLower() == "true";

                sql = $"UPDATE AcademicTerms SET " +
                      $"Name = N'{txtTenLop.Text}', " +
                      $"StartDate = '{txtDate1.Value:yyyy-MM-dd}', " +
                      $"EndDate = '{dateTimePicker1.Value:yyyy-MM-dd}', " +
                      $"IsCurrent = '{(isCurrent ? 1 : 0)}' " +
                      $"WHERE Code = '{txtMaLop.Text}'";

                kn.ThucThiSQL(sql);
                MessageBox.Show("Đã cập nhật kì học!");
                LoadHocKi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                sql = $"DELETE FROM AcademicTerms WHERE Code = '{txtMaLop.Text}'";
                kn.ThucThiSQL(sql);
                MessageBox.Show("Đã xóa kì học!");
                LoadHocKi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtMaLop.Text = dataGridView1.Rows[i].Cells["Code"].Value.ToString();
                txtTenLop.Text = dataGridView1.Rows[i].Cells["Name"].Value.ToString();
                txtDate1.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells["StartDate"].Value);
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells["EndDate"].Value);
                txtGhiChu.Text = (bool)dataGridView1.Rows[i].Cells["IsCurrent"].Value ? "1" : "0";
            }
        }
    }
}
