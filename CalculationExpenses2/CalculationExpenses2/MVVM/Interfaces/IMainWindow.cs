using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CalculationExpenses2.MVVM.Interfaces
{
    public interface IMainWindow
    {
        UIElementCollection GetAllControls();

        void GenerateControls();
    }
}
