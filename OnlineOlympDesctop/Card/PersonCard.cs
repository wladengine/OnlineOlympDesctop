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
    public partial class PersonCard : Form
    {
        public UpdateHandler hdl;
        public string SaveText = "Сохранить";
        public string ChangeText = "Изменить";

        public Guid? PersonId;
        public Guid? UserId;
        public List<Guid> PersonIdList;
        public bool isModified;

        public PersonCard(UpdateHandler h)
        {
            InitializeComponent();
            PersonId = null;
            this.MdiParent = Util.MainForm;
            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            isModified = true;
            hdl = h;
            FillCard();
            SetAllFieldsEnabled(isModified);
        }

        public PersonCard(Guid id, List<Guid> lst, UpdateHandler h)
        {
            InitializeComponent();
            PersonId = id;
            PersonIdList = lst;
            this.MdiParent = Util.MainForm;
            isModified = false;
            hdl = h;
            FillCard();
            SetAllFieldsEnabled(isModified);
        }
        public void FillCard()
        {
            lblFIO.Text = "";
            lblCheckResult.Text = "";
            gbExample.Visible = false;

            if (PersonId == null)
            {
                if (tabControl1.TabPages.Contains(tabHide))
                    tabControl1.TabPages.Remove(tabHide);
            }
            else
            {
                if (!tabControl1.TabPages.Contains(tabHide))
                    tabControl1.TabPages.Add(tabHide);
            }
            ComboServ.FillCombo(cbCountry, HelpClass.GetComboListByTable("dbo.Country", " order by LevelOfUsing"), false, false);
            ComboServ.FillCombo(cbRegion, HelpClass.GetComboListByTable("dbo.Region"), false, false);
            ComboServ.FillCombo(cbDocType, HelpClass.GetComboListByTable("dbo.DocumentType"), false, false);
            ComboServ.FillCombo(cbArrivalPlace, HelpClass.GetComboListByTable("dbo.PlaceList"), false, false);
            ComboServ.FillCombo(cbDepaturePlace, HelpClass.GetComboListByTable("dbo.PlaceList"), false, false);

            if (PersonId != null)
            {
                using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
                {
                    var Person = context.Person.Where(x => x.Id == PersonId).FirstOrDefault();
                    if (Person == null)
                        return;

                    UserId = Person.UserId;
                    if (Person.IsHidden)
                    {
                        if (tabControl1.TabPages.Contains(tabHide))
                            tabControl1.TabPages.Remove(tabHide);
                    }
                    tbSurname.Text = Person.Surname;
                    tbName.Text = Person.Name;
                    tbSecondName.Text = Person.SecondName;

                    lblFIO.Text = ((Person.Surname + " ") ?? "") + ((Person.Name + " ") ?? "") + (Person.SecondName ?? "").Trim();

                    dtpBirthday.Value = Person.BirthDate.HasValue ? Person.BirthDate.Value : DateTime.Now;
                    tbPlaceOfBirth.Text = Person.PlaceOfBirth;

                    rbSex1.Checked = ((Person.SexId ?? 1) == 1);
                    rbSex2.Checked = ((Person.SexId ?? 1) == 2);

                    if (!Person.Country.IsRussia)
                        cbRegion.Visible = false;
                    else
                        cbRegion.Visible = true;

                    ComboServ.SetComboId(cbCountry, Person.NationalityId ?? 193);
                    ComboServ.SetComboId(cbRegion, Person.RegionId ?? 1);

                    tbEmail.Text = context.User.Where(x => x.Id == PersonId).Select(x => x.Email).FirstOrDefault();
                    ComboServ.SetComboId(cbDocType, Person.PassportTypeId ?? 1);

                    tbSeria.Text = Person.PassportSeries;
                    tbNumber.Text = Person.PassportNumber;
                    dtpDocumentDate.Value = Person.PassportDate.HasValue ? Person.PassportDate.Value : DateTime.Now;
                    tbAuthor.Text = Person.PassportAuthor;

                    tbPost.Text = Person.Post;
                    tbAddress.Text = Person.Address;
                    tbComments.Text = Person.AdditionalComments;

                    tbArrivalDate.Text = Person.ArrivalDate.HasValue ? (Person.ArrivalDate.Value.ToShortDateString() + " " + Person.ArrivalDate.Value.ToShortTimeString()) : "";
                    tbArrivalNumber.Text = Person.ArrivalNumber;
                    ComboServ.SetComboId(cbArrivalPlace, Person.ArrivalPlaceId ?? 1);

                    tbDepatureDate.Text = Person.DepatureDate.HasValue ? (Person.DepatureDate.Value.ToShortDateString() + " " + Person.DepatureDate.Value.ToShortTimeString()) : "";
                    tbDepatureNumber.Text = Person.DepatureNumber;
                    ComboServ.SetComboId(cbDepaturePlace, Person.DepaturePlaceId ?? 1);
                    FillPerson(context);
                    FillParticipants(context);
                    FillFiles(context);
                }
            }
            btnFileOpen.Enabled = (dgvFiles.Rows.Count) > 0;
        }
        private void FillParticipants()
        {
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
                FillParticipants(context);
        }
        private void FillParticipants(OnlineOlymp2016Entities context)
        {
            var Participants = (from x in context.Participant
                                where x.UserId == UserId
                                join cl in context.SchoolClass on x.ClassId equals cl.Id
                                select new
                                {
                                    x.Id,
                                    ФИО = ((x.Surname ?? "") + " " + (x.Name ?? "") + " " + (x.SecondName ?? "")).Trim(),
                                    Класс = cl.Name,
                                    HasFiles = (context.PersonFile.Where(p => p.ParticipantId == x.Id).Count() > 0),
                                    Файлы = (context.PersonFile.Where(p => p.ParticipantId == x.Id).Count() > 0) ? "да" : "нет",
                                }).OrderBy(x=>x.ФИО).ToList();
            dgvParticipant.DataSource = Participants;

            foreach (var s in new List<string>() { "Id", "HasFiles" })
                if (dgvParticipant.Columns.Contains(s))
                    dgvParticipant.Columns[s].Visible = false;
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
                         join part in context.Participant on x.ParticipantId equals part.Id into _p
                         from part in _p.DefaultIfEmpty()

                         where x.PersonId == PersonId
                         select new
                         {
                             x.Id,
                             Название = x.FileName,
                             Тип = ft.Name,
                             Дата = x.LoadDate,
                             Комментарий = x.Comment,
                             Участник = ((part.Surname + " ") ?? "") + ((part.Name + " ") ?? "") + (part.SecondName ?? "").Trim(),
                         }).ToList();

            dgvFiles.DataSource = files;
            foreach (var s in new List<string>() { "Id" })
                if (dgvFiles.Columns.Contains(s))
                    dgvFiles.Columns[s].Visible = false;
        }
        private void FillPerson(OnlineOlymp2016Entities context)
        {
            if (UserId.HasValue && PersonId.HasValue)
                {
                    var persons = (from x in context.Person
                                   where x.UserId == UserId
                                   && x.Id != PersonId
                                   select new
                                   {
                                       Id = x.Id,
                                       isMain = x.Id == x.UserId,
                                       ФИО = ((x.Surname ?? "") + " " + (x.Name ?? "") + " " + (x.SecondName ?? "")).Trim(),
                                       x.IsHidden
                                   }).OrderBy(x => x.ФИО).ToList();
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
        private void FillPerson()
        {
            FillPerson(new OnlineOlymp2016Entities());
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (timer1 != null)
            {
                timer1.Stop();
            }

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
            if (PersonId.HasValue && PersonIdList!=null && PersonIdList.Count>0)
            {
                int ind = PersonIdList.IndexOf(PersonId.Value);
                if (ind == 0)
                    ind = PersonIdList.Count - 1;
                else ind--;
                PersonId = PersonIdList[ind];
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
            if (PersonId.HasValue && PersonIdList != null && PersonIdList.Count > 0)
            {
                int ind = PersonIdList.IndexOf(PersonId.Value);
                if (ind == PersonIdList.Count-1)
                    ind = 0;
                else ind++;
                PersonId = PersonIdList[ind];
                FillCard();
            }
        }

        private void dgvParticipant_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PersonId.HasValue)
            {
                if (dgvParticipant.CurrentCell == null)
                    return;
                if (dgvParticipant.CurrentCell.RowIndex == -1)
                    return;

                Guid partId = Guid.Parse(dgvParticipant.CurrentRow.Cells["Id"].Value.ToString());
                List<Guid> lst = new List<Guid>();
                foreach (DataGridViewRow rw in dgvParticipant.Rows)
                    lst.Add(Guid.Parse(rw.Cells["Id"].Value.ToString()));

                new ParticipantCard(partId, PersonId, lst, hdl).Show();
            }
            else
                MessageBox.Show("");
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
                    foreach (TabPage x in tabControl1.TabPages)
                    {
                        if (x.Controls.Contains(c))
                        {
                            tabControl1.SelectedTab = x;
                            break;
                        }
                    }
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
                    Person Part = context.Person.Where(x => x.Id == PersonId.Value).FirstOrDefault();

                    bool isNew = false;
                    if (!PersonId.HasValue)
                    {
                        PersonId = Guid.NewGuid();
                        Part = new Person();
                        Part.Id = PersonId.Value;
                        Part.UserId = UserId ?? PersonId.Value;
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
                    Part.Post = tbPost.Text.Trim();

                    Part.PassportTypeId = ComboServ.GetComboIdInt(cbDocType);
                    Part.PassportSeries = tbSeria.Text.Trim();
                    Part.PassportNumber = tbNumber.Text.Trim();
                    Part.PassportDate = dtpDocumentDate.Value;
                    Part.PassportAuthor = tbAuthor.Text.Trim();

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
                        context.Person.Add(Part);

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

            btnNext.Enabled = btnPrev.Enabled = btnParticipantAdd.Enabled =
                btnPersonAdd.Enabled = btnPersonExistedAdd.Enabled = btnParticipantAddExisted.Enabled = btnFileLoad.Enabled = PersonId.HasValue;
            tabControl1.Enabled = lblFIO.Enabled = btnSave.Enabled = tbEmail.Enabled = true;
            
            btnFileOpen.Enabled = ((dgvFiles.Rows.Count) > 0) && PersonId.HasValue;
            dgvFiles.Enabled = true;
        }
        public void SetEnable(Control control, bool Change)
        {
            if (control == this.tabHide)
                return;
            if (control.Controls.Count > 0)
            {
                foreach (Control c in control.Controls)
                    SetEnable(c, Change);
            }
            else
                control.Enabled = Change;
        }

        private void btnParticipantAdd_Click(object sender, EventArgs e)
        {
            if (PersonId.HasValue)
            {
                List<Guid> lst = new List<Guid>();
                foreach (DataGridViewRow rw in dgvParticipant.Rows)
                    lst.Add(Guid.Parse(rw.Cells["Id"].Value.ToString()));
                new ParticipantCard(null, PersonId.Value, lst, hdl).Show();
            }
            else
                MessageBox.Show("Сохраните карточку Сопровождающего");
        }

        private void btnParticipantAddExisted_Click(object sender, EventArgs e)
        {
            if (PersonId.HasValue)
                new ParticipantAddToPerson(PersonId.Value, new UpdateHandler(FillParticipants)).Show();
        }

        private void btnFileLoad_Click(object sender, EventArgs e)
        {
            if (PersonId.HasValue)
            {
                new FileLoad(PersonId.Value, null, false, new UpdateHandler(FillFiles)).Show();
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
                    if (File == null)
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
                MessageBox.Show("Ошибочка");
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
            if (PersonId.HasValue)
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
            if (!int.TryParse(tbAnswer.Text, out Answer) || (Answer != A + B))
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
                    var Part = context.Person.Where(x => x.Id == PersonId).First();
                    Part.IsHidden = true;
                    context.SaveChanges();
                }
                PersonIdList.Remove(PersonId.Value);
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
                new PersonCard(gId, lst, new UpdateHandler(FillPerson)).Show();
            }
            catch
            {
                MessageBox.Show("Ошибочка, позвоните нам. 326-49-59");
            }
        }

        private void btnPersonAdd_Click(object sender, EventArgs e)
        {
            var card = new PersonCard(new UpdateHandler(FillPerson));
            card.UserId = UserId;
            card.Show();
        }

        private void btnPersonExistedAdd_Click(object sender, EventArgs e)
        {
            if (PersonId.HasValue)
                new PersonAddToPerson(PersonId.Value, new UpdateHandler(FillPerson)).Show();
        }


    }
}
