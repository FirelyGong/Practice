using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class TreeViewWindowViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ObservableCollection<DisplayNode> Nodes { get; private set; }

        private void Notify(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
