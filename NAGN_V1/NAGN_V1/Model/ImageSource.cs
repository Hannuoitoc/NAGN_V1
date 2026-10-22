using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NAGN_V1.Model
{
    public class ImageSource:Node
    {
        private string _filePath;
        public string filePath
        {
            get => _filePath;
            set { _filePath = value; OnPropertyChanged(); }
        }
        private BitmapImage _image;
        public BitmapImage image
        {
            get => _image;
            set { _image = value; OnPropertyChanged(); }
        }
    }
}
