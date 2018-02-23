using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public interface ISymbolTableEntry
    {
        /// <summary>
        /// Gets the name of the entry.
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Gets the symbol table containing this entry.
        /// </summary>
        /// <returns></returns>
        ISymbolTable GetSymbolTable();

        /// <summary>
        /// Appends the line number.
        /// </summary>
        /// <param name="line_number">The line number.</param>
        void AppendLineNumber(int line_number);

        /// <summary>
        /// Gets the line numbers.
        /// </summary>
        /// <returns></returns>
        List<int> GetLineNumbers();



        /// <summary>
        /// Sets an attribute of the entry.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void SetAttribute(ISymbolTableAttributeKey key, object value);

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object GetAttribute(ISymbolTableAttributeKey key);
       
    }
}
