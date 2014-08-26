using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareServer.Interfaces
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class CategoryAttribute : Attribute
    {
        public CategoryAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}
