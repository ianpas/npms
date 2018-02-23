using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class ReadParser : StatementParser
    {
        public ReadParser(PascalParser parent)
            : base(parent)
        {

        }

        public override void Parse()
        {
            ConsumeCurrentToken();//skip read

            //
            var token = CurrentToken();
            if (token.m_Text != "(")
            {
                //TODO: error
                ErrorReporter.RecordError(1, m_Scanner.m_CurrentToken.m_LineNumber);
                return;
            }
            ConsumeCurrentToken();//skip (

            for (; ; )
            {

                token = CurrentToken();

                if (token.m_Text == ";")
                {
                    //TODO: error
                    ErrorReporter.RecordError(5, m_Scanner.m_CurrentToken.m_LineNumber);
                    return;
                }
                else if (token.m_Text == ")")
                {
                    ConsumeCurrentToken();//skip )
                    break;
                }
                else if ((token as PascalToken).GetType() != PascalTokenType.Identifier)
                {

                    //TODO: error
                    ErrorReporter.RecordError(3, m_Scanner.m_CurrentToken.m_LineNumber);
                    return;
                }

                if (OnParse() != true)
                {
                    return;
                }

                token = CurrentToken();
                if (token.m_Text == ",")
                {
                    ConsumeCurrentToken();//skip ,

                    if (OnParse() != true)
                    {
                        return;
                    }
                }
                else if (token.m_Text == ")")
                {
                    ConsumeCurrentToken();//skip )
                    return;
                }
                else
                {
                    //TODO: error
                    ErrorReporter.RecordError(0, m_Scanner.m_CurrentToken.m_LineNumber);
                    return;
                }
            }

        }

        private bool OnParse()
        {
            var token = CurrentToken();


            //
            var result = m_SymbolTableStack.Lookup(token.m_Text);
            if (result == null)
            {
                //TODO: error
                ErrorReporter.RecordError(4, m_Scanner.m_CurrentToken.m_LineNumber);
                return false;
            }

            var type = (result as SymbolTableEntry).GetAttribute(SymbolTableAttributeKeyType.Type);
            switch (type)
            {
                case SymbolTableEntryType.Variable:
                    var current_nesting_level = m_SymbolTableStack.GetCurrentNestingLevel();
                    var reference_nesting_level = result.GetSymbolTable().GetNestingLevel();
                    int offset = (int)(result as SymbolTableEntry).GetAttribute(SymbolTableAttributeKeyType.Offset);
                    CodeGenerator.Emit(Operation.Read);
                    CodeGenerator.Emit(Operation.Store, current_nesting_level - reference_nesting_level, offset);
                    break;

                default:
                    //TODO: error
                    ErrorReporter.RecordError(2, m_Scanner.m_CurrentToken.m_LineNumber);
                    return false;
            }

            ConsumeCurrentToken();//skip current variable
            return true;

        }
    }
}
