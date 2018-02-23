using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace npms
{
    class ProgramParser : PascalParser
    {
        public ProgramParser(PascalParser parent)
            : base(parent)
        {

        }

        public override void Parse()
        {
            var token = NextToken();
            if ((PascalTokenType)token.m_Type == PascalTokenType.Const)
            {
                var const_parser = new ConstDeclarationParser(this);
                const_parser.Parse();
            }

            //TODO: error?
            token = CurrentToken();
            if ((PascalTokenType)token.m_Type == PascalTokenType.Variable)
            {
                var var_parser = new VariableDeclarationParser(this);
                var_parser.Parse();
            }

            //
            CodeGenerator.Emit(Operation.Jump, 0, 0);//default 0
            int current = CodeGenerator.m_Code.Count - 1;


            //
            for (; ; )
            {
                token = CurrentToken();

                if ((PascalTokenType)token.m_Type == PascalTokenType.Procedure)
                {                 
                    //
                    var proceduer_parser = new ProcedureDeclarationParser(this);
                    proceduer_parser.Parse();
                   
                }
                else
                {
                    break;
                }
            }

            //TODO:
            SymbolTablePrinter.RecordSymbolTable(m_SymbolTableStack.GetLocalSymbolTable());

            //
            CodeGenerator.Refine(current, CodeGenerator.m_Code.Count);

            //allocate variables
            var local = m_SymbolTableStack.GetLocalSymbolTable();
            CodeGenerator.Emit(Operation.Allocate, 0, (local as SymbolTable).m_VariableAmount);

            //
            token = CurrentToken();
            var statement_parser = new StatementParser(this);
            statement_parser.Parse();

            //
            CodeGenerator.Emit(Operation.Return);
        }
    }
}
