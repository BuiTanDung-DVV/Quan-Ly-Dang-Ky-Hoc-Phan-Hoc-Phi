namespace Quan_Ly_Dang_Ky_Hoc_Phan_Hoc_Phi
{
    partial class FrmMonHoc
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
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.dataKQ = new Sunny.UI.UIDataGridView();
            this.txtTimKiem = new Sunny.UI.UITextBox();
            this.btnTimKiem = new Sunny.UI.UIButton();
            this.btnToaMoi = new Sunny.UI.UIButton();
            this.btnSua = new Sunny.UI.UIButton();
            this.btnXoa = new Sunny.UI.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataKQ)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDanhMuc
            // 
            this.lblDanhMuc.AutoSize = true;
            this.lblDanhMuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhMuc.Location = new System.Drawing.Point(674, 39);
            this.lblDanhMuc.Name = "lblDanhMuc";
            this.lblDanhMuc.Size = new System.Drawing.Size(274, 37);
            this.lblDanhMuc.TabIndex = 12;
            this.lblDanhMuc.Text = "Quản lý Môn học";
            // 
            // dataKQ
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dataKQ.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataKQ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataKQ.BackgroundColor = System.Drawing.Color.White;
            this.dataKQ.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataKQ.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataKQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataKQ.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataKQ.EnableHeadersVisualStyles = false;
            this.dataKQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dataKQ.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dataKQ.Location = new System.Drawing.Point(50, 198);
            this.dataKQ.Name = "dataKQ";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataKQ.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataKQ.RowHeadersWidth = 62;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dataKQ.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataKQ.RowTemplate.Height = 28;
            this.dataKQ.SelectedIndex = -1;
            this.dataKQ.Size = new System.Drawing.Size(1522, 577);
            this.dataKQ.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dataKQ.TabIndex = 13;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTimKiem.Location = new System.Drawing.Point(50, 118);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTimKiem.MinimumSize = new System.Drawing.Size(1, 16);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Padding = new System.Windows.Forms.Padding(5);
            this.txtTimKiem.Radius = 30;
            this.txtTimKiem.ShowText = false;
            this.txtTimKiem.Size = new System.Drawing.Size(602, 49);
            this.txtTimKiem.TabIndex = 14;
            this.txtTimKiem.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTimKiem.Watermark = "Tìm kiếm";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(782, 118);
            this.btnTimKiem.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Radius = 30;
            this.btnTimKiem.Size = new System.Drawing.Size(239, 49);
            this.btnTimKiem.TabIndex = 15;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // btnToaMoi
            // 
            this.btnToaMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToaMoi.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToaMoi.Location = new System.Drawing.Point(73, 813);
            this.btnToaMoi.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnToaMoi.Name = "btnToaMoi";
            this.btnToaMoi.Radius = 30;
            this.btnToaMoi.Size = new System.Drawing.Size(239, 49);
            this.btnToaMoi.TabIndex = 16;
            this.btnToaMoi.Text = "Tạo mới";
            this.btnToaMoi.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnToaMoi.Click += new System.EventHandler(this.btnToaMoi_Click);
            // 
            // btnSua
            // 
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(413, 813);
            this.btnSua.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSua.Name = "btnSua";
            this.btnSua.Radius = 30;
            this.btnSua.Size = new System.Drawing.Size(239, 49);
            this.btnSua.TabIndex = 17;
            this.btnSua.Text = "Sửa";
            this.btnSua.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(782, 813);
            this.btnXoa.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Radius = 30;
            this.btnXoa.Size = new System.Drawing.Size(239, 49);
            this.btnXoa.TabIndex = 18;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // FrmMonHoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1646, 892);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnToaMoi);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.dataKQ);
            this.Controls.Add(this.lblDanhMuc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmMonHoc";
            this.Text = "FrmMonHoc";
            this.Load += new System.EventHandler(this.FrmMonHoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataKQ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDanhMuc;
        private Sunny.UI.UIDataGridView dataKQ;
        private Sunny.UI.UITextBox txtTimKiem;
        private Sunny.UI.UIButton btnTimKiem;
        private Sunny.UI.UIButton btnToaMoi;
        private Sunny.UI.UIButton btnSua;
        private Sunny.UI.UIButton btnXoa;
    }
}