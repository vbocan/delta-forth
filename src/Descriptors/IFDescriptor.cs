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
    /// Definition of an IF structure used to code IF-ELSE-THEN
    /// </summary>
    public class IFDescriptor 
	{
        /// <summary>
        /// Label for the ELSE branch
        /// </summary>
		public Label lbElse {get; set;}

        /// <summary>
        /// // TRUE if lbElse has already been used
        /// </summary>
        public bool bElse { get; set; }

        /// <summary>
        /// // Label for the end of the control struct
        /// </summary>
        public Label lbEnd { get; set; }
	}
}
