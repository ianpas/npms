using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{

    public enum PascalTokenType
    {
        Error, Eof,
        Reserved,
        Identifier,
        Unsigned,
        Delimiter,

        Multiply, Divide, Add, Subtract,
        Begin, End, Else, If, Then, Const, Equals, Variable,
        Less,
        Assign,
        Procedure,
        Semicolon,
        Call,
        While,
        Do,
        Read,
        Write,
        NotEquals,
        Greater,
        LessEquals,
        GreaterEquals,
        LeftParenthesis,
        Repeat,
        Until,
        Odd
    }

    public static class TokenUtility
    {
        public static List<string> ReservedWordTable = new List<string>()
        {
            "const","var","procedure","odd",
            "if","then","else","while","do",
            "call","begin","end","repeat","until",
            "read","write"
        };

        //public static List<string> SingleOperatorTable = new List<string>()
        //{
        //    "+","-","*","/",
        //    "=","<",">"
        //};

        public static List<string> BinaryOperatorTable = new List<string>()
        {
            "<>","<=",">=",":="
        };

        public static List<string> OperatorFirstSymbolTable = new List<string>()
        {
            "+","-","*","/",
            "=","<",">",

            ":"
        };

        public static List<string> DelimiterTable = new List<string>()
        {
            "(",")",";",",","."
        };

        public static Dictionary<string, PascalTokenType> SpecialSymbolsMap = new Dictionary<string, PascalTokenType>()
        {
            {"*",PascalTokenType.Multiply },
            {"/",PascalTokenType.Divide },
            {"+",PascalTokenType.Add },
            {"-",PascalTokenType.Subtract },

            {";",PascalTokenType.Semicolon },
            {":=",PascalTokenType.Assign },

            {"=",PascalTokenType.Equals },
            {"<>",PascalTokenType.NotEquals },

            {"<",PascalTokenType.Less },
            {"<=",PascalTokenType.LessEquals },
            {">",PascalTokenType.Greater },
            {">=",PascalTokenType.GreaterEquals },

            {"(",PascalTokenType.LeftParenthesis }
        };

        public static Dictionary<string, PascalTokenType> ReservedWordMap = new Dictionary<string, PascalTokenType>()
        {
            {"else",PascalTokenType.Else },
            {"if",PascalTokenType.If },
            {"then",PascalTokenType.Then },
            {"begin",PascalTokenType.Begin },
            {"end",PascalTokenType.End },
            {"const",PascalTokenType.Const },
            {"var",PascalTokenType.Variable },
            {"procedure",PascalTokenType.Procedure },
            {"call",PascalTokenType.Call },
            {"while",PascalTokenType.While },
            {"read",PascalTokenType.Read },
            {"write",PascalTokenType.Write },
            {"do",PascalTokenType.Do },
            {"repeat",PascalTokenType.Repeat },
            {"until",PascalTokenType.Until },
            {"odd",PascalTokenType.Odd }

        };

        public static List<PascalTokenType> MultiplicativeOperators = new List<PascalTokenType>()
        {
           PascalTokenType.Multiply,
           PascalTokenType.Divide
        };

        public static List<PascalTokenType> AdditiveOperators = new List<PascalTokenType>()
        {
           PascalTokenType.Add,
           PascalTokenType.Subtract
        };
    }
}
