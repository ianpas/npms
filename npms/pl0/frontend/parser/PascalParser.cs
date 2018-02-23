using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class PascalParser : Parser
    {
        public PascalParser(PascalScanner scanner)
            : base(scanner)
        {
        }

        /// <summary>
        /// Constructor for subclass.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public PascalParser(PascalParser parent)
            : base(parent.m_Scanner)
        {
            m_SymbolTableStack = parent.m_SymbolTableStack;
        }

        public void ConsumeCurrentToken()
        {
            NextToken();
        }

        //
        public override void Parse()
        {
            var parser = new ProgramParser(this);
            parser.Parse();

            //
            var token = CurrentToken();
            if (token.m_Text != ".")
            {
                //TODO error:
                Debug.Write("Missing .");
            }

          

            //try
            //{
            //    while (!((token = NextToken()) is EofToken))
            //    {
            //        var type = (PascalTokenType)token.m_Type;
            //        if (type == PascalTokenType.Identifier)
            //        {
            //            string entry_name = token.m_Text;
            //            var entry = (m_SymbolTableStack as ISymbolTableStack).LookupLocal(entry_name);
            //            if (entry == null)
            //            {
            //                entry = (m_SymbolTableStack as ISymbolTableStack).EnterLocalSymbolTable(entry_name);
            //            }
            //            entry.AppendLineNumber(token.m_LineNumber);
            //        }
            //        else if (type == PascalTokenType.Error)
            //        {
            //            Console.WriteLine("Error token found during parsing.");
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    Console.WriteLine("Error during parsing.");
            //}
        }
    }
}
