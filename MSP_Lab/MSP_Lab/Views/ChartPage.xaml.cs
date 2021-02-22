using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSP_Lab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartPage : ContentPage
    {
        private static readonly PieChart _pieChart = new PieChart { Entries = Data.ChartData.PieEntries, LabelMode = LabelMode.None,  };
        private static readonly LineChart _lineChart = new LineChart { Entries = Data.ChartData.LineEntries, LineSize = 3, LineMode = LineMode.Straight, PointMode = PointMode.None };

        bool _switched = false;

        public ChartPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            chartView.Chart = _pieChart;
        }

        void OnClick(object sender, EventArgs e)
        {
            if (_switched)
            {
                chartView.Chart = _pieChart;
                chartSwitch.Text = "Замінити на графік";
                chartPage.Title = "Діаграма";
            }
            else
            {
                chartView.Chart = _lineChart;
                chartSwitch.Text = "Замінити на діаграму";
                chartPage.Title = "Графік";
            }

            _switched = !_switched;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            chartView.HeightRequest = Math.Min(height - 50, width);
        }
    }
}