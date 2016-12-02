lexer grammar SimpleGrammarRules; 

IF   : 'if' ;
THEN : 'then';
ELSE : 'else';
 
AND : 'and' ;  
OR  : 'or' ; 
  
TRUE  : 'true' ;
FALSE : 'false' ;

MIN  : 'min' ;
MAX : 'max' ;
 
MULT  : '*' ; 
DIV   : '/' ;
PLUS  : '+' ;
MINUS : '-' ;
 
GT : '>' ;
GE : '>=' ;
LT : '<' ;
LE : '<=' ;
EQ : '=' ;
 
LPAREN : '(' ;
RPAREN : ')' ;

 
// DECIMAL, IDENTIFIER, COMMENTS, WS are set using regular expressions
 
DECIMAL : '-'?[0-9]+('.'[0-9]+)? ;
 
IDENTIFIER : [a-zA-Z_][a-zA-Z_0-9]* ;
 
SEMI : ';' ;
COMMA : ',' ;
 
// COMMENT and WS are stripped from the output token stream by sending
// to a different channel 'skip'
 
COMMENT : '//' .+? ('\n'|EOF) -> skip ;
 
WS : [ \r\t\u000C\n]+ -> skip ;

fragment SPACE: ' ' | '\t';

EOL : ( '\r'? '\n' SPACE* )+;
