using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    /// <summary>
    /// Unsigned integer.
    /// </summary>
    /// <seealso cref="npms.PascalToken" />
    class PascalNumberToken : PascalToken
    {
        public PascalNumberToken(Source source)
            : base(source)
        {
        }

        protected override void Extract()
        {
            m_Type = PascalTokenType.Unsigned;

            char current = CurrentChar();

            while (Char.IsDigit(current))
            {
                m_Text += current.ToString();
                current = NextChar();
            }

            m_Value = Convert.ToInt32(m_Text);
        }
    }
}
