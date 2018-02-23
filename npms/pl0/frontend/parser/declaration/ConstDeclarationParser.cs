using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class ConstDeclarationParser : PascalParser
    {
        public ConstDeclarationParser(PascalParser parent)
            : base(parent)
        {

        }

        public override void Parse()
        {
            try
            {
                ConsumeCurrentToken(); //skip const
                OnParse();

                for (; ; )
                {

                    var token = CurrentToken();
                    if (token.m_Text == ",")
                    {
                        ConsumeCurrentToken();//skip ,
                        OnParse();
                    }
                    else if (token.m_Text == ";")
                    {
                        ConsumeCurrentToken();//skip ;
                        break;
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

        public void OnParse()
        {
            //
            var local = m_SymbolTableStack.GetLocalSymbolTable();

            //
            var token = CurrentToken();
            if ((token as PascalToken).GetType() != PascalTokenType.Identifier)
            {
                //TODO: error
                ErrorReporter.RecordError(18, m_Scanner.m_CurrentToken.m_LineNumber);
                throw new Exception();
            }

            var result = m_SymbolTableStack.LookupLocal(token.m_Text);
            if (result != null)
            {
                //TODO: error
                ErrorReporter.RecordError(19, m_Scanner.m_CurrentToken.m_LineNumber);
                throw new Exception();
            }

            // if not in table
            local.Enter(token.m_Text);
            var item = local.Lookup(token.m_Text) as SymbolTableEntry;

            // skip =
            token = NextToken();
            if (token.m_Text != "=")
            {
                //TODO: error
                ErrorReporter.RecordError(20, m_Scanner.m_CurrentToken.m_LineNumber);
                throw new Exception();
            }

            token = NextToken();
            if ((token as PascalToken).GetType() != PascalTokenType.Unsigned)
            {
                //TODO: error
                ErrorReporter.RecordError(21, m_Scanner.m_CurrentToken.m_LineNumber);
                throw new Exception();
            }
            item.SetAttribute(SymbolTableAttributeKeyType.ConstantValue, token.m_Value);
            item.SetAttribute(SymbolTableAttributeKeyType.Type, SymbolTableEntryType.Constant);

            ConsumeCurrentToken();// skip const value

        }
    }
}
