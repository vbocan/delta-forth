( --------------------------------------------------------------------
  File:		Permutations.4th

  Summary:	Example of backtracking implemented in Delta Forth

  Origin:	Valer BOCAN, PhD <valer@bocan.ro>

  Date:		April 3, 2002

  --------------------------------------------------------------------
  This file is part of the Delta Forth Code Samples.

  Copyright (C)2002 Valer BOCAN, PhD  All rights reserved.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  -------------------------------------------------------------------- )


\ Number of elements (change as desired)
7 constant N

\ Table used to build partial and final solutions
variable TAB N allot

\ Solution count
variable SolCount

(
  Initializes table
  IN:  None
  OUT: None
)
: Init ( --- )
  TAB N erase
;

(
  Checks solution validity
  IN:  The level up to which the verification is performed
  OUT: Boolean value, indicating whether the solution is correct
)
: CheckSolution (p --- b)
  variable i1
  variable i2
  variable ok

  1 ok !	\ Assume that the solution is correct

  dup
  N <> if drop 0 exit then	\ If the number of checked elements is less
				\ than the number of elements in the table, simply
				\ return FALSE
  dup 1
  do
  I i1 !
    dup 1
    do
    I i2 !
       i1 @ i2 @ <>
       if
          tab i1 @ + @
          tab i2 @ + @
          =
          if 0 ok ! then
       then
    loop
  loop
  drop
  ok @
;


(
  Display solution
  IN:  None
  OUT: None
)
: DisplaySolution
  N 1
  do
    TAB I + ?
    space
  loop
  cr
;

(
  Generate solutions by backtracking
  IN:  Starting level, usually 1
  OUT: None
)
: Backtracking
  N 1
  do
  dup
    TAB + I swap !
    dup CheckSolution
    if
       DisplaySolution
       SolCount @ 1+ SolCount !
    else
       dup N < 
       if dup 1+ Backtracking then
    then
  loop
  drop
;

\ Program starting point
: main
  0 SolCount !	\ Initialize solution count to 0

  Init
  1 Backtracking

  cr SolCount ? ." solution(s)." cr
;
