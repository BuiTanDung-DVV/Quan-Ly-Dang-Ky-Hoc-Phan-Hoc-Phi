namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmHocKi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.dataKQ = new Sunny.UI.UIDataGridView();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnXoa = new Sunny.UI.UIButton();
            this.btnSua = new Sunny.UI.UIButton();
            this.btnThemMoi = new Sunny.UI.UIButton();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.btnTimKiem = new Sunny.UI.UIButton();
            this.txtTimKiem = new Sunny.UI.UITextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.groupBoxData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataKQ)).BeginInit();
            this.groupBoxActions.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            this.pnlTop.Controls.Add(this.lblDanhMuc);
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1123, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblDanhMuc
            // 
            this.lblDanhMuc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDanhMuc.AutoSize = true;
            this.lblDanhMuc.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblDanhMuc.ForeColor = System.Drawing.Color.White;
            this.lblDanhMuc.Location = new System.Drawing.Point(456, 17);
            this.lblDanhMuc.Name = "lblDanhMuc";
            this.lblDanhMuc.Size = new System.Drawing.Size(196, 25);
            this.lblDanhMuc.TabIndex = 0;
            this.lblDanhMuc.Text = "📅 QUẢN LÝ HỌC KỲ";
            this.lblDanhMuc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.groupBoxData);
            this.pnlMain.Controls.Add(this.groupBoxActions);
            this.pnlMain.Controls.Add(this.groupBoxSearch);
            this.pnlMain.Location = new System.Drawing.Point(12, 56);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Size = new System.Drawing.Size(1098, 547);
            this.pnlMain.TabIndex = 1;
            // 
            // groupBoxData
            // 
            this.groupBoxData.Controls.Add(this.dataKQ);
            this.groupBoxData.Location = new System.Drawing.Point(10, 110);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxData.Size = new System.Drawing.Size(1075, 348);
            this.groupBoxData.TabIndex = 2;
            this.groupBoxData.TabStop = false;
            this.groupBoxData.Text = "📊 Danh sách Học Kỳ";
            // 
            // dataKQ
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dataKQ.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataKQ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataKQ.BackgroundColor = System.Drawing.Color.White;
            this.dataKQ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataKQ.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataKQ.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataKQ.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataKQ.ColumnHeadersHeight = 45;
            this.dataKQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(79)))), ((int)(((byte)(147)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataKQ.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataKQ.EnableHeadersVisualStyles = false;
            this.dataKQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dataKQ.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dataKQ.Location = new System.Drawing.Point(10, 30);
            this.dataKQ.Name = "dataKQ";
            this.dataKQ.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataKQ.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataKQ.RowHeadersVisible = false;
            this.dataKQ.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.dataKQ.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataKQ.RowTemplate.Height = 35;
            this.dataKQ.SelectedIndex = -1;
            this.dataKQ.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataKQ.Size = new System.Drawing.Size(1052, 305);
            this.dataKQ.StripeEvenColor = System.Drawing.Color.Empty;
            this.dataKQ.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dataKQ.TabIndex = 0;
            this.dataKQ.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataKQ_CellDoubleClick);
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.btnXoa);
            this.groupBoxActions.Controls.Add(this.btnSua);
            this.groupBoxActions.Controls.Add(this.btnThemMoi);
            this.groupBoxActions.Location = new System.Drawing.Point(10, 464);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxActions.Size = new System.Drawing.Size(1075, 70);
            this.groupBoxActions.TabIndex = 1;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "⚡ Thao tác";
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnXoa.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnXoa.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.Location = new System.Drawing.Point(712, 28);
            this.btnXoa.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Radius = 8;
            this.btnXoa.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnXoa.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnXoa.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnXoa.Size = new System.Drawing.Size(130, 30);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "🗑️ Xóa";
            this.btnXoa.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(183)))), ((int)(((byte)(0)))));
            this.btnSua.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(163)))), ((int)(((byte)(0)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.Black;
            this.btnSua.Location = new System.Drawing.Point(452, 28);
            this.btnSua.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSua.Name = "btnSua";
            this.btnSua.Radius = 8;
            this.btnSua.Size = new System.Drawing.Size(130, 30);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "✏️ Sửa";
            this.btnSua.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThemMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnThemMoi.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(186)))), ((int)(((byte)(91)))));
            this.btnThemMoi.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(164)))), ((int)(((byte)(69)))));
            this.btnThemMoi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThemMoi.Location = new System.Drawing.Point(192, 28);
            this.btnThemMoi.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Radius = 8;
            this.btnThemMoi.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnThemMoi.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(186)))), ((int)(((byte)(91)))));
            this.btnThemMoi.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(164)))), ((int)(((byte)(69)))));
            this.btnThemMoi.Size = new System.Drawing.Size(130, 30);
            this.btnThemMoi.TabIndex = 0;
            this.btnThemMoi.Text = "➕ Thêm mới";
            this.btnThemMoi.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.btnTimKiem);
            this.groupBoxSearch.Controls.Add(this.txtTimKiem);
            this.groupBoxSearch.Controls.Add(this.label1);
            this.groupBoxSearch.Location = new System.Drawing.Point(10, 10);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxSearch.Size = new System.Drawing.Size(1075, 80);
            this.groupBoxSearch.TabIndex = 0;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "🔍 Tìm kiếm";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnTimKiem.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(142)))), ((int)(((byte)(199)))));
            this.btnTimKiem.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(132)))), ((int)(((byte)(179)))));
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.Location = new System.Drawing.Point(925, 40);
            this.btnTimKiem.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Radius = 8;
            this.btnTimKiem.Size = new System.Drawing.Size(130, 30);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "🔍 Tìm kiếm";
            this.btnTimKiem.TipsFont = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTimKiem.Location = new System.Drawing.Point(183, 40);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTimKiem.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);
            this.txtTimKiem.Radius = 8;
            this.txtTimKiem.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.txtTimKiem.ShowText = false;
            this.txtTimKiem.Size = new System.Drawing.Size(725, 30);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTimKiem.Watermark = "Nhập mã, tên hoặc năm học để tìm kiếm...";
            this.txtTimKiem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimKiem_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(13, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập từ khóa tìm kiếm:";
            // 
            // FrmHocKi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1122, 615);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmHocKi";
            this.Text = "Quản lý Học kỳ";
            this.Load += new System.EventHandler(this.FrmHocKi_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.groupBoxData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataKQ)).EndInit();
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblDanhMuc;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UITextBox txtTimKiem;
        private Sunny.UI.UIButton btnTimKiem;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private Sunny.UI.UIButton btnThemMoi;
        private Sunny.UI.UIButton btnSua;
        private Sunny.UI.UIButton btnXoa;
        private System.Windows.Forms.GroupBox groupBoxData;
        private Sunny.UI.UIDataGridView dataKQ;
    }
}