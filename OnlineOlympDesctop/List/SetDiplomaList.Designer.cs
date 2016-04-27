namespace OnlineOlympDesctop
{
    partial class SetDiplomaList
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
            this.btnPrintProtocol = new System.Windows.Forms.Button();
            this.cbSchoolClass = new System.Windows.Forms.ComboBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.lblProtocolNum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPrintProtocol
            // 
            this.btnPrintProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintProtocol.Location = new System.Drawing.Point(917, 446);
            this.btnPrintProtocol.Name = "btnPrintProtocol";
            this.btnPrintProtocol.Size = new System.Drawing.Size(96, 42);
            this.btnPrintProtocol.TabIndex = 174;
            this.btnPrintProtocol.Text = "Печать дипломов";
            this.btnPrintProtocol.UseVisualStyleBackColor = true;
            this.btnPrintProtocol.Click += new System.EventHandler(this.btnPrintProtocol_Click);
            // 
            // cbSchoolClass
            // 
            this.cbSchoolClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbSchoolClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSchoolClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSchoolClass.FormattingEnabled = true;
            this.cbSchoolClass.Location = new System.Drawing.Point(12, 26);
            this.cbSchoolClass.Name = "cbSchoolClass";
            this.cbSchoolClass.Size = new System.Drawing.Size(421, 21);
            this.cbSchoolClass.TabIndex = 173;
            this.cbSchoolClass.SelectedIndexChanged += new System.EventHandler(this.cbSchoolClass_SelectedIndexChanged);
            // 
            // btnChange
            // 
            this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChange.Enabled = false;
            this.btnChange.Location = new System.Drawing.Point(12, 445);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(87, 41);
            this.btnChange.TabIndex = 171;
            this.btnChange.Text = "Добавить / изменить";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Visible = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1019, 465);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 170;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AllowUserToResizeRows = false;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(12, 53);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1082, 383);
            this.dgvList.TabIndex = 169;
            // 
            // lblProtocolNum
            // 
            this.lblProtocolNum.AutoSize = true;
            this.lblProtocolNum.Location = new System.Drawing.Point(12, 10);
            this.lblProtocolNum.Name = "lblProtocolNum";
            this.lblProtocolNum.Size = new System.Drawing.Size(38, 13);
            this.lblProtocolNum.TabIndex = 168;
            this.lblProtocolNum.Text = "Класс";
            // 
            // SetDiplomaList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 498);
            this.Controls.Add(this.btnPrintProtocol);
            this.Controls.Add(this.cbSchoolClass);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.lblProtocolNum);
            this.Name = "SetDiplomaList";
            this.Text = "Введение победителей и призёров олимпиад";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnPrintProtocol;
        private System.Windows.Forms.ComboBox cbSchoolClass;
        protected System.Windows.Forms.Button btnChange;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.DataGridView dgvList;
        protected System.Windows.Forms.Label lblProtocolNum;
    }
}