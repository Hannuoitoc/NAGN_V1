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
    /// Interaction logic for InputNameFlowDialog.xaml
    /// </summary>
    public partial class InputNameFlowDialog : Window
    {
        public string FlowName { get; set; } = string.Empty;
        public InputNameFlowDialog()
        {
            InitializeComponent();
            txtInputFlowModel.Focus();
        }

        private void butHuy_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void butTao_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtInputFlowModel.Text))
            {
                MessageBox.Show("Không được đặt tên trống.", "Cảnh báo", MessageBoxButton.OK);
                return;
            }
            FlowName = txtInputFlowModel.Text.Trim();
            this.DialogResult = true;
        }
    }
}
