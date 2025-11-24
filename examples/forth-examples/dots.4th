\ ============================================================================
\ File: dots.4th
\ 
\ Stack debugging example demonstrating stack inspection
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
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