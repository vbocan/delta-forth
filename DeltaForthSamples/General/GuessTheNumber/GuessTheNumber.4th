( --------------------------------------------------------------------
  File:		GuessTheNumber.4th

  Summary:	Simple game asking the user to guess a number

  Origin:	Valer BOCAN, PhD <valer@bocan.ro>

  Date:		October 16, 2001

  --------------------------------------------------------------------
  This file is part of the Delta Forth Code Samples.

  Copyright (C)2001 Valer BOCAN, PhD  All rights reserved.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
  -------------------------------------------------------------------- )

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