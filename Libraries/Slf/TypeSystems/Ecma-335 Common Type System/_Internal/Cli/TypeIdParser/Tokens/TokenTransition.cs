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
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Tokens
{
    
    // Module: RootModule
    internal struct TokenTransition :
        IEquatable<TokenTransition>
    {
        #region TokenTransition data members
        /// <summary>
        /// Data member for <see cref="AssemblyTerminals"/>
        /// </summary>
        private AssemblyTerminalCases assemblyTerminals;
        
        /// <summary>
        /// Data member for <see cref="CommonSymbols"/>
        /// </summary>
        private CommonSymbolCases commonSymbols;
        
        /// <summary>
        /// Data member for <see cref="Captures"/>
        /// </summary>
        private TypeIdParserTokens captures;
        
        internal static Func<TokenTransition, string> OrderingSelectorPointer = OrderingSelector;
        #endregion // TokenTransition data members
        #region TokenTransition properties
        public bool Empty
        {
            get
            {
                return (((this.assemblyTerminals == AssemblyTerminalCases.None) && (this.commonSymbols == CommonSymbolCases.None)) && (this.captures == TypeIdParserTokens.None));
            }
        }
        
        /// <summary>
        /// Enumeration set for AssemblyTerminals.
        /// </summary>
        public AssemblyTerminalCases AssemblyTerminals
        {
            get
            {
                return this.assemblyTerminals;
            }
        }
        
        /// <summary>
        /// Enumeration set for CommonSymbols.
        /// </summary>
        public CommonSymbolCases CommonSymbols
        {
            get
            {
                return this.commonSymbols;
            }
        }
        
        /// <summary>
        /// Capture rule restrictions.
        /// </summary>
        public TypeIdParserTokens Captures
        {
            get
            {
                return this.captures;
            }
        }
        #endregion // TokenTransition properties
        #region TokenTransition methods
        public static TokenTransition ExclusiveOr(TokenTransition left, TokenTransition right)
        {
            AssemblyTerminalCases assemblyTerminals = ~((left.assemblyTerminals & right.assemblyTerminals));
            CommonSymbolCases commonSymbols = ~((left.commonSymbols & right.commonSymbols));
            TypeIdParserTokens captures = ~((left.captures & right.captures));
            // Because some goofball relied on CodeDOM early on, no BitwiseExOr operator is available.
            return new TokenTransition((assemblyTerminals & (left.assemblyTerminals | right.assemblyTerminals)), (commonSymbols & (left.commonSymbols | right.commonSymbols)), (captures & (left.captures | right.captures)));
        }
        
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (this.assemblyTerminals != AssemblyTerminalCases.None)
                result.AppendFormat("AssemblyTerminals ({0})", this.assemblyTerminals);
            if (this.commonSymbols != CommonSymbolCases.None)
            {
                if (result.Length > 0)
                    result.Append(", ");
                result.AppendFormat("CommonSymbols ({0})", this.commonSymbols);
            }
            if (this.captures != TypeIdParserTokens.None)
            {
                if (result.Length > 0)
                    result.Append(", ");
                result.AppendFormat("Captures ({0})", this.captures);
            }
            return result.ToString();
        }
        
        public override int GetHashCode()
        {
            int intersection = 0;
            int result = 0;
            result = ((int)(this.assemblyTerminals));
            intersection = ~((((int)(this.commonSymbols)) & result));
            result = (intersection & (((int)(this.commonSymbols)) | result));
            intersection = ~((((int)(this.captures)) & result));
            result = (intersection & (((int)(this.captures)) | result));
            return result;
        }
        
        public static bool Equals(TokenTransition left, TokenTransition right)
        {
            if (left.assemblyTerminals != right.assemblyTerminals)
                return false;
            if (left.commonSymbols != right.commonSymbols)
                return false;
            if (left.captures != right.captures)
                return false;
            return true;
        }
        
        public bool Equals(TokenTransition other)
        {
            if (this.assemblyTerminals != other.assemblyTerminals)
                return false;
            if (this.commonSymbols != other.commonSymbols)
                return false;
            if (this.captures != other.captures)
                return false;
            return true;
        }
        
        private static string OrderingSelector(TokenTransition target)
        {
            return target.ToString();
        }
        #endregion // TokenTransition methods
        #region TokenTransition .ctors
        public TokenTransition(AssemblyTerminalCases assemblyTerminals)
             : this(assemblyTerminals, CommonSymbolCases.None, TypeIdParserTokens.None)
        {
        }
        
        public TokenTransition(CommonSymbolCases commonSymbols)
             : this(AssemblyTerminalCases.None, commonSymbols, TypeIdParserTokens.None)
        {
        }
        
        public TokenTransition(TypeIdParserTokens captures)
             : this(AssemblyTerminalCases.None, CommonSymbolCases.None, captures)
        {
        }
        
        public TokenTransition(AssemblyTerminalCases assemblyTerminals, CommonSymbolCases commonSymbols, TypeIdParserTokens captures)
             : this()
        {
            this.assemblyTerminals = assemblyTerminals;
            this.commonSymbols = commonSymbols;
            this.captures = captures;
        }
        #endregion // TokenTransition .ctors
        public static implicit operator TokenTransition(AssemblyTerminalCases source)
        {
            return new TokenTransition(source);
        }
        public static implicit operator TokenTransition(CommonSymbolCases source)
        {
            return new TokenTransition(source);
        }
        public static implicit operator TokenTransition(TypeIdParserTokens source)
        {
            return new TokenTransition(source);
        }
        public static TokenTransition operator |(TokenTransition leftSide,TokenTransition rightSide)
        {
            return new TokenTransition((leftSide.assemblyTerminals | rightSide.assemblyTerminals), (leftSide.commonSymbols | rightSide.commonSymbols), (leftSide.captures | rightSide.captures));
        }
        public static TokenTransition operator &(TokenTransition leftSide,TokenTransition rightSide)
        {
            return new TokenTransition((leftSide.assemblyTerminals & rightSide.assemblyTerminals), (leftSide.commonSymbols & rightSide.commonSymbols), (leftSide.captures & rightSide.captures));
        }
        public static bool operator ==(TokenTransition leftSide,TokenTransition rightSide)
        {
            if (leftSide.assemblyTerminals != rightSide.assemblyTerminals)
                return false;
            if (leftSide.commonSymbols != rightSide.commonSymbols)
                return false;
            if (leftSide.captures != rightSide.captures)
                return false;
            return true;
        }
        public static bool operator !=(TokenTransition leftSide,TokenTransition rightSide)
        {
            if (leftSide.assemblyTerminals != rightSide.assemblyTerminals)
                return true;
            if (leftSide.commonSymbols != rightSide.commonSymbols)
                return true;
            if (leftSide.captures != rightSide.captures)
                return true;
            return false;
        }
    }
}
 /* -----------------------------------------------------------------------------\
 |  This file took 00:00:00.0038266 to generate.                                 |
 |  Date generated: 4/8/2013 2:00:48 AM                                          |
 |  There were 10 types used by this file                                        |
 |  AssemblyTerminalCases, CommonSymbolCases, TypeIdParserTokens,                |
 |  Func`2[[TokenTransition],[System.String]], TokenTransition, System.String,   |
 |  System.Boolean, StringBuilder, System.Int32,                                 |
 |  IEquatable`1[[TokenTransition]]                                              |
 |-------------------------------------------------------------------------------|
 |  There were 1 assemblies referenced:                                          |
 |  mscorlib                                                                     |
 \----------------------------------------------------------------------------- */
