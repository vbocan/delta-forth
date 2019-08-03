/*
 * Delta Forth - World's first Forth compiler for .NET
 * Copyright (C)1997-2019 Valer BOCAN, PhD, Romania (valer@bocan.ro)
 * 
 * This program and its source code is distributed in the hope that it will
 * be useful. No warranty of any kind is provided.
 * Please DO NOT distribute modified copies of the source code.
 * 
 */

using System;
using System.IO;

namespace DeltaForth
{
    public class ForthConsole
    {
        #region Compiler command-line option values
		static bool DisplayLogo, QuietMode, ShowTimings, GenerateExecutable, CheckStack, DisplayMap;
		static int ForthStackSize = 524288, ReturnStackSize = 1024;

		static string SourceFile;		// Input filename
		static string OutputFile;	    // Output filename
		static string OutputDirectory;	// Output directory
		static string SignatureFile;	// Signature file (snk)        
        #endregion

        #region Local variables
        static DateTime CompilationTimeStart, CompilationTimeEnd;
        static DateTime PartialTimeStart, PartialTimeEnd;
        #endregion

        #region Display Methods
        /// <summary>
        /// Display text on the console with the specified color
        /// </summary>
        /// <param name="Text">Text to display to console</param>
        /// <param name="Color">Color of the text</param>
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

        /// <summary>
        /// Display text on the console with the default color
        /// </summary>
        /// <param name="Text">Text to display to console</param>
        static void DisplayLineToConsole(string Text)
        {
            DisplayLineToConsole(Text, ConsoleColor.Gray);
        }

        /// <summary>
        /// Display text on the console with the specified color
        /// </summary>
        /// <param name="Text">Text to display to console</param>
        /// <param name="Color">Color of the text</param>
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

        /// <summary>
        /// Display text on the console with the default color
        /// </summary>
        /// <param name="Text">Text to display to console</param>
        static void DisplayToConsole(string Text)
        {
            DisplayToConsole(Text, ConsoleColor.Gray);
        }

        static void DisplayLogoToConsole()
		{
            DisplayLineToConsole("Delta Forth .NET Compiler, Version 1.4", ConsoleColor.Yellow);
            DisplayLineToConsole("World's first Forth compiler for the .NET platform", ConsoleColor.White);
            DisplayLineToConsole("Copyright (C)1997-2019 Valer BOCAN, PhD <valer@bocan.ro>. All Rights Reserved.");
            DisplayLineToConsole("Web: http://www.bocan.ro/deltaforthnet\n\r");
		}

		static void DisplayUsageToConsole()
		{
			DisplayLogoToConsole();
            DisplayLineToConsole("Usage: DeltaForth.exe <source file> [options]", ConsoleColor.Yellow);
            DisplayLineToConsole("\n\rOptions:");
            DisplayLineToConsole("/NOLOGO\t\t\tDon't type the logo");
            DisplayLineToConsole("/QUIET\t\t\tDon't report compiling progress");
            DisplayLineToConsole("/CLOCK\t\t\tMeasure and report compilation times");
            DisplayLineToConsole("/EXE\t\t\tCompile to EXE (default)");
            DisplayLineToConsole("/DLL\t\t\tCompile to DLL");
            DisplayLineToConsole("/NOCHECK\t\tDisable stack bounds checking");
            DisplayLineToConsole("/FS:<size>\t\tSpecify Forth stack size (default is 524288 cells)");
            DisplayLineToConsole("/RS:<size>\t\tSpecify return stack size (default is 1024 cells)");
            DisplayLineToConsole("/MAP\t\t\tGenerate detailed map information");
            DisplayLineToConsole("/OUTPUT=<targetfile>\tCompile to file with specified name\n\r\t\t\t(user must provide extension, if any)");            
            DisplayLineToConsole("/KEY=<keyfile>\t\tCompile with strong signature\n\r\t\t\t(<keyfile> contains private key)");
            DisplayLineToConsole("\n\rDefault source file extension is .4th", ConsoleColor.Green);
		}
        #endregion        

