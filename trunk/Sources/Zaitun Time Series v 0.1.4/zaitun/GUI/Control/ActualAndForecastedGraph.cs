using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using zaitun.Data;
using ZedGraph;

namespace zaitun.GUI
{
    public partial class ActualAndForecastedGraph : UserControl
    {
        private SeriesVariable variable;
        private SeriesData data;

        private double[] forecasted;

        public ActualAndForecastedGraph()
        {
            InitializeComponent();
        }

        public void SetData(SeriesData data, SeriesVariable variable, double[] forecasted)
        {
            this.variable = variable;
            this.data = data;
            this.forecasted = forecasted;
            this.update();
        }


        private void update()
        {
            int totalDataToView = this.data.NumberObservations + this.forecasted.Length;
            double[] actualToView = new double[totalDataToView];
            double[] forecastedToView = new double[totalDataToView];

            for (int i = 0; i < this.data.NumberObservations; i++)
            {
                actualToView[i] = this.variable.SeriesValues[i];
                forecastedToView[i] = double.NaN;
            }

            for (int i = 0; i < this.forecasted.Length; i++)
            {
                actualToView[i + this.data.NumberObservations] = double.NaN;
                forecastedToView[i + this.data.NumberObservations] = this.forecasted[i];
            }

            GraphPane variablePane = this.actualAndForecastedZedGraph.GraphPane;

            variablePane.CurveList.Clear();

            variablePane.Title.Text = "Actual and Forecasted Graph";
            variablePane.XAxis.Title.Text = "Time";
            variablePane.YAxis.Title.Text = "Data";
            variablePane.XAxis.Type = AxisType.Text;
            //variablePane.XAxis.Scale.TextLabels = data.Time.ToArray();
            
            variablePane.AddCurve("Actual", null, actualToView, Color.Blue, SymbolType.None);

            if (forecasted.Length <= 1)
            {
                LineItem myCurve = variablePane.AddCurve("Forecasted", null, forecastedToView, Color.Red, SymbolType.Circle);
                myCurve.Symbol.Border.IsVisible = false;
                myCurve.Symbol.Fill = new Fill(Color.Red);
                myCurve.Symbol.Size = 5;
            }
            else
            { variablePane.AddCurve("Forecasted", null, forecastedToView, Color.Red, SymbolType.None); }
            variablePane.Chart.Fill = new Fill(Color.FromArgb(255, 255, 245),
                        Color.FromArgb(255, 255, 190), 90F);
            variablePane.Fill = new Fill(Color.White, Color.LightBlue, 135.0f);

            actualAndForecastedZedGraph.AxisChange();


        }
    }
}
