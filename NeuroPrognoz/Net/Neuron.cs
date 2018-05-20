using System;

namespace Net
{
    class Neuron
    {
        public double weight;
        public double center;
        public double radius;
        const double koef = 15.0 / 16.0;
        public static Random random;

        static Neuron(){
            random = new Random();
        }
        public Neuron()
        {
            weight = random.NextDouble();
            center = random.NextDouble();
            radius = random.NextDouble();

            if (double.IsNaN(weight) | double.IsNaN(center) | double.IsNaN(radius))
            {
                throw new Exception();
            }
        }

        public double OutputCalculate(double input)
        {
            return ActivationFunctionCalculate(input) * weight;
        }

        protected virtual double ActivationFunctionCalculate(double input)
        {
            
            double result = (input - center) / (2.0 * Math.Pow(radius, 2));
            result = Math.Pow(1.0 - result, 2) * (15.0 / 16.0);
            if (double.IsNaN(result))
            {
                throw new Exception();
            }
            
         //   double result = Math.Exp(-Math.Pow(input - center, 2) / (2 * Math.Pow(radius, 2)));
            return result;
        }

        public void Correction(double y, double d, double lambda, double x)
        {
            double w = weight - lambda * WeightDerivative(y, d, x);
            double c = center - lambda * CenterDerivative(y, d, x);
            double r = radius - lambda * RadiusDerivative(y, d, x);

            weight = w;
            center = c;
            radius = r;
            if (double.IsNaN(weight) | double.IsNaN(center) | double.IsNaN(radius))
            {
                throw new Exception();
            }
        }

        private double WeightDerivative(double y, double d, double x)
        {
            double result = (y - d) * ActivationFunctionCalculate(x) * (ActivationFunctionCalculate(x) * weight - d);
            return result;
        }
        private double CenterDerivative(double y, double d, double x)
        {
            double result = 15.0/16.0 * weight * Math.Pow(1 - ((x - center)/(2 * Math.Pow(radius, 2))), 2);
            result *= 15.0 / 16.0 * weight * (1 - ((x - center) / (2 * Math.Pow(radius, 2)))) * 1 / Math.Pow(radius, 2);
            /*
            double fragment = (center + 2.0 * Math.Pow(radius, 2) - x);
            double result = 15.0 * weight * fragment * (15.0 * weight * Math.Pow(fragment, 2) - 64.0 * d * Math.Pow(radius, 4));
            result = result / (2048.0 * Math.Pow(radius, 8));
            */
            return result;
        }
        private double RadiusDerivative(double y, double d, double x)
        {
            double fr = 15.0 / 16.0 * weight * (1 - ((x - center) / (2 * Math.Pow(radius, 2))));
            double result = (fr * (1 - ((x - center) / (2 * Math.Pow(radius, 2))) - d)) * (fr * 2) * (-3 * (center - x) / 2) / Math.Pow(radius, 3);
            /*
            double fragment = (center + 2.0 * Math.Pow(radius, 2) - x);
            double result = -15.0 * weight * (center - x) * fragment * (15.0 * weight * fragment * fragment - 64.0 * d * Math.Pow(radius, 4));
            result = result / (1024.0 * Math.Pow(radius, 9));
            */
            return result;
        }
    }
}

/*
 * 
 * sigma = C max / sqrt(2*K), где К - число нейронов
 * */