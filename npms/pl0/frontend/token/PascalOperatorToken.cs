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
    public class PascalOperatorToken : PascalToken
    {
        public PascalOperatorToken(Source source)
            : base(source)
        {

        }

        protected override void Extract()
        {
            m_Value = null;


            char current = CurrentChar();
            char next = PeekChar();
            string text = current.ToString() + next.ToString();

            if (TokenUtility.BinaryOperatorTable.Contains(text))
            {
                m_Text = text;
                m_Type = TokenUtility.SpecialSymbolsMap[m_Text];

                // consume current char and next char
                ConsumeCurrent();
                ConsumeCurrent();
                return;
            }
            else if (current == ':')
            {
                m_Type = PascalTokenType.Error;
                m_Text = text;
            }
            else
            {
                m_Text = current.ToString();
                m_Type = TokenUtility.SpecialSymbolsMap[m_Text];
            }

            ConsumeCurrent();

        }
    }
}
