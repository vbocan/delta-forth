( --------------------------------------------------------------------
  File:		Hanoi.4th

  Summary:	Towers of Hanoi example implemented in Delta Forth

  Origin:	Valer BOCAN, PhD <valer@bocan.ro>

  Date:		May 11, 2003

  --------------------------------------------------------------------
  This file is part of the Delta Forth Code Samples.

  Copyright (C)2003 Valer BOCAN, PhD  All rights reserved.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  -------------------------------------------------------------------- )

(
  The Tower of Hanoi puzzle was invented by the French mathematician
  Edouard Lucas in 1883. We are given a tower of eight disks, initially
  stacked in increasing size on one of three pegs. The objective is to
  transfer the entire tower to one of the other pegs, moving only one disk
  at a time and never a larger one onto a smaller.

  Recursive solution:
  Hanoi[Src, Aux, Dst, N]
    if N is 0 exit
    Hanoi[Src, Dst, Aux, N-1]
    Move from Src to Dst
    Hanoi[Aux, Src, Dst, N-1]
)

\ Step counter
variable StepCount

(
  Recursively solves the Hanoi problem
  IN:  The source, auxiliary and destination pegs, as well as
       the number of disks to move.
  OUT: None
)
: Hanoi (Src Aux Dst N --- )

  \ if N is 0 exit
  dup 0=
  if drop4 exit then

  \ Hanoi(Src, Dst, Aux, N-1)
  dup4
  >r swap r> 1 -
  Hanoi

  \ Display move details
  StepCount ? ."."
  ." Move " 
  >r rot dup .
  ." to "
  swap dup . ."."
  rot swap r>
  cr

  \ Increment step count
  StepCount @ 1+ StepCount !

  \ Hanoi(Aux, Src, Dst, N-1)
  dup4
  >r rot swap r> 1 -
  Hanoi

  drop4
;

(
  Duplicates the 4 top most values on the stack
  IN:  4 values
  OUT: duplicated 4 - 4 values
)
: dup4 (a b c n --- a b c n a b c n)
  variable a
  variable b
  variable c
  variable n

  n ! c ! b ! a !
  a @ b @ c @ n @
  a @ b @ c @ n @
;  

(
  Drops 4 values from the stack
  IN:  4 values
  OUT: None
)
: drop4 ( a b c d --- )
  drop
  drop
  drop
  drop
;

\ Program starting point
: main
  1 StepCount !	\ Initialize step count

  \ Source peg:      1
  \ Auxiliary peg:   2
  \ Destination peg: 3
  1 2 3
  ."Number of disks: "
  \ Read number of disks
  query str2int
  cr ."Solving for " dup . ." disks..." cr
  Hanoi
;
