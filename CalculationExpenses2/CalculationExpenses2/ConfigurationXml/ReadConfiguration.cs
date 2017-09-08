using CalculationExpenses2.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CalculationExpenses2.ConfigurationXml
{
    public class ReadConfiguration
    {
        private static ReadConfiguration _instance;
        private static string expensesConfiguration = "..\\..\\ExpensesConfiguration.xml";//Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExpensesConfiguration.xml");

        public static ReadConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ReadConfiguration();
                }
                return _instance;
            }
        }

        public Dictionary<string,string> GetCategoriesAndBusiness()
        {
            Dictionary<string, string> nameAndBusiness = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(expensesConfiguration);

            XmlElement root = doc.DocumentElement;// ChildNodes;
            XmlNode CategoriesAndBusiness = root.SelectSingleNode("CategoriesAndBusiness");
            XmlNodeList CategoriesAndBusinessNodes = CategoriesAndBusiness.ChildNodes;

            foreach (XmlNode node in CategoriesAndBusinessNodes)
            {
                nameAndBusiness.Add(node["HebrewName"].InnerText, node["BusinessNames"].InnerText);
            }

            return nameAndBusiness;
        }

        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            XmlDocument doc = new XmlDocument();
            doc.Load(expensesConfiguration);

            XmlElement root = doc.DocumentElement;
            XmlNode Categories = root.SelectSingleNode("CategoriesAndBusiness");
            XmlNodeList CategoriesNodes = Categories.ChildNodes;

            foreach (XmlElement category in CategoriesNodes)
            {
                categories.Add(category["HebrewName"].InnerText);
            }

            return categories;
        }
    }
}
