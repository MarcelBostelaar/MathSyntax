using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    public interface SyntaxBlock
    {
        bool IsConstant(Dictionary<ArgumentValue, bool> TemporaryConstant);
        string print();
        List<ArgumentValue> GetAllVariables();
        SyntaxBlock Derivative(Dictionary<ArgumentValue, bool> TemporaryConstant);
    }
}
