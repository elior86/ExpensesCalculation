using CalculatinExpenses.MVVM.Model;
using CalculatinExpenses.RelayCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace CalculatinExpenses.MVVM.ViewModel
{
    public class ItemControlViewModel : INotifyPropertyChanged
    {
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

        const string EXPENSES_FILE = "C:\\Projects\\expenses.txt";

        Dictionary<string, Action<ItemControlModel>> categories;

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

        private string _foodSum = "0";
        public string FoodSum
        {
            get { return _foodSum; }
            set
            {
                _foodSum = value;
                OnPropertyChanged("FoodSum");
            }
        }

        private string _fuelSum = "0";
        public string FuelSum
        {
            get { return _fuelSum; }
            set { _fuelSum = value; OnPropertyChanged("FuelSum"); }
        }

        private string _resturantsSum = "0";
        public string ResturantsSum
        {
            get { return _resturantsSum; }
            set { _resturantsSum = value; OnPropertyChanged("ResturantsSum"); }
        }

        private string _clothsSum = "0";
        public string ClothsSum
        {
            get { return _clothsSum; }
            set { _clothsSum = value; OnPropertyChanged("ClothsSum"); }
        }

        private string _behatsdaaSum = "0";
        public string BehatsdaaSum
        {
            get { return _behatsdaaSum; }
            set { _behatsdaaSum = value; OnPropertyChanged("BehatsdaaSum"); }
        }

        private string _othersSum = "0";
        public string OthersSum
        {
            get { return _othersSum; }
            set { _othersSum = value; OnPropertyChanged("OthersSum"); }
        }

        private string _foodSumText = "";
        public string FoodSumText
        {
            get { return _foodSumText; }
            set
            {
                _foodSumText = value;
                OnPropertyChanged("FoodSumText");
            }
        }

        private string _fuelSumText = "";
        public string FuelSumText
        {
            get { return _fuelSumText; }
            set { _fuelSumText = value; OnPropertyChanged("FuelSumText"); }
        }

        private string _resturantsSumText = "";
        public string ResturantsSumText
        {
            get { return _resturantsSumText; }
            set { _resturantsSumText = value; OnPropertyChanged("ResturantsSumText"); }
        }

        private string _clothsSumText = "";
        public string ClothsSumText
        {
            get { return _clothsSumText; }
            set { _clothsSumText = value; OnPropertyChanged("ClothsSumText"); }
        }

        private string _behatsdaaSumText = "";
        public string BehatsdaaSumText
        {
            get { return _behatsdaaSumText; }
            set { _behatsdaaSumText = value; OnPropertyChanged("BehatsdaaSumText"); }
        }

        private string _othersSumText = "";
        public string OthersSumText
        {
            get { return _othersSumText; }
            set { _othersSumText = value; OnPropertyChanged("OthersSumText"); }
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

        private ICommand _openExpensesFile;
        public ICommand OpenExpensesFile
        {
            get
            {
                return _openExpensesFile;
            }
            set
            {
                _openExpensesFile = value;
            }
        }

        private ICommand _restartAll;
        public ICommand RestartAll
        {
            get
            {
                return _restartAll;
            }
            set
            {
                _restartAll = value;
            }
        }

        public ItemControlViewModel()
        {
            InitializeItems();

            XmlDocument doc = new XmlDocument();
            doc.Load("ConfigurationXml\\ExpensesConfiguration.xml");

            //Display all the book titles.
            XmlNodeList elemList = doc.GetElementsByTagName("category");

            CalculateSections = new CalculateSectionsRelayCommand(new Action<object>(CalculateAllSections));
            OpenExpensesFile = new OpenFileRelayCommand(new Action<object>(OpenExpensesFileMethod));
            RestartAll = new OpenFileRelayCommand(new Action<object>(RestartAllMethod));

            categories = new Dictionary<string, Action<ItemControlModel>>()
            {
                { "בהצדעה", CalculateBehatsdaaSum },
                { "הלבשה", CalculateClothsSum },
                { "אוכל", CalculateFoodSum },
                { "דלק", CalculateFuelSum },
                { "אחר", CalculateOthersSum },
                { "בחוץ", CalculateResturantsSum }
            };

        }

        void InitializeItems()
        {
            if (_items == null)
            {
                _items = new ObservableCollection<ItemControlModel>();
            }
            _items.Clear();
            string[] lines = File.ReadAllLines(EXPENSES_FILE);
            if (lines != null)
            {
                foreach (string line in lines)
                {
                    string[] subLines = line.Split('\t');
                    if (subLines.Length == 5)
                    {
                        _items.Add(new ItemControlModel(subLines[0], subLines[1], subLines[2], subLines[3], subLines[4]));
                    }
                }
            }
        }

        private void CalculateAllSections(object obj)
        {
            ResetSumsAndTexts();
            foreach (ItemControlModel item in Items)
            {
                if (!string.IsNullOrEmpty(item.Category))
                {
                    categories[item.Category](item);// = (Convert.ToDouble(categories[item.Category]) + Convert.ToDouble(item.BillingAmount)).ToString();
                }
            }

        }

        private void OpenExpensesFileMethod(object obj)
        {
            Process myProcess = new Process();
            Process.Start("notepad++.exe", "\"C:\\Projects\\expenses.txt\"");
        }

        private void RestartAllMethod(object obj)
        {
            InitializeItems();
            ResetSumsAndTexts();
        }

        private void ResetSumsAndTexts()
        {
            BehatsdaaSum = "0";
            BehatsdaaSumText = "";
            ClothsSum = "0";
            ClothsSumText = "";
            FoodSum = "0";
            FoodSumText = "";
            FuelSum = "0";
            FuelSumText = "";
            OthersSum = "0";
            OthersSumText = "";
            ResturantsSum = "0";
            ResturantsSumText = "";
        }

        private void CalculateBehatsdaaSum(ItemControlModel item)
        {
            BehatsdaaSum = (Convert.ToDouble(BehatsdaaSum) + Convert.ToDouble(item.BillingAmount)).ToString();
            BehatsdaaSumText = !string.IsNullOrEmpty(BehatsdaaSumText) ? (BehatsdaaSumText.Contains(item.BusineesName) ? "" : BehatsdaaSumText + ", " + item.BusineesName) : item.BusineesName;
        }

        private void CalculateClothsSum(ItemControlModel item)
        {
            ClothsSum = (Convert.ToDouble(ClothsSum) + Convert.ToDouble(item.BillingAmount)).ToString();
            ClothsSumText = !string.IsNullOrEmpty(ClothsSumText) ? (ClothsSumText.Contains(item.BusineesName) ? ClothsSumText : ClothsSumText + ", " + item.BusineesName) : item.BusineesName;
        }

        private void CalculateFoodSum(ItemControlModel item)
        {
            FoodSum = (Convert.ToDouble(FoodSum) + Convert.ToDouble(item.BillingAmount)).ToString();
            FoodSumText = !string.IsNullOrEmpty(FoodSumText) ? (FoodSumText.Contains(item.BusineesName) ? FoodSumText : FoodSumText + ", " + item.BusineesName) : item.BusineesName;
        }

        private void CalculateFuelSum(ItemControlModel item)
        {
            FuelSum = (Convert.ToDouble(FuelSum) + Convert.ToDouble(item.BillingAmount)).ToString();
            FuelSumText = !string.IsNullOrEmpty(FuelSumText) ? (FuelSumText.Contains(item.BusineesName) ? FuelSumText : FuelSumText + ", " + item.BusineesName) : item.BusineesName;
        }

        private void CalculateOthersSum(ItemControlModel item)
        {
            OthersSum = (Convert.ToDouble(OthersSum) + Convert.ToDouble(item.BillingAmount)).ToString();
            OthersSumText = string.IsNullOrEmpty(OthersSumText) ? item.BillingAmount.ToString() + " - " + item.BusineesName : OthersSumText + ", " + item.BillingAmount.ToString() + " - " + item.BusineesName;
        }

        private void CalculateResturantsSum(ItemControlModel item)
        {
            ResturantsSum = (Convert.ToDouble(ResturantsSum) + Convert.ToDouble(item.BillingAmount)).ToString();
            ResturantsSumText = !string.IsNullOrEmpty(ResturantsSumText) ? (ResturantsSumText.Contains(item.BusineesName) ? ResturantsSumText : ResturantsSumText + ", " + item.BusineesName) : item.BusineesName;
        }

        private void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);

            if (tb != null)
            {
                tb.SelectAll();
            }
        }
    }
}
