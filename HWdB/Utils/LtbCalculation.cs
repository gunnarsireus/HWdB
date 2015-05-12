using CenterSpace.NMath.Core;
using CenterSpace.NMath.Stats;
using HWdB.Model;
using LTBCore;
using Microsoft.VisualBasic;
using System;
using System.Linq;

namespace HWdB.Utils
{
    class LtbCalculation
    {
        static long FromGamma;
        static long FromAverage;
        const long MaxYear = 10;
        const long MinLeadDays = 2;
        const long MaxLeadDays = 365;
        const long MaxServiceDays = MaxYear * 365 + 2;
        const long MaxDayArr = MaxServiceDays + 365;
        const long MaxLTArr = MaxServiceDays / MinLeadDays + 2;
        const long RedFont = 16711782;
        const long BlackFont = 0;
        static long[] IBin = new long[MaxYear + 1];
        static double[] FRin = new double[MaxYear + 1];
        static double[] RLin = new double[MaxYear + 1];
        static long[] RSin = new long[MaxYear + 1];
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
        static double[] FSLeadDaysDemand_Array = new double[MaxLTArr + 1];
        static double[] RepairLoss_Array = new double[MaxLTArr + 1];
        static double[] SumRepairLoss_Array = new double[MaxLTArr + 1];
        static double[] Repair_Array = new double[MaxLTArr + 1];
        static double[] SumRepair_Array = new double[MaxLTArr + 1];
        static double[] SafetyMargin_Array = new double[MaxLTArr + 1];
        static double[] SafetyMarginDayArray = new double[MaxDayArr + 365];
        static double Conf_in;
        static double ServiceDays;
        static double LeadDays;
        static int N;
        static double Conf_Level;
        static int MyServiceYears;
        static int _ServiceYears;
        static int tabidx;
        static bool RepairAvailable;
        static bool Live;

        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private static double[] norminv_a = { 2.50662823884, -18.61500062529, 41.39119773534, -25.44106049637 };
        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private static double[] norminv_b = { -8.47351093090, 23.08336743743, -21.06224101826, 3.13082909833 };
        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private static double[] norminv_c = { 0.3374754822726147, 0.9761690190917186, 0.1607979714918209, 0.0276438810333863, 0.0038405729373609, 0.0003951896511919, 0.0000321767881768, 0.0000002888167364, 0.0000003960315187 };
        private static double norminv(double u)
        {
            /* returns the inverse of cumulative normal distribution function Reference> The Full Monte, by Boris Moro, Union Bank of Switzerland
                         RISK 1995(2)*/

            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //static double a[4]={ 2.50662823884, -18.61500062529, 41.39119773534, -25.44106049637};
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //static double b[4]={ -8.47351093090, 23.08336743743, -21.06224101826, 3.13082909833};
            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //static double c[9]={0.3374754822726147, 0.9761690190917186, 0.1607979714918209, 0.0276438810333863, 0.0038405729373609, 0.0003951896511919, 0.0000321767881768, 0.0000002888167364, 0.0000003960315187};
            double x;
            double r;
            x = u - 0.5;
            if (Math.Abs(x) < 0.42)
            {
                r = x * x;
                r = x * (((norminv_a[3] * r + norminv_a[2]) * r + norminv_a[1]) * r + norminv_a[0]) / ((((norminv_b[3] * r + norminv_b[2]) * r + norminv_b[1]) * r + norminv_b[0]) * r + 1.0);
                return (r);
            }
            r = u;
            if (x > 0.0) r = 1.0 - u;
            r = Math.Log(-Math.Log(r));
            r = norminv_c[0] + r * (norminv_c[1] + r * (norminv_c[2] + r * (norminv_c[3] + r * (norminv_c[4] + r * (norminv_c[5] + r * (norminv_c[6] + r * (norminv_c[7] + r * norminv_c[8])))))));
            if (x < 0.0) r = -r;
            return (r);
        }

        //private static int calcreserve2(int M, double FR, double p)
        //{
        //    double pi = 3.1415926535897932384626433832795028841971693993751058209749;
        //    double L = M * FR / pi;
        //    //double lp;
        //    return (int)(RoundLong(2 * Math.Sqrt(L) + pi * L + NormSInv(p) * Math.Sqrt(L * (2 * pi - 4) + Math.Sqrt(L) + 3 / 16)));
        //}


        public static int calcreserve2(int M, double FR, double p)
        {
            switch (RoundLong(p * 1000, 0))
            {
                case 600: return (int)RoundLong(M + (133 * Sqr((double)M) / 100), 0);
                case 700: return (int)RoundLong(M + (156 * Sqr((double)M) / 100), 0);
                case 800: return (int)RoundLong(M + (184 * Sqr((double)M) / 100), 0);
                case 900: return (int)RoundLong(M + (223 * Sqr((double)M) / 100), 0);
                case 950: return (int)RoundLong(M + (255 * Sqr((double)M) / 100), 0);
                case 995: return (int)RoundLong(M + (340 * Sqr((double)M) / 100), 0);
                default: return (int)RoundLong(M + (340 * Sqr((double)M) / 100), 0);
            }
        }

