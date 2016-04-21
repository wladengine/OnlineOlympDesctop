using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AbiturientPost
{
    public class SettingsClass
    {
        DataGridView dgv;
        Button btnChange;
        Button btnNew;

        TextBox tbChange;
        TextBox tbNew;

        string ColumnName;
        string Name;
        string Table;

        public SettingsClass()
        {
            ColumnName = "Text";
        }
        public SettingsClass(string cname)
        {
            ColumnName = cname;
        }
        public void Init(DataGridView _dgv, Button _btn_change, Button _btn_new, TextBox _tb_change, TextBox _tb_new, string _name, string _table)
        {
            dgv = _dgv;
            btnChange = _btn_change;
            btnNew = _btn_new;
            tbChange = _tb_change;
            tbNew = _tb_new;
            Name = _name;
            Table = _table;
            FillDataGridView();
        }
        public void FillDataGridView()
        {
            try
            {
                string query = @"SELECT Id, "+ColumnName+" as '" + Name + "' FROM dbo." + Table;
                DataTable tbl = Util.BDC.GetDataTable(query, null);
                dgv.DataSource = tbl;
                btnChange.Enabled = false;
                if (dgv.Columns.Contains("Id"))
                    dgv.Columns["Id"].Visible = false;
            }
            catch
            {
            }
        }
        public void DataGridView_CurrentCellChanged()
        {
            if (dgv.CurrentCell == null)
            {
                btnChange.Enabled = false;
                return;
            }
            if (dgv.CurrentCell.RowIndex < 0 || dgv.CurrentCell.ColumnIndex < 0)
            {
                btnChange.Enabled = false;
                return;
            }
            btnChange.Enabled = true;
            tbChange.Text = dgv.CurrentRow.Cells[Name].Value.ToString();
        }
        public void ChangeButton_Click()
        {
            if (dgv.CurrentCell == null)
                return;
            if (dgv.CurrentCell.RowIndex < 0 || dgv.CurrentCell.ColumnIndex < 0)
                return;
            try
            {
                long id = long.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                string query = @"update dbo." + Table + " set Text=@Text where Id = @Id ";
                Util.BDC.ExecuteQuery(query, new Dictionary<string, object>() { { "@Text", tbChange.Text.Trim() }, { "@Id", id } });
                FillDataGridView();
            }
            catch
            {
                MessageBox.Show("Ошибка во время выполнения операции", "Печалька");
            }
        }
        public void TextBox_Changed()
        {
            if (String.IsNullOrEmpty(tbChange.Text.Trim()))
                btnChange.Enabled = false;
            else
                btnChange.Enabled = true;
            if (String.IsNullOrEmpty(tbNew.Text.Trim()))
                btnNew.Enabled = false;
            else
                btnNew.Enabled = true;
        }
        public void AddButton_Click()
        {
            if (String.IsNullOrEmpty(tbNew.Text.Trim()))
            {
                btnNew.Enabled = false;
                return;
            }
            try
            {
                int cnt = (int)Util.BDC.GetValue(@"select count(id) from dbo." + Table + " where " + ColumnName + " = @Text",
                    new Dictionary<string, object>() { { "@Text", tbNew.Text.Trim() } });
                if (cnt > 0)
                {
                    MessageBox.Show("Такое значение уже добавлено", "Ты не пройдешь!");
                    return;
                }
                Util.BDC.ExecuteQuery(@"insert into dbo." + Table + " ("+ColumnName+") values (@Text)",
                    new Dictionary<string, object>() { { "@Text", tbNew.Text.Trim() } });
                FillDataGridView();
                MessageBox.Show("Добавлено", "Это успех!");
            }
            catch
            {
                MessageBox.Show("Ошибка во время выполнения операции", "Печалька");
            }
        }
    }
}
