using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public interface ISymbolTableStack
    {
        /// <summary>
        /// Gets the current nesting level.
        /// </summary>
        /// <returns></returns>
        int GetCurrentNestingLevel();


        /// <summary>
        /// Return the local symbol table witch is at the top of the stack.
        /// </summary>
        /// <returns></returns>
        ISymbolTable GetLocalSymbolTable();

        /// <summary>
        /// Enters a new entry to the local symbol table.
        /// </summary>
        /// <param name="entry_name">Name of the entry.</param>
        ISymbolTableEntry EnterLocalSymbolTable(string entry_name);

        /// <summary>
        /// Lookup an existing symbol table entry in the local symbol table.
        /// </summary>
        /// <param name="entry_name">Name of the entry.</param>
        /// <returns></returns>
        ISymbolTableEntry LookupLocal(string entry_name);

        /// <summary>
        /// Lookup an existing symbol table entry throughout the stack.
        /// </summary>
        /// <param name="entry_name">Name of the entry.</param>
        /// <returns></returns>
        ISymbolTableEntry Lookup(string entry_name);
    }
}
