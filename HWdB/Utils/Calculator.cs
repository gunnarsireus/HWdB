using CenterSpace.NMath.Core;
using CenterSpace.NMath.Stats;
using HWdB.Model;
using LTBCore;
using System;

namespace HWdB.Utils
{
    class Calculator
    {
        static string GetConfidenceLLevelFromAverage(double confidenceLevel, double average)
        {
            string functionReturnValue = null;
            if (average <= 0)
            {
                functionReturnValue = " (100%)";
                return functionReturnValue;
            }
            if (average < 2492000)
            {
                var poisson = new PoissonDistribution(average);
                double tmp = 0;
                NMathConfiguration.Init();
                tmp = poisson.CDF(Math.Round(average, 0));
                //tmp = 0.75
                if (tmp > confidenceLevel)
                {
                    functionReturnValue = " (" + (100 * Math.Round(tmp, 2)).ToString() + "%)";
                }
                else
                {
                    functionReturnValue = string.Empty;
                }
            }
            else
            {
                functionReturnValue = string.Empty;
            }
            return functionReturnValue;
        }

        static string GetConfidenceLevelFromStock(double stock, double safety)
        {
            string functionReturnValue = null;
            if (stock == 0)
            {
                functionReturnValue = " (100%)";
                return functionReturnValue;
            }
            if (stock < 2492000)
            {
                var poisson = new PoissonDistribution(stock);
                double tmp = 0;
                NMathConfiguration.Init();
                tmp = poisson.CDF(Math.Round(stock + safety, 0));
                functionReturnValue = " (" + (100 * Math.Round(tmp, 4)).ToString() + "%)";
            }
            else
            {
                functionReturnValue = string.Empty;
            }
            return functionReturnValue;
        }

        static long GetSafetyFromAverage(double confidenceLevel, double average)
        {
            long functionReturnValue = 0;
            if (average <= 0)
            {
                functionReturnValue = 0;
                return functionReturnValue;
            }

            if (average < 2492000)
            {
                var poisson = new PoissonDistribution(average);
                long K = 0;
                K = Convert.ToInt64(Math.Round(average, 0));

                while (poisson.CDF(K) < confidenceLevel)
                {
                    K = K + 1;
                }

                functionReturnValue = Convert.ToInt64(Math.Round(K - average, 0));
                if (functionReturnValue < 0)
                    functionReturnValue = 0;
            }
            else
            {
                functionReturnValue = Mathematics.RoundLong(Mathematics.NormSInv(confidenceLevel) * Mathematics.Sqr(average), 0);
            }
            return functionReturnValue;
        }

        static long GetSafetyFromGamma(double confidenceLevel, double fromAverage, double returned)
        {
            var returnValue = (long)(Mathematics.Calcreserve((int)Mathematics.RoundLong(returned, 0), 1, confidenceLevel) - fromAverage);
            if (returnValue < 0) returnValue = 0;
            if (returnValue > fromAverage) returnValue = (long)fromAverage;

            return returnValue;
        }


        static void ConvertFromViewModel(LtbDataSet ltbDataSet, out double ConfidenceLevelFromNormsInv)
        {
            var serviceDays = Convert.ToDouble(ltbDataSet.ServiceDays);
            var finalYear = Mathematics.ServiceYears(ltbDataSet);
            var leadDays = Convert.ToDouble(ltbDataSet.RepairLeadTime);

            var cnt = 0;
            ConfidenceLevelFromNormsInv = 0.0;

            switch (ltbDataSet.ConfidenceLevel)
            {
                //Confidence Level

                case "60%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.6);
                    _confidenceLevelDbl = 0.6;

                    break;
                case "70%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.7);
                    _confidenceLevelDbl = 0.7;

                    break;
                case "80%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.8);
                    _confidenceLevelDbl = 0.8;

                    break;
                case "90%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.9);
                    _confidenceLevelDbl = 0.9;

                    break;
                case "95%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.95);
                    _confidenceLevelDbl = 0.95;

                    break;
                case "99,5%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.995);
                    _confidenceLevelDbl = 0.995;

                    break;

            }

            cnt = 0;

            while (cnt <= finalYear)
            {
                switch (cnt)
                {
                    case 0:
                        _installedBaseIn[0] = Convert.ToInt64(ltbDataSet.IB0);
                        _regionalStocksIn[0] = Convert.ToInt64(ltbDataSet.RS0);
                        _failureRateIn[0] = Convert.ToDouble(ltbDataSet.FR0);
                        _repairLossIn[0] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL0) / 100);

                        break;
                    case 1:
                        _installedBaseIn[1] = Convert.ToInt64(ltbDataSet.IB1);
                        _regionalStocksIn[1] = Convert.ToInt64(ltbDataSet.RS1);
                        _failureRateIn[1] = Convert.ToDouble(ltbDataSet.FR1);
                        _repairLossIn[1] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL1) / 100);

