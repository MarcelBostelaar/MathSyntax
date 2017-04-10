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
            this.Power = new NumericConstant(1);
        }
        public Variable(string Name, SyntaxBlock Power) : base(false, Name)
        {
            this.Power = Power;
        }
        public override string print()
        {
            return Name;
        }

        SyntaxBlock Power;

        public override List<ArgumentValue> GetAllVariables()
        {
            var i = new List<ArgumentValue>();
            i.Add(this);
            return i;
        }

        public override bool IsConstant(Dictionary<long, bool> TemporaryConstant)
        {
            if (Power.IsConstant(TemporaryConstant))
            {
                try
                {
                    if (TemporaryConstant[ID])
                    {
                        return true;
                    }
                }
                catch { }
            }
            return false;
        }
    }
}
