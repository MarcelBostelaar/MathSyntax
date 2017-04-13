using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Sum : AbstractClasses.AbstractOrderirrelevantOperator
    {
        /// <summary>
        /// Creates a sum syntax block which adds argument A and B together.
        /// </summary>
        /// <param name="A">The left side of the sum.</param>
        /// <param name="B">The right side of the sum.</param>
        public Sum(SyntaxBlock A, SyntaxBlock B) : base(A,B)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override string printFormat(string A, string B)
        {
            return ("(" + A + " + " + B + ")");
        }

        protected override SyntaxBlock SimplifySelf()
        {
            var a = A as NumericConstant;
            var b = B as NumericConstant;

            if (a == null && b == null) //Neither A nor B are numeric constants, return this sum in its existing state.
                return this;

            if (a != null && b != null) //Both A and B are numeric constants, return new numeric constant that is the sum of both.
                return new NumericConstant(a.value + b.value);

            if (a?.value == 0)  //if a is zero, return B;
                return B;

            if (b?.value == 0)  //if b is zero, return A;
                return A;

            return this; //No simplification possible, return this sum in its existing state.
        }

        public override double Calculate()
        {
            return A.Calculate() + B.Calculate();
        }

        public override SyntaxBlock DerivativeFormulate(SyntaxBlock Adir, SyntaxBlock Bdir)
        {
            return new Sum(Adir, Bdir);
        }

        protected override double CalculateResult(double a, double b)
        {
            return a + b;
        }
    }
}
