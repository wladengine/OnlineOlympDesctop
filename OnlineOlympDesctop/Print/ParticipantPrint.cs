using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using OfficeOpenXml;

namespace OnlineOlympDesctop
{
    public partial class ParticipantPrint : Form
    {
        public ParticipantPrint()
        {
            InitializeComponent();
            this.MdiParent = Util.MainForm;
            Init();
            lblColumns.Text = "";
            radioButton1.Checked = true;
        }

        #region Fields
        int PrintListId;
        List<KeyValuePair<string, string>> ColumnsList;
        List<KeyValuePair<string, string>> OrderList;
        List<KeyValuePair<string, bool>> ColumnsSelectedIndices = new List<KeyValuePair<string, bool>>();
        static int offset = 15; //чтоб имя перетаскиваемого элемента рядом с мышкой было 
        Stopwatch sw = new Stopwatch(); //чтобы не реагировал на обычный клик мышкой
        bool moving = false;
        int ColumnIndex = -1;
        Point columns_startloc;
        Point order_startloc;
        List<KeyValuePair<string, bool>> OrderSelectedIndices = new List<KeyValuePair<string, bool>>();
        int OrderIndex = -1;
        #endregion
        #region MainHandlers
        private void lbColumns_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < ColumnsSelectedIndices.Count; i++)
                ColumnsSelectedIndices[i] = new KeyValuePair<string, bool>(ColumnsSelectedIndices[i].Key, false);

            foreach (KeyValuePair<string, string> item in lbColumns.SelectedItems)
            {
                var ss = ColumnsSelectedIndices.Where(x => x.Key == item.Key).Select(x => x).ToList();
                if (ss.Count() == 0)
                    ColumnsSelectedIndices.Add(new KeyValuePair<string, bool>(item.Key, true));
                else
                    ColumnsSelectedIndices[ColumnsSelectedIndices.IndexOf(ss.First())] = new KeyValuePair<string, bool>(item.Key, true);
            }
            bool deleted = false;
            if (ColumnsSelectedIndices.Count() != ColumnsSelectedIndices.Where(x => x.Value).Select(x => x).Count())
                deleted = true;

            ColumnsSelectedIndices = ColumnsSelectedIndices.Where(x => x.Value).Select(x => x).ToList();
            if (deleted)
                return;

