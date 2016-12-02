grammar SimpleGrammar;

import SimpleGrammarRules;
 
/* Grammar rules */
 

formula  
: LPAREN formula RPAREN			#FormulaInParen 
| if_expr						#IfExpression 
| function_expr					#FunctionExpression
| arithmetic_expr				#ArithmeticExpression
; 
 
function_expr : function LPAREN first_argument COMMA second_argument RPAREN;
function	: MIN
			| MAX
			;
first_argument	: formula ;
second_argument	: formula ;

if_expr : IF condition THEN conclusion ELSE else_conclusion SEMI ;
 
condition		: logical_expr ;
conclusion		: formula ;
else_conclusion	: formula ;

logical_expr
: logical_expr AND logical_expr # LogicalExpressionAnd
| logical_expr OR logical_expr  # LogicalExpressionOr
| comparison_expr               # ComparisonExpression
| LPAREN logical_expr RPAREN    # LogicalExpressionInParen
| logical_entity                # LogicalEntity
;
 
comparison_expr : comparison_operand comp_operator comparison_operand # ComparisonExpressionWithOperator
                | LPAREN comparison_expr RPAREN						  # ComparisonExpressionParens
                ;
 
comparison_operand : formula
                   ;
 
comp_operator : GT
              | GE
              | LT
              | LE
              | EQ
              ;
 
arithmetic_expr
: arithmetic_expr MULT arithmetic_expr  # ArithmeticExpressionMult
| arithmetic_expr DIV arithmetic_expr   # ArithmeticExpressionDiv
| arithmetic_expr PLUS arithmetic_expr  # ArithmeticExpressionPlus
| arithmetic_expr MINUS arithmetic_expr # ArithmeticExpressionMinus
| MINUS arithmetic_expr                 # ArithmeticExpressionNegation
| LPAREN arithmetic_expr RPAREN         # ArithmeticExpressionParens
| numeric_entity                        # ArithmeticExpressionNumericEntity
| function_expr							# ArithmeticExpressionFunction
;
 
logical_entity : (TRUE | FALSE) # LogicalConst
               | IDENTIFIER     # LogicalVariable
               ;
 
numeric_entity : DECIMAL              # NumericConst
               | IDENTIFIER           # NumericVariable
               ;