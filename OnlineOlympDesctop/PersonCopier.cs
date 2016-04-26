using EducServLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOlympDesctop
{
    class PersonCopier
    {
        public static void UpdateInnerPersonsDatabase()
        {
            using (OnlineOlymp2016Entities online_context = new OnlineOlymp2016Entities())
            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var lstPersonsOnlineIds = online_context.Participant.Where(x => !x.IsHidden).Select(x => x.Id).ToList();
                var lstPersonsWorkBaseIds = context.Person.Select(x => x.Id).ToList();

                var lstDiff = lstPersonsOnlineIds.Except(lstPersonsWorkBaseIds).ToList();

                try
                {
                    foreach (Guid PersId in lstDiff)
                        CopyPersonFromOnlineToWorkBase(PersId, online_context, context);

                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    string msgErrors = ex.EntityValidationErrors
                        .Select(x => x.ValidationErrors.Select(z => z.ErrorMessage).Aggregate((m, tail) => m + ", " + tail))
                        .Aggregate((x, tail) => x + ";\n" + tail);

                    WinFormsServ.Error("Ошибки валидации при обновлении списков: " + msgErrors);
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error(ex);
                }
            }
        }

        private static void CopyPersonFromOnlineToWorkBase(Guid PersonId, OnlineOlymp2016Entities online_context, OlympVseross2016Entities context)
        {
            var OnlinePerson = online_context.Participant.Where(x => x.Id == PersonId).FirstOrDefault();

            Person_local Pers = new Person_local();
            Pers.Id = OnlinePerson.Id;
            Pers.Surname = OnlinePerson.Surname;
            Pers.Name = OnlinePerson.Name;
            Pers.SecondName = OnlinePerson.SecondName ?? "";
            Pers.SexId = OnlinePerson.SexId.Value;
            Pers.BirthDate = OnlinePerson.BirthDate.Value;
            Pers.CountryId = OnlinePerson.NationalityId.Value;
            Pers.RegionId = OnlinePerson.RegionId.Value;
            Pers.SchoolClassId = OnlinePerson.ClassId.Value;
            Pers.SchoolName = OnlinePerson.SchoolName ?? "";
            Pers.DocumentAuthor = OnlinePerson.DocumentAuthor ?? "";
            Pers.DocumentDate = OnlinePerson.DocumentDate ?? new DateTime(1970, 1, 1);
            Pers.DocumentNumber = OnlinePerson.DocumentNumber ?? "";
            Pers.DocumentSeries = OnlinePerson.DocumentSeries;
            Pers.DocumentTypeId = OnlinePerson.DocumentTypeId.Value;
            Pers.City = OnlinePerson.City ?? "";

            context.Person.Add(Pers);
        }
    }
}
