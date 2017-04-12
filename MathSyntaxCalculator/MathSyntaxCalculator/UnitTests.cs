using MathSyntax;
using System;
using System.Collections.Generic;

namespace MathSyntaxCalculator
{
    public class UnitTests
    {
        readonly NumericConstant Zero = new NumericConstant(0);
        readonly NumericConstant One = new NumericConstant(1);
        List<SyntaxBlock> Totest;
        VariableArgumentValue A = new VariableArgumentValue("A");
        ConstantArgumentValue B = new ConstantArgumentValue("B");

        public bool Test()
        {
            Build();
            return TestInterfacesForNotImplemented();
        }

        private void Build()
        {
            Totest = new List<SyntaxBlock>();
            Totest.Add(new Sum(Zero, One));
            Totest.Add(new Product(One, One));
            Totest.Add(new Quotient(One, One));
            Totest.Add(new NumericConstant(2));
            Totest.Add(new Variable(A));
            Totest.Add(new VariableConstant(B));
        }

        private bool TestInterfacesForNotImplemented()
        {
            try
            {
                foreach (var i in Totest)
                {
                    i.Calculate();
                    i.Derivative(A);
                    i.GetAllVariables(false);
                    i.IsConstant(A);
                    i.print();
                    i.Simplify();

                    i.ParallelCalculate(1);
                    i.ParallelDerivative(A, 1);
                    i.ParallelGetAllVariables(false, 1);
                    i.ParallelIsConstant(A, 1);
                    i.ParallelPrint(1);
                    i.ParallelSimplify(1);
                }
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
