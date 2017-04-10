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

        public SyntaxBlock Derivative(Dictionary<long, bool> TemporaryConstant)
        {
            bool Aisconstant, Bisconstant;
            Aisconstant = A.IsConstant(TemporaryConstant);
            Bisconstant = B.IsConstant(TemporaryConstant);
            if(!Aisconstant && !Bisconstant)
            {
                throw new DerivativeException("Cant derive derivative of mutli-variable products");
            }
            if (Aisconstant && Bisconstant)
            {
                return new NumericConstant(0);
            }
            if (Aisconstant)
            {
                return new Product(A, B.Derivative(TemporaryConstant));
            }
            else
            {
                return new Product(A.Derivative(TemporaryConstant), B);
            }
            throw new NotImplementedException();
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
