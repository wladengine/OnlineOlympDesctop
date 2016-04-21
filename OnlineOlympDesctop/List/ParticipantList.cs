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
    public partial class ParticipantList : Form
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
        public int? ClassId
        {
            get { return ComboServ.GetComboIdInt(cbClass); }
            set { ComboServ.SetComboId(cbClass, value); }
        }

        public ParticipantList()
        {
            InitializeComponent();
            this.MdiParent = Util.MainForm;
            FillCard();
        }

        public void FillCard()
        {

            ComboServ.FillCombo(cbCountry, HelpClass.GetComboListByTable("dbo.Country", " order by LevelOfUsing desc"), false, true);
            ComboServ.FillCombo(cbRegion, HelpClass.GetComboListByTable("dbo.Region"), false, true);
            ComboServ.FillCombo(cbClass, HelpClass.GetComboListByTable("dbo.SchoolClass"), false, true);

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
                var lst = (from x in context.Participant
                           join c in context.Country on x.NationalityId equals c.Id
                           join r in context.Region on x.RegionId equals r.Id
                           join cl in context.SchoolClass on x.ClassId equals cl.Id
                           where (CountryId.HasValue ? x.NationalityId == CountryId.Value : true)
                           && (RegionId.HasValue ? x.RegionId == RegionId.Value : true)
                           && (ClassId.HasValue ? x.ClassId == ClassId : true)
                           && (ShowHidden ? true : !x.IsHidden)
                           select new
                           {
                               x.Id,
                               x.UserId,
                               x.IsHidden,
                               Фамилия = x.Surname,
                               Имя = x.Name,
                               Отчество = x.SecondName,
                               Страна = c.Name,
                               Регион = x.Country.IsRussia ? r.Name : "",
                               Класс = cl.Name,
                               HasFiles = (context.PersonFile.Where(p => p.ParticipantId == x.Id).Count() > 0),
                               Файлы = (context.PersonFile.Where(p => p.ParticipantId == x.Id).Count() > 0) ? "да" : "нет",
                           }).ToList().OrderBy(x => x.Фамилия).ThenBy(x=>x.Имя).ToList();

                if (rbWithFiles.Checked)
                    lst = lst.Where(x => x.HasFiles).ToList();
                else if (rbNoFiles.Checked)
                    lst = lst.Where(x => !x.HasFiles).ToList();

                dgv.DataSource = lst;
                foreach (var s in new List<string>() { "Id", "HasFiles", "UserId", "IsHidden" })
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

        private void tbFIO_TextChanged(object sender, EventArgs e)
        {
            WinFormsServ.Search(this.dgv, "Фамилия", tbFIO.Text);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell == null)
                return;
            if (dgv.CurrentCell.RowIndex == -1)
                return;

            Guid Id = Guid.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
            Guid? UserId = null;
            try
            {
                UserId = Guid.Parse(dgv.CurrentRow.Cells["UserId"].Value.ToString());
            }
            catch
            {
            }

            List<Guid> lst = new List<Guid>();
            foreach (DataGridViewRow rw in dgv.Rows)
                lst.Add(Guid.Parse(rw.Cells["Id"].Value.ToString()));
            new ParticipantCard(Id, UserId, lst, new UpdateHandler(FillGrid)).Show();
        }

        private void btnParticipantAdd_Click(object sender, EventArgs e)
        {
            new ParticipantCard(new UpdateHandler(FillGrid)).Show();
        }

        private void asmiparticipantprintxls_Click(object sender, EventArgs e)
        {
            new ParticipantPrint().Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
