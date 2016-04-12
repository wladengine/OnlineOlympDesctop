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
        }

        private void smiPeronList_Click(object sender, EventArgs e)
        {
            new PersonList().Show();
        }

        private void smiParticipant_Click(object sender, EventArgs e)
        {
            new ParticipantList().Show();
        }
    }
}