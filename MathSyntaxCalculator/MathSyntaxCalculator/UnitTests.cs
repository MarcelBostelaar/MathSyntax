using MathSyntax;
using System;
using System.Collections.Generic;

namespace MathSyntaxCalculator
{
    public class UnitTests
    {
        readonly NumericConstant Zero = new NumericConstant(0);
        readonly NumericConstant One = new NumericConstant(1);
        List<SyntaxBlock> Totest, identical;
        VariableArgumentValue A = new VariableArgumentValue("A");
        ConstantArgumentValue B = new ConstantArgumentValue("B");

        Func<SyntaxBlock, SyntaxBlock, SyntaxBlock>[] Builders = new Func<SyntaxBlock, SyntaxBlock, SyntaxBlock>[]
            {
                (a,b)=> new Sum(a,b),
                (a,b)=> new Product(a,b),
                (a,b)=> new Quotient(a,b),
            };
        Func<double, SyntaxBlock>[] Variables = new Func<double, SyntaxBlock>[]
        {
                (a)=>new NumericConstant(a),
                (a)=> {var i = new VariableArgumentValue("V");
                    i.Value = a;
                    return new Variable(i);
                },
                (a)=> {var i = new ConstantArgumentValue("C");
                i.Value = a;
                return new VariableConstant(i);
                }
        };

        public bool Test()
        {
            bool succes = true;
            Build();
            succes &= TestInterfacesForNotImplemented();
            Build();
            succes &= EquivalentyTest();
            succes &= ParralelNormalEquivalenty();
            succes &= SimplifyTest();
            return succes;
        }

        private bool SimplifyTest()
        {
            bool succes = true;
            var x = RandomBigFormulas(100);
            foreach(var formula in x)
            {
                var z = formula.ParallelSimplify(3);
                succes &= z.ParallelCalculate(3) == formula.ParallelCalculate(3);
            }
            return succes;
        }

        private bool ParralelNormalEquivalenty()
        {
            bool succes = true;
            var x = RandomBigFormulas(100);
            foreach(var formula in x)
            {
                var variables = formula.ParallelGetAllVariables(true, 5);

                succes &= formula.Calculate() == formula.ParallelCalculate(5);
                succes &= formula.print() == formula.ParallelPrint(5);
                succes &= formula.Derivative((VariableArgumentValue)variables[0]).ParallelEquals(formula.ParallelDerivative((VariableArgumentValue)variables[0], 5), 5);
                succes &= formula.Simplify().Equals(formula.ParallelSimplify(5));
                succes &= formula.IsConstant((VariableArgumentValue)variables[0]) == formula.ParallelIsConstant((VariableArgumentValue)variables[0],5);

                List<ArgumentValue> lista, listb;
                lista = formula.GetAllVariables(false);
                listb = formula.ParallelGetAllVariables(false, 5);
                if (lista.Count == listb.Count)
                {
                    for (int index = 0; index < lista.Count; index++)
                    {
                        succes &= lista[index] == listb[index];
                    }
                }
                else
                {
                    succes = false;
                }
            }
            return succes;
        }

        private void Build()
        {
            Totest = BuildListSmall();
            identical = BuildListSmall();
        }

        private List<SyntaxBlock> RandomBigFormulas(int numOfFormulas)
        {
            Random random = new Random();
            
            List<SyntaxBlock> list = new List<SyntaxBlock>();

            for (int i = 0; i < numOfFormulas; i++)
            {
                list.Add(BuildRandomSyntax(random, 12));
            }
            return list;
        }

        private SyntaxBlock BuildRandomSyntax(Random random, int depth)
        {
            if (random.Next() % 10 == 0 || depth == 0)
                return new Sum(Variables[random.Next() % Variables.Length](random.Next()), new Variable(new VariableArgumentValue("V'")));
            return Builders[random.Next() % Builders.Length](BuildRandomSyntax(random, depth-1), BuildRandomSyntax(random, depth-1));
        }

        private List<SyntaxBlock> BuildListSmall()
        {
            var list = new List<SyntaxBlock>();
            list.Add(new Sum(Zero, One));
            list.Add(new Product(One, One));
            list.Add(new Quotient(One, One));
            list.Add(new NumericConstant(2));
            list.Add(new Variable(A));
            list.Add(new VariableConstant(B));
            return list;
        }

        private bool EquivalentyTest()
        {
            bool succes = true;
            for (int x = 0; x < Totest.Count; x++)
            {
                var i = Totest[x];
                var j = identical[x];
                succes &= i.Equals(i);
                succes &= i.Equals(i) && i.ParallelEquals(i, 1);
                succes &= i.Equals(j);
            }
            return succes;
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
                    i.Equals(i);

                    i.ParallelCalculate(1);
                    i.ParallelDerivative(A, 1);
                    i.ParallelGetAllVariables(false, 1);
                    i.ParallelIsConstant(A, 1);
                    i.ParallelPrint(1);
                    i.ParallelSimplify(1);
                    i.ParallelEquals(i, 1);
                }
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
    }
}
