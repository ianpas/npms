using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public interface ISymbolTableAttributeKey
    {
        /// <summary>
        /// Gets the type of the attribute key.
        /// </summary>
        /// <returns></returns>
        object GetAttributeKeyType();
    }
}
