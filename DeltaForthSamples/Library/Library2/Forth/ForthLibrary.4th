( --------------------------------------------------------------------
  File:		ForthLibrary.4th

  Summary:	Shows how to call a word in a library generated
		by Delta Forth .NET
		This file should be compiled with the /DLL option

  Origin:	Valer BOCAN, PhD <valer@bocan.ro>

  Date:		January 19, 2002

  --------------------------------------------------------------------
  This file is part of the Delta Forth Code Samples.

  Copyright (C)2002 Valer BOCAN, PhD  All rights reserved.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  -------------------------------------------------------------------- )

library ForthLib

: main
  \ Word MAIN must be defined, even if it is empty since it ensures
  \ proper initialization of the DLL
;

\ Perform addition of the top two elements
: Addition
  +
;

\ Perform subtraction of the top two elements
: Subtraction
  -
;

: DisplayTopOfStack
  ."Top of stack is: " . cr
;