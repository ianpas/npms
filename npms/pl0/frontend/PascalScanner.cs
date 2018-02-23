using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class PascalScanner : Scanner
    {
        public PascalScanner(Source source)
            : base(source)
        {

        }

        protected override Token ExtractToken()
        {

            SkipWhiteSpace();

            Token token;
            char current = CurrentChar();

            if (current == Source.EOF)
            {
                token = new EofToken(m_Source);
            }
            else if (Char.IsLetter(current))
            {
                token = new PascalWordToken(m_Source);
            }
            else if (Char.IsDigit(current))
            {
                token = new PascalNumberToken(m_Source);
            }
            else if (TokenUtility.OperatorFirstSymbolTable.Contains(current.ToString()))
            {
                token = new PascalOperatorToken(m_Source);
            }
            else if (TokenUtility.DelimiterTable.Contains(current.ToString()))
            {
                token = new PascalDelimiterToken(m_Source);
            }
            else
            {
                token = new PascalErrorToken(m_Source);
                token.m_Text = current.ToString();
                ConsumeCurrent();
            }

            return token;
        }

        private void SkipWhiteSpace()
        {
            char current = CurrentChar();
            while (Char.IsWhiteSpace(current))
            {
                current = NextChar();
            }
        }

        public void ScanNextLine()
        {
            m_Source.ReadLine();
            m_CurrentToken = NextToken();
        }
    }
}
