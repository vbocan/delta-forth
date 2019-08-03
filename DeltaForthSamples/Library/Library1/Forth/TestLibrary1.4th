( --------------------------------------------------------------------
  File:		TestLibrary1.4th

  Summary:	Shows how to call a function in a library generated
		by a .NET compiler (C#)

  Origin:	Valer BOCAN, PhD <valer@bocan.ro>

  Date:		January 18, 2002

  --------------------------------------------------------------------
  This file is part of the Delta Forth Code Samples.

  Copyright (C)2002 Valer BOCAN, PhD  All rights reserved.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  -------------------------------------------------------------------- )

\ Declare and external word in file Library1.dll
\ Namespace is Library1, class name is Class1 and method name is DisplayLogo
extern DisplayLogo Library1.dll Library1.Class1.DisplayLogo

\ Declare and external word in file Library1.dll
\ Namespace is Library1, class name is Class2 and method name is DisplayRandom
extern DisplayRandom Library1.dll Library1.Class2.DisplayRandom

: main
  ."Attempting to call functions:" cr
  DisplayLogo
  DisplayRandom
;
