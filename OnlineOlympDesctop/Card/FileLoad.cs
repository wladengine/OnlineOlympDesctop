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
    public partial class FileLoad : Form
    {
        public Guid? ParticipantId;
        public Guid? PersonId;
        UpdateHandler hdl;

        public FileLoad(Guid? Id, Guid? PartId, bool isForParticipant, UpdateHandler h)
        {
            InitializeComponent();
            ParticipantId = PartId;
            PersonId = Id;
            hdl = h;
            ComboServ.FillCombo(cbFileType, HelpClass.GetComboListByTable("dbo.FileType", isForParticipant ? " where IsForParticipant=1" : " where IsForParticipant = 0"), false, false);
        }

        byte[] fbyte;
        string filename;
        int FileTypeId;
        int fileSize;
        string fileext;

        private void btnFileOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "All files|*.*";
                if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                filename = ofd.FileName.Substring(ofd.FileName.LastIndexOf('\\') + 1);
                tbName.Text = filename;

                fileext = filename.Contains(".") ? (filename.Substring(filename.IndexOf("."))) : "";

                FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
                fbyte = new byte[fs.Length];
                fs.Read(fbyte, 0, System.Convert.ToInt32(fs.Length));

                FileTypeId = (int)ComboServ.GetComboIdInt(cbFileType);

                fileSize = Convert.ToInt32(fbyte.Length);
                tbSize.Text = (fileSize >= 1024)
                    ? ((fileSize >= 1024 * 1024) ? ((fileSize / 1024 / 1024).ToString() + " Мб") : ((fileSize / 1024).ToString() + " Кб"))
                    : (fileSize.ToString() + " Б");
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
                {
                    context.PersonFile.Add(new PersonFile()
                    {
                        Id = Guid.NewGuid(),
                        FileTypeId = FileTypeId,
                        FileData = fbyte,
                        FileExtention = fileext,
                        FileName = filename,
                        LoadDate = DateTime.Now,
                        ParticipantId = ParticipantId,
                        PersonId = PersonId,
                        FailReason = "",
                        Comment = tbComment.Text,
                        FileSize = fileSize,
                        MimeType = Util.GetMimeFromExtention(fileext),
                    });
                    context.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
            if (hdl != null)
                hdl();
            this.Close();
        }

        private void cbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFileType.SelectedItem != null)
                tbType.Text = ((KeyValuePair<string, string>)cbFileType.SelectedItem).Value;
            else tbType.Text = "";
        }
        
    }
}
