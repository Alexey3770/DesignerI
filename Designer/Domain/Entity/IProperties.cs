using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    public interface IProperties
    {
        string Name { get; }
        string Value { get; }
    }
}
