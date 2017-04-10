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
            var AllVariables = Formula.GetAllVariables();
            var NonConstants = new List<ArgumentValue>();
            foreach(var i in AllVariables)
            {
                if (!i.Constant)
                {
                    NonConstants.Add(i);
                }
            }



            throw new NotImplementedException();
        }
    }
}
