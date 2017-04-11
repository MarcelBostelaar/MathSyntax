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
            /*
            Currently implemented:
                Sums
                Products
                Quotients
                Numeric constants
                Variable constants (constants that can be changed)
                Variables
            
            Functionality:
                (Partial) derivatives
                Calculations
                Printing of formulas
            
            Notes:
                Simplification does not yet do anything beyond removing the following:
                    A * 0 -> 0
                    A * 1 -> A
                    A + 0 -> A
                    A / 1 -> A
                    0 / A -> 0
                    Number / Number -> Number
                    Number + Number -> Number
                    Number * Number -> Number
                
            */

            var A = new VariableArgumentValue("A");//create arguments with values, IE variables. The values of the variables can be changed using "ConstantArgumentValue" and "VariableArgumentValue".

            var B = new ConstantArgumentValue("B");//A variable constant. Can only be used to create constants in the syntax.

            var C = new VariableArgumentValue("C");//A true variable. Can only be used to create variables in the syntax.
            var D = new VariableArgumentValue("D");
            
            A.Value = 10;//change their value, their value type is a double.
            B.Value = 13;
            C.Value = 14.67;
            D.Value = -3;

            SyntaxBlock SomeSum = new Sum(new Variable(A), new Sum(new VariableConstant(B),new NumericConstant(420))); //create a formula.
            Console.WriteLine(SomeSum.print()); //print the forumula
            Console.WriteLine(SomeSum.Calculate()); //Calculate the results of a formula
            
            Console.WriteLine("\nCalculating partial derivatives");


            SyntaxBlock functionA, functionB;
            functionA = new Product(new NumericConstant(44), new Product(new Variable(A), new Variable(C)));
            functionB = new Product(new VariableConstant(B), new Product(new Variable(A), new Variable(D)));

            SyntaxBlock ComplexSum = new Sum(functionA, functionB); //functions of functions
            SyntaxBlock SuperComplexProduct = new Product(ComplexSum, ComplexSum); //functions of functions of functions
            SyntaxBlock SuperSuperComplexProduct = new Product(SuperComplexProduct, SuperComplexProduct); //functions^4

            List<Tuple<VariableArgumentValue, SyntaxBlock>> derivatives = Derivatives.CalculatePartialDerivatives(SuperSuperComplexProduct); //Calculate all the partial derivatives of a given function.
            //A Function to calculate the derivative for a simple (single argument) function is also present, but will throw an exception if the formula given has more than one variable.
            //swap out "SuperSuperComplexProduct" for any other syntaxblock to see the result.
            foreach(var i in derivatives)
            {
                Console.WriteLine(i.Item1.Name + " : " + i.Item2.print()); //print the resulting derivatives
                Console.WriteLine();
            }

            double CalculationResult = derivatives[0].Item2.Calculate(); //calculate the result of a derivative function.
            Console.WriteLine("Calculation result: " + CalculationResult);



            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
