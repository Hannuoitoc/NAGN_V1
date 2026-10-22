using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NAGN_V1.ViewModel
{
    public class ImageSourceViewModel:ViewModelBase
    {
        public Model.ImageSource ImageSource { get; set; }
        public ImageSourceViewModel(Model.ImageSource imageSource)
        {
            ImageSource=imageSource;
            GetImageSourceCommand = new RelayCommand(GetImageSource);
        }
        public ICommand GetImageSourceCommand { get; }
        public void GetImageSource()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*",
                Title = "Chọn ảnh đầu vào"
            };
            if(openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                ImageSource.filePath = filePath;
                if (!File.Exists(filePath)) return;
                try
                {
                    byte[] imageBytes = File.ReadAllBytes(filePath);
                    BitmapImage bitmap = new BitmapImage();
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = memoryStream;
                        bitmap.EndInit();
                        bitmap.Freeze();
                    }
                    ImageSource.image = bitmap;
                    if (App.listImage.FirstOrDefault(n => n.Id == ImageSource.Id) != null)
                    {
                        if(App.listImage.FirstOrDefault(n => n.Id == ImageSource.Id) is Model.ImageSource imageSource)
                        {
                            imageSource.image = imageSource.image;
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể set up ảnh: {ex.Message}", "Lỗi set ảnh", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }
    }
}