            ListBox listBox1 = lbColumns;
            Label label1 = lblColumns;
            try
            {
                if (lbColumns.SelectedIndex > -1)
                {
                    moving = true;
                    ColumnIndex = ColumnsList.Where(x => x.Key == ColumnsSelectedIndices[ColumnsSelectedIndices.Count - 1].Key).Select(x => lbColumns.Items.IndexOf(x)).First();
                    label1.Visible = true;
                    label1.Location = new Point(listBox1.Location.X + e.X + offset, listBox1.Location.Y + e.Y + offset);
                    columns_startloc = label1.Location;
                    label1.Text = ((KeyValuePair<string, string>)listBox1.Items[ColumnIndex]).Value.ToString();
                    sw.Start();
                }
            }
            catch (Exception) { }
        }
        private void lbColumns_MouseUp(object sender, MouseEventArgs e)
        {
            lblColumns.Visible = false;
            moving = false;
        }
        private void lbColumns_MouseMove(object sender, MouseEventArgs e)
        {
            int y = 12;
            Label label1 = lblColumns;
            ListBox listBox1 = lbColumns;
            if (label1.Visible == true && moving)
            {
                label1.Location = new Point(listBox1.Location.X + e.X + offset, listBox1.Location.Y + e.Y + offset);
                if (columns_startloc.Y > label1.Location.Y + y)
                {
                    if (ColumnIndex > 0)
                    {
                        KeyValuePair<string, string> temp = (KeyValuePair<string, string>)listBox1.Items[ColumnIndex];
                        listBox1.Items.RemoveAt(ColumnIndex);
                        listBox1.Items.Insert(ColumnIndex - 1, temp);
                        listBox1.SelectedIndex = ColumnIndex - 1;
                        ColumnIndex--;
                        columns_startloc = label1.Location;
                    }
                }
                else
                    if (columns_startloc.Y < label1.Location.Y - y)
                    {
                        if (ColumnIndex + 1 < listBox1.Items.Count)
                        {
                            KeyValuePair<string, string> temp = (KeyValuePair<string, string>)listBox1.Items[ColumnIndex];
                            listBox1.Items.RemoveAt(ColumnIndex);
                            listBox1.Items.Insert(ColumnIndex + 1, temp);
                            listBox1.SelectedIndex = ColumnIndex + 1;
                            ColumnIndex++;
                            columns_startloc = label1.Location;
                        }
                    }
            }
        }

        private void lbOrder_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < OrderSelectedIndices.Count; i++)
                OrderSelectedIndices[i] = new KeyValuePair<string, bool>(OrderSelectedIndices[i].Key, false);

            foreach (KeyValuePair<string, string> item in lbOrder.SelectedItems)
            {
                var ss = OrderSelectedIndices.Where(x => x.Key == item.Key).Select(x => x).ToList();
                if (ss.Count() == 0)
                    OrderSelectedIndices.Add(new KeyValuePair<string, bool>(item.Key, true));
                else
                    OrderSelectedIndices[OrderSelectedIndices.IndexOf(ss.First())] = new KeyValuePair<string, bool>(item.Key, true);
            }
            bool deleted = false;
            if (OrderSelectedIndices.Count() != OrderSelectedIndices.Where(x => x.Value).Select(x => x).Count())
                deleted = true;

            OrderSelectedIndices = OrderSelectedIndices.Where(x => x.Value).Select(x => x).ToList();
            if (deleted)
                return;

            ListBox listBox1 = lbOrder;
            Label label1 = lblColumns;
            try
            {
                if (listBox1.SelectedIndex > -1)
                {
                    moving = true;
                    OrderIndex = OrderList.Where(x => x.Key == OrderSelectedIndices[OrderSelectedIndices.Count - 1].Key).Select(x => listBox1.Items.IndexOf(x)).First();
                    label1.Visible = true;
                    label1.Location = new Point(listBox1.Location.X + e.X + offset, listBox1.Location.Y + e.Y + offset);
                    order_startloc = label1.Location;
                    label1.Text = ((KeyValuePair<string, string>)listBox1.Items[OrderIndex]).Value.ToString();
                    sw.Start();
                }
            }
            catch (Exception) { }
        }
        private void lbOrder_MouseMove(object sender, MouseEventArgs e)
        {
            int y = 12;
            Label label1 = lblColumns;
            ListBox listBox1 = lbOrder;
            if (label1.Visible == true && moving)
            {
                label1.Location = new Point(listBox1.Location.X + e.X + offset, listBox1.Location.Y + e.Y + offset);
                if (order_startloc.Y > label1.Location.Y + y)
                {
                    if (OrderIndex > 0)
                    {
                        KeyValuePair<string, string> temp = (KeyValuePair<string, string>)listBox1.Items[OrderIndex];
                        listBox1.Items.RemoveAt(OrderIndex);
                        listBox1.Items.Insert(OrderIndex - 1, temp);
                        listBox1.SelectedIndex = OrderIndex - 1;
                        OrderIndex--;
                        order_startloc = label1.Location;
                    }
                }
                else
                    if (order_startloc.Y < label1.Location.Y - y)
                    {
                        if (OrderIndex + 1 < listBox1.Items.Count)
                        {
                            KeyValuePair<string, string> temp = (KeyValuePair<string, string>)listBox1.Items[OrderIndex];
                            listBox1.Items.RemoveAt(OrderIndex);
                            listBox1.Items.Insert(OrderIndex + 1, temp);
                            listBox1.SelectedIndex = OrderIndex + 1;
                            OrderIndex++;
                            order_startloc = label1.Location;
                        }
                    }
            }
        }
        private void lbOrder_MouseUp(object sender, MouseEventArgs e)
        {
            lblColumns.Visible = false;
            moving = false;
        }
        #endregion
        #region Fill
        private void Init()
        {
            FillListBox();
            FillColumns();
            FillFormat();
            FillClass();
        }
        private void FillListBox()
        {
            while (lbPrintList.Items.Count > 0)
                lbPrintList.Items.RemoveAt(0);
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                var lst = (from c in context.PrintList
                           select c).ToList();
                var bind = (from rw in lst
                            select new KeyValuePair<string, string>( rw.Id.ToString(), rw.Name)).ToList();
                foreach (var x in bind)
                {
                    lbPrintList.Items.Add(x);
                }
                lbPrintList.DisplayMember = "Value";
                lbPrintList.ValueMember = "Key";
            }
        }
        private void FillColumns()
        {
            while (lbOrder.Items.Count > 0)
                lbOrder.Items.RemoveAt(0);
            while (lbColumns.Items.Count > 0)
                lbColumns.Items.RemoveAt(0);

            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                var lst = (from c in context.Columns
                           join x in context.PrintListColumns on new { ColumnId = c.Id, PrintListId = PrintListId } equals new { ColumnId = x.ColumnId, x.PrintListId } into _x
                           from x in _x.DefaultIfEmpty()
                           select new
                           {
                               c.Id,
                               Name = c.ColumnHeader,
                               Order = (x == null) ? 999 : x.OrdId,
                               Used = (x == null) ? false : (x.OrdId > 0),
                           }).OrderBy(t => t.Order).ToList();
                var bind = (from rw in lst
                            select new KeyValuePair<string, string>(rw.Id.ToString(), rw.Name)).ToList();
                ColumnsList = bind;
                foreach (var x in bind)
                {
                    lbColumns.Items.Add(x);
                }
                lbColumns.DisplayMember = "Value";
                lbColumns.ValueMember = "Key";

                lbColumns.SelectedIndices.Clear();
                for (int i = 0; i < lbColumns.Items.Count; i++)
                {
                    var x = lbColumns.Items[i];
                    if (x is KeyValuePair<string, string>)
                    {
                        KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)x;
                        if (lst.Where(l => l.Id.ToString() == kvp.Key).Select(l => l.Used).First())
                        {
                            lbColumns.SelectedItems.Add(x);
                            ColumnsSelectedIndices.Add(new KeyValuePair<string, bool>(kvp.Key, true));
                        }
                    }
                }

                lst = (from c in context.Columns
                       join x in context.PrintListOrder on new { ColumnId = c.Id, PrintListId = PrintListId } equals new { ColumnId = x.ColumnId, x.PrintListId } into _x
                       from x in _x.DefaultIfEmpty() 
                       select new
                       {
                           c.Id,
                           Name = c.ColumnHeader,
                           Order = (x == null) ? 999 : x.OrdId,
                           Used = (x == null) ? false : (x.OrdId > 0),
                       }).OrderBy(t => t.Order).ToList();
                bind = (from rw in lst
                            select new KeyValuePair<string, string>(rw.Id.ToString(), rw.Name)).ToList();
                OrderList = bind;
                foreach (var x in bind)
                {
                    lbOrder.Items.Add(x);
                }
                lbOrder.DisplayMember = "Value";
                lbOrder.ValueMember = "Key";
                lbOrder.SelectedIndices.Clear();
                for (int i = 0; i < lbOrder.Items.Count; i++)
                {
                    var x = lbOrder.Items[i];
                    if (x is KeyValuePair<string, string>)
                    {
                        KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)x;
                        if (lst.Where(l => l.Id.ToString() == kvp.Key).Select(l => l.Used).First())
                        {
                            lbOrder.SelectedItems.Add(x);
                            OrderSelectedIndices.Add(new KeyValuePair<string, bool>(kvp.Key, true));
                        }
                    }
                }
            }
        }
        private void FillFormat()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>("1", "Excel - .xls"));
            ComboServ.FillCombo(cbFormat, lst, false, false);
        }
        private void FillClass()
        {
            ComboServ.FillCombo(cbClass, HelpClass.GetComboListByTable("dbo.SchoolClass"), false, true);
        }
        #endregion

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = radioButton1.Checked;
            groupBox2.Enabled = radioButton2.Checked;
            groupBox3.Enabled = radioButton3.Checked;
        }
        private void lbPrintList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPrintList.SelectedItem != null)
            {
                KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)lbPrintList.SelectedItem;
                PrintListId = int.Parse(kvp.Key);
                FillColumns();
            }
        }
        private void btnPrintListCoumnsSave_Click(object sender, EventArgs e)
        {
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                List<int> lst = new List<int>();
                foreach (var Item in lbColumns.SelectedItems)
                {
                    if (Item is KeyValuePair<string, string>)
                    {
                        KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)Item;
                        lst.Add(int.Parse(kvp.Key));
                    }
                }
                context.PrintListColumns.RemoveRange(context.PrintListColumns.Where(x => x.PrintListId == PrintListId));
                context.SaveChanges();
                int priority = 1;
                foreach (int x in lst)
                {
                    context.PrintListColumns.Add(new PrintListColumns()
                        {
                            PrintListId = PrintListId,
                            ColumnId = x,
                            OrdId = priority
                        });
                    priority++;
                }
                context.SaveChanges();
                ///
                lst = new List<int>();
                foreach (var Item in lbOrder.SelectedItems)
                {
                    if (Item is KeyValuePair<string, string>)
                    {
                        KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)Item;
                        lst.Add(int.Parse(kvp.Key));
                    }
                }
                context.PrintListOrder.RemoveRange(context.PrintListOrder.Where(x => x.PrintListId == PrintListId));
                priority = 1;
                foreach (int x in lst)
                {
                    context.PrintListOrder.Add(new PrintListOrder()
                    {
                        PrintListId = PrintListId,
                        ColumnId = x,
                        OrdId = priority
                    });
                    priority++;
                }
                context.SaveChanges();

                MessageBox.Show("Сохранено", "Mission Complete");
            }
        }
        private void btnPrintListAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbPrintListNewName.Text))
            {return;}
            using (OnlineOlymp2016Entities context = new OnlineOlymp2016Entities())
            {
                if (context.PrintList.Where(x => x.Name == tbPrintListNewName.Text.Trim()).Count() > 0)
                    return;

                context.PrintList.Add(new PrintList()
                    {
                        Name = tbPrintListNewName.Text.Trim(),
                    });
                context.SaveChanges();
            }
            FillListBox();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string resultquery = @"select Participant.Id as 'Id'";
            Dictionary<string, object> Dict = new Dictionary<string, object>();
            Dict.Add("@PrintListId", PrintListId);

            string query = @"SELECT ColumnId
            , Columns.ColumnHeader
            , Columns.ColumnName
            , Columns.TableName
            , OrdId
            FROM  [dbo].[PrintListColumns]
            join dbo.[Columns]
            on [Columns].Id = PrintListColumns.ColumnId 
            where PrintListId = @PrintListId
            order by OrdId";
            DataTable tbl = Util.MainBD.GetDataTable(query, Dict);

            foreach (DataRow rw in tbl.Rows)
            {
                resultquery += ", " + rw.Field<string>("TableName") + "." + rw.Field<string>("ColumnName") + " as '" + rw.Field<string>("ColumnHeader") + "'";
            }
            resultquery += " from dbo.Participant ";

            query = @"SELECT ColumnId
            ,Columns.ColumnHeader
            ,Columns.ColumnName
            ,Columns.TableName
            ,OrdId
            FROM  dbo.PrintListOrder
            join dbo.[Columns]
            on [Columns].Id = PrintListOrder.ColumnId 
            where PrintListId = @PrintListId
            order by OrdId";
            DataTable tbl_order = Util.MainBD.GetDataTable(query, Dict);

            List<string> table_names = new List<string>();
            table_names = (from DataRow rw in tbl.Rows select rw.Field<string>("TableName")).Distinct().ToList().Union(from DataRow rw in tbl_order.Rows select rw.Field<string>("TableName")).Distinct().ToList();
            foreach (var x in table_names)
            {
                if (x.ToLower() != "Participant")
                {
                    resultquery += @" left join dbo." + x + " on " + x + ".Id =" + "Participant." + x + "Id";
                }
            }

            if (table_names.Count == 0)
                resultquery = GetStandartQuery();

            resultquery += " where 1=1 ";
            int? ClassId = ComboServ.GetComboIdInt(cbClass);
            if (ClassId.HasValue)
                resultquery += " and Participant.ClassId = " + ClassId.Value.ToString();
            ToExcel(query, Dict);
        }
        private void ToExcel (string query, Dictionary <string, object> Dict)
        {
            if (String.IsNullOrEmpty(query))
                return;
            try
            {
                DataTable tbl = Util.MainBD.GetDataTable(query, Dict);
                DataGridView dgv1 = new DataGridView();
                dgv1.DataSource = tbl;

                string filenameDate = String.IsNullOrEmpty(tbFilename.Text) ? "Выгрузка " : tbFilename.Text;
                string filename = Util.TempFolder + filenameDate + ".xls";

                int fileindex = 1;
                while (File.Exists(filename))
                {
                    filename = Util.TempFolder + filenameDate + "(" + fileindex + ")" + ".xls";
                    fileindex++;
                }
                System.IO.FileInfo newFile = new System.IO.FileInfo(filename);
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new System.IO.FileInfo(filename);
                }
                byte[] bt;

                using (ExcelPackage doc = new ExcelPackage(newFile))
                {
                    ExcelWorksheet ws = doc.Workbook.Worksheets.Add("list");
                    int colind = 0;
                    foreach (DataGridViewColumn cl in dgv1.Columns)
                    {
                        ws.Cells[++colind,1].Value = cl.Name.ToString();
                    }
                    foreach (DataGridViewRow rw in dgv1.Rows)
                    {
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            ws.Cells[rw.Index + 2, cell.ColumnIndex + 1].Value = cell.Value.ToString();
                        }
                    }
                    bt = doc.GetAsByteArray();
                }
                File.WriteAllText(filename, bt.ToString(), Encoding.UTF8);

                if (cbOpenFile.Checked)
                    System.Diagnostics.Process.Start(filename);
            }
            catch
            {
            }
        }
      
        private string GetStandartQuery()
        {
            return @"select distinct 
                       Surname as 'Фамилия'
                      , Name as 'Имя'
                      , SecondNamename as 'Отчество'
                      , BirthDate as 'Дата рождения'
                  FROM dbo.Participant ";
        }
    }
}
