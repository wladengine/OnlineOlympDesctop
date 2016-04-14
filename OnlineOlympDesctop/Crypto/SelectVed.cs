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
    public partial class SelectVed : Form
    {
        protected Guid? PersInVedId;
        protected bool isLocked;

        public SelectVed()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            tbCryptNum.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (PersInVedId == null)
                return;

            if (!isLocked)
            {
                MessageBox.Show("Данная ведомость не закрыта!", "Ошибка");
                return;
            }

            new EnterMarks(PersInVedId).Show();
            this.Close();
        }

        private void tbVedNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                if (PersInVedId == null)
                    return;

                if (!isLocked)
                {
                    MessageBox.Show("Данная ведомость не закрыта!", "Ошибка");
                    return;
                }

                new EnterMarks(PersInVedId).Show();
                this.Close();
            }

            if (e.KeyValue == 189)
            {
                if (!tbCryptNum.Text.Contains("=="))
                    return;

                string CryptNum = tbCryptNum.Text.Trim();
                //считываем 2 символа класса
                string ClassNum = CryptNum.Substring(0, 2);
                //считываем 5 символов кода человека
                CryptNum = CryptNum.Substring(CryptNum.IndexOf("==") + 2, 5);

                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    var olVed =
                        (from PersInVed in context.PersonInOlympVed
                         where PersInVed.CryptNumber == CryptNum
                         select new
                         {
                             SchoolClass = PersInVed.OlympVed.SchoolClass.Name,
                             SchoolClassNum = PersInVed.OlympVed.SchoolClass.IntVal,
                             PersInVed.OlympVed.OlympYear,
                             PersInVed.OlympVed.IsLocked,
                             PersInVed.Id,
                             PersInVed.CryptNumber
                         }).FirstOrDefault();

                    int iClassNum = 0;
                    int.TryParse(ClassNum, out iClassNum);

                    if (iClassNum != olVed.SchoolClassNum)
                    {
                        tbVedName.Text = "!НЕВЕРНО УКАЗАН КЛАСС";
                        btnOK.Enabled = false;
                    }
                    else
                    {
                        PersInVedId = olVed.Id;
                        isLocked = olVed.IsLocked;

                        string g = "";
                        g = olVed.SchoolClass + "(" + olVed.OlympYear + " год)" + "\r\n" +
                              "Шифрокод: " + olVed.CryptNumber + "\r\n";

                        tbVedName.Text = g;

                        btnOK.Enabled = true;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
