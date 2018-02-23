using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{



    public class CodeGenerator
    {
        public static void Emit(Operation op)
        {
            Emit(op, 0, m_OpCode[op]);
        }

        public static void Refine(int index, int argument)
        {
            var code = Tuple.Create(m_Code[index].Item1, m_Code[index].Item2, argument.ToString());
            m_Code[index] = code;
        }

        public static void Emit(Operation op, int level_difference, int argument)
        {
            switch (op)
            {
                case Operation.Allocate:
                    argument += 3;
                    break;

                default:
                    break;
            }

            var code = Tuple.Create(op.GetDescription(), level_difference.ToString(), argument.ToString());
            m_Code.Add(code);
        }

        public static List<Tuple<string, string, string>> m_Code = new List<Tuple<string, string, string>>();

        public static Dictionary<Operation, int> m_OpCode = new Dictionary<Operation, int>()
        {
           {Operation.Return,           0 },

           {Operation.Negate,           1 },

           {Operation.Add,              2 },
           {Operation.Subtract,         3 },
           {Operation.Multiply,         4 },
           {Operation.Divide,           5 },
           {Operation.Odd,              6 },

           {Operation.Equals,           8},
           {Operation.NotEquals,        9},
           {Operation.Less,             10},
           {Operation.LessEquals,       13},
           {Operation.Greater,          12},
           {Operation.GreaterEquals,    11},

           {Operation.Write,            14},
           {Operation.Read,             16}

        };
    }
}
