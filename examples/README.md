# Delta Forth Examples

This directory contains example programs demonstrating various features of Delta Forth, a **compiled** Forth implementation for .NET.

## About Delta Forth

Delta Forth is a **true compiler**, not an interpreter. It compiles Forth source code to .NET IL (Intermediate Language) and executes it in-memory using the .NET runtime. This provides:

- **Fast execution** - Compiled code, not interpreted
- **Cross-platform** - Runs identically on Windows, Linux, and macOS
- **Modern platform** - Built on .NET 10
- **Immediate feedback** - Compile and run in one step

## Running Examples

Simply run the compiler with a Forth source file. Works on all platforms:

```bash
# From the examples/forth-examples directory
cd forth-examples
dotnet run --project ../../src -- hello-world.4th

# From the repository root
dotnet run --project src -- examples/forth-examples/hello-world.4th

# From the src directory
cd src
dotnet run -- ../examples/forth-examples/hello-world.4th
```

The compiler will:
1. Parse and compile your Forth code to .NET IL
2. Execute the `main` word immediately

## Forth Examples

All `.4th` files demonstrate different language features.

### Basic Examples

| File | Description |
|------|-------------|
| `hello-world.4th` | Classic "Hello World" program - your first Delta Forth program |
| `variables.4th` | Demonstrates global and local variable usage |
| `constants.4th` | Shows how to define and use integer and string constants |
| `dots.4th` | Stack debugging example using the `.S` word |

### Control Structures

| File | Description |
|------|-------------|
| `if-else-then.4th` | Conditional branching with IF-ELSE-THEN |
| `do-loop.4th` | Counted loops with DO-LOOP and +LOOP |
| `begin-again.4th` | Infinite loop structure |
| `begin-until.4th` | Post-test loop (executes at least once) |
| `begin-while-repeat.4th` | Pre-test loop with condition |
| `case-endcase.4th` | Multi-way selection (switch/case equivalent) |

### Algorithms & Mathematics

| File | Description |
|------|-------------|
| `euclid.4th` | Euclidean algorithm for finding GCD (Greatest Common Divisor) |
| `prime-numbers.4th` | Prime number generator using Sieve of Eratosthenes |
| `leap-years.4th` | Leap year calculation |
| `permutations.4th` | Generate permutations of a sequence |
| `hanoi.4th` | Towers of Hanoi puzzle solver (recursive algorithm) |
| `fizzbuzz.4th` | Classic FizzBuzz challenge with statistics |
| `fibonacci.4th` | Fibonacci sequence calculator using arrays |
| `factorial.4th` | Factorial calculator using stack-based iteration |
| `multiplication-table.4th` | 12x12 multiplication table using nested loops |
| `binary-search.4th` | Binary search in sorted arrays |

### Interactive Programs

| File | Description |
|------|-------------|
| `guess-the-number.4th` | Number guessing game with user input |

## Modern Algorithm Showcase

The following new examples demonstrate modern programming concepts and algorithms in DeltaForth:

**fizzbuzz.4th** - The classic programming interview question. Prints numbers 1-100 with "Fizz" for multiples of 3, "Buzz" for multiples of 5, and "FizzBuzz" for multiples of both. Includes statistics tracking showing 6 FizzBuzz, 27 Fizz, 13 Buzz, and 53 regular numbers.

**multiplication-table.4th** - Generates a complete 12x12 multiplication table using nested DO-LOOP structures. Perfect demonstration of nested iteration and formatted output in Forth.

**fibonacci.4th** - Calculates Fibonacci numbers using an array-based approach. Pre-computes the sequence once and stores it in an array for efficient lookup. Demonstrates F(0) through F(30) (832,040) without overflow.

**factorial.4th** - Simple factorial calculator using pure stack-based iteration. Calculates factorials from 0! to 12! (479,001,600), demonstrating proper use of loop counters and accumulation patterns.

**binary-search.4th** - Implements the binary search algorithm for finding elements in sorted arrays. Shows efficient O(log n) searching with proper handling of the search space and termination conditions.

## Learning Path

Recommended order for beginners:

1. **Start Simple**: `hello-world.4th` → `variables.4th` → `constants.4th`
2. **Control Flow**: `if-else-then.4th` → `do-loop.4th` → `begin-until.4th`
3. **Modern Classics**: `fizzbuzz.4th` → `multiplication-table.4th` → `factorial.4th`
4. **Algorithms**: `euclid.4th` → `prime-numbers.4th` → `fibonacci.4th` → `binary-search.4th`
5. **Advanced**: `hanoi.4th` → `permutations.4th`

## How It Works

Delta Forth is a **compiler**, not an interpreter:

1. **Parsing**: Your Forth source code is parsed into an abstract syntax tree
2. **Compilation**: The compiler generates .NET IL (Intermediate Language) bytecode
3. **Execution**: The compiled code runs directly on the .NET runtime

This means your Forth programs run with compiled performance, not interpreted overhead. The same compiled IL runs identically on Windows, Linux, and macOS.

## Common Compiler Options

```bash
# Quiet mode (suppress informational messages)
dotnet run --project src -- examples/forth-examples/program.4th /QUIET

# Show compilation timing
dotnet run --project src -- examples/forth-examples/program.4th /CLOCK

# Disable logo
dotnet run --project src -- examples/forth-examples/program.4th /NOLOGO

# Set custom stack sizes
dotnet run --project src -- examples/forth-examples/program.4th /FS:1048576 /RS:2048
```

## Cross-Platform Verification

All examples have been designed to work identically across platforms:

- **Windows** - Tested with .NET 10 SDK
- **Linux** - Compatible with .NET 10 runtime
- **macOS** - Compatible with .NET 10 runtime

The .NET IL bytecode generated by Delta Forth is platform-independent, ensuring consistent behavior across all operating systems.

## Need Help?

- See the **[Reference Guide](../docs/Delta%20Forth%20-%20Reference%20Guide.md)** for complete language documentation
- Visit **https://www.bocan.ro** for more information

---

**Note**: Delta Forth compiles your code to .NET IL and executes it in-memory. This provides the performance of compiled code with the convenience of immediate execution, and works identically on Windows, Linux, and macOS.
