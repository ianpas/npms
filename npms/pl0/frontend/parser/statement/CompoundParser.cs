using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class CompoundParser : StatementParser
    {
        public CompoundParser(PascalParser parent)
            : base(parent)
        {

        }
        public override void Parse()
        {
            ConsumeCurrentToken(); //skip begin 
            OnParse();

            for (; ; )
            {
                var token = CurrentToken();
                if (token.m_Text == ";")
                {
                    ConsumeCurrentToken();//skip ;
                    OnParse();
                }
                else if (token.m_Text == "end")
                {
                    ConsumeCurrentToken();//skip end
                    break;
                }
                else if ((PascalTokenType)token.m_Type == PascalTokenType.Eof)
                {
                    break;
                }
                else 
                {
                    OnParse();
                }
               
                //else if (!FollowCompound.Contains(token.m_Text))
                //{
                //    //TODO: error
                //    ErrorReporter.RecordError(0, m_Scanner.m_CurrentToken.m_LineNumber, ",possibility: missing ;");
                //}


            }

        }

        private void OnParse()
        {
            var statement_parser = new StatementParser(this);
            statement_parser.Parse();
        }

        public static List<string> FollowCompound = new List<string>()
        {
           ".",";"
        };
    }
}
