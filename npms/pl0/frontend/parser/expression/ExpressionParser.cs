using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    public class ExpressionParser : PascalParser
    {
        public ExpressionParser(PascalParser parent)
            : base(parent)
        {

        }
        public override void Parse()
        {
            try
            {
                ParseExpression();
            }
            catch (Exception)
            {
            }
        }

        private void ParseExpression()
        {
            string sign = string.Empty;

            var token = CurrentToken();

            if (token.m_Text == "-" || token.m_Text == "+")
            {
                sign = token.m_Text;
                ConsumeCurrentToken();//skip sign 
            }

            // parse first term
            ParseTerm();

            if (sign == "-")
            {
                CodeGenerator.Emit(Operation.Negate);
            }


            for (; ; )
            {
                token = CurrentToken();
                Operation op = Operation.Null;

                switch (token.m_Type)
                {
                    case PascalTokenType.Add:
                        op = Operation.Add;
                        break;

                    case PascalTokenType.Subtract:
                        op = Operation.Subtract;
                        break;

                    default:
                        //TODO: error?
                        return;
                }

                ConsumeCurrentToken();//skip operator

                // parse another term
                ParseTerm();

                //
                CodeGenerator.Emit(op);
            }
        }

        private void ParseTerm()
        {
            // parse first factor
            ParseFactor();

            for (; ; )
            {
                var token = CurrentToken();
                Operation op = Operation.Null;

                switch (token.m_Type)
                {
                    case PascalTokenType.Divide:
                        op = Operation.Divide;
                        break;

                    case PascalTokenType.Multiply:
                        op = Operation.Multiply;
                        break;

                    default:
                        //TODO: error?
                        return;
                }

                ConsumeCurrentToken();//skip operator

                // parse another factor
                ParseFactor();
                CodeGenerator.Emit(op);
            }
        }

        private void ParseFactor()
        {
            var token = CurrentToken();

            switch (token.m_Type)
            {
                case PascalTokenType.Unsigned:
                    int value = (int)token.m_Value;
                    CodeGenerator.Emit(Operation.LoadLiteral, 0, value);
                    ConsumeCurrentToken();
                    break;

                case PascalTokenType.Identifier:
                    var result = m_SymbolTableStack.Lookup(token.m_Text);
                    if (result == null)
                    {
                        //TODO: error
                        ErrorReporter.RecordError(4, m_Scanner.m_CurrentToken.m_LineNumber);
                        throw new Exception();
                    }

                    var type = (result as SymbolTableEntry).GetAttribute(SymbolTableAttributeKeyType.Type);
                    switch (type)
                    {
                        case SymbolTableEntryType.Constant:
                            int const_value = (int)(result as SymbolTableEntry).GetAttribute(SymbolTableAttributeKeyType.ConstantValue);
                            CodeGenerator.Emit(Operation.LoadLiteral, 0, const_value);
                            break;

                        case SymbolTableEntryType.Variable:
                            var current_nesting_level = m_SymbolTableStack.GetCurrentNestingLevel();
                            var reference_nesting_level = result.GetSymbolTable().GetNestingLevel();
                            int offset = (int)(result as SymbolTableEntry).GetAttribute(SymbolTableAttributeKeyType.Offset);
                            CodeGenerator.Emit(Operation.LoadVariable, current_nesting_level - reference_nesting_level, offset);
                            break;

                        default:
                            break;
                    }
                    ConsumeCurrentToken();
                    break;

                case PascalTokenType.LeftParenthesis:

                    ConsumeCurrentToken();//skip (
                    var expression_parser = new ExpressionParser(this);
                    expression_parser.ParseExpression();

                    token = CurrentToken();
                    if (token.m_Text != ")")
                    {
                        //TODO: error
                        ErrorReporter.RecordError(5, m_Scanner.m_CurrentToken.m_LineNumber);
                        throw new Exception();
                    }

                    ConsumeCurrentToken();//skip )

                    break;

                default:
                    //TODO: error
                    ErrorReporter.RecordError(0, m_Scanner.m_CurrentToken.m_LineNumber);
                    throw new Exception();
            }



        }

    }
}
