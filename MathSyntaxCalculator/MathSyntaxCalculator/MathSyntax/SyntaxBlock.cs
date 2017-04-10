using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    public interface SyntaxBlock : ICloneable
    {
        bool IsConstant(Dictionary<long, bool> TemporaryConstant);
        string print();
        List<ArgumentValue> GetAllVariables();
        SyntaxBlock Derivative(Dictionary<long, bool> TemporaryConstant);
    }
}
