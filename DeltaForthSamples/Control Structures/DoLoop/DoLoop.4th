( --------------------------------------------------------------------
  File:		DoLoop.4th

  Summary:	Shows the use of the DO-LOOP loop

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

: main 		\ Entry point
  25		\ Final value
  0		\ Initial value
  do
    i . space
  loop
;