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
