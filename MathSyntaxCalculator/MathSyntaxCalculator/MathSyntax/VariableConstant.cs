using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class VariableConstant2 : SyntaxBlock
    {
        ArgumentValue Argument;
        bool isConstant;
        public VariableConstant2(ArgumentValue Argument)
        {
            this.Argument = Argument;
            isConstant = true;
        }

        protected VariableConstant2(ArgumentValue Argument, bool IsConstant)
        {
            this.Argument = Argument;
            isConstant = IsConstant;
        }

        public SyntaxBlock Derivative(Dictionary<ArgumentValue, bool> TemporaryConstant)
        {
            throw new NotImplementedException();
        }

        public List<ArgumentValue> GetAllVariables()
        {
            var list = new List<ArgumentValue>();
            list.Add(Argument);
            return list;
        }

        public bool IsConstant(Dictionary<ArgumentValue, bool> TemporaryConstant)
        {
            if (isConstant)
            {
                return true;
            }
            try
            {
                if (TemporaryConstant[Argument])
                {
                    return true;
                }
            }
            catch { }
            return false;
        }

        public string print()
        {
            return Argument.Name;
        }
    }

    class Variable2 : VariableConstant2
    {
        public Variable2(ArgumentValue Argument) : base(Argument, false) { }
    }

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

        public override SyntaxBlock Derivative(Dictionary<long, bool> TemporaryConstant)
        {
            return new NumericConstant(0);
        }
    }
}
