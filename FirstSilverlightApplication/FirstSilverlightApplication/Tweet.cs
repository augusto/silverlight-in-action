using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace FirstSilverlightApplication
{
    public class Tweet
    {
        public string Message { get; set; }
        public Uri Image { get; set; }

        public override string ToString()
        {
            return Message;
        }
    }
}
