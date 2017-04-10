using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Variable : ArgumentValue
    {
        public Variable(string Name) : base(false, Name)
        {
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

        public override bool IsConstant(Dictionary<long, bool> TemporaryConstant)
        {
            try {
                if (TemporaryConstant[ID]) {
                    return true; }
            }catch{}
            return false;
        }
    }
}
