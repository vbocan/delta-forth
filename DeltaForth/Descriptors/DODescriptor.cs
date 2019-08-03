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
    /// Definition of a structure used to code DO-LOOP/+LOOP structure
    /// </summary>
    public class DODescriptor
	{
        /// <summary>
        /// Label for DO
        /// </summary>
        public Label lbDo { get; set; }

        /// <summary>
        /// Label for LOOP
        /// </summary>
        public Label lbLoop { get; set; }
	}
}
