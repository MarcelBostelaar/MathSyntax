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
            var A = new ArgumentValue("A");
            var B = new ArgumentValue("B");
            var C = new ArgumentValue("C");
            var D = new ArgumentValue("D");
            var E = new ArgumentValue("E");
            var F = new ArgumentValue("F");
            var G = new ArgumentValue("G");
            SyntaxBlock SomeSum = new Sum(new Variable(A), new Sum(new VariableConstant(B),new NumericConstant(420)));
            SyntaxBlock functionA, functionB;
            functionA = new Product(new NumericConstant(44), new Product(new Variable(A), new Variable(B)));
            functionB = new Product(new VariableConstant(C), new Product(new Variable(A), new Variable(D)));
            SyntaxBlock ComplexSum = new Sum(functionA, functionB);
            SyntaxBlock SuperComplexProduct = new Product(ComplexSum, ComplexSum);
            SyntaxBlock SuperSuperComplexProduct = new Product(SuperComplexProduct, SuperComplexProduct);
            var derivatives = Derivatives.CalculatePartialDerivatives(SuperSuperComplexProduct);
            foreach(var i in derivatives)
            {
                Console.WriteLine(i.Item1.Name + " : " + i.Item2.print());
            }

            A.Value = 10;
            B.Value = 13;
            C.Value = 55;
            D.Value = -3;
            double CalculationResult = derivatives[0].Item2.Calculate();
            Console.WriteLine(CalculationResult);
            Console.WriteLine(SomeSum.print());
        }
    }
}
