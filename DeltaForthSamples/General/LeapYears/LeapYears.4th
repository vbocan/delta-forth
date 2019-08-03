( --------------------------------------------------------------------
  File:		LeapYears.4th

  Summary:	Counts the number of leap years between two specified
		years

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

1901 constant StartYear
2030 constant EndYear

variable leapyears

: CheckYears
  StartYear EndYear > 0=
;

: Leap
  0 LeapYears !
  EndYear StartYear
  do
    i 4 mod 0=
    if 1 LeapYears +!
    then
  loop
;

: main	\ Entry point
  CheckYears
  if Leap
     ."There are " LeapYears ? ." leap years between "
     StartYear . ." and " EndYear . ."."
  else
     ."Error, the start year is past the end year."
  then
;