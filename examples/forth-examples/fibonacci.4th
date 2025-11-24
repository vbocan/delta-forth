\ ============================================================================
\ File: fibonacci.4th
\ 
\ Fibonacci sequence calculator
\ Demonstrates loops, arrays, and mathematical sequences
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================

50 constant MAX_FIB
variable fib_array MAX_FIB allot

\ Initialize and calculate Fibonacci sequence up to n
: init_fib ( n --- )
  0 fib_array 0 + !
  1 fib_array 1 + !
  
  dup 2 >
  if
    dup 2 do
      fib_array i 1 - + @
      fib_array i 2 - + @
      + fib_array i + !
    loop
  then
  drop
;

\ Get nth Fibonacci number from array
: fib ( n --- result )
  fib_array swap + @
;

: main   \ Entry point
  ."Fibonacci Calculator" cr
  ."====================" cr cr
  
  \ Initialize up to F(30)
  30 init_fib
  
  ."First 20 Fibonacci numbers:" cr
  20 0 do
    ."F(" i . .") = " i fib . cr
  loop
  
  cr ."Larger values:" cr
  ."F(25) = " 25 fib . cr
  ."F(30) = " 30 fib . cr
  
  cr ."Calculation complete!" cr
;
