using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace npms
{
    /// <summary>
    /// Special symbols are Operator, DoubleOperator and Delimiter.
    /// </summary>
    /// <seealso cref="npms.PascalToken" />
    public class PascalWordToken : PascalToken
    {
        public PascalWordToken(Source source)
            : base(source)
        {

        }

        protected override void Extract()
        {
            char current = CurrentChar();

            while (Char.IsLetterOrDigit(current))
            {
                m_Text += current.ToString();
                current = NextChar();
            }

            if (TokenUtility.ReservedWordTable.Contains(m_Text))
            {
                m_Type = TokenUtility.ReservedWordMap[m_Text];
            }
            else
            {
                m_Type = PascalTokenType.Identifier;
            }

            m_Value = null;
        }
    }
}
