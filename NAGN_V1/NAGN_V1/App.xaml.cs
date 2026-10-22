using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace NAGN_V1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ObservableCollection<Model.Node> listImage = new ObservableCollection<Model.Node>();
        public static ObservableCollection<Model.ImageScreen> listScreen = new ObservableCollection<Model.ImageScreen>();
    }
}
