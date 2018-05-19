using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Net;

namespace NeuroPrognoz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            double[] inputMas = WorkWithFile.LoadingSequence();
            double maxValue = inputMas.Max();       
            double[] data = normalizing(inputMas, maxValue);


            int count = data.Length;
            int learnCount = count - 1000;

            int neuronCount = 20;
            var learnSet = convertToLearnSet(data.Take(learnCount).ToArray(), neuronCount);

            double lambda = 0.001;

            NeuroNet nn = new NeuroNet(neuronCount, lambda);
            nn.Learning(learnSet, 200);


        }

        public double[] normalizing(double[] mas, double maxVal)
        {
            double count = mas.Length;
            for (int i = 0; i < count; i++)
            {
                mas[i] = mas[i] / maxVal;
            }
            return mas;
        }

        public List<KeyValuePair<double, double[]>> convertToLearnSet(double[] mas, int neuronCount)
        {
            var learnSet = new List<KeyValuePair<double, double[]>>();

            for (int i = 0; i < mas.Length - neuronCount; i++)
            {
                double[] previousValue = new double[neuronCount];
                for (int j = 0; j < neuronCount; j++)
                {
                    previousValue[j] = mas[i + j];
                }

                var set = new KeyValuePair<double, double[]>(mas[i + neuronCount], previousValue);
                learnSet.Add(set);
            }
            return learnSet;
        }
    }
}
