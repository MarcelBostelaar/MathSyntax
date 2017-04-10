using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    public interface SyntaxBlock
    {
        bool IsConstant(ArgumentValue Non_Constant);
        string print();
        List<ArgumentValue> GetAllVariables(bool OnlyNonConstants);
        SyntaxBlock Derivative(ArgumentValue ArgumentToDerive);
        /// <summary>
        /// Simplifies the formula
        /// </summary>
        /// <returns>Returns the value with which replace itself</returns>
        SyntaxBlock Simplify();
    }
}
