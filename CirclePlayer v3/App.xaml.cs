using System;
using System.Windows;
using CircleFamily;

namespace CirclePlayer_v3
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void CirclePlayer_Startup(object sender, StartupEventArgs e)
        {
            CirclePlayerUtils Utils = new CirclePlayerUtils();
            String NaiveArgs = null;
            for (int i = 0; i < e.Args.Length; i++) {
                NaiveArgs += e.Args[i];
            }
            String[] args = (NaiveArgs == null) ? null : Utils.PlayerArgBuilder(NaiveArgs);
            if (args != null)
            {
                switch (args[0])
                {
                    case "listmusic":
                        ListmusicWindow listmusicWindow = new ListmusicWindow(args[0], args[1], args[2], args[3], args[4], true);
                        Application.Current.MainWindow = listmusicWindow;
                        listmusicWindow.Show();
                        break;
                }
                return;
            }
            HomeWindow homeWindow = new HomeWindow();
            Application.Current.MainWindow = homeWindow;
            homeWindow.Show();
        }
    }
}
