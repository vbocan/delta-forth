<div align="center">

# Delta Forth .NET

### World's First Forth Compiler for the .NET Platform

[![.NET Version](https://img.shields.io/badge/.NET-10.0-blue.svg)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)
[![Version](https://img.shields.io/badge/version-2.0-brightgreen.svg)](https://github.com/vbocan/delta-forth)
[![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux%20%7C%20macOS-lightgrey.svg)](https://dotnet.microsoft.com/)

**A true Forth compiler that generates .NET IL bytecode**  
*Compiled performance • Cross-platform • Immediate execution*

[Features](#features) •
[Quick Start](#quick-start) •
[Examples](#examples) •
[Documentation](#documentation) •
[Testimonials](#testimonials)

</div>

---

## What is Delta Forth?

Delta Forth is a **compiled** Forth implementation for .NET that combines the elegance of Forth with the power of the .NET platform. Unlike traditional interpreters, Delta Forth compiles your Forth source code directly to .NET Intermediate Language (IL), providing compiled performance with the convenience of immediate execution.

**Write once, run anywhere** - Your compiled Forth programs run identically on Windows, Linux, and macOS thanks to the cross-platform .NET runtime.

### Why Delta Forth?

- **Compiled Performance** - Your Forth code is compiled to .NET IL, not interpreted
- **Immediate Execution** - Compile and run in one step, no separate build process
- **True Cross-Platform** - Runs on Windows, Linux, and macOS with identical behavior
- **Modern Platform** - Built on .NET 10, the latest cross-platform runtime
- **Educational** - Perfect for learning stack-based programming and compiler design
- **Historical Significance** - Continues a legacy started in 1997 with the first Forth compiler for Java

## Features

- **True Compilation** - Generates .NET IL bytecode for fast execution
- **In-Memory Execution** - Compile and run Forth programs instantly
- **Stack-Based Architecture** - Traditional Forth data and return stacks
- **Control Structures** - IF-ELSE-THEN, DO-LOOP, BEGIN-UNTIL, CASE, and more
- **Variables & Constants** - Support for global and local variables
- **Modern Tooling** - Integrated with .NET SDK and toolchain
- **Platform Independent** - Single codebase runs on Windows, Linux, and macOS

## Quick Start

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later (available for Windows, Linux, and macOS)

### Installation

```bash
git clone https://github.com/vbocan/delta-forth.git
cd delta-forth
dotnet build src/DeltaForth
```

### Your First Program

Create `hello.4th`:

```forth
: main
  ." Hello, Forth World!" cr
;
```

Run it (works identically on Windows, Linux, and macOS):

```bash
# From the repository root
dotnet run --project src -- hello.4th

# Or from the src directory
cd src
dotnet run -- hello.4th
```

Output:

```
Delta Forth .NET Compiler, Version 2.0
World's first Forth compiler for the .NET platform
Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP

Assembly 'hello.exe' compiled successfully in memory.
Executing program...

Hello, Forth World!
```

## Examples

Delta Forth includes 16 example programs to help you learn:

### Basic Examples
- **hello-world.4th** - The classic first program
- **variables.4th** - Working with variables
- **constants.4th** - Defining and using constants
- **dots.4th** - Stack debugging with `.S`

### Control Structures
- **if-else-then.4th** - Conditional branching
- **do-loop.4th** - Counted loops
- **begin-until.4th** - Post-test loops
- **begin-while-repeat.4th** - Pre-test loops
- **case-endcase.4th** - Multi-way selection

### Algorithms
- **euclid.4th** - GCD using Euclid's algorithm
- **prime-numbers.4th** - Sieve of Eratosthenes
- **hanoi.4th** - Towers of Hanoi solver
- **permutations.4th** - Permutation generator

Run any example (platform independent):

```bash
# Works on Windows, Linux, and macOS
dotnet run --project src -- examples/forth-examples/euclid.4th
```

## Documentation

- **[Reference Guide](docs/Delta%20Forth%20-%20Reference%20Guide.md)** - Complete language documentation
- **[Examples README](examples/README.md)** - Detailed guide to example programs
- **[Website](https://www.bocan.ro)** - More information and resources

## About Forth

Forth is a procedural, stack-oriented programming language created by Chuck Moore in the early 1970s. It features:

- **Interactive Development** - Immediate execution of commands
- **Extensibility** - Define new words that become part of the language
- **Simplicity** - Minimal syntax, powerful primitives
- **Efficiency** - Close to the metal, ideal for embedded systems

Forth has been used in:
- Open Firmware boot ROMs (Apple, IBM, Sun)
- FreeBSD boot controller
- Embedded systems and instrument control
- Space applications (including NASA missions)

## The Delta Forth Project

Delta Forth is a non-standard Forth dialect optimized for the .NET environment. While it differs from traditional Forth implementations (being a true compiler rather than a compreter), it preserves Forth's essential character: staying close to the machine while providing powerful control structures.

**Key Differences:**
- Compiles to .NET IL rather than threaded code
- In-memory execution rather than disk-based .exe generation (Version 2.0+)
- Some traditional Forth words (STATE, COMPILE, IMMEDIATE) are not applicable
- Optimized for modern .NET runtime
- Cross-platform by design - runs on Windows, Linux, and macOS

**Historical Note:** Delta Forth builds on a legacy dating back to 1997, when it began as the world's first Forth compiler for Java. The .NET version was developed in remarkably short time - Beta 1 was released in just six weeks!

## Cross-Platform Capabilities

Delta Forth leverages the .NET platform to provide **true cross-platform capability**:

- **Same source code** - Write your Forth programs once
- **Same compiler** - Single codebase compiles on all platforms
- **Same behavior** - Identical execution on Windows, Linux, and macOS
- **No platform-specific code** - Pure .NET IL generation works everywhere

Whether you're developing on Windows, running on a Linux server, or teaching on macOS, Delta Forth provides a consistent, reliable Forth environment.

## Testimonials

> **Tim Sneath**, .NET Developer Group, Microsoft Corp.  
> *"Well done on your Forth compiler for .NET. It must be over fifteen years since I last touched Forth, but it's a great example of how the .NET Framework supports stack-based languages well."*

> **Brad Merrill**, Microsoft Corp.  
> *"I work with all of our .NET language partners, and noticed your recent announcement of your Forth compiler […]"*

> **Chris Maunder**, CodeProject  
> *"Excellent work Valer - I'm most impressed!"*

> **Howard Harawitz**, Author of HTML Assistant Pro  
> *"I taught Forth during most of the eighties. It's a wonderful language and many of my students loved it. I'm glad to see another modern incarnation. Congratulations!"*

> **Dennis Misener**  
> *"What a delight to trip across your Delta Forth .NET. I was wondering who'd have the first FORTH .NET offering. I need wonder no more. […] Keep up the good work."*

> **Cherng Chin**, Educator, Taiwan  
> *"I teach Forth and C# in Providence in Taiwan and how wonderful I could tell students Forth has a .Net version that could be used with C#. Thanks a lot!"*

> **Jim Shaw**  
> *"Thanks for keeping FORTH alive on .NET"*

## Command-Line Options

```bash
# Suppress the logo banner
dotnet run --project src/DeltaForth -- program.4th /NOLOGO

# Suppress compilation progress messages
dotnet run --project src/DeltaForth -- program.4th /QUIET

# Customize stack sizes (advanced users)
dotnet run --project src/DeltaForth -- program.4th /FS:1048576 /RS:2048

# Disable stack bounds checking (not recommended)
dotnet run --project src/DeltaForth -- program.4th /NOCHECK
```

**Note**: Stack size options are for advanced users exploring Forth's stack architecture. Default values work well for all included examples.

## License

Delta Forth is released under the MIT License. See [LICENSE](LICENSE) file for details.

Copyright © 1997-2025 Valer BOCAN, PhD, CSSLP

## Author

**Valer BOCAN, PhD, CSSLP**  
[www.bocan.ro](https://www.bocan.ro)

## Contributing

If you find Delta Forth useful, please consider giving it a star on GitHub.

---

<div align="center">

**Delta Forth .NET** - Bringing Forth to the modern era, one stack at a time

Made with ❤️ by Valer BOCAN

</div>