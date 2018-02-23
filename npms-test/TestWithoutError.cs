using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using npms;
using System.IO;
using System.Linq;

namespace npms_test
{
    [TestClass]
    public class TestWithoutError
    {
        public void Parse(string file_path)
        {
            Source source = new Source(file_path);
            var scanner = new PascalScanner(source);
            var parser = new PascalParser(scanner);
            parser.Parse();
        }

        [TestMethod]
        public void Test1()
        {
            Test("test1");
        }
        [TestMethod]
        public void Test2()
        {
            Test("test2");
        }
        [TestMethod]
        public void Test3()
        {
            Test("test3");
        }
        [TestMethod]
        public void TestExpression()
        {
            Test("expression");
        }

        [TestMethod]
        public void TestIfThen()
        {
            Test("if-then");
        }


        [TestMethod]
        public void TestOdd()
        {
            Test("odd");
        }

        [TestMethod]
        public void TestRepeatStatement()
        {
            Test("repeat");
        }


        [TestMethod]
        public void TestIfStatement()
        {
            Test("statement-if");
        }


        [TestMethod]
        public void TestProcedureDeclaration()
        {
            Test("declaration-procedure");
        }

        [TestMethod]
        public void TestCall()
        {
            Test("call");
        }

        [TestMethod]
        public void TestAssignment()
        {
            Test("assignment");
        }

        [TestMethod]
        public void TestWrite()
        {
            Test("write");
        }

        [TestMethod]
        public void TestRead()
        {
            Test("read");
        }

        [TestMethod]
        public void TestCondition()
        {
            Test("condition");
        }


        private void Test(string file_name)
        {
            //
            string file_path = $"unit-test/{file_name}.txt";
            Parse(file_path);

            //
            string output = string.Empty;

            //
            for (int i = 0; i < CodeGenerator.m_Code.Count; ++i)
            {
                var code = CodeGenerator.m_Code[i];
                output += $"{code.Item1} {code.Item2} {code.Item3}";
                output += "\r\n";
            }

            File.WriteAllText($"unit-test/{file_name}.out.txt", output);

            string compare = File.ReadAllText($"unit-test/{file_name}.compare.txt");
            Assert.AreEqual(GetString(output), GetString(compare));
        }

        private string GetString(string s)
        {
            return new string(s.Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
    }
}
