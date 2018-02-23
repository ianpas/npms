using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class ConditionParser : PascalParser
    {
        public ConditionParser(PascalParser parent)
            : base(parent)
        {

        }
        public override void Parse()
        {
            var token = CurrentToken();

            if (token.m_Text == "odd")
            {
                try
                {
                    ParseOdd();
                }
                catch (Exception)
                {
                }

                return;
            }

            // parse one expression
            var expression1_parser = new ExpressionParser(this);
            expression1_parser.Parse();

            // parse operator
            token = CurrentToken();
            Operation op = Operation.Null;

            try
            {
                op = m_TokenOperationMap[(PascalTokenType)token.m_Type];
            }
            catch (Exception)
            {
                //TODO: error
                ErrorReporter.RecordError(10, m_Scanner.m_CurrentToken.m_LineNumber);
                IsValid = false;
                return;
            }

            ConsumeCurrentToken();//skip operator

            // parse another expression
            var expression2_parser = new ExpressionParser(this);
            expression2_parser.Parse();

            //
            CodeGenerator.Emit(op);
        }

        private void ParseOdd()
        {
            var token = CurrentToken();
            var op = m_TokenOperationMap[(PascalTokenType)token.m_Type];

            ConsumeCurrentToken();//skip odd

            // parse one expression
            var expression_parser = new ExpressionParser(this);
            expression_parser.Parse();

            CodeGenerator.Emit(op);

        }

        public bool IsValid = true;
        public static Dictionary<PascalTokenType, Operation> m_TokenOperationMap = new Dictionary<PascalTokenType, Operation>()
        {
           {PascalTokenType.Equals,         Operation.Equals},
           {PascalTokenType.NotEquals,      Operation.NotEquals},
           {PascalTokenType.Less,           Operation.Less},
           {PascalTokenType.LessEquals,     Operation.LessEquals},
           {PascalTokenType.Greater,        Operation.Greater},
           {PascalTokenType.GreaterEquals,  Operation.GreaterEquals},
           {PascalTokenType.Odd,            Operation.Odd},
        };
    }
}
