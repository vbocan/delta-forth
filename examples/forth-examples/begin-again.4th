\ ============================================================================
\ File: begin-again.4th
\ 
\ Infinite loop structure demonstration
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
: main			\ Entry point
  variable cnt		\ Local variable
  0 cnt !		\ Initialize 'cnt' to 0
  begin
    cnt ? space		\ Display the counter value
    cnt @ 25 >
    if exit then        \ Exit word if the counter exceeds 25
    cnt @ 1+ cnt !	\ Increment variable by 1
  again
;