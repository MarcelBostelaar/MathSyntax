using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Product : SyntaxBlock
    {
        SyntaxBlock A, B;
        public Product(SyntaxBlock A, SyntaxBlock B)
        {
            this.A = A;
            this.B = B;
        }

        public SyntaxBlock Derivative(ArgumentValue ArgumentToDerive)
        {
            return new Sum(new Product(A.Derivative(ArgumentToDerive), B), new Product(A, B.Derivative(ArgumentToDerive)));
        }

        public List<ArgumentValue> GetAllVariables(bool OnlyNonConstants)
        {
            var lista = A.GetAllVariables(OnlyNonConstants);
            lista.AddRange(B.GetAllVariables(OnlyNonConstants));
            return lista;
        }

        public bool IsConstant(ArgumentValue Non_Constant)
        {
            if (A.IsConstant(Non_Constant) && B.IsConstant(Non_Constant))
                return true;
            return false;
        }

        public string print()
        {
            return ("(" + A.print() + " * " + B.print() + " )");
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


            if (_a == null && _b == null) //Neither A nor B are numeric constants, return this product in its existing state.
            {
                return this;
            }

            if (_a != null && _b != null) //Both A and B are numeric constants, return new numeric constant that is the product of both.
            {
                return new NumericConstant(_a.value * _b.value);
            }

            if (_a != null) //if a is zero, return zero;
            {
                if (_a.value == 0)
                {
                    return new NumericConstant(0);
                }
            }
            if (_b != null) //if b is zero, return zero;
            {
                if (_b.value == 0)
                {
                    return new NumericConstant(0);
                }
            }


            if (_a != null) //if a is one, return B;
            {
                if (_a.value == 1)
                {
                    return B;
                }
            }
            if (_b != null) //if b is one, return A;
            {
                if (_b.value == 1)
                {
                    return A;
                }
            }

            return this; //No simplification possible, return this sum in its existing state.
        }
    }
}
