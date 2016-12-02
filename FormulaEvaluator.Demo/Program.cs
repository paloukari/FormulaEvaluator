using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FormulaEvaluator.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputStream = new StreamReader("TestFile.txt");
            string inputText = "";
            var sw2 = new Stopwatch();
            sw2.Restart();
            long rowsCount = 0;
            var visitor = new SimpleGrammarVisitor();

            List<Task> tasks = new List<Task>();
            while (!String.IsNullOrEmpty(inputText = inputStream.ReadLine()))
            {
                tasks.Add(Task.Factory.StartNew((data) =>
                {
                    Interlocked.Increment(ref rowsCount);
                    var sw = new Stopwatch();
                    sw.Start();

                    var input = new AntlrInputStream((string)data);
                    var lexer = new SimpleGrammarLexer(input);

                    var tokenStream = new CommonTokenStream(lexer);
                    var parser = new SimpleGrammarParser(tokenStream);

                    var res = visitor.Visit(parser.formula());

                    Console.WriteLine(string.Format("[in {0:0.000} ms] {1} = {2} ", sw.Elapsed.TotalMilliseconds, data, res));
                }, inputText));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Completed {2} calculation in {0:0.000} ms total and average {1:0.000} ms", sw2.Elapsed.TotalMilliseconds, sw2.Elapsed.TotalMilliseconds / rowsCount, rowsCount);
            Console.ReadLine();

        }
    }
}
