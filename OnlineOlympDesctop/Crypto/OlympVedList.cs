using EducServLib;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class OlympVedList : Form
    {
        public Guid? OlympVedId
        {
            get
            {
                string valId = ComboServ.GetComboId(cbExamVed);
                if (string.IsNullOrEmpty(valId))
                    return null;
                else
                    return new Guid(valId);
            }
            set
            {
                if (value == null)
                    ComboServ.SetComboId(cbExamVed, (string)null);
                else
                    ComboServ.SetComboId(cbExamVed, value.ToString());
            }
        }

        public OlympVedList()
        {
            InitializeComponent();
            this.MdiParent = Util.MainForm;

            Task t = new Task(PersonCopier.UpdateInnerPersonsDatabase);
            t.Start();
            
            //PersonCopier.UpdateInnerPersonsDatabase()
            InitControls();
        }

        //дополнительная инициализация контролов
        private void InitControls()
        {
            try
            {
                if (Util.IsPasha() || Util.IsOwner())
                {
                    btnCreate.Visible = true;
                    btnChange.Visible = true;

                    tbCountCell.Visible = true;
                    lblCountCell.Visible = true;
                    btnLock.Visible = true;
                    btnPrintSticker.Visible = true;

                    btnChange.Visible = true;
                    btnDeleteFromVed.Visible = btnDeleteFromVed.Enabled = true;
                    btnUnload.Visible = btnUnload.Enabled = true;
                }
                else
                {
                    btnCreate.Visible = false;
                    btnChange.Visible = false;

                    tbCountCell.Visible = false;
                    lblCountCell.Visible = false;
                    btnLock.Visible = false;
                    btnPrintSticker.Visible = false;

                    btnUnload.Visible = btnUnload.Enabled = false;
                    btnDeleteFromVed.Visible = btnDeleteFromVed.Enabled = false;
                }

                tbCountCell.Text = (2).ToString();

                UpdateVedList();
                UpdateDataGrid();

                cbExamVed.SelectedIndexChanged += new EventHandler(cbExamVed_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при инициализации формы ведомостей: ", ex);
            }
        }

        void cbExamVed_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        //обновление списка 
        public void UpdateVedList()
        {
            try
            {
                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    List<KeyValuePair<string, string>> lst =
                        (from ent in context.OlympVed
                         where ent.OlympYear == DateTime.Now.Year
                         select new
                         {
                             ent.Id,
                             SchoolClass = ent.SchoolClass.Name
                         })
                         .Distinct()
                         .ToList()
                         .Select(u => new KeyValuePair<string, string>(u.Id.ToString(), u.SchoolClass))
                         .ToList();

                    ComboServ.FillCombo(cbExamVed, lst, true, false);
                }
            }
            catch (Exception ex)
            {
                WinFormsServ.Error("Ошибка при обновлении списка ведомостей: ", ex);
            }
        }

        //обновление грида
        public virtual void UpdateDataGrid()
        {
            //скрыли/показали кнопку, если надо            
            if (OlympVedId == null)
            {
                btnChange.Enabled = false;
                btnLock.Enabled = false;

                lblLocked.Visible = false;

                tbCountCell.Enabled = false;
                btnPrintSticker.Enabled = false;

                dgvList.DataSource = null;
                dgvList.Update();
                return;
            }
            else
            {
                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    bool isLocked = (from ev in context.OlympVed
                                     where ev.Id == OlympVedId
                                     select ev.IsLocked).FirstOrDefault();
                    if (isLocked)
                    {
                        lblLocked.Visible = true;
                        btnChange.Enabled = false;
                        btnLock.Enabled = false;
                        btnPrintSticker.Enabled = true;
                        tbCountCell.Enabled = true;
                    }
                    else
                    {
                        lblLocked.Visible = false;
                        btnChange.Enabled = true;
                        btnLock.Enabled = true;
                        btnPrintSticker.Enabled = false;
                        tbCountCell.Enabled = false;
                    }

                    var src =
                        (from Ved in context.OlympVed
                         join PersInVed in context.PersonInOlympVed on Ved.Id equals PersInVed.OlympVedId
                         join Pers in context.Person on PersInVed.PersonId equals Pers.Id
                         where Ved.Id == OlympVedId
                         select new
                         {
                             Pers.Id,
                             Pers.Surname,
                             Pers.Name,
                             Pers.SecondName,
                             Pers.BirthDate
                         }).ToList()
                         .Select(x => new
                         {
                             x.Id,
                             ФИО = (x.Surname + " " ?? "") + (x.Name ?? "") + (" " + x.SecondName ?? ""),
                             Дата_рождения = x.BirthDate.ToShortDateString()
                         }).ToArray();

                    dgvList.DataSource = Converter.ConvertToDataTable(src);

                    dgvList.Columns["Id"].Visible = false;
                }
            }

        }

        //закрытие
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //печать
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (OlympVedId == null)
                return;

            try
            {
                //WordDoc wd = new WordDoc(string.Format(@"{0}\Templates\CryptoExamsVed.dot", Application.StartupPath));
                //TableDoc td = wd.Tables[0];

                //using (PriemEntities context = new PriemEntities())
                //{
                //    extExamsVed ved = (from ev in context.extExamsVed
                //                       where ev.Id == ExamsVedId
                //                       select ev).FirstOrDefault();

                //    wd.Fields["Faculty"].Text = cbFaculty.Text.ToLower();
                //    wd.Fields["Exam"].Text = ved.ExamName;
                //    wd.Fields["StudyBasis"].Text = Util.ToStr(ved.StudyBasisId == null ? "все" : ved.StudyBasisName);
                //    wd.Fields["Date"].Text = ved.Date.ToShortDateString();
                //    wd.Fields["VedNum"].Text = ved.Number.ToString();

                //    int i = 1;

                //    // печать из грида
                //    foreach (DataGridViewRow dgvr in dgvList.Rows)
                //    {
                //        td[0, i] = i.ToString();
                //        td[1, i] = dgvr.Cells["Фамилия"].Value.ToString();
                //        td[2, i] = dgvr.Cells["Имя"].Value.ToString();
                //        td[3, i] = dgvr.Cells["Отчество"].Value.ToString();
                //        td[4, i] = DateTime.Parse(dgvr.Cells["Дата_рождения"].Value.ToString()).ToShortDateString();
                //        td[5, i] = dgvr.Cells["Ид_номер"].Value.ToString();
                //        td[6, i] = FacultyId.ToString();
                //        td[7, i] = ved.ExamName;
                //        td[8, i] = ved.ExamId.ToString();
                //        td[9, i] = ved.Date.ToShortDateString(); ;

                //        td.AddRow(1);
                //        i++;
                //    }

                //    td.DeleteLastRow();
                //}
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка вывода в Word: \n", exc);
            }
        }
        //изменение - только для супер
        private void btnChange_Click(object sender, EventArgs e)
        {
            //if (MainClass.RightsFacMain())
            {
                OlympVedCard p = new OlympVedCard(this, OlympVedId);
                p.Show();
            }
        }
        //создать новый протокол
        protected virtual void btnCreate_Click(object sender, EventArgs e)
        {
            if (Util.IsPasha() || Util.IsOwner())
            {
                SelectClassForm frm = new SelectClassForm();
                frm.OnCheck += new Func<int, bool>(x => 
                    {
                        using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                        {
                            int zz = context.OlympVed.Where(z => z.ClassId == x && z.OlympYear == Util.CampaignYear).Count();
                            return zz == 0;
                        }
                    });
                frm.OnOK += new Action<int>(x => { OlympVedCard p = new OlympVedCard(this, x); p.Show(); });
                frm.Show();
            }
        }

        //выбрать ведомость в списке
        public void SelectVed(Guid? vedId)
        {
            if (vedId != null)
                OlympVedId = vedId;
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (OlympVedId == null)
                return;

            if (Util.IsOwner() || Util.IsPasha())
            {
                if (MessageBox.Show("Ведомость будет закрыта для редактирования, продолжить? ", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                        {
                            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                            {
                                List<int> lstNumbers = new List<int>();
                                int curNum;
                                foreach (DataGridViewRow dgvr in dgvList.Rows)
                                {
                                    Guid persId = new Guid(dgvr.Cells["Id"].Value.ToString());
                                    curNum = GetRandomNumber(ref lstNumbers);
                                    
                                    //обновляем шифрономера
                                    PersonInOlympVed PersInVed = context.PersonInOlympVed.Where(x => x.OlympVedId == OlympVedId && x.PersonId == persId).FirstOrDefault();
                                    if (PersInVed != null)
                                        PersInVed.CryptNumber = curNum.ToString("D5");
                                    
                                    context.SaveChanges();
                                }

                                var Ved = context.OlympVed.Where(x => x.Id == OlympVedId).FirstOrDefault();
                                if (Ved != null)
                                    Ved.IsLocked = true;
                                context.SaveChanges();

                                btnChange.Enabled = false;
                                btnLock.Enabled = false;
                                lblLocked.Visible = true;

                                btnPrintSticker.Enabled = true;
                                tbCountCell.Enabled = true;

                                transaction.Complete();
                            }

                            MessageBox.Show("Выполнено");
                        }
                    }
                    catch (Exception ex)
                    {
                        WinFormsServ.Error("Ошибка обновления данных: ", ex);
                    }
                }
            }
            else
                WinFormsServ.Error("Невозможно закрытие ведомостей, недостаточно прав");
        }

        private int GetRandomNumber(ref List<int> lstNums)
        {
            int g;
            Random r = new Random();
            g = r.Next(99999);

            if (!lstNums.Contains(g))
            {
                lstNums.Add(g);
                return g;
            }
            else
                return GetRandomNumber(ref lstNums);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OlympVedId == null)
                return;

            try
            {
                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    int cnt = (from ev in context.OlympVed
                               where ev.Id == OlympVedId && (ev.IsLocked || ev.IsLoad)
                               select ev).Count();

                    if (cnt > 0)
                    {
                        WinFormsServ.Error("Данная ведомость уже закрыта. Удаление невозможно!");
                        return;
                    }

                    if (Util.IsPasha())
                    {
                        if (MessageBox.Show("Удалить выбранную ведомость? ", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                            {
                                var lstMarks = context.PersonInOlympVedMark.Where(x => x.PersonInOlympVed.OlympVedId == OlympVedId);
                                context.PersonInOlympVedMark.RemoveRange(lstMarks);

                                var lstPers = context.PersonInOlympVed.Where(x => x.OlympVedId == OlympVedId);
                                context.PersonInOlympVed.RemoveRange(lstPers);

                                var ved = context.OlympVed.Where(x => x.Id == OlympVedId);
                                context.OlympVed.RemoveRange(ved);

                                context.SaveChanges();

                                transaction.Complete();
                                UpdateVedList();
                            }
                        }
                    }
                }
            }
            catch (Exception de)
            {
                WinFormsServ.Error("Ошибка удаления данных ", de);
            }

        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvList.Columns["Number"].Index)
            {
                e.Value = string.Format("{0}", e.RowIndex + 1);
            }
        }

        private void btnPrintSticker_Click(object sender, EventArgs e)
        {
            if (OlympVedId == null)
                return;

            if (Util.IsOwner() || Util.IsPasha() || Util.IsCryptoMain())
            {
                FileStream fileS = null;
                string savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Sticker.pdf";

                float fontsize = 8;

                try
                {
                    using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                    {
                        Document document = new Document(PageSize.A4, 50, 50, 50, 50);
                        document.SetMargins(18, 18, 36, 5);

                        using (fileS = new FileStream(savePath, FileMode.Create))
                        {
                            BaseFont bfTimes = BaseFont.CreateFont(string.Format(@"{0}\times.ttf", Util.dirTemplates), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                            iTextSharp.text.Font font = new iTextSharp.text.Font(bfTimes, 10);

                            PdfWriter pw = PdfWriter.GetInstance(document, fileS);
                            document.Open();

                            int cntCells = 2;
                            int.TryParse(tbCountCell.Text.Trim(), out cntCells);

                            string className;
                            int ClassNum = 0;

                            var ved = (from ev in context.OlympVed
                                       where ev.Id == OlympVedId
                                       select ev).FirstOrDefault();

                            className = ved.SchoolClass.Name;
                            if (className.Length > 80)
                                className = className.Substring(0, 36) + "..." + className.Substring(className.Length - 36);
                            ClassNum = ved.SchoolClass.IntVal;

                            var dsPersons =
                                (from PersInVed in context.PersonInOlympVed
                                 join Pers in context.Person on PersInVed.PersonId equals Pers.Id
                                 where PersInVed.OlympVedId == OlympVedId
                                 select new 
                                 {
                                     Pers.Id,
                                     Pers.Surname,
                                     Pers.Name,
                                     Pers.SecondName,
                                     PersInVed.CryptNumber,
                                 }).ToList()
                                 .OrderBy(x => x.Surname)
                                 .ThenBy(x => x.Name)
                                 .ThenBy(x => x.SecondName)
                                 .ThenBy(x => x.Id)
                                 .ToList();
                                
                            PdfPTable t = new PdfPTable(3);
                            float pgW = (PageSize.A4.Width - 36) / 3;
                            float[] headerwidths = { pgW, pgW, pgW };
                            t.SetWidths(headerwidths);
                            t.WidthPercentage = 100f;
                            t.SpacingBefore = 10f;
                            t.SpacingAfter = 10f;
                            t.DefaultCell.MinimumHeight = 120;

                            int cellsNum = ((dsPersons.Count) * (cntCells * 8)) + 1;
                            int ost = cellsNum % 3;

                            foreach (var drr in dsPersons)
                            {
                                string FIO = (drr.Surname + " " ?? "") + (drr.Name ?? "") + (" " + drr.SecondName ?? "");

                                Barcode128 barcode1 = new Barcode128();
                                barcode1.Code = ClassNum.ToString("D2") + "==" + drr.CryptNumber + "-";

                                if (string.IsNullOrEmpty(drr.CryptNumber))
                                {
                                    WinFormsServ.Error("У участника " + FIO + " не создан шифровальный номер! Разлочьте ведомость и закройте её заново");
                                    return;
                                }

                                PdfContentByte cb = pw.DirectContent;
                                iTextSharp.text.Image img1 = barcode1.CreateImageWithBarcode(cb, null, null);
                                img1.ScaleAbsolute(80f, 60f);

                                float[] hwh = { pgW };
                                
                                for (int task = 1; task <= 8; task++)
                                {
                                    int d = (task + 4) / 4;
                                    string text = FIO + "\n" + className + " (день " + d + "-й)";

                                    if (task % 4 == 1)
                                    {
                                        PdfPTable ptPl1 = new PdfPTable(1);
                                        ptPl1.SetWidthPercentage(hwh, PageSize.A4);

                                        PdfPCell clPlText = new PdfPCell(new Phrase(text, new iTextSharp.text.Font(bfTimes, fontsize)));
                                        clPlText.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
                                        clPlText.PaddingBottom = 2;
                                        clPlText.PaddingTop = 2;
                                        clPlText.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                        PdfPCell clPlBarc = new PdfPCell();
                                        clPlBarc.AddElement(img1);
                                        clPlBarc.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
                                        clPlBarc.PaddingTop = 1;
                                        clPlBarc.PaddingLeft = 40;
                                        clPlBarc.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                        ptPl1.AddCell(clPlText);
                                        ptPl1.AddCell(clPlBarc);

                                        PdfPCell pcell1 = new PdfPCell(ptPl1);
                                        pcell1.PaddingTop = 6;
                                        pcell1.PaddingBottom = 6;
                                        pcell1.PaddingLeft = 6;
                                        pcell1.PaddingRight = 6;
                                        pcell1.FixedHeight = 100;
                                        pcell1.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                        t.AddCell(pcell1);
                                    }

                                    Barcode128 barcode2 = new Barcode128();
                                    string brc_code = ClassNum.ToString("D2") + "==" + drr.CryptNumber + "-" + task.ToString();
                                    barcode2.Code = brc_code;

                                    iTextSharp.text.Image img2 = barcode2.CreateImageWithBarcode(cb, iTextSharp.text.Color.BLACK, iTextSharp.text.Color.WHITE);
                                    img2.ScaleAbsolute(80f, 60f);

                                    for (int i = 0; i < cntCells; i++)
                                    {
                                        PdfPTable ptPl2 = new PdfPTable(1);
                                        ptPl2.SetWidthPercentage(hwh, PageSize.A4);

                                        PdfPCell clPlText2 = new PdfPCell();
                                        clPlText2.AddElement(img2);
                                        clPlText2.PaddingLeft = 40;
                                        clPlText2.PaddingRight = 40;
                                        clPlText2.PaddingTop = 20;
                                        clPlText2.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                        //подписываем снизу текст баркода (если понадобится убрать, то уберём)
                                        PdfPCell clPlBarc2 = new PdfPCell(new Phrase(brc_code, new iTextSharp.text.Font(bfTimes, fontsize)));
                                        clPlBarc2.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
                                        clPlBarc2.PaddingTop = 1;
                                        clPlBarc2.Border = iTextSharp.text.Rectangle.NO_BORDER;

                                        ptPl2.AddCell(clPlText2);
                                        ptPl2.AddCell(clPlBarc2);

                                        PdfPCell pcell2 = new PdfPCell(ptPl2);
                                        pcell2.FixedHeight = 100;
                                        pcell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                        t.AddCell(pcell2);
                                    }
                                }
                            }

                            //заполняем остатки таблицы пустыми ячейками
                            for (int i = 0; i < 3 - ost; i++)
                            {
                                PdfPCell pc = new PdfPCell();
                                pc.Border = iTextSharp.text.Rectangle.NO_BORDER;
                                t.AddCell(pc);
                            }

                            if (t != null)
                                document.Add(t);

                            document.Close();

                            Process pr = new Process();
                            if (!Util.IsOwner())
                                pr.StartInfo.Verb = "Print";
                            pr.StartInfo.FileName = string.Format(savePath);
                            pr.Start();

                            pr.Close();
                        }
                    }
                }

                catch (Exception exc)
                {
                    WinFormsServ.Error(exc);
                }
                finally
                {
                    if (fileS != null)
                        fileS.Dispose();

                }
            }

            else
                WinFormsServ.Error("Невозможно создание наклеек, недостаточно прав");
        }

        private void btnDeleteFromVed_Click(object sender, EventArgs e)
        {
            if (OlympVedId == null)
                return;

            if (Util.IsPasha())
            {
                if (MessageBox.Show("Удалить person из ведомости?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                    {
                        foreach (DataGridViewRow dgvr in dgvList.SelectedRows)
                        {
                            Guid persId = new Guid(dgvr.Cells["Id"].Value.ToString());
                            try
                            {

                                //context.ExamsVedHistory_DeleteByPersonAndVedId(OlympVedId, persId);
                            }
                            catch (Exception ex)
                            {
                                WinFormsServ.Error("Ошибка удаления данных", ex);
                                goto Next;
                            }
                        Next: ;
                        }
                        UpdateDataGrid();
                    }
                }
            }
        }

        private void btnUnload_Click(object sender, EventArgs e)
        {
            if (OlympVedId == null)
                return;
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var Ved = context.OlympVed.Where(x => x.Id == OlympVedId).FirstOrDefault();
                bool isLocked = Ved.IsLocked;
                if (!isLocked)
                    return;

                if (Util.IsPasha())
                {
                    if (MessageBox.Show("Разлочить ведомость и удалить загруженные оценки?", "Удаление", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.RequiresNew))
                            {
                                var lstMarks = context.PersonInOlympVedMark.Where(x => x.PersonInOlympVed.OlympVedId == OlympVedId);

                                foreach (var Mrk in lstMarks)
                                {
                                    Mrk.Mark = null;
                                    Mrk.AppealMark = null;
                                }

                                Ved.IsLoad = false;
                                Ved.IsLocked = false;

                                context.SaveChanges();

                                transaction.Complete();
                            }

                            MessageBox.Show("Выполнено");
                        }
                        catch (Exception ex)
                        {
                            WinFormsServ.Error("Ошибка удаления данных", ex);
                        }
                    }
                }
            }
        }

        private void btnPrintProtocol_Click(object sender, EventArgs e)
        {
            Novacode.DocX doc;
            string saveFileName = Util.TempFolder + @"\TableVedReport.docx";

            try
            {
                File.Copy(Util.dirTemplates + @"\TableVedReport.docx", saveFileName, true);
                doc = Novacode.DocX.Load(saveFileName);

                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    var OlympVedInfo = context.OlympVed.Where(x => x.Id == OlympVedId).FirstOrDefault();
                    if (OlympVedInfo == null)
                    {
                        WinFormsServ.Error("Не найдена ведомость!");
                        return;
                    }

                    doc.ReplaceText("&&SchoolClass", OlympVedInfo.SchoolClass.Name);
                    Novacode.Table td = doc.Tables[0];

                    var lstResults =
                        (from RatingList in context.extRatingList
                         join Pers in context.Person on RatingList.PersonId equals Pers.Id
                         where RatingList.OlympVedId == OlympVedId
                         select new
                         {
                             RatingList.TotalMark,
                             RatingList.Place,
                             Pers.Surname,
                             Pers.Name,
                             Pers.SecondName,
                             Pers.SchoolName,
                             Pers.City,
                             Region = Pers.Region.Name,
                             RatingList.CryptNumber,
                             RatingList.OlympVedId,
                             RatingList.PersonId,
                             SchoolClass = Pers.SchoolClass.IntVal
                         })
                         .ToList()
                         .OrderBy(x => x.Place)
                         .ThenBy(x => x.Surname)
                         .ThenBy(x => x.Name)
                         .ThenBy(x => x.SecondName)
                         .Select(x => new
                         {
                             x.Surname,
                             x.Name,
                             x.SecondName,
                             x.SchoolName,
                             x.Region,
                             x.City,
                             x.CryptNumber,
                             x.TotalMark,
                             x.Place,
                             x.SchoolClass,
                             Results = context.PersonInOlympVedMark
                              .Where(z => z.PersonInOlympVed.PersonId == x.PersonId && z.PersonInOlympVed.OlympVedId == x.OlympVedId)
                              .Select(z => new { z.TaskNumber, z.Mark, z.AppealMark }).ToList()
                         })
                         .ToList();

                    var row = td.Rows[2];
                    for (int m = 0; m < lstResults.Count - 1; m++)
                        td.InsertRow(row);

                    Novacode.Formatting formatting = new Novacode.Formatting();
                    formatting.Size = 8d;

                    // печать списка
                    int i = 2;
                    foreach (var Res in lstResults)
                    {
                        td.Rows[i].Cells[0].Paragraphs[0].InsertText((i - 1).ToString(), false, formatting);
                        td.Rows[i].Cells[1].Paragraphs[0].InsertText(Res.Surname, false, formatting);
                        td.Rows[i].Cells[2].Paragraphs[0].InsertText(Res.Name, false, formatting);
                        td.Rows[i].Cells[3].Paragraphs[0].InsertText(Res.SecondName, false, formatting);
                        td.Rows[i].Cells[4].Paragraphs[0].InsertText(Res.SchoolClass.ToString(), false, formatting);
                        td.Rows[i].Cells[5].Paragraphs[0].InsertText(Res.SchoolName, false, formatting);
                        td.Rows[i].Cells[6].Paragraphs[0].InsertText(Res.City ?? "", false, formatting);
                        td.Rows[i].Cells[7].Paragraphs[0].InsertText(Res.CryptNumber ?? "", false, formatting);
                        td.Rows[i].Cells[8].Paragraphs[0].InsertText(Res.Results.Where(x => x.TaskNumber == 1).Select(x => x.AppealMark ?? (x.Mark ?? 0m)).DefaultIfEmpty(0m).First().ToString(), false, formatting);
                        td.Rows[i].Cells[9].Paragraphs[0].InsertText(Res.Results.Where(x => x.TaskNumber == 2).Select(x => x.AppealMark ?? (x.Mark ?? 0m)).DefaultIfEmpty(0m).First().ToString(), false, formatting);
                        td.Rows[i].Cells[10].Paragraphs[0].InsertText(Res.Results.Where(x => x.TaskNumber == 3).Select(x => x.AppealMark ?? (x.Mark ?? 0m)).DefaultIfEmpty(0m).First().ToString(), false, formatting);
                        td.Rows[i].Cells[11].Paragraphs[0].InsertText(Res.Results.Where(x => x.TaskNumber == 4).Select(x => x.AppealMark ?? (x.Mark ?? 0m)).DefaultIfEmpty(0m).First().ToString(), false, formatting);
                        td.Rows[i].Cells[12].Paragraphs[0].InsertText(Res.Results.Where(x => x.TaskNumber == 5).Select(x => x.AppealMark ?? (x.Mark ?? 0m)).DefaultIfEmpty(0m).First().ToString(), false, formatting);
                        td.Rows[i].Cells[13].Paragraphs[0].InsertText(Res.Results.Where(x => x.TaskNumber == 6).Select(x => x.AppealMark ?? (x.Mark ?? 0m)).DefaultIfEmpty(0m).First().ToString(), false, formatting);
                        td.Rows[i].Cells[14].Paragraphs[0].InsertText(Res.Results.Where(x => x.TaskNumber == 7).Select(x => x.AppealMark ?? (x.Mark ?? 0m)).DefaultIfEmpty(0m).First().ToString(), false, formatting);
                        td.Rows[i].Cells[15].Paragraphs[0].InsertText(Res.Results.Where(x => x.TaskNumber == 8).Select(x => x.AppealMark ?? (x.Mark ?? 0m)).DefaultIfEmpty(0m).First().ToString(), false, formatting);
                        td.Rows[i].Cells[16].Paragraphs[0].InsertText((Res.TotalMark ?? 0m).ToString(), false, formatting);
                        td.Rows[i].Cells[17].Paragraphs[0].InsertText(Res.Place.HasValue ? Res.Place.Value.ToString() : "", false, formatting);

                        i++;
                        
                    }
                }

                doc.Save();

                Process.Start(saveFileName);
            }
            catch (Exception exc)
            {
                WinFormsServ.Error("Ошибка вывода в Word: \n", exc);
            }
        }
    }
}
