/*
 * Delta Forth - World's first Forth compiler for .NET
 * Copyright (C)1997-2019 Valer BOCAN, PhD, Romania (valer@bocan.ro)
 * 
 * This program and its source code is distributed in the hope that it will
 * be useful. No warranty of any kind is provided.
 * Please DO NOT distribute modified copies of the source code.
 * 
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