using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    class SimpleProperty : IProperties
    {
        public string Name
        {
            get;
            private set;
        }

        public string Value
        {
            get;
            private set;
        }

        public SimpleProperty(string name,string value)
        {
            Name = name;
            Value = value;
        }
    }
}
