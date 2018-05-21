using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroPrognoz.NeuroNetwork.Functions;

namespace NeuroPrognoz.NeuroNetwork
{
    public class Neuron
    {
        private double center;
        private double radius;
        private double weight;

        public AbstractFunctions FunctionsModule { get; set; }

        public void Initial(double c, double w)
        {
            center = c;
            weight = w;
        }

        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public double CalculationOutput(double x)
        {
            double result = FunctionsModule.CalculateFunction(x, weight, center, radius) * weight;
            return result;
        }

        public void Learning(double y_d, double x, double lambda)
        {
            double func = FunctionsModule.CalculateFunction(x, weight, center, radius);
            double w = weight - lambda * FunctionsModule.DerivativeEonW(y_d, func);
            double c = center - lambda * FunctionsModule.DerivativeEonC(y_d, x, weight, center, radius, func);
            double r = radius - lambda * FunctionsModule.DerivativeEonR(y_d, x, weight, center, radius, func);

            weight = w;
            center = c;
            radius = r;
        }
        
    }
}
