namespace SGC_Basic_____project_
{
    partial class F_Selecionar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F_Selecionar));
            this.dgv_selecionar = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_selecionar)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_selecionar
            // 
            this.dgv_selecionar.AllowUserToAddRows = false;
            this.dgv_selecionar.AllowUserToDeleteRows = false;
            this.dgv_selecionar.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_selecionar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_selecionar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_selecionar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgv_selecionar.EnableHeadersVisualStyles = false;
            this.dgv_selecionar.Location = new System.Drawing.Point(12, 12);
            this.dgv_selecionar.MultiSelect = false;
            this.dgv_selecionar.Name = "dgv_selecionar";
            this.dgv_selecionar.ReadOnly = true;
            this.dgv_selecionar.RowHeadersVisible = false;
            this.dgv_selecionar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_selecionar.Size = new System.Drawing.Size(666, 376);
            this.dgv_selecionar.TabIndex = 9;
            this.dgv_selecionar.DoubleClick += new System.EventHandler(this.dgv_selecionar_DoubleClick);
            // 
            // F_Selecionar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 400);
            this.Controls.Add(this.dgv_selecionar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "F_Selecionar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecionar";
            this.Load += new System.EventHandler(this.F_Selecionar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_selecionar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_selecionar;
    }
}