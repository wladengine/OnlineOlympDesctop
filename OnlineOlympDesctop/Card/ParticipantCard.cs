using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OnlineOlympDesctop
{
    public partial class ParticipantCard : Form
    {
        public UpdateHandler hdl;
        public string SaveText = "Сохранить";
        public string ChangeText = "Изменить";

        public Guid? ParticipantId;
        public Guid? PersonId;
        public List<Guid> ParticipantIdList;
        bool isModified; 

        public ParticipantCard(UpdateHandler h)
        {
            InitializeComponent();
            ParticipantId = null;
            hdl = h;
            PersonId = null;
            ParticipantIdList = null;
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            this.MdiParent = Util.MainForm;
            FillCard();
            isModified = true;
            SetAllFieldsEnabled(isModified);
        }

        public ParticipantCard(Guid? id, Guid? persId, List<Guid> lst, UpdateHandler h)
        {
            InitializeComponent();
            ParticipantId = id;
            hdl = h;
            PersonId = persId;
            ParticipantIdList = lst;
            this.MdiParent = Util.MainForm;
            FillCard();
            isModified = !ParticipantId.HasValue;
            SetAllFieldsEnabled(isModified);
        }
        public void FillCard()
        {
            lblFIO.Text = "";
            lblCheckResult.Text = "";
            gbExample.Visible = false;

            if (ParticipantId == null)
            {
                if (tabControl1.TabPages.Contains(tabHide))
                    tabControl1.TabPages.Remove(tabHide);
            }
            else
            {
                if (!tabControl1.TabPages.Contains(tabHide))
                    tabControl1.TabPages.Add(tabHide);
            }
            ComboServ.FillCombo(cbCountry, HelpClass.GetComboListByTable("dbo.Country", " order by LevelOfUsing desc"), false, false);
            ComboServ.FillCombo(cbRegion, HelpClass.GetComboListByTable("dbo.Region"), false, false);
            ComboServ.FillCombo(cbSchoolClass, HelpClass.GetComboListByTable("dbo.SchoolClass", " order by Id"), false, false);
            ComboServ.FillCombo(cbDocType, HelpClass.GetComboListByTable("dbo.DocumentType", " order by Id"), false, false);
            ComboServ.FillCombo(cbArrivalPlace, HelpClass.GetComboListByTable("dbo.PlaceList", " order by Id"), false, false);
            ComboServ.FillCombo(cbDepaturePlace, HelpClass.GetComboListByTable("dbo.PlaceList", " order by Id"), false, false);
            
            if (ParticipantId != null)
            {
                using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
                {
                    var Person = context.Participant.Where(x => x.Id == ParticipantId).FirstOrDefault();

                    if (Person == null)
                        return;

                    PersonId = Person.UserId;
                    if (Person.IsHidden)
                    {
                        if (tabControl1.TabPages.Contains(tabHide))
                            tabControl1.TabPages.Remove(tabHide);
                    }
                    
                    tbSurname.Text = Person.Surname;
                    tbName.Text = Person.Name;
                    tbSecondName.Text = Person.SecondName;

                    lblFIO.Text =  ((Person.Surname + " ") ?? "") + ((Person.Name + " ") ?? "") + (Person.SecondName ?? "").Trim();
                   
                    dtpBirthday.Value = Person.BirthDate.HasValue ? Person.BirthDate.Value : DateTime.Now;
                    tbPlaceOfBirth.Text = Person.PlaceOfBirth;

                    rbSex1.Checked = ((Person.SexId ?? 1) == 1);
                    rbSex2.Checked = ((Person.SexId ?? 1) == 2);

                    if (!Person.Country.IsRussia)
                        lblRegion.Visible = cbRegion.Visible = false;
                    else
                       lblRegion.Visible = cbRegion.Visible = true;

                    ComboServ.SetComboId(cbCountry, Person.NationalityId ?? 193);
                    ComboServ.SetComboId(cbRegion, Person.RegionId ?? 1);

                    tbCity.Text = Person.City;
                    tbSchoolName.Text = Person.SchoolName;
                    ComboServ.SetComboId(cbSchoolClass, Person.ClassId ?? 1);
                    ComboServ.SetComboId(cbDocType, Person.DocumentTypeId ?? 1);

                    tbSeria.Text = Person.DocumentSeries;
                    tbNumber.Text = Person.DocumentNumber;
                    dtpDocumentDate.Value = Person.DocumentDate.HasValue ? Person.DocumentDate.Value : DateTime.Now;
                    tbAuthor.Text = Person.DocumentAuthor;

                    tbAddress.Text = Person.Address;
                    tbComments.Text = Person.AdditionalComments;

                    tbArrivalDate.Text = Person.ArrivalDate.HasValue ? (Person.ArrivalDate.Value.ToShortDateString() + " " + Person.ArrivalDate.Value.ToShortTimeString()) : "";
                    tbArrivalNumber.Text = Person.ArrivalNumber;
                    ComboServ.SetComboId(cbArrivalPlace, Person.ArrivalPlaceId ?? 1);

                    tbDepatureDate.Text = Person.DepatureDate.HasValue ? (Person.DepatureDate.Value.ToShortDateString() + " " + Person.DepatureDate.Value.ToShortTimeString()) : "";
                    tbDepatureNumber.Text = Person.DepatureNumber;
                    ComboServ.SetComboId(cbDepaturePlace, Person.DepaturePlaceId ?? 1);

                    FillFiles(context);
                    FillPersons(context);
                }
            }
        }
        public void FillPersons(OnlineOlymp2016Entities context)
        {
            if (PersonId.HasValue)
            {
                var persons = (from x in context.Person
                               where x.UserId == PersonId
                               select new
                               {
                                   Id = x.Id,
                                   isMain = x.Id == x.UserId,
                                   ФИО = ((x.Surname ?? "") + " " + (x.Name ?? "") + " " + (x.SecondName ?? "")).Trim(),
                                   x.IsHidden
                               }).ToList();
                dgvPersons.DataSource = persons;
                foreach (var s in new List<string>() { "Id", "isMain", "isHidden" })
                    if (dgvPersons.Columns.Contains(s))
                        dgvPersons.Columns[s].Visible = false;

                foreach (DataGridViewRow rw in dgvPersons.Rows)
                {
                    if (rw.Cells["isHidden"].Value.ToString() == "1" || rw.Cells["isHidden"].Value.ToString().ToLower() == "true")
                        foreach (DataGridViewCell cl in rw.Cells)
                            cl.Style.BackColor = Color.LightGray;
                    if (rw.Cells["isMain"].Value.ToString() == "1" || rw.Cells["isMain"].Value.ToString().ToLower() == "true")
                        foreach (DataGridViewCell cl in rw.Cells)
                            cl.Style.BackColor = Color.LightGreen;
                }
            }
        }
        public void FillPersons()
        {
            FillPersons(new OnlineOlymp2016Entities());
            
        }
        private void FillFiles()
        {
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
                FillFiles(context);
        }
        private void FillFiles(OnlineOlymp2016Entities context)
         {
             var files = (from x in context.PersonFile
                          join ft in context.FileType on x.FileTypeId equals ft.Id

                          where x.ParticipantId == ParticipantId
                          select new
                          {
                              x.Id,
                              Название = x.FileName,
                              Тип = ft.Name,
                              Дата = x.LoadDate,
                              Комментарий = x.Comment,
                          }).ToList();

             dgvFiles.DataSource = files;
             foreach (var s in new List<string>() { "Id" })
                 if (dgvFiles.Columns.Contains(s))
                     dgvFiles.Columns[s].Visible = false;
         }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (timer1 != null)
                timer1.Stop();
            if (isModified)
            {
                DialogResult res = MessageBox.Show("Сохранить изменения?", "", MessageBoxButtons.YesNoCancel);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    Save();
                }
                else if (res == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }
            if (ParticipantId.HasValue && ParticipantIdList!=null && ParticipantIdList.Count > 0)
            {
                int ind = ParticipantIdList.IndexOf(ParticipantId.Value);
                if (ind == 0)
                    ind = ParticipantIdList.Count - 1;
                else ind--;
                ParticipantId = ParticipantIdList[ind];
                FillCard();
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (timer1 != null)
                timer1.Stop();

            if (isModified)
            {
                DialogResult res = MessageBox.Show("Сохранить изменения?", "", MessageBoxButtons.YesNoCancel);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    Save();
                }
                else if (res == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }
            if (ParticipantId.HasValue && ParticipantIdList != null && ParticipantIdList.Count > 0)
            {
                int ind = ParticipantIdList.IndexOf(ParticipantId.Value);
                if (ind == ParticipantIdList.Count-1)
                    ind = 0;
                else ind++;
                ParticipantId = ParticipantIdList[ind];
                FillCard();
            }
        }
        private void btnOpenPerson_Click(object sender, EventArgs e)
        {
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                if (!PersonId.HasValue)
                {
                    if (ParticipantId.HasValue)
                    {
                        Guid? Person = (from x in context.Participant
                                        where x.Id == ParticipantId
                                        select x.UserId).FirstOrDefault();

                        if (!Person.HasValue)
                        {
                            MessageBox.Show("Участник олимпиады был создан без привязки к Сопровождающему. || Функция привязки доступна из Карточки сопровождающего");
                            return;
                        }
                        List<Guid> lst = (from x in context.Person
                                          where x.UserId == Person.Value
                                          select x.Id).ToList();
                        new PersonCard(Person.Value, lst, hdl).Show();
                    }
                }
                else
                {
                    List<Guid> lst = (from x in context.Person
                                      where x.UserId == PersonId.Value
                                      select x.Id).ToList();
                    new PersonCard(PersonId.Value, lst, hdl).Show();
                }
            }
        }
        private void dgvFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFiles.CurrentCell == null)
                return;
            if (dgvFiles.CurrentRow.Index < 0)
                return;
            try
            {
                Guid FileId = Guid.Parse(dgvFiles.CurrentRow.Cells["Id"].Value.ToString());

                using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
                {
                    var File = (from x in context.PersonFile
                                    where x.Id == FileId
                                    select x).FirstOrDefault();
                    if (File== null)
                        return;

                if (File.FileData == null)
                    return;

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = Util.TempFolder;
                saveFileDialog1.Filter = "all files|*.*";
                saveFileDialog1.Title = "Сохранить как...";
                saveFileDialog1.FileName = File.FileName;
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    string pathNew = saveFileDialog1.FileName;
                    try
                    {
                        using (FileStream fsNew = new FileStream(pathNew,
                               FileMode.Create, FileAccess.Write))
                        {
                            fsNew.Write(File.FileData, 0, File.FileData.Length);
                        }
                    }
                    catch (FileNotFoundException ioEx)
                    {
                        Console.WriteLine(ioEx.Message);
                    }
                    catch (Exception xc)
                    {
                        MessageBox.Show("[Возможное решение: сохраните как новый файл]\n\nОшибка во время сохранения:\n" + (xc.InnerException ?? xc), "Ошибка");
                    }
                }
                }
            }
            catch
            {
                MessageBox.Show("Ошибочка, позвоните нам. 326-49-59");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        } 
        private bool Check()
        {
            List<Control> lst = new List<Control>() { tbSurname, tbName };
            foreach (Control c in lst)
                if (String.IsNullOrEmpty(c.Text))
                {
                    errorProvider1.SetError(c, "Введите значение");
                    return false;
                }
                else
                    errorProvider1.Clear();

            return true;
        }
        public void Save()
        {
           
            if (isModified)
            {
                if (!Check())
                {
                    return;
                }

                using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
                {
                    Participant Part = context.Participant.Where(x => x.Id == ParticipantId.Value).FirstOrDefault();

                    bool isNew = false;
                    if (!ParticipantId.HasValue)
                    {
                        ParticipantId = Guid.NewGuid();
                        Part = new Participant();
                        Part.Id = ParticipantId.Value;
                        Part.UserId = PersonId;
                        isNew = true;
                    }
                    Part.Surname = tbSurname.Text.Trim();
                    Part.Name = tbName.Text.Trim();
                    Part.SecondName = tbSecondName.Text.Trim();

                    Part.SexId = rbSex1.Checked ? (int?)1 : (rbSex2.Checked ? (int?)2 : null);

                    Part.BirthDate = dtpBirthday.Value;
                    Part.PlaceOfBirth = tbPlaceOfBirth.Text.Trim();

                    Part.NationalityId = ComboServ.GetComboIdInt(cbCountry);
                    Part.RegionId = ComboServ.GetComboIdInt(cbRegion);

                    Part.City = tbCity.Text.Trim();
                    Part.ClassId = ComboServ.GetComboIdInt(cbSchoolClass);
                    Part.SchoolName = tbSchoolName.Text.Trim();

                    Part.DocumentTypeId = ComboServ.GetComboIdInt(cbDocType);
                    Part.DocumentSeries = tbSeria.Text.Trim();
                    Part.DocumentNumber = tbNumber.Text.Trim();
                    Part.DocumentDate = dtpDocumentDate.Value;
                    Part.DocumentAuthor = tbAuthor.Text.Trim();

                    Part.Address = tbAddress.Text.Trim();
                    Part.AdditionalComments = tbComments.Text.Trim();

                    Part.ArrivalNumber = tbArrivalNumber.Text.Trim();
                    Part.DepatureNumber = tbDepatureNumber.Text.Trim();

                    Part.ArrivalPlaceId = ComboServ.GetComboIdInt(cbArrivalPlace);
                    Part.DepaturePlaceId = ComboServ.GetComboIdInt(cbDepaturePlace);

                    DateTime dt;
                    if (DateTime.TryParse(tbArrivalDate.Text.Trim(), out dt))
                        Part.ArrivalDate = dt;

                    if (DateTime.TryParse(tbDepatureDate.Text.Trim(), out dt))
                        Part.DepatureDate = dt;

                    if (isNew)
                        context.Participant.Add(Part);
                    context.SaveChanges();
                }
            }
            isModified = !isModified;
            SetAllFieldsEnabled(isModified);
            if (hdl != null)
                hdl();
        }
        public void SetAllFieldsEnabled(bool Change)
        {
            btnSave.Text = isModified ? SaveText : ChangeText;
            foreach (Control control in this.Controls)
                SetEnable(control, Change);

            btnNext.Enabled = btnPrev.Enabled = btnFileLoad.Enabled = ParticipantId.HasValue;
            btnOpenPerson.Enabled = tabControl1.Enabled = true;
            lblFIO.Enabled = true;
            btnSave.Enabled = true;

            if (ParticipantId == null && PersonId == null)
                btnOpenPerson.Enabled = false;
            lblCopyArrival.Enabled = lblCopyDepature.Enabled = PersonId.HasValue && isModified;
            btnFileOpen.Enabled = (dgvFiles.Rows.Count > 0) && ParticipantId.HasValue;
            dgvFiles.Enabled = true;
        }
        public void SetEnable(Control control, bool Change)
        {
            if (control == this.tabHide)
                return;
            if (control.Controls.Count >0)
            {
                foreach (Control c in control.Controls)
                    SetEnable(c, Change);
            }
            else
                control.Enabled = Change;
        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRegion.Visible = cbRegion.Visible = (ComboServ.GetComboIdInt(cbCountry) == Util.CountryRussiaId);
        }

        private void lblCopyArrival_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                if (PersonId.HasValue && isModified)
                {
                    var Person = (from x in context.Person
                                  where x.Id == PersonId.Value
                                  select new
                                  {
                                      x.ArrivalDate,
                                      x.ArrivalNumber,
                                      x.ArrivalPlaceId
                                  }).First();
                    tbArrivalDate.Text = Person.ArrivalDate.HasValue ?
                        (Person.ArrivalDate.Value.ToShortDateString() + " " + Person.ArrivalDate.Value.ToShortTimeString())
                        : "";
                    tbArrivalNumber.Text = Person.ArrivalNumber;
                    ComboServ.SetComboId(cbArrivalPlace, Person.ArrivalPlaceId ?? 1);
                }
            }
        }

        private void lblCopyDepature_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                if (PersonId.HasValue && isModified)
                {
                    var Person = (from x in context.Person
                                  where x.Id == PersonId.Value
                                  select new
                                  {
                                      x.DepatureDate,
                                      x.DepatureNumber,
                                      x.DepaturePlaceId
                                  }).First();
                    tbDepatureDate.Text = Person.DepatureDate.HasValue ?
                        (Person.DepatureDate.Value.ToShortDateString() + " " + Person.DepatureDate.Value.ToShortTimeString())
                        : "";
                    tbDepatureNumber.Text = Person.DepatureNumber;
                    ComboServ.SetComboId(cbDepaturePlace, Person.DepaturePlaceId ?? 1);
                }
            }
        }

        private void btnFileLoad_Click(object sender, EventArgs e)
        {
            if (ParticipantId.HasValue)
            {
                new FileLoad(PersonId, ParticipantId.Value, true, new UpdateHandler(FillFiles)).Show();
            }
        }

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            List<Guid> Idlst = new List<Guid>();
            foreach (DataGridViewRow rw in dgvFiles.SelectedRows)
            {
                Guid FileId = Guid.Parse(rw.Cells["Id"].Value.ToString());
                Idlst.Add(FileId);
            }
            if (Idlst.Count == 0)
                return;

            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                var lstFiles = (from x in context.PersonFile
                                where Idlst.Contains(x.Id)
                                select new
                                {
                                    x.Id,
                                    x.FileName,
                                    x.FileData
                                }).ToList();
                string NewOrganizationFolder = Util.TempFolder + lblFIO.Text;

                if (lstFiles.Count > 0)
                {
                    if (!Directory.Exists(NewOrganizationFolder))
                        Directory.CreateDirectory(NewOrganizationFolder);
                }
                else return;
                bool FileSaved = false;
                foreach (var file in lstFiles)
                {
                    if (file.FileData == null)
                        return;

                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = NewOrganizationFolder + @"\";
                    saveFileDialog1.FileName = file.FileName;

                    string filename = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
                    string extenion = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                    string FullName = file.FileName;
                    int ind = 1;
                    while (File.Exists(NewOrganizationFolder + @"\" + FullName))
                    {
                        FullName = filename + "_" + ind.ToString() + extenion;
                        ind++;
                    }


                    // If the file name is not an empty string open it for saving.
                    if (saveFileDialog1.FileName != "")
                    {
                        try
                        {
                            using (FileStream fsNew = new FileStream(NewOrganizationFolder + @"\" + FullName,
                                   FileMode.Create, FileAccess.Write))
                            {
                                fsNew.Write(file.FileData, 0, file.FileData.Length);
                                fsNew.Flush();
                                fsNew.Close();
                            }
                            FileSaved = true;
                        }
                        catch (FileNotFoundException ioEx)
                        {
                            Console.WriteLine(ioEx.Message);
                        }
                        catch (Exception xc)
                        {
                            MessageBox.Show("[Возможное решение: сохраните как новый файл]\n\nОшибка во время сохранения:\n" + (xc.InnerException ?? xc), "Ошибка");
                        }
                    }
                    else
                    {

                    }

                }
                if (FileSaved)
                    System.Diagnostics.Process.Start(NewOrganizationFolder + @"\");
            }

        }

        #region Hide

        int A;
        int B;
        Timer timer1;
        string sCheckResult = "Ответ верный. Карточка будет скрыта. \nКарточка закроется через {0} секунд{1}.";
       
        private void btnHide_Click(object sender, EventArgs e)
        {
            if (ParticipantId.HasValue)
            {
                gbExample.Visible = true;
                Random rand = new Random();
                A = rand.Next(100);
                B = rand.Next(100);
                lblExample.Text = A.ToString() + " + " + B.ToString() + " = ";
                tbAnswer.Text = "";
            }
        }
        private void btnAnswerSend_Click(object sender, EventArgs e)
        {
            int Answer;
            Random rand = new Random();
            if (!int.TryParse(tbAnswer.Text, out Answer) || (Answer != A+B))
            {
                string res;
                switch (rand.Next(5))
                {
                    case 0: { res = "Попробуйте еще раз."; break; }
                    case 1: { res = "Ответ не верный."; break; }
                    case 2: { res = "Ты пытался :)"; break; }
                    case 3: { res = "Не в этот раз"; break; }
                    case 4: { res = "Вы точно не робот?"; break; }
                    default: { res = "Ответ не корректный."; break; }
                }

                lblCheckResult.Text = res;
                lblCheckResult.ForeColor = Color.Red;
                return;
            }
            else
            {
                lblCheckResult.Text = String.Format(sCheckResult, TimerTicks, (TimerTicks == 5 ? "" : ((TimerTicks == 1) ? "у" : "ы")));
                lblCheckResult.ForeColor = Color.Green;
                using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
                {
                    var Part = context.Participant.Where(x => x.Id == ParticipantId).First();
                    Part.IsHidden = true;
                    context.SaveChanges();
                }
                ParticipantIdList.Remove(ParticipantId.Value);
                timer1 = new Timer();
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Interval = 1 * 1000;
                timer1.Start();
                
                return;
            }
        }
        int TimerTicks = 5;
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerTicks--;
            lblCheckResult.Text = String.Format(sCheckResult, TimerTicks, (TimerTicks == 5 ? "" : ((TimerTicks == 1) ? "у" : "ы")));
            if (TimerTicks == 0)
            {
                if (hdl != null)
                    hdl();
                this.Close();
            }
        }
        #endregion

        private void dgvPersons_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPersons.CurrentCell == null)
                return;
            if (dgvPersons.CurrentRow.Index < 0)
                return;
            try
            {
                Guid gId = Guid.Parse(dgvPersons.CurrentRow.Cells["Id"].Value.ToString());
                List<Guid> lst = new List<Guid>();
                foreach (DataGridViewRow rw in dgvPersons.Rows)
                {
                    lst.Add(Guid.Parse(dgvPersons.CurrentRow.Cells["Id"].Value.ToString()));
                }
                new PersonCard(gId, lst, new UpdateHandler(FillPersons)).Show();
            }
            catch
            {
                MessageBox.Show("Ошибочка, позвоните нам");
            }
        }

    }
}
