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
    public partial class SetDiplomaList : Form
    {
        private int? SchoolClassId
        {
            get { return ComboServ.GetComboIdInt(cbSchoolClass); }
        }

        public SetDiplomaList()
        {
            InitializeComponent();
            this.MdiParent = Util.MainForm;
            FillComboClass();
            UpdateButtonVisible();
        }

        private void UpdateButtonVisible()
        {
            if (Util.IsOwner() || Util.IsPasha())
            {
                btnChange.Visible = true;
                btnChange.Enabled = true;
            }
        }

        private void FillComboClass()
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var src = context.SchoolClass.Select(x => new { x.Id, x.Name }).ToList()
                    .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)).ToList();

                ComboServ.FillCombo(cbSchoolClass, src, false, false);
            }
        }
        public void FillGrid()
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var src =
                    (from dipl in context.OlympDiploma
                     join pers in context.Person on dipl.PersonId equals pers.Id
                     where (SchoolClassId.HasValue ? dipl.SchoolClassId == SchoolClassId : true)
                     && dipl.OlympYear == Util.CampaignYear
                     select new
                     {
                         pers.Id,
                         pers.Surname,
                         pers.Name,
                         pers.SecondName,
                         pers.BirthDate,
                         SchoolClass = pers.SchoolClass.Name,
                         DiplomaLevel = dipl.DiplomaLevel.Name,
                     }).ToList()
                     .Select(x => new
                     {
                         PersonId = x.Id,
                         FIO = (x.Surname + " " ?? "") + (x.Name ?? "") + (" " + x.SecondName ?? ""),
                         x.BirthDate,
                         x.SchoolClass,
                         x.DiplomaLevel,
                     });

                dgvList.DataSource = Converter.ConvertToDataTable(src.ToArray());

                dgvList.Columns["PersonId"].Visible = false;
                //dgvList.Columns["DiplomaId"].Visible = false;
                dgvList.ReadOnly = false;
                //dgv.Columns["Number"].ReadOnly = true;
                //dgvList.Columns["RegNum"].ReadOnly = true;
                //dgvList.Columns["RegNum"].HeaderText = "Рег.Номер";
                dgvList.Columns["FIO"].ReadOnly = true;
                dgvList.Columns["FIO"].HeaderText = "ФИО";
                dgvList.Columns["BirthDate"].ReadOnly = true;
                dgvList.Columns["BirthDate"].HeaderText = "Дата рождения";
                dgvList.Columns["SchoolClass"].ReadOnly = true;
                dgvList.Columns["SchoolClass"].HeaderText = "Класс";
                dgvList.Columns["DiplomaLevel"].ReadOnly = true;
                dgvList.Columns["DiplomaLevel"].HeaderText = "Уровень";
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            SelectClassForm frm = new SelectClassForm();
            frm.OnOK += (iClassId) =>
            {
                SelectDiplomaLevelForm frm1 = new SelectDiplomaLevelForm();
                frm1.OnOK += (iDiplomaLevelId) =>
                {
                    SetDiplomaCard crd = new SetDiplomaCard(this, iClassId, iDiplomaLevelId);
                    crd.Show();
                };
                frm1.Show();
            };
            frm.Show();
        }
        private void btnPrintProtocol_Click(object sender, EventArgs e)
        {
            var crd = new PrintDiplomaList();
            crd.Show();
        }
        private void cbSchoolClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
