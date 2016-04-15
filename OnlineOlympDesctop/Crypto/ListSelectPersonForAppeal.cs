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
    public partial class ListSelectPersonForAppeal : Form
    {
        public Guid? OlympVedId
        {
            get { return ComboServ.GetComboIdGuid(cbClass); }
        }
        public ListSelectPersonForAppeal()
        {
            InitializeComponent();

            this.MdiParent = Util.MainForm;

            UpdateComboClass();
        }

        private void UpdateComboClass()
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var src = context.OlympVed.Select(x => new { x.Id, x.SchoolClass.Name })
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name))
                    .ToList();

                ComboServ.FillCombo(cbClass, src, false, false);
            }
        }

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OlympVedId.HasValue)
                UpdateGrid();
        }

        private void UpdateGrid()
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var src =
                    (from rating in context.extRatingList
                     join Pers in context.Person on rating.PersonId equals Pers.Id
                     join PersInVed in context.PersonInOlympVed on Pers.Id equals PersInVed.PersonId
                     join Ved in context.OlympVed on rating.OlympVedId equals Ved.Id
                     where PersInVed.OlympVedId == Ved.Id
                     && Ved.Id == OlympVedId.Value
                     select new
                     {
                         PersInVedId = PersInVed.Id,
                         Pers.Surname,
                         Pers.Name,
                         Pers.SecondName,
                         rating.TotalMark,
                         rating.CryptNumber
                     }).ToList()
                     .Select(x => new {
                         x.PersInVedId,
                         FIO = x.Surname + " " + x.Name + " " + x.SecondName,
                         x.TotalMark,
                         x.CryptNumber
                     }).OrderBy(x => x.FIO).ToList();

                dgvPersons.DataSource = Converter.ConvertToDataTable(src.ToArray());
                dgvPersons.Columns["PersInVedId"].Visible = false;
                dgvPersons.Columns["FIO"].HeaderText = "ФИО";
                dgvPersons.Columns["CryptNumber"].HeaderText = "Шифр";
                dgvPersons.Columns["TotalMark"].HeaderText = "Сумма баллов";
            }
        }

        private void dgvPersons_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Guid gId = (Guid)dgvPersons["PersInVedId", e.RowIndex].Value;
            var crd = new EnterMarksAppeal(gId);
            crd.OnSaved += UpdateGrid;
            crd.Show();
        }
    }
}
