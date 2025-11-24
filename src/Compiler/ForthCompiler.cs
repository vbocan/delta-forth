/*
 * Delta Forth .NET - World's first Forth compiler for the .NET platform
 * Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro
 *
 * GitHub Repository: https://github.com/vbocan/delta-forth
 */

using System;
using DeltaForth.DataStructures;
using DeltaForth.Descriptors;
using DeltaForth.Parser;
using DeltaForth.SyntacticAnalyzer;

namespace DeltaForth.Compiler
{
    public class ForthCompiler
    {
        #region Event Delegate
        public delegate void CompilerEventHandler(object sender, object e);
        #endregion

        #region Compiler events
        public event CompilerEventHandler OnParsingStart;
        public event CompilerEventHandler OnParsingEnd;
        public event CompilerEventHandler OnSyntacticAnalysisStart;
        public event CompilerEventHandler OnSyntacticAnalysisEnd;
        public event CompilerEventHandler OnCodeGenerationStart;
        public event CompilerEventHandler OnCodeGenerationEnd;
        public event CompilerEventHandler OnCompilationStart;
        public event CompilerEventHandler OnCompilationEnd;
        public event CompilerEventHandler OnExecutionStart;
        public event CompilerEventHandler OnExecutionEnd;
        #endregion

        #region Event Launchers
        public void SignalCompilationStart()
        {
            if (OnCompilationStart != null)
            {
                OnCompilationStart(this, null);
            }
        }

        public void SignalCompilationEnd()
        {
            if (OnCompilationEnd != null)
            {
                OnCompilationEnd(this, null);
            }
        }

        public void SignalParsingStart(string FileName)
        {
            if (OnParsingStart != null)
            {
                OnParsingStart(this, FileName);
            }
        }

        public void SignalParsingEnd()
        {
            if (OnParsingEnd != null)
            {
                OnParsingEnd(this, null);
            }
        }

        public void SignalSyntacticAnalysisStart()
        {
            if (OnSyntacticAnalysisStart != null)
            {
                OnSyntacticAnalysisStart(this, null);
            }
        }

        public void SignalSyntacticAnalysisEnd()
        {
            if (OnSyntacticAnalysisEnd != null)
            {
                OnSyntacticAnalysisEnd(this, null);
            }
        }

        public void SignalCodeGenerationStart()
        {
            if (OnCodeGenerationStart != null)
            {
                OnCodeGenerationStart(this, null);
            }
        }

        public void SignalCodeGenerationEnd()
        {
            if (OnCodeGenerationEnd != null)
            {
                OnCodeGenerationEnd(this, null);
            }
        }

        public void SignalExecutionStart()
        {
            if (OnExecutionStart != null)
            {
                OnExecutionStart(this, null);
            }
        }

        public void SignalExecutionEnd()
        {
            if (OnExecutionEnd != null)
            {
                OnExecutionEnd(this, null);
            }
        }
        #endregion

        #region Local variables
        private CompilerMetadata _MetaData;
        #endregion

        #region Public accessors
        public CompilerMetadata MetaData { get { return _MetaData; } }        
        #endregion

        public ForthCompiler()
        {            			
			
		}

        public void CompileFile(string SourceFileName, string TargetFileName, string TargetDirectory, string SignatureFile, bool GenerateExecutable, bool GenerateStackFrames, int ForthStackSize, int ReturnStackSize)
        {
            // Compilation start
            SignalCompilationStart();

            try
            {
                // Parsing
                SignalParsingStart(SourceFileName);
                ForthParser parser = new ForthParser(SourceFileName);
                var SourceAtoms = parser.GetForthAtoms();
                SignalParsingEnd();

                // Syntactic analysis
                SignalSyntacticAnalysisStart();
                ForthSyntacticAnalyzer analyzer = new ForthSyntacticAnalyzer(SourceAtoms);
                _MetaData = analyzer.GetMetaData();
                SignalSyntacticAnalysisEnd();

                // Code generation
                SignalCodeGenerationStart();
                ForthCodeGenerator generator = new ForthCodeGenerator(MetaData, TargetFileName, TargetDirectory, SignatureFile, GenerateExecutable, GenerateStackFrames, ForthStackSize, ReturnStackSize);
                generator.DoGenerateCode();
                SignalCodeGenerationEnd();

                // Get the compiled type
                Type compiledType = generator.CreateAssembly();

                // Execute the program
                SignalExecutionStart();
                var mainMethod = compiledType.GetMethod("MAIN", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.IgnoreCase);
                if (mainMethod != null)
                {
                    mainMethod.Invoke(null, null);
                }
                SignalExecutionEnd();
                
                // Compilation end
                SignalCompilationEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }		
	}
}
