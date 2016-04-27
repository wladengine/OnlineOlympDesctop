using EducServLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace OnlineOlympDesctop
{
    public partial class SetDiplomaCard : Form
    {
        private SetDiplomaList owner;
        private int classId;
        private int diplomaLevelId;

        public SetDiplomaCard(SetDiplomaList owner, int iClassId, int iDiplomaLevelId)
        {
            InitializeComponent();
            this.classId = iClassId;
            this.owner = owner;
            this.diplomaLevelId = iDiplomaLevelId;
            InitControls();
        }

        //дополнительная инициализация
        private void InitControls()
        {
            this.MdiParent = Util.MainForm;
            this.CenterToParent();

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                tbClass.Text = (from vd in context.SchoolClass
                               where vd.Id == classId
                               select vd.Name).FirstOrDefault();
                
                tbLevel.Text = (from vd in context.DiplomaLevel
                                where vd.Id == diplomaLevelId
                                select vd.Name).FirstOrDefault();

                //заполнение гридов
                FillGridLeft();
                UpdateRightGrid();
            }
        }

        private void UpdateRightGrid()
        {
            FillGridRight(GetUsedPersonsList());
        }
        private List<Guid> GetUsedPersonsList()
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                return context.OlympDiploma.Select(x => x.PersonId).ToList();
            }
        }
        private void FillGridRight(List<Guid> lstIds)
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var data =
                    (from Pers in context.Person
                     join stat in context.extRatingList on Pers.Id equals stat.PersonId
                     where Pers.SchoolClassId == classId
                     && !lstIds.Contains(Pers.Id)
                     select new
                     {
                         Pers.Id,
                         Pers.Surname,
                         Pers.Name,
                         Pers.SecondName,
                         DocumentType = Pers.DocumentType.Name,
                         Pers.DocumentSeries,
                         Pers.DocumentNumber,
                         Pers.DocumentDate,
                         stat.TotalMark,
                         stat.Place,
                         stat.OlympVedId
                     }).ToList().OrderBy(x => x.Place).ThenBy(x => x.Surname).ThenBy(x => x.Name).ThenBy(x => x.SecondName)
                     .Select(x => new
                     {
                         x.Id,
                         FIO = x.Surname + " " + x.Name + " " + x.SecondName,
                         Document = x.DocumentType + " " + x.DocumentSeries + " " + x.DocumentNumber + " " + x.DocumentDate.ToString(),
                         x.OlympVedId,
                         x.TotalMark,
                         x.Place
                     });

                DataTable tbl = Converter.ConvertToDataTable(data.ToArray());

                FillGrid(dgvRight, tbl);
            }
        }
        protected virtual void FillGridLeft()
        {
            //заполнили левый
            if (classId != null)
            {
                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    var data =
                        (from Pers in context.Person
                         join Dipl in context.OlympDiploma on Pers.Id equals Dipl.PersonId
                         join stat in context.extRatingList on Pers.Id equals stat.PersonId
                         where Dipl.SchoolClassId == classId && Dipl.DiplomaLevelId == diplomaLevelId
                         select new
                         {
                             Pers.Id,
                             Pers.Surname,
                             Pers.Name,
                             Pers.SecondName,
                             DocumentType = Pers.DocumentType.Name,
                             Pers.DocumentSeries,
                             Pers.DocumentNumber,
                             Pers.DocumentDate,
                             stat.TotalMark,
                             stat.Place,
                             stat.OlympVedId
                         }).ToList().OrderBy(x => x.Place).ThenBy(x => x.Surname).ThenBy(x => x.Name).ThenBy(x => x.SecondName)
                         .Select(x => new
                         {
                             x.Id,
                             FIO = (x.Surname + " " ?? "") + (x.Name ?? "") + (" " + x.SecondName ?? ""),
                             Document = x.DocumentType + " " + x.DocumentSeries + " " + x.DocumentNumber + " " + x.DocumentDate.ToShortDateString(),
                             x.OlympVedId,
                             x.TotalMark,
                             x.Place
                         });

                    DataTable tbl = Converter.ConvertToDataTable(data.ToArray());
                    FillGrid(dgvLeft, tbl);
                }
            }
            else //новый
            {
                InitGrid(dgvLeft);
            }
        }
        //подготовка нужного грида
        private void InitGrid(DataGridView dgv)
        {
            dgv.Columns.Clear();

            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.Width = 20;
            column.ReadOnly = false;
            column.Resizable = DataGridViewTriState.False;
            dgv.Columns.Add(column);
            dgv.Columns.Add("Id", "Id");
            dgv.Columns.Add("FIO", "ФИО");
            dgv.Columns.Add("Document", "Документ");
            dgv.Columns.Add("OlympVedId", "OlympVedId");
            dgv.Columns.Add("Place", "Place");
            dgv.Columns.Add("TotalSum", "TotalSum");

            dgv.Columns["Id"].Visible = false;
            dgv.Columns["OlympVedId"].Visible = false;
            dgv.Columns["Place"].HeaderText = "Место";
            dgv.Columns["TotalSum"].HeaderText = "Сумма баллов";

            dgv.Update();
        }
        //функция заполнения грида
        private void FillGrid(DataGridView dgv, DataTable tblPersons)
        {
            try
            {
                //подготовили грид
                InitGrid(dgv);

                //заполняем строки
                foreach (DataRow dr in tblPersons.Rows)
                {
                    DataGridViewRow r = new DataGridViewRow();
                    r.CreateCells(dgv, false, dr["Id"].ToString(), dr["FIO"].ToString(), dr["Document"].ToString(), dr["OlympVedId"].ToString(), dr["Place"].ToString(), dr["TotalMark"].ToString());
                    dgv.Rows.Add(r);
                }

                //блокируем редактирование
                for (int i = 1; i < dgv.ColumnCount; i++)
                    dgv.Columns[i].ReadOnly = true;

                dgv.Update();
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при заполнении грида данными протокола: ", ex);
            }
        }

        private bool Save()
        {
            try
            {
                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        //записи в ведомостьхистори
                        List<Guid> lstOldPersons = context.OlympDiploma
                            .Where(x => x.OlympYear == Util.CampaignYear && x.SchoolClassId == classId && x.DiplomaLevelId == diplomaLevelId)
                            .Select(x => x.PersonId).ToList();

                        List<Guid> lstNewPersons = new List<Guid>();

                        foreach (DataGridViewRow r in dgvLeft.Rows)
                        {
                            Guid gPersonId = new Guid(r.Cells["Id"].Value.ToString());
                            Guid gOlympVedId = new Guid(r.Cells["OlympVedId"].Value.ToString());
                            int iPlace = int.Parse(r.Cells["Place"].Value.ToString());
                            int iTotalSum = int.Parse(r.Cells["TotalSum"].Value.ToString());

                            lstNewPersons.Add(gPersonId);

                            if (context.OlympDiploma.Where(x => x.PersonId == gPersonId).Count() == 0)
                            {
                                OlympDiploma Dipl = new OlympDiploma();
                                Guid gPersInVedId = Guid.NewGuid();
                                Dipl.Id = gPersInVedId;
                                Dipl.PersonId = gPersonId;
                                Dipl.OlympVedId = gOlympVedId;
                                Dipl.SchoolClassId = classId;
                                Dipl.CreateDate = DateTime.Now;
                                Dipl.DiplomaLevelId = diplomaLevelId;
                                Dipl.OlympSubjectId = 1;
                                Dipl.OlympYear = Util.CampaignYear;
                                Dipl.Place = iPlace;
                                Dipl.TotalSum = iTotalSum;
                                Dipl.CreateAuthor = Util.GetUserName();

                                context.OlympDiploma.Add(Dipl);

                                for (int i = 1; i <= 8; i++)
                                    context.OlympDiploma.Add(Dipl);

                                context.SaveChanges();
                            }
                        }

                        List<Guid> lstDiff = lstOldPersons.Except(lstNewPersons).ToList();

                        if (lstDiff.Count > 0)
                        {
                            var lstDipl = context.OlympDiploma.Where(x => lstDiff.Contains(x.PersonId));
                            if (lstDipl.Where(x => !string.IsNullOrEmpty(x.DiplomaRegNum)).Count() > 0)
                            {
                                WinFormsServ.Error("Удаляемые дипломы уже имеют регистрационные номера!");
                                return false;
                            }
                            else
                            {
                                context.OlympDiploma.RemoveRange(lstDipl);
                                context.SaveChanges();
                            }
                        }

                        transaction.Complete();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при создании новой ведомости: ", ex);
                return false;
            }
        }

        //отмена
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //все хорошо
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Save())
                this.Close();
        }
        //ресайз
        private void SetDiplomaCard_Resize(object sender, EventArgs e)
        {
            int width = (this.Width - 90 - 11) / 2;
            dgvLeft.Width = dgvRight.Width = width;
            int RightX = dgvRight.Location.X;
            int RightY = dgvRight.Location.Y;
            dgvRight.Location = new Point(width + 77, RightY);
        }

        #region MoveRows
        //убрать влево
        private void btnLeft_Click(object sender, EventArgs e)
        {
            MoveRows(dgvRight, dgvLeft, false);
        }
        //убрать вправо
        private void btnRight_Click(object sender, EventArgs e)
        {
            MoveRows(dgvLeft, dgvRight, false);
        }
        //все влево
        private void btnLeftAll_Click(object sender, EventArgs e)
        {
            MoveRows(dgvRight, dgvLeft, true);
        }
        //все вправо
        private void btnRightAll_Click(object sender, EventArgs e)
        {
            MoveRows(dgvLeft, dgvRight, true);
        }
        //перенос строк
        private void MoveRows(DataGridView from, DataGridView to, bool isAll)
        {
            for (int i = from.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow dr = from.Rows[i];

                if (isAll || (bool)dr.Cells[0].Value)
                {
                    dr.Cells[0].Value = false;
                    from.Rows.Remove(dr);
                    to.Rows.Add(dr);
                }
            }
        }
        #endregion

        private void SetDiplomaCard_FormClosed(object sender, FormClosedEventArgs e)
        {
            owner.FillGrid();
        }
    }
}
