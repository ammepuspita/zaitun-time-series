using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using zaitun.GUI;
using zaitun.Data;

namespace zaitun.MovingAverage
{
    public partial class MovingAverage2DataGrid : UserControl
    {
        private SeriesVariable variable;
        private SeriesData data;

        private double[] smoothed1;
        private double[] smoothed2;
        private double[] predicted;
        private double[] residual;

        private bool isSmoothedVisible;

        public MovingAverage2DataGrid()
        {
            InitializeComponent();
            grdMovingAverage.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;

        }

        public void SetData(SeriesData data, SeriesVariable variable, bool isSmoothedVisible, double[] smoothed1, double[] smoothed2, double[] predicted, double[] residual)
        {
            this.data = data;
            this.variable = variable;
            this.isSmoothedVisible = isSmoothedVisible;
            this.smoothed1 = smoothed1;
            this.smoothed2 = smoothed2;
            this.predicted = predicted;
            this.residual = residual;

            this.update();
        }

        private void update()
        {
            int cell = 0;
            int notIncludedObservation = this.data.NumberObservations - this.predicted.Length;

            this.grdMovingAverage.ColumnCount = 3;

            grdMovingAverage.RowCount = this.data.NumberObservations;
            grdMovingAverage.Columns[0].HeaderCell.Value = "Actual";
            grdMovingAverage.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;

            for (int i = 0; i < data.NumberObservations; i++)
            {
                grdMovingAverage.Rows[i].HeaderCell.Value = this.data.Time[i];
                grdMovingAverage.Rows[i].Cells[0].Value = this.variable[i].ToString("F4");
            }

            if (this.isSmoothedVisible)
            {
                this.grdMovingAverage.ColumnCount = 5;
                grdMovingAverage.Columns[cell + 1].HeaderCell.Value = "Single MA";
                grdMovingAverage.Columns[cell + 1].SortMode = DataGridViewColumnSortMode.Programmatic;

                grdMovingAverage.Columns[cell + 2].HeaderCell.Value = "Double MA";
                grdMovingAverage.Columns[cell + 2].SortMode = DataGridViewColumnSortMode.Programmatic;

                double[] smoothed1ToView = new double[this.data.NumberObservations];
                double[] smoothed2ToView = new double[this.data.NumberObservations];

                for (int i = 0; i < notIncludedObservation; i++)
                {
                    smoothed1ToView[i] = double.NaN;
                    smoothed2ToView[i] = double.NaN;
                }

                for (int i = notIncludedObservation; i < this.data.NumberObservations; i++)
                {
                    smoothed1ToView[i] = this.smoothed1[i - notIncludedObservation];
                    smoothed2ToView[i] = this.smoothed2[i - notIncludedObservation];
                }

                for (int i = 0; i < data.NumberObservations; i++)
                {
                    grdMovingAverage.Rows[i].Cells[cell + 1].Value = smoothed1ToView[i].ToString("F4");
                    grdMovingAverage.Rows[i].Cells[cell + 2].Value = smoothed2ToView[i].ToString("F4");
                }

                cell = cell + 2;
            }

            double[] predictedToView = new double[this.data.NumberObservations];
            double[] residualToView = new double[this.data.NumberObservations];

            for (int i = 0; i < 2 * (notIncludedObservation - 1); i++)
            {
                predictedToView[i] = double.NaN;
                residualToView[i] = double.NaN;
            }

            for (int i = notIncludedObservation; i < this.data.NumberObservations; i++)
            {
                predictedToView[i] = this.predicted[i - notIncludedObservation];
                residualToView[i] = this.variable[i] - this.predicted[i - notIncludedObservation];
            }

            grdMovingAverage.Columns[cell + 1].HeaderCell.Value = "Predicted";
            grdMovingAverage.Columns[cell + 1].SortMode = DataGridViewColumnSortMode.Programmatic;

            grdMovingAverage.Columns[cell + 2].HeaderCell.Value = "Residual";
            grdMovingAverage.Columns[cell + 2].SortMode = DataGridViewColumnSortMode.Programmatic;


            for (int i = 0; i < data.NumberObservations; i++)
            {
                grdMovingAverage.Rows[i].Cells[cell + 1].Value = predictedToView[i].ToString("F4");
                grdMovingAverage.Rows[i].Cells[cell + 2].Value = residualToView[i].ToString("F4");
            }
        }

    }
}
