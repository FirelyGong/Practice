using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class DisplayNode:INotifyPropertyChanged
    {
        private double _left;
        private double _Top;
        private double _width;
        private double _height;
        private string _content;
        private string _toolTip;

        public double Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }

        public double Top
        {
            get
            {
                return _Top;
            }
            set
            {
                _Top = value;
            }
        }

        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return _toolTip;
            }
            set
            {
                _toolTip = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void Notify(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