        public static int calcreserve(int M, double FR, double p)
        {
            if (M * FR > 10000) return calcreserve2(M, FR, p);
            int k; // init counter
            int i = 0;
            double pp = 0; // init prob
            while (pp < p) // loop while cumaltive probability less than p
            {
                for (k = 0; k <= i; k++)
                    /* calculate probability */
                    pp = pp + Math.Exp(-StatsFunctions.GammaLn(i + 2) - StatsFunctions.GammaLn(k + 1) + 2.0 * Math.Log((double)i + 1 - k) - 2.0 * FR * M + (k + i) * Math.Log(FR * M));
                i = i + 1;
            }
            return (i - 1);
        }
        static bool IsLeapYear(long Y)
        {
            return (Y > 0) && (Y % 4) == 0 && !((Y % 100) == 0 && !((Y % 400) == 0));
        }
        static long CountLeaps(long Y)
        {
            return (Y - 1) / 4 - (Y - 1) / 100 + (Y - 1) / 400;
        }
        static long CountDays(long Y)
        {
            return (Y - 1) * 365 + CountLeaps(Y);
        }
        static long CountYears(long d)
        {
            return 1 + (d - CountLeaps(d / 365)) / 365;
        }
        static long DaysBetweenYears(long y1, long y2)
        {
            return CountDays(y2) - CountDays(y1);
        }
        static void setServiceYears(System.DateTime ltbYear, System.DateTime eosYear)
        {
            System.DateTime NewYear = default(System.DateTime);

            if (ltbYear.Year == eosYear.Year)
            {
                _ServiceYears = 0;
            }
            else
            {
                NewYear = Convert.ToDateTime(ltbYear.Year.ToString() + "-01-01");
                if (IsLeapYear(ltbYear.Year) & DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, NewYear, ltbYear) < 59)
                {
                    _ServiceYears = Convert.ToInt32((DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, ltbYear, eosYear) + CountLeaps(ltbYear.Year) - CountLeaps(eosYear.Year) - 2) / 365);
                }
                else
                {
                    _ServiceYears = Convert.ToInt32((DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, ltbYear, eosYear) + CountLeaps(ltbYear.Year) - CountLeaps(eosYear.Year) - 1) / 365);
                }
            }
        }
        public static double Sqr(double x)
        {
            return Math.Pow(x, 0.5);
        }
        public static double RoundUpDouble(double x, int Y)
        {
            return Math.Round(x + 0.49999999999, Y);
        }
        public static long RoundUpLong(double x, int Y)
        {
            return Convert.ToInt64(Math.Round(x + 0.49999999999, Y));
        }
        public static long RoundLong(double x, int Y)
        {
            return Convert.ToInt64(Math.Round(x, Y));
        }
        public static long RoundLong(double x)
        {
            return Convert.ToInt64(Math.Round(x, 0));
        }
        public static int RoundUpInt(double x, int Y)
        {
            return Convert.ToInt32(Math.Round(x + 0.49999999999, Y));
        }
        // This function is a replacement for the Microsoft Excel Worksheet function NORMSINV.
        // It uses the algorithm of Peter J. Acklam to compute the inverse normal cumulative
        // distribution. Refer to http://home.online.no/~pjacklam/notes/invnorm/index.html for
        // a description of the algorithm.
        // Adapted to VB by Christian d'Heureuse, http://www.source-code.biz.
        public static double NormSInv(double p)
        {
            double functionReturnValue = 0;
            const double a1 = -39.6968302866538, a2 = 220.946098424521, a3 = -275.928510446969;
            const double a4 = 138.357751867269, a5 = -30.6647980661472, a6 = 2.50662827745924;
            const double b1 = -54.4760987982241, b2 = 161.585836858041, b3 = -155.698979859887;
            const double b4 = 66.8013118877197, b5 = -13.2806815528857, c1 = -0.00778489400243029;
            const double c2 = -0.322396458041136, c3 = -2.40075827716184, c4 = -2.54973253934373;
            const double c5 = 4.37466414146497, c6 = 2.93816398269878, d1 = 0.00778469570904146;
            const double d2 = 0.32246712907004, d3 = 2.445134137143, d4 = 3.75440866190742;
            const double p_low = 0.02425, p_high = 1 - p_low;
            double q = 0;
            double r = 0;
            functionReturnValue = 0;
            if (p < 0 | p > 1)
            {
                // Err.Raise(Constants.vbObjectError, "", "NormSInv: Argument out of range.");
            }
            else if (p < p_low)
            {
                q = Sqr(-2 * Math.Log(p));
                functionReturnValue = (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
            }
            else if (p <= p_high)
            {
                q = p - 0.5;
                r = q * q;
                functionReturnValue = (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q / (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1);
            }
            else
            {
                q = Sqr(-2 * Math.Log(1 - p));
                functionReturnValue = -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
            }
            return functionReturnValue;
        }
        public static double ConfL(double Y)
        {
            return NormSInv(Y);
        }
        public static double Pow(double x, double Y)
        {
            return Math.Pow(x, Y);
        }

        public static long Factorial(long x)
        {
            long functionReturnValue = 0;
            if (x <= 1)
            {
                functionReturnValue = 1;
            }
            else
            {
                functionReturnValue = x * Factorial(x - 1);
            }
            return functionReturnValue;
        }

        public static string GetCLFromAverage(double CL, double Average)
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

        public static string GetCLFromStock(double Stock, double Safety)
        {
            string functionReturnValue = null;
            if (Stock == 0)
            {
                functionReturnValue = " (100%)";
                return functionReturnValue;
            }
            if (Stock < 2492000)
            {
                PoissonDistribution Poisson = new PoissonDistribution(Stock);
                double tmp = 0;
                NMathConfiguration.Init();
                tmp = Poisson.CDF(Math.Round(Stock + Safety, 0));
                functionReturnValue = " (" + (100 * Math.Round(tmp, 4)).ToString() + "%)";
            }
            else
            {
                functionReturnValue = string.Empty;
            }
            return functionReturnValue;
        }

        public static long GetSafetyFromAverage(double CL, double Average)
        {
            long functionReturnValue = 0;
            if (Average <= 0)
            {
                functionReturnValue = 0;
                return functionReturnValue;
            }

            if (Average < 2492000)
            {
                PoissonDistribution Poisson = new PoissonDistribution(Average);
                long K = 0;
                K = Convert.ToInt64(Math.Round(Average, 0));

                while (Poisson.CDF(K) < CL)
                {
                    K = K + 1;
                }

                functionReturnValue = Convert.ToInt64(Math.Round(K - Average, 0));
                if (functionReturnValue < 0)
                    functionReturnValue = 0;
            }
            else
            {
                functionReturnValue = RoundLong(NormSInv(CL) * Sqr(Average), 0);
            }
            return functionReturnValue;
        }

        public static long GetSafetyFromGamma(double CL, double FromAverage, double Returned)
        {
            long ReturnValue;

            ReturnValue = (long)(calcreserve((int)RoundLong(Returned, 0), 1, CL) - FromAverage);
            if (ReturnValue < 0) ReturnValue = 0;
            if (ReturnValue > FromAverage) ReturnValue = (long)FromAverage;

            return ReturnValue;
        }


        public static double GetAverageFromReturned(double Average)
        {
            if (Average < 8) { return Average / 3.764705; } else { return (1.125 + Average / 8); }
        }



        private static double LogGamma(double x)
        {

            if (x <= 0.0)
            {
                //std.stringstream os = new std.stringstream();
                //os << "Invalid input argument " << x << ". Argument must be positive.";
                //throw std.invalid_argument(os.str());
            }

            if (x < 12.0)
            {
                return Math.Log(Math.Abs(Gamma(x)));
            }

            // Abramowitz and Stegun 6.1.41
            // Asymptotic series should be good to at least 11 or 12 figures
            // For error analysis, see Whittiker and Watson
            // A Course in Modern Analysis (1927), page 252

            double[] c = { 1.0 / 12.0, -1.0 / 360.0, 1.0 / 1260.0, -1.0 / 1680.0, 1.0 / 1188.0, -691.0 / 360360.0, 1.0 / 156.0, -3617.0 / 122400.0 };
            double z = 1.0 / (x * x);
            double sum = c[7];
            for (int i = 6; i >= 0; i--)
            {
                sum *= z;
                sum += c[i];
            }
            double series = sum / x;

            const double halfLogTwoPi = 0.91893853320467274178032973640562;
            double LogGamma = (x - 0.5) * Math.Log(x) - x + halfLogTwoPi + series;
            return LogGamma;
        }


        private static double Gamma(double x)
        {
            if (x <= 0.0)
            {
                //std.stringstream os = new std.stringstream();
                //os << "Invalid input argument " << x << ". Argument must be positive.";
                //throw std.invalid_argument(os.str());
            }

            // Split the function domain into three intervals:
            // (0, 0.001), [0.001, 12), and (12, infinity)

            ///////////////////////////////////////////////////////////////////////////
            // First interval: (0, 0.001)
            //
            // For small x, 1/Gamma(x) has power series x + gamma x^2  - ...
            // So in this range, 1/Gamma(x) = x + gamma x^2 with error on the order of x^3.
            // The relative error over this interval is less than 6e-7.

            const double Gamma = 0.577215664901532860606512090; // Euler's gamma constant

            if (x < 0.001)
                return 1.0 / (x * (1.0 + Gamma * x));

            ///////////////////////////////////////////////////////////////////////////
            // Second interval: [0.001, 12)

            if (x < 12.0)
            {
                // The algorithm directly approximates Gamma over (1,2) and uses
                // reduction identities to reduce other arguments to this interval.

                double y = x;
                int n = 0;
                bool arg_was_less_than_one = (y < 1.0);

                // Add or subtract integers as necessary to bring y into (1,2)
                // Will correct for this below
                if (arg_was_less_than_one)
                {
                    y += 1.0;
                }
                else
                {
                    n = (int)(Math.Floor(y)) - 1; // will use n later
                    y -= n;
                }

                // numerator coefficients for approximation over the interval (1,2)
                double[] p = { -1.71618513886549492533811E+0, 2.47656508055759199108314E+1, -3.79804256470945635097577E+2, 6.29331155312818442661052E+2, 8.66966202790413211295064E+2, -3.14512729688483675254357E+4, -3.61444134186911729807069E+4, 6.64561438202405440627855E+4 };

                // denominator coefficients for approximation over the interval (1,2)
                double[] q = { -3.08402300119738975254353E+1, 3.15350626979604161529144E+2, -1.01515636749021914166146E+3, -3.10777167157231109440444E+3, 2.25381184209801510330112E+4, 4.75584627752788110767815E+3, -1.34659959864969306392456E+5, -1.15132259675553483497211E+5 };

                double num = 0.0;
                double den = 1.0;
                int i;

                double z = y - 1;
                for (i = 0; i < 8; i++)
                {
                    num = (num + p[i]) * z;
                    den = den * z + q[i];
                }
                double result = num / den + 1.0;

                // Apply correction if argument was not initially in (1,2)
                if (arg_was_less_than_one)
                {
                    // Use identity Gamma(z) = gamma(z+1)/z
                    // The variable "result" now holds Gamma of the original y + 1
                    // Thus we use y-1 to get back the orginal y.
                    result /= (y - 1.0);
                }
                else
                {
                    // Use the identity Gamma(z+n) = z*(z+1)* ... *(z+n-1)*gamma(z)
                    for (i = 0; i < n; i++)
                        result *= y++;
                }

                return result;
            }

            ///////////////////////////////////////////////////////////////////////////
            // Third interval: [12, infinity)

            if (x > 171.624)
            {
                // Correct answer too large to display. Force +infinity.
                double temp = double.MaxValue;
                return temp * 2.0;
            }

            return Math.Exp(LogGamma(x));
        }
        static int getServiceYears()
        {
            return _ServiceYears;
        }
        public static void ClearResult(LtbDataSet Ltb)
        {
            Ltb.TotalStock = string.Empty;
            Ltb.Stock = string.Empty;
            Ltb.Safety = string.Empty;
            Ltb.InfoText = string.Empty;
            Ltb.Failed = string.Empty;
            Ltb.Repaired = string.Empty;
            Ltb.Lost = string.Empty;
        }

        public static void ClearChartData(LtbDataSet ltb)
        {
            int YearCnt = 0;
            while (YearCnt <= 10)
            {
                ltb.RSYearArray[YearCnt] = 0;
                ltb.StockYearArray[YearCnt] = 0;
                ltb.SafetyYearArray[YearCnt] = 0;
                YearCnt = YearCnt + 1;
            }
        }

        protected static bool BoundariesOK(int LastYear, LtbDataSet Ltb)
        {
            if (Ltb.HasErrors.Count > 0)
            {
                var first = Ltb.HasErrors.First();
                Ltb.InfoText = first.Value;
                return false;
            }
            bool functionReturnValue = false;
            int Cnt = 0; Ltb.InfoText = "";
            DateTime TmpDate = Convert.ToDateTime(Ltb.LTBDate);
            Cnt = 0;
            functionReturnValue = true;
            while (Cnt <= LastYear)
            {
                switch (Cnt)
                {
                    case 0:
                        if (Ltb.RS0 == string.Empty)
                            Ltb.RS0 = "0";
                        if (Information.IsNothing(Ltb.IB0) | Information.IsNothing(Ltb.RS0) | Information.IsNothing(Ltb.FR0) | Information.IsNothing(Ltb.RL0) | !Information.IsNumeric(Ltb.IB0) | !Information.IsNumeric(Ltb.RS0) | !Information.IsNumeric(Ltb.FR0) | !Information.IsNumeric(Ltb.RL0))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = string.Format("Error: Wrong parameters in " + "{0}", TmpDate.Year);
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB0) > 99999 | Convert.ToInt32(Ltb.IB0) < 0 | Convert.ToInt32(Ltb.RS0) < 0 | Convert.ToInt32(Ltb.RS0) > 9999 | Convert.ToDouble(Ltb.FR0) < 1E-07 | Convert.ToDouble(Ltb.FR0) > 100 | Convert.ToInt32(Ltb.RL0) < 0 | Convert.ToInt32(Ltb.RL0) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + TmpDate.Year;
                            return functionReturnValue;
                        }
                        break;
                    case 1:
                        if (Ltb.RS1 == string.Empty)
                            Ltb.RS1 = "0";
                        if (Information.IsNothing(Ltb.IB1) | Information.IsNothing(Ltb.RS1) | Information.IsNothing(Ltb.FR1) | Information.IsNothing(Ltb.RL1) | !Information.IsNumeric(Ltb.IB1) | !Information.IsNumeric(Ltb.RS1) | !Information.IsNumeric(Ltb.FR1) | !Information.IsNumeric(Ltb.RL1))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 1).ToString();
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB1) > 99999 | Convert.ToInt32(Ltb.IB1) < 0 | Convert.ToInt32(Ltb.RS1) < 0 | Convert.ToInt32(Ltb.RS1) > 9999 | Convert.ToDouble(Ltb.FR1) < 1E-06 | Convert.ToDouble(Ltb.FR1) > 100 | Convert.ToInt32(Ltb.RL1) < 0 | Convert.ToInt32(Ltb.RL1) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 1).ToString();
                            return functionReturnValue;
                        }
                        break;
                    case 2:
                        if (Ltb.RS2 == string.Empty)
                            Ltb.RS2 = "0";
                        if (Information.IsNothing(Ltb.IB2) | Information.IsNothing(Ltb.RS2) | Information.IsNothing(Ltb.FR2) | Information.IsNothing(Ltb.RL2) | !Information.IsNumeric(Ltb.IB2) | !Information.IsNumeric(Ltb.RS2) | !Information.IsNumeric(Ltb.FR2) | !Information.IsNumeric(Ltb.RL2))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 2).ToString();
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB2) > 99999 | Convert.ToInt32(Ltb.IB2) < 0 | Convert.ToInt32(Ltb.RS2) < 0 | Convert.ToInt32(Ltb.RS2) > 9999 | Convert.ToDouble(Ltb.FR2) < 1E-06 | Convert.ToDouble(Ltb.FR2) > 100 | Convert.ToInt32(Ltb.RL2) < 0 | Convert.ToInt32(Ltb.RL2) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 2).ToString();
                            return functionReturnValue;
                        }
                        break;
                    case 3:
                        if (Ltb.RS3 == string.Empty)
                            Ltb.RS3 = "0";
                        if (Information.IsNothing(Ltb.IB3) | Information.IsNothing(Ltb.RS3) | Information.IsNothing(Ltb.FR3) | Information.IsNothing(Ltb.RL3) | !Information.IsNumeric(Ltb.IB3) | !Information.IsNumeric(Ltb.RS3) | !Information.IsNumeric(Ltb.FR3) | !Information.IsNumeric(Ltb.RL3))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 3).ToString();
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB3) > 99999 | Convert.ToInt32(Ltb.IB3) < 0 | Convert.ToInt32(Ltb.RS3) < 0 | Convert.ToInt32(Ltb.RS3) > 9999 | Convert.ToDouble(Ltb.FR3) < 1E-06 | Convert.ToDouble(Ltb.FR3) > 100 | Convert.ToInt32(Ltb.RL3) < 0 | Convert.ToInt32(Ltb.RL3) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 3).ToString();
                            return functionReturnValue;
                        }
                        break;
                    case 4:
                        if (Ltb.RS4 == string.Empty)
                            Ltb.RS4 = "0";
                        if (Information.IsNothing(Ltb.IB4) | Information.IsNothing(Ltb.RS4) | Information.IsNothing(Ltb.FR4) | Information.IsNothing(Ltb.RL4) | !Information.IsNumeric(Ltb.IB4) | !Information.IsNumeric(Ltb.RS4) | !Information.IsNumeric(Ltb.FR4) | !Information.IsNumeric(Ltb.RL4))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 4).ToString();
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB4.ToString()) > 99999 | Convert.ToInt32(Ltb.IB4.ToString()) < 0 | Convert.ToInt32(Ltb.RS4) < 0 | Convert.ToInt32(Ltb.RS4) > 9999 | Convert.ToDouble(Ltb.FR4) < 1E-06 | Convert.ToDouble(Ltb.FR4) > 100 | Convert.ToInt32(Ltb.RL4) < 0 | Convert.ToInt32(Ltb.RL4) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 4).ToString();
                            return functionReturnValue;
                        }
                        break;
                    case 5:
                        if (Ltb.RS5 == string.Empty)
                            Ltb.RS5 = "0";
                        if (Information.IsNothing(Ltb.IB5) | Information.IsNothing(Ltb.RS5) | Information.IsNothing(Ltb.FR5) | Information.IsNothing(Ltb.RL5) | !Information.IsNumeric(Ltb.IB5) | !Information.IsNumeric(Ltb.RS5) | !Information.IsNumeric(Ltb.FR5) | !Information.IsNumeric(Ltb.RL5))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 5).ToString();
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB5) > 99999 | Convert.ToInt32(Ltb.IB5) < 0 | Convert.ToInt32(Ltb.RS5) < 0 | Convert.ToInt32(Ltb.RS5) > 9999 | Convert.ToDouble(Ltb.FR5) < 1E-06 | Convert.ToDouble(Ltb.FR5) > 100 | Convert.ToInt32(Ltb.RL5) < 0 | Convert.ToInt32(Ltb.RL5) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 5).ToString();
                            return functionReturnValue;
                        }
                        break;
                    case 6:
                        if (Ltb.RS6.ToString() == string.Empty)
                            Ltb.RS6 = "0";
                        if (Information.IsNothing(Ltb.IB6) | Information.IsNothing(Ltb.RS6) | Information.IsNothing(Ltb.FR6) | Information.IsNothing(Ltb.RL6) | !Information.IsNumeric(Ltb.IB6) | !Information.IsNumeric(Ltb.RS6.ToString()) | !Information.IsNumeric(Ltb.FR6) | !Information.IsNumeric(Ltb.RL6))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 6).ToString();
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB6.ToString()) > 99999 | Convert.ToInt32(Ltb.IB6.ToString()) < 0 | Convert.ToInt32(Ltb.RS6.ToString()) < 0 | Convert.ToInt32(Ltb.RS6.ToString()) > 9999 | Convert.ToDouble(Ltb.FR6) < 1E-06 | Convert.ToDouble(Ltb.FR6) > 100 | Convert.ToInt32(Ltb.RL6) < 0 | Convert.ToInt32(Ltb.RL6) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 6).ToString();
                            return functionReturnValue;
                        }
                        break;
                    case 7:
                        if (Ltb.RS7 == string.Empty)
                            Ltb.RS7 = "0";
                        if (Information.IsNothing(Ltb.IB7) | Information.IsNothing(Ltb.RS7) | Information.IsNothing(Ltb.FR7) | Information.IsNothing(Ltb.RL7) | !Information.IsNumeric(Ltb.IB7) | !Information.IsNumeric(Ltb.RS7) | !Information.IsNumeric(Ltb.FR7) | !Information.IsNumeric(Ltb.RL7))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 7).ToString();
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB7) > 99999 | Convert.ToInt32(Ltb.IB7) < 0 | Convert.ToInt32(Ltb.RS7) < 0 | Convert.ToInt32(Ltb.RS7) > 9999 | Convert.ToDouble(Ltb.FR7) < 1E-06 | Convert.ToDouble(Ltb.FR7) > 100 | Convert.ToInt32(Ltb.RL7) < 0 | Convert.ToInt32(Ltb.RL7) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 7).ToString();
                            return functionReturnValue;
                        }
                        break;
                    case 8:
                        if (Ltb.RS8 == string.Empty)
                            Ltb.RS8 = "0";
                        if (Information.IsNothing(Ltb.IB8) | Information.IsNothing(Ltb.RS8) | Information.IsNothing(Ltb.FR8) | Information.IsNothing(Ltb.RL8) | !Information.IsNumeric(Ltb.IB8) | !Information.IsNumeric(Ltb.RS8) | !Information.IsNumeric(Ltb.FR8) | !Information.IsNumeric(Ltb.RL8))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 8).ToString();
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB8) > 99999 | Convert.ToInt32(Ltb.IB8) < 0 | Convert.ToInt32(Ltb.RS8) < 0 | Convert.ToInt32(Ltb.RS8) > 9999 | Convert.ToDouble(Ltb.FR8) < 1E-06 | Convert.ToDouble(Ltb.FR8) > 100 | Convert.ToInt32(Ltb.RL8) < 0 | Convert.ToInt32(Ltb.RL8) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 8).ToString();
                            return functionReturnValue;
                        }
                        break;
                    case 9:
                        if (Ltb.RS9 == string.Empty)
                            Ltb.RS9 = "0";
                        if (Information.IsNothing(Ltb.IB9) | Information.IsNothing(Ltb.RS9) | Information.IsNothing(Ltb.FR9) | Information.IsNothing(Ltb.RL9) | !Information.IsNumeric(Ltb.IB9) | !Information.IsNumeric(Ltb.RS9) | !Information.IsNumeric(Ltb.FR9) | !Information.IsNumeric(Ltb.RL9))
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 9).ToString();
                            return functionReturnValue;
                        }
                        if (Convert.ToInt32(Ltb.IB9) > 99999 | Convert.ToInt32(Ltb.IB9) < 0 | Convert.ToInt32(Ltb.RS9) < 0 | Convert.ToInt32(Ltb.RS9) > 9999 | Convert.ToDouble(Ltb.FR9) < 1E-06 | Convert.ToDouble(Ltb.FR9) > 100 | Convert.ToInt32(Ltb.RL9) < 0 | Convert.ToInt32(Ltb.RL9) > 100)
                        {
                            functionReturnValue = false;
                            Cnt = LastYear;
                            Ltb.InfoText = "Error: Wrong parameters in " + (Convert.ToInt32(TmpDate.Year) + 9).ToString();
                            return functionReturnValue;
                        }
                        break;
                    case 10:
                        break;
                }
                Cnt += 1;
            }
            return functionReturnValue;
        }

        static void ReadIB(double SD, int LastYear, double LD, ref double CL, int N, LtbDataSet Ltb)
        {
            int Cnt = 0;
            //int Conf = Convert.ToInt32(Ltb.ConfidenceLevel);
            switch (Ltb.ConfidenceLevel)
            {
                //Confidence Level

                case "60%":
                    CL = ConfL(0.6);
                    Conf_in = 0.6;

                    break;
                case "70%":
                    CL = ConfL(0.7);
                    Conf_in = 0.7;

                    break;
                case "80%":
                    CL = ConfL(0.8);
                    Conf_in = 0.8;

                    break;
                case "90%":
                    CL = ConfL(0.9);
                    Conf_in = 0.9;

                    break;
                case "95%":
                    CL = ConfL(0.95);
                    Conf_in = 0.95;

                    break;
                case "99,5%":
                    CL = ConfL(0.995);
                    Conf_in = 0.995;

                    break;

            }

            Cnt = 0;

            while (Cnt <= LastYear)
            {
                switch (Cnt)
                {
                    case 0:
                        //////ViewData["RS0ForeColor"] = System.Drawing.Color.Black;
                        IBin[0] = Convert.ToInt64(Ltb.IB0);
                        RSin[0] = Convert.ToInt64(Ltb.RS0);
                        FRin[0] = Convert.ToDouble(Ltb.FR0);
                        RLin[0] = Convert.ToDouble(Convert.ToDouble(Ltb.RL0) / 100);

                        break;
                    case 1:
                        ////ViewData["RS1ForeColor"] = System.Drawing.Color.Black;
                        IBin[1] = Convert.ToInt64(Ltb.IB1);
                        RSin[1] = Convert.ToInt64(Ltb.RS1);
                        FRin[1] = Convert.ToDouble(Ltb.FR1);
                        RLin[1] = Convert.ToDouble(Convert.ToDouble(Ltb.RL1) / 100);

                        break;
                    case 2:
                        ////ViewData["RS2ForeColor"] = System.Drawing.Color.Black;
                        IBin[2] = Convert.ToInt64(Ltb.IB2);
                        RSin[2] = Convert.ToInt64(Ltb.RS2);
                        FRin[2] = Convert.ToDouble(Ltb.FR2);
                        RLin[2] = Convert.ToDouble(Convert.ToDouble(Ltb.RL2) / 100);

                        break;
                    case 3:
                        ////ViewData["RS3ForeColor"] = System.Drawing.Color.Black;
                        IBin[3] = Convert.ToInt64(Ltb.IB3);
                        RSin[3] = Convert.ToInt64(Ltb.RS3);
                        FRin[3] = Convert.ToDouble(Ltb.FR3);
                        RLin[3] = Convert.ToDouble(Convert.ToDouble(Ltb.RL3) / 100);

                        break;
                    case 4:
                        ////ViewData["RS4ForeColor"] = System.Drawing.Color.Black;
                        IBin[4] = Convert.ToInt64(Ltb.IB4.ToString());
                        RSin[4] = Convert.ToInt64(Ltb.RS4);
                        FRin[4] = Convert.ToDouble(Ltb.FR4);
                        RLin[4] = Convert.ToDouble(Convert.ToDouble(Ltb.RL4) / 100);

                        break;
                    case 5:
                        ////ViewData["RS5ForeColor"] = System.Drawing.Color.Black;
                        IBin[5] = Convert.ToInt64(Ltb.IB5);
                        RSin[5] = Convert.ToInt64(Ltb.RS5);
                        FRin[5] = Convert.ToDouble(Ltb.FR5);
                        RLin[5] = Convert.ToDouble(Convert.ToDouble(Ltb.RL5) / 100);

                        break;
                    case 6:
                        ////ViewData["RS6ForeColor"] = System.Drawing.Color.Black;
                        IBin[6] = Convert.ToInt64(Ltb.IB6.ToString());
                        RSin[6] = Convert.ToInt64(Ltb.RS6.ToString());
                        FRin[6] = Convert.ToDouble(Ltb.FR6);
                        RLin[6] = Convert.ToDouble(Convert.ToDouble(Ltb.RL6) / 100);

                        break;
                    case 7:
                        ////ViewData["RS7ForeColor"] = System.Drawing.Color.Black;
                        IBin[7] = Convert.ToInt64(Ltb.IB7);
                        RSin[7] = Convert.ToInt64(Ltb.RS7);
                        FRin[7] = Convert.ToDouble(Ltb.FR7);
                        RLin[7] = Convert.ToDouble(Convert.ToDouble(Ltb.RL7) / 100);

                        break;
                    case 8:
                        ////ViewData["RS8ForeColor"] = System.Drawing.Color.Black;
                        IBin[8] = Convert.ToInt64(Ltb.IB8);
                        RSin[8] = Convert.ToInt64(Ltb.RS8);
                        FRin[8] = Convert.ToDouble(Ltb.FR8);
                        RLin[8] = Convert.ToDouble(Convert.ToDouble(Ltb.RL8) / 100);

                        break;
                    case 9:
                        ////ViewData["RS9ForeColor"] = System.Drawing.Color.Black;
                        IBin[9] = Convert.ToInt64(Ltb.IB9);
                        RSin[9] = Convert.ToInt64(Ltb.RS9);
                        FRin[9] = Convert.ToDouble(Ltb.FR9);
                        RLin[9] = Convert.ToDouble(Convert.ToDouble(Ltb.RL9) / 100);

                        break;
                }
                Cnt += 1;
            }
            AdjustForecolorAndClearRemains(Cnt, Ltb);

        }

        static void AdjustForecolorAndClearRemains(int First, LtbDataSet Ltb)
        {

            int Cnt = First;
            while (Cnt <= MaxYear)
            {
                switch (Cnt)
                {

                    case 0:
                        //ViewData["RS0ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB0 = string.Empty;
                        Ltb.RS0 = string.Empty;
                        Ltb.FR0 = string.Empty;
                        Ltb.RL0 = string.Empty;
                        //ViewBag.Year0 = string.Empty;
                        //ViewData["RS1ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB1 = string.Empty;
                        Ltb.RS1 = string.Empty;
                        Ltb.FR1 = string.Empty;
                        Ltb.RL1 = string.Empty;
                        //ViewBag.Year1 = string.Empty;

                        break;
                    case 1:
                        //ViewData["RS2ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB2 = string.Empty;
                        Ltb.RS2 = string.Empty;
                        Ltb.FR2 = string.Empty;
                        Ltb.RL2 = string.Empty;
                        //ViewData["IB2TabIndex"] = 0;
                        //ViewData["RS2TabIndex"] = 0;
                        //ViewData["FR2TabIndex"] = 0;
                        //ViewData["RL2TabIndex"] = 0;
                        //ViewBag.Year2 = string.Empty;
                        break;
                    case 2:
                        //ViewData["RS3ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB3 = string.Empty;
                        Ltb.RS3 = string.Empty;
                        Ltb.FR3 = string.Empty;
                        Ltb.RL3 = string.Empty;
                        //ViewData["IB3TabIndex"] = 0;
                        //ViewData["RS3TabIndex"] = 0;
                        //ViewData["FR3TabIndex"] = 0;
                        //ViewData["RL3TabIndex"] = 0;
                        //ViewBag.Year3 = string.Empty;
                        break;
                    case 3:
                        //ViewData["RS4ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB4 = string.Empty;
                        Ltb.RS4 = string.Empty;
                        Ltb.FR4 = string.Empty;
                        Ltb.RL4 = string.Empty;
                        //ViewData["IB4TabIndex"] = 0;
                        //ViewData["RS4TabIndex"] = 0;
                        //ViewData["FR4TabIndex"] = 0;
                        //ViewData["RL4TabIndex"] = 0;
                        //ViewBag.Year4 = string.Empty;
                        break;
                    case 4:
                        //ViewData["RS5ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB5 = string.Empty;
                        Ltb.RS5 = string.Empty;
                        Ltb.FR5 = string.Empty;
                        Ltb.RL5 = string.Empty;
                        //ViewData["IB5TabIndex"] = 0;
                        //ViewData["RS5TabIndex"] = 0;
                        //ViewData["FR5TabIndex"] = 0;
                        //ViewData["RL5TabIndex"] = 0;
                        //ViewBag.Year5 = string.Empty;
                        break;
                    case 5:
                        //ViewData["RS6ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB6 = string.Empty;
                        Ltb.RS6 = string.Empty;
                        Ltb.FR6 = string.Empty;
                        Ltb.RL6 = string.Empty;
                        //ViewData["IB6TabIndex"] = 0;
                        //ViewData["FS62TabIndex"] = 0;
                        //ViewData["FR6TabIndex"] = 0;
                        //ViewData["RL6TabIndex"] = 0;
                        //ViewBag.Year6 = string.Empty;
                        break;
                    case 6:
                        //ViewData["RS7ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB7 = string.Empty;
                        Ltb.RS7 = string.Empty;
                        Ltb.FR7 = string.Empty;
                        Ltb.RL7 = string.Empty;
                        //ViewData["IB7TabIndex"] = 0;
                        //ViewData["RS7TabIndex"] = 0;
                        //ViewData["FR7TabIndex"] = 0;
                        //ViewData["RL7TabIndex"] = 0;
                        //ViewBag.Year7 = string.Empty;
                        break;
                    case 7:
                        //ViewData["RS8ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB8 = string.Empty;
                        Ltb.RS8 = string.Empty;
                        Ltb.FR8 = string.Empty;
                        Ltb.RL8 = string.Empty;
                        //ViewData["IB8TabIndex"] = 0;
                        //ViewData["RS8TabIndex"] = 0;
                        //ViewData["FR8TabIndex"] = 0;
                        //ViewData["RL8TabIndex"] = 0;
                        //ViewBag.Year8 = string.Empty;
                        break;
                    case 8:
                        //ViewData["RS9ForeColor"] = System.Drawing.Color.Black;
                        Ltb.IB9 = string.Empty;
                        Ltb.RS9 = string.Empty;
                        Ltb.FR9 = string.Empty;
                        Ltb.RL9 = string.Empty;
                        //ViewData["IB9TabIndex"] = 0;
                        //ViewData["RS9TabIndex"] = 0;
                        //ViewData["FR9TabIndex"] = 0;
                        //ViewData["RL9TabIndex"] = 0;
                        //ViewBag.Year9 = string.Empty;
                        break;

                    case 9:
                        //ViewData["IB10TabIndex"] = 0;
                        Ltb.IB10 = string.Empty;
                        break;
                    case 10:
                        break;
                }
                Cnt += 1;
            }
        }
        public static void Calculate(LtbDataSet Ltb)
        {
            NMathConfiguration.LogLocation = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            NMathConfiguration.Init();
            long StockPresent = 0;
            long SafetyPresent = 0;
            //CultureInfo ci = new CultureInfo(A.ThisCulture);
            DateTime StartDate = Convert.ToDateTime(Ltb.LTBDate);
            //Start Date = Today
            DateTime EndOfService = Convert.ToDateTime(Ltb.EOSDate);
            setServiceYears(StartDate, EndOfService);
            MyServiceYears = getServiceYears();

            if (!Information.IsNumeric(Ltb.RepairLeadTime))
            {
                //TextInfo = SireusMvc45.App_LocalResources.LTB_aspx.err1;
                //this.RepairLeadTime.Focus();
                Ltb.InfoText = "Repair Lead Time cannot be empty!";
                //sb.Insert(0, "InfoText=" + TextInfo + " ");
                ////L.LogActivity(sb.ToString(), true);
                return;
            }
            LeadDays = Convert.ToInt32(Ltb.RepairLeadTime);
            if (LeadDays < 1 | LeadDays > 365)
            {
                //TextInfo = SireusMvc45.App_LocalResources.LTB_aspx.err2;
                Ltb.InfoText = "Error: 2 <= Repair Lead Time <=365TextInfo;";
                //sb.Insert(0, "InfoText=" + TextInfo + " ");
                ////L.LogActivity(sb.ToString(), true);
                return;
            }

            ServiceDays = Convert.ToInt32(DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, StartDate, EndOfService));
            if (LeadDays > ServiceDays)
            {
                ClearResult(Ltb);
                //TextInfo = SireusMvc45.App_LocalResources.LTB_aspx.err3;
                Ltb.InfoText = "Error: Repair Lead Time cannot be longer than Service Period. Please change EoS or Repair Lead Time";
                //sb.Insert(0, "InfoText=" + TextInfo + " ");
                ////L.LogActivity(sb.ToString(), true);
                return;
            }

            if (ServiceDays > MaxServiceDays)
            {
                ClearResult(Ltb);
                //TextInfo = SireusMvc45.App_LocalResources.LTB_aspx.err4;
                Ltb.InfoText = "Error: The Service Period cannot be longer than 10 years. Please change EoS or LTB.";
                //sb.Insert(0, "InfoText=" + TextInfo + " ");
                ////L.LogActivity(sb.ToString(), true);
                return;
            }

            N = RoundUpInt(ServiceDays / LeadDays, 0);

            if (Ltb.HasErrors.Count > 0)
            {
                var first = Ltb.HasErrors.First();
                Ltb.InfoText = first.Value;
                return;
            }

            if (!BoundariesOK(MyServiceYears, Ltb))
            {
                return;
            }

            ReadIB(ServiceDays, MyServiceYears, LeadDays, ref Conf_Level, N, Ltb);
            ////sb.Append(" ServiceDays " + ServiceDays.ToString() + " MyServiceYears " + MyServiceYears.ToString() + " LeadDays " + LeadDays.ToString() + " Conf_Level " + Conf_Level + " N " + N.ToString());
            ////L.LogActivity(sb.ToString(), true);

            //sb.Length = 0;
            LTBCommon LTB = new LTBCommon();
            LTB.LTBWorker(N, ServiceDays, LeadDays, MyServiceYears, Conf_Level, ref IBArray, ref RSArray, ref FRArray, ref RLArray,
            ref Stock_Array, ref Returned_Array, ref Demand_Array, ref SumDemand_Array, ref RepairLoss_Array, ref  SumRepairLoss_Array, ref Repair_Array, ref SumRepair_Array, ref SafetyMargin_Array, ref SafetyMarginDayArray, ref FSLeadDaysDemand_Array,
            ref IBin, ref  FRin, ref  RLin, ref  RSin, ref  RSDayArray, ref  RLDayArray, ref  StockDayArray, ref  ReturnedDayArray, ref  SumDemandDayArray);

            //For Chart
            int YearCnt = 0;
            while (YearCnt <= MyServiceYears)
            {
                Ltb.RSYearArray[YearCnt] = RSDayArray[YearCnt * 365 + 1];
                Ltb.StockYearArray[YearCnt] = RoundLong(StockDayArray[YearCnt * 365 + 1] - RSDayArray[YearCnt * 365 + 1], 0);
                FromAverage = RoundLong(GetSafetyFromAverage(Conf_in, SafetyMarginDayArray[YearCnt * 365 + 1]), 0);
                FromGamma = RoundLong(GetSafetyFromGamma(Conf_in, SafetyMarginDayArray[YearCnt * 365 + 1] + ReturnedDayArray[YearCnt * 365 + 1] + FromAverage, ReturnedDayArray[YearCnt * 365 + (int)LeadDays + 1]), 0);
                Ltb.SafetyYearArray[YearCnt] = FromGamma + FromAverage;
                YearCnt = YearCnt + 1;
            }

            StockPresent = RoundLong(Stock_Array[1], 0);
            SafetyPresent = Ltb.SafetyYearArray[0];

            Ltb.Stock = StockPresent.ToString() + GetCLFromAverage(Conf_in, SafetyMargin_Array[1]).ToString();
            //sb.Append(" Stock " + Ltb.Stock);
            if (SafetyPresent > 0)
            {
                FromAverage = GetSafetyFromAverage(Conf_in, SafetyMargin_Array[1]);
                Ltb.Safety = SafetyPresent.ToString() + GetCLFromStock(SafetyMargin_Array[1], FromAverage).ToString();
            }
            else
            {
                Ltb.Safety = string.Empty;
            }
            //sb.Append(" Safety " + Ltb.Safety);

            Ltb.InfoText = "Total Stock:" + Convert.ToString(StockPresent + SafetyPresent);
            Ltb.TotalStock = Convert.ToString(StockPresent + SafetyPresent);
            //sb.Append(" Total " + Ltb.InfoText);
            Ltb.Failed = RoundLong(SumDemand_Array[1], 0).ToString();
            //sb.Append(" Failed " + Ltb.Failed);
            Ltb.Repaired = RoundLong(SumRepair_Array[1] - SumRepairLoss_Array[1], 0).ToString();
            //sb.Append(" Repaired " + Ltb.Repaired);
            if (Ltb.RepairPossible) { Ltb.Lost = RoundUpLong(SumRepairLoss_Array[1], 0).ToString(); } else { Ltb.Lost = "Nothing"; }
            //sb.Append(" Lost " + Ltb.Lost);
            //L.LogActivity(sb.ToString(), true);
        }

        public static void InitYearTabIndex(LtbDataSet ltb)
        {
            if (ltb.EOSDate == null || ltb.LTBDate == null) return;
            int Cnt = 0;
            int NbrOfServiceYears = 0;
            setServiceYears(Convert.ToDateTime(ltb.LTBDate), Convert.ToDateTime(ltb.EOSDate));
            NbrOfServiceYears = getServiceYears();
            Cnt = 0;
            while (Cnt <= NbrOfServiceYears)
            {

                switch (Cnt)
                {
                    case 0:
                        if (ltb.IB1 == "EoS") ltb.IB1 = string.Empty;
                        ltb.IB1ReadOnly = false;
                        break;
                    case 1:
                        if (ltb.IB2 == "EoS") ltb.IB2 = string.Empty;
                        ltb.IB2ReadOnly = false;
                        break;
                    case 2:
                        if (ltb.IB3 == "EoS") ltb.IB3 = string.Empty;
                        ltb.IB3ReadOnly = false;
                        break; ;
                    case 3:
                        if (ltb.IB4 == "EoS") ltb.IB4 = string.Empty;
                        ltb.IB4ReadOnly = false;
                        break; ;
                    case 4:
                        if (ltb.IB5 == "EoS") ltb.IB5 = string.Empty;
                        ltb.IB5ReadOnly = false;
                        break;
                    case 5:
                        if (ltb.IB6 == "EoS") ltb.IB6 = string.Empty;
                        ltb.IB6ReadOnly = false;
                        break;
                    case 6:
                        if (ltb.IB7 == "EoS") ltb.IB7 = string.Empty;
                        ltb.IB7ReadOnly = false;
                        break;
                    case 7:
                        if (ltb.IB8 == "EoS") ltb.IB8 = string.Empty;
                        ltb.IB8ReadOnly = false;
                        break;
                    case 8:
                        if (ltb.IB9 == "EoS") ltb.IB9 = string.Empty;
                        ltb.IB9ReadOnly = false;
                        break;
                    case 9:
                        break;
                }
                Cnt += 1;
            }

            switch (NbrOfServiceYears)
            {
                case 0:
                    ltb.IB1 = "EoS";
                    ltb.RS1 = string.Empty;
                    ltb.RL1 = string.Empty;
                    ltb.FR1 = string.Empty;
                    ltb.IB1ReadOnly = true;
                    ltb.RL1ReadOnly = true;
                    break;
                case 1:
                    ltb.IB2 = "EoS";
                    ltb.RS2 = string.Empty;
                    ltb.RL2 = string.Empty;
                    ltb.FR2 = string.Empty;
                    ltb.IB2ReadOnly = true;
                    ltb.RL2ReadOnly = true;
                    break;
                case 2:
                    ltb.IB3 = "EoS";
                    ltb.RS3 = string.Empty;
                    ltb.RL3 = string.Empty;
                    ltb.FR3 = string.Empty;
                    ltb.IB3ReadOnly = true;
                    ltb.RL3ReadOnly = true;
                    break;
                case 3:
                    ltb.IB4 = "EoS";
                    ltb.RS4 = string.Empty;
                    ltb.RL4 = string.Empty;
                    ltb.FR4 = string.Empty;
                    ltb.RL4ReadOnly = true;
                    ltb.IB4ReadOnly = true;
                    break;
                case 4:
                    ltb.IB5 = "EoS";
                    ltb.RS5 = string.Empty;
                    ltb.RL5 = string.Empty;
                    ltb.FR5 = string.Empty;
                    ltb.IB5ReadOnly = true;
                    ltb.RL5ReadOnly = true;
                    break;
                case 5:
                    ltb.IB6 = "EoS";
                    ltb.RS6 = string.Empty;
                    ltb.RL6 = string.Empty;
                    ltb.FR6 = string.Empty;
                    ltb.IB6ReadOnly = true;
                    ltb.RL6ReadOnly = true;
                    break;
                case 6:
                    ltb.IB7 = "EoS";
                    ltb.RS7 = string.Empty;
                    ltb.RL7 = string.Empty;
                    ltb.FR7 = string.Empty;
                    ltb.IB7ReadOnly = true;
                    ltb.RL7ReadOnly = true;
                    break;
                case 7:
                    ltb.IB8 = "EoS";
                    ltb.RS8 = string.Empty;
                    ltb.RL8 = string.Empty;
                    ltb.FR8 = string.Empty;
                    ltb.IB8ReadOnly = true;
                    ltb.RL8ReadOnly = true;
                    break;
                case 8:
                    ltb.IB9 = "EoS";
                    ltb.RS9 = string.Empty;
                    ltb.RL9 = string.Empty;
                    ltb.FR9 = string.Empty;
                    ltb.IB9ReadOnly = true;
                    ltb.RL9ReadOnly = true;
                    break;
                case 9:
                    ltb.IB10 = "EoS";
                    break;
                case 10:
                    break;
            }
            while (Cnt <= LTBCommon.MaxYear)
            {
                switch (Cnt)
                {
                    case 1:
                        if (NbrOfServiceYears != 0)
                        {
                            ltb.IB1 = string.Empty;
                            ltb.FR1 = string.Empty;
                            ltb.RS1 = string.Empty;
                            ltb.RL1 = string.Empty;
                            ltb.IB1ReadOnly = true;
                            ltb.RL1ReadOnly = true;
                        }
                        break;
                    case 2:
                        if (NbrOfServiceYears != 1)
                        {
                            ltb.IB2 = string.Empty;
                            ltb.FR2 = string.Empty;
                            ltb.RS2 = string.Empty;
                            ltb.RL2 = string.Empty;
                            ltb.IB2ReadOnly = true;
                            ltb.RL2ReadOnly = true;
                        }
                        break;
                    case 3:
                        if (NbrOfServiceYears != 2)
                        {
                            ltb.IB3 = string.Empty;
                            ltb.FR3 = string.Empty;
                            ltb.RS3 = string.Empty;
                            ltb.RL3 = string.Empty;
                            ltb.IB3ReadOnly = true;
                            ltb.RL3ReadOnly = true;
                        }
                        break;
                    case 4:
                        if (NbrOfServiceYears != 3)
                        {
                            ltb.IB4 = string.Empty;
                            ltb.FR4 = string.Empty;
                            ltb.RS4 = string.Empty;
                            ltb.RL4 = string.Empty;
                            ltb.IB4ReadOnly = true;
                            ltb.RL4ReadOnly = true;
                        }
                        break;
                    case 5:
                        if (NbrOfServiceYears != 4)
                        {
                            ltb.IB5 = string.Empty;
                            ltb.FR5 = string.Empty;
                            ltb.RS5 = string.Empty;
                            ltb.RL5 = string.Empty;
                            ltb.IB5ReadOnly = true;
                            ltb.RL5ReadOnly = true;
                        }
                        break;
                    case 6:
                        if (NbrOfServiceYears != 5)
                        {
                            ltb.IB6 = string.Empty;
                            ltb.FR6 = string.Empty;
                            ltb.RS6 = string.Empty;
                            ltb.RL6 = string.Empty;
                            ltb.IB6ReadOnly = true;
                            ltb.RL6ReadOnly = true;
                        }
                        break;
                    case 7:
                        if (NbrOfServiceYears != 6)
                        {
                            ltb.FR7 = string.Empty;
                            ltb.RS7 = string.Empty;
                            ltb.RL7 = string.Empty;
                            ltb.IB7 = string.Empty;
                            ltb.IB7ReadOnly = true;
                            ltb.RL7ReadOnly = true;
                        }
                        break;
                    case 8:
                        if (NbrOfServiceYears != 7)
                        {
                            ltb.IB8 = string.Empty;
                            ltb.FR8 = string.Empty;
                            ltb.RS8 = string.Empty;
                            ltb.RL8 = string.Empty;
                            ltb.IB8ReadOnly = true;
                            ltb.RL8ReadOnly = true;
                        }
                        break;
                    case 9:
                        if (NbrOfServiceYears != 8)
                        {
                            ltb.IB9 = string.Empty;
                            ltb.FR9 = string.Empty;
                            ltb.RS9 = string.Empty;
                            ltb.RL9 = string.Empty;
                            ltb.IB9ReadOnly = true;
                            ltb.RL9ReadOnly = true;
                        }
                        break;
                    case 10:
                        if (NbrOfServiceYears != 9)
                        {
                            ltb.IB10 = string.Empty;

                        }

                        break;
                }
                Cnt += 1;
            }
            AdjustRepair(ltb);
        }
        static void AdjustRepair(LtbDataSet ltb)
        {
            int Cnt = 0;
            int NbrOfServiceYears = 0;
            NbrOfServiceYears = getServiceYears();
            while (Cnt <= NbrOfServiceYears)
            {
                switch (Cnt)
                {
                    case 0:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL0ReadOnly = true;
                            ltb.RL0 = "100";
                        }
                        else
                        {
                            ltb.RL0ReadOnly = false;
                        }
                        break;
                    case 1:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL1ReadOnly = true;
                            ltb.RL1 = "100";
                        }
                        else
                        {
                            ltb.RL1ReadOnly = false;
                        }
                        break;
                    case 2:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL2ReadOnly = true;
                            ltb.RL2 = "100";
                        }
                        else
                        {
                            ltb.RL2ReadOnly = false;
                        }
                        break;
                    case 3:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL3ReadOnly = true;
                            ltb.RL3 = "100";
                        }
                        else
                        {
                            ltb.RL3ReadOnly = false;
                        }
                        break;
                    case 4:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL4ReadOnly = true;
                            ltb.RL4 = "100";
                        }
                        else
                        {
                            ltb.RL4ReadOnly = false;
                        }
                        break;
                    case 5:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL5ReadOnly = true;
                            ltb.RL5 = "100";
                        }
                        else
                        {
                            ltb.RL5ReadOnly = false;
                        }
                        break;
                    case 6:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL6ReadOnly = true;
                            ltb.RL6 = "100";
                        }
                        else
                        {
                            ltb.RL6ReadOnly = false;
                        }
                        break;
                    case 7:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL7ReadOnly = true;
                            ltb.RL7 = "100";
                        }
                        else
                        {
                            ltb.RL7ReadOnly = false;
                        }
                        break;
                    case 8:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL8ReadOnly = true;
                            ltb.RL8 = "100";
                        }
                        else
                        {
                            ltb.RL8ReadOnly = false;
                        }
                        break;
                    case 9:
                        if (!ltb.RepairPossible)
                        {
                            ltb.RL9ReadOnly = true;
                            ltb.RL9 = "100";
                        }
                        else
                        {
                            ltb.RL9ReadOnly = false;
                        }

                        break;
                }
                Cnt += 1;
            }
        }
    }
}
