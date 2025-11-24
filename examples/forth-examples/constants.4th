\ ============================================================================
\ File: constants.4th
\ 
\ Shows how to define and use integer and string constants
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
30 constant cint1		\ Integer constant
76 constant cint2		\ Integer constant
"The sum is:" constant cstr	\ String constant

: main			\ Entry point
  tib cstr		\ Dumps the text in 'cstr' to the address pointed by TIB
  tib dup count type	\ Types the text at TIB
  cint1 cint2 +		\ Computes the constant sum
  .			\ Displays the sum
;