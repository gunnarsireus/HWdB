using CenterSpace.NMath.Core;
using CenterSpace.NMath.Stats;
using HWdB.Model;
using LTBCore;
using System;

namespace HWdB.Utils
{
    class Calculator
    {
        static string GetCLFromAverage(double CL, double Average)
        {
            string functionReturnValue = null;
            if (Average <= 0)
            {
                functionReturnValue = " (100%)";
                return functionReturnValue;
            }
            if (Average < 2492000)
            {
                PoissonDistribution Poisson = new PoissonDistribution(Average);
                double tmp = 0;
                NMathConfiguration.Init();
                tmp = Poisson.CDF(Math.Round(Average, 0));
                //tmp = 0.75
                if (tmp > CL)
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

        static string GetCLFromStock(double Stock, double Safety)
        {
            string functionReturnValue = null;
            if (Stock == 0)
            {
                functionReturnValue = " (100%)";
                return functionReturnValue;
            }
            if (Stock < 2492000)
            {
                var poisson = new PoissonDistribution(Stock);
                double tmp = 0;
                NMathConfiguration.Init();
                tmp = poisson.CDF(Math.Round(Stock + Safety, 0));
                functionReturnValue = " (" + (100 * Math.Round(tmp, 4)).ToString() + "%)";
            }
            else
            {
                functionReturnValue = string.Empty;
            }
            return functionReturnValue;
        }

        static long GetSafetyFromAverage(double CL, double Average)
        {
            long functionReturnValue = 0;
            if (Average <= 0)
            {
                functionReturnValue = 0;
                return functionReturnValue;
            }

            if (Average < 2492000)
            {
                var poisson = new PoissonDistribution(Average);
                long K = 0;
                K = Convert.ToInt64(Math.Round(Average, 0));

                while (poisson.CDF(K) < CL)
                {
                    K = K + 1;
                }

                functionReturnValue = Convert.ToInt64(Math.Round(K - Average, 0));
                if (functionReturnValue < 0)
                    functionReturnValue = 0;
            }
            else
            {
                functionReturnValue = Mathematics.RoundLong(Mathematics.NormSInv(CL) * Mathematics.Sqr(Average), 0);
            }
            return functionReturnValue;
        }

        static long GetSafetyFromGamma(double CL, double FromAverage, double Returned)
        {
            long ReturnValue;

            ReturnValue = (long)(Mathematics.calcreserve((int)Mathematics.RoundLong(Returned, 0), 1, CL) - FromAverage);
            if (ReturnValue < 0) ReturnValue = 0;
            if (ReturnValue > FromAverage) ReturnValue = (long)FromAverage;

            return ReturnValue;
        }


        static void ConvertFromViewModel(LtbDataSet ltbDataSet, out double ConfidenceLevelFromNormsInv)
        {
            double serviceDays = Convert.ToDouble(ltbDataSet.ServiceDays);
            int finalYear = Mathematics.ServiceYears(ltbDataSet);
            double leadDays = Convert.ToDouble(ltbDataSet.RepairLeadTime);

            int Cnt = 0;
            ConfidenceLevelFromNormsInv = 0.0;

            switch (ltbDataSet.ConfidenceLevel)
            {
                //Confidence Level

                case "60%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.6);
                    ConfidenceLevelDbl = 0.6;

                    break;
                case "70%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.7);
                    ConfidenceLevelDbl = 0.7;

                    break;
                case "80%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.8);
                    ConfidenceLevelDbl = 0.8;

                    break;
                case "90%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.9);
                    ConfidenceLevelDbl = 0.9;

                    break;
                case "95%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.95);
                    ConfidenceLevelDbl = 0.95;

                    break;
                case "99,5%":
                    ConfidenceLevelFromNormsInv = Mathematics.ConfidenceLevelFromNormsInv(0.995);
                    ConfidenceLevelDbl = 0.995;

                    break;

            }

            Cnt = 0;

            while (Cnt <= finalYear)
            {
                switch (Cnt)
                {
                    case 0:
                        IBin[0] = Convert.ToInt64(ltbDataSet.IB0);
                        RSin[0] = Convert.ToInt64(ltbDataSet.RS0);
                        FRin[0] = Convert.ToDouble(ltbDataSet.FR0);
                        RLin[0] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL0) / 100);

                        break;
                    case 1:
                        IBin[1] = Convert.ToInt64(ltbDataSet.IB1);
                        RSin[1] = Convert.ToInt64(ltbDataSet.RS1);
                        FRin[1] = Convert.ToDouble(ltbDataSet.FR1);
                        RLin[1] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL1) / 100);

                        break;
                    case 2:
                        IBin[2] = Convert.ToInt64(ltbDataSet.IB2);
                        RSin[2] = Convert.ToInt64(ltbDataSet.RS2);
                        FRin[2] = Convert.ToDouble(ltbDataSet.FR2);
                        RLin[2] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL2) / 100);

