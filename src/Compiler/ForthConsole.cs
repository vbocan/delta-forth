/*
 * Delta Forth .NET - World's first Forth compiler for the .NET platform
 * Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro
 *
 * GitHub Repository: https://github.com/vbocan/delta-forth
 */

using System;
using System.IO;
using DeltaForth.DataStructures;

namespace DeltaForth.Compiler
{
    public class ForthConsole
    {
        #region Compiler command-line option values
        static bool DisplayLogo, QuietMode, CheckStack;
        static int ForthStackSize = 524288, ReturnStackSize = 1024;

        static string SourceFile;		// Input filename
        #endregion

        #region Display Methods
        static void DisplayLineToConsole(string Text, ConsoleColor Color)
        {
            if (!QuietMode)
            {
                var OldConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = Color;
                Console.WriteLine(Text);
                Console.ForegroundColor = OldConsoleColor;
            }
        }

        static void DisplayLineToConsole(string Text)
        {
            DisplayLineToConsole(Text, ConsoleColor.Gray);
        }

        static void DisplayToConsole(string Text, ConsoleColor Color)
        {
            if (!QuietMode)
            {
                var OldConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = Color;
                Console.Write(Text);
                Console.ForegroundColor = OldConsoleColor;
            }
        }

        static void DisplayToConsole(string Text)
        {
            DisplayToConsole(Text, ConsoleColor.Gray);
        }

        static void DisplayLogoToConsole()
        {
            DisplayLineToConsole("Delta Forth .NET Compiler, Version 2.0", ConsoleColor.Green);
            DisplayLineToConsole("World's first Forth compiler for the .NET platform", ConsoleColor.White);
            DisplayLineToConsole("Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro\n\r");            
        }

        static void DisplayUsageToConsole()
        {
            DisplayLogoToConsole();
            DisplayLineToConsole("Usage: DeltaForth.exe <source file> [options]", ConsoleColor.Yellow);
            DisplayLineToConsole("\n\rOptions:");
            DisplayLineToConsole("/NOLOGO\t\t\tDon't display the logo");
            DisplayLineToConsole("/QUIET\t\t\tSuppress compilation progress messages");
            DisplayLineToConsole("/NOCHECK\t\tDisable stack bounds checking (not recommended)");
            DisplayLineToConsole("/FS:<size>\t\tSpecify Forth stack size (default: 524288 cells)");
            DisplayLineToConsole("/RS:<size>\t\tSpecify return stack size (default: 1024 cells)");
            DisplayLineToConsole("\n\rDefault source file extension is .4th", ConsoleColor.Green);
        }
        #endregion

