\ ============================================================================
\ File: prime-numbers.4th
\ 
\ Prime number generator using Sieve of Eratosthenes
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
400 constant limit

: is-prime	\ Returns 1 if the number on top of stack is a prime number
  2
  begin
    over over mod 0= 0=
    rot rot dup >r
    over 2 / > 0=
    rot and r> swap
  while
    1+
  repeat
  over 2 / >
;

: main		\ Entry point
  ."Prime numbers up to " limit . .": "
  limit 1 do
          i is-prime
          if i . space then
	  loop
;