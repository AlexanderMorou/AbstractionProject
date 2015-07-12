using System;
using System.Collections.Generic;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser;
using System.IO;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Rules;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Tokens;
using AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser.Tokens;
using System.Data;
using AllenCopeland.Abstraction.Globalization;
using System.Text;

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.TypeIdParser
{
    // Module: RootModule
    internal partial class TypeIdentityParser
    {

        #region TypeIdentityParser data members
        private Scanner currentScanner;
        private static TokenTransition LookAhead_ArrayRankCheck = TokenTransitions.CommonSymbols.Comma | TokenTransitions.CommonSymbols.RightSquareBracket;
        private static TokenTransition LookAhead_TypeParameterCheck_TopLevel = TokenTransitions.CommonSymbols.Comma | TokenTransitions.CommonSymbols.LeftSquareBracket | TokenTransitions.CommonSymbols.NestingQualifier | TokenTransitions.CommonSymbols.PointerCallout | TokenTransitions.CommonSymbols.ByRefCallout;
        private static TokenTransition LookAhead_TypeParameterCheck_TypeParameter =
            TokenTransitions.CommonSymbols.Comma |
            TokenTransitions.CommonSymbols.LeftSquareBracket |
            TokenTransitions.CommonSymbols.NestingQualifier |
            TokenTransitions.CommonSymbols.PointerCallout |
            TokenTransitions.CommonSymbols.ByRefCallout |
            TokenTransitions.CommonSymbols.RightSquareBracket;

        private static TokenTransition LookAhead_TypeParameterCheck_PostNest_TopLevel = TokenTransitions.CommonSymbols.Comma | TokenTransitions.CommonSymbols.LeftSquareBracket | TokenTransitions.CommonSymbols.PointerCallout | TokenTransitions.CommonSymbols.ByRefCallout;
        private static TokenTransition LookAhead_TypeParameterCheck_PostNest_TypeParameter =
            TokenTransitions.CommonSymbols.Comma |
            TokenTransitions.CommonSymbols.LeftSquareBracket |
            TokenTransitions.CommonSymbols.PointerCallout |
            TokenTransitions.CommonSymbols.ByRefCallout |
            TokenTransitions.CommonSymbols.RightSquareBracket;

        private static TokenTransition LookAhead_ElementClassifications_TopLevel =
            TokenTransitions.CommonSymbols.Comma |
            TokenTransitions.CommonSymbols.LeftSquareBracket |
            TokenTransitions.CommonSymbols.PointerCallout |
            TokenTransitions.CommonSymbols.ByRefCallout;

        private static TokenTransition LookAhead_ElementClassifications_TypeParameter =
            TokenTransitions.CommonSymbols.Comma |
            TokenTransitions.CommonSymbols.LeftSquareBracket |
            TokenTransitions.CommonSymbols.PointerCallout |
            TokenTransitions.CommonSymbols.ByRefCallout |
            TokenTransitions.CommonSymbols.RightSquareBracket;

        private static TokenTransition LookAhead_TypeParameterCheck_First = TokenTransitions.CommonSymbols.LeftSquareBracket | TokenTransitions.Captures.QualifiedIdentifier;
        private static TokenTransition LookAhead_TypeParameterCheck_Follow = TokenTransitions.CommonSymbols.Comma | TokenTransitions.CommonSymbols.RightSquareBracket;
        private static TokenTransition LookAhead_AssemblyIdentity_TopLevel = TokenTransitions.Captures.QualifiedAssemblyIdentifier | TokenTransitions.CommonSymbols.QuoteChar;
        private static TokenTransition LookAhead_AssemblyIdentity_Nested = TokenTransitions.Captures.NestedQualifiedAssemblyIdentifier | TokenTransitions.CommonSymbols.QuoteChar;
        private static TokenTransition LookAhead_AssemblyIdentity_KeyToken = TokenTransitions.Captures.HexQWord | TokenTransitions.AssemblyTerminals.NullPublicKeyToken;
        private static TokenTransition LookAhead_ArrayCheck =
            //starting a type-parameter.
            TokenTransitions.CommonSymbols.LeftSquareBracket |
            //an array qualifier.
            TokenTransitions.CommonSymbols.Comma |
            TokenTransitions.CommonSymbols.RightSquareBracket |
            TokenTransitions.Captures.QualifiedIdentifier;
        private static TokenTransition LookAhead_TopLevelAssemblyQualifierCheck =
            TokenTransitions.CommonSymbols.Comma;
        private static TokenTransition LookAhead_NestedAssemblyQualifierCheck =
            TokenTransitions.CommonSymbols.Comma |
            TokenTransitions.CommonSymbols.RightSquareBracket;
        #endregion // TypeIdentityParser data members

        private TIAssemblyIdentityRule originatingAssembly;

        public TypeIdentityParser(string typeIdentity, TIAssemblyIdentityRule originatingAssembly)
        {
            this.originatingAssembly = originatingAssembly;
            this.currentScanner = new Scanner(new StringReader(typeIdentity));
        }

        public ITIAssemblyIdentityRule ParseAssemblyIdentity()
        {
            return ParseAssemblyIdentityInternal();
        }

        private ITIAssemblyIdentityRule ParseAssemblyIdentityInternal(bool insideTypeParameter = false)
        {

            /* *
             * Annoying truth of AssemblyQualifiedNames, commas are legal,
             * as are [, ], = and so on.
             * The rules for ] being escaped changes though.
             * */
            var quotedInfo = ParseCaptureOrSubset(insideTypeParameter ? LookAhead_AssemblyIdentity_Nested : LookAhead_AssemblyIdentity_TopLevel);
            string assemblyName = null;
            if (quotedInfo.Item3)
            {
                assemblyName = quotedInfo.Item1;
            }
            else
            {
                assemblyName = ParseCapture(insideTypeParameter ? TokenTransitions.Captures.NestedQualifiedAssemblyIdentifier : TokenTransitions.Captures.QualifiedAssemblyIdentifier);
                ParseSubset(TokenTransitions.CommonSymbols.QuoteChar);
            }
            ParseSubset(TokenTransitions.CommonSymbols.Comma);
            SkipWhitespace();
            ParseSubset(TokenTransitions.AssemblyTerminals.Version);
            SkipWhitespace();
            ParseSubset(TokenTransitions.CommonSymbols.Equals);
            SkipWhitespace();
            var version = ParseVersion();
            ParseSubset(TokenTransitions.CommonSymbols.Comma);
            SkipWhitespace();
            ParseSubset(TokenTransitions.AssemblyTerminals.Culture);
            SkipWhitespace();
            ParseSubset(TokenTransitions.CommonSymbols.Equals);
            SkipWhitespace();
            var cultureIdParse = ParseCapture(TokenTransitions.Captures.CultureIdentifier);
            var cultureId = CultureIdentifiers.GetIdentifierByName(cultureIdParse == "neutral" ? string.Empty : cultureIdParse);
            ParseSubset(TokenTransitions.CommonSymbols.Comma);
            SkipWhitespace();
            ParseSubset(TokenTransitions.AssemblyTerminals.KeyToken);
            SkipWhitespace();
            ParseSubset(TokenTransitions.CommonSymbols.Equals);
            var publicKeyTokenParse = ParseCaptureOrSubset(LookAhead_AssemblyIdentity_KeyToken);
            if (publicKeyTokenParse.Item3)
                return new TIAssemblyIdentityRule(assemblyName, version, cultureId, ParsePublicKeyBytes(publicKeyTokenParse.Item1));
            else
                return new TIAssemblyIdentityRule(assemblyName, version, cultureId, null);
        }

        private byte[] ParsePublicKeyBytes(string p)
        {
            if (p.Length != 16)
                return null;
            byte[] result = new byte[8];
            for (int i = 0; i < 16; i += 2)
                result[i / 2] = Convert.ToByte(p.Substring(i, 2), 16);
            return result;
        }

        private Tuple<string, TypeIdParserScanData.SubsetEntry, bool> ParseCaptureOrSubset(TokenTransition expected, bool advance = true, bool error = true)
        {
            var capture = ParseCaptureInternal(expected, false, advance);
            TypeIdParserScanData.SubsetEntry subset = null;
            if (capture == null)
                subset = ParseSubset(expected, advance, error);
            else
                return new Tuple<string, TypeIdParserScanData.SubsetEntry, bool>(capture.Capture, null, true);
            return new Tuple<string, TypeIdParserScanData.SubsetEntry, bool>(null, subset, false);
        }

        private void SkipWhitespace()
        {
            var whitespaceScan = currentScanner.NextToken(TokenTransitions.Captures.Whitespace);
            if (whitespaceScan.Count == 1)
                currentScanner.Accept(whitespaceScan[0]);
        }

        public ITIQualifiedTypeNameRule ParseQualifiedTypeName()
        {
            return ParseQualifiedTypeNameInternal();
        }

        private ITIQualifiedTypeNameRule ParseQualifiedTypeNameInternal(bool isTypeParameter = false, bool isInternalType = false)
        {
            var type = ParseTypeIdentityInternal(isTypeParameter);
            if (!isInternalType)
            {
                var assemblyQualifierComma = ParseSubset(isTypeParameter ? LookAhead_NestedAssemblyQualifierCheck : LookAhead_TopLevelAssemblyQualifierCheck, false, false);
                if (assemblyQualifierComma != null && assemblyQualifierComma.GetTransition().CommonSymbols == CommonSymbolCases.Comma)
                {
                    currentScanner.Accept(assemblyQualifierComma);
                    SkipWhitespace();
                    return new TIQualifiedTypeNameRule(type, ParseAssemblyIdentityInternal(isTypeParameter));
                }
            }
            return new TIQualifiedTypeNameRule(type, originatingAssembly);
        }

        public ITITypeIdentityRule ParseTypeIdentity()
        {
            return ParseTypeIdentityInternal();
        }

        private ITITypeIdentityRule ParseTypeIdentityInternal(bool isTypeParameter = false)
        {
            var fullName = GetTypeAndNamespace(ParseCapture(TokenTransitions.Captures.QualifiedIdentifier));
            List<string> names = new List<string>() { fullName.Item2 };
            var @namespace = fullName.Item1;
            var currentValid = isTypeParameter ? LookAhead_TypeParameterCheck_TypeParameter : LookAhead_TypeParameterCheck_TopLevel;
        typeParamPart:
            var typeParamCheck = ParseSubset(currentValid, false, false);
            List<ITITypeParameterIdentityRule> typeParameters = new List<ITITypeParameterIdentityRule>();
            List<ITIElementClassificationRule> elementClassifications = new List<ITIElementClassificationRule>();
            if (typeParamCheck == null)
                goto parseElementClassifications;
            switch ((CommonSymbolCases)typeParamCheck.SubsetValue)
            {
                case CommonSymbolCases.RightSquareBracket: //<-- Only valid when type is a type-parameter and assembly name is omitted.
                    if (!isTypeParameter)
                        throw new SyntaxErrorException();
                    return new TITypeIdentityRule(names, @namespace);
                case CommonSymbolCases.Comma:
                    return new TITypeIdentityRule(names, @namespace);
                case CommonSymbolCases.LeftSquareBracket:
                    /* *
                     * Now we check for another left-bracket, right-bracket, or comma.
                     * */
                    var originalLocation = currentScanner.Accept(typeParamCheck);
                    var arrayOrTypeParamCheckEither = ParseCaptureOrSubset(LookAhead_ArrayCheck, false);
                    if (arrayOrTypeParamCheckEither.Item3)
                    {
                        currentScanner.Reject(originalLocation);
                        currentScanner.Accept(typeParamCheck);
                        typeParameters.AddRange(ParseTypeParameterSet());
                    }
                    else
                    {
                        var arrayOrTypeParamCheck = arrayOrTypeParamCheckEither.Item2;
                        switch ((CommonSymbolCases)arrayOrTypeParamCheck.SubsetValue)
                        {
                            case CommonSymbolCases.Comma:
                            case CommonSymbolCases.RightSquareBracket:
                                /* *
                                 * This is an array qualifier.
                                 * */
                                currentScanner.Reject(originalLocation);
                                goto parseElementClassifications;
                            case CommonSymbolCases.LeftSquareBracket:
                                //Starting a type-parameter.
                                typeParameters.AddRange(ParseTypeParameterSet());
                                break;
                            default:
                                throw new SyntaxErrorException();
                        }
                    }
                    break;
                case CommonSymbolCases.NestingQualifier:
                    currentValid = isTypeParameter ? LookAhead_TypeParameterCheck_PostNest_TypeParameter : LookAhead_TypeParameterCheck_PostNest_TopLevel;
                    currentScanner.Accept(typeParamCheck);
                AppendNext:
                    names.Add(ParseCapture(TokenTransitions.Captures.QualifiedIdentifier));
                    if (ParseSubset(TokenTransitions.CommonSymbols.NestingQualifier, true, false) != null)
                        goto AppendNext;
                    goto typeParamPart;
                case CommonSymbolCases.PointerCallout:
                    goto parseElementClassifications;
                case CommonSymbolCases.ByRefCallout:
                    elementClassifications.Add(new TIElementClassificationRule(TypeElementClassification.Pointer));
                    return new TITypeIdentityRule(names, @namespace, 0, elementClassifications);
                default:
                    throw new SyntaxErrorException();
            }
        parseElementClassifications:
            var elementClassificationCheck = ParseSubset(isTypeParameter ? LookAhead_ElementClassifications_TypeParameter : LookAhead_ElementClassifications_TopLevel, false, false);
            if (elementClassificationCheck != null)
                switch ((CommonSymbolCases)elementClassificationCheck.SubsetValue)
                {
                    case CommonSymbolCases.LeftSquareBracket:
                        currentScanner.Accept(elementClassificationCheck);
                        int rank = 1;
                    rankCheck:
                        var currentRankCheck = ParseSubset(LookAhead_ArrayRankCheck);
                        var currentSymbol = ((CommonSymbolCases)currentRankCheck.SubsetValue);
                        if (currentSymbol == CommonSymbolCases.Comma)
                        {
                            rank++;
                            goto rankCheck;
                        }
                        elementClassifications.Add(new TIElementClassificationRule(rank));
                        goto parseElementClassifications;
                    case CommonSymbolCases.PointerCallout:
                        currentScanner.Accept(elementClassificationCheck);
                        elementClassifications.Add(new TIElementClassificationRule(TypeElementClassification.Pointer));
                        goto parseElementClassifications;
                    case CommonSymbolCases.ByRefCallout:
                        currentScanner.Accept(elementClassificationCheck);
                        elementClassifications.Add(new TIElementClassificationRule(TypeElementClassification.Reference));
                        break;
                }
            return new TITypeIdentityRule(names, @namespace, typeParameters.Count, typeParameters.Count == 0 ? null : typeParameters, elementClassifications.Count == 0 ? null : elementClassifications);
        }

        private ITITypeParameterIdentityRule[] ParseTypeParameterSet()
        {
            List<ITITypeParameterIdentityRule> result = new List<ITITypeParameterIdentityRule>();
            bool first = true;
        parseNext:
            var initialEither = ParseCaptureOrSubset(first ? LookAhead_TypeParameterCheck_First : LookAhead_TypeParameterCheck_Follow, false);
            if (initialEither.Item3)
                result.Add(ParseTypeParameterIdentity());
            else
            {
                var initial = initialEither.Item2;
                switch ((CommonSymbolCases)initial.SubsetValue)
                {
                    case CommonSymbolCases.Comma:

                        currentScanner.Accept(initial);
                        SkipWhitespace();
                        result.Add(ParseTypeParameterIdentity());
                        break;
                    case CommonSymbolCases.LeftSquareBracket:
                        result.Add(ParseTypeParameterIdentity());
                        SkipWhitespace();
                        break;
                    case CommonSymbolCases.RightSquareBracket:
                        currentScanner.Accept(initial);
                        return result.ToArray();
                    default:
                        throw new SyntaxErrorException();
                }

            }
            if (first)
                first = false;
            goto parseNext;
        }

        private Tuple<string, string> GetTypeAndNamespace(string fullName)
        {
            int startingPoint = fullName.Length - 1;
        repeatCheck:
            int periodIndex = fullName.LastIndexOf('.', startingPoint);
            if (periodIndex == -1)
                return new Tuple<string, string>(null, fullName);
            if (periodIndex == 0)
                return new Tuple<string, string>(string.Empty, fullName.Substring(1));
            if (fullName[periodIndex - 1] == '\\')
            {
                startingPoint = periodIndex - 1;
                goto repeatCheck;
            }
            return new Tuple<string, string>(fullName.Substring(0, periodIndex), fullName.Substring(periodIndex + 1));
        }

        private TypeIdParserScanData.SubsetEntry ParseSubset(TokenTransition expected, bool advance = true, bool error = true)
        {
            var subsetScan = currentScanner.NextToken(expected);
            if (subsetScan.Count == 1)
            {
                if (advance)
                    currentScanner.Accept(subsetScan[0]);
                return (TypeIdParserScanData.SubsetEntry)subsetScan[0];
            }
            else if (error)
                throw new SyntaxErrorException();
            return null;
        }

        private string ParseCapture(TokenTransition expected, bool error = true, bool advance = true)
        {
            return ParseCaptureInternal(expected, error, advance).Capture;
        }

        private TypeIdParserScanData.CaptureEntry ParseCaptureInternal(TokenTransition expected, bool error = true, bool advance = true)
        {
            var captureScan = currentScanner.NextToken(expected & (TypeIdParserTokens.CultureIdentifier | TypeIdParserTokens.HexByte | TypeIdParserTokens.HexQWord | TypeIdParserTokens.NestedQualifiedAssemblyIdentifier | TypeIdParserTokens.Number | TypeIdParserTokens.QualifiedAssemblyIdentifier | TypeIdParserTokens.QualifiedIdentifier | TypeIdParserTokens.Whitespace));
            if (captureScan.Count == 1)
            {
                var capture = (TypeIdParserScanData.CaptureEntry)captureScan[0];
                if (advance)
                    currentScanner.Accept(captureScan[0]);
                return capture;
            }
            else if (error)
                throw new SyntaxErrorException();
            return null;
        }

        public ITITypeParameterIdentityRule ParseTypeParameterIdentity()
        {
            ITIQualifiedTypeNameRule type;
            TypeIdParserScanData.SubsetEntry lsq = ParseSubset(TokenTransitions.CommonSymbols.LeftSquareBracket, true, false);
            if (lsq != null)
            {
                type = ParseQualifiedTypeNameInternal(true);
                ParseSubset(TokenTransitions.CommonSymbols.RightSquareBracket);
            }
            else
                type = ParseQualifiedTypeNameInternal(true, true);
            return new TITypeParameterIdentityRule(type);
        }

        public ITIVersionRule ParseVersion()
        {
            var major = int.Parse(ParseCapture(TokenTransitions.Captures.Number));
            ParseSubset(TokenTransitions.CommonSymbols.Period);
            var minor = int.Parse(ParseCapture(TokenTransitions.Captures.Number));
            ParseSubset(TokenTransitions.CommonSymbols.Period);
            var build = int.Parse(ParseCapture(TokenTransitions.Captures.Number));
            ParseSubset(TokenTransitions.CommonSymbols.Period);
            var revision = int.Parse(ParseCapture(TokenTransitions.Captures.Number));
            return new TIVersionRule(major, minor, build, revision);
        }

    }
}
