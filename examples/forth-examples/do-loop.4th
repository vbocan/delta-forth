\ ============================================================================
\ File: do-loop.4th
\ 
\ Counted loops using DO-LOOP and +LOOP constructs
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
: main 		\ Entry point
  25		\ Final value
  0		\ Initial value
  do
    i . space
  loop
;