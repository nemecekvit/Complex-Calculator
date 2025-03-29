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
        private MainWindow mainWindow;
        string[] cache;
        public FrontBackConnector(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            cache = fileManager.loadLastMemory();
        }

        public string evaluateExpression(string expression)
        {
            cmplxNum result = Evaluator.Evaluate(expression);
            return result.ToString();
        }

        public void validateExpression(string expression)
        {
            Evaluator.validateInput(expression);
        }
            


    }
}
