using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NAGN_V1.Model
{
    public class Node: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private double _canvasX;
        public double CanvasX
        {
            get => _canvasX;
            set
            {
                _canvasX = value;
                OnPropertyChanged();
            }
        }
        private double _canvasY;
        public double CanvasY
            {
            get => _canvasY;
            set
            {
                _canvasY = value;
                OnPropertyChanged();
            }
        }
        public Action? Alogorithm { get; set; }
        public void ExecuteAlogorith()
        {
            Alogorithm?.Invoke();
        }
    }
}
