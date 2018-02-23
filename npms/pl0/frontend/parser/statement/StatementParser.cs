using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class StatementParser : PascalParser
    {
        public StatementParser(PascalParser parent)
            : base(parent)
        {

        }

        public override void Parse()
        {
            try
            {
                var token = CurrentToken();
                switch (token.m_Type)
                {
                    case PascalTokenType.Begin:
                        var compound_parser = new CompoundParser(this);
                        compound_parser.Parse();
                        break;

                    case PascalTokenType.Identifier:
                        var assignment_parser = new AssignmentParser(this);
                        assignment_parser.Parse();
                        break;

                    case PascalTokenType.If:
                        var if_parser = new IfStatementParser(this);
                        if_parser.Parse();
                        break;

                    case PascalTokenType.Call:
                        var procedure_parser = new ProcedureParser(this);
                        procedure_parser.Parse();
                        break;

                    case PascalTokenType.While:
                        var while_parser = new WhileStatementParser(this);
                        while_parser.Parse();
                        break;

                    case PascalTokenType.Read:
                        var read_parser = new ReadParser(this);
                        read_parser.Parse();
                        break;

                    case PascalTokenType.Write:
                        var write_parser = new WriteParser(this);
                        write_parser.Parse();
                        break;

                    case PascalTokenType.Repeat:
                        var repeat_parser = new RepeatStatementParser(this);
                        repeat_parser.Parse();
                        break;

                    //case PascalTokenType.Semicolon:
                    //    ConsumeCurrentToken();
                    //    break;

                    default:
                        if (!FollowStatement.Contains(token.m_Text))
                        {
                            //TODO: error
                            ErrorReporter.RecordError(0, m_Scanner.m_CurrentToken.m_LineNumber, ",possibility: missing ;");
                            throw new Exception();
                        }
                        break;

                }
            }
            catch (Exception)
            {
            }

        }

        public static List<string> FollowStatement = new List<string>()
        {
           ".","else",";","end","until"
        };

    }
}
