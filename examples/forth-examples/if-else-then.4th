\ ============================================================================
\ File: if-else-then.4th
\ 
\ Conditional branching with IF-ELSE-THEN structures
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
10 constant num

: main 		\ Entry point

  num 2 mod	\ Remainder of division by 2
  num .		\ Display constant
  if ." is an odd number" else ." is an even number" then
  
;