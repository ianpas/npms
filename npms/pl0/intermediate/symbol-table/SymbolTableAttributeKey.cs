using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public enum SymbolTableEntryType
    {
        Variable, Constant,
        Procedure
    }
    public enum SymbolTableAttributeKeyType
    {
        ConstantValue, Offset, Type, Address
    }
}
