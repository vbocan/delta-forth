\ ============================================================================
\ File: leap-years.4th
\ 
\ Leap year calculation and validation
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
1901 constant start-year
2030 constant end-year

variable leap-years

: check-years
  start-year end-year > 0=
;

: leap
  0 leap-years !
  end-year start-year
  do
    i 4 mod 0=
    if 1 leap-years +!
    then
  loop
;

: main	\ Entry point
  check-years
  if leap
     ."There are " leap-years ? ." leap years between "
     start-year . ." and " end-year . ."."
  else
     ."Error, the start year is past the end year."
  then
;