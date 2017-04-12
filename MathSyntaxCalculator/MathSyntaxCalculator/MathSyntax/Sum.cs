using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Sum : SyntaxBlock
    {
        /// <summary>
        /// Creates a sum syntax block which adds argument A and B together.
        /// </summary>
        /// <param name="A">The left side of the sum.</param>
        /// <param name="B">The right side of the sum.</param>
        public Sum(SyntaxBlock A, SyntaxBlock B)
        {
            this.A = A;
            this.B = B;
        }

        SyntaxBlock A, B;

        public string print()
        {
            return printFormat(A.print(), B.print());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string printFormat(string A, string B)
        {
            return ("(" + A + " + " + B + ")");
        }

        public List<ArgumentValue> GetAllVariables(bool OnlyNonConstants)
        {
            List<ArgumentValue> listA, listB;
            listA = A.GetAllVariables(OnlyNonConstants);
            listB = B.GetAllVariables(OnlyNonConstants);
            listA.AddRange(listB);
            return listA;
        }

        public bool IsConstant(VariableArgumentValue Non_Constant)
        {
            return A.IsConstant(Non_Constant) && B.IsConstant(Non_Constant);
        }

        public SyntaxBlock Derivative(VariableArgumentValue ArgumentToDerive)
        {
            return new Sum(A.Derivative(ArgumentToDerive), B.Derivative(ArgumentToDerive));
        }

        public SyntaxBlock Simplify()
        {
            A = A.Simplify();
            B = B.Simplify();
            return SimplifySelf();
        }

        private SyntaxBlock SimplifySelf()
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

        public double Calculate()
        {
            return A.Calculate() + B.Calculate();
        }

        public bool ParallelIsConstant(VariableArgumentValue Non_Constant, int Depth)
        {
            if (Depth == 0)
            {
                return IsConstant(Non_Constant);
            }
            else
            {
                bool a, b;
                a = false;
                b = false;
                Parallel.Invoke(
                    () => { a = A.ParallelIsConstant(Non_Constant, Depth - 1); },
                    () => { b = B.ParallelIsConstant(Non_Constant, Depth - 1); }
                    );
                return a && b;
            }
        }

        public string ParallelPrint(int Depth)
        {
            if (Depth == 0)
            {
                return print();
            }
            else
            {
                string a, b;
                a = "";
                b = "";
                Parallel.Invoke(
                    () => { a = A.ParallelPrint(Depth - 1); },
                    () => { b = B.ParallelPrint(Depth - 1); }
                    );
                return printFormat(a, b);
            }
        }

        public List<ArgumentValue> ParallelGetAllVariables(bool OnlyNonConstants, int Depth)
        {
            if (Depth == 0)
            {
                return GetAllVariables(OnlyNonConstants);
            }
            List<ArgumentValue> listA, listB;
            listA = null;
            listB = null;
            Parallel.Invoke(
                () => { listA = A.ParallelGetAllVariables(OnlyNonConstants, Depth - 1); },
                () => { listB = B.ParallelGetAllVariables(OnlyNonConstants, Depth - 1); }
                );
            listA.AddRange(listB);
            return listA;
        }

        public SyntaxBlock ParallelDerivative(VariableArgumentValue ArgumentToDerive, int Depth)
        {
            if (Depth == 0)
            {
                return Derivative(ArgumentToDerive);
            }
            SyntaxBlock a, b;
            a = null;
            b = null;
            Parallel.Invoke(
                () => { a = A.ParallelDerivative(ArgumentToDerive, Depth - 1); },
                () => { b = B.ParallelDerivative(ArgumentToDerive, Depth - 1); }
                );
            return new Sum(a, b);
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

        public double ParallelCalculate(int Depth)
        {
            if (Depth == 0)
            {
                return Calculate();
            }
            double a, b;
            a = 0;
            b = 0;
            Parallel.Invoke(
                () => { a = A.ParallelCalculate(Depth - 1); },
                () => { b = B.ParallelCalculate(Depth - 1); }
                );
            return a + b;
        }
    }
}
