( --------------------------------------------------------------------
  File:		BeginUntil.4th

  Summary:	Shows the use of the BEGIN-UNTIL loop

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
  variable cnt		\ Local variable
  0 cnt !		\ Initialize 'cnt' to 0
  begin
    cnt @ 1+ cnt !	\ Increment variable by 1
    cnt ? space		\ Display the counter value
    cnt @ 25 >		\ Test if the counter is less than 25
  until
;