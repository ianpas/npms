using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class WhileStatementParser : StatementParser
    {
        public WhileStatementParser(PascalParser parent)
            : base(parent)
        {

        }
        public override void Parse()
        {
            ConsumeCurrentToken();//skip while

            int label_true = CodeGenerator.m_Code.Count;

            var condition_parser = new ConditionParser(this);
            condition_parser.Parse();

            if (condition_parser.IsValid == false)
            {
                return;
            }
            var token = CurrentToken();
            if (token.m_Text != "do")
            {
                //TODO: error
            }
            ConsumeCurrentToken();//skip do

            //
            int label_condition_jump = CodeGenerator.m_Code.Count;
            CodeGenerator.Emit(Operation.ConditionalJump, 0, 0);//default 0

            //
            var statement_parser = new StatementParser(this);
            statement_parser.Parse();

            //

            //
            CodeGenerator.Emit(Operation.Jump, 0, label_true);//default 0
            int label_false = CodeGenerator.m_Code.Count;


            CodeGenerator.Refine(label_condition_jump, label_false);
        }
    }
}
