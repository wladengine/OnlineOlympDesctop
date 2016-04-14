﻿using EducServLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace OnlineOlympDesctop
{
    public partial class EnterMarks : Form
    {
        private Guid? _personInVedId;
        private bool _isModified;
        private string _className;
        private List<string> lstNumbers;

        public EnterMarks(Guid? gPersInVedId)
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
            clm.ColumnName = "Шифр";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Номер задания";
            clm.ReadOnly = true;
            examTable.Columns.Add(clm);

            clm = new DataColumn();
            clm.ColumnName = "Баллы";            
            examTable.Columns.Add(clm);

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var persMark = from evh in context.PersonInOlympVedMark
                               where evh.PersonInOlympVedId == _personInVedId
                               select new
                               {
                                   evh.Id,
                                   evh.PersonInOlympVed.Person.Surname,
                                   evh.PersonInOlympVed.Person.Name,
                                   evh.PersonInOlympVed.Person.SecondName,
                                   evh.PersonInOlympVed.CryptNumber,
                                   evh.Mark,
                                   evh.TaskNumber
                               };

                foreach (var pm in persMark)
                {
                    DataRow newRow;
                    newRow = examTable.NewRow();
                    newRow["Id"] = pm.Id;
                    newRow["ФИО"] = (pm.Surname + " " ?? "") + (pm.Name ?? "") + (" " + pm.SecondName ?? "");
                    newRow["Шифр"] = pm.CryptNumber;
                    newRow["Номер задания"] = pm.TaskNumber;
                    newRow["Баллы"] = pm.Mark;
                    examTable.Rows.Add(newRow);

                    lstNumbers.Add(pm.CryptNumber);
                }

                DataView dv = new DataView(examTable);
                dv.AllowNew = false;

                dgvMarks.DataSource = dv;
                dgvMarks.Columns["Id"].Visible = false;
                dgvMarks.Columns["ФИО"].Visible = false;

                //if (!_forLoad && (bdc.IsCryptoMain() || bdc.IsSuperman()))
                //    dgvMarks.Columns["ФИО"].Visible = true;

                dgvMarks.Columns["Шифр"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvMarks.Columns["Баллы"].SortMode = DataGridViewColumnSortMode.NotSortable;
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
                            string CryptNumber = dgvMarks["Шифр", i].Value.ToString();
                            Guid PersInVedMarkId = new Guid(dgvMarks["Id", i].Value.ToString());
                            
                            if (dgvMarks["Баллы", i].Value != null)
                                mark = dgvMarks["Баллы", i].Value.ToString().Trim();

                            decimal? dUpdatedMark;
                            decimal dMark;
                                                        
                            if (string.IsNullOrEmpty(mark))
                                dUpdatedMark = null;
                            else if (decimal.TryParse(mark, out dMark) && dMark >= 0 && dMark < 101)
                                dUpdatedMark = dMark;
                            else
                            {
                                dgvMarks.CurrentCell = dgvMarks["Баллы", i];
                                WinFormsServ.Error("Задание №" + dgvMarks["Номер задания", i].Value.ToString() + ": неправильно введены данные");
                                return false;
                            }

                            var eMark = context.PersonInOlympVedMark
                                .Where(x => x.Id == PersInVedMarkId && x.PersonInOlympVed.CryptNumber == CryptNumber)
                                .FirstOrDefault();
                            if (eMark != null)
                                eMark.Mark = dUpdatedMark;

                            context.SaveChanges();
                        }

                        transaction.Complete();
                        return true;
                    }
                }
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
                    dgvMarks.Columns["PersonId"].ReadOnly = true;
                    dgvMarks.Columns["Номер"].ReadOnly = true;
                    return true;
                }                
            }
            return false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Novacode.DocX doc;
            string saveFileName = Util.TempFolder + @"\TableMarksView.docx";

            try
            {
                File.Copy(Util.dirTemplates + @"\TableMarksView.docx", saveFileName, true);
                doc = Novacode.DocX.Load(saveFileName);

                int colCount = 0;
                foreach (DataGridViewColumn clm in dgvMarks.Columns)
                {
                    if (clm.Visible)
                        colCount++;
                }

                Novacode.Table td = doc.Tables[2];

                // печать из грида
                int i = 0;
                foreach (DataGridViewColumn clm in dgvMarks.Columns)
                {
                    if (clm.Visible)
                    {
                        td.Rows[0].Cells[i].Paragraphs[0].InsertText(clm.HeaderText);
                        td.InsertRow();
                        i++;
                    }
                }

                i = 1;
                int j;
                foreach (DataGridViewRow dgvr in dgvMarks.Rows)
                {
                    j = 0;
                    foreach (DataGridViewColumn clm in dgvMarks.Columns)
                    {
                        if (clm.Visible)
                        {
                            td.Rows[j].Cells[i].Paragraphs[0].InsertText(dgvr.Cells[clm.Index].Value.ToString());
                            j++;
                        }
                    }
                    i++;
                    td.InsertRow();
                }

                doc.Save();

                Process.Start(saveFileName);
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка вывода в Word: \n", exc);
            }
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
