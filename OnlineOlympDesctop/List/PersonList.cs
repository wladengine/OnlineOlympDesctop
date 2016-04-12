using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EducServLib;

namespace OnlineOlympDesctop
{
    public delegate void UpdateHandler();

    public partial class PersonList : Form
    {
        public int? CountryId
        {
            get { return ComboServ.GetComboIdInt(cbCountry); }
            set { ComboServ.SetComboId(cbCountry, value); }
        }
        public int? RegionId
        {
            get { return ComboServ.GetComboIdInt(cbRegion); }
            set { ComboServ.SetComboId(cbRegion, value); }
        }


        public PersonList()
        {
            InitializeComponent();
            this.MdiParent = Util.MainForm;
            FillCard();
        }

        public void FillCard()
        {
            ComboServ.FillCombo(cbCountry, HelpClass.GetComboListByTable("dbo.Country", " order by LevelOfUsing desc"), false, true);
            ComboServ.FillCombo(cbRegion, HelpClass.GetComboListByTable("dbo.Region"), false, true);
            FillGrid();
        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CountryId != Util.CountryRussiaId)
                cbRegion.Enabled = false;
            else
                cbRegion.Enabled = true;
        }

        public void FillGrid()
        {
            bool ShowHidden = !chbDontShowHidden.Checked;
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                var lst = (from x in context.Person
                           join c in context.Country on x.NationalityId equals c.Id
                           join r in context.Region on x.RegionId equals r.Id

                           where (CountryId.HasValue ? x.NationalityId == CountryId.Value : true)
                           && (RegionId.HasValue ? x.RegionId == RegionId.Value : true)
                           && (ShowHidden ? true : !x.IsHidden)
                           select new
                           {
                               x.Id,
                               Фамилия = x.Surname,
                               Имя = x.Name,
                               x.IsHidden,
                               Отчество = x.SecondName,
                               Страна = c.Name,
                               Регион = x.Country.IsRussia ? r.Name : "",
                               Участники = context.Participant.Where(p => p.UserId == x.UserId).Count(),
                               HasFiles = (context.PersonFile.Where(p => p.PersonId == x.Id).Count() > 0),
                               Файлы = (context.PersonFile.Where(p => p.PersonId == x.Id).Count() > 0) ? "да" : "нет",
                           }).ToList().OrderBy(x => x.Регион).ThenBy(x => x.Фамилия).ToList();
                if (rbWithFiles.Checked)
                    lst = lst.Where(x => x.HasFiles).ToList();
                else if (rbNoFiles.Checked)
                    lst = lst.Where(x => !x.HasFiles).ToList();

                dgv.DataSource = lst;

                foreach (var s in new List<string>() { "Id", "HasFiles", "IsHidden" })
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;

                lblCount.Text = dgv.Rows.Count.ToString();
                foreach (DataGridViewRow rw in dgv.Rows)
                {
                    if (rw.Cells["isHidden"].Value.ToString() == "1" || rw.Cells["isHidden"].Value.ToString().ToLower() == "true")
                        foreach (DataGridViewCell cl in rw.Cells)
                            cl.Style.BackColor = Color.LightGray;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell == null)
                return;
            if (dgv.CurrentCell.RowIndex == -1)
                return;

            Guid Personid = Guid.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
            List<Guid> lst = new List<Guid>();
            foreach (DataGridViewRow rw in dgv.Rows)
                lst.Add(Guid.Parse(rw.Cells["Id"].Value.ToString()));
            new PersonCard(Personid, lst, new UpdateHandler(FillGrid)).Show();
        }

        private void btnPersonAdd_Click(object sender, EventArgs e)
        {
            new PersonCard(new UpdateHandler(FillGrid)).Show();
        }

        private void tbFIO_TextChanged(object sender, EventArgs e)
        {
            WinFormsServ.Search(this.dgv, "Фамилия", tbFIO.Text);
        }
    }
}
