\ ============================================================================
\ File: binary-search.4th
\ 
\ Binary Search Algorithm
\ Demonstrates efficient searching in sorted arrays
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================

10 constant ARRAY_SIZE

\ Sorted array
variable arr ARRAY_SIZE allot

\ Initialize sorted array
: init-array ( --- )
  5 arr 0 + !
  12 arr 1 + !
  23 arr 2 + !
  34 arr 3 + !
  45 arr 4 + !
  56 arr 5 + !
  67 arr 6 + !
  78 arr 7 + !
  89 arr 8 + !
  95 arr 9 + !
;

\ Binary search implementation
\ Returns index if found, -1 if not found
: binary-search ( target --- index )
  variable target
  variable low
  variable high
  variable mid
  variable found
  
  target !
  0 low !
  ARRAY_SIZE 1 - high !
  -1 found !
  
  begin
    low @ high @ >
    0=
  while
    \ mid = (low + high) / 2
    low @ high @ + 2 / mid !
    
    \ Check if target is at mid
    arr mid @ + @ target @ =
    if
      mid @ found !
      high @ 1 - high !  \ Exit loop
    else
      \ If target > arr[mid], search right half
      arr mid @ + @ target @ <
      if
        mid @ 1 + low !
      else
        \ Search left half
        mid @ 1 - high !
      then
    then
  repeat
  
  found @
;

\ Test binary search
: test-search ( value --- )
  ."Searching for " dup . .": "
  binary-search
  dup -1 =
  if
    drop ."Not found"
  else
    ."Found at index " .
  then
  cr
;

: main   \ Entry point
  ."Binary Search Demonstration" cr
  ."============================" cr cr
  
  init-array
  
  ."Sorted array: "
  ARRAY_SIZE 0 do
    arr i + ? space
  loop
  cr cr
  
  ."Test cases:" cr
  23 test-search
  56 test-search
  95 test-search
  100 test-search
  5 test-search
  50 test-search
  
  cr ."Binary search complete!" cr
;
