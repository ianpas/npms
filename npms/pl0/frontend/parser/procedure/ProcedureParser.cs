using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class ProcedureParser : StatementParser
    {
        public ProcedureParser(PascalParser parent)
            : base(parent)
        {

        }

        public override void Parse()
        {
            ConsumeCurrentToken(); //skip call 

            var token = CurrentToken();
            if ((PascalTokenType)token.m_Type != PascalTokenType.Identifier)
            {
                //TODO: error
                ErrorReporter.RecordError(6, m_Scanner.m_CurrentToken.m_LineNumber);
                return;
            }

            var result = m_SymbolTableStack.Lookup(token.m_Text);
            //TODO: if type is proceduer? what happen if identifier name is the same as procedure?
            if (result == null)
            {
                //TODO: error
                ErrorReporter.RecordError(7, m_Scanner.m_CurrentToken.m_LineNumber);
                return;
            }
            else
            {
                var type = (SymbolTableEntryType)(result as SymbolTableEntry).GetAttribute(SymbolTableAttributeKeyType.Type);
                if (type != SymbolTableEntryType.Procedure)
                {
                    //TODO: error
                    ErrorReporter.RecordError(8, m_Scanner.m_CurrentToken.m_LineNumber);
                    return;
                }
            }

            var current_nesting_level = m_SymbolTableStack.GetCurrentNestingLevel();
            var reference_nesting_level = result.GetSymbolTable().GetNestingLevel();
            int address = (int)(result as SymbolTableEntry).GetAttribute(SymbolTableAttributeKeyType.Address);
            CodeGenerator.Emit(Operation.Call, current_nesting_level - reference_nesting_level, address);

            //
            token = NextToken();//skip proceduer name
            //if (token.m_Text != ";")
            //{
            //    //TODO: error
            //    ErrorReporter.RecordError(9, m_Scanner.m_CurrentToken.m_LineNumber);
            //    return;
            //}
        }
    }
}
