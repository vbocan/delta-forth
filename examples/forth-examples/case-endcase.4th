\ ============================================================================
\ File: case-endcase.4th
\ 
\ Multi-way selection (switch/case equivalent)
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
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