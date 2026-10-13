using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NAGN_V1.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
        public MainWindowViewModel()
        {
               OpenViewDesignFlowCommand=new RelayCommand(OpenViewDesignFlow);
        }
        //Tạo mới flow
        //Mở flow
        //Mở cửa sổ design
        private View.ViewDesignFlow _viewDesignFlow;
        public ICommand OpenViewDesignFlowCommand { get; }
        private void OpenViewDesignFlow()
        {
            if(_viewDesignFlow == null)
            {
                _viewDesignFlow = new View.ViewDesignFlow();
                _viewDesignFlow.DataContext = this;
            }
            _viewDesignFlow.Show();
            _viewDesignFlow.Activate();
        }
    }
}
