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
    public partial class FormSetDiplomaRegNum : Form
    {
        public event Action OnOK;
        private Guid OlympDiplomaId;

        public FormSetDiplomaRegNum(Guid gOlympDiplomaId)
        {
            InitializeComponent();

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                tbValue.Text = context.OlympDiploma.Where(x => x.Id == gOlympDiplomaId)
                    .Select(x => x.DiplomaRegNum).DefaultIfEmpty("").First();
            }

            OlympDiplomaId = gOlympDiplomaId;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var val = context.OlympDiploma.Where(x => x.Id == OlympDiplomaId).FirstOrDefault();

                if (val != null)
                    val.DiplomaRegNum = tbValue.Text.Trim();

                context.SaveChanges();
            }

            if (OnOK != null)
            {
                OnOK();
                this.Close();
            }
        }

        private void tbValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }
    }
}
