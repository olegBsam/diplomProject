using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroPrognoz.NeuroNetwork.Functions;

namespace NeuroPrognoz.NeuroNetwork
{
    public class NeuroNet
    {
        NeuronLayer neuronLayer;

        public NeuroNet(int neuronCount, AbstractFunctions function, double lambda)
        {
            neuronLayer = new NeuronLayer(neuronCount, lambda, function);
        }

        public double CalculateNetOutput(double[] x)
        {
            return neuronLayer.CalculateOutput(x);
        }

        public void Learning(List<KeyValuePair<double, double[]>> learningSet, int learningIterationCount)
        {
            List<double> errors = new List<double>();

            double errorThreshold = 0.05;
            double error = double.MaxValue;
            int iteration = 0;

            while (error > errorThreshold && iteration < learningIterationCount)
            {
                double sum = 0;

                List<double> output = new List<double>();
                output = learningSet.ElementAt(0).Value.ToList();

                for (int i = 0; i < learningSet.Count(); i++)
                {
                    var set = learningSet.ElementAt(i);

                    double netOutput = CalculateNetOutput(output.ToArray());

                    neuronLayer.Learning(netOutput - set.Key, output.ToArray());

                    sum += Math.Pow(set.Key - netOutput, 2);

                    output.RemoveAt(0);
                    output.Add(netOutput);
                }
                error = Math.Sqrt(sum / (learningSet.Count() - 1));
                errors.Add(error);
                iteration++;
            }

            errors.Count();
            errors.Last();

        }

        public double[] Work(NeuroNet nn, double[] x, int n)
        {
            List<double> outputs = new List<double>();

            var input = x.ToList();

            for (int i = 0; i < n; i++)
            {
                double output = nn.CalculateNetOutput(input.ToArray());
                outputs.Add(output);

                input.RemoveAt(0);
                input.Add(output);
            }


            return outputs.ToArray();
        }
    }
}