        static void DisplayMapInformation(ForthCompiler compiler)
        {
            DisplayLineToConsole(string.Empty);
            DisplayLineToConsole("Summary of compilation objects", ConsoleColor.White);
            
            // Display global constants
            DisplayLineToConsole(string.Empty);
            DisplayLineToConsole("Global constants:", ConsoleColor.Blue);
            foreach (var fc in compiler.MetaData.GlobalConstants)
            {
                if (fc.Value.GetType() != typeof(string))
                {
                    DisplayLineToConsole(string.Format("{0} = {1}", fc.Name, fc.Value));
                }
                else
                {
                    DisplayLineToConsole(string.Format("{0} = \"{1}\"", fc.Name, fc.Value));
                }
            }

            // Display global variables
            DisplayLineToConsole(string.Empty);
            DisplayLineToConsole("Global variables:", ConsoleColor.Blue);
            foreach (var fv in compiler.MetaData.GlobalVariables)
            {
                DisplayLineToConsole(string.Format("{0} = (Addr:{1}, Size:{2})", fv.Name, fv.Address, fv.Size));
            }

            // Display local variables
            DisplayLineToConsole(string.Empty);
            DisplayLineToConsole("Local variables:", ConsoleColor.Blue);
            foreach (var flv in compiler.MetaData.LocalVariables)
            {
                DisplayLineToConsole(string.Format("{0} = (Addr:{1}, Word:{2})", flv.Name, flv.Address, flv.WordName));
            }

            // Display words
            DisplayLineToConsole(string.Empty);
            DisplayLineToConsole("Words:", ConsoleColor.Blue);
            foreach (var ew in compiler.MetaData.Words)
            {
                DisplayLineToConsole(string.Format("-> {0}", ew.Name));
            }

            // Display external words
            DisplayLineToConsole(string.Empty);
            DisplayLineToConsole("External words:", ConsoleColor.Blue);
            foreach (var ew in compiler.MetaData.ExternalWords)
            {
                DisplayLineToConsole(string.Format("{0} = (Library:{1}, Class:{2}, Method:{3})", ew.Name, ew.Library, ew.Class, ew.Method));
            }
        }

