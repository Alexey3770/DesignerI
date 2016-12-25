using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс содержащий информацию об одном свойстве
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Название свойства
        /// </summary>
        public string Name
        {
            get;
            private set;
        }
        /// <summary>
        /// Значение свойства
        /// </summary>
        public string Value
        {
            get;
            set;
        }
        /// <summary>
        /// Можно ли менять свойство
        /// </summary>
        public bool IsLock
        {
            get;
            private set;
        }

        public Property(string name,string value,bool isLock = false)
        {
            Name = name;
            Value = value;
            IsLock = isLock;
        }
    }
}
