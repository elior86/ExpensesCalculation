using CalculationExpenses2.ConfigurationXml;
using CalculationExpenses2.MVVM.Interfaces;
using CalculationExpenses2.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculationExpenses2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            GenerateControls();
            (DataContext as ItemControlViewModel).mainWindow = this as IMainWindow;
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);

            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        public void GenerateControls()
        {
            Dictionary<string,string> categoriesAndBusiness = ReadConfiguration.Instance.GetCategoriesAndBusiness();
            int j = 0;
            int z = 0;
            for (int i = 0; i < categoriesAndBusiness.Count; i++)
            {
                RowDefinition gridRow0 = new RowDefinition();
                gridRow0.Height = GridLength.Auto;
                CalculationsSumAndText.RowDefinitions.Add(gridRow0);

                RowDefinition gridRow1 = new RowDefinition();
                gridRow1.Height = GridLength.Auto;
                CalculationsSumAndText.RowDefinitions.Add(gridRow1);

                TextBlock tbCategoryName = new TextBlock();
                tbCategoryName.Text = categoriesAndBusiness.Keys.ElementAt(j);
                tbCategoryName.Margin = new Thickness(10);
                tbCategoryName.FontWeight = FontWeights.Bold;
                tbCategoryName.HorizontalAlignment = HorizontalAlignment.Center;
                tbCategoryName.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(tbCategoryName, i+1+z);
                Grid.SetColumn(tbCategoryName, 0);
                this.CalculationsSumAndText.Children.Add(tbCategoryName);

                TextBlock tbDeatils = new TextBlock();
                tbDeatils.Text = "פירוט";
                tbDeatils.Margin = new Thickness(10);
                tbDeatils.FontWeight = FontWeights.Bold;
                Grid.SetRow(tbDeatils, i+1+z);
                Grid.SetColumn(tbDeatils, 1);
                this.CalculationsSumAndText.Children.Add(tbDeatils);

                TextBox tbCategorySum = new TextBox();
                tbCategorySum.Text = "0";
                tbCategorySum.Name = categoriesAndBusiness.Keys.ElementAt(j) + "סכום";
                tbCategorySum.IsReadOnly = true;
                tbCategorySum.Margin = new Thickness(10);
                tbCategorySum.FontWeight = FontWeights.Bold;
                tbCategorySum.HorizontalAlignment = HorizontalAlignment.Center;
                tbCategorySum.VerticalAlignment = VerticalAlignment.Center;
                tbCategorySum.FlowDirection = System.Windows.FlowDirection.RightToLeft;
                tbCategorySum.MouseDoubleClick += TextBox_MouseDoubleClick;
                Grid.SetRow(tbCategorySum, i+2+z);
                Grid.SetColumn(tbCategorySum, 0);
                this.CalculationsSumAndText.Children.Add(tbCategorySum);

                TextBox tbBusinessNames = new TextBox();
                tbBusinessNames.Text = "";
                tbBusinessNames.Name = categoriesAndBusiness.Keys.ElementAt(j) + "פירוט";
                tbBusinessNames.IsReadOnly = true;
                tbBusinessNames.Margin = new Thickness(10);
                tbBusinessNames.FontWeight = FontWeights.Bold;
                tbBusinessNames.HorizontalAlignment = HorizontalAlignment.Left;
                tbBusinessNames.VerticalAlignment = VerticalAlignment.Center;
                tbBusinessNames.FlowDirection = System.Windows.FlowDirection.RightToLeft;
                tbBusinessNames.MouseDoubleClick += TextBox_MouseDoubleClick;
                Grid.SetRow(tbBusinessNames, i+2+z);
                Grid.SetColumn(tbBusinessNames, 1);
                this.CalculationsSumAndText.Children.Add(tbBusinessNames);
                z++;
                if (j<categoriesAndBusiness.Count-1)
                {
                    j++;
                }
            }
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = GridLength.Auto;
            CalculationsSumAndText.RowDefinitions.Add(gridRow2);

            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = GridLength.Auto;
            CalculationsSumAndText.RowDefinitions.Add(gridRow3);
        }

        public UIElementCollection GetAllControls()
        {
            return this.CalculationsSumAndText.Children;
        }
    }
}
