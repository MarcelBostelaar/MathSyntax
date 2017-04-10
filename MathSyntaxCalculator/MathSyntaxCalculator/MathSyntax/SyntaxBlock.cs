using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    interface SyntaxBlock
    {
        bool IsConstant(Dictionary<long, bool> TemporaryConstant);
        string print();
        List<ArgumentValue> GetAllVariables();
    }
}
