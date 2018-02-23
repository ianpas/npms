using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class RepeatStatementParser : StatementParser
    {
        public RepeatStatementParser(PascalParser parent)
            : base(parent)
        {

        }

        public override void Parse()
        {
            try
            {
                ConsumeCurrentToken();//skip repeat

                int label_JPC = CodeGenerator.m_Code.Count;

                OnParse();

                for (; ; )
                {

                    var token = CurrentToken();
                    if (token.m_Text == ";")
                    {
                        ConsumeCurrentToken();//skip ;
                        OnParse();
                    }
                    else if (token.m_Text == "until")
                    {
                        ConsumeCurrentToken();//skip until

                        var condition_parser = new ConditionParser(this);
                        condition_parser.Parse();

                        CodeGenerator.Emit(Operation.ConditionalJump, 0, label_JPC);
                        return;
                    }
                    else
                    {
                        //TODO: error
                        ErrorReporter.RecordError(0, m_Scanner.m_CurrentToken.m_LineNumber);
                        throw new Exception();
                    }

                }
            }
            catch (Exception)
            {

            }

           
        }

        private void OnParse()
        {
            var statement_parser = new StatementParser(this);
            statement_parser.Parse();
        }
    }
}
