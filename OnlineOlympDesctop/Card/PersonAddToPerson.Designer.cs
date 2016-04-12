namespace OnlineOlympDesctop
{
    partial class PersonAddToPerson
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
            this.lblFIO = new System.Windows.Forms.Label();
            this.lblCountryRegion = new System.Windows.Forms.Label();
            this.l = new System.Windows.Forms.Label();
            this.cbParticipant = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFIO
            // 
            this.lblFIO.AutoSize = true;
            this.lblFIO.Location = new System.Drawing.Point(120, 20);
            this.lblFIO.Name = "lblFIO";
            this.lblFIO.Size = new System.Drawing.Size(35, 13);
            this.lblFIO.TabIndex = 0;
            this.lblFIO.Text = "label1";
            // 
            // lblCountryRegion
            // 
            this.lblCountryRegion.AutoSize = true;
            this.lblCountryRegion.Location = new System.Drawing.Point(96, 45);
            this.lblCountryRegion.Name = "lblCountryRegion";
            this.lblCountryRegion.Size = new System.Drawing.Size(35, 13);
            this.lblCountryRegion.TabIndex = 1;
            this.lblCountryRegion.Text = "label2";
            // 
            // l
            // 
            this.l.AutoSize = true;
            this.l.Location = new System.Drawing.Point(12, 72);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(102, 13);
            this.l.TabIndex = 2;
            this.l.Text = "Сопровождающий:";
            // 
            // cbParticipant
            // 
            this.cbParticipant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParticipant.FormattingEnabled = true;
            this.cbParticipant.Location = new System.Drawing.Point(15, 88);
            this.cbParticipant.Name = "cbParticipant";
            this.cbParticipant.Size = new System.Drawing.Size(563, 21);
            this.cbParticipant.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(191, 115);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(226, 26);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Привязать";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Сопровождающий:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Гражданство:";
            // 
            // ParticipantAddToPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 166);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbParticipant);
            this.Controls.Add(this.l);
            this.Controls.Add(this.lblCountryRegion);
            this.Controls.Add(this.lblFIO);
            this.MaximumSize = new System.Drawing.Size(606, 204);
            this.MinimumSize = new System.Drawing.Size(606, 204);
            this.Name = "ParticipantAddToPerson";
            this.Text = "Добавить сопровождающего";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFIO;
        private System.Windows.Forms.Label lblCountryRegion;
        private System.Windows.Forms.Label l;
        private System.Windows.Forms.ComboBox cbParticipant;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}