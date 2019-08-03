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
using DeltaForth.DataStructures;
using DeltaForth.Descriptors;
using DeltaForth.Parser;
using DeltaForth.SyntacticAnalyzer;

namespace DeltaForth
{
    /// <summary>
    /// Delta Forth - The .NET Forth Compiler
    /// (C) Valer BOCAN, PhD (valer@bocan.ro)
    /// 
    /// Class ForthCompiler
    /// 
    /// Date of creation:		September  5, 2001
    /// Date of last update:	October 26, 2011
    /// 
    /// Description:
    ///		The ForthCompiler class requires a Forth source file as a parameter and an output file
    ///		to generate code to.
    /// </summary>
    public class ForthCompiler
    {
        #region Event Delegate
        public delegate void CompilerEventHandler(object sender, object e);
        #endregion

        #region Compiler events
        /// <summary>
        /// Raised when parsing of the source file starts
        /// </summary>
        public event CompilerEventHandler OnParsingStart;

        /// <summary>
        /// Raised when parsing of the source file ends
        /// </summary>
        public event CompilerEventHandler OnParsingEnd;

        /// <summary>
        /// Raised when syntactic analysis starts
        /// </summary>
        public event CompilerEventHandler OnSyntacticAnalysisStart;

        /// <summary>
        /// Raised when syntactic analysis ends
        /// </summary>
        public event CompilerEventHandler OnSyntacticAnalysisEnd;

        /// <summary>
        /// Raised when code generation starts
        /// </summary>
        public event CompilerEventHandler OnCodeGenerationStart;

        /// <summary>
        /// Raised when code generation ends
        /// </summary>
        public event CompilerEventHandler OnCodeGenerationEnd;

        /// <summary>
        /// Raised when compilation starts
        /// </summary>
        public event CompilerEventHandler OnCompilationStart;

        /// <summary>
        /// Raised when compilation ends
        /// </summary>
        public event CompilerEventHandler OnCompilationEnd;

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
