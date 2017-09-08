using CalculationExpenses2.ConfigurationXml;
using CalculationExpenses2.RelayCommands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculationExpenses2.MVVM.Model
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
            }
        }

        public string BusineesName
        {
            get { return _busineesName; }
            set { _busineesName = value; }
        }

        public string TransactionSum
        {
            get { return _transactionSum; }
            set { _transactionSum = value; }
        }

        public string BillingAmount
        {
            get { return _billingAmount; }
            set { _billingAmount = value; }
        }

        public string Remarks
        {
            get { return _remarks; }
            set
            {
                _remarks = value;
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

        private string _selectedComboBox;
        public string SelectedComboBox
        {
            get { return _selectedComboBox; }
            set
            {
                _selectedComboBox = value;
            }
        }

        private ObservableCollection<string> _comboBoxItems;
        public ObservableCollection<string> ComboBoxItems
        {
            get { return _comboBoxItems; }
            set
            {
                _comboBoxItems = value;
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
            AddCategory = new GenericRelayCommand(new Action<object>(AddToCategory));
            IntializeComboBoxItems();
        }

        private string GetCategory()
        {
            Dictionary<string, string> sectionsAndBusiness = ReadConfiguration.Instance.GetCategoriesAndBusiness();

            foreach (var sectionAndBusiness in sectionsAndBusiness)
            {
                if (sectionAndBusiness.Value.Contains(_busineesName))
                {
                    return sectionAndBusiness.Key;
                }
            }

            return "";
        }

        private void AddToCategory(object obj)
        {
            List<object> objList = obj as List<object>;
            System.Windows.Controls.ItemCollection ic = objList[2] as System.Windows.Controls.ItemCollection;
            List<string> categoryAndBussines = (obj as List<object>).ConvertAll(x => Convert.ToString(x));
            Dictionary<string, string> sectionsAndBusiness = ReadConfiguration.Instance.GetCategoriesAndBusiness();
            foreach (var sectionAndBusiness in sectionsAndBusiness)
            {
                string sectionName = sectionAndBusiness.Key;
                if (sectionName == categoryAndBussines[1])
                {
                    string business = sectionAndBusiness.Value;
                    business += categoryAndBussines[0] + ",";

                    WriteConfiguration.Instance.SetBusinessToExistingCategory(sectionName, business);

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

        private void IntializeComboBoxItems()
        {
            if (_comboBoxItems == null)
            {
                _comboBoxItems = new ObservableCollection<string>();
            }

            List<string> categories = ReadConfiguration.Instance.GetCategories();
            foreach (string category in categories)
            {
                _comboBoxItems.Add(category);
            }
        }
    }
}
