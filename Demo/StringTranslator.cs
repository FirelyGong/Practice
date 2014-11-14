using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Demo
{
    public class StringTranslator:MarkupExtension
    {
        private string _token;

        public StringTranslator(string token)
        {
            _token = token;
        }

        public override string ToString()
        {
            return "[" + _token + "]";
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ToString();
        }
    }
}
