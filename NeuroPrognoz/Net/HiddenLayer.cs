using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net
{
    class HiddenLayer
    {
        Neuron[] neurons;

        double[] outputs;
        double lambda;
        double neuronsCount;

        public HiddenLayer(int neuronsCount, double lambda)
        {
            this.lambda = lambda;
            this.neuronsCount = neuronsCount;

            outputs = new double[neuronsCount];
            neurons = new Neuron[neuronsCount];

            for (int i = 0; i < neuronsCount; i++)
            {
                neurons[i] = new Neuron();
                if (double.IsNaN(neurons[i].center))
                {
                    int a = 0;
                }
            }
        }

        private void CalculateOutput(double[] x)
        {
            for (int i = 0; i < neuronsCount; i++)
            {
               outputs[i] = neurons[i].OutputCalculate(x[i]);
            }
        }

        public void Correction(double y, double d)
        {
            for (int i = 0; i < neuronsCount; i++)
            {
                neurons[i].Correction(y, d, lambda);
            }
        }

        public double SumNeuronsOutput(double[] inputs)
        {
            CalculateOutput(inputs);
            return outputs.Sum();
        }
    }
}
