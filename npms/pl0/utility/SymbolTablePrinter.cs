using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class SymbolTablePrinter
    {
        public SymbolTablePrinter()
        {

        }

        public static void Print()
        {
            Console.WriteLine(m_Output);
        }

        public static void RecordSymbolTable(ISymbolTable table)
        {
            var entries = table.GetSortedEntries();
            foreach (var entry in entries)
            {
                RecordTableEntry(entry as SymbolTableEntry);
            }

            Append("\n");
        }

        public static void RecordTableEntry(SymbolTableEntry entry)
        {
            Append(entry.m_Name);
            var attributes = entry.m_Attributes;

            foreach (var attribute in attributes)
            {
                if (attribute.Key == SymbolTableAttributeKeyType.ConstantValue)
                {
                    Append($" const: {(int)attribute.Value}");
                }
                else if (attribute.Key == SymbolTableAttributeKeyType.Offset)
                {
                    Append($" offset: {(int)attribute.Value}");
                }
                else if (attribute.Key == SymbolTableAttributeKeyType.Address)
                {
                    Append($" address: {(int)attribute.Value}");
                }
            }
            Append("\n");

        }

        public static void Append(string s)
        {
            m_Output += s;
        }
        public static string m_Output { get; set; }

    }
}
