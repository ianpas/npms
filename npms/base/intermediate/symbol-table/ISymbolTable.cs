using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public interface ISymbolTable
    {
        /// <summary>
        /// Gets the nesting level.
        /// </summary>
        /// <returns></returns>
        int GetNestingLevel();

        /// <summary>
        /// Enters a new entry.
        /// </summary>
        /// <param name="entry_name">Name of the entry.</param>
        /// <returns></returns>
        ISymbolTableEntry Enter(string entry_name);

        /// <summary>
        /// Lookups the specified entry.
        /// </summary>
        /// <param name="entry_name">Name of the entry.</param>
        /// <returns>Return the entry if found, or null if it doesn't exist.</returns>
        ISymbolTableEntry Lookup(string entry_name);


        /// <summary>
        /// Gets the sorted entries.
        /// </summary>
        /// <returns></returns>
        List<ISymbolTableEntry> GetSortedEntries();

        
        
    }
}
