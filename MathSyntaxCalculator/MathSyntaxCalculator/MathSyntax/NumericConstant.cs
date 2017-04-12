using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class NumericConstant : SyntaxBlock
    {
        /// <summary>
        /// Creates a numeric constant. Value is unchangable once created.
        /// </summary>
        /// <param name="Value">The value for the constant.</param>
        public NumericConstant(double Value)
        {
            value = Value;
        }
        public double value { get; private set; }
        
        public string print()
        {
            return value.ToString();
        }

        public List<ArgumentValue> GetAllVariables(bool OnlyNonConstants)
        {
            return new List<ArgumentValue>();
        }

        public bool IsConstant(VariableArgumentValue Non_Constant)
        {
            return true;
        }

        public SyntaxBlock Derivative(VariableArgumentValue ArgumentToDerive)
        {
            return new NumericConstant(0);
        }

        public SyntaxBlock Simplify()
        {
            return this;
        }

        public double Calculate()
        {
            return value;
        }

        public bool ParallelIsConstant(VariableArgumentValue Non_Constant, int Depth)
        {
            return IsConstant(Non_Constant);
        }

        public string ParallelPrint(int Depth)
        {
            return print();
        }

        public List<ArgumentValue> ParallelGetAllVariables(bool OnlyNonConstants, int Depth)
        {
            return GetAllVariables(OnlyNonConstants);
        }

        public SyntaxBlock ParallelDerivative(VariableArgumentValue ArgumentToDerive, int Depth)
        {
            return Derivative(ArgumentToDerive);
        }

        public SyntaxBlock ParallelSimplify(int Depth)
        {
            return Simplify();
        }

        public double ParallelCalculate(int Depth)
        {
            return Calculate();
        }
    }
}
