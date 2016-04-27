using EducServLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlineOlympDesctop
{
    class PacketImporter
    {
        public static void ImportMarksFromExcel(string fileName, int ClassId)
        {
            DataTable tbl = OnlineOlympDesctop.Print.PrintClass.GetDataTableFromExcel2007_New(fileName, false);

            using (OlympVseross2016Entities context = new OlympVseross2016Entities())
            using (TransactionScope tran = new TransactionScope())
            {
                int rowNum = 0;
                try
                {
                    foreach (DataRow rw in tbl.Rows)
                    {
                        rowNum++;
                        int iEx1Val, iEx2Val, iEx3Val, iEx4Val, iEx5Val, iEx6Val, iEx7Val, iEx8Val;
                        string CryptNumber = rw[0].ToString();
                        string sEx1Val = rw[1].ToString();
                        string sEx2Val = rw[2].ToString();
                        string sEx3Val = rw[3].ToString();
                        string sEx4Val = rw[4].ToString();
                        string sEx5Val = rw[5].ToString();
                        string sEx6Val = rw[6].ToString();
                        string sEx7Val = rw[7].ToString();
                        string sEx8Val = rw[8].ToString();

                        int.TryParse(sEx1Val, out iEx1Val);
                        int.TryParse(sEx2Val, out iEx2Val);
                        int.TryParse(sEx3Val, out iEx3Val);
                        int.TryParse(sEx4Val, out iEx4Val);
                        int.TryParse(sEx5Val, out iEx5Val);
                        int.TryParse(sEx6Val, out iEx6Val);
                        int.TryParse(sEx7Val, out iEx7Val);
                        int.TryParse(sEx8Val, out iEx8Val);

                        var eMark1 = context.PersonInOlympVedMark
                            .Where(x => x.TaskNumber == 1 && x.PersonInOlympVed.CryptNumber == CryptNumber && x.PersonInOlympVed.OlympVed.ClassId == ClassId)
                            .FirstOrDefault();
                        eMark1.Mark = iEx1Val;

                        var eMark2 = context.PersonInOlympVedMark
                            .Where(x => x.TaskNumber == 2 && x.PersonInOlympVed.CryptNumber == CryptNumber && x.PersonInOlympVed.OlympVed.ClassId == ClassId)
                            .FirstOrDefault();
                        eMark2.Mark = iEx2Val;

                        var eMark3 = context.PersonInOlympVedMark
                            .Where(x => x.TaskNumber == 3 && x.PersonInOlympVed.CryptNumber == CryptNumber && x.PersonInOlympVed.OlympVed.ClassId == ClassId)
                            .FirstOrDefault();
                        eMark3.Mark = iEx3Val;

                        var eMark4 = context.PersonInOlympVedMark
                            .Where(x => x.TaskNumber == 4 && x.PersonInOlympVed.CryptNumber == CryptNumber && x.PersonInOlympVed.OlympVed.ClassId == ClassId)
                            .FirstOrDefault();
                        eMark4.Mark = iEx4Val;

                        var eMark5 = context.PersonInOlympVedMark
                            .Where(x => x.TaskNumber == 5 && x.PersonInOlympVed.CryptNumber == CryptNumber && x.PersonInOlympVed.OlympVed.ClassId == ClassId)
                            .FirstOrDefault();
                        eMark5.Mark = iEx5Val;

                        var eMark6 = context.PersonInOlympVedMark
                            .Where(x => x.TaskNumber == 6 && x.PersonInOlympVed.CryptNumber == CryptNumber && x.PersonInOlympVed.OlympVed.ClassId == ClassId)
                            .FirstOrDefault();
                        eMark6.Mark = iEx6Val;

                        var eMark7 = context.PersonInOlympVedMark
                            .Where(x => x.TaskNumber == 7 && x.PersonInOlympVed.CryptNumber == CryptNumber && x.PersonInOlympVed.OlympVed.ClassId == ClassId)
                            .FirstOrDefault();
                        eMark7.Mark = iEx7Val;

                        var eMark8 = context.PersonInOlympVedMark
                            .Where(x => x.TaskNumber == 8 && x.PersonInOlympVed.CryptNumber == CryptNumber && x.PersonInOlympVed.OlympVed.ClassId == ClassId)
                            .FirstOrDefault();
                        eMark8.Mark = iEx8Val;

                        context.SaveChanges();
                    }

                    tran.Complete();
                }
                catch (Exception ex)
                {
                    WinFormsServ.Error("RowNum=" + rowNum, ex);
                }
            }
        }
    }
}
