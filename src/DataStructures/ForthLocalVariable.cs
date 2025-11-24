/*
 * Delta Forth .NET - World's first Forth compiler for the .NET platform
 * Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro
 *
 * GitHub Repository: https://github.com/vbocan/delta-forth
 */

namespace DeltaForth.DataStructures
{
    /// <summary>
    /// Definition of a local variable as used by the Forth syntactic analyzer
    /// </summary>
    public class ForthLocalVariable
    {
        /// <summary>
        /// Variable name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Word where the variable has been defined
        /// </summary>
        public string WordName { get; set; }

        /// <summary>
        /// Address of the variable (computed by the code generator)
        /// </summary>
        public int Address { get; set; }
    }
}