( --------------------------------------------------------------------
  File:		CaseEndCase.4th

  Summary:	Shows the use of the CASE-ENDCASE selector

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

: main			\ Entry point
  1 test cr		\ Test for 1
  2 test cr             \ Test for 2
  3 test cr             \ Test for 3
  4 test cr		\ Test for 4
;

: test
  case
  1 of ."One"   endof
  2 of ."Two"   endof
  3 of ."Three" endof
  ."Something else"
  endcase
;