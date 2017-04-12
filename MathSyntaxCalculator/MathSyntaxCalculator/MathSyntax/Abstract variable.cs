using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    abstract class Abstract_variable : SyntaxBlock
    {
        protected ArgumentValue Argument;
        bool isAlwaysConstant;

        public abstract SyntaxBlock Derivative(VariableArgumentValue ArgumentToDerive);

        protected Abstract_variable(ArgumentValue Argument, bool alwaysConstant)
        {
            this.isAlwaysConstant = alwaysConstant;
            this.Argument = Argument;
        }
        public abstract List<ArgumentValue> GetAllVariables(bool OnlyNonConstants);

        public abstract bool IsConstant(VariableArgumentValue Non_Constant);

        public SyntaxBlock Simplify()
        {
            return this;
        }

        public string print()
        {
            return Argument.Name;
        }

        public double Calculate()
        {
            return Argument.Value;
        }
        
        public string ParallelPrint(int Depth)
        {
            return print();
        }
        
        public SyntaxBlock ParallelSimplify(int Depth)
        {
            return Simplify();
        }

        public double ParallelCalculate(int Depth)
        {
            return Calculate();
        }

        public bool ParallelIsConstant(VariableArgumentValue Non_Constant, int Depth)
        {
            return IsConstant(Non_Constant);
        }

        public List<ArgumentValue> ParallelGetAllVariables(bool OnlyNonConstants, int Depth)
        {
            return GetAllVariables(OnlyNonConstants);
        }

        public SyntaxBlock ParallelDerivative(VariableArgumentValue ArgumentToDerive, int Depth)
        {
            return Derivative(ArgumentToDerive);
        }
    }
}
