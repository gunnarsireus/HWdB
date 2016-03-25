using CenterSpace.NMath.Core;
using HWdB.Model;
using LTBCore;
using System;
using System.IO;

namespace HWdB.Utils
{
    class Calculator
    {
        private static readonly int[] InstalledBasePerYear = new int[LtbCommon.MaxYear + 1];
        private static readonly double[] FailureRatePerYear = new double[LtbCommon.MaxYear + 1];
        private static readonly double[] RepairLossPerYear = new double[LtbCommon.MaxYear + 1];
        private static readonly int[] RegionalStocksPerYear = new int[LtbCommon.MaxYear + 1];

        private static string _stock = string.Empty;
        private static string _safety = string.Empty;
        private static string _failed = string.Empty;
        private static string _repaired = string.Empty;
        private static string _lost = string.Empty;
        private static string _total = string.Empty;
        private static MemoryStream _ltbChart;

        private static double _confidenceLevelConverted;
        private static DateTime LifeTimeBuy { get; set; }
        private static DateTime EndOfService { get; set; }

        public static int RoundUpInt(double x, int y)
        {
            return Convert.ToInt32(Math.Round(x + 0.49999999999, y));
        }
        private static bool IsLeapYear(long year)
        {
            return (year > 0) && (year % 4) == 0 && !((year % 100) == 0 && (year % 400) != 0);
        }

        private static long CountLeaps(long year)
        {
            return (year - 1) / 4 - (year - 1) / 100 + (year - 1) / 400;
        }
        private static int ServiceDays
        {
            get
            {
                return Convert.ToInt32(DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, LifeTimeBuy, EndOfService));
            }
        }

