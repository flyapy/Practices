using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Practices.COM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // set the Red accent and dark theme only to the current window
            ThemeManager.ChangeAppStyle(this,
                ThemeManager.GetAccent("Orange"),
                ThemeManager.GetAppTheme("BaseLight"));
            base.OnStartup(e);
        }
    }
}
