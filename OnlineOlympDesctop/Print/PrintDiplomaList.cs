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
    public partial class PrintDiplomaList : Form
    {
        private int? SchoolClassId
        {
            get { return ComboServ.GetComboIdInt(cbClass); }
        }
        private int? DiplomaLevelId
        {
            get { return ComboServ.GetComboIdInt(cbDiplomaLevel); }
        }

        public PrintDiplomaList()
        {
            InitializeComponent();
            this.MdiParent = Util.MainForm;
            FillCombos();
        }

        private void FillCombos()
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var src = context.SchoolClass.Select(x => new { x.Id, x.Name }).ToList()
                    .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)).ToList();

                ComboServ.FillCombo(cbClass, src, false, false);

                src = context.DiplomaLevel.Select(x => new { x.Id, x.Name }).ToList()
                    .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)).ToList();

                ComboServ.FillCombo(cbDiplomaLevel, src, false, false);
            }
        }

        private void FillGrid()
        {
            DataView dv = new DataView(GetSource());
            dv.AllowNew = false;

            dgv.DataSource = dv;

            if (!dgv.Columns.Contains("PrintPdf"))
            {
                DataGridViewButtonColumn dgvbc = new DataGridViewButtonColumn();
                dgvbc.Text = "В Pdf";
                dgvbc.Name = "PrintPdf";
                dgvbc.HeaderText = "Просмотр в Pdf";
                dgvbc.UseColumnTextForButtonValue = true;
                dgvbc.Visible = true;
                dgv.Columns.Add(dgvbc);
            }

            dgv.Columns["PersonId"].Visible = false;
            dgv.Columns["DiplomaId"].Visible = false;
            dgv.ReadOnly = false;
            //dgv.Columns["Number"].ReadOnly = true;
            dgv.Columns["RegNum"].ReadOnly = true;
            dgv.Columns["RegNum"].HeaderText = "Рег.Номер";
            dgv.Columns["FIO"].ReadOnly = true;
            dgv.Columns["FIO"].HeaderText = "ФИО";
            dgv.Columns["BirthDate"].ReadOnly = true;
            dgv.Columns["BirthDate"].HeaderText = "Дата рождения";
            dgv.Columns["SchoolClass"].ReadOnly = true;
            dgv.Columns["SchoolClass"].HeaderText = "Класс";
            dgv.Columns["DiplomaLevel"].ReadOnly = true;
            dgv.Columns["DiplomaLevel"].HeaderText = "Уровень";
            dgv.Columns["DiplomaDate"].ReadOnly = true;
            dgv.Columns["DiplomaDate"].HeaderText = "Дата выдачи";
            dgv.Columns["BlankNumber"].ReadOnly = true;
            dgv.Columns["BlankNumber"].HeaderText = "Номер бланка";
            
        }
        private DataTable GetSource()
        {
            DataTable tbl = new DataTable();

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var src =
                    (from dipl in context.OlympDiploma
                     join pers in context.Person on dipl.PersonId equals pers.Id
                     where (SchoolClassId.HasValue ? dipl.SchoolClassId == SchoolClassId : true)
                     && dipl.DiplomaLevelId == DiplomaLevelId
                     select new
                     {
                         pers.Id,
                         pers.Surname,
                         pers.Name,
                         pers.SecondName,
                         pers.BirthDate,
                         SchoolClass = pers.SchoolClass.Name,
                         DiplomaLevel = dipl.DiplomaLevel.Name,
                         dipl.DiplomaRegNum,
                         dipl.DiplomaDate,
                         DiplomaId = dipl.Id,
                         dipl.BlankNumber
                     }).ToList()
                     .Select(x => new
                     {
                         PersonId = x.Id,
                         FIO = (x.Surname + " " ?? "") + (x.Name ?? "") + (" " + x.SecondName ?? ""),
                         x.BirthDate,
                         x.SchoolClass,
                         x.DiplomaLevel,
                         RegNum = x.DiplomaRegNum,
                         x.DiplomaDate,
                         x.DiplomaId,
                         x.BlankNumber
                     });

                tbl = Converter.ConvertToDataTable(src.ToArray());
            }

            return tbl;
        }

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void cbDiplomaLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string itemId = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["PersonId"].Value.ToString();

            //int i = dgv.Columns["Проверено"].Index;

            if (e.ColumnIndex == dgv.Columns["PrintPdf"].Index)
                OpenPdf(itemId);
            //else if (e.ColumnIndex == dgv.Columns["Check"].Index && dgv[i, e.RowIndex].Value.ToString() == "нет" /*&& _bdc.IsRectoratCheck()*/)
            //    CheckedToPrint(itemId);
            //else if (e.ColumnIndex == dgv.Columns["Return"].Index) /* && dgvDiploms[i, e.RowIndex].Value.ToString() == "нет" && _bdc.IsRectoratCheck()*/)
            //    ReturnToFacMain(itemId);
            //else if (e.ColumnIndex == dgv.Columns["ChangeNum"].Index)
            //    ChangeNum(itemId, dgv["Рег_номер", e.RowIndex].Value.ToString());
            else
                return;
        }

        private void OpenPdf(string itemId)
        {
            Guid PersonId = new Guid(itemId);

            PDFUtil.PrintDiploma(false, Util.TempFolder + "//dipl.pdf", PersonId);
        }

        private Guid GetDiplomaIdFromSelectedCell()
        {
            string itemId = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["DiplomaId"].Value.ToString();
            return new Guid(itemId);
        }
        private Guid GetPersonIdFromSelectedCell()
        {
            string itemId = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["PersonId"].Value.ToString();
            return new Guid(itemId);
        }

        private void btnSetDiplomaDate_Click(object sender, EventArgs e)
        {
            var crd = new FormSetDiplomaDate(GetDiplomaIdFromSelectedCell());
            crd.OnOK += FillGrid;
            crd.Show();
        }
        private void btnSetRegNum_Click(object sender, EventArgs e)
        {
            var crd = new FormSetDiplomaRegNum(GetDiplomaIdFromSelectedCell());
            crd.OnOK += FillGrid;
            crd.Show();
        }
        private void btnSetSchoolName_Click(object sender, EventArgs e)
        {
            var crd = new FormSetDiplomaSchoolName(GetPersonIdFromSelectedCell());
            crd.OnOK += FillGrid;
            crd.Show();
        }
        private void btnSetDiplomaBlankNumber_Click(object sender, EventArgs e)
        {
            var crd = new FormSetDiplomaBlankNumber(GetDiplomaIdFromSelectedCell());
            crd.OnOK += FillGrid;
            crd.Show();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}