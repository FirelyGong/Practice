using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.DataBase.Objects
{
    public interface IRelatedObject
    {
        IList<string> Properties { get; }

        string GetPropertyValue(string propertyName, out bool isRelatedObject);
    }
}
