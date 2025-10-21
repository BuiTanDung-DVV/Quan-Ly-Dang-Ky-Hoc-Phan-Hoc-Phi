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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelThanhToan = new Sunny.UI.UIPanel();
            this.cboHocKi = new Sunny.UI.UIComboBox();
            this.cboNamHoc = new Sunny.UI.UIComboBox();
            this.uiDataGridView1 = new Sunny.UI.UIDataGridView();
            this.panelThanhToan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelThanhToan
            // 
            this.panelThanhToan.Controls.Add(this.cboHocKi);
            this.panelThanhToan.Controls.Add(this.cboNamHoc);
            this.panelThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.panelThanhToan.Location = new System.Drawing.Point(53, 11);
            this.panelThanhToan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelThanhToan.MinimumSize = new System.Drawing.Size(1, 1);
            this.panelThanhToan.Name = "panelThanhToan";
            this.panelThanhToan.Radius = 25;
            this.panelThanhToan.Size = new System.Drawing.Size(1456, 66);
            this.panelThanhToan.TabIndex = 0;
            this.panelThanhToan.Text = null;
            this.panelThanhToan.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboHocKi
            // 
            this.cboHocKi.DataSource = null;
            this.cboHocKi.FillColor = System.Drawing.Color.White;
            this.cboHocKi.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHocKi.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboHocKi.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboHocKi.Location = new System.Drawing.Point(569, 14);
            this.cboHocKi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboHocKi.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboHocKi.Name = "cboHocKi";
            this.cboHocKi.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboHocKi.Radius = 30;
            this.cboHocKi.Size = new System.Drawing.Size(356, 47);
            this.cboHocKi.SymbolSize = 24;
            this.cboHocKi.TabIndex = 1;
            this.cboHocKi.Text = "Học kì";
            this.cboHocKi.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboHocKi.Watermark = "";
            // 
            // cboNamHoc
            // 
            this.cboNamHoc.DataSource = null;
            this.cboNamHoc.FillColor = System.Drawing.Color.White;
            this.cboNamHoc.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboNamHoc.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.cboNamHoc.ItemSelectForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.cboNamHoc.Location = new System.Drawing.Point(14, 14);
            this.cboNamHoc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cboNamHoc.MinimumSize = new System.Drawing.Size(63, 0);
            this.cboNamHoc.Name = "cboNamHoc";
            this.cboNamHoc.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cboNamHoc.Radius = 30;
            this.cboNamHoc.Size = new System.Drawing.Size(387, 47);
            this.cboNamHoc.SymbolSize = 24;
            this.cboNamHoc.TabIndex = 0;
            this.cboNamHoc.Text = "Năm học";
            this.cboNamHoc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cboNamHoc.Watermark = "";
            // 
            // uiDataGridView1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.uiDataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.uiDataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.uiDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.uiDataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.uiDataGridView1.EnableHeadersVisualStyles = false;
            this.uiDataGridView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiDataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.Location = new System.Drawing.Point(53, 84);
            this.uiDataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uiDataGridView1.Name = "uiDataGridView1";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.uiDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.uiDataGridView1.RowHeadersWidth = 62;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiDataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.uiDataGridView1.RowTemplate.Height = 28;
            this.uiDataGridView1.SelectedIndex = -1;
            this.uiDataGridView1.Size = new System.Drawing.Size(1455, 641);
            this.uiDataGridView1.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiDataGridView1.TabIndex = 1;
            this.uiDataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.uiDataGridView1_CellContentClick);
            // 
            // FrmThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1540, 734);
            this.Controls.Add(this.uiDataGridView1);
            this.Controls.Add(this.panelThanhToan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmThanhToan";
            this.Text = "FrmThanhToan";
            this.panelThanhToan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIPanel panelThanhToan;
        private Sunny.UI.UIDataGridView uiDataGridView1;
        private Sunny.UI.UIComboBox cboNamHoc;
        private Sunny.UI.UIComboBox cboHocKi;
    }
}