using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    class Product : SyntaxBlock
    {
        SyntaxBlock A, B;
        public Product(SyntaxBlock A, SyntaxBlock B)
        {
            this.A = A;
            this.B = B;
        }

        public List<ArgumentValue> GetAllVariables()
        {
            var lista = A.GetAllVariables();
            lista.AddRange(B.GetAllVariables());
            return lista;
        }

        public bool IsConstant(Dictionary<long, bool> TemporaryConstant)
        {
            if (A.IsConstant(TemporaryConstant) && B.IsConstant(TemporaryConstant))
                return true;
            return false;
        }

        public string print()
        {
            return ("(" + A.print() + " * " + B.print() + " )");
        }
    }
}
