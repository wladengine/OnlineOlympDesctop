using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.DirectoryServices.AccountManagement;
using System.Data;

using System.Reflection;
using iTextSharp.text.pdf;
using EducServLib;

namespace OnlineOlympDesctop
{
    class PDFUtil
    {
        public static void PrintDiploma(bool forPrint, string savePath, Guid PersonId)
        {
            try
            {
                using (FileStream fs = new FileStream(savePath, FileMode.Create))
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] buffer = PrintDiplomaPDF(PersonId);
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush();
                    fs.Close();
                }

                System.Diagnostics.Process.Start(savePath);
            }
            catch (Exception ex)
            {
                WinFormsServ.Error(ex);
            }
        }

        public static byte[] PrintDiplomaPDF(Guid PersonId)
        {
            MemoryStream ms = new MemoryStream();
            string dotName = "VserossTemplate_2016.pdf";

            PdfReader pdfRd = GetAcrobatFileFromTemplate(Util.dirTemplates + "\\" + dotName);
            PdfStamper pdfStm = GetPdfStamper(ref pdfRd, ref ms, false);
            AcroFields acrFlds = pdfStm.AcroFields;

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var persData =
                    (from dipl in context.OlympDiploma
                     join pers in context.Person on dipl.PersonId equals pers.Id
                     where pers.Id == PersonId
                     select new
                     {
                         pers.Surname,
                         pers.Name,
                         pers.SecondName,
                         dipl.DiplomaRegNum,
                         dipl.DiplomaDate,
                         SchoolClass = pers.SchoolClass.PrintDiplomaName,
                         pers.SchoolName,
                     }).FirstOrDefault();

                //------check persData---------
                bool bChecked = true;
                if (!persData.DiplomaDate.HasValue)
                {
                    WinFormsServ.Error("Не указана дата выдачи диплома!");
                    bChecked = false;
                }
                if (string.IsNullOrEmpty(persData.DiplomaRegNum))
                {
                    WinFormsServ.Error("Не указана рег номер диплома!");
                    bChecked = false;
                }
                if (!bChecked)
                    throw new Exception("Не удалось распечатать диплом");
                //-----------------------------

                acrFlds.SetField("FIO", (persData.Surname + " " ?? "") + (persData.Name ?? "") + (" " + persData.SecondName ?? ""));
                acrFlds.SetField("SchoolClass", persData.SchoolClass);
                acrFlds.SetField("SchoolName", persData.SchoolName);

                acrFlds.SetField("RegNum", persData.DiplomaRegNum);
                acrFlds.SetField("DiplomaDate", persData.DiplomaDate.Value.ToString("d MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo("ru-RU")));

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();
            }

            return ms.ToArray();
        }
        public static byte[] PrintComplimentMentionPDF(Guid PersonId)
        {
            MemoryStream ms = new MemoryStream();
            string dotName = "VserossComplimentMention_2016.pdf";

            PdfReader pdfRd = GetAcrobatFileFromTemplate(Util.dirTemplates + "\\" + dotName);
            PdfStamper pdfStm = GetPdfStamper(ref pdfRd, ref ms, false);
            AcroFields acrFlds = pdfStm.AcroFields;

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            {
                var persData =
                    (from dipl in context.OlympDiploma
                     join pers in context.Person on dipl.PersonId equals pers.Id
                     select new
                     {
                         pers.Surname,
                         pers.Name,
                         pers.SecondName,
                         dipl.DiplomaRegNum,
                         dipl.DiplomaDate,
                         SchoolClass = pers.SchoolClass.PrintDiplomaName,
                         pers.SchoolName,
                     }).FirstOrDefault();

                //------check persData---------
                bool bChecked = true;
                if (!persData.DiplomaDate.HasValue)
                {
                    WinFormsServ.Error("Не указана дата выдачи диплома!");
                    bChecked = false;
                }
                if (string.IsNullOrEmpty(persData.DiplomaRegNum))
                {
                    WinFormsServ.Error("Не указана рег номер диплома!");
                    bChecked = false;
                }
                if (!bChecked)
                    throw new Exception("Не удалось распечатать диплом");
                //-----------------------------

                acrFlds.SetField("FIO", (persData.Surname + " " ?? "") + (persData.Name ?? "") + (" " + persData.SecondName ?? ""));
                acrFlds.SetField("SchoolClass", persData.SchoolClass);
                acrFlds.SetField("SchoolName", persData.SchoolName);

                acrFlds.SetField("RegNum", persData.DiplomaRegNum);
                acrFlds.SetField("DiplomaDate", persData.DiplomaDate.Value.ToString("dd MMMM yyyy", System.Globalization.CultureInfo.GetCultureInfo("ru-RU")));

                pdfStm.FormFlattening = true;
                pdfStm.Close();
                pdfRd.Close();
            }

            return ms.ToArray();
        }

        public static PdfReader GetAcrobatFileFromTemplate(string templateFile)
        {
            byte[] templateBytes;
            using (FileStream fs = new FileStream(templateFile, FileMode.Open, FileAccess.Read))
            {
                templateBytes = new byte[fs.Length];
                fs.Read(templateBytes, 0, templateBytes.Length);
            }

            return new PdfReader(templateBytes);
        }
        public static PdfStamper GetPdfStamper(ref PdfReader pdfRd, ref MemoryStream ms, bool setEncryption)
        {
            PdfStamper pdfStm = new PdfStamper(pdfRd, ms);
            if (setEncryption)
                pdfStm.SetEncryption(PdfWriter.STRENGTH128BITS, "", "", PdfWriter.ALLOW_SCREENREADERS | PdfWriter.ALLOW_PRINTING | PdfWriter.AllowPrinting);

            return pdfStm;
        }
    }
}