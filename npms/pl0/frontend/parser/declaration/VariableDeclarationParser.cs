﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class VariableDeclarationParser : PascalParser
    {
        public VariableDeclarationParser(PascalParser parent)
            : base(parent)
        {

        }

        public override void Parse()
        {
            try
            {
                ConsumeCurrentToken(); //skip var
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
                ErrorReporter.RecordError(16, m_Scanner.m_CurrentToken.m_LineNumber);
                throw new Exception();
            }

            var result = m_SymbolTableStack.LookupLocal(token.m_Text);
            if (result != null)
            {
                //TODO: error
                ErrorReporter.RecordError(17, m_Scanner.m_CurrentToken.m_LineNumber);
                throw new Exception();
            }

            // if not in table
            local.Enter(token.m_Text);
            var item = local.Lookup(token.m_Text) as SymbolTableEntry;

            item.SetAttribute(SymbolTableAttributeKeyType.Offset, (local as SymbolTable).m_VariableAmount + 3);
            item.SetAttribute(SymbolTableAttributeKeyType.Type, SymbolTableEntryType.Variable);
            ++(local as SymbolTable).m_VariableAmount;

            ConsumeCurrentToken();//skip to next

        }
    }
}
