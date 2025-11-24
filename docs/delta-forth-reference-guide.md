# Delta Forth - Reference Guide

**Version 2.0**

Copyright 1997-2025 © Valer BOCAN, PhD, CSSLP

www.bocan.ro

---

## Table of Contents

- [Overview](#overview)
- [Legal Stuff](#legal-stuff)
- [Delta Forth Basics](#delta-forth-basics)
  - [Overview of a Forth program](#overview-of-a-forth-program)
  - [A Word on Stacks (not a Forth Word though)](#a-word-on-stacks-not-a-forth-word-though)
- [Primitive Forth Words](#primitive-forth-words)
  - [Memory Operations](#memory-operations)
  - [Arithmetic Operations](#arithmetic-operations)
  - [Logical Operations](#logical-operations)
  - [Bitwise Operations](#bitwise-operations)
  - [Stack Operations](#stack-operations)
  - [Return Stack Operations](#return-stack-operations)
  - [Display Operations](#display-operations)
  - [Keyboard Operations](#keyboard-operations)
  - [String Operations](#string-operations)
  - [Conversions](#conversions)
  - [Miscellaneous](#miscellaneous)
  - [System Variables](#system-variables)
- [Constants](#constants)
- [Global and Local Variables](#global-and-local-variables)
- [Libraries](#libraries)
- [Control Structures](#control-structures)
  - [IF-ELSE-THEN](#if-else-then)
  - [DO-LOOP](#do-loop)
  - [BEGIN-AGAIN](#begin-again)
  - [BEGIN-UNTIL](#begin-until)
  - [BEGIN-WHILE-REPEAT](#begin-while-repeat)
  - [CASE-ENDCASE](#case-endcase)
- [Compiler Error Messages](#compiler-error-messages)
- [Compiler Command-Line Options](#compiler-command-line-options)

---

## Overview

Delta Forth represents a modern, compile-only implementation of the Forth programming language targeting the .NET runtime environment. While diverging from traditional Forth standards, this dialect provides an accessible entry point for newcomers while maintaining the core philosophy that makes Forth unique: minimal syntax coupled with powerful expressiveness.

### Design Philosophy

Unlike traditional Forth implementations that employ an interactive "compreter" (the combined compiler-interpreter model), Delta Forth embraces a pure compilation strategy. This architectural decision stems from the .NET execution model, where ahead-of-time compilation produces assemblies rather than incrementally building an interactive dictionary. Consequently, certain metacompilation primitives from ANS Forth (such as `STATE`, `COMPILE`, `IMMEDIATE`, and `[COMPILE]`) have no meaningful interpretation in this context and are therefore omitted.

This compiled approach, however, preserves Forth's fundamental strengths: direct hardware-level expressiveness through stack-based computation, zero-overhead abstractions, and remarkably concise control flow constructs that rival assembly language in both clarity and efficiency.

### Code Generation and Deployment

The Delta Forth compiler emits fully compliant Common Language Runtime (CLR) bytecode, packaged as either console executables (`.EXE`) or dynamic link libraries (`.DLL`). The generated assemblies execute on any conformant .NET platform, including:

- **Microsoft .NET Framework** (Windows)
- **Mono Runtime** (Linux, macOS, BSD)
- **.NET Core / .NET 5+** (cross-platform)

Since version 1.2, Delta Forth supports strong-name signing through integration with the .NET Strong Name tool (`sn.exe`), enabling deployment to the Global Assembly Cache (GAC) and satisfying enterprise security policies.

### Historical Context

Delta Forth descends from the award-winning Delta Forth for Java project, initiated in 1997 as one of the earliest Forth compilers targeting a managed runtime environment. The current .NET implementation continues this legacy, demonstrating that Forth's minimalist elegance translates effectively to modern managed execution platforms.

## Legal Stuff

Delta Forth is released under the MIT License: <https://choosealicense.com/licenses/mit/>

## Delta Forth Basics

### Program Structure and Word Definitions

In Forth terminology, the fundamental unit of code organization is the *word* (analogous to functions, procedures, or methods in other languages). A word represents a named sequence of operations that manipulates the parameter stack to accomplish a specific task.

#### Minimal Program Example

Every Delta Forth program must define a `main` word, which serves as the program's entry point:

```forth
: main
  ."Hello world!"
;
```

The colon (`:`) begins a word definition, followed by the word's name and its implementation. The semicolon (`;`) terminates the definition.

#### Word Invocation and Stack-Based Parameters

Words are invoked by their name alone—no parentheses, no explicit parameter lists. All data flows through the parameter stack, eliminating the syntactic overhead of parameter declarations. This stack-oriented paradigm is both Forth's greatest strength and its steepest learning curve:

```forth
: main
  DisplayText
;

: displaytext
  ."Hello world!"
;
```

**Key Language Properties:**
- **Case Insensitivity**: `DisplayText`, `DISPLAYTEXT`, and `displaytext` refer to the same word
- **Order Independence**: Words may be referenced before their definitions appear in the source
- **Zero-Overhead Calls**: Word invocation compiles to direct method calls in the CLR

#### Comment Syntax

Delta Forth supports two complementary comment styles:

- **Single-line comments**: `\\` introduces a comment extending to the end of the line
- **Multi-line comments**: `( ... )` encloses arbitrary text, potentially spanning multiple lines

**Example:**
```forth
: main  \\ Program starting point
  DisplayText  \\ Invoke the display routine
;

( Word: DisplayText
  Purpose: Emit a greeting message to standard output
  Stack Effect: ( -- )
  Author: John Doe
  Version: 1.0
)
: DisplayText
  ."Hello world with comments!"
;
```

**Note:** The parenthetical comment form requires whitespace after the opening parenthesis for proper parsing.

#### Source File Organization

Large programs benefit from modular organization across multiple source files. The `LOAD` directive provides textual inclusion semantics similar to `#include` in C:

**File: DisplayWords.4th**
```forth
( Display utilities module )
: DisplayText
  ."Hello world from another file!"
;
```

**File: Main.4th**
```forth
( Main program entry point )
load DisplayWords.4th

: main
  DisplayText
;
```

#### Compilation Model

The compiler is invoked on the primary source file (which must contain `main`). All transitively referenced files via `LOAD` directives are incorporated into the compilation unit. Key behaviors:

- **Dependency Resolution**: The compiler automatically processes all loaded files
- **Cycle Detection**: Circular dependencies are detected and reported as errors
- **Single-Pass Processing**: Each file is processed exactly once, regardless of multiple `LOAD` references
- **Entry Point Requirement**: Absence of `main` causes a compilation error

**Convention:** Use the `.4th` extension for Delta Forth source files to distinguish them from other Forth dialects.

### Stack Architecture

Delta Forth implements a dual-stack architecture fundamental to all Forth systems. Understanding these stacks is essential for effective Forth programming.

#### The Parameter Stack (Data Stack)

The **parameter stack** serves as the primary data interchange mechanism, replacing explicit function parameters found in conventional languages. Key characteristics:

- **Element Type**: 32-bit signed integers (`System.Int32` in CLR terms)
- **Default Capacity**: 524,288 cells (configurable via `/FS` compiler option)
- **Overflow Behavior**: Stack overflow/underflow triggers runtime exceptions (unless `/NOCHECK` is specified)
- **Conceptual Model**: An expandable array of CPU registers accessible through stack operations

Think of the parameter stack as an implicit parameter list that all words can read from and write to. This eliminates parameter passing boilerplate while enabling highly compositional code.

**Example: Implicit Stack Loading**
```forth
: main
  10 .  \\ Integer literal pushes 10; dot (.) pops and displays it
;
```

Numbers appearing in the instruction stream are automatically compiled as stack-push operations (provided they're not part of constant or variable declarations).

#### The Return Stack (Control Stack)

The **return stack** serves dual purposes:

1. **Subroutine Return Addresses**: Maintains the call chain (managed transparently by the runtime)
2. **Temporary Storage**: Provides brief storage for values during complex calculations
3. **Loop Control**: Holds loop indices and limits for `DO`...`LOOP` constructs

Properties:
- **Default Capacity**: 1,024 cells (configurable via `/RS` compiler option)
- **Access Discipline**: Return stack manipulation must be balanced within a word definition
- **Primary Use**: Loop index access via `I`, `I'`, and `J` primitives

**Warning:** Return stack manipulation via `>R` and `R>` must be used judiciously. Unbalanced return stack operations corrupt the call stack and lead to undefined behavior.

## Primitive Forth Words

This section catalogs the built-in vocabulary provided by Delta Forth. Each word includes its stack effect diagram using standard Forth notation:

**Stack Effect Notation:**
- `( before -- after )` describes stack transformation
- `n`, `n1`, `n2`: generic integers
- `addr`: memory address
- `c`: character code (integer)
- `b`: boolean flag (0 = false, non-zero = true)

All primitive words are compiled inline where possible, minimizing runtime overhead.

### Memory Operations

These words provide low-level memory access, treating memory as an array of 32-bit cells.

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `@` | `( addr -- n )` | **Fetch**: Reads the 32-bit value at memory address `addr` and pushes it onto the stack |
| `!` | `( n addr -- )` | **Store**: Writes value `n` to memory address `addr` |
| `+!` | `( n addr -- )` | **Plus-store**: Atomically adds `n` to the value stored at `addr` (equivalent to `addr @ n + addr !`) |
| `?` | `( addr -- )` | **Print**: Fetches and displays the value at `addr` (convenience word combining `@` and `.`) |

**Memory Model:** Delta Forth provides a unified address space for variables and arrays. Addresses are zero-based offsets into the data segment.

### Arithmetic Operations

All arithmetic operates on 32-bit signed integers with standard two's complement semantics.

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `+` | `( n1 n2 -- n3 )` | Adds `n1` and `n2`, producing sum `n3` |
| `-` | `( n1 n2 -- n3 )` | Subtracts `n2` from `n1`, yielding `n3 = n1 - n2` |
| `*` | `( n1 n2 -- n3 )` | Multiplies `n1` by `n2` |
| `/` | `( n1 n2 -- n3 )` | Divides `n1` by `n2` (integer division, truncates toward zero) |
| `MOD` | `( n1 n2 -- n3 )` | Computes remainder of `n1 / n2` |
| `/MOD` | `( n1 n2 -- nrem nquot )` | Combined division: returns both remainder and quotient |
| `*/` | `( n1 n2 n3 -- n4 )` | **Scaling operation**: computes `(n1 * n2) / n3` with intermediate 64-bit precision to prevent overflow |
| `*/MOD` | `( n1 n2 n3 -- nrem nquot )` | Scaling division with remainder |
| `MINUS` | `( n -- -n )` | Negates `n` (arithmetic negation) |
| `ABS` | `( n -- u )` | Returns absolute value of `n` |
| `MIN` | `( n1 n2 -- n3 )` | Returns the lesser of `n1` and `n2` |
| `MAX` | `( n1 n2 -- n3 )` | Returns the greater of `n1` and `n2` |
| `1+` | `( n -- n+1 )` | Increments by one (optimized operation) |
| `2+` | `( n -- n+2 )` | Increments by two |

#### Relational Operators

Comparison words return a boolean flag: `-1` (true) or `0` (false).

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `=` | `( n1 n2 -- flag )` | Tests equality: `n1 = n2` |
| `<>` | `( n1 n2 -- flag )` | Tests inequality: `n1 ≠ n2` |
| `<` | `( n1 n2 -- flag )` | Tests less-than: `n1 < n2` |
| `>` | `( n1 n2 -- flag )` | Tests greater-than: `n1 > n2` |
| `0=` | `( n -- flag )` | Tests equality to zero |
| `0<` | `( n -- flag )` | Tests negativity (sign bit) |

### Logical Operations

These words perform boolean logic, treating zero as false and any non-zero value as true. Results are normalized to `0` (false) or `-1` (true).

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `AND` | `( flag1 flag2 -- flag3 )` | Logical conjunction: true if both operands are non-zero |
| `OR` | `( flag1 flag2 -- flag3 )` | Logical disjunction: true if either operand is non-zero |
| `NOT` | `( flag1 -- flag2 )` | Logical negation: inverts the boolean value |

**Note:** These are *logical* operations for control flow. For bitwise manipulation, use the bitwise operators below.

### Bitwise Operations

Bitwise operators perform bit-level manipulation on the binary representation of integers.

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `~AND` | `( n1 n2 -- n3 )` | Bitwise AND: `n3 = n1 & n2` |
| `~OR` | `( n1 n2 -- n3 )` | Bitwise OR: `n3 = n1 \| n2` |
| `~XOR` | `( n1 n2 -- n3 )` | Bitwise XOR (exclusive-or): `n3 = n1 ^ n2` |
| `~NOT` | `( n1 -- n2 )` | Bitwise complement: inverts all bits (one's complement) |

**Application:** Use bitwise operators for flag manipulation, masking, and low-level data packing.

### Stack Operations

Stack manipulation words enable complex data juggling without named variables. Mastering these is essential for idiomatic Forth.

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `DUP` | `( n -- n n )` | **Duplicate**: Copies the top stack item |
| `?DUP` | `( n -- 0 \| n n )` | **Conditional duplicate**: Duplicates only if `n` is non-zero; otherwise leaves zero |
| `DROP` | `( n -- )` | **Discard**: Removes the top stack item |
| `SWAP` | `( n1 n2 -- n2 n1 )` | **Exchange**: Swaps the top two items |
| `OVER` | `( n1 n2 -- n1 n2 n1 )` | **Copy second**: Duplicates the second item to the top |
| `ROT` | `( n1 n2 n3 -- n2 n3 n1 )` | **Rotate**: Moves the third item to the top (left rotation) |
| `SP@` | `( -- addr )` | Returns the current parameter stack pointer as an address |
| `RP@` | `( -- addr )` | Returns the current return stack pointer |
| `SP!` | `( -- )` | **Emergency reset**: Clears the entire parameter stack |
| `RP!` | `( -- )` | **Emergency reset**: Clears the entire return stack |

**Stack Visualization:**
```
Before: ... n1 n2 n3  <-- top
ROT     
After:  ... n2 n3 n1  <-- top
```

**Warning:** `SP!` and `RP!` are dangerous operations intended for error recovery. They discard all stack contents.

### Return Stack Operations

The return stack provides temporary storage for values that would otherwise clutter the parameter stack. Use sparingly and always balance pushes/pops within a single word.

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `>R` | `( n -- ) ( R: -- n )` | **To-R**: Moves a value from parameter stack to return stack |
| `R>` | `( -- n ) ( R: n -- )` | **R-from**: Moves a value from return stack to parameter stack |
| `I` | `( -- n ) ( R: n -- n )` | Copies the current loop index (top of return stack) without removing it |
| `I'` | `( -- n )` | Copies the outer loop index (second item on return stack) |
| `J` | `( -- n )` | Alias for `I'`; accesses the outer loop index in nested loops |

**Critical Constraint:** Every `>R` must be balanced by a corresponding `R>` *within the same word definition*. Unbalanced return stack operations corrupt the call chain.

**Typical Use Case:**
```forth
: example  ( n1 n2 -- result )
  >R        \ Save n2 on return stack
  DUP *     \ Square n1
  R>        \ Retrieve n2
  +         \ Add them
;
```

### Display Operations

Output operations write to the standard output stream (typically the console).

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `EMIT` | `( c -- )` | Outputs the character with ASCII/Unicode code `c` |
| `CR` | `( -- )` | Emits a newline sequence (CR+LF on Windows, LF on Unix) |
| `SPACE` | `( -- )` | Outputs a single space character (equivalent to `32 EMIT`) |
| `SPACES` | `( n -- )` | Outputs `n` consecutive spaces |
| `."<text>"` | `( -- )` | **Compile-time string**: Outputs the literal text enclosed in quotes |
| `.` | `( n -- )` | Pops and displays the top stack value in decimal format |
| `TYPE` | `( addr n -- )` | Outputs `n` characters from memory starting at `addr` |

**Example:**
```forth
: greet  ( -- )
  ." Hello, " 
  NAME TYPE  \ Assuming NAME contains address of a string
  ." !" CR
;
```

### Keyboard Operations

Input operations read from the standard input stream.

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `KEY` | `( -- c )` | Blocks until a key is pressed, returns its ASCII/Unicode code |
| `EXPECT` | `( addr n -- )` | Reads up to `n` characters from input, storing them starting at `addr` |
| `QUERY` | `( -- )` | Reads up to 80 characters into the Terminal Input Buffer (TIB) |

**Note:** `QUERY` is a convenience word that calls `EXPECT` with `TIB` as the destination address.

### String Operations

String and memory block manipulation primitives. Strings in Delta Forth are represented as null-terminated sequences or counted strings.

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `FILL` | `( addr n c -- )` | Fills `n` consecutive memory cells starting at `addr` with value `c` |
| `ERASE` | `( addr n -- )` | Zeroes `n` consecutive cells starting at `addr` (equivalent to `0 FILL`) |
| `BLANKS` | `( addr n -- )` | Fills `n` cells with ASCII space (32) |
| `CMOVE` | `( addr1 addr2 n -- )` | **Copy memory**: Transfers `n` cells from `addr1` to `addr2` |
| `COUNT` | `( addr -- n )` | Computes string length: scans memory from `addr` until null terminator (0) |
| `"<text>"` | `( addr -- )` | **Runtime string**: Stores the literal string at address `addr` (the address must be on stack) |

**Memory Safety:** These operations perform no bounds checking. Ensure destination buffers are adequately sized.

### Conversions

Type conversion between strings and numeric values.

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `STR2INT` | `( -- n )` | Parses the null-terminated string in TIB as a decimal integer, pushes the result |
| `INT2STR` | `( addr n -- )` | Converts integer `n` to its decimal string representation, storing it at `addr` |

### Miscellaneous

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `EXIT` | `( -- )` | **Early return**: Immediately terminates execution of the current word definition |

**Note:** `EXIT` is typically used within conditional branches to implement early returns without nesting.

### System Variables

These words return addresses of predefined memory regions maintained by the runtime.

| Word | Stack Effect | Description |
|------|--------------|-------------|
| `PAD` | `( -- addr )` | Returns address of a 64-cell scratch buffer for temporary calculations |
| `S0` | `( -- addr )` | Returns base address of the parameter stack |
| `R0` | `( -- addr )` | Returns base address of the return stack |
| `TIB` | `( -- addr )` | **Terminal Input Buffer**: Returns address of the 80-cell input buffer used by `QUERY` and `EXPECT` |

**Usage:** System variables are primarily for advanced memory management and debugging.

## Constants

Constants provide named, immutable values defined at compile time. Delta Forth supports both integer and string constants.

### Constant Definition Syntax

```forth
<value> CONSTANT <name>
```

The value must appear on the "virtual" compilation stack before the `CONSTANT` keyword. Integer literals and string literals are automatically pushed onto this stack.

**Examples:**
```forth
10 constant con1             \\ Integer constant
20 constant con2             \\ Another integer constant
"The sum is: " constant MSG  \\ String constant
```

### Compile-Time Evaluation

When the compiler encounters a constant name within a word definition:
- **Integer constants**: The value is compiled as an inline literal (push instruction)
- **String constants**: The string is stored in the data segment, and its address is pushed at runtime

**Example Usage:**
```forth
: display-sum  ( n -- )
  MSG TYPE    \ Push address of "The sum is: " and display it
  .           \ Display the number
  CR
;

: example  ( -- )
  con1 con2 +    \ Computes 10 + 20 = 30
  display-sum    \ Shows "The sum is: 30"
;
```

**Namespace:** Constants share the global namespace with words and variables. Redefining a constant is an error.

## Variables

Variables allocate mutable storage cells in the data segment. Delta Forth distinguishes between global and local variables based on their declaration context.

### Global Variables

Global variables are declared outside any word definition and persist for the program's lifetime.

**Syntax:**
```forth
VARIABLE <name>
VARIABLE <name> <n> ALLOT  \ Allocate array of n+1 cells
```

**Examples:**
```forth
variable X           \\ Single-cell variable
variable Y 19 allot  \\ Array of 20 cells (1 + 19)
variable Buffer 255 allot  \\ 256-cell buffer
```

**Semantics:** When a variable name is referenced in code, its *address* is pushed onto the stack (not its value). Access the value using fetch (`@`) and store (`!`):

```forth
: demo  ( -- )
  42 X !     \ Store 42 in X
  X @        \ Fetch value from X (pushes 42)
  .          \ Display it
;
```

### Array Indexing

Use pointer arithmetic to access array elements:

```forth
: fill-array  ( -- )
  20 0 do
    I  Y I + !  \ Store index i in Y[i]
  loop
;
```

### Local Variables

Local variables are declared within a word definition and provide lexical scoping.

**Example:**
```forth
: word1  ( n -- n*n )
  variable temp  \ Local to word1
  DUP temp !     \ Save argument
  temp @ *       \ Square it
;

: main  ( -- )
  5 word1 .      \ Displays 25
  temp @ .       \ ERROR: 'temp' not in scope
;
```

**Important Properties:**
- **Scope:** Local variables are visible only within their defining word
- **Storage Class:** All variables (global and local) are **static**—they retain values across calls
- **No Dynamic Allocation:** All storage is allocated at compile time

**Namespace Collision:** Local variable names shadow global definitions within their scope.

## Libraries

Delta Forth supports compilation to reusable libraries (DLLs), enabling interoperability with other .NET languages and modular code organization.

### Creating a Library

Compile with the `/DLL` option to generate a .NET assembly (`.dll` file). Use the `LIBRARY` directive to specify the CLR class name:

**Syntax:**
```forth
LIBRARY <ClassName>
```

**Example: MathOp.4th**
```forth
library MathOp

: main  \\ Library initialization entry point
  \\ Initialize global state, lookup tables, etc.
;

: addition  ( n1 n2 -- sum )
  +
;

: subtraction  ( n1 n2 -- diff )
  -
;
```

**Compilation:**
```
DeltaForth.exe MathOp.4th /DLL
```

**Output:** `MathOp.dll` containing class `MathOp` with public methods `main`, `addition`, and `subtraction`.

### Library Semantics

- **Initialization:** The `main` word is invoked once when the library is first loaded
- **Public Interface:** All defined words become public static methods on the generated class
- **State Management:** Global variables persist across method calls
- **Default Class Name:** If `LIBRARY` is omitted, the class is named `DeltaForthEngine`

### Consuming Libraries

Import external Delta Forth libraries using the `EXTERN` directive:

**Syntax:**
```forth
EXTERN <localname> <dllfile> <ClassName>.<MethodName>
```

**Example:**
```forth
extern add mathop.dll MathOp.addition
extern sub mathop.dll MathOp.subtraction

: main  ( -- )
  10 5 add .   \ Displays 15
  10 5 sub .   \ Displays 5
;
```

**Resolution:** At runtime, the CLR dynamically loads `mathop.dll` and binds the specified methods. The library must be in the same directory or the GAC.

### Interoperability with Other .NET Languages

Delta Forth libraries can be consumed by any CLR language (C#, F#, VB.NET). For example, in C#:

```csharp
using MathOp;

class Program {
    static void Main() {
        MathOp.main();  // Initialize library
        int result = MathOp.addition(10, 5);
        Console.WriteLine(result);  // Output: 15
    }
}
```

**Caveat:** Stack-based parameter passing may require wrapper methods for conventional calling conventions.

## Control Structures

Delta Forth provides structured control flow primitives that compile to efficient CLR bytecode. All control structures must be properly nested and balanced within a single word definition.

### IF-ELSE-THEN: Conditional Execution

Implements branching based on a boolean flag.

**Syntax:**
```forth
<condition> IF
  <true-branch>
THEN

<condition> IF
  <true-branch>
ELSE
  <false-branch>
THEN
```

**Semantics:**
- The `<condition>` expression must leave a flag on the stack
- **True**: Any non-zero value executes the true branch
- **False**: Zero executes the else branch (or skips to `THEN` if no `ELSE`)

**Example:**
```forth
: classify  ( n -- )
  DUP 0 > IF
    ."Positive"
  ELSE
    DUP 0 = IF
      ."Zero"
    ELSE
      ."Negative"
    THEN
  THEN
  DROP
;
```

**Nesting:** Arbitrary nesting depth is supported. Each `IF` must have a corresponding `THEN`.

### DO-LOOP: Bounded Iteration

Counted loops with predetermined iteration counts.

**Syntax:**
```forth
<limit> <start> DO
  <loop-body>
LOOP

<limit> <start> DO
  <loop-body>
  <increment>
+LOOP
```

**Parameters:**
- `<start>`: Initial loop index (inclusive)
- `<limit>`: Terminal value (exclusive)
- `<increment>`: Step size for `+LOOP` variant

**Semantics:**
- Loop index and limit are stored on the return stack
- `LOOP` increments the index by 1
- `+LOOP` increments by the value on top of the parameter stack
- Loop terminates when index ≥ limit

**Index Access:**
- `I`: Current loop index
- `I'` or `J`: Outer loop index (for nested loops)

**Examples:**
```forth
: print-range  ( limit start -- )
  DO
    I . space
  LOOP
;

: count-evens  ( -- )
  100 0 DO
    I . space
    2  \ Increment by 2
  +LOOP
;

: nested-demo  ( -- )
  10 0 DO
    10 0 DO
      I J * .  \ Multiplication table
    LOOP CR
  LOOP
;
```

**Early Exit:** Use `LEAVE` to break out of a loop prematurely:
```forth
: find-first-divisible  ( n -- )
  100 0 DO
    I OVER MOD 0= IF
      I . LEAVE
    THEN
  LOOP DROP
;
```

### BEGIN-AGAIN: Infinite Loop

Unconditional loop requiring explicit break mechanism.

**Syntax:**
```forth
BEGIN
  <loop-body>
AGAIN
```

**Warning:** This construct has no built-in exit condition. Use with caution. Typical exits involve `EXIT` or exception handling.

**Example (intentional infinite loop):**
```forth
: server-loop  ( -- )
  BEGIN
    wait-for-connection
    handle-request
  AGAIN
;
```

### BEGIN-UNTIL: Post-Test Loop

Executes loop body at least once, then tests condition.

**Syntax:**
```forth
BEGIN
  <loop-body>
  <condition>
UNTIL
```

**Semantics:** Loop repeats while condition is *false* (zero). Exits when condition becomes *true* (non-zero).

**Example:**
```forth
: wait-for-input  ( -- c )
  BEGIN
    KEY DUP
    0<>  \ Exit when non-null character received
  UNTIL
;

: count-to-n  ( n -- )
  variable counter
  0 counter !
  BEGIN
    counter @ 1+ DUP counter !
    .
    DUP OVER =  \ Exit when counter = n
  UNTIL
  DROP DROP
;
```

### BEGIN-WHILE-REPEAT: Pre-Test Loop

Tests condition before each iteration.

**Syntax:**
```forth
BEGIN
  <condition>
WHILE
  <loop-body>
REPEAT
```

**Semantics:**
- Evaluates `<condition>` at loop start
- **True** (non-zero): Executes body, then loops
- **False** (zero): Skips body and exits

**Example:**
```forth
: scan-until-zero  ( addr -- addr' )
  BEGIN
    DUP @  \ Fetch value
    DUP    \ Duplicate for test
  WHILE
    DROP   \ Discard value
    1+     \ Advance pointer
  REPEAT
  DROP     \ Discard zero value
;

: validate-input  ( -- n )
  BEGIN
    ." Enter number (1-10): " 
    KEY DUP
    48 - DUP 1 >= OVER 10 <= AND  \ Valid digit?
  WHILE
    DROP
    ." Invalid! Try again. " CR
  REPEAT
;
```

### CASE-ENDCASE: Multi-Way Selection

Implements a switch/case-style dispatch structure.

**Syntax:**
```forth
<selector>
CASE
  <value1> OF <action1> ENDOF
  <value2> OF <action2> ENDOF
  ...
  <default-action>
ENDCASE
```

**Semantics:**
- The `<selector>` value remains on the stack during case evaluation
- Each `OF` clause tests for equality with the selector
- First matching case executes and exits the `CASE` structure
- If no case matches, the optional default action executes
- The selector is consumed after `ENDCASE`

**Example:**
```forth
: day-name  ( n -- )
  CASE
    1 OF ." Monday"    ENDOF
    2 OF ." Tuesday"   ENDOF
    3 OF ." Wednesday" ENDOF
    4 OF ." Thursday"  ENDOF
    5 OF ." Friday"    ENDOF
    6 OF ." Saturday"  ENDOF
    7 OF ." Sunday"    ENDOF
    ." Invalid day"  \ Default case
  ENDCASE
;

: demo  ( -- )
  1 day-name CR  \ Monday
  3 day-name CR  \ Wednesday
  9 day-name CR  \ Invalid day
;
```

**Comparison with C/C++ switch:**
- No fall-through behavior (each case auto-breaks)
- Cases can contain arbitrary code, not just constants
- Default case is optional

**Range-Based Dispatch:**
```forth
: classify-age  ( age -- )
  CASE
    DUP 0 12 WITHIN OF ." Child"   DROP ENDOF
    DUP 13 19 WITHIN OF ." Teen"   DROP ENDOF
    DUP 20 64 WITHIN OF ." Adult"  DROP ENDOF
    ." Senior"
  ENDCASE
;
```

## Compiler Diagnostics

The Delta Forth compiler performs comprehensive static analysis and reports various compile-time errors with descriptive messages.

### Identifier should be declared outside words

**Context:** Certain language constructs (`CONSTANT`, `VARIABLE`, `ALLOT`, `LIBRARY`) must appear at file scope, outside word definitions.

**Resolution:** Move the declaration before or after word definitions.

### Identifier should be declared inside words

**Context:** Control flow keywords (`IF`, `THEN`, `ELSE`, `DO`, `LOOP`, `BEGIN`, `UNTIL`, `WHILE`, `REPEAT`) are only valid within word definitions.

**Resolution:** These constructs must appear inside `: ... ;` definitions.

### Identifier is a reserved identifier

**Context:** Attempted redefinition of a built-in primitive word.

**Resolution:** Choose a different name. Built-in words cannot be overridden.

### Identifier is an invalid identifier

**Context:** Identifier violates naming rules.

**Rules:**
- Must not begin with a digit
- Maximum length: 31 characters
- Allowed characters: letters, digits, and certain punctuation

**Resolution:** Rename the identifier to conform to syntax rules.

### Unable to define constant. Number or string required before CONSTANT

**Context:** The constant definition syntax requires a value on the compilation stack.

**Correct Usage:**
```forth
42 CONSTANT answer        \\ OK
CONSTANT invalid          \\ ERROR: no value
```

### Unable to allot variable space. Number needed before ALLOT

**Context:** `ALLOT` requires an integer specifying how many additional cells to allocate.

**Correct Usage:**
```forth
VARIABLE buffer 255 ALLOT  \\ OK: allocate 256 cells
VARIABLE X ALLOT           \\ ERROR: missing size
```

### Unexpected end of file

**Context:** Unbalanced control structure or incomplete word definition at end of file.

**Common Causes:**
- Missing `;` to terminate word definition
- Unclosed `IF` without `THEN`
- Unclosed loop construct

### Wrong constant type specified for ALLOT. Use an integer

**Context:** Attempted to use a string constant as the `ALLOT` size parameter.

**Resolution:** Only integer constants are valid for array sizing.

### Nested words are not allowed

**Context:** Attempted to define a word inside another word definition.

**Example of Error:**
```forth
: outer
  : inner  \\ ERROR: nested definition
    42
  ;
;
```

**Resolution:** End the first word with `;` before defining the next.

### Malformed control structure

**Context:** Mismatched or improperly nested control structure keywords.

**Common Issues:**
- `IF` without matching `THEN`
- `DO` without matching `LOOP`
- Control structure spanning multiple words
- Unbalanced structures at word boundary

**Resolution:** Ensure each control structure is complete and properly nested within a single word.

### Program starting point is missing. Please define the word MAIN

**Context:** Every Delta Forth program (executable or library) must define a `main` word.

**Purpose:** `main` serves as:
- Entry point for executables
- Initialization routine for libraries

**Resolution:** Add a `main` word definition to your source.

### {Constant | Variable} redefines an already defined constant or variable

**Context:** Name collision in the global namespace.

**Namespace Rules:**
- Constants and variables share a single namespace
- Each name must be unique globally
- Redefinition is prohibited

**Resolution:** Use distinct names for all global constants and variables.

## Compiler Command-Line Reference

The Delta Forth compiler provides various options to control code generation, optimization, and output format.

### Invocation Syntax

```shell
DeltaForth.exe <source-file> [options...]
```

**Parameters:**
- `<source-file>`: Primary Forth source file (must contain `main` word)
- `[options...]`: Zero or more compiler directives (case-sensitive)

### Compiler Options

#### `/NOLOGO`
**Purpose:** Suppresses compiler banner and copyright notice.

**Use Case:** Batch builds, automated scripts where output verbosity should be minimized.

**Example:**
```shell
DeltaForth.exe program.4th /NOLOGO
```

#### `/QUIET`
**Purpose:** Disables informational messages; only errors and warnings are displayed.

**Use Case:** Continuous integration builds where only failures matter.

**Behavior:** Compilation statistics, progress indicators, and success messages are suppressed.

#### `/CLOCK`
**Purpose:** Displays detailed timing statistics for each compilation phase.

**Output Example:**
```
Lexical analysis:   45ms
Parsing:           123ms
Code generation:    78ms
Assembly emission:  34ms
Total:             280ms
```

**Use Case:** Performance profiling and optimization of large codebases.

#### `/EXE` (Default)
**Purpose:** Generates a standalone console executable (`.exe`).

**Behavior:**
- Creates a console application with `main` as the entry point
- References required .NET Framework assemblies
- Output file: `<source-file>.exe`

#### `/DLL`
**Purpose:** Generates a reusable class library (`.dll`).

**Behavior:**
- Creates a .NET assembly with public methods for each word
- The `main` word becomes the library initializer
- Suitable for consumption by other .NET languages

**Example:**
```shell
DeltaForth.exe MathLib.4th /DLL
```

#### `/NOCHECK`
**Purpose:** Disables runtime stack bounds checking.

**Performance Impact:** Eliminates overhead of stack validation code (~5-10% speedup).

**Risk:** Stack overflow/underflow will corrupt memory rather than throwing exceptions.

**Recommendation:** Use only for well-tested, performance-critical code.

#### `/FS:<size>`
**Purpose:** Configures parameter stack capacity.

**Syntax:** `/FS:n` where `n` is the number of 32-bit cells.

**Default:** 524,288 cells (2MB on 32-bit systems)

**Example:**
```shell
DeltaForth.exe largeprog.4th /FS:1048576  \\ 1 million cells
```

**Consideration:** Larger stacks consume more memory but prevent stack overflow in deep recursion.

#### `/RS:<size>`
**Purpose:** Configures return stack capacity.

**Syntax:** `/RS:n` where `n` is the number of cells.

**Default:** 1,024 cells

**Example:**
```shell
DeltaForth.exe deeprecurse.4th /RS:4096
```

**Use Case:** Programs with deep recursion or heavy `>R`/`R>` usage.

#### `/MAP`
**Purpose:** Generates a symbol map file documenting the compiled assembly structure.

**Output File:** `<source-file>.map`

**Contents:**
- Global variable addresses and sizes
- Constant values and types
- External word bindings
- Memory layout information

**Use Case:** Debugging, interoperability planning, code analysis.

#### `/OUTPUT=<path>`
**Purpose:** Specifies custom output file path and name.

**Syntax:** `/OUTPUT=path\filename.ext`

**Default Behavior:** Output files are created in the source file's directory with the same base name.

**Example:**
```shell
DeltaForth.exe src\program.4th /OUTPUT=bin\Release\MyApp.exe
```

**Note:** The output directory must exist; the compiler will not create it.

#### `/KEY=<keyfile>`
**Purpose:** Strong-name signs the assembly using the specified key file.

**Prerequisites:**
- Generate a key pair using: `sn.exe -k mykey.snk`
- Keep the private key secure

**Example:**
```shell
DeltaForth.exe library.4th /DLL /KEY=CompanyKey.snk
```

**Benefits:**
- Enables GAC deployment
- Provides assembly identity verification
- Satisfies enterprise security policies

### Option Combinations

**Typical Development Build:**
```shell
DeltaForth.exe program.4th /CLOCK
```

**Release Build (optimized):**
```shell
DeltaForth.exe program.4th /NOLOGO /QUIET /NOCHECK
```

**Library with Debugging Info:**
```shell
DeltaForth.exe mathlib.4th /DLL /MAP /OUTPUT=bin\MathLib.dll
```

**Signed Library for Production:**
```shell
DeltaForth.exe securelib.4th /DLL /KEY=release.snk /NOCHECK /OUTPUT=dist\SecureLib.dll
```

---

**End of Reference Manual**
