\ ============================================================================
\ File: guess-the-number.4th
\ 
\ Interactive number guessing game with user input
\ 
\ Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP
\ https://www.bocan.ro
\ 
\ Licensed under the MIT License.
\ This code is provided as-is, for educational and research purposes.
\ ============================================================================
221 constant guess 	\ This is the number to guess
variable NoTries

: main 				\ Entry point
  0 NoTries !  			\ Number of tries is initially 0
  ."Play the game. Guess a number!" cr
  begin
    NoTries @ 1+ NoTries ! 	\ Increment number of tries
    ."Type a number:"
    query			\ Expect at most 80 characters to the TIB area
    str2int			\ Convert characters to integer, if possible
    dup guess <>		\ Check for match
  while
    dup
    guess <
    if . ." is too small." else . ." is too big." then
    ." Try again..." cr
  repeat
  ."You guessed in " NoTries ? ." tries. Congratulations!" cr
;