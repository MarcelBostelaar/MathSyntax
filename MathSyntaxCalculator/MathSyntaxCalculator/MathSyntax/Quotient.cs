using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Quotient : SyntaxBlock
    {
        SyntaxBlock A, B;
        public Quotient(SyntaxBlock A, SyntaxBlock B)
        {
            this.A = A;
            this.B = B;
        }

        public double Calculate()
        {
            return A.Calculate() / B.Calculate();
        }

        public SyntaxBlock Derivative(VariableArgumentValue ArgumentToDerive)
        {
            return DerivativeForming(A.Derivative(ArgumentToDerive), B.Derivative(ArgumentToDerive));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private SyntaxBlock DerivativeForming(SyntaxBlock Adir, SyntaxBlock Bdir)
        {
            //(A'*B + -1(B'*A))/(B*B) == (A'*B - B'*A)/B^2
            return new Quotient(new Sum(new Product(Adir, B),
                new Product(new NumericConstant(-1), new Product(Bdir, A))), new Product(B, B));
        }

        public List<ArgumentValue> GetAllVariables(bool OnlyNonConstants)
        {
            var list = A.GetAllVariables(OnlyNonConstants);
            list.AddRange(B.GetAllVariables(OnlyNonConstants));
            return list;
        }

        public bool IsConstant(VariableArgumentValue Non_Constant)
        {
            return A.IsConstant(Non_Constant) && B.IsConstant(Non_Constant);
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
                () => { a = A.ParallelCalculate( Depth - 1); },
                () => { b = B.ParallelCalculate( Depth - 1); }
                );
            return a/b;
        }

        public SyntaxBlock ParallelDerivative(VariableArgumentValue ArgumentToDerive, int Depth)
        {
            if(Depth == 0)
            {
                return Derivative(ArgumentToDerive);
            }
            SyntaxBlock Adir = null;
            SyntaxBlock Bdir = null;
            Parallel.Invoke(
                () => { Adir = A.ParallelDerivative(ArgumentToDerive, Depth - 1); },
                () => { Bdir = B.ParallelDerivative(ArgumentToDerive, Depth - 1); }
                );
            return DerivativeForming(Adir, Bdir);
        }

        public List<ArgumentValue> ParallelGetAllVariables(bool OnlyNonConstants, int Depth)
        {
            if (Depth == 0)
                return GetAllVariables(OnlyNonConstants);
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
            if(Depth == 0)
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
            if(Depth == 0)
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
            if(Depth == 0)
            {
                return Simplify();
            }
            Parallel.Invoke(
                () => { A = A.ParallelSimplify(Depth - 1); },
                () => { B = B.ParallelSimplify(Depth - 1); }
                );
            return SimplySelf();
        }

        public string print()
        {
            return printFormat(A.print(), B.print());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string printFormat(string a, string b)
        {
            return "(" + a + " / " + b + ")";
        }

        public SyntaxBlock Simplify()
        {
            A = A.Simplify();
            B = B.Simplify();
            return SimplySelf();
        }

        private SyntaxBlock SimplySelf()
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
