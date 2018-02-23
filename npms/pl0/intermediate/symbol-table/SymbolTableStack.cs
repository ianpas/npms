using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class SymbolTableStack : ISymbolTableStack
    {
        public SymbolTableStack()
        {
            m_CurrentNestingLevel = 0;

            m_SymbolTableStack = new List<ISymbolTable>
            {
                new SymbolTable(m_CurrentNestingLevel)
            };
        }

        public ISymbolTable Push()
        {
            ++m_CurrentNestingLevel;
            m_SymbolTableStack.Add(new SymbolTable(m_CurrentNestingLevel));
            return (this as ISymbolTableStack).GetLocalSymbolTable();
        }

        public ISymbolTable Pop()
        {
            var table = (this as ISymbolTableStack).GetLocalSymbolTable();
            m_SymbolTableStack.RemoveAt(m_CurrentNestingLevel);
            --m_CurrentNestingLevel;
            return table;
        }

        int ISymbolTableStack.GetCurrentNestingLevel() => m_CurrentNestingLevel;

        ISymbolTable ISymbolTableStack.GetLocalSymbolTable()
        {
            var local_table = m_SymbolTableStack.LastOrDefault();
            return local_table;
        }

        ISymbolTableEntry ISymbolTableStack.EnterLocalSymbolTable(string entry_name)
        {
            var local_table = m_SymbolTableStack.LastOrDefault();
            return local_table.Enter(entry_name);
        }

        ISymbolTableEntry ISymbolTableStack.LookupLocal(string entry_name)
        {
            var local_table = m_SymbolTableStack.LastOrDefault();
            return local_table.Lookup(entry_name);
        }

        ISymbolTableEntry ISymbolTableStack.Lookup(string entry_name)
        {
            for (int i = m_SymbolTableStack.Count - 1; i >= 0; --i)
            {
                var table = m_SymbolTableStack[i];
                var result = table.Lookup(entry_name);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public List<ISymbolTable> m_SymbolTableStack { get; set; }
        public int m_CurrentNestingLevel { get; set; }
    }
}
