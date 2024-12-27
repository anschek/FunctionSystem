using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionSystem
{
    internal class Logarithmic
    {
        // ln a
        internal static double _base(double x) => Math.Log(x);
        // log a (x)
        internal static double Log(double @base, double arg, Func<double,double>?ln=null)
        {
            ln ??= _base;
            return ln(arg)/ln(@base);
        }
        // second function from task
        public static double SystemFunc(double x, Func<double,double>?ln=null, Func<double, double,double>? log = null)
        {

            if(x<=0) return double.NaN;

            ln ??= _base;
            log ??= (b, arg) => Log(b, arg, _base);                 
            return (log(5, Math.Pow(x,3)) * log(10, x) + log(3, x)) / ln(x)
                + ln(x) * log(5, x);
        }
    }
}
