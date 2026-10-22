using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NAGN_V1.Model
{
    public class SetImage:Node
    {
        private string _imageId;
        public string ImageId
        {
            get=> _imageId;
            set
            {
                _imageId = value;
                OnPropertyChanged();
            }
        }
        private Image _screen;
        public Image Screen
        {
            get => _screen;
            set { _screen = value; OnPropertyChanged(); }
        }
    }
}
