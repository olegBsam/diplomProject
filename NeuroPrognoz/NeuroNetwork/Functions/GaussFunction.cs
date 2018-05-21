using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroPrognoz.NeuroNetwork.Functions
{
    class GaussFunction : AbstractFunctions
    {
        public override double CalculateFunction(double x, double w, double c, double r)
        {
            double result = Math.Exp(-( Math.Pow(x - c, 2) / (2 * Math.Pow(r, 2))));
            return result;
        }

        public override double DerivativeEonC(double y_d, double x, double w, double c, double r, double function)
        {
            double result = y_d * w * function * ((x - c) / Math.Pow(r, 2));
            return result;
        }

        public override double DerivativeEonR(double y_d, double x, double w, double c, double r, double function)
        {
            double result = DerivativeEonC(y_d, x, w, c, r, function) * (x - c) / r;
            return result;
        }

        public override double DerivativeEonW(double y_d, double function)
        {
            double result = y_d * function;
            return result;
        }
    }
}
