using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterInstallerCopy.MVVM.Model
{
    public class FilesAndFolders
    {
        public static bool FolderExist(string folder)
        {
            return Directory.Exists(folder);
        }
    }
}
