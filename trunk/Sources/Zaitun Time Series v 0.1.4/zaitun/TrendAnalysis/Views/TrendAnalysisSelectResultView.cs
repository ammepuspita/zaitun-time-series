using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace zaitun.TrendAnalysis
{
    public partial class TrendAnalysisSelectResultView : Form
    {
        public TrendAnalysisSelectResultView()
        {
            InitializeComponent();
        }

        private void forecastedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedCheck=((CheckBox)sender).Checked;
            this.forecastingStepTextBox.Enabled = checkedCheck;
            this.actualForecastedGraphCheckBox.Enabled = checkedCheck;
        }

        public bool IsTrendAnalysisModelSummaryChecked
        {
            get { return this.modelSummaryCheckBox.Checked; }
            set { this.modelSummaryCheckBox.Checked = value; }
        }

        public bool IsActualPredictedResidualDataGridChecked
        {
            get { return this.actualPredictedResidualCheckBox.Checked; }
            set { this.actualPredictedResidualCheckBox.Checked = value; }
        }

        public bool IsForecastedDataGridChecked
        {
            get { return this.forecastedCheckBox.Checked; }
            set { this.forecastedCheckBox.Checked = value; }
        }

        public bool IsActualAndPredictedGraphChecked
        {
            get { return this.actualPredictedGraphCheckBox.Checked; }
            set { this.actualPredictedGraphCheckBox.Checked = value; }
        }        

        public bool IsActualAndForecastedGraphChecked
        {
            get { return this.actualForecastedGraphCheckBox.Checked; }
            set { this.actualForecastedGraphCheckBox.Checked = value; }
        }

        public bool IsActualVsPredictedGraphChecked
        {
            get { return this.actualVsPredictedGraphCheckBox.Checked; }
            set { this.actualVsPredictedGraphCheckBox.Checked = value; }
        }

        public bool IsResidualGraphChecked
        {
            get { return this.residualGraphCheckBox.Checked; }
            set { this.residualGraphCheckBox.Checked = value; }
        }

        public bool IsResidualVsActualGraphChecked
        {
            get { return this.residualVsActualGraphCheckBox.Checked; }
            set { this.residualVsActualGraphCheckBox.Checked = value; }
        }

        public bool IsResidualVsPredictedGraphChecked
        {
            get { return this.residualVsPredictedCheckBox.Checked; }
            set { this.residualVsPredictedCheckBox.Checked = value; }
        }

        public int ForecastingStep
        {
            get
            {
                int result;
                try { result = int.Parse(forecastingStepTextBox.Text); }
                catch { result = 1; }
                return result;
            }
            set {
                if (value > 0)
                {
                    this.forecastingStepTextBox.Text = value.ToString();
                }
            }
        }
    }
}