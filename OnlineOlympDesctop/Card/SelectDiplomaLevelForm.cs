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
    public partial class SelectDiplomaLevelForm : Form
    {
        public event Action<int> OnOK;
        public event Func<int, bool> OnCheck;
        public SelectDiplomaLevelForm()
        {
            InitializeComponent();
            this.MdiParent = Util.MainForm;
            FillCombo();
        }

        private void FillCombo()
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var src = context.DiplomaLevel.Select(x => new { x.Id, x.Name })
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name))
                    .ToList();

                ComboServ.FillCombo(cbDiplomaLevel, src, false, false);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int? DiplomaLevelId = ComboServ.GetComboIdInt(cbDiplomaLevel);

            if (!DiplomaLevelId.HasValue)
            {
                WinFormsServ.Error("Не указан уровень!");
                return;
            }

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                bool bCheckResult = true;
                if (OnCheck != null)
                    bCheckResult = OnCheck(DiplomaLevelId.Value);

                if (bCheckResult)
                {
                    if (OnOK != null && DiplomaLevelId.HasValue)
                        OnOK(DiplomaLevelId.Value);

                    this.Close();
                }
                else
                    WinFormsServ.Error("Ведомость на указанный уровень уже существует!");
            }
        }
    }
}
