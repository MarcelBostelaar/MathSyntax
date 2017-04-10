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

        public List<ArgumentValue> GetAllVariables(bool OnlyNonConstants)
        {
            return new List<ArgumentValue>();
        }

        public bool IsConstant(ArgumentValue Non_Constant)
        {
            return true;
        }

        public SyntaxBlock Derivative(ArgumentValue ArgumentToDerive)
        {
            return new NumericConstant(0);
        }

        public SyntaxBlock Simplify()
        {
            return this;
        }
    }
}
