using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace CirclePlayer_v3
{
    /// <summary>
    /// HomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            DisableButton(true, search_button, TimeSpan.FromSeconds(0.3));

        }
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        public void CloseWindow(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
        public void MinisizeWindow(object sender, RoutedEventArgs arg2) {
            this.WindowState = WindowState.Minimized;
        }

        bool isFirstChange = true;
        private void SearchButton_Control(object sender, TextChangedEventArgs e)
        {
            if (!search_box.Text.Equals(""))
            {
                if (isFirstChange)
                {
                    DisableButton(false, search_button, TimeSpan.FromSeconds(0.5));
                    isFirstChange = false;
                }
            }
            else
            {
                DisableButton(true, search_button, TimeSpan.FromSeconds(0.5));
                isFirstChange = true;
            }
        }

        private void DisableButton(bool action, Button button, TimeSpan duration)
        {
            DoubleAnimation fadeAnimation = action ? new DoubleAnimation() { From = 1, To = 0, Duration = duration } : new DoubleAnimation() { From = 0, To = 1, Duration = duration };
            DoubleAnimation fadeAnimation1 = action ? new DoubleAnimation() { From = 0, To = 1, Duration = duration } : new DoubleAnimation() { From = 1, To = 0, Duration = duration };
            try
            {
                button.BeginAnimation(OpacityProperty, fadeAnimation);
                search_tip.BeginAnimation(OpacityProperty, fadeAnimation1);
                button.IsEnabled = !action;
                //search_tip.Visibility = action ? Visibility.Visible : Visibility.Hidden;
            }
            catch { }
        }

        private void Search_button_Click(object sender, RoutedEventArgs e)
        {
            String inputText = search_box.Text;
            ListmusicWindow listmusicWindow = new ListmusicWindow("listmusic", inputText, "name", "netease", "1");
            listmusicWindow.Show();
        }
    }
}
