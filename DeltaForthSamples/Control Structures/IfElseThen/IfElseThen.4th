( --------------------------------------------------------------------
  File:		IfElseThen.4th

  Summary:	Shows the use of the IF-ELSE-THEN structure

  Origin:	Valer BOCAN, PhD <valer@bocan.ro>

  Date:		October 12, 2001

  --------------------------------------------------------------------
  This file is part of the Delta Forth Code Samples.

  Copyright (C)2001 Valer BOCAN, PhD  All rights reserved.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  -------------------------------------------------------------------- )

10 constant num

: main 		\ Entry point

  num 2 mod	\ Remainder of division by 2
  num .		\ Display constant
  if ." is an odd number" else ." is an even number" then
  
;