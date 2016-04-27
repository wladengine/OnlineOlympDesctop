using EducServLib;
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
    public partial class SelectClassForm : Form
    {
        public event Action<int> OnOK;
        public event Func<int, bool> OnCheck;
        public SelectClassForm()
        {
            InitializeComponent();
            this.MdiParent = Util.MainForm;
            FillCombo();
        }

        private void FillCombo()
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var src = context.SchoolClass.Select(x => new { x.Id, x.Name })
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name))
                    .ToList();

                ComboServ.FillCombo(cbClass, src, false, false);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int? ClassId = ComboServ.GetComboIdInt(cbClass);

            if (!ClassId.HasValue)
            {
                WinFormsServ.Error("Не выбран класс!");
                return;
            }

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                bool bCheckResult = true;
                if (OnCheck != null)
                    bCheckResult = OnCheck(ClassId.Value);

                if (bCheckResult)
                {
                    if (OnOK != null && ClassId.HasValue)
                        OnOK(ClassId.Value);

                    this.Close();
                }
                else
                    WinFormsServ.Error("Ведомость на указанный класс уже существует!");
            }
        }
    }
}
