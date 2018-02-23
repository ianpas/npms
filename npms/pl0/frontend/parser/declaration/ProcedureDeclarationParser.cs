using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class ProcedureDeclarationParser : PascalParser
    {
        public ProcedureDeclarationParser(PascalParser parent)
            : base(parent)
        {

        }
        public override void Parse()
        {
            try
            {
                ParseHead();

                //
                (m_SymbolTableStack as SymbolTableStack).Push();

                var program_parser = new ProgramParser(this);
                program_parser.Parse();

                (m_SymbolTableStack as SymbolTableStack).Pop();

                var token = CurrentToken();
                if (token.m_Text != ";")
                {
                    //TODO: error
                    ErrorReporter.RecordError(9, m_Scanner.m_CurrentToken.m_LineNumber);
                    throw new Exception();
                }

                //
                ConsumeCurrentToken();//skip ;
            }
            catch (Exception)
            {

            }
            
            //if ((PascalTokenType)token.m_Type == PascalTokenType.Procedure)
            //{
            //    //
            //    (m_SymbolTableStack as SymbolTableStack).Push();

            //    var proceduer_parser = new ProcedureParser(this);
            //    proceduer_parser.Parse();

            //    //
            //    SymbolTablePrinter.RecordSymbolTable(m_SymbolTableStack.GetLocalSymbolTable());
            //    (m_SymbolTableStack as SymbolTableStack).Pop();
            //}
        }

        private void ParseHead()
        {
            ConsumeCurrentToken(); //skip procedure

            var token = CurrentToken();
            if ((PascalTokenType)token.m_Type != PascalTokenType.Identifier)
            {
                //TODO: error
                ErrorReporter.RecordError(14, m_Scanner.m_CurrentToken.m_LineNumber);
                throw new Exception();
            }

            // enter procedure in symbol table

            // if not in table
            var result = m_SymbolTableStack.LookupLocal(token.m_Text);
            if (result != null)
            {
                //TODO: error
                ErrorReporter.RecordError(15, m_Scanner.m_CurrentToken.m_LineNumber);
                throw new Exception();

            }

            m_SymbolTableStack.EnterLocalSymbolTable(token.m_Text);

            var item = m_SymbolTableStack.LookupLocal(token.m_Text) as SymbolTableEntry;
            item.SetAttribute(SymbolTableAttributeKeyType.Address, CodeGenerator.m_Code.Count + 1);
            item.SetAttribute(SymbolTableAttributeKeyType.Type, SymbolTableEntryType.Procedure);

            token = NextToken();//skip proceduer name
            if (token.m_Text != ";")
            {
                //TODO: error
                ErrorReporter.RecordError(9, m_Scanner.m_CurrentToken.m_LineNumber);
                throw new Exception();
            }


        }
    }
}
