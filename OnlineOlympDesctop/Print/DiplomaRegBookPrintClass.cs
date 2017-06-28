using EducServLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOlympDesctop
{
    class DiplomaRegBookPrintClass
    {
        public static void PrintRegBook()
        {
            Novacode.DocX doc;
            string saveFileName = Util.TempFolder + @"\TableDiplomaRegBook.docx";

            try
            {
                File.Copy(Util.dirTemplates + @"\TableDiplomaRegBook.docx", saveFileName, true);
                doc = Novacode.DocX.Load(saveFileName);

                using (OlympVseross2016Entities context = new OlympVseross2016Entities())
                {
                    Novacode.Table td = doc.Tables[0];

                    var lstResults =
                        (from Dipl in context.OlympDiploma
                         join Ved in context.PersonInOlympVed on Dipl.OlympVedId equals Ved.OlympVedId
                         join Pers in context.Person on Dipl.PersonId equals Pers.Id
                         where Dipl.OlympYear == Util.CampaignYear
                         && Ved.PersonId == Pers.Id
                         && Dipl.DiplomaLevel.Id < 3
                         select new
                         {
                             Pers.Surname,
                             Pers.Name,
                             Pers.SecondName,
                             Pers.SchoolName,
                             Pers.City,
                             Region = Pers.Region.Name,
                             Ved.CryptNumber,
                             SchoolClass = Pers.SchoolClass.IntVal,
                             DocumentType = Pers.DocumentType.Name,
                             Pers.DocumentSeries,
                             Pers.DocumentNumber,
                             DiplomaLevel = Dipl.DiplomaLevel.Name,
                             Dipl.DiplomaRegNum,
                             Dipl.BlankNumber
                         })
                         .ToList()
                         .OrderBy(x => x.SchoolClass)
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
                             x.SchoolClass,
                             x.DocumentType,
                             DocumentInfo = (x.DocumentSeries + " " ?? "") + (x.DocumentNumber ?? ""),
                             x.DiplomaLevel,
                             x.BlankNumber,
                             x.DiplomaRegNum
                         })
                         .ToList();

                    var row = td.Rows[1];
                    for (int m = 0; m < lstResults.Count - 1; m++)
                        td.InsertRow(row);

                    Novacode.Formatting formatting = new Novacode.Formatting();
                    formatting.Size = 10d;
                    formatting.FontFamily = new System.Drawing.FontFamily("Times New Roman");

                    // печать списка
                    int i = 1;
                    foreach (var Res in lstResults)
                    {
                        string FIO = (Res.Surname + " " ?? "") + (Res.Name ?? "") + (" " + Res.SecondName ?? "");

                        td.Rows[i].Cells[0].Paragraphs[0].InsertText((i).ToString(), false, formatting);
                        td.Rows[i].Cells[1].Paragraphs[0].InsertText(FIO, false, formatting);
                        td.Rows[i].Cells[2].Paragraphs[0].InsertText(Res.DocumentType, false, formatting);
                        td.Rows[i].Cells[3].Paragraphs[0].InsertText(Res.DocumentInfo, false, formatting);
                        td.Rows[i].Cells[4].Paragraphs[0].InsertText(Res.SchoolName, false, formatting);
                        td.Rows[i].Cells[5].Paragraphs[0].InsertText(Res.City ?? "", false, formatting);
                        td.Rows[i].Cells[6].Paragraphs[0].InsertText(Res.SchoolClass.ToString(), false, formatting);
                        td.Rows[i].Cells[7].Paragraphs[0].InsertText(Res.DiplomaLevel ?? "", false, formatting);
                        td.Rows[i].Cells[8].Paragraphs[0].InsertText(Res.BlankNumber ?? "", false, formatting);
                        td.Rows[i].Cells[9].Paragraphs[0].InsertText(Res.DiplomaRegNum ?? "", false, formatting);

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