        static void Main(string[] args)
        {
            // Initialize default parameter values
            DisplayLogo = CheckStack = true;
            QuietMode = false;


            // Display usage screen if no parameters are given
            if (args.Length < 1)
            {
                DisplayUsageToConsole();
                return;
            }

            SourceFile = args[0];

            // Cycle through command line parameters
            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i].ToUpper())
                {
                    case "/NOLOGO":
                        DisplayLogo = false;
                        break;
                    case "/QUIET":
                        QuietMode = true;
                        break;
                    case "/NOCHECK":
                        CheckStack = false;
                        break;
                    default:
                        if (args[i].ToUpper().StartsWith("/FS:"))
                        {
                            try
                            {
                                ForthStackSize = Convert.ToInt32(args[i].Substring(4));
                            }
                            catch (FormatException)
                            {
                                DisplayUsageToConsole();
                                return;
                            }
                        }
                        else
                                if (args[i].ToUpper().StartsWith("/RS:"))
                        {
                            try
                            {
                                ReturnStackSize = Convert.ToInt32(args[i].Substring(4));
                            }
                            catch (FormatException)
                            {
                                DisplayUsageToConsole();
                                return;
                            }
                        }
                        else
                        {
                            DisplayUsageToConsole();
                            return;
                        }
                        break;
                }
            }

            // Process parameters
            if (DisplayLogo) DisplayLogoToConsole();

            #region Input File Processing
            // If the extension for the input file is missing, add it
            if (!Path.HasExtension(SourceFile))
            {
                SourceFile = Path.ChangeExtension(SourceFile, ".4th");
            }
            #endregion

            #region Input File Existence Check
            // Check whether the input file really exists
            if (!File.Exists(SourceFile))
            {
                DisplayLineToConsole(string.Format("\n\rERROR: The file '{0}' could not be found.", SourceFile), ConsoleColor.Red);
                return;
            }
            #endregion

            #region Output File Processing
            // Generate output file name based on input file (for display purposes)
            string OutputFile = Path.GetFileName(SourceFile);
            OutputFile = Path.ChangeExtension(OutputFile, ".exe");
            string OutputDirectory = Path.GetDirectoryName(SourceFile);
            if (string.IsNullOrEmpty(OutputDirectory))
            {
                OutputDirectory = ".";
            }
            #endregion

            ForthCompiler compiler = new();
            compiler.OnCodeGenerationStart += new ForthCompiler.CompilerEventHandler(OnCodeGenerationStart);
            compiler.OnCodeGenerationEnd += new ForthCompiler.CompilerEventHandler(OnCodeGenerationEnd);
            compiler.OnCompilationStart += new ForthCompiler.CompilerEventHandler(OnCompilationStart);
            compiler.OnCompilationEnd += new ForthCompiler.CompilerEventHandler(OnCompilationEnd);
            compiler.OnParsingEnd += new ForthCompiler.CompilerEventHandler(OnParsingEnd);
            compiler.OnParsingStart += new ForthCompiler.CompilerEventHandler(OnParsingStart);
            compiler.OnSyntacticAnalysisEnd += new ForthCompiler.CompilerEventHandler(OnSyntacticAnalysisEnd);
            compiler.OnSyntacticAnalysisStart += new ForthCompiler.CompilerEventHandler(OnSyntacticAnalysisStart);
            compiler.OnExecutionStart += new ForthCompiler.CompilerEventHandler(OnExecutionStart);
            compiler.OnExecutionEnd += new ForthCompiler.CompilerEventHandler(OnExecutionEnd);

            try
            {
                compiler.CompileFile(SourceFile, OutputFile, OutputDirectory, "", true, CheckStack, ForthStackSize, ReturnStackSize);
            }
            catch (Exception e)
            {
                DisplayLineToConsole("FAILED", ConsoleColor.Red);
                DisplayLineToConsole(string.Format("\n\rCompilation error: {0}", e.Message), ConsoleColor.White);
                return;
            }
        }

        #region Event Handlers
        static void OnSyntacticAnalysisStart(object sender, object e)
        {
            DisplayToConsole("Analyzing syntax");
        }

        static void OnSyntacticAnalysisEnd(object sender, object e)
        {
            DisplayLineToConsole(" OK", ConsoleColor.Green);
        }

        static void OnParsingStart(object sender, object e)
        {
            DisplayToConsole("Parsing source file");
        }

        static void OnParsingEnd(object sender, object e)
        {

            DisplayLineToConsole(" OK", ConsoleColor.Green);

        }

        static void OnCompilationStart(object sender, object e)
        {
            DisplayLineToConsole("Compiling", ConsoleColor.White);            
        }

        static void OnCompilationEnd(object sender, object e)
        {
            // Empty - final status is shown after execution
        }
        
        static void OnCodeGenerationStart(object sender, object e)
        {
            DisplayToConsole("Generating IL code");
        }
        
        static void OnCodeGenerationEnd(object sender, object e)
        {
            DisplayLineToConsole(" OK", ConsoleColor.Green);
        }

        static void OnExecutionStart(object sender, object e)
        {
            DisplayLineToConsole("Build succeeded!", ConsoleColor.Green);
            DisplayLineToConsole("Executing program...\n", ConsoleColor.White);
        }

        static void OnExecutionEnd(object sender, object e)
        {            
        }
        #endregion
    }
}
