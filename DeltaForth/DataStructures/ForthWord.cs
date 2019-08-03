/*
 * Delta Forth - World's first Forth compiler for .NET
 * Copyright (C)1997-2019 Valer BOCAN, PhD, Romania (valer@bocan.ro)
 * 
 * This program and its source code is distributed in the hope that it will
 * be useful. No warranty of any kind is provided.
 * Please DO NOT distribute modified copies of the source code.
 * 
 */

using System.Collections.Generic;

namespace DeltaForth.DataStructures
{
    /// <summary>
    /// Definition of a word as used by the Forth syntactic analyzer 
    /// </summary>
    public class ForthWord
    {
        /// <summary>
        /// Forth word name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  List of atoms that define the word
        /// </summary>
        public List<string> Definition { get; set; }

        public ForthWord()
        {
            Definition = new List<string>();
        }
    }


}