﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineOlympDesctop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Util.MainForm = this;
            SetVisibleMenus();
        }

        private void SetVisibleMenus()
        {
            if (Util.IsOwner() || Util.IsPasha())
            {
                //меню "Ведомости"
                smiVed.Visible = true;
                smiOlympVedList.Visible = true;
                //Меню шифровалки
                smiCrypto.Visible = true;
                smiSelectVed.Visible = true;
                smiVedAppeal.Visible = true;
            }

            if (Util.IsCryptoMain() || Util.IsCrypto())
            {
                //Меню шифровалки
                smiCrypto.Visible = true;
                smiSelectVed.Visible = true;
                smiVedAppeal.Visible = true;
            }
        }

        private void smiPeronList_Click(object sender, EventArgs e)
        {
            new PersonList().Show();
        }

        private void smiParticipant_Click(object sender, EventArgs e)
        {
            new ParticipantList().Show();
        }

        private void smiOlympVedList_Click(object sender, EventArgs e)
        {
            new OlympVedList().Show();
        }

        private void smiSelectVed_Click(object sender, EventArgs e)
        {
            new SelectVed().Show();
        }

        private void smiVedAppeal_Click(object sender, EventArgs e)
        {
            new ListSelectPersonForAppeal().Show();
        }

        private void smiPrintList_Click(object sender, EventArgs e)
        {
            new ParticipantPrint().Show();
        }
    }
}
