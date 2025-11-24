/*
 * Delta Forth .NET - World's first Forth compiler for the .NET platform
 * Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro
 *
 * GitHub Repository: https://github.com/vbocan/delta-forth
 */

namespace DeltaForth.DataStructures
{
    /// <summary>
    /// Definition of a global constant as used by the Forth syntactic analyzer
    /// </summary>
    public class ForthConstant
    {
        /// <summary>
        /// Constant name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constant value (can be either string or integer)
        /// </summary>
        public object Value { get; set; }
    }
}