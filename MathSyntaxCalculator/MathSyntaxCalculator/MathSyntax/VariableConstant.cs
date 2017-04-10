using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class VariableConstant : ArgumentValue
    {
        public VariableConstant(string Name) : base (true, Name)
        {
        }
        public override bool IsConstant(Dictionary<long, bool> TemporaryConstant)
        {
            return true;
        }

        public override string print()
        {
            return Name;
        }
        public override List<ArgumentValue> GetAllVariables()
        {
            var i = new List<ArgumentValue>();
            i.Add(this);
            return i;
        }
    }
}
