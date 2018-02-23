using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class Program
    {
        static void Main(string[] args)
        {
            //for(; ; )
            {
                try
                {
                    Console.WriteLine("---pl0 compiler---");
                    Console.WriteLine();

                    Console.Write("input file: ");

                    string file_path = Console.ReadLine();

                    Console.WriteLine();
                    Console.WriteLine("compile " + file_path + "...");
                    Console.WriteLine();

                    var compiler = new Program();
                    compiler.Compile(file_path);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error.\n" + e.Message);
                }

                Console.WriteLine();
                Console.WriteLine("---press any key to exit...---");
                Console.ReadLine();
            }


        }

        public void Compile(string file_path)
        {
            Source source = new Source(file_path);
            var scanner = new PascalScanner(source);
            ErrorReporter.m_Scanner = scanner;
            var parser = new PascalParser(scanner);
            parser.Parse();

            //SymbolTablePrinter.Print();

            if (ErrorReporter.m_Errors.Count != 0)
            {
                Console.WriteLine("\n errors: ");

                for (int i = 0; i < ErrorReporter.m_Errors.Count; ++i)
                {
                    var error = ErrorReporter.m_Errors[i];
                    Console.WriteLine($"{i}: {error.Item1} in line {error.Item2}");
                }
            }
            else
            {
                Console.WriteLine("\ncompile succeed without errors.\n");

                for (int i = 0; i < CodeGenerator.m_Code.Count; ++i)
                {
                    var code = CodeGenerator.m_Code[i];
                    Console.WriteLine($"{i,-5}{code.Item1,-5}{code.Item2,-5}{code.Item3,-5}");
                }
            }

        }

    }
}
