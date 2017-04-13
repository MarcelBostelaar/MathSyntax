using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax.AbstractClasses
{
    abstract class AbstractOrderstrictOperator : AbstractOperator
    {
        public AbstractOrderstrictOperator(SyntaxBlock A, SyntaxBlock B) : base(A, B) { }

        public override bool ParallelEquals(SyntaxBlock ToCompare, int Depth)
        {
            if (Depth == 0)
                return Equals(ToCompare);

            var casted = ToCompare as AbstractOrderstrictOperator;
            if (casted == null)
                return false;
            if (ToCompare.GetType() != GetType())
                return false;
            bool a = false;
            bool b = false;
            Parallel.Invoke(
                () => { a = A.Equals(casted.A); },
                () => { b = B.Equals(casted.B); }
                );
            return a && b;
        }
        public override bool Equals(SyntaxBlock ToCompare)
        {
            var casted = ToCompare as AbstractOrderstrictOperator;
            if (casted == null)
                return false;
            if (ToCompare.GetType() != GetType())
                return false;

            return A.Equals(casted.A) && B.Equals(casted.B);
        }
    }
}
