using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineOlympDesctop
{
    public partial class PersonAddToPerson : Form
    {
        Guid PersonId;
        Guid UserId;
        UpdateHandler hdl;
        public PersonAddToPerson(Guid Id, UpdateHandler h)
        {
            InitializeComponent();
            PersonId = Id;
            hdl = h;
            this.MdiParent = Util.MainForm;
            FillCard();
        }
        private void FillCard()
        {
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                var Person = (from x in context.Person
                              where x.Id == PersonId
                              select x).FirstOrDefault();

                if (Person == null)
                {
                    MessageBox.Show("Что-то не так");
                    return;
                }

                UserId = Person.UserId;

                lblFIO.Text = ((Person.Surname ?? "") + " " + (Person.Name ?? "") + " " + (Person.SecondName ?? "")).Trim();
                if (Person.NationalityId.HasValue)
                    lblCountryRegion.Text = Person.Country.Name + (Person.Country.IsRussia && Person.RegionId.HasValue? " " +  context.Region.Where(x=>x.Id == Person.RegionId).Select(x=>x.Name).First(): "");

                var lst = (from x in context.Person
                           where x.UserId != UserId
                           select new
                           {
                               Id = x.Id,
                               Name = ((x.Surname ?? "") + " " + (x.Name ?? "") + " " + (x.SecondName ?? "")).Trim(),
                               Nationality = x.Country.Name,
                               Region = x.Country.IsRussia ? context.Region.Where(r => r.Id == x.RegionId).Select(r => r.Name).FirstOrDefault() : "",
                               ParticipantCnt = context.Participant.Where(p=>p.UserId == x.UserId).Count(),
                               Persons = context.Person.Where(p=>p.UserId == x.UserId).Count(),
                           }).ToList().Select(x => new KeyValuePair<string, string>
                               (x.Id.ToString(),
                               x.Name + " (" + x.ParticipantCnt + " уч., "+ x.Persons+" сопр.)," + x.Nationality + ", " + x.Region)).ToList();
                
                ComboServ.FillCombo(cbParticipant, lst, false, false);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Guid? PartId = ComboServ.GetComboIdGuid(cbParticipant);
            if (!PartId.HasValue)
                return;

            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                var Pers = (from x in context.Person
                            where x.Id == PartId
                            select x).FirstOrDefault();

                var Persons = (from x in context.Person
                               where x.UserId == Pers.UserId
                               select x).ToList();

                var Parts = (from x in context.Participant
                             where x.UserId == Pers.UserId
                             select x).ToList();

                foreach (var part in Parts)
                {
                    var files = (from x in context.PersonFile
                                 where x.ParticipantId == part.Id
                                 select x).ToList();
                    foreach (var file in files)
                        file.PersonId = UserId;
                    
                    part.UserId = UserId;
                }
                foreach (var pers in Persons)
                    pers.UserId = UserId;

                context.SaveChanges();
                if (hdl != null)
                    hdl();
                this.Close();
            }
        }

    }
}
