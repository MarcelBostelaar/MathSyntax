using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Product : AbstractClasses.AbstractOrderirrelevantOperator
    {
        /// <summary>
        /// Creates a product syntax block, which multiplies argument A and B together.
        /// </summary>
        /// <param name="A">The left side of the product.</param>
        /// <param name="B">The right side of the product.</param>
        public Product(SyntaxBlock A, SyntaxBlock B) : base(A,B)
        {
        }

        public override double Calculate()
        {
            return A.Calculate() * B.Calculate();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override SyntaxBlock DerivativeFormulate(SyntaxBlock Adir, SyntaxBlock Bdir)
        {
            return new Sum(new Product(Adir, B), new Product(A, Bdir));
        }

        protected override double CalculateResult(double a, double b)
        {
            return a * b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override string printFormat(string a, string b)
        {
            return ("(" + a + " * " + b + " )");
        }
        protected override SyntaxBlock SimplifySelf()
        {
            var a = A as NumericConstant;
            var b = B as NumericConstant;


            if (a == null && b == null) //Neither A nor B are numeric constants, return this product in its existing state.
                return this;

            if (a != null && b != null) //Both A and B are numeric constants, return new numeric constant that is the product of both.
                return new NumericConstant(a.value* b.value);

            if (a?.value == 0)  //if a is zero, return zero;
                return new NumericConstant(0);
            if (b?.value == 0)  //if b is zero, return zero;
                return new NumericConstant(0);


            if (a?.value == 1)  //if a is one, return B;
                return B;
            if (b?.value == 1)  //if b is one, return A;
                return A;

            return this; //No simplification possible, return this sum in its existing state.
        }
        
    }
}
