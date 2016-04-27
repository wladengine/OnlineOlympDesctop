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
    public partial class FormSetDiplomaSchoolName : Form
    {
        public event Action OnOK;
        private Guid PersonId;

        public FormSetDiplomaSchoolName(Guid gPersonId)
        {
            InitializeComponent();

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                tbValue.Text = context.Person.Where(x => x.Id == gPersonId)
                    .Select(x => x.SchoolName).DefaultIfEmpty("").First();
            }

            PersonId = gPersonId;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var val = context.Person.Where(x => x.Id == PersonId).FirstOrDefault();

                if (val != null)
                    val.SchoolName = tbValue.Text.Trim();

                context.SaveChanges();
            }

            if (OnOK != null)
            {
                OnOK();
                this.Close();
            }
        }
    }
}
