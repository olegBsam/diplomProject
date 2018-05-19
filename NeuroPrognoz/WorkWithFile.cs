using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuroPrognoz
{
    public static class WorkWithFile
    {
        public static double[] LoadingSequence()
        {
            OpenFileDialog openFileDialog_OpenData = new OpenFileDialog();
            openFileDialog_OpenData.ShowDialog();
            var data = new Dictionary<string, List<string>>();

            if (openFileDialog_OpenData.FileName != null && openFileDialog_OpenData.FileName != string.Empty)
            {
                data = OpenCSV(openFileDialog_OpenData.FileName);
            }


            int count = data["Volume"].Count();
            double[] inputMas = new double[count];

            for (int i = 0; i < count; i++)
            {
                inputMas[i] = double.Parse(data["Volume"][i]);
            }
            return inputMas;
        }

        private static Dictionary<string, List<string>> OpenCSV(string path)
        {
            var data = new Dictionary<string, List<string>>();
            using (TextFieldParser tfp = new TextFieldParser(path))
            {
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(",");

                string[] headers = tfp.ReadFields();

                List<List<string>> dictLists = new List<List<string>>();


                for (int i = 0; i < headers.Length; i++)
                {
                    dictLists.Add(new List<string>());
                }

                while (!tfp.EndOfData)
                {
                    string[] fields = tfp.ReadFields();
                    for (int i = 0; i < fields.Length; i++)
                    {
                        dictLists[i].Add(fields[i]);
                    }
                }

                for (int i = 0; i < headers.Length; i++)
                {
                    data.Add(headers[i], dictLists[i]);
                }
            }
            return data;
        }
    }
}
