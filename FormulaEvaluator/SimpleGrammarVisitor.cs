using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace FormulaEvaluator
{
    public class SimpleGrammarVisitor : ISimpleGrammarVisitor<double>
    {
        public const double GT = 1.0;
        public const string GTSymbol = @">";
        public const double GE = 2.0;
        public const string GESymbol = @">=";
        public const double LT = -1.0;
        public const string LTSymbol = @"<";
        public const double LE = -2.0;
        public const string LESymbol = @"<=";
        public const double EQ = 0.0;
        public const string EQSymbol = @"=";

        public const double TRUE = 1.0;
        public const double FALSE = 0.0;


        public double VisitArithmeticExpressionMult(SimpleGrammarParser.ArithmeticExpressionMultContext context)
        {
            return context.arithmetic_expr(0).Accept(this) * context.arithmetic_expr(1).Accept(this);
        }

        public double VisitComparisonExpressionParens(SimpleGrammarParser.ComparisonExpressionParensContext context)
        {
            return context.comparison_expr().Accept(this);
        }

        public double VisitNumericConst(SimpleGrammarParser.NumericConstContext context)
        {
            return context.GetChild(0).Accept(this);
        }
        public double VisitElse_conclusion(SimpleGrammarParser.Else_conclusionContext context)
        {
            return context.GetChild(0).Accept(this);
        }
        public double VisitConclusion(SimpleGrammarParser.ConclusionContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitLogicalEntity(SimpleGrammarParser.LogicalEntityContext context)
        {
            throw new NotImplementedException();
        }


        public double VisitLogicalExpressionInParen(SimpleGrammarParser.LogicalExpressionInParenContext context)
        {
            return context.logical_expr().Accept(this);
        }

        public double VisitComp_operator(SimpleGrammarParser.Comp_operatorContext context)
        {
            return ComparissonOperantToDoule(context.GetChild(0).ToString());
        }

        private double ComparissonOperantToDoule(string operant)
        {
            switch (operant)
            {
                case GTSymbol: { return GT; }
                case GESymbol: { return GE; }
                case LTSymbol: { return LT; }
                case LESymbol: { return LE; }
                case EQSymbol: { return EQ; }
            }
            throw new ArgumentException("Invalid comparisson operant :" + operant);
        }

        string DoubleToComparissonOperant(double operant)
        {

            if (operant == GT) { return GTSymbol; }
            if (operant == GE) { return GESymbol; }
            if (operant == LT) { return LTSymbol; }
            if (operant == LE) { return LESymbol; }
            if (operant == EQ) { return EQSymbol; }
            throw new ArgumentException("Invalid comparisson operant :" + operant);
        }

        public double VisitFormulaInParen(SimpleGrammarParser.FormulaInParenContext context)
        {
            return context.GetChild(1).Accept(this);
        }

        public double VisitLogicalVariable(SimpleGrammarParser.LogicalVariableContext context)
        {
            throw new NotImplementedException();
        }

        public double VisitIfExpression(SimpleGrammarParser.IfExpressionContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitArithmeticExpressionMinus(SimpleGrammarParser.ArithmeticExpressionMinusContext context)
        {
            return context.arithmetic_expr(0).Accept(this) - context.arithmetic_expr(1).Accept(this);
        }

        public double VisitArithmeticExpressionParens(SimpleGrammarParser.ArithmeticExpressionParensContext context)
        {
            return context.GetChild(1).Accept(this);
        }

        public double VisitArithmeticExpressionNumericEntity(SimpleGrammarParser.ArithmeticExpressionNumericEntityContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitArithmeticExpression(SimpleGrammarParser.ArithmeticExpressionContext context)
        {
            return context.arithmetic_expr().Accept(this);
        }

        public double VisitArithmeticExpressionPlus(SimpleGrammarParser.ArithmeticExpressionPlusContext context)
        {
            return context.arithmetic_expr(0).Accept(this) + context.arithmetic_expr(1).Accept(this);
        }

        public double VisitNumericVariable(SimpleGrammarParser.NumericVariableContext context)
        {
            throw new NotImplementedException();
        }

        public double VisitIf_expr(SimpleGrammarParser.If_exprContext context)
        {
            if (context.GetChild(1).Accept(this) > 0)
                return context.GetChild(3).Accept(this);
            else
                return context.GetChild(5).Accept(this);

        }

        public double VisitComparisonExpression(SimpleGrammarParser.ComparisonExpressionContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitCondition(SimpleGrammarParser.ConditionContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitLogicalExpressionAnd(SimpleGrammarParser.LogicalExpressionAndContext context)
        {
            return BooleanToDoulbe(context.logical_expr(0).Accept(this) > 0 && context.logical_expr(0).Accept(this) > 0);
        }

        public double VisitComparisonExpressionWithOperator(SimpleGrammarParser.ComparisonExpressionWithOperatorContext context)
        {
            var left = context.GetChild(0).Accept(this);
            var operant = DoubleToComparissonOperant(context.GetChild(1).Accept(this));
            var right = context.GetChild(2).Accept(this);

            switch (operant)
            {
                case GTSymbol: { return BooleanToDoulbe(left > right); }
                case GESymbol: { return BooleanToDoulbe(left >= right); }
                case LTSymbol: { return BooleanToDoulbe(left < right); }
                case LESymbol: { return BooleanToDoulbe(left <= right); }
                case EQSymbol: { return BooleanToDoulbe(left == right); }
            }
            throw new ArgumentException("Invalid Argument :" + operant);
        }

        private double BooleanToDoulbe(bool p)
        {
            if (p)
                return TRUE;
            return FALSE;
        }

        public double VisitArithmeticExpressionDiv(SimpleGrammarParser.ArithmeticExpressionDivContext context)
        {
            //todo: test what happens in Div By Zero!!
            return context.arithmetic_expr(0).Accept(this) / context.arithmetic_expr(1).Accept(this);
        }

        public double VisitLogicalExpressionOr(SimpleGrammarParser.LogicalExpressionOrContext context)
        {
            return BooleanToDoulbe(context.logical_expr(0).Accept(this) > 0 || context.logical_expr(0).Accept(this) > 0);
        }

        public double VisitComparison_operand(SimpleGrammarParser.Comparison_operandContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitArithmeticExpressionNegation(SimpleGrammarParser.ArithmeticExpressionNegationContext context)
        {
            throw new NotImplementedException();
        }

        public double VisitLogicalConst(SimpleGrammarParser.LogicalConstContext context)
        {
            throw new NotImplementedException();
        }

        public double Visit(Antlr4.Runtime.Tree.IParseTree tree)
        {
            return tree.Accept(this);
        }

        public double VisitChildren(Antlr4.Runtime.Tree.IRuleNode node)
        {
            throw new NotImplementedException();
        }

        public double VisitErrorNode(Antlr4.Runtime.Tree.IErrorNode node)
        {
            throw new NotImplementedException();
        }

        public double VisitTerminal(Antlr4.Runtime.Tree.ITerminalNode node)
        {
            return double.Parse(node.ToString(), CultureInfo.InvariantCulture);
        }




        public double VisitFunctionExpression(SimpleGrammarParser.FunctionExpressionContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitFunction(SimpleGrammarParser.FunctionContext context)
        {
             return FunctionToDouble(context.GetChild(0).ToString());
        }


        public double VisitFunction_expr(SimpleGrammarParser.Function_exprContext context)
        {
            var function = DoubleToFunction(context.GetChild(0).Accept(this));
            var firstArgument = context.GetChild(2).Accept(this);
            var secondArgument = context.GetChild(4).Accept(this);

            return ExecuteFunction(function, firstArgument, secondArgument);
        }

        private double ExecuteFunction(string function, double firstArgument, double secondArgument)
        {
            switch (function)
            {
                case "min": { return Math.Min(firstArgument, secondArgument); }
                case "max": { return Math.Max(firstArgument, secondArgument); }
                default:
                    throw new ArgumentException("Invalid function:" + function);
            }
        }


        private double FunctionToDouble(string function)
        {
            switch (function)
            {
                case "min": { return -1.0; }
                case "max": { return 1.0; }
                default:
                    throw new ArgumentException("Invalid function:" + function);
            }
        }
        private string DoubleToFunction(double function)
        {
            if (function == 1.0)
                return "max";
            if (function == -1.0)
                return "min";
            throw new ArgumentException("Invalid function:" + function);
        }

        public double VisitFirst_argument(SimpleGrammarParser.First_argumentContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitSecond_argument(SimpleGrammarParser.Second_argumentContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitArithmeticExpressionFunction([NotNull] SimpleGrammarParser.ArithmeticExpressionFunctionContext context)
        {
            return context.GetChild(0).Accept(this);
        }

        public double VisitFormula([NotNull] SimpleGrammarParser.FormulaContext context)
        {
            throw new NotImplementedException();
        }

        public double VisitLogical_expr([NotNull] SimpleGrammarParser.Logical_exprContext context)
        {
            throw new NotImplementedException();
        }

        public double VisitComparison_expr([NotNull] SimpleGrammarParser.Comparison_exprContext context)
        {
            throw new NotImplementedException();
        }

        public double VisitArithmetic_expr([NotNull] SimpleGrammarParser.Arithmetic_exprContext context)
        {
            throw new NotImplementedException();
        }

        public double VisitLogical_entity([NotNull] SimpleGrammarParser.Logical_entityContext context)
        {
            throw new NotImplementedException();
        }

        public double VisitNumeric_entity([NotNull] SimpleGrammarParser.Numeric_entityContext context)
        {
            throw new NotImplementedException();
        }
    }


}
