using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

namespace NAGN_V1.ViewModel
{
    public class ThresholdeViewModel
    {
        public Model.Threshold Threshold { get; set; }
        public ObservableCollection<Model.Node> listImage => App.listImage;
        public ObservableCollection<Model.Node> listImageNotThis { get; set; }
        public ThresholdeViewModel(Model.Threshold threshold)
        {
            Threshold = threshold;
            Threshold.Alogorithm = () =>
            {
                ThresholdeAlogorithm(Threshold.Min, Threshold.Max);
            };
            App.listImage.CollectionChanged += AppListImage_CollectionChanged;
            var filteredItems = App.listImage.Where(x => x != Threshold);
            listImageNotThis = new ObservableCollection<Model.Node>(filteredItems);
        }

        private void AppListImage_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Model.Node newItem in e.NewItems)
                    {
                        listImageNotThis.Add(newItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (Model.Node oldItem in e.OldItems)
                    {
                        listImageNotThis.Remove(oldItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    listImageNotThis.Clear();
                    break;
            }
        }
        private async void ThresholdeAlogorithm(int min, int max)
        {
            if (Threshold.ImageId == null) return;
            if (listImage.FirstOrDefault(n => n.Id == Threshold.ImageId) is Model.ImageSource imageSource)
            {
                min = Math.Clamp(min, 0, 255);
                max = Math.Clamp(max, 0, 255);
                if (min > max)
                {
                    int temp = min;
                    min = max;
                    max = temp;
                }
                await Task.Run(() =>
                {
                    using(Mat imageMat=imageSource.image.ToMat())
                    using(Mat grayImage=new Mat())
                    using(Mat thresholdImage=new Mat())
                    {
                        if (imageMat.Channels() == 1)
                    {
                        imageMat.CopyTo(grayImage);
                    }
                    else
                    {
                        ColorConversionCodes colorConversionCodes = (imageMat.Channels() == 3) ? ColorConversionCodes.RGB2GRAY : ColorConversionCodes.RGBA2GRAY;
                        Cv2.CvtColor(imageMat, grayImage, colorConversionCodes);
                    }
                        Cv2.InRange(grayImage, new Scalar(min), new Scalar(max), thresholdImage);
                        BitmapSource bs = thresholdImage.ToBitmapSource();
                        bs.Freeze();
                        Threshold.ThresholdImage = bs;
                    }
                });
            }
        }
    }
}
