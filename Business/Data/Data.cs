using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Data
{
    public static class Data 
    {
        public static string applicationUrl = "https://www.epam.com";
        public static string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"); //TC3
        public static string fileName = "EPAM_Corporate_Overview_Q4FY-2024.pdf"; // used for TC3
        public static int Timeout = 30;
        public static int numberOfClicks = 2; // used for TC4
    }
}
 