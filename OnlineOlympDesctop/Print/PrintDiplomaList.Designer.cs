namespace OnlineOlympDesctop
{
    partial class PrintDiplomaList
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetDiplomaDate = new System.Windows.Forms.Button();
            this.btnSetRegNum = new System.Windows.Forms.Button();
            this.btnSetSchoolName = new System.Windows.Forms.Button();
            this.btnSetDiplomaBlankNumber = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDiplomaLevel = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(12, 54);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.Size = new System.Drawing.Size(817, 359);
            this.dgv.TabIndex = 0;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(754, 432);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbClass
            // 
            this.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Location = new System.Drawing.Point(12, 27);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(121, 21);
            this.cbClass.TabIndex = 2;
            this.cbClass.SelectedIndexChanged += new System.EventHandler(this.cbClass_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Класс";
            // 
            // btnSetDiplomaDate
            // 
            this.btnSetDiplomaDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetDiplomaDate.Location = new System.Drawing.Point(12, 419);
            this.btnSetDiplomaDate.Name = "btnSetDiplomaDate";
            this.btnSetDiplomaDate.Size = new System.Drawing.Size(85, 36);
            this.btnSetDiplomaDate.TabIndex = 4;
            this.btnSetDiplomaDate.Text = "Назначить дату выдачи";
            this.btnSetDiplomaDate.UseVisualStyleBackColor = true;
            this.btnSetDiplomaDate.Click += new System.EventHandler(this.btnSetDiplomaDate_Click);
            // 
            // btnSetRegNum
            // 
            this.btnSetRegNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetRegNum.Location = new System.Drawing.Point(103, 419);
            this.btnSetRegNum.Name = "btnSetRegNum";
            this.btnSetRegNum.Size = new System.Drawing.Size(85, 36);
            this.btnSetRegNum.TabIndex = 5;
            this.btnSetRegNum.Text = "Назначить рег. номер";
            this.btnSetRegNum.UseVisualStyleBackColor = true;
            this.btnSetRegNum.Click += new System.EventHandler(this.btnSetRegNum_Click);
            // 
            // btnSetSchoolName
            // 
            this.btnSetSchoolName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetSchoolName.Location = new System.Drawing.Point(194, 419);
            this.btnSetSchoolName.Name = "btnSetSchoolName";
            this.btnSetSchoolName.Size = new System.Drawing.Size(93, 36);
            this.btnSetSchoolName.TabIndex = 6;
            this.btnSetSchoolName.Text = "Редактировать название ОУ";
            this.btnSetSchoolName.UseVisualStyleBackColor = true;
            this.btnSetSchoolName.Click += new System.EventHandler(this.btnSetSchoolName_Click);
            // 
            // btnSetDiplomaBlankNumber
            // 
            this.btnSetDiplomaBlankNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetDiplomaBlankNumber.Location = new System.Drawing.Point(293, 419);
            this.btnSetDiplomaBlankNumber.Name = "btnSetDiplomaBlankNumber";
            this.btnSetDiplomaBlankNumber.Size = new System.Drawing.Size(98, 36);
            this.btnSetDiplomaBlankNumber.TabIndex = 7;
            this.btnSetDiplomaBlankNumber.Text = "Ввести номер бланка диплома";
            this.btnSetDiplomaBlankNumber.UseVisualStyleBackColor = true;
            this.btnSetDiplomaBlankNumber.Click += new System.EventHandler(this.btnSetDiplomaBlankNumber_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Уровень";
            // 
            // cbDiplomaLevel
            // 
            this.cbDiplomaLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiplomaLevel.FormattingEnabled = true;
            this.cbDiplomaLevel.Location = new System.Drawing.Point(139, 27);
            this.cbDiplomaLevel.Name = "cbDiplomaLevel";
            this.cbDiplomaLevel.Size = new System.Drawing.Size(225, 21);
            this.cbDiplomaLevel.TabIndex = 8;
            this.cbDiplomaLevel.SelectedIndexChanged += new System.EventHandler(this.cbDiplomaLevel_SelectedIndexChanged);
            // 
            // PrintDiplomaList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 467);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbDiplomaLevel);
            this.Controls.Add(this.btnSetDiplomaBlankNumber);
            this.Controls.Add(this.btnSetSchoolName);
            this.Controls.Add(this.btnSetRegNum);
            this.Controls.Add(this.btnSetDiplomaDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbClass);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgv);
            this.Name = "PrintDiplomaList";
            this.Text = "Печать дипломов";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetDiplomaDate;
        private System.Windows.Forms.Button btnSetRegNum;
        private System.Windows.Forms.Button btnSetSchoolName;
        private System.Windows.Forms.Button btnSetDiplomaBlankNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDiplomaLevel;
    }
}