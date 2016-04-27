using System;
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

        private void smiLoadFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel 2007|*.xlsx";
            var dr = ofd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                var frm = new SelectClassForm();
                frm.OnOK += (x) => { PacketImporter.ImportMarksFromExcel(ofd.FileName, x); };
                frm.Show();
            }
        }

        private void smiDiplomaList_Click(object sender, EventArgs e)
        {
            new PrintDiplomaList().Show();
        }

        private void smiSetDiploma_Click(object sender, EventArgs e)
        {
            new SetDiplomaList().Show();
        }

        private void smiDiplomaRegBook_Click(object sender, EventArgs e)
        {
            DiplomaRegBookPrintClass.PrintRegBook();
        }
    }
}
