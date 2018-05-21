using NeuroPrognoz.NeuroNetwork.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroPrognoz.NeuroNetwork
{
    public class NeuronLayer
    {
        Neuron[] neurons;
        double lambda;

        public NeuronLayer(int neuronCount, double lambda, AbstractFunctions function)
        {
            this.lambda = lambda;
            neurons = new Neuron[neuronCount];

            Random rand = new Random();

            double maxC = 0.0;

            //инициализация весов и центров
            for (int i = 0; i < neuronCount; i++)
            {
                neurons[i] = new Neuron();

                double center = rand.NextDouble();
                double weight = rand.NextDouble();

                if (center > maxC)
                {
                    maxC = center;
                }

                neurons[i].FunctionsModule = function;
                neurons[i].Initial(center, weight);
            }
            //sigma = C max / sqrt(2 * K), где К -число нейронов
            //инициализация радиусов
            double radius = maxC / Math.Sqrt(2 * neuronCount);
            for (int i = 0; i < neuronCount; i++)
            {
                neurons[i].Radius = radius;
            }
        }

        public double CalculateOutput(double[] x)
        {
            double sum = 0;
            for (int i = 0; i < neurons.Length; i++)
            {
                sum += neurons[i].CalculationOutput(x[i]);
            }
            return sum;
        }

        public void Learning(double y_d, double[] x)
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].Learning(y_d, x[i], lambda);
            }
        }
    }
}
