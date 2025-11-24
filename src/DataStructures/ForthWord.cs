/*
 * Delta Forth .NET - World's first Forth compiler for the .NET platform
 * Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro
 *
 * GitHub Repository: https://github.com/vbocan/delta-forth
 */

using System.Collections.Generic;

namespace DeltaForth.DataStructures
{
    /// <summary>
    /// Definition of a word as used by the Forth syntactic analyzer 
    /// </summary>
    public class ForthWord
    {
        /// <summary>
        /// Forth word name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  List of atoms that define the word
        /// </summary>
        public List<string> Definition { get; set; }

        public ForthWord()
        {
            Definition = new List<string>();
        }
    }


}