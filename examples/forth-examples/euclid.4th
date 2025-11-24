\ ============================================================================
\ File: euclid.4th
\ 
\ Euclidean algorithm for finding GCD (Greatest Common Divisor)
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================

4330 	constant num1	\ The first number
8235 	constant num2	\ The second number

( Word to test if value on top of stack is equal to or less than 0 )
: zero-less-equal
  dup 0=
  swap 0<
  or
;

: gcd		(num1 num2 - - -)
  over zero-less-equal
  if		(num1 is <= 0)
    drop drop
  else
    dup zero-less-equal
    if		(num2 is <= 0)
      drop drop
    else
      begin
      over over =
      if	(We've got the result)
        .
      else
        over over >
        if
          swap
        then
        over - 0
      then
      until
    then
  then
;

: main		\ Entry point
  ."GCD of " num1 . ." and " num2 . ." is " num1 num2 gcd cr
;
