using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class ErrorReporter
    {
        public ErrorReporter()
        {
        }

        public static void RecordError(int error_code, int line_numner, string possibility = "")
        {
            m_Errors.Add(new Tuple<string, int>(ErrorMap[error_code] + possibility, line_numner + 1));
            m_Scanner.ScanNextLine();
        }

        public static Dictionary<int, string> ErrorMap = new Dictionary<int, string>()
        {
            {0,"Unexpected token" },
            {1,"Missing (" },
            {2,"The target of write must be a variable" },
            {3,"The target of read must be a variable" },
            {4,"Unknown identifier" },
            {5,"Missing )" },
            {6,"The target of call must be a procedure" },
            {7,"Unknown procedure" },
            {8,"The target of call must be a procedure" },
            {9,"Missing ;" },
            {10,"Unexpected operator" },
            {11,"Missing :=" },
            {12,"Unknown variable" },
            {13,"The target of assignment must be a variable" },
            {14,"The name of procedure must be an identifier" },
            {15,"The name of procedure is already used" },
            {16,"The name of variable must be an identifier" },
            {17,"The name of variable is already used" },
            {18,"The name of constant must be an identifier" },
            {19,"The name of constant is already used" },
            {20,"Missing =" },
            {21,"The value of constant must be an unsigned" },
            {22,"Missing then" }

        };

        public static PascalScanner m_Scanner { get; set; }
        public static List<Tuple<string, int>> m_Errors = new List<Tuple<string, int>>();
    }
}