                        break;
                    case 3:
                        IBin[3] = Convert.ToInt64(ltbDataSet.IB3);
                        RSin[3] = Convert.ToInt64(ltbDataSet.RS3);
                        FRin[3] = Convert.ToDouble(ltbDataSet.FR3);
                        RLin[3] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL3) / 100);

                        break;
                    case 4:
                        IBin[4] = Convert.ToInt64(ltbDataSet.IB4);
                        RSin[4] = Convert.ToInt64(ltbDataSet.RS4);
                        FRin[4] = Convert.ToDouble(ltbDataSet.FR4);
                        RLin[4] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL4) / 100);

                        break;
                    case 5:
                        IBin[5] = Convert.ToInt64(ltbDataSet.IB5);
                        RSin[5] = Convert.ToInt64(ltbDataSet.RS5);
                        FRin[5] = Convert.ToDouble(ltbDataSet.FR5);
                        RLin[5] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL5) / 100);

                        break;
                    case 6:
                        IBin[6] = Convert.ToInt64(ltbDataSet.IB6);
                        RSin[6] = Convert.ToInt64(ltbDataSet.RS6);
                        FRin[6] = Convert.ToDouble(ltbDataSet.FR6);
                        RLin[6] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL6) / 100);

                        break;
                    case 7:
                        IBin[7] = Convert.ToInt64(ltbDataSet.IB7);
                        RSin[7] = Convert.ToInt64(ltbDataSet.RS7);
                        FRin[7] = Convert.ToDouble(ltbDataSet.FR7);
                        RLin[7] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL7) / 100);

                        break;
                    case 8:
                        IBin[8] = Convert.ToInt64(ltbDataSet.IB8);
                        RSin[8] = Convert.ToInt64(ltbDataSet.RS8);
                        FRin[8] = Convert.ToDouble(ltbDataSet.FR8);
                        RLin[8] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL8) / 100);

                        break;
                    case 9:
                        IBin[9] = Convert.ToInt64(ltbDataSet.IB9);
                        RSin[9] = Convert.ToInt64(ltbDataSet.RS9);
                        FRin[9] = Convert.ToDouble(ltbDataSet.FR9);
                        RLin[9] = Convert.ToDouble(Convert.ToDouble(ltbDataSet.RL9) / 100);

                        break;
                }
                Cnt += 1;
            }
            ClearRemains(ltbDataSet, Cnt);

        }

        static void ClearRemains(LtbDataSet ltbDataSet, int StartYear)
        {
            int Cnt = StartYear;
            while (Cnt <= LTBCommon.MaxYear)
            {
                switch (Cnt)
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
                Cnt += 1;
            }
        }

        static long FromGamma;
        static long FromAverage;
        //const long MaxYear = 10;
        const long MinRepairLeadTime = 2;
        const long MaxRepairLeadTime = 365;
        const long MaxServiceDays = LTBCommon.MaxYear * 365 + 2;
        const long MaxDayArr = MaxServiceDays + 365;
        const long MaxLTArr = MaxServiceDays / MinRepairLeadTime + 2;
        static long[] IBin = new long[LTBCommon.MaxYear + 1];
        static double[] FRin = new double[LTBCommon.MaxYear + 1];
        static double[] RLin = new double[LTBCommon.MaxYear + 1];
        static long[] RSin = new long[LTBCommon.MaxYear + 1];
        static long[] RSArray = new long[MaxLTArr + 1];
        static long[] RSDayArray = new long[MaxDayArr + 365];
        static double[] StockDayArray = new double[MaxDayArr + 365];
        static double[] ReturnedDayArray = new double[MaxDayArr + 365];
        static double[] SumDemandDayArray = new double[MaxDayArr + 365];
        static double[] IBArray = new double[MaxLTArr + 1];
        static double[] FRArray = new double[MaxLTArr + 1];
        static double[] RLArray = new double[MaxLTArr + 1];
        static double[] RLDayArray = new double[MaxDayArr + 365];
        static double[] Stock_Array = new double[MaxLTArr + 1];
        static double[] Returned_Array = new double[MaxLTArr + 2];
        static double[] Demand_Array = new double[MaxLTArr + 1];
        static double[] SumDemand_Array = new double[MaxLTArr + 1];
        static double[] FSRepairLeadTimeDemand_Array = new double[MaxLTArr + 1];
        static double[] RepairLoss_Array = new double[MaxLTArr + 1];
        static double[] SumRepairLoss_Array = new double[MaxLTArr + 1];
        static double[] Repair_Array = new double[MaxLTArr + 1];
        static double[] SumRepair_Array = new double[MaxLTArr + 1];
        static double[] SafetyMargin_Array = new double[MaxLTArr + 1];
        static double[] SafetyMarginDayArray = new double[MaxDayArr + 365];
        static double ConfidenceLevelDbl;
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
            ltb.LTBWorker(NbrOfSamples, ltbDataSet.ServiceDays, ltbDataSet.RepairLeadTime, Mathematics.ServiceYears(ltbDataSet), ConfidenceLevelFromNormsInv, ref IBArray, ref RSArray, ref FRArray, ref RLArray,
            ref Stock_Array, ref Returned_Array, ref Demand_Array, ref SumDemand_Array, ref RepairLoss_Array, ref  SumRepairLoss_Array, ref Repair_Array, ref SumRepair_Array, ref SafetyMargin_Array, ref SafetyMarginDayArray, ref FSRepairLeadTimeDemand_Array,
            ref IBin, ref  FRin, ref  RLin, ref  RSin, ref RSDayArray, ref  RLDayArray, ref  StockDayArray, ref  ReturnedDayArray, ref  SumDemandDayArray);
            SetChartData(ltbDataSet);
            ltbDataSet.LtbChart = Presenter.GetChart(ltbDataSet);
            stockPresent = Mathematics.RoundLong(Stock_Array[1], 0);
            safetyPresent = ltbDataSet.SafetyYearArray[0];

            ltbDataSet.Stock = stockPresent.ToString() + GetCLFromAverage(ConfidenceLevelDbl, SafetyMargin_Array[1]).ToString();

            if (safetyPresent > 0)
            {
                FromAverage = GetSafetyFromAverage(ConfidenceLevelDbl, SafetyMargin_Array[1]);
                ltbDataSet.Safety = safetyPresent.ToString() + GetCLFromStock(SafetyMargin_Array[1], FromAverage).ToString();
            }
            else
            {
                ltbDataSet.Safety = string.Empty;
            }

            ltbDataSet.TotalStock = Convert.ToString(stockPresent + safetyPresent);

            ltbDataSet.Failed = Mathematics.RoundLong(SumDemand_Array[1], 0).ToString();

            ltbDataSet.Repaired = Mathematics.RoundLong(SumRepair_Array[1] - SumRepairLoss_Array[1], 0).ToString();

            ltbDataSet.Lost = ltbDataSet.RepairPossible ? Mathematics.RoundUpLong(SumRepairLoss_Array[1], 0).ToString() : "Nothing";
        }

        static void SetChartData(LtbDataSet ltbDataSet)
        {
            //For Chart
            var yearCnt = 0;
            while (yearCnt <= Mathematics.ServiceYears(ltbDataSet))
            {
                ltbDataSet.RSYearArray[yearCnt] = RSDayArray[yearCnt * 365 + 1];
                ltbDataSet.StockYearArray[yearCnt] = Mathematics.RoundLong(StockDayArray[yearCnt * 365 + 1] - RSDayArray[yearCnt * 365 + 1], 0);
                FromAverage = Mathematics.RoundLong(GetSafetyFromAverage(ConfidenceLevelDbl, SafetyMarginDayArray[yearCnt * 365 + 1]), 0);
                FromGamma = Mathematics.RoundLong(GetSafetyFromGamma(ConfidenceLevelDbl, SafetyMarginDayArray[yearCnt * 365 + 1] + ReturnedDayArray[yearCnt * 365 + 1] + FromAverage, ReturnedDayArray[yearCnt * 365 + ltbDataSet.RepairLeadTime + 1]), 0);
                ltbDataSet.SafetyYearArray[yearCnt] = FromGamma + FromAverage;
                yearCnt = yearCnt + 1;
            }
        }
    }
}
