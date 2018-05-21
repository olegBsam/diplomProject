using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroPrognoz.NeuroNetwork.Functions
{
    public abstract class AbstractFunctions
    {
        public abstract double CalculateFunction(double x, double w, double c, double r);
        public abstract double DerivativeEonW(double y_d, double function);
        public abstract double DerivativeEonC(double y_d, double x, double w, double c, double r, double function);
        public abstract double DerivativeEonR(double y_d, double x, double w, double c, double r, double function);

        public double CalculateError(double y, double d)
        {
            double e = 0.5 * Math.Pow(y - d, 2);
            return e;            
        }
    } 
}
