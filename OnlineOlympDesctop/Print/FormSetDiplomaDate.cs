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
    public partial class FormSetDiplomaDate : Form
    {
        public event Action OnOK;
        private Guid OlympDiplomaId;
        public FormSetDiplomaDate(Guid gOlympDiplomaId)
        {
            InitializeComponent();

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                dtp.Value = context.OlympDiploma.Where(x => x.Id == gOlympDiplomaId)
                    .Select(x => x.DiplomaDate ?? DateTime.Now).DefaultIfEmpty(DateTime.Now).First();
            }

            OlympDiplomaId = gOlympDiplomaId;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var val = context.OlympDiploma.Where(x => x.Id == OlympDiplomaId).FirstOrDefault();

                if (val != null)
                    val.DiplomaDate = dtp.Value.Date;

                context.SaveChanges();
            }

            if (OnOK != null)
            {
                OnOK();
                this.Close();
            }
        }

        private void dtp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }
    }
}
