using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Численные_методы_3
{
    class MyFunctions
    {
        public double k1(double x)
        {
            return x + 1;
        }
        public double k2(double x)
        {
            return 1;
        }
        public double q1(double x)
        {
            return Math.Exp(-x);
        }
        public double q2(double x)
        {
            return Math.Exp(-x * x);
        }
        public double f1(double x)
        {
            return Math.Cos(x);
        }
        public double f2(double x)
        {
            return 1;
        }        
    }
}
