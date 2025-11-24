/*
 * Delta Forth .NET - World's first Forth compiler for the .NET platform
 * Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro
 *
 * GitHub Repository: https://github.com/vbocan/delta-forth
 */

namespace DeltaForth.DataStructures
{    
    /// <summary>
    /// Definition of a global variable as used by the Forth syntactic analyzer 
    /// </summary>
    public class ForthVariable
    {
        /// <summary>
        /// Variable name
        /// </summary>
        public string Name {get;set;}

        /// <summary>
        /// Number of cells required by the variable
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Address of the variable (computed by the code generator)
        /// </summary>
        public int Address { get; set; }
    }
}