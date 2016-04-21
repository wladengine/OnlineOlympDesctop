namespace OnlineOlympDesctop
{
    partial class Settings
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
            this.lblColumns = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbOrder = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lbColumns = new System.Windows.Forms.ListBox();
            this.btnAbitTypeListReasonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(319, 66);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(41, 13);
            this.lblColumns.TabIndex = 9;
            this.lblColumns.Text = "label13";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(279, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Порядок сортировки:";
            // 
            // lbOrder
            // 
            this.lbOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOrder.FormattingEnabled = true;
            this.lbOrder.Location = new System.Drawing.Point(282, 23);
            this.lbOrder.Name = "lbOrder";
            this.lbOrder.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbOrder.Size = new System.Drawing.Size(188, 121);
            this.lbOrder.TabIndex = 7;
            this.lbOrder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbOrder_MouseDown);
            this.lbOrder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbOrder_MouseMove);
            this.lbOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbOrder_MouseUp);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(79, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Выводимые столбцы:";
            // 
            // lbColumns
            // 
            this.lbColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbColumns.FormattingEnabled = true;
            this.lbColumns.Location = new System.Drawing.Point(82, 23);
            this.lbColumns.Name = "lbColumns";
            this.lbColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbColumns.Size = new System.Drawing.Size(188, 121);
            this.lbColumns.TabIndex = 3;
            this.lbColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbColumns_MouseDown);
            this.lbColumns.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbColumns_MouseMove);
            this.lbColumns.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbColumns_MouseUp);
            // 
            // btnAbitTypeListReasonSave
            // 
            this.btnAbitTypeListReasonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbitTypeListReasonSave.Location = new System.Drawing.Point(82, 159);
            this.btnAbitTypeListReasonSave.Name = "btnAbitTypeListReasonSave";
            this.btnAbitTypeListReasonSave.Size = new System.Drawing.Size(104, 23);
            this.btnAbitTypeListReasonSave.TabIndex = 2;
            this.btnAbitTypeListReasonSave.Text = "Сохранить";
            this.btnAbitTypeListReasonSave.UseVisualStyleBackColor = true;
            this.btnAbitTypeListReasonSave.Click += new System.EventHandler(this.btnAbitTypeListReasonSave_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 509);
            this.Controls.Add(this.btnAbitTypeListReasonSave);
            this.Controls.Add(this.lblColumns);
            this.Controls.Add(this.lbColumns);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lbOrder);
            this.MinimumSize = new System.Drawing.Size(773, 442);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox lbColumns;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListBox lbOrder;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.Button btnAbitTypeListReasonSave;
    }
}