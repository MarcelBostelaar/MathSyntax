using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax.AbstractClasses
{
    /// <summary>
    /// Shared methods for Sum and Product (add a super class that abstracts shared methods for all operators)
    /// </summary>
    abstract class AbstractOrderirrelevantOperator : AbstractOperator
    {
        public AbstractOrderirrelevantOperator(SyntaxBlock A, SyntaxBlock B) : base(A, B) { }
        public override bool ParallelEquals(SyntaxBlock ToCompare, int Depth)
        {
            if (Depth == 0)
            {
                return Equals(ToCompare);
            }

            var casted = ToCompare as AbstractOrderirrelevantOperator;
            if (casted == null)
                return false;
            if (ToCompare.GetType() != GetType())
                return false;

            bool aisa, aisb;
            aisa = false;
            aisb = false;

            Parallel.Invoke(
                () => {
                    if (A.Equals(casted.A))
                    {
                        if (B.Equals(casted.B))
                            aisa = true;
                    }
                },
                () => {
                    if (B.Equals(casted.A))
                    {
                        if (A.Equals(casted.B))
                            aisb = true;
                    }
                }
                );
            return aisa || aisb;
        }
        public override bool Equals(SyntaxBlock ToCompare)
        {
            var casted = ToCompare as AbstractOrderirrelevantOperator;
            if (casted == null)
            {
                return false;
            }
            if (ToCompare.GetType() != GetType())
                return false;

            if (A.Equals(casted.A))
            {
                if (B.Equals(casted.B))
                    return true;
            }
            if (B.Equals(casted.A))
            {
                if (A.Equals(casted.B))
                    return true;
            }
            return false;
        }
    }
}
