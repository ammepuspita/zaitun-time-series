// Zaitun Time Series 
// http://www.zaitunsoftware.com/
// http://code.google.com/p/zaitun-time-series/
//
// Copyright ©  2008-2009, Zaitun Time Series Developer Team and individual contributors
//
// Leader: Rizal Zaini Ahmad Fathony (rizalzaf@gmail.com)
// Members: Suryono Hadi Wibowo (ryonoha@gmail.com), Khaerul Anas (anasikova@gmail.com), 
//          Lia Amelia (afifahlia@gmail.com), Almaratul Sholihah, Muhamad Fuad Hasan
//
// This is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as
// published by the Free Software Foundation; either version 3 of
// the License, or (at your option) any later version.
//
// This software is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public
// License along with this software; if not, write to the Free
// Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
// 02110-1301 USA, or see the FSF site: http://www.fsf.org.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using zaitun.Data;
using ZedGraph;

namespace zaitun.Decomposition
{
    public partial class DeseasonalGraph : UserControl
    {
        private SeriesVariable variable;
        private SeriesData data;

        private double[] deseasonal;

        public DeseasonalGraph()
        {
            InitializeComponent();
        }

        public void SetData(SeriesData data, SeriesVariable variable, double[] deseasonal)
        {
            this.variable = variable;
            this.data = data;
            this.deseasonal = deseasonal;
            this.update();
        }


        private void update()
        {
            int notIncludedObservation = this.data.NumberObservations - this.deseasonal.Length;
            double[] deseasonalToView = new double[this.data.NumberObservations];

            for (int i = 0; i < notIncludedObservation; i++)
                deseasonalToView[i] = double.NaN;

            for (int i = notIncludedObservation; i < this.data.NumberObservations; i++)
                deseasonalToView[i] = this.deseasonal[i - notIncludedObservation];

            GraphPane variablePane = this.deseasonalZedGraph.GraphPane;

            variablePane.CurveList.Clear();

            variablePane.Title.Text = "Deseasonal Graph";
            variablePane.XAxis.Title.Text = "Time";
            variablePane.YAxis.Title.Text = "Data";
            variablePane.XAxis.Type = AxisType.Text;
            variablePane.XAxis.Scale.TextLabels = data.Time.ToArray();

            LineItem myCurve = variablePane.AddCurve("Deseasonal", null, deseasonalToView, Color.Blue, SymbolType.Circle);


            myCurve.Symbol.Border.IsVisible = false;
            myCurve.Symbol.Fill = new Fill(Color.Red);
            myCurve.Symbol.Size = 5;

            variablePane.Chart.Fill = new Fill(Color.FromArgb(255, 255, 245),
                        Color.FromArgb(255, 255, 190), 90F);
            variablePane.Fill = new Fill(Color.White, Color.LightBlue, 135.0f);

            deseasonalZedGraph.AxisChange();

            zedGraphToolstrip1.SetData(deseasonalZedGraph, variablePane);
        }
    }
}