        private static int ServiceYears
        {
            get
            {
                if (LifeTimeBuy.Year == EndOfService.Year)
                {
                    return 0;
                }

                var newYear = Convert.ToDateTime(LifeTimeBuy.Year + "-01-01");
                if (IsLeapYear(LifeTimeBuy.Year) &
                    DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, newYear, LifeTimeBuy) < 59)
                {
                    return
                        Convert.ToInt32(
                            (DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, LifeTimeBuy, EndOfService) +
                             CountLeaps(LifeTimeBuy.Year) - CountLeaps(EndOfService.Year) - 2) / 365);
                }
                return
                    Convert.ToInt32((DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, LifeTimeBuy, EndOfService) +
                                     CountLeaps(LifeTimeBuy.Year) - CountLeaps(EndOfService.Year) - 1) / 365);
            }
        }

        static void ConvertFromViewModel(LtbDataSet ltbDataSet, out double getConfidenceLevelFromNormsInv)
        {
            LifeTimeBuy = Convert.ToDateTime(ltbDataSet.LTBDate);
            EndOfService = Convert.ToDateTime(ltbDataSet.EOSDate);

            var cnt = 0;
            getConfidenceLevelFromNormsInv = 0.0;

            switch (ltbDataSet.ConfidenceLevel)
            {
                //Confidence Level

                case "60%":
                    getConfidenceLevelFromNormsInv = LtbCommon.GetConfidenceLevelFromNormsInv(0.6);
                    _confidenceLevelConverted = 0.6;

                    break;
                case "70%":
                    getConfidenceLevelFromNormsInv = LtbCommon.GetConfidenceLevelFromNormsInv(0.7);
                    _confidenceLevelConverted = 0.7;

                    break;
                case "80%":
                    getConfidenceLevelFromNormsInv = LtbCommon.GetConfidenceLevelFromNormsInv(0.8);
                    _confidenceLevelConverted = 0.8;

                    break;
                case "90%":
                    getConfidenceLevelFromNormsInv = LtbCommon.GetConfidenceLevelFromNormsInv(0.9);
                    _confidenceLevelConverted = 0.9;

                    break;
                case "95%":
                    getConfidenceLevelFromNormsInv = LtbCommon.GetConfidenceLevelFromNormsInv(0.95);
                    _confidenceLevelConverted = 0.95;

                    break;
                case "99,5%":
                    getConfidenceLevelFromNormsInv = LtbCommon.GetConfidenceLevelFromNormsInv(0.995);
                    _confidenceLevelConverted = 0.995;

                    break;

            }

            cnt = 0;

            while (cnt <= ServiceYears)
            {
                switch (cnt)
                {
                    case 0:
                        InstalledBasePerYear[0] = Convert.ToInt32(ltbDataSet.IB0);
                        RegionalStocksPerYear[0] = Convert.ToInt32(ltbDataSet.RS0);
                        FailureRatePerYear[0] = Convert.ToDouble(ltbDataSet.FR0);
                        RepairLossPerYear[0] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL0) / 100);

                        break;
                    case 1:
                        InstalledBasePerYear[1] = Convert.ToInt32(ltbDataSet.IB1);
                        RegionalStocksPerYear[1] = Convert.ToInt32(ltbDataSet.RS1);
                        FailureRatePerYear[1] = Convert.ToDouble(ltbDataSet.FR1);
                        RepairLossPerYear[1] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL1) / 100);

                        break;
                    case 2:
                        InstalledBasePerYear[2] = Convert.ToInt32(ltbDataSet.IB2);
                        RegionalStocksPerYear[2] = Convert.ToInt32(ltbDataSet.RS2);
                        FailureRatePerYear[2] = Convert.ToDouble(ltbDataSet.FR2);
                        RepairLossPerYear[2] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL2) / 100);

                        break;
                    case 3:
                        InstalledBasePerYear[3] = Convert.ToInt32(ltbDataSet.IB3);
                        RegionalStocksPerYear[3] = Convert.ToInt32(ltbDataSet.RS3);
                        FailureRatePerYear[3] = Convert.ToDouble(ltbDataSet.FR3);
                        RepairLossPerYear[3] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL3) / 100);

                        break;
                    case 4:
                        InstalledBasePerYear[4] = Convert.ToInt32(ltbDataSet.IB4);
                        RegionalStocksPerYear[4] = Convert.ToInt32(ltbDataSet.RS4);
                        FailureRatePerYear[4] = Convert.ToDouble(ltbDataSet.FR4);
                        RepairLossPerYear[4] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL4) / 100);

                        break;
                    case 5:
                        InstalledBasePerYear[5] = Convert.ToInt32(ltbDataSet.IB5);
                        RegionalStocksPerYear[5] = Convert.ToInt32(ltbDataSet.RS5);
                        FailureRatePerYear[5] = Convert.ToDouble(ltbDataSet.FR5);
                        RepairLossPerYear[5] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL5) / 100);

                        break;
                    case 6:
                        InstalledBasePerYear[6] = Convert.ToInt32(ltbDataSet.IB6);
                        RegionalStocksPerYear[6] = Convert.ToInt32(ltbDataSet.RS6);
                        FailureRatePerYear[6] = Convert.ToDouble(ltbDataSet.FR6);
                        RepairLossPerYear[6] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL6) / 100);

                        break;
                    case 7:
                        InstalledBasePerYear[7] = Convert.ToInt32(ltbDataSet.IB7);
                        RegionalStocksPerYear[7] = Convert.ToInt32(ltbDataSet.RS7);
                        FailureRatePerYear[7] = Convert.ToDouble(ltbDataSet.FR7);
                        RepairLossPerYear[7] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL7) / 100);

                        break;
                    case 8:
                        InstalledBasePerYear[8] = Convert.ToInt32(ltbDataSet.IB8);
                        RegionalStocksPerYear[8] = Convert.ToInt32(ltbDataSet.RS8);
                        FailureRatePerYear[8] = Convert.ToDouble(ltbDataSet.FR8);
                        RepairLossPerYear[8] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL8) / 100);

                        break;
                    case 9:
                        InstalledBasePerYear[9] = Convert.ToInt32(ltbDataSet.IB9);
                        RegionalStocksPerYear[9] = Convert.ToInt32(ltbDataSet.RS9);
                        FailureRatePerYear[9] = Convert.ToDouble(ltbDataSet.FR9);
                        RepairLossPerYear[9] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL9) / 100);

                        break;
                }
                cnt += 1;
            }
            ClearRemains(ltbDataSet, cnt);

        }

        static void ClearRemains(LtbDataSet ltbDataSet, int startYear)
        {
            var cnt = startYear;
            while (cnt <= LtbCommon.MaxYear)
            {
                switch (cnt)
                {
                    case 0:
                        ltbDataSet.IB0 = string.Empty;
                        ltbDataSet.RS0 = string.Empty;
                        ltbDataSet.FR0 = string.Empty;
                        ltbDataSet.RL0 = string.Empty;
                        ltbDataSet.IB1 = string.Empty;
                        ltbDataSet.RS1 = string.Empty;
                        ltbDataSet.FR1 = string.Empty;
                        ltbDataSet.RL1 = string.Empty;
                        break;
                    case 1:
                        ltbDataSet.IB2 = string.Empty;
                        ltbDataSet.RS2 = string.Empty;
                        ltbDataSet.FR2 = string.Empty;
                        ltbDataSet.RL2 = string.Empty;
                        break;
                    case 2:
                        ltbDataSet.IB3 = string.Empty;
                        ltbDataSet.RS3 = string.Empty;
                        ltbDataSet.FR3 = string.Empty;
                        ltbDataSet.RL3 = string.Empty;
                        break;
                    case 3:
                        ltbDataSet.IB4 = string.Empty;
                        ltbDataSet.RS4 = string.Empty;
                        ltbDataSet.FR4 = string.Empty;
                        ltbDataSet.RL4 = string.Empty;
                        break;
                    case 4:
                        ltbDataSet.IB5 = string.Empty;
                        ltbDataSet.RS5 = string.Empty;
                        ltbDataSet.FR5 = string.Empty;
                        ltbDataSet.RL5 = string.Empty;
                        break;
                    case 5:
                        ltbDataSet.IB6 = string.Empty;
                        ltbDataSet.RS6 = string.Empty;
                        ltbDataSet.FR6 = string.Empty;
                        ltbDataSet.RL6 = string.Empty;
                        break;
                    case 6:
                        ltbDataSet.IB7 = string.Empty;
                        ltbDataSet.RS7 = string.Empty;
                        ltbDataSet.FR7 = string.Empty;
                        ltbDataSet.RL7 = string.Empty;
                        break;
                    case 7:
                        ltbDataSet.IB8 = string.Empty;
                        ltbDataSet.RS8 = string.Empty;
                        ltbDataSet.FR8 = string.Empty;
                        ltbDataSet.RL8 = string.Empty;
                        break;
                    case 8:
                        ltbDataSet.IB9 = string.Empty;
                        ltbDataSet.RS9 = string.Empty;
                        ltbDataSet.FR9 = string.Empty;
                        ltbDataSet.RL9 = string.Empty;
                        break;
                    case 9:
                        ltbDataSet.IB10 = string.Empty;
                        break;
                    case 10:
                        break;
                }
                cnt += 1;
            }
        }

        public static void CalculateLtb(LtbDataSet ltbDataSet)
        {

            double getConfidenceLevelFromNormsInv;
            Presenter.ClearResult(ltbDataSet);
            NMathConfiguration.LogLocation = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            NMathConfiguration.Init();

            if (ltbDataSet.RepairLeadTime < 1 || ltbDataSet.RepairLeadTime > 365)
            {
                ltbDataSet.InfoText = "Error: 2 <= Repair Lead Time <=365;";
                return;
            }

            if (ltbDataSet.RepairLeadTime > ltbDataSet.ServiceDays)
            {
                Presenter.ClearResult(ltbDataSet);
                ltbDataSet.InfoText = "Error: Repair Lead Time cannot be longer than Service Period. Please change EoS or Repair Lead Time";
                return;
            }

            if (ltbDataSet.ServiceDays > LtbCommon.MaxServiceDays)
            {
                Presenter.ClearResult(ltbDataSet);
                ltbDataSet.InfoText = "Error: The Service Period cannot be longer than 10 years. Please change EoS or LTB.";
                return;
            }

            var nbrOfSamples = RoundUpInt(ltbDataSet.ServiceDays / (double)ltbDataSet.RepairLeadTime, 0);

            ConvertFromViewModel(ltbDataSet, out getConfidenceLevelFromNormsInv);

            var ltb = new LtbCommon(900);
            ltb.LtbWorker(nbrOfSamples,
                ServiceDays,
                ServiceYears,
                ltbDataSet.RepairLeadTime,
                out _stock,
                out _safety,
                out _failed,
                out _repaired,
                out _lost,
                out _total,
                getConfidenceLevelFromNormsInv,
                _confidenceLevelConverted,
                InstalledBasePerYear,
                FailureRatePerYear,
                RepairLossPerYear,
                RegionalStocksPerYear,
                out _ltbChart,
                NMathConfiguration.LogLocation);

            ltbDataSet.LtbChart = ltbDataSet.ConvertToBitmapImage(_ltbChart);
            ltbDataSet.Stock = _stock;
            ltbDataSet.Safety = _safety;
            ltbDataSet.TotalStock = _total;
            ltbDataSet.Failed = _failed;
            ltbDataSet.Repaired = _repaired;
            ltbDataSet.Lost = _lost;
        }
    }
}
