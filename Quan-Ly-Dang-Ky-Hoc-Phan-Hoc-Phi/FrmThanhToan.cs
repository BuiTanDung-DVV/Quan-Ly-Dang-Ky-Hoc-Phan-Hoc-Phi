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
    public partial class FrmThanhToan : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();
        private int currentStudentID = 0;

        public FrmThanhToan()
        {
            InitializeComponent();
        }

        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            // Thiết lập font cho ComboBox
            SetupComboBoxFonts();
            LoadStudentInfo();
            SetupDataGridView();
            LoadTerms();
            LoadInvoices();
            UpdateSummary();
        }

        private void SetupComboBoxFonts()
        {
            // Thiết lập font hỗ trợ tiếng Việt cho các ComboBox
            cboNamHoc.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 163);
            cboHocKi.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 163);
        }

        private void LoadStudentInfo()
        {
            if (UserSession.IsStudent() && UserSession.LinkedStudentID.HasValue)
            {
                currentStudentID = UserSession.LinkedStudentID.Value;
                lblTitle.Text = $"THANH TOÁN HỌC PHÍ - {UserSession.Username.ToUpper()}";
            }
            else if (UserSession.IsAdmin())
            {
                lblTitle.Text = "QUẢN LÝ THANH TOÁN HỌC PHÍ";
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private void SetupDataGridView()
        {
            uiDataGridView1.AutoGenerateColumns = false;
            uiDataGridView1.AllowUserToAddRows = false;
            uiDataGridView1.AllowUserToDeleteRows = false;
            uiDataGridView1.ReadOnly = true;
            uiDataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            uiDataGridView1.MultiSelect = false;

            // Xóa tất cả columns hiện tại
            uiDataGridView1.Columns.Clear();

            // Tạo các columns với styling tốt hơn
            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InvoiceID",
                HeaderText = "Mã HĐ",
                DataPropertyName = "InvoiceID",
                Width = 80,
                Visible = false
            });

            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentCode",
                HeaderText = "Mã SV",
                DataPropertyName = "StudentCode",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Times New Roman", 12, FontStyle.Bold) }
            });

            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StudentName",
                HeaderText = "Họ và tên",
                DataPropertyName = "StudentName",
                Width = 200
            });

            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TermName",
                HeaderText = "Học kỳ",
                DataPropertyName = "TermName",
                Width = 200
            });

            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAmount",
                HeaderText = "Tổng tiền",
                DataPropertyName = "TotalAmount",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "#,##0 VNĐ", 
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Times New Roman", 12, FontStyle.Bold),
                    ForeColor = Color.Blue
                }
            });

            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PaidAmount",
                HeaderText = "Đã thanh toán",
                DataPropertyName = "PaidAmount",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "#,##0 VNĐ", 
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Times New Roman", 12, FontStyle.Bold),
                    ForeColor = Color.Green
                }
            });

            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RemainingAmount",
                HeaderText = "Còn lại",
                DataPropertyName = "RemainingAmount",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "#,##0 VNĐ", 
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Times New Roman", 12, FontStyle.Bold),
                    ForeColor = Color.Red
                }
            });

            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedDate",
                HeaderText = "Ngày tạo",
                DataPropertyName = "CreatedDate",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            // Thêm cột DueDate để hiển thị hạn thanh toán
            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DueDate",
                HeaderText = "Hạn thanh toán",
                DataPropertyName = "DueDate",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            uiDataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Trạng thái",
                DataPropertyName = "Status",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Font = new Font("Times New Roman", 12, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Thêm nút thanh toán với styling đẹp hơn
            DataGridViewButtonColumn payButton = new DataGridViewButtonColumn
            {
                Name = "PayButton",
                HeaderText = "Thao tác",
                Text = "💳Thanh toán",
                UseColumnTextForButtonValue = true,
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(80, 160, 255),
                    ForeColor = Color.White,
                    Font = new Font("Times New Roman", 11, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            uiDataGridView1.Columns.Add(payButton);

            // Auto size columns
            uiDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadTerms()
        {
            try
            {
                string sql = "SELECT TermID, Name FROM AcademicTerms ORDER BY StartDate DESC";
                DataTable dt = kn.Lay_DulieuBang(sql);

                // Thêm option "Tất cả"
                DataRow allRow = dt.NewRow();
                allRow["TermID"] = 0;
                allRow["Name"] = "🔄 Tất cả học kỳ";
                dt.Rows.InsertAt(allRow, 0);

                cboHocKi.DisplayMember = "Name";
                cboHocKi.ValueMember = "TermID";
                cboHocKi.DataSource = dt;
                cboHocKi.SelectedIndex = 0;

                // Load năm học
                LoadAcademicYears();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load học kỳ: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAcademicYears()
        {
            try
            {
                // Sử dụng query đơn giản hơn và xử lý string trong C#
                string sql = @"SELECT DISTINCT 
                                YEAR(StartDate) as AcademicYear
                               FROM AcademicTerms 
                               ORDER BY AcademicYear DESC";
                DataTable dt = kn.Lay_DulieuBang(sql);

                // Tạo DataTable mới với cấu trúc cần thiết
                DataTable dtFormatted = new DataTable();
                dtFormatted.Columns.Add("AcademicYear", typeof(int));
                dtFormatted.Columns.Add("YearName", typeof(string));

                // Thêm option "Tất cả"
                DataRow allRow = dtFormatted.NewRow();
                allRow["AcademicYear"] = 0;
                allRow["YearName"] = "🔄 Tất cả năm học";
                dtFormatted.Rows.Add(allRow);

                // Thêm các năm học với format trong C#
                foreach (DataRow row in dt.Rows)
                {
                    int year = Convert.ToInt32(row["AcademicYear"]);
                    DataRow newRow = dtFormatted.NewRow();
                    newRow["AcademicYear"] = year;
                    newRow["YearName"] = $"📅 Năm học {year}-{year + 1}";
                    dtFormatted.Rows.Add(newRow);
                }

                cboNamHoc.DisplayMember = "YearName";
                cboNamHoc.ValueMember = "AcademicYear";
                cboNamHoc.DataSource = dtFormatted;
                cboNamHoc.SelectedIndex = 0;

                // Đảm bảo font được áp dụng sau khi set DataSource
                cboNamHoc.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 163);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load năm học: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoices()
        {
            try
            {
                string whereClause = "WHERE 1=1";

                // Nếu là sinh viên, chỉ xem hóa đơn của mình
                if (UserSession.IsStudent())
                {
                    whereClause += $" AND StudentID = {currentStudentID}";
                }

                // Lọc theo học kỳ
                if (cboHocKi.SelectedValue != null && Convert.ToInt32(cboHocKi.SelectedValue) > 0)
                {
                    whereClause += $" AND TermID = {cboHocKi.SelectedValue}";
                }

                // Lọc theo năm học
                if (cboNamHoc.SelectedValue != null && Convert.ToInt32(cboNamHoc.SelectedValue) > 0)
                {
                    whereClause += $@" AND TermID IN (
                        SELECT TermID FROM AcademicTerms 
                        WHERE YEAR(StartDate) = {cboNamHoc.SelectedValue}
                    )";
                }

                // Sử dụng VIEW để lấy dữ liệu chính xác
                string sql = $@"
                    SELECT 
                        InvoiceID,
                        StudentCode,
                        StudentName,
                        TermName,
                        TotalAmount,
                        PaidAmount,
                        RemainingAmount,
                        CreatedDate,
                        PaymentStatus as Status,
                        DueDate
                    FROM vw_PaymentOverview
                    {whereClause}
                    ORDER BY CreatedDate DESC, StudentCode";

                DataTable dt = kn.Lay_DulieuBang(sql);
                uiDataGridView1.DataSource = dt;

                // Enhanced row styling với logic cải tiến
                foreach (DataGridViewRow row in uiDataGridView1.Rows)
                {
                    if (row.Cells["Status"].Value != null)
                    {
                        string status = row.Cells["Status"].Value.ToString();
                        decimal remainingAmount = Convert.ToDecimal(row.Cells["RemainingAmount"].Value);
                        
                        // Kiểm tra hạn thanh toán - lấy từ DataTable thay vì cells
                        bool isOverdue = false;
                        int rowIndex = row.Index;
                        if (rowIndex < dt.Rows.Count && dt.Rows[rowIndex]["DueDate"] != DBNull.Value)
                        {
                            DateTime dueDate = Convert.ToDateTime(dt.Rows[rowIndex]["DueDate"]);
                            isOverdue = DateTime.Now > dueDate && remainingAmount > 0;
                        }

                        if (status.Contains("Đã thanh toán"))
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220); // Light green
                            row.Cells["PayButton"].ReadOnly = true;
                            row.Cells["PayButton"].Style.BackColor = Color.Gray;
                            row.Cells["PayButton"].Value = "✅ Hoàn tất";
                        }
                        else if (status.Contains("Thanh toán một phần"))
                        {
                            row.DefaultCellStyle.BackColor = isOverdue ? 
                                Color.FromArgb(255, 200, 150) : // Orange for overdue
                                Color.FromArgb(255, 255, 200); // Light yellow
                            row.Cells["PayButton"].Value = isOverdue ? "⚠️ Quá hạn" : "💳 Thanh toán";
                        }
                        else if (status.Contains("Chưa thanh toán"))
                        {
                            row.DefaultCellStyle.BackColor = isOverdue ? 
                                Color.FromArgb(255, 180, 180) : // Dark pink for overdue
                                Color.FromArgb(255, 220, 220); // Light pink
                            row.Cells["PayButton"].Value = isOverdue ? "🚨 Quá hạn" : "💳 Thanh toán";
                        }

                        // Disable payment button if no remaining amount
                        if (remainingAmount <= 0)
                        {
                            row.Cells["PayButton"].ReadOnly = true;
                            row.Cells["PayButton"].Style.BackColor = Color.Gray;
                            row.Cells["PayButton"].Value = "✅ Hoàn tất";
                        }

                        // Highlight overdue invoices
                        if (isOverdue && row.Cells["DueDate"] != null)
                        {
                            row.Cells["DueDate"].Style.ForeColor = Color.Red;
                            row.Cells["DueDate"].Style.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                        }
                    }
                }

                UpdateSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi load hóa đơn: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary()
        {
            try
            {
                if (uiDataGridView1.DataSource is DataTable dt && dt.Rows.Count > 0)
                {
                    int totalInvoices = dt.Rows.Count;
                    decimal totalAmount = 0;
                    decimal paidAmount = 0;
                    decimal remainingAmount = 0;
                    int overdueCount = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        totalAmount += Convert.ToDecimal(row["TotalAmount"]);
                        paidAmount += Convert.ToDecimal(row["PaidAmount"]);
                        remainingAmount += Convert.ToDecimal(row["RemainingAmount"]);
                        
                        // Đếm số hóa đơn quá hạn
                        if (row["DueDate"] != null && row["DueDate"] != DBNull.Value)
                        {
                            DateTime dueDate = Convert.ToDateTime(row["DueDate"]);
                            decimal remaining = Convert.ToDecimal(row["RemainingAmount"]);
                            if (DateTime.Now > dueDate && remaining > 0)
                            {
                                overdueCount++;
                            }
                        }
                    }

                    lblTongHoaDon.Text = $"📊 Tổng hóa đơn: {totalInvoices}" + 
                        (overdueCount > 0 ? $" (🚨 {overdueCount} quá hạn)" : "");
                    lblTongTien.Text = $"💰 Tổng tiền: {totalAmount:N0} VNĐ";
                    lblDaThanhToan.Text = $"✅ Đã thanh toán: {paidAmount:N0} VNĐ";
                    lblConLai.Text = $"⏳ Còn lại: {remainingAmount:N0} VNĐ";
                    
                    // Đổi màu nếu có hóa đơn quá hạn
                    if (overdueCount > 0)
                    {
                        lblTongHoaDon.ForeColor = Color.Red;
                        lblConLai.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblTongHoaDon.ForeColor = Color.FromArgb(48, 48, 48);
                        lblConLai.ForeColor = Color.FromArgb(220, 20, 60);
                    }
                }
                else
                {
                    lblTongHoaDon.Text = "📊 Tổng hóa đơn: 0";
                    lblTongTien.Text = "💰 Tổng tiền: 0 VNĐ";
                    lblDaThanhToan.Text = "✅ Đã thanh toán: 0 VNĐ";
                    lblConLai.Text = "⏳ Còn lại: 0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi cập nhật thống kê: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uiDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = uiDataGridView1.Columns[e.ColumnIndex].Name;
                
                if (columnName == "PayButton")
                {
                    DataGridViewRow row = uiDataGridView1.Rows[e.RowIndex];
                    int invoiceID = Convert.ToInt32(row.Cells["InvoiceID"].Value);
                    decimal remainingAmount = Convert.ToDecimal(row.Cells["RemainingAmount"].Value);
                    string status = row.Cells["Status"].Value.ToString();

                    if (status.Contains("Đã thanh toán") && remainingAmount <= 0)
                    {
                        MessageBox.Show("✅ Hóa đơn này đã được thanh toán đầy đủ!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Kiểm tra quyền thanh toán
                    if (UserSession.IsStudent())
                    {
                        if (!IsOwnInvoice(invoiceID))
                        {
                            MessageBox.Show("⚠️ Bạn chỉ được thanh toán hóa đơn của mình!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    ShowPaymentDialog(invoiceID, remainingAmount);
                }
            }
        }

        private bool IsOwnInvoice(int invoiceID)        
        {
            try
            {
                string sql = $"SELECT COUNT(*) FROM Invoices WHERE InvoiceID = {invoiceID} AND StudentID = {currentStudentID}";
                DataTable dt = kn.Lay_DulieuBang(sql);
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
            catch
            {
                return false;
            }
        }

        private void ShowPaymentDialog(int invoiceID, decimal remainingAmount)
        {
            FrmPaymentDialog paymentDialog = new FrmPaymentDialog(invoiceID, remainingAmount);
            if (paymentDialog.ShowDialog() == DialogResult.OK)
            {
                LoadInvoices(); // Refresh data after payment
                MessageBox.Show("🎉 Thanh toán thành công!\n" +
                              "💡 Số tiền còn lại đã được cập nhật tự động.\n" +
                              "📧 Biên lai thanh toán đã được lưu vào hệ thống.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cboHocKi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void cboNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInvoices();
            MessageBox.Show("🔄 Đã làm mới dữ liệu!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmThanhToan_FormClosed(object sender, FormClosedEventArgs e)
        {
            kn.NgatKetNoi();
        }
    }

    // Enhanced Payment Dialog với UI đẹp hơn
    public partial class FrmPaymentDialog : Form
    {
        private KETNOI_CSDL kn = new KETNOI_CSDL();
        private int invoiceID;
        private decimal remainingAmount;

        public FrmPaymentDialog(int invoiceID, decimal remainingAmount)
        {
            InitializeComponent();
            this.invoiceID = invoiceID;
            this.remainingAmount = remainingAmount;
            LoadPaymentDialog();
        }

        private void InitializeComponent()
        {
            this.lblAmount = new Label();
            this.txtAmount = new TextBox();
            this.lblMethod = new Label();
            this.cboMethod = new ComboBox();
            this.lblNote = new Label();
            this.txtNote = new TextBox();
            this.btnPay = new Button();
            this.btnCancel = new Button();
            this.lblTitle = new Label();
            this.lblRemaining = new Label();
            this.SuspendLayout();

            // Form properties
            this.Text = "Thanh toán học phí";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // lblTitle
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Size = new Size(440, 40);
            this.lblTitle.Text = "💳 THANH TOÁN HỌC PHÍ";
            this.lblTitle.Font = new Font("Times New Roman", 18, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(80, 160, 255);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // lblRemaining
            this.lblRemaining.Location = new Point(20, 70);
            this.lblRemaining.Size = new Size(440, 30);
            this.lblRemaining.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            this.lblRemaining.ForeColor = Color.Red;
            this.lblRemaining.TextAlign = ContentAlignment.MiddleCenter;

            // lblAmount
            this.lblAmount.Location = new Point(20, 120);
            this.lblAmount.Size = new Size(120, 25);
            this.lblAmount.Text = "💰 Số tiền:";
            this.lblAmount.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            // txtAmount
            this.txtAmount.Location = new Point(150, 120);
            this.txtAmount.Size = new Size(300, 25);
            this.txtAmount.Font = new Font("Times New Roman", 12);

            // lblMethod
            this.lblMethod.Location = new Point(20, 160);
            this.lblMethod.Size = new Size(120, 25);
            this.lblMethod.Text = "🏦 Phương thức:";
            this.lblMethod.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            // cboMethod
            this.cboMethod.Location = new Point(150, 160);
            this.cboMethod.Size = new Size(300, 25);
            this.cboMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboMethod.Font = new Font("Times New Roman", 12);

            // lblNote
            this.lblNote.Location = new Point(20, 200);
            this.lblNote.Size = new Size(120, 25);
            this.lblNote.Text = "📝 Ghi chú:";
            this.lblNote.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            // txtNote
            this.txtNote.Location = new Point(150, 200);
            this.txtNote.Size = new Size(300, 80);
            this.txtNote.Multiline = true;
            this.txtNote.Font = new Font("Times New Roman", 12);

            // btnPay
            this.btnPay.Location = new Point(150, 300);
            this.btnPay.Size = new Size(120, 40);
            this.btnPay.Text = "💳 Thanh toán";
            this.btnPay.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            this.btnPay.BackColor = Color.FromArgb(80, 160, 255);
            this.btnPay.ForeColor = Color.White;
            this.btnPay.FlatStyle = FlatStyle.Flat;
            this.btnPay.UseVisualStyleBackColor = false;
            this.btnPay.Click += new EventHandler(this.btnPay_Click);

            // btnCancel
            this.btnCancel.Location = new Point(290, 300);
            this.btnCancel.Size = new Size(120, 40);
            this.btnCancel.Text = "❌ Hủy";
            this.btnCancel.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.DialogResult = DialogResult.Cancel;

            // Add controls to form
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblRemaining);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.cboMethod);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        private Label lblTitle;
        private Label lblRemaining;
        private Label lblAmount;
        private TextBox txtAmount;
        private Label lblMethod;
        private ComboBox cboMethod;
        private Label lblNote;
        private TextBox txtNote;
        private Button btnPay;
        private Button btnCancel;

        private void LoadPaymentDialog()
        {
            lblRemaining.Text = $"Số tiền cần thanh toán: {remainingAmount:N0} VNĐ";
            txtAmount.Text = remainingAmount.ToString("N0");
            
            cboMethod.Items.Add("💵 Tiền mặt");
            cboMethod.Items.Add("🏦 Chuyển khoản");
            cboMethod.Items.Add("💳 Thẻ tín dụng");
            cboMethod.Items.Add("📱 Ví điện tử");
            cboMethod.SelectedIndex = 0;

            txtNote.Text = $"Thanh toán học phí - Hóa đơn #{invoiceID}";
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                string amountText = txtAmount.Text.Replace(",", "").Replace(".", "");
                decimal payAmount;
                
                if (!decimal.TryParse(amountText, out payAmount))
                {
                    MessageBox.Show("❌ Số tiền không hợp lệ!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                if (payAmount <= 0)
                {
                    MessageBox.Show("❌ Số tiền thanh toán phải lớn hơn 0!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (payAmount > remainingAmount)
                {
                    MessageBox.Show($"❌ Số tiền thanh toán không được vượt quá số tiền còn lại ({remainingAmount:N0} VNĐ)!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Xác nhận thanh toán với thông tin chi tiết
                DialogResult confirm = MessageBox.Show(
                    $"💳 XÁC NHẬN THANH TOÁN\n\n" +
                    $"💰 Số tiền: {payAmount:N0} VNĐ\n" +
                    $"🏦 Phương thức: {cboMethod.Text}\n" +
                    $"📝 Ghi chú: {txtNote.Text}\n" +
                    $"⏰ Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n" +
                    $"Bạn có chắc chắn muốn thanh toán?", 
                    "Xác nhận thanh toán", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes) return;

                // Thực hiện thanh toán
                string sql = $@"
                    INSERT INTO Payments (InvoiceID, PaymentDate, AmountPaid, Method, Note)
                    VALUES ({invoiceID}, GETDATE(), {payAmount}, N'{cboMethod.Text.Replace("'", "''")}', N'{txtNote.Text.Replace("'", "''")}')";

                kn.ThucThiSQL(sql);

                // Cập nhật trạng thái hóa đơn dựa trên tổng số tiền đã thanh toán
                string updateStatusSql = $@"
                    DECLARE @TotalPaid DECIMAL(12,2);
                    DECLARE @InvoiceAmount DECIMAL(12,2);
                    
                    SELECT @TotalPaid = ISNULL(SUM(AmountPaid), 0) FROM Payments WHERE InvoiceID = {invoiceID};
                    SELECT @InvoiceAmount = TotalAmount FROM Invoices WHERE InvoiceID = {invoiceID};
                    
                    UPDATE Invoices 
                    SET Status = CASE 
                        WHEN @TotalPaid >= @InvoiceAmount THEN N'Đã thanh toán'
                        WHEN @TotalPaid > 0 THEN N'Thanh toán một phần'
                        ELSE N'Chưa thanh toán'
                    END,
                    IsPaid = CASE 
                        WHEN @TotalPaid >= @InvoiceAmount THEN 1 
                        ELSE 0 
                    END
                    WHERE InvoiceID = {invoiceID}";

                kn.ThucThiSQL(updateStatusSql);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thanh toán: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
