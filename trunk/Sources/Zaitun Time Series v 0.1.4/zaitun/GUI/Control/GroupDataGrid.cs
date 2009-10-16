using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using zaitun.GUI;
using zaitun.Data;

namespace zaitun.GUI
{
    public partial class GroupDataGrid : UserControl
    {
        private SeriesGroup group;
        private SeriesData data;

        public GroupDataGrid()
        {
            InitializeComponent();
            grdSeriesGroup.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
        }

        public void SetData(SeriesGroup group, SeriesData data)
        {
            this.group = group;
            this.data = data;
            this.group.Changed += new ChangedEventHandler(group_Changed);
            foreach (SeriesVariable var in this.group.GroupList)
            {
                var.SeriesValues.Changed += new ChangedEventHandler(SeriesValues_Changed);
            }            
            this.update();
        }

        void group_Changed(object sender, EventArgs e)
        {
            this.update();
        }

        void SeriesValues_Changed(object sender, EventArgs e)
        {
            this.update();
        }

        private void update()
        {
            grdSeriesGroup.Columns.Clear();
            foreach (SeriesVariable item in this.group.GroupList)
            {
                DataGridViewTextBoxColumn variableColumn = new DataGridViewTextBoxColumn();
                variableColumn.DefaultCellStyle.Format = "G7";
                variableColumn.Name = item.VariableName;
                variableColumn.HeaderText = item.VariableName;
                variableColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                this.grdSeriesGroup.Columns.Add(variableColumn);                
            }

            grdSeriesGroup.RowCount = this.data.NumberObservations;
            for (int i = 0; i < this.data.NumberObservations; i++)
                grdSeriesGroup.Rows[i].HeaderCell.Value = this.data.Time[i];

            for (int i = 0; i < grdSeriesGroup.Columns.Count; i++)
                for (int j = 0; j < grdSeriesGroup.Rows.Count; j++)
                    grdSeriesGroup.Rows[j].Cells[i].Value = Math.Round(this.group.GroupList[i].SeriesValues[j], 4);
            
        }

        private void grdSeriesVariable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           // this.grdSeriesGroup.BeginEdit(false);
        }

        private void grdSeriesVariable_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //this.group[e.RowIndex] = Convert.ToDouble(this.grdSeriesVariable.Rows[e.RowIndex].Cells[0].Value);
        }

        private void grdSeriesVariable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >=0 && e.ColumnIndex>=0)
            //    this.group[e.RowIndex] = Convert.ToDouble(this.grdSeriesGroup.Rows[e.RowIndex].Cells[0].Value);
        }     

        
    }
}
