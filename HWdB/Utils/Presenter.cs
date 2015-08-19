using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HWdB.Model;
using System.Windows.Media.Imaging;
using System.IO;
using LTBCore;

namespace HWdB.Utils
{
    class Presenter
    {
        public static void InitLabels(LtbDataSet ltbDataSet)
        {
            ltbDataSet.YearLabel0 = "LTB";
            ltbDataSet.YearLabel1 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(1).Year.ToString();
            ltbDataSet.YearLabel2 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(2).Year.ToString();
            ltbDataSet.YearLabel3 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(3).Year.ToString();
            ltbDataSet.YearLabel4 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(4).Year.ToString();
            ltbDataSet.YearLabel5 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(5).Year.ToString();
            ltbDataSet.YearLabel6 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(6).Year.ToString();
            ltbDataSet.YearLabel7 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(7).Year.ToString();
            ltbDataSet.YearLabel8 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(8).Year.ToString();
            ltbDataSet.YearLabel9 = Convert.ToDateTime(ltbDataSet.LTBDate).AddYears(9).Year.ToString();
        }
        public static void UpdateInputViewModel(LtbDataSet ltbDataSet)
        {
            if (ltbDataSet.EOSDate == null || ltbDataSet.LTBDate == null) return;
            InitLabels(ltbDataSet);
            var cnt = 0;
            cnt = 0;
            var eosFound = false;
            while (cnt <= Mathematics.ServiceYears(ltbDataSet))
            {
                switch (cnt)
                {
                    case 0:
                        if (ltbDataSet.IB1 == "EoS")
                        {
                            eosFound = true;
                            ltbDataSet.IB1 = " ";
                            ltbDataSet.FR1 = " ";
                            ltbDataSet.RL1 = " ";
                            ltbDataSet.RS1 = " ";
                        }
                        ltbDataSet.IB1IsEnabled = true;
                        break;
                    case 1:
                        if (ltbDataSet.IB2 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            ltbDataSet.IB2 = " ";
                            ltbDataSet.FR2 = " ";
                            ltbDataSet.RL2 = " ";
                            ltbDataSet.RS2 = " ";
                        }
                        ltbDataSet.IB2IsEnabled = true;
                        break;
                    case 2:
                        if (ltbDataSet.IB3 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            ltbDataSet.IB3 = " ";
                            ltbDataSet.FR3 = " ";
                            ltbDataSet.RL3 = " ";
                            ltbDataSet.RS3 = " ";
                        }
                        ltbDataSet.IB3IsEnabled = true;
                        break; ;
                    case 3:
                        if (ltbDataSet.IB4 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            ltbDataSet.IB4 = " ";
                            ltbDataSet.FR4 = " ";
                            ltbDataSet.RL4 = " ";
                            ltbDataSet.RS4 = " ";
                        }
                        ltbDataSet.IB4IsEnabled = true;
                        break; ;
                    case 4:
                        if (ltbDataSet.IB5 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            ltbDataSet.IB5 = " ";
                            ltbDataSet.FR5 = " ";
                            ltbDataSet.RL5 = " ";
                            ltbDataSet.RS5 = " ";
                        }
                        ltbDataSet.IB5IsEnabled = true;
                        break;
                    case 5:
                        if (ltbDataSet.IB6 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            ltbDataSet.IB6 = " ";
                            ltbDataSet.FR6 = " ";
                            ltbDataSet.RL6 = " ";
                            ltbDataSet.RS6 = " ";
                        }
                        ltbDataSet.IB6IsEnabled = true;
                        break;
                    case 6:
                        if (ltbDataSet.IB7 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            ltbDataSet.IB7 = " ";
                            ltbDataSet.FR7 = " ";
                            ltbDataSet.RL7 = " ";
                            ltbDataSet.RS7 = " ";
                        }
                        ltbDataSet.IB7IsEnabled = true;
                        break;
                    case 7:
                        if (ltbDataSet.IB8 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            ltbDataSet.IB8 = " ";
                            ltbDataSet.FR8 = " ";
                            ltbDataSet.RL8 = " ";
                            ltbDataSet.RS8 = " ";
                        }
                        ltbDataSet.IB8IsEnabled = true;
                        break;
                    case 8:
                        if (ltbDataSet.IB9 == "EoS" || eosFound)
                        {
                            eosFound = true;
                            ltbDataSet.IB9 = " ";
                            ltbDataSet.FR9 = " ";
                            ltbDataSet.RL9 = " ";
                            ltbDataSet.RS9 = " ";
                        }
                        ltbDataSet.IB9IsEnabled = true;
                        break;
                    case 9:
                        if (ltbDataSet.IB10 == "EoS" || eosFound)
                        {
                            ltbDataSet.IB10 = " ";
                        }
                        break;
                }
                cnt += 1;
            }

            switch (Mathematics.ServiceYears(ltbDataSet))
            {
                case 0:
                    ltbDataSet.IB1 = "EoS";
                    ltbDataSet.RS1 = string.Empty;
                    ltbDataSet.RL1 = string.Empty;
                    ltbDataSet.FR1 = string.Empty;
                    ltbDataSet.IB1IsEnabled = false;
                    ltbDataSet.RL1IsEnabled = false;
                    break;
                case 1:
                    ltbDataSet.IB2 = "EoS";
                    ltbDataSet.RS2 = string.Empty;
                    ltbDataSet.RL2 = string.Empty;
                    ltbDataSet.FR2 = string.Empty;
                    ltbDataSet.IB2IsEnabled = false;
                    ltbDataSet.RL2IsEnabled = false;
                    break;
                case 2:
                    ltbDataSet.IB3 = "EoS";
                    ltbDataSet.RS3 = string.Empty;
                    ltbDataSet.RL3 = string.Empty;
                    ltbDataSet.FR3 = string.Empty;
                    ltbDataSet.IB3IsEnabled = false;
                    ltbDataSet.RL3IsEnabled = false;
                    break;
                case 3:
                    ltbDataSet.IB4 = "EoS";
                    ltbDataSet.RS4 = string.Empty;
                    ltbDataSet.RL4 = string.Empty;
                    ltbDataSet.FR4 = string.Empty;
                    ltbDataSet.RL4IsEnabled = false;
                    ltbDataSet.IB4IsEnabled = false;
                    break;
                case 4:
                    ltbDataSet.IB5 = "EoS";
                    ltbDataSet.RS5 = string.Empty;
                    ltbDataSet.RL5 = string.Empty;
                    ltbDataSet.FR5 = string.Empty;
                    ltbDataSet.IB5IsEnabled = false;
                    ltbDataSet.RL5IsEnabled = false;
                    break;
                case 5:
                    ltbDataSet.IB6 = "EoS";
                    ltbDataSet.RS6 = string.Empty;
                    ltbDataSet.RL6 = string.Empty;
                    ltbDataSet.FR6 = string.Empty;
                    ltbDataSet.IB6IsEnabled = false;
                    ltbDataSet.RL6IsEnabled = false;
                    break;
                case 6:
                    ltbDataSet.IB7 = "EoS";
                    ltbDataSet.RS7 = string.Empty;
                    ltbDataSet.RL7 = string.Empty;
                    ltbDataSet.FR7 = string.Empty;
                    ltbDataSet.IB7IsEnabled = false;
                    ltbDataSet.RL7IsEnabled = false;
                    break;
                case 7:
                    ltbDataSet.IB8 = "EoS";
                    ltbDataSet.RS8 = string.Empty;
                    ltbDataSet.RL8 = string.Empty;
                    ltbDataSet.FR8 = string.Empty;
                    ltbDataSet.IB8IsEnabled = false;
                    ltbDataSet.RL8IsEnabled = false;
                    break;
                case 8:
                    ltbDataSet.IB9 = "EoS";
                    ltbDataSet.RS9 = string.Empty;
                    ltbDataSet.RL9 = string.Empty;
                    ltbDataSet.FR9 = string.Empty;
                    ltbDataSet.IB9IsEnabled = false;
                    ltbDataSet.RL9IsEnabled = false;
                    break;
                case 9:
                    ltbDataSet.IB10 = "EoS";
                    break;
                case 10:
                    break;
                default: break;
            }
            while (cnt <= LTBCommon.MaxYear)
            {
                switch (cnt)
                {
                    case 1:
                        if (Mathematics.ServiceYears(ltbDataSet) != 0)
                        {
                            ltbDataSet.IB1 = string.Empty;
                            ltbDataSet.FR1 = string.Empty;
                            ltbDataSet.RS1 = string.Empty;
                            ltbDataSet.RL1 = string.Empty;
                            ltbDataSet.IB1IsEnabled = false;
                            ltbDataSet.RL1IsEnabled = false;
                        }
                        break;
                    case 2:
                        if (Mathematics.ServiceYears(ltbDataSet) != 1)
                        {
                            ltbDataSet.IB2 = string.Empty;
                            ltbDataSet.FR2 = string.Empty;
                            ltbDataSet.RS2 = string.Empty;
                            ltbDataSet.RL2 = string.Empty;
                            ltbDataSet.IB2IsEnabled = false;
                            ltbDataSet.RL2IsEnabled = false;
                        }
                        break;
                    case 3:
                        if (Mathematics.ServiceYears(ltbDataSet) != 2)
                        {
                            ltbDataSet.IB3 = string.Empty;
                            ltbDataSet.FR3 = string.Empty;
                            ltbDataSet.RS3 = string.Empty;
                            ltbDataSet.RL3 = string.Empty;
                            ltbDataSet.IB3IsEnabled = false;
                            ltbDataSet.RL3IsEnabled = false;
                        }
                        break;
                    case 4:
                        if (Mathematics.ServiceYears(ltbDataSet) != 3)
                        {
                            ltbDataSet.IB4 = string.Empty;
                            ltbDataSet.FR4 = string.Empty;
                            ltbDataSet.RS4 = string.Empty;
                            ltbDataSet.RL4 = string.Empty;
                            ltbDataSet.IB4IsEnabled = false;
                            ltbDataSet.RL4IsEnabled = false;
                        }
                        break;
                    case 5:
                        if (Mathematics.ServiceYears(ltbDataSet) != 4)
                        {
                            ltbDataSet.IB5 = string.Empty;
                            ltbDataSet.FR5 = string.Empty;
                            ltbDataSet.RS5 = string.Empty;
                            ltbDataSet.RL5 = string.Empty;
                            ltbDataSet.IB5IsEnabled = false;
                            ltbDataSet.RL5IsEnabled = false;
                        }
                        break;
                    case 6:
                        if (Mathematics.ServiceYears(ltbDataSet) != 5)
                        {
                            ltbDataSet.IB6 = string.Empty;
                            ltbDataSet.FR6 = string.Empty;
                            ltbDataSet.RS6 = string.Empty;
                            ltbDataSet.RL6 = string.Empty;
                            ltbDataSet.IB6IsEnabled = false;
                            ltbDataSet.RL6IsEnabled = false;
                        }
                        break;
                    case 7:
                        if (Mathematics.ServiceYears(ltbDataSet) != 6)
                        {
                            ltbDataSet.FR7 = string.Empty;
                            ltbDataSet.RS7 = string.Empty;
                            ltbDataSet.RL7 = string.Empty;
                            ltbDataSet.IB7 = string.Empty;
                            ltbDataSet.IB7IsEnabled = false;
                            ltbDataSet.RL7IsEnabled = false;
                        }
                        break;
                    case 8:
                        if (Mathematics.ServiceYears(ltbDataSet) != 7)
                        {
                            ltbDataSet.IB8 = string.Empty;
                            ltbDataSet.FR8 = string.Empty;
                            ltbDataSet.RS8 = string.Empty;
                            ltbDataSet.RL8 = string.Empty;
                            ltbDataSet.IB8IsEnabled = false;
                            ltbDataSet.RL8IsEnabled = false;
                        }
                        break;
                    case 9:
                        if (Mathematics.ServiceYears(ltbDataSet) != 8)
                        {
                            ltbDataSet.IB9 = string.Empty;
                            ltbDataSet.FR9 = string.Empty;
                            ltbDataSet.RS9 = string.Empty;
                            ltbDataSet.RL9 = string.Empty;
                            ltbDataSet.IB9IsEnabled = false;
                            ltbDataSet.RL9IsEnabled = false;
                        }
                        break;
                    case 10:
                        if (Mathematics.ServiceYears(ltbDataSet) != 9)
                        {
                            ltbDataSet.IB10 = string.Empty;
                        }
                        break;
                }
                cnt += 1;
            }
            AdjustRepair(ltbDataSet);
        }
        static void AdjustRepair(LtbDataSet ltbDataSet)
        {
            int Cnt = 0;
            while (Cnt <= Mathematics.ServiceYears(ltbDataSet))
            {
                switch (Cnt)
                {
                    case 0:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL0IsEnabled = false;
                            ltbDataSet.RL0 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL0IsEnabled = true;
                        }
                        break;
                    case 1:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL1IsEnabled = false;
                            ltbDataSet.RL1 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL1IsEnabled = true;
                        }
                        break;
                    case 2:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL2IsEnabled = false;
                            ltbDataSet.RL2 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL2IsEnabled = true;
                        }
                        break;
                    case 3:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL3IsEnabled = false;
                            ltbDataSet.RL3 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL3IsEnabled = true;
                        }
                        break;
                    case 4:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL4IsEnabled = false;
                            ltbDataSet.RL4 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL4IsEnabled = true;
                        }
                        break;
                    case 5:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL5IsEnabled = false;
                            ltbDataSet.RL5 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL5IsEnabled = true;
                        }
                        break;
                    case 6:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL6IsEnabled = false;
                            ltbDataSet.RL6 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL6IsEnabled = true;
                        }
                        break;
                    case 7:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL7IsEnabled = false;
                            ltbDataSet.RL7 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL7IsEnabled = true;
                        }
                        break;
                    case 8:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL8IsEnabled = false;
                            ltbDataSet.RL8 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL8IsEnabled = true;
                        }
                        break;
                    case 9:
                        if (!ltbDataSet.RepairPossible)
                        {
                            ltbDataSet.RL9IsEnabled = false;
                            ltbDataSet.RL9 = "100";
                        }
                        else
                        {
                            ltbDataSet.RL9IsEnabled = true;
                        }

                        break;
                }
                Cnt += 1;
            }
        }
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
        public static BitmapImage GetChart(LtbDataSet ltbDataSet)
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
            return image;
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
