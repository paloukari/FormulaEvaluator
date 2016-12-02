using Antlr4.Runtime;
using System;
using System.Globalization;

namespace FormulaEvaluator
{
    public class FormulaEvaluator
    {
        private const string _base = "@PV";
        private const string _notChanged = "@NC";
        private const string _notaNumber = "NaN";


        static FormulaEvaluator()
        {
            Visitor = new SimpleGrammarVisitor();
        }

        public static Double Evaluate(string formula, Double parentValue)
        {
            //if not changed value appears do not try to evaluate it
            if (formula.Contains(_notChanged) || formula.Contains(_notaNumber))
                return Double.NaN;
            
            if (formula.Contains(_base))
            {
                if (Double.IsNaN(parentValue))
                    return Double.NaN;
                formula = formula.Replace(_base, parentValue.ToString(CultureInfo.InvariantCulture));
            }
            var input = new AntlrInputStream(formula);
            var lexer = new SimpleGrammarLexer(input);

            var tokenStream = new CommonTokenStream(lexer);
            var parser = new SimpleGrammarParser(tokenStream);
            Double res = Double.NaN;
            try
            {
                res = Visitor.Visit(parser.formula());
            }
            catch (Exception)
            {
                //todo:log this
                return Double.NaN;
            };
            return res;
        }

        static SimpleGrammarVisitor Visitor;
    }
}
