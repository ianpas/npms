using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    

    public class SymbolTableEntry : ISymbolTableEntry
    {
        public SymbolTableEntry(string entry_name, ISymbolTable symbol_table)
        {
            m_Name = entry_name;
            m_SymbolTable = symbol_table;
            m_LineNumbers = new List<int>();
            m_Attributes = new Dictionary<SymbolTableAttributeKeyType, object>();

        }



        string ISymbolTableEntry.GetName() => m_Name;

        ISymbolTable ISymbolTableEntry.GetSymbolTable() => m_SymbolTable;

        void ISymbolTableEntry.AppendLineNumber(int line_number) => m_LineNumbers.Add(line_number);

        List<int> ISymbolTableEntry.GetLineNumbers() => m_LineNumbers;

        public void SetAttribute(SymbolTableAttributeKeyType key, object value)
        {
            m_Attributes.Add(key, value);
        }

        public object GetAttribute(SymbolTableAttributeKeyType key)
        {
            return m_Attributes[key];
        }


        void ISymbolTableEntry.SetAttribute(ISymbolTableAttributeKey key, object value)
        {
            throw new NotImplementedException();
        }
        object ISymbolTableEntry.GetAttribute(ISymbolTableAttributeKey key)
        {
            throw new NotImplementedException();

        }



        public ISymbolTable m_SymbolTable { get; set; }
        public string m_Name { get; set; }
        public List<int> m_LineNumbers { get; set; }
        public Dictionary<SymbolTableAttributeKeyType, object> m_Attributes { get; set; }
    }
}
