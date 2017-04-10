using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Sum : SyntaxBlock
    {
        public Sum(SyntaxBlock A, SyntaxBlock B)
        {
            this.A = A;
            this.B = B;
        }

        SyntaxBlock A, B;

        public string print()
        {
            return ("(" + A.print() + " + " + B.print() + ")");
        }

        public List<ArgumentValue> GetAllVariables(bool OnlyNonConstants)
        {
            List<ArgumentValue> listA, listB;
            listA = A.GetAllVariables(OnlyNonConstants);
            listB = B.GetAllVariables(OnlyNonConstants);
            listA.AddRange(listB);
            return listA;
        }

        public bool IsConstant(ArgumentValue Non_Constant)
        {
            if (A.IsConstant(Non_Constant) && B.IsConstant(Non_Constant))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SyntaxBlock Derivative(ArgumentValue ArgumentToDerive)
        {
            bool AisConstant, BisConstant;
            AisConstant = A.IsConstant(ArgumentToDerive);
            BisConstant = B.IsConstant(ArgumentToDerive);
            if (AisConstant && BisConstant)
            {
                return new NumericConstant(0);
            }
            SyntaxBlock _a, _b;

            if (AisConstant)
            {
                _a = new NumericConstant(0);
            }
            else
            {
                _a = A.Derivative(ArgumentToDerive);
            }

            if (BisConstant)
            {
                _b = new NumericConstant(0);
            }
            else
            {
                _b = B.Derivative(ArgumentToDerive);
            }
            return new Sum(_a, _b);
        }

        public SyntaxBlock Simplify()
        {
            A = A.Simplify();
            B = B.Simplify();
            
            NumericConstant _a, _b;
            _a = null;
            _b = null;
            try
            {
                _a = (NumericConstant)A;
            }
            catch { }
            try
            {
                _b = (NumericConstant)B;
            }
            catch { }

            if(_a == null && _b == null) //Neither A nor B are numeric constants, return this sum in its existing state.
            {
                return this;
            }

            if (_a != null && _b != null) //Both A and B are numeric constants, return new numeric constant that is the sum of both.
            {
                return new NumericConstant(_a.value + _b.value);
            }

            if(_a != null) //if a is zero, return B;
            {
                if(_a.value == 0)
                {
                    return B;
                }
            }

            if (_b != null) //if b is zero, return A;
            {
                if (_b.value == 0)
                {
                    return A;
                }
            }

            return this; //No simplification possible, return this sum in its existing state.
        }

        public double Calculate()
        {
            return A.Calculate() + B.Calculate();
        }
    }
}
