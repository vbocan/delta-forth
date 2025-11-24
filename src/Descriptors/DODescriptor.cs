/*
 * Delta Forth .NET - World's first Forth compiler for the .NET platform
 * Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro
 *
 * GitHub Repository: https://github.com/vbocan/delta-forth
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
