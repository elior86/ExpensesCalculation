using CalculationExpenses2.ConfigurationXml;
using CalculationExpenses2.MVVM.Interfaces;
using CalculationExpenses2.MVVM.Model;
using CalculationExpenses2.RelayCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalculationExpenses2.MVVM.ViewModel
{
    public class ItemControlViewModel : INotifyPropertyChanged
    {
        const string EXPENSES_FILE = "C:\\Projects\\expenses.txt";

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                try
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion INotifyPropertyChanged

        public IMainWindow mainWindow { get; set; }

        private ObservableCollection<ItemControlModel> _items;
        public ObservableCollection<ItemControlModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        private ICommand _calculateSections;
        public ICommand CalculateSections
        {
            get
            {
                return _calculateSections;
            }
            set
            {
                _calculateSections = value;
            }
        }

        private ICommand _closeWindow;
        public ICommand CloseWindow
        {
            get
            {
                return _closeWindow;
            }
            set
            {
                _closeWindow = value;
            }
        }

        private ICommand _addingNewCategory;
        public ICommand AddingNewCategory
        {
            get
            {
                return _addingNewCategory;
            }
            set
            {
                _addingNewCategory = value;
            }
        }

        public ItemControlViewModel()
        {
            InitializeItems();

            CalculateSections = new GenericRelayCommand(new Action<object>(CalculateAllSections));
            CloseWindow = new GenericRelayCommand(new Action<object>(CloseMainWindow));
            AddingNewCategory = new GenericRelayCommand(new Action<object>(AddingNewCategoryCommand));
        }

        private void InitializeItems()
        {
            if (_items == null)
            {
                _items = new ObservableCollection<ItemControlModel>();
            }
            _items.Clear();
            string[] lines = File.ReadAllLines(EXPENSES_FILE);//change the expenses file
            if (lines != null)
            {
                foreach (string line in lines)
                {
                    string[] subLines = line.Split('\t');
                    if (subLines.Length == 5)
                    {
                        _items.Add(new ItemControlModel(subLines[0], subLines[1], subLines[2], subLines[3], subLines[4]));
                    }
                    else if (subLines.Length == 9)
                    {
                        _items.Add(new ItemControlModel(subLines[1], subLines[3], subLines[5], subLines[6], subLines[7]));
                    }
                }
            }
        }

        private void CalculateAllSections(object obj)
        {
            UIElementCollection tbsOfSumsAndDetails = mainWindow.GetAllControls();
            List<TextBox> tbSumsAndDetails = RemoveTextBlocks(tbsOfSumsAndDetails);
            ResetSumsAndTexts(tbSumsAndDetails);
            foreach (ItemControlModel item in Items)
            {
                if (!string.IsNullOrEmpty(item.Category))
                {
                    foreach (TextBox tbSumAndDetail in tbSumsAndDetails)
                    {
                        if (tbSumAndDetail.Name == item.Category + "סכום")
                        {
                            tbSumAndDetail.Text = (Convert.ToDouble(tbSumAndDetail.Text) + Convert.ToDouble(item.BillingAmount)).ToString();
                        }
                        if (tbSumAndDetail.Name == item.Category + "פירוט")
                        {
                            tbSumAndDetail.Text = tbSumAndDetail.Text + (tbSumAndDetail.Text == "" ? tbSumAndDetail.Text : ", ") + item.BusineesName + " - " + item.BillingAmount;
                        }
                    }
                }
            }
        }

        private List<TextBox> RemoveTextBlocks(UIElementCollection tbsOfSumsAndDetails)
        {
            List<TextBox> tbSumsAndDetails = new List<TextBox>();
            foreach (UIElement uiElement in tbsOfSumsAndDetails)
            {
                if(uiElement.GetType() == typeof(TextBox))
                {
                    tbSumsAndDetails.Add(uiElement as TextBox);
                }
            }
            return tbSumsAndDetails;
        }

        private void ResetSumsAndTexts(List<TextBox> tbSumsAndDetails)
        {
            foreach (TextBox tbSumAndDetail in tbSumsAndDetails)
            {
                if (tbSumAndDetail.Name.Contains("סכום"))
                {
                    tbSumAndDetail.Text = "0";
                }
                if (tbSumAndDetail.Name.Contains("פירוט"))
                {
                    tbSumAndDetail.Text = "";
                }

            }
        }

        private void CloseMainWindow(object obj)
        {
            Application.Current.Shutdown();
        }

        private void AddingNewCategoryCommand(object obj)
        {
            string newCategoryName = obj as string;
            if (string.IsNullOrEmpty(newCategoryName) != true)
            {
                WriteConfiguration.Instance.SetNewCategory(newCategoryName);
            }

            InitializeItems();
            mainWindow.GenerateControls();
            //UIElementCollection tbsOfSumsAndDetails = mainWindow.GetAllControls();
            //List<TextBox> tbSumsAndDetails = RemoveTextBlocks(tbsOfSumsAndDetails);
            //ResetSumsAndTexts(tbSumsAndDetails);
        }
    }
}
