using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CalculationExpenses2.ConfigurationXml
{
    public class WriteConfiguration
    {
        private static WriteConfiguration _instance;
        private static string expensesConfiguration = "..\\..\\ExpensesConfiguration.xml";//Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExpensesConfiguration.xml");

        public static WriteConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WriteConfiguration();
                }
                return _instance;
            }
        }

        public void SetBusinessToExistingCategory(string sectionName, string businessNames)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(expensesConfiguration);

            XmlElement root = doc.DocumentElement;
            XmlNode CategoriesAndBusiness = root.SelectSingleNode("CategoriesAndBusiness");
            XmlNodeList CategoriesAndBusinessNodes = CategoriesAndBusiness.ChildNodes;

            foreach (XmlNode node in CategoriesAndBusinessNodes)
            {
                if (node["HebrewName"].InnerText.Contains(sectionName))
                {
                    node["BusinessNames"].InnerText = businessNames;
                    break;
                }
            }

            doc.Save(expensesConfiguration);
            //doc.Save("..\\..\\ExpensesConfiguration.xml");
        }

        public void SetNewCategory(string categoryName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(expensesConfiguration);

            XmlElement root = doc.DocumentElement;
            XmlNode CategoriesAndBusiness = root.SelectSingleNode("CategoriesAndBusiness");



            //create node and add value
            XmlNode node = doc.CreateNode(XmlNodeType.Element, "CategoryBusiness", null);

            //create title node
            XmlNode nodeHebrewName = doc.CreateElement("HebrewName");
            //add value for it
            nodeHebrewName.InnerText = categoryName;

            //create title node
            XmlNode nodeEnglishName = doc.CreateElement("EnglishName");
            //add value for it
            //nodeEnglishName.InnerText = "";

            //create title node
            XmlNode nodeBussinesNames = doc.CreateElement("BusinessNames");
            //add value for it
            //nodeBussinesNames.InnerText = "";

            //add to parent node
            node.AppendChild(nodeHebrewName);
            node.AppendChild(nodeEnglishName);
            node.AppendChild(nodeBussinesNames);

            //add to elements collection
            CategoriesAndBusiness.AppendChild(node);

            //save back
            doc.Save(expensesConfiguration);
        }
    }
}
