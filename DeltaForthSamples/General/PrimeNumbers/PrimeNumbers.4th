( --------------------------------------------------------------------
  File:		PrimeNumbers.4th

  Summary:	Displays the prime numbers up to a specified limit

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

400 constant limit

: isprime	\ Returns 1 if the number on top of stack is a prime number
  2
  begin
    over over mod 0= 0=
    rot rot dup >r
    over 2 / > 0=
    rot and r> swap
  while
    1+
  repeat
  over 2 / >
;

: main		\ Entry point
  ."Prime numbers up to " limit . .": "
  limit 1 do
          i isprime
          if i . space then
	  loop
;