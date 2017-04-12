using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Product : SyntaxBlock
    {
        SyntaxBlock A, B;
        /// <summary>
        /// Creates a product syntax block, which multiplies argument A and B together.
        /// </summary>
        /// <param name="A">The left side of the product.</param>
        /// <param name="B">The right side of the product.</param>
        public Product(SyntaxBlock A, SyntaxBlock B)
        {
            this.A = A;
            this.B = B;
        }

        public double Calculate()
        {
            return A.Calculate() * B.Calculate();
        }

        public SyntaxBlock Derivative(VariableArgumentValue ArgumentToDerive)
        {
            return DerivativeFormulate(A.Derivative(ArgumentToDerive), B.Derivative(ArgumentToDerive));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SyntaxBlock DerivativeFormulate(SyntaxBlock Adir, SyntaxBlock Bdir)
        {
            return new Sum(new Product(Adir, B), new Product(A, Bdir));
        }

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
            if(Depth == 0)
            {
                return Calculate();
            }
            double a = 0;
            double b = 0;
            Parallel.Invoke(
                () => { a = A.ParallelCalculate(Depth - 1); },
                () => { b = B.ParallelCalculate(Depth - 1); }
                );
            return a * b;
        }

        public SyntaxBlock ParallelDerivative(VariableArgumentValue ArgumentToDerive, int Depth)
        {
            if(Depth == 0)
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
            if(Depth == 0)
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
            if(Depth == 0)
            {
                return IsConstant(Non_Constant);
            }
            bool a = false;
            bool b = false;
            Parallel.Invoke(
                () => { a = A.ParallelIsConstant(Non_Constant, Depth-1); },
                () => { b = B.ParallelIsConstant(Non_Constant, Depth-1); }
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
            return SimplifySelf();
        }

        public string print()
        {
            return printFormat(A.print(), B.print());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string printFormat(string a, string b)
        {
            return ("(" + a + " * " + b + " )");
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
