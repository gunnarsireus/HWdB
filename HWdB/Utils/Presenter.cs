using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HWdB.Model;
using System.Windows.Media.Imaging;
using System.IO;

namespace HWdB.Utils
{
    class Presenter
    {
        public static void ClearResult(LtbDataSet ltbDataSet)
        {
            ltbDataSet.TotalStock = string.Empty;
            ltbDataSet.Stock = string.Empty;
            ltbDataSet.Safety = string.Empty;
            ltbDataSet.InfoText = string.Empty;
            ltbDataSet.Failed = string.Empty;
            ltbDataSet.Repaired = string.Empty;
            ltbDataSet.Lost = string.Empty;
        }
        public static void ClearChartData(LtbDataSet ltbDataSet)
        {
            int YearCnt = 0;
            while (YearCnt <= 10)
            {
                ltbDataSet.RSYearArray[YearCnt] = 0;
                ltbDataSet.StockYearArray[YearCnt] = 0;
                ltbDataSet.SafetyYearArray[YearCnt] = 0;
                YearCnt = YearCnt + 1;
            }
        }
        public static void GetChart(LtbDataSet ltbDataSet)
        {
            System.Web.UI.DataVisualization.Charting.Chart chart = new System.Web.UI.DataVisualization.Charting.Chart()
            {
                Height = 300,
                Width = 900,
                ImageType = System.Web.UI.DataVisualization.Charting.ChartImageType.Png
            };
            System.Web.UI.DataVisualization.Charting.ChartArea chartArea = chart.ChartAreas.Add("Stock");
            chartArea.Area3DStyle.Enable3D = true;

            System.Web.UI.DataVisualization.Charting.Series RS = chart.Series.Add("0");
            System.Web.UI.DataVisualization.Charting.Series Stock = chart.Series.Add("1");
            System.Web.UI.DataVisualization.Charting.Series Safety = chart.Series.Add("2");
            RS.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;
            Stock.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;
            Safety.ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.StackedColumn;

            chart.Series["0"].Points.DataBindXY(xValues, ltbDataSet.RSYearArray);
            chart.Series["0"].Color = System.Drawing.Color.Green;
            chart.Series["1"].Points.DataBindXY(xValues, ltbDataSet.StockYearArray);
            chart.Series["1"].Color = System.Drawing.Color.Blue;
            chart.Series["2"].Points.DataBindXY(xValues, ltbDataSet.SafetyYearArray);
            chart.Series["2"].Color = System.Drawing.Color.Red;
            MemoryStream ms = new MemoryStream();

            chart.SaveImage(ms);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            ltbDataSet.LtbChart = image;

        }
        static string[] xValues = {
		"LTB",
		"+1Year",
		"+2Year",
		"+3Year",
		"+4Year",
		"+5Year",
		"+6Year",
		"+7Year",
		"+8Year",
		"+9Year",
		"EoS"
	};
    }
}
