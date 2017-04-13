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
    abstract class AbstractOrderirrelevantOperator : SyntaxBlock
    {
        SyntaxBlock A, B;
        public AbstractOrderirrelevantOperator(SyntaxBlock A, SyntaxBlock B)
        {
            this.A = A;
            this.B = B;
        }

        public abstract double Calculate();

        public SyntaxBlock Derivative(VariableArgumentValue ArgumentToDerive)
        {
            return DerivativeFormulate(A.Derivative(ArgumentToDerive), B.Derivative(ArgumentToDerive));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract SyntaxBlock DerivativeFormulate(SyntaxBlock Adir, SyntaxBlock Bdir);

        public List<ArgumentValue> GetAllVariables(bool OnlyNonConstants)
        {
            var lista = A.GetAllVariables(OnlyNonConstants);
            lista.AddRange(B.GetAllVariables(OnlyNonConstants));
            return lista;
        }

        public bool IsConstant(VariableArgumentValue Non_Constant)
        {
            if (A.IsConstant(Non_Constant) && B.IsConstant(Non_Constant))
                return true;
            return false;
        }

        public double ParallelCalculate(int Depth)
        {
            if (Depth == 0)
            {
                return Calculate();
            }
            double a = 0;
            double b = 0;
            Parallel.Invoke(
                () => { a = A.ParallelCalculate(Depth - 1); },
                () => { b = B.ParallelCalculate(Depth - 1); }
                );
            return CalculateResult(a, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract double CalculateResult(double a, double b);


        public SyntaxBlock ParallelDerivative(VariableArgumentValue ArgumentToDerive, int Depth)
        {
            if (Depth == 0)
            {
                return Derivative(ArgumentToDerive);
            }
            SyntaxBlock adir = null;
            SyntaxBlock bdir = null;
            Parallel.Invoke(
                () => { adir = A.ParallelDerivative(ArgumentToDerive, Depth - 1); },
                () => { bdir = B.ParallelDerivative(ArgumentToDerive, Depth - 1); }
                );
            return DerivativeFormulate(adir, bdir);
        }

        public List<ArgumentValue> ParallelGetAllVariables(bool OnlyNonConstants, int Depth)
        {
            if (Depth == 0)
            {
                return GetAllVariables(OnlyNonConstants);
            }
            List<ArgumentValue> a = null;
            List<ArgumentValue> b = null;
            Parallel.Invoke(
                () => { a = A.ParallelGetAllVariables(OnlyNonConstants, Depth - 1); },
                () => { b = B.ParallelGetAllVariables(OnlyNonConstants, Depth - 1); }
                );
            a.AddRange(b);
            return a;
        }

        public bool ParallelIsConstant(VariableArgumentValue Non_Constant, int Depth)
        {
            if (Depth == 0)
            {
                return IsConstant(Non_Constant);
            }
            bool a = false;
            bool b = false;
            Parallel.Invoke(
                () => { a = A.ParallelIsConstant(Non_Constant, Depth - 1); },
                () => { b = B.ParallelIsConstant(Non_Constant, Depth - 1); }
                );
            return a && b;
        }

        public string ParallelPrint(int Depth)
        {
            if (Depth == 0)
            {
                return print();
            }
            string a = "";
            string b = "";
            Parallel.Invoke(
                () => { a = A.ParallelPrint(Depth - 1); },
                () => { b = B.ParallelPrint(Depth - 1); }
                );
            return printFormat(a, b);
        }

        public SyntaxBlock ParallelSimplify(int Depth)
        {
            if (Depth == 0)
            {
                return Simplify();
            }
            Parallel.Invoke(
                () => { A = A.ParallelSimplify(Depth - 1); },
                () => { B = B.ParallelSimplify(Depth - 1); }
                );
            return SimplifySelf();
        }

        public string print()
        {
            return printFormat(A.print(), B.print());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        abstract protected string printFormat(string a, string b);

        public SyntaxBlock Simplify()
        {
            A = A.Simplify();
            B = B.Simplify();
            return SimplifySelf();
        }
        protected abstract SyntaxBlock SimplifySelf();

        public bool Equals(SyntaxBlock ToCompare)
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

        public bool ParallelEquals(SyntaxBlock ToCompare, int Depth)
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
    }
}