                        break;
                    case 2:
                        _installedBaseIn[2] = Convert.ToInt64(ltbDataSet.IB2);
                        _regionalStocksIn[2] = Convert.ToInt64(ltbDataSet.RS2);
                        _failureRateIn[2] = Convert.ToDouble(ltbDataSet.FR2);
                        _repairLossIn[2] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL2) / 100);

                        break;
                    case 3:
                        _installedBaseIn[3] = Convert.ToInt64(ltbDataSet.IB3);
                        _regionalStocksIn[3] = Convert.ToInt64(ltbDataSet.RS3);
                        _failureRateIn[3] = Convert.ToDouble(ltbDataSet.FR3);
                        _repairLossIn[3] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL3) / 100);

                        break;
                    case 4:
                        _installedBaseIn[4] = Convert.ToInt64(ltbDataSet.IB4);
                        _regionalStocksIn[4] = Convert.ToInt64(ltbDataSet.RS4);
                        _failureRateIn[4] = Convert.ToDouble(ltbDataSet.FR4);
                        _repairLossIn[4] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL4) / 100);

                        break;
                    case 5:
                        _installedBaseIn[5] = Convert.ToInt64(ltbDataSet.IB5);
                        _regionalStocksIn[5] = Convert.ToInt64(ltbDataSet.RS5);
                        _failureRateIn[5] = Convert.ToDouble(ltbDataSet.FR5);
                        _repairLossIn[5] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL5) / 100);

                        break;
                    case 6:
                        _installedBaseIn[6] = Convert.ToInt64(ltbDataSet.IB6);
                        _regionalStocksIn[6] = Convert.ToInt64(ltbDataSet.RS6);
                        _failureRateIn[6] = Convert.ToDouble(ltbDataSet.FR6);
                        _repairLossIn[6] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL6) / 100);

                        break;
                    case 7:
                        _installedBaseIn[7] = Convert.ToInt64(ltbDataSet.IB7);
                        _regionalStocksIn[7] = Convert.ToInt64(ltbDataSet.RS7);
                        _failureRateIn[7] = Convert.ToDouble(ltbDataSet.FR7);
                        _repairLossIn[7] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL7) / 100);

                        break;
                    case 8:
                        _installedBaseIn[8] = Convert.ToInt64(ltbDataSet.IB8);
                        _regionalStocksIn[8] = Convert.ToInt64(ltbDataSet.RS8);
                        _failureRateIn[8] = Convert.ToDouble(ltbDataSet.FR8);
                        _repairLossIn[8] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL8) / 100);

                        break;
                    case 9:
                        _installedBaseIn[9] = Convert.ToInt64(ltbDataSet.IB9);
                        _regionalStocksIn[9] = Convert.ToInt64(ltbDataSet.RS9);
                        _failureRateIn[9] = Convert.ToDouble(ltbDataSet.FR9);
                        _repairLossIn[9] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL9) / 100);

                        break;
                }
                cnt += 1;
            }
            ClearRemains(ltbDataSet, cnt);

        }

        static void ClearRemains(LtbDataSet ltbDataSet, int startYear)
        {
            var cnt = startYear;
            while (cnt <= LTBCommon.MaxYear)
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

        static long _fromGamma;
        static long _fromAverage;
        //const long MaxYear = 10;
        const long MinRepairLeadTime = 2;
        const long MaxRepairLeadTime = 365;
        const long MaxServiceDays = LTBCommon.MaxYear * 365 + 2;
        const long MaxDayArr = MaxServiceDays + 365;
        const long MaxLTArr = MaxServiceDays / MinRepairLeadTime + 2;
        static long[] _installedBaseIn = new long[LTBCommon.MaxYear + 1];
        static double[] _failureRateIn = new double[LTBCommon.MaxYear + 1];
        static double[] _repairLossIn = new double[LTBCommon.MaxYear + 1];
        static long[] _regionalStocksIn = new long[LTBCommon.MaxYear + 1];
        static long[] _regionalStockArray = new long[MaxLTArr + 1];
        static long[] _regionalStockDayArray = new long[MaxDayArr + 365];
        static double[] _stockDayArray = new double[MaxDayArr + 365];
        static double[] _returnedDayArray = new double[MaxDayArr + 365];
        static double[] _sumDemandDayArray = new double[MaxDayArr + 365];
        static double[] _installedBaseArray = new double[MaxLTArr + 1];
        static double[] _failureRateArray = new double[MaxLTArr + 1];
        static double[] _repairLossLArray = new double[MaxLTArr + 1];
        static double[] _repairLossDayArray = new double[MaxDayArr + 365];
        static double[] _stockArray = new double[MaxLTArr + 1];
        static double[] _returnedArray = new double[MaxLTArr + 2];
        static double[] _demandArray = new double[MaxLTArr + 1];
        static double[] _sumDemandArray = new double[MaxLTArr + 1];
        static double[] _fieldsStockRepairLeadTimeDemandArray = new double[MaxLTArr + 1];
        static double[] _repairLossArray = new double[MaxLTArr + 1];
        static double[] _sumRepairLossArray = new double[MaxLTArr + 1];
        static double[] _repairArray = new double[MaxLTArr + 1];
        static double[] _sumRepairArray = new double[MaxLTArr + 1];
        static double[] _safetyMarginArray = new double[MaxLTArr + 1];
        static double[] _safetyMarginDayArray = new double[MaxDayArr + 365];
        static double _confidenceLevelDbl;
        public static void CalculateLtb(LtbDataSet ltbDataSet)
        {

            int NbrOfSamples;
            double ConfidenceLevelFromNormsInv;
            Presenter.ClearResult(ltbDataSet);
            Presenter.ClearChartData(ltbDataSet);
            NMathConfiguration.LogLocation = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            NMathConfiguration.Init();
            long stockPresent = 0;
            long safetyPresent = 0;

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

            if (ltbDataSet.ServiceDays > MaxServiceDays)
            {
                Presenter.ClearResult(ltbDataSet);
                ltbDataSet.InfoText = "Error: The Service Period cannot be longer than 10 years. Please change EoS or LTB.";
                return;
            }

            NbrOfSamples = Mathematics.RoundUpInt(ltbDataSet.ServiceDays / ltbDataSet.RepairLeadTime, 0);

            ConvertFromViewModel(ltbDataSet, out ConfidenceLevelFromNormsInv);

            var ltb = new LTBCommon();
            ltb.LTBWorker(NbrOfSamples, ltbDataSet.ServiceDays, ltbDataSet.RepairLeadTime, Mathematics.ServiceYears(ltbDataSet), ConfidenceLevelFromNormsInv, ref _installedBaseArray, ref _regionalStockArray, ref _failureRateArray, ref _repairLossLArray,
            ref _stockArray, ref _returnedArray, ref _demandArray, ref _sumDemandArray, ref _repairLossArray, ref  _sumRepairLossArray, ref _repairArray, ref _sumRepairArray, ref _safetyMarginArray, ref _safetyMarginDayArray, ref _fieldsStockRepairLeadTimeDemandArray,
            ref _installedBaseIn, ref  _failureRateIn, ref  _repairLossIn, ref  _regionalStocksIn, ref _regionalStockDayArray, ref  _repairLossDayArray, ref  _stockDayArray, ref  _returnedDayArray, ref  _sumDemandDayArray);
            SetChartData(ltbDataSet);
            ltbDataSet.LtbChart = Presenter.GetChart(ltbDataSet);
            stockPresent = Mathematics.RoundLong(_stockArray[1], 0);
            safetyPresent = ltbDataSet.SafetyYearArray[0];

            ltbDataSet.Stock = stockPresent.ToString() + GetConfidenceLLevelFromAverage(_confidenceLevelDbl, _safetyMarginArray[1]).ToString();

            if (safetyPresent > 0)
            {
                _fromAverage = GetSafetyFromAverage(_confidenceLevelDbl, _safetyMarginArray[1]);
                ltbDataSet.Safety = safetyPresent.ToString() + GetConfidenceLevelFromStock(_safetyMarginArray[1], _fromAverage).ToString();
            }
            else
            {
                ltbDataSet.Safety = string.Empty;
            }

            ltbDataSet.TotalStock = Convert.ToString(stockPresent + safetyPresent);

            ltbDataSet.Failed = Mathematics.RoundLong(_sumDemandArray[1], 0).ToString();

            ltbDataSet.Repaired = Mathematics.RoundLong(_sumRepairArray[1] - _sumRepairLossArray[1], 0).ToString();

            ltbDataSet.Lost = ltbDataSet.RepairPossible ? Mathematics.RoundUpLong(_sumRepairLossArray[1], 0).ToString() : "Nothing";
        }

        static void SetChartData(LtbDataSet ltbDataSet)
        {
            //For Chart
            var yearCnt = 0;
            while (yearCnt <= Mathematics.ServiceYears(ltbDataSet))
            {
                ltbDataSet.RSYearArray[yearCnt] = _regionalStockDayArray[yearCnt * 365 + 1];
                ltbDataSet.StockYearArray[yearCnt] = Mathematics.RoundLong(_stockDayArray[yearCnt * 365 + 1] - _regionalStockDayArray[yearCnt * 365 + 1], 0);
                _fromAverage = Mathematics.RoundLong(GetSafetyFromAverage(_confidenceLevelDbl, _safetyMarginDayArray[yearCnt * 365 + 1]), 0);
                _fromGamma = Mathematics.RoundLong(GetSafetyFromGamma(_confidenceLevelDbl, _safetyMarginDayArray[yearCnt * 365 + 1] + _returnedDayArray[yearCnt * 365 + 1] + _fromAverage, _returnedDayArray[yearCnt * 365 + ltbDataSet.RepairLeadTime + 1]), 0);
                ltbDataSet.SafetyYearArray[yearCnt] = _fromGamma + _fromAverage;
                yearCnt = yearCnt + 1;
            }
        }
    }
}
