namespace Punto_ventas
{
    partial class frmReportes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comboBoxReporte = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerar = new FontAwesome.Sharp.IconButton();
            this.textTotal = new System.Windows.Forms.TextBox();
            this.dataGridViewReporte = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxReporte
            // 
            this.comboBoxReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReporte.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxReporte.FormattingEnabled = true;
            this.comboBoxReporte.Items.AddRange(new object[] {
            "Reporte ventas"});
            this.comboBoxReporte.Location = new System.Drawing.Point(422, 38);
            this.comboBoxReporte.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxReporte.Name = "comboBoxReporte";
            this.comboBoxReporte.Size = new System.Drawing.Size(352, 39);
            this.comboBoxReporte.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(88, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 32);
            this.label1.TabIndex = 31;
            this.label1.Text = "Reporte a generar";
            // 
            // btnGenerar
            // 
            this.btnGenerar.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F);
            this.btnGenerar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnGenerar.IconColor = System.Drawing.Color.Black;
            this.btnGenerar.IconSize = 16;
            this.btnGenerar.Location = new System.Drawing.Point(789, 36);
            this.btnGenerar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Rotation = 0D;
            this.btnGenerar.Size = new System.Drawing.Size(231, 46);
            this.btnGenerar.TabIndex = 32;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // textTotal
            // 
            this.textTotal.BackColor = System.Drawing.Color.Yellow;
            this.textTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textTotal.ForeColor = System.Drawing.Color.Red;
            this.textTotal.Location = new System.Drawing.Point(1507, 617);
            this.textTotal.Margin = new System.Windows.Forms.Padding(4);
            this.textTotal.Multiline = true;
            this.textTotal.Name = "textTotal";
            this.textTotal.ReadOnly = true;
            this.textTotal.ShortcutsEnabled = false;
            this.textTotal.Size = new System.Drawing.Size(308, 47);
            this.textTotal.TabIndex = 33;
            // 
            // dataGridViewReporte
            // 
            this.dataGridViewReporte.AllowUserToAddRows = false;
            this.dataGridViewReporte.AllowUserToDeleteRows = false;
            this.dataGridViewReporte.AllowUserToResizeRows = false;
            this.dataGridViewReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReporte.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewReporte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewReporte.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewReporte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewReporte.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewReporte.Location = new System.Drawing.Point(49, 138);
            this.dataGridViewReporte.Name = "dataGridViewReporte";
            this.dataGridViewReporte.ReadOnly = true;
            this.dataGridViewReporte.RowHeadersWidth = 51;
            this.dataGridViewReporte.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewReporte.RowTemplate.Height = 24;
            this.dataGridViewReporte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReporte.Size = new System.Drawing.Size(1766, 463);
            this.dataGridViewReporte.TabIndex = 34;
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1839, 702);
            this.Controls.Add(this.dataGridViewReporte);
            this.Controls.Add(this.textTotal);
            this.Controls.Add(this.btnGenerar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxReporte);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "frmReportes";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VIRTUAL GAMES-[REPORTES]";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxReporte;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnGenerar;
        private System.Windows.Forms.TextBox textTotal;
        public System.Windows.Forms.DataGridView dataGridViewReporte;
    }
}