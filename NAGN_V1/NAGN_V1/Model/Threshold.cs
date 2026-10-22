using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NAGN_V1.Model
{
    public class Threshold:Node
    {
        private int _min = 127;
        private int _max = 127;
        private BitmapSource _thresholdImage;
        private string _imageId;
        public int Min
        {
            get => _min;
            set
            {
                _min = value;
                OnPropertyChanged();
            }
        }
        public int Max
        {
            get => _max;
            set
            {
                _max = value;
                OnPropertyChanged();
            }
        }
        public BitmapSource ThresholdImage
        {
            get => _thresholdImage;
            set
            {
                _thresholdImage = value;
                OnPropertyChanged();
            }
        }
        public string ImageId
        {
            get => _imageId;
            set
            {
                _imageId = value;
                OnPropertyChanged();
            }
        }
    }
}
