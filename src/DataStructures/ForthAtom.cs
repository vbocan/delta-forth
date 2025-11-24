/*
 * Delta Forth .NET - World's first Forth compiler for the .NET platform
 * Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro
 *
 * GitHub Repository: https://github.com/vbocan/delta-forth
 */

namespace DeltaForth.DataStructures
{    
    /// <summary>
    /// Definition of an atom as used by the Forth parser 
    /// </summary>
    public class ForthAtom
    {
        /// <summary>
        /// Atom name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File where the atom occured
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Line number at which the atom occured
        /// </summary>
        public int LineNumber { get; set; }
    }
}