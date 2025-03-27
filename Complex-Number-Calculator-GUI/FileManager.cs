using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Complex_Number_Calculator_GUI
{
    class FileManager
    {
        private string saveExpressionsPath = "savedExpressions.csv";

        private void checkIfFileExists(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public string[] loadLastMemory()
        {
            return loadExpressionsFromCVS(saveExpressionsPath);
        }

        public void saveToMemory(string[] expressions)
        {
            saveExpressionToCVS(saveExpressionsPath, expressions);
        }

        public string[] loadExpressionsFromCVS(string path)
        {
            checkIfFileExists(path);

            StreamReader reader = new StreamReader(path, Encoding.Default);
            
            string[] expressions = reader.ReadToEnd().Split('\n');
            reader.Close();
            return expressions;
      
        }
        public void saveExpressionToCVS(string path, string[] expression)
        {
            checkIfFileExists(path);

            StreamWriter writer = new StreamWriter(path, false, Encoding.Default);

            foreach (string exp in expression)
            {
                writer.WriteLine(exp);
            }
            writer.Close();
        }
    }
}
