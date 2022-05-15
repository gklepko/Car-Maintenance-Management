using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement.Repos
{
    class Helper
    {
        private static string applicationFolderName = "Car Management";
        public static string GetWorkingDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), applicationFolderName);
        }
    }
}
