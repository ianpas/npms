using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class PascalAssignmentStatementParser : PascalStatementParser
    {
        public PascalAssignmentStatementParser(PascalParser parent)
            : base(parent)
        {

        }

        public ICodeNode Parse(Token token)
        {
            var node_type = new PascalCodeNodeType(CodeNodeType.Assign);
            var assign_node = PascalCodeFactory.CreateCodeNode(node_type);

            string target_name = token.m_Text;
            node_type = new PascalCodeNodeType(CodeNodeType.Variable);
            var variable_node = PascalCodeFactory.CreateCodeNode(node_type);
            assign_node.AddChild(variable_node);

            token = NextToken();

            if (token.m_Text == ":=")
            {
                token = NextToken();

                var expression_parser = new PascalExpressionParser(this);
                assign_node.AddChild(expression_parser.Parse(token));
            }
            else
            {
                // error: assignment without :=
            }

            return assign_node;
        }
    }
}
