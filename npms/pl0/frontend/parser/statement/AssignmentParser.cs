using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class AssignmentParser : StatementParser
    {
        public AssignmentParser(PascalParser parent)
            : base(parent)
        {

        }
        public override void Parse()
        {
            var token = CurrentToken();


            var result = m_SymbolTableStack.Lookup(token.m_Text);
            if (result == null)
            {
                //TODO: error
                ErrorReporter.RecordError(12, m_Scanner.m_CurrentToken.m_LineNumber);
                return;
            }
            else
            {
                var type = (SymbolTableEntryType)(result as SymbolTableEntry).GetAttribute(SymbolTableAttributeKeyType.Type);
                if (type != SymbolTableEntryType.Variable)
                {
                    //TODO: error
                    ErrorReporter.RecordError(13, m_Scanner.m_CurrentToken.m_LineNumber);
                    return;
                }
            }

            var current_nesting_level = m_SymbolTableStack.GetCurrentNestingLevel();
            var reference_nesting_level = result.GetSymbolTable().GetNestingLevel();

            int offset = (int)(result as SymbolTableEntry).GetAttribute(SymbolTableAttributeKeyType.Offset);


            token = NextToken();
            if (token.m_Text != ":=")
            {
                //TODO: error
                ErrorReporter.RecordError(11, m_Scanner.m_CurrentToken.m_LineNumber);
                return;
            }

            ConsumeCurrentToken();//skip :=


            var expression_parser = new ExpressionParser(this);
            expression_parser.Parse();


            //token = CurrentToken();
            //if (token.m_Text != ";")
            //{
            //    //TODO: error
            //}

            //ConsumeCurrentToken();

            CodeGenerator.Emit(Operation.Store, current_nesting_level - reference_nesting_level, offset);

        }

    }
}
