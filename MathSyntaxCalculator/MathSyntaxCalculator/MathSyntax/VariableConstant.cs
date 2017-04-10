using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class VariableConstant : Abstract_variable
    {
        public VariableConstant(ArgumentValue Argument) : base(Argument, true) { }

        public override SyntaxBlock Derivative(ArgumentValue ArgumentToDerive)
        {
            return new NumericConstant(0);
        }

        public override List<ArgumentValue> GetAllVariables(bool OnlyNonConstants)
        {
            var list = new List<ArgumentValue>();
            if (!OnlyNonConstants)
            {
                list.Add(Argument);
            }
            return list;
        }

        public override bool IsConstant(ArgumentValue Non_Constant)
        {
            return true;
        }
    }
}
