/*
 * Delta Forth .NET - World's first Forth compiler for the .NET platform
 * Copyright (C) 1997-2025 Valer BOCAN, PhD, CSSLP | www.bocan.ro
 *
 * GitHub Repository: https://github.com/vbocan/delta-forth
 */

namespace DeltaForth.DataStructures
{
    /// <summary>
    /// Definition of an external word as used by the Forth syntactic analyzer 
    /// </summary>
    public class ExternalWord
    {
        /// <summary>
        /// Word name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Library filename
        /// </summary>
        public string Library { get; set; }

        /// <summary>
        /// Class name
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Method name
        /// </summary>
        public string Method { get; set; }
    }
}