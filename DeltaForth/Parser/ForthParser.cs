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
using System.Text.RegularExpressions;
using DeltaForth.DataStructures;
using System.Collections.Generic;
using System.Linq;

namespace DeltaForth.Parser
{
    /// <summary>
    /// The ForthParser class takes a input file that contains Forth code and extracts the atoms. An atom is the smallest semantic unit that has a meaning of its own.
    /// </summary>
    /// <remarks>
    /// Date of creation:       September 5, 2001
    /// Date of last update:    October 25, 2011
    /// </remarks>
    internal class ForthParser
    {
        #region Local variables
        /// <summary>
        /// Comment mode (TRUE if atoms should be ignored)
        /// </summary>
        private bool CommentMode = false;

        /// <summary>
        /// List of files already processed (so we don't process the same file multiple times in case of circular references)
        /// </summary>
        private HashSet<string> ProcessedFiles;
		      
        /// <summary>
        /// Name of the file to parse(it may include LOAD directives case in which other files will be processed as well)
        /// </summary>
        private string SourceFileName;
        #endregion
        
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="SourceFileName">Source file with Forth code.</param>
		public ForthParser(string SourceFileName)
		{			
			ProcessedFiles = new HashSet<string>();
            this.SourceFileName = SourceFileName;
		}

        /// <summary>
        /// Get the list of Forth atoms from the input source file
        /// </summary>
        /// <returns>List of Forth atoms</returns>
        public List<ForthAtom> GetForthAtoms()
        {
            // Prcess the source file
            var SourceAtoms = ProcessSourceFile(SourceFileName);
            // Process (any) LOAD directives
            var ProcessedSourceAtoms = ProcessLoadDirectives(SourceAtoms);

            return ProcessedSourceAtoms;
        }

        /// <summary>
        /// Parse a file containing Forth source code.
        /// </summary>
        /// <param name="SourceFileName">File name of the source code.</param>
        /// <returns>An list of ForthAtom objects found in the file.</returns>
		private List<ForthAtom> ProcessSourceFile(string SourceFileName)
		{
			// Check whether we already visited this file
            if (ProcessedFiles.Contains(SourceFileName))
            {
                throw new Exception(string.Format("Circular dependency in LOAD directives for file {0}.", SourceFileName));
            }

			var AtomList = new List<ForthAtom>();

            try
            {
                int LineNumber = 1;
                StreamReader source = new StreamReader(SourceFileName);
                string line = source.ReadLine();
                while (line != null)
                {
                    var ParsedAtoms = ProcessSourceLine(line, SourceFileName, LineNumber++);
                    AtomList.AddRange(ParsedAtoms);
                    line = source.ReadLine();
                }
                source.Close();
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Invalid file specified by the LOAD directive: {0}", SourceFileName));
            }

			// Add the file to the processed file list
			ProcessedFiles.Add(SourceFileName);

			return AtomList;
		}
		
        /// <summary>
        /// Parse the source line containing Forth code.
        /// </summary>
        /// <param name="ForthSourceCode">A line containing some Forth code</param>
        /// <param name="SourceFileName">File name of the source code (for reporting in case of error).</param>
        /// <param name="LineNumber">Line number in the file name with the source code (for reporting in case of error).</param>
        /// <returns>An enumeration of ForthAtom objects found in the line.</returns>
		private IEnumerable<ForthAtom> ProcessSourceLine(string ForthSourceCode, string SourceFileName, int LineNumber)
		{
			string atom = string.Empty;

			// The regular expression matches display strings (." "), dump strings (" ") and individual atoms
			Regex reg = new Regex("\\.\"[^\"]*\"|\"[^\"]*\"|\\S+");
			Match match = reg.Match(ForthSourceCode);
			while (match.Success)
			{
				atom = match.ToString();	// Get the current atom

                // Deal with the single line comment (if found, drop the line)
                if (atom.StartsWith(@"\"))
                {
                    yield break;
                }

				// Avoid entering comment mode
				// if the atom is in the form '(comment)'
				if(atom.StartsWith("(") && atom.EndsWith(")"))
				{
					match = match.NextMatch();	// Advance to the next atom
					continue;
				}
				if(atom.StartsWith("("))  // Begin multi-line comment
				{
					CommentMode = true;
					match = match.NextMatch();	// Advance to the next atom
					continue;
				}
				if(atom.EndsWith(")"))  // End multi-line comment
				{
						CommentMode = false;
						match = match.NextMatch();	// Advance to the next atom
						continue;
				}

                if (!CommentMode)
                {
                    yield return new ForthAtom { Name = atom, FileName = SourceFileName, LineNumber = LineNumber };
                }
				match = match.NextMatch();
			}
		}
		
        /// <summary>
        /// Parses the atom list and recursively processes the LOAD directives
        /// </summary>
        /// <param name="ForthAtoms">List of Forth atoms that may possibily include LOAD directives</param>
        /// <returns>List of Forth atoms with expanded definitions of LOAD directives</returns>
        private List<ForthAtom> ProcessLoadDirectives(List<ForthAtom> ForthAtoms)
		{
            var ProcessedForthAtoms = new List<ForthAtom>();

            for (int i = 0; i < ForthAtoms.Count(); i++)
            {
                var CurrentAtom = ForthAtoms.ElementAt(i);
                // Check whether the current atom is a LOAD directive
                if (CurrentAtom.Name.ToUpper() != "LOAD")
                {
                    ProcessedForthAtoms.Add(ForthAtoms.ElementAt(i));
                    continue;
                }

                string FileToLoad = string.Empty;
                try
                {
                    FileToLoad = ForthAtoms.ElementAt(i + 1).Name;
                }
                catch (Exception)
                {
                    throw new Exception("No file name specified after the LOAD directive. (" + CurrentAtom.FileName + ", line " + CurrentAtom.LineNumber + ")");	// Signal error if no file name is supplied
                }

                var NewAtoms = ProcessSourceFile(FileToLoad);
                ProcessedForthAtoms.AddRange(NewAtoms);
            }

            return ProcessedForthAtoms;
		}
	}
}
