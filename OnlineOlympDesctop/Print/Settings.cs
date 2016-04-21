using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AbiturientPost
{ 
    public partial class Settings : Form
    {
        SettingsClass RejectionReason;
        SettingsClass Content;
        SettingsClass Delivered;
        SettingsClass PostOperator;
        SettingsClass AbiturientTypeList;
        //Label lblColumns = new Label();

        public Settings()
        {
            InitializeComponent();
            this.Text = "Настройки";
            FillCard();

            lblColumns.Visible = false;
            lblColumns.BackColor = Color.Transparent;
            lblColumns.BorderStyle = BorderStyle.FixedSingle;
        }
        private void FillCard()
        {
            RejectionReason = new SettingsClass();
            RejectionReason.Init(dgvRejectionReason, btnRejectionReasonChange, btnRejectionReasonAddNew, tbRejectionReasonChange, tbRejectionReasonNew, "Причина", "OsnovanieDlaOtkaza");
            Content = new SettingsClass();
            Content.Init(dgvContent, btnContentChange, btnContentAddNew, tbContentChange, tbContentNew, "Содержимое", "Soderjimoe");
            Delivered = new SettingsClass();
            Delivered.Init(dgvDelivered, btnDeliveredChange, btnDeliveredAdd, tbDeliveredChange, tbDeliveredAdd, "Доставлено", "Dostavleno");
            PostOperator = new SettingsClass();
            PostOperator.Init(dgvPostOperator, btnPostOperatorChange, btnPostOperatorAdd, tbPostOperatorChange, tbPostOperatorAdd, "Почтовый оператор", "PochtoviyOperator");
            AbiturientTypeList = new SettingsClass("Name");
            AbiturientTypeList.Init(dgvAbiturientTypeList, btnAbiturientTypeListChange, btnAbiturientTypeListAdd, tbAbiturientTypeListChange, tbAbiturientTypeListAdd, "Список", "AbiturientTypeList");
            FillAbiturientTypeList();
        }
        #region RejectionReason
        
        private void dgvRejectionReason_CurrentCellChanged(object sender, EventArgs e)
        {
            RejectionReason.DataGridView_CurrentCellChanged();
        }
        private void btnRejectionReasonChange_Click(object sender, EventArgs e)
        {
            RejectionReason.ChangeButton_Click();
        }
        private void tbRejectionReasonChange_TextChanged(object sender, EventArgs e)
        {
            RejectionReason.TextBox_Changed();
        } 
        private void btnRejectionReasonAddNew_Click(object sender, EventArgs e)
        {
            RejectionReason.AddButton_Click();
        }
        #endregion
        #region Content
        private void dgvContent_CurrentCellChanged(object sender, EventArgs e)
        {
            Content.DataGridView_CurrentCellChanged();
        }

        private void tbContentChange_TextChanged(object sender, EventArgs e)
        {
            Content.TextBox_Changed();
        }

        private void btnContentChange_Click(object sender, EventArgs e)
        {
            Content.ChangeButton_Click();
        }

        private void btnContentAddNew_Click(object sender, EventArgs e)
        {
            Content.AddButton_Click();
        }
        #endregion
        #region Delivered
        private void dgvDelivered_CurrentCellChanged(object sender, EventArgs e)
        {
            Delivered.DataGridView_CurrentCellChanged();
        }

        private void tbDeliveredChange_TextChanged(object sender, EventArgs e)
        {
            Delivered.TextBox_Changed();
        }

        private void btnDeliveredChange_Click(object sender, EventArgs e)
        {
            Delivered.ChangeButton_Click();
        }

        private void btnDeliveredAdd_Click(object sender, EventArgs e)
        {
            Delivered.AddButton_Click();
        }

        #endregion
        #region PostOperator
        private void dgvPostOperator_CurrentCellChanged(object sender, EventArgs e)
        {
            PostOperator.DataGridView_CurrentCellChanged();
        }

        private void tbPostOperatorChange_TextChanged(object sender, EventArgs e)
        {
            PostOperator.TextBox_Changed();
        }

        private void btnPostOperatorChange_Click(object sender, EventArgs e)
        {
            PostOperator.ChangeButton_Click();
        }

        private void btnPostOperatorAdd_Click(object sender, EventArgs e)
        {
            PostOperator.AddButton_Click();
        }
        #endregion
        #region AbiturientTypeList
        private void dgvAbiturientTypeList_CurrentCellChanged(object sender, EventArgs e)
        {
            AbiturientTypeList.DataGridView_CurrentCellChanged();
        }
        private void tbAbiturientTypeListChange_TextChanged(object sender, EventArgs e)
        {
            AbiturientTypeList.TextBox_Changed();
        }
        private void btnAbiturientTypeListChange_Click(object sender, EventArgs e)
        {
            AbiturientTypeList.ChangeButton_Click();
        }
        private void btnAbiturientTypeListAdd_Click(object sender, EventArgs e)
        {
            AbiturientTypeList.AddButton_Click();
            FillAbiturientTypeList();
        }
        private void FillAbiturientTypeList()
        {
            string query = @"SELECT Id, Name FROM dbo.AbiturientTypeList";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(cbAbitTypeList, bind, false, false);
        }
        private void FillAbiturientTypeListReason()
        {
            int? AbiturientTypeListId = ComboServ.GetComboIdInt(cbAbitTypeList);
            if (!AbiturientTypeListId.HasValue)
                return;
            string query = @"SELECT 
            Id, Text as Name
            FROM   dbo.OsnovanieDlaOtkaza ";
            DataTable tbl = Util.BDC.GetDataTable(query, null);
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<long>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ComboServ.FillCombo(lbAbitListReason, bind, false, false);
            query = @"SELECT 
            ReasonId 
            FROM dbo.AbiturientTypeListReason
            join dbo.OsnovanieDlaOtkaza
            on OsnovanieDlaOtkaza.Id = ReasonId 
            where AbiturientTypeListReason.AbiturientTypeListId = " + AbiturientTypeListId.Value.ToString();
            tbl = Util.BDC.GetDataTable(query, null);
            var select = (from DataRow rw in tbl.Rows select rw.Field<int>("ReasonId").ToString()).ToList();
            lbAbitListReason.SelectedIndices.Clear();

            for (int i = 0; i< lbAbitListReason.Items.Count; i++)
            {
                var x = lbAbitListReason.Items[i];
                if (x is KeyValuePair<string, string>)
                {
                    KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)x;
                    if (select.Contains(kvp.Key))
                    {
                        lbAbitListReason.SelectedItems.Add(x);
                    }
                }
            }
        }
        private void cbAbitTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillAbiturientTypeListReason();
            FillColumns();
        }
        private void btnAbitTypeListReasonSave_Click(object sender, EventArgs e)
        {
            int? AbiturientTypeListId = ComboServ.GetComboIdInt(cbAbitTypeList);
            if (!AbiturientTypeListId.HasValue)
                return;
            List<int> lst = new List<int>();
            foreach (var Item in lbAbitListReason.SelectedItems)
            {
                if (Item is KeyValuePair<string, string>)
                {
                    KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)Item;
                    lst.Add(int.Parse(kvp.Key));
                }
            }
            //
            string query = @"delete from dbo.AbiturientTypeListReason where AbiturientTypeListId = " + AbiturientTypeListId.Value.ToString();
            Util.BDC.ExecuteQuery(query, null);
            foreach (int x in lst)
            {
                Util.BDC.ExecuteQuery(@"insert into dbo.AbiturientTypeListReason (AbiturientTypeListId, ReasonId) VALUES (@Id, @reasonId)",
                    new Dictionary<string, object>() { { "@Id", AbiturientTypeListId }, { "@reasonId", x } });
            }
            ///
            lst = new List<int>();
            foreach (var Item in lbColumns.SelectedItems)
            {
                if (Item is KeyValuePair<string, string>)
                {
                    KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)Item;
                    lst.Add(int.Parse(kvp.Key));
                }
            }
            query = @"delete from dbo.AbiturientTypeListColumns where AbiturientTypeListId = " + AbiturientTypeListId.Value.ToString();
            Util.BDC.ExecuteQuery(query, null);
            int priority = 1;
            foreach (int x in lst)
            {
                Util.BDC.ExecuteQuery(@"insert into dbo.AbiturientTypeListColumns (AbiturientTypeListId, ColumnId, Priority) VALUES (@Id, @ColumnId, @Priority)",
                    new Dictionary<string, object>() { { "@Id", AbiturientTypeListId }, { "@ColumnId", x }, { "@Priority", priority } });
                priority++;
            }
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
            query = @"delete from dbo.AbiturientTypeListOrder where AbiturientTypeListId = " + AbiturientTypeListId.Value.ToString();
            Util.BDC.ExecuteQuery(query, null);
            priority = 1;
            foreach (int x in lst)
            {
                Util.BDC.ExecuteQuery(@"insert into dbo.AbiturientTypeListOrder (AbiturientTypeListId, ColumnId, Priority) VALUES (@Id, @ColumnId, @Priority)",
                    new Dictionary<string, object>() { { "@Id", AbiturientTypeListId }, { "@ColumnId", x }, { "@Priority", priority } });
                priority++;
            }

            MessageBox.Show("Сохранено", "Mission Complete");
        }
 
        List<KeyValuePair<string, string>> ColumnsList;
        List<KeyValuePair<string, string>> OrderList;

        List<KeyValuePair<string, bool>> ColumnsSelectedIndices = new List<KeyValuePair<string, bool>>();
        private void FillColumns()
        {
            int? AbiturientTypeListId = ComboServ.GetComboIdInt(cbAbitTypeList);
            if (!AbiturientTypeListId.HasValue)
                return;
            while (lbOrder.Items.Count > 0)
                lbOrder.Items.RemoveAt(0);
            while (lbColumns.Items.Count > 0)
                lbColumns.Items.RemoveAt(0);
            string query = @"select Id, ColumnHeader as Name, Priority, case when (Priority >0) then 1 else 0 end as  Used
 from dbo.[Columns]
 left join dbo.AbiturientTypeListColumns on 
 ([Columns].Id = AbiturientTypeListColumns.ColumnId and AbiturientTypeListColumns.AbiturientTypeListId = @Id)
 order by used desc, Priority ";
            DataTable tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", AbiturientTypeListId } });
            var bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            ColumnsList = bind;
            foreach (var x in bind)
            {
                lbColumns.Items.Add(x);
            }
            lbColumns.DisplayMember = "Value";
            lbColumns.ValueMember = "Key";

            query = @"select Id, ColumnHeader as Name, Priority, case when (Priority >0) then 1 else 0 end as  Used
 from dbo.[Columns]
 left join dbo.AbiturientTypeListOrder on 
 ([Columns].Id = AbiturientTypeListOrder.ColumnId and AbiturientTypeListOrder.AbiturientTypeListId = @Id)
 order by used desc, Priority ";
            tbl = Util.BDC.GetDataTable(query, new Dictionary<string, object>() { { "@Id", AbiturientTypeListId } });
            bind = (from DataRow rw in tbl.Rows
                        select new KeyValuePair<string, string>(rw.Field<int>("Id").ToString(), rw.Field<string>("Name"))).ToList();
            OrderList = bind;
            foreach (var x in bind)
            {
                lbOrder.Items.Add(x);
            }
            lbOrder.DisplayMember = "Value";
            lbOrder.ValueMember = "Key";

            query = @"SELECT 
            ColumnId 
            FROM dbo.AbiturientTypeListColumns
            join dbo.Columns
            on Columns.Id = ColumnId 
            where AbiturientTypeListColumns.AbiturientTypeListId = " + AbiturientTypeListId.Value.ToString();
            tbl = Util.BDC.GetDataTable(query, null);
            var select = (from DataRow rw in tbl.Rows select rw.Field<int>("ColumnId").ToString()).ToList();
            lbColumns.SelectedIndices.Clear();
            ColumnsSelectedIndices = new List<KeyValuePair<string, bool>>();
            
            for (int i = 0; i < lbColumns.Items.Count; i++)
            {
                var x = lbColumns.Items[i];
                if (x is KeyValuePair<string, string>)
                {
                    KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)x;
                    if (select.Contains(kvp.Key))
                    {
                        lbColumns.SelectedItems.Add(x);
                        ColumnsSelectedIndices.Add(new KeyValuePair<string, bool>(kvp.Key, true));
                    }
                }
            }

            query = @"SELECT 
            ColumnId 
            FROM dbo.AbiturientTypeListOrder
            join dbo.Columns
            on Columns.Id = ColumnId 
            where AbiturientTypeListOrder.AbiturientTypeListId = " + AbiturientTypeListId.Value.ToString();
            tbl = Util.BDC.GetDataTable(query, null);
            select = (from DataRow rw in tbl.Rows select rw.Field<int>("ColumnId").ToString()).ToList();
            lbOrder.SelectedIndices.Clear();
            OrderSelectedIndices = new List<KeyValuePair<string, bool>>();
            for (int i = 0; i < lbOrder.Items.Count; i++)
            {
                var x = lbOrder.Items[i];
                if (x is KeyValuePair<string, string>)
                {
                    KeyValuePair<string, string> kvp = (KeyValuePair<string, string>)x;
                    if (select.Contains(kvp.Key))
                    {
                        lbOrder.SelectedItems.Add(x);
                        OrderSelectedIndices.Add(new KeyValuePair<string, bool>(kvp.Key, true));
                    }
                }
            }
        }
        static int offset = 15; //чтоб имя перетаскиваемого элемента рядом с мышкой было 
        Stopwatch sw = new Stopwatch(); //чтобы не реагировал на обычный клик мышкой
        bool moving = false;
        int ColumnIndex = -1;
        Point columns_startloc;
        Point order_startloc;

        List<KeyValuePair<string, bool>> OrderSelectedIndices = new List<KeyValuePair<string, bool>>();
        int OrderIndex = -1;
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

    } 
}
