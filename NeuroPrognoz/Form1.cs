using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NeuroPrognoz.NeuroNetwork;
using NeuroPrognoz.NeuroNetwork.Functions;

namespace NeuroPrognoz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            double[] inputMas = WorkWithFile.LoadingSequence();
            /*
            {
                Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
                //Книга.
                ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
                //Таблица.
                ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
                // Заполняем значениями.
                //Значения [y - строка,x - столбец]int 
                int iLastRow = ObjWorkSheet.Cells[ObjWorkSheet.Rows.Count, "A"].End[Microsoft.Office.Interop.Excel.XlDirection.xlUp].Row;  //последняя заполненная строка в столбце А
                for (int i = 1; i < inputMas.Length; i++)
                {
                    iLastRow++;
                    ObjWorkSheet.Cells[iLastRow, "A"].Value = inputMas[i];
                }
               // В итоге, делаем созданную эксельку видимой и доступной!
                ObjExcel.Visible = true;
                ObjExcel.UserControl = true;
            }
            */
            double maxValue = inputMas.Max();
            double minValue = inputMas.Min();
            double[] data = normalizing(inputMas, maxValue, minValue);
           
            int count = data.Length;
            int learnCount = count - 100;

            int neuronCount = 32;
            var learnSet = convertToLearnSet(data, 0, learnCount, neuronCount);

            double lambda = 0.01;

            NeuroNet nn = new NeuroNet(neuronCount, new GaussFunction(), lambda);

            nn.Learning(learnSet, 5000);

            var sets = convertToLearnSet(data, learnCount - neuronCount, data.Length, neuronCount);
            var result = nn.Work(nn, sets.ElementAt(0).Value, sets.Count());

            double[] val = new double[sets.Count()];
            double sum = 0;
            for (int i = 0; i < sets.Count(); i++)
            {
                sum += Math.Pow(sets.ElementAt(i).Key - result[i], 2);
                val[i] = sets.ElementAt(i).Key;
            }
            double error = Math.Sqrt(sum / (sets.Count() - 1));
            int a = 9;

            try
            {
                chart1.Series.Add("s1");
                chart1.Series["s1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["s1"].Color = Color.Black;
                for (int i = 0; i < result.Length; i++)
                {
                    chart1.Series["s1"].Points.AddY(result[i]);
                }
              

                chart1.Series.Add("s2");
                chart1.Series["s2"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series["s2"].Color = Color.Red;
                for (int i = 0; i < result.Length; i++)
                {
                    chart1.Series["s2"].Points.AddY(val[i]);
                }
            }
            catch (Exception e)
            {
                a = 12;
            }
        }

        public double[] normalizing(double[] mas, double maxVal, double minVal)
        {
            double count = mas.Length;
            /*
            double v = minVal / maxVal;
            for (int i = 0; i < count; i++)
            {
                mas[i] = mas[i] / maxVal - v;
            }

            */
            for (int i = 0; i < count; i++)
            {
                mas[i] = 2 * (mas[i] - 1/2 * (maxVal - minVal)) / (maxVal - minVal);
            }

            return mas;
        }

        public List<KeyValuePair<double, double[]>> convertToLearnSet(double[] mas, int start, double end, int neuronCount)
        {
            var learnSet = new List<KeyValuePair<double, double[]>>();

            for (int i = start; i < end - neuronCount; i++)
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
