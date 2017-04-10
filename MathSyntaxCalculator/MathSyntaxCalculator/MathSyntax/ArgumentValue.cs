using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSyntax
{
    abstract class ArgumentValue : SyntaxBlock
    {
        public ArgumentValue(bool Constant, string Name)
        {
            this.Constant = Constant;
            this.ID = Counter;
            Counter++;
            this.Name = Name;
            Value = 0;
        }
        public long ID { get; private set; }
        public string Name { get; private set; }
        public float Value { get; set; }
        public bool Constant { get; private set; }
        
        public abstract string print();
        public abstract List<ArgumentValue> GetAllVariables();
        public abstract bool IsConstant(Dictionary<long, bool> TemporaryConstant);

        private static long Counter = 0;
    }
}
