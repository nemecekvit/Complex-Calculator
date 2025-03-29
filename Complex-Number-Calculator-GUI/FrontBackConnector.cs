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
        public FrontBackConnector(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

    }
}
