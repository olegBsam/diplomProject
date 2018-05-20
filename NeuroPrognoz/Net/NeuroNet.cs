using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net
{
    public class NeuroNet
    {
        HiddenLayer hiddenLayer;

        public NeuroNet(int neuronCount, double lambda)
        {
            hiddenLayer = new HiddenLayer(neuronCount, lambda);
        }

        public double CalculateNetOutput(double[] inputs)
        {
            return hiddenLayer.SumNeuronsOutput(inputs);
        }
        
        public void Learning(List<KeyValuePair<double, double[]>> learningSets, int learningIterationsCount)
        {
            List<double> errMas = new List<double>(); 
            double errThreshold = 0.05;
            double err = double.MaxValue;
            int iteration = 0;

            while (err > errThreshold && iteration < 1500)
            {
                double sum = 0;
                for (int i = 0; i < learningSets.Count(); i++)
                {
                    var set = learningSets.ElementAt(i);
                    double netOutput = CalculateNetOutput(set.Value);
                    hiddenLayer.Correction(netOutput, set.Key, set.Value);
                    if (double.IsNaN(netOutput))
                    {
                        int a = 0;
                    }
                    sum += Math.Pow(set.Key - netOutput, 2);
                    // errMas.Add(ErrCalculate(netOutput, set.Key));
                }

                err = sum / (learningSets.Count());
                errMas.Add(err);
                iteration++;
            }
            errMas.Count();
            errMas.Last();
        }

        public double ErrCalculate(double y, double d)
        {
            double err;
            err = (1 / 2) * Math.Pow(y - d, 2);
            return err;
        }
    }
}