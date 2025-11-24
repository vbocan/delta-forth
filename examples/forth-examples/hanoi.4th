\ ============================================================================
\ File: hanoi.4th
\ 
\ Towers of Hanoi puzzle solver using recursive algorithm
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
variable step-count

(
  Recursively solves the Hanoi problem
  IN:  The source, auxiliary and destination pegs, as well as
       the number of disks to move.
  OUT: None
)
: hanoi (Src Aux Dst N --- )

  \ if N is 0 exit
  dup 0=
  if drop4 exit then

  \ hanoi(Src, Dst, Aux, N-1)
  dup4
  >r swap r> 1 -
  hanoi

  \ Display move details
  step-count ? ."."
  ." Move " 
  >r rot dup .
  ." to "
  swap dup . ."."
  rot swap r>
  cr

  \ Increment step count
  step-count @ 1+ step-count !

  \ hanoi(Aux, Src, Dst, N-1)
  dup4
  >r rot swap r> 1 -
  hanoi

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
  1 step-count !	\ Initialize step count

  \ Source peg:      1
  \ Auxiliary peg:   2
  \ Destination peg: 3
  1 2 3
  ."Number of disks: "
  \ Read number of disks
  query str2int
  cr ."Solving for " dup . ." disks..." cr
  hanoi
;