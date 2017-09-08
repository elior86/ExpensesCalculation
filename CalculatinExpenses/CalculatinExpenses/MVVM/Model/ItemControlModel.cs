using CalculatinExpenses.RelayCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatinExpenses.MVVM.Model
{
    public class ItemControlModel : INotifyPropertyChanged
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

        Dictionary<string, string> categories = new Dictionary<string, string>()
        {
            { "behatsdaaPath", "בהצדעה" },
            { "clothsPath", "הלבשה" },
            { "foodPath", "אוכל" },
            { "fuelPath", "דלק" },
            { "othersPath", "אחר" },
            { "resturantsPath", "בחוץ" }
        };

        const string ALL_CATEGORIES = "C:\\Projects\\CategoryDictionaries";
        private string _date;
        private string _busineesName;
        private string _transactionSum;
        private string _billingAmount;
        private string _remarks;
        private string _category;
        private ICommand _addCategoryBtnCommand;

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public string BusineesName
        {
            get { return _busineesName; }
            set { _busineesName = value; OnPropertyChanged("BusineesName"); }
        }

        public string TransactionSum
        {
            get { return _transactionSum; }
            set { _transactionSum = value; OnPropertyChanged("TransactionSum"); }
        }

        public string BillingAmount
        {
            get { return _billingAmount; }
            set { _billingAmount = value; OnPropertyChanged("BillingAmount"); }
        }

        public string Remarks
        {
            get { return _remarks; }
            set
            {
                _remarks = value;
                OnPropertyChanged("Remarks");
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }

        public ICommand AddCategory
        {
            get
            {
                return _addCategoryBtnCommand;
            }
            set
            {
                _addCategoryBtnCommand = value;
            }
        }

        public ItemControlModel(string date, string business, string transaction, string billing, string remarks)
        {
            _date = date;
            _busineesName = business;
            _transactionSum = transaction;
            _billingAmount = billing;
            _remarks = remarks;
            _category = GetCategory();
            AddCategory = new AddCategoryRelayCommand(new Action<object>(AddToCategory));
        }

        private string GetCategory()
        {
            //D:\Visual Studio Projects\CategoryDictionaries
            string[] allFiles = Directory.GetFiles(ALL_CATEGORIES);
            foreach (string file in allFiles)
            {
                string categoryName = Path.GetFileNameWithoutExtension(file);
                string text = File.ReadAllText(file, Encoding.GetEncoding(1255));

                if (text.Contains(_busineesName))
                {
                    return categories[categoryName];
                }
            }
            return "";
        }

        public void AddToCategory(object obj)
        {
            List<object> objList = obj as List<object>;
            System.Windows.Controls.ItemCollection ic = objList[2] as System.Windows.Controls.ItemCollection;
            List<string> categoryAndBussines = (obj as List<object>).ConvertAll(x => Convert.ToString(x));
            string[] allFiles = Directory.GetFiles(ALL_CATEGORIES);
            foreach (string file in allFiles)
            {
                string categoryName = Path.GetFileNameWithoutExtension(file);
                if (categories[categoryName] == categoryAndBussines[1])
                {
                    string text = File.ReadAllText(file, Encoding.GetEncoding(1255));
                    text += categoryAndBussines[0] + ",";
                    File.WriteAllText(file, text, Encoding.UTF8);
                    Category = categoryAndBussines[1];
                    foreach (ItemControlModel item in ic)
                    {
                        if (item.BusineesName == categoryAndBussines[0])
                        {
                            if (item.Category == "")
                            {
                                item.Category = categoryAndBussines[1];
                            }
                        }
                    }
                }
            }
        }
    }
}
