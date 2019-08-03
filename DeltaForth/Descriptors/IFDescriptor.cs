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
