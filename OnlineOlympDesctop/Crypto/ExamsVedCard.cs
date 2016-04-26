using EducServLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace OnlineOlympDesctop
{
    public partial class OlympVedCard : Form
    {
        private OlympVedList owner;
        private Guid? _Id;
        private int classId;

        public OlympVedCard(OlympVedList owner, Guid? vedId)
        {
            InitializeComponent();

            _Id = vedId;
            this.owner = owner;

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                OlympVed ved = (from vd in context.OlympVed
                                where vd.Id == _Id
                                select vd).FirstOrDefault();

                this.classId = ved.ClassId;
            }

            InitControls();
        }

        public OlympVedCard(OlympVedList owner, int iClassId)
        {
            InitializeComponent();

            this.owner = owner;
            this.classId = iClassId;

            InitControls();
        }

        //дополнительная инициализация
        private void InitControls()
        {
            this.MdiParent = Util.MainForm;
            this.CenterToParent();

            //Dgv = dgvRight;

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                tbExam.Text = (from vd in context.SchoolClass
                               where vd.Id == classId
                               select vd.Name).FirstOrDefault();


                //заполнение гридов            
                FillGridLeft();
                UpdateRightGrid();
            }
        }

        private void UpdateRightGrid()
        {
            string rez = string.Empty;

            List<Guid> lstIds = new List<Guid>();
            foreach (DataGridViewRow dgrw in dgvLeft.Rows)
                lstIds.Add(Guid.Parse(dgrw.Cells["Id"].Value.ToString()));

            FillGridRight(lstIds);
        }
        private void FillGridRight(List<Guid> lstIds)
        {
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var data =
                    (from Pers in context.Person
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
                     }).ToList()
                     .Select(x => new
                     {
                         x.Id,
                         FIO = x.Surname + " " + x.Name + " " + x.SecondName,
                         Document = x.DocumentType + " " + x.DocumentSeries + " " + x.DocumentNumber + " " + x.DocumentDate.ToString()
                     });

                DataTable tbl = Converter.ConvertToDataTable(data.ToArray());

                FillGrid(dgvRight, tbl);
            }
        }
        protected virtual void FillGridLeft()
        {
            //заполнили левый
            if (_Id != null)
            {
                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    var data =
                        (from Pers in context.Person
                         join PersInVed in context.PersonInOlympVed on Pers.Id equals PersInVed.PersonId
                         where PersInVed.OlympVedId == _Id.Value
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
                         }).ToList()
                         .Select(x => new
                         {
                             x.Id,
                             FIO = x.Surname + " " + x.Name + " " + x.SecondName,
                             Document = x.DocumentType + " " + x.DocumentSeries + " " + x.DocumentNumber + " " + x.DocumentDate.ToString()
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

            dgv.Columns["Id"].Visible = false;

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
                    r.CreateCells(dgv, false, dr["Id"].ToString(), dr["FIO"].ToString(), dr["Document"].ToString());
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
                if (dgvLeft.Rows.Count == 0)
                {
                    MessageBox.Show("Нельзя создать пустую ведомость!", "Внимание");
                    return false;
                }

                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {
                        if (_Id == null)
                        {
                            _Id = Guid.NewGuid();

                            OlympVed Ved = new OlympVed();
                            Ved.ClassId = classId;
                            Ved.Id = _Id.Value;
                            Ved.IsLoad = false;
                            Ved.IsLocked = false;
                            Ved.OlympYear = Util.CampaignYear;
                            context.OlympVed.Add(Ved);
                        }
                        else
                        {
                            if (context.OlympVed.Where(x => x.Id == _Id).Select(x => x.IsLocked).DefaultIfEmpty(false).First())
                            {
                                WinFormsServ.Error("Ведомость уже закрыта!");
                                return false;
                            }
                            //параноидальная перестраховка
                            if (context.PersonInOlympVedMark.Where(x => x.PersonInOlympVed.OlympVedId == _Id && x.Mark != null).Count() > 0)
                            {
                                WinFormsServ.Error("Ведомость уже имеет оценки!");
                                return false;
                            }

                            var lstMarks = context.PersonInOlympVedMark.Where(x => x.PersonInOlympVed.OlympVedId == _Id);
                            context.PersonInOlympVedMark.RemoveRange(lstMarks);

                            var lstPers = context.PersonInOlympVed.Where(x => x.OlympVedId == _Id);
                            context.PersonInOlympVed.RemoveRange(lstPers);
                        }

                        //записи в ведомостьхистори
                        foreach (DataGridViewRow r in dgvLeft.Rows)
                        {
                            Guid? persId = new Guid(r.Cells["Id"].Value.ToString());

                            PersonInOlympVed PersInVed = new PersonInOlympVed();
                            Guid gPersInVedId = Guid.NewGuid();
                            PersInVed.Id = gPersInVedId;
                            PersInVed.PersonId = persId;
                            PersInVed.OlympVedId = _Id;
                            context.PersonInOlympVed.Add(PersInVed);

                            for (int i = 1; i <= 8; i++)
                                context.PersonInOlympVedMark.Add(new PersonInOlympVedMark() { Id = Guid.NewGuid(), PersonInOlympVedId = gPersInVedId, TaskNumber = i });

                            context.SaveChanges();
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
        private void OlympVedCard_Resize(object sender, EventArgs e)
        {
            int width = (this.Width - 90 - 11) / 2;
            dgvLeft.Width = dgvRight.Width = width;
            //int LeftX = dgvLeft.Location.X;
            //int LeftY = dgvLeft.Location.Y;
            int RightX = dgvRight.Location.X;
            int RightY = dgvRight.Location.Y;
            //dgvLeft.Location = new Point(LeftX, LeftY);
            dgvRight.Location = new Point(width + 77, RightY);

            //btnLeft.Left = 11 + width + 11;
            //btnLeftAll.Left = 11 + width + 11;

            //btnRight.Left = 90 + width - btnRight.Width - 11;
            //btnRightAll.Left = 90 + width - btnRightAll.Width - 11;
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
        #endregion

        private void ExamsVed_FormClosed(object sender, FormClosedEventArgs e)
        {
            owner.UpdateVedList();
            owner.SelectVed(_Id);
        }
    }
}
