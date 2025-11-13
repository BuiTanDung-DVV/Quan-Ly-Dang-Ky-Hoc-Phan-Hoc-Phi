namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmThanhToan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new Sunny.UI.UIPanel();
            this.lblTitle = new Sunny.UI.UILabel();
            this.panelFilters = new Sunny.UI.UIPanel();
            this.tableLayoutPanelFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblNamHoc = new Sunny.UI.UILabel();
            this.cboNamHoc = new Sunny.UI.UIComboBox();
            this.lblHocKi = new Sunny.UI.UILabel();
            this.cboHocKi = new Sunny.UI.UIComboBox();
            this.btnRefresh = new Sunny.UI.UIButton();
            this.btnXuatPhieu = new Sunny.UI.UIButton();
            this.panelSummary = new Sunny.UI.UIPanel();
            this.tableLayoutPanelSummary = new System.Windows.Forms.TableLayoutPanel();
            this.lblTongHoaDon = new Sunny.UI.UILabel();
            this.lblTongTien = new Sunny.UI.UILabel();
            this.lblDaThanhToan = new Sunny.UI.UILabel();
            this.lblConLai = new Sunny.UI.UILabel();
            this.uiDataGridView1 = new Sunny.UI.UIDataGridView();
            this.panelHeader.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.tableLayoutPanelFilters.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.tableLayoutPanelSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelHeader.MinimumSize = new System.Drawing.Size(1, 1);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.panelHeader.Radius = 0;
            this.panelHeader.Size = new System.Drawing.Size(1120, 50);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Text = null;
            this.panelHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(-35, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1166, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ THANH TOÁN HỌC PHÍ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFilters
            // 
            this.panelFilters.Controls.Add(this.tableLayoutPanelFilters);
            this.panelFilters.FillColor = System.Drawing.Color.White;
            this.panelFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.panelFilters.Location = new System.Drawing.Point(0, 50);
            this.panelFilters.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelFilters.MinimumSize = new System.Drawing.Size(1, 1);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.panelFilters.Radius = 0;
            this.panelFilters.Size = new System.Drawing.Size(1120, 70);
            this.panelFilters.TabIndex = 1;
            this.panelFilters.Text = null;
            this.panelFilters.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelFilters
            // 
            this.tableLayoutPanelFilters.ColumnCount = 6;
            this.tableLayoutPanelFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanelFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanelFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanelFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.403557F));
            this.tableLayoutPanelFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.05746F));
            this.tableLayoutPanelFilters.Controls.Add(this.lblNamHoc, 0, 0);
            this.tableLayoutPanelFilters.Controls.Add(this.cboNamHoc, 0, 1);
            this.tableLayoutPanelFilters.Controls.Add(this.lblHocKi, 1, 0);
            this.tableLayoutPanelFilters.Controls.Add(this.cboHocKi, 1, 1);
            this.tableLayoutPanelFilters.Controls.Add(this.btnRefresh, 3, 1);
            this.tableLayoutPanelFilters.Controls.Add(this.btnXuatPhieu, 5, 1);
            this.tableLayoutPanelFilters.Location = new System.Drawing.Point(15, 8);
            this.tableLayoutPanelFilters.Name = "tableLayoutPanelFilters";
            this.tableLayoutPanelFilters.RowCount = 2;
            this.tableLayoutPanelFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelFilters.Size = new System.Drawing.Size(1105, 54);
            this.tableLayoutPanelFilters.TabIndex = 0;
            // 
            // lblNamHoc
            // 
            this.lblNamHoc.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblNamHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblNamHoc.Location = new System.Drawing.Point(3, 0);
            this.lblNamHoc.Name = "lblNamHoc";
            this.lblNamHoc.Size = new System.Drawing.Size(287, 21);
            this.lblNamHoc.TabIndex = 0;
            this.lblNamHoc.Text = "📅 Năm học:";
            this.lblNamHoc.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboNamHoc
            // 
            this.cboNamHoc.DataSource = null;
            this.cboNamHoc.FillColor = System.Drawing.Color.White;
            this.cboNamHoc.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboNamHoc.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboNamHoc.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboNamHoc.Location = new System.Drawing.Point(5, 25);
            this.cboNamHoc.Margin = new System.Windows.Forms.Padding(5, 4, 8, 4);
            this.cboNamHoc.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboNamHoc.Name = "cboNamHoc";
            this.cboNamHoc.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboNamHoc.Radius = 6;
            this.cboNamHoc.Size = new System.Drawing.Size(280, 25);
            this.cboNamHoc.SymbolSize = 20;
            this.cboNamHoc.TabIndex = 1;
            this.cboNamHoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboNamHoc.Watermark = "Chọn năm học...";
            this.cboNamHoc.SelectedIndexChanged += new System.EventHandler(this.cboNamHoc_SelectedIndexChanged);
            // 
            // lblHocKi
            // 
            this.lblHocKi.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblHocKi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblHocKi.Location = new System.Drawing.Point(307, 0);
            this.lblHocKi.Name = "lblHocKi";
            this.lblHocKi.Size = new System.Drawing.Size(287, 21);
            this.lblHocKi.TabIndex = 2;
            this.lblHocKi.Text = "📚 Học kỳ:";
            this.lblHocKi.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboHocKi
            // 
            this.cboHocKi.DataSource = null;
            this.cboHocKi.FillColor = System.Drawing.Color.White;
            this.cboHocKi.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cboHocKi.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboHocKi.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboHocKi.Location = new System.Drawing.Point(309, 25);
            this.cboHocKi.Margin = new System.Windows.Forms.Padding(5, 4, 8, 4);
            this.cboHocKi.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboHocKi.Name = "cboHocKi";
            this.cboHocKi.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboHocKi.Radius = 6;
            this.cboHocKi.Size = new System.Drawing.Size(280, 25);
            this.cboHocKi.SymbolSize = 20;
            this.cboHocKi.TabIndex = 3;
            this.cboHocKi.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboHocKi.Watermark = "Chọn học kỳ...";
            this.cboHocKi.SelectedIndexChanged += new System.EventHandler(this.cboHocKi_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(634, 25);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnRefresh.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Radius = 6;
            this.btnRefresh.Size = new System.Drawing.Size(199, 25);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnXuatPhieu
            // 
            this.btnXuatPhieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatPhieu.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.btnXuatPhieu.Location = new System.Drawing.Point(907, 24);
            this.btnXuatPhieu.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnXuatPhieu.Name = "btnXuatPhieu";
            this.btnXuatPhieu.Radius = 6;
            this.btnXuatPhieu.Size = new System.Drawing.Size(187, 27);
            this.btnXuatPhieu.TabIndex = 5;
            this.btnXuatPhieu.Text = "📝 Xuất phiếu";
            this.btnXuatPhieu.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnXuatPhieu.Click += new System.EventHandler(this.btnXuatPhieu_Click);
            // 
            // panelSummary
            // 
            this.panelSummary.Controls.Add(this.tableLayoutPanelSummary);
            this.panelSummary.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.panelSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.panelSummary.Location = new System.Drawing.Point(0, 120);
            this.panelSummary.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSummary.MinimumSize = new System.Drawing.Size(1, 1);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.panelSummary.Radius = 0;
            this.panelSummary.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.panelSummary.Size = new System.Drawing.Size(1120, 55);
            this.panelSummary.TabIndex = 2;
            this.panelSummary.Text = null;
            this.panelSummary.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanelSummary
            // 
            this.tableLayoutPanelSummary.ColumnCount = 4;
            this.tableLayoutPanelSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelSummary.Controls.Add(this.lblTongHoaDon, 0, 0);
            this.tableLayoutPanelSummary.Controls.Add(this.lblTongTien, 1, 0);
            this.tableLayoutPanelSummary.Controls.Add(this.lblDaThanhToan, 2, 0);
            this.tableLayoutPanelSummary.Controls.Add(this.lblConLai, 3, 0);
            this.tableLayoutPanelSummary.Location = new System.Drawing.Point(15, 8);
            this.tableLayoutPanelSummary.Name = "tableLayoutPanelSummary";
            this.tableLayoutPanelSummary.RowCount = 1;
            this.tableLayoutPanelSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSummary.Size = new System.Drawing.Size(1105, 39);
            this.tableLayoutPanelSummary.TabIndex = 0;
            // 
            // lblTongHoaDon
            // 
            this.lblTongHoaDon.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongHoaDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTongHoaDon.Location = new System.Drawing.Point(3, 0);
            this.lblTongHoaDon.Name = "lblTongHoaDon";
            this.lblTongHoaDon.Size = new System.Drawing.Size(260, 39);
            this.lblTongHoaDon.TabIndex = 0;
            this.lblTongHoaDon.Text = "📊 Tổng hóa đơn: 0";
            this.lblTongHoaDon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTongTien
            // 
            this.lblTongTien.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.lblTongTien.Location = new System.Drawing.Point(279, 0);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(260, 39);
            this.lblTongTien.TabIndex = 1;
            this.lblTongTien.Text = "💰 Tổng tiền: 0 VNĐ";
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDaThanhToan
            // 
            this.lblDaThanhToan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblDaThanhToan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(0)))));
            this.lblDaThanhToan.Location = new System.Drawing.Point(555, 0);
            this.lblDaThanhToan.Name = "lblDaThanhToan";
            this.lblDaThanhToan.Size = new System.Drawing.Size(259, 39);
            this.lblDaThanhToan.TabIndex = 2;
            this.lblDaThanhToan.Text = "✅ Đã thanh toán: 0 VNĐ";
            this.lblDaThanhToan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConLai
            // 
            this.lblConLai.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblConLai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60)))));
            this.lblConLai.Location = new System.Drawing.Point(831, 0);
            this.lblConLai.Name = "lblConLai";
            this.lblConLai.Size = new System.Drawing.Size(264, 39);
            this.lblConLai.TabIndex = 3;
            this.lblConLai.Text = "⏳ Còn lại: 0 VNĐ";
            this.lblConLai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiDataGridView1
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.uiDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.uiDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.uiDataGridView1.ColumnHeadersHeight = 35;
            this.uiDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Times New Roman", 10F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.uiDataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.uiDataGridView1.EnableHeadersVisualStyles = false;
            this.uiDataGridView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiDataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.Location = new System.Drawing.Point(0, 175);
            this.uiDataGridView1.Name = "uiDataGridView1";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.uiDataGridView1.RowHeadersWidth = 50;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Times New Roman", 10F);
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.uiDataGridView1.RowTemplate.Height = 32;
            this.uiDataGridView1.SelectedIndex = -1;
            this.uiDataGridView1.Size = new System.Drawing.Size(1120, 441);
            this.uiDataGridView1.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.TabIndex = 3;
            this.uiDataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.uiDataGridView1_CellContentClick);
            // 
            // FrmThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1122, 615);
            this.Controls.Add(this.uiDataGridView1);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "FrmThanhToan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmThanhToan";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmThanhToan_FormClosed);
            this.Load += new System.EventHandler(this.FrmThanhToan_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelFilters.ResumeLayout(false);
            this.tableLayoutPanelFilters.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.tableLayoutPanelSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel panelHeader;
        private Sunny.UI.UILabel lblTitle;
        private Sunny.UI.UIPanel panelFilters;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFilters;
        private Sunny.UI.UILabel lblNamHoc;
        private Sunny.UI.UIComboBox cboNamHoc;
        private Sunny.UI.UILabel lblHocKi;
        private Sunny.UI.UIComboBox cboHocKi;
        private Sunny.UI.UIButton btnRefresh;
        private Sunny.UI.UIPanel panelSummary;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSummary;
        private Sunny.UI.UILabel lblTongHoaDon;
        private Sunny.UI.UILabel lblTongTien;
        private Sunny.UI.UILabel lblDaThanhToan;
        private Sunny.UI.UILabel lblConLai;
        private Sunny.UI.UIDataGridView uiDataGridView1;
        private Sunny.UI.UIButton btnXuatPhieu;
    }
}