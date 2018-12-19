using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CirclePlayer_v3
{
    /// <summary>
    /// ListmusicWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ListmusicWindow : Window
    {

        public string Action { set; get; }
        public string Input { set; get; }
        public string Filter { set; get; }
        public string Platform { set; get; }
        public string Page { set; get; }

        private static bool isReady = false;

        public void setInfor(string Action, string Input, string Filter, string Platform, string Page) {
            if (!(Action is null || Input is null || Filter is null || Platform is null || Page is null))
            {
                this.Action = Action;
                this.Input = Input;
                this.Filter = Filter;
                this.Platform = Platform;
                this.Page = Page;
                isReady = true;
            }
        }
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        public void CloseWindow(object sender, RoutedEventArgs args)
        {
            this.Close();
        }

        public ListmusicWindow(string Action, string Input, string Filter, string Platform, string Page)
        {
            if (!(Action is null || Input is null || Filter is null || Platform is null || Page is null))
            {
                if (!(Action == "" || Input == "" || Filter == "" || Platform == "" || Page == "")) {
                    this.Action = Action;
                    this.Input = Input;
                    this.Filter = Filter;
                    this.Platform = Platform;
                    this.Page = Page;
                    isReady = true;
                }
            }
            InitializeComponent();
            if (isReady)
            {
                inputText.Content = Input;
            }
            else {
                MessageBox.Show("参数错误");
            }
        }
    }
}
