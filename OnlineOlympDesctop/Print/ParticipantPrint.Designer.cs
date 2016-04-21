namespace OnlineOlympDesctop
{
    partial class ParticipantPrint
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPrintListNewName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrintListAdd = new System.Windows.Forms.Button();
            this.lbPrintList = new System.Windows.Forms.ListBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPrintListCoumnsSave = new System.Windows.Forms.Button();
            this.lblColumns = new System.Windows.Forms.Label();
            this.lbColumns = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbOrder = new System.Windows.Forms.ListBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbClass = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbFilename = new System.Windows.Forms.TextBox();
            this.cbOpenFile = new System.Windows.Forms.CheckBox();
            this.chbExcludeHided = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbPrintListNewName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPrintListAdd);
            this.groupBox1.Controls.Add(this.lbPrintList);
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 385);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите файл для вывода";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Сохраненные шаблоны:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 309);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Название шаблона:";
            // 
            // tbPrintListNewName
            // 
            this.tbPrintListNewName.Location = new System.Drawing.Point(11, 325);
            this.tbPrintListNewName.Name = "tbPrintListNewName";
            this.tbPrintListNewName.Size = new System.Drawing.Size(193, 20);
            this.tbPrintListNewName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "или создайте новый шаблон\r\nдля вывода файла:";
            // 
            // btnPrintListAdd
            // 
            this.btnPrintListAdd.Location = new System.Drawing.Point(122, 356);
            this.btnPrintListAdd.Name = "btnPrintListAdd";
            this.btnPrintListAdd.Size = new System.Drawing.Size(82, 24);
            this.btnPrintListAdd.TabIndex = 1;
            this.btnPrintListAdd.Text = "Добавить";
            this.btnPrintListAdd.UseVisualStyleBackColor = true;
            this.btnPrintListAdd.Click += new System.EventHandler(this.btnPrintListAdd_Click);
            // 
            // lbPrintList
            // 
            this.lbPrintList.FormattingEnabled = true;
            this.lbPrintList.Location = new System.Drawing.Point(6, 42);
            this.lbPrintList.Name = "lbPrintList";
            this.lbPrintList.Size = new System.Drawing.Size(193, 199);
            this.lbPrintList.TabIndex = 0;
            this.lbPrintList.SelectedIndexChanged += new System.EventHandler(this.lbPrintList_SelectedIndexChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(57, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(140, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Шаг 1. Выбор шаблона";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(288, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(274, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Шаг 2. Настройка столбцов и порядок их вывода";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPrintListCoumnsSave);
            this.groupBox2.Controls.Add(this.lblColumns);
            this.groupBox2.Controls.Add(this.lbColumns);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lbOrder);
            this.groupBox2.Location = new System.Drawing.Point(252, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(364, 385);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Порядок вывода столбцов шаблона";
            // 
            // btnPrintListCoumnsSave
            // 
            this.btnPrintListCoumnsSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintListCoumnsSave.Location = new System.Drawing.Point(6, 356);
            this.btnPrintListCoumnsSave.Name = "btnPrintListCoumnsSave";
            this.btnPrintListCoumnsSave.Size = new System.Drawing.Size(104, 23);
            this.btnPrintListCoumnsSave.TabIndex = 10;
            this.btnPrintListCoumnsSave.Text = "Сохранить";
            this.btnPrintListCoumnsSave.UseVisualStyleBackColor = true;
            this.btnPrintListCoumnsSave.Click += new System.EventHandler(this.btnPrintListCoumnsSave_Click);
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(243, 85);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(41, 13);
            this.lblColumns.TabIndex = 15;
            this.lblColumns.Text = "label13";
            // 
            // lbColumns
            // 
            this.lbColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbColumns.FormattingEnabled = true;
            this.lbColumns.Location = new System.Drawing.Point(6, 42);
            this.lbColumns.Name = "lbColumns";
            this.lbColumns.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbColumns.Size = new System.Drawing.Size(177, 303);
            this.lbColumns.TabIndex = 11;
            this.lbColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbColumns_MouseDown);
            this.lbColumns.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbColumns_MouseMove);
            this.lbColumns.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbColumns_MouseUp);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(194, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Порядок сортировки:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Выводимые столбцы:";
            // 
            // lbOrder
            // 
            this.lbOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOrder.FormattingEnabled = true;
            this.lbOrder.Location = new System.Drawing.Point(189, 42);
            this.lbOrder.Name = "lbOrder";
            this.lbOrder.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbOrder.Size = new System.Drawing.Size(169, 303);
            this.lbOrder.TabIndex = 13;
            this.lbOrder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbOrder_MouseDown);
            this.lbOrder.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbOrder_MouseMove);
            this.lbOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lbOrder_MouseUp);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(685, 12);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(145, 17);
            this.radioButton3.TabIndex = 1;
            this.radioButton3.Text = "Шаг 3. Вывод на печать";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chbExcludeHided);
            this.groupBox3.Controls.Add(this.cbOpenFile);
            this.groupBox3.Controls.Add(this.tbFilename);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbClass);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cbFormat);
            this.groupBox3.Controls.Add(this.btnPrint);
            this.groupBox3.Location = new System.Drawing.Point(633, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 385);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Печать шаблона";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(180, 356);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cbFormat
            // 
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Location = new System.Drawing.Point(104, 55);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(151, 21);
            this.cbFormat.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Формат файла:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(60, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Настройки не сохраняются";
            // 
            // cbClass
            // 
            this.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClass.FormattingEnabled = true;
            this.cbClass.Location = new System.Drawing.Point(104, 97);
            this.cbClass.Name = "cbClass";
            this.cbClass.Size = new System.Drawing.Size(151, 21);
            this.cbClass.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Класс:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 279);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Название файла";
            // 
            // tbFilename
            // 
            this.tbFilename.Location = new System.Drawing.Point(106, 276);
            this.tbFilename.Name = "tbFilename";
            this.tbFilename.Size = new System.Drawing.Size(149, 20);
            this.tbFilename.TabIndex = 7;
            // 
            // cbOpenFile
            // 
            this.cbOpenFile.AutoSize = true;
            this.cbOpenFile.Checked = true;
            this.cbOpenFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenFile.Location = new System.Drawing.Point(11, 305);
            this.cbOpenFile.Name = "cbOpenFile";
            this.cbOpenFile.Size = new System.Drawing.Size(145, 17);
            this.cbOpenFile.TabIndex = 8;
            this.cbOpenFile.Text = "Открыть по готовности";
            this.cbOpenFile.UseVisualStyleBackColor = true;
            // 
            // chbExcludeHided
            // 
            this.chbExcludeHided.AutoSize = true;
            this.chbExcludeHided.Checked = true;
            this.chbExcludeHided.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbExcludeHided.Location = new System.Drawing.Point(104, 137);
            this.chbExcludeHided.Name = "chbExcludeHided";
            this.chbExcludeHided.Size = new System.Drawing.Size(129, 17);
            this.chbExcludeHided.TabIndex = 8;
            this.chbExcludeHided.Text = "Исключить скрытых";
            this.chbExcludeHided.UseVisualStyleBackColor = true;
            // 
            // ParticipantPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 440);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.groupBox1);
            this.MaximumSize = new System.Drawing.Size(922, 478);
            this.MinimumSize = new System.Drawing.Size(922, 478);
            this.Name = "ParticipantPrint";
            this.Text = "Печать списков";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPrintListNewName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrintListAdd;
        private System.Windows.Forms.ListBox lbPrintList;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnPrintListCoumnsSave;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.ListBox lbColumns;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox lbOrder;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbFormat;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbClass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbOpenFile;
        private System.Windows.Forms.TextBox tbFilename;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chbExcludeHided;

    }
}