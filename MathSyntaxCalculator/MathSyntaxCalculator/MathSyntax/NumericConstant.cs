using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class NumericConstant : SyntaxBlock
    {
        public NumericConstant(float Value)
        {
            value = Value;
        }
        public float value { get; private set; }
        
        public string print()
        {
            return value.ToString();
        }

        public List<ArgumentValue> GetAllVariables()
        {
            return new List<ArgumentValue>();
        }

        public bool IsConstant(Dictionary<long, bool> TemporaryConstant)
        {
            return true;
        }

        public SyntaxBlock Derivative(Dictionary<long, bool> TemporaryConstant)
        {
            return new NumericConstant(0);
        }
    }
}
