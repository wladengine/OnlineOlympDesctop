namespace OnlineOlympDesctop
{
    partial class OlympVedList
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
            this.cbExamVed = new System.Windows.Forms.ComboBox();
            this.btnUnload = new System.Windows.Forms.Button();
            this.btnDeleteFromVed = new System.Windows.Forms.Button();
            this.lblCountCell = new System.Windows.Forms.Label();
            this.tbCountCell = new System.Windows.Forms.TextBox();
            this.btnPrintSticker = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lblLocked = new System.Windows.Forms.Label();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.lblProtocolNum = new System.Windows.Forms.Label();
            this.btnPrintProtocol = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // cbExamVed
            // 
            this.cbExamVed.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbExamVed.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbExamVed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExamVed.FormattingEnabled = true;
            this.cbExamVed.Location = new System.Drawing.Point(12, 25);
            this.cbExamVed.Name = "cbExamVed";
            this.cbExamVed.Size = new System.Drawing.Size(421, 21);
            this.cbExamVed.TabIndex = 166;
            // 
            // btnUnload
            // 
            this.btnUnload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnload.Location = new System.Drawing.Point(315, 445);
            this.btnUnload.Name = "btnUnload";
            this.btnUnload.Size = new System.Drawing.Size(83, 42);
            this.btnUnload.TabIndex = 165;
            this.btnUnload.Text = "Разлочить ведомость";
            this.btnUnload.UseVisualStyleBackColor = true;
            this.btnUnload.Click += new System.EventHandler(this.btnUnload_Click);
            // 
            // btnDeleteFromVed
            // 
            this.btnDeleteFromVed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteFromVed.Location = new System.Drawing.Point(105, 445);
            this.btnDeleteFromVed.Name = "btnDeleteFromVed";
            this.btnDeleteFromVed.Size = new System.Drawing.Size(91, 42);
            this.btnDeleteFromVed.TabIndex = 164;
            this.btnDeleteFromVed.Text = "Удалить из ведомости";
            this.btnDeleteFromVed.UseVisualStyleBackColor = true;
            this.btnDeleteFromVed.Visible = false;
            this.btnDeleteFromVed.Click += new System.EventHandler(this.btnDeleteFromVed_Click);
            // 
            // lblCountCell
            // 
            this.lblCountCell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountCell.Location = new System.Drawing.Point(619, 438);
            this.lblCountCell.Name = "lblCountCell";
            this.lblCountCell.Size = new System.Drawing.Size(114, 25);
            this.lblCountCell.TabIndex = 163;
            this.lblCountCell.Text = "Кол-во\r\nш/кодов на задание";
            // 
            // tbCountCell
            // 
            this.tbCountCell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCountCell.Location = new System.Drawing.Point(643, 466);
            this.tbCountCell.Name = "tbCountCell";
            this.tbCountCell.Size = new System.Drawing.Size(59, 20);
            this.tbCountCell.TabIndex = 161;
            // 
            // btnPrintSticker
            // 
            this.btnPrintSticker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintSticker.Location = new System.Drawing.Point(532, 445);
            this.btnPrintSticker.Name = "btnPrintSticker";
            this.btnPrintSticker.Size = new System.Drawing.Size(81, 42);
            this.btnPrintSticker.TabIndex = 160;
            this.btnPrintSticker.Text = "Печать наклеек";
            this.btnPrintSticker.UseVisualStyleBackColor = true;
            this.btnPrintSticker.Click += new System.EventHandler(this.btnPrintSticker_Click);
            // 
            // lblLocked
            // 
            this.lblLocked.AutoSize = true;
            this.lblLocked.ForeColor = System.Drawing.Color.Red;
            this.lblLocked.Location = new System.Drawing.Point(439, 28);
            this.lblLocked.Name = "lblLocked";
            this.lblLocked.Size = new System.Drawing.Size(51, 13);
            this.lblLocked.TabIndex = 157;
            this.lblLocked.Text = "Закрыта";
            this.lblLocked.Visible = false;
            // 
            // btnLock
            // 
            this.btnLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLock.Location = new System.Drawing.Point(222, 445);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(87, 42);
            this.btnLock.TabIndex = 155;
            this.btnLock.Text = "Закрыть ведомость";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(430, 445);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(96, 42);
            this.btnPrint.TabIndex = 148;
            this.btnPrint.Text = "Печать ведомости";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(929, 23);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(165, 23);
            this.btnCreate.TabIndex = 154;
            this.btnCreate.Text = "Новая ведомость";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Visible = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnChange
            // 
            this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChange.Enabled = false;
            this.btnChange.Location = new System.Drawing.Point(12, 445);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(87, 41);
            this.btnChange.TabIndex = 153;
            this.btnChange.Text = "Изменить";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Visible = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1019, 464);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 152;
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
            this.dgvList.Location = new System.Drawing.Point(12, 52);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1082, 383);
            this.dgvList.TabIndex = 151;
            // 
            // lblProtocolNum
            // 
            this.lblProtocolNum.AutoSize = true;
            this.lblProtocolNum.Location = new System.Drawing.Point(12, 9);
            this.lblProtocolNum.Name = "lblProtocolNum";
            this.lblProtocolNum.Size = new System.Drawing.Size(38, 13);
            this.lblProtocolNum.TabIndex = 150;
            this.lblProtocolNum.Text = "Класс";
            // 
            // btnPrintProtocol
            // 
            this.btnPrintProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintProtocol.Location = new System.Drawing.Point(917, 445);
            this.btnPrintProtocol.Name = "btnPrintProtocol";
            this.btnPrintProtocol.Size = new System.Drawing.Size(96, 42);
            this.btnPrintProtocol.TabIndex = 167;
            this.btnPrintProtocol.Text = "Печать протокола";
            this.btnPrintProtocol.UseVisualStyleBackColor = true;
            this.btnPrintProtocol.Click += new System.EventHandler(this.btnPrintProtocol_Click);
            // 
            // OlympVedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 498);
            this.Controls.Add(this.btnPrintProtocol);
            this.Controls.Add(this.cbExamVed);
            this.Controls.Add(this.btnUnload);
            this.Controls.Add(this.btnDeleteFromVed);
            this.Controls.Add(this.lblCountCell);
            this.Controls.Add(this.tbCountCell);
            this.Controls.Add(this.btnPrintSticker);
            this.Controls.Add(this.lblLocked);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.lblProtocolNum);
            this.Name = "OlympVedList";
            this.Text = "Список ведомостей";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbExamVed;
        protected System.Windows.Forms.Button btnUnload;
        protected System.Windows.Forms.Button btnDeleteFromVed;
        private System.Windows.Forms.Label lblCountCell;
        private System.Windows.Forms.TextBox tbCountCell;
        protected System.Windows.Forms.Button btnPrintSticker;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lblLocked;
        protected System.Windows.Forms.Button btnLock;
        protected System.Windows.Forms.Button btnPrint;
        protected System.Windows.Forms.Button btnCreate;
        protected System.Windows.Forms.Button btnChange;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.DataGridView dgvList;
        protected System.Windows.Forms.Label lblProtocolNum;
        protected System.Windows.Forms.Button btnPrintProtocol;
    }
}