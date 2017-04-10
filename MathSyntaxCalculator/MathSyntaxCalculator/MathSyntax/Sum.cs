using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Sum : SyntaxBlock
    {
        public Sum(SyntaxBlock A, SyntaxBlock B)
        {
            this.A = A;
            this.B = B;
        }

        SyntaxBlock A, B;

        public string print()
        {
            return ("(" + A.print() + " + " + B.print() + ")");
        }

        public List<ArgumentValue> GetAllVariables()
        {
            List<ArgumentValue> listA, listB;
            listA = A.GetAllVariables();
            listB = B.GetAllVariables();
            listA.AddRange(listB);
            return listA;
        }

        public bool IsConstant(Dictionary<long, bool> TemporaryConstant)
        {
            if (A.IsConstant(TemporaryConstant) && B.IsConstant(TemporaryConstant))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
