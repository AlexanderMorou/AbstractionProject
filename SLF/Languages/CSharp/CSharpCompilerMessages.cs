using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Compilers;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Properties;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    /// <summary>
    /// Provides a series of methods and properties which relate to the
    /// errors and warnings of the C&#9839; language.
    /// </summary>
    public static partial class CSharpCompilerMessages
    {
        /// <summary><para>C&#9839; compiler warning (level 4) &#35;28:
        /// </para><para>{0} has the wrong signature to be an entry point
        /// </para></summary>
        public static ICSharpCompilerReferenceWarning CS0028
        {
            get
            {
                if (_CS0028 == null)
                    _CS0028 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0028, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS0028);
                return _CS0028;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0028;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;67:
        /// </para><para>The event {0} is never used</para></summary>
        public static ICSharpCompilerReferenceWarning CS0067
        {
            get
            {
                if (_CS0067 == null)
                    _CS0067 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0067, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0067);
                return _CS0067;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0067;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;78:
        /// </para><para>The 'l' suffix is easily confused with the
        /// digit '1' -- use 'L' for clarity</para></summary>
        public static ICSharpCompilerReferenceWarning CS0078
        {
            get
            {
                if (_CS0078 == null)
                    _CS0078 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0078, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS0078);
                return _CS0078;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0078;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;105:
        /// </para><para>The using directive for {0} appeared previously
        /// in this namespace</para></summary>
        public static ICSharpCompilerReferenceWarning CS0105
        {
            get
            {
                if (_CS0105 == null)
                    _CS0105 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0105, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0105);
                return _CS0105;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0105;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;108:
        /// </para><para>{0} hides inherited member {1}. Use the new 
        /// keyword if hiding was intended.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0108
        {
            get
            {
                if (_CS0108 == null)
                    _CS0108 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0108, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0108);
                return _CS0108;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0108;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;109:
        /// </para><para>The member {0} does not hide an inherited member.
        /// The new keyword is not required</para></summary>
        public static ICSharpCompilerReferenceWarning CS0109
        {
            get
            {
                if (_CS0109 == null)
                    _CS0109 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0109, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS0109);
                return _CS0109;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0109;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;114:
        /// </para><para>{0} hides inherited member {1}. To make the
        /// current method override that implementation, add the override
        /// keyword. Otherwise add the new keyword.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0114
        {
            get
            {
                if (_CS0114 == null)
                    _CS0114 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0114, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0114);
                return _CS0114;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0114;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;162:
        /// </para><para>Unreachable code detected</para></summary>
        public static ICSharpCompilerReferenceWarning CS0162
        {
            get
            {
                if (_CS0162 == null)
                    _CS0162 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0162, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0162);
                return _CS0162;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0162;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;164:
        /// </para><para>This label has not been referenced</para></summary>
        public static ICSharpCompilerReferenceWarning CS0164
        {
            get
            {
                if (_CS0164 == null)
                    _CS0164 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0164, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0164);
                return _CS0164;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0164;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;168:
        /// </para><para>The variable {0} is assigned but its value is
        /// never used</para></summary>
        public static ICSharpCompilerReferenceWarning CS0168
        {
            get
            {
                if (_CS0168 == null)
                    _CS0168 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0168, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0168);
                return _CS0168;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0168;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;169:
        /// </para><para>The private field {0} is never used</para></summary>
        public static ICSharpCompilerReferenceWarning CS0169
        {
            get
            {
                if (_CS0169 == null)
                    _CS0169 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0169, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0169);
                return _CS0169;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0169;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;183:
        /// </para><para>The given expression is always of the provided
        /// ({0}) type</para></summary>
        public static ICSharpCompilerReferenceWarning CS0183
        {
            get
            {
                if (_CS0183 == null)
                    _CS0183 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0183, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0183);
                return _CS0183;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0183;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;184:
        /// </para><para>The given expression is never of the provided
        /// ({0}) type</para></summary>
        public static ICSharpCompilerReferenceWarning CS0184
        {
            get
            {
                if (_CS0184 == null)
                    _CS0184 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0184, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0184);
                return _CS0184;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0184;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;197:
        /// </para><para>Passing {0} as ref or out or taking its address
        /// may cause a runtime exception because it is a field of a
        /// marshal-by-reference class</para></summary>
        public static ICSharpCompilerReferenceWarning CS0197
        {
            get
            {
                if (_CS0197 == null)
                    _CS0197 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0197, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0197);
                return _CS0197;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0197;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;219:
        /// </para><para>The variable {0} is assigned but its value is
        /// never used</para></summary>
        public static ICSharpCompilerReferenceWarning CS0219
        {
            get
            {
                if (_CS0219 == null)
                    _CS0219 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0219, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0219);
                return _CS0219;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0219;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;251:
        /// </para><para>Indexing an array with a negative index (array
        /// indices always start at zero)</para></summary>
        public static ICSharpCompilerReferenceWarning CS0251
        {
            get
            {
                if (_CS0251 == null)
                    _CS0251 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0251, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0251);
                return _CS0251;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0251;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;252:
        /// </para><para>Possible unintended reference comparison; to get
        /// a value comparison, cast the left hand side to type {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0252
        {
            get
            {
                if (_CS0252 == null)
                    _CS0252 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0252, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0252);
                return _CS0252;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0252;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;253:
        /// </para><para>Possible unintended reference comparison; to get
        /// a value comparison, cast the right hand side to type {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0253
        {
            get
            {
                if (_CS0253 == null)
                    _CS0253 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0253, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0253);
                return _CS0253;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0253;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;278:</para><para>{0} does not implement the {1} pattern. {2} is ambiguous with {2}.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0278
        {
            get
            {
                if (_CS0278 == null)
                    _CS0278 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0278, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0278);
                return _CS0278;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0278;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;279:</para><para>{0} does not implement the {1} pattern. {2} is either static or not public.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0279
        {
            get
            {
                if (_CS0279 == null)
                    _CS0279 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0279, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0279);
                return _CS0279;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0279;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;280:</para><para>{0} does not implement the {1} pattern. {2} has the wrong signature.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0280
        {
            get
            {
                if (_CS0280 == null)
                    _CS0280 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0280, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0280);
                return _CS0280;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0280;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;282:</para><para>There is no defined ordering between fields in multiple declarations of partial class or struct {0}. To specify an ordering, all instance fields must be in the same declaration.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0282
        {
            get
            {
                if (_CS0282 == null)
                    _CS0282 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0282, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0282);
                return _CS0282;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0282;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;402:</para><para>{0} : an entry point cannot be generic or in a generic type</para></summary>
        public static ICSharpCompilerReferenceWarning CS0402
        {
            get
            {
                if (_CS0402 == null)
                    _CS0402 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0402, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS0402);
                return _CS0402;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0402;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;414:</para><para>The private field {0} is assigned but its value is never used</para></summary>
        public static ICSharpCompilerReferenceWarning CS0414
        {
            get
            {
                if (_CS0414 == null)
                    _CS0414 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0414, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0414);
                return _CS0414;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0414;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;419:</para><para>Ambiguous reference in cref attribute: {0}. Assuming {1}, but could have also matched other overloads including {2}.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0419
        {
            get
            {
                if (_CS0419 == null)
                    _CS0419 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0419, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0419);
                return _CS0419;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0419;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;420:</para><para>{0}: a reference to a volatile field will not be treated as volatile</para></summary>
        public static ICSharpCompilerReferenceWarning CS0420
        {
            get
            {
                if (_CS0420 == null)
                    _CS0420 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0420, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0420);
                return _CS0420;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0420;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;422:</para><para>The /incremental option is no longer supported</para></summary>
        public static ICSharpCompilerReferenceWarning CS0422
        {
            get
            {
                if (_CS0422 == null)
                    _CS0422 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0422, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS0422);
                return _CS0422;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0422;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;429:</para><para>Unreachable expression code detected </para></summary>
        public static ICSharpCompilerReferenceWarning CS0429
        {
            get
            {
                if (_CS0429 == null)
                    _CS0429 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0429, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS0429);
                return _CS0429;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0429;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;435:</para><para>The namespace {0} in {1} conflicts with the imported type {2} in {3}. Using the namespace defined in {1}..</para></summary>
        public static ICSharpCompilerReferenceWarning CS0435
        {
            get
            {
                if (_CS0435 == null)
                    _CS0435 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0435, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0435);
                return _CS0435;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0435;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;436:</para><para>The type {0} in {1} conflicts with the imported type {2} in {3}. Using the type defined in {1}.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0436
        {
            get
            {
                if (_CS0436 == null)
                    _CS0436 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0436, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0436);
                return _CS0436;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0436;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;437:</para><para>The type {0} in {1} conflicts with the imported namespace {2} in {3}. Using the type defined in {1}.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0437
        {
            get
            {
                if (_CS0437 == null)
                    _CS0437 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0437, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0437);
                return _CS0437;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0437;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;440:</para><para>Defining an alias named 'global' is ill-advised since 'global::' always references the global namespace and not an alias</para></summary>
        public static ICSharpCompilerReferenceWarning CS0440
        {
            get
            {
                if (_CS0440 == null)
                    _CS0440 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0440, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0440);
                return _CS0440;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0440;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;444:</para><para>Predefined type {0} was not found in {1} but was found in {2}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0444
        {
            get
            {
                if (_CS0444 == null)
                    _CS0444 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0444, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0444);
                return _CS0444;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0444;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;458:</para><para>The result of the expression is always 'null' of type {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0458
        {
            get
            {
                if (_CS0458 == null)
                    _CS0458 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0458, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0458);
                return _CS0458;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0458;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;464:</para><para>Comparing with null of type {0} always produces 'false'</para></summary>
        public static ICSharpCompilerReferenceWarning CS0464
        {
            get
            {
                if (_CS0464 == null)
                    _CS0464 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0464, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0464);
                return _CS0464;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0464;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;465:</para><para>Introducing a 'Finalize' method can interfere with destructor invocation. Did you intend to declare a destructor?</para></summary>
        public static ICSharpCompilerReferenceWarning CS0465
        {
            get
            {
                if (_CS0465 == null)
                    _CS0465 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0465, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0465);
                return _CS0465;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0465;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;467:</para><para>Ambiguity between method {0} and non-method {1}. Using method group.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0467
        {
            get
            {
                if (_CS0467 == null)
                    _CS0467 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0467, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0467);
                return _CS0467;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0467;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;469:</para><para>The {0} value is not implicitly convertible to type {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0469
        {
            get
            {
                if (_CS0469 == null)
                    _CS0469 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0469, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0469);
                return _CS0469;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0469;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;472:</para><para>The result of the expression is always {0} since a value of type {1} is never equal to 'null' of type {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0472
        {
            get
            {
                if (_CS0472 == null)
                    _CS0472 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0472, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0472);
                return _CS0472;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0472;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;602:</para><para>The feature {0} is deprecated. Please use {1} instead</para></summary>
        public static ICSharpCompilerReferenceWarning CS0602
        {
            get
            {
                if (_CS0602 == null)
                    _CS0602 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0602, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0602);
                return _CS0602;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0602;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;612:</para><para>{0} is obsolete</para></summary>
        public static ICSharpCompilerReferenceWarning CS0612
        {
            get
            {
                if (_CS0612 == null)
                    _CS0612 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0612, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0612);
                return _CS0612;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0612;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;618:</para><para>{0} is obsolete: {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0618
        {
            get
            {
                if (_CS0618 == null)
                    _CS0618 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0618, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0618);
                return _CS0618;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0618;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;626:</para><para>Method, operator, or accessor {0} is marked external and has no attributes on it. Consider adding a DllImport attribute to specify the external implementation</para></summary>
        public static ICSharpCompilerReferenceWarning CS0626
        {
            get
            {
                if (_CS0626 == null)
                    _CS0626 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0626, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0626);
                return _CS0626;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0626;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;628:</para><para>{0} : new protected member declared in sealed class</para></summary>
        public static ICSharpCompilerReferenceWarning CS0628
        {
            get
            {
                if (_CS0628 == null)
                    _CS0628 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0628, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS0628);
                return _CS0628;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0628;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;642:</para><para>Possible mistaken empty statement</para></summary>
        public static ICSharpCompilerReferenceWarning CS0642
        {
            get
            {
                if (_CS0642 == null)
                    _CS0642 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0642, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0642);
                return _CS0642;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0642;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;649:</para><para>Field {0} is never assigned to, and will always have its default value {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0649
        {
            get
            {
                if (_CS0649 == null)
                    _CS0649 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0649, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS0649);
                return _CS0649;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0649;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;652:</para><para>Comparison to integral constant is useless; the constant is outside the range of type {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0652
        {
            get
            {
                if (_CS0652 == null)
                    _CS0652 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0652, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0652);
                return _CS0652;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0652;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;657:</para><para>{0} is not a valid attribute location for this declaration. Valid attribute locations for this declaration are {1}. All attributes in this block will be ignored.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0657
        {
            get
            {
                if (_CS0657 == null)
                    _CS0657 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0657, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0657);
                return _CS0657;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0657;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;658:</para><para>{0} is not a recognized attribute location. All attributes in this block will be ignored.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0658
        {
            get
            {
                if (_CS0658 == null)
                    _CS0658 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0658, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0658);
                return _CS0658;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0658;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;659:</para><para>{0} overrides Object.Equals(object o) but does not override Object.GetHashCode()</para></summary>
        public static ICSharpCompilerReferenceWarning CS0659
        {
            get
            {
                if (_CS0659 == null)
                    _CS0659 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0659, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0659);
                return _CS0659;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0659;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;660:</para><para>{0} defines operator == or operator != but does not override Object.Equals(object o)</para></summary>
        public static ICSharpCompilerReferenceWarning CS0660
        {
            get
            {
                if (_CS0660 == null)
                    _CS0660 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0660, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0660);
                return _CS0660;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0660;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;661:</para><para>{0} defines operator == or operator != but does not override Object.GetHashCode()</para></summary>
        public static ICSharpCompilerReferenceWarning CS0661
        {
            get
            {
                if (_CS0661 == null)
                    _CS0661 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0661, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0661);
                return _CS0661;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0661;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;665:</para><para>Assignment in conditional expression is always constant; did you mean to use '==' instead of '='?</para></summary>
        public static ICSharpCompilerReferenceWarning CS0665
        {
            get
            {
                if (_CS0665 == null)
                    _CS0665 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0665, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0665);
                return _CS0665;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0665;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;672:</para><para>Member {0} overrides obsolete member '{1}. Add the Obsolete attribute to {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0672
        {
            get
            {
                if (_CS0672 == null)
                    _CS0672 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0672, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0672);
                return _CS0672;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0672;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;675:</para><para>Bitwise-or operator used on a sign-extended operand; consider casting to a smaller unsigned type first</para></summary>
        public static ICSharpCompilerReferenceWarning CS0675
        {
            get
            {
                if (_CS0675 == null)
                    _CS0675 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0675, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0675);
                return _CS0675;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0675;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;693:</para><para>GenericParameter parameter {0} has the same name as the type parameter from outer type {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS0693
        {
            get
            {
                if (_CS0693 == null)
                    _CS0693 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0693, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS0693);
                return _CS0693;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0693;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;728:</para><para>Possibly incorrect assignment to local {0} which is the argument to a using or lock statement. The Dispose call or unlocking will happen on the original value of the local.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0728
        {
            get
            {
                if (_CS0728 == null)
                    _CS0728 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0728, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS0728);
                return _CS0728;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0728;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;809:</para><para>Obsolete member {0} overrides non-obsolete member {1}.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0809
        {
            get
            {
                if (_CS0809 == null)
                    _CS0809 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0809, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0809);
                return _CS0809;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0809;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;824:</para><para>Constructor {0} is marked external.</para></summary>
        public static ICSharpCompilerReferenceWarning CS0824
        {
            get
            {
                if (_CS0824 == null)
                    _CS0824 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS0824, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS0824);
                return _CS0824;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS0824;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1030:</para><para>#warning: {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1030
        {
            get
            {
                if (_CS1030 == null)
                    _CS1030 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1030, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1030);
                return _CS1030;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1030;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1058:</para><para>A previous catch clause already catches all exceptions. All exceptions thrown will be wrapped in a System.Runtime.CompilerServices.RuntimeWrappedException</para></summary>
        public static ICSharpCompilerReferenceWarning CS1058
        {
            get
            {
                if (_CS1058 == null)
                    _CS1058 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1058, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1058);
                return _CS1058;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1058;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1060:</para><para>Use of possibly unassigned field 'name'. Struct instance variables are initially unassigned if struct is unassigned.</para></summary>
        public static ICSharpCompilerReferenceWarning CS1060
        {
            get
            {
                if (_CS1060 == null)
                    _CS1060 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1060, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1060);
                return _CS1060;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1060;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1522:</para><para>Empty switch block</para></summary>
        public static ICSharpCompilerReferenceWarning CS1522
        {
            get
            {
                if (_CS1522 == null)
                    _CS1522 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1522, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1522);
                return _CS1522;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1522;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1570:</para><para>XML comment on {0} has badly formed XML — {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1570
        {
            get
            {
                if (_CS1570 == null)
                    _CS1570 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1570, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1570);
                return _CS1570;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1570;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1571:</para><para>XML comment on {0} has a duplicate param tag for {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1571
        {
            get
            {
                if (_CS1571 == null)
                    _CS1571 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1571, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS1571);
                return _CS1571;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1571;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1572:</para><para>XML comment on {0} has a param tag for {1}, but there is no parameter by that name</para></summary>
        public static ICSharpCompilerReferenceWarning CS1572
        {
            get
            {
                if (_CS1572 == null)
                    _CS1572 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1572, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS1572);
                return _CS1572;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1572;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;1573:</para><para>Parameter {0} has no matching param tag in the XML comment for {0} (but other parameters do)</para></summary>
        public static ICSharpCompilerReferenceWarning CS1573
        {
            get
            {
                if (_CS1573 == null)
                    _CS1573 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1573, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS1573);
                return _CS1573;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1573;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1574:</para><para>XML comment on {0} has cref attribute {1} that could not be resolved.</para></summary>
        public static ICSharpCompilerReferenceWarning CS1574
        {
            get
            {
                if (_CS1574 == null)
                    _CS1574 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1574, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1574);
                return _CS1574;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1574;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1580:</para><para>Invalid type for parameter {0} in XML comment cref attribute</para></summary>
        public static ICSharpCompilerReferenceWarning CS1580
        {
            get
            {
                if (_CS1580 == null)
                    _CS1580 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1580, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1580);
                return _CS1580;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1580;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1581:</para><para>Invalid return type in XML comment cref attribute</para></summary>
        public static ICSharpCompilerReferenceWarning CS1581
        {
            get
            {
                if (_CS1581 == null)
                    _CS1581 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1581, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1581);
                return _CS1581;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1581;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1584:</para><para>XML comment on {0} has syntactically incorrect cref attribute {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1584
        {
            get
            {
                if (_CS1584 == null)
                    _CS1584 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1584, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1584);
                return _CS1584;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1584;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1587:</para><para>XML comment is not placed on a valid language element</para></summary>
        public static ICSharpCompilerReferenceWarning CS1587
        {
            get
            {
                if (_CS1587 == null)
                    _CS1587 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1587, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS1587);
                return _CS1587;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1587;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1589:</para><para>Unable to include XML fragment {0} of file {1} -- {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1589
        {
            get
            {
                if (_CS1589 == null)
                    _CS1589 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1589, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1589);
                return _CS1589;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1589;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1590:</para><para>Invalid XML include element -- Missing file attribute</para></summary>
        public static ICSharpCompilerReferenceWarning CS1590
        {
            get
            {
                if (_CS1590 == null)
                    _CS1590 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1590, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1590);
                return _CS1590;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1590;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;1591:</para><para>Missing XML comment for publicly visible type or member {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1591
        {
            get
            {
                if (_CS1591 == null)
                    _CS1591 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1591, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS1591);
                return _CS1591;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1591;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1592:</para><para>Badly formed XML in included comments file -- {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1592
        {
            get
            {
                if (_CS1592 == null)
                    _CS1592 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1592, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1592);
                return _CS1592;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1592;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1598:</para><para>XML parser could not be loaded for the following reason: {0}. The XML documentation file {1} will not be generated.</para></summary>
        public static ICSharpCompilerReferenceWarning CS1598
        {
            get
            {
                if (_CS1598 == null)
                    _CS1598 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1598, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1598);
                return _CS1598;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1598;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1607:</para><para>Assembly generation -- {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1607
        {
            get
            {
                if (_CS1607 == null)
                    _CS1607 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1607, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1607);
                return _CS1607;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1607;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;1610:</para><para>Unable to delete temporary file {0} used for default Win32 resource -- {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1610
        {
            get
            {
                if (_CS1610 == null)
                    _CS1610 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1610, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS1610);
                return _CS1610;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1610;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1616:</para><para>Option {0} overrides attribute {1} given in a source file or added module</para></summary>
        public static ICSharpCompilerReferenceWarning CS1616
        {
            get
            {
                if (_CS1616 == null)
                    _CS1616 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1616, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1616);
                return _CS1616;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1616;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1633:</para><para>Unrecognized #pragma directive</para></summary>
        public static ICSharpCompilerReferenceWarning CS1633
        {
            get
            {
                if (_CS1633 == null)
                    _CS1633 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1633, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1633);
                return _CS1633;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1633;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1634:</para><para>Expected disable or restore</para></summary>
        public static ICSharpCompilerReferenceWarning CS1634
        {
            get
            {
                if (_CS1634 == null)
                    _CS1634 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1634, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1634);
                return _CS1634;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1634;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1635:</para><para>Cannot restore warning {0} because it was disabled globally</para></summary>
        public static ICSharpCompilerReferenceWarning CS1635
        {
            get
            {
                if (_CS1635 == null)
                    _CS1635 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1635, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1635);
                return _CS1635;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1635;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1645:</para><para>Feature {0} is not part of the standardized ISO C# language specification, and may not be accepted by other compilers</para></summary>
        public static ICSharpCompilerReferenceWarning CS1645
        {
            get
            {
                if (_CS1645 == null)
                    _CS1645 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1645, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1645);
                return _CS1645;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1645;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1658:</para><para>{0}. See also error: {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1658
        {
            get
            {
                if (_CS1658 == null)
                    _CS1658 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1658, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1658);
                return _CS1658;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1658;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1668:</para><para>Invalid search path 'path' specified in {0} -- {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1668
        {
            get
            {
                if (_CS1668 == null)
                    _CS1668 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1668, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS1668);
                return _CS1668;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1668;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1682:</para><para>Reference to type {0} claims it is nested within {1}, but it could not be found</para></summary>
        public static ICSharpCompilerReferenceWarning CS1682
        {
            get
            {
                if (_CS1682 == null)
                    _CS1682 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1682, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1682);
                return _CS1682;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1682;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1683:</para><para>Reference to type {0} claims it is defined in this assembly, but it is not defined in source or any added modules</para></summary>
        public static ICSharpCompilerReferenceWarning CS1683
        {
            get
            {
                if (_CS1683 == null)
                    _CS1683 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1683, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1683);
                return _CS1683;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1683;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1684:</para><para>Reference to type {0} claims it is defined in {1}, but it could not be found</para></summary>
        public static ICSharpCompilerReferenceWarning CS1684
        {
            get
            {
                if (_CS1684 == null)
                    _CS1684 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1684, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1684);
                return _CS1684;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1684;

        //System variation of CS0436
        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1685:</para><para>The predefined type {0} is defined in multiple assemblies in the global alias; using definition from {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1685
        {
            get
            {
                if (_CS1685 == null)
                    _CS1685 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1685, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1685);
                return _CS1685;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1685;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1687:</para><para>Source file has exceeded the limit of 16,707,565 lines representable in the PDB, debug information will be incorrect</para></summary>
        public static ICSharpCompilerReferenceWarning CS1687
        {
            get
            {
                if (_CS1687 == null)
                    _CS1687 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1687, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1687);
                return _CS1687;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1687;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1690:</para><para>Accessing a member on {0} may cause a runtime exception because it is a field of a marshal-by-reference class</para></summary>
        public static ICSharpCompilerReferenceWarning CS1690
        {
            get
            {
                if (_CS1690 == null)
                    _CS1690 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1690, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1690);
                return _CS1690;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1690;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1691:</para><para>{0} is not a valid warning number</para></summary>
        public static ICSharpCompilerReferenceWarning CS1691
        {
            get
            {
                if (_CS1691 == null)
                    _CS1691 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1691, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1691);
                return _CS1691;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1691;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1692:</para><para>Invalid number</para></summary>
        public static ICSharpCompilerReferenceWarning CS1692
        {
            get
            {
                if (_CS1692 == null)
                    _CS1692 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1692, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1692);
                return _CS1692;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1692;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1694:</para><para>Invalid filename specified for preprocessor directive. Filename is too long or not a valid filename.</para></summary>
        public static ICSharpCompilerReferenceWarning CS1694
        {
            get
            {
                if (_CS1694 == null)
                    _CS1694 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1694, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1694);
                return _CS1694;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1694;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1695:</para><para>Invalid #pragma checksum syntax; should be #pragma checksum ""filename"" ""{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}"" ""XXXX...""</para></summary>
        public static ICSharpCompilerReferenceWarning CS1695
        {
            get
            {
                if (_CS1695 == null)
                    _CS1695 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1695, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1695);
                return _CS1695;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1695;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1696:</para><para>Single-line comment or end-of-line expected</para></summary>
        public static ICSharpCompilerReferenceWarning CS1696
        {
            get
            {
                if (_CS1696 == null)
                    _CS1696 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1696, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1696);
                return _CS1696;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1696;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1697:</para><para>Different checksum values given for {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1697
        {
            get
            {
                if (_CS1697 == null)
                    _CS1697 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1697, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1697);
                return _CS1697;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1697;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1698:</para><para>Circular assembly reference {0} does not match the output assembly name {1}. Try adding a reference to {0} or changing the output assembly name to match.</para></summary>
        public static ICSharpCompilerReferenceWarning CS1698
        {
            get
            {
                if (_CS1698 == null)
                    _CS1698 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1698, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS1698);
                return _CS1698;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1698;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1699:</para><para>Use command line option {0} or appropriate project settings instead of {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1699
        {
            get
            {
                if (_CS1699 == null)
                    _CS1699 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1699, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1699);
                return _CS1699;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1699;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;1700:</para><para>Assembly reference Assembly Name is invalid and cannot be resolved</para></summary>
        public static ICSharpCompilerReferenceWarning CS1700
        {
            get
            {
                if (_CS1700 == null)
                    _CS1700 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1700, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS1700);
                return _CS1700;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1700;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1701:</para><para>Assuming assembly reference {0} matches {1}, you may need to supply runtime policy </para></summary>
        public static ICSharpCompilerReferenceWarning CS1701
        {
            get
            {
                if (_CS1701 == null)
                    _CS1701 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1701, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS1701);
                return _CS1701;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1701;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;1702:</para><para>Assuming assembly reference {0} matches {1}, you may need to supply runtime policy</para></summary>
        public static ICSharpCompilerReferenceWarning CS1702
        {
            get
            {
                if (_CS1702 == null)
                    _CS1702 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1702, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS1702);
                return _CS1702;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1702;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1707:</para><para>Delegate {0} bound to {1} instead of {2} because of new language rules</para></summary>
        public static ICSharpCompilerReferenceWarning CS1707
        {
            get
            {
                if (_CS1707 == null)
                    _CS1707 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1707, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1707);
                return _CS1707;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1707;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1709:</para><para>Filename specified for preprocessor directive is empty</para></summary>
        public static ICSharpCompilerReferenceWarning CS1709
        {
            get
            {
                if (_CS1709 == null)
                    _CS1709 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1709, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1709);
                return _CS1709;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1709;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1710:</para><para>XML comment on {0} has a duplicate typeparam tag for {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS1710
        {
            get
            {
                if (_CS1710 == null)
                    _CS1710 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1710, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS1710);
                return _CS1710;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1710;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1711:</para><para>XML comment on {0} has a typeparam tag for {1}, but there is no type parameter by that name</para></summary>
        public static ICSharpCompilerReferenceWarning CS1711
        {
            get
            {
                if (_CS1711 == null)
                    _CS1711 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1711, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS1711);
                return _CS1711;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1711;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;1712:</para><para>GenericParameter parameter {0} has no matching typeparam tag in the XML comment on {1} (but other type parameters do)</para></summary>
        public static ICSharpCompilerReferenceWarning CS1712
        {
            get
            {
                if (_CS1712 == null)
                    _CS1712 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1712, CSharpWarningLevels.Level4, CSharpWarningIdentifiers.CS1712);
                return _CS1712;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1712;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;1717:</para><para>Assignment made to same variable; did you mean to assign something else?</para></summary>
        public static ICSharpCompilerReferenceWarning CS1717
        {
            get
            {
                if (_CS1717 == null)
                    _CS1717 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1717, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS1717);
                return _CS1717;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1717;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;1718:</para><para>Comparison made to same variable; did you mean to compare something else?</para></summary>
        public static ICSharpCompilerReferenceWarning CS1718
        {
            get
            {
                if (_CS1718 == null)
                    _CS1718 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1718, CSharpWarningLevels.Level3, CSharpWarningIdentifiers.CS1718);
                return _CS1718;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1718;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1720:</para><para>Expression will always cause a System.NullReferenceException because the default value of {0} is null</para></summary>
        public static ICSharpCompilerReferenceWarning CS1720
        {
            get
            {
                if (_CS1720 == null)
                    _CS1720 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1720, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1720);
                return _CS1720;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1720;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1723:</para><para>XML comment on {0} has cref attribute {1} that refers to a type parameter</para></summary>
        public static ICSharpCompilerReferenceWarning CS1723
        {
            get
            {
                if (_CS1723 == null)
                    _CS1723 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1723, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1723);
                return _CS1723;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1723;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1911:</para><para>Access to member {0} through a 'base' keyword from an anonymous method, lambda expression, query expression, or iterator results in unverifiable code. Consider moving the access into a helper method on the containing type.</para></summary>
        public static ICSharpCompilerReferenceWarning CS1911
        {
            get
            {
                if (_CS1911 == null)
                    _CS1911 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1911, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1911);
                return _CS1911;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1911;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1927:</para><para>Ignoring /win32manifest for module because it only applies to assemblies.</para></summary>
        public static ICSharpCompilerReferenceWarning CS1927
        {
            get
            {
                if (_CS1927 == null)
                    _CS1927 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1927, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS1927);
                return _CS1927;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1927;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1956:</para><para>Member {0} implements interface member {0} in type {1}. There are multiple matches for the interface member at run-time. It is implementation dependent which method will be called.</para></summary>
        public static ICSharpCompilerReferenceWarning CS1956
        {
            get
            {
                if (_CS1956 == null)
                    _CS1956 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1956, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1956);
                return _CS1956;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1956;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1957:</para><para>Member {0} overrides {1}. There are multiple override candidates at run-time. It is implementation dependent which method will be called.</para></summary>
        public static ICSharpCompilerReferenceWarning CS1957
        {
            get
            {
                if (_CS1957 == null)
                    _CS1957 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS1957, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS1957);
                return _CS1957;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS1957;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;2002:</para><para>Source file {0} specified multiple times</para></summary>
        public static ICSharpCompilerReferenceWarning CS2002
        {
            get
            {
                if (_CS2002 == null)
                    _CS2002 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS2002, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS2002);
                return _CS2002;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS2002;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;2014:</para><para>Compiler option {0} is obsolete, please use {1} instead</para></summary>
        public static ICSharpCompilerReferenceWarning CS2014
        {
            get
            {
                if (_CS2014 == null)
                    _CS2014 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS2014, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS2014);
                return _CS2014;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS2014;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;2023:</para><para>Ignoring /noconfig option because it was specified in a response file</para></summary>
        public static ICSharpCompilerReferenceWarning CS2023
        {
            get
            {
                if (_CS2023 == null)
                    _CS2023 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS2023, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS2023);
                return _CS2023;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS2023;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;2029:</para><para>Invalid value for '/define'; {0} is not a valid identifier</para></summary>
        public static ICSharpCompilerReferenceWarning CS2029
        {
            get
            {
                if (_CS2029 == null)
                    _CS2029 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS2029, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS2029);
                return _CS2029;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS2029;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3000:</para><para>Methods with variable arguments are not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3000
        {
            get
            {
                if (_CS3000 == null)
                    _CS3000 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3000, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3000);
                return _CS3000;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3000;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3001:</para><para>Argument type {0} is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3001
        {
            get
            {
                if (_CS3001 == null)
                    _CS3001 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3001, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3001);
                return _CS3001;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3001;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3002:</para><para>Return type of {0} is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3002
        {
            get
            {
                if (_CS3002 == null)
                    _CS3002 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3002, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3002);
                return _CS3002;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3002;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3003:</para><para>GenericParameter of {0} is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3003
        {
            get
            {
                if (_CS3003 == null)
                    _CS3003 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3003, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3003);
                return _CS3003;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3003;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3004:</para><para>Mixed and decomposed Unicode characters are not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3004
        {
            get
            {
                if (_CS3004 == null)
                    _CS3004 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3004, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3004);
                return _CS3004;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3004;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3005:</para><para>Identifier {0} differing only in case is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3005
        {
            get
            {
                if (_CS3005 == null)
                    _CS3005 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3005, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3005);
                return _CS3005;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3005;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3006:</para><para>Overloaded method {0} differing only in ref or out, or in array rank, is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3006
        {
            get
            {
                if (_CS3006 == null)
                    _CS3006 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3006, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3006);
                return _CS3006;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3006;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3007:</para><para>Overloaded method {0} differing only by unnamed array types is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3007
        {
            get
            {
                if (_CS3007 == null)
                    _CS3007 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3007, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3007);
                return _CS3007;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3007;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3008:</para><para>Identifier {0} differing only in case is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3008
        {
            get
            {
                if (_CS3008 == null)
                    _CS3008 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3008, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3008);
                return _CS3008;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3008;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3009:</para><para>{0}: base type {0} is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3009
        {
            get
            {
                if (_CS3009 == null)
                    _CS3009 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3009, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3009);
                return _CS3009;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3009;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3010:</para><para>{0}: CLS-compliant interfaces must have only CLS-compliant members</para></summary>
        public static ICSharpCompilerReferenceWarning CS3010
        {
            get
            {
                if (_CS3010 == null)
                    _CS3010 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3010, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3010);
                return _CS3010;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3010;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3011:</para><para>{0}: only CLS-compliant members can be abstract</para></summary>
        public static ICSharpCompilerReferenceWarning CS3011
        {
            get
            {
                if (_CS3011 == null)
                    _CS3011 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3011, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3011);
                return _CS3011;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3011;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3012:</para><para>You cannot specify the CLSCompliant attribute on a module that differs from the CLSCompliant attribute on the assembly</para></summary>
        public static ICSharpCompilerReferenceWarning CS3012
        {
            get
            {
                if (_CS3012 == null)
                    _CS3012 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3012, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3012);
                return _CS3012;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3012;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3013:</para><para>Added modules must be marked with the CLSCompliant attribute to match the assembly</para></summary>
        public static ICSharpCompilerReferenceWarning CS3013
        {
            get
            {
                if (_CS3013 == null)
                    _CS3013 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3013, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3013);
                return _CS3013;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3013;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3014:</para><para>{0} does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute</para></summary>
        public static ICSharpCompilerReferenceWarning CS3014
        {
            get
            {
                if (_CS3014 == null)
                    _CS3014 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3014, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3014);
                return _CS3014;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3014;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3015:</para><para>{0} has no accessible constructors which use only CLS-compliant types</para></summary>
        public static ICSharpCompilerReferenceWarning CS3015
        {
            get
            {
                if (_CS3015 == null)
                    _CS3015 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3015, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3015);
                return _CS3015;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3015;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3016:</para><para>Arrays as attribute arguments is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3016
        {
            get
            {
                if (_CS3016 == null)
                    _CS3016 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3016, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3016);
                return _CS3016;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3016;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3017:</para><para>You cannot specify the CLSCompliant attribute on a module that differs from the CLSCompliant attribute on the assembly</para></summary>
        public static ICSharpCompilerReferenceWarning CS3017
        {
            get
            {
                if (_CS3017 == null)
                    _CS3017 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3017, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3017);
                return _CS3017;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3017;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3018:</para><para>{0} cannot be marked as CLS-Compliant because it is a member of non CLS-compliant type {1}</para></summary>
        public static ICSharpCompilerReferenceWarning CS3018
        {
            get
            {
                if (_CS3018 == null)
                    _CS3018 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3018, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3018);
                return _CS3018;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3018;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;3019:</para><para>CLS compliance checking will not be performed on {0} because it is not visible from outside this assembly.</para></summary>
        public static ICSharpCompilerReferenceWarning CS3019
        {
            get
            {
                if (_CS3019 == null)
                    _CS3019 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3019, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS3019);
                return _CS3019;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3019;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;3021:</para><para>{0} does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute</para></summary>
        public static ICSharpCompilerReferenceWarning CS3021
        {
            get
            {
                if (_CS3021 == null)
                    _CS3021 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3021, CSharpWarningLevels.Level2, CSharpWarningIdentifiers.CS3021);
                return _CS3021;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3021;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3022:</para><para>CLSCompliant attribute has no meaning when applied to parameters. Try putting it on the method instead.</para></summary>
        public static ICSharpCompilerReferenceWarning CS3022
        {
            get
            {
                if (_CS3022 == null)
                    _CS3022 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3022, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3022);
                return _CS3022;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3022;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3023:</para><para>CLSCompliant attribute has no meaning when applied to return types. Try putting it on the method instead.</para></summary>
        public static ICSharpCompilerReferenceWarning CS3023
        {
            get
            {
                if (_CS3023 == null)
                    _CS3023 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3023, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3023);
                return _CS3023;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3023;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3026:</para><para>CLS-compliant field {0} cannot be volatile</para></summary>
        public static ICSharpCompilerReferenceWarning CS3026
        {
            get
            {
                if (_CS3026 == null)
                    _CS3026 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3026, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3026);
                return _CS3026;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3026;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3027:</para><para>{0} is not CLS-compliant because base interface {1} is not CLS-compliant</para></summary>
        public static ICSharpCompilerReferenceWarning CS3027
        {
            get
            {
                if (_CS3027 == null)
                    _CS3027 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS3027, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS3027);
                return _CS3027;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS3027;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;5000:</para><para>Unknown compiler option {0}</para></summary>
        public static ICSharpCompilerReferenceWarning CS5000
        {
            get
            {
                if (_CS5000 == null)
                    _CS5000 = new CSharpCompilerReferenceWarning(Resources.CSharpWarnings_CS5000, CSharpWarningLevels.Level1, CSharpWarningIdentifiers.CS5000);
                return _CS5000;
            }
        }
        private static ICSharpCompilerReferenceWarning _CS5000;

        /// <summary><para>C&#9839; compiler error &#35;1:</para><para>Internal compiler error</para></summary>
        public static ICSharpCompilerReferenceError CS0001
        {
            get
            {
                if (_CS0001 == null)
                    _CS0001 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0001, CSharpErrorIdentifiers.CS0001);
                return _CS0001;
            }
        }
        private static ICSharpCompilerReferenceError _CS0001;

        /// <summary><para>C&#9839; compiler error &#35;3:</para><para>Out of memory</para></summary>
        public static ICSharpCompilerReferenceError CS0003
        {
            get
            {
                if (_CS0003 == null)
                    _CS0003 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0003, CSharpErrorIdentifiers.CS0003);
                return _CS0003;
            }
        }
        private static ICSharpCompilerReferenceError _CS0003;

        /// <summary><para>C&#9839; compiler error &#35;4:</para><para>SourceWarning treated as error</para></summary>
        public static ICSharpCompilerReferenceError CS0004
        {
            get
            {
                if (_CS0004 == null)
                    _CS0004 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0004, CSharpErrorIdentifiers.CS0004);
                return _CS0004;
            }
        }
        private static ICSharpCompilerReferenceError _CS0004;

        /// <summary><para>C&#9839; compiler error &#35;5:</para><para>Compiler option {0} must be followed by an argument</para></summary>
        public static ICSharpCompilerReferenceError CS0005
        {
            get
            {
                if (_CS0005 == null)
                    _CS0005 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0005, CSharpErrorIdentifiers.CS0005);
                return _CS0005;
            }
        }
        private static ICSharpCompilerReferenceError _CS0005;

        /// <summary><para>C&#9839; compiler error &#35;6:</para><para>Metadata file {0} could not be found</para></summary>
        public static ICSharpCompilerReferenceError CS0006
        {
            get
            {
                if (_CS0006 == null)
                    _CS0006 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0006, CSharpErrorIdentifiers.CS0006);
                return _CS0006;
            }
        }
        private static ICSharpCompilerReferenceError _CS0006;

        /// <summary><para>C&#9839; compiler error &#35;7:</para><para>Unexpected common language runtime initialization error — {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0007
        {
            get
            {
                if (_CS0007 == null)
                    _CS0007 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0007, CSharpErrorIdentifiers.CS0007);
                return _CS0007;
            }
        }
        private static ICSharpCompilerReferenceError _CS0007;

        /// <summary><para>C&#9839; compiler error &#35;8:</para><para>Unexpected error reading metadata from file 'file' — {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0008
        {
            get
            {
                if (_CS0008 == null)
                    _CS0008 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0008, CSharpErrorIdentifiers.CS0008);
                return _CS0008;
            }
        }
        private static ICSharpCompilerReferenceError _CS0008;

        /// <summary><para>C&#9839; compiler error &#35;9:</para><para>Metadata file {0} could not be opened — {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0009
        {
            get
            {
                if (_CS0009 == null)
                    _CS0009 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0009, CSharpErrorIdentifiers.CS0009);
                return _CS0009;
            }
        }
        private static ICSharpCompilerReferenceError _CS0009;

        /// <summary><para>C&#9839; compiler error &#35;10:</para><para>Unexpected fatal error -- {0}.</para></summary>
        public static ICSharpCompilerReferenceError CS0010
        {
            get
            {
                if (_CS0010 == null)
                    _CS0010 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0010, CSharpErrorIdentifiers.CS0010);
                return _CS0010;
            }
        }
        private static ICSharpCompilerReferenceError _CS0010;

        /// <summary><para>C&#9839; compiler error &#35;11:</para><para>The base class or interface {0} in assembly {1} referenced by type {2} could not be resolved</para></summary>
        public static ICSharpCompilerReferenceError CS0011
        {
            get
            {
                if (_CS0011 == null)
                    _CS0011 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0011, CSharpErrorIdentifiers.CS0011);
                return _CS0011;
            }
        }
        private static ICSharpCompilerReferenceError _CS0011;

        /// <summary><para>C&#9839; compiler error &#35;12:</para><para>The type {0} is defined in an assembly that is not referenced. You must add a reference to assembly {1}.</para></summary>
        public static ICSharpCompilerReferenceError CS0012
        {
            get
            {
                if (_CS0012 == null)
                    _CS0012 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0012, CSharpErrorIdentifiers.CS0012);
                return _CS0012;
            }
        }
        private static ICSharpCompilerReferenceError _CS0012;

        /// <summary><para>C&#9839; compiler error &#35;13:</para><para>Unexpected error writing metadata to file {0} -- {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0013
        {
            get
            {
                if (_CS0013 == null)
                    _CS0013 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0013, CSharpErrorIdentifiers.CS0013);
                return _CS0013;
            }
        }
        private static ICSharpCompilerReferenceError _CS0013;

        /// <summary><para>C&#9839; compiler error &#35;14:</para><para>Required file {0} could not be found</para></summary>
        public static ICSharpCompilerReferenceError CS0014
        {
            get
            {
                if (_CS0014 == null)
                    _CS0014 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0014, CSharpErrorIdentifiers.CS0014);
                return _CS0014;
            }
        }
        private static ICSharpCompilerReferenceError _CS0014;

        /// <summary><para>C&#9839; compiler error &#35;15:</para><para>The name of type {0} is too long</para></summary>
        public static ICSharpCompilerReferenceError CS0015
        {
            get
            {
                if (_CS0015 == null)
                    _CS0015 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0015, CSharpErrorIdentifiers.CS0015);
                return _CS0015;
            }
        }
        private static ICSharpCompilerReferenceError _CS0015;

        /// <summary><para>C&#9839; compiler error &#35;16:</para><para>Could not write to output file {0} — {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0016
        {
            get
            {
                if (_CS0016 == null)
                    _CS0016 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0016, CSharpErrorIdentifiers.CS0016);
                return _CS0016;
            }
        }
        private static ICSharpCompilerReferenceError _CS0016;

        /// <summary><para>C&#9839; compiler error &#35;17:</para><para>Program {0} has more than one entry point defined. Compile with /main to specify the type that contains the entry point.</para></summary>
        public static ICSharpCompilerReferenceError CS0017
        {
            get
            {
                if (_CS0017 == null)
                    _CS0017 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0017, CSharpErrorIdentifiers.CS0017);
                return _CS0017;
            }
        }
        private static ICSharpCompilerReferenceError _CS0017;

        /// <summary><para>C&#9839; compiler error &#35;19:</para><para>Operator {0} cannot be applied to operands of type {1} and {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0019
        {
            get
            {
                if (_CS0019 == null)
                    _CS0019 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0019, CSharpErrorIdentifiers.CS0019);
                return _CS0019;
            }
        }
        private static ICSharpCompilerReferenceError _CS0019;

        /// <summary><para>C&#9839; compiler error &#35;20:</para><para>Division by constant zero</para></summary>
        public static ICSharpCompilerReferenceError CS0020
        {
            get
            {
                if (_CS0020 == null)
                    _CS0020 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0020, CSharpErrorIdentifiers.CS0020);
                return _CS0020;
            }
        }
        private static ICSharpCompilerReferenceError _CS0020;

        /// <summary><para>C&#9839; compiler error &#35;21:</para><para>Cannot apply indexing with [] to an expression of type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0021
        {
            get
            {
                if (_CS0021 == null)
                    _CS0021 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0021, CSharpErrorIdentifiers.CS0021);
                return _CS0021;
            }
        }
        private static ICSharpCompilerReferenceError _CS0021;

        /// <summary><para>C&#9839; compiler error &#35;22:</para><para>Wrong number of indices inside [], expected {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0022
        {
            get
            {
                if (_CS0022 == null)
                    _CS0022 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0022, CSharpErrorIdentifiers.CS0022);
                return _CS0022;
            }
        }
        private static ICSharpCompilerReferenceError _CS0022;

        /// <summary><para>C&#9839; compiler error &#35;23:</para><para>Operator {0} cannot be applied to operand of type {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0023
        {
            get
            {
                if (_CS0023 == null)
                    _CS0023 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0023, CSharpErrorIdentifiers.CS0023);
                return _CS0023;
            }
        }
        private static ICSharpCompilerReferenceError _CS0023;

        /// <summary><para>C&#9839; compiler error &#35;25:</para><para>Standard library file {0} could not be found</para></summary>
        public static ICSharpCompilerReferenceError CS0025
        {
            get
            {
                if (_CS0025 == null)
                    _CS0025 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0025, CSharpErrorIdentifiers.CS0025);
                return _CS0025;
            }
        }
        private static ICSharpCompilerReferenceError _CS0025;

        /// <summary><para>C&#9839; compiler error &#35;26:</para><para>Keyword 'this' is not valid in a static property, static method, or static field initializer</para></summary>
        public static ICSharpCompilerReferenceError CS0026
        {
            get
            {
                if (_CS0026 == null)
                    _CS0026 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0026, CSharpErrorIdentifiers.CS0026);
                return _CS0026;
            }
        }
        private static ICSharpCompilerReferenceError _CS0026;

        /// <summary><para>C&#9839; compiler error &#35;27:</para><para>Keyword 'this' is not available in the current context</para></summary>
        public static ICSharpCompilerReferenceError CS0027
        {
            get
            {
                if (_CS0027 == null)
                    _CS0027 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0027, CSharpErrorIdentifiers.CS0027);
                return _CS0027;
            }
        }
        private static ICSharpCompilerReferenceError _CS0027;

        /// <summary><para>C&#9839; compiler error &#35;29:</para><para>Cannot implicitly convert type {0} to {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0029
        {
            get
            {
                if (_CS0029 == null)
                    _CS0029 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0029, CSharpErrorIdentifiers.CS0029);
                return _CS0029;
            }
        }
        private static ICSharpCompilerReferenceError _CS0029;

        /// <summary><para>C&#9839; compiler error &#35;30:</para><para>Cannot convert type {0} to {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0030
        {
            get
            {
                if (_CS0030 == null)
                    _CS0030 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0030, CSharpErrorIdentifiers.CS0030);
                return _CS0030;
            }
        }
        private static ICSharpCompilerReferenceError _CS0030;

        /// <summary><para>C&#9839; compiler error &#35;31:</para><para>Constant value {0} cannot be converted to a {1}. (use 'unchecked' syntax to override)</para></summary>
        public static ICSharpCompilerReferenceError CS0031
        {
            get
            {
                if (_CS0031 == null)
                    _CS0031 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0031, CSharpErrorIdentifiers.CS0031);
                return _CS0031;
            }
        }
        private static ICSharpCompilerReferenceError _CS0031;

        /// <summary><para>C&#9839; compiler error &#35;34:</para><para>Operator {0} is ambiguous on operands of type {1} and {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0034
        {
            get
            {
                if (_CS0034 == null)
                    _CS0034 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0034, CSharpErrorIdentifiers.CS0034);
                return _CS0034;
            }
        }
        private static ICSharpCompilerReferenceError _CS0034;

        /// <summary><para>C&#9839; compiler error &#35;35:</para><para>Operator {0} is ambiguous on an operand of type {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0035
        {
            get
            {
                if (_CS0035 == null)
                    _CS0035 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0035, CSharpErrorIdentifiers.CS0035);
                return _CS0035;
            }
        }
        private static ICSharpCompilerReferenceError _CS0035;

        /// <summary><para>C&#9839; compiler error &#35;36:</para><para>An out parameter cannot have the '[In]' attribute</para></summary>
        public static ICSharpCompilerReferenceError CS0036
        {
            get
            {
                if (_CS0036 == null)
                    _CS0036 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0036, CSharpErrorIdentifiers.CS0036);
                return _CS0036;
            }
        }
        private static ICSharpCompilerReferenceError _CS0036;

        /// <summary><para>C&#9839; compiler error &#35;37:</para><para>Cannot convert null to {0} because it is a non-nullable value type</para></summary>
        public static ICSharpCompilerReferenceError CS0037
        {
            get
            {
                if (_CS0037 == null)
                    _CS0037 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0037, CSharpErrorIdentifiers.CS0037);
                return _CS0037;
            }
        }
        private static ICSharpCompilerReferenceError _CS0037;

        /// <summary><para>C&#9839; compiler error &#35;38:</para><para>Cannot access a nonstatic member of outer type {0} via nested type {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0038
        {
            get
            {
                if (_CS0038 == null)
                    _CS0038 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0038, CSharpErrorIdentifiers.CS0038);
                return _CS0038;
            }
        }
        private static ICSharpCompilerReferenceError _CS0038;

        /// <summary><para>C&#9839; compiler error &#35;39:</para><para>Cannot convert type {0} to {1} via a reference conversion, boxing conversion, unboxing conversion, wrapping conversion, or null type conversion</para></summary>
        public static ICSharpCompilerReferenceError CS0039
        {
            get
            {
                if (_CS0039 == null)
                    _CS0039 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0039, CSharpErrorIdentifiers.CS0039);
                return _CS0039;
            }
        }
        private static ICSharpCompilerReferenceError _CS0039;

        /// <summary><para>C&#9839; compiler error &#35;40:</para><para>Unexpected error creating debug information file — {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0040
        {
            get
            {
                if (_CS0040 == null)
                    _CS0040 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0040, CSharpErrorIdentifiers.CS0040);
                return _CS0040;
            }
        }
        private static ICSharpCompilerReferenceError _CS0040;

        /// <summary><para>C&#9839; compiler error &#35;41:</para><para>The fully qualified name for {0} is too long for debug information. Compile without '/debug' option.</para></summary>
        public static ICSharpCompilerReferenceError CS0041
        {
            get
            {
                if (_CS0041 == null)
                    _CS0041 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0041, CSharpErrorIdentifiers.CS0041);
                return _CS0041;
            }
        }
        private static ICSharpCompilerReferenceError _CS0041;

        /// <summary><para>C&#9839; compiler error &#35;42:</para><para>Unexpected error creating debug information file {0} — {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0042
        {
            get
            {
                if (_CS0042 == null)
                    _CS0042 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0042, CSharpErrorIdentifiers.CS0042);
                return _CS0042;
            }
        }
        private static ICSharpCompilerReferenceError _CS0042;

        /// <summary><para>C&#9839; compiler error &#35;43:</para><para>PDB file {0} has an incorrect or out-of-date format. Delete it and rebuild.</para></summary>
        public static ICSharpCompilerReferenceError CS0043
        {
            get
            {
                if (_CS0043 == null)
                    _CS0043 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0043, CSharpErrorIdentifiers.CS0043);
                return _CS0043;
            }
        }
        private static ICSharpCompilerReferenceError _CS0043;

        /// <summary><para>C&#9839; compiler error &#35;50:</para><para>Inconsistent accessibility: return type {0} is less accessible than method {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0050
        {
            get
            {
                if (_CS0050 == null)
                    _CS0050 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0050, CSharpErrorIdentifiers.CS0050);
                return _CS0050;
            }
        }
        private static ICSharpCompilerReferenceError _CS0050;

        /// <summary><para>C&#9839; compiler error &#35;51:</para><para>Inconsistent accessibility: parameter type {0} is less accessible than method {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0051
        {
            get
            {
                if (_CS0051 == null)
                    _CS0051 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0051, CSharpErrorIdentifiers.CS0051);
                return _CS0051;
            }
        }
        private static ICSharpCompilerReferenceError _CS0051;

        /// <summary><para>C&#9839; compiler error &#35;52:</para><para>Inconsistent accessibility: field type {0} is less accessible than field {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0052
        {
            get
            {
                if (_CS0052 == null)
                    _CS0052 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0052, CSharpErrorIdentifiers.CS0052);
                return _CS0052;
            }
        }
        private static ICSharpCompilerReferenceError _CS0052;

        /// <summary><para>C&#9839; compiler error &#35;53:</para><para>Inconsistent accessibility: property type {0} is less accessible than property {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0053
        {
            get
            {
                if (_CS0053 == null)
                    _CS0053 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0053, CSharpErrorIdentifiers.CS0053);
                return _CS0053;
            }
        }
        private static ICSharpCompilerReferenceError _CS0053;

        /// <summary><para>C&#9839; compiler error &#35;54:</para><para>Inconsistent accessibility: indexer return type {0} is less accessible than indexer {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0054
        {
            get
            {
                if (_CS0054 == null)
                    _CS0054 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0054, CSharpErrorIdentifiers.CS0054);
                return _CS0054;
            }
        }
        private static ICSharpCompilerReferenceError _CS0054;

        /// <summary><para>C&#9839; compiler error &#35;55:</para><para>Inconsistent accessibility: parameter type {0} is less accessible than indexer {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0055
        {
            get
            {
                if (_CS0055 == null)
                    _CS0055 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0055, CSharpErrorIdentifiers.CS0055);
                return _CS0055;
            }
        }
        private static ICSharpCompilerReferenceError _CS0055;

        /// <summary><para>C&#9839; compiler error &#35;56:</para><para>Inconsistent accessibility: return type {0} is less accessible than operator {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0056
        {
            get
            {
                if (_CS0056 == null)
                    _CS0056 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0056, CSharpErrorIdentifiers.CS0056);
                return _CS0056;
            }
        }
        private static ICSharpCompilerReferenceError _CS0056;

        /// <summary><para>C&#9839; compiler error &#35;57:</para><para>Inconsistent accessibility: parameter type {0} is less accessible than operator {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0057
        {
            get
            {
                if (_CS0057 == null)
                    _CS0057 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0057, CSharpErrorIdentifiers.CS0057);
                return _CS0057;
            }
        }
        private static ICSharpCompilerReferenceError _CS0057;

        /// <summary><para>C&#9839; compiler error &#35;58:</para><para>Inconsistent accessibility: return type {0} is less accessible than delegate {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0058
        {
            get
            {
                if (_CS0058 == null)
                    _CS0058 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0058, CSharpErrorIdentifiers.CS0058);
                return _CS0058;
            }
        }
        private static ICSharpCompilerReferenceError _CS0058;

        /// <summary><para>C&#9839; compiler error &#35;59:</para><para>Inconsistent accessibility: parameter type {0} is less accessible than delegate {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0059
        {
            get
            {
                if (_CS0059 == null)
                    _CS0059 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0059, CSharpErrorIdentifiers.CS0059);
                return _CS0059;
            }
        }
        private static ICSharpCompilerReferenceError _CS0059;

        /// <summary><para>C&#9839; compiler error &#35;60:</para><para>Inconsistent accessibility: base class {0} is less accessible than class {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0060
        {
            get
            {
                if (_CS0060 == null)
                    _CS0060 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0060, CSharpErrorIdentifiers.CS0060);
                return _CS0060;
            }
        }
        private static ICSharpCompilerReferenceError _CS0060;

        /// <summary><para>C&#9839; compiler error &#35;61:</para><para>Inconsistent accessibility: base interface {0} is less accessible than interface {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0061
        {
            get
            {
                if (_CS0061 == null)
                    _CS0061 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0061, CSharpErrorIdentifiers.CS0061);
                return _CS0061;
            }
        }
        private static ICSharpCompilerReferenceError _CS0061;

        /// <summary><para>C&#9839; compiler error &#35;65:</para><para>{0}: event property must have both add and remove accessors</para></summary>
        public static ICSharpCompilerReferenceError CS0065
        {
            get
            {
                if (_CS0065 == null)
                    _CS0065 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0065, CSharpErrorIdentifiers.CS0065);
                return _CS0065;
            }
        }
        private static ICSharpCompilerReferenceError _CS0065;

        /// <summary><para>C&#9839; compiler error &#35;66:</para><para>{0}: event must be of a delegate type</para></summary>
        public static ICSharpCompilerReferenceError CS0066
        {
            get
            {
                if (_CS0066 == null)
                    _CS0066 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0066, CSharpErrorIdentifiers.CS0066);
                return _CS0066;
            }
        }
        private static ICSharpCompilerReferenceError _CS0066;

        /// <summary><para>C&#9839; compiler error &#35;68:</para><para>{0}: event in interface cannot have initializer</para></summary>
        public static ICSharpCompilerReferenceError CS0068
        {
            get
            {
                if (_CS0068 == null)
                    _CS0068 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0068, CSharpErrorIdentifiers.CS0068);
                return _CS0068;
            }
        }
        private static ICSharpCompilerReferenceError _CS0068;

        /// <summary><para>C&#9839; compiler error &#35;69:</para><para>An event in an interface cannot have add or remove accessors</para></summary>
        public static ICSharpCompilerReferenceError CS0069
        {
            get
            {
                if (_CS0069 == null)
                    _CS0069 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0069, CSharpErrorIdentifiers.CS0069);
                return _CS0069;
            }
        }
        private static ICSharpCompilerReferenceError _CS0069;

        /// <summary><para>C&#9839; compiler error &#35;70:</para><para>The event {0} can only appear on the left hand side of += or -= (except when used from within the type {1})</para></summary>
        public static ICSharpCompilerReferenceError CS0070
        {
            get
            {
                if (_CS0070 == null)
                    _CS0070 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0070, CSharpErrorIdentifiers.CS0070);
                return _CS0070;
            }
        }
        private static ICSharpCompilerReferenceError _CS0070;

        /// <summary><para>C&#9839; compiler error &#35;71:</para><para>An explicit interface implementation of an event must use event accessor syntax</para></summary>
        public static ICSharpCompilerReferenceError CS0071
        {
            get
            {
                if (_CS0071 == null)
                    _CS0071 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0071, CSharpErrorIdentifiers.CS0071);
                return _CS0071;
            }
        }
        private static ICSharpCompilerReferenceError _CS0071;

        /// <summary><para>C&#9839; compiler error &#35;72:</para><para>{0} : cannot override; {1} is not an event</para></summary>
        public static ICSharpCompilerReferenceError CS0072
        {
            get
            {
                if (_CS0072 == null)
                    _CS0072 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0072, CSharpErrorIdentifiers.CS0072);
                return _CS0072;
            }
        }
        private static ICSharpCompilerReferenceError _CS0072;

        /// <summary><para>C&#9839; compiler error &#35;73:</para><para>An add or remove accessor must have a body</para></summary>
        public static ICSharpCompilerReferenceError CS0073
        {
            get
            {
                if (_CS0073 == null)
                    _CS0073 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0073, CSharpErrorIdentifiers.CS0073);
                return _CS0073;
            }
        }
        private static ICSharpCompilerReferenceError _CS0073;

        /// <summary><para>C&#9839; compiler error &#35;74:</para><para>{0}: abstract event cannot have initializer</para></summary>
        public static ICSharpCompilerReferenceError CS0074
        {
            get
            {
                if (_CS0074 == null)
                    _CS0074 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0074, CSharpErrorIdentifiers.CS0074);
                return _CS0074;
            }
        }
        private static ICSharpCompilerReferenceError _CS0074;

        /// <summary><para>C&#9839; compiler error &#35;75:</para><para>To cast a negative value, you must enclose the value in parentheses</para></summary>
        public static ICSharpCompilerReferenceError CS0075
        {
            get
            {
                if (_CS0075 == null)
                    _CS0075 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0075, CSharpErrorIdentifiers.CS0075);
                return _CS0075;
            }
        }
        private static ICSharpCompilerReferenceError _CS0075;

        /// <summary><para>C&#9839; compiler error &#35;76:</para><para>The enumerator name 'value__' is reserved and cannot be used</para></summary>
        public static ICSharpCompilerReferenceError CS0076
        {
            get
            {
                if (_CS0076 == null)
                    _CS0076 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0076, CSharpErrorIdentifiers.CS0076);
                return _CS0076;
            }
        }
        private static ICSharpCompilerReferenceError _CS0076;

        /// <summary><para>C&#9839; compiler error &#35;77:</para><para>The as operator must be used with a reference type or nullable type ({0} is a non-nullable value type).</para></summary>
        public static ICSharpCompilerReferenceError CS0077
        {
            get
            {
                if (_CS0077 == null)
                    _CS0077 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0077, CSharpErrorIdentifiers.CS0077);
                return _CS0077;
            }
        }
        private static ICSharpCompilerReferenceError _CS0077;

        /// <summary><para>C&#9839; compiler error &#35;79:</para><para>The event {0} can only appear on the left hand side of += or -=</para></summary>
        public static ICSharpCompilerReferenceError CS0079
        {
            get
            {
                if (_CS0079 == null)
                    _CS0079 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0079, CSharpErrorIdentifiers.CS0079);
                return _CS0079;
            }
        }
        private static ICSharpCompilerReferenceError _CS0079;

        /// <summary><para>C&#9839; compiler error &#35;80:</para><para>Constraints are not allowed on non-generic declarations</para></summary>
        public static ICSharpCompilerReferenceError CS0080
        {
            get
            {
                if (_CS0080 == null)
                    _CS0080 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0080, CSharpErrorIdentifiers.CS0080);
                return _CS0080;
            }
        }
        private static ICSharpCompilerReferenceError _CS0080;

        /// <summary><para>C&#9839; compiler error &#35;81:</para><para>GenericParameter parameter declaration must be an identifier not a type</para></summary>
        public static ICSharpCompilerReferenceError CS0081
        {
            get
            {
                if (_CS0081 == null)
                    _CS0081 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0081, CSharpErrorIdentifiers.CS0081);
                return _CS0081;
            }
        }
        private static ICSharpCompilerReferenceError _CS0081;

        /// <summary><para>C&#9839; compiler error &#35;82:</para><para>GenericParameter {0} already reserves a member called {1} with the same parameter types</para></summary>
        public static ICSharpCompilerReferenceError CS0082
        {
            get
            {
                if (_CS0082 == null)
                    _CS0082 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0082, CSharpErrorIdentifiers.CS0082);
                return _CS0082;
            }
        }
        private static ICSharpCompilerReferenceError _CS0082;

        /// <summary><para>C&#9839; compiler error &#35;100:</para><para>The parameter name {0} is a duplicate</para></summary>
        public static ICSharpCompilerReferenceError CS0100
        {
            get
            {
                if (_CS0100 == null)
                    _CS0100 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0100, CSharpErrorIdentifiers.CS0100);
                return _CS0100;
            }
        }
        private static ICSharpCompilerReferenceError _CS0100;

        /// <summary><para>C&#9839; compiler error &#35;101:</para><para>The namespace {0} already contains a definition for {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0101
        {
            get
            {
                if (_CS0101 == null)
                    _CS0101 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0101, CSharpErrorIdentifiers.CS0101);
                return _CS0101;
            }
        }
        private static ICSharpCompilerReferenceError _CS0101;

        /// <summary><para>C&#9839; compiler error &#35;102:</para><para>The type {0} already contains a definition for {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0102
        {
            get
            {
                if (_CS0102 == null)
                    _CS0102 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0102, CSharpErrorIdentifiers.CS0102);
                return _CS0102;
            }
        }
        private static ICSharpCompilerReferenceError _CS0102;

        /// <summary><para>C&#9839; compiler error &#35;103:</para><para>The name {0} does not exist in the current context</para></summary>
        public static ICSharpCompilerReferenceError CS0103
        {
            get
            {
                if (_CS0103 == null)
                    _CS0103 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0103, CSharpErrorIdentifiers.CS0103);
                return _CS0103;
            }
        }
        private static ICSharpCompilerReferenceError _CS0103;

        /// <summary><para>C&#9839; compiler error &#35;104:</para><para>{0} is an ambiguous reference between {1} and {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0104
        {
            get
            {
                if (_CS0104 == null)
                    _CS0104 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0104, CSharpErrorIdentifiers.CS0104);
                return _CS0104;
            }
        }
        private static ICSharpCompilerReferenceError _CS0104;

        /// <summary><para>C&#9839; compiler error &#35;106:</para><para>The modifier {0} is not valid for this item</para></summary>
        public static ICSharpCompilerReferenceError CS0106
        {
            get
            {
                if (_CS0106 == null)
                    _CS0106 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0106, CSharpErrorIdentifiers.CS0106);
                return _CS0106;
            }
        }
        private static ICSharpCompilerReferenceError _CS0106;

        /// <summary><para>C&#9839; compiler error &#35;107:</para><para>More than one protection modifier</para></summary>
        public static ICSharpCompilerReferenceError CS0107
        {
            get
            {
                if (_CS0107 == null)
                    _CS0107 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0107, CSharpErrorIdentifiers.CS0107);
                return _CS0107;
            }
        }
        private static ICSharpCompilerReferenceError _CS0107;

        /// <summary><para>C&#9839; compiler error &#35;110:</para><para>The evaluation of the constant value for {0} involves a circular definition</para></summary>
        public static ICSharpCompilerReferenceError CS0110
        {
            get
            {
                if (_CS0110 == null)
                    _CS0110 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0110, CSharpErrorIdentifiers.CS0110);
                return _CS0110;
            }
        }
        private static ICSharpCompilerReferenceError _CS0110;

        /// <summary><para>C&#9839; compiler error &#35;111:</para><para>GenericParameter {0} already defines a member called {1} with the same parameter types</para></summary>
        public static ICSharpCompilerReferenceError CS0111
        {
            get
            {
                if (_CS0111 == null)
                    _CS0111 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0111, CSharpErrorIdentifiers.CS0111);
                return _CS0111;
            }
        }
        private static ICSharpCompilerReferenceError _CS0111;

        /// <summary><para>C&#9839; compiler error &#35;112:</para><para>A static member {0} cannot be marked as override, virtual or abstract</para></summary>
        public static ICSharpCompilerReferenceError CS0112
        {
            get
            {
                if (_CS0112 == null)
                    _CS0112 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0112, CSharpErrorIdentifiers.CS0112);
                return _CS0112;
            }
        }
        private static ICSharpCompilerReferenceError _CS0112;

        /// <summary><para>C&#9839; compiler error &#35;113:</para><para>A member {0} marked as override cannot be marked as new or virtual</para></summary>
        public static ICSharpCompilerReferenceError CS0113
        {
            get
            {
                if (_CS0113 == null)
                    _CS0113 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0113, CSharpErrorIdentifiers.CS0113);
                return _CS0113;
            }
        }
        private static ICSharpCompilerReferenceError _CS0113;

        /// <summary><para>C&#9839; compiler error &#35;115:</para><para>{0} : no suitable method found to override</para></summary>
        public static ICSharpCompilerReferenceError CS0115
        {
            get
            {
                if (_CS0115 == null)
                    _CS0115 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0115, CSharpErrorIdentifiers.CS0115);
                return _CS0115;
            }
        }
        private static ICSharpCompilerReferenceError _CS0115;

        /// <summary><para>C&#9839; compiler error &#35;116:</para><para>A namespace does not directly contain members such as fields or methods</para></summary>
        public static ICSharpCompilerReferenceError CS0116
        {
            get
            {
                if (_CS0116 == null)
                    _CS0116 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0116, CSharpErrorIdentifiers.CS0116);
                return _CS0116;
            }
        }
        private static ICSharpCompilerReferenceError _CS0116;

        /// <summary><para>C&#9839; compiler error &#35;117:</para><para>{0} does not contain a definition for 'identifier'</para></summary>
        public static ICSharpCompilerReferenceError CS0117
        {
            get
            {
                if (_CS0117 == null)
                    _CS0117 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0117, CSharpErrorIdentifiers.CS0117);
                return _CS0117;
            }
        }
        private static ICSharpCompilerReferenceError _CS0117;

        /// <summary><para>C&#9839; compiler error &#35;118:</para><para>{0} is a {1} but is used like a {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0118
        {
            get
            {
                if (_CS0118 == null)
                    _CS0118 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0118, CSharpErrorIdentifiers.CS0118);
                return _CS0118;
            }
        }
        private static ICSharpCompilerReferenceError _CS0118;

        /// <summary><para>C&#9839; compiler error &#35;119:</para><para>{0} is a {1}, which is not valid in the given context.</para></summary>
        public static ICSharpCompilerReferenceError CS0119
        {
            get
            {
                if (_CS0119 == null)
                    _CS0119 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0119, CSharpErrorIdentifiers.CS0119);
                return _CS0119;
            }
        }
        private static ICSharpCompilerReferenceError _CS0119;

        /// <summary><para>C&#9839; compiler error &#35;120:</para><para>An object reference is required for the nonstatic field, method, or property {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0120
        {
            get
            {
                if (_CS0120 == null)
                    _CS0120 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0120, CSharpErrorIdentifiers.CS0120);
                return _CS0120;
            }
        }
        private static ICSharpCompilerReferenceError _CS0120;

        /// <summary><para>C&#9839; compiler error &#35;121:</para><para>The call is ambiguous between the following methods or properties: {0} and {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0121
        {
            get
            {
                if (_CS0121 == null)
                    _CS0121 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0121, CSharpErrorIdentifiers.CS0121);
                return _CS0121;
            }
        }
        private static ICSharpCompilerReferenceError _CS0121;

        /// <summary><para>C&#9839; compiler error &#35;122:</para><para>{0} is inaccessible due to its protection level</para></summary>
        public static ICSharpCompilerReferenceError CS0122
        {
            get
            {
                if (_CS0122 == null)
                    _CS0122 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0122, CSharpErrorIdentifiers.CS0122);
                return _CS0122;
            }
        }
        private static ICSharpCompilerReferenceError _CS0122;

        /// <summary><para>C&#9839; compiler error &#35;123:</para><para>No overload for {0} matches delegate {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0123
        {
            get
            {
                if (_CS0123 == null)
                    _CS0123 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0123, CSharpErrorIdentifiers.CS0123);
                return _CS0123;
            }
        }
        private static ICSharpCompilerReferenceError _CS0123;

        /// <summary><para>C&#9839; compiler error &#35;126:</para><para>An object of a type convertible to {0} is required</para></summary>
        public static ICSharpCompilerReferenceError CS0126
        {
            get
            {
                if (_CS0126 == null)
                    _CS0126 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0126, CSharpErrorIdentifiers.CS0126);
                return _CS0126;
            }
        }
        private static ICSharpCompilerReferenceError _CS0126;

        /// <summary><para>C&#9839; compiler error &#35;127:</para><para>Since {0} returns void, a return keyword must not be followed by an object expression</para></summary>
        public static ICSharpCompilerReferenceError CS0127
        {
            get
            {
                if (_CS0127 == null)
                    _CS0127 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0127, CSharpErrorIdentifiers.CS0127);
                return _CS0127;
            }
        }
        private static ICSharpCompilerReferenceError _CS0127;

        /// <summary><para>C&#9839; compiler error &#35;128:</para><para>A local variable named {0} is already defined in this scope</para></summary>
        public static ICSharpCompilerReferenceError CS0128
        {
            get
            {
                if (_CS0128 == null)
                    _CS0128 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0128, CSharpErrorIdentifiers.CS0128);
                return _CS0128;
            }
        }
        private static ICSharpCompilerReferenceError _CS0128;

        /// <summary><para>C&#9839; compiler error &#35;131:</para><para>The left-hand side of an assignment must be a variable, property or indexer</para></summary>
        public static ICSharpCompilerReferenceError CS0131
        {
            get
            {
                if (_CS0131 == null)
                    _CS0131 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0131, CSharpErrorIdentifiers.CS0131);
                return _CS0131;
            }
        }
        private static ICSharpCompilerReferenceError _CS0131;

        /// <summary><para>C&#9839; compiler error &#35;132:</para><para>{0} : a static constructor must be parameterless</para></summary>
        public static ICSharpCompilerReferenceError CS0132
        {
            get
            {
                if (_CS0132 == null)
                    _CS0132 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0132, CSharpErrorIdentifiers.CS0132);
                return _CS0132;
            }
        }
        private static ICSharpCompilerReferenceError _CS0132;

        /// <summary><para>C&#9839; compiler error &#35;133:</para><para>The expression being assigned to {0} must be constant</para></summary>
        public static ICSharpCompilerReferenceError CS0133
        {
            get
            {
                if (_CS0133 == null)
                    _CS0133 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0133, CSharpErrorIdentifiers.CS0133);
                return _CS0133;
            }
        }
        private static ICSharpCompilerReferenceError _CS0133;

        /// <summary><para>C&#9839; compiler error &#35;134:</para><para>{0} is of type {1}. A const field of a reference type other than string can only be initialized with null.</para></summary>
        public static ICSharpCompilerReferenceError CS0134
        {
            get
            {
                if (_CS0134 == null)
                    _CS0134 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0134, CSharpErrorIdentifiers.CS0134);
                return _CS0134;
            }
        }
        private static ICSharpCompilerReferenceError _CS0134;

        /// <summary><para>C&#9839; compiler error &#35;135:</para><para>{0} conflicts with the declaration {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0135
        {
            get
            {
                if (_CS0135 == null)
                    _CS0135 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0135, CSharpErrorIdentifiers.CS0135);
                return _CS0135;
            }
        }
        private static ICSharpCompilerReferenceError _CS0135;

        /// <summary><para>C&#9839; compiler error &#35;136:</para><para>A local variable named {0} cannot be declared in this scope because it would give a different meaning to {0}, which is already used in a 'parent or current/child' scope to denote something else</para></summary>
        public static ICSharpCompilerReferenceError CS0136
        {
            get
            {
                if (_CS0136 == null)
                    _CS0136 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0136, CSharpErrorIdentifiers.CS0136);
                return _CS0136;
            }
        }
        private static ICSharpCompilerReferenceError _CS0136;

        /// <summary><para>C&#9839; compiler error &#35;138:</para><para>A using namespace directive can only be applied to namespaces; {0} is a type not a namespace</para></summary>
        public static ICSharpCompilerReferenceError CS0138
        {
            get
            {
                if (_CS0138 == null)
                    _CS0138 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0138, CSharpErrorIdentifiers.CS0138);
                return _CS0138;
            }
        }
        private static ICSharpCompilerReferenceError _CS0138;

        /// <summary><para>C&#9839; compiler error &#35;139:</para><para>No enclosing loop out of which to break or continue</para></summary>
        public static ICSharpCompilerReferenceError CS0139
        {
            get
            {
                if (_CS0139 == null)
                    _CS0139 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0139, CSharpErrorIdentifiers.CS0139);
                return _CS0139;
            }
        }
        private static ICSharpCompilerReferenceError _CS0139;

        /// <summary><para>C&#9839; compiler error &#35;140:</para><para>The label {0} is a duplicate</para></summary>
        public static ICSharpCompilerReferenceError CS0140
        {
            get
            {
                if (_CS0140 == null)
                    _CS0140 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0140, CSharpErrorIdentifiers.CS0140);
                return _CS0140;
            }
        }
        private static ICSharpCompilerReferenceError _CS0140;

        /// <summary><para>C&#9839; compiler error &#35;143:</para><para>The type {0} has no constructors defined</para></summary>
        public static ICSharpCompilerReferenceError CS0143
        {
            get
            {
                if (_CS0143 == null)
                    _CS0143 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0143, CSharpErrorIdentifiers.CS0143);
                return _CS0143;
            }
        }
        private static ICSharpCompilerReferenceError _CS0143;

        /// <summary><para>C&#9839; compiler error &#35;144:</para><para>Cannot create an instance of the abstract class or interface {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0144
        {
            get
            {
                if (_CS0144 == null)
                    _CS0144 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0144, CSharpErrorIdentifiers.CS0144);
                return _CS0144;
            }
        }
        private static ICSharpCompilerReferenceError _CS0144;

        /// <summary><para>C&#9839; compiler error &#35;145:</para><para>A const field requires a value to be provided</para></summary>
        public static ICSharpCompilerReferenceError CS0145
        {
            get
            {
                if (_CS0145 == null)
                    _CS0145 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0145, CSharpErrorIdentifiers.CS0145);
                return _CS0145;
            }
        }
        private static ICSharpCompilerReferenceError _CS0145;

        /// <summary><para>C&#9839; compiler error &#35;146:</para><para>Circular base class dependency involving {0} and {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0146
        {
            get
            {
                if (_CS0146 == null)
                    _CS0146 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0146, CSharpErrorIdentifiers.CS0146);
                return _CS0146;
            }
        }
        private static ICSharpCompilerReferenceError _CS0146;

        /// <summary><para>C&#9839; compiler error &#35;148:</para><para>The delegate {0} does not have a valid constructor</para></summary>
        public static ICSharpCompilerReferenceError CS0148
        {
            get
            {
                if (_CS0148 == null)
                    _CS0148 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0148, CSharpErrorIdentifiers.CS0148);
                return _CS0148;
            }
        }
        private static ICSharpCompilerReferenceError _CS0148;

        /// <summary><para>C&#9839; compiler error &#35;149:</para><para>Method name expected</para></summary>
        public static ICSharpCompilerReferenceError CS0149
        {
            get
            {
                if (_CS0149 == null)
                    _CS0149 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0149, CSharpErrorIdentifiers.CS0149);
                return _CS0149;
            }
        }
        private static ICSharpCompilerReferenceError _CS0149;

        /// <summary><para>C&#9839; compiler error &#35;150:</para><para>A constant value is expected</para></summary>
        public static ICSharpCompilerReferenceError CS0150
        {
            get
            {
                if (_CS0150 == null)
                    _CS0150 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0150, CSharpErrorIdentifiers.CS0150);
                return _CS0150;
            }
        }
        private static ICSharpCompilerReferenceError _CS0150;

        /// <summary><para>C&#9839; compiler error &#35;151:</para><para>A value of an integral type expected</para></summary>
        public static ICSharpCompilerReferenceError CS0151
        {
            get
            {
                if (_CS0151 == null)
                    _CS0151 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0151, CSharpErrorIdentifiers.CS0151);
                return _CS0151;
            }
        }
        private static ICSharpCompilerReferenceError _CS0151;

        /// <summary><para>C&#9839; compiler error &#35;152:</para><para>The label {0} already occurs in this switch statement</para></summary>
        public static ICSharpCompilerReferenceError CS0152
        {
            get
            {
                if (_CS0152 == null)
                    _CS0152 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0152, CSharpErrorIdentifiers.CS0152);
                return _CS0152;
            }
        }
        private static ICSharpCompilerReferenceError _CS0152;

        /// <summary><para>C&#9839; compiler error &#35;153:</para><para>A goto case is only valid inside a switch statement</para></summary>
        public static ICSharpCompilerReferenceError CS0153
        {
            get
            {
                if (_CS0153 == null)
                    _CS0153 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0153, CSharpErrorIdentifiers.CS0153);
                return _CS0153;
            }
        }
        private static ICSharpCompilerReferenceError _CS0153;

        /// <summary><para>C&#9839; compiler error &#35;154:</para><para>The property or indexer 'property' cannot be used in this context because it lacks the get accessor</para></summary>
        public static ICSharpCompilerReferenceError CS0154
        {
            get
            {
                if (_CS0154 == null)
                    _CS0154 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0154, CSharpErrorIdentifiers.CS0154);
                return _CS0154;
            }
        }
        private static ICSharpCompilerReferenceError _CS0154;

        /// <summary><para>C&#9839; compiler error &#35;155:</para><para>The type caught or thrown must be derived from System.Exception</para></summary>
        public static ICSharpCompilerReferenceError CS0155
        {
            get
            {
                if (_CS0155 == null)
                    _CS0155 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0155, CSharpErrorIdentifiers.CS0155);
                return _CS0155;
            }
        }
        private static ICSharpCompilerReferenceError _CS0155;

        /// <summary><para>C&#9839; compiler error &#35;156:</para><para>A throw statement with no arguments is not allowed in a finally clause that is nested inside the nearest enclosing catch clause</para></summary>
        public static ICSharpCompilerReferenceError CS0156
        {
            get
            {
                if (_CS0156 == null)
                    _CS0156 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0156, CSharpErrorIdentifiers.CS0156);
                return _CS0156;
            }
        }
        private static ICSharpCompilerReferenceError _CS0156;

        /// <summary><para>C&#9839; compiler error &#35;157:</para><para>Control cannot leave the body of a finally clause</para></summary>
        public static ICSharpCompilerReferenceError CS0157
        {
            get
            {
                if (_CS0157 == null)
                    _CS0157 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0157, CSharpErrorIdentifiers.CS0157);
                return _CS0157;
            }
        }
        private static ICSharpCompilerReferenceError _CS0157;

        /// <summary><para>C&#9839; compiler error &#35;158:</para><para>The label {0} shadows another label by the same name in a contained scope</para></summary>
        public static ICSharpCompilerReferenceError CS0158
        {
            get
            {
                if (_CS0158 == null)
                    _CS0158 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0158, CSharpErrorIdentifiers.CS0158);
                return _CS0158;
            }
        }
        private static ICSharpCompilerReferenceError _CS0158;

        /// <summary><para>C&#9839; compiler error &#35;159:</para><para>No such label {0} within the scope of the goto statement</para></summary>
        public static ICSharpCompilerReferenceError CS0159
        {
            get
            {
                if (_CS0159 == null)
                    _CS0159 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0159, CSharpErrorIdentifiers.CS0159);
                return _CS0159;
            }
        }
        private static ICSharpCompilerReferenceError _CS0159;

        /// <summary><para>C&#9839; compiler error &#35;160:</para><para>A previous catch clause already catches all exceptions of this or of a super type ({0})</para></summary>
        public static ICSharpCompilerReferenceError CS0160
        {
            get
            {
                if (_CS0160 == null)
                    _CS0160 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0160, CSharpErrorIdentifiers.CS0160);
                return _CS0160;
            }
        }
        private static ICSharpCompilerReferenceError _CS0160;

        /// <summary><para>C&#9839; compiler error &#35;161:</para><para>{0}: not all code paths return a value</para></summary>
        public static ICSharpCompilerReferenceError CS0161
        {
            get
            {
                if (_CS0161 == null)
                    _CS0161 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0161, CSharpErrorIdentifiers.CS0161);
                return _CS0161;
            }
        }
        private static ICSharpCompilerReferenceError _CS0161;

        /// <summary><para>C&#9839; compiler error &#35;163:</para><para>Control cannot fall through from one case label ({0}) to another</para></summary>
        public static ICSharpCompilerReferenceError CS0163
        {
            get
            {
                if (_CS0163 == null)
                    _CS0163 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0163, CSharpErrorIdentifiers.CS0163);
                return _CS0163;
            }
        }
        private static ICSharpCompilerReferenceError _CS0163;

        /// <summary><para>C&#9839; compiler error &#35;165:</para><para>Use of unassigned local variable {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0165
        {
            get
            {
                if (_CS0165 == null)
                    _CS0165 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0165, CSharpErrorIdentifiers.CS0165);
                return _CS0165;
            }
        }
        private static ICSharpCompilerReferenceError _CS0165;

        /// <summary><para>C&#9839; compiler error &#35;167:</para><para>The delegate {0} is missing the Invoke method</para></summary>
        public static ICSharpCompilerReferenceError CS0167
        {
            get
            {
                if (_CS0167 == null)
                    _CS0167 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0167, CSharpErrorIdentifiers.CS0167);
                return _CS0167;
            }
        }
        private static ICSharpCompilerReferenceError _CS0167;

        /// <summary><para>C&#9839; compiler error &#35;170:</para><para>Use of possibly unassigned field {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0170
        {
            get
            {
                if (_CS0170 == null)
                    _CS0170 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0170, CSharpErrorIdentifiers.CS0170);
                return _CS0170;
            }
        }
        private static ICSharpCompilerReferenceError _CS0170;

        /// <summary><para>C&#9839; compiler error &#35;171:</para><para>Backing field for automatically implemented property {0} must be fully assigned before control is returned to the caller. Consider calling the default constructor from a constructor initializer.</para></summary>
        public static ICSharpCompilerReferenceError CS0171
        {
            get
            {
                if (_CS0171 == null)
                    _CS0171 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0171, CSharpErrorIdentifiers.CS0171);
                return _CS0171;
            }
        }
        private static ICSharpCompilerReferenceError _CS0171;

        /// <summary><para>C&#9839; compiler error &#35;172:</para><para>GenericParameter of conditional expression cannot be determined because {0} and {1} implicitly convert to one another</para></summary>
        public static ICSharpCompilerReferenceError CS0172
        {
            get
            {
                if (_CS0172 == null)
                    _CS0172 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0172, CSharpErrorIdentifiers.CS0172);
                return _CS0172;
            }
        }
        private static ICSharpCompilerReferenceError _CS0172;

        /// <summary><para>C&#9839; compiler error &#35;173:</para><para>GenericParameter of conditional expression cannot be determined because there is no implicit conversion between {0} and {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0173
        {
            get
            {
                if (_CS0173 == null)
                    _CS0173 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0173, CSharpErrorIdentifiers.CS0173);
                return _CS0173;
            }
        }
        private static ICSharpCompilerReferenceError _CS0173;

        /// <summary><para>C&#9839; compiler error &#35;174:</para><para>A base class is required for a 'base' reference</para></summary>
        public static ICSharpCompilerReferenceError CS0174
        {
            get
            {
                if (_CS0174 == null)
                    _CS0174 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0174, CSharpErrorIdentifiers.CS0174);
                return _CS0174;
            }
        }
        private static ICSharpCompilerReferenceError _CS0174;

        /// <summary><para>C&#9839; compiler error &#35;175:</para><para>Use of keyword 'base' is not valid in this context</para></summary>
        public static ICSharpCompilerReferenceError CS0175
        {
            get
            {
                if (_CS0175 == null)
                    _CS0175 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0175, CSharpErrorIdentifiers.CS0175);
                return _CS0175;
            }
        }
        private static ICSharpCompilerReferenceError _CS0175;

        /// <summary><para>C&#9839; compiler error &#35;176:</para><para>Static member {0} cannot be accessed with an instance reference; qualify it with a type name instead</para></summary>
        public static ICSharpCompilerReferenceError CS0176
        {
            get
            {
                if (_CS0176 == null)
                    _CS0176 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0176, CSharpErrorIdentifiers.CS0176);
                return _CS0176;
            }
        }
        private static ICSharpCompilerReferenceError _CS0176;

        /// <summary><para>C&#9839; compiler error &#35;177:</para><para>The out parameter {0} must be assigned to before control leaves the current method</para></summary>
        public static ICSharpCompilerReferenceError CS0177
        {
            get
            {
                if (_CS0177 == null)
                    _CS0177 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0177, CSharpErrorIdentifiers.CS0177);
                return _CS0177;
            }
        }
        private static ICSharpCompilerReferenceError _CS0177;

        /// <summary><para>C&#9839; compiler error &#35;178:</para><para>Invalid rank specifier: expected ',' or ']'</para></summary>
        public static ICSharpCompilerReferenceError CS0178
        {
            get
            {
                if (_CS0178 == null)
                    _CS0178 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0178, CSharpErrorIdentifiers.CS0178);
                return _CS0178;
            }
        }
        private static ICSharpCompilerReferenceError _CS0178;

        /// <summary><para>C&#9839; compiler error &#35;179:</para><para>{0} cannot be extern and declare a body</para></summary>
        public static ICSharpCompilerReferenceError CS0179
        {
            get
            {
                if (_CS0179 == null)
                    _CS0179 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0179, CSharpErrorIdentifiers.CS0179);
                return _CS0179;
            }
        }
        private static ICSharpCompilerReferenceError _CS0179;

        /// <summary><para>C&#9839; compiler error &#35;180:</para><para>{0} cannot be both extern and abstract</para></summary>
        public static ICSharpCompilerReferenceError CS0180
        {
            get
            {
                if (_CS0180 == null)
                    _CS0180 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0180, CSharpErrorIdentifiers.CS0180);
                return _CS0180;
            }
        }
        private static ICSharpCompilerReferenceError _CS0180;

        /// <summary><para>C&#9839; compiler error &#35;182:</para><para>An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type</para></summary>
        public static ICSharpCompilerReferenceError CS0182
        {
            get
            {
                if (_CS0182 == null)
                    _CS0182 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0182, CSharpErrorIdentifiers.CS0182);
                return _CS0182;
            }
        }
        private static ICSharpCompilerReferenceError _CS0182;

        /// <summary><para>C&#9839; compiler error &#35;185:</para><para>{0} is not a reference type as required by the lock statement</para></summary>
        public static ICSharpCompilerReferenceError CS0185
        {
            get
            {
                if (_CS0185 == null)
                    _CS0185 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0185, CSharpErrorIdentifiers.CS0185);
                return _CS0185;
            }
        }
        private static ICSharpCompilerReferenceError _CS0185;

        /// <summary><para>C&#9839; compiler error &#35;186:</para><para>Use of null is not valid in this context </para></summary>
        public static ICSharpCompilerReferenceError CS0186
        {
            get
            {
                if (_CS0186 == null)
                    _CS0186 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0186, CSharpErrorIdentifiers.CS0186);
                return _CS0186;
            }
        }
        private static ICSharpCompilerReferenceError _CS0186;

        /// <summary><para>C&#9839; compiler error &#35;188:</para><para>The 'this' object cannot be used before all of its fields are assigned to</para></summary>
        public static ICSharpCompilerReferenceError CS0188
        {
            get
            {
                if (_CS0188 == null)
                    _CS0188 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0188, CSharpErrorIdentifiers.CS0188);
                return _CS0188;
            }
        }
        private static ICSharpCompilerReferenceError _CS0188;

        /// <summary><para>C&#9839; compiler error &#35;191:</para><para>Property or indexer {0} cannot be assigned to -- it is read only</para></summary>
        public static ICSharpCompilerReferenceError CS0191
        {
            get
            {
                if (_CS0191 == null)
                    _CS0191 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0191, CSharpErrorIdentifiers.CS0191);
                return _CS0191;
            }
        }
        private static ICSharpCompilerReferenceError _CS0191;

        /// <summary><para>C&#9839; compiler error &#35;192:</para><para>Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)</para></summary>
        public static ICSharpCompilerReferenceError CS0192
        {
            get
            {
                if (_CS0192 == null)
                    _CS0192 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0192, CSharpErrorIdentifiers.CS0192);
                return _CS0192;
            }
        }
        private static ICSharpCompilerReferenceError _CS0192;

        /// <summary><para>C&#9839; compiler error &#35;193:</para><para>The * or -> operator must be applied to a pointer</para></summary>
        public static ICSharpCompilerReferenceError CS0193
        {
            get
            {
                if (_CS0193 == null)
                    _CS0193 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0193, CSharpErrorIdentifiers.CS0193);
                return _CS0193;
            }
        }
        private static ICSharpCompilerReferenceError _CS0193;

        /// <summary><para>C&#9839; compiler error &#35;196:</para><para>A pointer must be indexed by only one value</para></summary>
        public static ICSharpCompilerReferenceError CS0196
        {
            get
            {
                if (_CS0196 == null)
                    _CS0196 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0196, CSharpErrorIdentifiers.CS0196);
                return _CS0196;
            }
        }
        private static ICSharpCompilerReferenceError _CS0196;

        /// <summary><para>C&#9839; compiler error &#35;198:</para><para>Fields of static readonly field {0} cannot be assigned to (except in a static constructor or a variable initializer)</para></summary>
        public static ICSharpCompilerReferenceError CS0198
        {
            get
            {
                if (_CS0198 == null)
                    _CS0198 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0198, CSharpErrorIdentifiers.CS0198);
                return _CS0198;
            }
        }
        private static ICSharpCompilerReferenceError _CS0198;

        /// <summary><para>C&#9839; compiler error &#35;199:</para><para>Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)</para></summary>
        public static ICSharpCompilerReferenceError CS0199
        {
            get
            {
                if (_CS0199 == null)
                    _CS0199 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0199, CSharpErrorIdentifiers.CS0199);
                return _CS0199;
            }
        }
        private static ICSharpCompilerReferenceError _CS0199;

        /// <summary><para>C&#9839; compiler error &#35;200:</para><para>Property or indexer {0} cannot be assigned to — it is read only</para></summary>
        public static ICSharpCompilerReferenceError CS0200
        {
            get
            {
                if (_CS0200 == null)
                    _CS0200 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0200, CSharpErrorIdentifiers.CS0200);
                return _CS0200;
            }
        }
        private static ICSharpCompilerReferenceError _CS0200;

        /// <summary><para>C&#9839; compiler error &#35;201:</para><para>Only assignment, call, increment, decrement, and new object expressions can be used as a statement</para></summary>
        public static ICSharpCompilerReferenceError CS0201
        {
            get
            {
                if (_CS0201 == null)
                    _CS0201 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0201, CSharpErrorIdentifiers.CS0201);
                return _CS0201;
            }
        }
        private static ICSharpCompilerReferenceError _CS0201;

        /// <summary><para>C&#9839; compiler error &#35;202:</para><para>foreach requires that the return type {0} of '{1}.GetEnumerator()' must have a suitable public MoveNext method and public Current property</para></summary>
        public static ICSharpCompilerReferenceError CS0202
        {
            get
            {
                if (_CS0202 == null)
                    _CS0202 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0202, CSharpErrorIdentifiers.CS0202);
                return _CS0202;
            }
        }
        private static ICSharpCompilerReferenceError _CS0202;

        /// <summary><para>C&#9839; compiler error &#35;204:</para><para>Only 65534 locals are allowed</para></summary>
        public static ICSharpCompilerReferenceError CS0204
        {
            get
            {
                if (_CS0204 == null)
                    _CS0204 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0204, CSharpErrorIdentifiers.CS0204);
                return _CS0204;
            }
        }
        private static ICSharpCompilerReferenceError _CS0204;

        /// <summary><para>C&#9839; compiler error &#35;205:</para><para>Cannot call an abstract base member: {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0205
        {
            get
            {
                if (_CS0205 == null)
                    _CS0205 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0205, CSharpErrorIdentifiers.CS0205);
                return _CS0205;
            }
        }
        private static ICSharpCompilerReferenceError _CS0205;

        /// <summary><para>C&#9839; compiler error &#35;206:</para><para>A property or indexer may not be passed as an out or ref parameter</para></summary>
        public static ICSharpCompilerReferenceError CS0206
        {
            get
            {
                if (_CS0206 == null)
                    _CS0206 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0206, CSharpErrorIdentifiers.CS0206);
                return _CS0206;
            }
        }
        private static ICSharpCompilerReferenceError _CS0206;

        /// <summary><para>C&#9839; compiler error &#35;208:</para><para>Cannot take the address of, get the size of, or declare a pointer to a managed type ({0})</para></summary>
        public static ICSharpCompilerReferenceError CS0208
        {
            get
            {
                if (_CS0208 == null)
                    _CS0208 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0208, CSharpErrorIdentifiers.CS0208);
                return _CS0208;
            }
        }
        private static ICSharpCompilerReferenceError _CS0208;

        /// <summary><para>C&#9839; compiler error &#35;209:</para><para>The type of local declared in a fixed statement must be a pointer type</para></summary>
        public static ICSharpCompilerReferenceError CS0209
        {
            get
            {
                if (_CS0209 == null)
                    _CS0209 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0209, CSharpErrorIdentifiers.CS0209);
                return _CS0209;
            }
        }
        private static ICSharpCompilerReferenceError _CS0209;

        /// <summary><para>C&#9839; compiler error &#35;210:</para><para>You must provide an initializer in a fixed or using statement declaration</para></summary>
        public static ICSharpCompilerReferenceError CS0210
        {
            get
            {
                if (_CS0210 == null)
                    _CS0210 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0210, CSharpErrorIdentifiers.CS0210);
                return _CS0210;
            }
        }
        private static ICSharpCompilerReferenceError _CS0210;

        /// <summary><para>C&#9839; compiler error &#35;211:</para><para>Cannot take the address of the given expression</para></summary>
        public static ICSharpCompilerReferenceError CS0211
        {
            get
            {
                if (_CS0211 == null)
                    _CS0211 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0211, CSharpErrorIdentifiers.CS0211);
                return _CS0211;
            }
        }
        private static ICSharpCompilerReferenceError _CS0211;

        /// <summary><para>C&#9839; compiler error &#35;212:</para><para>You can only take the address of an unfixed expression inside of a fixed statement initializer</para></summary>
        public static ICSharpCompilerReferenceError CS0212
        {
            get
            {
                if (_CS0212 == null)
                    _CS0212 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0212, CSharpErrorIdentifiers.CS0212);
                return _CS0212;
            }
        }
        private static ICSharpCompilerReferenceError _CS0212;

        /// <summary><para>C&#9839; compiler error &#35;213:</para><para>You cannot use the fixed statement to take the address of an already fixed expression</para></summary>
        public static ICSharpCompilerReferenceError CS0213
        {
            get
            {
                if (_CS0213 == null)
                    _CS0213 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0213, CSharpErrorIdentifiers.CS0213);
                return _CS0213;
            }
        }
        private static ICSharpCompilerReferenceError _CS0213;

        /// <summary><para>C&#9839; compiler error &#35;214:</para><para>Pointers and fixed size buffers may only be used in an unsafe context</para></summary>
        public static ICSharpCompilerReferenceError CS0214
        {
            get
            {
                if (_CS0214 == null)
                    _CS0214 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0214, CSharpErrorIdentifiers.CS0214);
                return _CS0214;
            }
        }
        private static ICSharpCompilerReferenceError _CS0214;

        /// <summary><para>C&#9839; compiler error &#35;215:</para><para>The return type of operator True or False must be bool</para></summary>
        public static ICSharpCompilerReferenceError CS0215
        {
            get
            {
                if (_CS0215 == null)
                    _CS0215 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0215, CSharpErrorIdentifiers.CS0215);
                return _CS0215;
            }
        }
        private static ICSharpCompilerReferenceError _CS0215;

        /// <summary><para>C&#9839; compiler error &#35;216:</para><para>The operator {0} requires a matching operator {1} to also be defined</para></summary>
        public static ICSharpCompilerReferenceError CS0216
        {
            get
            {
                if (_CS0216 == null)
                    _CS0216 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0216, CSharpErrorIdentifiers.CS0216);
                return _CS0216;
            }
        }
        private static ICSharpCompilerReferenceError _CS0216;

        /// <summary><para>C&#9839; compiler error &#35;217:</para><para>In order to be applicable as a short circuit operator a user-defined logical operator ({0}) must have the same return type as the type of its 2 parameters.</para></summary>
        public static ICSharpCompilerReferenceError CS0217
        {
            get
            {
                if (_CS0217 == null)
                    _CS0217 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0217, CSharpErrorIdentifiers.CS0217);
                return _CS0217;
            }
        }
        private static ICSharpCompilerReferenceError _CS0217;

        /// <summary><para>C&#9839; compiler error &#35;218:</para><para>The type ({0}) must contain declarations of operator true and operator false</para></summary>
        public static ICSharpCompilerReferenceError CS0218
        {
            get
            {
                if (_CS0218 == null)
                    _CS0218 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0218, CSharpErrorIdentifiers.CS0218);
                return _CS0218;
            }
        }
        private static ICSharpCompilerReferenceError _CS0218;

        /// <summary><para>C&#9839; compiler error &#35;220:</para><para>The operation overflows at compile time in checked mode</para></summary>
        public static ICSharpCompilerReferenceError CS0220
        {
            get
            {
                if (_CS0220 == null)
                    _CS0220 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0220, CSharpErrorIdentifiers.CS0220);
                return _CS0220;
            }
        }
        private static ICSharpCompilerReferenceError _CS0220;

        /// <summary><para>C&#9839; compiler error &#35;221:</para><para>Constant value {0} cannot be converted to a {1} (use 'unchecked' syntax to override)</para></summary>
        public static ICSharpCompilerReferenceError CS0221
        {
            get
            {
                if (_CS0221 == null)
                    _CS0221 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0221, CSharpErrorIdentifiers.CS0221);
                return _CS0221;
            }
        }
        private static ICSharpCompilerReferenceError _CS0221;

        /// <summary><para>C&#9839; compiler error &#35;225:</para><para>The params parameter must be a single dimensional array</para></summary>
        public static ICSharpCompilerReferenceError CS0225
        {
            get
            {
                if (_CS0225 == null)
                    _CS0225 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0225, CSharpErrorIdentifiers.CS0225);
                return _CS0225;
            }
        }
        private static ICSharpCompilerReferenceError _CS0225;

        /// <summary><para>C&#9839; compiler error &#35;226:</para><para>An __arglist expression may only appear inside of a call or new expression.</para></summary>
        public static ICSharpCompilerReferenceError CS0226
        {
            get
            {
                if (_CS0226 == null)
                    _CS0226 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0226, CSharpErrorIdentifiers.CS0226);
                return _CS0226;
            }
        }
        private static ICSharpCompilerReferenceError _CS0226;

        /// <summary><para>C&#9839; compiler error &#35;227:</para><para>Unsafe code may only appear if compiling with /unsafe</para></summary>
        public static ICSharpCompilerReferenceError CS0227
        {
            get
            {
                if (_CS0227 == null)
                    _CS0227 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0227, CSharpErrorIdentifiers.CS0227);
                return _CS0227;
            }
        }
        private static ICSharpCompilerReferenceError _CS0227;

        /// <summary><para>C&#9839; compiler error &#35;228:</para><para>{0} does not contain a definition for {1}, or it is not accessible</para></summary>
        public static ICSharpCompilerReferenceError CS0228
        {
            get
            {
                if (_CS0228 == null)
                    _CS0228 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0228, CSharpErrorIdentifiers.CS0228);
                return _CS0228;
            }
        }
        private static ICSharpCompilerReferenceError _CS0228;

        /// <summary><para>C&#9839; compiler error &#35;229:</para><para>Ambiguity between {0} and {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0229
        {
            get
            {
                if (_CS0229 == null)
                    _CS0229 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0229, CSharpErrorIdentifiers.CS0229);
                return _CS0229;
            }
        }
        private static ICSharpCompilerReferenceError _CS0229;

        /// <summary><para>C&#9839; compiler error &#35;230:</para><para>GenericParameter and identifier are both required in a foreach statement</para></summary>
        public static ICSharpCompilerReferenceError CS0230
        {
            get
            {
                if (_CS0230 == null)
                    _CS0230 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0230, CSharpErrorIdentifiers.CS0230);
                return _CS0230;
            }
        }
        private static ICSharpCompilerReferenceError _CS0230;

        /// <summary><para>C&#9839; compiler error &#35;231:</para><para>A params parameter must be the last parameter in a formal parameter list.</para></summary>
        public static ICSharpCompilerReferenceError CS0231
        {
            get
            {
                if (_CS0231 == null)
                    _CS0231 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0231, CSharpErrorIdentifiers.CS0231);
                return _CS0231;
            }
        }
        private static ICSharpCompilerReferenceError _CS0231;

        /// <summary><para>C&#9839; compiler error &#35;233:</para><para>{0} does not have a predefined size, therefore sizeof can only be used in an unsafe context (consider using System.Runtime.InteropServices.Marshal.SizeOf)</para></summary>
        public static ICSharpCompilerReferenceError CS0233
        {
            get
            {
                if (_CS0233 == null)
                    _CS0233 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0233, CSharpErrorIdentifiers.CS0233);
                return _CS0233;
            }
        }
        private static ICSharpCompilerReferenceError _CS0233;

        /// <summary><para>C&#9839; compiler error &#35;234:</para><para>The type or namespace name {0} does not exist in the namespace {1} (are you missing an assembly reference?)</para></summary>
        public static ICSharpCompilerReferenceError CS0234
        {
            get
            {
                if (_CS0234 == null)
                    _CS0234 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0234, CSharpErrorIdentifiers.CS0234);
                return _CS0234;
            }
        }
        private static ICSharpCompilerReferenceError _CS0234;

        /// <summary><para>C&#9839; compiler error &#35;236:</para><para>A field initializer cannot reference the nonstatic field, method, or property {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0236
        {
            get
            {
                if (_CS0236 == null)
                    _CS0236 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0236, CSharpErrorIdentifiers.CS0236);
                return _CS0236;
            }
        }
        private static ICSharpCompilerReferenceError _CS0236;

        /// <summary><para>C&#9839; compiler error &#35;238:</para><para>{0} cannot be sealed because it is not an override</para></summary>
        public static ICSharpCompilerReferenceError CS0238
        {
            get
            {
                if (_CS0238 == null)
                    _CS0238 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0238, CSharpErrorIdentifiers.CS0238);
                return _CS0238;
            }
        }
        private static ICSharpCompilerReferenceError _CS0238;

        /// <summary><para>C&#9839; compiler error &#35;239:</para><para>{0} : cannot override inherited member {1} because it is sealed</para></summary>
        public static ICSharpCompilerReferenceError CS0239
        {
            get
            {
                if (_CS0239 == null)
                    _CS0239 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0239, CSharpErrorIdentifiers.CS0239);
                return _CS0239;
            }
        }
        private static ICSharpCompilerReferenceError _CS0239;

        /// <summary><para>C&#9839; compiler error &#35;241:</para><para>Default parameter specifiers are not permitted</para></summary>
        public static ICSharpCompilerReferenceError CS0241
        {
            get
            {
                if (_CS0241 == null)
                    _CS0241 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0241, CSharpErrorIdentifiers.CS0241);
                return _CS0241;
            }
        }
        private static ICSharpCompilerReferenceError _CS0241;

        /// <summary><para>C&#9839; compiler error &#35;242:</para><para>The operation in question is undefined on void pointers</para></summary>
        public static ICSharpCompilerReferenceError CS0242
        {
            get
            {
                if (_CS0242 == null)
                    _CS0242 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0242, CSharpErrorIdentifiers.CS0242);
                return _CS0242;
            }
        }
        private static ICSharpCompilerReferenceError _CS0242;

        /// <summary><para>C&#9839; compiler error &#35;243:</para><para>The Conditional attribute is not valid on 'method' because it is an override method</para></summary>
        public static ICSharpCompilerReferenceError CS0243
        {
            get
            {
                if (_CS0243 == null)
                    _CS0243 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0243, CSharpErrorIdentifiers.CS0243);
                return _CS0243;
            }
        }
        private static ICSharpCompilerReferenceError _CS0243;

        /// <summary><para>C&#9839; compiler error &#35;244:</para><para>Neither 'is' nor 'as' is valid on pointer types</para></summary>
        public static ICSharpCompilerReferenceError CS0244
        {
            get
            {
                if (_CS0244 == null)
                    _CS0244 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0244, CSharpErrorIdentifiers.CS0244);
                return _CS0244;
            }
        }
        private static ICSharpCompilerReferenceError _CS0244;

        /// <summary><para>C&#9839; compiler error &#35;245:</para><para>Destructors and object.Finalize cannot be called directly. Consider calling IDisposable.Dispose if available.</para></summary>
        public static ICSharpCompilerReferenceError CS0245
        {
            get
            {
                if (_CS0245 == null)
                    _CS0245 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0245, CSharpErrorIdentifiers.CS0245);
                return _CS0245;
            }
        }
        private static ICSharpCompilerReferenceError _CS0245;

        /// <summary><para>C&#9839; compiler error &#35;246:</para><para>The type or namespace name {0} could not be found (are you missing a using directive or an assembly reference?)</para></summary>
        public static ICSharpCompilerReferenceError CS0246
        {
            get
            {
                if (_CS0246 == null)
                    _CS0246 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0246, CSharpErrorIdentifiers.CS0246);
                return _CS0246;
            }
        }
        private static ICSharpCompilerReferenceError _CS0246;

        /// <summary><para>C&#9839; compiler error &#35;247:</para><para>Cannot use a negative size with stackalloc</para></summary>
        public static ICSharpCompilerReferenceError CS0247
        {
            get
            {
                if (_CS0247 == null)
                    _CS0247 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0247, CSharpErrorIdentifiers.CS0247);
                return _CS0247;
            }
        }
        private static ICSharpCompilerReferenceError _CS0247;

        /// <summary><para>C&#9839; compiler error &#35;248:</para><para>Cannot create an array with a negative size</para></summary>
        public static ICSharpCompilerReferenceError CS0248
        {
            get
            {
                if (_CS0248 == null)
                    _CS0248 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0248, CSharpErrorIdentifiers.CS0248);
                return _CS0248;
            }
        }
        private static ICSharpCompilerReferenceError _CS0248;

        /// <summary><para>C&#9839; compiler error &#35;249:</para><para>Do not override object.Finalize. Instead, provide a destructor.</para></summary>
        public static ICSharpCompilerReferenceError CS0249
        {
            get
            {
                if (_CS0249 == null)
                    _CS0249 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0249, CSharpErrorIdentifiers.CS0249);
                return _CS0249;
            }
        }
        private static ICSharpCompilerReferenceError _CS0249;

        /// <summary><para>C&#9839; compiler error &#35;250:</para><para>Do not directly call your base class Finalize method. It is called automatically from your destructor.</para></summary>
        public static ICSharpCompilerReferenceError CS0250
        {
            get
            {
                if (_CS0250 == null)
                    _CS0250 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0250, CSharpErrorIdentifiers.CS0250);
                return _CS0250;
            }
        }
        private static ICSharpCompilerReferenceError _CS0250;

        /// <summary><para>C&#9839; compiler error &#35;254:</para><para>The right hand side of a fixed statement assignment may not be a cast expression</para></summary>
        public static ICSharpCompilerReferenceError CS0254
        {
            get
            {
                if (_CS0254 == null)
                    _CS0254 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0254, CSharpErrorIdentifiers.CS0254);
                return _CS0254;
            }
        }
        private static ICSharpCompilerReferenceError _CS0254;

        /// <summary><para>C&#9839; compiler error &#35;255:</para><para>stackalloc may not be used in a catch or finally block</para></summary>
        public static ICSharpCompilerReferenceError CS0255
        {
            get
            {
                if (_CS0255 == null)
                    _CS0255 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0255, CSharpErrorIdentifiers.CS0255);
                return _CS0255;
            }
        }
        private static ICSharpCompilerReferenceError _CS0255;

        /// <summary><para>C&#9839; compiler error &#35;260:</para><para>Missing partial modifier on declaration of type {0}; another partial declaration of this type exists</para></summary>
        public static ICSharpCompilerReferenceError CS0260
        {
            get
            {
                if (_CS0260 == null)
                    _CS0260 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0260, CSharpErrorIdentifiers.CS0260);
                return _CS0260;
            }
        }
        private static ICSharpCompilerReferenceError _CS0260;

        /// <summary><para>C&#9839; compiler error &#35;261:</para><para>Partial declarations of {0} must be all classes, all structs, or all interfaces</para></summary>
        public static ICSharpCompilerReferenceError CS0261
        {
            get
            {
                if (_CS0261 == null)
                    _CS0261 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0261, CSharpErrorIdentifiers.CS0261);
                return _CS0261;
            }
        }
        private static ICSharpCompilerReferenceError _CS0261;

        /// <summary><para>C&#9839; compiler error &#35;262:</para><para>Partial declarations of {0} have conflicting accessibility modifiers</para></summary>
        public static ICSharpCompilerReferenceError CS0262
        {
            get
            {
                if (_CS0262 == null)
                    _CS0262 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0262, CSharpErrorIdentifiers.CS0262);
                return _CS0262;
            }
        }
        private static ICSharpCompilerReferenceError _CS0262;

        /// <summary><para>C&#9839; compiler error &#35;263:</para><para>Partial declarations of {0} must not specify different base classes</para></summary>
        public static ICSharpCompilerReferenceError CS0263
        {
            get
            {
                if (_CS0263 == null)
                    _CS0263 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0263, CSharpErrorIdentifiers.CS0263);
                return _CS0263;
            }
        }
        private static ICSharpCompilerReferenceError _CS0263;

        /// <summary><para>C&#9839; compiler error &#35;264:</para><para>Partial declarations of {0} must have the same type parameter names in the same order</para></summary>
        public static ICSharpCompilerReferenceError CS0264
        {
            get
            {
                if (_CS0264 == null)
                    _CS0264 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0264, CSharpErrorIdentifiers.CS0264);
                return _CS0264;
            }
        }
        private static ICSharpCompilerReferenceError _CS0264;

        /// <summary><para>C&#9839; compiler error &#35;265:</para><para>Partial declarations of {0} have inconsistent constraints for type parameter {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0265
        {
            get
            {
                if (_CS0265 == null)
                    _CS0265 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0265, CSharpErrorIdentifiers.CS0265);
                return _CS0265;
            }
        }
        private static ICSharpCompilerReferenceError _CS0265;

        /// <summary><para>C&#9839; compiler error &#35;266:</para><para>Cannot implicitly convert type {0} to {1}. An explicit conversion exists (are you missing a cast?)</para></summary>
        public static ICSharpCompilerReferenceError CS0266
        {
            get
            {
                if (_CS0266 == null)
                    _CS0266 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0266, CSharpErrorIdentifiers.CS0266);
                return _CS0266;
            }
        }
        private static ICSharpCompilerReferenceError _CS0266;

        /// <summary><para>C&#9839; compiler error &#35;267:</para><para>The partial modifier can only appear immediately before 'class', 'struct', or 'interface'</para></summary>
        public static ICSharpCompilerReferenceError CS0267
        {
            get
            {
                if (_CS0267 == null)
                    _CS0267 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0267, CSharpErrorIdentifiers.CS0267);
                return _CS0267;
            }
        }
        private static ICSharpCompilerReferenceError _CS0267;

        /// <summary><para>C&#9839; compiler error &#35;268:</para><para>Imported type {0} is invalid. It contains a circular base class dependency.</para></summary>
        public static ICSharpCompilerReferenceError CS0268
        {
            get
            {
                if (_CS0268 == null)
                    _CS0268 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0268, CSharpErrorIdentifiers.CS0268);
                return _CS0268;
            }
        }
        private static ICSharpCompilerReferenceError _CS0268;

        /// <summary><para>C&#9839; compiler error &#35;269:</para><para>Use of unassigned out parameter {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0269
        {
            get
            {
                if (_CS0269 == null)
                    _CS0269 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0269, CSharpErrorIdentifiers.CS0269);
                return _CS0269;
            }
        }
        private static ICSharpCompilerReferenceError _CS0269;

        /// <summary><para>C&#9839; compiler error &#35;270:</para><para>Array size cannot be specified in a variable declaration (try initializing with a 'new' expression)</para></summary>
        public static ICSharpCompilerReferenceError CS0270
        {
            get
            {
                if (_CS0270 == null)
                    _CS0270 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0270, CSharpErrorIdentifiers.CS0270);
                return _CS0270;
            }
        }
        private static ICSharpCompilerReferenceError _CS0270;

        /// <summary><para>C&#9839; compiler error &#35;271:</para><para>The property or indexer {0} cannot be used in this context because the get accessor is inaccessible</para></summary>
        public static ICSharpCompilerReferenceError CS0271
        {
            get
            {
                if (_CS0271 == null)
                    _CS0271 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0271, CSharpErrorIdentifiers.CS0271);
                return _CS0271;
            }
        }
        private static ICSharpCompilerReferenceError _CS0271;

        /// <summary><para>C&#9839; compiler error &#35;272:</para><para>The property or indexer {0} cannot be used in this context because the set accessor is inaccessible</para></summary>
        public static ICSharpCompilerReferenceError CS0272
        {
            get
            {
                if (_CS0272 == null)
                    _CS0272 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0272, CSharpErrorIdentifiers.CS0272);
                return _CS0272;
            }
        }
        private static ICSharpCompilerReferenceError _CS0272;

        /// <summary><para>C&#9839; compiler error &#35;273:</para><para>The accessibility modifier of the {0} accessor must be more restrictive than the property or indexer {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0273
        {
            get
            {
                if (_CS0273 == null)
                    _CS0273 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0273, CSharpErrorIdentifiers.CS0273);
                return _CS0273;
            }
        }
        private static ICSharpCompilerReferenceError _CS0273;

        /// <summary><para>C&#9839; compiler error &#35;274:</para><para>Cannot specify accessibility modifiers for both accessors of the property or indexer {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0274
        {
            get
            {
                if (_CS0274 == null)
                    _CS0274 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0274, CSharpErrorIdentifiers.CS0274);
                return _CS0274;
            }
        }
        private static ICSharpCompilerReferenceError _CS0274;

        /// <summary><para>C&#9839; compiler error &#35;275:</para><para>{0}: accessibility modifiers may not be used on accessors in an interface</para></summary>
        public static ICSharpCompilerReferenceError CS0275
        {
            get
            {
                if (_CS0275 == null)
                    _CS0275 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0275, CSharpErrorIdentifiers.CS0275);
                return _CS0275;
            }
        }
        private static ICSharpCompilerReferenceError _CS0275;

        /// <summary><para>C&#9839; compiler error &#35;276:</para><para>{0}: accessibility modifiers on accessors may only be used if the property or indexer has both a get and a set accessor</para></summary>
        public static ICSharpCompilerReferenceError CS0276
        {
            get
            {
                if (_CS0276 == null)
                    _CS0276 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0276, CSharpErrorIdentifiers.CS0276);
                return _CS0276;
            }
        }
        private static ICSharpCompilerReferenceError _CS0276;

        /// <summary><para>C&#9839; compiler error &#35;277:</para><para>{0} does not implement interface member {1}. {2} is not public</para></summary>
        public static ICSharpCompilerReferenceError CS0277
        {
            get
            {
                if (_CS0277 == null)
                    _CS0277 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0277, CSharpErrorIdentifiers.CS0277);
                return _CS0277;
            }
        }
        private static ICSharpCompilerReferenceError _CS0277;

        /// <summary><para>C&#9839; compiler error &#35;281:</para><para>Friend access was granted to {0}, but the output assembly is named {1}. Try adding a reference to {0} or changing the output assembly name to match.</para></summary>
        public static ICSharpCompilerReferenceError CS0281
        {
            get
            {
                if (_CS0281 == null)
                    _CS0281 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0281, CSharpErrorIdentifiers.CS0281);
                return _CS0281;
            }
        }
        private static ICSharpCompilerReferenceError _CS0281;

        /// <summary><para>C&#9839; compiler error &#35;283:</para><para>The type {0} cannot be declared const</para></summary>
        public static ICSharpCompilerReferenceError CS0283
        {
            get
            {
                if (_CS0283 == null)
                    _CS0283 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0283, CSharpErrorIdentifiers.CS0283);
                return _CS0283;
            }
        }
        private static ICSharpCompilerReferenceError _CS0283;

        /// <summary><para>C&#9839; compiler error &#35;304:</para><para>Cannot create an instance of the variable type {0} because it does not have the new() constraint</para></summary>
        public static ICSharpCompilerReferenceError CS0304
        {
            get
            {
                if (_CS0304 == null)
                    _CS0304 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0304, CSharpErrorIdentifiers.CS0304);
                return _CS0304;
            }
        }
        private static ICSharpCompilerReferenceError _CS0304;

        /// <summary><para>C&#9839; compiler error &#35;305:</para><para>Using the generic type {0} requires {1} type arguments</para></summary>
        public static ICSharpCompilerReferenceError CS0305
        {
            get
            {
                if (_CS0305 == null)
                    _CS0305 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0305, CSharpErrorIdentifiers.CS0305);
                return _CS0305;
            }
        }
        private static ICSharpCompilerReferenceError _CS0305;

        /// <summary><para>C&#9839; compiler error &#35;306:</para><para>The type {0} may not be used as a type argument</para></summary>
        public static ICSharpCompilerReferenceError CS0306
        {
            get
            {
                if (_CS0306 == null)
                    _CS0306 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0306, CSharpErrorIdentifiers.CS0306);
                return _CS0306;
            }
        }
        private static ICSharpCompilerReferenceError _CS0306;

        /// <summary><para>C&#9839; compiler error &#35;307:</para><para>The {0} {1} is not a generic method. If you intended an expression list, use parentheses around the &lt; expression.</para></summary>
        public static ICSharpCompilerReferenceError CS0307
        {
            get
            {
                if (_CS0307 == null)
                    _CS0307 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0307, CSharpErrorIdentifiers.CS0307);
                return _CS0307;
            }
        }
        private static ICSharpCompilerReferenceError _CS0307;

        /// <summary><para>C&#9839; compiler error &#35;308:</para><para>The non-generic type-or-method {0} cannot be used with type arguments.</para></summary>
        public static ICSharpCompilerReferenceError CS0308
        {
            get
            {
                if (_CS0308 == null)
                    _CS0308 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0308, CSharpErrorIdentifiers.CS0308);
                return _CS0308;
            }
        }
        private static ICSharpCompilerReferenceError _CS0308;

        /// <summary><para>C&#9839; compiler error &#35;310:</para><para>The type {0} must be a non-abstract type with a public parameterless constructor in order to use it as parameter {1} in the generic type or method {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0310
        {
            get
            {
                if (_CS0310 == null)
                    _CS0310 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0310, CSharpErrorIdentifiers.CS0310);
                return _CS0310;
            }
        }
        private static ICSharpCompilerReferenceError _CS0310;

        /// <summary><para>C&#9839; compiler error &#35;311:</para><para>The type {0} cannot be used as type parameter {2} in the generic type or method {3}. There is no implicit reference conversion from {0} to {1}.</para></summary>
        public static ICSharpCompilerReferenceError CS0311
        {
            get
            {
                if (_CS0311 == null)
                    _CS0311 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0311, CSharpErrorIdentifiers.CS0311);
                return _CS0311;
            }
        }
        private static ICSharpCompilerReferenceError _CS0311;

        /// <summary><para>C&#9839; compiler error &#35;312:</para><para>The type {0} cannot be used as type parameter {1} in the generic type or method {2}. The nullable type {0} does not satisfy the constraint of {3}.</para></summary>
        public static ICSharpCompilerReferenceError CS0312
        {
            get
            {
                if (_CS0312 == null)
                    _CS0312 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0312, CSharpErrorIdentifiers.CS0312);
                return _CS0312;
            }
        }
        private static ICSharpCompilerReferenceError _CS0312;

        /// <summary><para>C&#9839; compiler error &#35;313:</para><para>The type {0} cannot be used as type parameter {1} in the generic type or method {2}. The nullable type {0} does not satisfy the constraint of {2}. Nullable types cannot satisfy any interface constraints.</para></summary>
        public static ICSharpCompilerReferenceError CS0313
        {
            get
            {
                if (_CS0313 == null)
                    _CS0313 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0313, CSharpErrorIdentifiers.CS0313);
                return _CS0313;
            }
        }
        private static ICSharpCompilerReferenceError _CS0313;

        /// <summary><para>C&#9839; compiler error &#35;314:</para><para>The type {0} cannot be used as type parameter {1} in the generic type or method {2}. There is no boxing conversion or type parameter conversion from {0} to {3}.</para></summary>
        public static ICSharpCompilerReferenceError CS0314
        {
            get
            {
                if (_CS0314 == null)
                    _CS0314 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0314, CSharpErrorIdentifiers.CS0314);
                return _CS0314;
            }
        }
        private static ICSharpCompilerReferenceError _CS0314;

        /// <summary><para>C&#9839; compiler error &#35;315:</para><para>The type {0} cannot be used as type parameter {1} in the generic type or method {2}. There is no boxing conversion from {0} to {3}.</para></summary>
        public static ICSharpCompilerReferenceError CS0315
        {
            get
            {
                if (_CS0315 == null)
                    _CS0315 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0315, CSharpErrorIdentifiers.CS0315);
                return _CS0315;
            }
        }
        private static ICSharpCompilerReferenceError _CS0315;

        /// <summary><para>C&#9839; compiler error &#35;316:</para><para>The parameter name {0} conflicts with an automatically-generated parameter name.</para></summary>
        public static ICSharpCompilerReferenceError CS0316
        {
            get
            {
                if (_CS0316 == null)
                    _CS0316 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0316, CSharpErrorIdentifiers.CS0316);
                return _CS0316;
            }
        }
        private static ICSharpCompilerReferenceError _CS0316;

        /// <summary><para>C&#9839; compiler error &#35;400:</para><para>The type or namespace name {0} could not be found in the global namespace (are you missing an assembly reference?)</para></summary>
        public static ICSharpCompilerReferenceError CS0400
        {
            get
            {
                if (_CS0400 == null)
                    _CS0400 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0400, CSharpErrorIdentifiers.CS0400);
                return _CS0400;
            }
        }
        private static ICSharpCompilerReferenceError _CS0400;

        /// <summary><para>C&#9839; compiler error &#35;401:</para><para>The new() constraint must be the last constraint specified</para></summary>
        public static ICSharpCompilerReferenceError CS0401
        {
            get
            {
                if (_CS0401 == null)
                    _CS0401 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0401, CSharpErrorIdentifiers.CS0401);
                return _CS0401;
            }
        }
        private static ICSharpCompilerReferenceError _CS0401;

        /// <summary><para>C&#9839; compiler error &#35;403:</para><para>Cannot convert null to type parameter {0} because it could be a non-nullable value type. Consider using default({0}) instead.</para></summary>
        public static ICSharpCompilerReferenceError CS0403
        {
            get
            {
                if (_CS0403 == null)
                    _CS0403 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0403, CSharpErrorIdentifiers.CS0403);
                return _CS0403;
            }
        }
        private static ICSharpCompilerReferenceError _CS0403;

        /// <summary><para>C&#9839; compiler error &#35;404:</para><para>'&lt;' unexpected : attributes cannot be generic</para></summary>
        public static ICSharpCompilerReferenceError CS0404
        {
            get
            {
                if (_CS0404 == null)
                    _CS0404 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0404, CSharpErrorIdentifiers.CS0404);
                return _CS0404;
            }
        }
        private static ICSharpCompilerReferenceError _CS0404;

        /// <summary><para>C&#9839; compiler error &#35;405:</para><para>Duplicate constraint {0} for type parameter {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0405
        {
            get
            {
                if (_CS0405 == null)
                    _CS0405 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0405, CSharpErrorIdentifiers.CS0405);
                return _CS0405;
            }
        }
        private static ICSharpCompilerReferenceError _CS0405;

        /// <summary><para>C&#9839; compiler error &#35;406:</para><para>The class type constraint {0} must come before any other constraints</para></summary>
        public static ICSharpCompilerReferenceError CS0406
        {
            get
            {
                if (_CS0406 == null)
                    _CS0406 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0406, CSharpErrorIdentifiers.CS0406);
                return _CS0406;
            }
        }
        private static ICSharpCompilerReferenceError _CS0406;

        /// <summary><para>C&#9839; compiler error &#35;407:</para><para>{0} has the wrong return type</para></summary>
        public static ICSharpCompilerReferenceError CS0407
        {
            get
            {
                if (_CS0407 == null)
                    _CS0407 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0407, CSharpErrorIdentifiers.CS0407);
                return _CS0407;
            }
        }
        private static ICSharpCompilerReferenceError _CS0407;

        /// <summary><para>C&#9839; compiler error &#35;409:</para><para>A constraint clause has already been specified for type parameter {0}. All of the constraints for a type parameter must be specified in a single where clause.</para></summary>
        public static ICSharpCompilerReferenceError CS0409
        {
            get
            {
                if (_CS0409 == null)
                    _CS0409 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0409, CSharpErrorIdentifiers.CS0409);
                return _CS0409;
            }
        }
        private static ICSharpCompilerReferenceError _CS0409;

        /// <summary><para>C&#9839; compiler error &#35;410:</para><para>No overload for {0} has the correct parameter and return types</para></summary>
        public static ICSharpCompilerReferenceError CS0410
        {
            get
            {
                if (_CS0410 == null)
                    _CS0410 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0410, CSharpErrorIdentifiers.CS0410);
                return _CS0410;
            }
        }
        private static ICSharpCompilerReferenceError _CS0410;

        /// <summary><para>C&#9839; compiler error &#35;411:</para><para>The type arguments for method {0} cannot be inferred from the usage. Try specifying the type arguments explicitly.</para></summary>
        public static ICSharpCompilerReferenceError CS0411
        {
            get
            {
                if (_CS0411 == null)
                    _CS0411 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0411, CSharpErrorIdentifiers.CS0411);
                return _CS0411;
            }
        }
        private static ICSharpCompilerReferenceError _CS0411;

        /// <summary><para>C&#9839; compiler error &#35;412:</para><para>{0}: a parameter or local variable cannot have the same name as a method type parameter</para></summary>
        public static ICSharpCompilerReferenceError CS0412
        {
            get
            {
                if (_CS0412 == null)
                    _CS0412 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0412, CSharpErrorIdentifiers.CS0412);
                return _CS0412;
            }
        }
        private static ICSharpCompilerReferenceError _CS0412;

        /// <summary><para>C&#9839; compiler error &#35;413:</para><para>The type parameter {0} cannot be used with the 'as' operator because it does not have a class type constraint nor a constraint that is a class</para></summary>
        public static ICSharpCompilerReferenceError CS0413
        {
            get
            {
                if (_CS0413 == null)
                    _CS0413 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0413, CSharpErrorIdentifiers.CS0413);
                return _CS0413;
            }
        }
        private static ICSharpCompilerReferenceError _CS0413;

        /// <summary><para>C&#9839; compiler error &#35;415:</para><para>The {0} attribute is valid only on an indexer that is not an explicit interface member declaration</para></summary>
        public static ICSharpCompilerReferenceError CS0415
        {
            get
            {
                if (_CS0415 == null)
                    _CS0415 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0415, CSharpErrorIdentifiers.CS0415);
                return _CS0415;
            }
        }
        private static ICSharpCompilerReferenceError _CS0415;

        /// <summary><para>C&#9839; compiler error &#35;416:</para><para>{0}: an attribute argument cannot use type parameters</para></summary>
        public static ICSharpCompilerReferenceError CS0416
        {
            get
            {
                if (_CS0416 == null)
                    _CS0416 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0416, CSharpErrorIdentifiers.CS0416);
                return _CS0416;
            }
        }
        private static ICSharpCompilerReferenceError _CS0416;

        /// <summary><para>C&#9839; compiler error &#35;417:</para><para>{0}: cannot provide arguments when creating an instance of a variable type</para></summary>
        public static ICSharpCompilerReferenceError CS0417
        {
            get
            {
                if (_CS0417 == null)
                    _CS0417 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0417, CSharpErrorIdentifiers.CS0417);
                return _CS0417;
            }
        }
        private static ICSharpCompilerReferenceError _CS0417;

        /// <summary><para>C&#9839; compiler error &#35;418:</para><para>{0}: an abstract class cannot be sealed or static</para></summary>
        public static ICSharpCompilerReferenceError CS0418
        {
            get
            {
                if (_CS0418 == null)
                    _CS0418 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0418, CSharpErrorIdentifiers.CS0418);
                return _CS0418;
            }
        }
        private static ICSharpCompilerReferenceError _CS0418;

        /// <summary><para>C&#9839; compiler error &#35;423:</para><para>Since {0} has the ComImport attribute, {1} must be extern or abstract</para></summary>
        public static ICSharpCompilerReferenceError CS0423
        {
            get
            {
                if (_CS0423 == null)
                    _CS0423 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0423, CSharpErrorIdentifiers.CS0423);
                return _CS0423;
            }
        }
        private static ICSharpCompilerReferenceError _CS0423;

        /// <summary><para>C&#9839; compiler error &#35;424:</para><para>{0}: a class with the ComImport attribute cannot specify a base class</para></summary>
        public static ICSharpCompilerReferenceError CS0424
        {
            get
            {
                if (_CS0424 == null)
                    _CS0424 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0424, CSharpErrorIdentifiers.CS0424);
                return _CS0424;
            }
        }
        private static ICSharpCompilerReferenceError _CS0424;

        /// <summary><para>C&#9839; compiler error &#35;425:</para><para>The constraints for type parameter {0} of method {1} must match the constraints for type parameter {2} of interface method {3}. Consider using an explicit interface implementation instead.</para></summary>
        public static ICSharpCompilerReferenceError CS0425
        {
            get
            {
                if (_CS0425 == null)
                    _CS0425 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0425, CSharpErrorIdentifiers.CS0425);
                return _CS0425;
            }
        }
        private static ICSharpCompilerReferenceError _CS0425;

        /// <summary><para>C&#9839; compiler error &#35;426:</para><para>The type name {0} does not exist in the type {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0426
        {
            get
            {
                if (_CS0426 == null)
                    _CS0426 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0426, CSharpErrorIdentifiers.CS0426);
                return _CS0426;
            }
        }
        private static ICSharpCompilerReferenceError _CS0426;

        /// <summary><para>C&#9839; compiler error &#35;428:</para><para>Cannot convert method group {0} to non-delegate type {1}. Did you intend to invoke the method?</para></summary>
        public static ICSharpCompilerReferenceError CS0428
        {
            get
            {
                if (_CS0428 == null)
                    _CS0428 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0428, CSharpErrorIdentifiers.CS0428);
                return _CS0428;
            }
        }
        private static ICSharpCompilerReferenceError _CS0428;

        /// <summary><para>C&#9839; compiler error &#35;430:</para><para>The extern alias {0} was not specified in a /reference option</para></summary>
        public static ICSharpCompilerReferenceError CS0430
        {
            get
            {
                if (_CS0430 == null)
                    _CS0430 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0430, CSharpErrorIdentifiers.CS0430);
                return _CS0430;
            }
        }
        private static ICSharpCompilerReferenceError _CS0430;

        /// <summary><para>C&#9839; compiler error &#35;431:</para><para>Cannot use alias {0} with '::' since the alias references a type. Use '.' instead.</para></summary>
        public static ICSharpCompilerReferenceError CS0431
        {
            get
            {
                if (_CS0431 == null)
                    _CS0431 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0431, CSharpErrorIdentifiers.CS0431);
                return _CS0431;
            }
        }
        private static ICSharpCompilerReferenceError _CS0431;

        /// <summary><para>C&#9839; compiler error &#35;432:</para><para>Alias {0} not found</para></summary>
        public static ICSharpCompilerReferenceError CS0432
        {
            get
            {
                if (_CS0432 == null)
                    _CS0432 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0432, CSharpErrorIdentifiers.CS0432);
                return _CS0432;
            }
        }
        private static ICSharpCompilerReferenceError _CS0432;

        /// <summary><para>C&#9839; compiler error &#35;433:</para><para>The type {0} exists in both {1} and {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0433
        {
            get
            {
                if (_CS0433 == null)
                    _CS0433 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0433, CSharpErrorIdentifiers.CS0433);
                return _CS0433;
            }
        }
        private static ICSharpCompilerReferenceError _CS0433;

        /// <summary><para>C&#9839; compiler error &#35;434:</para><para>The namespace {0} in {1} conflicts with the type {2} in {3}</para></summary>
        public static ICSharpCompilerReferenceError CS0434
        {
            get
            {
                if (_CS0434 == null)
                    _CS0434 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0434, CSharpErrorIdentifiers.CS0434);
                return _CS0434;
            }
        }
        private static ICSharpCompilerReferenceError _CS0434;

        /// <summary><para>C&#9839; compiler error &#35;438:</para><para>The type {0} in {1} conflicts with the namespace {2} in {3}.</para></summary>
        public static ICSharpCompilerReferenceError CS0438
        {
            get
            {
                if (_CS0438 == null)
                    _CS0438 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0438, CSharpErrorIdentifiers.CS0438);
                return _CS0438;
            }
        }
        private static ICSharpCompilerReferenceError _CS0438;

        /// <summary><para>C&#9839; compiler error &#35;439:</para><para>An extern alias declaration must precede all other elements defined in the namespace</para></summary>
        public static ICSharpCompilerReferenceError CS0439
        {
            get
            {
                if (_CS0439 == null)
                    _CS0439 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0439, CSharpErrorIdentifiers.CS0439);
                return _CS0439;
            }
        }
        private static ICSharpCompilerReferenceError _CS0439;

        /// <summary><para>C&#9839; compiler error &#35;441:</para><para>{0}: a class cannot be both static and sealed</para></summary>
        public static ICSharpCompilerReferenceError CS0441
        {
            get
            {
                if (_CS0441 == null)
                    _CS0441 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0441, CSharpErrorIdentifiers.CS0441);
                return _CS0441;
            }
        }
        private static ICSharpCompilerReferenceError _CS0441;

        /// <summary><para>C&#9839; compiler error &#35;442:</para><para>{0}: abstract properties cannot have private accessors</para></summary>
        public static ICSharpCompilerReferenceError CS0442
        {
            get
            {
                if (_CS0442 == null)
                    _CS0442 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0442, CSharpErrorIdentifiers.CS0442);
                return _CS0442;
            }
        }
        private static ICSharpCompilerReferenceError _CS0442;

        /// <summary><para>C&#9839; compiler error &#35;443:</para><para>Syntax error, value expected</para></summary>
        public static ICSharpCompilerReferenceError CS0443
        {
            get
            {
                if (_CS0443 == null)
                    _CS0443 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0443, CSharpErrorIdentifiers.CS0443);
                return _CS0443;
            }
        }
        private static ICSharpCompilerReferenceError _CS0443;

        /// <summary><para>C&#9839; compiler error &#35;445:</para><para>Cannot modify the result of an unboxing conversion</para></summary>
        public static ICSharpCompilerReferenceError CS0445
        {
            get
            {
                if (_CS0445 == null)
                    _CS0445 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0445, CSharpErrorIdentifiers.CS0445);
                return _CS0445;
            }
        }
        private static ICSharpCompilerReferenceError _CS0445;

        /// <summary><para>C&#9839; compiler error &#35;446:</para><para>Foreach cannot operate on a {0}. Did you intend to invoke the {0}?</para></summary>
        public static ICSharpCompilerReferenceError CS0446
        {
            get
            {
                if (_CS0446 == null)
                    _CS0446 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0446, CSharpErrorIdentifiers.CS0446);
                return _CS0446;
            }
        }
        private static ICSharpCompilerReferenceError _CS0446;

        /// <summary><para>C&#9839; compiler error &#35;447:</para><para>Attributes cannot be used on type arguments, only on type parameters</para></summary>
        public static ICSharpCompilerReferenceError CS0447
        {
            get
            {
                if (_CS0447 == null)
                    _CS0447 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0447, CSharpErrorIdentifiers.CS0447);
                return _CS0447;
            }
        }
        private static ICSharpCompilerReferenceError _CS0447;

        /// <summary><para>C&#9839; compiler error &#35;448:</para><para>The return type for ++ or -- operator must be the containing type or derived from the containing type</para></summary>
        public static ICSharpCompilerReferenceError CS0448
        {
            get
            {
                if (_CS0448 == null)
                    _CS0448 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0448, CSharpErrorIdentifiers.CS0448);
                return _CS0448;
            }
        }
        private static ICSharpCompilerReferenceError _CS0448;

        /// <summary><para>C&#9839; compiler error &#35;449:</para><para>The 'class' or 'struct' constraint must come before any other constraints</para></summary>
        public static ICSharpCompilerReferenceError CS0449
        {
            get
            {
                if (_CS0449 == null)
                    _CS0449 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0449, CSharpErrorIdentifiers.CS0449);
                return _CS0449;
            }
        }
        private static ICSharpCompilerReferenceError _CS0449;

        /// <summary><para>C&#9839; compiler error &#35;450:</para><para>{0}: cannot specify both a constraint class and the 'class' or 'struct' constraint</para></summary>
        public static ICSharpCompilerReferenceError CS0450
        {
            get
            {
                if (_CS0450 == null)
                    _CS0450 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0450, CSharpErrorIdentifiers.CS0450);
                return _CS0450;
            }
        }
        private static ICSharpCompilerReferenceError _CS0450;

        /// <summary><para>C&#9839; compiler error &#35;451:</para><para>The 'new()' constraint cannot be used with the 'struct' constraint</para></summary>
        public static ICSharpCompilerReferenceError CS0451
        {
            get
            {
                if (_CS0451 == null)
                    _CS0451 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0451, CSharpErrorIdentifiers.CS0451);
                return _CS0451;
            }
        }
        private static ICSharpCompilerReferenceError _CS0451;

        /// <summary><para>C&#9839; compiler error &#35;452:</para><para>The type {0} must be a reference type in order to use it as parameter {1} in the generic type or method {2}.</para></summary>
        public static ICSharpCompilerReferenceError CS0452
        {
            get
            {
                if (_CS0452 == null)
                    _CS0452 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0452, CSharpErrorIdentifiers.CS0452);
                return _CS0452;
            }
        }
        private static ICSharpCompilerReferenceError _CS0452;

        /// <summary><para>C&#9839; compiler error &#35;453:</para><para>The type {0} must be a non-nullable value type in order to use it as parameter {1} in the generic type or method {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0453
        {
            get
            {
                if (_CS0453 == null)
                    _CS0453 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0453, CSharpErrorIdentifiers.CS0453);
                return _CS0453;
            }
        }
        private static ICSharpCompilerReferenceError _CS0453;

        /// <summary><para>C&#9839; compiler error &#35;454:</para><para>Circular constraint dependency involving {0} and {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0454
        {
            get
            {
                if (_CS0454 == null)
                    _CS0454 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0454, CSharpErrorIdentifiers.CS0454);
                return _CS0454;
            }
        }
        private static ICSharpCompilerReferenceError _CS0454;

        /// <summary><para>C&#9839; compiler error &#35;455:</para><para>GenericParameter parameter {0} inherits conflicting constraints {1} and {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0455
        {
            get
            {
                if (_CS0455 == null)
                    _CS0455 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0455, CSharpErrorIdentifiers.CS0455);
                return _CS0455;
            }
        }
        private static ICSharpCompilerReferenceError _CS0455;

        /// <summary><para>C&#9839; compiler error &#35;456:</para><para>GenericParameter parameter {0} has the 'struct' constraint so {0} cannot be used as a constraint for {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0456
        {
            get
            {
                if (_CS0456 == null)
                    _CS0456 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0456, CSharpErrorIdentifiers.CS0456);
                return _CS0456;
            }
        }
        private static ICSharpCompilerReferenceError _CS0456;

        /// <summary><para>C&#9839; compiler error &#35;457:</para><para>Ambiguous user defined conversions {0} and {1} when converting from {2} to {3}</para></summary>
        public static ICSharpCompilerReferenceError CS0457
        {
            get
            {
                if (_CS0457 == null)
                    _CS0457 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0457, CSharpErrorIdentifiers.CS0457);
                return _CS0457;
            }
        }
        private static ICSharpCompilerReferenceError _CS0457;

        /// <summary><para>C&#9839; compiler error &#35;459:</para><para>Cannot take the address of a read-only local variable</para></summary>
        public static ICSharpCompilerReferenceError CS0459
        {
            get
            {
                if (_CS0459 == null)
                    _CS0459 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0459, CSharpErrorIdentifiers.CS0459);
                return _CS0459;
            }
        }
        private static ICSharpCompilerReferenceError _CS0459;

        /// <summary><para>C&#9839; compiler error &#35;460:</para><para>Constraints for override and explicit interface implementation methods are inherited from the base method, so they cannot be specified directly</para></summary>
        public static ICSharpCompilerReferenceError CS0460
        {
            get
            {
                if (_CS0460 == null)
                    _CS0460 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0460, CSharpErrorIdentifiers.CS0460);
                return _CS0460;
            }
        }
        private static ICSharpCompilerReferenceError _CS0460;

        /// <summary><para>C&#9839; compiler error &#35;462:</para><para>The inherited members {0} and {1} have the same signature in type {1}, so they cannot be overridden</para></summary>
        public static ICSharpCompilerReferenceError CS0462
        {
            get
            {
                if (_CS0462 == null)
                    _CS0462 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0462, CSharpErrorIdentifiers.CS0462);
                return _CS0462;
            }
        }
        private static ICSharpCompilerReferenceError _CS0462;

        /// <summary><para>C&#9839; compiler error &#35;463:</para><para>Evaluation of the decimal constant expression failed with error: {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0463
        {
            get
            {
                if (_CS0463 == null)
                    _CS0463 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0463, CSharpErrorIdentifiers.CS0463);
                return _CS0463;
            }
        }
        private static ICSharpCompilerReferenceError _CS0463;

        /// <summary><para>C&#9839; compiler error &#35;466:</para><para>{0} should not have a params parameter since {1} does not</para></summary>
        public static ICSharpCompilerReferenceError CS0466
        {
            get
            {
                if (_CS0466 == null)
                    _CS0466 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0466, CSharpErrorIdentifiers.CS0466);
                return _CS0466;
            }
        }
        private static ICSharpCompilerReferenceError _CS0466;

        /// <summary><para>C&#9839; compiler error &#35;468:</para><para>Ambiguity between type {0} and type {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0468
        {
            get
            {
                if (_CS0468 == null)
                    _CS0468 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0468, CSharpErrorIdentifiers.CS0468);
                return _CS0468;
            }
        }
        private static ICSharpCompilerReferenceError _CS0468;

        /// <summary><para>C&#9839; compiler error &#35;470:</para><para>Method {0} cannot implement interface accessor {1} for type {2}. Use an explicit interface implementation.</para></summary>
        public static ICSharpCompilerReferenceError CS0470
        {
            get
            {
                if (_CS0470 == null)
                    _CS0470 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0470, CSharpErrorIdentifiers.CS0470);
                return _CS0470;
            }
        }
        private static ICSharpCompilerReferenceError _CS0470;

        /// <summary><para>C&#9839; compiler error &#35;471:</para><para>The method [0} is not a generic method. If you intended an expression list, use parentheses around the &lt; expression.</para></summary>
        public static ICSharpCompilerReferenceError CS0471
        {
            get
            {
                if (_CS0471 == null)
                    _CS0471 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0471, CSharpErrorIdentifiers.CS0471);
                return _CS0471;
            }
        }
        private static ICSharpCompilerReferenceError _CS0471;

        /// <summary><para>C&#9839; compiler error &#35;473:</para><para>Explicit interface implementation {0} matches more than one interface member. Which interface member is actually chosen is implementation-dependent. Consider using a non-explicit implementation instead.</para></summary>
        public static ICSharpCompilerReferenceError CS0473
        {
            get
            {
                if (_CS0473 == null)
                    _CS0473 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0473, CSharpErrorIdentifiers.CS0473);
                return _CS0473;
            }
        }
        private static ICSharpCompilerReferenceError _CS0473;

        /// <summary><para>C&#9839; compiler error &#35;500:</para><para>{0} cannot declare a body because it is marked abstract</para></summary>
        public static ICSharpCompilerReferenceError CS0500
        {
            get
            {
                if (_CS0500 == null)
                    _CS0500 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0500, CSharpErrorIdentifiers.CS0500);
                return _CS0500;
            }
        }
        private static ICSharpCompilerReferenceError _CS0500;

        /// <summary><para>C&#9839; compiler error &#35;501:</para><para>{0} must declare a body because it is not marked abstract, extern, or partial</para></summary>
        public static ICSharpCompilerReferenceError CS0501
        {
            get
            {
                if (_CS0501 == null)
                    _CS0501 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0501, CSharpErrorIdentifiers.CS0501);
                return _CS0501;
            }
        }
        private static ICSharpCompilerReferenceError _CS0501;

        /// <summary><para>C&#9839; compiler error &#35;502:</para><para>{0} cannot be both abstract and sealed</para></summary>
        public static ICSharpCompilerReferenceError CS0502
        {
            get
            {
                if (_CS0502 == null)
                    _CS0502 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0502, CSharpErrorIdentifiers.CS0502);
                return _CS0502;
            }
        }
        private static ICSharpCompilerReferenceError _CS0502;

        /// <summary><para>C&#9839; compiler error &#35;503:</para><para>The abstract method {0} cannot be marked virtual</para></summary>
        public static ICSharpCompilerReferenceError CS0503
        {
            get
            {
                if (_CS0503 == null)
                    _CS0503 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0503, CSharpErrorIdentifiers.CS0503);
                return _CS0503;
            }
        }
        private static ICSharpCompilerReferenceError _CS0503;

        /// <summary><para>C&#9839; compiler error &#35;504:</para><para>The constant {0} cannot be marked static</para></summary>
        public static ICSharpCompilerReferenceError CS0504
        {
            get
            {
                if (_CS0504 == null)
                    _CS0504 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0504, CSharpErrorIdentifiers.CS0504);
                return _CS0504;
            }
        }
        private static ICSharpCompilerReferenceError _CS0504;

        /// <summary><para>C&#9839; compiler error &#35;505:</para><para>{0}: cannot override because {1} is not a function</para></summary>
        public static ICSharpCompilerReferenceError CS0505
        {
            get
            {
                if (_CS0505 == null)
                    _CS0505 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0505, CSharpErrorIdentifiers.CS0505);
                return _CS0505;
            }
        }
        private static ICSharpCompilerReferenceError _CS0505;

        /// <summary><para>C&#9839; compiler error &#35;506:</para><para>{0} : cannot override inherited member {1} because it is not marked 'virtual', 'abstract', or 'override'</para></summary>
        public static ICSharpCompilerReferenceError CS0506
        {
            get
            {
                if (_CS0506 == null)
                    _CS0506 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0506, CSharpErrorIdentifiers.CS0506);
                return _CS0506;
            }
        }
        private static ICSharpCompilerReferenceError _CS0506;

        /// <summary><para>C&#9839; compiler error &#35;507:</para><para>{0} : cannot change access modifiers when overriding {1} inherited member {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0507
        {
            get
            {
                if (_CS0507 == null)
                    _CS0507 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0507, CSharpErrorIdentifiers.CS0507);
                return _CS0507;
            }
        }
        private static ICSharpCompilerReferenceError _CS0507;

        /// <summary><para>C&#9839; compiler error &#35;508:</para><para>{0}: return type must be {1} to match overridden member {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0508
        {
            get
            {
                if (_CS0508 == null)
                    _CS0508 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0508, CSharpErrorIdentifiers.CS0508);
                return _CS0508;
            }
        }
        private static ICSharpCompilerReferenceError _CS0508;

        /// <summary><para>C&#9839; compiler error &#35;509:</para><para>{0} : cannot derive from sealed type {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0509
        {
            get
            {
                if (_CS0509 == null)
                    _CS0509 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0509, CSharpErrorIdentifiers.CS0509);
                return _CS0509;
            }
        }
        private static ICSharpCompilerReferenceError _CS0509;

        /// <summary><para>C&#9839; compiler error &#35;513:</para><para>{0} is abstract but it is contained in nonabstract class {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0513
        {
            get
            {
                if (_CS0513 == null)
                    _CS0513 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0513, CSharpErrorIdentifiers.CS0513);
                return _CS0513;
            }
        }
        private static ICSharpCompilerReferenceError _CS0513;

        /// <summary><para>C&#9839; compiler error &#35;514:</para><para>{0} : static constructor cannot have an explicit 'this' or 'base' constructor call</para></summary>
        public static ICSharpCompilerReferenceError CS0514
        {
            get
            {
                if (_CS0514 == null)
                    _CS0514 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0514, CSharpErrorIdentifiers.CS0514);
                return _CS0514;
            }
        }
        private static ICSharpCompilerReferenceError _CS0514;

        /// <summary><para>C&#9839; compiler error &#35;515:</para><para>{0} : access modifiers are not allowed on static constructors</para></summary>
        public static ICSharpCompilerReferenceError CS0515
        {
            get
            {
                if (_CS0515 == null)
                    _CS0515 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0515, CSharpErrorIdentifiers.CS0515);
                return _CS0515;
            }
        }
        private static ICSharpCompilerReferenceError _CS0515;

        /// <summary><para>C&#9839; compiler error &#35;516:</para><para>Constructor {0} can not call itself</para></summary>
        public static ICSharpCompilerReferenceError CS0516
        {
            get
            {
                if (_CS0516 == null)
                    _CS0516 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0516, CSharpErrorIdentifiers.CS0516);
                return _CS0516;
            }
        }
        private static ICSharpCompilerReferenceError _CS0516;

        /// <summary><para>C&#9839; compiler error &#35;517:</para><para>{0} has no base class and cannot call a base constructor</para></summary>
        public static ICSharpCompilerReferenceError CS0517
        {
            get
            {
                if (_CS0517 == null)
                    _CS0517 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0517, CSharpErrorIdentifiers.CS0517);
                return _CS0517;
            }
        }
        private static ICSharpCompilerReferenceError _CS0517;

        /// <summary><para>C&#9839; compiler error &#35;518:</para><para>Predefined type {0} is not defined or imported</para></summary>
        public static ICSharpCompilerReferenceError CS0518
        {
            get
            {
                if (_CS0518 == null)
                    _CS0518 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0518, CSharpErrorIdentifiers.CS0518);
                return _CS0518;
            }
        }
        private static ICSharpCompilerReferenceError _CS0518;

        /// <summary><para>C&#9839; compiler error &#35;520:</para><para>Predefined type {0} is declared incorrectly</para></summary>
        public static ICSharpCompilerReferenceError CS0520
        {
            get
            {
                if (_CS0520 == null)
                    _CS0520 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0520, CSharpErrorIdentifiers.CS0520);
                return _CS0520;
            }
        }
        private static ICSharpCompilerReferenceError _CS0520;

        /// <summary><para>C&#9839; compiler error &#35;522:</para><para>{0} : structs cannot call base class constructors</para></summary>
        public static ICSharpCompilerReferenceError CS0522
        {
            get
            {
                if (_CS0522 == null)
                    _CS0522 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0522, CSharpErrorIdentifiers.CS0522);
                return _CS0522;
            }
        }
        private static ICSharpCompilerReferenceError _CS0522;

        /// <summary><para>C&#9839; compiler error &#35;523:</para><para>Struct member {0} of type {1} causes a cycle in the struct layout</para></summary>
        public static ICSharpCompilerReferenceError CS0523
        {
            get
            {
                if (_CS0523 == null)
                    _CS0523 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0523, CSharpErrorIdentifiers.CS0523);
                return _CS0523;
            }
        }
        private static ICSharpCompilerReferenceError _CS0523;

        /// <summary><para>C&#9839; compiler error &#35;524:</para><para>{0} : interfaces cannot declare types</para></summary>
        public static ICSharpCompilerReferenceError CS0524
        {
            get
            {
                if (_CS0524 == null)
                    _CS0524 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0524, CSharpErrorIdentifiers.CS0524);
                return _CS0524;
            }
        }
        private static ICSharpCompilerReferenceError _CS0524;

        /// <summary><para>C&#9839; compiler error &#35;525:</para><para>Interfaces cannot contain fields</para></summary>
        public static ICSharpCompilerReferenceError CS0525
        {
            get
            {
                if (_CS0525 == null)
                    _CS0525 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0525, CSharpErrorIdentifiers.CS0525);
                return _CS0525;
            }
        }
        private static ICSharpCompilerReferenceError _CS0525;

        /// <summary><para>C&#9839; compiler error &#35;526:</para><para>Interfaces cannot contain constructors</para></summary>
        public static ICSharpCompilerReferenceError CS0526
        {
            get
            {
                if (_CS0526 == null)
                    _CS0526 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0526, CSharpErrorIdentifiers.CS0526);
                return _CS0526;
            }
        }
        private static ICSharpCompilerReferenceError _CS0526;

        /// <summary><para>C&#9839; compiler error &#35;527:</para><para>GenericParameter {0} in interface list is not an interface</para></summary>
        public static ICSharpCompilerReferenceError CS0527
        {
            get
            {
                if (_CS0527 == null)
                    _CS0527 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0527, CSharpErrorIdentifiers.CS0527);
                return _CS0527;
            }
        }
        private static ICSharpCompilerReferenceError _CS0527;

        /// <summary><para>C&#9839; compiler error &#35;528:</para><para>{0} is already listed in interface list</para></summary>
        public static ICSharpCompilerReferenceError CS0528
        {
            get
            {
                if (_CS0528 == null)
                    _CS0528 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0528, CSharpErrorIdentifiers.CS0528);
                return _CS0528;
            }
        }
        private static ICSharpCompilerReferenceError _CS0528;

        /// <summary><para>C&#9839; compiler error &#35;529:</para><para>Inherited interface {0} causes a cycle in the interface hierarchy of {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0529
        {
            get
            {
                if (_CS0529 == null)
                    _CS0529 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0529, CSharpErrorIdentifiers.CS0529);
                return _CS0529;
            }
        }
        private static ICSharpCompilerReferenceError _CS0529;

        /// <summary><para>C&#9839; compiler error &#35;531:</para><para>{0} : interface members cannot have a definition</para></summary>
        public static ICSharpCompilerReferenceError CS0531
        {
            get
            {
                if (_CS0531 == null)
                    _CS0531 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0531, CSharpErrorIdentifiers.CS0531);
                return _CS0531;
            }
        }
        private static ICSharpCompilerReferenceError _CS0531;

        /// <summary><para>C&#9839; compiler error &#35;533:</para><para>{0} hides inherited abstract member {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0533
        {
            get
            {
                if (_CS0533 == null)
                    _CS0533 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0533, CSharpErrorIdentifiers.CS0533);
                return _CS0533;
            }
        }
        private static ICSharpCompilerReferenceError _CS0533;

        /// <summary><para>C&#9839; compiler error &#35;534:</para><para>{0} does not implement inherited abstract member {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0534
        {
            get
            {
                if (_CS0534 == null)
                    _CS0534 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0534, CSharpErrorIdentifiers.CS0534);
                return _CS0534;
            }
        }
        private static ICSharpCompilerReferenceError _CS0534;

        /// <summary><para>C&#9839; compiler error &#35;535:</para><para>{0} does not implement interface member {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0535
        {
            get
            {
                if (_CS0535 == null)
                    _CS0535 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0535, CSharpErrorIdentifiers.CS0535);
                return _CS0535;
            }
        }
        private static ICSharpCompilerReferenceError _CS0535;

        /// <summary><para>C&#9839; compiler error &#35;537:</para><para>The class System.Object cannot have a base class or implement an interface</para></summary>
        public static ICSharpCompilerReferenceError CS0537
        {
            get
            {
                if (_CS0537 == null)
                    _CS0537 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0537, CSharpErrorIdentifiers.CS0537);
                return _CS0537;
            }
        }
        private static ICSharpCompilerReferenceError _CS0537;

        /// <summary><para>C&#9839; compiler error &#35;538:</para><para>{0} in explicit interface declaration is not an interface</para></summary>
        public static ICSharpCompilerReferenceError CS0538
        {
            get
            {
                if (_CS0538 == null)
                    _CS0538 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0538, CSharpErrorIdentifiers.CS0538);
                return _CS0538;
            }
        }
        private static ICSharpCompilerReferenceError _CS0538;

        /// <summary><para>C&#9839; compiler error &#35;539:</para><para>{0} in explicit interface declaration is not a member of interface</para></summary>
        public static ICSharpCompilerReferenceError CS0539
        {
            get
            {
                if (_CS0539 == null)
                    _CS0539 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0539, CSharpErrorIdentifiers.CS0539);
                return _CS0539;
            }
        }
        private static ICSharpCompilerReferenceError _CS0539;

        /// <summary><para>C&#9839; compiler error &#35;540:</para><para>{0} : containing type does not implement interface {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0540
        {
            get
            {
                if (_CS0540 == null)
                    _CS0540 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0540, CSharpErrorIdentifiers.CS0540);
                return _CS0540;
            }
        }
        private static ICSharpCompilerReferenceError _CS0540;

        /// <summary><para>C&#9839; compiler error &#35;541:</para><para>{0} : explicit interface declaration can only be declared in a class or struct</para></summary>
        public static ICSharpCompilerReferenceError CS0541
        {
            get
            {
                if (_CS0541 == null)
                    _CS0541 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0541, CSharpErrorIdentifiers.CS0541);
                return _CS0541;
            }
        }
        private static ICSharpCompilerReferenceError _CS0541;

        /// <summary><para>C&#9839; compiler error &#35;542:</para><para>{0} : member names cannot be the same as their enclosing type</para></summary>
        public static ICSharpCompilerReferenceError CS0542
        {
            get
            {
                if (_CS0542 == null)
                    _CS0542 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0542, CSharpErrorIdentifiers.CS0542);
                return _CS0542;
            }
        }
        private static ICSharpCompilerReferenceError _CS0542;

        /// <summary><para>C&#9839; compiler error &#35;543:</para><para>{0} : the enumerator value is too large to fit in its type</para></summary>
        public static ICSharpCompilerReferenceError CS0543
        {
            get
            {
                if (_CS0543 == null)
                    _CS0543 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0543, CSharpErrorIdentifiers.CS0543);
                return _CS0543;
            }
        }
        private static ICSharpCompilerReferenceError _CS0543;

        /// <summary><para>C&#9839; compiler error &#35;544:</para><para>{0}: cannot override because {1} is not a property</para></summary>
        public static ICSharpCompilerReferenceError CS0544
        {
            get
            {
                if (_CS0544 == null)
                    _CS0544 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0544, CSharpErrorIdentifiers.CS0544);
                return _CS0544;
            }
        }
        private static ICSharpCompilerReferenceError _CS0544;

        /// <summary><para>C&#9839; compiler error &#35;545:</para><para>{0} : cannot override because {1} does not have an overridable get accessor</para></summary>
        public static ICSharpCompilerReferenceError CS0545
        {
            get
            {
                if (_CS0545 == null)
                    _CS0545 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0545, CSharpErrorIdentifiers.CS0545);
                return _CS0545;
            }
        }
        private static ICSharpCompilerReferenceError _CS0545;

        /// <summary><para>C&#9839; compiler error &#35;546:</para><para>{0} : cannot override because {1} does not have an overridable set accessor</para></summary>
        public static ICSharpCompilerReferenceError CS0546
        {
            get
            {
                if (_CS0546 == null)
                    _CS0546 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0546, CSharpErrorIdentifiers.CS0546);
                return _CS0546;
            }
        }
        private static ICSharpCompilerReferenceError _CS0546;

        /// <summary><para>C&#9839; compiler error &#35;547:</para><para>{0} : property or indexer cannot have void type</para></summary>
        public static ICSharpCompilerReferenceError CS0547
        {
            get
            {
                if (_CS0547 == null)
                    _CS0547 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0547, CSharpErrorIdentifiers.CS0547);
                return _CS0547;
            }
        }
        private static ICSharpCompilerReferenceError _CS0547;

        /// <summary><para>C&#9839; compiler error &#35;548:</para><para>{0} : property or indexer must have at least one accessor</para></summary>
        public static ICSharpCompilerReferenceError CS0548
        {
            get
            {
                if (_CS0548 == null)
                    _CS0548 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0548, CSharpErrorIdentifiers.CS0548);
                return _CS0548;
            }
        }
        private static ICSharpCompilerReferenceError _CS0548;

        /// <summary><para>C&#9839; compiler error &#35;549:</para><para>{0} is a new virtual member in sealed class {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0549
        {
            get
            {
                if (_CS0549 == null)
                    _CS0549 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0549, CSharpErrorIdentifiers.CS0549);
                return _CS0549;
            }
        }
        private static ICSharpCompilerReferenceError _CS0549;

        /// <summary><para>C&#9839; compiler error &#35;550:</para><para>{0} adds an accessor not found in interface member {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0550
        {
            get
            {
                if (_CS0550 == null)
                    _CS0550 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0550, CSharpErrorIdentifiers.CS0550);
                return _CS0550;
            }
        }
        private static ICSharpCompilerReferenceError _CS0550;

        /// <summary><para>C&#9839; compiler error &#35;551:</para><para>Explicit interface implementation {0} is missing accessor {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0551
        {
            get
            {
                if (_CS0551 == null)
                    _CS0551 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0551, CSharpErrorIdentifiers.CS0551);
                return _CS0551;
            }
        }
        private static ICSharpCompilerReferenceError _CS0551;

        /// <summary><para>C&#9839; compiler error &#35;552:</para><para>{0} : user defined conversion to/from interface</para></summary>
        public static ICSharpCompilerReferenceError CS0552
        {
            get
            {
                if (_CS0552 == null)
                    _CS0552 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0552, CSharpErrorIdentifiers.CS0552);
                return _CS0552;
            }
        }
        private static ICSharpCompilerReferenceError _CS0552;

        /// <summary><para>C&#9839; compiler error &#35;553:</para><para>{0} : user defined conversion to/from base class</para></summary>
        public static ICSharpCompilerReferenceError CS0553
        {
            get
            {
                if (_CS0553 == null)
                    _CS0553 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0553, CSharpErrorIdentifiers.CS0553);
                return _CS0553;
            }
        }
        private static ICSharpCompilerReferenceError _CS0553;

        /// <summary><para>C&#9839; compiler error &#35;554:</para><para>{0} : user defined conversion to/from derived class</para></summary>
        public static ICSharpCompilerReferenceError CS0554
        {
            get
            {
                if (_CS0554 == null)
                    _CS0554 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0554, CSharpErrorIdentifiers.CS0554);
                return _CS0554;
            }
        }
        private static ICSharpCompilerReferenceError _CS0554;

        /// <summary><para>C&#9839; compiler error &#35;555:</para><para>User-defined operator cannot take an object of the enclosing type and convert to an object of the enclosing type</para></summary>
        public static ICSharpCompilerReferenceError CS0555
        {
            get
            {
                if (_CS0555 == null)
                    _CS0555 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0555, CSharpErrorIdentifiers.CS0555);
                return _CS0555;
            }
        }
        private static ICSharpCompilerReferenceError _CS0555;

        /// <summary><para>C&#9839; compiler error &#35;556:</para><para>User-defined conversion must convert to or from the enclosing type</para></summary>
        public static ICSharpCompilerReferenceError CS0556
        {
            get
            {
                if (_CS0556 == null)
                    _CS0556 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0556, CSharpErrorIdentifiers.CS0556);
                return _CS0556;
            }
        }
        private static ICSharpCompilerReferenceError _CS0556;

        /// <summary><para>C&#9839; compiler error &#35;557:</para><para>Duplicate user-defined conversion in type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0557
        {
            get
            {
                if (_CS0557 == null)
                    _CS0557 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0557, CSharpErrorIdentifiers.CS0557);
                return _CS0557;
            }
        }
        private static ICSharpCompilerReferenceError _CS0557;

        /// <summary><para>C&#9839; compiler error &#35;558:</para><para>User-defined operator {0} must be declared static and public</para></summary>
        public static ICSharpCompilerReferenceError CS0558
        {
            get
            {
                if (_CS0558 == null)
                    _CS0558 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0558, CSharpErrorIdentifiers.CS0558);
                return _CS0558;
            }
        }
        private static ICSharpCompilerReferenceError _CS0558;

        /// <summary><para>C&#9839; compiler error &#35;559:</para><para>The parameter type for ++ or -- operator must be the containing type</para></summary>
        public static ICSharpCompilerReferenceError CS0559
        {
            get
            {
                if (_CS0559 == null)
                    _CS0559 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0559, CSharpErrorIdentifiers.CS0559);
                return _CS0559;
            }
        }
        private static ICSharpCompilerReferenceError _CS0559;

        /// <summary><para>C&#9839; compiler error &#35;562:</para><para>The parameter of a unary operator must be the containing type</para></summary>
        public static ICSharpCompilerReferenceError CS0562
        {
            get
            {
                if (_CS0562 == null)
                    _CS0562 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0562, CSharpErrorIdentifiers.CS0562);
                return _CS0562;
            }
        }
        private static ICSharpCompilerReferenceError _CS0562;

        /// <summary><para>C&#9839; compiler error &#35;563:</para><para>One of the parameters of a binary operator must be the containing type</para></summary>
        public static ICSharpCompilerReferenceError CS0563
        {
            get
            {
                if (_CS0563 == null)
                    _CS0563 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0563, CSharpErrorIdentifiers.CS0563);
                return _CS0563;
            }
        }
        private static ICSharpCompilerReferenceError _CS0563;

        /// <summary><para>C&#9839; compiler error &#35;564:</para><para>The first operand of an overloaded shift operator must have the same type as the containing type, and the type of the second operand must be int</para></summary>
        public static ICSharpCompilerReferenceError CS0564
        {
            get
            {
                if (_CS0564 == null)
                    _CS0564 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0564, CSharpErrorIdentifiers.CS0564);
                return _CS0564;
            }
        }
        private static ICSharpCompilerReferenceError _CS0564;

        /// <summary><para>C&#9839; compiler error &#35;567:</para><para>Interfaces cannot contain operators</para></summary>
        public static ICSharpCompilerReferenceError CS0567
        {
            get
            {
                if (_CS0567 == null)
                    _CS0567 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0567, CSharpErrorIdentifiers.CS0567);
                return _CS0567;
            }
        }
        private static ICSharpCompilerReferenceError _CS0567;

        /// <summary><para>C&#9839; compiler error &#35;568:</para><para>Structs cannot contain explicit parameterless constructors</para></summary>
        public static ICSharpCompilerReferenceError CS0568
        {
            get
            {
                if (_CS0568 == null)
                    _CS0568 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0568, CSharpErrorIdentifiers.CS0568);
                return _CS0568;
            }
        }
        private static ICSharpCompilerReferenceError _CS0568;

        /// <summary><para>C&#9839; compiler error &#35;569:</para><para>{1} : cannot override {0} because it is not supported by the language</para></summary>
        public static ICSharpCompilerReferenceError CS0569
        {
            get
            {
                if (_CS0569 == null)
                    _CS0569 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0569, CSharpErrorIdentifiers.CS0569);
                return _CS0569;
            }
        }
        private static ICSharpCompilerReferenceError _CS0569;

        /// <summary><para>C&#9839; compiler error &#35;570:</para><para>Property, indexer, or event {0} is not supported by the language; try directly calling accessor method {1}!</para></summary>
        public static ICSharpCompilerReferenceError CS0570
        {
            get
            {
                if (_CS0570 == null)
                    _CS0570 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0570, CSharpErrorIdentifiers.CS0570);
                return _CS0570;
            }
        }
        private static ICSharpCompilerReferenceError _CS0570;

        /// <summary><para>C&#9839; compiler error &#35;571:</para><para>{0} : cannot explicitly call operator or accessor</para></summary>
        public static ICSharpCompilerReferenceError CS0571
        {
            get
            {
                if (_CS0571 == null)
                    _CS0571 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0571, CSharpErrorIdentifiers.CS0571);
                return _CS0571;
            }
        }
        private static ICSharpCompilerReferenceError _CS0571;

        /// <summary><para>C&#9839; compiler error &#35;572:</para><para>{0} : cannot reference a type through an expression; try {1} instead</para></summary>
        public static ICSharpCompilerReferenceError CS0572
        {
            get
            {
                if (_CS0572 == null)
                    _CS0572 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0572, CSharpErrorIdentifiers.CS0572);
                return _CS0572;
            }
        }
        private static ICSharpCompilerReferenceError _CS0572;

        /// <summary><para>C&#9839; compiler error &#35;573:</para><para>{0} : cannot have instance field initializers in structs</para></summary>
        public static ICSharpCompilerReferenceError CS0573
        {
            get
            {
                if (_CS0573 == null)
                    _CS0573 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0573, CSharpErrorIdentifiers.CS0573);
                return _CS0573;
            }
        }
        private static ICSharpCompilerReferenceError _CS0573;

        /// <summary><para>C&#9839; compiler error &#35;574:</para><para>Name of destructor must match name of class</para></summary>
        public static ICSharpCompilerReferenceError CS0574
        {
            get
            {
                if (_CS0574 == null)
                    _CS0574 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0574, CSharpErrorIdentifiers.CS0574);
                return _CS0574;
            }
        }
        private static ICSharpCompilerReferenceError _CS0574;

        /// <summary><para>C&#9839; compiler error &#35;575:</para><para>Only class types can contain destructors</para></summary>
        public static ICSharpCompilerReferenceError CS0575
        {
            get
            {
                if (_CS0575 == null)
                    _CS0575 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0575, CSharpErrorIdentifiers.CS0575);
                return _CS0575;
            }
        }
        private static ICSharpCompilerReferenceError _CS0575;

        /// <summary><para>C&#9839; compiler error &#35;576:</para><para>Namespace {0} contains a definition conflicting with alias {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0576
        {
            get
            {
                if (_CS0576 == null)
                    _CS0576 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0576, CSharpErrorIdentifiers.CS0576);
                return _CS0576;
            }
        }
        private static ICSharpCompilerReferenceError _CS0576;

        /// <summary><para>C&#9839; compiler error &#35;577:</para><para>The Conditional attribute is not valid on {0} because it is a constructor, destructor, operator, or explicit interface implementation</para></summary>
        public static ICSharpCompilerReferenceError CS0577
        {
            get
            {
                if (_CS0577 == null)
                    _CS0577 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0577, CSharpErrorIdentifiers.CS0577);
                return _CS0577;
            }
        }
        private static ICSharpCompilerReferenceError _CS0577;

        /// <summary><para>C&#9839; compiler error &#35;578:</para><para>The Conditional attribute is not valid on {0} because its return type is not void</para></summary>
        public static ICSharpCompilerReferenceError CS0578
        {
            get
            {
                if (_CS0578 == null)
                    _CS0578 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0578, CSharpErrorIdentifiers.CS0578);
                return _CS0578;
            }
        }
        private static ICSharpCompilerReferenceError _CS0578;

        /// <summary><para>C&#9839; compiler error &#35;579:</para><para>Duplicate {0} attribute</para></summary>
        public static ICSharpCompilerReferenceError CS0579
        {
            get
            {
                if (_CS0579 == null)
                    _CS0579 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0579, CSharpErrorIdentifiers.CS0579);
                return _CS0579;
            }
        }
        private static ICSharpCompilerReferenceError _CS0579;

        /// <summary><para>C&#9839; compiler error &#35;582:</para><para>The Conditional not valid on interface members</para></summary>
        public static ICSharpCompilerReferenceError CS0582
        {
            get
            {
                if (_CS0582 == null)
                    _CS0582 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0582, CSharpErrorIdentifiers.CS0582);
                return _CS0582;
            }
        }
        private static ICSharpCompilerReferenceError _CS0582;

        /// <summary><para>C&#9839; compiler error &#35;583:</para><para>Internal Compiler SourceError. An internal error has occurred in the compiler. To work around this problem, try simplifying or changing the program near the locations listed below. Locations at the top of the list are closer to the point at which the internal error occurred. Errors such as this can be reported to Microsoft by using the /errorreport option.</para></summary>
        public static ICSharpCompilerReferenceError CS0583
        {
            get
            {
                if (_CS0583 == null)
                    _CS0583 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0583, CSharpErrorIdentifiers.CS0583);
                return _CS0583;
            }
        }
        private static ICSharpCompilerReferenceError _CS0583;

        /// <summary><para>C&#9839; compiler error &#35;584:</para><para>Internal Compiler SourceError: stage {0} symbol {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0584
        {
            get
            {
                if (_CS0584 == null)
                    _CS0584 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0584, CSharpErrorIdentifiers.CS0584);
                return _CS0584;
            }
        }
        private static ICSharpCompilerReferenceError _CS0584;

        /// <summary><para>C&#9839; compiler error &#35;585:</para><para>Internal Compiler SourceError: stage {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0585
        {
            get
            {
                if (_CS0585 == null)
                    _CS0585 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0585, CSharpErrorIdentifiers.CS0585);
                return _CS0585;
            }
        }
        private static ICSharpCompilerReferenceError _CS0585;

        /// <summary><para>C&#9839; compiler error &#35;586:</para><para>Internal Compiler SourceError: stage {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0586
        {
            get
            {
                if (_CS0586 == null)
                    _CS0586 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0586, CSharpErrorIdentifiers.CS0586);
                return _CS0586;
            }
        }
        private static ICSharpCompilerReferenceError _CS0586;

        /// <summary><para>C&#9839; compiler error &#35;587:</para><para>Internal Compiler SourceError: stage {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0587
        {
            get
            {
                if (_CS0587 == null)
                    _CS0587 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0587, CSharpErrorIdentifiers.CS0587);
                return _CS0587;
            }
        }
        private static ICSharpCompilerReferenceError _CS0587;

        /// <summary><para>C&#9839; compiler error &#35;588:</para><para>Internal Compiler SourceError: stage 'LEX'</para></summary>
        public static ICSharpCompilerReferenceError CS0588
        {
            get
            {
                if (_CS0588 == null)
                    _CS0588 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0588, CSharpErrorIdentifiers.CS0588);
                return _CS0588;
            }
        }
        private static ICSharpCompilerReferenceError _CS0588;

        /// <summary><para>C&#9839; compiler error &#35;589:</para><para>Internal Compiler SourceError: stage 'PARSE'</para></summary>
        public static ICSharpCompilerReferenceError CS0589
        {
            get
            {
                if (_CS0589 == null)
                    _CS0589 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0589, CSharpErrorIdentifiers.CS0589);
                return _CS0589;
            }
        }
        private static ICSharpCompilerReferenceError _CS0589;

        /// <summary><para>C&#9839; compiler error &#35;590:</para><para>User-defined operators cannot return void</para></summary>
        public static ICSharpCompilerReferenceError CS0590
        {
            get
            {
                if (_CS0590 == null)
                    _CS0590 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0590, CSharpErrorIdentifiers.CS0590);
                return _CS0590;
            }
        }
        private static ICSharpCompilerReferenceError _CS0590;

        /// <summary><para>C&#9839; compiler error &#35;591:</para><para>Invalid value for argument to {0} attribute</para></summary>
        public static ICSharpCompilerReferenceError CS0591
        {
            get
            {
                if (_CS0591 == null)
                    _CS0591 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0591, CSharpErrorIdentifiers.CS0591);
                return _CS0591;
            }
        }
        private static ICSharpCompilerReferenceError _CS0591;

        /// <summary><para>C&#9839; compiler error &#35;592:</para><para>Attribute {0} is not valid on this declaration type. It is valid on {1} declarations only.</para></summary>
        public static ICSharpCompilerReferenceError CS0592
        {
            get
            {
                if (_CS0592 == null)
                    _CS0592 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0592, CSharpErrorIdentifiers.CS0592);
                return _CS0592;
            }
        }
        private static ICSharpCompilerReferenceError _CS0592;

        /// <summary><para>C&#9839; compiler error &#35;594:</para><para>Floating-point constant is outside the range of type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0594
        {
            get
            {
                if (_CS0594 == null)
                    _CS0594 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0594, CSharpErrorIdentifiers.CS0594);
                return _CS0594;
            }
        }
        private static ICSharpCompilerReferenceError _CS0594;

        /// <summary><para>C&#9839; compiler error &#35;596:</para><para>The Guid attribute must be specified with the ComImport attribute</para></summary>
        public static ICSharpCompilerReferenceError CS0596
        {
            get
            {
                if (_CS0596 == null)
                    _CS0596 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0596, CSharpErrorIdentifiers.CS0596);
                return _CS0596;
            }
        }
        private static ICSharpCompilerReferenceError _CS0596;

        /// <summary><para>C&#9839; compiler error &#35;599:</para><para>Invalid value for named attribute argument {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0599
        {
            get
            {
                if (_CS0599 == null)
                    _CS0599 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0599, CSharpErrorIdentifiers.CS0599);
                return _CS0599;
            }
        }
        private static ICSharpCompilerReferenceError _CS0599;

        /// <summary><para>C&#9839; compiler error &#35;601:</para><para>The DllImport attribute must be specified on a method marked 'static' and 'extern'</para></summary>
        public static ICSharpCompilerReferenceError CS0601
        {
            get
            {
                if (_CS0601 == null)
                    _CS0601 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0601, CSharpErrorIdentifiers.CS0601);
                return _CS0601;
            }
        }
        private static ICSharpCompilerReferenceError _CS0601;

        /// <summary><para>C&#9839; compiler error &#35;609:</para><para>Cannot set the IndexerName attribute on an indexer marked override</para></summary>
        public static ICSharpCompilerReferenceError CS0609
        {
            get
            {
                if (_CS0609 == null)
                    _CS0609 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0609, CSharpErrorIdentifiers.CS0609);
                return _CS0609;
            }
        }
        private static ICSharpCompilerReferenceError _CS0609;

        /// <summary><para>C&#9839; compiler error &#35;610:</para><para>Field or property cannot be of type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0610
        {
            get
            {
                if (_CS0610 == null)
                    _CS0610 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0610, CSharpErrorIdentifiers.CS0610);
                return _CS0610;
            }
        }
        private static ICSharpCompilerReferenceError _CS0610;

        /// <summary><para>C&#9839; compiler error &#35;611:</para><para>Array elements cannot be of type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0611
        {
            get
            {
                if (_CS0611 == null)
                    _CS0611 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0611, CSharpErrorIdentifiers.CS0611);
                return _CS0611;
            }
        }
        private static ICSharpCompilerReferenceError _CS0611;

        /// <summary><para>C&#9839; compiler error &#35;616:</para><para>{0} is not an attribute class</para></summary>
        public static ICSharpCompilerReferenceError CS0616
        {
            get
            {
                if (_CS0616 == null)
                    _CS0616 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0616, CSharpErrorIdentifiers.CS0616);
                return _CS0616;
            }
        }
        private static ICSharpCompilerReferenceError _CS0616;

        /// <summary><para>C&#9839; compiler error &#35;617:</para><para>{0} is not a valid named attribute argument because it is not a valid attribute parameter type</para></summary>
        public static ICSharpCompilerReferenceError CS0617
        {
            get
            {
                if (_CS0617 == null)
                    _CS0617 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0617, CSharpErrorIdentifiers.CS0617);
                return _CS0617;
            }
        }
        private static ICSharpCompilerReferenceError _CS0617;

        /// <summary><para>C&#9839; compiler error &#35;619:</para><para>{0} is obsolete: {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0619
        {
            get
            {
                if (_CS0619 == null)
                    _CS0619 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0619, CSharpErrorIdentifiers.CS0619);
                return _CS0619;
            }
        }
        private static ICSharpCompilerReferenceError _CS0619;

        /// <summary><para>C&#9839; compiler error &#35;620:</para><para>Indexers cannot have void type</para></summary>
        public static ICSharpCompilerReferenceError CS0620
        {
            get
            {
                if (_CS0620 == null)
                    _CS0620 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0620, CSharpErrorIdentifiers.CS0620);
                return _CS0620;
            }
        }
        private static ICSharpCompilerReferenceError _CS0620;

        /// <summary><para>C&#9839; compiler error &#35;621:</para><para>{0} : virtual or abstract members cannot be private</para></summary>
        public static ICSharpCompilerReferenceError CS0621
        {
            get
            {
                if (_CS0621 == null)
                    _CS0621 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0621, CSharpErrorIdentifiers.CS0621);
                return _CS0621;
            }
        }
        private static ICSharpCompilerReferenceError _CS0621;

        /// <summary><para>C&#9839; compiler error &#35;622:</para><para>Can only use array initializer expressions to assign to array types. Try using a new expression instead.</para></summary>
        public static ICSharpCompilerReferenceError CS0622
        {
            get
            {
                if (_CS0622 == null)
                    _CS0622 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0622, CSharpErrorIdentifiers.CS0622);
                return _CS0622;
            }
        }
        private static ICSharpCompilerReferenceError _CS0622;

        /// <summary><para>C&#9839; compiler error &#35;623:</para><para>Array initializers can only be used in a variable or field initializer. Try using a new expression instead.</para></summary>
        public static ICSharpCompilerReferenceError CS0623
        {
            get
            {
                if (_CS0623 == null)
                    _CS0623 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0623, CSharpErrorIdentifiers.CS0623);
                return _CS0623;
            }
        }
        private static ICSharpCompilerReferenceError _CS0623;

        /// <summary><para>C&#9839; compiler error &#35;625:</para><para>{0}: instance field types marked with StructLayout(LayoutKind.Explicit) must have a FieldOffset attribute</para></summary>
        public static ICSharpCompilerReferenceError CS0625
        {
            get
            {
                if (_CS0625 == null)
                    _CS0625 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0625, CSharpErrorIdentifiers.CS0625);
                return _CS0625;
            }
        }
        private static ICSharpCompilerReferenceError _CS0625;

        /// <summary><para>C&#9839; compiler error &#35;629:</para><para>Conditional member {0} cannot implement interface member {1} in type {2}</para></summary>
        public static ICSharpCompilerReferenceError CS0629
        {
            get
            {
                if (_CS0629 == null)
                    _CS0629 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0629, CSharpErrorIdentifiers.CS0629);
                return _CS0629;
            }
        }
        private static ICSharpCompilerReferenceError _CS0629;

        /// <summary><para>C&#9839; compiler error &#35;631:</para><para>ref and out are not valid in this context</para></summary>
        public static ICSharpCompilerReferenceError CS0631
        {
            get
            {
                if (_CS0631 == null)
                    _CS0631 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0631, CSharpErrorIdentifiers.CS0631);
                return _CS0631;
            }
        }
        private static ICSharpCompilerReferenceError _CS0631;

        /// <summary><para>C&#9839; compiler error &#35;633:</para><para>The argument to the {0} attribute must be a valid identifier</para></summary>
        public static ICSharpCompilerReferenceError CS0633
        {
            get
            {
                if (_CS0633 == null)
                    _CS0633 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0633, CSharpErrorIdentifiers.CS0633);
                return _CS0633;
            }
        }
        private static ICSharpCompilerReferenceError _CS0633;

        /// <summary><para>C&#9839; compiler error &#35;635:</para><para>{0} : System.Interop.UnmanagedType.CustomMarshaller requires named arguments ComType and Marshal</para></summary>
        public static ICSharpCompilerReferenceError CS0635
        {
            get
            {
                if (_CS0635 == null)
                    _CS0635 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0635, CSharpErrorIdentifiers.CS0635);
                return _CS0635;
            }
        }
        private static ICSharpCompilerReferenceError _CS0635;

        /// <summary><para>C&#9839; compiler error &#35;636:</para><para>The FieldOffset attribute can only be placed on members of types marked with the StructLayout(LayoutKind.Explicit)</para></summary>
        public static ICSharpCompilerReferenceError CS0636
        {
            get
            {
                if (_CS0636 == null)
                    _CS0636 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0636, CSharpErrorIdentifiers.CS0636);
                return _CS0636;
            }
        }
        private static ICSharpCompilerReferenceError _CS0636;

        /// <summary><para>C&#9839; compiler error &#35;637:</para><para>The FieldOffset attribute is not allowed on static or const fields</para></summary>
        public static ICSharpCompilerReferenceError CS0637
        {
            get
            {
                if (_CS0637 == null)
                    _CS0637 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0637, CSharpErrorIdentifiers.CS0637);
                return _CS0637;
            }
        }
        private static ICSharpCompilerReferenceError _CS0637;

        /// <summary><para>C&#9839; compiler error &#35;641:</para><para>{0} : attribute is only valid on classes derived from System.Attribute</para></summary>
        public static ICSharpCompilerReferenceError CS0641
        {
            get
            {
                if (_CS0641 == null)
                    _CS0641 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0641, CSharpErrorIdentifiers.CS0641);
                return _CS0641;
            }
        }
        private static ICSharpCompilerReferenceError _CS0641;

        /// <summary><para>C&#9839; compiler error &#35;643:</para><para>{0} duplicate named attribute argument</para></summary>
        public static ICSharpCompilerReferenceError CS0643
        {
            get
            {
                if (_CS0643 == null)
                    _CS0643 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0643, CSharpErrorIdentifiers.CS0643);
                return _CS0643;
            }
        }
        private static ICSharpCompilerReferenceError _CS0643;

        /// <summary><para>C&#9839; compiler error &#35;644:</para><para>{0} cannot derive from special class {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0644
        {
            get
            {
                if (_CS0644 == null)
                    _CS0644 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0644, CSharpErrorIdentifiers.CS0644);
                return _CS0644;
            }
        }
        private static ICSharpCompilerReferenceError _CS0644;

        /// <summary><para>C&#9839; compiler error &#35;645:</para><para>Identifier too long</para></summary>
        public static ICSharpCompilerReferenceError CS0645
        {
            get
            {
                if (_CS0645 == null)
                    _CS0645 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0645, CSharpErrorIdentifiers.CS0645);
                return _CS0645;
            }
        }
        private static ICSharpCompilerReferenceError _CS0645;

        /// <summary><para>C&#9839; compiler error &#35;646:</para><para>Cannot specify the DefaultMember attribute on a type containing an indexer</para></summary>
        public static ICSharpCompilerReferenceError CS0646
        {
            get
            {
                if (_CS0646 == null)
                    _CS0646 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0646, CSharpErrorIdentifiers.CS0646);
                return _CS0646;
            }
        }
        private static ICSharpCompilerReferenceError _CS0646;

        /// <summary><para>C&#9839; compiler error &#35;647:</para><para>SourceError emitting {0} attribute -- {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0647
        {
            get
            {
                if (_CS0647 == null)
                    _CS0647 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0647, CSharpErrorIdentifiers.CS0647);
                return _CS0647;
            }
        }
        private static ICSharpCompilerReferenceError _CS0647;

        /// <summary><para>C&#9839; compiler error &#35;648:</para><para>{0} is a type not supported by the language</para></summary>
        public static ICSharpCompilerReferenceError CS0648
        {
            get
            {
                if (_CS0648 == null)
                    _CS0648 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0648, CSharpErrorIdentifiers.CS0648);
                return _CS0648;
            }
        }
        private static ICSharpCompilerReferenceError _CS0648;

        /// <summary><para>C&#9839; compiler error &#35;650:</para><para>Bad array declarator: To declare a managed array the rank specifier precedes the variable's identifier. To declare a fixed size buffer field, use the fixed keyword before the field type.</para></summary>
        public static ICSharpCompilerReferenceError CS0650
        {
            get
            {
                if (_CS0650 == null)
                    _CS0650 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0650, CSharpErrorIdentifiers.CS0650);
                return _CS0650;
            }
        }
        private static ICSharpCompilerReferenceError _CS0650;

        /// <summary><para>C&#9839; compiler error &#35;653:</para><para>Cannot apply attribute class {0} because it is abstract</para></summary>
        public static ICSharpCompilerReferenceError CS0653
        {
            get
            {
                if (_CS0653 == null)
                    _CS0653 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0653, CSharpErrorIdentifiers.CS0653);
                return _CS0653;
            }
        }
        private static ICSharpCompilerReferenceError _CS0653;

        /// <summary><para>C&#9839; compiler error &#35;655:</para><para>{0} is not a valid named attribute argument because it is not a valid attribute parameter type</para></summary>
        public static ICSharpCompilerReferenceError CS0655
        {
            get
            {
                if (_CS0655 == null)
                    _CS0655 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0655, CSharpErrorIdentifiers.CS0655);
                return _CS0655;
            }
        }
        private static ICSharpCompilerReferenceError _CS0655;

        /// <summary><para>C&#9839; compiler error &#35;656:</para><para>Missing compiler required member {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0656
        {
            get
            {
                if (_CS0656 == null)
                    _CS0656 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0656, CSharpErrorIdentifiers.CS0656);
                return _CS0656;
            }
        }
        private static ICSharpCompilerReferenceError _CS0656;

        /// <summary><para>C&#9839; compiler error &#35;662:</para><para>{0} cannot specify only Out attribute on a ref parameter. Use both In and Out attributes, or neither.</para></summary>
        public static ICSharpCompilerReferenceError CS0662
        {
            get
            {
                if (_CS0662 == null)
                    _CS0662 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0662, CSharpErrorIdentifiers.CS0662);
                return _CS0662;
            }
        }
        private static ICSharpCompilerReferenceError _CS0662;

        /// <summary><para>C&#9839; compiler error &#35;663:</para><para>Cannot define overloaded methods that differ only on ref and out.</para></summary>
        public static ICSharpCompilerReferenceError CS0663
        {
            get
            {
                if (_CS0663 == null)
                    _CS0663 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0663, CSharpErrorIdentifiers.CS0663);
                return _CS0663;
            }
        }
        private static ICSharpCompilerReferenceError _CS0663;

        /// <summary><para>C&#9839; compiler error &#35;664:</para><para>Literal of type double cannot be implicitly converted to type {0}; use an {1} suffix to create a literal of this type</para></summary>
        public static ICSharpCompilerReferenceError CS0664
        {
            get
            {
                if (_CS0664 == null)
                    _CS0664 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0664, CSharpErrorIdentifiers.CS0664);
                return _CS0664;
            }
        }
        private static ICSharpCompilerReferenceError _CS0664;

        /// <summary><para>C&#9839; compiler error &#35;666:</para><para>{0} : new protected member declared in struct</para></summary>
        public static ICSharpCompilerReferenceError CS0666
        {
            get
            {
                if (_CS0666 == null)
                    _CS0666 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0666, CSharpErrorIdentifiers.CS0666);
                return _CS0666;
            }
        }
        private static ICSharpCompilerReferenceError _CS0666;

        /// <summary><para>C&#9839; compiler error &#35;667:</para><para>The feature {0} is deprecated. Please use {1} instead.</para></summary>
        public static ICSharpCompilerReferenceError CS0667
        {
            get
            {
                if (_CS0667 == null)
                    _CS0667 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0667, CSharpErrorIdentifiers.CS0667);
                return _CS0667;
            }
        }
        private static ICSharpCompilerReferenceError _CS0667;

        /// <summary><para>C&#9839; compiler error &#35;668:</para><para>Two indexers have different names; the IndexerName attribute must be used with the same name on every indexer within a type</para></summary>
        public static ICSharpCompilerReferenceError CS0668
        {
            get
            {
                if (_CS0668 == null)
                    _CS0668 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0668, CSharpErrorIdentifiers.CS0668);
                return _CS0668;
            }
        }
        private static ICSharpCompilerReferenceError _CS0668;

        /// <summary><para>C&#9839; compiler error &#35;669:</para><para>A class with the ComImport attribute cannot have a user-defined constructor</para></summary>
        public static ICSharpCompilerReferenceError CS0669
        {
            get
            {
                if (_CS0669 == null)
                    _CS0669 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0669, CSharpErrorIdentifiers.CS0669);
                return _CS0669;
            }
        }
        private static ICSharpCompilerReferenceError _CS0669;

        /// <summary><para>C&#9839; compiler error &#35;670:</para><para>Field cannot have void type</para></summary>
        public static ICSharpCompilerReferenceError CS0670
        {
            get
            {
                if (_CS0670 == null)
                    _CS0670 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0670, CSharpErrorIdentifiers.CS0670);
                return _CS0670;
            }
        }
        private static ICSharpCompilerReferenceError _CS0670;

        /// <summary><para>C&#9839; compiler error &#35;673:</para><para>System.Void cannot be used from C# -- use typeof(void) to get the void type object.</para></summary>
        public static ICSharpCompilerReferenceError CS0673
        {
            get
            {
                if (_CS0673 == null)
                    _CS0673 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0673, CSharpErrorIdentifiers.CS0673);
                return _CS0673;
            }
        }
        private static ICSharpCompilerReferenceError _CS0673;

        /// <summary><para>C&#9839; compiler error &#35;674:</para><para>Do not use 'System.ParamArrayMetadatum'. Use the 'params' keyword instead.</para></summary>
        public static ICSharpCompilerReferenceError CS0674
        {
            get
            {
                if (_CS0674 == null)
                    _CS0674 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0674, CSharpErrorIdentifiers.CS0674);
                return _CS0674;
            }
        }
        private static ICSharpCompilerReferenceError _CS0674;

        /// <summary><para>C&#9839; compiler error &#35;677:</para><para>{0}: a volatile field cannot be of the type {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0677
        {
            get
            {
                if (_CS0677 == null)
                    _CS0677 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0677, CSharpErrorIdentifiers.CS0677);
                return _CS0677;
            }
        }
        private static ICSharpCompilerReferenceError _CS0677;

        /// <summary><para>C&#9839; compiler error &#35;678:</para><para>{0}: a field can not be both volatile and readonly</para></summary>
        public static ICSharpCompilerReferenceError CS0678
        {
            get
            {
                if (_CS0678 == null)
                    _CS0678 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0678, CSharpErrorIdentifiers.CS0678);
                return _CS0678;
            }
        }
        private static ICSharpCompilerReferenceError _CS0678;

        /// <summary><para>C&#9839; compiler error &#35;681:</para><para>The modifier 'abstract' is not valid on fields. Try using a property instead</para></summary>
        public static ICSharpCompilerReferenceError CS0681
        {
            get
            {
                if (_CS0681 == null)
                    _CS0681 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0681, CSharpErrorIdentifiers.CS0681);
                return _CS0681;
            }
        }
        private static ICSharpCompilerReferenceError _CS0681;

        /// <summary><para>C&#9839; compiler error &#35;682:</para><para>{0} cannot implement {1} because it is not supported by the language</para></summary>
        public static ICSharpCompilerReferenceError CS0682
        {
            get
            {
                if (_CS0682 == null)
                    _CS0682 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0682, CSharpErrorIdentifiers.CS0682);
                return _CS0682;
            }
        }
        private static ICSharpCompilerReferenceError _CS0682;

        /// <summary><para>C&#9839; compiler error &#35;683:</para><para>{0} explicit method implementation cannot implement {1} because it is an accessor</para></summary>
        public static ICSharpCompilerReferenceError CS0683
        {
            get
            {
                if (_CS0683 == null)
                    _CS0683 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0683, CSharpErrorIdentifiers.CS0683);
                return _CS0683;
            }
        }
        private static ICSharpCompilerReferenceError _CS0683;

        /// <summary><para>C&#9839; compiler error &#35;685:</para><para>Conditional member {0} cannot have an out parameter</para></summary>
        public static ICSharpCompilerReferenceError CS0685
        {
            get
            {
                if (_CS0685 == null)
                    _CS0685 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0685, CSharpErrorIdentifiers.CS0685);
                return _CS0685;
            }
        }
        private static ICSharpCompilerReferenceError _CS0685;

        /// <summary><para>C&#9839; compiler error &#35;686:</para><para>Accessor {0} cannot implement interface member {1} for type {2}. Use an explicit interface implementation.</para></summary>
        public static ICSharpCompilerReferenceError CS0686
        {
            get
            {
                if (_CS0686 == null)
                    _CS0686 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0686, CSharpErrorIdentifiers.CS0686);
                return _CS0686;
            }
        }
        private static ICSharpCompilerReferenceError _CS0686;

        /// <summary><para>C&#9839; compiler error &#35;687:</para><para>The namespace alias qualifier '::' always resolves to a type or namespace so is illegal here. Consider using '.' instead.</para></summary>
        public static ICSharpCompilerReferenceError CS0687
        {
            get
            {
                if (_CS0687 == null)
                    _CS0687 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0687, CSharpErrorIdentifiers.CS0687);
                return _CS0687;
            }
        }
        private static ICSharpCompilerReferenceError _CS0687;

        /// <summary><para>C&#9839; compiler error &#35;689:</para><para>Cannot derive from {0} because it is a type parameter</para></summary>
        public static ICSharpCompilerReferenceError CS0689
        {
            get
            {
                if (_CS0689 == null)
                    _CS0689 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0689, CSharpErrorIdentifiers.CS0689);
                return _CS0689;
            }
        }
        private static ICSharpCompilerReferenceError _CS0689;

        /// <summary><para>C&#9839; compiler error &#35;690:</para><para>Input file {0} contains invalid metadata.</para></summary>
        public static ICSharpCompilerReferenceError CS0690
        {
            get
            {
                if (_CS0690 == null)
                    _CS0690 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0690, CSharpErrorIdentifiers.CS0690);
                return _CS0690;
            }
        }
        private static ICSharpCompilerReferenceError _CS0690;

        /// <summary><para>C&#9839; compiler error &#35;692:</para><para>Duplicate type parameter {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0692
        {
            get
            {
                if (_CS0692 == null)
                    _CS0692 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0692, CSharpErrorIdentifiers.CS0692);
                return _CS0692;
            }
        }
        private static ICSharpCompilerReferenceError _CS0692;

        /// <summary><para>C&#9839; compiler error &#35;694:</para><para>GenericParameter parameter {0} has the same name as the containing type, or method</para></summary>
        public static ICSharpCompilerReferenceError CS0694
        {
            get
            {
                if (_CS0694 == null)
                    _CS0694 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0694, CSharpErrorIdentifiers.CS0694);
                return _CS0694;
            }
        }
        private static ICSharpCompilerReferenceError _CS0694;

        /// <summary><para>C&#9839; compiler error &#35;695:</para><para>{0} cannot implement both {1} and {1} because they may unify for some type parameter substitutions</para></summary>
        public static ICSharpCompilerReferenceError CS0695
        {
            get
            {
                if (_CS0695 == null)
                    _CS0695 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0695, CSharpErrorIdentifiers.CS0695);
                return _CS0695;
            }
        }
        private static ICSharpCompilerReferenceError _CS0695;

        /// <summary><para>C&#9839; compiler error &#35;698:</para><para>A generic type cannot derive from {0} because it is an attribute class</para></summary>
        public static ICSharpCompilerReferenceError CS0698
        {
            get
            {
                if (_CS0698 == null)
                    _CS0698 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0698, CSharpErrorIdentifiers.CS0698);
                return _CS0698;
            }
        }
        private static ICSharpCompilerReferenceError _CS0698;

        /// <summary><para>C&#9839; compiler error &#35;699:</para><para>{0} does not define type parameter {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0699
        {
            get
            {
                if (_CS0699 == null)
                    _CS0699 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0699, CSharpErrorIdentifiers.CS0699);
                return _CS0699;
            }
        }
        private static ICSharpCompilerReferenceError _CS0699;

        /// <summary><para>C&#9839; compiler error &#35;701:</para><para>{0} is not a valid constraint. A type used as a constraint must be an interface, a non-sealed class or a type parameter.</para></summary>
        public static ICSharpCompilerReferenceError CS0701
        {
            get
            {
                if (_CS0701 == null)
                    _CS0701 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0701, CSharpErrorIdentifiers.CS0701);
                return _CS0701;
            }
        }
        private static ICSharpCompilerReferenceError _CS0701;

        /// <summary><para>C&#9839; compiler error &#35;702:</para><para>Constraint cannot be special class {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0702
        {
            get
            {
                if (_CS0702 == null)
                    _CS0702 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0702, CSharpErrorIdentifiers.CS0702);
                return _CS0702;
            }
        }
        private static ICSharpCompilerReferenceError _CS0702;

        /// <summary><para>C&#9839; compiler error &#35;703:</para><para>Inconsistent accessibility: constraint type {0} is less accessible than {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0703
        {
            get
            {
                if (_CS0703 == null)
                    _CS0703 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0703, CSharpErrorIdentifiers.CS0703);
                return _CS0703;
            }
        }
        private static ICSharpCompilerReferenceError _CS0703;

        /// <summary><para>C&#9839; compiler error &#35;704:</para><para>Cannot do member lookup in {0} because it is a type parameter</para></summary>
        public static ICSharpCompilerReferenceError CS0704
        {
            get
            {
                if (_CS0704 == null)
                    _CS0704 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0704, CSharpErrorIdentifiers.CS0704);
                return _CS0704;
            }
        }
        private static ICSharpCompilerReferenceError _CS0704;

        /// <summary><para>C&#9839; compiler error &#35;706:</para><para>Invalid constraint type. A type used as a constraint must be an interface, a non-sealed class or a type parameter.</para></summary>
        public static ICSharpCompilerReferenceError CS0706
        {
            get
            {
                if (_CS0706 == null)
                    _CS0706 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0706, CSharpErrorIdentifiers.CS0706);
                return _CS0706;
            }
        }
        private static ICSharpCompilerReferenceError _CS0706;

        /// <summary><para>C&#9839; compiler error &#35;708:</para><para>{0}: cannot declare instance members in a static class</para></summary>
        public static ICSharpCompilerReferenceError CS0708
        {
            get
            {
                if (_CS0708 == null)
                    _CS0708 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0708, CSharpErrorIdentifiers.CS0708);
                return _CS0708;
            }
        }
        private static ICSharpCompilerReferenceError _CS0708;

        /// <summary><para>C&#9839; compiler error &#35;709:</para><para>{0}: cannot derive from static class {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0709
        {
            get
            {
                if (_CS0709 == null)
                    _CS0709 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0709, CSharpErrorIdentifiers.CS0709);
                return _CS0709;
            }
        }
        private static ICSharpCompilerReferenceError _CS0709;

        /// <summary><para>C&#9839; compiler error &#35;710:</para><para>Static classes cannot have instance constructors</para></summary>
        public static ICSharpCompilerReferenceError CS0710
        {
            get
            {
                if (_CS0710 == null)
                    _CS0710 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0710, CSharpErrorIdentifiers.CS0710);
                return _CS0710;
            }
        }
        private static ICSharpCompilerReferenceError _CS0710;

        /// <summary><para>C&#9839; compiler error &#35;711:</para><para>Static classes cannot contain destructors</para></summary>
        public static ICSharpCompilerReferenceError CS0711
        {
            get
            {
                if (_CS0711 == null)
                    _CS0711 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0711, CSharpErrorIdentifiers.CS0711);
                return _CS0711;
            }
        }
        private static ICSharpCompilerReferenceError _CS0711;

        /// <summary><para>C&#9839; compiler error &#35;712:</para><para>Cannot create an instance of the static class {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0712
        {
            get
            {
                if (_CS0712 == null)
                    _CS0712 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0712, CSharpErrorIdentifiers.CS0712);
                return _CS0712;
            }
        }
        private static ICSharpCompilerReferenceError _CS0712;

        /// <summary><para>C&#9839; compiler error &#35;713:</para><para>Static class {0} cannot derive from type {1}. Static classes must derive from object.</para></summary>
        public static ICSharpCompilerReferenceError CS0713
        {
            get
            {
                if (_CS0713 == null)
                    _CS0713 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0713, CSharpErrorIdentifiers.CS0713);
                return _CS0713;
            }
        }
        private static ICSharpCompilerReferenceError _CS0713;

        /// <summary><para>C&#9839; compiler error &#35;714:</para><para>{0} : static classes cannot implement interfaces</para></summary>
        public static ICSharpCompilerReferenceError CS0714
        {
            get
            {
                if (_CS0714 == null)
                    _CS0714 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0714, CSharpErrorIdentifiers.CS0714);
                return _CS0714;
            }
        }
        private static ICSharpCompilerReferenceError _CS0714;

        /// <summary><para>C&#9839; compiler error &#35;715:</para><para>{0} : static classes cannot contain user defined operators</para></summary>
        public static ICSharpCompilerReferenceError CS0715
        {
            get
            {
                if (_CS0715 == null)
                    _CS0715 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0715, CSharpErrorIdentifiers.CS0715);
                return _CS0715;
            }
        }
        private static ICSharpCompilerReferenceError _CS0715;

        /// <summary><para>C&#9839; compiler error &#35;716:</para><para>Cannot convert to static type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0716
        {
            get
            {
                if (_CS0716 == null)
                    _CS0716 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0716, CSharpErrorIdentifiers.CS0716);
                return _CS0716;
            }
        }
        private static ICSharpCompilerReferenceError _CS0716;

        /// <summary><para>C&#9839; compiler error &#35;717:</para><para>{0}: static classes cannot be used as constraints</para></summary>
        public static ICSharpCompilerReferenceError CS0717
        {
            get
            {
                if (_CS0717 == null)
                    _CS0717 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0717, CSharpErrorIdentifiers.CS0717);
                return _CS0717;
            }
        }
        private static ICSharpCompilerReferenceError _CS0717;

        /// <summary><para>C&#9839; compiler error &#35;718:</para><para>{0}: static types cannot be used as type arguments</para></summary>
        public static ICSharpCompilerReferenceError CS0718
        {
            get
            {
                if (_CS0718 == null)
                    _CS0718 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0718, CSharpErrorIdentifiers.CS0718);
                return _CS0718;
            }
        }
        private static ICSharpCompilerReferenceError _CS0718;

        /// <summary><para>C&#9839; compiler error &#35;719:</para><para>{0}: array elements cannot be of static type</para></summary>
        public static ICSharpCompilerReferenceError CS0719
        {
            get
            {
                if (_CS0719 == null)
                    _CS0719 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0719, CSharpErrorIdentifiers.CS0719);
                return _CS0719;
            }
        }
        private static ICSharpCompilerReferenceError _CS0719;

        /// <summary><para>C&#9839; compiler error &#35;720:</para><para>{0}: cannot declare indexers in a static class</para></summary>
        public static ICSharpCompilerReferenceError CS0720
        {
            get
            {
                if (_CS0720 == null)
                    _CS0720 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0720, CSharpErrorIdentifiers.CS0720);
                return _CS0720;
            }
        }
        private static ICSharpCompilerReferenceError _CS0720;

        /// <summary><para>C&#9839; compiler error &#35;721:</para><para>{0}: static types cannot be used as parameters</para></summary>
        public static ICSharpCompilerReferenceError CS0721
        {
            get
            {
                if (_CS0721 == null)
                    _CS0721 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0721, CSharpErrorIdentifiers.CS0721);
                return _CS0721;
            }
        }
        private static ICSharpCompilerReferenceError _CS0721;

        /// <summary><para>C&#9839; compiler error &#35;722:</para><para>{0}: static types cannot be used as return types</para></summary>
        public static ICSharpCompilerReferenceError CS0722
        {
            get
            {
                if (_CS0722 == null)
                    _CS0722 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0722, CSharpErrorIdentifiers.CS0722);
                return _CS0722;
            }
        }
        private static ICSharpCompilerReferenceError _CS0722;

        /// <summary><para>C&#9839; compiler error &#35;723:</para><para>Cannot declare variable of static type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0723
        {
            get
            {
                if (_CS0723 == null)
                    _CS0723 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0723, CSharpErrorIdentifiers.CS0723);
                return _CS0723;
            }
        }
        private static ICSharpCompilerReferenceError _CS0723;

        /// <summary><para>C&#9839; compiler error &#35;724:</para><para>does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute</para></summary>
        public static ICSharpCompilerReferenceError CS0724
        {
            get
            {
                if (_CS0724 == null)
                    _CS0724 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0724, CSharpErrorIdentifiers.CS0724);
                return _CS0724;
            }
        }
        private static ICSharpCompilerReferenceError _CS0724;

        /// <summary><para>C&#9839; compiler error &#35;729:</para><para>GenericParameter {0} is defined in this assembly, but a type forwarder is specified for it</para></summary>
        public static ICSharpCompilerReferenceError CS0729
        {
            get
            {
                if (_CS0729 == null)
                    _CS0729 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0729, CSharpErrorIdentifiers.CS0729);
                return _CS0729;
            }
        }
        private static ICSharpCompilerReferenceError _CS0729;

        /// <summary><para>C&#9839; compiler error &#35;730:</para><para>Cannot forward type {0} because it is a nested type of {1}</para></summary>
        public static ICSharpCompilerReferenceError CS0730
        {
            get
            {
                if (_CS0730 == null)
                    _CS0730 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0730, CSharpErrorIdentifiers.CS0730);
                return _CS0730;
            }
        }
        private static ICSharpCompilerReferenceError _CS0730;

        /// <summary><para>C&#9839; compiler error &#35;731:</para><para>The type forwarder for type {0} in assembly {1} causes a cycle</para></summary>
        public static ICSharpCompilerReferenceError CS0731
        {
            get
            {
                if (_CS0731 == null)
                    _CS0731 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0731, CSharpErrorIdentifiers.CS0731);
                return _CS0731;
            }
        }
        private static ICSharpCompilerReferenceError _CS0731;

        /// <summary><para>C&#9839; compiler error &#35;733:</para><para>Cannot forward generic type, {0}</para></summary>
        public static ICSharpCompilerReferenceError CS0733
        {
            get
            {
                if (_CS0733 == null)
                    _CS0733 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0733, CSharpErrorIdentifiers.CS0733);
                return _CS0733;
            }
        }
        private static ICSharpCompilerReferenceError _CS0733;

        /// <summary><para>C&#9839; compiler error &#35;734:</para><para>The /moduleassemblyname option may only be specified when building a target type of 'module'</para></summary>
        public static ICSharpCompilerReferenceError CS0734
        {
            get
            {
                if (_CS0734 == null)
                    _CS0734 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0734, CSharpErrorIdentifiers.CS0734);
                return _CS0734;
            }
        }
        private static ICSharpCompilerReferenceError _CS0734;

        /// <summary><para>C&#9839; compiler error &#35;735:</para><para>Invalid type specified as an argument for TypeForwardedTo attribute</para></summary>
        public static ICSharpCompilerReferenceError CS0735
        {
            get
            {
                if (_CS0735 == null)
                    _CS0735 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0735, CSharpErrorIdentifiers.CS0735);
                return _CS0735;
            }
        }
        private static ICSharpCompilerReferenceError _CS0735;

        /// <summary><para>C&#9839; compiler error &#35;736:</para><para>{0} does not implement interface member {1}. {2} cannot implement an interface member because it is static.</para></summary>
        public static ICSharpCompilerReferenceError CS0736
        {
            get
            {
                if (_CS0736 == null)
                    _CS0736 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0736, CSharpErrorIdentifiers.CS0736);
                return _CS0736;
            }
        }
        private static ICSharpCompilerReferenceError _CS0736;

        /// <summary><para>C&#9839; compiler error &#35;737:</para><para>{0} does not implement interface member {1}. {2} cannot implement an interface member because it is not public.</para></summary>
        public static ICSharpCompilerReferenceError CS0737
        {
            get
            {
                if (_CS0737 == null)
                    _CS0737 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0737, CSharpErrorIdentifiers.CS0737);
                return _CS0737;
            }
        }
        private static ICSharpCompilerReferenceError _CS0737;

        /// <summary><para>C&#9839; compiler error &#35;738:</para><para>{0} does not implement interface member {1}. {2} cannot implement {1} because it does not have the matching return type of {3}.</para></summary>
        public static ICSharpCompilerReferenceError CS0738
        {
            get
            {
                if (_CS0738 == null)
                    _CS0738 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0738, CSharpErrorIdentifiers.CS0738);
                return _CS0738;
            }
        }
        private static ICSharpCompilerReferenceError _CS0738;

        /// <summary><para>C&#9839; compiler error &#35;739:</para><para>{0} duplicate TypeForwardedToAttribute.</para></summary>
        public static ICSharpCompilerReferenceError CS0739
        {
            get
            {
                if (_CS0739 == null)
                    _CS0739 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0739, CSharpErrorIdentifiers.CS0739);
                return _CS0739;
            }
        }
        private static ICSharpCompilerReferenceError _CS0739;

        /// <summary><para>C&#9839; compiler error &#35;742:</para><para>A query body must end with a select clause or a group clause</para></summary>
        public static ICSharpCompilerReferenceError CS0742
        {
            get
            {
                if (_CS0742 == null)
                    _CS0742 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0742, CSharpErrorIdentifiers.CS0742);
                return _CS0742;
            }
        }
        private static ICSharpCompilerReferenceError _CS0742;

        /// <summary><para>C&#9839; compiler error &#35;743:</para><para>Expected contextual keyword 'on'</para></summary>
        public static ICSharpCompilerReferenceError CS0743
        {
            get
            {
                if (_CS0743 == null)
                    _CS0743 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0743, CSharpErrorIdentifiers.CS0743);
                return _CS0743;
            }
        }
        private static ICSharpCompilerReferenceError _CS0743;

        /// <summary><para>C&#9839; compiler error &#35;744:</para><para>Expected contextual keyword 'equals'</para></summary>
        public static ICSharpCompilerReferenceError CS0744
        {
            get
            {
                if (_CS0744 == null)
                    _CS0744 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0744, CSharpErrorIdentifiers.CS0744);
                return _CS0744;
            }
        }
        private static ICSharpCompilerReferenceError _CS0744;

        /// <summary><para>C&#9839; compiler error &#35;745:</para><para>Expected contextual keyword 'by'</para></summary>
        public static ICSharpCompilerReferenceError CS0745
        {
            get
            {
                if (_CS0745 == null)
                    _CS0745 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0745, CSharpErrorIdentifiers.CS0745);
                return _CS0745;
            }
        }
        private static ICSharpCompilerReferenceError _CS0745;

        /// <summary><para>C&#9839; compiler error &#35;746:</para><para>Invalid anonymous type member declarator. Anonymous type members must be declared with a member assignment, simple name or member access.</para></summary>
        public static ICSharpCompilerReferenceError CS0746
        {
            get
            {
                if (_CS0746 == null)
                    _CS0746 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0746, CSharpErrorIdentifiers.CS0746);
                return _CS0746;
            }
        }
        private static ICSharpCompilerReferenceError _CS0746;

        /// <summary><para>C&#9839; compiler error &#35;747:</para><para>Invalid initializer member declarator.</para></summary>
        public static ICSharpCompilerReferenceError CS0747
        {
            get
            {
                if (_CS0747 == null)
                    _CS0747 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0747, CSharpErrorIdentifiers.CS0747);
                return _CS0747;
            }
        }
        private static ICSharpCompilerReferenceError _CS0747;

        /// <summary><para>C&#9839; compiler error &#35;748:</para><para>Inconsistent lambda parameter usage; all parameter types must either be explicit or implicit.</para></summary>
        public static ICSharpCompilerReferenceError CS0748
        {
            get
            {
                if (_CS0748 == null)
                    _CS0748 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0748, CSharpErrorIdentifiers.CS0748);
                return _CS0748;
            }
        }
        private static ICSharpCompilerReferenceError _CS0748;

        /// <summary><para>C&#9839; compiler error &#35;750:</para><para>A partial method cannot have access modifiers or the virtual, abstract, override, new, sealed, or extern modifiers.</para></summary>
        public static ICSharpCompilerReferenceError CS0750
        {
            get
            {
                if (_CS0750 == null)
                    _CS0750 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0750, CSharpErrorIdentifiers.CS0750);
                return _CS0750;
            }
        }
        private static ICSharpCompilerReferenceError _CS0750;

        /// <summary><para>C&#9839; compiler error &#35;751:</para><para>A partial method must be declared in a partial class or partial struct</para></summary>
        public static ICSharpCompilerReferenceError CS0751
        {
            get
            {
                if (_CS0751 == null)
                    _CS0751 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0751, CSharpErrorIdentifiers.CS0751);
                return _CS0751;
            }
        }
        private static ICSharpCompilerReferenceError _CS0751;

        /// <summary><para>C&#9839; compiler error &#35;752:</para><para>A partial method cannot have out parameters</para></summary>
        public static ICSharpCompilerReferenceError CS0752
        {
            get
            {
                if (_CS0752 == null)
                    _CS0752 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0752, CSharpErrorIdentifiers.CS0752);
                return _CS0752;
            }
        }
        private static ICSharpCompilerReferenceError _CS0752;

        /// <summary><para>C&#9839; compiler error &#35;753:</para><para>Only methods, classes, structs, or interfaces may be partial.</para></summary>
        public static ICSharpCompilerReferenceError CS0753
        {
            get
            {
                if (_CS0753 == null)
                    _CS0753 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0753, CSharpErrorIdentifiers.CS0753);
                return _CS0753;
            }
        }
        private static ICSharpCompilerReferenceError _CS0753;

        /// <summary><para>C&#9839; compiler error &#35;754:</para><para>A partial method may not explicitly implement an interface method.</para></summary>
        public static ICSharpCompilerReferenceError CS0754
        {
            get
            {
                if (_CS0754 == null)
                    _CS0754 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0754, CSharpErrorIdentifiers.CS0754);
                return _CS0754;
            }
        }
        private static ICSharpCompilerReferenceError _CS0754;

        /// <summary><para>C&#9839; compiler error &#35;755:</para><para>Both partial method declarations must be extension methods or neither may be an extension method.</para></summary>
        public static ICSharpCompilerReferenceError CS0755
        {
            get
            {
                if (_CS0755 == null)
                    _CS0755 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0755, CSharpErrorIdentifiers.CS0755);
                return _CS0755;
            }
        }
        private static ICSharpCompilerReferenceError _CS0755;

        /// <summary><para>C&#9839; compiler error &#35;756:</para><para>A partial method may not have multiple defining declarations.</para></summary>
        public static ICSharpCompilerReferenceError CS0756
        {
            get
            {
                if (_CS0756 == null)
                    _CS0756 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0756, CSharpErrorIdentifiers.CS0756);
                return _CS0756;
            }
        }
        private static ICSharpCompilerReferenceError _CS0756;

        /// <summary><para>C&#9839; compiler error &#35;757:</para><para>A partial method may not have multiple implementing declarations.</para></summary>
        public static ICSharpCompilerReferenceError CS0757
        {
            get
            {
                if (_CS0757 == null)
                    _CS0757 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0757, CSharpErrorIdentifiers.CS0757);
                return _CS0757;
            }
        }
        private static ICSharpCompilerReferenceError _CS0757;

        /// <summary><para>C&#9839; compiler error &#35;758:</para><para>Both partial method declarations must use a params parameter or neither may use a params parameter</para></summary>
        public static ICSharpCompilerReferenceError CS0758
        {
            get
            {
                if (_CS0758 == null)
                    _CS0758 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0758, CSharpErrorIdentifiers.CS0758);
                return _CS0758;
            }
        }
        private static ICSharpCompilerReferenceError _CS0758;

        /// <summary><para>C&#9839; compiler error &#35;759:</para><para>No defining declaration found for implementing declaration of partial method {0}.</para></summary>
        public static ICSharpCompilerReferenceError CS0759
        {
            get
            {
                if (_CS0759 == null)
                    _CS0759 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0759, CSharpErrorIdentifiers.CS0759);
                return _CS0759;
            }
        }
        private static ICSharpCompilerReferenceError _CS0759;

        /// <summary><para>C&#9839; compiler error &#35;761:</para><para>Partial method declarations of {0} have inconsistent type parameter constraints.</para></summary>
        public static ICSharpCompilerReferenceError CS0761
        {
            get
            {
                if (_CS0761 == null)
                    _CS0761 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0761, CSharpErrorIdentifiers.CS0761);
                return _CS0761;
            }
        }
        private static ICSharpCompilerReferenceError _CS0761;

        /// <summary><para>C&#9839; compiler error &#35;762:</para><para>Cannot create delegate from method {0} because it is a partial method without an implementing declaration</para></summary>
        public static ICSharpCompilerReferenceError CS0762
        {
            get
            {
                if (_CS0762 == null)
                    _CS0762 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0762, CSharpErrorIdentifiers.CS0762);
                return _CS0762;
            }
        }
        private static ICSharpCompilerReferenceError _CS0762;

        /// <summary><para>C&#9839; compiler error &#35;763:</para><para>Both partial method declarations must be static or neither may be static.</para></summary>
        public static ICSharpCompilerReferenceError CS0763
        {
            get
            {
                if (_CS0763 == null)
                    _CS0763 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0763, CSharpErrorIdentifiers.CS0763);
                return _CS0763;
            }
        }
        private static ICSharpCompilerReferenceError _CS0763;

        /// <summary><para>C&#9839; compiler error &#35;764:</para><para>Both partial method declarations must be unsafe or neither may be unsafe</para></summary>
        public static ICSharpCompilerReferenceError CS0764
        {
            get
            {
                if (_CS0764 == null)
                    _CS0764 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0764, CSharpErrorIdentifiers.CS0764);
                return _CS0764;
            }
        }
        private static ICSharpCompilerReferenceError _CS0764;

        /// <summary><para>C&#9839; compiler error &#35;765:</para><para>Partial methods with only a defining declaration or removed conditional methods cannot be used in expression trees</para></summary>
        public static ICSharpCompilerReferenceError CS0765
        {
            get
            {
                if (_CS0765 == null)
                    _CS0765 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0765, CSharpErrorIdentifiers.CS0765);
                return _CS0765;
            }
        }
        private static ICSharpCompilerReferenceError _CS0765;

        /// <summary><para>C&#9839; compiler error &#35;766:</para><para>Partial methods must have a void return type.</para></summary>
        public static ICSharpCompilerReferenceError CS0766
        {
            get
            {
                if (_CS0766 == null)
                    _CS0766 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0766, CSharpErrorIdentifiers.CS0766);
                return _CS0766;
            }
        }
        private static ICSharpCompilerReferenceError _CS0766;

        /// <summary><para>C&#9839; compiler error &#35;811:</para><para>The fully qualified name for {0} is too long for debug information. Compile without '/debug' option.</para></summary>
        public static ICSharpCompilerReferenceError CS0811
        {
            get
            {
                if (_CS0811 == null)
                    _CS0811 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0811, CSharpErrorIdentifiers.CS0811);
                return _CS0811;
            }
        }
        private static ICSharpCompilerReferenceError _CS0811;

        /// <summary><para>C&#9839; compiler error &#35;815:</para><para>Cannot assign {0} to an implicitly typed local</para></summary>
        public static ICSharpCompilerReferenceError CS0815
        {
            get
            {
                if (_CS0815 == null)
                    _CS0815 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0815, CSharpErrorIdentifiers.CS0815);
                return _CS0815;
            }
        }
        private static ICSharpCompilerReferenceError _CS0815;

        /// <summary><para>C&#9839; compiler error &#35;818:</para><para>Implicitly typed locals must be initialized</para></summary>
        public static ICSharpCompilerReferenceError CS0818
        {
            get
            {
                if (_CS0818 == null)
                    _CS0818 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0818, CSharpErrorIdentifiers.CS0818);
                return _CS0818;
            }
        }
        private static ICSharpCompilerReferenceError _CS0818;

        /// <summary><para>C&#9839; compiler error &#35;819:</para><para>Implicitly typed locals cannot have multiple declarators.</para></summary>
        public static ICSharpCompilerReferenceError CS0819
        {
            get
            {
                if (_CS0819 == null)
                    _CS0819 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0819, CSharpErrorIdentifiers.CS0819);
                return _CS0819;
            }
        }
        private static ICSharpCompilerReferenceError _CS0819;

        /// <summary><para>C&#9839; compiler error &#35;820:</para><para>Cannot assign array initializer to an implicitly typed local</para></summary>
        public static ICSharpCompilerReferenceError CS0820
        {
            get
            {
                if (_CS0820 == null)
                    _CS0820 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0820, CSharpErrorIdentifiers.CS0820);
                return _CS0820;
            }
        }
        private static ICSharpCompilerReferenceError _CS0820;

        /// <summary><para>C&#9839; compiler error &#35;821:</para><para>Implicitly typed locals cannot be fixed</para></summary>
        public static ICSharpCompilerReferenceError CS0821
        {
            get
            {
                if (_CS0821 == null)
                    _CS0821 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0821, CSharpErrorIdentifiers.CS0821);
                return _CS0821;
            }
        }
        private static ICSharpCompilerReferenceError _CS0821;

        /// <summary><para>C&#9839; compiler error &#35;822:</para><para>Implicitly typed locals cannot be const</para></summary>
        public static ICSharpCompilerReferenceError CS0822
        {
            get
            {
                if (_CS0822 == null)
                    _CS0822 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0822, CSharpErrorIdentifiers.CS0822);
                return _CS0822;
            }
        }
        private static ICSharpCompilerReferenceError _CS0822;

        /// <summary><para>C&#9839; compiler error &#35;825:</para><para>The contextual keyword 'var' may only appear within a local variable declaration.</para></summary>
        public static ICSharpCompilerReferenceError CS0825
        {
            get
            {
                if (_CS0825 == null)
                    _CS0825 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0825, CSharpErrorIdentifiers.CS0825);
                return _CS0825;
            }
        }
        private static ICSharpCompilerReferenceError _CS0825;

        /// <summary><para>C&#9839; compiler error &#35;826:</para><para>No best type found for implicitly typed array.</para></summary>
        public static ICSharpCompilerReferenceError CS0826
        {
            get
            {
                if (_CS0826 == null)
                    _CS0826 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0826, CSharpErrorIdentifiers.CS0826);
                return _CS0826;
            }
        }
        private static ICSharpCompilerReferenceError _CS0826;

        /// <summary><para>C&#9839; compiler error &#35;828:</para><para>Cannot assign {0} to anonymous type property.</para></summary>
        public static ICSharpCompilerReferenceError CS0828
        {
            get
            {
                if (_CS0828 == null)
                    _CS0828 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0828, CSharpErrorIdentifiers.CS0828);
                return _CS0828;
            }
        }
        private static ICSharpCompilerReferenceError _CS0828;

        /// <summary><para>C&#9839; compiler error &#35;831:</para><para>An expression tree may not contain a base access.</para></summary>
        public static ICSharpCompilerReferenceError CS0831
        {
            get
            {
                if (_CS0831 == null)
                    _CS0831 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0831, CSharpErrorIdentifiers.CS0831);
                return _CS0831;
            }
        }
        private static ICSharpCompilerReferenceError _CS0831;

        /// <summary><para>C&#9839; compiler error &#35;832:</para><para>An expression tree may not contain an assignment operator.</para></summary>
        public static ICSharpCompilerReferenceError CS0832
        {
            get
            {
                if (_CS0832 == null)
                    _CS0832 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0832, CSharpErrorIdentifiers.CS0832);
                return _CS0832;
            }
        }
        private static ICSharpCompilerReferenceError _CS0832;

        /// <summary><para>C&#9839; compiler error &#35;833:</para><para>An anonymous type cannot have multiple properties with the same name.</para></summary>
        public static ICSharpCompilerReferenceError CS0833
        {
            get
            {
                if (_CS0833 == null)
                    _CS0833 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0833, CSharpErrorIdentifiers.CS0833);
                return _CS0833;
            }
        }
        private static ICSharpCompilerReferenceError _CS0833;

        /// <summary><para>C&#9839; compiler error &#35;834:</para><para>A lambda expression must have an expression body to be converted to an expression tree.</para></summary>
        public static ICSharpCompilerReferenceError CS0834
        {
            get
            {
                if (_CS0834 == null)
                    _CS0834 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0834, CSharpErrorIdentifiers.CS0834);
                return _CS0834;
            }
        }
        private static ICSharpCompilerReferenceError _CS0834;

        /// <summary><para>C&#9839; compiler error &#35;835:</para><para>Cannot convert lambda to an expression tree whose type argument {0} is not a delegate type.</para></summary>
        public static ICSharpCompilerReferenceError CS0835
        {
            get
            {
                if (_CS0835 == null)
                    _CS0835 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0835, CSharpErrorIdentifiers.CS0835);
                return _CS0835;
            }
        }
        private static ICSharpCompilerReferenceError _CS0835;

        /// <summary><para>C&#9839; compiler error &#35;836:</para><para>Cannot use anonymous type in a constant expression.</para></summary>
        public static ICSharpCompilerReferenceError CS0836
        {
            get
            {
                if (_CS0836 == null)
                    _CS0836 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0836, CSharpErrorIdentifiers.CS0836);
                return _CS0836;
            }
        }
        private static ICSharpCompilerReferenceError _CS0836;

        /// <summary><para>C&#9839; compiler error &#35;837:</para><para>The first operand of an 'is' or 'as' operator may not be a lambda expression or anonymous method.</para></summary>
        public static ICSharpCompilerReferenceError CS0837
        {
            get
            {
                if (_CS0837 == null)
                    _CS0837 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0837, CSharpErrorIdentifiers.CS0837);
                return _CS0837;
            }
        }
        private static ICSharpCompilerReferenceError _CS0837;

        /// <summary><para>C&#9839; compiler error &#35;838:</para><para>An expression tree may not contain a multidimensional array initializer.</para></summary>
        public static ICSharpCompilerReferenceError CS0838
        {
            get
            {
                if (_CS0838 == null)
                    _CS0838 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0838, CSharpErrorIdentifiers.CS0838);
                return _CS0838;
            }
        }
        private static ICSharpCompilerReferenceError _CS0838;

        /// <summary><para>C&#9839; compiler error &#35;839:</para><para>Argument missing.</para></summary>
        public static ICSharpCompilerReferenceError CS0839
        {
            get
            {
                if (_CS0839 == null)
                    _CS0839 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0839, CSharpErrorIdentifiers.CS0839);
                return _CS0839;
            }
        }
        private static ICSharpCompilerReferenceError _CS0839;

        /// <summary><para>C&#9839; compiler error &#35;840:</para><para>{0} must declare a body because it is not marked abstract or extern. Automatically implemented properties must define both get and set accessors.</para></summary>
        public static ICSharpCompilerReferenceError CS0840
        {
            get
            {
                if (_CS0840 == null)
                    _CS0840 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0840, CSharpErrorIdentifiers.CS0840);
                return _CS0840;
            }
        }
        private static ICSharpCompilerReferenceError _CS0840;

        /// <summary><para>C&#9839; compiler error &#35;841:</para><para>Cannot use variable {0} before it is declared.</para></summary>
        public static ICSharpCompilerReferenceError CS0841
        {
            get
            {
                if (_CS0841 == null)
                    _CS0841 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0841, CSharpErrorIdentifiers.CS0841);
                return _CS0841;
            }
        }
        private static ICSharpCompilerReferenceError _CS0841;

        /// <summary><para>C&#9839; compiler error &#35;842:</para><para>Automatically implemented properties cannot be used inside a type marked with StructLayout(LayoutKind.Explicit).</para></summary>
        public static ICSharpCompilerReferenceError CS0842
        {
            get
            {
                if (_CS0842 == null)
                    _CS0842 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0842, CSharpErrorIdentifiers.CS0842);
                return _CS0842;
            }
        }
        private static ICSharpCompilerReferenceError _CS0842;

        /// <summary><para>C&#9839; compiler error &#35;843:</para><para>Backing field for automatically implemented property {0} must be fully assigned before control is returned to the caller. Consider calling the default constructor from a constructor initializer.</para></summary>
        public static ICSharpCompilerReferenceError CS0843
        {
            get
            {
                if (_CS0843 == null)
                    _CS0843 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0843, CSharpErrorIdentifiers.CS0843);
                return _CS0843;
            }
        }
        private static ICSharpCompilerReferenceError _CS0843;

        /// <summary><para>C&#9839; compiler error &#35;844:</para><para>Cannot use local variable {0} before it is declared. The declaration of the local variable hides the field {1}.</para></summary>
        public static ICSharpCompilerReferenceError CS0844
        {
            get
            {
                if (_CS0844 == null)
                    _CS0844 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0844, CSharpErrorIdentifiers.CS0844);
                return _CS0844;
            }
        }
        private static ICSharpCompilerReferenceError _CS0844;

        /// <summary><para>C&#9839; compiler error &#35;845:</para><para>An expression tree lambda may not contain a coalescing operator with a null literal left-hand side.</para></summary>
        public static ICSharpCompilerReferenceError CS0845
        {
            get
            {
                if (_CS0845 == null)
                    _CS0845 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS0845, CSharpErrorIdentifiers.CS0845);
                return _CS0845;
            }
        }
        private static ICSharpCompilerReferenceError _CS0845;

        /// <summary><para>C&#9839; compiler error &#35;1001:</para><para>Identifier expected</para></summary>
        public static ICSharpCompilerReferenceError CS1001
        {
            get
            {
                if (_CS1001 == null)
                    _CS1001 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1001, CSharpErrorIdentifiers.CS1001);
                return _CS1001;
            }
        }
        private static ICSharpCompilerReferenceError _CS1001;

        /// <summary><para>C&#9839; compiler error &#35;1002:</para><para>; expected</para></summary>
        public static ICSharpCompilerReferenceError CS1002
        {
            get
            {
                if (_CS1002 == null)
                    _CS1002 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1002, CSharpErrorIdentifiers.CS1002);
                return _CS1002;
            }
        }
        private static ICSharpCompilerReferenceError _CS1002;

        /// <summary><para>C&#9839; compiler error &#35;1003:</para><para>Syntax error, 'char' expected</para></summary>
        public static ICSharpCompilerReferenceError CS1003
        {
            get
            {
                if (_CS1003 == null)
                    _CS1003 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1003, CSharpErrorIdentifiers.CS1003);
                return _CS1003;
            }
        }
        private static ICSharpCompilerReferenceError _CS1003;

        /// <summary><para>C&#9839; compiler error &#35;1004:</para><para>Duplicate {0} modifier</para></summary>
        public static ICSharpCompilerReferenceError CS1004
        {
            get
            {
                if (_CS1004 == null)
                    _CS1004 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1004, CSharpErrorIdentifiers.CS1004);
                return _CS1004;
            }
        }
        private static ICSharpCompilerReferenceError _CS1004;

        /// <summary><para>C&#9839; compiler error &#35;1007:</para><para>Property accessor already defined</para></summary>
        public static ICSharpCompilerReferenceError CS1007
        {
            get
            {
                if (_CS1007 == null)
                    _CS1007 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1007, CSharpErrorIdentifiers.CS1007);
                return _CS1007;
            }
        }
        private static ICSharpCompilerReferenceError _CS1007;

        /// <summary><para>C&#9839; compiler error &#35;1008:</para><para>GenericParameter byte, sbyte, short, ushort, int, uint, long, or ulong expected</para></summary>
        public static ICSharpCompilerReferenceError CS1008
        {
            get
            {
                if (_CS1008 == null)
                    _CS1008 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1008, CSharpErrorIdentifiers.CS1008);
                return _CS1008;
            }
        }
        private static ICSharpCompilerReferenceError _CS1008;

        /// <summary><para>C&#9839; compiler error &#35;1009:</para><para>Unrecognized escape sequence</para></summary>
        public static ICSharpCompilerReferenceError CS1009
        {
            get
            {
                if (_CS1009 == null)
                    _CS1009 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1009, CSharpErrorIdentifiers.CS1009);
                return _CS1009;
            }
        }
        private static ICSharpCompilerReferenceError _CS1009;

        /// <summary><para>C&#9839; compiler error &#35;1010:</para><para>Newline in constant</para></summary>
        public static ICSharpCompilerReferenceError CS1010
        {
            get
            {
                if (_CS1010 == null)
                    _CS1010 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1010, CSharpErrorIdentifiers.CS1010);
                return _CS1010;
            }
        }
        private static ICSharpCompilerReferenceError _CS1010;

        /// <summary><para>C&#9839; compiler error &#35;1011:</para><para>Empty character literal</para></summary>
        public static ICSharpCompilerReferenceError CS1011
        {
            get
            {
                if (_CS1011 == null)
                    _CS1011 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1011, CSharpErrorIdentifiers.CS1011);
                return _CS1011;
            }
        }
        private static ICSharpCompilerReferenceError _CS1011;

        /// <summary><para>C&#9839; compiler error &#35;1012:</para><para>Too many characters in character literal</para></summary>
        public static ICSharpCompilerReferenceError CS1012
        {
            get
            {
                if (_CS1012 == null)
                    _CS1012 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1012, CSharpErrorIdentifiers.CS1012);
                return _CS1012;
            }
        }
        private static ICSharpCompilerReferenceError _CS1012;

        /// <summary><para>C&#9839; compiler error &#35;1013:</para><para>Invalid number</para></summary>
        public static ICSharpCompilerReferenceError CS1013
        {
            get
            {
                if (_CS1013 == null)
                    _CS1013 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1013, CSharpErrorIdentifiers.CS1013);
                return _CS1013;
            }
        }
        private static ICSharpCompilerReferenceError _CS1013;

        /// <summary><para>C&#9839; compiler error &#35;1014:</para><para>A get or set accessor expected</para></summary>
        public static ICSharpCompilerReferenceError CS1014
        {
            get
            {
                if (_CS1014 == null)
                    _CS1014 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1014, CSharpErrorIdentifiers.CS1014);
                return _CS1014;
            }
        }
        private static ICSharpCompilerReferenceError _CS1014;

        /// <summary><para>C&#9839; compiler error &#35;1015:</para><para>An object, string, or class type expected</para></summary>
        public static ICSharpCompilerReferenceError CS1015
        {
            get
            {
                if (_CS1015 == null)
                    _CS1015 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1015, CSharpErrorIdentifiers.CS1015);
                return _CS1015;
            }
        }
        private static ICSharpCompilerReferenceError _CS1015;

        /// <summary><para>C&#9839; compiler error &#35;1016:</para><para>Named attribute argument expected</para></summary>
        public static ICSharpCompilerReferenceError CS1016
        {
            get
            {
                if (_CS1016 == null)
                    _CS1016 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1016, CSharpErrorIdentifiers.CS1016);
                return _CS1016;
            }
        }
        private static ICSharpCompilerReferenceError _CS1016;

        /// <summary><para>C&#9839; compiler error &#35;1017:</para><para>Catch clauses cannot follow the general catch clause of a try statement</para></summary>
        public static ICSharpCompilerReferenceError CS1017
        {
            get
            {
                if (_CS1017 == null)
                    _CS1017 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1017, CSharpErrorIdentifiers.CS1017);
                return _CS1017;
            }
        }
        private static ICSharpCompilerReferenceError _CS1017;

        /// <summary><para>C&#9839; compiler error &#35;1018:</para><para>Keyword 'this' or 'base' expected</para></summary>
        public static ICSharpCompilerReferenceError CS1018
        {
            get
            {
                if (_CS1018 == null)
                    _CS1018 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1018, CSharpErrorIdentifiers.CS1018);
                return _CS1018;
            }
        }
        private static ICSharpCompilerReferenceError _CS1018;

        /// <summary><para>C&#9839; compiler error &#35;1019:</para><para>Overloadable unary operator expected</para></summary>
        public static ICSharpCompilerReferenceError CS1019
        {
            get
            {
                if (_CS1019 == null)
                    _CS1019 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1019, CSharpErrorIdentifiers.CS1019);
                return _CS1019;
            }
        }
        private static ICSharpCompilerReferenceError _CS1019;

        /// <summary><para>C&#9839; compiler error &#35;1020:</para><para>Overloadable binary operator expected</para></summary>
        public static ICSharpCompilerReferenceError CS1020
        {
            get
            {
                if (_CS1020 == null)
                    _CS1020 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1020, CSharpErrorIdentifiers.CS1020);
                return _CS1020;
            }
        }
        private static ICSharpCompilerReferenceError _CS1020;

        /// <summary><para>C&#9839; compiler error &#35;1021:</para><para>Integral constant is too large</para></summary>
        public static ICSharpCompilerReferenceError CS1021
        {
            get
            {
                if (_CS1021 == null)
                    _CS1021 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1021, CSharpErrorIdentifiers.CS1021);
                return _CS1021;
            }
        }
        private static ICSharpCompilerReferenceError _CS1021;

        /// <summary><para>C&#9839; compiler error &#35;1022:</para><para>GenericParameter or namespace definition, or end-of-file expected</para></summary>
        public static ICSharpCompilerReferenceError CS1022
        {
            get
            {
                if (_CS1022 == null)
                    _CS1022 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1022, CSharpErrorIdentifiers.CS1022);
                return _CS1022;
            }
        }
        private static ICSharpCompilerReferenceError _CS1022;

        /// <summary><para>C&#9839; compiler error &#35;1023:</para><para>Embedded statement cannot be a declaration or labeled statement</para></summary>
        public static ICSharpCompilerReferenceError CS1023
        {
            get
            {
                if (_CS1023 == null)
                    _CS1023 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1023, CSharpErrorIdentifiers.CS1023);
                return _CS1023;
            }
        }
        private static ICSharpCompilerReferenceError _CS1023;

        /// <summary><para>C&#9839; compiler error &#35;1024:</para><para>Preprocessor directive expected</para></summary>
        public static ICSharpCompilerReferenceError CS1024
        {
            get
            {
                if (_CS1024 == null)
                    _CS1024 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1024, CSharpErrorIdentifiers.CS1024);
                return _CS1024;
            }
        }
        private static ICSharpCompilerReferenceError _CS1024;

        /// <summary><para>C&#9839; compiler error &#35;1025:</para><para>Single-line comment or end-of-line expected</para></summary>
        public static ICSharpCompilerReferenceError CS1025
        {
            get
            {
                if (_CS1025 == null)
                    _CS1025 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1025, CSharpErrorIdentifiers.CS1025);
                return _CS1025;
            }
        }
        private static ICSharpCompilerReferenceError _CS1025;

        /// <summary><para>C&#9839; compiler error &#35;1026:</para><para>) expected</para></summary>
        public static ICSharpCompilerReferenceError CS1026
        {
            get
            {
                if (_CS1026 == null)
                    _CS1026 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1026, CSharpErrorIdentifiers.CS1026);
                return _CS1026;
            }
        }
        private static ICSharpCompilerReferenceError _CS1026;

        /// <summary><para>C&#9839; compiler error &#35;1027:</para><para>#endif directive expected</para></summary>
        public static ICSharpCompilerReferenceError CS1027
        {
            get
            {
                if (_CS1027 == null)
                    _CS1027 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1027, CSharpErrorIdentifiers.CS1027);
                return _CS1027;
            }
        }
        private static ICSharpCompilerReferenceError _CS1027;

        /// <summary><para>C&#9839; compiler error &#35;1028:</para><para>Unexpected preprocessor directive</para></summary>
        public static ICSharpCompilerReferenceError CS1028
        {
            get
            {
                if (_CS1028 == null)
                    _CS1028 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1028, CSharpErrorIdentifiers.CS1028);
                return _CS1028;
            }
        }
        private static ICSharpCompilerReferenceError _CS1028;

        /// <summary><para>C&#9839; compiler error &#35;1029:</para><para>#error: {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1029
        {
            get
            {
                if (_CS1029 == null)
                    _CS1029 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1029, CSharpErrorIdentifiers.CS1029);
                return _CS1029;
            }
        }
        private static ICSharpCompilerReferenceError _CS1029;

        /// <summary><para>C&#9839; compiler error &#35;1031:</para><para>GenericParameter expected</para></summary>
        public static ICSharpCompilerReferenceError CS1031
        {
            get
            {
                if (_CS1031 == null)
                    _CS1031 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1031, CSharpErrorIdentifiers.CS1031);
                return _CS1031;
            }
        }
        private static ICSharpCompilerReferenceError _CS1031;

        /// <summary><para>C&#9839; compiler error &#35;1032:</para><para>Cannot define/undefine preprocessor symbols after first token in file</para></summary>
        public static ICSharpCompilerReferenceError CS1032
        {
            get
            {
                if (_CS1032 == null)
                    _CS1032 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1032, CSharpErrorIdentifiers.CS1032);
                return _CS1032;
            }
        }
        private static ICSharpCompilerReferenceError _CS1032;

        /// <summary><para>C&#9839; compiler error &#35;1033:</para><para>Source file has exceeded the limit of 16,707,565 lines representable in the PDB; debug information will be incorrect</para></summary>
        public static ICSharpCompilerReferenceError CS1033
        {
            get
            {
                if (_CS1033 == null)
                    _CS1033 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1033, CSharpErrorIdentifiers.CS1033);
                return _CS1033;
            }
        }
        private static ICSharpCompilerReferenceError _CS1033;

        /// <summary><para>C&#9839; compiler error &#35;1034:</para><para>Compiler limit exceeded: Line cannot exceed 'number' characters</para></summary>
        public static ICSharpCompilerReferenceError CS1034
        {
            get
            {
                if (_CS1034 == null)
                    _CS1034 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1034, CSharpErrorIdentifiers.CS1034);
                return _CS1034;
            }
        }
        private static ICSharpCompilerReferenceError _CS1034;

        /// <summary><para>C&#9839; compiler error &#35;1035:</para><para>End-of-file found, '*/' expected</para></summary>
        public static ICSharpCompilerReferenceError CS1035
        {
            get
            {
                if (_CS1035 == null)
                    _CS1035 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1035, CSharpErrorIdentifiers.CS1035);
                return _CS1035;
            }
        }
        private static ICSharpCompilerReferenceError _CS1035;

        /// <summary><para>C&#9839; compiler error &#35;1036:</para><para>( or . expected</para></summary>
        public static ICSharpCompilerReferenceError CS1036
        {
            get
            {
                if (_CS1036 == null)
                    _CS1036 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1036, CSharpErrorIdentifiers.CS1036);
                return _CS1036;
            }
        }
        private static ICSharpCompilerReferenceError _CS1036;

        /// <summary><para>C&#9839; compiler error &#35;1037:</para><para>Overloadable operator expected</para></summary>
        public static ICSharpCompilerReferenceError CS1037
        {
            get
            {
                if (_CS1037 == null)
                    _CS1037 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1037, CSharpErrorIdentifiers.CS1037);
                return _CS1037;
            }
        }
        private static ICSharpCompilerReferenceError _CS1037;

        /// <summary><para>C&#9839; compiler error &#35;1038:</para><para>#endregion directive expected</para></summary>
        public static ICSharpCompilerReferenceError CS1038
        {
            get
            {
                if (_CS1038 == null)
                    _CS1038 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1038, CSharpErrorIdentifiers.CS1038);
                return _CS1038;
            }
        }
        private static ICSharpCompilerReferenceError _CS1038;

        /// <summary><para>C&#9839; compiler error &#35;1039:</para><para>Unterminated string literal</para></summary>
        public static ICSharpCompilerReferenceError CS1039
        {
            get
            {
                if (_CS1039 == null)
                    _CS1039 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1039, CSharpErrorIdentifiers.CS1039);
                return _CS1039;
            }
        }
        private static ICSharpCompilerReferenceError _CS1039;

        /// <summary><para>C&#9839; compiler error &#35;1040:</para><para>Preprocessor directives must appear as the first non-whitespace character on a line</para></summary>
        public static ICSharpCompilerReferenceError CS1040
        {
            get
            {
                if (_CS1040 == null)
                    _CS1040 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1040, CSharpErrorIdentifiers.CS1040);
                return _CS1040;
            }
        }
        private static ICSharpCompilerReferenceError _CS1040;

        /// <summary><para>C&#9839; compiler error &#35;1041:</para><para>Identifier expected, 'keyword' is a keyword</para></summary>
        public static ICSharpCompilerReferenceError CS1041
        {
            get
            {
                if (_CS1041 == null)
                    _CS1041 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1041, CSharpErrorIdentifiers.CS1041);
                return _CS1041;
            }
        }
        private static ICSharpCompilerReferenceError _CS1041;

        /// <summary><para>C&#9839; compiler error &#35;1043:</para><para>{ or ; expected</para></summary>
        public static ICSharpCompilerReferenceError CS1043
        {
            get
            {
                if (_CS1043 == null)
                    _CS1043 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1043, CSharpErrorIdentifiers.CS1043);
                return _CS1043;
            }
        }
        private static ICSharpCompilerReferenceError _CS1043;

        /// <summary><para>C&#9839; compiler error &#35;1044:</para><para>Cannot use more than one type in a for, using, fixed, or declaration statement</para></summary>
        public static ICSharpCompilerReferenceError CS1044
        {
            get
            {
                if (_CS1044 == null)
                    _CS1044 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1044, CSharpErrorIdentifiers.CS1044);
                return _CS1044;
            }
        }
        private static ICSharpCompilerReferenceError _CS1044;

        /// <summary><para>C&#9839; compiler error &#35;1055:</para><para>An add or remove accessor expected</para></summary>
        public static ICSharpCompilerReferenceError CS1055
        {
            get
            {
                if (_CS1055 == null)
                    _CS1055 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1055, CSharpErrorIdentifiers.CS1055);
                return _CS1055;
            }
        }
        private static ICSharpCompilerReferenceError _CS1055;

        /// <summary><para>C&#9839; compiler error &#35;1056:</para><para>Unexpected character 'character'</para></summary>
        public static ICSharpCompilerReferenceError CS1056
        {
            get
            {
                if (_CS1056 == null)
                    _CS1056 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1056, CSharpErrorIdentifiers.CS1056);
                return _CS1056;
            }
        }
        private static ICSharpCompilerReferenceError _CS1056;

        /// <summary><para>C&#9839; compiler error &#35;1057:</para><para>{0}: static classes cannot contain protected members</para></summary>
        public static ICSharpCompilerReferenceError CS1057
        {
            get
            {
                if (_CS1057 == null)
                    _CS1057 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1057, CSharpErrorIdentifiers.CS1057);
                return _CS1057;
            }
        }
        private static ICSharpCompilerReferenceError _CS1057;

        /// <summary><para>C&#9839; compiler error &#35;1059:</para><para>The operand of an increment or decrement operator must be a variable, property or indexer.</para></summary>
        public static ICSharpCompilerReferenceError CS1059
        {
            get
            {
                if (_CS1059 == null)
                    _CS1059 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1059, CSharpErrorIdentifiers.CS1059);
                return _CS1059;
            }
        }
        private static ICSharpCompilerReferenceError _CS1059;

        /// <summary><para>C&#9839; compiler error &#35;1061:</para><para>{0} does not contain a definition for {1} and no extension method {2} accepting a first argument of type {0} could be found (are you missing a using directive or an assembly reference?).</para></summary>
        public static ICSharpCompilerReferenceError CS1061
        {
            get
            {
                if (_CS1061 == null)
                    _CS1061 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1061, CSharpErrorIdentifiers.CS1061);
                return _CS1061;
            }
        }
        private static ICSharpCompilerReferenceError _CS1061;

        /// <summary><para>C&#9839; compiler error &#35;1100:</para><para>Method {0} has a parameter modifier 'this' which is not on the first parameter.</para></summary>
        public static ICSharpCompilerReferenceError CS1100
        {
            get
            {
                if (_CS1100 == null)
                    _CS1100 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1100, CSharpErrorIdentifiers.CS1100);
                return _CS1100;
            }
        }
        private static ICSharpCompilerReferenceError _CS1100;

        /// <summary><para>C&#9839; compiler error &#35;1101:</para><para>The parameter modifier 'ref' cannot be used with 'this'.</para></summary>
        public static ICSharpCompilerReferenceError CS1101
        {
            get
            {
                if (_CS1101 == null)
                    _CS1101 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1101, CSharpErrorIdentifiers.CS1101);
                return _CS1101;
            }
        }
        private static ICSharpCompilerReferenceError _CS1101;

        /// <summary><para>C&#9839; compiler error &#35;1102:</para><para>The parameter modifier 'out' cannot be used with 'this'.</para></summary>
        public static ICSharpCompilerReferenceError CS1102
        {
            get
            {
                if (_CS1102 == null)
                    _CS1102 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1102, CSharpErrorIdentifiers.CS1102);
                return _CS1102;
            }
        }
        private static ICSharpCompilerReferenceError _CS1102;

        /// <summary><para>C&#9839; compiler error &#35;1103:</para><para>The first parameter of an extension method cannot be of type {0}.</para></summary>
        public static ICSharpCompilerReferenceError CS1103
        {
            get
            {
                if (_CS1103 == null)
                    _CS1103 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1103, CSharpErrorIdentifiers.CS1103);
                return _CS1103;
            }
        }
        private static ICSharpCompilerReferenceError _CS1103;

        /// <summary><para>C&#9839; compiler error &#35;1104:</para><para>A parameter array cannot be used with 'this' modifier on an extension method.</para></summary>
        public static ICSharpCompilerReferenceError CS1104
        {
            get
            {
                if (_CS1104 == null)
                    _CS1104 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1104, CSharpErrorIdentifiers.CS1104);
                return _CS1104;
            }
        }
        private static ICSharpCompilerReferenceError _CS1104;

        /// <summary><para>C&#9839; compiler error &#35;1105:</para><para>Extension methods must be static.</para></summary>
        public static ICSharpCompilerReferenceError CS1105
        {
            get
            {
                if (_CS1105 == null)
                    _CS1105 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1105, CSharpErrorIdentifiers.CS1105);
                return _CS1105;
            }
        }
        private static ICSharpCompilerReferenceError _CS1105;

        /// <summary><para>C&#9839; compiler error &#35;1106:</para><para>Extension methods must be defined in a non generic static class.</para></summary>
        public static ICSharpCompilerReferenceError CS1106
        {
            get
            {
                if (_CS1106 == null)
                    _CS1106 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1106, CSharpErrorIdentifiers.CS1106);
                return _CS1106;
            }
        }
        private static ICSharpCompilerReferenceError _CS1106;

        /// <summary><para>C&#9839; compiler error &#35;1107:</para><para>A parameter can only have one {0} modifier.</para></summary>
        public static ICSharpCompilerReferenceError CS1107
        {
            get
            {
                if (_CS1107 == null)
                    _CS1107 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1107, CSharpErrorIdentifiers.CS1107);
                return _CS1107;
            }
        }
        private static ICSharpCompilerReferenceError _CS1107;

        /// <summary><para>C&#9839; compiler error &#35;1108:</para><para>A parameter cannot have all the specified modifiers; there are too many modifiers on the parameter.</para></summary>
        public static ICSharpCompilerReferenceError CS1108
        {
            get
            {
                if (_CS1108 == null)
                    _CS1108 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1108, CSharpErrorIdentifiers.CS1108);
                return _CS1108;
            }
        }
        private static ICSharpCompilerReferenceError _CS1108;

        /// <summary><para>C&#9839; compiler error &#35;1109:</para><para>Extension Methods must be defined on top level static classes, {0} is a nested class.</para></summary>
        public static ICSharpCompilerReferenceError CS1109
        {
            get
            {
                if (_CS1109 == null)
                    _CS1109 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1109, CSharpErrorIdentifiers.CS1109);
                return _CS1109;
            }
        }
        private static ICSharpCompilerReferenceError _CS1109;

        /// <summary><para>C&#9839; compiler error &#35;1110:</para><para>Cannot use 'this' modifier on first parameter of method declaration without a reference to System.Core.dll. Add a reference to System.Core.dll or remove 'this' modifier from the method declaration.</para></summary>
        public static ICSharpCompilerReferenceError CS1110
        {
            get
            {
                if (_CS1110 == null)
                    _CS1110 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1110, CSharpErrorIdentifiers.CS1110);
                return _CS1110;
            }
        }
        private static ICSharpCompilerReferenceError _CS1110;

        /// <summary><para>C&#9839; compiler error &#35;1112:</para><para>Do not use 'System.Runtime.CompilerServices.ExtensionAttribute'. Use the 'this' keyword instead.</para></summary>
        public static ICSharpCompilerReferenceError CS1112
        {
            get
            {
                if (_CS1112 == null)
                    _CS1112 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1112, CSharpErrorIdentifiers.CS1112);
                return _CS1112;
            }
        }
        private static ICSharpCompilerReferenceError _CS1112;

        /// <summary><para>C&#9839; compiler error &#35;1113:</para><para>Extension methods {0} defined on value type {1} cannot be used to create delegates.</para></summary>
        public static ICSharpCompilerReferenceError CS1113
        {
            get
            {
                if (_CS1113 == null)
                    _CS1113 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1113, CSharpErrorIdentifiers.CS1113);
                return _CS1113;
            }
        }
        private static ICSharpCompilerReferenceError _CS1113;

        /// <summary><para>C&#9839; compiler error &#35;1501:</para><para>No overload for method {0} takes {1} arguments</para></summary>
        public static ICSharpCompilerReferenceError CS1501
        {
            get
            {
                if (_CS1501 == null)
                    _CS1501 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1501, CSharpErrorIdentifiers.CS1501);
                return _CS1501;
            }
        }
        private static ICSharpCompilerReferenceError _CS1501;

        /// <summary><para>C&#9839; compiler error &#35;1502:</para><para>The best overloaded Add method {0} for the collection initializer has some invalid arguments</para></summary>
        public static ICSharpCompilerReferenceError CS1502
        {
            get
            {
                if (_CS1502 == null)
                    _CS1502 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1502, CSharpErrorIdentifiers.CS1502);
                return _CS1502;
            }
        }
        private static ICSharpCompilerReferenceError _CS1502;

        /// <summary><para>C&#9839; compiler error &#35;1503:</para><para>The best overloaded Add method {0} for the collection initializer has some invalid arguments</para></summary>
        public static ICSharpCompilerReferenceError CS1503
        {
            get
            {
                if (_CS1503 == null)
                    _CS1503 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1503, CSharpErrorIdentifiers.CS1503);
                return _CS1503;
            }
        }
        private static ICSharpCompilerReferenceError _CS1503;

        /// <summary><para>C&#9839; compiler error &#35;1504:</para><para>Source file {0} could not be opened ({1})</para></summary>
        public static ICSharpCompilerReferenceError CS1504
        {
            get
            {
                if (_CS1504 == null)
                    _CS1504 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1504, CSharpErrorIdentifiers.CS1504);
                return _CS1504;
            }
        }
        private static ICSharpCompilerReferenceError _CS1504;

        /// <summary><para>C&#9839; compiler error &#35;1507:</para><para>Cannot link resource file {0} when building a module</para></summary>
        public static ICSharpCompilerReferenceError CS1507
        {
            get
            {
                if (_CS1507 == null)
                    _CS1507 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1507, CSharpErrorIdentifiers.CS1507);
                return _CS1507;
            }
        }
        private static ICSharpCompilerReferenceError _CS1507;

        /// <summary><para>C&#9839; compiler error &#35;1508:</para><para>Resource identifier {0} has already been used in this assembly</para></summary>
        public static ICSharpCompilerReferenceError CS1508
        {
            get
            {
                if (_CS1508 == null)
                    _CS1508 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1508, CSharpErrorIdentifiers.CS1508);
                return _CS1508;
            }
        }
        private static ICSharpCompilerReferenceError _CS1508;

        /// <summary><para>C&#9839; compiler error &#35;1509:</para><para>Referenced file {0} is not an assembly; use '/addmodule' option instead</para></summary>
        public static ICSharpCompilerReferenceError CS1509
        {
            get
            {
                if (_CS1509 == null)
                    _CS1509 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1509, CSharpErrorIdentifiers.CS1509);
                return _CS1509;
            }
        }
        private static ICSharpCompilerReferenceError _CS1509;

        /// <summary><para>C&#9839; compiler error &#35;1510:</para><para>A ref or out argument must be an assignable variable</para></summary>
        public static ICSharpCompilerReferenceError CS1510
        {
            get
            {
                if (_CS1510 == null)
                    _CS1510 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1510, CSharpErrorIdentifiers.CS1510);
                return _CS1510;
            }
        }
        private static ICSharpCompilerReferenceError _CS1510;

        /// <summary><para>C&#9839; compiler error &#35;1511:</para><para>Keyword 'base' is not available in a static method</para></summary>
        public static ICSharpCompilerReferenceError CS1511
        {
            get
            {
                if (_CS1511 == null)
                    _CS1511 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1511, CSharpErrorIdentifiers.CS1511);
                return _CS1511;
            }
        }
        private static ICSharpCompilerReferenceError _CS1511;

        /// <summary><para>C&#9839; compiler error &#35;1512:</para><para>Keyword 'base' is not available in the current context</para></summary>
        public static ICSharpCompilerReferenceError CS1512
        {
            get
            {
                if (_CS1512 == null)
                    _CS1512 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1512, CSharpErrorIdentifiers.CS1512);
                return _CS1512;
            }
        }
        private static ICSharpCompilerReferenceError _CS1512;

        /// <summary><para>C&#9839; compiler error &#35;1513:</para><para>} expected</para></summary>
        public static ICSharpCompilerReferenceError CS1513
        {
            get
            {
                if (_CS1513 == null)
                    _CS1513 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1513, CSharpErrorIdentifiers.CS1513);
                return _CS1513;
            }
        }
        private static ICSharpCompilerReferenceError _CS1513;

        /// <summary><para>C&#9839; compiler error &#35;1514:</para><para>{ expected</para></summary>
        public static ICSharpCompilerReferenceError CS1514
        {
            get
            {
                if (_CS1514 == null)
                    _CS1514 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1514, CSharpErrorIdentifiers.CS1514);
                return _CS1514;
            }
        }
        private static ICSharpCompilerReferenceError _CS1514;

        /// <summary><para>C&#9839; compiler error &#35;1515:</para><para>'in' expected</para></summary>
        public static ICSharpCompilerReferenceError CS1515
        {
            get
            {
                if (_CS1515 == null)
                    _CS1515 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1515, CSharpErrorIdentifiers.CS1515);
                return _CS1515;
            }
        }
        private static ICSharpCompilerReferenceError _CS1515;

        /// <summary><para>C&#9839; compiler error &#35;1517:</para><para>Invalid preprocessor expression</para></summary>
        public static ICSharpCompilerReferenceError CS1517
        {
            get
            {
                if (_CS1517 == null)
                    _CS1517 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1517, CSharpErrorIdentifiers.CS1517);
                return _CS1517;
            }
        }
        private static ICSharpCompilerReferenceError _CS1517;

        /// <summary><para>C&#9839; compiler error &#35;1518:</para><para>Expected class, delegate, enum, interface, or struct</para></summary>
        public static ICSharpCompilerReferenceError CS1518
        {
            get
            {
                if (_CS1518 == null)
                    _CS1518 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1518, CSharpErrorIdentifiers.CS1518);
                return _CS1518;
            }
        }
        private static ICSharpCompilerReferenceError _CS1518;

        /// <summary><para>C&#9839; compiler error &#35;1519:</para><para>Invalid token {0} in class, struct, or interface member declaration</para></summary>
        public static ICSharpCompilerReferenceError CS1519
        {
            get
            {
                if (_CS1519 == null)
                    _CS1519 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1519, CSharpErrorIdentifiers.CS1519);
                return _CS1519;
            }
        }
        private static ICSharpCompilerReferenceError _CS1519;

        /// <summary><para>C&#9839; compiler error &#35;1520:</para><para>Method must have a return type</para></summary>
        public static ICSharpCompilerReferenceError CS1520
        {
            get
            {
                if (_CS1520 == null)
                    _CS1520 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1520, CSharpErrorIdentifiers.CS1520);
                return _CS1520;
            }
        }
        private static ICSharpCompilerReferenceError _CS1520;

        /// <summary><para>C&#9839; compiler error &#35;1521:</para><para>Invalid base type</para></summary>
        public static ICSharpCompilerReferenceError CS1521
        {
            get
            {
                if (_CS1521 == null)
                    _CS1521 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1521, CSharpErrorIdentifiers.CS1521);
                return _CS1521;
            }
        }
        private static ICSharpCompilerReferenceError _CS1521;

        /// <summary><para>C&#9839; compiler error &#35;1524:</para><para>Expected catch or finally</para></summary>
        public static ICSharpCompilerReferenceError CS1524
        {
            get
            {
                if (_CS1524 == null)
                    _CS1524 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1524, CSharpErrorIdentifiers.CS1524);
                return _CS1524;
            }
        }
        private static ICSharpCompilerReferenceError _CS1524;

        /// <summary><para>C&#9839; compiler error &#35;1525:</para><para>Invalid expression term {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1525
        {
            get
            {
                if (_CS1525 == null)
                    _CS1525 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1525, CSharpErrorIdentifiers.CS1525);
                return _CS1525;
            }
        }
        private static ICSharpCompilerReferenceError _CS1525;

        /// <summary><para>C&#9839; compiler error &#35;1526:</para><para>A new expression requires (), [], or {} after type</para></summary>
        public static ICSharpCompilerReferenceError CS1526
        {
            get
            {
                if (_CS1526 == null)
                    _CS1526 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1526, CSharpErrorIdentifiers.CS1526);
                return _CS1526;
            }
        }
        private static ICSharpCompilerReferenceError _CS1526;

        /// <summary><para>C&#9839; compiler error &#35;1527:</para><para>Elements defined in a namespace cannot be explicitly declared as private, protected, or protected internal</para></summary>
        public static ICSharpCompilerReferenceError CS1527
        {
            get
            {
                if (_CS1527 == null)
                    _CS1527 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1527, CSharpErrorIdentifiers.CS1527);
                return _CS1527;
            }
        }
        private static ICSharpCompilerReferenceError _CS1527;

        /// <summary><para>C&#9839; compiler error &#35;1528:</para><para>Expected ; or = (cannot specify constructor arguments in declaration)</para></summary>
        public static ICSharpCompilerReferenceError CS1528
        {
            get
            {
                if (_CS1528 == null)
                    _CS1528 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1528, CSharpErrorIdentifiers.CS1528);
                return _CS1528;
            }
        }
        private static ICSharpCompilerReferenceError _CS1528;

        /// <summary><para>C&#9839; compiler error &#35;1529:</para><para>A using clause must precede all other elements defined in the namespace except extern alias declarations</para></summary>
        public static ICSharpCompilerReferenceError CS1529
        {
            get
            {
                if (_CS1529 == null)
                    _CS1529 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1529, CSharpErrorIdentifiers.CS1529);
                return _CS1529;
            }
        }
        private static ICSharpCompilerReferenceError _CS1529;

        /// <summary><para>C&#9839; compiler error &#35;1530:</para><para>Keyword 'new' is not allowed on elements defined in a namespace</para></summary>
        public static ICSharpCompilerReferenceError CS1530
        {
            get
            {
                if (_CS1530 == null)
                    _CS1530 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1530, CSharpErrorIdentifiers.CS1530);
                return _CS1530;
            }
        }
        private static ICSharpCompilerReferenceError _CS1530;

        /// <summary><para>C&#9839; compiler error &#35;1534:</para><para>Overloaded binary operator {0} takes two parameters</para></summary>
        public static ICSharpCompilerReferenceError CS1534
        {
            get
            {
                if (_CS1534 == null)
                    _CS1534 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1534, CSharpErrorIdentifiers.CS1534);
                return _CS1534;
            }
        }
        private static ICSharpCompilerReferenceError _CS1534;

        /// <summary><para>C&#9839; compiler error &#35;1535:</para><para>Overloaded unary operator {0} takes one parameter</para></summary>
        public static ICSharpCompilerReferenceError CS1535
        {
            get
            {
                if (_CS1535 == null)
                    _CS1535 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1535, CSharpErrorIdentifiers.CS1535);
                return _CS1535;
            }
        }
        private static ICSharpCompilerReferenceError _CS1535;

        /// <summary><para>C&#9839; compiler error &#35;1536:</para><para>Invalid parameter type void</para></summary>
        public static ICSharpCompilerReferenceError CS1536
        {
            get
            {
                if (_CS1536 == null)
                    _CS1536 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1536, CSharpErrorIdentifiers.CS1536);
                return _CS1536;
            }
        }
        private static ICSharpCompilerReferenceError _CS1536;

        /// <summary><para>C&#9839; compiler error &#35;1537:</para><para>The using alias {0} appeared previously in this namespace</para></summary>
        public static ICSharpCompilerReferenceError CS1537
        {
            get
            {
                if (_CS1537 == null)
                    _CS1537 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1537, CSharpErrorIdentifiers.CS1537);
                return _CS1537;
            }
        }
        private static ICSharpCompilerReferenceError _CS1537;

        /// <summary><para>C&#9839; compiler error &#35;1540:</para><para>Cannot access protected member {0} via a qualifier of type {1}; the qualifier must be of type {2} (or derived from it)</para></summary>
        public static ICSharpCompilerReferenceError CS1540
        {
            get
            {
                if (_CS1540 == null)
                    _CS1540 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1540, CSharpErrorIdentifiers.CS1540);
                return _CS1540;
            }
        }
        private static ICSharpCompilerReferenceError _CS1540;

        /// <summary><para>C&#9839; compiler error &#35;1541:</para><para>Invalid reference option: {0} — cannot reference directories</para></summary>
        public static ICSharpCompilerReferenceError CS1541
        {
            get
            {
                if (_CS1541 == null)
                    _CS1541 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1541, CSharpErrorIdentifiers.CS1541);
                return _CS1541;
            }
        }
        private static ICSharpCompilerReferenceError _CS1541;

        /// <summary><para>C&#9839; compiler error &#35;1542:</para><para>{0} cannot be added to this assembly because it already is an assembly; use '/R' option instead</para></summary>
        public static ICSharpCompilerReferenceError CS1542
        {
            get
            {
                if (_CS1542 == null)
                    _CS1542 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1542, CSharpErrorIdentifiers.CS1542);
                return _CS1542;
            }
        }
        private static ICSharpCompilerReferenceError _CS1542;

        /// <summary><para>C&#9839; compiler error &#35;1545:</para><para>Property, indexer, or event {0} is not supported by the language; try directly calling accessor methods {1} or {2}</para></summary>
        public static ICSharpCompilerReferenceError CS1545
        {
            get
            {
                if (_CS1545 == null)
                    _CS1545 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1545, CSharpErrorIdentifiers.CS1545);
                return _CS1545;
            }
        }
        private static ICSharpCompilerReferenceError _CS1545;

        /// <summary><para>C&#9839; compiler error &#35;1546:</para><para>Property, indexer, or event {0} is not supported by the language; try directly calling accessor method {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1546
        {
            get
            {
                if (_CS1546 == null)
                    _CS1546 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1546, CSharpErrorIdentifiers.CS1546);
                return _CS1546;
            }
        }
        private static ICSharpCompilerReferenceError _CS1546;

        /// <summary><para>C&#9839; compiler error &#35;1547:</para><para>Keyword 'void' cannot be used in this context</para></summary>
        public static ICSharpCompilerReferenceError CS1547
        {
            get
            {
                if (_CS1547 == null)
                    _CS1547 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1547, CSharpErrorIdentifiers.CS1547);
                return _CS1547;
            }
        }
        private static ICSharpCompilerReferenceError _CS1547;

        /// <summary><para>C&#9839; compiler error &#35;1548:</para><para>Cryptographic failure while signing assembly {0} — {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1548
        {
            get
            {
                if (_CS1548 == null)
                    _CS1548 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1548, CSharpErrorIdentifiers.CS1548);
                return _CS1548;
            }
        }
        private static ICSharpCompilerReferenceError _CS1548;

        /// <summary><para>C&#9839; compiler error &#35;1549:</para><para>Appropriate cryptographic service not found</para></summary>
        public static ICSharpCompilerReferenceError CS1549
        {
            get
            {
                if (_CS1549 == null)
                    _CS1549 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1549, CSharpErrorIdentifiers.CS1549);
                return _CS1549;
            }
        }
        private static ICSharpCompilerReferenceError _CS1549;

        /// <summary><para>C&#9839; compiler error &#35;1551:</para><para>Indexers must have at least one parameter</para></summary>
        public static ICSharpCompilerReferenceError CS1551
        {
            get
            {
                if (_CS1551 == null)
                    _CS1551 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1551, CSharpErrorIdentifiers.CS1551);
                return _CS1551;
            }
        }
        private static ICSharpCompilerReferenceError _CS1551;

        /// <summary><para>C&#9839; compiler error &#35;1552:</para><para>Array type specifier, [], must appear before parameter name</para></summary>
        public static ICSharpCompilerReferenceError CS1552
        {
            get
            {
                if (_CS1552 == null)
                    _CS1552 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1552, CSharpErrorIdentifiers.CS1552);
                return _CS1552;
            }
        }
        private static ICSharpCompilerReferenceError _CS1552;

        /// <summary><para>C&#9839; compiler error &#35;1553:</para><para>Declaration is not valid; use '{0} operator {1} (...' instead</para></summary>
        public static ICSharpCompilerReferenceError CS1553
        {
            get
            {
                if (_CS1553 == null)
                    _CS1553 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1553, CSharpErrorIdentifiers.CS1553);
                return _CS1553;
            }
        }
        private static ICSharpCompilerReferenceError _CS1553;

        /// <summary><para>C&#9839; compiler error &#35;1554:</para><para>Declaration is not valid; use '{0} operator {1} (...' instead</para></summary>
        public static ICSharpCompilerReferenceError CS1554
        {
            get
            {
                if (_CS1554 == null)
                    _CS1554 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1554, CSharpErrorIdentifiers.CS1554);
                return _CS1554;
            }
        }
        private static ICSharpCompilerReferenceError _CS1554;

        /// <summary><para>C&#9839; compiler error &#35;1555:</para><para>Could not find {0} specified for Main method</para></summary>
        public static ICSharpCompilerReferenceError CS1555
        {
            get
            {
                if (_CS1555 == null)
                    _CS1555 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1555, CSharpErrorIdentifiers.CS1555);
                return _CS1555;
            }
        }
        private static ICSharpCompilerReferenceError _CS1555;

        /// <summary><para>C&#9839; compiler error &#35;1556:</para><para>{0} specified for Main method must be a valid class or struct</para></summary>
        public static ICSharpCompilerReferenceError CS1556
        {
            get
            {
                if (_CS1556 == null)
                    _CS1556 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1556, CSharpErrorIdentifiers.CS1556);
                return _CS1556;
            }
        }
        private static ICSharpCompilerReferenceError _CS1556;

        /// <summary><para>C&#9839; compiler error &#35;1557:</para><para>Cannot use {0} for Main method because it is in a different output file</para></summary>
        public static ICSharpCompilerReferenceError CS1557
        {
            get
            {
                if (_CS1557 == null)
                    _CS1557 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1557, CSharpErrorIdentifiers.CS1557);
                return _CS1557;
            }
        }
        private static ICSharpCompilerReferenceError _CS1557;

        /// <summary><para>C&#9839; compiler error &#35;1558:</para><para>{0} does not have a suitable static Main method</para></summary>
        public static ICSharpCompilerReferenceError CS1558
        {
            get
            {
                if (_CS1558 == null)
                    _CS1558 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1558, CSharpErrorIdentifiers.CS1558);
                return _CS1558;
            }
        }
        private static ICSharpCompilerReferenceError _CS1558;

        /// <summary><para>C&#9839; compiler error &#35;1559:</para><para>Cannot use {0} for Main method because it is imported</para></summary>
        public static ICSharpCompilerReferenceError CS1559
        {
            get
            {
                if (_CS1559 == null)
                    _CS1559 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1559, CSharpErrorIdentifiers.CS1559);
                return _CS1559;
            }
        }
        private static ICSharpCompilerReferenceError _CS1559;

        /// <summary><para>C&#9839; compiler error &#35;1560:</para><para>Invalid filename specified for preprocessor directive. Filename is too long or not a valid filename</para></summary>
        public static ICSharpCompilerReferenceError CS1560
        {
            get
            {
                if (_CS1560 == null)
                    _CS1560 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1560, CSharpErrorIdentifiers.CS1560);
                return _CS1560;
            }
        }
        private static ICSharpCompilerReferenceError _CS1560;

        /// <summary><para>C&#9839; compiler error &#35;1561:</para><para>Output filename is too long or invalid</para></summary>
        public static ICSharpCompilerReferenceError CS1561
        {
            get
            {
                if (_CS1561 == null)
                    _CS1561 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1561, CSharpErrorIdentifiers.CS1561);
                return _CS1561;
            }
        }
        private static ICSharpCompilerReferenceError _CS1561;

        /// <summary><para>C&#9839; compiler error &#35;1562:</para><para>Outputs without source must have the /out option specified</para></summary>
        public static ICSharpCompilerReferenceError CS1562
        {
            get
            {
                if (_CS1562 == null)
                    _CS1562 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1562, CSharpErrorIdentifiers.CS1562);
                return _CS1562;
            }
        }
        private static ICSharpCompilerReferenceError _CS1562;

        /// <summary><para>C&#9839; compiler error &#35;1563:</para><para>Output {0} does not have any source files</para></summary>
        public static ICSharpCompilerReferenceError CS1563
        {
            get
            {
                if (_CS1563 == null)
                    _CS1563 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1563, CSharpErrorIdentifiers.CS1563);
                return _CS1563;
            }
        }
        private static ICSharpCompilerReferenceError _CS1563;

        /// <summary><para>C&#9839; compiler error &#35;1564:</para><para>Conflicting options specified: Win32 resource file; Win32 manifest.</para></summary>
        public static ICSharpCompilerReferenceError CS1564
        {
            get
            {
                if (_CS1564 == null)
                    _CS1564 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1564, CSharpErrorIdentifiers.CS1564);
                return _CS1564;
            }
        }
        private static ICSharpCompilerReferenceError _CS1564;

        /// <summary><para>C&#9839; compiler error &#35;1565:</para><para>Conflicting options specified: Win32 resource file; Win32 icon</para></summary>
        public static ICSharpCompilerReferenceError CS1565
        {
            get
            {
                if (_CS1565 == null)
                    _CS1565 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1565, CSharpErrorIdentifiers.CS1565);
                return _CS1565;
            }
        }
        private static ICSharpCompilerReferenceError _CS1565;

        /// <summary><para>C&#9839; compiler error &#35;1566:</para><para>SourceError reading resource file {0} — {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1566
        {
            get
            {
                if (_CS1566 == null)
                    _CS1566 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1566, CSharpErrorIdentifiers.CS1566);
                return _CS1566;
            }
        }
        private static ICSharpCompilerReferenceError _CS1566;

        /// <summary><para>C&#9839; compiler error &#35;1567:</para><para>SourceError generating Win32 resource: {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1567
        {
            get
            {
                if (_CS1567 == null)
                    _CS1567 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1567, CSharpErrorIdentifiers.CS1567);
                return _CS1567;
            }
        }
        private static ICSharpCompilerReferenceError _CS1567;

        /// <summary><para>C&#9839; compiler error &#35;1569:</para><para>SourceError generating XML documentation file {0} ({1})</para></summary>
        public static ICSharpCompilerReferenceError CS1569
        {
            get
            {
                if (_CS1569 == null)
                    _CS1569 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1569, CSharpErrorIdentifiers.CS1569);
                return _CS1569;
            }
        }
        private static ICSharpCompilerReferenceError _CS1569;

        /// <summary><para>C&#9839; compiler error &#35;1575:</para><para>A stackalloc expression requires [] after type</para></summary>
        public static ICSharpCompilerReferenceError CS1575
        {
            get
            {
                if (_CS1575 == null)
                    _CS1575 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1575, CSharpErrorIdentifiers.CS1575);
                return _CS1575;
            }
        }
        private static ICSharpCompilerReferenceError _CS1575;

        /// <summary><para>C&#9839; compiler error &#35;1576:</para><para>The line number specified for #line directive is missing or invalid</para></summary>
        public static ICSharpCompilerReferenceError CS1576
        {
            get
            {
                if (_CS1576 == null)
                    _CS1576 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1576, CSharpErrorIdentifiers.CS1576);
                return _CS1576;
            }
        }
        private static ICSharpCompilerReferenceError _CS1576;

        /// <summary><para>C&#9839; compiler error &#35;1577:</para><para>Assembly generation failed — reason</para></summary>
        public static ICSharpCompilerReferenceError CS1577
        {
            get
            {
                if (_CS1577 == null)
                    _CS1577 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1577, CSharpErrorIdentifiers.CS1577);
                return _CS1577;
            }
        }
        private static ICSharpCompilerReferenceError _CS1577;

        /// <summary><para>C&#9839; compiler error &#35;1578:</para><para>Filename, single-line comment or end-of-line expected</para></summary>
        public static ICSharpCompilerReferenceError CS1578
        {
            get
            {
                if (_CS1578 == null)
                    _CS1578 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1578, CSharpErrorIdentifiers.CS1578);
                return _CS1578;
            }
        }
        private static ICSharpCompilerReferenceError _CS1578;

        /// <summary><para>C&#9839; compiler error &#35;1579:</para><para>foreach statement cannot operate on variables of type {0} because {1} does not contain a public definition for {2}</para></summary>
        public static ICSharpCompilerReferenceError CS1579
        {
            get
            {
                if (_CS1579 == null)
                    _CS1579 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1579, CSharpErrorIdentifiers.CS1579);
                return _CS1579;
            }
        }
        private static ICSharpCompilerReferenceError _CS1579;

        /// <summary><para>C&#9839; compiler error &#35;1583:</para><para>{0} is not a valid Win32 resource file</para></summary>
        public static ICSharpCompilerReferenceError CS1583
        {
            get
            {
                if (_CS1583 == null)
                    _CS1583 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1583, CSharpErrorIdentifiers.CS1583);
                return _CS1583;
            }
        }
        private static ICSharpCompilerReferenceError _CS1583;

        /// <summary><para>C&#9839; compiler error &#35;1585:</para><para>Member modifier {0} must precede the member type and name</para></summary>
        public static ICSharpCompilerReferenceError CS1585
        {
            get
            {
                if (_CS1585 == null)
                    _CS1585 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1585, CSharpErrorIdentifiers.CS1585);
                return _CS1585;
            }
        }
        private static ICSharpCompilerReferenceError _CS1585;

        /// <summary><para>C&#9839; compiler error &#35;1586:</para><para>Array creation must have array size or array initializer</para></summary>
        public static ICSharpCompilerReferenceError CS1586
        {
            get
            {
                if (_CS1586 == null)
                    _CS1586 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1586, CSharpErrorIdentifiers.CS1586);
                return _CS1586;
            }
        }
        private static ICSharpCompilerReferenceError _CS1586;

        /// <summary><para>C&#9839; compiler error &#35;1588:</para><para>Cannot determine common language runtime directory -- {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1588
        {
            get
            {
                if (_CS1588 == null)
                    _CS1588 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1588, CSharpErrorIdentifiers.CS1588);
                return _CS1588;
            }
        }
        private static ICSharpCompilerReferenceError _CS1588;

        /// <summary><para>C&#9839; compiler error &#35;1593:</para><para>Delegate {0} does not take {1} arguments</para></summary>
        public static ICSharpCompilerReferenceError CS1593
        {
            get
            {
                if (_CS1593 == null)
                    _CS1593 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1593, CSharpErrorIdentifiers.CS1593);
                return _CS1593;
            }
        }
        private static ICSharpCompilerReferenceError _CS1593;

        /// <summary><para>C&#9839; compiler error &#35;1594:</para><para>Delegate {0} has some invalid arguments</para></summary>
        public static ICSharpCompilerReferenceError CS1594
        {
            get
            {
                if (_CS1594 == null)
                    _CS1594 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1594, CSharpErrorIdentifiers.CS1594);
                return _CS1594;
            }
        }
        private static ICSharpCompilerReferenceError _CS1594;

        /// <summary><para>C&#9839; compiler error &#35;1597:</para><para>Semicolon after method or accessor block is not valid</para></summary>
        public static ICSharpCompilerReferenceError CS1597
        {
            get
            {
                if (_CS1597 == null)
                    _CS1597 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1597, CSharpErrorIdentifiers.CS1597);
                return _CS1597;
            }
        }
        private static ICSharpCompilerReferenceError _CS1597;

        /// <summary><para>C&#9839; compiler error &#35;1599:</para><para>Method or delegate cannot return type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1599
        {
            get
            {
                if (_CS1599 == null)
                    _CS1599 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1599, CSharpErrorIdentifiers.CS1599);
                return _CS1599;
            }
        }
        private static ICSharpCompilerReferenceError _CS1599;

        /// <summary><para>C&#9839; compiler error &#35;1600:</para><para>Compilation cancelled by user</para></summary>
        public static ICSharpCompilerReferenceError CS1600
        {
            get
            {
                if (_CS1600 == null)
                    _CS1600 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1600, CSharpErrorIdentifiers.CS1600);
                return _CS1600;
            }
        }
        private static ICSharpCompilerReferenceError _CS1600;

        /// <summary><para>C&#9839; compiler error &#35;1601:</para><para>Method or delegate parameter cannot be of type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1601
        {
            get
            {
                if (_CS1601 == null)
                    _CS1601 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1601, CSharpErrorIdentifiers.CS1601);
                return _CS1601;
            }
        }
        private static ICSharpCompilerReferenceError _CS1601;

        /// <summary><para>C&#9839; compiler error &#35;1604:</para><para>Cannot assign to {0} because it is read-only</para></summary>
        public static ICSharpCompilerReferenceError CS1604
        {
            get
            {
                if (_CS1604 == null)
                    _CS1604 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1604, CSharpErrorIdentifiers.CS1604);
                return _CS1604;
            }
        }
        private static ICSharpCompilerReferenceError _CS1604;

        /// <summary><para>C&#9839; compiler error &#35;1605:</para><para>Cannot pass {0} as a ref or out argument because it is read-only</para></summary>
        public static ICSharpCompilerReferenceError CS1605
        {
            get
            {
                if (_CS1605 == null)
                    _CS1605 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1605, CSharpErrorIdentifiers.CS1605);
                return _CS1605;
            }
        }
        private static ICSharpCompilerReferenceError _CS1605;

        /// <summary><para>C&#9839; compiler error &#35;1606:</para><para>Assembly signing failed; output may not be signed -- {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1606
        {
            get
            {
                if (_CS1606 == null)
                    _CS1606 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1606, CSharpErrorIdentifiers.CS1606);
                return _CS1606;
            }
        }
        private static ICSharpCompilerReferenceError _CS1606;

        /// <summary><para>C&#9839; compiler error &#35;1608:</para><para>The Required attribute is not permitted on C# types</para></summary>
        public static ICSharpCompilerReferenceError CS1608
        {
            get
            {
                if (_CS1608 == null)
                    _CS1608 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1608, CSharpErrorIdentifiers.CS1608);
                return _CS1608;
            }
        }
        private static ICSharpCompilerReferenceError _CS1608;

        /// <summary><para>C&#9839; compiler error &#35;1609:</para><para>Modifiers cannot be placed on event accessor declarations</para></summary>
        public static ICSharpCompilerReferenceError CS1609
        {
            get
            {
                if (_CS1609 == null)
                    _CS1609 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1609, CSharpErrorIdentifiers.CS1609);
                return _CS1609;
            }
        }
        private static ICSharpCompilerReferenceError _CS1609;

        /// <summary><para>C&#9839; compiler error &#35;1611:</para><para>The params parameter cannot be declared as ref or out</para></summary>
        public static ICSharpCompilerReferenceError CS1611
        {
            get
            {
                if (_CS1611 == null)
                    _CS1611 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1611, CSharpErrorIdentifiers.CS1611);
                return _CS1611;
            }
        }
        private static ICSharpCompilerReferenceError _CS1611;

        /// <summary><para>C&#9839; compiler error &#35;1612:</para><para>Cannot modify the return value of {0} because it is not a variable</para></summary>
        public static ICSharpCompilerReferenceError CS1612
        {
            get
            {
                if (_CS1612 == null)
                    _CS1612 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1612, CSharpErrorIdentifiers.CS1612);
                return _CS1612;
            }
        }
        private static ICSharpCompilerReferenceError _CS1612;

        /// <summary><para>C&#9839; compiler error &#35;1613:</para><para>The managed coclass wrapper class {0} for interface {1} cannot be found (are you missing an assembly reference?)</para></summary>
        public static ICSharpCompilerReferenceError CS1613
        {
            get
            {
                if (_CS1613 == null)
                    _CS1613 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1613, CSharpErrorIdentifiers.CS1613);
                return _CS1613;
            }
        }
        private static ICSharpCompilerReferenceError _CS1613;

        /// <summary><para>C&#9839; compiler error &#35;1614:</para><para>{0} is ambiguous; between {1} and {2}. use either '@{0}' or '{0}Attribute'</para></summary>
        public static ICSharpCompilerReferenceError CS1614
        {
            get
            {
                if (_CS1614 == null)
                    _CS1614 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1614, CSharpErrorIdentifiers.CS1614);
                return _CS1614;
            }
        }
        private static ICSharpCompilerReferenceError _CS1614;

        /// <summary><para>C&#9839; compiler error &#35;1615:</para><para>Argument {0} should not be passed with the {1} keyword</para></summary>
        public static ICSharpCompilerReferenceError CS1615
        {
            get
            {
                if (_CS1615 == null)
                    _CS1615 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1615, CSharpErrorIdentifiers.CS1615);
                return _CS1615;
            }
        }
        private static ICSharpCompilerReferenceError _CS1615;

        /// <summary><para>C&#9839; compiler error &#35;1617:</para><para>Invalid option {0} for /langversion; must be ISO-1, ISO-2 or Default</para></summary>
        public static ICSharpCompilerReferenceError CS1617
        {
            get
            {
                if (_CS1617 == null)
                    _CS1617 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1617, CSharpErrorIdentifiers.CS1617);
                return _CS1617;
            }
        }
        private static ICSharpCompilerReferenceError _CS1617;

        /// <summary><para>C&#9839; compiler error &#35;1618:</para><para>Cannot create delegate with {0} because it has a Conditional attribute</para></summary>
        public static ICSharpCompilerReferenceError CS1618
        {
            get
            {
                if (_CS1618 == null)
                    _CS1618 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1618, CSharpErrorIdentifiers.CS1618);
                return _CS1618;
            }
        }
        private static ICSharpCompilerReferenceError _CS1618;

        /// <summary><para>C&#9839; compiler error &#35;1619:</para><para>Cannot create temporary file {0} -- {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1619
        {
            get
            {
                if (_CS1619 == null)
                    _CS1619 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1619, CSharpErrorIdentifiers.CS1619);
                return _CS1619;
            }
        }
        private static ICSharpCompilerReferenceError _CS1619;

        /// <summary><para>C&#9839; compiler error &#35;1620:</para><para>Argument {0} must be passed with the {1} keyword</para></summary>
        public static ICSharpCompilerReferenceError CS1620
        {
            get
            {
                if (_CS1620 == null)
                    _CS1620 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1620, CSharpErrorIdentifiers.CS1620);
                return _CS1620;
            }
        }
        private static ICSharpCompilerReferenceError _CS1620;

        /// <summary><para>C&#9839; compiler error &#35;1621:</para><para>The yield statement cannot be used inside an anonymous method or lambda expression</para></summary>
        public static ICSharpCompilerReferenceError CS1621
        {
            get
            {
                if (_CS1621 == null)
                    _CS1621 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1621, CSharpErrorIdentifiers.CS1621);
                return _CS1621;
            }
        }
        private static ICSharpCompilerReferenceError _CS1621;

        /// <summary><para>C&#9839; compiler error &#35;1622:</para><para>Cannot return a value from an iterator. Use the yield return statement to return a value, or yield break to end the iteration.</para></summary>
        public static ICSharpCompilerReferenceError CS1622
        {
            get
            {
                if (_CS1622 == null)
                    _CS1622 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1622, CSharpErrorIdentifiers.CS1622);
                return _CS1622;
            }
        }
        private static ICSharpCompilerReferenceError _CS1622;

        /// <summary><para>C&#9839; compiler error &#35;1623:</para><para>Iterators cannot have ref or out parameters</para></summary>
        public static ICSharpCompilerReferenceError CS1623
        {
            get
            {
                if (_CS1623 == null)
                    _CS1623 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1623, CSharpErrorIdentifiers.CS1623);
                return _CS1623;
            }
        }
        private static ICSharpCompilerReferenceError _CS1623;

        /// <summary><para>C&#9839; compiler error &#35;1624:</para><para>The body of {0} cannot be an iterator block because {1} is not an iterator interface type</para></summary>
        public static ICSharpCompilerReferenceError CS1624
        {
            get
            {
                if (_CS1624 == null)
                    _CS1624 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1624, CSharpErrorIdentifiers.CS1624);
                return _CS1624;
            }
        }
        private static ICSharpCompilerReferenceError _CS1624;

        /// <summary><para>C&#9839; compiler error &#35;1625:</para><para>Cannot yield in the body of a finally clause</para></summary>
        public static ICSharpCompilerReferenceError CS1625
        {
            get
            {
                if (_CS1625 == null)
                    _CS1625 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1625, CSharpErrorIdentifiers.CS1625);
                return _CS1625;
            }
        }
        private static ICSharpCompilerReferenceError _CS1625;

        /// <summary><para>C&#9839; compiler error &#35;1626:</para><para>Cannot yield a value in the body of a try block with a catch clause</para></summary>
        public static ICSharpCompilerReferenceError CS1626
        {
            get
            {
                if (_CS1626 == null)
                    _CS1626 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1626, CSharpErrorIdentifiers.CS1626);
                return _CS1626;
            }
        }
        private static ICSharpCompilerReferenceError _CS1626;

        /// <summary><para>C&#9839; compiler error &#35;1627:</para><para>Expression expected after yield return</para></summary>
        public static ICSharpCompilerReferenceError CS1627
        {
            get
            {
                if (_CS1627 == null)
                    _CS1627 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1627, CSharpErrorIdentifiers.CS1627);
                return _CS1627;
            }
        }
        private static ICSharpCompilerReferenceError _CS1627;

        /// <summary><para>C&#9839; compiler error &#35;1628:</para><para>Cannot use ref or out parameter {0} inside an anonymous method, lambda expression, or query expression</para></summary>
        public static ICSharpCompilerReferenceError CS1628
        {
            get
            {
                if (_CS1628 == null)
                    _CS1628 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1628, CSharpErrorIdentifiers.CS1628);
                return _CS1628;
            }
        }
        private static ICSharpCompilerReferenceError _CS1628;

        /// <summary><para>C&#9839; compiler error &#35;1629:</para><para>Unsafe code may not appear in iterators</para></summary>
        public static ICSharpCompilerReferenceError CS1629
        {
            get
            {
                if (_CS1629 == null)
                    _CS1629 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1629, CSharpErrorIdentifiers.CS1629);
                return _CS1629;
            }
        }
        private static ICSharpCompilerReferenceError _CS1629;

        /// <summary><para>C&#9839; compiler error &#35;1630:</para><para>Invalid option {0} for /errorreport; must be prompt, send, queue, or none</para></summary>
        public static ICSharpCompilerReferenceError CS1630
        {
            get
            {
                if (_CS1630 == null)
                    _CS1630 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1630, CSharpErrorIdentifiers.CS1630);
                return _CS1630;
            }
        }
        private static ICSharpCompilerReferenceError _CS1630;

        /// <summary><para>C&#9839; compiler error &#35;1631:</para><para>Cannot yield a value in the body of a catch clause</para></summary>
        public static ICSharpCompilerReferenceError CS1631
        {
            get
            {
                if (_CS1631 == null)
                    _CS1631 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1631, CSharpErrorIdentifiers.CS1631);
                return _CS1631;
            }
        }
        private static ICSharpCompilerReferenceError _CS1631;

        /// <summary><para>C&#9839; compiler error &#35;1632:</para><para>Control cannot leave the body of an anonymous method or lambda expression</para></summary>
        public static ICSharpCompilerReferenceError CS1632
        {
            get
            {
                if (_CS1632 == null)
                    _CS1632 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1632, CSharpErrorIdentifiers.CS1632);
                return _CS1632;
            }
        }
        private static ICSharpCompilerReferenceError _CS1632;

        /// <summary><para>C&#9839; compiler error &#35;1637:</para><para>Iterators cannot have unsafe parameters or yield types</para></summary>
        public static ICSharpCompilerReferenceError CS1637
        {
            get
            {
                if (_CS1637 == null)
                    _CS1637 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1637, CSharpErrorIdentifiers.CS1637);
                return _CS1637;
            }
        }
        private static ICSharpCompilerReferenceError _CS1637;

        /// <summary><para>C&#9839; compiler error &#35;1638:</para><para>{0} is a reserved identifier and cannot be used when ISO language version mode is used</para></summary>
        public static ICSharpCompilerReferenceError CS1638
        {
            get
            {
                if (_CS1638 == null)
                    _CS1638 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1638, CSharpErrorIdentifiers.CS1638);
                return _CS1638;
            }
        }
        private static ICSharpCompilerReferenceError _CS1638;

        /// <summary><para>C&#9839; compiler error &#35;1639:</para><para>The managed coclass wrapper class signature {0} for interface {1} is not a valid class name signature</para></summary>
        public static ICSharpCompilerReferenceError CS1639
        {
            get
            {
                if (_CS1639 == null)
                    _CS1639 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1639, CSharpErrorIdentifiers.CS1639);
                return _CS1639;
            }
        }
        private static ICSharpCompilerReferenceError _CS1639;

        /// <summary><para>C&#9839; compiler error &#35;1640:</para><para>foreach statement cannot operate on variables of type {0} because it implements multiple instantiations of {1}, try casting to a specific interface instantiation</para></summary>
        public static ICSharpCompilerReferenceError CS1640
        {
            get
            {
                if (_CS1640 == null)
                    _CS1640 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1640, CSharpErrorIdentifiers.CS1640);
                return _CS1640;
            }
        }
        private static ICSharpCompilerReferenceError _CS1640;

        /// <summary><para>C&#9839; compiler error &#35;1641:</para><para>A fixed size buffer field must have the array size specifier after the field name</para></summary>
        public static ICSharpCompilerReferenceError CS1641
        {
            get
            {
                if (_CS1641 == null)
                    _CS1641 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1641, CSharpErrorIdentifiers.CS1641);
                return _CS1641;
            }
        }
        private static ICSharpCompilerReferenceError _CS1641;

        /// <summary><para>C&#9839; compiler error &#35;1642:</para><para>Fixed size buffer fields may only be members of structs.</para></summary>
        public static ICSharpCompilerReferenceError CS1642
        {
            get
            {
                if (_CS1642 == null)
                    _CS1642 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1642, CSharpErrorIdentifiers.CS1642);
                return _CS1642;
            }
        }
        private static ICSharpCompilerReferenceError _CS1642;

        /// <summary><para>C&#9839; compiler error &#35;1643:</para><para>Not all code paths return a value in method of type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1643
        {
            get
            {
                if (_CS1643 == null)
                    _CS1643 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1643, CSharpErrorIdentifiers.CS1643);
                return _CS1643;
            }
        }
        private static ICSharpCompilerReferenceError _CS1643;

        /// <summary><para>C&#9839; compiler error &#35;1644:</para><para>Feature {0} is not part of the standardized ISO C# language specification, and may not be accepted by other compilers</para></summary>
        public static ICSharpCompilerReferenceError CS1644
        {
            get
            {
                if (_CS1644 == null)
                    _CS1644 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1644, CSharpErrorIdentifiers.CS1644);
                return _CS1644;
            }
        }
        private static ICSharpCompilerReferenceError _CS1644;

        /// <summary><para>C&#9839; compiler error &#35;1646:</para><para>Keyword, identifier, or string expected after verbatim specifier: @</para></summary>
        public static ICSharpCompilerReferenceError CS1646
        {
            get
            {
                if (_CS1646 == null)
                    _CS1646 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1646, CSharpErrorIdentifiers.CS1646);
                return _CS1646;
            }
        }
        private static ICSharpCompilerReferenceError _CS1646;

        /// <summary><para>C&#9839; compiler error &#35;1647:</para><para>An expression is too long or complex to compile near {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1647
        {
            get
            {
                if (_CS1647 == null)
                    _CS1647 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1647, CSharpErrorIdentifiers.CS1647);
                return _CS1647;
            }
        }
        private static ICSharpCompilerReferenceError _CS1647;

        /// <summary><para>C&#9839; compiler error &#35;1648:</para><para>Members of readonly field {0} cannot be modified (except in a constructor or a variable initializer)</para></summary>
        public static ICSharpCompilerReferenceError CS1648
        {
            get
            {
                if (_CS1648 == null)
                    _CS1648 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1648, CSharpErrorIdentifiers.CS1648);
                return _CS1648;
            }
        }
        private static ICSharpCompilerReferenceError _CS1648;

        /// <summary><para>C&#9839; compiler error &#35;1649:</para><para>Members of readonly field {0} cannot be passed ref or out (except in a constructor)</para></summary>
        public static ICSharpCompilerReferenceError CS1649
        {
            get
            {
                if (_CS1649 == null)
                    _CS1649 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1649, CSharpErrorIdentifiers.CS1649);
                return _CS1649;
            }
        }
        private static ICSharpCompilerReferenceError _CS1649;

        /// <summary><para>C&#9839; compiler error &#35;1650:</para><para>Fields of static readonly field {0} cannot be assigned to (except in a static constructor or a variable initializer)</para></summary>
        public static ICSharpCompilerReferenceError CS1650
        {
            get
            {
                if (_CS1650 == null)
                    _CS1650 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1650, CSharpErrorIdentifiers.CS1650);
                return _CS1650;
            }
        }
        private static ICSharpCompilerReferenceError _CS1650;

        /// <summary><para>C&#9839; compiler error &#35;1651:</para><para>Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)</para></summary>
        public static ICSharpCompilerReferenceError CS1651
        {
            get
            {
                if (_CS1651 == null)
                    _CS1651 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1651, CSharpErrorIdentifiers.CS1651);
                return _CS1651;
            }
        }
        private static ICSharpCompilerReferenceError _CS1651;

        /// <summary><para>C&#9839; compiler error &#35;1654:</para><para>Cannot modify members of {0} because it is a {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1654
        {
            get
            {
                if (_CS1654 == null)
                    _CS1654 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1654, CSharpErrorIdentifiers.CS1654);
                return _CS1654;
            }
        }
        private static ICSharpCompilerReferenceError _CS1654;

        /// <summary><para>C&#9839; compiler error &#35;1655:</para><para>Cannot pass fields of {0} as a ref or out argument because it is a {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1655
        {
            get
            {
                if (_CS1655 == null)
                    _CS1655 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1655, CSharpErrorIdentifiers.CS1655);
                return _CS1655;
            }
        }
        private static ICSharpCompilerReferenceError _CS1655;

        /// <summary><para>C&#9839; compiler error &#35;1656:</para><para>Cannot assign to {0} because it is a {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1656
        {
            get
            {
                if (_CS1656 == null)
                    _CS1656 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1656, CSharpErrorIdentifiers.CS1656);
                return _CS1656;
            }
        }
        private static ICSharpCompilerReferenceError _CS1656;

        /// <summary><para>C&#9839; compiler error &#35;1657:</para><para>Cannot pass {0} as a ref or out argument because {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1657
        {
            get
            {
                if (_CS1657 == null)
                    _CS1657 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1657, CSharpErrorIdentifiers.CS1657);
                return _CS1657;
            }
        }
        private static ICSharpCompilerReferenceError _CS1657;

        /// <summary><para>C&#9839; compiler error &#35;1660:</para><para>Cannot convert anonymous method block to type {0} because it is not a delegate type</para></summary>
        public static ICSharpCompilerReferenceError CS1660
        {
            get
            {
                if (_CS1660 == null)
                    _CS1660 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1660, CSharpErrorIdentifiers.CS1660);
                return _CS1660;
            }
        }
        private static ICSharpCompilerReferenceError _CS1660;

        /// <summary><para>C&#9839; compiler error &#35;1661:</para><para>Cannot convert anonymous method block to delegate type {0} because the specified block's parameter types do not match the delegate parameter types</para></summary>
        public static ICSharpCompilerReferenceError CS1661
        {
            get
            {
                if (_CS1661 == null)
                    _CS1661 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1661, CSharpErrorIdentifiers.CS1661);
                return _CS1661;
            }
        }
        private static ICSharpCompilerReferenceError _CS1661;

        /// <summary><para>C&#9839; compiler error &#35;1662:</para><para>Cannot convert anonymous method block to delegate type {0} because some of the return types in the block are not implicitly convertible to the delegate return type</para></summary>
        public static ICSharpCompilerReferenceError CS1662
        {
            get
            {
                if (_CS1662 == null)
                    _CS1662 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1662, CSharpErrorIdentifiers.CS1662);
                return _CS1662;
            }
        }
        private static ICSharpCompilerReferenceError _CS1662;

        /// <summary><para>C&#9839; compiler error &#35;1663:</para><para>Fixed size buffer type must be one of the following: bool, byte, short, int, long, char, sbyte, ushort, uint, ulong, float or double</para></summary>
        public static ICSharpCompilerReferenceError CS1663
        {
            get
            {
                if (_CS1663 == null)
                    _CS1663 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1663, CSharpErrorIdentifiers.CS1663);
                return _CS1663;
            }
        }
        private static ICSharpCompilerReferenceError _CS1663;

        /// <summary><para>C&#9839; compiler error &#35;1664:</para><para>Fixed size buffer of length {0} and type {1} is too big</para></summary>
        public static ICSharpCompilerReferenceError CS1664
        {
            get
            {
                if (_CS1664 == null)
                    _CS1664 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1664, CSharpErrorIdentifiers.CS1664);
                return _CS1664;
            }
        }
        private static ICSharpCompilerReferenceError _CS1664;

        /// <summary><para>C&#9839; compiler error &#35;1665:</para><para>Fixed size buffers must have a length greater than zero</para></summary>
        public static ICSharpCompilerReferenceError CS1665
        {
            get
            {
                if (_CS1665 == null)
                    _CS1665 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1665, CSharpErrorIdentifiers.CS1665);
                return _CS1665;
            }
        }
        private static ICSharpCompilerReferenceError _CS1665;

        /// <summary><para>C&#9839; compiler error &#35;1666:</para><para>You cannot use fixed size buffers contained in unfixed expressions. Try using the fixed statement.</para></summary>
        public static ICSharpCompilerReferenceError CS1666
        {
            get
            {
                if (_CS1666 == null)
                    _CS1666 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1666, CSharpErrorIdentifiers.CS1666);
                return _CS1666;
            }
        }
        private static ICSharpCompilerReferenceError _CS1666;

        /// <summary><para>C&#9839; compiler error &#35;1667:</para><para>Attribute {0} is not valid on property or event accessors. It is valid on {1} declarations only.</para></summary>
        public static ICSharpCompilerReferenceError CS1667
        {
            get
            {
                if (_CS1667 == null)
                    _CS1667 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1667, CSharpErrorIdentifiers.CS1667);
                return _CS1667;
            }
        }
        private static ICSharpCompilerReferenceError _CS1667;

        /// <summary><para>C&#9839; compiler error &#35;1670:</para><para>params is not valid in this context</para></summary>
        public static ICSharpCompilerReferenceError CS1670
        {
            get
            {
                if (_CS1670 == null)
                    _CS1670 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1670, CSharpErrorIdentifiers.CS1670);
                return _CS1670;
            }
        }
        private static ICSharpCompilerReferenceError _CS1670;

        /// <summary><para>C&#9839; compiler error &#35;1671:</para><para>A namespace declaration cannot have modifiers or attributes</para></summary>
        public static ICSharpCompilerReferenceError CS1671
        {
            get
            {
                if (_CS1671 == null)
                    _CS1671 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1671, CSharpErrorIdentifiers.CS1671);
                return _CS1671;
            }
        }
        private static ICSharpCompilerReferenceError _CS1671;

        /// <summary><para>C&#9839; compiler error &#35;1672:</para><para>Invalid option {0} for /platform; must be anycpu, x86, Itanium or x64</para></summary>
        public static ICSharpCompilerReferenceError CS1672
        {
            get
            {
                if (_CS1672 == null)
                    _CS1672 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1672, CSharpErrorIdentifiers.CS1672);
                return _CS1672;
            }
        }
        private static ICSharpCompilerReferenceError _CS1672;

        /// <summary><para>C&#9839; compiler error &#35;1673:</para><para>Anonymous methods, lambda expressions, and query expressions inside structs cannot access instance members of 'this'. Consider copying 'this' to a local variable outside the anonymous method, lambda expression or query expression and using the local instead.</para></summary>
        public static ICSharpCompilerReferenceError CS1673
        {
            get
            {
                if (_CS1673 == null)
                    _CS1673 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1673, CSharpErrorIdentifiers.CS1673);
                return _CS1673;
            }
        }
        private static ICSharpCompilerReferenceError _CS1673;

        /// <summary><para>C&#9839; compiler error &#35;1674:</para><para>{0}: type used in a using statement must be implicitly convertible to 'System.IDisposable'</para></summary>
        public static ICSharpCompilerReferenceError CS1674
        {
            get
            {
                if (_CS1674 == null)
                    _CS1674 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1674, CSharpErrorIdentifiers.CS1674);
                return _CS1674;
            }
        }
        private static ICSharpCompilerReferenceError _CS1674;

        /// <summary><para>C&#9839; compiler error &#35;1675:</para><para>Enums cannot have type parameters</para></summary>
        public static ICSharpCompilerReferenceError CS1675
        {
            get
            {
                if (_CS1675 == null)
                    _CS1675 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1675, CSharpErrorIdentifiers.CS1675);
                return _CS1675;
            }
        }
        private static ICSharpCompilerReferenceError _CS1675;

        /// <summary><para>C&#9839; compiler error &#35;1676:</para><para>Parameter {0} must be declared with the {1} keyword</para></summary>
        public static ICSharpCompilerReferenceError CS1676
        {
            get
            {
                if (_CS1676 == null)
                    _CS1676 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1676, CSharpErrorIdentifiers.CS1676);
                return _CS1676;
            }
        }
        private static ICSharpCompilerReferenceError _CS1676;

        /// <summary><para>C&#9839; compiler error &#35;1677:</para><para>Parameter {0} should not be declared with the {1} keyword</para></summary>
        public static ICSharpCompilerReferenceError CS1677
        {
            get
            {
                if (_CS1677 == null)
                    _CS1677 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1677, CSharpErrorIdentifiers.CS1677);
                return _CS1677;
            }
        }
        private static ICSharpCompilerReferenceError _CS1677;

        /// <summary><para>C&#9839; compiler error &#35;1678:</para><para>Parameter {0} is declared as type {1} but should be {2}</para></summary>
        public static ICSharpCompilerReferenceError CS1678
        {
            get
            {
                if (_CS1678 == null)
                    _CS1678 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1678, CSharpErrorIdentifiers.CS1678);
                return _CS1678;
            }
        }
        private static ICSharpCompilerReferenceError _CS1678;

        /// <summary><para>C&#9839; compiler error &#35;1679:</para><para>Invalid extern alias for {0}; {1} is not a valid identifier</para></summary>
        public static ICSharpCompilerReferenceError CS1679
        {
            get
            {
                if (_CS1679 == null)
                    _CS1679 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1679, CSharpErrorIdentifiers.CS1679);
                return _CS1679;
            }
        }
        private static ICSharpCompilerReferenceError _CS1679;

        /// <summary><para>C&#9839; compiler error &#35;1680:</para><para>Invalid reference alias option: 'alias=' -- missing filename.</para></summary>
        public static ICSharpCompilerReferenceError CS1680
        {
            get
            {
                if (_CS1680 == null)
                    _CS1680 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1680, CSharpErrorIdentifiers.CS1680);
                return _CS1680;
            }
        }
        private static ICSharpCompilerReferenceError _CS1680;

        /// <summary><para>C&#9839; compiler error &#35;1681:</para><para>You cannot redefine the global extern alias</para></summary>
        public static ICSharpCompilerReferenceError CS1681
        {
            get
            {
                if (_CS1681 == null)
                    _CS1681 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1681, CSharpErrorIdentifiers.CS1681);
                return _CS1681;
            }
        }
        private static ICSharpCompilerReferenceError _CS1681;

        /// <summary><para>C&#9839; compiler error &#35;1686:</para><para>Local {0} or its members cannot have their address taken and be used inside an anonymous method or lambda expression</para></summary>
        public static ICSharpCompilerReferenceError CS1686
        {
            get
            {
                if (_CS1686 == null)
                    _CS1686 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1686, CSharpErrorIdentifiers.CS1686);
                return _CS1686;
            }
        }
        private static ICSharpCompilerReferenceError _CS1686;

        /// <summary><para>C&#9839; compiler error &#35;1688:</para><para>Cannot convert anonymous method block without a parameter list to delegate type {0} because it has one or more out parameters</para></summary>
        public static ICSharpCompilerReferenceError CS1688
        {
            get
            {
                if (_CS1688 == null)
                    _CS1688 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1688, CSharpErrorIdentifiers.CS1688);
                return _CS1688;
            }
        }
        private static ICSharpCompilerReferenceError _CS1688;

        /// <summary><para>C&#9839; compiler error &#35;1689:</para><para>Attribute {0} is only valid on methods or attribute classes</para></summary>
        public static ICSharpCompilerReferenceError CS1689
        {
            get
            {
                if (_CS1689 == null)
                    _CS1689 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1689, CSharpErrorIdentifiers.CS1689);
                return _CS1689;
            }
        }
        private static ICSharpCompilerReferenceError _CS1689;

        /// <summary><para>C&#9839; compiler error &#35;1703:</para><para>An assembly with the same simple name {0} has already been imported. Try removing one of the references or sign them to enable side-by-side.</para></summary>
        public static ICSharpCompilerReferenceError CS1703
        {
            get
            {
                if (_CS1703 == null)
                    _CS1703 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1703, CSharpErrorIdentifiers.CS1703);
                return _CS1703;
            }
        }
        private static ICSharpCompilerReferenceError _CS1703;

        /// <summary><para>C&#9839; compiler error &#35;1704:</para><para>An assembly with the same simple name {0} has already been imported. Try removing one of the references or sign them to enable side-by-side.</para></summary>
        public static ICSharpCompilerReferenceError CS1704
        {
            get
            {
                if (_CS1704 == null)
                    _CS1704 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1704, CSharpErrorIdentifiers.CS1704);
                return _CS1704;
            }
        }
        private static ICSharpCompilerReferenceError _CS1704;

        /// <summary><para>C&#9839; compiler error &#35;1705:</para><para>Assembly {0} uses {1} which has a higher version than referenced assembly {2}</para></summary>
        public static ICSharpCompilerReferenceError CS1705
        {
            get
            {
                if (_CS1705 == null)
                    _CS1705 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1705, CSharpErrorIdentifiers.CS1705);
                return _CS1705;
            }
        }
        private static ICSharpCompilerReferenceError _CS1705;

        /// <summary><para>C&#9839; compiler error &#35;1706:</para><para>Expression cannot contain anonymous methods or lambda expressions</para></summary>
        public static ICSharpCompilerReferenceError CS1706
        {
            get
            {
                if (_CS1706 == null)
                    _CS1706 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1706, CSharpErrorIdentifiers.CS1706);
                return _CS1706;
            }
        }
        private static ICSharpCompilerReferenceError _CS1706;

        /// <summary><para>C&#9839; compiler error &#35;1708:</para><para>Fixed size buffers can only be accessed through locals or fields</para></summary>
        public static ICSharpCompilerReferenceError CS1708
        {
            get
            {
                if (_CS1708 == null)
                    _CS1708 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1708, CSharpErrorIdentifiers.CS1708);
                return _CS1708;
            }
        }
        private static ICSharpCompilerReferenceError _CS1708;

        /// <summary><para>C&#9839; compiler error &#35;1713:</para><para>Unexpected error building metadata name for type {0} — {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1713
        {
            get
            {
                if (_CS1713 == null)
                    _CS1713 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1713, CSharpErrorIdentifiers.CS1713);
                return _CS1713;
            }
        }
        private static ICSharpCompilerReferenceError _CS1713;

        /// <summary><para>C&#9839; compiler error &#35;1714:</para><para>The base class or interface of {0} could not be resolved or is invalid</para></summary>
        public static ICSharpCompilerReferenceError CS1714
        {
            get
            {
                if (_CS1714 == null)
                    _CS1714 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1714, CSharpErrorIdentifiers.CS1714);
                return _CS1714;
            }
        }
        private static ICSharpCompilerReferenceError _CS1714;

        /// <summary><para>C&#9839; compiler error &#35;1715:</para><para>{0}: type must be {1} to match overridden member {2}</para></summary>
        public static ICSharpCompilerReferenceError CS1715
        {
            get
            {
                if (_CS1715 == null)
                    _CS1715 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1715, CSharpErrorIdentifiers.CS1715);
                return _CS1715;
            }
        }
        private static ICSharpCompilerReferenceError _CS1715;

        /// <summary><para>C&#9839; compiler error &#35;1716:</para><para>Do not use 'System.Runtime.CompilerServices.FixedBuffer' attribute. Use the 'fixed' field modifier instead.</para></summary>
        public static ICSharpCompilerReferenceError CS1716
        {
            get
            {
                if (_CS1716 == null)
                    _CS1716 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1716, CSharpErrorIdentifiers.CS1716);
                return _CS1716;
            }
        }
        private static ICSharpCompilerReferenceError _CS1716;

        /// <summary><para>C&#9839; compiler error &#35;1719:</para><para>SourceError reading Win32 resource file {0} -- {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1719
        {
            get
            {
                if (_CS1719 == null)
                    _CS1719 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1719, CSharpErrorIdentifiers.CS1719);
                return _CS1719;
            }
        }
        private static ICSharpCompilerReferenceError _CS1719;

        /// <summary><para>C&#9839; compiler error &#35;1721:</para><para>Class {0} cannot have multiple base classes: {1} and {2}</para></summary>
        public static ICSharpCompilerReferenceError CS1721
        {
            get
            {
                if (_CS1721 == null)
                    _CS1721 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1721, CSharpErrorIdentifiers.CS1721);
                return _CS1721;
            }
        }
        private static ICSharpCompilerReferenceError _CS1721;

        /// <summary><para>C&#9839; compiler error &#35;1722:</para><para>Base class {0} must come before any interfaces</para></summary>
        public static ICSharpCompilerReferenceError CS1722
        {
            get
            {
                if (_CS1722 == null)
                    _CS1722 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1722, CSharpErrorIdentifiers.CS1722);
                return _CS1722;
            }
        }
        private static ICSharpCompilerReferenceError _CS1722;

        /// <summary><para>C&#9839; compiler error &#35;1724:</para><para>Value specified for the argument to 'System.Runtime.InteropServices.DefaultCharSetAttribute' is not valid</para></summary>
        public static ICSharpCompilerReferenceError CS1724
        {
            get
            {
                if (_CS1724 == null)
                    _CS1724 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1724, CSharpErrorIdentifiers.CS1724);
                return _CS1724;
            }
        }
        private static ICSharpCompilerReferenceError _CS1724;

        /// <summary><para>C&#9839; compiler error &#35;1725:</para><para>Friend assembly reference {0} is invalid. InternalsVisibleTo declarations cannot have a version, culture, public key token, or processor architecture specified.</para></summary>
        public static ICSharpCompilerReferenceError CS1725
        {
            get
            {
                if (_CS1725 == null)
                    _CS1725 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1725, CSharpErrorIdentifiers.CS1725);
                return _CS1725;
            }
        }
        private static ICSharpCompilerReferenceError _CS1725;

        /// <summary><para>C&#9839; compiler error &#35;1726:</para><para>Friend assembly reference {0} is invalid. Strong-name signed assemblies must specify a public key in their InternalsVisibleTo declarations.</para></summary>
        public static ICSharpCompilerReferenceError CS1726
        {
            get
            {
                if (_CS1726 == null)
                    _CS1726 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1726, CSharpErrorIdentifiers.CS1726);
                return _CS1726;
            }
        }
        private static ICSharpCompilerReferenceError _CS1726;

        /// <summary><para>C&#9839; compiler error &#35;1727:</para><para>Cannot send error report automatically without authorization. Please visit {0} to authorize sending error report.</para></summary>
        public static ICSharpCompilerReferenceError CS1727
        {
            get
            {
                if (_CS1727 == null)
                    _CS1727 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1727, CSharpErrorIdentifiers.CS1727);
                return _CS1727;
            }
        }
        private static ICSharpCompilerReferenceError _CS1727;

        /// <summary><para>C&#9839; compiler error &#35;1728:</para><para>Cannot bind delegate to {0} because it is a member of {1}</para></summary>
        public static ICSharpCompilerReferenceError CS1728
        {
            get
            {
                if (_CS1728 == null)
                    _CS1728 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1728, CSharpErrorIdentifiers.CS1728);
                return _CS1728;
            }
        }
        private static ICSharpCompilerReferenceError _CS1728;

        /// <summary><para>C&#9839; compiler error &#35;1729:</para><para>{0} does not contain a constructor that takes {1} arguments.</para></summary>
        public static ICSharpCompilerReferenceError CS1729
        {
            get
            {
                if (_CS1729 == null)
                    _CS1729 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1729, CSharpErrorIdentifiers.CS1729);
                return _CS1729;
            }
        }
        private static ICSharpCompilerReferenceError _CS1729;

        /// <summary><para>C&#9839; compiler error &#35;1730:</para><para>Assembly and module attributes must precede all other elements defined in a file except using clauses and extern alias declarations.</para></summary>
        public static ICSharpCompilerReferenceError CS1730
        {
            get
            {
                if (_CS1730 == null)
                    _CS1730 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1730, CSharpErrorIdentifiers.CS1730);
                return _CS1730;
            }
        }
        private static ICSharpCompilerReferenceError _CS1730;

        /// <summary><para>C&#9839; compiler error &#35;1731:</para><para>Cannot convert {0} to delegate because some of the return types in the block are not implicitly convertible to the delegate return type.</para></summary>
        public static ICSharpCompilerReferenceError CS1731
        {
            get
            {
                if (_CS1731 == null)
                    _CS1731 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1731, CSharpErrorIdentifiers.CS1731);
                return _CS1731;
            }
        }
        private static ICSharpCompilerReferenceError _CS1731;

        /// <summary><para>C&#9839; compiler error &#35;1732:</para><para>Expected parameter.</para></summary>
        public static ICSharpCompilerReferenceError CS1732
        {
            get
            {
                if (_CS1732 == null)
                    _CS1732 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1732, CSharpErrorIdentifiers.CS1732);
                return _CS1732;
            }
        }
        private static ICSharpCompilerReferenceError _CS1732;

        /// <summary><para>C&#9839; compiler error &#35;1733:</para><para>Expected expression.</para></summary>
        public static ICSharpCompilerReferenceError CS1733
        {
            get
            {
                if (_CS1733 == null)
                    _CS1733 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1733, CSharpErrorIdentifiers.CS1733);
                return _CS1733;
            }
        }
        private static ICSharpCompilerReferenceError _CS1733;

        /// <summary><para>C&#9839; compiler error &#35;1900:</para><para>SourceWarning level must be in the range 0-4</para></summary>
        public static ICSharpCompilerReferenceError CS1900
        {
            get
            {
                if (_CS1900 == null)
                    _CS1900 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1900, CSharpErrorIdentifiers.CS1900);
                return _CS1900;
            }
        }
        private static ICSharpCompilerReferenceError _CS1900;

        /// <summary><para>C&#9839; compiler error &#35;1902:</para><para>Invalid option {0} for /debug; must be full or pdbonly</para></summary>
        public static ICSharpCompilerReferenceError CS1902
        {
            get
            {
                if (_CS1902 == null)
                    _CS1902 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1902, CSharpErrorIdentifiers.CS1902);
                return _CS1902;
            }
        }
        private static ICSharpCompilerReferenceError _CS1902;

        /// <summary><para>C&#9839; compiler error &#35;1906:</para><para>Invalid option {0}; Resource visibility must be either 'public' or 'private'</para></summary>
        public static ICSharpCompilerReferenceError CS1906
        {
            get
            {
                if (_CS1906 == null)
                    _CS1906 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1906, CSharpErrorIdentifiers.CS1906);
                return _CS1906;
            }
        }
        private static ICSharpCompilerReferenceError _CS1906;

        /// <summary><para>C&#9839; compiler error &#35;1908:</para><para>The type of the argument to the DefaultValue attribute must match the parameter type</para></summary>
        public static ICSharpCompilerReferenceError CS1908
        {
            get
            {
                if (_CS1908 == null)
                    _CS1908 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1908, CSharpErrorIdentifiers.CS1908);
                return _CS1908;
            }
        }
        private static ICSharpCompilerReferenceError _CS1908;

        /// <summary><para>C&#9839; compiler error &#35;1909:</para><para>The DefaultValue attribute is not applicable on parameters of type {0}</para></summary>
        public static ICSharpCompilerReferenceError CS1909
        {
            get
            {
                if (_CS1909 == null)
                    _CS1909 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1909, CSharpErrorIdentifiers.CS1909);
                return _CS1909;
            }
        }
        private static ICSharpCompilerReferenceError _CS1909;

        /// <summary><para>C&#9839; compiler error &#35;1910:</para><para>Argument of type {0} is not applicable for the DefaultValue attribute</para></summary>
        public static ICSharpCompilerReferenceError CS1910
        {
            get
            {
                if (_CS1910 == null)
                    _CS1910 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1910, CSharpErrorIdentifiers.CS1910);
                return _CS1910;
            }
        }
        private static ICSharpCompilerReferenceError _CS1910;

        /// <summary><para>C&#9839; compiler error &#35;1912:</para><para>Duplicate initialization of member {0}.</para></summary>
        public static ICSharpCompilerReferenceError CS1912
        {
            get
            {
                if (_CS1912 == null)
                    _CS1912 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1912, CSharpErrorIdentifiers.CS1912);
                return _CS1912;
            }
        }
        private static ICSharpCompilerReferenceError _CS1912;

        /// <summary><para>C&#9839; compiler error &#35;1913:</para><para>Member {0} cannot be initialized. It is not a field or property.</para></summary>
        public static ICSharpCompilerReferenceError CS1913
        {
            get
            {
                if (_CS1913 == null)
                    _CS1913 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1913, CSharpErrorIdentifiers.CS1913);
                return _CS1913;
            }
        }
        private static ICSharpCompilerReferenceError _CS1913;

        /// <summary><para>C&#9839; compiler error &#35;1914:</para><para>Static field {0} cannot be assigned in an object initializer</para></summary>
        public static ICSharpCompilerReferenceError CS1914
        {
            get
            {
                if (_CS1914 == null)
                    _CS1914 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1914, CSharpErrorIdentifiers.CS1914);
                return _CS1914;
            }
        }
        private static ICSharpCompilerReferenceError _CS1914;

        /// <summary><para>C&#9839; compiler error &#35;1917:</para><para>Members of read-only field {0} of type {1} cannot be assigned with an object initializer because it is of a value type.</para></summary>
        public static ICSharpCompilerReferenceError CS1917
        {
            get
            {
                if (_CS1917 == null)
                    _CS1917 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1917, CSharpErrorIdentifiers.CS1917);
                return _CS1917;
            }
        }
        private static ICSharpCompilerReferenceError _CS1917;

        /// <summary><para>C&#9839; compiler error &#35;1918:</para><para>Members of property {0} of type {1} cannot be assigned with an object initializer because it is of a value type.</para></summary>
        public static ICSharpCompilerReferenceError CS1918
        {
            get
            {
                if (_CS1918 == null)
                    _CS1918 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1918, CSharpErrorIdentifiers.CS1918);
                return _CS1918;
            }
        }
        private static ICSharpCompilerReferenceError _CS1918;

        /// <summary><para>C&#9839; compiler error &#35;1919:</para><para>Unsafe type {0} cannot be used in object creation.</para></summary>
        public static ICSharpCompilerReferenceError CS1919
        {
            get
            {
                if (_CS1919 == null)
                    _CS1919 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1919, CSharpErrorIdentifiers.CS1919);
                return _CS1919;
            }
        }
        private static ICSharpCompilerReferenceError _CS1919;

        /// <summary><para>C&#9839; compiler error &#35;1920:</para><para>Element initializer cannot be empty.</para></summary>
        public static ICSharpCompilerReferenceError CS1920
        {
            get
            {
                if (_CS1920 == null)
                    _CS1920 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1920, CSharpErrorIdentifiers.CS1920);
                return _CS1920;
            }
        }
        private static ICSharpCompilerReferenceError _CS1920;

        /// <summary><para>C&#9839; compiler error &#35;1921:</para><para>The best overloaded method match for {0} has wrong signature for the initializer element. The initializable Add must be an accessible instance method.</para></summary>
        public static ICSharpCompilerReferenceError CS1921
        {
            get
            {
                if (_CS1921 == null)
                    _CS1921 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1921, CSharpErrorIdentifiers.CS1921);
                return _CS1921;
            }
        }
        private static ICSharpCompilerReferenceError _CS1921;

        /// <summary><para>C&#9839; compiler error &#35;1922:</para><para>Collection initializer requires its type {0} to implement System.Collections.IEnumerable.</para></summary>
        public static ICSharpCompilerReferenceError CS1922
        {
            get
            {
                if (_CS1922 == null)
                    _CS1922 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1922, CSharpErrorIdentifiers.CS1922);
                return _CS1922;
            }
        }
        private static ICSharpCompilerReferenceError _CS1922;

        /// <summary><para>C&#9839; compiler error &#35;1925:</para><para>Cannot initialize object of type {0} with a collection initializer.</para></summary>
        public static ICSharpCompilerReferenceError CS1925
        {
            get
            {
                if (_CS1925 == null)
                    _CS1925 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1925, CSharpErrorIdentifiers.CS1925);
                return _CS1925;
            }
        }
        private static ICSharpCompilerReferenceError _CS1925;

        /// <summary><para>C&#9839; compiler error &#35;1926:</para><para>SourceError reading Win32 manifest file {0} -- {1}.</para></summary>
        public static ICSharpCompilerReferenceError CS1926
        {
            get
            {
                if (_CS1926 == null)
                    _CS1926 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1926, CSharpErrorIdentifiers.CS1926);
                return _CS1926;
            }
        }
        private static ICSharpCompilerReferenceError _CS1926;

        /// <summary><para>C&#9839; compiler error &#35;1928:</para><para>{0} does not contain a definition for {1} and the best extension method overload {2} has some invalid arguments.</para></summary>
        public static ICSharpCompilerReferenceError CS1928
        {
            get
            {
                if (_CS1928 == null)
                    _CS1928 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1928, CSharpErrorIdentifiers.CS1928);
                return _CS1928;
            }
        }
        private static ICSharpCompilerReferenceError _CS1928;

        /// <summary><para>C&#9839; compiler error &#35;1929:</para><para>Instance argument: cannot convert from {0} to {1}.</para></summary>
        public static ICSharpCompilerReferenceError CS1929
        {
            get
            {
                if (_CS1929 == null)
                    _CS1929 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1929, CSharpErrorIdentifiers.CS1929);
                return _CS1929;
            }
        }
        private static ICSharpCompilerReferenceError _CS1929;

        /// <summary><para>C&#9839; compiler error &#35;1930:</para><para>The range variable {0} has already been declared</para></summary>
        public static ICSharpCompilerReferenceError CS1930
        {
            get
            {
                if (_CS1930 == null)
                    _CS1930 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1930, CSharpErrorIdentifiers.CS1930);
                return _CS1930;
            }
        }
        private static ICSharpCompilerReferenceError _CS1930;

        /// <summary><para>C&#9839; compiler error &#35;1931:</para><para>The range variable {0} conflicts with a previous declaration of {1}.</para></summary>
        public static ICSharpCompilerReferenceError CS1931
        {
            get
            {
                if (_CS1931 == null)
                    _CS1931 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1931, CSharpErrorIdentifiers.CS1931);
                return _CS1931;
            }
        }
        private static ICSharpCompilerReferenceError _CS1931;

        /// <summary><para>C&#9839; compiler error &#35;1932:</para><para>Cannot assign {0} to a range variable.</para></summary>
        public static ICSharpCompilerReferenceError CS1932
        {
            get
            {
                if (_CS1932 == null)
                    _CS1932 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1932, CSharpErrorIdentifiers.CS1932);
                return _CS1932;
            }
        }
        private static ICSharpCompilerReferenceError _CS1932;

        /// <summary><para>C&#9839; compiler error &#35;1933:</para><para>Expression cannot contain query expressions</para></summary>
        public static ICSharpCompilerReferenceError CS1933
        {
            get
            {
                if (_CS1933 == null)
                    _CS1933 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1933, CSharpErrorIdentifiers.CS1933);
                return _CS1933;
            }
        }
        private static ICSharpCompilerReferenceError _CS1933;

        /// <summary><para>C&#9839; compiler error &#35;1934:</para><para>Could not find an implementation of the query pattern for source type {0}. {1} not found. Consider explicitly specifying the type of the range variable {2}.</para></summary>
        public static ICSharpCompilerReferenceError CS1934
        {
            get
            {
                if (_CS1934 == null)
                    _CS1934 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1934, CSharpErrorIdentifiers.CS1934);
                return _CS1934;
            }
        }
        private static ICSharpCompilerReferenceError _CS1934;

        /// <summary><para>C&#9839; compiler error &#35;1935:</para><para>Could not find an implementation of the query pattern for source type {0}. {1} not found. Are you missing a reference to 'System.Core.dll' or a using directive for 'System.Linq'?</para></summary>
        public static ICSharpCompilerReferenceError CS1935
        {
            get
            {
                if (_CS1935 == null)
                    _CS1935 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1935, CSharpErrorIdentifiers.CS1935);
                return _CS1935;
            }
        }
        private static ICSharpCompilerReferenceError _CS1935;

        /// <summary><para>C&#9839; compiler error &#35;1936:</para><para>Could not find an implementation of the query pattern for source type {0}. {1} not found.</para></summary>
        public static ICSharpCompilerReferenceError CS1936
        {
            get
            {
                if (_CS1936 == null)
                    _CS1936 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1936, CSharpErrorIdentifiers.CS1936);
                return _CS1936;
            }
        }
        private static ICSharpCompilerReferenceError _CS1936;

        /// <summary><para>C&#9839; compiler error &#35;1937:</para><para>The name {0} is not in scope on the left side of 'equals'. Consider swapping the expressions on either side of 'equals'.</para></summary>
        public static ICSharpCompilerReferenceError CS1937
        {
            get
            {
                if (_CS1937 == null)
                    _CS1937 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1937, CSharpErrorIdentifiers.CS1937);
                return _CS1937;
            }
        }
        private static ICSharpCompilerReferenceError _CS1937;

        /// <summary><para>C&#9839; compiler error &#35;1938:</para><para>The name {0} is not in scope on the right side of 'equals'. Consider swapping the expressions on either side of 'equals'.</para></summary>
        public static ICSharpCompilerReferenceError CS1938
        {
            get
            {
                if (_CS1938 == null)
                    _CS1938 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1938, CSharpErrorIdentifiers.CS1938);
                return _CS1938;
            }
        }
        private static ICSharpCompilerReferenceError _CS1938;

        /// <summary><para>C&#9839; compiler error &#35;1939:</para><para>Cannot pass the range variable {0} as an out or ref parameter.</para></summary>
        public static ICSharpCompilerReferenceError CS1939
        {
            get
            {
                if (_CS1939 == null)
                    _CS1939 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1939, CSharpErrorIdentifiers.CS1939);
                return _CS1939;
            }
        }
        private static ICSharpCompilerReferenceError _CS1939;

        /// <summary><para>C&#9839; compiler error &#35;1940:</para><para>Multiple implementations of the query pattern were found for source type {0}. Ambiguous call to {1}.</para></summary>
        public static ICSharpCompilerReferenceError CS1940
        {
            get
            {
                if (_CS1940 == null)
                    _CS1940 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1940, CSharpErrorIdentifiers.CS1940);
                return _CS1940;
            }
        }
        private static ICSharpCompilerReferenceError _CS1940;

        /// <summary><para>C&#9839; compiler error &#35;1941:</para><para>The type of one of the expressions in the {0} clause is incorrect. GenericParameter inference failed in the call to {1}.</para></summary>
        public static ICSharpCompilerReferenceError CS1941
        {
            get
            {
                if (_CS1941 == null)
                    _CS1941 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1941, CSharpErrorIdentifiers.CS1941);
                return _CS1941;
            }
        }
        private static ICSharpCompilerReferenceError _CS1941;

        /// <summary><para>C&#9839; compiler error &#35;1942:</para><para>The type of the expression in the {0} clause is incorrect. GenericParameter inference failed in the call to {1}.</para></summary>
        public static ICSharpCompilerReferenceError CS1942
        {
            get
            {
                if (_CS1942 == null)
                    _CS1942 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1942, CSharpErrorIdentifiers.CS1942);
                return _CS1942;
            }
        }
        private static ICSharpCompilerReferenceError _CS1942;

        /// <summary><para>C&#9839; compiler error &#35;1943:</para><para>An expression of type {0} is not allowed in a subsequent from clause in a query expression with source type {1}. GenericParameter inference failed in the call to {2}.</para></summary>
        public static ICSharpCompilerReferenceError CS1943
        {
            get
            {
                if (_CS1943 == null)
                    _CS1943 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1943, CSharpErrorIdentifiers.CS1943);
                return _CS1943;
            }
        }
        private static ICSharpCompilerReferenceError _CS1943;

        /// <summary><para>C&#9839; compiler error &#35;1944:</para><para>An expression tree may not contain an unsafe pointer operation</para></summary>
        public static ICSharpCompilerReferenceError CS1944
        {
            get
            {
                if (_CS1944 == null)
                    _CS1944 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1944, CSharpErrorIdentifiers.CS1944);
                return _CS1944;
            }
        }
        private static ICSharpCompilerReferenceError _CS1944;

        /// <summary><para>C&#9839; compiler error &#35;1945:</para><para>An expression tree may not contain an anonymous method expression.</para></summary>
        public static ICSharpCompilerReferenceError CS1945
        {
            get
            {
                if (_CS1945 == null)
                    _CS1945 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1945, CSharpErrorIdentifiers.CS1945);
                return _CS1945;
            }
        }
        private static ICSharpCompilerReferenceError _CS1945;

        /// <summary><para>C&#9839; compiler error &#35;1946:</para><para>An anonymous method expression cannot be converted to an expression tree.</para></summary>
        public static ICSharpCompilerReferenceError CS1946
        {
            get
            {
                if (_CS1946 == null)
                    _CS1946 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1946, CSharpErrorIdentifiers.CS1946);
                return _CS1946;
            }
        }
        private static ICSharpCompilerReferenceError _CS1946;

        /// <summary><para>C&#9839; compiler error &#35;1947:</para><para>Range variable {0} cannot be assigned to -- it is read only.</para></summary>
        public static ICSharpCompilerReferenceError CS1947
        {
            get
            {
                if (_CS1947 == null)
                    _CS1947 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1947, CSharpErrorIdentifiers.CS1947);
                return _CS1947;
            }
        }
        private static ICSharpCompilerReferenceError _CS1947;

        /// <summary><para>C&#9839; compiler error &#35;1948:</para><para>The range variable {0} cannot have the same name as a method type parameter</para></summary>
        public static ICSharpCompilerReferenceError CS1948
        {
            get
            {
                if (_CS1948 == null)
                    _CS1948 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1948, CSharpErrorIdentifiers.CS1948);
                return _CS1948;
            }
        }
        private static ICSharpCompilerReferenceError _CS1948;

        /// <summary><para>C&#9839; compiler error &#35;1949:</para><para>The contextual keyword 'var' cannot be used in a range variable declaration.</para></summary>
        public static ICSharpCompilerReferenceError CS1949
        {
            get
            {
                if (_CS1949 == null)
                    _CS1949 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1949, CSharpErrorIdentifiers.CS1949);
                return _CS1949;
            }
        }
        private static ICSharpCompilerReferenceError _CS1949;

        /// <summary><para>C&#9839; compiler error &#35;1950:</para><para>The best overloaded Add method {0} for the collection initializer has some invalid arguments.</para></summary>
        public static ICSharpCompilerReferenceError CS1950
        {
            get
            {
                if (_CS1950 == null)
                    _CS1950 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1950, CSharpErrorIdentifiers.CS1950);
                return _CS1950;
            }
        }
        private static ICSharpCompilerReferenceError _CS1950;

        /// <summary><para>C&#9839; compiler error &#35;1951:</para><para>An expression tree lambda may not contain an out or ref parameter.</para></summary>
        public static ICSharpCompilerReferenceError CS1951
        {
            get
            {
                if (_CS1951 == null)
                    _CS1951 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1951, CSharpErrorIdentifiers.CS1951);
                return _CS1951;
            }
        }
        private static ICSharpCompilerReferenceError _CS1951;

        /// <summary><para>C&#9839; compiler error &#35;1952:</para><para>An expression tree lambda may not contain a method with variable arguments</para></summary>
        public static ICSharpCompilerReferenceError CS1952
        {
            get
            {
                if (_CS1952 == null)
                    _CS1952 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1952, CSharpErrorIdentifiers.CS1952);
                return _CS1952;
            }
        }
        private static ICSharpCompilerReferenceError _CS1952;

        /// <summary><para>C&#9839; compiler error &#35;1953:</para><para>An expression tree lambda may not contain a method group.</para></summary>
        public static ICSharpCompilerReferenceError CS1953
        {
            get
            {
                if (_CS1953 == null)
                    _CS1953 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1953, CSharpErrorIdentifiers.CS1953);
                return _CS1953;
            }
        }
        private static ICSharpCompilerReferenceError _CS1953;

        /// <summary><para>C&#9839; compiler error &#35;1954:</para><para>The best overloaded method match {0} for the collection initializer element cannot be used. Collection initializer 'Add' methods cannot have ref or out parameters.</para></summary>
        public static ICSharpCompilerReferenceError CS1954
        {
            get
            {
                if (_CS1954 == null)
                    _CS1954 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1954, CSharpErrorIdentifiers.CS1954);
                return _CS1954;
            }
        }
        private static ICSharpCompilerReferenceError _CS1954;

        /// <summary><para>C&#9839; compiler error &#35;1955:</para><para>Non-invocable member {0} cannot be used like a method.</para></summary>
        public static ICSharpCompilerReferenceError CS1955
        {
            get
            {
                if (_CS1955 == null)
                    _CS1955 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1955, CSharpErrorIdentifiers.CS1955);
                return _CS1955;
            }
        }
        private static ICSharpCompilerReferenceError _CS1955;

        /// <summary><para>C&#9839; compiler error &#35;1958:</para><para>Object and collection initializer expressions may not be applied to a delegate creation expression,</para></summary>
        public static ICSharpCompilerReferenceError CS1958
        {
            get
            {
                if (_CS1958 == null)
                    _CS1958 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1958, CSharpErrorIdentifiers.CS1958);
                return _CS1958;
            }
        }
        private static ICSharpCompilerReferenceError _CS1958;

        /// <summary><para>C&#9839; compiler error &#35;1959:</para><para>{0} is of type {1}. The type specified in a constant declaration must be sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, bool, string, an enum-type, or a reference-type.</para></summary>
        public static ICSharpCompilerReferenceError CS1959
        {
            get
            {
                if (_CS1959 == null)
                    _CS1959 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS1959, CSharpErrorIdentifiers.CS1959);
                return _CS1959;
            }
        }
        private static ICSharpCompilerReferenceError _CS1959;

        /// <summary><para>C&#9839; compiler error &#35;2001:</para><para>Source file {0} could not be found</para></summary>
        public static ICSharpCompilerReferenceError CS2001
        {
            get
            {
                if (_CS2001 == null)
                    _CS2001 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2001, CSharpErrorIdentifiers.CS2001);
                return _CS2001;
            }
        }
        private static ICSharpCompilerReferenceError _CS2001;

        /// <summary><para>C&#9839; compiler error &#35;2003:</para><para>Response file {0} included multiple times</para></summary>
        public static ICSharpCompilerReferenceError CS2003
        {
            get
            {
                if (_CS2003 == null)
                    _CS2003 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2003, CSharpErrorIdentifiers.CS2003);
                return _CS2003;
            }
        }
        private static ICSharpCompilerReferenceError _CS2003;

        /// <summary><para>C&#9839; compiler error &#35;2005:</para><para>Missing file specification for {0} option</para></summary>
        public static ICSharpCompilerReferenceError CS2005
        {
            get
            {
                if (_CS2005 == null)
                    _CS2005 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2005, CSharpErrorIdentifiers.CS2005);
                return _CS2005;
            }
        }
        private static ICSharpCompilerReferenceError _CS2005;

        /// <summary><para>C&#9839; compiler error &#35;2006:</para><para>Command-line syntax error: Missing {0} for {1} option</para></summary>
        public static ICSharpCompilerReferenceError CS2006
        {
            get
            {
                if (_CS2006 == null)
                    _CS2006 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2006, CSharpErrorIdentifiers.CS2006);
                return _CS2006;
            }
        }
        private static ICSharpCompilerReferenceError _CS2006;

        /// <summary><para>C&#9839; compiler error &#35;2007:</para><para>Unrecognized command-line option: {0}</para></summary>
        public static ICSharpCompilerReferenceError CS2007
        {
            get
            {
                if (_CS2007 == null)
                    _CS2007 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2007, CSharpErrorIdentifiers.CS2007);
                return _CS2007;
            }
        }
        private static ICSharpCompilerReferenceError _CS2007;

        /// <summary><para>C&#9839; compiler error &#35;2008:</para><para>No inputs specified</para></summary>
        public static ICSharpCompilerReferenceError CS2008
        {
            get
            {
                if (_CS2008 == null)
                    _CS2008 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2008, CSharpErrorIdentifiers.CS2008);
                return _CS2008;
            }
        }
        private static ICSharpCompilerReferenceError _CS2008;

        /// <summary><para>C&#9839; compiler error &#35;2012:</para><para>Unable to open response file {0}</para></summary>
        public static ICSharpCompilerReferenceError CS2011
        {
            get
            {
                if (_CS2011 == null)
                    _CS2011 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2011, CSharpErrorIdentifiers.CS2012);
                return _CS2011;
            }
        }
        private static ICSharpCompilerReferenceError _CS2011;

        /// <summary><para>C&#9839; compiler error &#35;2012:</para><para>Cannot open {0} for writing</para></summary>
        public static ICSharpCompilerReferenceError CS2012
        {
            get
            {
                if (_CS2012 == null)
                    _CS2012 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2012, CSharpErrorIdentifiers.CS2012);
                return _CS2012;
            }
        }
        private static ICSharpCompilerReferenceError _CS2012;

        /// <summary><para>C&#9839; compiler error &#35;2013:</para><para>Invalid image base number {0}</para></summary>
        public static ICSharpCompilerReferenceError CS2013
        {
            get
            {
                if (_CS2013 == null)
                    _CS2013 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2013, CSharpErrorIdentifiers.CS2013);
                return _CS2013;
            }
        }
        private static ICSharpCompilerReferenceError _CS2013;

        /// <summary><para>C&#9839; compiler error &#35;2015:</para><para>{0} is a binary file instead of a text file</para></summary>
        public static ICSharpCompilerReferenceError CS2015
        {
            get
            {
                if (_CS2015 == null)
                    _CS2015 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2015, CSharpErrorIdentifiers.CS2015);
                return _CS2015;
            }
        }
        private static ICSharpCompilerReferenceError _CS2015;

        /// <summary><para>C&#9839; compiler error &#35;2016:</para><para>Code page {0} is invalid or not installed</para></summary>
        public static ICSharpCompilerReferenceError CS2016
        {
            get
            {
                if (_CS2016 == null)
                    _CS2016 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2016, CSharpErrorIdentifiers.CS2016);
                return _CS2016;
            }
        }
        private static ICSharpCompilerReferenceError _CS2016;

        /// <summary><para>C&#9839; compiler error &#35;2017:</para><para>Cannot specify /main if building a module or library</para></summary>
        public static ICSharpCompilerReferenceError CS2017
        {
            get
            {
                if (_CS2017 == null)
                    _CS2017 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2017, CSharpErrorIdentifiers.CS2017);
                return _CS2017;
            }
        }
        private static ICSharpCompilerReferenceError _CS2017;

        /// <summary><para>C&#9839; compiler error &#35;2018:</para><para>Unable to find messages file 'cscmsgs.dll'</para></summary>
        public static ICSharpCompilerReferenceError CS2018
        {
            get
            {
                if (_CS2018 == null)
                    _CS2018 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2018, CSharpErrorIdentifiers.CS2018);
                return _CS2018;
            }
        }
        private static ICSharpCompilerReferenceError _CS2018;

        /// <summary><para>C&#9839; compiler error &#35;2019:</para><para>Invalid target type for /target: must specify 'exe', 'winexe', 'library', or 'module'</para></summary>
        public static ICSharpCompilerReferenceError CS2019
        {
            get
            {
                if (_CS2019 == null)
                    _CS2019 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2019, CSharpErrorIdentifiers.CS2019);
                return _CS2019;
            }
        }
        private static ICSharpCompilerReferenceError _CS2019;

        /// <summary><para>C&#9839; compiler error &#35;2020:</para><para>Only the first set of input files can build a target other than 'module'</para></summary>
        public static ICSharpCompilerReferenceError CS2020
        {
            get
            {
                if (_CS2020 == null)
                    _CS2020 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2020, CSharpErrorIdentifiers.CS2020);
                return _CS2020;
            }
        }
        private static ICSharpCompilerReferenceError _CS2020;

        /// <summary><para>C&#9839; compiler error &#35;2021:</para><para>File name {0} is too long or invalid</para></summary>
        public static ICSharpCompilerReferenceError CS2021
        {
            get
            {
                if (_CS2021 == null)
                    _CS2021 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2021, CSharpErrorIdentifiers.CS2021);
                return _CS2021;
            }
        }
        private static ICSharpCompilerReferenceError _CS2021;

        /// <summary><para>C&#9839; compiler error &#35;2022:</para><para>Options '/out' and '/target' must appear before source file names</para></summary>
        public static ICSharpCompilerReferenceError CS2022
        {
            get
            {
                if (_CS2022 == null)
                    _CS2022 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2022, CSharpErrorIdentifiers.CS2022);
                return _CS2022;
            }
        }
        private static ICSharpCompilerReferenceError _CS2022;

        /// <summary><para>C&#9839; compiler error &#35;2024:</para><para>Invalid file section alignment number '#'</para></summary>
        public static ICSharpCompilerReferenceError CS2024
        {
            get
            {
                if (_CS2024 == null)
                    _CS2024 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2024, CSharpErrorIdentifiers.CS2024);
                return _CS2024;
            }
        }
        private static ICSharpCompilerReferenceError _CS2024;

        /// <summary><para>C&#9839; compiler error &#35;2032:</para><para>Character {0} is not allowed on the command-line or in response files</para></summary>
        public static ICSharpCompilerReferenceError CS2032
        {
            get
            {
                if (_CS2032 == null)
                    _CS2032 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2032, CSharpErrorIdentifiers.CS2032);
                return _CS2032;
            }
        }
        private static ICSharpCompilerReferenceError _CS2032;

        /// <summary><para>C&#9839; compiler error &#35;2033:</para><para>Cannot create short filename {0} when a long filename with the same short filename already exists </para></summary>
        public static ICSharpCompilerReferenceError CS2033
        {
            get
            {
                if (_CS2033 == null)
                    _CS2033 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2033, CSharpErrorIdentifiers.CS2033);
                return _CS2033;
            }
        }
        private static ICSharpCompilerReferenceError _CS2033;

        /// <summary><para>C&#9839; compiler error &#35;2034:</para><para>A /reference option that declares an extern alias can only have one filename. To specify multiple aliases or filenames, use multiple /reference options.</para></summary>
        public static ICSharpCompilerReferenceError CS2034
        {
            get
            {
                if (_CS2034 == null)
                    _CS2034 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2034, CSharpErrorIdentifiers.CS2034);
                return _CS2034;
            }
        }
        private static ICSharpCompilerReferenceError _CS2034;

        /// <summary><para>C&#9839; compiler error &#35;2035:</para><para>Command-line syntax error: Missing ':&lt;number&gt;' for {0} option</para></summary>
        public static ICSharpCompilerReferenceError CS2035
        {
            get
            {
                if (_CS2035 == null)
                    _CS2035 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2035, CSharpErrorIdentifiers.CS2035);
                return _CS2035;
            }
        }
        private static ICSharpCompilerReferenceError _CS2035;

        /// <summary><para>C&#9839; compiler error &#35;2036:</para><para>The /pdb option requires that the /debug option also be used.</para></summary>
        public static ICSharpCompilerReferenceError CS2036
        {
            get
            {
                if (_CS2036 == null)
                    _CS2036 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS2036, CSharpErrorIdentifiers.CS2036);
                return _CS2036;
            }
        }
        private static ICSharpCompilerReferenceError _CS2036;

        /// <summary><para>C&#9839; compiler error &#35;5001:</para><para>Program {0} does not contain a static 'Main' method suitable for an entry point</para></summary>
        public static ICSharpCompilerReferenceError CS5001
        {
            get
            {
                if (_CS5001 == null)
                    _CS5001 = new CSharpCompilerReferenceError(Resources.CSharpErrors_CS5001, CSharpErrorIdentifiers.CS5001);
                return _CS5001;
            }
        }
        private static ICSharpCompilerReferenceError _CS5001;

    }
}
