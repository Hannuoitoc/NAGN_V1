using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NAGN_V1.View
{
    /// <summary>
    /// Interaction logic for DesignFlowView.xaml
    /// </summary>
    public partial class DesignFlowView : Window
    {
        private Model.Flow flow=>(this.DataContext as ViewModel.DesignFlowViewModel).Flow;
        public DesignFlowView()
        {
            InitializeComponent();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        //Thêm node lên flow
        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item && item.DataContext is Model.Node node)
            {
                // Tính điểm giữa CanvasContainer quy về hệ tọa độ Canvas thực tế
                Point center = ToCanvasPoint(new Point(
                    CanvasContainer.ActualWidth / 2,
                    CanvasContainer.ActualHeight / 2
                ));
                double cx = center.X - 90;  // NodeView Width=180 -> lệch 90
                double cy = center.Y - 110; // NodeView Height=220 -> lệch 110
                switch (node.Id)
                {
                    case "IS":
                        Model.ImageSource imageSource = new Model.ImageSource()
                        {
                            Id = CreateId(node.Id),
                            Name = $"{flow.NodeList.Count + 1}. Image Source",
                            CanvasX = cx,
                            CanvasY = cy
                        };
                        flow?.NodeList.Add(imageSource);
                        App.listImage.Add(imageSource);
                        break;
                    case "SI":
                        Model.SetImage setImage = new Model.SetImage()
                        {
                            Id = CreateId(node.Id),
                            Name = $"{flow.NodeList.Count + 1}. Set Image",
                            CanvasX = cx,
                            CanvasY = cy
                        };
                        flow?.NodeList.Add(setImage);
                        break;
                    case "TH":
                        Model.Threshold threshold = new Model.Threshold()
                        {
                            Id = CreateId(node.Id),
                            Name = $"{flow.NodeList.Count + 1}. Threshold",
                            CanvasX = cx,
                            CanvasY = cy
                        };
                        flow?.NodeList.Add(threshold);
                        App.listImage.Add(threshold);
                        break;
                }
            }
        }
        private string CreateId(string id)
        {
            int randomnumber = Random.Shared.Next(100, 999);
            int countNode=flow.NodeList.Count;
            string ID = $"{id}{countNode}{randomnumber}";
            return ID;
        }
        //Chọn node để hiển thị usercontrol
        private void Node_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Model.Node selectedNode)
            {
                if(this.DataContext is ViewModel.DesignFlowViewModel designFlowViewModel)
                {
                    designFlowViewModel.NodeSelect = selectedNode;
                    if(selectedNode is Model.ImageSource imageSource)
                    {
                        ViewModel.ImageSourceViewModel imageSourceViewModel = new ViewModel.ImageSourceViewModel(imageSource);
                        var view = new ImageSourceView();
                        view.DataContext = imageSourceViewModel;
                        NodeSetupPanel.Content=view;
                    }
                    if(selectedNode is Model.SetImage setImage)
                    {
                        ViewModel.SetImageViewModel setImageViewModel = new ViewModel.SetImageViewModel(setImage);
                        var view = new SetImageView();
                        view.DataContext = setImageViewModel;
                        NodeSetupPanel.Content = view;
                    }
                    if(selectedNode is Model.Threshold threshold)
                    {
                        ViewModel.ThresholdeViewModel thresholdeViewModel = new ViewModel.ThresholdeViewModel(threshold);
                        var view = new ThresholdView();
                        view.DataContext = thresholdeViewModel;
                        NodeSetupPanel.Content = view;
                    }
                }
            }
        }
        //Di chuyển node
        // Di chuyển node
        // Hàm tiện ích: Đổi tọa độ từ CanvasContainer -> Canvas thực tế (có tính Transform)
        private Point ToCanvasPoint(Point pointOnContainer)
        {
            return new Point(
                (pointOnContainer.X - CanvasTranslateTransform.X) / CanvasScaleTransform.ScaleX,
                (pointOnContainer.Y - CanvasTranslateTransform.Y) / CanvasScaleTransform.ScaleY
            );
        }
        private bool isDragging = false;
        private Point dragOffset;
        private Model.Node? draggingNode;
        private void Node_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Model.Node selectedNode)
            {
                isDragging = true;
                draggingNode = selectedNode;
                // Lấy tọa độ chuột đã quy đổi về hệ Canvas thực tế
                Point mouseOnCanvas = ToCanvasPoint(e.GetPosition(CanvasContainer));
                dragOffset.X = mouseOnCanvas.X - selectedNode.CanvasX;
                dragOffset.Y = mouseOnCanvas.Y - selectedNode.CanvasY;
                element.CaptureMouse();
                e.Handled = true;
            }
        }
        private void Node_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging || draggingNode == null) return;
            if (sender is FrameworkElement element)
            {
                // Quy đổi về hệ Canvas thực tế rồi mới gán
                Point mouseOnCanvas = ToCanvasPoint(e.GetPosition(CanvasContainer));
                draggingNode.CanvasX = mouseOnCanvas.X - dragOffset.X;
                draggingNode.CanvasY = mouseOnCanvas.Y - dragOffset.Y;
                e.Handled = true;
            }
        }
        private void Node_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            draggingNode = null;
            if (sender is FrameworkElement element)
                element.ReleaseMouseCapture();
        }
        private void Screen_Design_Loaded(object sender, RoutedEventArgs e)
        {
            if(sender is Image image)
            {
                Model.ImageScreen imageScreen = new Model.ImageScreen
                {
                    Name = image.Name,
                    Screen = image
                };
                App.listScreen.Add(imageScreen);
            }
        }
        //Zoom flow
        private double _currentZoom = 1;
        private bool isCtrl = false;
        private void MainCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (flow.NodeList.Count <= 0) return;
            Point mousePos=e.GetPosition(CanvasContainer);
            double zoomFactor = e.Delta > 0 ? 1.2 : 0.8;
            ZoomAtPotion(mousePos, zoomFactor);
            e.Handled = true;
        }
        private void ZoomAtPotion(Point centerPoint, double zoomFactor)
        {
            double newZoom = zoomFactor*_currentZoom;
            newZoom = Math.Clamp(newZoom, 0.05, 50.0);

            double effectiveFactor = newZoom / _currentZoom;
            _currentZoom = newZoom;
            CanvasTranslateTransform.X=centerPoint.X-((centerPoint.X - CanvasTranslateTransform.X) * effectiveFactor);
            CanvasTranslateTransform.Y=centerPoint.Y-((centerPoint.Y - CanvasTranslateTransform.Y) * effectiveFactor);
            CanvasScaleTransform.ScaleX = _currentZoom;
            CanvasScaleTransform.ScaleY = _currentZoom;
        }

    }
}
