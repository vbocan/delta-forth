/*
 * Delta Forth - World's first Forth compiler for .NET
 * Copyright (C)1997-2019 Valer BOCAN, PhD, Romania (valer@bocan.ro)
 * 
 * This program and its source code is distributed in the hope that it will
 * be useful. No warranty of any kind is provided.
 * Please DO NOT distribute modified copies of the source code.
 * 
 */

namespace DeltaForth.SyntacticAnalyzer
{
    /// <summary>
    /// Types of exceptions that may be thrown by the syntactic analyzer
    /// </summary>
    public enum SyntacticExceptionType
    {
        _EDeclareOutsideWords,	// The identifier should be defined outside words
        _EDeclareInsideWords,	// The identifier should be defined inside words
        _EReservedWord,			// The specified identifier is a reserved word
        _EInvalidIdentifier,	// The specified identifier is invalid
        _EUnableToDefineConst,	// Unable to define constant since the stack is empty
        _EUnableToAllocVar,		// Unable to alloc variable space since the stack is empty
        _EUnexpectedEndOfFile,	// An unexpected end of file has occured
        _EWrongAllotConstType,	// A constant of type other than 'int' was specified for ALLOT
        _ENestedWordsNotAllowed,// A word definition occured within another word definition
        _EMalformedBWRStruct,	// Malformed BEGIN-WHILE-REPEAT struct encountered
        _EMalformedBAStruct,	// Malformed BEGIN-AGAIN struct encountered
        _EMalformedBUStruct,	// Malformed BEGIN-UNTIL struct encountered
        _EMalformedIETStruct,	// Malformed IF-ELSE-THEN struct encountered
        _EMalformedDLStruct,	// Malformed DO-LOOP/+LOOP struct encountered
        _EMalformedCOEStruct,	// Malformed CASE-OF-ENDCASE struct encountered
        _EUnfinishedControlStruct,	// Control structures must be terminated before ';'
        _EMalformedConversion,	// Malformed conversion
        _EUnfinishedConversion,	// Conversions must be finished before ';'
        _EMainNotDefined,		// Word MAIN not defined
        _EDuplicateConst,		// Duplicate constant defined
        _EDuplicateVar,			// Duplicate variable defined
    }
}
