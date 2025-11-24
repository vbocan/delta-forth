\ ============================================================================
\ File: fizzbuzz.4th
\ 
\ FizzBuzz - The classic programming interview question
\ Print numbers 1-100, but:
\   - Print "Fizz" for multiples of 3
\   - Print "Buzz" for multiples of 5
\   - Print "FizzBuzz" for multiples of both 3 and 5
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================

\ Check if number is divisible by 3 and 5
: fizzbuzz? ( n --- bool )
  dup 3 mod 0=
  swap 5 mod 0=
  and
;

\ Check if number is divisible by 3
: fizz? ( n --- bool )
  3 mod 0=
;

\ Check if number is divisible by 5
: buzz? ( n --- bool )
  5 mod 0=
;

\ Process one number
: fizzbuzz-number ( n --- )
  dup fizzbuzz?
  if
    drop ."FizzBuzz"
  else
    dup fizz?
    if
      drop ."Fizz"
    else
      dup buzz?
      if
        drop ."Buzz"
      else
        .
      then
    then
  then
  cr
;

\ Extended FizzBuzz with statistics
: fizzbuzz-extended ( n --- )
  variable limit
  variable fizz-count
  variable buzz-count
  variable fizzbuzz-count
  variable number-count
  
  limit !
  0 fizz-count !
  0 buzz-count !
  0 fizzbuzz-count !
  0 number-count !
  
  ."FizzBuzz from 1 to " limit @ . cr
  ."=====================" cr cr
  
  limit @ 1 do
    i dup fizzbuzz?
    if
      fizzbuzz-count @ 1 + fizzbuzz-count !
    else
      dup fizz?
      if
        fizz-count @ 1 + fizz-count !
      else
        dup buzz?
        if
          buzz-count @ 1 + buzz-count !
        else
          number-count @ 1 + number-count !
        then
      then
    then
    drop
    
    i fizzbuzz-number
  loop
  
  cr ."Statistics:" cr
  ."  FizzBuzz: " fizzbuzz-count ? cr
  ."  Fizz:     " fizz-count ? cr
  ."  Buzz:     " buzz-count ? cr
  ."  Numbers:  " number-count ? cr
;

: main   \ Entry point
  ."FizzBuzz Challenge" cr
  ."==================" cr cr
  
  100 fizzbuzz-extended
  
  cr ."Challenge complete!" cr
;
