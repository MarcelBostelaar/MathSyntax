using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Quotient : AbstractClasses.AbstractOrderstrictOperator
    {
        public Quotient(SyntaxBlock A, SyntaxBlock B) : base(A, B) { }

        public override double Calculate()
        {
            return A.Calculate() / B.Calculate();
        }

        public override SyntaxBlock DerivativeFormulate(SyntaxBlock Adir, SyntaxBlock Bdir)
        {
            //(A'*B + -1(B' * A))/ (B * B) => (A'*B - B' * A)/ B ^ 2
            return new Quotient(new Sum(new Product(Adir, B),
                new Product(new NumericConstant(-1), new Product(Bdir, A))), new Product(B, B));
        }

        protected override double CalculateResult(double a, double b)
        {
            return a / b;
        }

        protected override string printFormat(string a, string b)
        {
            return "(" + a + "/" + b + ")";
        }

        protected override SyntaxBlock SimplifySelf()
        {
            var a = A as NumericConstant;
            var b = B as NumericConstant;

            if (a == null && b == null) //Neither A nor B are numeric constants, return this quotient in its existing state.
                return this;

            if (a != null && b != null) //Both A and B are numeric constants, return new numeric constant that is the quotient of both.
                return new NumericConstant(a.value / b.value);

            if (a?.value == 0)
                return new NumericConstant(0);

            if (b?.value == 1)
            {
                return A;
            }
            else if (b?.value == 0)
            {
                throw new DivideByZeroException("Can't devide by zero!");
            }
            return this; //No simplification possible, return this quotient in its existing state.
        }
    }
}
