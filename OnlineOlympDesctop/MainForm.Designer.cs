namespace OnlineOlympDesctop
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.спискиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPeronList = new System.Windows.Forms.ToolStripMenuItem();
            this.smiParticipant = new System.Windows.Forms.ToolStripMenuItem();
            this.smiVed = new System.Windows.Forms.ToolStripMenuItem();
            this.smiOlympVedList = new System.Windows.Forms.ToolStripMenuItem();
            this.smiCrypto = new System.Windows.Forms.ToolStripMenuItem();
            this.smiSelectVed = new System.Windows.Forms.ToolStripMenuItem();
            this.smiVedAppeal = new System.Windows.Forms.ToolStripMenuItem();
            this.печатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smiPrintList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.спискиToolStripMenuItem,
            this.smiVed,
            this.smiCrypto,
            this.печатьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(592, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // спискиToolStripMenuItem
            // 
            this.спискиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiPeronList,
            this.smiParticipant});
            this.спискиToolStripMenuItem.Name = "спискиToolStripMenuItem";
            this.спискиToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.спискиToolStripMenuItem.Text = "Списки";
            // 
            // smiPeronList
            // 
            this.smiPeronList.Name = "smiPeronList";
            this.smiPeronList.Size = new System.Drawing.Size(219, 22);
            this.smiPeronList.Text = "Список сопровождающих";
            this.smiPeronList.Click += new System.EventHandler(this.smiPeronList_Click);
            // 
            // smiParticipant
            // 
            this.smiParticipant.Name = "smiParticipant";
            this.smiParticipant.Size = new System.Drawing.Size(219, 22);
            this.smiParticipant.Text = "Список участников";
            this.smiParticipant.Click += new System.EventHandler(this.smiParticipant_Click);
            // 
            // smiVed
            // 
            this.smiVed.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiOlympVedList});
            this.smiVed.Name = "smiVed";
            this.smiVed.Size = new System.Drawing.Size(79, 20);
            this.smiVed.Text = "Ведомости";
            this.smiVed.Visible = false;
            // 
            // smiOlympVedList
            // 
            this.smiOlympVedList.Name = "smiOlympVedList";
            this.smiOlympVedList.Size = new System.Drawing.Size(183, 22);
            this.smiOlympVedList.Text = "Список ведомостей";
            this.smiOlympVedList.Click += new System.EventHandler(this.smiOlympVedList_Click);
            // 
            // smiCrypto
            // 
            this.smiCrypto.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiSelectVed,
            this.smiVedAppeal});
            this.smiCrypto.Name = "smiCrypto";
            this.smiCrypto.Size = new System.Drawing.Size(92, 20);
            this.smiCrypto.Text = "Шифрование";
            this.smiCrypto.Visible = false;
            // 
            // smiSelectVed
            // 
            this.smiSelectVed.Name = "smiSelectVed";
            this.smiSelectVed.Size = new System.Drawing.Size(218, 22);
            this.smiSelectVed.Text = "Ввод оценок";
            this.smiSelectVed.Click += new System.EventHandler(this.smiSelectVed_Click);
            // 
            // smiVedAppeal
            // 
            this.smiVedAppeal.Name = "smiVedAppeal";
            this.smiVedAppeal.Size = new System.Drawing.Size(218, 22);
            this.smiVedAppeal.Text = "Ведомость для апелляции";
            this.smiVedAppeal.Click += new System.EventHandler(this.smiVedAppeal_Click);
            // 
            // печатьToolStripMenuItem
            // 
            this.печатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiPrintList});
            this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            this.печатьToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.печатьToolStripMenuItem.Text = "Печать";
            // 
            // smiPrintList
            // 
            this.smiPrintList.Name = "smiPrintList";
            this.smiPrintList.Size = new System.Drawing.Size(254, 22);
            this.smiPrintList.Text = "Основные шаблоны участников";
            this.smiPrintList.Click += new System.EventHandler(this.smiPrintList_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 399);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Регистрация на всероссийскую олимпиаду по математике 2016";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem спискиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiPeronList;
        private System.Windows.Forms.ToolStripMenuItem smiParticipant;
        private System.Windows.Forms.ToolStripMenuItem smiVed;
        private System.Windows.Forms.ToolStripMenuItem smiOlympVedList;
        private System.Windows.Forms.ToolStripMenuItem smiCrypto;
        private System.Windows.Forms.ToolStripMenuItem smiSelectVed;
        private System.Windows.Forms.ToolStripMenuItem smiVedAppeal;
        private System.Windows.Forms.ToolStripMenuItem печатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smiPrintList;
    }
}

