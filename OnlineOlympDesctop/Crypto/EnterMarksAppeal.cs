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
    public partial class EnterMarksAppeal : Form
    {
        private Guid? _personInVedId;
        private bool _isModified;
        private string _className;
        private List<string> lstNumbers;
        public event Action OnSaved;

        public EnterMarksAppeal(Guid? gPersInVedId)
        {
            InitializeComponent();
            this._personInVedId = gPersInVedId;

            _isModified = true;

            InitControls();
            FillGridMarks();
        }

        private void InitControls()
        {
            this.MdiParent = Util.MainForm;
                      
            if (!_isModified)
            {
                dgvMarks.ReadOnly = true;
                btnSave.Text = "Изменить";
            }

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var olVed = (from ev in context.PersonInOlympVed
                             where ev.Id == _personInVedId
                             select ev.OlympVed).FirstOrDefault();

                _className = olVed.SchoolClass.Name.ToString();
                lblExam.Text += _className;

                if (olVed.IsLoad)
                {
                    _isModified = false;
                    dgvMarks.ReadOnly = true;
                    btnSave.Text = "Изменить";
                    btnSave.Enabled = false;
                    lblIsLoad.Text = "Загружена";
                }
            }
        }

        private void FillGridMarks()
        {
            dgvMarks.Columns.Clear();
            lstNumbers = new List<string>();
           
            DataTable examTable = new DataTable();         

            DataColumn clm;
            clm = new DataColumn();
            clm.ColumnName = "Id";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "ФИО";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);
            
            clm = new DataColumn();
            clm.ColumnName = "Номер задания";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Баллы";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Баллы (апелляция)";
            examTable.Columns.Add(clm);

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var persMark =
                    (from evh in context.PersonInOlympVedMark
                     where evh.PersonInOlympVedId == _personInVedId
                     orderby evh.TaskNumber
                     select new
                     {
                         evh.Id,
                         evh.PersonInOlympVed.Person.Surname,
                         evh.PersonInOlympVed.Person.Name,
                         evh.PersonInOlympVed.Person.SecondName,
                         evh.PersonInOlympVed.CryptNumber,
                         evh.Mark,
                         evh.AppealMark,
                         evh.TaskNumber
                     }).ToList();

                foreach (var pm in persMark)
                {
                    DataRow newRow;
                    newRow = examTable.NewRow();
                    newRow["Id"] = pm.Id;
                    newRow["ФИО"] = (pm.Surname + " " ?? "") + (pm.Name ?? "") + (" " + pm.SecondName ?? "");
                    newRow["Номер задания"] = pm.TaskNumber;
                    newRow["Баллы"] = pm.Mark;
                    newRow["Баллы (апелляция)"] = pm.AppealMark;
                    examTable.Rows.Add(newRow);

                    lstNumbers.Add(pm.CryptNumber);
                }

                DataView dv = new DataView(examTable);
                dv.AllowNew = false;

                dgvMarks.DataSource = dv;
                dgvMarks.Columns["Id"].Visible = false;
                dgvMarks.Columns["Баллы"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Columns["Баллы (апелляция)"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Columns["Номер задания"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Update();
            }
        }  

        private bool SaveMarks()
        {            
            try
            {
                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                    {                       
                        string mark = null;
                        for (int i = 0; i < dgvMarks.Rows.Count; i++)
                        {
                            Guid PersInVedMarkId = new Guid(dgvMarks["Id", i].Value.ToString());

                            if (dgvMarks["Баллы (апелляция)", i].Value != null)
                                mark = dgvMarks["Баллы (апелляция)", i].Value.ToString().Trim();

                            int? iUpdatedMark;
                            int iMark;
                                                        
                            if (string.IsNullOrEmpty(mark))
                                iUpdatedMark = null;
                            else if (int.TryParse(mark, out iMark) && iMark >= 0 && iMark < 101)
                                iUpdatedMark = iMark;
                            else
                            {
                                dgvMarks.CurrentCell = dgvMarks["Баллы", i];
                                WinFormsServ.Error("Задание №" + dgvMarks["Номер задания", i].Value.ToString() + ": неправильно введены данные");
                                return false;
                            }

                            var eMark = context.PersonInOlympVedMark
                                .Where(x => x.Id == PersInVedMarkId)
                                .FirstOrDefault();
                            if (eMark != null)
                                eMark.AppealMark = iUpdatedMark;

                            context.SaveChanges();
                        }

                        transaction.Complete();
                    }
                }

                if (OnSaved != null)
                    OnSaved();

                this.Close();

                return true;
            }

            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка сохранения данных: \n", exc);
                return false;
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveClick();
        }

        private bool SaveClick()
        {
            if (btnSave.Enabled && btnSave.Visible)
            {
                if (_isModified)
                {
                    if (SaveMarks())
                    {
                        btnSave.Text = "Изменить";
                        _isModified = false;
                        dgvMarks.ReadOnly = true;
                        return true;
                    }
                    return false;
                }
                else
                {
                    btnSave.Text = "Сохранить";
                    _isModified = true;
                    dgvMarks.ReadOnly = false;
                    dgvMarks.Columns["Id"].ReadOnly = true;
                    dgvMarks.Columns["ФИО"].ReadOnly = true;
                    dgvMarks.Columns["Номер задания"].ReadOnly = true;
                    dgvMarks.Columns["Баллы"].ReadOnly = true;
                    return true;
                }                
            }
            return false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {

        }
        
        private void EnterMarks_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isModified)
            {
                DialogResult res = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {                    
                    if (!SaveClick())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            } 
        }
    }
}
