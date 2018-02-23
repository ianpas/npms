using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class PascalErrorToken : PascalToken
    {
        public PascalErrorToken(Source source)
            : base(source)
        {
        }
        protected override void Extract()
        {
            m_Type = PascalTokenType.Error;
            m_Text = null;
            m_Value = null;
        }
    }
}
