using MasterInstallerCopy.MVVM.Model;
using MasterInstallerCopy.RelayCommands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace MasterInstallerCopy.MVVM.ViewModel
{
    class CopyProgramViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _sourcePathText;
        public string SourcePathText
        {
            get
            {
                return _sourcePathText;
            }
            set
            {
                _sourcePathText = value;
                OnPropertyChanged("SourcePathText");
            }
        }

        private string _destinationPathText;
        public string DestinationPathText
        {
            get
            {
                return _destinationPathText;
            }
            set
            {
                _destinationPathText = value;
                OnPropertyChanged("DestinationPathText");
            }
        }

        private string _MCCLatestVersionText;
        public string MCCLatestVersionText
        {
            get
            {
                return _MCCLatestVersionText;
            }
            set
            {
                _MCCLatestVersionText = value;
                OnPropertyChanged("MCCLatestVersionText");
            }
        }

        private string _MCCCurrentVersionText;
        public string MCCCurrentVersionText
        {
            get
            {
                return _MCCCurrentVersionText;
            }
            set
            {
                _MCCCurrentVersionText = value;
                OnPropertyChanged("MCCCurrentVersionText");
            }
        }

        private string _popUpStringText;
        public string PopUpStringText
        {
            get
            {
                return _popUpStringText;
            }
            set
            {
                _popUpStringText = value;
                OnPropertyChanged("PopUpStringText");
            }
        }

        private double _proBarValue;
        public double ProBarValue
        {
            get
            {
                return _proBarValue;
            }
            set
            {
                _proBarValue = value;
                OnPropertyChanged("ProBarValue");
            }
        }

        private string _proBarTag;
        public string ProBarTag
        {
            get
            {
                return _proBarTag;
            }
            set
            {
                _proBarTag = value;
                OnPropertyChanged("ProBarTag");
            }
        }

        private ICommand m_ButtonCommand;
        public ICommand ButtonCommand
        {
            get
            {
                return m_ButtonCommand;
            }
            set
            {
                m_ButtonCommand = value;
            }
        }

        private ICommand m_checkBoxCommand;
        public ICommand CheckBoxCommand
        {
            get
            {
                return m_checkBoxCommand;
            }
            set
            {
                m_checkBoxCommand = value;
            }
        }


        public CopyProgramViewModel()
        {
            ButtonCommand = new CopyBtnRelayCommand(new Action<object>(ButtonCommandClick));
            CheckBoxCommand = new CheckBoxRelayCommand(new Action<object>(CheckBoxClick));
        }

        public void ButtonCommandClick(object obj)
        {
            Task.Factory.StartNew(() =>
            {
                ShowMessage(obj);
            });
        }

        private void CheckBoxClick(object obj)
        {
            //throw new NotImplementedException();
            Task.Factory.StartNew(() =>
            {
                ShowVersions(obj);
            });
        }

        private void ShowVersions(object obj)
        {
            if (string.IsNullOrEmpty(SourcePathText) || string.IsNullOrEmpty(DestinationPathText))
            {
                //todo -> source is empty
                return;
            }
            CheckVersions(SourcePathText, Convert.ToString(obj), true);
            CheckVersions(DestinationPathText, Convert.ToString(obj), false);
        }

        private void CheckVersions(string path, string compoundName, bool sourceOrNot)
        {
            string compundPath = path + "\\" + compoundName + (sourceOrNot == true ? "\\Archive\\" : "");
            DirectoryInfo compoundDI = new DirectoryInfo(compundPath);
            FileInfo[] compoundVersion;
            if (sourceOrNot == true)
            {
                compoundVersion = compoundDI.GetDirectories().Last().GetFiles("*.xml");
            }
            else
            {
                compoundVersion = compoundDI.GetFiles("*.xml");
            }
            //TODO: check that the array have something in it

            XmlDocument doc = new XmlDocument();
            doc.Load(compoundVersion[0].FullName);

            if (sourceOrNot == true)
            {
                MCCLatestVersionText = doc.DocumentElement["Version"].InnerText;
            }
            else
            {
                MCCCurrentVersionText = doc.DocumentElement["Version"].InnerText;
            }
        }

        private void ShowMessage(object obj)
        {
            List<string> pathsAndCompounds = (obj as List<object>).ConvertAll(x => Convert.ToString(x));

            string sourcePath = pathsAndCompounds[0];
            string destinationPath = pathsAndCompounds[1];

            if (checkFolderOrFileExistence(sourcePath) == false)
            {
                PrintToLogger("Source path does not exist");
                UpdateProgressBar(100);
                return;
            }

            if (checkFolderOrFileExistence(destinationPath) == false)
            {
                PrintToLogger("Destination path does not exist");
                UpdateProgressBar(100);
                return;
            }

            //Start Copy
            for (int i = 2; i < pathsAndCompounds.Count; i++)
            {
                if (CopyCompound(sourcePath, destinationPath, pathsAndCompounds[i]) == false)
                {
                    PrintToLogger("ERROR");
                    UpdateProgressBar(100);
                    break;
                }
            }
        }

        private bool CopyCompound(string sourcePath, string destinationPath, string compoundName)
        {
            if (checkFolderOrFileExistence(sourcePath + "\\" + compoundName) == false)
            {
                PrintToLogger(compoundName + " compund source path does not exist");
                UpdateProgressBar(100);
                return false;
            }

            if (checkFolderOrFileExistence(destinationPath + "\\" + compoundName) == false)
            {
                PrintToLogger(compoundName + " compund destination path does not exist");
                UpdateProgressBar(100);
                return false;
            }

            // retrieving the directory information
            DirectoryInfo myDirectoryInfo = new DirectoryInfo(destinationPath + "\\" + compoundName);

            FileInfo[] mccVersionFile = myDirectoryInfo.GetFiles("*.xml");
            FileInfo[] allFilesInDirectory = myDirectoryInfo.GetFiles();
            //TODO: check that the array have something in it

            XmlDocument doc = new XmlDocument();
            doc.Load(mccVersionFile[0].FullName);

            string version = doc.DocumentElement["Version"].InnerText;

            if (checkFolderOrFileExistence(destinationPath + "\\" + compoundName + "\\Archive") == false)
            {
                Directory.CreateDirectory(destinationPath + "\\" + compoundName + "\\Archive");
                PrintToLogger("Compund archive path does not exist, creating it right now");
                //UpdateProgressBar(100);
            }
            else
            {
                PrintToLogger("Compund archive path exist");
                //UpdateProgressBar(100);
            }

            if (Directory.Exists(destinationPath + "\\" + compoundName + "\\Archive\\" + version) == true)
            {
                //TODO: logs with delete evrything inside it
            }
            else
            {
                Directory.CreateDirectory(destinationPath + "\\" + compoundName + "\\Archive\\" + version);
            }

            foreach (FileInfo file in allFilesInDirectory)
            {
                File.Copy(destinationPath + "\\" + compoundName + "\\" + file.Name, destinationPath + "\\" + compoundName + "\\Archive\\" + version + "\\" + file.Name, true);
            }

            //Maybe can forget from delete and copy paste on it
            foreach (FileInfo file in allFilesInDirectory)
            {
                File.Delete(file.FullName);
            }

            DirectoryInfo myDirectoryInfoSource = new DirectoryInfo(sourcePath);
            FileInfo[] allFilesInDirectorySource = myDirectoryInfoSource.GetFiles();
            foreach (FileInfo file in allFilesInDirectorySource)
            {
                File.Copy(file.FullName, destinationPath + "\\" + compoundName + "\\" + file.Name, true);
            }

            return true;
        }

        private bool checkFolderOrFileExistence(string folderOrFilePath)
        {
            return FilesAndFolders.FolderExist(folderOrFilePath);
        }

        private void PrintToLogger(string message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                PopUpStringText += message + Environment.NewLine;
            });
        }

        private void UpdateProgressBar(double value)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                ProBarValue = value;
                ProBarTag = string.Format("{0}%", value.ToString());
            });
        }
    }
}
