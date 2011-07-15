using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace FirstSilverlightApplication
{
    public partial class MainPage : UserControl
    {
        private ObservableCollection<Tweet> _tweets = new ObservableCollection<Tweet>();

        public MainPage()
        {
            InitializeComponent();
            TweetList.ItemsSource = _tweets;
        }
        
        private void GetTweets_Click(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (s, ea) =>
            {
                System.Diagnostics.Debug.WriteLine(ea.Result);

                XDocument doc = XDocument.Parse(ea.Result);
                XNamespace ns = "http://www.w3.org/2005/Atom";
                var items = from item in doc.Descendants(ns + "entry")
                            select new Tweet()
                            {
                                Message = item.Element(ns + "title").Value,
                                Image = new Uri((
                                from XElement xe in item.Descendants(ns + "link")
                                where xe.Attribute("type").Value == "image/png"
                                select xe.Attribute("href").Value
                                ).First<string>()),
                            };
                foreach (Tweet t in items)
                {
                    _tweets.Add(t);
                }
            };

            client.DownloadStringAsync( new Uri("http://search.twitter.com/search.atom?q=silverlight")); 
        }
    }
}
