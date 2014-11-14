using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Demo
{
    public class MyTextBox:Control
    {
        static MyTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyTextBox), new FrameworkPropertyMetadata(typeof(MyTextBox)));
        }

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();

        //    if (Style == null)
        //    {
        //        Style = new Style(GetType(), FindResource(typeof(MyTextBox)) as Style);
        //    }
        //}
    }
}