        static void Main(string[] args)
		{
			// Initialize default parameter values
			DisplayLogo = GenerateExecutable = CheckStack = true;
			QuietMode = ShowTimings = DisplayMap = false;
			OutputFile = OutputDirectory = SignatureFile = string.Empty;            

			// Display usage screen if no parameters are given
			if(args.Length < 1)
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
                    case "/CLOCK":
                        ShowTimings = true;
                        break;
                    case "/EXE":
                        GenerateExecutable = true;
                        break;
                    case "/DLL":
                        GenerateExecutable = false;
                        break;
                    case "/NOCHECK":
                        CheckStack = false;
                        break;
                    case "/MAP":
                        DisplayMap = true;
                        break;
                    default:
                        if (args[i].ToUpper().StartsWith("/OUTPUT="))
                        {
                            OutputFile = args[i].Substring(8);
                        }
                        else
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
                                    if (args[i].ToUpper().StartsWith("/KEY="))
                                    {
                                        SignatureFile = args[i].Substring(5);
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
            // If there is no output file specified, generate a name for it based on the input file
            if (OutputFile == string.Empty)
            {
                OutputFile = Path.GetFileName(SourceFile);
                OutputFile = Path.ChangeExtension(OutputFile, GenerateExecutable ? ".exe" : ".dll");
                OutputDirectory = Path.GetDirectoryName(SourceFile);
                if (OutputDirectory == string.Empty)
                {
                    OutputDirectory = ".";
                }
            }
            else
            {
                OutputDirectory = Path.GetDirectoryName(OutputFile);
                if (OutputDirectory == string.Empty)
                    OutputDirectory = null;
                OutputFile = Path.GetFileName(OutputFile);                
            }            
            #endregion            
			
            ForthCompiler compiler = new ForthCompiler();
            compiler.OnCodeGenerationStart      += new ForthCompiler.CompilerEventHandler(compiler_OnCodeGenerationStart);
            compiler.OnCodeGenerationEnd        += new ForthCompiler.CompilerEventHandler(compiler_OnCodeGenerationEnd);
            compiler.OnCompilationStart         += new ForthCompiler.CompilerEventHandler(compiler_OnCompilationStart);
            compiler.OnCompilationEnd           += new ForthCompiler.CompilerEventHandler(compiler_OnCompilationEnd);
            compiler.OnParsingEnd               += new ForthCompiler.CompilerEventHandler(compiler_OnParsingEnd);
            compiler.OnParsingStart             += new ForthCompiler.CompilerEventHandler(compiler_OnParsingStart);
            compiler.OnSyntacticAnalysisEnd     += new ForthCompiler.CompilerEventHandler(compiler_OnSyntacticAnalysisEnd);
            compiler.OnSyntacticAnalysisStart   += new ForthCompiler.CompilerEventHandler(compiler_OnSyntacticAnalysisStart);

			try 
			{                
				compiler.CompileFile(SourceFile, OutputFile, OutputDirectory, SignatureFile, GenerateExecutable, CheckStack, ForthStackSize, ReturnStackSize);
			} 
			catch(Exception e)
			{
                DisplayLineToConsole("\t\tFAILED", ConsoleColor.Red);
                DisplayLineToConsole(string.Format("\n\rCompilation error: {0}", e.Message), ConsoleColor.White);                
				return;
			}
            
            if(DisplayMap)
                DisplayMapInformation(compiler);
		}

        #region Event Handlers
        static void compiler_OnSyntacticAnalysisStart(object sender, object e)
        {
            PartialTimeStart = DateTime.Now;
            DisplayToConsole("Starting syntactic analysis...");
        }

        static void compiler_OnSyntacticAnalysisEnd(object sender, object e)
        {
            PartialTimeEnd = DateTime.Now;
            PartialTimeEnd = DateTime.Now;
            if (ShowTimings)
            {
                DisplayToConsole("\t\tOK", ConsoleColor.Green);
                DisplayLineToConsole("\t(" + Math.Round((PartialTimeEnd - PartialTimeStart).TotalMilliseconds) + " ms)");
            }
            else
            {
                DisplayLineToConsole("\t\tOK", ConsoleColor.Green);
            }
        }

        static void compiler_OnParsingStart(object sender, object e)
        {
            PartialTimeStart = DateTime.Now;
            DisplayToConsole("Parsing source file...");
        }

        static void compiler_OnParsingEnd(object sender, object e)
        {
            PartialTimeEnd = DateTime.Now;
            if (ShowTimings)
            {
                DisplayToConsole("\t\t\tOK", ConsoleColor.Green);
                DisplayLineToConsole("\t(" + Math.Round((PartialTimeEnd - PartialTimeStart).TotalMilliseconds) + " ms)");
            }
            else
            {
                DisplayLineToConsole("\t\t\tOK", ConsoleColor.Green);
            }
        }

        static void compiler_OnCompilationStart(object sender, object e)
        {
            DisplayLineToConsole("Starting compilation...", ConsoleColor.White);
            DisplayLineToConsole(string.Empty);
            CompilationTimeStart = DateTime.Now;
        }

        static void compiler_OnCompilationEnd(object sender, object e)
        {
            CompilationTimeEnd = DateTime.Now;
            if (ShowTimings)
            {
                DisplayLineToConsole(string.Empty);
                DisplayToConsole("Compilation ended.", ConsoleColor.White);
                DisplayLineToConsole("\t\t\t\t(" + Math.Round((CompilationTimeEnd - CompilationTimeStart).TotalMilliseconds) + " ms)");
            }
            else
            {
                DisplayLineToConsole(string.Empty);
                DisplayLineToConsole("Compilation ended successfully.", ConsoleColor.White);
            }
        }

        static void compiler_OnCodeGenerationEnd(object sender, object e)
        {
            PartialTimeEnd = DateTime.Now;
            if (ShowTimings)
            {
                DisplayToConsole("\t\tOK", ConsoleColor.Green);
                DisplayLineToConsole("\t(" + Math.Round((PartialTimeEnd - PartialTimeStart).TotalMilliseconds) + " ms)");
            }
            else
            {
                DisplayLineToConsole("\t\tOK", ConsoleColor.Green);
            }
        }

        static void compiler_OnCodeGenerationStart(object sender, object e)
        {
            PartialTimeStart = DateTime.Now;
            DisplayToConsole("Generating executable code...");
        }
        #endregion
    }		
}
