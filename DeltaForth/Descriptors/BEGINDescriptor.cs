/*
 * Delta Forth - World's first Forth compiler for .NET
 * Copyright (C)1997-2019 Valer BOCAN, PhD, Romania (valer@bocan.ro)
 * 
 * This program and its source code is distributed in the hope that it will
 * be useful. No warranty of any kind is provided.
 * Please DO NOT distribute modified copies of the source code.
 * 
 */

using System.Reflection.Emit;

namespace DeltaForth.Descriptors
{
    /// <summary>
    /// Definition of a structure used to code BEGIN-AGAIN, BEGIN-UNTIL, BEGIN-WHILE-REPEAT
    /// </summary>
    public class BEGINDescriptor
	{
        /// <summary>
        /// Label for BEGIN
        /// </summary>
        public Label lbBegin { get; set; }
        
        /// <summary>
        /// Label for END
        /// </summary>
        public Label lbEnd { get; set; }
	}
}
