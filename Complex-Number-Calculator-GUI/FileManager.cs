using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Complex_Number_Calculator_GUI
{
    class FileManager
    {

        private void checkIfFileExists(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public string[] loadSavedExpressionsFromCVS()
        {
            string path = "savedExpressions.csv";

            checkIfFileExists(path);

            StreamReader reader = new StreamReader(path, Encoding.Default);
            
            string[] expressions = reader.ReadToEnd().Split('\n');
            reader.Close();
            return expressions;
      
        }
        public void saveExpressionToCVS(string[] expression)
        {
            string path = "savedExpressions.csv";

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
