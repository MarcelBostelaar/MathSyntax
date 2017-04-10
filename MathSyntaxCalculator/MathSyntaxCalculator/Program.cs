using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathSyntax;

namespace MathSyntaxCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var A = new Variable("A");
            var B = new VariableConstant("B");
            var C = new NumericConstant(420);
            SyntaxBlock SomeSum = new Sum(A, new Sum(B,C));
            var dict = new Dictionary<long, bool>();
            //dict.Add(A.ID, true);
            Console.WriteLine(SomeSum.IsConstant(dict));
            var list = SomeSum.GetAllVariables();
            foreach(var i in list)
            {
                Console.Write(i.Name + ", ");
            }
            Console.WriteLine("");
            Console.WriteLine(SomeSum.print());           
        }
    }
}
