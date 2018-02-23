using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public abstract class PascalToken : Token
    {
        public PascalToken(Source source)
            : base(source)
        {

        }

        public new PascalTokenType GetType()
        {
            return (PascalTokenType)m_Type;
        }

    }
}
