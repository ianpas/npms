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
    public class PascalDelimiterToken : PascalToken
    {
        public PascalDelimiterToken(Source source)
            : base(source)
        {

        }

        protected override void Extract()
        {
            m_Type = PascalTokenType.Delimiter;

            m_Text = CurrentChar().ToString();
            if (m_Text == "(")
            {
                m_Type = PascalTokenType.LeftParenthesis;
            }

            ConsumeCurrent();

            m_Value = null;
        }
    }
}
