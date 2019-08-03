( --------------------------------------------------------------------
  File:		Euclid.4th

  Summary:	Computes the greatest common divider using
		Euclid's algorithm

  Origin:	Valer BOCAN, PhD <valer@bocan.ro>

  Date:		October 11, 2001

  --------------------------------------------------------------------
  This file is part of the Delta Forth Code Samples.

  Copyright (C)2001 Valer BOCAN, PhD  All rights reserved.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  -------------------------------------------------------------------- )

4330 	constant num1	\ The first number
8235 	constant num2	\ The second number

( Word to test if value on top of stack is equal to or less than 0 )
: ZeroLessEqual
  dup 0=
  swap 0<
  or
;

: gcd		(num1 num2 - - -)
  over ZeroLessEqual
  if		(num1 is <= 0)
    drop drop
  else
    dup ZeroLessEqual
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