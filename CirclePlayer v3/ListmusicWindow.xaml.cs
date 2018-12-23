using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

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

        private static bool isStartWithArg = false;
        private static bool isReady = false;
        
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        public void CloseWindow(object sender, RoutedEventArgs args)
        {
            if (isStartWithArg)
            {
                isStartWithArg = false; 
                HomeWindow homeWindow = new HomeWindow();
                Application.Current.MainWindow = homeWindow;
                homeWindow.Show();
            }
            this.Close();
        }

        public ListmusicWindow(string Action, string Input, string Filter, string Platform, string Page, bool isStartWithArgs = false)
        {
            if (!(Action is null || Input is null || Filter is null || Platform is null || Page is null))
            {
                if (!(Action == "" || Input == "" || Filter == "" || Platform == "" || Page == ""))
                {
                    this.Action = Action;
                    this.Input = Input;
                    this.Filter = Filter;
                    this.Platform = Platform;
                    this.Page = Page;
                    isReady = true;
                }
                if (isStartWithArgs)
                    isStartWithArg = true;
            }
            InitializeComponent();
            if (isReady)
            {
                inputText.Content = Input;

                string url = "https://music.boxmoe.cn/";
                Encoding encoding = Encoding.GetEncoding("utf-8");
                IDictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "input", Input },
                    { "filter", Filter },
                    { "type", Platform },
                    { "page", Page }
                };
                RootObject backData = PostHttpRequest(url, parameters, encoding);
                for (int i = 0; i < backData.data.Count; i ++) {
                    data_list.Items.Add(new
                    {
                        music_name = backData.data[i].title,
                        music_author = backData.data[i].author,
                        music_id = backData.data[i].songid,
                        owner_link = backData.data[i].link,
                        music_link = backData.data[i].url
                    });
                }
            }
            else
            {
                
                MessageBox.Show("参数错误");
            }
        }

        public static RootObject PostHttpRequest(string url, IDictionary<string, string> parameters, Encoding charset)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            StringBuilder buf = new StringBuilder();
            int i = 0;
            foreach (string key in parameters.Keys)
            {
                if (i > 0)
                {
                    buf.AppendFormat("&{0}={1}", key, parameters[key]);
                }
                else
                {
                    buf.AppendFormat("{0}={1}", key, parameters[key]);
                }
                i++;
            }
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0");
            request.AddParameter("undefined", buf.ToString(), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            RootObject rb = JsonConvert.DeserializeObject<RootObject>(response.Content);
            //MessageBox.Show(rb.data[0].author);
            return rb;
        }


    }
    public class Data
    {
        public string type { get; set; }
        public string link { get; set; }
        public string songid { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string lrc { get; set; }
        public string url { get; set; }
        public string pic { get; set; }
    }

    public class RootObject
    {
        public List<Data> data { get; set; }
        public string code { get; set; }
        public string error { get; set; }
    }
}
