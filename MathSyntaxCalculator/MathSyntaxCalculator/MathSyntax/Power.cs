using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Power : SyntaxBlock
    {
        public Power(SyntaxBlock A, SyntaxBlock ToThePowerOf)
        {
            throw new NotImplementedException();
        }

        public double Calculate()
        {
            throw new NotImplementedException();
        }

        public SyntaxBlock Derivative(ArgumentValue ArgumentToDerive)
        {
            throw new NotImplementedException();
        }

        public List<ArgumentValue> GetAllVariables(bool OnlyNonConstants = true)
        {
            throw new NotImplementedException();
        }

        public bool IsConstant(ArgumentValue Non_Constant)
        {
            throw new NotImplementedException();
        }

        public string print()
        {
            throw new NotImplementedException();
        }

        public SyntaxBlock Simplify()
        {
            throw new NotImplementedException();
        }
    }
}
