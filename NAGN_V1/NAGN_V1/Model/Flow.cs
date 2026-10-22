using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAGN_V1.Model
{
    public class Flow
    {
        public string Name { get; set; }
        public ObservableCollection<Node> NodeList { get; set; }=new ObservableCollection<Node>();
    }
}
