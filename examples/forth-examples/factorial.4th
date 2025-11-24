\ ============================================================================
\ File: factorial.4th
\ 
\ Factorial calculator using loops
\ Demonstrates simple iterative calculations
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================

\ Calculate factorial of n (stack-based)
: factorial ( n --- result )
  dup 1 >
  if
    1 swap 1 + 1 do
      i *
    loop
  else
    drop 1
  then
;

: main   \ Entry point
  ."Factorial Calculator" cr
  ."=====================" cr cr
  
  ."Factorials from 0 to 12:" cr
  13 0 do
    i . ." ! = " i factorial . cr
  loop
  
  cr ."Calculation complete!" cr
;
