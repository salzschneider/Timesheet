using System;
using System.Text;
using System.Configuration;

namespace Timesheet.UI.Utilities
{    
    /// <summary>
    /// Getting basic application information from App.Config
    /// </summary>
    public class ApplicationConfig
    {
        public static string ApplicationVersion { get; private set; }

        public static string ApplicationName { get; private set; }

        static ApplicationConfig()
        {
            ApplicationVersion = Timesheet.UI.Properties.Settings.Default.ApplicationVersion.ToString();
            ApplicationName = Timesheet.UI.Properties.Settings.Default.ApplicationName.ToString();
        }
    }
}
