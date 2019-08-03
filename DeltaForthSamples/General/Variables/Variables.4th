( --------------------------------------------------------------------
  File:		Variables.4th

  Summary:	Shows the use of global and local variables

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

variable a		\ First variable
variable b 		\ Second variable
variable tab 1 allot	\ Holds the sum and the product of 'a' and 'b'

: main		\ Entry point
  initialize	\ Initialize variables
  sum		\ Computes the sum and stores the result in tab[0]
  product	\ Computes the sum and stores the result in tab[1]
  display	\ Displays the operation summary
;

: initialize
  10 a !	\ Initialize 'a' with 10
  20 b !	\ Initialize 'b' with 20
;

: sum
  a @ b @
  +		\ Compute sum of 'a' and 'b'
  tab !		\ Store result in tab[0]
;

: product
  a @ b @
  *		\ Compute product of 'a' and 'b'
  tab 1+ !	\ Store result in tab[1]
;

: display
  variable s	\ Local variable to hold the sum
  variable p    \ Local variable to hold the product
  tab @ s !	\ Store sum to 's'
  tab 1+ @ p !	\ Store product to 'p'
  ."The sum is " s ? ." and the product is " p ? cr
;