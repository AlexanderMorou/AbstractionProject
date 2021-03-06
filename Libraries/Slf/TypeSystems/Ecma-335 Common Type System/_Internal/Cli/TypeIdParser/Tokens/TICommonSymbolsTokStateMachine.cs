 /* -----------------------------------------------------------\
 |  This code was generated by Oilexer.                        |
 |  Version: 1.0.0.0                                           |
 |-------------------------------------------------------------|
 |  To ensure the code works properly,                         |
 |  please do not make any changes to the file.                |
 |-------------------------------------------------------------|
 |  The specific language is C# (Runtime version: v4.0.30319)  |
 |  Sub-tool Name: Oilexer.CSharpCodeTranslator                |
 |  Sub-tool Version: 1.0.0.0                                  |
 \----------------------------------------------------------- */
using System;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Tokens;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Tokens
{
    // Module: RootModule
    internal class TICommonSymbolsTokStateMachine
    {
        #region TICommonSymbolsTokStateMachine data members
        /// <summary>
        /// Data member which tracks the current execution path of the state machine.
        /// </summary>
        private int state;

        /// <summary>
        /// Data member which denotes the final execution point of the state machine.
        /// </summary>
        /// <remarks>
        /// Only set when an edge in the state machine is hit.
        /// </remarks>
        private ExitStates exitState;

        /// <summary>
        /// Defines the allowable range for the overall series
        /// </summary>
        private CommonSymbolCases AllowedCommonSymbols;
        #endregion // TICommonSymbolsTokStateMachine data members
        #region TICommonSymbolsTokStateMachine properties
        public int BytesConsumed
        {
            get
            {
                switch (this.exitState)
                {
                    case ExitStates.Period:
                    case ExitStates.Comma:
                    case ExitStates.LeftSquareBracket:
                    case ExitStates.RightSquareBracket:
                    case ExitStates.Equals:
                    case ExitStates.NestingQualifier:
                    case ExitStates.GenericParameterSignal:
                    case ExitStates.PointerCallout:
                    case ExitStates.ByRefCallout:
                    case ExitStates.QuoteChar:
                        return 1;
                        break;
                }
                return 0;
            }
        }

        public bool IsValidEndState
        {
            get
            {
                int _temp_exitState = ((int)(this.exitState));
                if ((_temp_exitState > 0) && (_temp_exitState <= 10))
                    return true;
                return false;
            }
        }
        #endregion // TICommonSymbolsTokStateMachine properties
        #region TICommonSymbolsTokStateMachine methods
        public bool Next(char @char)
        {
            switch (this.state)
            {
                case 0:
                    {
                        switch (@char)
                        {
                            case '.':
                                // .
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.Period) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.Period;
                                    this.state = -1;
                                }
                                break;
                            case ',':
                                // ,
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.Comma) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.Comma;
                                    this.state = -1;
                                }
                                break;
                            case '[':
                                // [
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.LeftSquareBracket) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.LeftSquareBracket;
                                    this.state = -1;
                                }
                                break;
                            case ']':
                                // ]
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.RightSquareBracket) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.RightSquareBracket;
                                    this.state = -1;
                                }
                                break;
                            case '=':
                                // =
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.Equals) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.Equals;
                                    this.state = -1;
                                }
                                break;
                            case '+':
                                // +
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.NestingQualifier) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.NestingQualifier;
                                    this.state = -1;
                                }
                                break;
                            case '`':
                                // `
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.GenericParameterSignal) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.GenericParameterSignal;
                                    this.state = -1;
                                }
                                break;
                            case '*':
                                // *
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.PointerCallout) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.PointerCallout;
                                    this.state = -1;
                                }
                                break;
                            case '&':
                                // &
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.ByRefCallout) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.ByRefCallout;
                                    this.state = -1;
                                }
                                break;
                            case '"':
                                // "
                                if ((this.AllowedCommonSymbols & CommonSymbolCases.QuoteChar) != CommonSymbolCases.None)
                                {
                                    this.exitState = ExitStates.QuoteChar;
                                    this.state = -1;
                                }
                                break;
                        }
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Resets the state machine to its default state.
        /// </summary>
        public void Reset(CommonSymbolCases allowedCommonSymbols)
        {
            this.AllowedCommonSymbols = allowedCommonSymbols;
            this.exitState = ExitStates.None;
            this.state = 0;
        }

        public void Reset()
        {
            this.Reset(CommonSymbolCases.All);
        }

        public void Inject(TypeIdParserScanData data)
        {
            if (this.IsValidEndState)
                switch (this.exitState)
                {
                    case ExitStates.ByRefCallout:
                        // Exit point for ByRefCallout.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.ByRefCallout) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.ByRefCallout);
                        break;
                    case ExitStates.Comma:
                        // Exit point for Comma.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.Comma) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.Comma);
                        break;
                    case ExitStates.Equals:
                        // Exit point for Equals.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.Equals) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.Equals);
                        break;
                    case ExitStates.GenericParameterSignal:
                        // Exit point for GenericParameterSignal.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.GenericParameterSignal) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.GenericParameterSignal);
                        break;
                    case ExitStates.LeftSquareBracket:
                        // Exit point for LeftSquareBracket.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.LeftSquareBracket) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.LeftSquareBracket);
                        break;
                    case ExitStates.NestingQualifier:
                        // Exit point for NestingQualifier.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.NestingQualifier) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.NestingQualifier);
                        break;
                    case ExitStates.Period:
                        // Exit point for Period.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.Period) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.Period);
                        break;
                    case ExitStates.PointerCallout:
                        // Exit point for PointerCallout.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.PointerCallout) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.PointerCallout);
                        break;
                    case ExitStates.QuoteChar:
                        // Exit point for QuoteChar.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.QuoteChar) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.QuoteChar);
                        break;
                    case ExitStates.RightSquareBracket:
                        // Exit point for RightSquareBracket.
                        if ((this.AllowedCommonSymbols & CommonSymbolCases.RightSquareBracket) != CommonSymbolCases.None)
                            data.AddCommonSymbols(CommonSymbolCases.RightSquareBracket);
                        break;
                }
        }
        #endregion // TICommonSymbolsTokStateMachine methods

        #region TICommonSymbolsTokStateMachine nested types

        private enum ExitStates
        {
            None,

            /// <summary>
            /// Used to express the exit-state for the Period case.
            /// </summary>
            /// <remarks>
            /// Original definition: '.':Period;
            /// </remarks>
            Period = 1,

            /// <summary>
            /// Used to express the exit-state for the Comma case.
            /// </summary>
            /// <remarks>
            /// Original definition: ',':Comma;
            /// </remarks>
            Comma = 2,

            /// <summary>
            /// Used to express the exit-state for the LeftSquareBracket case.
            /// </summary>
            /// <remarks>
            /// Original definition: '[':LeftSquareBracket;
            /// </remarks>
            LeftSquareBracket = 3,

            /// <summary>
            /// Used to express the exit-state for the RightSquareBracket case.
            /// </summary>
            /// <remarks>
            /// Original definition: ']':RightSquareBracket;
            /// </remarks>
            RightSquareBracket = 4,

            /// <summary>
            /// Used to express the exit-state for the Equals case.
            /// </summary>
            /// <remarks>
            /// Original definition: '=':Equals;
            /// </remarks>
            Equals = 5,

            /// <summary>
            /// Used to express the exit-state for the NestingQualifier case.
            /// </summary>
            /// <remarks>
            /// Original definition: '+':NestingQualifier;
            /// </remarks>
            NestingQualifier = 6,

            /// <summary>
            /// Used to express the exit-state for the GenericParameterSignal case.
            /// </summary>
            /// <remarks>
            /// Original definition: '`':GenericParameterSignal;
            /// </remarks>
            GenericParameterSignal = 7,

            /// <summary>
            /// Used to express the exit-state for the PointerCallout case.
            /// </summary>
            /// <remarks>
            /// Original definition: '*':PointerCallout;
            /// </remarks>
            PointerCallout = 8,

            /// <summary>
            /// Used to express the exit-state for the ByRefCallout case.
            /// </summary>
            /// <remarks>
            /// Original definition: '&':ByRefCallout;
            /// </remarks>
            ByRefCallout = 9,

            /// <summary>
            /// Used to express the exit-state for the QuoteChar case.
            /// </summary>
            /// <remarks>
            /// Original definition: '"':QuoteChar;
            /// </remarks>
            QuoteChar = 10
        }
        #endregion // TICommonSymbolsTokStateMachine nested types
    }
}
 /* ------------------------------------------------------------------------------\
 |  This file took 00:00:00.0010160 to generate.                                  |
 |  Date generated: 4/8/2013 7:25:03 PM                                           |
 |  There were 7 types used by this file                                          |
 |  System.Int32, TICommonSymbolsTokStateMachine+ExitStates, CommonSymbolCases,   |
 |  System.Boolean, System.Char, System.Void,                                     |
 |  TypeIdParserScanData                                                          |
 |--------------------------------------------------------------------------------|
 |  There were 1 assemblies referenced:                                           |
 |  mscorlib                                                                      |
 \------------------------------------------------------------------------------ */
