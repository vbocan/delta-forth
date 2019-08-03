( --------------------------------------------------------------------
  File:		Dots.4th

  Summary:	The .S word: shows the content of the parameter stack

  Origin:	Valer BOCAN, PhD <valer@bocan.ro>

  Date:		September 22, 2004

  --------------------------------------------------------------------
  This file is part of the Delta Forth Code Samples.

  Copyright (C)2004 Valer BOCAN, PhD  All rights reserved.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  -------------------------------------------------------------------- )

: main
1 2 0 4 .S
;

: .S
  ."Number of elements on the stack: "
  sp@ s0 - dup . cr
  0 > if
  ."Stack elements: "
  sp@ 1 - s0 do
	i @ . space
  loop
  then
  cr
;