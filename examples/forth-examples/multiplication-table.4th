\ ============================================================================
\ File: multiplication-table.4th
\ 
\ Multiplication table generator
\ Demonstrates nested loops and formatted output
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================

\ Print table header
: print-header ( --- )
  ."    x|"
  13 1 do
    i . space
  loop
  cr
;

\ Print one row of the table
: print-row ( n --- )
  dup . ." ->|"
  dup 13 1 do
    dup i * . space
  loop
  drop cr
;

: main   \ Entry point
  ."Multiplication Table (1-12)" cr
  ."============================" cr cr
  
  print-header
  
  13 1 do
    i print-row
  loop
  
  cr ."Table complete!" cr
;
