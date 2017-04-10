using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    public class ArgumentValue
    {
        public ArgumentValue(string Name)
        {
            this.Name = Name;
            Value = 0;
        }
        public string Name { get; private set; }
        public double Value { get; set; }
    }
}
