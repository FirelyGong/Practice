using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice.DataBase.Objects
{
    public class RelatedObjectAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
