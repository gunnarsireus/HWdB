using CenterSpace.NMath.Stats;
using HWdB.Model;
using System;

namespace HWdB.Utils
{
    class Mathematics
    {
        public static int ServiceYears(LtbDataSet ltbDataSet)
        {
            if (Convert.ToDateTime(ltbDataSet.LTBDate).Year >= Convert.ToDateTime(ltbDataSet.EOSDate).Year)
            {
                return 0;
            }
            var newYear = Convert.ToDateTime(Convert.ToDateTime(ltbDataSet.LTBDate).Year.ToString() + "-01-01");
            if (Mathematics.IsLeapYear(Convert.ToDateTime(ltbDataSet.LTBDate).Year) & DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, newYear, Convert.ToDateTime(ltbDataSet.LTBDate)) < 59)
            {
                return Convert.ToInt32((DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, Convert.ToDateTime(ltbDataSet.LTBDate), Convert.ToDateTime(ltbDataSet.EOSDate)) + Mathematics.CountLeaps(Convert.ToDateTime(ltbDataSet.LTBDate).Year) - Mathematics.CountLeaps(Convert.ToDateTime(ltbDataSet.EOSDate).Year) - 2) / 365);
            }
            return Convert.ToInt32((DateTimeUtil.DateDiff(DateTimeUtil.DateInterval.Day, Convert.ToDateTime(ltbDataSet.LTBDate), Convert.ToDateTime(ltbDataSet.EOSDate)) + Mathematics.CountLeaps(Convert.ToDateTime(ltbDataSet.LTBDate).Year) - Mathematics.CountLeaps(Convert.ToDateTime(ltbDataSet.EOSDate).Year) - 1) / 365);
        }
        static int Calcreserve2(int m, double failureRate, double p)
        {
            switch (RoundLong(p * 1000, 0))
            {
                case 600: return (int)RoundLong(m + (133 * Sqr((double)m) / 100), 0);
                case 700: return (int)RoundLong(m + (156 * Sqr((double)m) / 100), 0);
                case 800: return (int)RoundLong(m + (184 * Sqr((double)m) / 100), 0);
                case 900: return (int)RoundLong(m + (223 * Sqr((double)m) / 100), 0);
                case 950: return (int)RoundLong(m + (255 * Sqr((double)m) / 100), 0);
                case 995: return (int)RoundLong(m + (340 * Sqr((double)m) / 100), 0);
                default: return (int)RoundLong(m + (340 * Sqr((double)m) / 100), 0);
            }
        }

        public static int Calcreserve(int m, double failureRate, double p)
        {
            if (m * failureRate > 10000) return Calcreserve2(m, failureRate, p);
            var i = 0;
            double pp = 0; // init prob
            while (pp < p) // loop while cumaltive probability less than p
            {
                int k; // init counter
                for (k = 0; k <= i; k++)
                    /* calculate probability */
                    pp = pp + Math.Exp(-StatsFunctions.GammaLn(i + 2) - StatsFunctions.GammaLn(k + 1) + 2.0 * Math.Log((double)i + 1 - k) - 2.0 * failureRate * m + (k + i) * Math.Log(failureRate * m));
                i = i + 1;
            }
            return (i - 1);
        }
        public static bool IsLeapYear(long Y)
        {
            return (Y > 0) && (Y % 4) == 0 && !((Y % 100) == 0 && (Y % 400) != 0);
        }
        public static long CountLeaps(long y)
        {
            return (y - 1) / 4 - (y - 1) / 100 + (y - 1) / 400;
        }

        public static double Sqr(double x)
        {
            return Math.Pow(x, 0.5);
        }

        public static long RoundUpLong(double x, int Y)
        {
            return Convert.ToInt64(Math.Round(x + 0.49999999999, Y));
        }
        public static long RoundLong(double x, int Y)
        {
            return Convert.ToInt64(Math.Round(x, Y));
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
            const double pLow = 0.02425, pHigh = 1 - pLow;
            double q = 0;
            functionReturnValue = 0;
            if (p < 0 | p > 1)
            {
                // Err.Raise(Constants.vbObjectError, "", "NormSInv: Argument out of range.");
            }
            else if (p < pLow)
            {
                q = Sqr(-2 * Math.Log(p));
                functionReturnValue = (((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
            }
            else if (p <= pHigh)
            {
                q = p - 0.5;
                var r = q * q;
                functionReturnValue = (((((a1 * r + a2) * r + a3) * r + a4) * r + a5) * r + a6) * q / (((((b1 * r + b2) * r + b3) * r + b4) * r + b5) * r + 1);
            }
            else
            {
                q = Sqr(-2 * Math.Log(1 - p));
                functionReturnValue = -(((((c1 * q + c2) * q + c3) * q + c4) * q + c5) * q + c6) / ((((d1 * q + d2) * q + d3) * q + d4) * q + 1);
            }
            return functionReturnValue;
        }
        public static double ConfidenceLevelFromNormsInv(double Y)
        {
            return NormSInv(Y);
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
            var z = 1.0 / (x * x);
            var sum = c[7];
            for (var i = 6; i >= 0; i--)
            {
                sum *= z;
                sum += c[i];
            }
            var series = sum / x;

            const double halfLogTwoPi = 0.91893853320467274178032973640562;
            var logGamma = (x - 0.5) * Math.Log(x) - x + halfLogTwoPi + series;
            return logGamma;
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

                var y = x;
                var n = 0;
                bool argWasLessThanOne = (y < 1.0);

                // Add or subtract integers as necessary to bring y into (1,2)
                // Will correct for this below
                if (argWasLessThanOne)
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

                var num = 0.0;
                var den = 1.0;
                int i;

                var z = y - 1;
                for (i = 0; i < 8; i++)
                {
                    num = (num + p[i]) * z;
                    den = den * z + q[i];
                }
                var result = num / den + 1.0;

                // Apply correction if argument was not initially in (1,2)
                if (argWasLessThanOne)
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

            if (!(x > 171.624)) return Math.Exp(LogGamma(x));
            // Correct answer too large to display. Force +infinity.
            const double temp = double.MaxValue;
            return temp * 2.0;
        }
    }
}
