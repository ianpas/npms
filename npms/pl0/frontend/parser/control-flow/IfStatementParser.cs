using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class IfStatementParser : StatementParser
    {
        public IfStatementParser(PascalParser parent)
            : base(parent)
        {

        }

        public override void Parse()
        {
            // <condition> JPC <then-statement1> JMP label_JPC <else-statement> label_JMP

            try
            {
                ConsumeCurrentToken();//skip if

                var condition_parser = new ConditionParser(this);
                condition_parser.Parse();

                var token = CurrentToken();
                if (token.m_Text != "then")
                {
                    //TODO: error
                    ErrorReporter.RecordError(22, m_Scanner.m_CurrentToken.m_LineNumber);
                    throw new Exception();
                }
                ConsumeCurrentToken();//skip then

                //
                CodeGenerator.Emit(Operation.ConditionalJump, 0, 0);//default 0
                int JPC = CodeGenerator.m_Code.Count - 1;//label1 is the address of JPC 0 ...

                //
                var statement_parser = new StatementParser(this);
                statement_parser.Parse();

                CodeGenerator.Emit(Operation.Jump, 0, 0);//default 0
                int JMP = CodeGenerator.m_Code.Count - 1;//label1 is the address of JMP 0 ...
                int label_JPC = CodeGenerator.m_Code.Count;
                CodeGenerator.Refine(JPC, label_JPC);

                token = CurrentToken();
                if (token.m_Text == "else")
                {
                    ConsumeCurrentToken();//skip then
                    statement_parser = new StatementParser(this);
                    statement_parser.Parse();

                    int label_JMP = CodeGenerator.m_Code.Count;
                    CodeGenerator.Refine(JMP, label_JMP);
                }
                else
                {
                    CodeGenerator.Refine(JMP, label_JPC);
                }
            }
            catch (Exception)
            {

            }
           
        }
    }
}
