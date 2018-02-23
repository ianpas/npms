using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class PascalIfStatementParser : PascalStatementParser
    {
        public PascalIfStatementParser(PascalParser parent)
            : base(parent)
        {
        }

        public ICodeNode Parse(Token token)
        {
            token = NextToken(); // consume the "if" token

            // create an if node
            var node_type = new PascalCodeNodeType(CodeNodeType.If);
            var if_node = PascalCodeFactory.CreateCodeNode(node_type);
            var expression_parser = new PascalExpressionParser(this);
            if_node.AddChild(expression_parser.Parse(token));

            // consume the "then" token
            token = NextToken();

            var statement_parser = new PascalStatementParser(this);
            if_node.AddChild(statement_parser.Parse(token));

            token = CurrentToken();
            if ((PascalTokenType)token.m_Type == PascalTokenType.Else)
            {
                token = NextToken(); // consume the "else" token
                if_node.AddChild(statement_parser.Parse(token));
            }

            return if_node;

        }
    }
}
