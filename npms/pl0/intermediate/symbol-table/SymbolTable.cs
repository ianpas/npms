using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class SymbolTable : ISymbolTable
    {
        public SymbolTable(int nesting_level)
        {
            m_NestingLevel = nesting_level;
            m_SymbolTable = new SortedDictionary<string, ISymbolTableEntry>();
        }

        int ISymbolTable.GetNestingLevel() => m_NestingLevel;

        ISymbolTableEntry ISymbolTable.Enter(string entry_name)
        {
            SymbolTableEntry entry = new SymbolTableEntry(entry_name, this);
            m_SymbolTable.Add(entry_name, entry);
            return entry;
        }

        ISymbolTableEntry ISymbolTable.Lookup(string entry_name)
        {
            var entry = m_SymbolTable.FirstOrDefault(table_entry => table_entry.Key == entry_name);
            return entry.Value;
        }

        List<ISymbolTableEntry> ISymbolTable.GetSortedEntries()
        {
            var entries = new List<ISymbolTableEntry>();
            foreach (var entry in m_SymbolTable)
            {
                entries.Add(entry.Value);
            }
            return entries;
        }

        //
        public int m_NestingLevel { get; set; }
        public SortedDictionary<string, ISymbolTableEntry> m_SymbolTable { get; set; }
        public int m_VariableAmount { get; set; }
    }
}
