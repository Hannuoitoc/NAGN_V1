using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NAGN_V1.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
        //Dach sách viewmodel sẽ dược khởi tạo
        public DesignFlowViewModel DesignFlowViewModel { get; set; }
        private Model.Flow _flow { get; set; }
        public Model.Flow Flow
        {
            
            get => _flow;
            set{
                _flow = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            //Khi viewmodel được khởi tạo sẽ
            DesignFlowViewModel = new DesignFlowViewModel(this);
            NewFlowCommand = new RelayCommand(NewFlow);
            OpenViewDesignFlowCommand=new RelayCommand(OpenViewDesignFlow,isFlow);
            
        }
        //Tạo mới flow
        public ICommand NewFlowCommand { get; }
        private void NewFlow()
        {
            View.InputNameFlowDialog inputNameFlowDialog = new View.InputNameFlowDialog();
            if(inputNameFlowDialog.ShowDialog() == true)
            {
                Flow = new Model.Flow
                {
                    Name = inputNameFlowDialog.FlowName 
                };
            }
        }
        //Mở flow
        //Mở cửa sổ design
        private View.DesignFlowView _viewDesignFlow;
        public ICommand OpenViewDesignFlowCommand { get; }
        private void OpenViewDesignFlow()
        {
            if(_viewDesignFlow == null)
            {
                _viewDesignFlow = new View.DesignFlowView();
                _viewDesignFlow.DataContext = this.DesignFlowViewModel;
                _viewDesignFlow.Show();
                _viewDesignFlow.Activate();
            }
            else
            {
                _viewDesignFlow.Show();
                _viewDesignFlow.Activate();
            }  
        }
        private bool isFlow()
        {
            return Flow!=null;
        }
    }
}
