using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex_Number_Calculator_GUI
{
    internal class FrontBackConnector
    {
        private FileManager fileManager = new FileManager();
        private Evaluator evaluator = new Evaluator();
        private MainWindow mainWindow;
        private string[] loadedMemory = new string[3];

        public string[] currentMemory = new string[3];

        public FrontBackConnector(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public string evaluateExpression(string expression)
        {
            evaluator.currentMemory = currentMemory;
            cmplxNum result = evaluator.Evaluate(expression);
            return result.ToString();
        }

        public void validateExpression(string expression)
        {
            evaluator.validateInput(expression);
        }
            
        
        private void copyMemory(string[] source, string[] destination)
        {
            for (int i = 0; i < source.Length; i++)
            {
                destination[i] = source[i];
            }
        }

        private void saveMemory(string[] memory)
        {
            fileManager.saveToMemory(memory);
        }
    }
}
