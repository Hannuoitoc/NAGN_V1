using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NAGN_V1.ViewModel
{
    public class DesignFlowViewModel
    {

        private readonly MainWindowViewModel _mainWindownViewModel;
        public Model.Flow Flow => _mainWindownViewModel.Flow;
        public Model.Node NodeSelect;
        public ObservableCollection<Model.Node> ToolList { get; set; }
        public DesignFlowViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindownViewModel = mainWindowViewModel;
            ToolList = new ObservableCollection<Model.Node>()
            {
                new Model.Node{Id="IS",Name="Image source"},
                new Model.Node{Id="SI",Name="Set image"},
                new Model.Node{Id="TH",Name="Threshold"},
                new Model.Node{Id="BL",Name="Blur"}
            };
            RunNodeCommand = new RelayCommand(RunNode);
        }

        public ICommand RunNodeCommand { get;}
        public void RunNode()
        {
            NodeSelect.ExecuteAlogorith();
        }
    }
}
