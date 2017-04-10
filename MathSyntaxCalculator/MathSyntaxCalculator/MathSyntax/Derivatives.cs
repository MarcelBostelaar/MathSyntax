using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    public static class Derivatives
    {
        public static List<Tuple<ArgumentValue, SyntaxBlock>> CalculatePartialDerivatives(SyntaxBlock Formula)
        {
            var AllVariableVariables = Formula.GetAllVariables(true);
            var distinctVariables = AllVariableVariables.Distinct();
            List<Tuple<ArgumentValue, SyntaxBlock>> PartialDerivatives = new List<Tuple<ArgumentValue, SyntaxBlock>>();

            foreach (ArgumentValue i in distinctVariables) {
                PartialDerivatives.Add(new Tuple<ArgumentValue, SyntaxBlock>(i, Formula.Derivative(i).Simplify()));
            }
            return PartialDerivatives;
        }

        public static SyntaxBlock CalculateDerivative(SyntaxBlock Formula)
        {
            var listOfPartials = CalculatePartialDerivatives(Formula);
            if(listOfPartials.Count > 1)
            {
                throw new DerivativeException("Formula has more than one variable");
            }
            if (listOfPartials.Count < 1)
            {
                throw new DerivativeException("Formula has no derivatives");
            }
            return listOfPartials[0].Item2;
        }
    }
}
