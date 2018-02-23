using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public abstract class Parser
    {
        public ISymbolTableStack m_SymbolTableStack { get; set; }
        public Scanner m_Scanner { get; set; }

        public Parser(Scanner scanner)
        {
            m_Scanner = scanner;
            m_SymbolTableStack = new SymbolTableStack();

        }

        /// <summary>
        /// Parse a source program and generate the intermediate code and the symbol table.
        /// To be implemented by a language-specific parser subclass.
        /// </summary>
        public abstract void Parse();


        public Token CurrentToken() => m_Scanner.m_CurrentToken;
        public Token NextToken() => m_Scanner.NextToken();
    }
}
