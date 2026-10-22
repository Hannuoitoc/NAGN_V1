using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NAGN_V1.ViewModel
{
    public class SetImageViewModel:ViewModelBase
    {
        public Model.SetImage SetImage { get; set; }
        public ObservableCollection<Model.Node> listImage => App.listImage;
        public ObservableCollection<Model.ImageScreen> listScreen => App.listScreen;
        public SetImageViewModel(Model.SetImage setImage)
        {
            SetImage = setImage;
            SetImage.Alogorithm = () =>
            {
                if(listImage.FirstOrDefault(n => n.Id == setImage.ImageId) is Model.ImageSource imageSource)
                {
                    SetImage.Screen.Source = imageSource.image;
                }
                if(listImage.FirstOrDefault(n => n.Id == setImage.ImageId) is Model.Threshold thresholdImage)
                {
                    SetImage.Screen.Source = thresholdImage.ThresholdImage;
                }
            };
        }
    }
}
