( --------------------------------------------------------------------
  File:		Constants.4th

  Summary:	Shows the use of integer and string constants

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

30 constant cint1		\ Integer constant
76 constant cint2		\ Integer constant
"The sum is:" constant cstr	\ String constant

: main			\ Entry point
  tib cstr		\ Dumps the text in 'cstr' to the address pointed by TIB
  tib dup count type	\ Types the text at TIB
  cint1 cint2 +		\ Computes the constant sum
  .			\ Displays the sum
;