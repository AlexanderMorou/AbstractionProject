using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Languages.CSharp.Properties;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public static class CSharpCompilerMessages
    {
        /// <summary><para>C&#9839; compiler warning (level 4) &#35;28:</para><para>{0} has the wrong signature to be an entry point </para></summary>
        public static ICompilerReferenceWarning CS0028
        {
            get
            {
                if (_CS0028 == null)
                    _CS0028 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0028, 4, 28);
                return _CS0028;
            }
        }
        private static ICompilerReferenceWarning _CS0028;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;67:</para><para>The event {0} is never used</para></summary>
        public static ICompilerReferenceWarning CS0067
        {
            get
            {
                if (_CS0067 == null)
                    _CS0067 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0067, 3, 67);
                return _CS0067;
            }
        }
        private static ICompilerReferenceWarning _CS0067;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;78:</para><para>The 'l' suffix is easily confused with the digit '1' -- use 'L' for clarity</para></summary>
        public static ICompilerReferenceWarning CS0078
        {
            get
            {
                if (_CS0078 == null)
                    _CS0078 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0078, 4, 78);
                return _CS0078;
            }
        }
        private static ICompilerReferenceWarning _CS0078;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;105:</para><para>The using directive for {0} appeared previously in this namespace</para></summary>
        public static ICompilerReferenceWarning CS0105
        {
            get
            {
                if (_CS0105 == null)
                    _CS0105 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0105, 3, 105);
                return _CS0105;
            }
        }
        private static ICompilerReferenceWarning _CS0105;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;108:</para><para>{0} hides inherited member {1}. Use the new keyword if hiding was intended.</para></summary>
        public static ICompilerReferenceWarning CS0108
        {
            get
            {
                if (_CS0108 == null)
                    _CS0108 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0108, 2, 108);
                return _CS0108;
            }
        }
        private static ICompilerReferenceWarning _CS0108;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;109:</para><para>The member {0} does not hide an inherited member. The new keyword is not required</para></summary>
        public static ICompilerReferenceWarning CS0109
        {
            get
            {
                if (_CS0109 == null)
                    _CS0109 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0109, 4, 109);
                return _CS0109;
            }
        }
        private static ICompilerReferenceWarning _CS0109;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;114:</para><para>{0} hides inherited member {1}. To make the current method override that implementation, add the override keyword. Otherwise add the new keyword.</para></summary>
        public static ICompilerReferenceWarning CS0114
        {
            get
            {
                if (_CS0114 == null)
                    _CS0114 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0114, 2, 114);
                return _CS0114;
            }
        }
        private static ICompilerReferenceWarning _CS0114;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;162:</para><para>Unreachable code detected</para></summary>
        public static ICompilerReferenceWarning CS0162
        {
            get
            {
                if (_CS0162 == null)
                    _CS0162 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0162, 2, 162);
                return _CS0162;
            }
        }
        private static ICompilerReferenceWarning _CS0162;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;164:</para><para>This label has not been referenced</para></summary>
        public static ICompilerReferenceWarning CS0164
        {
            get
            {
                if (_CS0164 == null)
                    _CS0164 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0164, 2, 164);
                return _CS0164;
            }
        }
        private static ICompilerReferenceWarning _CS0164;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;168:</para><para>The variable {0} is assigned but its value is never used</para></summary>
        public static ICompilerReferenceWarning CS0168
        {
            get
            {
                if (_CS0168 == null)
                    _CS0168 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0168, 3, 168);
                return _CS0168;
            }
        }
        private static ICompilerReferenceWarning _CS0168;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;169:</para><para>The private field {0} is never used</para></summary>
        public static ICompilerReferenceWarning CS0169
        {
            get
            {
                if (_CS0169 == null)
                    _CS0169 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0169, 3, 169);
                return _CS0169;
            }
        }
        private static ICompilerReferenceWarning _CS0169;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;183:</para><para>The given expression is always of the provided ({0}) type</para></summary>
        public static ICompilerReferenceWarning CS0183
        {
            get
            {
                if (_CS0183 == null)
                    _CS0183 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0183, 1, 183);
                return _CS0183;
            }
        }
        private static ICompilerReferenceWarning _CS0183;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;184:</para><para>The given expression is never of the provided ({0}) type</para></summary>
        public static ICompilerReferenceWarning CS0184
        {
            get
            {
                if (_CS0184 == null)
                    _CS0184 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0184, 1, 184);
                return _CS0184;
            }
        }
        private static ICompilerReferenceWarning _CS0184;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;197:</para><para>Passing {0} as ref or out or taking its address may cause a runtime exception because it is a field of a marshal-by-reference class</para></summary>
        public static ICompilerReferenceWarning CS0197
        {
            get
            {
                if (_CS0197 == null)
                    _CS0197 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0197, 1, 197);
                return _CS0197;
            }
        }
        private static ICompilerReferenceWarning _CS0197;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;219:</para><para>The variable {0} is assigned but its value is never used</para></summary>
        public static ICompilerReferenceWarning CS0219
        {
            get
            {
                if (_CS0219 == null)
                    _CS0219 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0219, 3, 219);
                return _CS0219;
            }
        }
        private static ICompilerReferenceWarning _CS0219;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;251:</para><para>Indexing an array with a negative index (array indices always start at zero)</para></summary>
        public static ICompilerReferenceWarning CS0251
        {
            get
            {
                if (_CS0251 == null)
                    _CS0251 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0251, 2, 251);
                return _CS0251;
            }
        }
        private static ICompilerReferenceWarning _CS0251;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;252:</para><para>Possible unintended reference comparison; to get a value comparison, cast the left hand side to type {0}</para></summary>
        public static ICompilerReferenceWarning CS0252
        {
            get
            {
                if (_CS0252 == null)
                    _CS0252 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0252, 2, 252);
                return _CS0252;
            }
        }
        private static ICompilerReferenceWarning _CS0252;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;253:</para><para>Possible unintended reference comparison; to get a value comparison, cast the right hand side to type {0}</para></summary>
        public static ICompilerReferenceWarning CS0253
        {
            get
            {
                if (_CS0253 == null)
                    _CS0253 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0253, 2, 253);
                return _CS0253;
            }
        }
        private static ICompilerReferenceWarning _CS0253;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;278:</para><para>{0} does not implement the {1} pattern. {2} is ambiguous with {2}.</para></summary>
        public static ICompilerReferenceWarning CS0278
        {
            get
            {
                if (_CS0278 == null)
                    _CS0278 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0278, 2, 278);
                return _CS0278;
            }
        }
        private static ICompilerReferenceWarning _CS0278;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;279:</para><para>{0} does not implement the {1} pattern. {2} is either static or not public.</para></summary>
        public static ICompilerReferenceWarning CS0279
        {
            get
            {
                if (_CS0279 == null)
                    _CS0279 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0279, 2, 279);
                return _CS0279;
            }
        }
        private static ICompilerReferenceWarning _CS0279;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;280:</para><para>{0} does not implement the {1} pattern. {2} has the wrong signature.</para></summary>
        public static ICompilerReferenceWarning CS0280
        {
            get
            {
                if (_CS0280 == null)
                    _CS0280 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0280, 2, 280);
                return _CS0280;
            }
        }
        private static ICompilerReferenceWarning _CS0280;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;282:</para><para>There is no defined ordering between fields in multiple declarations of partial class or struct {0}. To specify an ordering, all instance fields must be in the same declaration.</para></summary>
        public static ICompilerReferenceWarning CS0282
        {
            get
            {
                if (_CS0282 == null)
                    _CS0282 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0282, 3, 282);
                return _CS0282;
            }
        }
        private static ICompilerReferenceWarning _CS0282;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;402:</para><para>{0} : an entry point cannot be generic or in a generic type</para></summary>
        public static ICompilerReferenceWarning CS0402
        {
            get
            {
                if (_CS0402 == null)
                    _CS0402 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0402, 4, 402);
                return _CS0402;
            }
        }
        private static ICompilerReferenceWarning _CS0402;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;414:</para><para>The private field {0} is assigned but its value is never used</para></summary>
        public static ICompilerReferenceWarning CS0414
        {
            get
            {
                if (_CS0414 == null)
                    _CS0414 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0414, 3, 414);
                return _CS0414;
            }
        }
        private static ICompilerReferenceWarning _CS0414;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;419:</para><para>Ambiguous reference in cref attribute: {0}. Assuming {1}, but could have also matched other overloads including {2}.</para></summary>
        public static ICompilerReferenceWarning CS0419
        {
            get
            {
                if (_CS0419 == null)
                    _CS0419 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0419, 3, 419);
                return _CS0419;
            }
        }
        private static ICompilerReferenceWarning _CS0419;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;420:</para><para>{0}: a reference to a volatile field will not be treated as volatile</para></summary>
        public static ICompilerReferenceWarning CS0420
        {
            get
            {
                if (_CS0420 == null)
                    _CS0420 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0420, 1, 420);
                return _CS0420;
            }
        }
        private static ICompilerReferenceWarning _CS0420;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;422:</para><para>The /incremental option is no longer supported</para></summary>
        public static ICompilerReferenceWarning CS0422
        {
            get
            {
                if (_CS0422 == null)
                    _CS0422 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0422, 4, 422);
                return _CS0422;
            }
        }
        private static ICompilerReferenceWarning _CS0422;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;429:</para><para>Unreachable expression code detected </para></summary>
        public static ICompilerReferenceWarning CS0429
        {
            get
            {
                if (_CS0429 == null)
                    _CS0429 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0429, 4, 429);
                return _CS0429;
            }
        }
        private static ICompilerReferenceWarning _CS0429;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;435:</para><para>The namespace {0} in {1} conflicts with the imported type {2} in {3}. Using the namespace defined in {1}..</para></summary>
        public static ICompilerReferenceWarning CS0435
        {
            get
            {
                if (_CS0435 == null)
                    _CS0435 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0435, 2, 435);
                return _CS0435;
            }
        }
        private static ICompilerReferenceWarning _CS0435;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;436:</para><para>The type {0} in {1} conflicts with the imported type {2} in {3}. Using the type defined in {1}.</para></summary>
        public static ICompilerReferenceWarning CS0436
        {
            get
            {
                if (_CS0436 == null)
                    _CS0436 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0436, 2, 436);
                return _CS0436;
            }
        }
        private static ICompilerReferenceWarning _CS0436;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;437:</para><para>The type {0} in {1} conflicts with the imported namespace {2} in {3}. Using the type defined in {1}.</para></summary>
        public static ICompilerReferenceWarning CS0437
        {
            get
            {
                if (_CS0437 == null)
                    _CS0437 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0437, 2, 437);
                return _CS0437;
            }
        }
        private static ICompilerReferenceWarning _CS0437;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;440:</para><para>Defining an alias named 'global' is ill-advised since 'global::' always references the global namespace and not an alias</para></summary>
        public static ICompilerReferenceWarning CS0440
        {
            get
            {
                if (_CS0440 == null)
                    _CS0440 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0440, 2, 440);
                return _CS0440;
            }
        }
        private static ICompilerReferenceWarning _CS0440;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;444:</para><para>Predefined type {0} was not found in {1} but was found in {2}</para></summary>
        public static ICompilerReferenceWarning CS0444
        {
            get
            {
                if (_CS0444 == null)
                    _CS0444 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0444, 2, 444);
                return _CS0444;
            }
        }
        private static ICompilerReferenceWarning _CS0444;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;458:</para><para>The result of the expression is always 'null' of type {0}</para></summary>
        public static ICompilerReferenceWarning CS0458
        {
            get
            {
                if (_CS0458 == null)
                    _CS0458 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0458, 2, 458);
                return _CS0458;
            }
        }
        private static ICompilerReferenceWarning _CS0458;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;464:</para><para>Comparing with null of type {0} always produces 'false'</para></summary>
        public static ICompilerReferenceWarning CS0464
        {
            get
            {
                if (_CS0464 == null)
                    _CS0464 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0464, 2, 464);
                return _CS0464;
            }
        }
        private static ICompilerReferenceWarning _CS0464;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;465:</para><para>Introducing a 'Finalize' method can interfere with destructor invocation. Did you intend to declare a destructor?</para></summary>
        public static ICompilerReferenceWarning CS0465
        {
            get
            {
                if (_CS0465 == null)
                    _CS0465 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0465, 1, 465);
                return _CS0465;
            }
        }
        private static ICompilerReferenceWarning _CS0465;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;467:</para><para>Ambiguity between method {0} and non-method {1}. Using method group.</para></summary>
        public static ICompilerReferenceWarning CS0467
        {
            get
            {
                if (_CS0467 == null)
                    _CS0467 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0467, 2, 467);
                return _CS0467;
            }
        }
        private static ICompilerReferenceWarning _CS0467;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;469:</para><para>The {0} value is not implicitly convertible to type {1}</para></summary>
        public static ICompilerReferenceWarning CS0469
        {
            get
            {
                if (_CS0469 == null)
                    _CS0469 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0469, 2, 469);
                return _CS0469;
            }
        }
        private static ICompilerReferenceWarning _CS0469;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;472:</para><para>The result of the expression is always {0} since a value of type {1} is never equal to 'null' of type {1}</para></summary>
        public static ICompilerReferenceWarning CS0472
        {
            get
            {
                if (_CS0472 == null)
                    _CS0472 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0472, 2, 472);
                return _CS0472;
            }
        }
        private static ICompilerReferenceWarning _CS0472;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;602:</para><para>The feature {0} is deprecated. Please use {1} instead</para></summary>
        public static ICompilerReferenceWarning CS0602
        {
            get
            {
                if (_CS0602 == null)
                    _CS0602 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0602, 1, 602);
                return _CS0602;
            }
        }
        private static ICompilerReferenceWarning _CS0602;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;612:</para><para>{0} is obsolete</para></summary>
        public static ICompilerReferenceWarning CS0612
        {
            get
            {
                if (_CS0612 == null)
                    _CS0612 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0612, 1, 612);
                return _CS0612;
            }
        }
        private static ICompilerReferenceWarning _CS0612;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;618:</para><para>{0} is obsolete: {1}</para></summary>
        public static ICompilerReferenceWarning CS0618
        {
            get
            {
                if (_CS0618 == null)
                    _CS0618 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0618, 2, 618);
                return _CS0618;
            }
        }
        private static ICompilerReferenceWarning _CS0618;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;626:</para><para>Method, operator, or accessor {0} is marked external and has no attributes on it. Consider adding a DllImport attribute to specify the external implementation</para></summary>
        public static ICompilerReferenceWarning CS0626
        {
            get
            {
                if (_CS0626 == null)
                    _CS0626 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0626, 1, 626);
                return _CS0626;
            }
        }
        private static ICompilerReferenceWarning _CS0626;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;628:</para><para>{0} : new protected member declared in sealed class</para></summary>
        public static ICompilerReferenceWarning CS0628
        {
            get
            {
                if (_CS0628 == null)
                    _CS0628 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0628, 4, 628);
                return _CS0628;
            }
        }
        private static ICompilerReferenceWarning _CS0628;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;642:</para><para>Possible mistaken empty statement</para></summary>
        public static ICompilerReferenceWarning CS0642
        {
            get
            {
                if (_CS0642 == null)
                    _CS0642 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0642, 3, 642);
                return _CS0642;
            }
        }
        private static ICompilerReferenceWarning _CS0642;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;649:</para><para>Field {0} is never assigned to, and will always have its default value {1}</para></summary>
        public static ICompilerReferenceWarning CS0649
        {
            get
            {
                if (_CS0649 == null)
                    _CS0649 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0649, 4, 649);
                return _CS0649;
            }
        }
        private static ICompilerReferenceWarning _CS0649;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;652:</para><para>Comparison to integral constant is useless; the constant is outside the range of type {0}</para></summary>
        public static ICompilerReferenceWarning CS0652
        {
            get
            {
                if (_CS0652 == null)
                    _CS0652 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0652, 2, 652);
                return _CS0652;
            }
        }
        private static ICompilerReferenceWarning _CS0652;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;657:</para><para>{0} is not a valid attribute location for this declaration. Valid attribute locations for this declaration are {1}. All attributes in this block will be ignored.</para></summary>
        public static ICompilerReferenceWarning CS0657
        {
            get
            {
                if (_CS0657 == null)
                    _CS0657 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0657, 1, 657);
                return _CS0657;
            }
        }
        private static ICompilerReferenceWarning _CS0657;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;658:</para><para>{0} is not a recognized attribute location. All attributes in this block will be ignored.</para></summary>
        public static ICompilerReferenceWarning CS0658
        {
            get
            {
                if (_CS0658 == null)
                    _CS0658 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0658, 1, 658);
                return _CS0658;
            }
        }
        private static ICompilerReferenceWarning _CS0658;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;659:</para><para>{0} overrides Object.Equals(object o) but does not override Object.GetHashCode()</para></summary>
        public static ICompilerReferenceWarning CS0659
        {
            get
            {
                if (_CS0659 == null)
                    _CS0659 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0659, 3, 659);
                return _CS0659;
            }
        }
        private static ICompilerReferenceWarning _CS0659;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;660:</para><para>{0} defines operator == or operator != but does not override Object.Equals(object o)</para></summary>
        public static ICompilerReferenceWarning CS0660
        {
            get
            {
                if (_CS0660 == null)
                    _CS0660 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0660, 3, 660);
                return _CS0660;
            }
        }
        private static ICompilerReferenceWarning _CS0660;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;661:</para><para>{0} defines operator == or operator != but does not override Object.GetHashCode()</para></summary>
        public static ICompilerReferenceWarning CS0661
        {
            get
            {
                if (_CS0661 == null)
                    _CS0661 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0661, 3, 661);
                return _CS0661;
            }
        }
        private static ICompilerReferenceWarning _CS0661;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;665:</para><para>Assignment in conditional expression is always constant; did you mean to use '==' instead of '='?</para></summary>
        public static ICompilerReferenceWarning CS0665
        {
            get
            {
                if (_CS0665 == null)
                    _CS0665 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0665, 3, 665);
                return _CS0665;
            }
        }
        private static ICompilerReferenceWarning _CS0665;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;672:</para><para>Member {0} overrides obsolete member '{1}. Add the Obsolete attribute to {0}</para></summary>
        public static ICompilerReferenceWarning CS0672
        {
            get
            {
                if (_CS0672 == null)
                    _CS0672 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0672, 1, 672);
                return _CS0672;
            }
        }
        private static ICompilerReferenceWarning _CS0672;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;675:</para><para>Bitwise-or operator used on a sign-extended operand; consider casting to a smaller unsigned type first</para></summary>
        public static ICompilerReferenceWarning CS0675
        {
            get
            {
                if (_CS0675 == null)
                    _CS0675 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0675, 3, 675);
                return _CS0675;
            }
        }
        private static ICompilerReferenceWarning _CS0675;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;693:</para><para>Type parameter {0} has the same name as the type parameter from outer type {1}</para></summary>
        public static ICompilerReferenceWarning CS0693
        {
            get
            {
                if (_CS0693 == null)
                    _CS0693 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0693, 3, 693);
                return _CS0693;
            }
        }
        private static ICompilerReferenceWarning _CS0693;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;728:</para><para>Possibly incorrect assignment to local {0} which is the argument to a using or lock statement. The Dispose call or unlocking will happen on the original value of the local.</para></summary>
        public static ICompilerReferenceWarning CS0728
        {
            get
            {
                if (_CS0728 == null)
                    _CS0728 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0728, 2, 728);
                return _CS0728;
            }
        }
        private static ICompilerReferenceWarning _CS0728;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;809:</para><para>Obsolete member {0} overrides non-obsolete member {1}.</para></summary>
        public static ICompilerReferenceWarning CS0809
        {
            get
            {
                if (_CS0809 == null)
                    _CS0809 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0809, 1, 809);
                return _CS0809;
            }
        }
        private static ICompilerReferenceWarning _CS0809;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;824:</para><para>Constructor {0} is marked external.</para></summary>
        public static ICompilerReferenceWarning CS0824
        {
            get
            {
                if (_CS0824 == null)
                    _CS0824 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS0824, 1, 824);
                return _CS0824;
            }
        }
        private static ICompilerReferenceWarning _CS0824;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1030:</para><para>#warning: {0}</para></summary>
        public static ICompilerReferenceWarning CS1030
        {
            get
            {
                if (_CS1030 == null)
                    _CS1030 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1030, 1, 1030);
                return _CS1030;
            }
        }
        private static ICompilerReferenceWarning _CS1030;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1058:</para><para>A previous catch clause already catches all exceptions. All exceptions thrown will be wrapped in a System.Runtime.CompilerServices.RuntimeWrappedException</para></summary>
        public static ICompilerReferenceWarning CS1058
        {
            get
            {
                if (_CS1058 == null)
                    _CS1058 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1058, 1, 1058);
                return _CS1058;
            }
        }
        private static ICompilerReferenceWarning _CS1058;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1060:</para><para>Use of possibly unassigned field 'name'. Struct instance variables are initially unassigned if struct is unassigned.</para></summary>
        public static ICompilerReferenceWarning CS1060
        {
            get
            {
                if (_CS1060 == null)
                    _CS1060 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1060, 1, 1060);
                return _CS1060;
            }
        }
        private static ICompilerReferenceWarning _CS1060;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1522:</para><para>Empty switch block</para></summary>
        public static ICompilerReferenceWarning CS1522
        {
            get
            {
                if (_CS1522 == null)
                    _CS1522 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1522, 1, 1522);
                return _CS1522;
            }
        }
        private static ICompilerReferenceWarning _CS1522;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1570:</para><para>XML comment on {0} has badly formed XML — {1}</para></summary>
        public static ICompilerReferenceWarning CS1570
        {
            get
            {
                if (_CS1570 == null)
                    _CS1570 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1570, 1, 1570);
                return _CS1570;
            }
        }
        private static ICompilerReferenceWarning _CS1570;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1571:</para><para>XML comment on {0} has a duplicate param tag for {1}</para></summary>
        public static ICompilerReferenceWarning CS1571
        {
            get
            {
                if (_CS1571 == null)
                    _CS1571 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1571, 2, 1571);
                return _CS1571;
            }
        }
        private static ICompilerReferenceWarning _CS1571;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1572:</para><para>XML comment on {0} has a param tag for {1}, but there is no parameter by that name</para></summary>
        public static ICompilerReferenceWarning CS1572
        {
            get
            {
                if (_CS1572 == null)
                    _CS1572 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1572, 2, 1572);
                return _CS1572;
            }
        }
        private static ICompilerReferenceWarning _CS1572;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;1573:</para><para>Parameter {0} has no matching param tag in the XML comment for {0} (but other parameters do)</para></summary>
        public static ICompilerReferenceWarning CS1573
        {
            get
            {
                if (_CS1573 == null)
                    _CS1573 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1573, 4, 1573);
                return _CS1573;
            }
        }
        private static ICompilerReferenceWarning _CS1573;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1574:</para><para>XML comment on {0} has syntactically incorrect cref attribute {1}</para></summary>
        public static ICompilerReferenceWarning CS1574
        {
            get
            {
                if (_CS1574 == null)
                    _CS1574 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1574, 1, 1574);
                return _CS1574;
            }
        }
        private static ICompilerReferenceWarning _CS1574;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1580:</para><para>Invalid type for parameter {0} in XML comment cref attribute</para></summary>
        public static ICompilerReferenceWarning CS1580
        {
            get
            {
                if (_CS1580 == null)
                    _CS1580 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1580, 1, 1580);
                return _CS1580;
            }
        }
        private static ICompilerReferenceWarning _CS1580;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1581:</para><para>Invalid return type in XML comment cref attribute</para></summary>
        public static ICompilerReferenceWarning CS1581
        {
            get
            {
                if (_CS1581 == null)
                    _CS1581 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1581, 1, 1581);
                return _CS1581;
            }
        }
        private static ICompilerReferenceWarning _CS1581;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1584:</para><para>XML comment on {0} has syntactically incorrect cref attribute {1}</para></summary>
        public static ICompilerReferenceWarning CS1584
        {
            get
            {
                if (_CS1584 == null)
                    _CS1584 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1584, 1, 1584);
                return _CS1584;
            }
        }
        private static ICompilerReferenceWarning _CS1584;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1587:</para><para>XML comment is not placed on a valid language element</para></summary>
        public static ICompilerReferenceWarning CS1587
        {
            get
            {
                if (_CS1587 == null)
                    _CS1587 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1587, 2, 1587);
                return _CS1587;
            }
        }
        private static ICompilerReferenceWarning _CS1587;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1589:</para><para>Unable to include XML fragment {0} of file {1} -- {1}</para></summary>
        public static ICompilerReferenceWarning CS1589
        {
            get
            {
                if (_CS1589 == null)
                    _CS1589 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1589, 1, 1589);
                return _CS1589;
            }
        }
        private static ICompilerReferenceWarning _CS1589;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1590:</para><para>Invalid XML include element -- Missing file attribute</para></summary>
        public static ICompilerReferenceWarning CS1590
        {
            get
            {
                if (_CS1590 == null)
                    _CS1590 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1590, 1, 1590);
                return _CS1590;
            }
        }
        private static ICompilerReferenceWarning _CS1590;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;1591:</para><para>Missing XML comment for publicly visible type or member {0}</para></summary>
        public static ICompilerReferenceWarning CS1591
        {
            get
            {
                if (_CS1591 == null)
                    _CS1591 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1591, 4, 1591);
                return _CS1591;
            }
        }
        private static ICompilerReferenceWarning _CS1591;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1592:</para><para>Badly formed XML in included comments file -- {0}</para></summary>
        public static ICompilerReferenceWarning CS1592
        {
            get
            {
                if (_CS1592 == null)
                    _CS1592 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1592, 1, 1592);
                return _CS1592;
            }
        }
        private static ICompilerReferenceWarning _CS1592;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1598:</para><para>XML parser could not be loaded for the following reason: {0}. The XML documentation file {1} will not be generated.</para></summary>
        public static ICompilerReferenceWarning CS1598
        {
            get
            {
                if (_CS1598 == null)
                    _CS1598 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1598, 1, 1598);
                return _CS1598;
            }
        }
        private static ICompilerReferenceWarning _CS1598;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1607:</para><para>Assembly generation -- {0}</para></summary>
        public static ICompilerReferenceWarning CS1607
        {
            get
            {
                if (_CS1607 == null)
                    _CS1607 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1607, 1, 1607);
                return _CS1607;
            }
        }
        private static ICompilerReferenceWarning _CS1607;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;1610:</para><para>Unable to delete temporary file {0} used for default Win32 resource -- {1}</para></summary>
        public static ICompilerReferenceWarning CS1610
        {
            get
            {
                if (_CS1610 == null)
                    _CS1610 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1610, 4, 1610);
                return _CS1610;
            }
        }
        private static ICompilerReferenceWarning _CS1610;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1616:</para><para>Option {0} overrides attribute {1} given in a source file or added module</para></summary>
        public static ICompilerReferenceWarning CS1616
        {
            get
            {
                if (_CS1616 == null)
                    _CS1616 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1616, 1, 1616);
                return _CS1616;
            }
        }
        private static ICompilerReferenceWarning _CS1616;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1633:</para><para>Unrecognized #pragma directive</para></summary>
        public static ICompilerReferenceWarning CS1633
        {
            get
            {
                if (_CS1633 == null)
                    _CS1633 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1633, 1, 1633);
                return _CS1633;
            }
        }
        private static ICompilerReferenceWarning _CS1633;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1634:</para><para>Expected disable or restore</para></summary>
        public static ICompilerReferenceWarning CS1634
        {
            get
            {
                if (_CS1634 == null)
                    _CS1634 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1634, 1, 1634);
                return _CS1634;
            }
        }
        private static ICompilerReferenceWarning _CS1634;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1635:</para><para>Cannot restore warning {0} because it was disabled globally</para></summary>
        public static ICompilerReferenceWarning CS1635
        {
            get
            {
                if (_CS1635 == null)
                    _CS1635 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1635, 1, 1635);
                return _CS1635;
            }
        }
        private static ICompilerReferenceWarning _CS1635;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1645:</para><para>Feature {0} is not part of the standardized ISO C# language specification, and may not be accepted by other compilers</para></summary>
        public static ICompilerReferenceWarning CS1645
        {
            get
            {
                if (_CS1645 == null)
                    _CS1645 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1645, 1, 1645);
                return _CS1645;
            }
        }
        private static ICompilerReferenceWarning _CS1645;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1658:</para><para>{0}. See also error: {1}</para></summary>
        public static ICompilerReferenceWarning CS1658
        {
            get
            {
                if (_CS1658 == null)
                    _CS1658 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1658, 1, 1658);
                return _CS1658;
            }
        }
        private static ICompilerReferenceWarning _CS1658;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1668:</para><para>Invalid search path 'path' specified in {0} -- {1}</para></summary>
        public static ICompilerReferenceWarning CS1668
        {
            get
            {
                if (_CS1668 == null)
                    _CS1668 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1668, 2, 1668);
                return _CS1668;
            }
        }
        private static ICompilerReferenceWarning _CS1668;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1682:</para><para>Reference to type {0} claims it is nested within {1}, but it could not be found</para></summary>
        public static ICompilerReferenceWarning CS1682
        {
            get
            {
                if (_CS1682 == null)
                    _CS1682 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1682, 1, 1682);
                return _CS1682;
            }
        }
        private static ICompilerReferenceWarning _CS1682;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1683:</para><para>Reference to type {0} claims it is defined in this assembly, but it is not defined in source or any added modules</para></summary>
        public static ICompilerReferenceWarning CS1683
        {
            get
            {
                if (_CS1683 == null)
                    _CS1683 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1683, 1, 1683);
                return _CS1683;
            }
        }
        private static ICompilerReferenceWarning _CS1683;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1684:</para><para>Reference to type {0} claims it is defined in {1}, but it could not be found</para></summary>
        public static ICompilerReferenceWarning CS1684
        {
            get
            {
                if (_CS1684 == null)
                    _CS1684 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1684, 1, 1684);
                return _CS1684;
            }
        }
        private static ICompilerReferenceWarning _CS1684;

        //System variation of CS0436
        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1685:</para><para>The predefined type {0} is defined in multiple assemblies in the global alias; using definition from {1}</para></summary>
        public static ICompilerReferenceWarning CS1685
        {
            get
            {
                if (_CS1685 == null)
                    _CS1685 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1685, 1, 1685);
                return _CS1685;
            }
        }
        private static ICompilerReferenceWarning _CS1685;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1687:</para><para>Source file has exceeded the limit of 16,707,565 lines representable in the PDB, debug information will be incorrect</para></summary>
        public static ICompilerReferenceWarning CS1687
        {
            get
            {
                if (_CS1687 == null)
                    _CS1687 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1687, 1, 1687);
                return _CS1687;
            }
        }
        private static ICompilerReferenceWarning _CS1687;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1690:</para><para>Accessing a member on {0} may cause a runtime exception because it is a field of a marshal-by-reference class</para></summary>
        public static ICompilerReferenceWarning CS1690
        {
            get
            {
                if (_CS1690 == null)
                    _CS1690 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1690, 1, 1690);
                return _CS1690;
            }
        }
        private static ICompilerReferenceWarning _CS1690;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1691:</para><para>{0} is not a valid warning number</para></summary>
        public static ICompilerReferenceWarning CS1691
        {
            get
            {
                if (_CS1691 == null)
                    _CS1691 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1691, 1, 1691);
                return _CS1691;
            }
        }
        private static ICompilerReferenceWarning _CS1691;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1692:</para><para>Invalid number</para></summary>
        public static ICompilerReferenceWarning CS1692
        {
            get
            {
                if (_CS1692 == null)
                    _CS1692 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1692, 1, 1692);
                return _CS1692;
            }
        }
        private static ICompilerReferenceWarning _CS1692;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1694:</para><para>Invalid filename specified for preprocessor directive. Filename is too long or not a valid filename.</para></summary>
        public static ICompilerReferenceWarning CS1694
        {
            get
            {
                if (_CS1694 == null)
                    _CS1694 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1694, 1, 1694);
                return _CS1694;
            }
        }
        private static ICompilerReferenceWarning _CS1694;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1695:</para><para>Invalid #pragma checksum syntax; should be #pragma checksum ""filename"" ""{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}"" ""XXXX...""</para></summary>
        public static ICompilerReferenceWarning CS1695
        {
            get
            {
                if (_CS1695 == null)
                    _CS1695 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1695, 1, 1695);
                return _CS1695;
            }
        }
        private static ICompilerReferenceWarning _CS1695;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1696:</para><para>Single-line comment or end-of-line expected</para></summary>
        public static ICompilerReferenceWarning CS1696
        {
            get
            {
                if (_CS1696 == null)
                    _CS1696 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1696, 1, 1696);
                return _CS1696;
            }
        }
        private static ICompilerReferenceWarning _CS1696;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1697:</para><para>Different checksum values given for {0}</para></summary>
        public static ICompilerReferenceWarning CS1697
        {
            get
            {
                if (_CS1697 == null)
                    _CS1697 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1697, 1, 1697);
                return _CS1697;
            }
        }
        private static ICompilerReferenceWarning _CS1697;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1698:</para><para>Circular assembly reference {0} does not match the output assembly name {1}. Try adding a reference to {0} or changing the output assembly name to match.</para></summary>
        public static ICompilerReferenceWarning CS1698
        {
            get
            {
                if (_CS1698 == null)
                    _CS1698 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1698, 2, 1698);
                return _CS1698;
            }
        }
        private static ICompilerReferenceWarning _CS1698;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1699:</para><para>Use command line option {0} or appropriate project settings instead of {1}</para></summary>
        public static ICompilerReferenceWarning CS1699
        {
            get
            {
                if (_CS1699 == null)
                    _CS1699 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1699, 1, 1699);
                return _CS1699;
            }
        }
        private static ICompilerReferenceWarning _CS1699;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;1700:</para><para>Assembly reference Assembly Name is invalid and cannot be resolved</para></summary>
        public static ICompilerReferenceWarning CS1700
        {
            get
            {
                if (_CS1700 == null)
                    _CS1700 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1700, 3, 1700);
                return _CS1700;
            }
        }
        private static ICompilerReferenceWarning _CS1700;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1701:</para><para>Assuming assembly reference {0} matches {1}, you may need to supply runtime policy </para></summary>
        public static ICompilerReferenceWarning CS1701
        {
            get
            {
                if (_CS1701 == null)
                    _CS1701 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1701, 2, 1701);
                return _CS1701;
            }
        }
        private static ICompilerReferenceWarning _CS1701;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;1702:</para><para>Assuming assembly reference {0} matches {1}, you may need to supply runtime policy</para></summary>
        public static ICompilerReferenceWarning CS1702
        {
            get
            {
                if (_CS1702 == null)
                    _CS1702 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1702, 3, 1702);
                return _CS1702;
            }
        }
        private static ICompilerReferenceWarning _CS1702;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1707:</para><para>Delegate {0} bound to {1} instead of {2} because of new language rules</para></summary>
        public static ICompilerReferenceWarning CS1707
        {
            get
            {
                if (_CS1707 == null)
                    _CS1707 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1707, 1, 1707);
                return _CS1707;
            }
        }
        private static ICompilerReferenceWarning _CS1707;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1709:</para><para>Filename specified for preprocessor directive is empty</para></summary>
        public static ICompilerReferenceWarning CS1709
        {
            get
            {
                if (_CS1709 == null)
                    _CS1709 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1709, 1, 1709);
                return _CS1709;
            }
        }
        private static ICompilerReferenceWarning _CS1709;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1710:</para><para>XML comment on {0} has a duplicate typeparam tag for {1}</para></summary>
        public static ICompilerReferenceWarning CS1710
        {
            get
            {
                if (_CS1710 == null)
                    _CS1710 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1710, 2, 1710);
                return _CS1710;
            }
        }
        private static ICompilerReferenceWarning _CS1710;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1711:</para><para>XML comment on {0} has a typeparam tag for {1}, but there is no type parameter by that name</para></summary>
        public static ICompilerReferenceWarning CS1711
        {
            get
            {
                if (_CS1711 == null)
                    _CS1711 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1711, 2, 1711);
                return _CS1711;
            }
        }
        private static ICompilerReferenceWarning _CS1711;

        /// <summary><para>C&#9839; compiler warning (level 4) &#35;1712:</para><para>Type parameter {0} has no matching typeparam tag in the XML comment on {1} (but other type parameters do)</para></summary>
        public static ICompilerReferenceWarning CS1712
        {
            get
            {
                if (_CS1712 == null)
                    _CS1712 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1712, 4, 1712);
                return _CS1712;
            }
        }
        private static ICompilerReferenceWarning _CS1712;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;1717:</para><para>Assignment made to same variable; did you mean to assign something else?</para></summary>
        public static ICompilerReferenceWarning CS1717
        {
            get
            {
                if (_CS1717 == null)
                    _CS1717 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1717, 3, 1717);
                return _CS1717;
            }
        }
        private static ICompilerReferenceWarning _CS1717;

        /// <summary><para>C&#9839; compiler warning (level 3) &#35;1718:</para><para>Comparison made to same variable; did you mean to compare something else?</para></summary>
        public static ICompilerReferenceWarning CS1718
        {
            get
            {
                if (_CS1718 == null)
                    _CS1718 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1718, 3, 1718);
                return _CS1718;
            }
        }
        private static ICompilerReferenceWarning _CS1718;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1720:</para><para>Expression will always cause a System.NullReferenceException because the default value of {0} is null</para></summary>
        public static ICompilerReferenceWarning CS1720
        {
            get
            {
                if (_CS1720 == null)
                    _CS1720 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1720, 1, 1720);
                return _CS1720;
            }
        }
        private static ICompilerReferenceWarning _CS1720;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1723:</para><para>XML comment on {0} has cref attribute {1} that refers to a type parameter</para></summary>
        public static ICompilerReferenceWarning CS1723
        {
            get
            {
                if (_CS1723 == null)
                    _CS1723 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1723, 1, 1723);
                return _CS1723;
            }
        }
        private static ICompilerReferenceWarning _CS1723;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1911:</para><para>Access to member {0} through a 'base' keyword from an anonymous method, lambda expression, query expression, or iterator results in unverifiable code. Consider moving the access into a helper method on the containing type.</para></summary>
        public static ICompilerReferenceWarning CS1911
        {
            get
            {
                if (_CS1911 == null)
                    _CS1911 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1911, 1, 1911);
                return _CS1911;
            }
        }
        private static ICompilerReferenceWarning _CS1911;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;1927:</para><para>Ignoring /win32manifest for module because it only applies to assemblies.</para></summary>
        public static ICompilerReferenceWarning CS1927
        {
            get
            {
                if (_CS1927 == null)
                    _CS1927 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1927, 2, 1927);
                return _CS1927;
            }
        }
        private static ICompilerReferenceWarning _CS1927;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1956:</para><para>Member {0} implements interface member {0} in type {1}. There are multiple matches for the interface member at run-time. It is implementation dependent which method will be called.</para></summary>
        public static ICompilerReferenceWarning CS1956
        {
            get
            {
                if (_CS1956 == null)
                    _CS1956 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1956, 1, 1956);
                return _CS1956;
            }
        }
        private static ICompilerReferenceWarning _CS1956;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;1957:</para><para>Member {0} overrides {1}. There are multiple override candidates at run-time. It is implementation dependent which method will be called.</para></summary>
        public static ICompilerReferenceWarning CS1957
        {
            get
            {
                if (_CS1957 == null)
                    _CS1957 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS1957, 1, 1957);
                return _CS1957;
            }
        }
        private static ICompilerReferenceWarning _CS1957;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;2002:</para><para>Source file {0} specified multiple times</para></summary>
        public static ICompilerReferenceWarning CS2002
        {
            get
            {
                if (_CS2002 == null)
                    _CS2002 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS2002, 1, 2002);
                return _CS2002;
            }
        }
        private static ICompilerReferenceWarning _CS2002;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;2014:</para><para>Compiler option {0} is obsolete, please use {1} instead</para></summary>
        public static ICompilerReferenceWarning CS2014
        {
            get
            {
                if (_CS2014 == null)
                    _CS2014 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS2014, 1, 2014);
                return _CS2014;
            }
        }
        private static ICompilerReferenceWarning _CS2014;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;2023:</para><para>Ignoring /noconfig option because it was specified in a response file</para></summary>
        public static ICompilerReferenceWarning CS2023
        {
            get
            {
                if (_CS2023 == null)
                    _CS2023 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS2023, 1, 2023);
                return _CS2023;
            }
        }
        private static ICompilerReferenceWarning _CS2023;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;2029:</para><para>Invalid value for '/define'; {0} is not a valid identifier</para></summary>
        public static ICompilerReferenceWarning CS2029
        {
            get
            {
                if (_CS2029 == null)
                    _CS2029 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS2029, 1, 2029);
                return _CS2029;
            }
        }
        private static ICompilerReferenceWarning _CS2029;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3000:</para><para>Methods with variable arguments are not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3000
        {
            get
            {
                if (_CS3000 == null)
                    _CS3000 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3000, 1, 3000);
                return _CS3000;
            }
        }
        private static ICompilerReferenceWarning _CS3000;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3001:</para><para>Argument type {0} is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3001
        {
            get
            {
                if (_CS3001 == null)
                    _CS3001 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3001, 1, 3001);
                return _CS3001;
            }
        }
        private static ICompilerReferenceWarning _CS3001;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3002:</para><para>Return type of {0} is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3002
        {
            get
            {
                if (_CS3002 == null)
                    _CS3002 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3002, 1, 3002);
                return _CS3002;
            }
        }
        private static ICompilerReferenceWarning _CS3002;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3003:</para><para>Type of {0} is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3003
        {
            get
            {
                if (_CS3003 == null)
                    _CS3003 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3003, 1, 3003);
                return _CS3003;
            }
        }
        private static ICompilerReferenceWarning _CS3003;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3004:</para><para>Mixed and decomposed Unicode characters are not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3004
        {
            get
            {
                if (_CS3004 == null)
                    _CS3004 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3004, 1, 3004);
                return _CS3004;
            }
        }
        private static ICompilerReferenceWarning _CS3004;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3005:</para><para>Identifier {0} differing only in case is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3005
        {
            get
            {
                if (_CS3005 == null)
                    _CS3005 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3005, 1, 3005);
                return _CS3005;
            }
        }
        private static ICompilerReferenceWarning _CS3005;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3006:</para><para>Overloaded method {0} differing only in ref or out, or in array rank, is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3006
        {
            get
            {
                if (_CS3006 == null)
                    _CS3006 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3006, 1, 3006);
                return _CS3006;
            }
        }
        private static ICompilerReferenceWarning _CS3006;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3007:</para><para>Overloaded method {0} differing only by unnamed array types is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3007
        {
            get
            {
                if (_CS3007 == null)
                    _CS3007 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3007, 1, 3007);
                return _CS3007;
            }
        }
        private static ICompilerReferenceWarning _CS3007;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3008:</para><para>Identifier {0} differing only in case is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3008
        {
            get
            {
                if (_CS3008 == null)
                    _CS3008 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3008, 1, 3008);
                return _CS3008;
            }
        }
        private static ICompilerReferenceWarning _CS3008;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3009:</para><para>{0}: base type {0} is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3009
        {
            get
            {
                if (_CS3009 == null)
                    _CS3009 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3009, 1, 3009);
                return _CS3009;
            }
        }
        private static ICompilerReferenceWarning _CS3009;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3010:</para><para>{0}: CLS-compliant interfaces must have only CLS-compliant members</para></summary>
        public static ICompilerReferenceWarning CS3010
        {
            get
            {
                if (_CS3010 == null)
                    _CS3010 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3010, 1, 3010);
                return _CS3010;
            }
        }
        private static ICompilerReferenceWarning _CS3010;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3011:</para><para>{0}: only CLS-compliant members can be abstract</para></summary>
        public static ICompilerReferenceWarning CS3011
        {
            get
            {
                if (_CS3011 == null)
                    _CS3011 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3011, 1, 3011);
                return _CS3011;
            }
        }
        private static ICompilerReferenceWarning _CS3011;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3012:</para><para>You cannot specify the CLSCompliant attribute on a module that differs from the CLSCompliant attribute on the assembly</para></summary>
        public static ICompilerReferenceWarning CS3012
        {
            get
            {
                if (_CS3012 == null)
                    _CS3012 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3012, 1, 3012);
                return _CS3012;
            }
        }
        private static ICompilerReferenceWarning _CS3012;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3013:</para><para>Added modules must be marked with the CLSCompliant attribute to match the assembly</para></summary>
        public static ICompilerReferenceWarning CS3013
        {
            get
            {
                if (_CS3013 == null)
                    _CS3013 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3013, 1, 3013);
                return _CS3013;
            }
        }
        private static ICompilerReferenceWarning _CS3013;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3014:</para><para>{0} does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute</para></summary>
        public static ICompilerReferenceWarning CS3014
        {
            get
            {
                if (_CS3014 == null)
                    _CS3014 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3014, 1, 3014);
                return _CS3014;
            }
        }
        private static ICompilerReferenceWarning _CS3014;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3015:</para><para>{0} has no accessible constructors which use only CLS-compliant types</para></summary>
        public static ICompilerReferenceWarning CS3015
        {
            get
            {
                if (_CS3015 == null)
                    _CS3015 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3015, 1, 3015);
                return _CS3015;
            }
        }
        private static ICompilerReferenceWarning _CS3015;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3016:</para><para>Arrays as attribute arguments is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3016
        {
            get
            {
                if (_CS3016 == null)
                    _CS3016 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3016, 1, 3016);
                return _CS3016;
            }
        }
        private static ICompilerReferenceWarning _CS3016;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3017:</para><para>You cannot specify the CLSCompliant attribute on a module that differs from the CLSCompliant attribute on the assembly</para></summary>
        public static ICompilerReferenceWarning CS3017
        {
            get
            {
                if (_CS3017 == null)
                    _CS3017 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3017, 1, 3017);
                return _CS3017;
            }
        }
        private static ICompilerReferenceWarning _CS3017;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3018:</para><para>{0} cannot be marked as CLS-Compliant because it is a member of non CLS-compliant type {1}</para></summary>
        public static ICompilerReferenceWarning CS3018
        {
            get
            {
                if (_CS3018 == null)
                    _CS3018 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3018, 1, 3018);
                return _CS3018;
            }
        }
        private static ICompilerReferenceWarning _CS3018;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;3019:</para><para>CLS compliance checking will not be performed on {0} because it is not visible from outside this assembly.</para></summary>
        public static ICompilerReferenceWarning CS3019
        {
            get
            {
                if (_CS3019 == null)
                    _CS3019 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3019, 2, 3019);
                return _CS3019;
            }
        }
        private static ICompilerReferenceWarning _CS3019;

        /// <summary><para>C&#9839; compiler warning (level 2) &#35;3021:</para><para>{0} does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute</para></summary>
        public static ICompilerReferenceWarning CS3021
        {
            get
            {
                if (_CS3021 == null)
                    _CS3021 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3021, 2, 3021);
                return _CS3021;
            }
        }
        private static ICompilerReferenceWarning _CS3021;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3022:</para><para>CLSCompliant attribute has no meaning when applied to parameters. Try putting it on the method instead.</para></summary>
        public static ICompilerReferenceWarning CS3022
        {
            get
            {
                if (_CS3022 == null)
                    _CS3022 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3022, 1, 3022);
                return _CS3022;
            }
        }
        private static ICompilerReferenceWarning _CS3022;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3023:</para><para>CLSCompliant attribute has no meaning when applied to return types. Try putting it on the method instead.</para></summary>
        public static ICompilerReferenceWarning CS3023
        {
            get
            {
                if (_CS3023 == null)
                    _CS3023 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3023, 1, 3023);
                return _CS3023;
            }
        }
        private static ICompilerReferenceWarning _CS3023;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3026:</para><para>CLS-compliant field {0} cannot be volatile</para></summary>
        public static ICompilerReferenceWarning CS3026
        {
            get
            {
                if (_CS3026 == null)
                    _CS3026 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3026, 1, 3026);
                return _CS3026;
            }
        }
        private static ICompilerReferenceWarning _CS3026;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;3027:</para><para>{0} is not CLS-compliant because base interface {1} is not CLS-compliant</para></summary>
        public static ICompilerReferenceWarning CS3027
        {
            get
            {
                if (_CS3027 == null)
                    _CS3027 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS3027, 1, 3027);
                return _CS3027;
            }
        }
        private static ICompilerReferenceWarning _CS3027;

        /// <summary><para>C&#9839; compiler warning (level 1) &#35;5000:</para><para>Unknown compiler option {0}</para></summary>
        public static ICompilerReferenceWarning CS5000
        {
            get
            {
                if (_CS5000 == null)
                    _CS5000 = new CompilerReferenceWarning(Resources.CSharpWarnings_CS5000, 1, 5000);
                return _CS5000;
            }
        }
        private static ICompilerReferenceWarning _CS5000;

        /// <summary><para>C&#9839; compiler error &#35;1:</para><para>Internal compiler error</para></summary>
        public static ICompilerReferenceError CS0001
        {
            get
            {
                if (_CS0001 == null)
                    _CS0001 = new CompilerReferenceError(Resources.CSharpErrors_CS0001, 1);
                return _CS0001;
            }
        }
        private static ICompilerReferenceError _CS0001;

        /// <summary><para>C&#9839; compiler error &#35;3:</para><para>Out of memory</para></summary>
        public static ICompilerReferenceError CS0003
        {
            get
            {
                if (_CS0003 == null)
                    _CS0003 = new CompilerReferenceError(Resources.CSharpErrors_CS0003, 3);
                return _CS0003;
            }
        }
        private static ICompilerReferenceError _CS0003;

        /// <summary><para>C&#9839; compiler error &#35;4:</para><para>Warning treated as error</para></summary>
        public static ICompilerReferenceError CS0004
        {
            get
            {
                if (_CS0004 == null)
                    _CS0004 = new CompilerReferenceError(Resources.CSharpErrors_CS0004, 4);
                return _CS0004;
            }
        }
        private static ICompilerReferenceError _CS0004;

        /// <summary><para>C&#9839; compiler error &#35;5:</para><para>Compiler option {0} must be followed by an argument</para></summary>
        public static ICompilerReferenceError CS0005
        {
            get
            {
                if (_CS0005 == null)
                    _CS0005 = new CompilerReferenceError(Resources.CSharpErrors_CS0005, 5);
                return _CS0005;
            }
        }
        private static ICompilerReferenceError _CS0005;

        /// <summary><para>C&#9839; compiler error &#35;6:</para><para>Metadata file {0} could not be found</para></summary>
        public static ICompilerReferenceError CS0006
        {
            get
            {
                if (_CS0006 == null)
                    _CS0006 = new CompilerReferenceError(Resources.CSharpErrors_CS0006, 6);
                return _CS0006;
            }
        }
        private static ICompilerReferenceError _CS0006;

        /// <summary><para>C&#9839; compiler error &#35;7:</para><para>Unexpected common language runtime initialization error — {0}</para></summary>
        public static ICompilerReferenceError CS0007
        {
            get
            {
                if (_CS0007 == null)
                    _CS0007 = new CompilerReferenceError(Resources.CSharpErrors_CS0007, 7);
                return _CS0007;
            }
        }
        private static ICompilerReferenceError _CS0007;

        /// <summary><para>C&#9839; compiler error &#35;8:</para><para>Unexpected error reading metadata from file 'file' — {0}</para></summary>
        public static ICompilerReferenceError CS0008
        {
            get
            {
                if (_CS0008 == null)
                    _CS0008 = new CompilerReferenceError(Resources.CSharpErrors_CS0008, 8);
                return _CS0008;
            }
        }
        private static ICompilerReferenceError _CS0008;

        /// <summary><para>C&#9839; compiler error &#35;9:</para><para>Metadata file {0} could not be opened — {1}</para></summary>
        public static ICompilerReferenceError CS0009
        {
            get
            {
                if (_CS0009 == null)
                    _CS0009 = new CompilerReferenceError(Resources.CSharpErrors_CS0009, 9);
                return _CS0009;
            }
        }
        private static ICompilerReferenceError _CS0009;

        /// <summary><para>C&#9839; compiler error &#35;10:</para><para>Unexpected fatal error -- {0}.</para></summary>
        public static ICompilerReferenceError CS0010
        {
            get
            {
                if (_CS0010 == null)
                    _CS0010 = new CompilerReferenceError(Resources.CSharpErrors_CS0010, 10);
                return _CS0010;
            }
        }
        private static ICompilerReferenceError _CS0010;

        /// <summary><para>C&#9839; compiler error &#35;11:</para><para>The base class or interface {0} in assembly {1} referenced by type {2} could not be resolved</para></summary>
        public static ICompilerReferenceError CS0011
        {
            get
            {
                if (_CS0011 == null)
                    _CS0011 = new CompilerReferenceError(Resources.CSharpErrors_CS0011, 11);
                return _CS0011;
            }
        }
        private static ICompilerReferenceError _CS0011;

        /// <summary><para>C&#9839; compiler error &#35;12:</para><para>The type {0} is defined in an assembly that is not referenced. You must add a reference to assembly {1}.</para></summary>
        public static ICompilerReferenceError CS0012
        {
            get
            {
                if (_CS0012 == null)
                    _CS0012 = new CompilerReferenceError(Resources.CSharpErrors_CS0012, 12);
                return _CS0012;
            }
        }
        private static ICompilerReferenceError _CS0012;

        /// <summary><para>C&#9839; compiler error &#35;13:</para><para>Unexpected error writing metadata to file {0} -- {1}</para></summary>
        public static ICompilerReferenceError CS0013
        {
            get
            {
                if (_CS0013 == null)
                    _CS0013 = new CompilerReferenceError(Resources.CSharpErrors_CS0013, 13);
                return _CS0013;
            }
        }
        private static ICompilerReferenceError _CS0013;

        /// <summary><para>C&#9839; compiler error &#35;14:</para><para>Required file {0} could not be found</para></summary>
        public static ICompilerReferenceError CS0014
        {
            get
            {
                if (_CS0014 == null)
                    _CS0014 = new CompilerReferenceError(Resources.CSharpErrors_CS0014, 14);
                return _CS0014;
            }
        }
        private static ICompilerReferenceError _CS0014;

        /// <summary><para>C&#9839; compiler error &#35;15:</para><para>The name of type {0} is too long</para></summary>
        public static ICompilerReferenceError CS0015
        {
            get
            {
                if (_CS0015 == null)
                    _CS0015 = new CompilerReferenceError(Resources.CSharpErrors_CS0015, 15);
                return _CS0015;
            }
        }
        private static ICompilerReferenceError _CS0015;

        /// <summary><para>C&#9839; compiler error &#35;16:</para><para>Could not write to output file {0} — {1}</para></summary>
        public static ICompilerReferenceError CS0016
        {
            get
            {
                if (_CS0016 == null)
                    _CS0016 = new CompilerReferenceError(Resources.CSharpErrors_CS0016, 16);
                return _CS0016;
            }
        }
        private static ICompilerReferenceError _CS0016;

        /// <summary><para>C&#9839; compiler error &#35;17:</para><para>Program {0} has more than one entry point defined. Compile with /main to specify the type that contains the entry point.</para></summary>
        public static ICompilerReferenceError CS0017
        {
            get
            {
                if (_CS0017 == null)
                    _CS0017 = new CompilerReferenceError(Resources.CSharpErrors_CS0017, 17);
                return _CS0017;
            }
        }
        private static ICompilerReferenceError _CS0017;

        /// <summary><para>C&#9839; compiler error &#35;19:</para><para>Operator {0} cannot be applied to operands of type {1} and {2}</para></summary>
        public static ICompilerReferenceError CS0019
        {
            get
            {
                if (_CS0019 == null)
                    _CS0019 = new CompilerReferenceError(Resources.CSharpErrors_CS0019, 19);
                return _CS0019;
            }
        }
        private static ICompilerReferenceError _CS0019;

        /// <summary><para>C&#9839; compiler error &#35;20:</para><para>Division by constant zero</para></summary>
        public static ICompilerReferenceError CS0020
        {
            get
            {
                if (_CS0020 == null)
                    _CS0020 = new CompilerReferenceError(Resources.CSharpErrors_CS0020, 20);
                return _CS0020;
            }
        }
        private static ICompilerReferenceError _CS0020;

        /// <summary><para>C&#9839; compiler error &#35;21:</para><para>Cannot apply indexing with [] to an expression of type {0}</para></summary>
        public static ICompilerReferenceError CS0021
        {
            get
            {
                if (_CS0021 == null)
                    _CS0021 = new CompilerReferenceError(Resources.CSharpErrors_CS0021, 21);
                return _CS0021;
            }
        }
        private static ICompilerReferenceError _CS0021;

        /// <summary><para>C&#9839; compiler error &#35;22:</para><para>Wrong number of indices inside [], expected {0}</para></summary>
        public static ICompilerReferenceError CS0022
        {
            get
            {
                if (_CS0022 == null)
                    _CS0022 = new CompilerReferenceError(Resources.CSharpErrors_CS0022, 22);
                return _CS0022;
            }
        }
        private static ICompilerReferenceError _CS0022;

        /// <summary><para>C&#9839; compiler error &#35;23:</para><para>Operator {0} cannot be applied to operand of type {1}</para></summary>
        public static ICompilerReferenceError CS0023
        {
            get
            {
                if (_CS0023 == null)
                    _CS0023 = new CompilerReferenceError(Resources.CSharpErrors_CS0023, 23);
                return _CS0023;
            }
        }
        private static ICompilerReferenceError _CS0023;

        /// <summary><para>C&#9839; compiler error &#35;25:</para><para>Standard library file {0} could not be found</para></summary>
        public static ICompilerReferenceError CS0025
        {
            get
            {
                if (_CS0025 == null)
                    _CS0025 = new CompilerReferenceError(Resources.CSharpErrors_CS0025, 25);
                return _CS0025;
            }
        }
        private static ICompilerReferenceError _CS0025;

        /// <summary><para>C&#9839; compiler error &#35;26:</para><para>Keyword 'this' is not valid in a static property, static method, or static field initializer</para></summary>
        public static ICompilerReferenceError CS0026
        {
            get
            {
                if (_CS0026 == null)
                    _CS0026 = new CompilerReferenceError(Resources.CSharpErrors_CS0026, 26);
                return _CS0026;
            }
        }
        private static ICompilerReferenceError _CS0026;

        /// <summary><para>C&#9839; compiler error &#35;27:</para><para>Keyword 'this' is not available in the current context</para></summary>
        public static ICompilerReferenceError CS0027
        {
            get
            {
                if (_CS0027 == null)
                    _CS0027 = new CompilerReferenceError(Resources.CSharpErrors_CS0027, 27);
                return _CS0027;
            }
        }
        private static ICompilerReferenceError _CS0027;

        /// <summary><para>C&#9839; compiler error &#35;29:</para><para>Cannot implicitly convert type {0} to {1}</para></summary>
        public static ICompilerReferenceError CS0029
        {
            get
            {
                if (_CS0029 == null)
                    _CS0029 = new CompilerReferenceError(Resources.CSharpErrors_CS0029, 29);
                return _CS0029;
            }
        }
        private static ICompilerReferenceError _CS0029;

        /// <summary><para>C&#9839; compiler error &#35;30:</para><para>Cannot convert type {0} to {1}</para></summary>
        public static ICompilerReferenceError CS0030
        {
            get
            {
                if (_CS0030 == null)
                    _CS0030 = new CompilerReferenceError(Resources.CSharpErrors_CS0030, 30);
                return _CS0030;
            }
        }
        private static ICompilerReferenceError _CS0030;

        /// <summary><para>C&#9839; compiler error &#35;31:</para><para>Constant value {0} cannot be converted to a {1}. (use 'unchecked' syntax to override)</para></summary>
        public static ICompilerReferenceError CS0031
        {
            get
            {
                if (_CS0031 == null)
                    _CS0031 = new CompilerReferenceError(Resources.CSharpErrors_CS0031, 31);
                return _CS0031;
            }
        }
        private static ICompilerReferenceError _CS0031;

        /// <summary><para>C&#9839; compiler error &#35;34:</para><para>Operator {0} is ambiguous on operands of type {1} and {2}</para></summary>
        public static ICompilerReferenceError CS0034
        {
            get
            {
                if (_CS0034 == null)
                    _CS0034 = new CompilerReferenceError(Resources.CSharpErrors_CS0034, 34);
                return _CS0034;
            }
        }
        private static ICompilerReferenceError _CS0034;

        /// <summary><para>C&#9839; compiler error &#35;35:</para><para>Operator {0} is ambiguous on an operand of type {1}</para></summary>
        public static ICompilerReferenceError CS0035
        {
            get
            {
                if (_CS0035 == null)
                    _CS0035 = new CompilerReferenceError(Resources.CSharpErrors_CS0035, 35);
                return _CS0035;
            }
        }
        private static ICompilerReferenceError _CS0035;

        /// <summary><para>C&#9839; compiler error &#35;36:</para><para>An out parameter cannot have the '[In]' attribute</para></summary>
        public static ICompilerReferenceError CS0036
        {
            get
            {
                if (_CS0036 == null)
                    _CS0036 = new CompilerReferenceError(Resources.CSharpErrors_CS0036, 36);
                return _CS0036;
            }
        }
        private static ICompilerReferenceError _CS0036;

        /// <summary><para>C&#9839; compiler error &#35;37:</para><para>Cannot convert null to {0} because it is a non-nullable value type</para></summary>
        public static ICompilerReferenceError CS0037
        {
            get
            {
                if (_CS0037 == null)
                    _CS0037 = new CompilerReferenceError(Resources.CSharpErrors_CS0037, 37);
                return _CS0037;
            }
        }
        private static ICompilerReferenceError _CS0037;

        /// <summary><para>C&#9839; compiler error &#35;38:</para><para>Cannot access a nonstatic member of outer type {0} via nested type {1}</para></summary>
        public static ICompilerReferenceError CS0038
        {
            get
            {
                if (_CS0038 == null)
                    _CS0038 = new CompilerReferenceError(Resources.CSharpErrors_CS0038, 38);
                return _CS0038;
            }
        }
        private static ICompilerReferenceError _CS0038;

        /// <summary><para>C&#9839; compiler error &#35;39:</para><para>Cannot convert type {0} to {1} via a reference conversion, boxing conversion, unboxing conversion, wrapping conversion, or null type conversion</para></summary>
        public static ICompilerReferenceError CS0039
        {
            get
            {
                if (_CS0039 == null)
                    _CS0039 = new CompilerReferenceError(Resources.CSharpErrors_CS0039, 39);
                return _CS0039;
            }
        }
        private static ICompilerReferenceError _CS0039;

        /// <summary><para>C&#9839; compiler error &#35;40:</para><para>Unexpected error creating debug information file — {0}</para></summary>
        public static ICompilerReferenceError CS0040
        {
            get
            {
                if (_CS0040 == null)
                    _CS0040 = new CompilerReferenceError(Resources.CSharpErrors_CS0040, 40);
                return _CS0040;
            }
        }
        private static ICompilerReferenceError _CS0040;

        /// <summary><para>C&#9839; compiler error &#35;41:</para><para>The fully qualified name for {0} is too long for debug information. Compile without '/debug' option.</para></summary>
        public static ICompilerReferenceError CS0041
        {
            get
            {
                if (_CS0041 == null)
                    _CS0041 = new CompilerReferenceError(Resources.CSharpErrors_CS0041, 41);
                return _CS0041;
            }
        }
        private static ICompilerReferenceError _CS0041;

        /// <summary><para>C&#9839; compiler error &#35;42:</para><para>Unexpected error creating debug information file {0} — {1}</para></summary>
        public static ICompilerReferenceError CS0042
        {
            get
            {
                if (_CS0042 == null)
                    _CS0042 = new CompilerReferenceError(Resources.CSharpErrors_CS0042, 42);
                return _CS0042;
            }
        }
        private static ICompilerReferenceError _CS0042;

        /// <summary><para>C&#9839; compiler error &#35;43:</para><para>PDB file {0} has an incorrect or out-of-date format. Delete it and rebuild.</para></summary>
        public static ICompilerReferenceError CS0043
        {
            get
            {
                if (_CS0043 == null)
                    _CS0043 = new CompilerReferenceError(Resources.CSharpErrors_CS0043, 43);
                return _CS0043;
            }
        }
        private static ICompilerReferenceError _CS0043;

        /// <summary><para>C&#9839; compiler error &#35;50:</para><para>Inconsistent accessibility: return type {0} is less accessible than method {1}</para></summary>
        public static ICompilerReferenceError CS0050
        {
            get
            {
                if (_CS0050 == null)
                    _CS0050 = new CompilerReferenceError(Resources.CSharpErrors_CS0050, 50);
                return _CS0050;
            }
        }
        private static ICompilerReferenceError _CS0050;

        /// <summary><para>C&#9839; compiler error &#35;51:</para><para>Inconsistent accessibility: parameter type {0} is less accessible than method {1}</para></summary>
        public static ICompilerReferenceError CS0051
        {
            get
            {
                if (_CS0051 == null)
                    _CS0051 = new CompilerReferenceError(Resources.CSharpErrors_CS0051, 51);
                return _CS0051;
            }
        }
        private static ICompilerReferenceError _CS0051;

        /// <summary><para>C&#9839; compiler error &#35;52:</para><para>Inconsistent accessibility: field type {0} is less accessible than field {1}</para></summary>
        public static ICompilerReferenceError CS0052
        {
            get
            {
                if (_CS0052 == null)
                    _CS0052 = new CompilerReferenceError(Resources.CSharpErrors_CS0052, 52);
                return _CS0052;
            }
        }
        private static ICompilerReferenceError _CS0052;

        /// <summary><para>C&#9839; compiler error &#35;53:</para><para>Inconsistent accessibility: property type {0} is less accessible than property {1}</para></summary>
        public static ICompilerReferenceError CS0053
        {
            get
            {
                if (_CS0053 == null)
                    _CS0053 = new CompilerReferenceError(Resources.CSharpErrors_CS0053, 53);
                return _CS0053;
            }
        }
        private static ICompilerReferenceError _CS0053;

        /// <summary><para>C&#9839; compiler error &#35;54:</para><para>Inconsistent accessibility: indexer return type {0} is less accessible than indexer {1}</para></summary>
        public static ICompilerReferenceError CS0054
        {
            get
            {
                if (_CS0054 == null)
                    _CS0054 = new CompilerReferenceError(Resources.CSharpErrors_CS0054, 54);
                return _CS0054;
            }
        }
        private static ICompilerReferenceError _CS0054;

        /// <summary><para>C&#9839; compiler error &#35;55:</para><para>Inconsistent accessibility: parameter type {0} is less accessible than indexer {1}</para></summary>
        public static ICompilerReferenceError CS0055
        {
            get
            {
                if (_CS0055 == null)
                    _CS0055 = new CompilerReferenceError(Resources.CSharpErrors_CS0055, 55);
                return _CS0055;
            }
        }
        private static ICompilerReferenceError _CS0055;

        /// <summary><para>C&#9839; compiler error &#35;56:</para><para>Inconsistent accessibility: return type {0} is less accessible than operator {1}</para></summary>
        public static ICompilerReferenceError CS0056
        {
            get
            {
                if (_CS0056 == null)
                    _CS0056 = new CompilerReferenceError(Resources.CSharpErrors_CS0056, 56);
                return _CS0056;
            }
        }
        private static ICompilerReferenceError _CS0056;

        /// <summary><para>C&#9839; compiler error &#35;57:</para><para>Inconsistent accessibility: parameter type {0} is less accessible than operator {1}</para></summary>
        public static ICompilerReferenceError CS0057
        {
            get
            {
                if (_CS0057 == null)
                    _CS0057 = new CompilerReferenceError(Resources.CSharpErrors_CS0057, 57);
                return _CS0057;
            }
        }
        private static ICompilerReferenceError _CS0057;

        /// <summary><para>C&#9839; compiler error &#35;58:</para><para>Inconsistent accessibility: return type {0} is less accessible than delegate {1}</para></summary>
        public static ICompilerReferenceError CS0058
        {
            get
            {
                if (_CS0058 == null)
                    _CS0058 = new CompilerReferenceError(Resources.CSharpErrors_CS0058, 58);
                return _CS0058;
            }
        }
        private static ICompilerReferenceError _CS0058;

        /// <summary><para>C&#9839; compiler error &#35;59:</para><para>Inconsistent accessibility: parameter type {0} is less accessible than delegate {1}</para></summary>
        public static ICompilerReferenceError CS0059
        {
            get
            {
                if (_CS0059 == null)
                    _CS0059 = new CompilerReferenceError(Resources.CSharpErrors_CS0059, 59);
                return _CS0059;
            }
        }
        private static ICompilerReferenceError _CS0059;

        /// <summary><para>C&#9839; compiler error &#35;60:</para><para>Inconsistent accessibility: base class {0} is less accessible than class {1}</para></summary>
        public static ICompilerReferenceError CS0060
        {
            get
            {
                if (_CS0060 == null)
                    _CS0060 = new CompilerReferenceError(Resources.CSharpErrors_CS0060, 60);
                return _CS0060;
            }
        }
        private static ICompilerReferenceError _CS0060;

        /// <summary><para>C&#9839; compiler error &#35;61:</para><para>Inconsistent accessibility: base interface {0} is less accessible than interface {1}</para></summary>
        public static ICompilerReferenceError CS0061
        {
            get
            {
                if (_CS0061 == null)
                    _CS0061 = new CompilerReferenceError(Resources.CSharpErrors_CS0061, 61);
                return _CS0061;
            }
        }
        private static ICompilerReferenceError _CS0061;

        /// <summary><para>C&#9839; compiler error &#35;65:</para><para>{0}: event property must have both add and remove accessors</para></summary>
        public static ICompilerReferenceError CS0065
        {
            get
            {
                if (_CS0065 == null)
                    _CS0065 = new CompilerReferenceError(Resources.CSharpErrors_CS0065, 65);
                return _CS0065;
            }
        }
        private static ICompilerReferenceError _CS0065;

        /// <summary><para>C&#9839; compiler error &#35;66:</para><para>{0}: event must be of a delegate type</para></summary>
        public static ICompilerReferenceError CS0066
        {
            get
            {
                if (_CS0066 == null)
                    _CS0066 = new CompilerReferenceError(Resources.CSharpErrors_CS0066, 66);
                return _CS0066;
            }
        }
        private static ICompilerReferenceError _CS0066;

        /// <summary><para>C&#9839; compiler error &#35;68:</para><para>{0}: event in interface cannot have initializer</para></summary>
        public static ICompilerReferenceError CS0068
        {
            get
            {
                if (_CS0068 == null)
                    _CS0068 = new CompilerReferenceError(Resources.CSharpErrors_CS0068, 68);
                return _CS0068;
            }
        }
        private static ICompilerReferenceError _CS0068;

        /// <summary><para>C&#9839; compiler error &#35;69:</para><para>An event in an interface cannot have add or remove accessors</para></summary>
        public static ICompilerReferenceError CS0069
        {
            get
            {
                if (_CS0069 == null)
                    _CS0069 = new CompilerReferenceError(Resources.CSharpErrors_CS0069, 69);
                return _CS0069;
            }
        }
        private static ICompilerReferenceError _CS0069;

        /// <summary><para>C&#9839; compiler error &#35;70:</para><para>The event {0} can only appear on the left hand side of += or -= (except when used from within the type {1})</para></summary>
        public static ICompilerReferenceError CS0070
        {
            get
            {
                if (_CS0070 == null)
                    _CS0070 = new CompilerReferenceError(Resources.CSharpErrors_CS0070, 70);
                return _CS0070;
            }
        }
        private static ICompilerReferenceError _CS0070;

        /// <summary><para>C&#9839; compiler error &#35;71:</para><para>An explicit interface implementation of an event must use event accessor syntax</para></summary>
        public static ICompilerReferenceError CS0071
        {
            get
            {
                if (_CS0071 == null)
                    _CS0071 = new CompilerReferenceError(Resources.CSharpErrors_CS0071, 71);
                return _CS0071;
            }
        }
        private static ICompilerReferenceError _CS0071;

        /// <summary><para>C&#9839; compiler error &#35;72:</para><para>{0} : cannot override; {1} is not an event</para></summary>
        public static ICompilerReferenceError CS0072
        {
            get
            {
                if (_CS0072 == null)
                    _CS0072 = new CompilerReferenceError(Resources.CSharpErrors_CS0072, 72);
                return _CS0072;
            }
        }
        private static ICompilerReferenceError _CS0072;

        /// <summary><para>C&#9839; compiler error &#35;73:</para><para>An add or remove accessor must have a body</para></summary>
        public static ICompilerReferenceError CS0073
        {
            get
            {
                if (_CS0073 == null)
                    _CS0073 = new CompilerReferenceError(Resources.CSharpErrors_CS0073, 73);
                return _CS0073;
            }
        }
        private static ICompilerReferenceError _CS0073;

        /// <summary><para>C&#9839; compiler error &#35;74:</para><para>{0}: abstract event cannot have initializer</para></summary>
        public static ICompilerReferenceError CS0074
        {
            get
            {
                if (_CS0074 == null)
                    _CS0074 = new CompilerReferenceError(Resources.CSharpErrors_CS0074, 74);
                return _CS0074;
            }
        }
        private static ICompilerReferenceError _CS0074;

        /// <summary><para>C&#9839; compiler error &#35;75:</para><para>To cast a negative value, you must enclose the value in parentheses</para></summary>
        public static ICompilerReferenceError CS0075
        {
            get
            {
                if (_CS0075 == null)
                    _CS0075 = new CompilerReferenceError(Resources.CSharpErrors_CS0075, 75);
                return _CS0075;
            }
        }
        private static ICompilerReferenceError _CS0075;

        /// <summary><para>C&#9839; compiler error &#35;76:</para><para>The enumerator name 'value__' is reserved and cannot be used</para></summary>
        public static ICompilerReferenceError CS0076
        {
            get
            {
                if (_CS0076 == null)
                    _CS0076 = new CompilerReferenceError(Resources.CSharpErrors_CS0076, 76);
                return _CS0076;
            }
        }
        private static ICompilerReferenceError _CS0076;

        /// <summary><para>C&#9839; compiler error &#35;77:</para><para>The as operator must be used with a reference type or nullable type ({0} is a non-nullable value type).</para></summary>
        public static ICompilerReferenceError CS0077
        {
            get
            {
                if (_CS0077 == null)
                    _CS0077 = new CompilerReferenceError(Resources.CSharpErrors_CS0077, 77);
                return _CS0077;
            }
        }
        private static ICompilerReferenceError _CS0077;

        /// <summary><para>C&#9839; compiler error &#35;79:</para><para>The event {0} can only appear on the left hand side of += or -=</para></summary>
        public static ICompilerReferenceError CS0079
        {
            get
            {
                if (_CS0079 == null)
                    _CS0079 = new CompilerReferenceError(Resources.CSharpErrors_CS0079, 79);
                return _CS0079;
            }
        }
        private static ICompilerReferenceError _CS0079;

        /// <summary><para>C&#9839; compiler error &#35;80:</para><para>Constraints are not allowed on non-generic declarations</para></summary>
        public static ICompilerReferenceError CS0080
        {
            get
            {
                if (_CS0080 == null)
                    _CS0080 = new CompilerReferenceError(Resources.CSharpErrors_CS0080, 80);
                return _CS0080;
            }
        }
        private static ICompilerReferenceError _CS0080;

        /// <summary><para>C&#9839; compiler error &#35;81:</para><para>Type parameter declaration must be an identifier not a type</para></summary>
        public static ICompilerReferenceError CS0081
        {
            get
            {
                if (_CS0081 == null)
                    _CS0081 = new CompilerReferenceError(Resources.CSharpErrors_CS0081, 81);
                return _CS0081;
            }
        }
        private static ICompilerReferenceError _CS0081;

        /// <summary><para>C&#9839; compiler error &#35;82:</para><para>Type {0} already reserves a member called {1} with the same parameter types</para></summary>
        public static ICompilerReferenceError CS0082
        {
            get
            {
                if (_CS0082 == null)
                    _CS0082 = new CompilerReferenceError(Resources.CSharpErrors_CS0082, 82);
                return _CS0082;
            }
        }
        private static ICompilerReferenceError _CS0082;

        /// <summary><para>C&#9839; compiler error &#35;100:</para><para>The parameter name {0} is a duplicate</para></summary>
        public static ICompilerReferenceError CS0100
        {
            get
            {
                if (_CS0100 == null)
                    _CS0100 = new CompilerReferenceError(Resources.CSharpErrors_CS0100, 100);
                return _CS0100;
            }
        }
        private static ICompilerReferenceError _CS0100;

        /// <summary><para>C&#9839; compiler error &#35;101:</para><para>The namespace {0} already contains a definition for {1}</para></summary>
        public static ICompilerReferenceError CS0101
        {
            get
            {
                if (_CS0101 == null)
                    _CS0101 = new CompilerReferenceError(Resources.CSharpErrors_CS0101, 101);
                return _CS0101;
            }
        }
        private static ICompilerReferenceError _CS0101;

        /// <summary><para>C&#9839; compiler error &#35;102:</para><para>The type {0} already contains a definition for {1}</para></summary>
        public static ICompilerReferenceError CS0102
        {
            get
            {
                if (_CS0102 == null)
                    _CS0102 = new CompilerReferenceError(Resources.CSharpErrors_CS0102, 102);
                return _CS0102;
            }
        }
        private static ICompilerReferenceError _CS0102;

        /// <summary><para>C&#9839; compiler error &#35;103:</para><para>The name {0} does not exist in the current context</para></summary>
        public static ICompilerReferenceError CS0103
        {
            get
            {
                if (_CS0103 == null)
                    _CS0103 = new CompilerReferenceError(Resources.CSharpErrors_CS0103, 103);
                return _CS0103;
            }
        }
        private static ICompilerReferenceError _CS0103;

        /// <summary><para>C&#9839; compiler error &#35;104:</para><para>{0} is an ambiguous reference between {1} and {2}</para></summary>
        public static ICompilerReferenceError CS0104
        {
            get
            {
                if (_CS0104 == null)
                    _CS0104 = new CompilerReferenceError(Resources.CSharpErrors_CS0104, 104);
                return _CS0104;
            }
        }
        private static ICompilerReferenceError _CS0104;

        /// <summary><para>C&#9839; compiler error &#35;106:</para><para>The modifier {0} is not valid for this item</para></summary>
        public static ICompilerReferenceError CS0106
        {
            get
            {
                if (_CS0106 == null)
                    _CS0106 = new CompilerReferenceError(Resources.CSharpErrors_CS0106, 106);
                return _CS0106;
            }
        }
        private static ICompilerReferenceError _CS0106;

        /// <summary><para>C&#9839; compiler error &#35;107:</para><para>More than one protection modifier</para></summary>
        public static ICompilerReferenceError CS0107
        {
            get
            {
                if (_CS0107 == null)
                    _CS0107 = new CompilerReferenceError(Resources.CSharpErrors_CS0107, 107);
                return _CS0107;
            }
        }
        private static ICompilerReferenceError _CS0107;

        /// <summary><para>C&#9839; compiler error &#35;110:</para><para>The evaluation of the constant value for {0} involves a circular definition</para></summary>
        public static ICompilerReferenceError CS0110
        {
            get
            {
                if (_CS0110 == null)
                    _CS0110 = new CompilerReferenceError(Resources.CSharpErrors_CS0110, 110);
                return _CS0110;
            }
        }
        private static ICompilerReferenceError _CS0110;

        /// <summary><para>C&#9839; compiler error &#35;111:</para><para>Type {0} already defines a member called {1} with the same parameter types</para></summary>
        public static ICompilerReferenceError CS0111
        {
            get
            {
                if (_CS0111 == null)
                    _CS0111 = new CompilerReferenceError(Resources.CSharpErrors_CS0111, 111);
                return _CS0111;
            }
        }
        private static ICompilerReferenceError _CS0111;

        /// <summary><para>C&#9839; compiler error &#35;112:</para><para>A static member {0} cannot be marked as override, virtual or abstract</para></summary>
        public static ICompilerReferenceError CS0112
        {
            get
            {
                if (_CS0112 == null)
                    _CS0112 = new CompilerReferenceError(Resources.CSharpErrors_CS0112, 112);
                return _CS0112;
            }
        }
        private static ICompilerReferenceError _CS0112;

        /// <summary><para>C&#9839; compiler error &#35;113:</para><para>A member {0} marked as override cannot be marked as new or virtual</para></summary>
        public static ICompilerReferenceError CS0113
        {
            get
            {
                if (_CS0113 == null)
                    _CS0113 = new CompilerReferenceError(Resources.CSharpErrors_CS0113, 113);
                return _CS0113;
            }
        }
        private static ICompilerReferenceError _CS0113;

        /// <summary><para>C&#9839; compiler error &#35;115:</para><para>{0} : no suitable method found to override</para></summary>
        public static ICompilerReferenceError CS0115
        {
            get
            {
                if (_CS0115 == null)
                    _CS0115 = new CompilerReferenceError(Resources.CSharpErrors_CS0115, 115);
                return _CS0115;
            }
        }
        private static ICompilerReferenceError _CS0115;

        /// <summary><para>C&#9839; compiler error &#35;116:</para><para>A namespace does not directly contain members such as fields or methods</para></summary>
        public static ICompilerReferenceError CS0116
        {
            get
            {
                if (_CS0116 == null)
                    _CS0116 = new CompilerReferenceError(Resources.CSharpErrors_CS0116, 116);
                return _CS0116;
            }
        }
        private static ICompilerReferenceError _CS0116;

        /// <summary><para>C&#9839; compiler error &#35;117:</para><para>{0} does not contain a definition for 'identifier'</para></summary>
        public static ICompilerReferenceError CS0117
        {
            get
            {
                if (_CS0117 == null)
                    _CS0117 = new CompilerReferenceError(Resources.CSharpErrors_CS0117, 117);
                return _CS0117;
            }
        }
        private static ICompilerReferenceError _CS0117;

        /// <summary><para>C&#9839; compiler error &#35;118:</para><para>{0} is a {1} but is used like a {2}</para></summary>
        public static ICompilerReferenceError CS0118
        {
            get
            {
                if (_CS0118 == null)
                    _CS0118 = new CompilerReferenceError(Resources.CSharpErrors_CS0118, 118);
                return _CS0118;
            }
        }
        private static ICompilerReferenceError _CS0118;

        /// <summary><para>C&#9839; compiler error &#35;119:</para><para>{0} is a {1}, which is not valid in the given context.</para></summary>
        public static ICompilerReferenceError CS0119
        {
            get
            {
                if (_CS0119 == null)
                    _CS0119 = new CompilerReferenceError(Resources.CSharpErrors_CS0119, 119);
                return _CS0119;
            }
        }
        private static ICompilerReferenceError _CS0119;

        /// <summary><para>C&#9839; compiler error &#35;120:</para><para>An object reference is required for the nonstatic field, method, or property {0}</para></summary>
        public static ICompilerReferenceError CS0120
        {
            get
            {
                if (_CS0120 == null)
                    _CS0120 = new CompilerReferenceError(Resources.CSharpErrors_CS0120, 120);
                return _CS0120;
            }
        }
        private static ICompilerReferenceError _CS0120;

        /// <summary><para>C&#9839; compiler error &#35;121:</para><para>The call is ambiguous between the following methods or properties: {0} and {1}</para></summary>
        public static ICompilerReferenceError CS0121
        {
            get
            {
                if (_CS0121 == null)
                    _CS0121 = new CompilerReferenceError(Resources.CSharpErrors_CS0121, 121);
                return _CS0121;
            }
        }
        private static ICompilerReferenceError _CS0121;

        /// <summary><para>C&#9839; compiler error &#35;122:</para><para>{0} is inaccessible due to its protection level</para></summary>
        public static ICompilerReferenceError CS0122
        {
            get
            {
                if (_CS0122 == null)
                    _CS0122 = new CompilerReferenceError(Resources.CSharpErrors_CS0122, 122);
                return _CS0122;
            }
        }
        private static ICompilerReferenceError _CS0122;

        /// <summary><para>C&#9839; compiler error &#35;123:</para><para>No overload for {0} matches delegate {1}</para></summary>
        public static ICompilerReferenceError CS0123
        {
            get
            {
                if (_CS0123 == null)
                    _CS0123 = new CompilerReferenceError(Resources.CSharpErrors_CS0123, 123);
                return _CS0123;
            }
        }
        private static ICompilerReferenceError _CS0123;

        /// <summary><para>C&#9839; compiler error &#35;126:</para><para>An object of a type convertible to {0} is required</para></summary>
        public static ICompilerReferenceError CS0126
        {
            get
            {
                if (_CS0126 == null)
                    _CS0126 = new CompilerReferenceError(Resources.CSharpErrors_CS0126, 126);
                return _CS0126;
            }
        }
        private static ICompilerReferenceError _CS0126;

        /// <summary><para>C&#9839; compiler error &#35;127:</para><para>Since {0} returns void, a return keyword must not be followed by an object expression</para></summary>
        public static ICompilerReferenceError CS0127
        {
            get
            {
                if (_CS0127 == null)
                    _CS0127 = new CompilerReferenceError(Resources.CSharpErrors_CS0127, 127);
                return _CS0127;
            }
        }
        private static ICompilerReferenceError _CS0127;

        /// <summary><para>C&#9839; compiler error &#35;128:</para><para>A local variable named {0} is already defined in this scope</para></summary>
        public static ICompilerReferenceError CS0128
        {
            get
            {
                if (_CS0128 == null)
                    _CS0128 = new CompilerReferenceError(Resources.CSharpErrors_CS0128, 128);
                return _CS0128;
            }
        }
        private static ICompilerReferenceError _CS0128;

        /// <summary><para>C&#9839; compiler error &#35;131:</para><para>The left-hand side of an assignment must be a variable, property or indexer</para></summary>
        public static ICompilerReferenceError CS0131
        {
            get
            {
                if (_CS0131 == null)
                    _CS0131 = new CompilerReferenceError(Resources.CSharpErrors_CS0131, 131);
                return _CS0131;
            }
        }
        private static ICompilerReferenceError _CS0131;

        /// <summary><para>C&#9839; compiler error &#35;132:</para><para>{0} : a static constructor must be parameterless</para></summary>
        public static ICompilerReferenceError CS0132
        {
            get
            {
                if (_CS0132 == null)
                    _CS0132 = new CompilerReferenceError(Resources.CSharpErrors_CS0132, 132);
                return _CS0132;
            }
        }
        private static ICompilerReferenceError _CS0132;

        /// <summary><para>C&#9839; compiler error &#35;133:</para><para>The expression being assigned to {0} must be constant</para></summary>
        public static ICompilerReferenceError CS0133
        {
            get
            {
                if (_CS0133 == null)
                    _CS0133 = new CompilerReferenceError(Resources.CSharpErrors_CS0133, 133);
                return _CS0133;
            }
        }
        private static ICompilerReferenceError _CS0133;

        /// <summary><para>C&#9839; compiler error &#35;134:</para><para>{0} is of type {1}. A const field of a reference type other than string can only be initialized with null.</para></summary>
        public static ICompilerReferenceError CS0134
        {
            get
            {
                if (_CS0134 == null)
                    _CS0134 = new CompilerReferenceError(Resources.CSharpErrors_CS0134, 134);
                return _CS0134;
            }
        }
        private static ICompilerReferenceError _CS0134;

        /// <summary><para>C&#9839; compiler error &#35;135:</para><para>{0} conflicts with the declaration {1}</para></summary>
        public static ICompilerReferenceError CS0135
        {
            get
            {
                if (_CS0135 == null)
                    _CS0135 = new CompilerReferenceError(Resources.CSharpErrors_CS0135, 135);
                return _CS0135;
            }
        }
        private static ICompilerReferenceError _CS0135;

        /// <summary><para>C&#9839; compiler error &#35;136:</para><para>A local variable named {0} cannot be declared in this scope because it would give a different meaning to {0}, which is already used in a 'parent or current/child' scope to denote something else</para></summary>
        public static ICompilerReferenceError CS0136
        {
            get
            {
                if (_CS0136 == null)
                    _CS0136 = new CompilerReferenceError(Resources.CSharpErrors_CS0136, 136);
                return _CS0136;
            }
        }
        private static ICompilerReferenceError _CS0136;

        /// <summary><para>C&#9839; compiler error &#35;138:</para><para>A using namespace directive can only be applied to namespaces; {0} is a type not a namespace</para></summary>
        public static ICompilerReferenceError CS0138
        {
            get
            {
                if (_CS0138 == null)
                    _CS0138 = new CompilerReferenceError(Resources.CSharpErrors_CS0138, 138);
                return _CS0138;
            }
        }
        private static ICompilerReferenceError _CS0138;

        /// <summary><para>C&#9839; compiler error &#35;139:</para><para>No enclosing loop out of which to break or continue</para></summary>
        public static ICompilerReferenceError CS0139
        {
            get
            {
                if (_CS0139 == null)
                    _CS0139 = new CompilerReferenceError(Resources.CSharpErrors_CS0139, 139);
                return _CS0139;
            }
        }
        private static ICompilerReferenceError _CS0139;

        /// <summary><para>C&#9839; compiler error &#35;140:</para><para>The label {0} is a duplicate</para></summary>
        public static ICompilerReferenceError CS0140
        {
            get
            {
                if (_CS0140 == null)
                    _CS0140 = new CompilerReferenceError(Resources.CSharpErrors_CS0140, 140);
                return _CS0140;
            }
        }
        private static ICompilerReferenceError _CS0140;

        /// <summary><para>C&#9839; compiler error &#35;143:</para><para>The type {0} has no constructors defined</para></summary>
        public static ICompilerReferenceError CS0143
        {
            get
            {
                if (_CS0143 == null)
                    _CS0143 = new CompilerReferenceError(Resources.CSharpErrors_CS0143, 143);
                return _CS0143;
            }
        }
        private static ICompilerReferenceError _CS0143;

        /// <summary><para>C&#9839; compiler error &#35;144:</para><para>Cannot create an instance of the abstract class or interface {0}</para></summary>
        public static ICompilerReferenceError CS0144
        {
            get
            {
                if (_CS0144 == null)
                    _CS0144 = new CompilerReferenceError(Resources.CSharpErrors_CS0144, 144);
                return _CS0144;
            }
        }
        private static ICompilerReferenceError _CS0144;

        /// <summary><para>C&#9839; compiler error &#35;145:</para><para>A const field requires a value to be provided</para></summary>
        public static ICompilerReferenceError CS0145
        {
            get
            {
                if (_CS0145 == null)
                    _CS0145 = new CompilerReferenceError(Resources.CSharpErrors_CS0145, 145);
                return _CS0145;
            }
        }
        private static ICompilerReferenceError _CS0145;

        /// <summary><para>C&#9839; compiler error &#35;146:</para><para>Circular base class dependency involving {0} and {1}</para></summary>
        public static ICompilerReferenceError CS0146
        {
            get
            {
                if (_CS0146 == null)
                    _CS0146 = new CompilerReferenceError(Resources.CSharpErrors_CS0146, 146);
                return _CS0146;
            }
        }
        private static ICompilerReferenceError _CS0146;

        /// <summary><para>C&#9839; compiler error &#35;148:</para><para>The delegate {0} does not have a valid constructor</para></summary>
        public static ICompilerReferenceError CS0148
        {
            get
            {
                if (_CS0148 == null)
                    _CS0148 = new CompilerReferenceError(Resources.CSharpErrors_CS0148, 148);
                return _CS0148;
            }
        }
        private static ICompilerReferenceError _CS0148;

        /// <summary><para>C&#9839; compiler error &#35;149:</para><para>Method name expected</para></summary>
        public static ICompilerReferenceError CS0149
        {
            get
            {
                if (_CS0149 == null)
                    _CS0149 = new CompilerReferenceError(Resources.CSharpErrors_CS0149, 149);
                return _CS0149;
            }
        }
        private static ICompilerReferenceError _CS0149;

        /// <summary><para>C&#9839; compiler error &#35;150:</para><para>A constant value is expected</para></summary>
        public static ICompilerReferenceError CS0150
        {
            get
            {
                if (_CS0150 == null)
                    _CS0150 = new CompilerReferenceError(Resources.CSharpErrors_CS0150, 150);
                return _CS0150;
            }
        }
        private static ICompilerReferenceError _CS0150;

        /// <summary><para>C&#9839; compiler error &#35;151:</para><para>A value of an integral type expected</para></summary>
        public static ICompilerReferenceError CS0151
        {
            get
            {
                if (_CS0151 == null)
                    _CS0151 = new CompilerReferenceError(Resources.CSharpErrors_CS0151, 151);
                return _CS0151;
            }
        }
        private static ICompilerReferenceError _CS0151;

        /// <summary><para>C&#9839; compiler error &#35;152:</para><para>The label {0} already occurs in this switch statement</para></summary>
        public static ICompilerReferenceError CS0152
        {
            get
            {
                if (_CS0152 == null)
                    _CS0152 = new CompilerReferenceError(Resources.CSharpErrors_CS0152, 152);
                return _CS0152;
            }
        }
        private static ICompilerReferenceError _CS0152;

        /// <summary><para>C&#9839; compiler error &#35;153:</para><para>A goto case is only valid inside a switch statement</para></summary>
        public static ICompilerReferenceError CS0153
        {
            get
            {
                if (_CS0153 == null)
                    _CS0153 = new CompilerReferenceError(Resources.CSharpErrors_CS0153, 153);
                return _CS0153;
            }
        }
        private static ICompilerReferenceError _CS0153;

        /// <summary><para>C&#9839; compiler error &#35;154:</para><para>The property or indexer 'property' cannot be used in this context because it lacks the get accessor</para></summary>
        public static ICompilerReferenceError CS0154
        {
            get
            {
                if (_CS0154 == null)
                    _CS0154 = new CompilerReferenceError(Resources.CSharpErrors_CS0154, 154);
                return _CS0154;
            }
        }
        private static ICompilerReferenceError _CS0154;

        /// <summary><para>C&#9839; compiler error &#35;155:</para><para>The type caught or thrown must be derived from System.Exception</para></summary>
        public static ICompilerReferenceError CS0155
        {
            get
            {
                if (_CS0155 == null)
                    _CS0155 = new CompilerReferenceError(Resources.CSharpErrors_CS0155, 155);
                return _CS0155;
            }
        }
        private static ICompilerReferenceError _CS0155;

        /// <summary><para>C&#9839; compiler error &#35;156:</para><para>A throw statement with no arguments is not allowed in a finally clause that is nested inside the nearest enclosing catch clause</para></summary>
        public static ICompilerReferenceError CS0156
        {
            get
            {
                if (_CS0156 == null)
                    _CS0156 = new CompilerReferenceError(Resources.CSharpErrors_CS0156, 156);
                return _CS0156;
            }
        }
        private static ICompilerReferenceError _CS0156;

        /// <summary><para>C&#9839; compiler error &#35;157:</para><para>Control cannot leave the body of a finally clause</para></summary>
        public static ICompilerReferenceError CS0157
        {
            get
            {
                if (_CS0157 == null)
                    _CS0157 = new CompilerReferenceError(Resources.CSharpErrors_CS0157, 157);
                return _CS0157;
            }
        }
        private static ICompilerReferenceError _CS0157;

        /// <summary><para>C&#9839; compiler error &#35;158:</para><para>The label {0} shadows another label by the same name in a contained scope</para></summary>
        public static ICompilerReferenceError CS0158
        {
            get
            {
                if (_CS0158 == null)
                    _CS0158 = new CompilerReferenceError(Resources.CSharpErrors_CS0158, 158);
                return _CS0158;
            }
        }
        private static ICompilerReferenceError _CS0158;

        /// <summary><para>C&#9839; compiler error &#35;159:</para><para>No such label {0} within the scope of the goto statement</para></summary>
        public static ICompilerReferenceError CS0159
        {
            get
            {
                if (_CS0159 == null)
                    _CS0159 = new CompilerReferenceError(Resources.CSharpErrors_CS0159, 159);
                return _CS0159;
            }
        }
        private static ICompilerReferenceError _CS0159;

        /// <summary><para>C&#9839; compiler error &#35;160:</para><para>A previous catch clause already catches all exceptions of this or of a super type ({0})</para></summary>
        public static ICompilerReferenceError CS0160
        {
            get
            {
                if (_CS0160 == null)
                    _CS0160 = new CompilerReferenceError(Resources.CSharpErrors_CS0160, 160);
                return _CS0160;
            }
        }
        private static ICompilerReferenceError _CS0160;

        /// <summary><para>C&#9839; compiler error &#35;161:</para><para>{0}: not all code paths return a value</para></summary>
        public static ICompilerReferenceError CS0161
        {
            get
            {
                if (_CS0161 == null)
                    _CS0161 = new CompilerReferenceError(Resources.CSharpErrors_CS0161, 161);
                return _CS0161;
            }
        }
        private static ICompilerReferenceError _CS0161;

        /// <summary><para>C&#9839; compiler error &#35;163:</para><para>Control cannot fall through from one case label ({0}) to another</para></summary>
        public static ICompilerReferenceError CS0163
        {
            get
            {
                if (_CS0163 == null)
                    _CS0163 = new CompilerReferenceError(Resources.CSharpErrors_CS0163, 163);
                return _CS0163;
            }
        }
        private static ICompilerReferenceError _CS0163;

        /// <summary><para>C&#9839; compiler error &#35;165:</para><para>Use of unassigned local variable {0}</para></summary>
        public static ICompilerReferenceError CS0165
        {
            get
            {
                if (_CS0165 == null)
                    _CS0165 = new CompilerReferenceError(Resources.CSharpErrors_CS0165, 165);
                return _CS0165;
            }
        }
        private static ICompilerReferenceError _CS0165;

        /// <summary><para>C&#9839; compiler error &#35;167:</para><para>The delegate {0} is missing the Invoke method</para></summary>
        public static ICompilerReferenceError CS0167
        {
            get
            {
                if (_CS0167 == null)
                    _CS0167 = new CompilerReferenceError(Resources.CSharpErrors_CS0167, 167);
                return _CS0167;
            }
        }
        private static ICompilerReferenceError _CS0167;

        /// <summary><para>C&#9839; compiler error &#35;170:</para><para>Use of possibly unassigned field {0}</para></summary>
        public static ICompilerReferenceError CS0170
        {
            get
            {
                if (_CS0170 == null)
                    _CS0170 = new CompilerReferenceError(Resources.CSharpErrors_CS0170, 170);
                return _CS0170;
            }
        }
        private static ICompilerReferenceError _CS0170;

        /// <summary><para>C&#9839; compiler error &#35;171:</para><para>Backing field for automatically implemented property {0} must be fully assigned before control is returned to the caller. Consider calling the default constructor from a constructor initializer.</para></summary>
        public static ICompilerReferenceError CS0171
        {
            get
            {
                if (_CS0171 == null)
                    _CS0171 = new CompilerReferenceError(Resources.CSharpErrors_CS0171, 171);
                return _CS0171;
            }
        }
        private static ICompilerReferenceError _CS0171;

        /// <summary><para>C&#9839; compiler error &#35;172:</para><para>Type of conditional expression cannot be determined because {0} and {1} implicitly convert to one another</para></summary>
        public static ICompilerReferenceError CS0172
        {
            get
            {
                if (_CS0172 == null)
                    _CS0172 = new CompilerReferenceError(Resources.CSharpErrors_CS0172, 172);
                return _CS0172;
            }
        }
        private static ICompilerReferenceError _CS0172;

        /// <summary><para>C&#9839; compiler error &#35;173:</para><para>Type of conditional expression cannot be determined because there is no implicit conversion between {0} and {1}</para></summary>
        public static ICompilerReferenceError CS0173
        {
            get
            {
                if (_CS0173 == null)
                    _CS0173 = new CompilerReferenceError(Resources.CSharpErrors_CS0173, 173);
                return _CS0173;
            }
        }
        private static ICompilerReferenceError _CS0173;

        /// <summary><para>C&#9839; compiler error &#35;174:</para><para>A base class is required for a 'base' reference</para></summary>
        public static ICompilerReferenceError CS0174
        {
            get
            {
                if (_CS0174 == null)
                    _CS0174 = new CompilerReferenceError(Resources.CSharpErrors_CS0174, 174);
                return _CS0174;
            }
        }
        private static ICompilerReferenceError _CS0174;

        /// <summary><para>C&#9839; compiler error &#35;175:</para><para>Use of keyword 'base' is not valid in this context</para></summary>
        public static ICompilerReferenceError CS0175
        {
            get
            {
                if (_CS0175 == null)
                    _CS0175 = new CompilerReferenceError(Resources.CSharpErrors_CS0175, 175);
                return _CS0175;
            }
        }
        private static ICompilerReferenceError _CS0175;

        /// <summary><para>C&#9839; compiler error &#35;176:</para><para>Static member {0} cannot be accessed with an instance reference; qualify it with a type name instead</para></summary>
        public static ICompilerReferenceError CS0176
        {
            get
            {
                if (_CS0176 == null)
                    _CS0176 = new CompilerReferenceError(Resources.CSharpErrors_CS0176, 176);
                return _CS0176;
            }
        }
        private static ICompilerReferenceError _CS0176;

        /// <summary><para>C&#9839; compiler error &#35;177:</para><para>The out parameter {0} must be assigned to before control leaves the current method</para></summary>
        public static ICompilerReferenceError CS0177
        {
            get
            {
                if (_CS0177 == null)
                    _CS0177 = new CompilerReferenceError(Resources.CSharpErrors_CS0177, 177);
                return _CS0177;
            }
        }
        private static ICompilerReferenceError _CS0177;

        /// <summary><para>C&#9839; compiler error &#35;178:</para><para>Invalid rank specifier: expected ',' or ']'</para></summary>
        public static ICompilerReferenceError CS0178
        {
            get
            {
                if (_CS0178 == null)
                    _CS0178 = new CompilerReferenceError(Resources.CSharpErrors_CS0178, 178);
                return _CS0178;
            }
        }
        private static ICompilerReferenceError _CS0178;

        /// <summary><para>C&#9839; compiler error &#35;179:</para><para>{0} cannot be extern and declare a body</para></summary>
        public static ICompilerReferenceError CS0179
        {
            get
            {
                if (_CS0179 == null)
                    _CS0179 = new CompilerReferenceError(Resources.CSharpErrors_CS0179, 179);
                return _CS0179;
            }
        }
        private static ICompilerReferenceError _CS0179;

        /// <summary><para>C&#9839; compiler error &#35;180:</para><para>{0} cannot be both extern and abstract</para></summary>
        public static ICompilerReferenceError CS0180
        {
            get
            {
                if (_CS0180 == null)
                    _CS0180 = new CompilerReferenceError(Resources.CSharpErrors_CS0180, 180);
                return _CS0180;
            }
        }
        private static ICompilerReferenceError _CS0180;

        /// <summary><para>C&#9839; compiler error &#35;182:</para><para>An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type</para></summary>
        public static ICompilerReferenceError CS0182
        {
            get
            {
                if (_CS0182 == null)
                    _CS0182 = new CompilerReferenceError(Resources.CSharpErrors_CS0182, 182);
                return _CS0182;
            }
        }
        private static ICompilerReferenceError _CS0182;

        /// <summary><para>C&#9839; compiler error &#35;185:</para><para>{0} is not a reference type as required by the lock statement</para></summary>
        public static ICompilerReferenceError CS0185
        {
            get
            {
                if (_CS0185 == null)
                    _CS0185 = new CompilerReferenceError(Resources.CSharpErrors_CS0185, 185);
                return _CS0185;
            }
        }
        private static ICompilerReferenceError _CS0185;

        /// <summary><para>C&#9839; compiler error &#35;186:</para><para>Use of null is not valid in this context </para></summary>
        public static ICompilerReferenceError CS0186
        {
            get
            {
                if (_CS0186 == null)
                    _CS0186 = new CompilerReferenceError(Resources.CSharpErrors_CS0186, 186);
                return _CS0186;
            }
        }
        private static ICompilerReferenceError _CS0186;

        /// <summary><para>C&#9839; compiler error &#35;188:</para><para>The 'this' object cannot be used before all of its fields are assigned to</para></summary>
        public static ICompilerReferenceError CS0188
        {
            get
            {
                if (_CS0188 == null)
                    _CS0188 = new CompilerReferenceError(Resources.CSharpErrors_CS0188, 188);
                return _CS0188;
            }
        }
        private static ICompilerReferenceError _CS0188;

        /// <summary><para>C&#9839; compiler error &#35;191:</para><para>Property or indexer {0} cannot be assigned to -- it is read only</para></summary>
        public static ICompilerReferenceError CS0191
        {
            get
            {
                if (_CS0191 == null)
                    _CS0191 = new CompilerReferenceError(Resources.CSharpErrors_CS0191, 191);
                return _CS0191;
            }
        }
        private static ICompilerReferenceError _CS0191;

        /// <summary><para>C&#9839; compiler error &#35;192:</para><para>Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)</para></summary>
        public static ICompilerReferenceError CS0192
        {
            get
            {
                if (_CS0192 == null)
                    _CS0192 = new CompilerReferenceError(Resources.CSharpErrors_CS0192, 192);
                return _CS0192;
            }
        }
        private static ICompilerReferenceError _CS0192;

        /// <summary><para>C&#9839; compiler error &#35;193:</para><para>The * or -> operator must be applied to a pointer</para></summary>
        public static ICompilerReferenceError CS0193
        {
            get
            {
                if (_CS0193 == null)
                    _CS0193 = new CompilerReferenceError(Resources.CSharpErrors_CS0193, 193);
                return _CS0193;
            }
        }
        private static ICompilerReferenceError _CS0193;

        /// <summary><para>C&#9839; compiler error &#35;196:</para><para>A pointer must be indexed by only one value</para></summary>
        public static ICompilerReferenceError CS0196
        {
            get
            {
                if (_CS0196 == null)
                    _CS0196 = new CompilerReferenceError(Resources.CSharpErrors_CS0196, 196);
                return _CS0196;
            }
        }
        private static ICompilerReferenceError _CS0196;

        /// <summary><para>C&#9839; compiler error &#35;198:</para><para>Fields of static readonly field {0} cannot be assigned to (except in a static constructor or a variable initializer)</para></summary>
        public static ICompilerReferenceError CS0198
        {
            get
            {
                if (_CS0198 == null)
                    _CS0198 = new CompilerReferenceError(Resources.CSharpErrors_CS0198, 198);
                return _CS0198;
            }
        }
        private static ICompilerReferenceError _CS0198;

        /// <summary><para>C&#9839; compiler error &#35;199:</para><para>Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)</para></summary>
        public static ICompilerReferenceError CS0199
        {
            get
            {
                if (_CS0199 == null)
                    _CS0199 = new CompilerReferenceError(Resources.CSharpErrors_CS0199, 199);
                return _CS0199;
            }
        }
        private static ICompilerReferenceError _CS0199;

        /// <summary><para>C&#9839; compiler error &#35;200:</para><para>Property or indexer {0} cannot be assigned to — it is read only</para></summary>
        public static ICompilerReferenceError CS0200
        {
            get
            {
                if (_CS0200 == null)
                    _CS0200 = new CompilerReferenceError(Resources.CSharpErrors_CS0200, 200);
                return _CS0200;
            }
        }
        private static ICompilerReferenceError _CS0200;

        /// <summary><para>C&#9839; compiler error &#35;201:</para><para>Only assignment, call, increment, decrement, and new object expressions can be used as a statement</para></summary>
        public static ICompilerReferenceError CS0201
        {
            get
            {
                if (_CS0201 == null)
                    _CS0201 = new CompilerReferenceError(Resources.CSharpErrors_CS0201, 201);
                return _CS0201;
            }
        }
        private static ICompilerReferenceError _CS0201;

        /// <summary><para>C&#9839; compiler error &#35;202:</para><para>foreach requires that the return type {0} of '{1}.GetEnumerator()' must have a suitable public MoveNext method and public Current property</para></summary>
        public static ICompilerReferenceError CS0202
        {
            get
            {
                if (_CS0202 == null)
                    _CS0202 = new CompilerReferenceError(Resources.CSharpErrors_CS0202, 202);
                return _CS0202;
            }
        }
        private static ICompilerReferenceError _CS0202;

        /// <summary><para>C&#9839; compiler error &#35;204:</para><para>Only 65534 locals are allowed</para></summary>
        public static ICompilerReferenceError CS0204
        {
            get
            {
                if (_CS0204 == null)
                    _CS0204 = new CompilerReferenceError(Resources.CSharpErrors_CS0204, 204);
                return _CS0204;
            }
        }
        private static ICompilerReferenceError _CS0204;

        /// <summary><para>C&#9839; compiler error &#35;205:</para><para>Cannot call an abstract base member: {0}</para></summary>
        public static ICompilerReferenceError CS0205
        {
            get
            {
                if (_CS0205 == null)
                    _CS0205 = new CompilerReferenceError(Resources.CSharpErrors_CS0205, 205);
                return _CS0205;
            }
        }
        private static ICompilerReferenceError _CS0205;

        /// <summary><para>C&#9839; compiler error &#35;206:</para><para>A property or indexer may not be passed as an out or ref parameter</para></summary>
        public static ICompilerReferenceError CS0206
        {
            get
            {
                if (_CS0206 == null)
                    _CS0206 = new CompilerReferenceError(Resources.CSharpErrors_CS0206, 206);
                return _CS0206;
            }
        }
        private static ICompilerReferenceError _CS0206;

        /// <summary><para>C&#9839; compiler error &#35;208:</para><para>Cannot take the address of, get the size of, or declare a pointer to a managed type ({0})</para></summary>
        public static ICompilerReferenceError CS0208
        {
            get
            {
                if (_CS0208 == null)
                    _CS0208 = new CompilerReferenceError(Resources.CSharpErrors_CS0208, 208);
                return _CS0208;
            }
        }
        private static ICompilerReferenceError _CS0208;

        /// <summary><para>C&#9839; compiler error &#35;209:</para><para>The type of local declared in a fixed statement must be a pointer type</para></summary>
        public static ICompilerReferenceError CS0209
        {
            get
            {
                if (_CS0209 == null)
                    _CS0209 = new CompilerReferenceError(Resources.CSharpErrors_CS0209, 209);
                return _CS0209;
            }
        }
        private static ICompilerReferenceError _CS0209;

        /// <summary><para>C&#9839; compiler error &#35;210:</para><para>You must provide an initializer in a fixed or using statement declaration</para></summary>
        public static ICompilerReferenceError CS0210
        {
            get
            {
                if (_CS0210 == null)
                    _CS0210 = new CompilerReferenceError(Resources.CSharpErrors_CS0210, 210);
                return _CS0210;
            }
        }
        private static ICompilerReferenceError _CS0210;

        /// <summary><para>C&#9839; compiler error &#35;211:</para><para>Cannot take the address of the given expression</para></summary>
        public static ICompilerReferenceError CS0211
        {
            get
            {
                if (_CS0211 == null)
                    _CS0211 = new CompilerReferenceError(Resources.CSharpErrors_CS0211, 211);
                return _CS0211;
            }
        }
        private static ICompilerReferenceError _CS0211;

        /// <summary><para>C&#9839; compiler error &#35;212:</para><para>You can only take the address of an unfixed expression inside of a fixed statement initializer</para></summary>
        public static ICompilerReferenceError CS0212
        {
            get
            {
                if (_CS0212 == null)
                    _CS0212 = new CompilerReferenceError(Resources.CSharpErrors_CS0212, 212);
                return _CS0212;
            }
        }
        private static ICompilerReferenceError _CS0212;

        /// <summary><para>C&#9839; compiler error &#35;213:</para><para>You cannot use the fixed statement to take the address of an already fixed expression</para></summary>
        public static ICompilerReferenceError CS0213
        {
            get
            {
                if (_CS0213 == null)
                    _CS0213 = new CompilerReferenceError(Resources.CSharpErrors_CS0213, 213);
                return _CS0213;
            }
        }
        private static ICompilerReferenceError _CS0213;

        /// <summary><para>C&#9839; compiler error &#35;214:</para><para>Pointers and fixed size buffers may only be used in an unsafe context</para></summary>
        public static ICompilerReferenceError CS0214
        {
            get
            {
                if (_CS0214 == null)
                    _CS0214 = new CompilerReferenceError(Resources.CSharpErrors_CS0214, 214);
                return _CS0214;
            }
        }
        private static ICompilerReferenceError _CS0214;

        /// <summary><para>C&#9839; compiler error &#35;215:</para><para>The return type of operator True or False must be bool</para></summary>
        public static ICompilerReferenceError CS0215
        {
            get
            {
                if (_CS0215 == null)
                    _CS0215 = new CompilerReferenceError(Resources.CSharpErrors_CS0215, 215);
                return _CS0215;
            }
        }
        private static ICompilerReferenceError _CS0215;

        /// <summary><para>C&#9839; compiler error &#35;216:</para><para>The operator {0} requires a matching operator {1} to also be defined</para></summary>
        public static ICompilerReferenceError CS0216
        {
            get
            {
                if (_CS0216 == null)
                    _CS0216 = new CompilerReferenceError(Resources.CSharpErrors_CS0216, 216);
                return _CS0216;
            }
        }
        private static ICompilerReferenceError _CS0216;

        /// <summary><para>C&#9839; compiler error &#35;217:</para><para>In order to be applicable as a short circuit operator a user-defined logical operator ({0}) must have the same return type as the type of its 2 parameters.</para></summary>
        public static ICompilerReferenceError CS0217
        {
            get
            {
                if (_CS0217 == null)
                    _CS0217 = new CompilerReferenceError(Resources.CSharpErrors_CS0217, 217);
                return _CS0217;
            }
        }
        private static ICompilerReferenceError _CS0217;

        /// <summary><para>C&#9839; compiler error &#35;218:</para><para>The type ({0}) must contain declarations of operator true and operator false</para></summary>
        public static ICompilerReferenceError CS0218
        {
            get
            {
                if (_CS0218 == null)
                    _CS0218 = new CompilerReferenceError(Resources.CSharpErrors_CS0218, 218);
                return _CS0218;
            }
        }
        private static ICompilerReferenceError _CS0218;

        /// <summary><para>C&#9839; compiler error &#35;220:</para><para>The operation overflows at compile time in checked mode</para></summary>
        public static ICompilerReferenceError CS0220
        {
            get
            {
                if (_CS0220 == null)
                    _CS0220 = new CompilerReferenceError(Resources.CSharpErrors_CS0220, 220);
                return _CS0220;
            }
        }
        private static ICompilerReferenceError _CS0220;

        /// <summary><para>C&#9839; compiler error &#35;221:</para><para>Constant value {0} cannot be converted to a {1} (use 'unchecked' syntax to override)</para></summary>
        public static ICompilerReferenceError CS0221
        {
            get
            {
                if (_CS0221 == null)
                    _CS0221 = new CompilerReferenceError(Resources.CSharpErrors_CS0221, 221);
                return _CS0221;
            }
        }
        private static ICompilerReferenceError _CS0221;

        /// <summary><para>C&#9839; compiler error &#35;225:</para><para>The params parameter must be a single dimensional array</para></summary>
        public static ICompilerReferenceError CS0225
        {
            get
            {
                if (_CS0225 == null)
                    _CS0225 = new CompilerReferenceError(Resources.CSharpErrors_CS0225, 225);
                return _CS0225;
            }
        }
        private static ICompilerReferenceError _CS0225;

        /// <summary><para>C&#9839; compiler error &#35;226:</para><para>An __arglist expression may only appear inside of a call or new expression.</para></summary>
        public static ICompilerReferenceError CS0226
        {
            get
            {
                if (_CS0226 == null)
                    _CS0226 = new CompilerReferenceError(Resources.CSharpErrors_CS0226, 226);
                return _CS0226;
            }
        }
        private static ICompilerReferenceError _CS0226;

        /// <summary><para>C&#9839; compiler error &#35;227:</para><para>Unsafe code may only appear if compiling with /unsafe</para></summary>
        public static ICompilerReferenceError CS0227
        {
            get
            {
                if (_CS0227 == null)
                    _CS0227 = new CompilerReferenceError(Resources.CSharpErrors_CS0227, 227);
                return _CS0227;
            }
        }
        private static ICompilerReferenceError _CS0227;

        /// <summary><para>C&#9839; compiler error &#35;228:</para><para>{0} does not contain a definition for {1}, or it is not accessible</para></summary>
        public static ICompilerReferenceError CS0228
        {
            get
            {
                if (_CS0228 == null)
                    _CS0228 = new CompilerReferenceError(Resources.CSharpErrors_CS0228, 228);
                return _CS0228;
            }
        }
        private static ICompilerReferenceError _CS0228;

        /// <summary><para>C&#9839; compiler error &#35;229:</para><para>Ambiguity between {0} and {1}</para></summary>
        public static ICompilerReferenceError CS0229
        {
            get
            {
                if (_CS0229 == null)
                    _CS0229 = new CompilerReferenceError(Resources.CSharpErrors_CS0229, 229);
                return _CS0229;
            }
        }
        private static ICompilerReferenceError _CS0229;

        /// <summary><para>C&#9839; compiler error &#35;230:</para><para>Type and identifier are both required in a foreach statement</para></summary>
        public static ICompilerReferenceError CS0230
        {
            get
            {
                if (_CS0230 == null)
                    _CS0230 = new CompilerReferenceError(Resources.CSharpErrors_CS0230, 230);
                return _CS0230;
            }
        }
        private static ICompilerReferenceError _CS0230;

        /// <summary><para>C&#9839; compiler error &#35;231:</para><para>A params parameter must be the last parameter in a formal parameter list.</para></summary>
        public static ICompilerReferenceError CS0231
        {
            get
            {
                if (_CS0231 == null)
                    _CS0231 = new CompilerReferenceError(Resources.CSharpErrors_CS0231, 231);
                return _CS0231;
            }
        }
        private static ICompilerReferenceError _CS0231;

        /// <summary><para>C&#9839; compiler error &#35;233:</para><para>{0} does not have a predefined size, therefore sizeof can only be used in an unsafe context (consider using System.Runtime.InteropServices.Marshal.SizeOf)</para></summary>
        public static ICompilerReferenceError CS0233
        {
            get
            {
                if (_CS0233 == null)
                    _CS0233 = new CompilerReferenceError(Resources.CSharpErrors_CS0233, 233);
                return _CS0233;
            }
        }
        private static ICompilerReferenceError _CS0233;

        /// <summary><para>C&#9839; compiler error &#35;234:</para><para>The type or namespace name {0} does not exist in the namespace {1} (are you missing an assembly reference?)</para></summary>
        public static ICompilerReferenceError CS0234
        {
            get
            {
                if (_CS0234 == null)
                    _CS0234 = new CompilerReferenceError(Resources.CSharpErrors_CS0234, 234);
                return _CS0234;
            }
        }
        private static ICompilerReferenceError _CS0234;

        /// <summary><para>C&#9839; compiler error &#35;236:</para><para>A field initializer cannot reference the nonstatic field, method, or property {0}</para></summary>
        public static ICompilerReferenceError CS0236
        {
            get
            {
                if (_CS0236 == null)
                    _CS0236 = new CompilerReferenceError(Resources.CSharpErrors_CS0236, 236);
                return _CS0236;
            }
        }
        private static ICompilerReferenceError _CS0236;

        /// <summary><para>C&#9839; compiler error &#35;238:</para><para>{0} cannot be sealed because it is not an override</para></summary>
        public static ICompilerReferenceError CS0238
        {
            get
            {
                if (_CS0238 == null)
                    _CS0238 = new CompilerReferenceError(Resources.CSharpErrors_CS0238, 238);
                return _CS0238;
            }
        }
        private static ICompilerReferenceError _CS0238;

        /// <summary><para>C&#9839; compiler error &#35;239:</para><para>{0} : cannot override inherited member {1} because it is sealed</para></summary>
        public static ICompilerReferenceError CS0239
        {
            get
            {
                if (_CS0239 == null)
                    _CS0239 = new CompilerReferenceError(Resources.CSharpErrors_CS0239, 239);
                return _CS0239;
            }
        }
        private static ICompilerReferenceError _CS0239;

        /// <summary><para>C&#9839; compiler error &#35;241:</para><para>Default parameter specifiers are not permitted</para></summary>
        public static ICompilerReferenceError CS0241
        {
            get
            {
                if (_CS0241 == null)
                    _CS0241 = new CompilerReferenceError(Resources.CSharpErrors_CS0241, 241);
                return _CS0241;
            }
        }
        private static ICompilerReferenceError _CS0241;

        /// <summary><para>C&#9839; compiler error &#35;242:</para><para>The operation in question is undefined on void pointers</para></summary>
        public static ICompilerReferenceError CS0242
        {
            get
            {
                if (_CS0242 == null)
                    _CS0242 = new CompilerReferenceError(Resources.CSharpErrors_CS0242, 242);
                return _CS0242;
            }
        }
        private static ICompilerReferenceError _CS0242;

        /// <summary><para>C&#9839; compiler error &#35;243:</para><para>The Conditional attribute is not valid on 'method' because it is an override method</para></summary>
        public static ICompilerReferenceError CS0243
        {
            get
            {
                if (_CS0243 == null)
                    _CS0243 = new CompilerReferenceError(Resources.CSharpErrors_CS0243, 243);
                return _CS0243;
            }
        }
        private static ICompilerReferenceError _CS0243;

        /// <summary><para>C&#9839; compiler error &#35;244:</para><para>Neither 'is' nor 'as' is valid on pointer types</para></summary>
        public static ICompilerReferenceError CS0244
        {
            get
            {
                if (_CS0244 == null)
                    _CS0244 = new CompilerReferenceError(Resources.CSharpErrors_CS0244, 244);
                return _CS0244;
            }
        }
        private static ICompilerReferenceError _CS0244;

        /// <summary><para>C&#9839; compiler error &#35;245:</para><para>Destructors and object.Finalize cannot be called directly. Consider calling IDisposable.Dispose if available.</para></summary>
        public static ICompilerReferenceError CS0245
        {
            get
            {
                if (_CS0245 == null)
                    _CS0245 = new CompilerReferenceError(Resources.CSharpErrors_CS0245, 245);
                return _CS0245;
            }
        }
        private static ICompilerReferenceError _CS0245;

        /// <summary><para>C&#9839; compiler error &#35;246:</para><para>The type or namespace name {0} could not be found (are you missing a using directive or an assembly reference?)</para></summary>
        public static ICompilerReferenceError CS0246
        {
            get
            {
                if (_CS0246 == null)
                    _CS0246 = new CompilerReferenceError(Resources.CSharpErrors_CS0246, 246);
                return _CS0246;
            }
        }
        private static ICompilerReferenceError _CS0246;

        /// <summary><para>C&#9839; compiler error &#35;247:</para><para>Cannot use a negative size with stackalloc</para></summary>
        public static ICompilerReferenceError CS0247
        {
            get
            {
                if (_CS0247 == null)
                    _CS0247 = new CompilerReferenceError(Resources.CSharpErrors_CS0247, 247);
                return _CS0247;
            }
        }
        private static ICompilerReferenceError _CS0247;

        /// <summary><para>C&#9839; compiler error &#35;248:</para><para>Cannot create an array with a negative size</para></summary>
        public static ICompilerReferenceError CS0248
        {
            get
            {
                if (_CS0248 == null)
                    _CS0248 = new CompilerReferenceError(Resources.CSharpErrors_CS0248, 248);
                return _CS0248;
            }
        }
        private static ICompilerReferenceError _CS0248;

        /// <summary><para>C&#9839; compiler error &#35;249:</para><para>Do not override object.Finalize. Instead, provide a destructor.</para></summary>
        public static ICompilerReferenceError CS0249
        {
            get
            {
                if (_CS0249 == null)
                    _CS0249 = new CompilerReferenceError(Resources.CSharpErrors_CS0249, 249);
                return _CS0249;
            }
        }
        private static ICompilerReferenceError _CS0249;

        /// <summary><para>C&#9839; compiler error &#35;250:</para><para>Do not directly call your base class Finalize method. It is called automatically from your destructor.</para></summary>
        public static ICompilerReferenceError CS0250
        {
            get
            {
                if (_CS0250 == null)
                    _CS0250 = new CompilerReferenceError(Resources.CSharpErrors_CS0250, 250);
                return _CS0250;
            }
        }
        private static ICompilerReferenceError _CS0250;

        /// <summary><para>C&#9839; compiler error &#35;254:</para><para>The right hand side of a fixed statement assignment may not be a cast expression</para></summary>
        public static ICompilerReferenceError CS0254
        {
            get
            {
                if (_CS0254 == null)
                    _CS0254 = new CompilerReferenceError(Resources.CSharpErrors_CS0254, 254);
                return _CS0254;
            }
        }
        private static ICompilerReferenceError _CS0254;

        /// <summary><para>C&#9839; compiler error &#35;255:</para><para>stackalloc may not be used in a catch or finally block</para></summary>
        public static ICompilerReferenceError CS0255
        {
            get
            {
                if (_CS0255 == null)
                    _CS0255 = new CompilerReferenceError(Resources.CSharpErrors_CS0255, 255);
                return _CS0255;
            }
        }
        private static ICompilerReferenceError _CS0255;

        /// <summary><para>C&#9839; compiler error &#35;260:</para><para>Missing partial modifier on declaration of type {0}; another partial declaration of this type exists</para></summary>
        public static ICompilerReferenceError CS0260
        {
            get
            {
                if (_CS0260 == null)
                    _CS0260 = new CompilerReferenceError(Resources.CSharpErrors_CS0260, 260);
                return _CS0260;
            }
        }
        private static ICompilerReferenceError _CS0260;

        /// <summary><para>C&#9839; compiler error &#35;261:</para><para>Partial declarations of {0} must be all classes, all structs, or all interfaces</para></summary>
        public static ICompilerReferenceError CS0261
        {
            get
            {
                if (_CS0261 == null)
                    _CS0261 = new CompilerReferenceError(Resources.CSharpErrors_CS0261, 261);
                return _CS0261;
            }
        }
        private static ICompilerReferenceError _CS0261;

        /// <summary><para>C&#9839; compiler error &#35;262:</para><para>Partial declarations of {0} have conflicting accessibility modifiers</para></summary>
        public static ICompilerReferenceError CS0262
        {
            get
            {
                if (_CS0262 == null)
                    _CS0262 = new CompilerReferenceError(Resources.CSharpErrors_CS0262, 262);
                return _CS0262;
            }
        }
        private static ICompilerReferenceError _CS0262;

        /// <summary><para>C&#9839; compiler error &#35;263:</para><para>Partial declarations of {0} must not specify different base classes</para></summary>
        public static ICompilerReferenceError CS0263
        {
            get
            {
                if (_CS0263 == null)
                    _CS0263 = new CompilerReferenceError(Resources.CSharpErrors_CS0263, 263);
                return _CS0263;
            }
        }
        private static ICompilerReferenceError _CS0263;

        /// <summary><para>C&#9839; compiler error &#35;264:</para><para>Partial declarations of {0} must have the same type parameter names in the same order</para></summary>
        public static ICompilerReferenceError CS0264
        {
            get
            {
                if (_CS0264 == null)
                    _CS0264 = new CompilerReferenceError(Resources.CSharpErrors_CS0264, 264);
                return _CS0264;
            }
        }
        private static ICompilerReferenceError _CS0264;

        /// <summary><para>C&#9839; compiler error &#35;265:</para><para>Partial declarations of {0} have inconsistent constraints for type parameter {1}</para></summary>
        public static ICompilerReferenceError CS0265
        {
            get
            {
                if (_CS0265 == null)
                    _CS0265 = new CompilerReferenceError(Resources.CSharpErrors_CS0265, 265);
                return _CS0265;
            }
        }
        private static ICompilerReferenceError _CS0265;

        /// <summary><para>C&#9839; compiler error &#35;266:</para><para>Cannot implicitly convert type {0} to {1}. An explicit conversion exists (are you missing a cast?)</para></summary>
        public static ICompilerReferenceError CS0266
        {
            get
            {
                if (_CS0266 == null)
                    _CS0266 = new CompilerReferenceError(Resources.CSharpErrors_CS0266, 266);
                return _CS0266;
            }
        }
        private static ICompilerReferenceError _CS0266;

        /// <summary><para>C&#9839; compiler error &#35;267:</para><para>The partial modifier can only appear immediately before 'class', 'struct', or 'interface'</para></summary>
        public static ICompilerReferenceError CS0267
        {
            get
            {
                if (_CS0267 == null)
                    _CS0267 = new CompilerReferenceError(Resources.CSharpErrors_CS0267, 267);
                return _CS0267;
            }
        }
        private static ICompilerReferenceError _CS0267;

        /// <summary><para>C&#9839; compiler error &#35;268:</para><para>Imported type {0} is invalid. It contains a circular base class dependency.</para></summary>
        public static ICompilerReferenceError CS0268
        {
            get
            {
                if (_CS0268 == null)
                    _CS0268 = new CompilerReferenceError(Resources.CSharpErrors_CS0268, 268);
                return _CS0268;
            }
        }
        private static ICompilerReferenceError _CS0268;

        /// <summary><para>C&#9839; compiler error &#35;269:</para><para>Use of unassigned out parameter {0}</para></summary>
        public static ICompilerReferenceError CS0269
        {
            get
            {
                if (_CS0269 == null)
                    _CS0269 = new CompilerReferenceError(Resources.CSharpErrors_CS0269, 269);
                return _CS0269;
            }
        }
        private static ICompilerReferenceError _CS0269;

        /// <summary><para>C&#9839; compiler error &#35;270:</para><para>Array size cannot be specified in a variable declaration (try initializing with a 'new' expression)</para></summary>
        public static ICompilerReferenceError CS0270
        {
            get
            {
                if (_CS0270 == null)
                    _CS0270 = new CompilerReferenceError(Resources.CSharpErrors_CS0270, 270);
                return _CS0270;
            }
        }
        private static ICompilerReferenceError _CS0270;

        /// <summary><para>C&#9839; compiler error &#35;271:</para><para>The property or indexer {0} cannot be used in this context because the get accessor is inaccessible</para></summary>
        public static ICompilerReferenceError CS0271
        {
            get
            {
                if (_CS0271 == null)
                    _CS0271 = new CompilerReferenceError(Resources.CSharpErrors_CS0271, 271);
                return _CS0271;
            }
        }
        private static ICompilerReferenceError _CS0271;

        /// <summary><para>C&#9839; compiler error &#35;272:</para><para>The property or indexer {0} cannot be used in this context because the set accessor is inaccessible</para></summary>
        public static ICompilerReferenceError CS0272
        {
            get
            {
                if (_CS0272 == null)
                    _CS0272 = new CompilerReferenceError(Resources.CSharpErrors_CS0272, 272);
                return _CS0272;
            }
        }
        private static ICompilerReferenceError _CS0272;

        /// <summary><para>C&#9839; compiler error &#35;273:</para><para>The accessibility modifier of the {0} accessor must be more restrictive than the property or indexer {1}</para></summary>
        public static ICompilerReferenceError CS0273
        {
            get
            {
                if (_CS0273 == null)
                    _CS0273 = new CompilerReferenceError(Resources.CSharpErrors_CS0273, 273);
                return _CS0273;
            }
        }
        private static ICompilerReferenceError _CS0273;

        /// <summary><para>C&#9839; compiler error &#35;274:</para><para>Cannot specify accessibility modifiers for both accessors of the property or indexer {0}</para></summary>
        public static ICompilerReferenceError CS0274
        {
            get
            {
                if (_CS0274 == null)
                    _CS0274 = new CompilerReferenceError(Resources.CSharpErrors_CS0274, 274);
                return _CS0274;
            }
        }
        private static ICompilerReferenceError _CS0274;

        /// <summary><para>C&#9839; compiler error &#35;275:</para><para>{0}: accessibility modifiers may not be used on accessors in an interface</para></summary>
        public static ICompilerReferenceError CS0275
        {
            get
            {
                if (_CS0275 == null)
                    _CS0275 = new CompilerReferenceError(Resources.CSharpErrors_CS0275, 275);
                return _CS0275;
            }
        }
        private static ICompilerReferenceError _CS0275;

        /// <summary><para>C&#9839; compiler error &#35;276:</para><para>{0}: accessibility modifiers on accessors may only be used if the property or indexer has both a get and a set accessor</para></summary>
        public static ICompilerReferenceError CS0276
        {
            get
            {
                if (_CS0276 == null)
                    _CS0276 = new CompilerReferenceError(Resources.CSharpErrors_CS0276, 276);
                return _CS0276;
            }
        }
        private static ICompilerReferenceError _CS0276;

        /// <summary><para>C&#9839; compiler error &#35;277:</para><para>{0} does not implement interface member {1}. {2} is not public</para></summary>
        public static ICompilerReferenceError CS0277
        {
            get
            {
                if (_CS0277 == null)
                    _CS0277 = new CompilerReferenceError(Resources.CSharpErrors_CS0277, 277);
                return _CS0277;
            }
        }
        private static ICompilerReferenceError _CS0277;

        /// <summary><para>C&#9839; compiler error &#35;281:</para><para>Friend access was granted to {0}, but the output assembly is named {1}. Try adding a reference to {0} or changing the output assembly name to match.</para></summary>
        public static ICompilerReferenceError CS0281
        {
            get
            {
                if (_CS0281 == null)
                    _CS0281 = new CompilerReferenceError(Resources.CSharpErrors_CS0281, 281);
                return _CS0281;
            }
        }
        private static ICompilerReferenceError _CS0281;

        /// <summary><para>C&#9839; compiler error &#35;283:</para><para>The type {0} cannot be declared const</para></summary>
        public static ICompilerReferenceError CS0283
        {
            get
            {
                if (_CS0283 == null)
                    _CS0283 = new CompilerReferenceError(Resources.CSharpErrors_CS0283, 283);
                return _CS0283;
            }
        }
        private static ICompilerReferenceError _CS0283;

        /// <summary><para>C&#9839; compiler error &#35;304:</para><para>Cannot create an instance of the variable type {0} because it does not have the new() constraint</para></summary>
        public static ICompilerReferenceError CS0304
        {
            get
            {
                if (_CS0304 == null)
                    _CS0304 = new CompilerReferenceError(Resources.CSharpErrors_CS0304, 304);
                return _CS0304;
            }
        }
        private static ICompilerReferenceError _CS0304;

        /// <summary><para>C&#9839; compiler error &#35;305:</para><para>Using the generic type {0} requires {1} type arguments</para></summary>
        public static ICompilerReferenceError CS0305
        {
            get
            {
                if (_CS0305 == null)
                    _CS0305 = new CompilerReferenceError(Resources.CSharpErrors_CS0305, 305);
                return _CS0305;
            }
        }
        private static ICompilerReferenceError _CS0305;

        /// <summary><para>C&#9839; compiler error &#35;306:</para><para>The type {0} may not be used as a type argument</para></summary>
        public static ICompilerReferenceError CS0306
        {
            get
            {
                if (_CS0306 == null)
                    _CS0306 = new CompilerReferenceError(Resources.CSharpErrors_CS0306, 306);
                return _CS0306;
            }
        }
        private static ICompilerReferenceError _CS0306;

        /// <summary><para>C&#9839; compiler error &#35;307:</para><para>The {0} {1} is not a generic method. If you intended an expression list, use parentheses around the &lt; expression.</para></summary>
        public static ICompilerReferenceError CS0307
        {
            get
            {
                if (_CS0307 == null)
                    _CS0307 = new CompilerReferenceError(Resources.CSharpErrors_CS0307, 307);
                return _CS0307;
            }
        }
        private static ICompilerReferenceError _CS0307;

        /// <summary><para>C&#9839; compiler error &#35;308:</para><para>The non-generic type-or-method {0} cannot be used with type arguments.</para></summary>
        public static ICompilerReferenceError CS0308
        {
            get
            {
                if (_CS0308 == null)
                    _CS0308 = new CompilerReferenceError(Resources.CSharpErrors_CS0308, 308);
                return _CS0308;
            }
        }
        private static ICompilerReferenceError _CS0308;

        /// <summary><para>C&#9839; compiler error &#35;310:</para><para>The type {0} must be a non-abstract type with a public parameterless constructor in order to use it as parameter {1} in the generic type or method {2}</para></summary>
        public static ICompilerReferenceError CS0310
        {
            get
            {
                if (_CS0310 == null)
                    _CS0310 = new CompilerReferenceError(Resources.CSharpErrors_CS0310, 310);
                return _CS0310;
            }
        }
        private static ICompilerReferenceError _CS0310;

        /// <summary><para>C&#9839; compiler error &#35;311:</para><para>The type {0} cannot be used as type parameter {2} in the generic type or method {3}. There is no implicit reference conversion from {0} to {1}.</para></summary>
        public static ICompilerReferenceError CS0311
        {
            get
            {
                if (_CS0311 == null)
                    _CS0311 = new CompilerReferenceError(Resources.CSharpErrors_CS0311, 311);
                return _CS0311;
            }
        }
        private static ICompilerReferenceError _CS0311;

        /// <summary><para>C&#9839; compiler error &#35;312:</para><para>The type {0} cannot be used as type parameter 'name' in the generic type or method 'name'. The nullable type 'type1' does not satisfy the constraint of 'type2'.</para></summary>
        public static ICompilerReferenceError CS0312
        {
            get
            {
                if (_CS0312 == null)
                    _CS0312 = new CompilerReferenceError(Resources.CSharpErrors_CS0312, 312);
                return _CS0312;
            }
        }
        private static ICompilerReferenceError _CS0312;

        /// <summary><para>C&#9839; compiler error &#35;313:</para><para>The type {0} cannot be used as type parameter 'parameter name' in the generic type or method 'type2'. The nullable type 'type1' does not satisfy the constraint of 'type2'. Nullable types cannot satisfy any interface constraints.</para></summary>
        public static ICompilerReferenceError CS0313
        {
            get
            {
                if (_CS0313 == null)
                    _CS0313 = new CompilerReferenceError(Resources.CSharpErrors_CS0313, 313);
                return _CS0313;
            }
        }
        private static ICompilerReferenceError _CS0313;

        /// <summary><para>C&#9839; compiler error &#35;314:</para><para>The type {0} cannot be used as type parameter 'name' in the generic type or method 'name'. There is no boxing conversion or type parameter conversion from 'type1' to 'type2'.</para></summary>
        public static ICompilerReferenceError CS0314
        {
            get
            {
                if (_CS0314 == null)
                    _CS0314 = new CompilerReferenceError(Resources.CSharpErrors_CS0314, 314);
                return _CS0314;
            }
        }
        private static ICompilerReferenceError _CS0314;

        /// <summary><para>C&#9839; compiler error &#35;315:</para><para>The type {0} cannot be used as type parameter 'T' in the generic type or method 'TypeorMethod&lt;T&gt;'. There is no boxing conversion from 'valueType' to 'referenceType'.</para></summary>
        public static ICompilerReferenceError CS0315
        {
            get
            {
                if (_CS0315 == null)
                    _CS0315 = new CompilerReferenceError(Resources.CSharpErrors_CS0315, 315);
                return _CS0315;
            }
        }
        private static ICompilerReferenceError _CS0315;

        /// <summary><para>C&#9839; compiler error &#35;316:</para><para>The parameter name {0} conflicts with an automatically-generated parameter name.</para></summary>
        public static ICompilerReferenceError CS0316
        {
            get
            {
                if (_CS0316 == null)
                    _CS0316 = new CompilerReferenceError(Resources.CSharpErrors_CS0316, 316);
                return _CS0316;
            }
        }
        private static ICompilerReferenceError _CS0316;

        /// <summary><para>C&#9839; compiler error &#35;400:</para><para>The type or namespace name {0} could not be found in the global namespace (are you missing an assembly reference?)</para></summary>
        public static ICompilerReferenceError CS0400
        {
            get
            {
                if (_CS0400 == null)
                    _CS0400 = new CompilerReferenceError(Resources.CSharpErrors_CS0400, 400);
                return _CS0400;
            }
        }
        private static ICompilerReferenceError _CS0400;

        /// <summary><para>C&#9839; compiler error &#35;401:</para><para>The new() constraint must be the last constraint specified</para></summary>
        public static ICompilerReferenceError CS0401
        {
            get
            {
                if (_CS0401 == null)
                    _CS0401 = new CompilerReferenceError(Resources.CSharpErrors_CS0401, 401);
                return _CS0401;
            }
        }
        private static ICompilerReferenceError _CS0401;

        /// <summary><para>C&#9839; compiler error &#35;403:</para><para>Cannot convert null to type parameter {0} because it could be a non-nullable value type. Consider using default({0}) instead.</para></summary>
        public static ICompilerReferenceError CS0403
        {
            get
            {
                if (_CS0403 == null)
                    _CS0403 = new CompilerReferenceError(Resources.CSharpErrors_CS0403, 403);
                return _CS0403;
            }
        }
        private static ICompilerReferenceError _CS0403;

        /// <summary><para>C&#9839; compiler error &#35;404:</para><para>'&lt;' unexpected : attributes cannot be generic</para></summary>
        public static ICompilerReferenceError CS0404
        {
            get
            {
                if (_CS0404 == null)
                    _CS0404 = new CompilerReferenceError(Resources.CSharpErrors_CS0404, 404);
                return _CS0404;
            }
        }
        private static ICompilerReferenceError _CS0404;

        /// <summary><para>C&#9839; compiler error &#35;405:</para><para>Duplicate constraint {0} for type parameter {1}</para></summary>
        public static ICompilerReferenceError CS0405
        {
            get
            {
                if (_CS0405 == null)
                    _CS0405 = new CompilerReferenceError(Resources.CSharpErrors_CS0405, 405);
                return _CS0405;
            }
        }
        private static ICompilerReferenceError _CS0405;

        /// <summary><para>C&#9839; compiler error &#35;406:</para><para>The class type constraint 'constraint' must come before any other constraints</para></summary>
        public static ICompilerReferenceError CS0406
        {
            get
            {
                if (_CS0406 == null)
                    _CS0406 = new CompilerReferenceError(Resources.CSharpErrors_CS0406, 406);
                return _CS0406;
            }
        }
        private static ICompilerReferenceError _CS0406;

        /// <summary><para>C&#9839; compiler error &#35;407:</para><para>'return-type method' has the wrong return type</para></summary>
        public static ICompilerReferenceError CS0407
        {
            get
            {
                if (_CS0407 == null)
                    _CS0407 = new CompilerReferenceError(Resources.CSharpErrors_CS0407, 407);
                return _CS0407;
            }
        }
        private static ICompilerReferenceError _CS0407;

        /// <summary><para>C&#9839; compiler error &#35;409:</para><para>A constraint clause has already been specified for type parameter 'type parameter'. All of the constraints for a type parameter must be specified in a single where clause.</para></summary>
        public static ICompilerReferenceError CS0409
        {
            get
            {
                if (_CS0409 == null)
                    _CS0409 = new CompilerReferenceError(Resources.CSharpErrors_CS0409, 409);
                return _CS0409;
            }
        }
        private static ICompilerReferenceError _CS0409;

        /// <summary><para>C&#9839; compiler error &#35;410:</para><para>No overload for 'method' has the correct parameter and return types</para></summary>
        public static ICompilerReferenceError CS0410
        {
            get
            {
                if (_CS0410 == null)
                    _CS0410 = new CompilerReferenceError(Resources.CSharpErrors_CS0410, 410);
                return _CS0410;
            }
        }
        private static ICompilerReferenceError _CS0410;

        /// <summary><para>C&#9839; compiler error &#35;411:</para><para>The type arguments for method 'method' cannot be inferred from the usage. Try specifying the type arguments explicitly.</para></summary>
        public static ICompilerReferenceError CS0411
        {
            get
            {
                if (_CS0411 == null)
                    _CS0411 = new CompilerReferenceError(Resources.CSharpErrors_CS0411, 411);
                return _CS0411;
            }
        }
        private static ICompilerReferenceError _CS0411;

        /// <summary><para>C&#9839; compiler error &#35;412:</para><para>'generic': a parameter or local variable cannot have the same name as a method type parameter</para></summary>
        public static ICompilerReferenceError CS0412
        {
            get
            {
                if (_CS0412 == null)
                    _CS0412 = new CompilerReferenceError(Resources.CSharpErrors_CS0412, 412);
                return _CS0412;
            }
        }
        private static ICompilerReferenceError _CS0412;

        /// <summary><para>C&#9839; compiler error &#35;413:</para><para>The type parameter 'type parameter' cannot be used with the 'as' operator because it does not have a class type constraint nor a 'class' constraint</para></summary>
        public static ICompilerReferenceError CS0413
        {
            get
            {
                if (_CS0413 == null)
                    _CS0413 = new CompilerReferenceError(Resources.CSharpErrors_CS0413, 413);
                return _CS0413;
            }
        }
        private static ICompilerReferenceError _CS0413;

        /// <summary><para>C&#9839; compiler error &#35;415:</para><para>The 'IndexerName' attribute is valid only on an indexer that is not an explicit interface member declaration</para></summary>
        public static ICompilerReferenceError CS0415
        {
            get
            {
                if (_CS0415 == null)
                    _CS0415 = new CompilerReferenceError(Resources.CSharpErrors_CS0415, 415);
                return _CS0415;
            }
        }
        private static ICompilerReferenceError _CS0415;

        /// <summary><para>C&#9839; compiler error &#35;416:</para><para>'type parameter': an attribute argument cannot use type parameters</para></summary>
        public static ICompilerReferenceError CS0416
        {
            get
            {
                if (_CS0416 == null)
                    _CS0416 = new CompilerReferenceError(Resources.CSharpErrors_CS0416, 416);
                return _CS0416;
            }
        }
        private static ICompilerReferenceError _CS0416;

        /// <summary><para>C&#9839; compiler error &#35;417:</para><para>'identifier': cannot provide arguments when creating an instance of a variable type</para></summary>
        public static ICompilerReferenceError CS0417
        {
            get
            {
                if (_CS0417 == null)
                    _CS0417 = new CompilerReferenceError(Resources.CSharpErrors_CS0417, 417);
                return _CS0417;
            }
        }
        private static ICompilerReferenceError _CS0417;

        /// <summary><para>C&#9839; compiler error &#35;418:</para><para>'class name': an abstract class cannot be sealed or static</para></summary>
        public static ICompilerReferenceError CS0418
        {
            get
            {
                if (_CS0418 == null)
                    _CS0418 = new CompilerReferenceError(Resources.CSharpErrors_CS0418, 418);
                return _CS0418;
            }
        }
        private static ICompilerReferenceError _CS0418;

        /// <summary><para>C&#9839; compiler error &#35;423:</para><para>Since 'class' has the ComImport attribute, 'method' must be extern or abstract</para></summary>
        public static ICompilerReferenceError CS0423
        {
            get
            {
                if (_CS0423 == null)
                    _CS0423 = new CompilerReferenceError(Resources.CSharpErrors_CS0423, 423);
                return _CS0423;
            }
        }
        private static ICompilerReferenceError _CS0423;

        /// <summary><para>C&#9839; compiler error &#35;424:</para><para>'class': a class with the ComImport attribute cannot specify a base class</para></summary>
        public static ICompilerReferenceError CS0424
        {
            get
            {
                if (_CS0424 == null)
                    _CS0424 = new CompilerReferenceError(Resources.CSharpErrors_CS0424, 424);
                return _CS0424;
            }
        }
        private static ICompilerReferenceError _CS0424;

        /// <summary><para>C&#9839; compiler error &#35;425:</para><para>The constraints for type parameter 'type parameter' of method 'method' must match the constraints for type parameter 'type parameter' of interface method 'method'. Consider using an explicit interface implementation instead.</para></summary>
        public static ICompilerReferenceError CS0425
        {
            get
            {
                if (_CS0425 == null)
                    _CS0425 = new CompilerReferenceError(Resources.CSharpErrors_CS0425, 425);
                return _CS0425;
            }
        }
        private static ICompilerReferenceError _CS0425;

        /// <summary><para>C&#9839; compiler error &#35;426:</para><para>The type name 'identifier' does not exist in the type 'type'</para></summary>
        public static ICompilerReferenceError CS0426
        {
            get
            {
                if (_CS0426 == null)
                    _CS0426 = new CompilerReferenceError(Resources.CSharpErrors_CS0426, 426);
                return _CS0426;
            }
        }
        private static ICompilerReferenceError _CS0426;

        /// <summary><para>C&#9839; compiler error &#35;428:</para><para>Cannot convert method group 'Identifier' to non-delegate type 'type'. Did you intend to invoke the method?</para></summary>
        public static ICompilerReferenceError CS0428
        {
            get
            {
                if (_CS0428 == null)
                    _CS0428 = new CompilerReferenceError(Resources.CSharpErrors_CS0428, 428);
                return _CS0428;
            }
        }
        private static ICompilerReferenceError _CS0428;

        /// <summary><para>C&#9839; compiler error &#35;430:</para><para>The extern alias 'alias' was not specified in a /reference option</para></summary>
        public static ICompilerReferenceError CS0430
        {
            get
            {
                if (_CS0430 == null)
                    _CS0430 = new CompilerReferenceError(Resources.CSharpErrors_CS0430, 430);
                return _CS0430;
            }
        }
        private static ICompilerReferenceError _CS0430;

        /// <summary><para>C&#9839; compiler error &#35;431:</para><para>Cannot use alias 'identifier' with '::' since the alias references a type. Use '.' instead.</para></summary>
        public static ICompilerReferenceError CS0431
        {
            get
            {
                if (_CS0431 == null)
                    _CS0431 = new CompilerReferenceError(Resources.CSharpErrors_CS0431, 431);
                return _CS0431;
            }
        }
        private static ICompilerReferenceError _CS0431;

        /// <summary><para>C&#9839; compiler error &#35;432:</para><para>Alias 'identifier' not found</para></summary>
        public static ICompilerReferenceError CS0432
        {
            get
            {
                if (_CS0432 == null)
                    _CS0432 = new CompilerReferenceError(Resources.CSharpErrors_CS0432, 432);
                return _CS0432;
            }
        }
        private static ICompilerReferenceError _CS0432;

        /// <summary><para>C&#9839; compiler error &#35;433:</para><para>The type TypeName1 exists in both TypeName2 and TypeName3</para></summary>
        public static ICompilerReferenceError CS0433
        {
            get
            {
                if (_CS0433 == null)
                    _CS0433 = new CompilerReferenceError(Resources.CSharpErrors_CS0433, 433);
                return _CS0433;
            }
        }
        private static ICompilerReferenceError _CS0433;

        /// <summary><para>C&#9839; compiler error &#35;434:</para><para>The namespace NamespaceName1 in NamespaceName2 conflicts with the type TypeName1 in NamespaceName3</para></summary>
        public static ICompilerReferenceError CS0434
        {
            get
            {
                if (_CS0434 == null)
                    _CS0434 = new CompilerReferenceError(Resources.CSharpErrors_CS0434, 434);
                return _CS0434;
            }
        }
        private static ICompilerReferenceError _CS0434;

        /// <summary><para>C&#9839; compiler error &#35;438:</para><para>The type 'type' in 'module_1' conflicts with the namespace 'namespace' in 'module_2'.</para></summary>
        public static ICompilerReferenceError CS0438
        {
            get
            {
                if (_CS0438 == null)
                    _CS0438 = new CompilerReferenceError(Resources.CSharpErrors_CS0438, 438);
                return _CS0438;
            }
        }
        private static ICompilerReferenceError _CS0438;

        /// <summary><para>C&#9839; compiler error &#35;439:</para><para>An extern alias declaration must precede all other elements defined in the namespace</para></summary>
        public static ICompilerReferenceError CS0439
        {
            get
            {
                if (_CS0439 == null)
                    _CS0439 = new CompilerReferenceError(Resources.CSharpErrors_CS0439, 439);
                return _CS0439;
            }
        }
        private static ICompilerReferenceError _CS0439;

        /// <summary><para>C&#9839; compiler error &#35;441:</para><para>'class': a class cannot be both static and sealed</para></summary>
        public static ICompilerReferenceError CS0441
        {
            get
            {
                if (_CS0441 == null)
                    _CS0441 = new CompilerReferenceError(Resources.CSharpErrors_CS0441, 441);
                return _CS0441;
            }
        }
        private static ICompilerReferenceError _CS0441;

        /// <summary><para>C&#9839; compiler error &#35;442:</para><para>'Property': abstract properties cannot have private accessors</para></summary>
        public static ICompilerReferenceError CS0442
        {
            get
            {
                if (_CS0442 == null)
                    _CS0442 = new CompilerReferenceError(Resources.CSharpErrors_CS0442, 442);
                return _CS0442;
            }
        }
        private static ICompilerReferenceError _CS0442;

        /// <summary><para>C&#9839; compiler error &#35;443:</para><para>Syntax error, value expected</para></summary>
        public static ICompilerReferenceError CS0443
        {
            get
            {
                if (_CS0443 == null)
                    _CS0443 = new CompilerReferenceError(Resources.CSharpErrors_CS0443, 443);
                return _CS0443;
            }
        }
        private static ICompilerReferenceError _CS0443;

        /// <summary><para>C&#9839; compiler error &#35;445:</para><para>Cannot modify the result of an unboxing conversion</para></summary>
        public static ICompilerReferenceError CS0445
        {
            get
            {
                if (_CS0445 == null)
                    _CS0445 = new CompilerReferenceError(Resources.CSharpErrors_CS0445, 445);
                return _CS0445;
            }
        }
        private static ICompilerReferenceError _CS0445;

        /// <summary><para>C&#9839; compiler error &#35;446:</para><para>Foreach cannot operate on a 'Method or Delegate'. Did you intend to invoke the 'Method or Delegate'?</para></summary>
        public static ICompilerReferenceError CS0446
        {
            get
            {
                if (_CS0446 == null)
                    _CS0446 = new CompilerReferenceError(Resources.CSharpErrors_CS0446, 446);
                return _CS0446;
            }
        }
        private static ICompilerReferenceError _CS0446;

        /// <summary><para>C&#9839; compiler error &#35;447:</para><para>Attributes cannot be used on type arguments, only on type parameters</para></summary>
        public static ICompilerReferenceError CS0447
        {
            get
            {
                if (_CS0447 == null)
                    _CS0447 = new CompilerReferenceError(Resources.CSharpErrors_CS0447, 447);
                return _CS0447;
            }
        }
        private static ICompilerReferenceError _CS0447;

        /// <summary><para>C&#9839; compiler error &#35;448:</para><para>The return type for ++ or -- operator must be the containing type or derived from the containing type</para></summary>
        public static ICompilerReferenceError CS0448
        {
            get
            {
                if (_CS0448 == null)
                    _CS0448 = new CompilerReferenceError(Resources.CSharpErrors_CS0448, 448);
                return _CS0448;
            }
        }
        private static ICompilerReferenceError _CS0448;

        /// <summary><para>C&#9839; compiler error &#35;449:</para><para>The 'class' or 'struct' constraint must come before any other constraints</para></summary>
        public static ICompilerReferenceError CS0449
        {
            get
            {
                if (_CS0449 == null)
                    _CS0449 = new CompilerReferenceError(Resources.CSharpErrors_CS0449, 449);
                return _CS0449;
            }
        }
        private static ICompilerReferenceError _CS0449;

        /// <summary><para>C&#9839; compiler error &#35;450:</para><para>'Type Parameter Name': cannot specify both a constraint class and the 'class' or 'struct' constraint</para></summary>
        public static ICompilerReferenceError CS0450
        {
            get
            {
                if (_CS0450 == null)
                    _CS0450 = new CompilerReferenceError(Resources.CSharpErrors_CS0450, 450);
                return _CS0450;
            }
        }
        private static ICompilerReferenceError _CS0450;

        /// <summary><para>C&#9839; compiler error &#35;451:</para><para>The 'new()' constraint cannot be used with the 'struct' constraint</para></summary>
        public static ICompilerReferenceError CS0451
        {
            get
            {
                if (_CS0451 == null)
                    _CS0451 = new CompilerReferenceError(Resources.CSharpErrors_CS0451, 451);
                return _CS0451;
            }
        }
        private static ICompilerReferenceError _CS0451;

        /// <summary><para>C&#9839; compiler error &#35;452:</para><para>The type 'type name' must be a reference type in order to use it as parameter 'parameter name' in the generic type or method 'identifier of generic'</para></summary>
        public static ICompilerReferenceError CS0452
        {
            get
            {
                if (_CS0452 == null)
                    _CS0452 = new CompilerReferenceError(Resources.CSharpErrors_CS0452, 452);
                return _CS0452;
            }
        }
        private static ICompilerReferenceError _CS0452;

        /// <summary><para>C&#9839; compiler error &#35;453:</para><para>The type 'Type Name' must be a non-nullable value type in order to use it as parameter 'Parameter Name' in the generic type or method 'Generic Identifier'</para></summary>
        public static ICompilerReferenceError CS0453
        {
            get
            {
                if (_CS0453 == null)
                    _CS0453 = new CompilerReferenceError(Resources.CSharpErrors_CS0453, 453);
                return _CS0453;
            }
        }
        private static ICompilerReferenceError _CS0453;

        /// <summary><para>C&#9839; compiler error &#35;454:</para><para>Circular constraint dependency involving 'Type Parameter 1' and 'Type Parameter 2'</para></summary>
        public static ICompilerReferenceError CS0454
        {
            get
            {
                if (_CS0454 == null)
                    _CS0454 = new CompilerReferenceError(Resources.CSharpErrors_CS0454, 454);
                return _CS0454;
            }
        }
        private static ICompilerReferenceError _CS0454;

        /// <summary><para>C&#9839; compiler error &#35;455:</para><para>Type parameter 'Type Parameter Name' inherits conflicting constraints 'Constraint Name 1' and 'Constraint Name 2'</para></summary>
        public static ICompilerReferenceError CS0455
        {
            get
            {
                if (_CS0455 == null)
                    _CS0455 = new CompilerReferenceError(Resources.CSharpErrors_CS0455, 455);
                return _CS0455;
            }
        }
        private static ICompilerReferenceError _CS0455;

        /// <summary><para>C&#9839; compiler error &#35;456:</para><para>Type parameter 'Type Parameter Name 1' has the 'struct' constraint so 'Type Parameter Name 1' cannot be used as a constraint for 'Type Parameter Name 2'</para></summary>
        public static ICompilerReferenceError CS0456
        {
            get
            {
                if (_CS0456 == null)
                    _CS0456 = new CompilerReferenceError(Resources.CSharpErrors_CS0456, 456);
                return _CS0456;
            }
        }
        private static ICompilerReferenceError _CS0456;

        /// <summary><para>C&#9839; compiler error &#35;457:</para><para>Ambiguous user defined conversions 'Conversion method name 1' and 'Conversion method name 2' when converting from 'type name 1' to 'type name 2'</para></summary>
        public static ICompilerReferenceError CS0457
        {
            get
            {
                if (_CS0457 == null)
                    _CS0457 = new CompilerReferenceError(Resources.CSharpErrors_CS0457, 457);
                return _CS0457;
            }
        }
        private static ICompilerReferenceError _CS0457;

        /// <summary><para>C&#9839; compiler error &#35;459:</para><para>Cannot take the address of a read-only local variable</para></summary>
        public static ICompilerReferenceError CS0459
        {
            get
            {
                if (_CS0459 == null)
                    _CS0459 = new CompilerReferenceError(Resources.CSharpErrors_CS0459, 459);
                return _CS0459;
            }
        }
        private static ICompilerReferenceError _CS0459;

        /// <summary><para>C&#9839; compiler error &#35;460:</para><para>Constraints for override and explicit interface implementation methods are inherited from the base method, so they cannot be specified directly</para></summary>
        public static ICompilerReferenceError CS0460
        {
            get
            {
                if (_CS0460 == null)
                    _CS0460 = new CompilerReferenceError(Resources.CSharpErrors_CS0460, 460);
                return _CS0460;
            }
        }
        private static ICompilerReferenceError _CS0460;

        /// <summary><para>C&#9839; compiler error &#35;462:</para><para>The inherited members 'member1' and 'member2' have the same signature in type 'type', so they cannot be overridden</para></summary>
        public static ICompilerReferenceError CS0462
        {
            get
            {
                if (_CS0462 == null)
                    _CS0462 = new CompilerReferenceError(Resources.CSharpErrors_CS0462, 462);
                return _CS0462;
            }
        }
        private static ICompilerReferenceError _CS0462;

        /// <summary><para>C&#9839; compiler error &#35;463:</para><para>Evaluation of the decimal constant expression failed with error: 'error'</para></summary>
        public static ICompilerReferenceError CS0463
        {
            get
            {
                if (_CS0463 == null)
                    _CS0463 = new CompilerReferenceError(Resources.CSharpErrors_CS0463, 463);
                return _CS0463;
            }
        }
        private static ICompilerReferenceError _CS0463;

        /// <summary><para>C&#9839; compiler error &#35;466:</para><para>'method1' should not have a params parameter since 'method2' does not</para></summary>
        public static ICompilerReferenceError CS0466
        {
            get
            {
                if (_CS0466 == null)
                    _CS0466 = new CompilerReferenceError(Resources.CSharpErrors_CS0466, 466);
                return _CS0466;
            }
        }
        private static ICompilerReferenceError _CS0466;

        /// <summary><para>C&#9839; compiler error &#35;468:</para><para>Ambiguity between type 'type1' and type 'type2'</para></summary>
        public static ICompilerReferenceError CS0468
        {
            get
            {
                if (_CS0468 == null)
                    _CS0468 = new CompilerReferenceError(Resources.CSharpErrors_CS0468, 468);
                return _CS0468;
            }
        }
        private static ICompilerReferenceError _CS0468;

        /// <summary><para>C&#9839; compiler error &#35;470:</para><para>Method 'method' cannot implement interface accessor 'accessor' for type 'type'. Use an explicit interface implementation.</para></summary>
        public static ICompilerReferenceError CS0470
        {
            get
            {
                if (_CS0470 == null)
                    _CS0470 = new CompilerReferenceError(Resources.CSharpErrors_CS0470, 470);
                return _CS0470;
            }
        }
        private static ICompilerReferenceError _CS0470;

        /// <summary><para>C&#9839; compiler error &#35;471:</para><para>The method 'name' is not a generic method. If you intended an expression list, use parentheses around the &lt; expression.</para></summary>
        public static ICompilerReferenceError CS0471
        {
            get
            {
                if (_CS0471 == null)
                    _CS0471 = new CompilerReferenceError(Resources.CSharpErrors_CS0471, 471);
                return _CS0471;
            }
        }
        private static ICompilerReferenceError _CS0471;

        /// <summary><para>C&#9839; compiler error &#35;473:</para><para>Explicit interface implementation 'method name' matches more than one interface member. Which interface member is actually chosen is implementation-dependent. Consider using a non-explicit implementation instead.</para></summary>
        public static ICompilerReferenceError CS0473
        {
            get
            {
                if (_CS0473 == null)
                    _CS0473 = new CompilerReferenceError(Resources.CSharpErrors_CS0473, 473);
                return _CS0473;
            }
        }
        private static ICompilerReferenceError _CS0473;

        /// <summary><para>C&#9839; compiler error &#35;500:</para><para>'class member' cannot declare a body because it is marked abstract</para></summary>
        public static ICompilerReferenceError CS0500
        {
            get
            {
                if (_CS0500 == null)
                    _CS0500 = new CompilerReferenceError(Resources.CSharpErrors_CS0500, 500);
                return _CS0500;
            }
        }
        private static ICompilerReferenceError _CS0500;

        /// <summary><para>C&#9839; compiler error &#35;501:</para><para>'member function' must declare a body because it is not marked abstract, extern, or partial</para></summary>
        public static ICompilerReferenceError CS0501
        {
            get
            {
                if (_CS0501 == null)
                    _CS0501 = new CompilerReferenceError(Resources.CSharpErrors_CS0501, 501);
                return _CS0501;
            }
        }
        private static ICompilerReferenceError _CS0501;

        /// <summary><para>C&#9839; compiler error &#35;502:</para><para>'member' cannot be both abstract and sealed</para></summary>
        public static ICompilerReferenceError CS0502
        {
            get
            {
                if (_CS0502 == null)
                    _CS0502 = new CompilerReferenceError(Resources.CSharpErrors_CS0502, 502);
                return _CS0502;
            }
        }
        private static ICompilerReferenceError _CS0502;

        /// <summary><para>C&#9839; compiler error &#35;503:</para><para>The abstract method 'method' cannot be marked virtual</para></summary>
        public static ICompilerReferenceError CS0503
        {
            get
            {
                if (_CS0503 == null)
                    _CS0503 = new CompilerReferenceError(Resources.CSharpErrors_CS0503, 503);
                return _CS0503;
            }
        }
        private static ICompilerReferenceError _CS0503;

        /// <summary><para>C&#9839; compiler error &#35;504:</para><para>The constant 'variable' cannot be marked static</para></summary>
        public static ICompilerReferenceError CS0504
        {
            get
            {
                if (_CS0504 == null)
                    _CS0504 = new CompilerReferenceError(Resources.CSharpErrors_CS0504, 504);
                return _CS0504;
            }
        }
        private static ICompilerReferenceError _CS0504;

        /// <summary><para>C&#9839; compiler error &#35;505:</para><para>'member1': cannot override because 'member2' is not a function</para></summary>
        public static ICompilerReferenceError CS0505
        {
            get
            {
                if (_CS0505 == null)
                    _CS0505 = new CompilerReferenceError(Resources.CSharpErrors_CS0505, 505);
                return _CS0505;
            }
        }
        private static ICompilerReferenceError _CS0505;

        /// <summary><para>C&#9839; compiler error &#35;506:</para><para>'function1' : cannot override inherited member 'function2' because it is not marked ""virtual"", ""abstract"", or ""override""</para></summary>
        public static ICompilerReferenceError CS0506
        {
            get
            {
                if (_CS0506 == null)
                    _CS0506 = new CompilerReferenceError(Resources.CSharpErrors_CS0506, 506);
                return _CS0506;
            }
        }
        private static ICompilerReferenceError _CS0506;

        /// <summary><para>C&#9839; compiler error &#35;507:</para><para>'function1' : cannot change access modifiers when overriding 'access' inherited member 'function2'</para></summary>
        public static ICompilerReferenceError CS0507
        {
            get
            {
                if (_CS0507 == null)
                    _CS0507 = new CompilerReferenceError(Resources.CSharpErrors_CS0507, 507);
                return _CS0507;
            }
        }
        private static ICompilerReferenceError _CS0507;

        /// <summary><para>C&#9839; compiler error &#35;508:</para><para>'Type 1': return type must be 'Type 2' to match overridden member 'Member Name'</para></summary>
        public static ICompilerReferenceError CS0508
        {
            get
            {
                if (_CS0508 == null)
                    _CS0508 = new CompilerReferenceError(Resources.CSharpErrors_CS0508, 508);
                return _CS0508;
            }
        }
        private static ICompilerReferenceError _CS0508;

        /// <summary><para>C&#9839; compiler error &#35;509:</para><para>'class1' : cannot derive from sealed type 'class2'</para></summary>
        public static ICompilerReferenceError CS0509
        {
            get
            {
                if (_CS0509 == null)
                    _CS0509 = new CompilerReferenceError(Resources.CSharpErrors_CS0509, 509);
                return _CS0509;
            }
        }
        private static ICompilerReferenceError _CS0509;

        /// <summary><para>C&#9839; compiler error &#35;513:</para><para>'function' is abstract but it is contained in nonabstract class 'class'</para></summary>
        public static ICompilerReferenceError CS0513
        {
            get
            {
                if (_CS0513 == null)
                    _CS0513 = new CompilerReferenceError(Resources.CSharpErrors_CS0513, 513);
                return _CS0513;
            }
        }
        private static ICompilerReferenceError _CS0513;

        /// <summary><para>C&#9839; compiler error &#35;514:</para><para>'constructor' : static constructor cannot have an explicit 'this' or 'base' constructor call</para></summary>
        public static ICompilerReferenceError CS0514
        {
            get
            {
                if (_CS0514 == null)
                    _CS0514 = new CompilerReferenceError(Resources.CSharpErrors_CS0514, 514);
                return _CS0514;
            }
        }
        private static ICompilerReferenceError _CS0514;

        /// <summary><para>C&#9839; compiler error &#35;515:</para><para>'function' : access modifiers are not allowed on static constructors</para></summary>
        public static ICompilerReferenceError CS0515
        {
            get
            {
                if (_CS0515 == null)
                    _CS0515 = new CompilerReferenceError(Resources.CSharpErrors_CS0515, 515);
                return _CS0515;
            }
        }
        private static ICompilerReferenceError _CS0515;

        /// <summary><para>C&#9839; compiler error &#35;516:</para><para>Constructor 'constructor' can not call itself</para></summary>
        public static ICompilerReferenceError CS0516
        {
            get
            {
                if (_CS0516 == null)
                    _CS0516 = new CompilerReferenceError(Resources.CSharpErrors_CS0516, 516);
                return _CS0516;
            }
        }
        private static ICompilerReferenceError _CS0516;

        /// <summary><para>C&#9839; compiler error &#35;517:</para><para>'class' has no base class and cannot call a base constructor</para></summary>
        public static ICompilerReferenceError CS0517
        {
            get
            {
                if (_CS0517 == null)
                    _CS0517 = new CompilerReferenceError(Resources.CSharpErrors_CS0517, 517);
                return _CS0517;
            }
        }
        private static ICompilerReferenceError _CS0517;

        /// <summary><para>C&#9839; compiler error &#35;518:</para><para>Predefined type 'type' is not defined or imported</para></summary>
        public static ICompilerReferenceError CS0518
        {
            get
            {
                if (_CS0518 == null)
                    _CS0518 = new CompilerReferenceError(Resources.CSharpErrors_CS0518, 518);
                return _CS0518;
            }
        }
        private static ICompilerReferenceError _CS0518;

        /// <summary><para>C&#9839; compiler error &#35;520:</para><para>Predefined type 'name' is declared incorrectly</para></summary>
        public static ICompilerReferenceError CS0520
        {
            get
            {
                if (_CS0520 == null)
                    _CS0520 = new CompilerReferenceError(Resources.CSharpErrors_CS0520, 520);
                return _CS0520;
            }
        }
        private static ICompilerReferenceError _CS0520;

        /// <summary><para>C&#9839; compiler error &#35;522:</para><para>'constructor' : structs cannot call base class constructors</para></summary>
        public static ICompilerReferenceError CS0522
        {
            get
            {
                if (_CS0522 == null)
                    _CS0522 = new CompilerReferenceError(Resources.CSharpErrors_CS0522, 522);
                return _CS0522;
            }
        }
        private static ICompilerReferenceError _CS0522;

        /// <summary><para>C&#9839; compiler error &#35;523:</para><para>Struct member 'struct2 field' of type 'struct1' causes a cycle in the struct layout</para></summary>
        public static ICompilerReferenceError CS0523
        {
            get
            {
                if (_CS0523 == null)
                    _CS0523 = new CompilerReferenceError(Resources.CSharpErrors_CS0523, 523);
                return _CS0523;
            }
        }
        private static ICompilerReferenceError _CS0523;

        /// <summary><para>C&#9839; compiler error &#35;524:</para><para>'type' : interfaces cannot declare types</para></summary>
        public static ICompilerReferenceError CS0524
        {
            get
            {
                if (_CS0524 == null)
                    _CS0524 = new CompilerReferenceError(Resources.CSharpErrors_CS0524, 524);
                return _CS0524;
            }
        }
        private static ICompilerReferenceError _CS0524;

        /// <summary><para>C&#9839; compiler error &#35;525:</para><para>Interfaces cannot contain fields</para></summary>
        public static ICompilerReferenceError CS0525
        {
            get
            {
                if (_CS0525 == null)
                    _CS0525 = new CompilerReferenceError(Resources.CSharpErrors_CS0525, 525);
                return _CS0525;
            }
        }
        private static ICompilerReferenceError _CS0525;

        /// <summary><para>C&#9839; compiler error &#35;526:</para><para>Interfaces cannot contain constructors</para></summary>
        public static ICompilerReferenceError CS0526
        {
            get
            {
                if (_CS0526 == null)
                    _CS0526 = new CompilerReferenceError(Resources.CSharpErrors_CS0526, 526);
                return _CS0526;
            }
        }
        private static ICompilerReferenceError _CS0526;

        /// <summary><para>C&#9839; compiler error &#35;527:</para><para>Type 'type' in interface list is not an interface</para></summary>
        public static ICompilerReferenceError CS0527
        {
            get
            {
                if (_CS0527 == null)
                    _CS0527 = new CompilerReferenceError(Resources.CSharpErrors_CS0527, 527);
                return _CS0527;
            }
        }
        private static ICompilerReferenceError _CS0527;

        /// <summary><para>C&#9839; compiler error &#35;528:</para><para>'interface' is already listed in interface list</para></summary>
        public static ICompilerReferenceError CS0528
        {
            get
            {
                if (_CS0528 == null)
                    _CS0528 = new CompilerReferenceError(Resources.CSharpErrors_CS0528, 528);
                return _CS0528;
            }
        }
        private static ICompilerReferenceError _CS0528;

        /// <summary><para>C&#9839; compiler error &#35;529:</para><para>Inherited interface 'interface1' causes a cycle in the interface hierarchy of 'interface2'</para></summary>
        public static ICompilerReferenceError CS0529
        {
            get
            {
                if (_CS0529 == null)
                    _CS0529 = new CompilerReferenceError(Resources.CSharpErrors_CS0529, 529);
                return _CS0529;
            }
        }
        private static ICompilerReferenceError _CS0529;

        /// <summary><para>C&#9839; compiler error &#35;531:</para><para>'member' : interface members cannot have a definition</para></summary>
        public static ICompilerReferenceError CS0531
        {
            get
            {
                if (_CS0531 == null)
                    _CS0531 = new CompilerReferenceError(Resources.CSharpErrors_CS0531, 531);
                return _CS0531;
            }
        }
        private static ICompilerReferenceError _CS0531;

        /// <summary><para>C&#9839; compiler error &#35;533:</para><para>'derived-class member' hides inherited abstract member 'base-class member'</para></summary>
        public static ICompilerReferenceError CS0533
        {
            get
            {
                if (_CS0533 == null)
                    _CS0533 = new CompilerReferenceError(Resources.CSharpErrors_CS0533, 533);
                return _CS0533;
            }
        }
        private static ICompilerReferenceError _CS0533;

        /// <summary><para>C&#9839; compiler error &#35;534:</para><para>'function1' does not implement inherited abstract member 'function2'</para></summary>
        public static ICompilerReferenceError CS0534
        {
            get
            {
                if (_CS0534 == null)
                    _CS0534 = new CompilerReferenceError(Resources.CSharpErrors_CS0534, 534);
                return _CS0534;
            }
        }
        private static ICompilerReferenceError _CS0534;

        /// <summary><para>C&#9839; compiler error &#35;535:</para><para>'class' does not implement interface member 'member'</para></summary>
        public static ICompilerReferenceError CS0535
        {
            get
            {
                if (_CS0535 == null)
                    _CS0535 = new CompilerReferenceError(Resources.CSharpErrors_CS0535, 535);
                return _CS0535;
            }
        }
        private static ICompilerReferenceError _CS0535;

        /// <summary><para>C&#9839; compiler error &#35;537:</para><para>The class System.Object cannot have a base class or implement an interface</para></summary>
        public static ICompilerReferenceError CS0537
        {
            get
            {
                if (_CS0537 == null)
                    _CS0537 = new CompilerReferenceError(Resources.CSharpErrors_CS0537, 537);
                return _CS0537;
            }
        }
        private static ICompilerReferenceError _CS0537;

        /// <summary><para>C&#9839; compiler error &#35;538:</para><para>'name' in explicit interface declaration is not an interface</para></summary>
        public static ICompilerReferenceError CS0538
        {
            get
            {
                if (_CS0538 == null)
                    _CS0538 = new CompilerReferenceError(Resources.CSharpErrors_CS0538, 538);
                return _CS0538;
            }
        }
        private static ICompilerReferenceError _CS0538;

        /// <summary><para>C&#9839; compiler error &#35;539:</para><para>'member' in explicit interface declaration is not a member of interface</para></summary>
        public static ICompilerReferenceError CS0539
        {
            get
            {
                if (_CS0539 == null)
                    _CS0539 = new CompilerReferenceError(Resources.CSharpErrors_CS0539, 539);
                return _CS0539;
            }
        }
        private static ICompilerReferenceError _CS0539;

        /// <summary><para>C&#9839; compiler error &#35;540:</para><para>'interface member' : containing type does not implement interface 'interface'</para></summary>
        public static ICompilerReferenceError CS0540
        {
            get
            {
                if (_CS0540 == null)
                    _CS0540 = new CompilerReferenceError(Resources.CSharpErrors_CS0540, 540);
                return _CS0540;
            }
        }
        private static ICompilerReferenceError _CS0540;

        /// <summary><para>C&#9839; compiler error &#35;541:</para><para>'declaration' : explicit interface declaration can only be declared in a class or struct</para></summary>
        public static ICompilerReferenceError CS0541
        {
            get
            {
                if (_CS0541 == null)
                    _CS0541 = new CompilerReferenceError(Resources.CSharpErrors_CS0541, 541);
                return _CS0541;
            }
        }
        private static ICompilerReferenceError _CS0541;

        /// <summary><para>C&#9839; compiler error &#35;542:</para><para>'user-defined type' : member names cannot be the same as their enclosing type</para></summary>
        public static ICompilerReferenceError CS0542
        {
            get
            {
                if (_CS0542 == null)
                    _CS0542 = new CompilerReferenceError(Resources.CSharpErrors_CS0542, 542);
                return _CS0542;
            }
        }
        private static ICompilerReferenceError _CS0542;

        /// <summary><para>C&#9839; compiler error &#35;543:</para><para>'enumeration' : the enumerator value is too large to fit in its type</para></summary>
        public static ICompilerReferenceError CS0543
        {
            get
            {
                if (_CS0543 == null)
                    _CS0543 = new CompilerReferenceError(Resources.CSharpErrors_CS0543, 543);
                return _CS0543;
            }
        }
        private static ICompilerReferenceError _CS0543;

        /// <summary><para>C&#9839; compiler error &#35;544:</para><para>'property override': cannot override because 'non-property' is not a property</para></summary>
        public static ICompilerReferenceError CS0544
        {
            get
            {
                if (_CS0544 == null)
                    _CS0544 = new CompilerReferenceError(Resources.CSharpErrors_CS0544, 544);
                return _CS0544;
            }
        }
        private static ICompilerReferenceError _CS0544;

        /// <summary><para>C&#9839; compiler error &#35;545:</para><para>'function' : cannot override because 'property' does not have an overridable get accessor</para></summary>
        public static ICompilerReferenceError CS0545
        {
            get
            {
                if (_CS0545 == null)
                    _CS0545 = new CompilerReferenceError(Resources.CSharpErrors_CS0545, 545);
                return _CS0545;
            }
        }
        private static ICompilerReferenceError _CS0545;

        /// <summary><para>C&#9839; compiler error &#35;546:</para><para>'accessor' : cannot override because 'property' does not have an overridable set accessor</para></summary>
        public static ICompilerReferenceError CS0546
        {
            get
            {
                if (_CS0546 == null)
                    _CS0546 = new CompilerReferenceError(Resources.CSharpErrors_CS0546, 546);
                return _CS0546;
            }
        }
        private static ICompilerReferenceError _CS0546;

        /// <summary><para>C&#9839; compiler error &#35;547:</para><para>'property' : property or indexer cannot have void type</para></summary>
        public static ICompilerReferenceError CS0547
        {
            get
            {
                if (_CS0547 == null)
                    _CS0547 = new CompilerReferenceError(Resources.CSharpErrors_CS0547, 547);
                return _CS0547;
            }
        }
        private static ICompilerReferenceError _CS0547;

        /// <summary><para>C&#9839; compiler error &#35;548:</para><para>'property' : property or indexer must have at least one accessor</para></summary>
        public static ICompilerReferenceError CS0548
        {
            get
            {
                if (_CS0548 == null)
                    _CS0548 = new CompilerReferenceError(Resources.CSharpErrors_CS0548, 548);
                return _CS0548;
            }
        }
        private static ICompilerReferenceError _CS0548;

        /// <summary><para>C&#9839; compiler error &#35;549:</para><para>'function' is a new virtual member in sealed class 'class'</para></summary>
        public static ICompilerReferenceError CS0549
        {
            get
            {
                if (_CS0549 == null)
                    _CS0549 = new CompilerReferenceError(Resources.CSharpErrors_CS0549, 549);
                return _CS0549;
            }
        }
        private static ICompilerReferenceError _CS0549;

        /// <summary><para>C&#9839; compiler error &#35;550:</para><para>'accessor' adds an accessor not found in interface member 'property'</para></summary>
        public static ICompilerReferenceError CS0550
        {
            get
            {
                if (_CS0550 == null)
                    _CS0550 = new CompilerReferenceError(Resources.CSharpErrors_CS0550, 550);
                return _CS0550;
            }
        }
        private static ICompilerReferenceError _CS0550;

        /// <summary><para>C&#9839; compiler error &#35;551:</para><para>Explicit interface implementation 'implementation' is missing accessor 'accessor'</para></summary>
        public static ICompilerReferenceError CS0551
        {
            get
            {
                if (_CS0551 == null)
                    _CS0551 = new CompilerReferenceError(Resources.CSharpErrors_CS0551, 551);
                return _CS0551;
            }
        }
        private static ICompilerReferenceError _CS0551;

        /// <summary><para>C&#9839; compiler error &#35;552:</para><para>'conversion routine' : user defined conversion to/from interface</para></summary>
        public static ICompilerReferenceError CS0552
        {
            get
            {
                if (_CS0552 == null)
                    _CS0552 = new CompilerReferenceError(Resources.CSharpErrors_CS0552, 552);
                return _CS0552;
            }
        }
        private static ICompilerReferenceError _CS0552;

        /// <summary><para>C&#9839; compiler error &#35;553:</para><para>'conversion routine' : user defined conversion to/from base class</para></summary>
        public static ICompilerReferenceError CS0553
        {
            get
            {
                if (_CS0553 == null)
                    _CS0553 = new CompilerReferenceError(Resources.CSharpErrors_CS0553, 553);
                return _CS0553;
            }
        }
        private static ICompilerReferenceError _CS0553;

        /// <summary><para>C&#9839; compiler error &#35;554:</para><para>'conversion routine' : user defined conversion to/from derived class</para></summary>
        public static ICompilerReferenceError CS0554
        {
            get
            {
                if (_CS0554 == null)
                    _CS0554 = new CompilerReferenceError(Resources.CSharpErrors_CS0554, 554);
                return _CS0554;
            }
        }
        private static ICompilerReferenceError _CS0554;

        /// <summary><para>C&#9839; compiler error &#35;555:</para><para>User-defined operator cannot take an object of the enclosing type and convert to an object of the enclosing type</para></summary>
        public static ICompilerReferenceError CS0555
        {
            get
            {
                if (_CS0555 == null)
                    _CS0555 = new CompilerReferenceError(Resources.CSharpErrors_CS0555, 555);
                return _CS0555;
            }
        }
        private static ICompilerReferenceError _CS0555;

        /// <summary><para>C&#9839; compiler error &#35;556:</para><para>User-defined conversion must convert to or from the enclosing type</para></summary>
        public static ICompilerReferenceError CS0556
        {
            get
            {
                if (_CS0556 == null)
                    _CS0556 = new CompilerReferenceError(Resources.CSharpErrors_CS0556, 556);
                return _CS0556;
            }
        }
        private static ICompilerReferenceError _CS0556;

        /// <summary><para>C&#9839; compiler error &#35;557:</para><para>Duplicate user-defined conversion in type 'class'</para></summary>
        public static ICompilerReferenceError CS0557
        {
            get
            {
                if (_CS0557 == null)
                    _CS0557 = new CompilerReferenceError(Resources.CSharpErrors_CS0557, 557);
                return _CS0557;
            }
        }
        private static ICompilerReferenceError _CS0557;

        /// <summary><para>C&#9839; compiler error &#35;558:</para><para>User-defined operator 'operator' must be declared static and public</para></summary>
        public static ICompilerReferenceError CS0558
        {
            get
            {
                if (_CS0558 == null)
                    _CS0558 = new CompilerReferenceError(Resources.CSharpErrors_CS0558, 558);
                return _CS0558;
            }
        }
        private static ICompilerReferenceError _CS0558;

        /// <summary><para>C&#9839; compiler error &#35;559:</para><para>The parameter type for ++ or -- operator must be the containing type</para></summary>
        public static ICompilerReferenceError CS0559
        {
            get
            {
                if (_CS0559 == null)
                    _CS0559 = new CompilerReferenceError(Resources.CSharpErrors_CS0559, 559);
                return _CS0559;
            }
        }
        private static ICompilerReferenceError _CS0559;

        /// <summary><para>C&#9839; compiler error &#35;562:</para><para>The parameter of a unary operator must be the containing type</para></summary>
        public static ICompilerReferenceError CS0562
        {
            get
            {
                if (_CS0562 == null)
                    _CS0562 = new CompilerReferenceError(Resources.CSharpErrors_CS0562, 562);
                return _CS0562;
            }
        }
        private static ICompilerReferenceError _CS0562;

        /// <summary><para>C&#9839; compiler error &#35;563:</para><para>One of the parameters of a binary operator must be the containing type</para></summary>
        public static ICompilerReferenceError CS0563
        {
            get
            {
                if (_CS0563 == null)
                    _CS0563 = new CompilerReferenceError(Resources.CSharpErrors_CS0563, 563);
                return _CS0563;
            }
        }
        private static ICompilerReferenceError _CS0563;

        /// <summary><para>C&#9839; compiler error &#35;564:</para><para>The first operand of an overloaded shift operator must have the same type as the containing type, and the type of the second operand must be int</para></summary>
        public static ICompilerReferenceError CS0564
        {
            get
            {
                if (_CS0564 == null)
                    _CS0564 = new CompilerReferenceError(Resources.CSharpErrors_CS0564, 564);
                return _CS0564;
            }
        }
        private static ICompilerReferenceError _CS0564;

        /// <summary><para>C&#9839; compiler error &#35;567:</para><para>Interfaces cannot contain operators</para></summary>
        public static ICompilerReferenceError CS0567
        {
            get
            {
                if (_CS0567 == null)
                    _CS0567 = new CompilerReferenceError(Resources.CSharpErrors_CS0567, 567);
                return _CS0567;
            }
        }
        private static ICompilerReferenceError _CS0567;

        /// <summary><para>C&#9839; compiler error &#35;568:</para><para>Structs cannot contain explicit parameterless constructors</para></summary>
        public static ICompilerReferenceError CS0568
        {
            get
            {
                if (_CS0568 == null)
                    _CS0568 = new CompilerReferenceError(Resources.CSharpErrors_CS0568, 568);
                return _CS0568;
            }
        }
        private static ICompilerReferenceError _CS0568;

        /// <summary><para>C&#9839; compiler error &#35;569:</para><para>'method2' : cannot override 'method1' because it is not supported by the language</para></summary>
        public static ICompilerReferenceError CS0569
        {
            get
            {
                if (_CS0569 == null)
                    _CS0569 = new CompilerReferenceError(Resources.CSharpErrors_CS0569, 569);
                return _CS0569;
            }
        }
        private static ICompilerReferenceError _CS0569;

        /// <summary><para>C&#9839; compiler error &#35;570:</para><para>Property, indexer, or event 'name' is not supported by the language; try directly calling accessor method 'name!'</para></summary>
        public static ICompilerReferenceError CS0570
        {
            get
            {
                if (_CS0570 == null)
                    _CS0570 = new CompilerReferenceError(Resources.CSharpErrors_CS0570, 570);
                return _CS0570;
            }
        }
        private static ICompilerReferenceError _CS0570;

        /// <summary><para>C&#9839; compiler error &#35;571:</para><para>'function' : cannot explicitly call operator or accessor</para></summary>
        public static ICompilerReferenceError CS0571
        {
            get
            {
                if (_CS0571 == null)
                    _CS0571 = new CompilerReferenceError(Resources.CSharpErrors_CS0571, 571);
                return _CS0571;
            }
        }
        private static ICompilerReferenceError _CS0571;

        /// <summary><para>C&#9839; compiler error &#35;572:</para><para>'type' : cannot reference a type through an expression; try 'path_to_type' instead</para></summary>
        public static ICompilerReferenceError CS0572
        {
            get
            {
                if (_CS0572 == null)
                    _CS0572 = new CompilerReferenceError(Resources.CSharpErrors_CS0572, 572);
                return _CS0572;
            }
        }
        private static ICompilerReferenceError _CS0572;

        /// <summary><para>C&#9839; compiler error &#35;573:</para><para>'field declaration' : cannot have instance field initializers in structs</para></summary>
        public static ICompilerReferenceError CS0573
        {
            get
            {
                if (_CS0573 == null)
                    _CS0573 = new CompilerReferenceError(Resources.CSharpErrors_CS0573, 573);
                return _CS0573;
            }
        }
        private static ICompilerReferenceError _CS0573;

        /// <summary><para>C&#9839; compiler error &#35;574:</para><para>Name of destructor must match name of class</para></summary>
        public static ICompilerReferenceError CS0574
        {
            get
            {
                if (_CS0574 == null)
                    _CS0574 = new CompilerReferenceError(Resources.CSharpErrors_CS0574, 574);
                return _CS0574;
            }
        }
        private static ICompilerReferenceError _CS0574;

        /// <summary><para>C&#9839; compiler error &#35;575:</para><para>Only class types can contain destructors</para></summary>
        public static ICompilerReferenceError CS0575
        {
            get
            {
                if (_CS0575 == null)
                    _CS0575 = new CompilerReferenceError(Resources.CSharpErrors_CS0575, 575);
                return _CS0575;
            }
        }
        private static ICompilerReferenceError _CS0575;

        /// <summary><para>C&#9839; compiler error &#35;576:</para><para>Namespace 'namespace' contains a definition conflicting with alias 'identifier'</para></summary>
        public static ICompilerReferenceError CS0576
        {
            get
            {
                if (_CS0576 == null)
                    _CS0576 = new CompilerReferenceError(Resources.CSharpErrors_CS0576, 576);
                return _CS0576;
            }
        }
        private static ICompilerReferenceError _CS0576;

        /// <summary><para>C&#9839; compiler error &#35;577:</para><para>The Conditional attribute is not valid on 'function' because it is a constructor, destructor, operator, or explicit interface implementation</para></summary>
        public static ICompilerReferenceError CS0577
        {
            get
            {
                if (_CS0577 == null)
                    _CS0577 = new CompilerReferenceError(Resources.CSharpErrors_CS0577, 577);
                return _CS0577;
            }
        }
        private static ICompilerReferenceError _CS0577;

        /// <summary><para>C&#9839; compiler error &#35;578:</para><para>The Conditional attribute is not valid on 'function' because its return type is not void</para></summary>
        public static ICompilerReferenceError CS0578
        {
            get
            {
                if (_CS0578 == null)
                    _CS0578 = new CompilerReferenceError(Resources.CSharpErrors_CS0578, 578);
                return _CS0578;
            }
        }
        private static ICompilerReferenceError _CS0578;

        /// <summary><para>C&#9839; compiler error &#35;579:</para><para>Duplicate 'attribute' attribute</para></summary>
        public static ICompilerReferenceError CS0579
        {
            get
            {
                if (_CS0579 == null)
                    _CS0579 = new CompilerReferenceError(Resources.CSharpErrors_CS0579, 579);
                return _CS0579;
            }
        }
        private static ICompilerReferenceError _CS0579;

        /// <summary><para>C&#9839; compiler error &#35;582:</para><para>The Conditional not valid on interface members</para></summary>
        public static ICompilerReferenceError CS0582
        {
            get
            {
                if (_CS0582 == null)
                    _CS0582 = new CompilerReferenceError(Resources.CSharpErrors_CS0582, 582);
                return _CS0582;
            }
        }
        private static ICompilerReferenceError _CS0582;

        /// <summary><para>C&#9839; compiler error &#35;583:</para><para>Internal Compiler Error. An internal error has occurred in the compiler. To work around this problem, try simplifying or changing the program near the locations listed below. Locations at the top of the list are closer to the point at which the internal error occurred. Errors such as this can be reported to Microsoft by using the /errorreport option.</para></summary>
        public static ICompilerReferenceError CS0583
        {
            get
            {
                if (_CS0583 == null)
                    _CS0583 = new CompilerReferenceError(Resources.CSharpErrors_CS0583, 583);
                return _CS0583;
            }
        }
        private static ICompilerReferenceError _CS0583;

        /// <summary><para>C&#9839; compiler error &#35;584:</para><para>Internal Compiler Error: stage 'stage' symbol 'symbol'</para></summary>
        public static ICompilerReferenceError CS0584
        {
            get
            {
                if (_CS0584 == null)
                    _CS0584 = new CompilerReferenceError(Resources.CSharpErrors_CS0584, 584);
                return _CS0584;
            }
        }
        private static ICompilerReferenceError _CS0584;

        /// <summary><para>C&#9839; compiler error &#35;585:</para><para>Internal Compiler Error: stage 'stage'</para></summary>
        public static ICompilerReferenceError CS0585
        {
            get
            {
                if (_CS0585 == null)
                    _CS0585 = new CompilerReferenceError(Resources.CSharpErrors_CS0585, 585);
                return _CS0585;
            }
        }
        private static ICompilerReferenceError _CS0585;

        /// <summary><para>C&#9839; compiler error &#35;586:</para><para>Internal Compiler Error: stage 'stage'</para></summary>
        public static ICompilerReferenceError CS0586
        {
            get
            {
                if (_CS0586 == null)
                    _CS0586 = new CompilerReferenceError(Resources.CSharpErrors_CS0586, 586);
                return _CS0586;
            }
        }
        private static ICompilerReferenceError _CS0586;

        /// <summary><para>C&#9839; compiler error &#35;587:</para><para>Internal Compiler Error: stage 'stage'</para></summary>
        public static ICompilerReferenceError CS0587
        {
            get
            {
                if (_CS0587 == null)
                    _CS0587 = new CompilerReferenceError(Resources.CSharpErrors_CS0587, 587);
                return _CS0587;
            }
        }
        private static ICompilerReferenceError _CS0587;

        /// <summary><para>C&#9839; compiler error &#35;588:</para><para>Internal Compiler Error: stage 'LEX'</para></summary>
        public static ICompilerReferenceError CS0588
        {
            get
            {
                if (_CS0588 == null)
                    _CS0588 = new CompilerReferenceError(Resources.CSharpErrors_CS0588, 588);
                return _CS0588;
            }
        }
        private static ICompilerReferenceError _CS0588;

        /// <summary><para>C&#9839; compiler error &#35;589:</para><para>Internal Compiler Error: stage 'PARSE'</para></summary>
        public static ICompilerReferenceError CS0589
        {
            get
            {
                if (_CS0589 == null)
                    _CS0589 = new CompilerReferenceError(Resources.CSharpErrors_CS0589, 589);
                return _CS0589;
            }
        }
        private static ICompilerReferenceError _CS0589;

        /// <summary><para>C&#9839; compiler error &#35;590:</para><para>User-defined operators cannot return void</para></summary>
        public static ICompilerReferenceError CS0590
        {
            get
            {
                if (_CS0590 == null)
                    _CS0590 = new CompilerReferenceError(Resources.CSharpErrors_CS0590, 590);
                return _CS0590;
            }
        }
        private static ICompilerReferenceError _CS0590;

        /// <summary><para>C&#9839; compiler error &#35;591:</para><para>Invalid value for argument to 'attribute' attribute</para></summary>
        public static ICompilerReferenceError CS0591
        {
            get
            {
                if (_CS0591 == null)
                    _CS0591 = new CompilerReferenceError(Resources.CSharpErrors_CS0591, 591);
                return _CS0591;
            }
        }
        private static ICompilerReferenceError _CS0591;

        /// <summary><para>C&#9839; compiler error &#35;592:</para><para>Attribute 'attribute' is not valid on this declaration type. It is valid on 'type' declarations only.</para></summary>
        public static ICompilerReferenceError CS0592
        {
            get
            {
                if (_CS0592 == null)
                    _CS0592 = new CompilerReferenceError(Resources.CSharpErrors_CS0592, 592);
                return _CS0592;
            }
        }
        private static ICompilerReferenceError _CS0592;

        /// <summary><para>C&#9839; compiler error &#35;594:</para><para>Floating-point constant is outside the range of type 'type'</para></summary>
        public static ICompilerReferenceError CS0594
        {
            get
            {
                if (_CS0594 == null)
                    _CS0594 = new CompilerReferenceError(Resources.CSharpErrors_CS0594, 594);
                return _CS0594;
            }
        }
        private static ICompilerReferenceError _CS0594;

        /// <summary><para>C&#9839; compiler error &#35;596:</para><para>The Guid attribute must be specified with the ComImport attribute</para></summary>
        public static ICompilerReferenceError CS0596
        {
            get
            {
                if (_CS0596 == null)
                    _CS0596 = new CompilerReferenceError(Resources.CSharpErrors_CS0596, 596);
                return _CS0596;
            }
        }
        private static ICompilerReferenceError _CS0596;

        /// <summary><para>C&#9839; compiler error &#35;599:</para><para>Invalid value for named attribute argument 'argument'</para></summary>
        public static ICompilerReferenceError CS0599
        {
            get
            {
                if (_CS0599 == null)
                    _CS0599 = new CompilerReferenceError(Resources.CSharpErrors_CS0599, 599);
                return _CS0599;
            }
        }
        private static ICompilerReferenceError _CS0599;

        /// <summary><para>C&#9839; compiler error &#35;601:</para><para>The DllImport attribute must be specified on a method marked 'static' and 'extern'</para></summary>
        public static ICompilerReferenceError CS0601
        {
            get
            {
                if (_CS0601 == null)
                    _CS0601 = new CompilerReferenceError(Resources.CSharpErrors_CS0601, 601);
                return _CS0601;
            }
        }
        private static ICompilerReferenceError _CS0601;

        /// <summary><para>C&#9839; compiler error &#35;609:</para><para>Cannot set the IndexerName attribute on an indexer marked override</para></summary>
        public static ICompilerReferenceError CS0609
        {
            get
            {
                if (_CS0609 == null)
                    _CS0609 = new CompilerReferenceError(Resources.CSharpErrors_CS0609, 609);
                return _CS0609;
            }
        }
        private static ICompilerReferenceError _CS0609;

        /// <summary><para>C&#9839; compiler error &#35;610:</para><para>Field or property cannot be of type 'type'</para></summary>
        public static ICompilerReferenceError CS0610
        {
            get
            {
                if (_CS0610 == null)
                    _CS0610 = new CompilerReferenceError(Resources.CSharpErrors_CS0610, 610);
                return _CS0610;
            }
        }
        private static ICompilerReferenceError _CS0610;

        /// <summary><para>C&#9839; compiler error &#35;611:</para><para>Array elements cannot be of type 'type'</para></summary>
        public static ICompilerReferenceError CS0611
        {
            get
            {
                if (_CS0611 == null)
                    _CS0611 = new CompilerReferenceError(Resources.CSharpErrors_CS0611, 611);
                return _CS0611;
            }
        }
        private static ICompilerReferenceError _CS0611;

        /// <summary><para>C&#9839; compiler error &#35;616:</para><para>'class' is not an attribute class</para></summary>
        public static ICompilerReferenceError CS0616
        {
            get
            {
                if (_CS0616 == null)
                    _CS0616 = new CompilerReferenceError(Resources.CSharpErrors_CS0616, 616);
                return _CS0616;
            }
        }
        private static ICompilerReferenceError _CS0616;

        /// <summary><para>C&#9839; compiler error &#35;617:</para><para>'reference' is not a valid named attribute argument because it is not a valid attribute parameter type</para></summary>
        public static ICompilerReferenceError CS0617
        {
            get
            {
                if (_CS0617 == null)
                    _CS0617 = new CompilerReferenceError(Resources.CSharpErrors_CS0617, 617);
                return _CS0617;
            }
        }
        private static ICompilerReferenceError _CS0617;

        /// <summary><para>C&#9839; compiler error &#35;619:</para><para>'member' is obsolete: 'text'</para></summary>
        public static ICompilerReferenceError CS0619
        {
            get
            {
                if (_CS0619 == null)
                    _CS0619 = new CompilerReferenceError(Resources.CSharpErrors_CS0619, 619);
                return _CS0619;
            }
        }
        private static ICompilerReferenceError _CS0619;

        /// <summary><para>C&#9839; compiler error &#35;620:</para><para>Indexers cannot have void type</para></summary>
        public static ICompilerReferenceError CS0620
        {
            get
            {
                if (_CS0620 == null)
                    _CS0620 = new CompilerReferenceError(Resources.CSharpErrors_CS0620, 620);
                return _CS0620;
            }
        }
        private static ICompilerReferenceError _CS0620;

        /// <summary><para>C&#9839; compiler error &#35;621:</para><para>'member' : virtual or abstract members cannot be private</para></summary>
        public static ICompilerReferenceError CS0621
        {
            get
            {
                if (_CS0621 == null)
                    _CS0621 = new CompilerReferenceError(Resources.CSharpErrors_CS0621, 621);
                return _CS0621;
            }
        }
        private static ICompilerReferenceError _CS0621;

        /// <summary><para>C&#9839; compiler error &#35;622:</para><para>Can only use array initializer expressions to assign to array types. Try using a new expression instead.</para></summary>
        public static ICompilerReferenceError CS0622
        {
            get
            {
                if (_CS0622 == null)
                    _CS0622 = new CompilerReferenceError(Resources.CSharpErrors_CS0622, 622);
                return _CS0622;
            }
        }
        private static ICompilerReferenceError _CS0622;

        /// <summary><para>C&#9839; compiler error &#35;623:</para><para>Array initializers can only be used in a variable or field initializer. Try using a new expression instead.</para></summary>
        public static ICompilerReferenceError CS0623
        {
            get
            {
                if (_CS0623 == null)
                    _CS0623 = new CompilerReferenceError(Resources.CSharpErrors_CS0623, 623);
                return _CS0623;
            }
        }
        private static ICompilerReferenceError _CS0623;

        /// <summary><para>C&#9839; compiler error &#35;625:</para><para>'field': instance field types marked with StructLayout(LayoutKind.Explicit) must have a FieldOffset attribute</para></summary>
        public static ICompilerReferenceError CS0625
        {
            get
            {
                if (_CS0625 == null)
                    _CS0625 = new CompilerReferenceError(Resources.CSharpErrors_CS0625, 625);
                return _CS0625;
            }
        }
        private static ICompilerReferenceError _CS0625;

        /// <summary><para>C&#9839; compiler error &#35;629:</para><para>Conditional member 'member' cannot implement interface member 'base class member' in type 'Type Name'</para></summary>
        public static ICompilerReferenceError CS0629
        {
            get
            {
                if (_CS0629 == null)
                    _CS0629 = new CompilerReferenceError(Resources.CSharpErrors_CS0629, 629);
                return _CS0629;
            }
        }
        private static ICompilerReferenceError _CS0629;

        /// <summary><para>C&#9839; compiler error &#35;631:</para><para>ref and out are not valid in this context</para></summary>
        public static ICompilerReferenceError CS0631
        {
            get
            {
                if (_CS0631 == null)
                    _CS0631 = new CompilerReferenceError(Resources.CSharpErrors_CS0631, 631);
                return _CS0631;
            }
        }
        private static ICompilerReferenceError _CS0631;

        /// <summary><para>C&#9839; compiler error &#35;633:</para><para>The argument to the 'attribute' attribute must be a valid identifier</para></summary>
        public static ICompilerReferenceError CS0633
        {
            get
            {
                if (_CS0633 == null)
                    _CS0633 = new CompilerReferenceError(Resources.CSharpErrors_CS0633, 633);
                return _CS0633;
            }
        }
        private static ICompilerReferenceError _CS0633;

        /// <summary><para>C&#9839; compiler error &#35;635:</para><para>'attribute' : System.Interop.UnmanagedType.CustomMarshaller requires named arguments ComType and Marshal</para></summary>
        public static ICompilerReferenceError CS0635
        {
            get
            {
                if (_CS0635 == null)
                    _CS0635 = new CompilerReferenceError(Resources.CSharpErrors_CS0635, 635);
                return _CS0635;
            }
        }
        private static ICompilerReferenceError _CS0635;

        /// <summary><para>C&#9839; compiler error &#35;636:</para><para>The FieldOffset attribute can only be placed on members of types marked with the StructLayout(LayoutKind.Explicit)</para></summary>
        public static ICompilerReferenceError CS0636
        {
            get
            {
                if (_CS0636 == null)
                    _CS0636 = new CompilerReferenceError(Resources.CSharpErrors_CS0636, 636);
                return _CS0636;
            }
        }
        private static ICompilerReferenceError _CS0636;

        /// <summary><para>C&#9839; compiler error &#35;637:</para><para>The FieldOffset attribute is not allowed on static or const fields</para></summary>
        public static ICompilerReferenceError CS0637
        {
            get
            {
                if (_CS0637 == null)
                    _CS0637 = new CompilerReferenceError(Resources.CSharpErrors_CS0637, 637);
                return _CS0637;
            }
        }
        private static ICompilerReferenceError _CS0637;

        /// <summary><para>C&#9839; compiler error &#35;641:</para><para>'attribute' : attribute is only valid on classes derived from System.Attribute</para></summary>
        public static ICompilerReferenceError CS0641
        {
            get
            {
                if (_CS0641 == null)
                    _CS0641 = new CompilerReferenceError(Resources.CSharpErrors_CS0641, 641);
                return _CS0641;
            }
        }
        private static ICompilerReferenceError _CS0641;

        /// <summary><para>C&#9839; compiler error &#35;643:</para><para>'arg' duplicate named attribute argument</para></summary>
        public static ICompilerReferenceError CS0643
        {
            get
            {
                if (_CS0643 == null)
                    _CS0643 = new CompilerReferenceError(Resources.CSharpErrors_CS0643, 643);
                return _CS0643;
            }
        }
        private static ICompilerReferenceError _CS0643;

        /// <summary><para>C&#9839; compiler error &#35;644:</para><para>'class1' cannot derive from special class 'class2'</para></summary>
        public static ICompilerReferenceError CS0644
        {
            get
            {
                if (_CS0644 == null)
                    _CS0644 = new CompilerReferenceError(Resources.CSharpErrors_CS0644, 644);
                return _CS0644;
            }
        }
        private static ICompilerReferenceError _CS0644;

        /// <summary><para>C&#9839; compiler error &#35;645:</para><para>Identifier too long</para></summary>
        public static ICompilerReferenceError CS0645
        {
            get
            {
                if (_CS0645 == null)
                    _CS0645 = new CompilerReferenceError(Resources.CSharpErrors_CS0645, 645);
                return _CS0645;
            }
        }
        private static ICompilerReferenceError _CS0645;

        /// <summary><para>C&#9839; compiler error &#35;646:</para><para>Cannot specify the DefaultMember attribute on a type containing an indexer</para></summary>
        public static ICompilerReferenceError CS0646
        {
            get
            {
                if (_CS0646 == null)
                    _CS0646 = new CompilerReferenceError(Resources.CSharpErrors_CS0646, 646);
                return _CS0646;
            }
        }
        private static ICompilerReferenceError _CS0646;

        /// <summary><para>C&#9839; compiler error &#35;647:</para><para>Error emitting 'attribute' attribute -- 'reason'</para></summary>
        public static ICompilerReferenceError CS0647
        {
            get
            {
                if (_CS0647 == null)
                    _CS0647 = new CompilerReferenceError(Resources.CSharpErrors_CS0647, 647);
                return _CS0647;
            }
        }
        private static ICompilerReferenceError _CS0647;

        /// <summary><para>C&#9839; compiler error &#35;648:</para><para>'type' is a type not supported by the language</para></summary>
        public static ICompilerReferenceError CS0648
        {
            get
            {
                if (_CS0648 == null)
                    _CS0648 = new CompilerReferenceError(Resources.CSharpErrors_CS0648, 648);
                return _CS0648;
            }
        }
        private static ICompilerReferenceError _CS0648;

        /// <summary><para>C&#9839; compiler error &#35;650:</para><para>Bad array declarator: To declare a managed array the rank specifier precedes the variable's identifier. To declare a fixed size buffer field, use the fixed keyword before the field type.</para></summary>
        public static ICompilerReferenceError CS0650
        {
            get
            {
                if (_CS0650 == null)
                    _CS0650 = new CompilerReferenceError(Resources.CSharpErrors_CS0650, 650);
                return _CS0650;
            }
        }
        private static ICompilerReferenceError _CS0650;

        /// <summary><para>C&#9839; compiler error &#35;653:</para><para>Cannot apply attribute class 'class' because it is abstract</para></summary>
        public static ICompilerReferenceError CS0653
        {
            get
            {
                if (_CS0653 == null)
                    _CS0653 = new CompilerReferenceError(Resources.CSharpErrors_CS0653, 653);
                return _CS0653;
            }
        }
        private static ICompilerReferenceError _CS0653;

        /// <summary><para>C&#9839; compiler error &#35;655:</para><para>'parameter' is not a valid named attribute argument because it is not a valid attribute parameter type</para></summary>
        public static ICompilerReferenceError CS0655
        {
            get
            {
                if (_CS0655 == null)
                    _CS0655 = new CompilerReferenceError(Resources.CSharpErrors_CS0655, 655);
                return _CS0655;
            }
        }
        private static ICompilerReferenceError _CS0655;

        /// <summary><para>C&#9839; compiler error &#35;656:</para><para>Missing compiler required member 'object.member'</para></summary>
        public static ICompilerReferenceError CS0656
        {
            get
            {
                if (_CS0656 == null)
                    _CS0656 = new CompilerReferenceError(Resources.CSharpErrors_CS0656, 656);
                return _CS0656;
            }
        }
        private static ICompilerReferenceError _CS0656;

        /// <summary><para>C&#9839; compiler error &#35;662:</para><para>'method' cannot specify only Out attribute on a ref parameter. Use both In and Out attributes, or neither.</para></summary>
        public static ICompilerReferenceError CS0662
        {
            get
            {
                if (_CS0662 == null)
                    _CS0662 = new CompilerReferenceError(Resources.CSharpErrors_CS0662, 662);
                return _CS0662;
            }
        }
        private static ICompilerReferenceError _CS0662;

        /// <summary><para>C&#9839; compiler error &#35;663:</para><para>Cannot define overloaded methods that differ only on ref and out.</para></summary>
        public static ICompilerReferenceError CS0663
        {
            get
            {
                if (_CS0663 == null)
                    _CS0663 = new CompilerReferenceError(Resources.CSharpErrors_CS0663, 663);
                return _CS0663;
            }
        }
        private static ICompilerReferenceError _CS0663;

        /// <summary><para>C&#9839; compiler error &#35;664:</para><para>Literal of type double cannot be implicitly converted to type 'type'; use an 'suffix' suffix to create a literal of this type</para></summary>
        public static ICompilerReferenceError CS0664
        {
            get
            {
                if (_CS0664 == null)
                    _CS0664 = new CompilerReferenceError(Resources.CSharpErrors_CS0664, 664);
                return _CS0664;
            }
        }
        private static ICompilerReferenceError _CS0664;

        /// <summary><para>C&#9839; compiler error &#35;666:</para><para>'member' : new protected member declared in struct</para></summary>
        public static ICompilerReferenceError CS0666
        {
            get
            {
                if (_CS0666 == null)
                    _CS0666 = new CompilerReferenceError(Resources.CSharpErrors_CS0666, 666);
                return _CS0666;
            }
        }
        private static ICompilerReferenceError _CS0666;

        /// <summary><para>C&#9839; compiler error &#35;667:</para><para>The feature 'invalid feature' is deprecated. Please use 'valid feature' instead'.</para></summary>
        public static ICompilerReferenceError CS0667
        {
            get
            {
                if (_CS0667 == null)
                    _CS0667 = new CompilerReferenceError(Resources.CSharpErrors_CS0667, 667);
                return _CS0667;
            }
        }
        private static ICompilerReferenceError _CS0667;

        /// <summary><para>C&#9839; compiler error &#35;668:</para><para>Two indexers have different names; the IndexerName attribute must be used with the same name on every indexer within a type</para></summary>
        public static ICompilerReferenceError CS0668
        {
            get
            {
                if (_CS0668 == null)
                    _CS0668 = new CompilerReferenceError(Resources.CSharpErrors_CS0668, 668);
                return _CS0668;
            }
        }
        private static ICompilerReferenceError _CS0668;

        /// <summary><para>C&#9839; compiler error &#35;669:</para><para>A class with the ComImport attribute cannot have a user-defined constructor</para></summary>
        public static ICompilerReferenceError CS0669
        {
            get
            {
                if (_CS0669 == null)
                    _CS0669 = new CompilerReferenceError(Resources.CSharpErrors_CS0669, 669);
                return _CS0669;
            }
        }
        private static ICompilerReferenceError _CS0669;

        /// <summary><para>C&#9839; compiler error &#35;670:</para><para>Field cannot have void type</para></summary>
        public static ICompilerReferenceError CS0670
        {
            get
            {
                if (_CS0670 == null)
                    _CS0670 = new CompilerReferenceError(Resources.CSharpErrors_CS0670, 670);
                return _CS0670;
            }
        }
        private static ICompilerReferenceError _CS0670;

        /// <summary><para>C&#9839; compiler error &#35;673:</para><para>System.Void cannot be used from C# -- use typeof(void) to get the void type object.</para></summary>
        public static ICompilerReferenceError CS0673
        {
            get
            {
                if (_CS0673 == null)
                    _CS0673 = new CompilerReferenceError(Resources.CSharpErrors_CS0673, 673);
                return _CS0673;
            }
        }
        private static ICompilerReferenceError _CS0673;

        /// <summary><para>C&#9839; compiler error &#35;674:</para><para>Do not use 'System.ParamArrayAttribute'. Use the 'params' keyword instead.</para></summary>
        public static ICompilerReferenceError CS0674
        {
            get
            {
                if (_CS0674 == null)
                    _CS0674 = new CompilerReferenceError(Resources.CSharpErrors_CS0674, 674);
                return _CS0674;
            }
        }
        private static ICompilerReferenceError _CS0674;

        /// <summary><para>C&#9839; compiler error &#35;677:</para><para>'variable': a volatile field cannot be of the type 'type'</para></summary>
        public static ICompilerReferenceError CS0677
        {
            get
            {
                if (_CS0677 == null)
                    _CS0677 = new CompilerReferenceError(Resources.CSharpErrors_CS0677, 677);
                return _CS0677;
            }
        }
        private static ICompilerReferenceError _CS0677;

        /// <summary><para>C&#9839; compiler error &#35;678:</para><para>'variable': a field can not be both volatile and readonly</para></summary>
        public static ICompilerReferenceError CS0678
        {
            get
            {
                if (_CS0678 == null)
                    _CS0678 = new CompilerReferenceError(Resources.CSharpErrors_CS0678, 678);
                return _CS0678;
            }
        }
        private static ICompilerReferenceError _CS0678;

        /// <summary><para>C&#9839; compiler error &#35;681:</para><para>The modifier 'abstract' is not valid on fields. Try using a property instead</para></summary>
        public static ICompilerReferenceError CS0681
        {
            get
            {
                if (_CS0681 == null)
                    _CS0681 = new CompilerReferenceError(Resources.CSharpErrors_CS0681, 681);
                return _CS0681;
            }
        }
        private static ICompilerReferenceError _CS0681;

        /// <summary><para>C&#9839; compiler error &#35;682:</para><para>'type1' cannot implement 'type2' because it is not supported by the language</para></summary>
        public static ICompilerReferenceError CS0682
        {
            get
            {
                if (_CS0682 == null)
                    _CS0682 = new CompilerReferenceError(Resources.CSharpErrors_CS0682, 682);
                return _CS0682;
            }
        }
        private static ICompilerReferenceError _CS0682;

        /// <summary><para>C&#9839; compiler error &#35;683:</para><para>'explicitmethod' explicit method implementation cannot implement 'method' because it is an accessor</para></summary>
        public static ICompilerReferenceError CS0683
        {
            get
            {
                if (_CS0683 == null)
                    _CS0683 = new CompilerReferenceError(Resources.CSharpErrors_CS0683, 683);
                return _CS0683;
            }
        }
        private static ICompilerReferenceError _CS0683;

        /// <summary><para>C&#9839; compiler error &#35;685:</para><para>Conditional member 'member' cannot have an out parameter</para></summary>
        public static ICompilerReferenceError CS0685
        {
            get
            {
                if (_CS0685 == null)
                    _CS0685 = new CompilerReferenceError(Resources.CSharpErrors_CS0685, 685);
                return _CS0685;
            }
        }
        private static ICompilerReferenceError _CS0685;

        /// <summary><para>C&#9839; compiler error &#35;686:</para><para>Accessor 'accessor' cannot implement interface member 'member' for type 'type'. Use an explicit interface implementation.</para></summary>
        public static ICompilerReferenceError CS0686
        {
            get
            {
                if (_CS0686 == null)
                    _CS0686 = new CompilerReferenceError(Resources.CSharpErrors_CS0686, 686);
                return _CS0686;
            }
        }
        private static ICompilerReferenceError _CS0686;

        /// <summary><para>C&#9839; compiler error &#35;687:</para><para>The namespace alias qualifier '::' always resolves to a type or namespace so is illegal here. Consider using '.' instead.</para></summary>
        public static ICompilerReferenceError CS0687
        {
            get
            {
                if (_CS0687 == null)
                    _CS0687 = new CompilerReferenceError(Resources.CSharpErrors_CS0687, 687);
                return _CS0687;
            }
        }
        private static ICompilerReferenceError _CS0687;

        /// <summary><para>C&#9839; compiler error &#35;689:</para><para>Cannot derive from 'identifier' because it is a type parameter</para></summary>
        public static ICompilerReferenceError CS0689
        {
            get
            {
                if (_CS0689 == null)
                    _CS0689 = new CompilerReferenceError(Resources.CSharpErrors_CS0689, 689);
                return _CS0689;
            }
        }
        private static ICompilerReferenceError _CS0689;

        /// <summary><para>C&#9839; compiler error &#35;690:</para><para>Input file 'file' contains invalid metadata.</para></summary>
        public static ICompilerReferenceError CS0690
        {
            get
            {
                if (_CS0690 == null)
                    _CS0690 = new CompilerReferenceError(Resources.CSharpErrors_CS0690, 690);
                return _CS0690;
            }
        }
        private static ICompilerReferenceError _CS0690;

        /// <summary><para>C&#9839; compiler error &#35;692:</para><para>Duplicate type parameter 'identifier'</para></summary>
        public static ICompilerReferenceError CS0692
        {
            get
            {
                if (_CS0692 == null)
                    _CS0692 = new CompilerReferenceError(Resources.CSharpErrors_CS0692, 692);
                return _CS0692;
            }
        }
        private static ICompilerReferenceError _CS0692;

        /// <summary><para>C&#9839; compiler error &#35;694:</para><para>Type parameter 'identifier' has the same name as the containing type, or method</para></summary>
        public static ICompilerReferenceError CS0694
        {
            get
            {
                if (_CS0694 == null)
                    _CS0694 = new CompilerReferenceError(Resources.CSharpErrors_CS0694, 694);
                return _CS0694;
            }
        }
        private static ICompilerReferenceError _CS0694;

        /// <summary><para>C&#9839; compiler error &#35;695:</para><para>'generic type' cannot implement both 'generic interface' and 'generic interface' because they may unify for some type parameter substitutions</para></summary>
        public static ICompilerReferenceError CS0695
        {
            get
            {
                if (_CS0695 == null)
                    _CS0695 = new CompilerReferenceError(Resources.CSharpErrors_CS0695, 695);
                return _CS0695;
            }
        }
        private static ICompilerReferenceError _CS0695;

        /// <summary><para>C&#9839; compiler error &#35;698:</para><para>A generic type cannot derive from 'class' because it is an attribute class</para></summary>
        public static ICompilerReferenceError CS0698
        {
            get
            {
                if (_CS0698 == null)
                    _CS0698 = new CompilerReferenceError(Resources.CSharpErrors_CS0698, 698);
                return _CS0698;
            }
        }
        private static ICompilerReferenceError _CS0698;

        /// <summary><para>C&#9839; compiler error &#35;699:</para><para>'generic' does not define type parameter 'identifier'</para></summary>
        public static ICompilerReferenceError CS0699
        {
            get
            {
                if (_CS0699 == null)
                    _CS0699 = new CompilerReferenceError(Resources.CSharpErrors_CS0699, 699);
                return _CS0699;
            }
        }
        private static ICompilerReferenceError _CS0699;

        /// <summary><para>C&#9839; compiler error &#35;701:</para><para>'identifier' is not a valid constraint. A type used as a constraint must be an interface, a non-sealed class or a type parameter.</para></summary>
        public static ICompilerReferenceError CS0701
        {
            get
            {
                if (_CS0701 == null)
                    _CS0701 = new CompilerReferenceError(Resources.CSharpErrors_CS0701, 701);
                return _CS0701;
            }
        }
        private static ICompilerReferenceError _CS0701;

        /// <summary><para>C&#9839; compiler error &#35;702:</para><para>Constraint cannot be special class 'identifier'</para></summary>
        public static ICompilerReferenceError CS0702
        {
            get
            {
                if (_CS0702 == null)
                    _CS0702 = new CompilerReferenceError(Resources.CSharpErrors_CS0702, 702);
                return _CS0702;
            }
        }
        private static ICompilerReferenceError _CS0702;

        /// <summary><para>C&#9839; compiler error &#35;703:</para><para>Inconsistent accessibility: constraint type 'identifier' is less accessible than 'identifier'</para></summary>
        public static ICompilerReferenceError CS0703
        {
            get
            {
                if (_CS0703 == null)
                    _CS0703 = new CompilerReferenceError(Resources.CSharpErrors_CS0703, 703);
                return _CS0703;
            }
        }
        private static ICompilerReferenceError _CS0703;

        /// <summary><para>C&#9839; compiler error &#35;704:</para><para>Cannot do member lookup in 'type' because it is a type parameter</para></summary>
        public static ICompilerReferenceError CS0704
        {
            get
            {
                if (_CS0704 == null)
                    _CS0704 = new CompilerReferenceError(Resources.CSharpErrors_CS0704, 704);
                return _CS0704;
            }
        }
        private static ICompilerReferenceError _CS0704;

        /// <summary><para>C&#9839; compiler error &#35;706:</para><para>Invalid constraint type. A type used as a constraint must be an interface, a non-sealed class or a type parameter.</para></summary>
        public static ICompilerReferenceError CS0706
        {
            get
            {
                if (_CS0706 == null)
                    _CS0706 = new CompilerReferenceError(Resources.CSharpErrors_CS0706, 706);
                return _CS0706;
            }
        }
        private static ICompilerReferenceError _CS0706;

        /// <summary><para>C&#9839; compiler error &#35;708:</para><para>'field': cannot declare instance members in a static class</para></summary>
        public static ICompilerReferenceError CS0708
        {
            get
            {
                if (_CS0708 == null)
                    _CS0708 = new CompilerReferenceError(Resources.CSharpErrors_CS0708, 708);
                return _CS0708;
            }
        }
        private static ICompilerReferenceError _CS0708;

        /// <summary><para>C&#9839; compiler error &#35;709:</para><para>'derived class': cannot derive from static class 'base class'</para></summary>
        public static ICompilerReferenceError CS0709
        {
            get
            {
                if (_CS0709 == null)
                    _CS0709 = new CompilerReferenceError(Resources.CSharpErrors_CS0709, 709);
                return _CS0709;
            }
        }
        private static ICompilerReferenceError _CS0709;

        /// <summary><para>C&#9839; compiler error &#35;710:</para><para>Static classes cannot have instance constructors</para></summary>
        public static ICompilerReferenceError CS0710
        {
            get
            {
                if (_CS0710 == null)
                    _CS0710 = new CompilerReferenceError(Resources.CSharpErrors_CS0710, 710);
                return _CS0710;
            }
        }
        private static ICompilerReferenceError _CS0710;

        /// <summary><para>C&#9839; compiler error &#35;711:</para><para>Static classes cannot contain destructors</para></summary>
        public static ICompilerReferenceError CS0711
        {
            get
            {
                if (_CS0711 == null)
                    _CS0711 = new CompilerReferenceError(Resources.CSharpErrors_CS0711, 711);
                return _CS0711;
            }
        }
        private static ICompilerReferenceError _CS0711;

        /// <summary><para>C&#9839; compiler error &#35;712:</para><para>Cannot create an instance of the static class 'static class'</para></summary>
        public static ICompilerReferenceError CS0712
        {
            get
            {
                if (_CS0712 == null)
                    _CS0712 = new CompilerReferenceError(Resources.CSharpErrors_CS0712, 712);
                return _CS0712;
            }
        }
        private static ICompilerReferenceError _CS0712;

        /// <summary><para>C&#9839; compiler error &#35;713:</para><para>Static class 'static type' cannot derive from type 'type'. Static classes must derive from object.</para></summary>
        public static ICompilerReferenceError CS0713
        {
            get
            {
                if (_CS0713 == null)
                    _CS0713 = new CompilerReferenceError(Resources.CSharpErrors_CS0713, 713);
                return _CS0713;
            }
        }
        private static ICompilerReferenceError _CS0713;

        /// <summary><para>C&#9839; compiler error &#35;714:</para><para>'static type' : static classes cannot implement interfaces</para></summary>
        public static ICompilerReferenceError CS0714
        {
            get
            {
                if (_CS0714 == null)
                    _CS0714 = new CompilerReferenceError(Resources.CSharpErrors_CS0714, 714);
                return _CS0714;
            }
        }
        private static ICompilerReferenceError _CS0714;

        /// <summary><para>C&#9839; compiler error &#35;715:</para><para>'static class' : static classes cannot contain user defined operators</para></summary>
        public static ICompilerReferenceError CS0715
        {
            get
            {
                if (_CS0715 == null)
                    _CS0715 = new CompilerReferenceError(Resources.CSharpErrors_CS0715, 715);
                return _CS0715;
            }
        }
        private static ICompilerReferenceError _CS0715;

        /// <summary><para>C&#9839; compiler error &#35;716:</para><para>Cannot convert to static type 'type'</para></summary>
        public static ICompilerReferenceError CS0716
        {
            get
            {
                if (_CS0716 == null)
                    _CS0716 = new CompilerReferenceError(Resources.CSharpErrors_CS0716, 716);
                return _CS0716;
            }
        }
        private static ICompilerReferenceError _CS0716;

        /// <summary><para>C&#9839; compiler error &#35;717:</para><para>'static class': static classes cannot be used as constraints</para></summary>
        public static ICompilerReferenceError CS0717
        {
            get
            {
                if (_CS0717 == null)
                    _CS0717 = new CompilerReferenceError(Resources.CSharpErrors_CS0717, 717);
                return _CS0717;
            }
        }
        private static ICompilerReferenceError _CS0717;

        /// <summary><para>C&#9839; compiler error &#35;718:</para><para>'type': static types cannot be used as type arguments</para></summary>
        public static ICompilerReferenceError CS0718
        {
            get
            {
                if (_CS0718 == null)
                    _CS0718 = new CompilerReferenceError(Resources.CSharpErrors_CS0718, 718);
                return _CS0718;
            }
        }
        private static ICompilerReferenceError _CS0718;

        /// <summary><para>C&#9839; compiler error &#35;719:</para><para>'type': array elements cannot be of static type</para></summary>
        public static ICompilerReferenceError CS0719
        {
            get
            {
                if (_CS0719 == null)
                    _CS0719 = new CompilerReferenceError(Resources.CSharpErrors_CS0719, 719);
                return _CS0719;
            }
        }
        private static ICompilerReferenceError _CS0719;

        /// <summary><para>C&#9839; compiler error &#35;720:</para><para>'static class': cannot declare indexers in a static class</para></summary>
        public static ICompilerReferenceError CS0720
        {
            get
            {
                if (_CS0720 == null)
                    _CS0720 = new CompilerReferenceError(Resources.CSharpErrors_CS0720, 720);
                return _CS0720;
            }
        }
        private static ICompilerReferenceError _CS0720;

        /// <summary><para>C&#9839; compiler error &#35;721:</para><para>'type': static types cannot be used as parameters</para></summary>
        public static ICompilerReferenceError CS0721
        {
            get
            {
                if (_CS0721 == null)
                    _CS0721 = new CompilerReferenceError(Resources.CSharpErrors_CS0721, 721);
                return _CS0721;
            }
        }
        private static ICompilerReferenceError _CS0721;

        /// <summary><para>C&#9839; compiler error &#35;722:</para><para>'type': static types cannot be used as return types</para></summary>
        public static ICompilerReferenceError CS0722
        {
            get
            {
                if (_CS0722 == null)
                    _CS0722 = new CompilerReferenceError(Resources.CSharpErrors_CS0722, 722);
                return _CS0722;
            }
        }
        private static ICompilerReferenceError _CS0722;

        /// <summary><para>C&#9839; compiler error &#35;723:</para><para>Cannot declare variable of static type 'type'</para></summary>
        public static ICompilerReferenceError CS0723
        {
            get
            {
                if (_CS0723 == null)
                    _CS0723 = new CompilerReferenceError(Resources.CSharpErrors_CS0723, 723);
                return _CS0723;
            }
        }
        private static ICompilerReferenceError _CS0723;

        /// <summary><para>C&#9839; compiler error &#35;724:</para><para>does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute</para></summary>
        public static ICompilerReferenceError CS0724
        {
            get
            {
                if (_CS0724 == null)
                    _CS0724 = new CompilerReferenceError(Resources.CSharpErrors_CS0724, 724);
                return _CS0724;
            }
        }
        private static ICompilerReferenceError _CS0724;

        /// <summary><para>C&#9839; compiler error &#35;726:</para><para>'format specifier' is not a valid format specifier</para></summary>
        public static ICompilerReferenceError CS0726
        {
            get
            {
                if (_CS0726 == null)
                    _CS0726 = new CompilerReferenceError(Resources.CSharpErrors_CS0726, 726);
                return _CS0726;
            }
        }
        private static ICompilerReferenceError _CS0726;

        /// <summary><para>C&#9839; compiler error &#35;727:</para><para>Invalid format specifier</para></summary>
        public static ICompilerReferenceError CS0727
        {
            get
            {
                if (_CS0727 == null)
                    _CS0727 = new CompilerReferenceError(Resources.CSharpErrors_CS0727, 727);
                return _CS0727;
            }
        }
        private static ICompilerReferenceError _CS0727;

        /// <summary><para>C&#9839; compiler error &#35;729:</para><para>Type 'type' is defined in this assembly, but a type forwarder is specified for it</para></summary>
        public static ICompilerReferenceError CS0729
        {
            get
            {
                if (_CS0729 == null)
                    _CS0729 = new CompilerReferenceError(Resources.CSharpErrors_CS0729, 729);
                return _CS0729;
            }
        }
        private static ICompilerReferenceError _CS0729;

        /// <summary><para>C&#9839; compiler error &#35;730:</para><para>Cannot forward type 'type' because it is a nested type of 'type'</para></summary>
        public static ICompilerReferenceError CS0730
        {
            get
            {
                if (_CS0730 == null)
                    _CS0730 = new CompilerReferenceError(Resources.CSharpErrors_CS0730, 730);
                return _CS0730;
            }
        }
        private static ICompilerReferenceError _CS0730;

        /// <summary><para>C&#9839; compiler error &#35;731:</para><para>The type forwarder for type 'type' in assembly 'assembly' causes a cycle</para></summary>
        public static ICompilerReferenceError CS0731
        {
            get
            {
                if (_CS0731 == null)
                    _CS0731 = new CompilerReferenceError(Resources.CSharpErrors_CS0731, 731);
                return _CS0731;
            }
        }
        private static ICompilerReferenceError _CS0731;

        /// <summary><para>C&#9839; compiler error &#35;733:</para><para>Cannot forward generic type, 'GenericType<>'</para></summary>
        public static ICompilerReferenceError CS0733
        {
            get
            {
                if (_CS0733 == null)
                    _CS0733 = new CompilerReferenceError(Resources.CSharpErrors_CS0733, 733);
                return _CS0733;
            }
        }
        private static ICompilerReferenceError _CS0733;

        /// <summary><para>C&#9839; compiler error &#35;734:</para><para>The /moduleassemblyname option may only be specified when building a target type of 'module'</para></summary>
        public static ICompilerReferenceError CS0734
        {
            get
            {
                if (_CS0734 == null)
                    _CS0734 = new CompilerReferenceError(Resources.CSharpErrors_CS0734, 734);
                return _CS0734;
            }
        }
        private static ICompilerReferenceError _CS0734;

        /// <summary><para>C&#9839; compiler error &#35;735:</para><para>Invalid type specified as an argument for TypeForwardedTo attribute</para></summary>
        public static ICompilerReferenceError CS0735
        {
            get
            {
                if (_CS0735 == null)
                    _CS0735 = new CompilerReferenceError(Resources.CSharpErrors_CS0735, 735);
                return _CS0735;
            }
        }
        private static ICompilerReferenceError _CS0735;

        /// <summary><para>C&#9839; compiler error &#35;736:</para><para>'type name' does not implement interface member 'member name'. 'method name' cannot implement an interface member because it is static.</para></summary>
        public static ICompilerReferenceError CS0736
        {
            get
            {
                if (_CS0736 == null)
                    _CS0736 = new CompilerReferenceError(Resources.CSharpErrors_CS0736, 736);
                return _CS0736;
            }
        }
        private static ICompilerReferenceError _CS0736;

        /// <summary><para>C&#9839; compiler error &#35;737:</para><para>'type name' does not implement interface member 'member name'. 'method name' cannot implement an interface member because it is not public.</para></summary>
        public static ICompilerReferenceError CS0737
        {
            get
            {
                if (_CS0737 == null)
                    _CS0737 = new CompilerReferenceError(Resources.CSharpErrors_CS0737, 737);
                return _CS0737;
            }
        }
        private static ICompilerReferenceError _CS0737;

        /// <summary><para>C&#9839; compiler error &#35;738:</para><para>'type name' does not implement interface member 'member name'. 'method name' cannot implement 'interface member' because it does not have the matching return type of ' type name'.</para></summary>
        public static ICompilerReferenceError CS0738
        {
            get
            {
                if (_CS0738 == null)
                    _CS0738 = new CompilerReferenceError(Resources.CSharpErrors_CS0738, 738);
                return _CS0738;
            }
        }
        private static ICompilerReferenceError _CS0738;

        /// <summary><para>C&#9839; compiler error &#35;739:</para><para>'type name' duplicate TypeForwardedToAttribute.</para></summary>
        public static ICompilerReferenceError CS0739
        {
            get
            {
                if (_CS0739 == null)
                    _CS0739 = new CompilerReferenceError(Resources.CSharpErrors_CS0739, 739);
                return _CS0739;
            }
        }
        private static ICompilerReferenceError _CS0739;

        /// <summary><para>C&#9839; compiler error &#35;742:</para><para>A query body must end with a select clause or a group clause</para></summary>
        public static ICompilerReferenceError CS0742
        {
            get
            {
                if (_CS0742 == null)
                    _CS0742 = new CompilerReferenceError(Resources.CSharpErrors_CS0742, 742);
                return _CS0742;
            }
        }
        private static ICompilerReferenceError _CS0742;

        /// <summary><para>C&#9839; compiler error &#35;743:</para><para>Expected contextual keyword 'on'</para></summary>
        public static ICompilerReferenceError CS0743
        {
            get
            {
                if (_CS0743 == null)
                    _CS0743 = new CompilerReferenceError(Resources.CSharpErrors_CS0743, 743);
                return _CS0743;
            }
        }
        private static ICompilerReferenceError _CS0743;

        /// <summary><para>C&#9839; compiler error &#35;744:</para><para>Expected contextual keyword 'equals'</para></summary>
        public static ICompilerReferenceError CS0744
        {
            get
            {
                if (_CS0744 == null)
                    _CS0744 = new CompilerReferenceError(Resources.CSharpErrors_CS0744, 744);
                return _CS0744;
            }
        }
        private static ICompilerReferenceError _CS0744;

        /// <summary><para>C&#9839; compiler error &#35;745:</para><para>Expected contextual keyword 'by'</para></summary>
        public static ICompilerReferenceError CS0745
        {
            get
            {
                if (_CS0745 == null)
                    _CS0745 = new CompilerReferenceError(Resources.CSharpErrors_CS0745, 745);
                return _CS0745;
            }
        }
        private static ICompilerReferenceError _CS0745;

        /// <summary><para>C&#9839; compiler error &#35;746:</para><para>Invalid anonymous type member declarator. Anonymous type members must be declared with a member assignment, simple name or member access.</para></summary>
        public static ICompilerReferenceError CS0746
        {
            get
            {
                if (_CS0746 == null)
                    _CS0746 = new CompilerReferenceError(Resources.CSharpErrors_CS0746, 746);
                return _CS0746;
            }
        }
        private static ICompilerReferenceError _CS0746;

        /// <summary><para>C&#9839; compiler error &#35;747:</para><para>Invalid initializer member declarator.</para></summary>
        public static ICompilerReferenceError CS0747
        {
            get
            {
                if (_CS0747 == null)
                    _CS0747 = new CompilerReferenceError(Resources.CSharpErrors_CS0747, 747);
                return _CS0747;
            }
        }
        private static ICompilerReferenceError _CS0747;

        /// <summary><para>C&#9839; compiler error &#35;748:</para><para>Inconsistent lambda parameter usage; all parameter types must either be explicit or implicit.</para></summary>
        public static ICompilerReferenceError CS0748
        {
            get
            {
                if (_CS0748 == null)
                    _CS0748 = new CompilerReferenceError(Resources.CSharpErrors_CS0748, 748);
                return _CS0748;
            }
        }
        private static ICompilerReferenceError _CS0748;

        /// <summary><para>C&#9839; compiler error &#35;750:</para><para>A partial method cannot have access modifiers or the virtual, abstract, override, new, sealed, or extern modifiers.</para></summary>
        public static ICompilerReferenceError CS0750
        {
            get
            {
                if (_CS0750 == null)
                    _CS0750 = new CompilerReferenceError(Resources.CSharpErrors_CS0750, 750);
                return _CS0750;
            }
        }
        private static ICompilerReferenceError _CS0750;

        /// <summary><para>C&#9839; compiler error &#35;751:</para><para>A partial method must be declared in a partial class or partial struct</para></summary>
        public static ICompilerReferenceError CS0751
        {
            get
            {
                if (_CS0751 == null)
                    _CS0751 = new CompilerReferenceError(Resources.CSharpErrors_CS0751, 751);
                return _CS0751;
            }
        }
        private static ICompilerReferenceError _CS0751;

        /// <summary><para>C&#9839; compiler error &#35;752:</para><para>A partial method cannot have out parameters</para></summary>
        public static ICompilerReferenceError CS0752
        {
            get
            {
                if (_CS0752 == null)
                    _CS0752 = new CompilerReferenceError(Resources.CSharpErrors_CS0752, 752);
                return _CS0752;
            }
        }
        private static ICompilerReferenceError _CS0752;

        /// <summary><para>C&#9839; compiler error &#35;753:</para><para>Only methods, classes, structs, or interfaces may be partial.</para></summary>
        public static ICompilerReferenceError CS0753
        {
            get
            {
                if (_CS0753 == null)
                    _CS0753 = new CompilerReferenceError(Resources.CSharpErrors_CS0753, 753);
                return _CS0753;
            }
        }
        private static ICompilerReferenceError _CS0753;

        /// <summary><para>C&#9839; compiler error &#35;754:</para><para>A partial method may not explicitly implement an interface method.</para></summary>
        public static ICompilerReferenceError CS0754
        {
            get
            {
                if (_CS0754 == null)
                    _CS0754 = new CompilerReferenceError(Resources.CSharpErrors_CS0754, 754);
                return _CS0754;
            }
        }
        private static ICompilerReferenceError _CS0754;

        /// <summary><para>C&#9839; compiler error &#35;755:</para><para>Both partial method declarations must be extension methods or neither may be an extension method.</para></summary>
        public static ICompilerReferenceError CS0755
        {
            get
            {
                if (_CS0755 == null)
                    _CS0755 = new CompilerReferenceError(Resources.CSharpErrors_CS0755, 755);
                return _CS0755;
            }
        }
        private static ICompilerReferenceError _CS0755;

        /// <summary><para>C&#9839; compiler error &#35;756:</para><para>A partial method may not have multiple defining declarations.</para></summary>
        public static ICompilerReferenceError CS0756
        {
            get
            {
                if (_CS0756 == null)
                    _CS0756 = new CompilerReferenceError(Resources.CSharpErrors_CS0756, 756);
                return _CS0756;
            }
        }
        private static ICompilerReferenceError _CS0756;

        /// <summary><para>C&#9839; compiler error &#35;757:</para><para>A partial method may not have multiple implementing declarations.</para></summary>
        public static ICompilerReferenceError CS0757
        {
            get
            {
                if (_CS0757 == null)
                    _CS0757 = new CompilerReferenceError(Resources.CSharpErrors_CS0757, 757);
                return _CS0757;
            }
        }
        private static ICompilerReferenceError _CS0757;

        /// <summary><para>C&#9839; compiler error &#35;758:</para><para>Both partial method declarations must use a params parameter or neither may use a params parameter</para></summary>
        public static ICompilerReferenceError CS0758
        {
            get
            {
                if (_CS0758 == null)
                    _CS0758 = new CompilerReferenceError(Resources.CSharpErrors_CS0758, 758);
                return _CS0758;
            }
        }
        private static ICompilerReferenceError _CS0758;

        /// <summary><para>C&#9839; compiler error &#35;759:</para><para>No defining declaration found for implementing declaration of partial method 'method'.</para></summary>
        public static ICompilerReferenceError CS0759
        {
            get
            {
                if (_CS0759 == null)
                    _CS0759 = new CompilerReferenceError(Resources.CSharpErrors_CS0759, 759);
                return _CS0759;
            }
        }
        private static ICompilerReferenceError _CS0759;

        /// <summary><para>C&#9839; compiler error &#35;761:</para><para>Partial method declarations of 'method<T>' have inconsistent type parameter constraints.</para></summary>
        public static ICompilerReferenceError CS0761
        {
            get
            {
                if (_CS0761 == null)
                    _CS0761 = new CompilerReferenceError(Resources.CSharpErrors_CS0761, 761);
                return _CS0761;
            }
        }
        private static ICompilerReferenceError _CS0761;

        /// <summary><para>C&#9839; compiler error &#35;762:</para><para>Cannot create delegate from method 'method' because it is a partial method without an implementing declaration</para></summary>
        public static ICompilerReferenceError CS0762
        {
            get
            {
                if (_CS0762 == null)
                    _CS0762 = new CompilerReferenceError(Resources.CSharpErrors_CS0762, 762);
                return _CS0762;
            }
        }
        private static ICompilerReferenceError _CS0762;

        /// <summary><para>C&#9839; compiler error &#35;763:</para><para>Both partial method declarations must be static or neither may be static.</para></summary>
        public static ICompilerReferenceError CS0763
        {
            get
            {
                if (_CS0763 == null)
                    _CS0763 = new CompilerReferenceError(Resources.CSharpErrors_CS0763, 763);
                return _CS0763;
            }
        }
        private static ICompilerReferenceError _CS0763;

        /// <summary><para>C&#9839; compiler error &#35;764:</para><para>Both partial method declarations must be unsafe or neither may be unsafe</para></summary>
        public static ICompilerReferenceError CS0764
        {
            get
            {
                if (_CS0764 == null)
                    _CS0764 = new CompilerReferenceError(Resources.CSharpErrors_CS0764, 764);
                return _CS0764;
            }
        }
        private static ICompilerReferenceError _CS0764;

        /// <summary><para>C&#9839; compiler error &#35;765:</para><para>Partial methods with only a defining declaration or removed conditional methods cannot be used in expression trees</para></summary>
        public static ICompilerReferenceError CS0765
        {
            get
            {
                if (_CS0765 == null)
                    _CS0765 = new CompilerReferenceError(Resources.CSharpErrors_CS0765, 765);
                return _CS0765;
            }
        }
        private static ICompilerReferenceError _CS0765;

        /// <summary><para>C&#9839; compiler error &#35;766:</para><para>Partial methods must have a void return type.</para></summary>
        public static ICompilerReferenceError CS0766
        {
            get
            {
                if (_CS0766 == null)
                    _CS0766 = new CompilerReferenceError(Resources.CSharpErrors_CS0766, 766);
                return _CS0766;
            }
        }
        private static ICompilerReferenceError _CS0766;

        /// <summary><para>C&#9839; compiler error &#35;811:</para><para>The fully qualified name for 'name' is too long for debug information. Compile without '/debug' option.</para></summary>
        public static ICompilerReferenceError CS0811
        {
            get
            {
                if (_CS0811 == null)
                    _CS0811 = new CompilerReferenceError(Resources.CSharpErrors_CS0811, 811);
                return _CS0811;
            }
        }
        private static ICompilerReferenceError _CS0811;

        /// <summary><para>C&#9839; compiler error &#35;815:</para><para>Cannot assign 'expression' to an implicitly typed local</para></summary>
        public static ICompilerReferenceError CS0815
        {
            get
            {
                if (_CS0815 == null)
                    _CS0815 = new CompilerReferenceError(Resources.CSharpErrors_CS0815, 815);
                return _CS0815;
            }
        }
        private static ICompilerReferenceError _CS0815;

        /// <summary><para>C&#9839; compiler error &#35;818:</para><para>Implicitly typed locals must be initialized</para></summary>
        public static ICompilerReferenceError CS0818
        {
            get
            {
                if (_CS0818 == null)
                    _CS0818 = new CompilerReferenceError(Resources.CSharpErrors_CS0818, 818);
                return _CS0818;
            }
        }
        private static ICompilerReferenceError _CS0818;

        /// <summary><para>C&#9839; compiler error &#35;819:</para><para>Implicitly typed locals cannot have multiple declarators.</para></summary>
        public static ICompilerReferenceError CS0819
        {
            get
            {
                if (_CS0819 == null)
                    _CS0819 = new CompilerReferenceError(Resources.CSharpErrors_CS0819, 819);
                return _CS0819;
            }
        }
        private static ICompilerReferenceError _CS0819;

        /// <summary><para>C&#9839; compiler error &#35;820:</para><para>Cannot assign array initializer to an implicitly typed local</para></summary>
        public static ICompilerReferenceError CS0820
        {
            get
            {
                if (_CS0820 == null)
                    _CS0820 = new CompilerReferenceError(Resources.CSharpErrors_CS0820, 820);
                return _CS0820;
            }
        }
        private static ICompilerReferenceError _CS0820;

        /// <summary><para>C&#9839; compiler error &#35;821:</para><para>Implicitly typed locals cannot be fixed</para></summary>
        public static ICompilerReferenceError CS0821
        {
            get
            {
                if (_CS0821 == null)
                    _CS0821 = new CompilerReferenceError(Resources.CSharpErrors_CS0821, 821);
                return _CS0821;
            }
        }
        private static ICompilerReferenceError _CS0821;

        /// <summary><para>C&#9839; compiler error &#35;822:</para><para>Implicitly typed locals cannot be const</para></summary>
        public static ICompilerReferenceError CS0822
        {
            get
            {
                if (_CS0822 == null)
                    _CS0822 = new CompilerReferenceError(Resources.CSharpErrors_CS0822, 822);
                return _CS0822;
            }
        }
        private static ICompilerReferenceError _CS0822;

        /// <summary><para>C&#9839; compiler error &#35;825:</para><para>The contextual keyword 'var' may only appear within a local variable declaration.</para></summary>
        public static ICompilerReferenceError CS0825
        {
            get
            {
                if (_CS0825 == null)
                    _CS0825 = new CompilerReferenceError(Resources.CSharpErrors_CS0825, 825);
                return _CS0825;
            }
        }
        private static ICompilerReferenceError _CS0825;

        /// <summary><para>C&#9839; compiler error &#35;826:</para><para>No best type found for implicitly typed array.</para></summary>
        public static ICompilerReferenceError CS0826
        {
            get
            {
                if (_CS0826 == null)
                    _CS0826 = new CompilerReferenceError(Resources.CSharpErrors_CS0826, 826);
                return _CS0826;
            }
        }
        private static ICompilerReferenceError _CS0826;

        /// <summary><para>C&#9839; compiler error &#35;828:</para><para>Cannot assign 'expression' to anonymous type property.</para></summary>
        public static ICompilerReferenceError CS0828
        {
            get
            {
                if (_CS0828 == null)
                    _CS0828 = new CompilerReferenceError(Resources.CSharpErrors_CS0828, 828);
                return _CS0828;
            }
        }
        private static ICompilerReferenceError _CS0828;

        /// <summary><para>C&#9839; compiler error &#35;831:</para><para>An expression tree may not contain a base access.</para></summary>
        public static ICompilerReferenceError CS0831
        {
            get
            {
                if (_CS0831 == null)
                    _CS0831 = new CompilerReferenceError(Resources.CSharpErrors_CS0831, 831);
                return _CS0831;
            }
        }
        private static ICompilerReferenceError _CS0831;

        /// <summary><para>C&#9839; compiler error &#35;832:</para><para>An expression tree may not contain an assignment operator.</para></summary>
        public static ICompilerReferenceError CS0832
        {
            get
            {
                if (_CS0832 == null)
                    _CS0832 = new CompilerReferenceError(Resources.CSharpErrors_CS0832, 832);
                return _CS0832;
            }
        }
        private static ICompilerReferenceError _CS0832;

        /// <summary><para>C&#9839; compiler error &#35;833:</para><para>An anonymous type cannot have multiple properties with the same name.</para></summary>
        public static ICompilerReferenceError CS0833
        {
            get
            {
                if (_CS0833 == null)
                    _CS0833 = new CompilerReferenceError(Resources.CSharpErrors_CS0833, 833);
                return _CS0833;
            }
        }
        private static ICompilerReferenceError _CS0833;

        /// <summary><para>C&#9839; compiler error &#35;834:</para><para>A lambda expression must have an expression body to be converted to an expression tree.</para></summary>
        public static ICompilerReferenceError CS0834
        {
            get
            {
                if (_CS0834 == null)
                    _CS0834 = new CompilerReferenceError(Resources.CSharpErrors_CS0834, 834);
                return _CS0834;
            }
        }
        private static ICompilerReferenceError _CS0834;

        /// <summary><para>C&#9839; compiler error &#35;835:</para><para>Cannot convert lambda to an expression tree whose type argument 'type' is not a delegate type.</para></summary>
        public static ICompilerReferenceError CS0835
        {
            get
            {
                if (_CS0835 == null)
                    _CS0835 = new CompilerReferenceError(Resources.CSharpErrors_CS0835, 835);
                return _CS0835;
            }
        }
        private static ICompilerReferenceError _CS0835;

        /// <summary><para>C&#9839; compiler error &#35;836:</para><para>Cannot use anonymous type in a constant expression.</para></summary>
        public static ICompilerReferenceError CS0836
        {
            get
            {
                if (_CS0836 == null)
                    _CS0836 = new CompilerReferenceError(Resources.CSharpErrors_CS0836, 836);
                return _CS0836;
            }
        }
        private static ICompilerReferenceError _CS0836;

        /// <summary><para>C&#9839; compiler error &#35;837:</para><para>The first operand of an ""is"" or ""as"" operator may not be a lambda expression or anonymous method.</para></summary>
        public static ICompilerReferenceError CS0837
        {
            get
            {
                if (_CS0837 == null)
                    _CS0837 = new CompilerReferenceError(Resources.CSharpErrors_CS0837, 837);
                return _CS0837;
            }
        }
        private static ICompilerReferenceError _CS0837;

        /// <summary><para>C&#9839; compiler error &#35;838:</para><para>An expression tree may not contain a multidimensional array initializer.</para></summary>
        public static ICompilerReferenceError CS0838
        {
            get
            {
                if (_CS0838 == null)
                    _CS0838 = new CompilerReferenceError(Resources.CSharpErrors_CS0838, 838);
                return _CS0838;
            }
        }
        private static ICompilerReferenceError _CS0838;

        /// <summary><para>C&#9839; compiler error &#35;839:</para><para>Argument missing.</para></summary>
        public static ICompilerReferenceError CS0839
        {
            get
            {
                if (_CS0839 == null)
                    _CS0839 = new CompilerReferenceError(Resources.CSharpErrors_CS0839, 839);
                return _CS0839;
            }
        }
        private static ICompilerReferenceError _CS0839;

        /// <summary><para>C&#9839; compiler error &#35;840:</para><para>'Property name' must declare a body because it is not marked abstract or extern. Automatically implemented properties must define both get and set accessors.</para></summary>
        public static ICompilerReferenceError CS0840
        {
            get
            {
                if (_CS0840 == null)
                    _CS0840 = new CompilerReferenceError(Resources.CSharpErrors_CS0840, 840);
                return _CS0840;
            }
        }
        private static ICompilerReferenceError _CS0840;

        /// <summary><para>C&#9839; compiler error &#35;841:</para><para>Cannot use variable 'name' before it is declared.</para></summary>
        public static ICompilerReferenceError CS0841
        {
            get
            {
                if (_CS0841 == null)
                    _CS0841 = new CompilerReferenceError(Resources.CSharpErrors_CS0841, 841);
                return _CS0841;
            }
        }
        private static ICompilerReferenceError _CS0841;

        /// <summary><para>C&#9839; compiler error &#35;842:</para><para>Automatically implemented properties cannot be used inside a type marked with StructLayout(LayoutKind.Explicit).</para></summary>
        public static ICompilerReferenceError CS0842
        {
            get
            {
                if (_CS0842 == null)
                    _CS0842 = new CompilerReferenceError(Resources.CSharpErrors_CS0842, 842);
                return _CS0842;
            }
        }
        private static ICompilerReferenceError _CS0842;

        /// <summary><para>C&#9839; compiler error &#35;843:</para><para>Backing field for automatically implemented property 'name' must be fully assigned before control is returned to the caller. Consider calling the default constructor from a constructor initializer.</para></summary>
        public static ICompilerReferenceError CS0843
        {
            get
            {
                if (_CS0843 == null)
                    _CS0843 = new CompilerReferenceError(Resources.CSharpErrors_CS0843, 843);
                return _CS0843;
            }
        }
        private static ICompilerReferenceError _CS0843;

        /// <summary><para>C&#9839; compiler error &#35;844:</para><para>Cannot use local variable 'name' before it is declared. The declaration of the local variable hides the field 'name'.</para></summary>
        public static ICompilerReferenceError CS0844
        {
            get
            {
                if (_CS0844 == null)
                    _CS0844 = new CompilerReferenceError(Resources.CSharpErrors_CS0844, 844);
                return _CS0844;
            }
        }
        private static ICompilerReferenceError _CS0844;

        /// <summary><para>C&#9839; compiler error &#35;845:</para><para>An expression tree lambda may not contain a coalescing operator with a null literal left-hand side.</para></summary>
        public static ICompilerReferenceError CS0845
        {
            get
            {
                if (_CS0845 == null)
                    _CS0845 = new CompilerReferenceError(Resources.CSharpErrors_CS0845, 845);
                return _CS0845;
            }
        }
        private static ICompilerReferenceError _CS0845;

        /// <summary><para>C&#9839; compiler error &#35;1001:</para><para>Identifier expected</para></summary>
        public static ICompilerReferenceError CS1001
        {
            get
            {
                if (_CS1001 == null)
                    _CS1001 = new CompilerReferenceError(Resources.CSharpErrors_CS1001, 1001);
                return _CS1001;
            }
        }
        private static ICompilerReferenceError _CS1001;

        /// <summary><para>C&#9839; compiler error &#35;1002:</para><para>; expected</para></summary>
        public static ICompilerReferenceError CS1002
        {
            get
            {
                if (_CS1002 == null)
                    _CS1002 = new CompilerReferenceError(Resources.CSharpErrors_CS1002, 1002);
                return _CS1002;
            }
        }
        private static ICompilerReferenceError _CS1002;

        /// <summary><para>C&#9839; compiler error &#35;1003:</para><para>Syntax error, 'char' expected</para></summary>
        public static ICompilerReferenceError CS1003
        {
            get
            {
                if (_CS1003 == null)
                    _CS1003 = new CompilerReferenceError(Resources.CSharpErrors_CS1003, 1003);
                return _CS1003;
            }
        }
        private static ICompilerReferenceError _CS1003;

        /// <summary><para>C&#9839; compiler error &#35;1004:</para><para>Duplicate 'modifier' modifier</para></summary>
        public static ICompilerReferenceError CS1004
        {
            get
            {
                if (_CS1004 == null)
                    _CS1004 = new CompilerReferenceError(Resources.CSharpErrors_CS1004, 1004);
                return _CS1004;
            }
        }
        private static ICompilerReferenceError _CS1004;

        /// <summary><para>C&#9839; compiler error &#35;1007:</para><para>Property accessor already defined</para></summary>
        public static ICompilerReferenceError CS1007
        {
            get
            {
                if (_CS1007 == null)
                    _CS1007 = new CompilerReferenceError(Resources.CSharpErrors_CS1007, 1007);
                return _CS1007;
            }
        }
        private static ICompilerReferenceError _CS1007;

        /// <summary><para>C&#9839; compiler error &#35;1008:</para><para>Type byte, sbyte, short, ushort, int, uint, long, or ulong expected</para></summary>
        public static ICompilerReferenceError CS1008
        {
            get
            {
                if (_CS1008 == null)
                    _CS1008 = new CompilerReferenceError(Resources.CSharpErrors_CS1008, 1008);
                return _CS1008;
            }
        }
        private static ICompilerReferenceError _CS1008;

        /// <summary><para>C&#9839; compiler error &#35;1009:</para><para>Unrecognized escape sequence</para></summary>
        public static ICompilerReferenceError CS1009
        {
            get
            {
                if (_CS1009 == null)
                    _CS1009 = new CompilerReferenceError(Resources.CSharpErrors_CS1009, 1009);
                return _CS1009;
            }
        }
        private static ICompilerReferenceError _CS1009;

        /// <summary><para>C&#9839; compiler error &#35;1010:</para><para>Newline in constant</para></summary>
        public static ICompilerReferenceError CS1010
        {
            get
            {
                if (_CS1010 == null)
                    _CS1010 = new CompilerReferenceError(Resources.CSharpErrors_CS1010, 1010);
                return _CS1010;
            }
        }
        private static ICompilerReferenceError _CS1010;

        /// <summary><para>C&#9839; compiler error &#35;1011:</para><para>Empty character literal</para></summary>
        public static ICompilerReferenceError CS1011
        {
            get
            {
                if (_CS1011 == null)
                    _CS1011 = new CompilerReferenceError(Resources.CSharpErrors_CS1011, 1011);
                return _CS1011;
            }
        }
        private static ICompilerReferenceError _CS1011;

        /// <summary><para>C&#9839; compiler error &#35;1012:</para><para>Too many characters in character literal</para></summary>
        public static ICompilerReferenceError CS1012
        {
            get
            {
                if (_CS1012 == null)
                    _CS1012 = new CompilerReferenceError(Resources.CSharpErrors_CS1012, 1012);
                return _CS1012;
            }
        }
        private static ICompilerReferenceError _CS1012;

        /// <summary><para>C&#9839; compiler error &#35;1013:</para><para>Invalid number</para></summary>
        public static ICompilerReferenceError CS1013
        {
            get
            {
                if (_CS1013 == null)
                    _CS1013 = new CompilerReferenceError(Resources.CSharpErrors_CS1013, 1013);
                return _CS1013;
            }
        }
        private static ICompilerReferenceError _CS1013;

        /// <summary><para>C&#9839; compiler error &#35;1014:</para><para>A get or set accessor expected</para></summary>
        public static ICompilerReferenceError CS1014
        {
            get
            {
                if (_CS1014 == null)
                    _CS1014 = new CompilerReferenceError(Resources.CSharpErrors_CS1014, 1014);
                return _CS1014;
            }
        }
        private static ICompilerReferenceError _CS1014;

        /// <summary><para>C&#9839; compiler error &#35;1015:</para><para>An object, string, or class type expected</para></summary>
        public static ICompilerReferenceError CS1015
        {
            get
            {
                if (_CS1015 == null)
                    _CS1015 = new CompilerReferenceError(Resources.CSharpErrors_CS1015, 1015);
                return _CS1015;
            }
        }
        private static ICompilerReferenceError _CS1015;

        /// <summary><para>C&#9839; compiler error &#35;1016:</para><para>Named attribute argument expected</para></summary>
        public static ICompilerReferenceError CS1016
        {
            get
            {
                if (_CS1016 == null)
                    _CS1016 = new CompilerReferenceError(Resources.CSharpErrors_CS1016, 1016);
                return _CS1016;
            }
        }
        private static ICompilerReferenceError _CS1016;

        /// <summary><para>C&#9839; compiler error &#35;1017:</para><para>Catch clauses cannot follow the general catch clause of a try statement</para></summary>
        public static ICompilerReferenceError CS1017
        {
            get
            {
                if (_CS1017 == null)
                    _CS1017 = new CompilerReferenceError(Resources.CSharpErrors_CS1017, 1017);
                return _CS1017;
            }
        }
        private static ICompilerReferenceError _CS1017;

        /// <summary><para>C&#9839; compiler error &#35;1018:</para><para>Keyword 'this' or 'base' expected</para></summary>
        public static ICompilerReferenceError CS1018
        {
            get
            {
                if (_CS1018 == null)
                    _CS1018 = new CompilerReferenceError(Resources.CSharpErrors_CS1018, 1018);
                return _CS1018;
            }
        }
        private static ICompilerReferenceError _CS1018;

        /// <summary><para>C&#9839; compiler error &#35;1019:</para><para>Overloadable unary operator expected</para></summary>
        public static ICompilerReferenceError CS1019
        {
            get
            {
                if (_CS1019 == null)
                    _CS1019 = new CompilerReferenceError(Resources.CSharpErrors_CS1019, 1019);
                return _CS1019;
            }
        }
        private static ICompilerReferenceError _CS1019;

        /// <summary><para>C&#9839; compiler error &#35;1020:</para><para>Overloadable binary operator expected</para></summary>
        public static ICompilerReferenceError CS1020
        {
            get
            {
                if (_CS1020 == null)
                    _CS1020 = new CompilerReferenceError(Resources.CSharpErrors_CS1020, 1020);
                return _CS1020;
            }
        }
        private static ICompilerReferenceError _CS1020;

        /// <summary><para>C&#9839; compiler error &#35;1021:</para><para>Integral constant is too large</para></summary>
        public static ICompilerReferenceError CS1021
        {
            get
            {
                if (_CS1021 == null)
                    _CS1021 = new CompilerReferenceError(Resources.CSharpErrors_CS1021, 1021);
                return _CS1021;
            }
        }
        private static ICompilerReferenceError _CS1021;

        /// <summary><para>C&#9839; compiler error &#35;1022:</para><para>Type or namespace definition, or end-of-file expected</para></summary>
        public static ICompilerReferenceError CS1022
        {
            get
            {
                if (_CS1022 == null)
                    _CS1022 = new CompilerReferenceError(Resources.CSharpErrors_CS1022, 1022);
                return _CS1022;
            }
        }
        private static ICompilerReferenceError _CS1022;

        /// <summary><para>C&#9839; compiler error &#35;1023:</para><para>Embedded statement cannot be a declaration or labeled statement</para></summary>
        public static ICompilerReferenceError CS1023
        {
            get
            {
                if (_CS1023 == null)
                    _CS1023 = new CompilerReferenceError(Resources.CSharpErrors_CS1023, 1023);
                return _CS1023;
            }
        }
        private static ICompilerReferenceError _CS1023;

        /// <summary><para>C&#9839; compiler error &#35;1024:</para><para>Preprocessor directive expected</para></summary>
        public static ICompilerReferenceError CS1024
        {
            get
            {
                if (_CS1024 == null)
                    _CS1024 = new CompilerReferenceError(Resources.CSharpErrors_CS1024, 1024);
                return _CS1024;
            }
        }
        private static ICompilerReferenceError _CS1024;

        /// <summary><para>C&#9839; compiler error &#35;1025:</para><para>Single-line comment or end-of-line expected</para></summary>
        public static ICompilerReferenceError CS1025
        {
            get
            {
                if (_CS1025 == null)
                    _CS1025 = new CompilerReferenceError(Resources.CSharpErrors_CS1025, 1025);
                return _CS1025;
            }
        }
        private static ICompilerReferenceError _CS1025;

        /// <summary><para>C&#9839; compiler error &#35;1026:</para><para>) expected</para></summary>
        public static ICompilerReferenceError CS1026
        {
            get
            {
                if (_CS1026 == null)
                    _CS1026 = new CompilerReferenceError(Resources.CSharpErrors_CS1026, 1026);
                return _CS1026;
            }
        }
        private static ICompilerReferenceError _CS1026;

        /// <summary><para>C&#9839; compiler error &#35;1027:</para><para>#endif directive expected</para></summary>
        public static ICompilerReferenceError CS1027
        {
            get
            {
                if (_CS1027 == null)
                    _CS1027 = new CompilerReferenceError(Resources.CSharpErrors_CS1027, 1027);
                return _CS1027;
            }
        }
        private static ICompilerReferenceError _CS1027;

        /// <summary><para>C&#9839; compiler error &#35;1028:</para><para>Unexpected preprocessor directive</para></summary>
        public static ICompilerReferenceError CS1028
        {
            get
            {
                if (_CS1028 == null)
                    _CS1028 = new CompilerReferenceError(Resources.CSharpErrors_CS1028, 1028);
                return _CS1028;
            }
        }
        private static ICompilerReferenceError _CS1028;

        /// <summary><para>C&#9839; compiler error &#35;1029:</para><para>#error: 'text'</para></summary>
        public static ICompilerReferenceError CS1029
        {
            get
            {
                if (_CS1029 == null)
                    _CS1029 = new CompilerReferenceError(Resources.CSharpErrors_CS1029, 1029);
                return _CS1029;
            }
        }
        private static ICompilerReferenceError _CS1029;

        /// <summary><para>C&#9839; compiler error &#35;1031:</para><para>Type expected</para></summary>
        public static ICompilerReferenceError CS1031
        {
            get
            {
                if (_CS1031 == null)
                    _CS1031 = new CompilerReferenceError(Resources.CSharpErrors_CS1031, 1031);
                return _CS1031;
            }
        }
        private static ICompilerReferenceError _CS1031;

        /// <summary><para>C&#9839; compiler error &#35;1032:</para><para>Cannot define/undefine preprocessor symbols after first token in file</para></summary>
        public static ICompilerReferenceError CS1032
        {
            get
            {
                if (_CS1032 == null)
                    _CS1032 = new CompilerReferenceError(Resources.CSharpErrors_CS1032, 1032);
                return _CS1032;
            }
        }
        private static ICompilerReferenceError _CS1032;

        /// <summary><para>C&#9839; compiler error &#35;1033:</para><para>Source file has exceeded the limit of 16,707,565 lines representable in the PDB; debug information will be incorrect</para></summary>
        public static ICompilerReferenceError CS1033
        {
            get
            {
                if (_CS1033 == null)
                    _CS1033 = new CompilerReferenceError(Resources.CSharpErrors_CS1033, 1033);
                return _CS1033;
            }
        }
        private static ICompilerReferenceError _CS1033;

        /// <summary><para>C&#9839; compiler error &#35;1034:</para><para>Compiler limit exceeded: Line cannot exceed 'number' characters</para></summary>
        public static ICompilerReferenceError CS1034
        {
            get
            {
                if (_CS1034 == null)
                    _CS1034 = new CompilerReferenceError(Resources.CSharpErrors_CS1034, 1034);
                return _CS1034;
            }
        }
        private static ICompilerReferenceError _CS1034;

        /// <summary><para>C&#9839; compiler error &#35;1035:</para><para>End-of-file found, '*/' expected</para></summary>
        public static ICompilerReferenceError CS1035
        {
            get
            {
                if (_CS1035 == null)
                    _CS1035 = new CompilerReferenceError(Resources.CSharpErrors_CS1035, 1035);
                return _CS1035;
            }
        }
        private static ICompilerReferenceError _CS1035;

        /// <summary><para>C&#9839; compiler error &#35;1036:</para><para>( or . expected</para></summary>
        public static ICompilerReferenceError CS1036
        {
            get
            {
                if (_CS1036 == null)
                    _CS1036 = new CompilerReferenceError(Resources.CSharpErrors_CS1036, 1036);
                return _CS1036;
            }
        }
        private static ICompilerReferenceError _CS1036;

        /// <summary><para>C&#9839; compiler error &#35;1037:</para><para>Overloadable operator expected</para></summary>
        public static ICompilerReferenceError CS1037
        {
            get
            {
                if (_CS1037 == null)
                    _CS1037 = new CompilerReferenceError(Resources.CSharpErrors_CS1037, 1037);
                return _CS1037;
            }
        }
        private static ICompilerReferenceError _CS1037;

        /// <summary><para>C&#9839; compiler error &#35;1038:</para><para>#endregion directive expected</para></summary>
        public static ICompilerReferenceError CS1038
        {
            get
            {
                if (_CS1038 == null)
                    _CS1038 = new CompilerReferenceError(Resources.CSharpErrors_CS1038, 1038);
                return _CS1038;
            }
        }
        private static ICompilerReferenceError _CS1038;

        /// <summary><para>C&#9839; compiler error &#35;1039:</para><para>Unterminated string literal</para></summary>
        public static ICompilerReferenceError CS1039
        {
            get
            {
                if (_CS1039 == null)
                    _CS1039 = new CompilerReferenceError(Resources.CSharpErrors_CS1039, 1039);
                return _CS1039;
            }
        }
        private static ICompilerReferenceError _CS1039;

        /// <summary><para>C&#9839; compiler error &#35;1040:</para><para>Preprocessor directives must appear as the first non-whitespace character on a line</para></summary>
        public static ICompilerReferenceError CS1040
        {
            get
            {
                if (_CS1040 == null)
                    _CS1040 = new CompilerReferenceError(Resources.CSharpErrors_CS1040, 1040);
                return _CS1040;
            }
        }
        private static ICompilerReferenceError _CS1040;

        /// <summary><para>C&#9839; compiler error &#35;1041:</para><para>Identifier expected, 'keyword' is a keyword</para></summary>
        public static ICompilerReferenceError CS1041
        {
            get
            {
                if (_CS1041 == null)
                    _CS1041 = new CompilerReferenceError(Resources.CSharpErrors_CS1041, 1041);
                return _CS1041;
            }
        }
        private static ICompilerReferenceError _CS1041;

        /// <summary><para>C&#9839; compiler error &#35;1043:</para><para>{ or ; expected</para></summary>
        public static ICompilerReferenceError CS1043
        {
            get
            {
                if (_CS1043 == null)
                    _CS1043 = new CompilerReferenceError(Resources.CSharpErrors_CS1043, 1043);
                return _CS1043;
            }
        }
        private static ICompilerReferenceError _CS1043;

        /// <summary><para>C&#9839; compiler error &#35;1044:</para><para>Cannot use more than one type in a for, using, fixed, or declaration statement</para></summary>
        public static ICompilerReferenceError CS1044
        {
            get
            {
                if (_CS1044 == null)
                    _CS1044 = new CompilerReferenceError(Resources.CSharpErrors_CS1044, 1044);
                return _CS1044;
            }
        }
        private static ICompilerReferenceError _CS1044;

        /// <summary><para>C&#9839; compiler error &#35;1055:</para><para>An add or remove accessor expected</para></summary>
        public static ICompilerReferenceError CS1055
        {
            get
            {
                if (_CS1055 == null)
                    _CS1055 = new CompilerReferenceError(Resources.CSharpErrors_CS1055, 1055);
                return _CS1055;
            }
        }
        private static ICompilerReferenceError _CS1055;

        /// <summary><para>C&#9839; compiler error &#35;1056:</para><para>Unexpected character 'character'</para></summary>
        public static ICompilerReferenceError CS1056
        {
            get
            {
                if (_CS1056 == null)
                    _CS1056 = new CompilerReferenceError(Resources.CSharpErrors_CS1056, 1056);
                return _CS1056;
            }
        }
        private static ICompilerReferenceError _CS1056;

        /// <summary><para>C&#9839; compiler error &#35;1057:</para><para>'member': static classes cannot contain protected members</para></summary>
        public static ICompilerReferenceError CS1057
        {
            get
            {
                if (_CS1057 == null)
                    _CS1057 = new CompilerReferenceError(Resources.CSharpErrors_CS1057, 1057);
                return _CS1057;
            }
        }
        private static ICompilerReferenceError _CS1057;

        /// <summary><para>C&#9839; compiler error &#35;1059:</para><para>The operand of an increment or decrement operator must be a variable, property or indexer.</para></summary>
        public static ICompilerReferenceError CS1059
        {
            get
            {
                if (_CS1059 == null)
                    _CS1059 = new CompilerReferenceError(Resources.CSharpErrors_CS1059, 1059);
                return _CS1059;
            }
        }
        private static ICompilerReferenceError _CS1059;

        /// <summary><para>C&#9839; compiler error &#35;1061:</para><para>'type' does not contain a definition for 'member' and no extension method 'name' accepting a first argument of type 'type' could be found (are you missing a using directive or an assembly reference?).</para></summary>
        public static ICompilerReferenceError CS1061
        {
            get
            {
                if (_CS1061 == null)
                    _CS1061 = new CompilerReferenceError(Resources.CSharpErrors_CS1061, 1061);
                return _CS1061;
            }
        }
        private static ICompilerReferenceError _CS1061;

        /// <summary><para>C&#9839; compiler error &#35;1100:</para><para>Method 'name' has a parameter modifier 'this' which is not on the first parameter.</para></summary>
        public static ICompilerReferenceError CS1100
        {
            get
            {
                if (_CS1100 == null)
                    _CS1100 = new CompilerReferenceError(Resources.CSharpErrors_CS1100, 1100);
                return _CS1100;
            }
        }
        private static ICompilerReferenceError _CS1100;

        /// <summary><para>C&#9839; compiler error &#35;1101:</para><para>The parameter modifier 'ref' cannot be used with 'this'.</para></summary>
        public static ICompilerReferenceError CS1101
        {
            get
            {
                if (_CS1101 == null)
                    _CS1101 = new CompilerReferenceError(Resources.CSharpErrors_CS1101, 1101);
                return _CS1101;
            }
        }
        private static ICompilerReferenceError _CS1101;

        /// <summary><para>C&#9839; compiler error &#35;1102:</para><para>The parameter modifier 'out' cannot be used with 'this'.</para></summary>
        public static ICompilerReferenceError CS1102
        {
            get
            {
                if (_CS1102 == null)
                    _CS1102 = new CompilerReferenceError(Resources.CSharpErrors_CS1102, 1102);
                return _CS1102;
            }
        }
        private static ICompilerReferenceError _CS1102;

        /// <summary><para>C&#9839; compiler error &#35;1103:</para><para>The first parameter of an extension method cannot be of type 'type'.</para></summary>
        public static ICompilerReferenceError CS1103
        {
            get
            {
                if (_CS1103 == null)
                    _CS1103 = new CompilerReferenceError(Resources.CSharpErrors_CS1103, 1103);
                return _CS1103;
            }
        }
        private static ICompilerReferenceError _CS1103;

        /// <summary><para>C&#9839; compiler error &#35;1104:</para><para>A parameter array cannot be used with 'this' modifier on an extension method.</para></summary>
        public static ICompilerReferenceError CS1104
        {
            get
            {
                if (_CS1104 == null)
                    _CS1104 = new CompilerReferenceError(Resources.CSharpErrors_CS1104, 1104);
                return _CS1104;
            }
        }
        private static ICompilerReferenceError _CS1104;

        /// <summary><para>C&#9839; compiler error &#35;1105:</para><para>Extension methods must be static.</para></summary>
        public static ICompilerReferenceError CS1105
        {
            get
            {
                if (_CS1105 == null)
                    _CS1105 = new CompilerReferenceError(Resources.CSharpErrors_CS1105, 1105);
                return _CS1105;
            }
        }
        private static ICompilerReferenceError _CS1105;

        /// <summary><para>C&#9839; compiler error &#35;1106:</para><para>Extension methods must be defined in a non generic static class.</para></summary>
        public static ICompilerReferenceError CS1106
        {
            get
            {
                if (_CS1106 == null)
                    _CS1106 = new CompilerReferenceError(Resources.CSharpErrors_CS1106, 1106);
                return _CS1106;
            }
        }
        private static ICompilerReferenceError _CS1106;

        /// <summary><para>C&#9839; compiler error &#35;1107:</para><para>A parameter can only have one 'modifier name' modifier.</para></summary>
        public static ICompilerReferenceError CS1107
        {
            get
            {
                if (_CS1107 == null)
                    _CS1107 = new CompilerReferenceError(Resources.CSharpErrors_CS1107, 1107);
                return _CS1107;
            }
        }
        private static ICompilerReferenceError _CS1107;

        /// <summary><para>C&#9839; compiler error &#35;1108:</para><para>A parameter cannot have all the specified modifiers; there are too many modifiers on the parameter.</para></summary>
        public static ICompilerReferenceError CS1108
        {
            get
            {
                if (_CS1108 == null)
                    _CS1108 = new CompilerReferenceError(Resources.CSharpErrors_CS1108, 1108);
                return _CS1108;
            }
        }
        private static ICompilerReferenceError _CS1108;

        /// <summary><para>C&#9839; compiler error &#35;1109:</para><para>Extension Methods must be defined on top level static classes, 'name' is a nested class.</para></summary>
        public static ICompilerReferenceError CS1109
        {
            get
            {
                if (_CS1109 == null)
                    _CS1109 = new CompilerReferenceError(Resources.CSharpErrors_CS1109, 1109);
                return _CS1109;
            }
        }
        private static ICompilerReferenceError _CS1109;

        /// <summary><para>C&#9839; compiler error &#35;1110:</para><para>Cannot use 'this' modifier on first parameter of method declaration without a reference to System.Core.dll. Add a reference to System.Core.dll or remove 'this' modifier from the method declaration.</para></summary>
        public static ICompilerReferenceError CS1110
        {
            get
            {
                if (_CS1110 == null)
                    _CS1110 = new CompilerReferenceError(Resources.CSharpErrors_CS1110, 1110);
                return _CS1110;
            }
        }
        private static ICompilerReferenceError _CS1110;

        /// <summary><para>C&#9839; compiler error &#35;1112:</para><para>Do not use 'System.Runtime.CompilerServices.ExtensionAttribute'. Use the 'this' keyword instead.</para></summary>
        public static ICompilerReferenceError CS1112
        {
            get
            {
                if (_CS1112 == null)
                    _CS1112 = new CompilerReferenceError(Resources.CSharpErrors_CS1112, 1112);
                return _CS1112;
            }
        }
        private static ICompilerReferenceError _CS1112;

        /// <summary><para>C&#9839; compiler error &#35;1113:</para><para>Extension methods 'name' defined on value type 'name' cannot be used to create delegates.</para></summary>
        public static ICompilerReferenceError CS1113
        {
            get
            {
                if (_CS1113 == null)
                    _CS1113 = new CompilerReferenceError(Resources.CSharpErrors_CS1113, 1113);
                return _CS1113;
            }
        }
        private static ICompilerReferenceError _CS1113;

        /// <summary><para>C&#9839; compiler error &#35;1501:</para><para>No overload for method 'method' takes 'number' arguments</para></summary>
        public static ICompilerReferenceError CS1501
        {
            get
            {
                if (_CS1501 == null)
                    _CS1501 = new CompilerReferenceError(Resources.CSharpErrors_CS1501, 1501);
                return _CS1501;
            }
        }
        private static ICompilerReferenceError _CS1501;

        /// <summary><para>C&#9839; compiler error &#35;1502:</para><para>The best overloaded Add method 'name' for the collection initializer has some invalid arguments</para></summary>
        public static ICompilerReferenceError CS1502
        {
            get
            {
                if (_CS1502 == null)
                    _CS1502 = new CompilerReferenceError(Resources.CSharpErrors_CS1502, 1502);
                return _CS1502;
            }
        }
        private static ICompilerReferenceError _CS1502;

        /// <summary><para>C&#9839; compiler error &#35;1503:</para><para>The best overloaded Add method 'name for the collection initializer has some invalid arguments</para></summary>
        public static ICompilerReferenceError CS1503
        {
            get
            {
                if (_CS1503 == null)
                    _CS1503 = new CompilerReferenceError(Resources.CSharpErrors_CS1503, 1503);
                return _CS1503;
            }
        }
        private static ICompilerReferenceError _CS1503;

        /// <summary><para>C&#9839; compiler error &#35;1504:</para><para>Source file 'file' could not be opened ('reason')</para></summary>
        public static ICompilerReferenceError CS1504
        {
            get
            {
                if (_CS1504 == null)
                    _CS1504 = new CompilerReferenceError(Resources.CSharpErrors_CS1504, 1504);
                return _CS1504;
            }
        }
        private static ICompilerReferenceError _CS1504;

        /// <summary><para>C&#9839; compiler error &#35;1507:</para><para>Cannot link resource file 'file' when building a module</para></summary>
        public static ICompilerReferenceError CS1507
        {
            get
            {
                if (_CS1507 == null)
                    _CS1507 = new CompilerReferenceError(Resources.CSharpErrors_CS1507, 1507);
                return _CS1507;
            }
        }
        private static ICompilerReferenceError _CS1507;

        /// <summary><para>C&#9839; compiler error &#35;1508:</para><para>Resource identifier 'identifier' has already been used in this assembly</para></summary>
        public static ICompilerReferenceError CS1508
        {
            get
            {
                if (_CS1508 == null)
                    _CS1508 = new CompilerReferenceError(Resources.CSharpErrors_CS1508, 1508);
                return _CS1508;
            }
        }
        private static ICompilerReferenceError _CS1508;

        /// <summary><para>C&#9839; compiler error &#35;1509:</para><para>Referenced file 'file' is not an assembly; use '/addmodule' option instead</para></summary>
        public static ICompilerReferenceError CS1509
        {
            get
            {
                if (_CS1509 == null)
                    _CS1509 = new CompilerReferenceError(Resources.CSharpErrors_CS1509, 1509);
                return _CS1509;
            }
        }
        private static ICompilerReferenceError _CS1509;

        /// <summary><para>C&#9839; compiler error &#35;1510:</para><para>A ref or out argument must be an assignable variable</para></summary>
        public static ICompilerReferenceError CS1510
        {
            get
            {
                if (_CS1510 == null)
                    _CS1510 = new CompilerReferenceError(Resources.CSharpErrors_CS1510, 1510);
                return _CS1510;
            }
        }
        private static ICompilerReferenceError _CS1510;

        /// <summary><para>C&#9839; compiler error &#35;1511:</para><para>Keyword 'base' is not available in a static method</para></summary>
        public static ICompilerReferenceError CS1511
        {
            get
            {
                if (_CS1511 == null)
                    _CS1511 = new CompilerReferenceError(Resources.CSharpErrors_CS1511, 1511);
                return _CS1511;
            }
        }
        private static ICompilerReferenceError _CS1511;

        /// <summary><para>C&#9839; compiler error &#35;1512:</para><para>Keyword 'base' is not available in the current context</para></summary>
        public static ICompilerReferenceError CS1512
        {
            get
            {
                if (_CS1512 == null)
                    _CS1512 = new CompilerReferenceError(Resources.CSharpErrors_CS1512, 1512);
                return _CS1512;
            }
        }
        private static ICompilerReferenceError _CS1512;

        /// <summary><para>C&#9839; compiler error &#35;1513:</para><para>} expected</para></summary>
        public static ICompilerReferenceError CS1513
        {
            get
            {
                if (_CS1513 == null)
                    _CS1513 = new CompilerReferenceError(Resources.CSharpErrors_CS1513, 1513);
                return _CS1513;
            }
        }
        private static ICompilerReferenceError _CS1513;

        /// <summary><para>C&#9839; compiler error &#35;1514:</para><para>{ expected</para></summary>
        public static ICompilerReferenceError CS1514
        {
            get
            {
                if (_CS1514 == null)
                    _CS1514 = new CompilerReferenceError(Resources.CSharpErrors_CS1514, 1514);
                return _CS1514;
            }
        }
        private static ICompilerReferenceError _CS1514;

        /// <summary><para>C&#9839; compiler error &#35;1515:</para><para>'in' expected</para></summary>
        public static ICompilerReferenceError CS1515
        {
            get
            {
                if (_CS1515 == null)
                    _CS1515 = new CompilerReferenceError(Resources.CSharpErrors_CS1515, 1515);
                return _CS1515;
            }
        }
        private static ICompilerReferenceError _CS1515;

        /// <summary><para>C&#9839; compiler error &#35;1517:</para><para>Invalid preprocessor expression</para></summary>
        public static ICompilerReferenceError CS1517
        {
            get
            {
                if (_CS1517 == null)
                    _CS1517 = new CompilerReferenceError(Resources.CSharpErrors_CS1517, 1517);
                return _CS1517;
            }
        }
        private static ICompilerReferenceError _CS1517;

        /// <summary><para>C&#9839; compiler error &#35;1518:</para><para>Expected class, delegate, enum, interface, or struct</para></summary>
        public static ICompilerReferenceError CS1518
        {
            get
            {
                if (_CS1518 == null)
                    _CS1518 = new CompilerReferenceError(Resources.CSharpErrors_CS1518, 1518);
                return _CS1518;
            }
        }
        private static ICompilerReferenceError _CS1518;

        /// <summary><para>C&#9839; compiler error &#35;1519:</para><para>Invalid token 'token' in class, struct, or interface member declaration</para></summary>
        public static ICompilerReferenceError CS1519
        {
            get
            {
                if (_CS1519 == null)
                    _CS1519 = new CompilerReferenceError(Resources.CSharpErrors_CS1519, 1519);
                return _CS1519;
            }
        }
        private static ICompilerReferenceError _CS1519;

        /// <summary><para>C&#9839; compiler error &#35;1520:</para><para>Method must have a return type</para></summary>
        public static ICompilerReferenceError CS1520
        {
            get
            {
                if (_CS1520 == null)
                    _CS1520 = new CompilerReferenceError(Resources.CSharpErrors_CS1520, 1520);
                return _CS1520;
            }
        }
        private static ICompilerReferenceError _CS1520;

        /// <summary><para>C&#9839; compiler error &#35;1521:</para><para>Invalid base type</para></summary>
        public static ICompilerReferenceError CS1521
        {
            get
            {
                if (_CS1521 == null)
                    _CS1521 = new CompilerReferenceError(Resources.CSharpErrors_CS1521, 1521);
                return _CS1521;
            }
        }
        private static ICompilerReferenceError _CS1521;

        /// <summary><para>C&#9839; compiler error &#35;1524:</para><para>Expected catch or finally</para></summary>
        public static ICompilerReferenceError CS1524
        {
            get
            {
                if (_CS1524 == null)
                    _CS1524 = new CompilerReferenceError(Resources.CSharpErrors_CS1524, 1524);
                return _CS1524;
            }
        }
        private static ICompilerReferenceError _CS1524;

        /// <summary><para>C&#9839; compiler error &#35;1525:</para><para>Invalid expression term 'character'</para></summary>
        public static ICompilerReferenceError CS1525
        {
            get
            {
                if (_CS1525 == null)
                    _CS1525 = new CompilerReferenceError(Resources.CSharpErrors_CS1525, 1525);
                return _CS1525;
            }
        }
        private static ICompilerReferenceError _CS1525;

        /// <summary><para>C&#9839; compiler error &#35;1526:</para><para>A new expression requires (), [], or {} after type</para></summary>
        public static ICompilerReferenceError CS1526
        {
            get
            {
                if (_CS1526 == null)
                    _CS1526 = new CompilerReferenceError(Resources.CSharpErrors_CS1526, 1526);
                return _CS1526;
            }
        }
        private static ICompilerReferenceError _CS1526;

        /// <summary><para>C&#9839; compiler error &#35;1527:</para><para>Elements defined in a namespace cannot be explicitly declared as private, protected, or protected internal</para></summary>
        public static ICompilerReferenceError CS1527
        {
            get
            {
                if (_CS1527 == null)
                    _CS1527 = new CompilerReferenceError(Resources.CSharpErrors_CS1527, 1527);
                return _CS1527;
            }
        }
        private static ICompilerReferenceError _CS1527;

        /// <summary><para>C&#9839; compiler error &#35;1528:</para><para>Expected ; or = (cannot specify constructor arguments in declaration)</para></summary>
        public static ICompilerReferenceError CS1528
        {
            get
            {
                if (_CS1528 == null)
                    _CS1528 = new CompilerReferenceError(Resources.CSharpErrors_CS1528, 1528);
                return _CS1528;
            }
        }
        private static ICompilerReferenceError _CS1528;

        /// <summary><para>C&#9839; compiler error &#35;1529:</para><para>A using clause must precede all other elements defined in the namespace except extern alias declarations</para></summary>
        public static ICompilerReferenceError CS1529
        {
            get
            {
                if (_CS1529 == null)
                    _CS1529 = new CompilerReferenceError(Resources.CSharpErrors_CS1529, 1529);
                return _CS1529;
            }
        }
        private static ICompilerReferenceError _CS1529;

        /// <summary><para>C&#9839; compiler error &#35;1530:</para><para>Keyword 'new' is not allowed on elements defined in a namespace</para></summary>
        public static ICompilerReferenceError CS1530
        {
            get
            {
                if (_CS1530 == null)
                    _CS1530 = new CompilerReferenceError(Resources.CSharpErrors_CS1530, 1530);
                return _CS1530;
            }
        }
        private static ICompilerReferenceError _CS1530;

        /// <summary><para>C&#9839; compiler error &#35;1534:</para><para>Overloaded binary operator 'operator' takes two parameters</para></summary>
        public static ICompilerReferenceError CS1534
        {
            get
            {
                if (_CS1534 == null)
                    _CS1534 = new CompilerReferenceError(Resources.CSharpErrors_CS1534, 1534);
                return _CS1534;
            }
        }
        private static ICompilerReferenceError _CS1534;

        /// <summary><para>C&#9839; compiler error &#35;1535:</para><para>Overloaded unary operator 'operator' takes one parameter</para></summary>
        public static ICompilerReferenceError CS1535
        {
            get
            {
                if (_CS1535 == null)
                    _CS1535 = new CompilerReferenceError(Resources.CSharpErrors_CS1535, 1535);
                return _CS1535;
            }
        }
        private static ICompilerReferenceError _CS1535;

        /// <summary><para>C&#9839; compiler error &#35;1536:</para><para>Invalid parameter type void</para></summary>
        public static ICompilerReferenceError CS1536
        {
            get
            {
                if (_CS1536 == null)
                    _CS1536 = new CompilerReferenceError(Resources.CSharpErrors_CS1536, 1536);
                return _CS1536;
            }
        }
        private static ICompilerReferenceError _CS1536;

        /// <summary><para>C&#9839; compiler error &#35;1537:</para><para>The using alias 'alias' appeared previously in this namespace</para></summary>
        public static ICompilerReferenceError CS1537
        {
            get
            {
                if (_CS1537 == null)
                    _CS1537 = new CompilerReferenceError(Resources.CSharpErrors_CS1537, 1537);
                return _CS1537;
            }
        }
        private static ICompilerReferenceError _CS1537;

        /// <summary><para>C&#9839; compiler error &#35;1540:</para><para>Cannot access protected member 'member' via a qualifier of type 'type1'; the qualifier must be of type 'type2' (or derived from it)</para></summary>
        public static ICompilerReferenceError CS1540
        {
            get
            {
                if (_CS1540 == null)
                    _CS1540 = new CompilerReferenceError(Resources.CSharpErrors_CS1540, 1540);
                return _CS1540;
            }
        }
        private static ICompilerReferenceError _CS1540;

        /// <summary><para>C&#9839; compiler error &#35;1541:</para><para>Invalid reference option: 'symbol' — cannot reference directories</para></summary>
        public static ICompilerReferenceError CS1541
        {
            get
            {
                if (_CS1541 == null)
                    _CS1541 = new CompilerReferenceError(Resources.CSharpErrors_CS1541, 1541);
                return _CS1541;
            }
        }
        private static ICompilerReferenceError _CS1541;

        /// <summary><para>C&#9839; compiler error &#35;1542:</para><para>'dll' cannot be added to this assembly because it already is an assembly; use '/R' option instead</para></summary>
        public static ICompilerReferenceError CS1542
        {
            get
            {
                if (_CS1542 == null)
                    _CS1542 = new CompilerReferenceError(Resources.CSharpErrors_CS1542, 1542);
                return _CS1542;
            }
        }
        private static ICompilerReferenceError _CS1542;

        /// <summary><para>C&#9839; compiler error &#35;1545:</para><para>Property, indexer, or event 'property' is not supported by the language; try directly calling accessor methods 'set accessor' or 'get accessor'</para></summary>
        public static ICompilerReferenceError CS1545
        {
            get
            {
                if (_CS1545 == null)
                    _CS1545 = new CompilerReferenceError(Resources.CSharpErrors_CS1545, 1545);
                return _CS1545;
            }
        }
        private static ICompilerReferenceError _CS1545;

        /// <summary><para>C&#9839; compiler error &#35;1546:</para><para>Property, indexer, or event 'property' is not supported by the language; try directly calling accessor method 'accessor'</para></summary>
        public static ICompilerReferenceError CS1546
        {
            get
            {
                if (_CS1546 == null)
                    _CS1546 = new CompilerReferenceError(Resources.CSharpErrors_CS1546, 1546);
                return _CS1546;
            }
        }
        private static ICompilerReferenceError _CS1546;

        /// <summary><para>C&#9839; compiler error &#35;1547:</para><para>Keyword 'void' cannot be used in this context</para></summary>
        public static ICompilerReferenceError CS1547
        {
            get
            {
                if (_CS1547 == null)
                    _CS1547 = new CompilerReferenceError(Resources.CSharpErrors_CS1547, 1547);
                return _CS1547;
            }
        }
        private static ICompilerReferenceError _CS1547;

        /// <summary><para>C&#9839; compiler error &#35;1548:</para><para>Cryptographic failure while signing assembly 'assembly' — 'reason'</para></summary>
        public static ICompilerReferenceError CS1548
        {
            get
            {
                if (_CS1548 == null)
                    _CS1548 = new CompilerReferenceError(Resources.CSharpErrors_CS1548, 1548);
                return _CS1548;
            }
        }
        private static ICompilerReferenceError _CS1548;

        /// <summary><para>C&#9839; compiler error &#35;1549:</para><para>Appropriate cryptographic service not found</para></summary>
        public static ICompilerReferenceError CS1549
        {
            get
            {
                if (_CS1549 == null)
                    _CS1549 = new CompilerReferenceError(Resources.CSharpErrors_CS1549, 1549);
                return _CS1549;
            }
        }
        private static ICompilerReferenceError _CS1549;

        /// <summary><para>C&#9839; compiler error &#35;1551:</para><para>Indexers must have at least one parameter</para></summary>
        public static ICompilerReferenceError CS1551
        {
            get
            {
                if (_CS1551 == null)
                    _CS1551 = new CompilerReferenceError(Resources.CSharpErrors_CS1551, 1551);
                return _CS1551;
            }
        }
        private static ICompilerReferenceError _CS1551;

        /// <summary><para>C&#9839; compiler error &#35;1552:</para><para>Array type specifier, [], must appear before parameter name</para></summary>
        public static ICompilerReferenceError CS1552
        {
            get
            {
                if (_CS1552 == null)
                    _CS1552 = new CompilerReferenceError(Resources.CSharpErrors_CS1552, 1552);
                return _CS1552;
            }
        }
        private static ICompilerReferenceError _CS1552;

        /// <summary><para>C&#9839; compiler error &#35;1553:</para><para>Declaration is not valid; use 'modifier operator &lt;dest-type&gt; (...' instead</para></summary>
        public static ICompilerReferenceError CS1553
        {
            get
            {
                if (_CS1553 == null)
                    _CS1553 = new CompilerReferenceError(Resources.CSharpErrors_CS1553, 1553);
                return _CS1553;
            }
        }
        private static ICompilerReferenceError _CS1553;

        /// <summary><para>C&#9839; compiler error &#35;1554:</para><para>Declaration is not valid; use '&lt;type&gt; operator op (...' instead</para></summary>
        public static ICompilerReferenceError CS1554
        {
            get
            {
                if (_CS1554 == null)
                    _CS1554 = new CompilerReferenceError(Resources.CSharpErrors_CS1554, 1554);
                return _CS1554;
            }
        }
        private static ICompilerReferenceError _CS1554;

        /// <summary><para>C&#9839; compiler error &#35;1555:</para><para>Could not find 'class' specified for Main method</para></summary>
        public static ICompilerReferenceError CS1555
        {
            get
            {
                if (_CS1555 == null)
                    _CS1555 = new CompilerReferenceError(Resources.CSharpErrors_CS1555, 1555);
                return _CS1555;
            }
        }
        private static ICompilerReferenceError _CS1555;

        /// <summary><para>C&#9839; compiler error &#35;1556:</para><para>'construct' specified for Main method must be a valid class or struct</para></summary>
        public static ICompilerReferenceError CS1556
        {
            get
            {
                if (_CS1556 == null)
                    _CS1556 = new CompilerReferenceError(Resources.CSharpErrors_CS1556, 1556);
                return _CS1556;
            }
        }
        private static ICompilerReferenceError _CS1556;

        /// <summary><para>C&#9839; compiler error &#35;1557:</para><para>Cannot use 'class' for Main method because it is in a different output file</para></summary>
        public static ICompilerReferenceError CS1557
        {
            get
            {
                if (_CS1557 == null)
                    _CS1557 = new CompilerReferenceError(Resources.CSharpErrors_CS1557, 1557);
                return _CS1557;
            }
        }
        private static ICompilerReferenceError _CS1557;

        /// <summary><para>C&#9839; compiler error &#35;1558:</para><para>'class' does not have a suitable static Main method</para></summary>
        public static ICompilerReferenceError CS1558
        {
            get
            {
                if (_CS1558 == null)
                    _CS1558 = new CompilerReferenceError(Resources.CSharpErrors_CS1558, 1558);
                return _CS1558;
            }
        }
        private static ICompilerReferenceError _CS1558;

        /// <summary><para>C&#9839; compiler error &#35;1559:</para><para>Cannot use 'object' for Main method because it is imported</para></summary>
        public static ICompilerReferenceError CS1559
        {
            get
            {
                if (_CS1559 == null)
                    _CS1559 = new CompilerReferenceError(Resources.CSharpErrors_CS1559, 1559);
                return _CS1559;
            }
        }
        private static ICompilerReferenceError _CS1559;

        /// <summary><para>C&#9839; compiler error &#35;1560:</para><para>Invalid filename specified for preprocessor directive. Filename is too long or not a valid filename</para></summary>
        public static ICompilerReferenceError CS1560
        {
            get
            {
                if (_CS1560 == null)
                    _CS1560 = new CompilerReferenceError(Resources.CSharpErrors_CS1560, 1560);
                return _CS1560;
            }
        }
        private static ICompilerReferenceError _CS1560;

        /// <summary><para>C&#9839; compiler error &#35;1561:</para><para>Output filename is too long or invalid</para></summary>
        public static ICompilerReferenceError CS1561
        {
            get
            {
                if (_CS1561 == null)
                    _CS1561 = new CompilerReferenceError(Resources.CSharpErrors_CS1561, 1561);
                return _CS1561;
            }
        }
        private static ICompilerReferenceError _CS1561;

        /// <summary><para>C&#9839; compiler error &#35;1562:</para><para>Outputs without source must have the /out option specified</para></summary>
        public static ICompilerReferenceError CS1562
        {
            get
            {
                if (_CS1562 == null)
                    _CS1562 = new CompilerReferenceError(Resources.CSharpErrors_CS1562, 1562);
                return _CS1562;
            }
        }
        private static ICompilerReferenceError _CS1562;

        /// <summary><para>C&#9839; compiler error &#35;1563:</para><para>Output 'output file' does not have any source files</para></summary>
        public static ICompilerReferenceError CS1563
        {
            get
            {
                if (_CS1563 == null)
                    _CS1563 = new CompilerReferenceError(Resources.CSharpErrors_CS1563, 1563);
                return _CS1563;
            }
        }
        private static ICompilerReferenceError _CS1563;

        /// <summary><para>C&#9839; compiler error &#35;1564:</para><para>Conflicting options specified: Win32 resource file; Win32 manifest.</para></summary>
        public static ICompilerReferenceError CS1564
        {
            get
            {
                if (_CS1564 == null)
                    _CS1564 = new CompilerReferenceError(Resources.CSharpErrors_CS1564, 1564);
                return _CS1564;
            }
        }
        private static ICompilerReferenceError _CS1564;

        /// <summary><para>C&#9839; compiler error &#35;1565:</para><para>Conflicting options specified: Win32 resource file; Win32 icon</para></summary>
        public static ICompilerReferenceError CS1565
        {
            get
            {
                if (_CS1565 == null)
                    _CS1565 = new CompilerReferenceError(Resources.CSharpErrors_CS1565, 1565);
                return _CS1565;
            }
        }
        private static ICompilerReferenceError _CS1565;

        /// <summary><para>C&#9839; compiler error &#35;1566:</para><para>Error reading resource file 'file' — 'reason'</para></summary>
        public static ICompilerReferenceError CS1566
        {
            get
            {
                if (_CS1566 == null)
                    _CS1566 = new CompilerReferenceError(Resources.CSharpErrors_CS1566, 1566);
                return _CS1566;
            }
        }
        private static ICompilerReferenceError _CS1566;

        /// <summary><para>C&#9839; compiler error &#35;1567:</para><para>Error generating Win32 resource: 'file'</para></summary>
        public static ICompilerReferenceError CS1567
        {
            get
            {
                if (_CS1567 == null)
                    _CS1567 = new CompilerReferenceError(Resources.CSharpErrors_CS1567, 1567);
                return _CS1567;
            }
        }
        private static ICompilerReferenceError _CS1567;

        /// <summary><para>C&#9839; compiler error &#35;1569:</para><para>Error generating XML documentation file 'Filename' ('reason')</para></summary>
        public static ICompilerReferenceError CS1569
        {
            get
            {
                if (_CS1569 == null)
                    _CS1569 = new CompilerReferenceError(Resources.CSharpErrors_CS1569, 1569);
                return _CS1569;
            }
        }
        private static ICompilerReferenceError _CS1569;

        /// <summary><para>C&#9839; compiler error &#35;1575:</para><para>A stackalloc expression requires [] after type</para></summary>
        public static ICompilerReferenceError CS1575
        {
            get
            {
                if (_CS1575 == null)
                    _CS1575 = new CompilerReferenceError(Resources.CSharpErrors_CS1575, 1575);
                return _CS1575;
            }
        }
        private static ICompilerReferenceError _CS1575;

        /// <summary><para>C&#9839; compiler error &#35;1576:</para><para>The line number specified for #line directive is missing or invalid</para></summary>
        public static ICompilerReferenceError CS1576
        {
            get
            {
                if (_CS1576 == null)
                    _CS1576 = new CompilerReferenceError(Resources.CSharpErrors_CS1576, 1576);
                return _CS1576;
            }
        }
        private static ICompilerReferenceError _CS1576;

        /// <summary><para>C&#9839; compiler error &#35;1577:</para><para>Assembly generation failed — reason</para></summary>
        public static ICompilerReferenceError CS1577
        {
            get
            {
                if (_CS1577 == null)
                    _CS1577 = new CompilerReferenceError(Resources.CSharpErrors_CS1577, 1577);
                return _CS1577;
            }
        }
        private static ICompilerReferenceError _CS1577;

        /// <summary><para>C&#9839; compiler error &#35;1578:</para><para>Filename, single-line comment or end-of-line expected</para></summary>
        public static ICompilerReferenceError CS1578
        {
            get
            {
                if (_CS1578 == null)
                    _CS1578 = new CompilerReferenceError(Resources.CSharpErrors_CS1578, 1578);
                return _CS1578;
            }
        }
        private static ICompilerReferenceError _CS1578;

        /// <summary><para>C&#9839; compiler error &#35;1579:</para><para>foreach statement cannot operate on variables of type 'type1' because 'type2' does not contain a public definition for 'identifier'</para></summary>
        public static ICompilerReferenceError CS1579
        {
            get
            {
                if (_CS1579 == null)
                    _CS1579 = new CompilerReferenceError(Resources.CSharpErrors_CS1579, 1579);
                return _CS1579;
            }
        }
        private static ICompilerReferenceError _CS1579;

        /// <summary><para>C&#9839; compiler error &#35;1583:</para><para>'file' is not a valid Win32 resource file</para></summary>
        public static ICompilerReferenceError CS1583
        {
            get
            {
                if (_CS1583 == null)
                    _CS1583 = new CompilerReferenceError(Resources.CSharpErrors_CS1583, 1583);
                return _CS1583;
            }
        }
        private static ICompilerReferenceError _CS1583;

        /// <summary><para>C&#9839; compiler error &#35;1585:</para><para>Member modifier 'keyword' must precede the member type and name</para></summary>
        public static ICompilerReferenceError CS1585
        {
            get
            {
                if (_CS1585 == null)
                    _CS1585 = new CompilerReferenceError(Resources.CSharpErrors_CS1585, 1585);
                return _CS1585;
            }
        }
        private static ICompilerReferenceError _CS1585;

        /// <summary><para>C&#9839; compiler error &#35;1586:</para><para>Array creation must have array size or array initializer</para></summary>
        public static ICompilerReferenceError CS1586
        {
            get
            {
                if (_CS1586 == null)
                    _CS1586 = new CompilerReferenceError(Resources.CSharpErrors_CS1586, 1586);
                return _CS1586;
            }
        }
        private static ICompilerReferenceError _CS1586;

        /// <summary><para>C&#9839; compiler error &#35;1588:</para><para>Cannot determine common language runtime directory -- 'reason'</para></summary>
        public static ICompilerReferenceError CS1588
        {
            get
            {
                if (_CS1588 == null)
                    _CS1588 = new CompilerReferenceError(Resources.CSharpErrors_CS1588, 1588);
                return _CS1588;
            }
        }
        private static ICompilerReferenceError _CS1588;

        /// <summary><para>C&#9839; compiler error &#35;1593:</para><para>Delegate 'del' does not take 'number' arguments</para></summary>
        public static ICompilerReferenceError CS1593
        {
            get
            {
                if (_CS1593 == null)
                    _CS1593 = new CompilerReferenceError(Resources.CSharpErrors_CS1593, 1593);
                return _CS1593;
            }
        }
        private static ICompilerReferenceError _CS1593;

        /// <summary><para>C&#9839; compiler error &#35;1594:</para><para>Delegate 'delegate' has some invalid arguments</para></summary>
        public static ICompilerReferenceError CS1594
        {
            get
            {
                if (_CS1594 == null)
                    _CS1594 = new CompilerReferenceError(Resources.CSharpErrors_CS1594, 1594);
                return _CS1594;
            }
        }
        private static ICompilerReferenceError _CS1594;

        /// <summary><para>C&#9839; compiler error &#35;1597:</para><para>Semicolon after method or accessor block is not valid</para></summary>
        public static ICompilerReferenceError CS1597
        {
            get
            {
                if (_CS1597 == null)
                    _CS1597 = new CompilerReferenceError(Resources.CSharpErrors_CS1597, 1597);
                return _CS1597;
            }
        }
        private static ICompilerReferenceError _CS1597;

        /// <summary><para>C&#9839; compiler error &#35;1599:</para><para>Method or delegate cannot return type 'type'</para></summary>
        public static ICompilerReferenceError CS1599
        {
            get
            {
                if (_CS1599 == null)
                    _CS1599 = new CompilerReferenceError(Resources.CSharpErrors_CS1599, 1599);
                return _CS1599;
            }
        }
        private static ICompilerReferenceError _CS1599;

        /// <summary><para>C&#9839; compiler error &#35;1600:</para><para>Compilation cancelled by user</para></summary>
        public static ICompilerReferenceError CS1600
        {
            get
            {
                if (_CS1600 == null)
                    _CS1600 = new CompilerReferenceError(Resources.CSharpErrors_CS1600, 1600);
                return _CS1600;
            }
        }
        private static ICompilerReferenceError _CS1600;

        /// <summary><para>C&#9839; compiler error &#35;1601:</para><para>Method or delegate parameter cannot be of type 'type'</para></summary>
        public static ICompilerReferenceError CS1601
        {
            get
            {
                if (_CS1601 == null)
                    _CS1601 = new CompilerReferenceError(Resources.CSharpErrors_CS1601, 1601);
                return _CS1601;
            }
        }
        private static ICompilerReferenceError _CS1601;

        /// <summary><para>C&#9839; compiler error &#35;1604:</para><para>Cannot assign to 'variable' because it is read-only</para></summary>
        public static ICompilerReferenceError CS1604
        {
            get
            {
                if (_CS1604 == null)
                    _CS1604 = new CompilerReferenceError(Resources.CSharpErrors_CS1604, 1604);
                return _CS1604;
            }
        }
        private static ICompilerReferenceError _CS1604;

        /// <summary><para>C&#9839; compiler error &#35;1605:</para><para>Cannot pass 'var' as a ref or out argument because it is read-only</para></summary>
        public static ICompilerReferenceError CS1605
        {
            get
            {
                if (_CS1605 == null)
                    _CS1605 = new CompilerReferenceError(Resources.CSharpErrors_CS1605, 1605);
                return _CS1605;
            }
        }
        private static ICompilerReferenceError _CS1605;

        /// <summary><para>C&#9839; compiler error &#35;1606:</para><para>Assembly signing failed; output may not be signed -- reason</para></summary>
        public static ICompilerReferenceError CS1606
        {
            get
            {
                if (_CS1606 == null)
                    _CS1606 = new CompilerReferenceError(Resources.CSharpErrors_CS1606, 1606);
                return _CS1606;
            }
        }
        private static ICompilerReferenceError _CS1606;

        /// <summary><para>C&#9839; compiler error &#35;1608:</para><para>The Required attribute is not permitted on C# types</para></summary>
        public static ICompilerReferenceError CS1608
        {
            get
            {
                if (_CS1608 == null)
                    _CS1608 = new CompilerReferenceError(Resources.CSharpErrors_CS1608, 1608);
                return _CS1608;
            }
        }
        private static ICompilerReferenceError _CS1608;

        /// <summary><para>C&#9839; compiler error &#35;1609:</para><para>Modifiers cannot be placed on event accessor declarations</para></summary>
        public static ICompilerReferenceError CS1609
        {
            get
            {
                if (_CS1609 == null)
                    _CS1609 = new CompilerReferenceError(Resources.CSharpErrors_CS1609, 1609);
                return _CS1609;
            }
        }
        private static ICompilerReferenceError _CS1609;

        /// <summary><para>C&#9839; compiler error &#35;1611:</para><para>The params parameter cannot be declared as ref or out</para></summary>
        public static ICompilerReferenceError CS1611
        {
            get
            {
                if (_CS1611 == null)
                    _CS1611 = new CompilerReferenceError(Resources.CSharpErrors_CS1611, 1611);
                return _CS1611;
            }
        }
        private static ICompilerReferenceError _CS1611;

        /// <summary><para>C&#9839; compiler error &#35;1612:</para><para>Cannot modify the return value of 'expression' because it is not a variable</para></summary>
        public static ICompilerReferenceError CS1612
        {
            get
            {
                if (_CS1612 == null)
                    _CS1612 = new CompilerReferenceError(Resources.CSharpErrors_CS1612, 1612);
                return _CS1612;
            }
        }
        private static ICompilerReferenceError _CS1612;

        /// <summary><para>C&#9839; compiler error &#35;1613:</para><para>The managed coclass wrapper class 'class' for interface 'interface' cannot be found (are you missing an assembly reference?)</para></summary>
        public static ICompilerReferenceError CS1613
        {
            get
            {
                if (_CS1613 == null)
                    _CS1613 = new CompilerReferenceError(Resources.CSharpErrors_CS1613, 1613);
                return _CS1613;
            }
        }
        private static ICompilerReferenceError _CS1613;

        /// <summary><para>C&#9839; compiler error &#35;1614:</para><para>'name' is ambiguous; between 'attribute1' and 'attribute2'. use either '@attribute' or 'attributeAttribute'</para></summary>
        public static ICompilerReferenceError CS1614
        {
            get
            {
                if (_CS1614 == null)
                    _CS1614 = new CompilerReferenceError(Resources.CSharpErrors_CS1614, 1614);
                return _CS1614;
            }
        }
        private static ICompilerReferenceError _CS1614;

        /// <summary><para>C&#9839; compiler error &#35;1615:</para><para>Argument 'number' should not be passed with the 'keyword' keyword</para></summary>
        public static ICompilerReferenceError CS1615
        {
            get
            {
                if (_CS1615 == null)
                    _CS1615 = new CompilerReferenceError(Resources.CSharpErrors_CS1615, 1615);
                return _CS1615;
            }
        }
        private static ICompilerReferenceError _CS1615;

        /// <summary><para>C&#9839; compiler error &#35;1617:</para><para>Invalid option 'option' for /langversion; must be ISO-1, ISO-2 or Default</para></summary>
        public static ICompilerReferenceError CS1617
        {
            get
            {
                if (_CS1617 == null)
                    _CS1617 = new CompilerReferenceError(Resources.CSharpErrors_CS1617, 1617);
                return _CS1617;
            }
        }
        private static ICompilerReferenceError _CS1617;

        /// <summary><para>C&#9839; compiler error &#35;1618:</para><para>Cannot create delegate with 'method' because it has a Conditional attribute</para></summary>
        public static ICompilerReferenceError CS1618
        {
            get
            {
                if (_CS1618 == null)
                    _CS1618 = new CompilerReferenceError(Resources.CSharpErrors_CS1618, 1618);
                return _CS1618;
            }
        }
        private static ICompilerReferenceError _CS1618;

        /// <summary><para>C&#9839; compiler error &#35;1619:</para><para>Cannot create temporary file 'filename' -- reason</para></summary>
        public static ICompilerReferenceError CS1619
        {
            get
            {
                if (_CS1619 == null)
                    _CS1619 = new CompilerReferenceError(Resources.CSharpErrors_CS1619, 1619);
                return _CS1619;
            }
        }
        private static ICompilerReferenceError _CS1619;

        /// <summary><para>C&#9839; compiler error &#35;1620:</para><para>Argument 'number' must be passed with the 'keyword' keyword</para></summary>
        public static ICompilerReferenceError CS1620
        {
            get
            {
                if (_CS1620 == null)
                    _CS1620 = new CompilerReferenceError(Resources.CSharpErrors_CS1620, 1620);
                return _CS1620;
            }
        }
        private static ICompilerReferenceError _CS1620;

        /// <summary><para>C&#9839; compiler error &#35;1621:</para><para>The yield statement cannot be used inside an anonymous method or lambda expression</para></summary>
        public static ICompilerReferenceError CS1621
        {
            get
            {
                if (_CS1621 == null)
                    _CS1621 = new CompilerReferenceError(Resources.CSharpErrors_CS1621, 1621);
                return _CS1621;
            }
        }
        private static ICompilerReferenceError _CS1621;

        /// <summary><para>C&#9839; compiler error &#35;1622:</para><para>Cannot return a value from an iterator. Use the yield return statement to return a value, or yield break to end the iteration.</para></summary>
        public static ICompilerReferenceError CS1622
        {
            get
            {
                if (_CS1622 == null)
                    _CS1622 = new CompilerReferenceError(Resources.CSharpErrors_CS1622, 1622);
                return _CS1622;
            }
        }
        private static ICompilerReferenceError _CS1622;

        /// <summary><para>C&#9839; compiler error &#35;1623:</para><para>Iterators cannot have ref or out parameters</para></summary>
        public static ICompilerReferenceError CS1623
        {
            get
            {
                if (_CS1623 == null)
                    _CS1623 = new CompilerReferenceError(Resources.CSharpErrors_CS1623, 1623);
                return _CS1623;
            }
        }
        private static ICompilerReferenceError _CS1623;

        /// <summary><para>C&#9839; compiler error &#35;1624:</para><para>The body of 'accessor' cannot be an iterator block because 'type' is not an iterator interface type</para></summary>
        public static ICompilerReferenceError CS1624
        {
            get
            {
                if (_CS1624 == null)
                    _CS1624 = new CompilerReferenceError(Resources.CSharpErrors_CS1624, 1624);
                return _CS1624;
            }
        }
        private static ICompilerReferenceError _CS1624;

        /// <summary><para>C&#9839; compiler error &#35;1625:</para><para>Cannot yield in the body of a finally clause</para></summary>
        public static ICompilerReferenceError CS1625
        {
            get
            {
                if (_CS1625 == null)
                    _CS1625 = new CompilerReferenceError(Resources.CSharpErrors_CS1625, 1625);
                return _CS1625;
            }
        }
        private static ICompilerReferenceError _CS1625;

        /// <summary><para>C&#9839; compiler error &#35;1626:</para><para>Cannot yield a value in the body of a try block with a catch clause</para></summary>
        public static ICompilerReferenceError CS1626
        {
            get
            {
                if (_CS1626 == null)
                    _CS1626 = new CompilerReferenceError(Resources.CSharpErrors_CS1626, 1626);
                return _CS1626;
            }
        }
        private static ICompilerReferenceError _CS1626;

        /// <summary><para>C&#9839; compiler error &#35;1627:</para><para>Expression expected after yield return</para></summary>
        public static ICompilerReferenceError CS1627
        {
            get
            {
                if (_CS1627 == null)
                    _CS1627 = new CompilerReferenceError(Resources.CSharpErrors_CS1627, 1627);
                return _CS1627;
            }
        }
        private static ICompilerReferenceError _CS1627;

        /// <summary><para>C&#9839; compiler error &#35;1628:</para><para>Cannot use ref or out parameter 'parameter' inside an anonymous method, lambda expression, or query expression</para></summary>
        public static ICompilerReferenceError CS1628
        {
            get
            {
                if (_CS1628 == null)
                    _CS1628 = new CompilerReferenceError(Resources.CSharpErrors_CS1628, 1628);
                return _CS1628;
            }
        }
        private static ICompilerReferenceError _CS1628;

        /// <summary><para>C&#9839; compiler error &#35;1629:</para><para>Unsafe code may not appear in iterators</para></summary>
        public static ICompilerReferenceError CS1629
        {
            get
            {
                if (_CS1629 == null)
                    _CS1629 = new CompilerReferenceError(Resources.CSharpErrors_CS1629, 1629);
                return _CS1629;
            }
        }
        private static ICompilerReferenceError _CS1629;

        /// <summary><para>C&#9839; compiler error &#35;1630:</para><para>Invalid option 'option' for /errorreport; must be prompt, send, queue, or none</para></summary>
        public static ICompilerReferenceError CS1630
        {
            get
            {
                if (_CS1630 == null)
                    _CS1630 = new CompilerReferenceError(Resources.CSharpErrors_CS1630, 1630);
                return _CS1630;
            }
        }
        private static ICompilerReferenceError _CS1630;

        /// <summary><para>C&#9839; compiler error &#35;1631:</para><para>Cannot yield a value in the body of a catch clause</para></summary>
        public static ICompilerReferenceError CS1631
        {
            get
            {
                if (_CS1631 == null)
                    _CS1631 = new CompilerReferenceError(Resources.CSharpErrors_CS1631, 1631);
                return _CS1631;
            }
        }
        private static ICompilerReferenceError _CS1631;

        /// <summary><para>C&#9839; compiler error &#35;1632:</para><para>Control cannot leave the body of an anonymous method or lambda expression</para></summary>
        public static ICompilerReferenceError CS1632
        {
            get
            {
                if (_CS1632 == null)
                    _CS1632 = new CompilerReferenceError(Resources.CSharpErrors_CS1632, 1632);
                return _CS1632;
            }
        }
        private static ICompilerReferenceError _CS1632;

        /// <summary><para>C&#9839; compiler error &#35;1637:</para><para>Iterators cannot have unsafe parameters or yield types</para></summary>
        public static ICompilerReferenceError CS1637
        {
            get
            {
                if (_CS1637 == null)
                    _CS1637 = new CompilerReferenceError(Resources.CSharpErrors_CS1637, 1637);
                return _CS1637;
            }
        }
        private static ICompilerReferenceError _CS1637;

        /// <summary><para>C&#9839; compiler error &#35;1638:</para><para>'identifier' is a reserved identifier and cannot be used when ISO language version mode is used</para></summary>
        public static ICompilerReferenceError CS1638
        {
            get
            {
                if (_CS1638 == null)
                    _CS1638 = new CompilerReferenceError(Resources.CSharpErrors_CS1638, 1638);
                return _CS1638;
            }
        }
        private static ICompilerReferenceError _CS1638;

        /// <summary><para>C&#9839; compiler error &#35;1639:</para><para>The managed coclass wrapper class signature 'signature' for interface 'interface' is not a valid class name signature</para></summary>
        public static ICompilerReferenceError CS1639
        {
            get
            {
                if (_CS1639 == null)
                    _CS1639 = new CompilerReferenceError(Resources.CSharpErrors_CS1639, 1639);
                return _CS1639;
            }
        }
        private static ICompilerReferenceError _CS1639;

        /// <summary><para>C&#9839; compiler error &#35;1640:</para><para>foreach statement cannot operate on variables of type 'type' because it implements multiple instantiations of 'interface', try casting to a specific interface instantiation</para></summary>
        public static ICompilerReferenceError CS1640
        {
            get
            {
                if (_CS1640 == null)
                    _CS1640 = new CompilerReferenceError(Resources.CSharpErrors_CS1640, 1640);
                return _CS1640;
            }
        }
        private static ICompilerReferenceError _CS1640;

        /// <summary><para>C&#9839; compiler error &#35;1641:</para><para>A fixed size buffer field must have the array size specifier after the field name</para></summary>
        public static ICompilerReferenceError CS1641
        {
            get
            {
                if (_CS1641 == null)
                    _CS1641 = new CompilerReferenceError(Resources.CSharpErrors_CS1641, 1641);
                return _CS1641;
            }
        }
        private static ICompilerReferenceError _CS1641;

        /// <summary><para>C&#9839; compiler error &#35;1642:</para><para>Fixed size buffer fields may only be members of structs.</para></summary>
        public static ICompilerReferenceError CS1642
        {
            get
            {
                if (_CS1642 == null)
                    _CS1642 = new CompilerReferenceError(Resources.CSharpErrors_CS1642, 1642);
                return _CS1642;
            }
        }
        private static ICompilerReferenceError _CS1642;

        /// <summary><para>C&#9839; compiler error &#35;1643:</para><para>Not all code paths return a value in method of type 'type!'</para></summary>
        public static ICompilerReferenceError CS1643
        {
            get
            {
                if (_CS1643 == null)
                    _CS1643 = new CompilerReferenceError(Resources.CSharpErrors_CS1643, 1643);
                return _CS1643;
            }
        }
        private static ICompilerReferenceError _CS1643;

        /// <summary><para>C&#9839; compiler error &#35;1644:</para><para>Feature 'feature' is not part of the standardized ISO C# language specification, and may not be accepted by other compilers</para></summary>
        public static ICompilerReferenceError CS1644
        {
            get
            {
                if (_CS1644 == null)
                    _CS1644 = new CompilerReferenceError(Resources.CSharpErrors_CS1644, 1644);
                return _CS1644;
            }
        }
        private static ICompilerReferenceError _CS1644;

        /// <summary><para>C&#9839; compiler error &#35;1646:</para><para>Keyword, identifier, or string expected after verbatim specifier: @</para></summary>
        public static ICompilerReferenceError CS1646
        {
            get
            {
                if (_CS1646 == null)
                    _CS1646 = new CompilerReferenceError(Resources.CSharpErrors_CS1646, 1646);
                return _CS1646;
            }
        }
        private static ICompilerReferenceError _CS1646;

        /// <summary><para>C&#9839; compiler error &#35;1647:</para><para>An expression is too long or complex to compile near 'code'</para></summary>
        public static ICompilerReferenceError CS1647
        {
            get
            {
                if (_CS1647 == null)
                    _CS1647 = new CompilerReferenceError(Resources.CSharpErrors_CS1647, 1647);
                return _CS1647;
            }
        }
        private static ICompilerReferenceError _CS1647;

        /// <summary><para>C&#9839; compiler error &#35;1648:</para><para>Members of readonly field 'identifier' cannot be modified (except in a constructor or a variable initializer)</para></summary>
        public static ICompilerReferenceError CS1648
        {
            get
            {
                if (_CS1648 == null)
                    _CS1648 = new CompilerReferenceError(Resources.CSharpErrors_CS1648, 1648);
                return _CS1648;
            }
        }
        private static ICompilerReferenceError _CS1648;

        /// <summary><para>C&#9839; compiler error &#35;1649:</para><para>Members of readonly field 'identifier' cannot be passed ref or out (except in a constructor)</para></summary>
        public static ICompilerReferenceError CS1649
        {
            get
            {
                if (_CS1649 == null)
                    _CS1649 = new CompilerReferenceError(Resources.CSharpErrors_CS1649, 1649);
                return _CS1649;
            }
        }
        private static ICompilerReferenceError _CS1649;

        /// <summary><para>C&#9839; compiler error &#35;1650:</para><para>Fields of static readonly field 'identifier' cannot be assigned to (except in a static constructor or a variable initializer)</para></summary>
        public static ICompilerReferenceError CS1650
        {
            get
            {
                if (_CS1650 == null)
                    _CS1650 = new CompilerReferenceError(Resources.CSharpErrors_CS1650, 1650);
                return _CS1650;
            }
        }
        private static ICompilerReferenceError _CS1650;

        /// <summary><para>C&#9839; compiler error &#35;1651:</para><para>Fields of static readonly field 'identifier' cannot be passed ref or out (except in a static constructor)</para></summary>
        public static ICompilerReferenceError CS1651
        {
            get
            {
                if (_CS1651 == null)
                    _CS1651 = new CompilerReferenceError(Resources.CSharpErrors_CS1651, 1651);
                return _CS1651;
            }
        }
        private static ICompilerReferenceError _CS1651;

        /// <summary><para>C&#9839; compiler error &#35;1654:</para><para>Cannot modify members of 'variable' because it is a 'read-only variable type'</para></summary>
        public static ICompilerReferenceError CS1654
        {
            get
            {
                if (_CS1654 == null)
                    _CS1654 = new CompilerReferenceError(Resources.CSharpErrors_CS1654, 1654);
                return _CS1654;
            }
        }
        private static ICompilerReferenceError _CS1654;

        /// <summary><para>C&#9839; compiler error &#35;1655:</para><para>Cannot pass fields of 'variable' as a ref or out argument because it is a 'readonly variable type'</para></summary>
        public static ICompilerReferenceError CS1655
        {
            get
            {
                if (_CS1655 == null)
                    _CS1655 = new CompilerReferenceError(Resources.CSharpErrors_CS1655, 1655);
                return _CS1655;
            }
        }
        private static ICompilerReferenceError _CS1655;

        /// <summary><para>C&#9839; compiler error &#35;1656:</para><para>Cannot assign to 'variable' because it is a 'read-only variable type'</para></summary>
        public static ICompilerReferenceError CS1656
        {
            get
            {
                if (_CS1656 == null)
                    _CS1656 = new CompilerReferenceError(Resources.CSharpErrors_CS1656, 1656);
                return _CS1656;
            }
        }
        private static ICompilerReferenceError _CS1656;

        /// <summary><para>C&#9839; compiler error &#35;1657:</para><para>Cannot pass 'parameter' as a ref or out argument because 'reason''</para></summary>
        public static ICompilerReferenceError CS1657
        {
            get
            {
                if (_CS1657 == null)
                    _CS1657 = new CompilerReferenceError(Resources.CSharpErrors_CS1657, 1657);
                return _CS1657;
            }
        }
        private static ICompilerReferenceError _CS1657;

        /// <summary><para>C&#9839; compiler error &#35;1660:</para><para>Cannot convert anonymous method block to type 'type' because it is not a delegate type</para></summary>
        public static ICompilerReferenceError CS1660
        {
            get
            {
                if (_CS1660 == null)
                    _CS1660 = new CompilerReferenceError(Resources.CSharpErrors_CS1660, 1660);
                return _CS1660;
            }
        }
        private static ICompilerReferenceError _CS1660;

        /// <summary><para>C&#9839; compiler error &#35;1661:</para><para>Cannot convert anonymous method block to delegate type 'delegate type' because the specified block's parameter types do not match the delegate parameter types</para></summary>
        public static ICompilerReferenceError CS1661
        {
            get
            {
                if (_CS1661 == null)
                    _CS1661 = new CompilerReferenceError(Resources.CSharpErrors_CS1661, 1661);
                return _CS1661;
            }
        }
        private static ICompilerReferenceError _CS1661;

        /// <summary><para>C&#9839; compiler error &#35;1662:</para><para>Cannot convert anonymous method block to delegate type 'delegate type' because some of the return types in the block are not implicitly convertible to the delegate return type</para></summary>
        public static ICompilerReferenceError CS1662
        {
            get
            {
                if (_CS1662 == null)
                    _CS1662 = new CompilerReferenceError(Resources.CSharpErrors_CS1662, 1662);
                return _CS1662;
            }
        }
        private static ICompilerReferenceError _CS1662;

        /// <summary><para>C&#9839; compiler error &#35;1663:</para><para>Fixed size buffer type must be one of the following: bool, byte, short, int, long, char, sbyte, ushort, uint, ulong, float or double</para></summary>
        public static ICompilerReferenceError CS1663
        {
            get
            {
                if (_CS1663 == null)
                    _CS1663 = new CompilerReferenceError(Resources.CSharpErrors_CS1663, 1663);
                return _CS1663;
            }
        }
        private static ICompilerReferenceError _CS1663;

        /// <summary><para>C&#9839; compiler error &#35;1664:</para><para>Fixed size buffer of length 'length' and type 'type' is too big</para></summary>
        public static ICompilerReferenceError CS1664
        {
            get
            {
                if (_CS1664 == null)
                    _CS1664 = new CompilerReferenceError(Resources.CSharpErrors_CS1664, 1664);
                return _CS1664;
            }
        }
        private static ICompilerReferenceError _CS1664;

        /// <summary><para>C&#9839; compiler error &#35;1665:</para><para>Fixed size buffers must have a length greater than zero</para></summary>
        public static ICompilerReferenceError CS1665
        {
            get
            {
                if (_CS1665 == null)
                    _CS1665 = new CompilerReferenceError(Resources.CSharpErrors_CS1665, 1665);
                return _CS1665;
            }
        }
        private static ICompilerReferenceError _CS1665;

        /// <summary><para>C&#9839; compiler error &#35;1666:</para><para>You cannot use fixed size buffers contained in unfixed expressions. Try using the fixed statement.</para></summary>
        public static ICompilerReferenceError CS1666
        {
            get
            {
                if (_CS1666 == null)
                    _CS1666 = new CompilerReferenceError(Resources.CSharpErrors_CS1666, 1666);
                return _CS1666;
            }
        }
        private static ICompilerReferenceError _CS1666;

        /// <summary><para>C&#9839; compiler error &#35;1667:</para><para>Attribute 'attribute' is not valid on property or event accessors. It is valid on 'declaration type' declarations only.</para></summary>
        public static ICompilerReferenceError CS1667
        {
            get
            {
                if (_CS1667 == null)
                    _CS1667 = new CompilerReferenceError(Resources.CSharpErrors_CS1667, 1667);
                return _CS1667;
            }
        }
        private static ICompilerReferenceError _CS1667;

        /// <summary><para>C&#9839; compiler error &#35;1670:</para><para>params is not valid in this context</para></summary>
        public static ICompilerReferenceError CS1670
        {
            get
            {
                if (_CS1670 == null)
                    _CS1670 = new CompilerReferenceError(Resources.CSharpErrors_CS1670, 1670);
                return _CS1670;
            }
        }
        private static ICompilerReferenceError _CS1670;

        /// <summary><para>C&#9839; compiler error &#35;1671:</para><para>A namespace declaration cannot have modifiers or attributes</para></summary>
        public static ICompilerReferenceError CS1671
        {
            get
            {
                if (_CS1671 == null)
                    _CS1671 = new CompilerReferenceError(Resources.CSharpErrors_CS1671, 1671);
                return _CS1671;
            }
        }
        private static ICompilerReferenceError _CS1671;

        /// <summary><para>C&#9839; compiler error &#35;1672:</para><para>Invalid option 'option' for /platform; must be anycpu, x86, Itanium or x64</para></summary>
        public static ICompilerReferenceError CS1672
        {
            get
            {
                if (_CS1672 == null)
                    _CS1672 = new CompilerReferenceError(Resources.CSharpErrors_CS1672, 1672);
                return _CS1672;
            }
        }
        private static ICompilerReferenceError _CS1672;

        /// <summary><para>C&#9839; compiler error &#35;1673:</para><para>Anonymous methods, lambda expressions, and query expressions inside structs cannot access instance members of 'this'. Consider copying 'this' to a local variable outside the anonymous method, lambda expression or query expression and using the local instead.</para></summary>
        public static ICompilerReferenceError CS1673
        {
            get
            {
                if (_CS1673 == null)
                    _CS1673 = new CompilerReferenceError(Resources.CSharpErrors_CS1673, 1673);
                return _CS1673;
            }
        }
        private static ICompilerReferenceError _CS1673;

        /// <summary><para>C&#9839; compiler error &#35;1674:</para><para>'T': type used in a using statement must be implicitly convertible to 'System.IDisposable'</para></summary>
        public static ICompilerReferenceError CS1674
        {
            get
            {
                if (_CS1674 == null)
                    _CS1674 = new CompilerReferenceError(Resources.CSharpErrors_CS1674, 1674);
                return _CS1674;
            }
        }
        private static ICompilerReferenceError _CS1674;

        /// <summary><para>C&#9839; compiler error &#35;1675:</para><para>Enums cannot have type parameters</para></summary>
        public static ICompilerReferenceError CS1675
        {
            get
            {
                if (_CS1675 == null)
                    _CS1675 = new CompilerReferenceError(Resources.CSharpErrors_CS1675, 1675);
                return _CS1675;
            }
        }
        private static ICompilerReferenceError _CS1675;

        /// <summary><para>C&#9839; compiler error &#35;1676:</para><para>Parameter 'number' must be declared with the 'keyword' keyword</para></summary>
        public static ICompilerReferenceError CS1676
        {
            get
            {
                if (_CS1676 == null)
                    _CS1676 = new CompilerReferenceError(Resources.CSharpErrors_CS1676, 1676);
                return _CS1676;
            }
        }
        private static ICompilerReferenceError _CS1676;

        /// <summary><para>C&#9839; compiler error &#35;1677:</para><para>Parameter 'number' should not be declared with the 'keyword' keyword</para></summary>
        public static ICompilerReferenceError CS1677
        {
            get
            {
                if (_CS1677 == null)
                    _CS1677 = new CompilerReferenceError(Resources.CSharpErrors_CS1677, 1677);
                return _CS1677;
            }
        }
        private static ICompilerReferenceError _CS1677;

        /// <summary><para>C&#9839; compiler error &#35;1678:</para><para>Parameter 'number' is declared as type 'type1' but should be 'type2'</para></summary>
        public static ICompilerReferenceError CS1678
        {
            get
            {
                if (_CS1678 == null)
                    _CS1678 = new CompilerReferenceError(Resources.CSharpErrors_CS1678, 1678);
                return _CS1678;
            }
        }
        private static ICompilerReferenceError _CS1678;

        /// <summary><para>C&#9839; compiler error &#35;1679:</para><para>Invalid extern alias for '/reference'; 'identifier' is not a valid identifier</para></summary>
        public static ICompilerReferenceError CS1679
        {
            get
            {
                if (_CS1679 == null)
                    _CS1679 = new CompilerReferenceError(Resources.CSharpErrors_CS1679, 1679);
                return _CS1679;
            }
        }
        private static ICompilerReferenceError _CS1679;

        /// <summary><para>C&#9839; compiler error &#35;1680:</para><para>Invalid reference alias option: 'alias=' -- missing filename.</para></summary>
        public static ICompilerReferenceError CS1680
        {
            get
            {
                if (_CS1680 == null)
                    _CS1680 = new CompilerReferenceError(Resources.CSharpErrors_CS1680, 1680);
                return _CS1680;
            }
        }
        private static ICompilerReferenceError _CS1680;

        /// <summary><para>C&#9839; compiler error &#35;1681:</para><para>You cannot redefine the global extern alias</para></summary>
        public static ICompilerReferenceError CS1681
        {
            get
            {
                if (_CS1681 == null)
                    _CS1681 = new CompilerReferenceError(Resources.CSharpErrors_CS1681, 1681);
                return _CS1681;
            }
        }
        private static ICompilerReferenceError _CS1681;

        /// <summary><para>C&#9839; compiler error &#35;1686:</para><para>Local 'variable' or its members cannot have their address taken and be used inside an anonymous method or lambda expression</para></summary>
        public static ICompilerReferenceError CS1686
        {
            get
            {
                if (_CS1686 == null)
                    _CS1686 = new CompilerReferenceError(Resources.CSharpErrors_CS1686, 1686);
                return _CS1686;
            }
        }
        private static ICompilerReferenceError _CS1686;

        /// <summary><para>C&#9839; compiler error &#35;1688:</para><para>Cannot convert anonymous method block without a parameter list to delegate type 'delegate' because it has one or more out parameters</para></summary>
        public static ICompilerReferenceError CS1688
        {
            get
            {
                if (_CS1688 == null)
                    _CS1688 = new CompilerReferenceError(Resources.CSharpErrors_CS1688, 1688);
                return _CS1688;
            }
        }
        private static ICompilerReferenceError _CS1688;

        /// <summary><para>C&#9839; compiler error &#35;1689:</para><para>Attribute 'Attribute Name' is only valid on methods or attribute classes</para></summary>
        public static ICompilerReferenceError CS1689
        {
            get
            {
                if (_CS1689 == null)
                    _CS1689 = new CompilerReferenceError(Resources.CSharpErrors_CS1689, 1689);
                return _CS1689;
            }
        }
        private static ICompilerReferenceError _CS1689;

        /// <summary><para>C&#9839; compiler error &#35;1703:</para><para>An assembly with the same simple name 'name' has already been imported. Try removing one of the references or sign them to enable side-by-side.</para></summary>
        public static ICompilerReferenceError CS1703
        {
            get
            {
                if (_CS1703 == null)
                    _CS1703 = new CompilerReferenceError(Resources.CSharpErrors_CS1703, 1703);
                return _CS1703;
            }
        }
        private static ICompilerReferenceError _CS1703;

        /// <summary><para>C&#9839; compiler error &#35;1704:</para><para>An assembly with the same simple name 'Assembly Name' has already been imported. Try removing one of the references or sign them to enable side-by-side.</para></summary>
        public static ICompilerReferenceError CS1704
        {
            get
            {
                if (_CS1704 == null)
                    _CS1704 = new CompilerReferenceError(Resources.CSharpErrors_CS1704, 1704);
                return _CS1704;
            }
        }
        private static ICompilerReferenceError _CS1704;

        /// <summary><para>C&#9839; compiler error &#35;1705:</para><para>Assembly 'AssemblyName1' uses 'TypeName' which has a higher version than referenced assembly 'AssemblyName2'</para></summary>
        public static ICompilerReferenceError CS1705
        {
            get
            {
                if (_CS1705 == null)
                    _CS1705 = new CompilerReferenceError(Resources.CSharpErrors_CS1705, 1705);
                return _CS1705;
            }
        }
        private static ICompilerReferenceError _CS1705;

        /// <summary><para>C&#9839; compiler error &#35;1706:</para><para>Expression cannot contain anonymous methods or lambda expressions</para></summary>
        public static ICompilerReferenceError CS1706
        {
            get
            {
                if (_CS1706 == null)
                    _CS1706 = new CompilerReferenceError(Resources.CSharpErrors_CS1706, 1706);
                return _CS1706;
            }
        }
        private static ICompilerReferenceError _CS1706;

        /// <summary><para>C&#9839; compiler error &#35;1708:</para><para>Fixed size buffers can only be accessed through locals or fields</para></summary>
        public static ICompilerReferenceError CS1708
        {
            get
            {
                if (_CS1708 == null)
                    _CS1708 = new CompilerReferenceError(Resources.CSharpErrors_CS1708, 1708);
                return _CS1708;
            }
        }
        private static ICompilerReferenceError _CS1708;

        /// <summary><para>C&#9839; compiler error &#35;1713:</para><para>Unexpected error building metadata name for type Typename1—'Reason'</para></summary>
        public static ICompilerReferenceError CS1713
        {
            get
            {
                if (_CS1713 == null)
                    _CS1713 = new CompilerReferenceError(Resources.CSharpErrors_CS1713, 1713);
                return _CS1713;
            }
        }
        private static ICompilerReferenceError _CS1713;

        /// <summary><para>C&#9839; compiler error &#35;1714:</para><para>The base class or interface of TypeName1 could not be resolved or is invalid</para></summary>
        public static ICompilerReferenceError CS1714
        {
            get
            {
                if (_CS1714 == null)
                    _CS1714 = new CompilerReferenceError(Resources.CSharpErrors_CS1714, 1714);
                return _CS1714;
            }
        }
        private static ICompilerReferenceError _CS1714;

        /// <summary><para>C&#9839; compiler error &#35;1715:</para><para>'Type1': type must be 'Type2' to match overridden member 'MemberName'</para></summary>
        public static ICompilerReferenceError CS1715
        {
            get
            {
                if (_CS1715 == null)
                    _CS1715 = new CompilerReferenceError(Resources.CSharpErrors_CS1715, 1715);
                return _CS1715;
            }
        }
        private static ICompilerReferenceError _CS1715;

        /// <summary><para>C&#9839; compiler error &#35;1716:</para><para>Do not use 'System.Runtime.CompilerServices.FixedBuffer' attribute. Use the 'fixed' field modifier instead.</para></summary>
        public static ICompilerReferenceError CS1716
        {
            get
            {
                if (_CS1716 == null)
                    _CS1716 = new CompilerReferenceError(Resources.CSharpErrors_CS1716, 1716);
                return _CS1716;
            }
        }
        private static ICompilerReferenceError _CS1716;

        /// <summary><para>C&#9839; compiler error &#35;1719:</para><para>Error reading Win32 resource file 'File Name' -- 'reason'</para></summary>
        public static ICompilerReferenceError CS1719
        {
            get
            {
                if (_CS1719 == null)
                    _CS1719 = new CompilerReferenceError(Resources.CSharpErrors_CS1719, 1719);
                return _CS1719;
            }
        }
        private static ICompilerReferenceError _CS1719;

        /// <summary><para>C&#9839; compiler error &#35;1721:</para><para>Class 'class' cannot have multiple base classes: 'class_1' and 'class_2'</para></summary>
        public static ICompilerReferenceError CS1721
        {
            get
            {
                if (_CS1721 == null)
                    _CS1721 = new CompilerReferenceError(Resources.CSharpErrors_CS1721, 1721);
                return _CS1721;
            }
        }
        private static ICompilerReferenceError _CS1721;

        /// <summary><para>C&#9839; compiler error &#35;1722:</para><para>Base class 'class' must come before any interfaces</para></summary>
        public static ICompilerReferenceError CS1722
        {
            get
            {
                if (_CS1722 == null)
                    _CS1722 = new CompilerReferenceError(Resources.CSharpErrors_CS1722, 1722);
                return _CS1722;
            }
        }
        private static ICompilerReferenceError _CS1722;

        /// <summary><para>C&#9839; compiler error &#35;1724:</para><para>Value specified for the argument to 'System.Runtime.InteropServices.DefaultCharSetAttribute' is not valid</para></summary>
        public static ICompilerReferenceError CS1724
        {
            get
            {
                if (_CS1724 == null)
                    _CS1724 = new CompilerReferenceError(Resources.CSharpErrors_CS1724, 1724);
                return _CS1724;
            }
        }
        private static ICompilerReferenceError _CS1724;

        /// <summary><para>C&#9839; compiler error &#35;1725:</para><para>Friend assembly reference 'reference' is invalid. InternalsVisibleTo declarations cannot have a version, culture, public key token, or processor architecture specified.</para></summary>
        public static ICompilerReferenceError CS1725
        {
            get
            {
                if (_CS1725 == null)
                    _CS1725 = new CompilerReferenceError(Resources.CSharpErrors_CS1725, 1725);
                return _CS1725;
            }
        }
        private static ICompilerReferenceError _CS1725;

        /// <summary><para>C&#9839; compiler error &#35;1726:</para><para>Friend assembly reference 'reference' is invalid. Strong-name signed assemblies must specify a public key in their InternalsVisibleTo declarations.</para></summary>
        public static ICompilerReferenceError CS1726
        {
            get
            {
                if (_CS1726 == null)
                    _CS1726 = new CompilerReferenceError(Resources.CSharpErrors_CS1726, 1726);
                return _CS1726;
            }
        }
        private static ICompilerReferenceError _CS1726;

        /// <summary><para>C&#9839; compiler error &#35;1727:</para><para>Cannot send error report automatically without authorization. Please visit '' to authorize sending error report.</para></summary>
        public static ICompilerReferenceError CS1727
        {
            get
            {
                if (_CS1727 == null)
                    _CS1727 = new CompilerReferenceError(Resources.CSharpErrors_CS1727, 1727);
                return _CS1727;
            }
        }
        private static ICompilerReferenceError _CS1727;

        /// <summary><para>C&#9839; compiler error &#35;1728:</para><para>Cannot bind delegate to 'member' because it is a member of 'type'</para></summary>
        public static ICompilerReferenceError CS1728
        {
            get
            {
                if (_CS1728 == null)
                    _CS1728 = new CompilerReferenceError(Resources.CSharpErrors_CS1728, 1728);
                return _CS1728;
            }
        }
        private static ICompilerReferenceError _CS1728;

        /// <summary><para>C&#9839; compiler error &#35;1729:</para><para>'type' does not contain a constructor that takes 'number' arguments.</para></summary>
        public static ICompilerReferenceError CS1729
        {
            get
            {
                if (_CS1729 == null)
                    _CS1729 = new CompilerReferenceError(Resources.CSharpErrors_CS1729, 1729);
                return _CS1729;
            }
        }
        private static ICompilerReferenceError _CS1729;

        /// <summary><para>C&#9839; compiler error &#35;1730:</para><para>Assembly and module attributes must precede all other elements defined in a file except using clauses and extern alias declarations.</para></summary>
        public static ICompilerReferenceError CS1730
        {
            get
            {
                if (_CS1730 == null)
                    _CS1730 = new CompilerReferenceError(Resources.CSharpErrors_CS1730, 1730);
                return _CS1730;
            }
        }
        private static ICompilerReferenceError _CS1730;

        /// <summary><para>C&#9839; compiler error &#35;1731:</para><para>Cannot convert 'expression' to delegate because some of the return types in the block are not implicitly convertible to the delegate return type.</para></summary>
        public static ICompilerReferenceError CS1731
        {
            get
            {
                if (_CS1731 == null)
                    _CS1731 = new CompilerReferenceError(Resources.CSharpErrors_CS1731, 1731);
                return _CS1731;
            }
        }
        private static ICompilerReferenceError _CS1731;

        /// <summary><para>C&#9839; compiler error &#35;1732:</para><para>Expected parameter.</para></summary>
        public static ICompilerReferenceError CS1732
        {
            get
            {
                if (_CS1732 == null)
                    _CS1732 = new CompilerReferenceError(Resources.CSharpErrors_CS1732, 1732);
                return _CS1732;
            }
        }
        private static ICompilerReferenceError _CS1732;

        /// <summary><para>C&#9839; compiler error &#35;1733:</para><para>Expected expression.</para></summary>
        public static ICompilerReferenceError CS1733
        {
            get
            {
                if (_CS1733 == null)
                    _CS1733 = new CompilerReferenceError(Resources.CSharpErrors_CS1733, 1733);
                return _CS1733;
            }
        }
        private static ICompilerReferenceError _CS1733;

        /// <summary><para>C&#9839; compiler error &#35;1900:</para><para>Warning level must be in the range 0-4</para></summary>
        public static ICompilerReferenceError CS1900
        {
            get
            {
                if (_CS1900 == null)
                    _CS1900 = new CompilerReferenceError(Resources.CSharpErrors_CS1900, 1900);
                return _CS1900;
            }
        }
        private static ICompilerReferenceError _CS1900;

        /// <summary><para>C&#9839; compiler error &#35;1902:</para><para>Invalid option 'option' for /debug; must be full or pdbonly</para></summary>
        public static ICompilerReferenceError CS1902
        {
            get
            {
                if (_CS1902 == null)
                    _CS1902 = new CompilerReferenceError(Resources.CSharpErrors_CS1902, 1902);
                return _CS1902;
            }
        }
        private static ICompilerReferenceError _CS1902;

        /// <summary><para>C&#9839; compiler error &#35;1906:</para><para>Invalid option 'option'; Resource visibility must be either 'public' or 'private'</para></summary>
        public static ICompilerReferenceError CS1906
        {
            get
            {
                if (_CS1906 == null)
                    _CS1906 = new CompilerReferenceError(Resources.CSharpErrors_CS1906, 1906);
                return _CS1906;
            }
        }
        private static ICompilerReferenceError _CS1906;

        /// <summary><para>C&#9839; compiler error &#35;1908:</para><para>The type of the argument to the DefaultValue attribute must match the parameter type</para></summary>
        public static ICompilerReferenceError CS1908
        {
            get
            {
                if (_CS1908 == null)
                    _CS1908 = new CompilerReferenceError(Resources.CSharpErrors_CS1908, 1908);
                return _CS1908;
            }
        }
        private static ICompilerReferenceError _CS1908;

        /// <summary><para>C&#9839; compiler error &#35;1909:</para><para>The DefaultValue attribute is not applicable on parameters of type 'type'</para></summary>
        public static ICompilerReferenceError CS1909
        {
            get
            {
                if (_CS1909 == null)
                    _CS1909 = new CompilerReferenceError(Resources.CSharpErrors_CS1909, 1909);
                return _CS1909;
            }
        }
        private static ICompilerReferenceError _CS1909;

        /// <summary><para>C&#9839; compiler error &#35;1910:</para><para>Argument of type 'type' is not applicable for the DefaultValue attribute</para></summary>
        public static ICompilerReferenceError CS1910
        {
            get
            {
                if (_CS1910 == null)
                    _CS1910 = new CompilerReferenceError(Resources.CSharpErrors_CS1910, 1910);
                return _CS1910;
            }
        }
        private static ICompilerReferenceError _CS1910;

        /// <summary><para>C&#9839; compiler error &#35;1912:</para><para>Duplicate initialization of member 'name'.</para></summary>
        public static ICompilerReferenceError CS1912
        {
            get
            {
                if (_CS1912 == null)
                    _CS1912 = new CompilerReferenceError(Resources.CSharpErrors_CS1912, 1912);
                return _CS1912;
            }
        }
        private static ICompilerReferenceError _CS1912;

        /// <summary><para>C&#9839; compiler error &#35;1913:</para><para>Member 'name' cannot be initialized. It is not a field or property.</para></summary>
        public static ICompilerReferenceError CS1913
        {
            get
            {
                if (_CS1913 == null)
                    _CS1913 = new CompilerReferenceError(Resources.CSharpErrors_CS1913, 1913);
                return _CS1913;
            }
        }
        private static ICompilerReferenceError _CS1913;

        /// <summary><para>C&#9839; compiler error &#35;1914:</para><para>Static field 'name' cannot be assigned in an object initializer</para></summary>
        public static ICompilerReferenceError CS1914
        {
            get
            {
                if (_CS1914 == null)
                    _CS1914 = new CompilerReferenceError(Resources.CSharpErrors_CS1914, 1914);
                return _CS1914;
            }
        }
        private static ICompilerReferenceError _CS1914;

        /// <summary><para>C&#9839; compiler error &#35;1917:</para><para>Members of read-only field 'name' of type 'struct name' cannot be assigned with an object initializer because it is of a value type.</para></summary>
        public static ICompilerReferenceError CS1917
        {
            get
            {
                if (_CS1917 == null)
                    _CS1917 = new CompilerReferenceError(Resources.CSharpErrors_CS1917, 1917);
                return _CS1917;
            }
        }
        private static ICompilerReferenceError _CS1917;

        /// <summary><para>C&#9839; compiler error &#35;1918:</para><para>Members of property 'name' of type 'type' cannot be assigned with an object initializer because it is of a value type.</para></summary>
        public static ICompilerReferenceError CS1918
        {
            get
            {
                if (_CS1918 == null)
                    _CS1918 = new CompilerReferenceError(Resources.CSharpErrors_CS1918, 1918);
                return _CS1918;
            }
        }
        private static ICompilerReferenceError _CS1918;

        /// <summary><para>C&#9839; compiler error &#35;1919:</para><para>Unsafe type 'type name' cannot be used in object creation.</para></summary>
        public static ICompilerReferenceError CS1919
        {
            get
            {
                if (_CS1919 == null)
                    _CS1919 = new CompilerReferenceError(Resources.CSharpErrors_CS1919, 1919);
                return _CS1919;
            }
        }
        private static ICompilerReferenceError _CS1919;

        /// <summary><para>C&#9839; compiler error &#35;1920:</para><para>Element initializer cannot be empty.</para></summary>
        public static ICompilerReferenceError CS1920
        {
            get
            {
                if (_CS1920 == null)
                    _CS1920 = new CompilerReferenceError(Resources.CSharpErrors_CS1920, 1920);
                return _CS1920;
            }
        }
        private static ICompilerReferenceError _CS1920;

        /// <summary><para>C&#9839; compiler error &#35;1921:</para><para>The best overloaded method match for 'method' has wrong signature for the initializer element. The initializable Add must be an accessible instance method.</para></summary>
        public static ICompilerReferenceError CS1921
        {
            get
            {
                if (_CS1921 == null)
                    _CS1921 = new CompilerReferenceError(Resources.CSharpErrors_CS1921, 1921);
                return _CS1921;
            }
        }
        private static ICompilerReferenceError _CS1921;

        /// <summary><para>C&#9839; compiler error &#35;1922:</para><para>Collection initializer requires its type 'type' to implement System.Collections.IEnumerable.</para></summary>
        public static ICompilerReferenceError CS1922
        {
            get
            {
                if (_CS1922 == null)
                    _CS1922 = new CompilerReferenceError(Resources.CSharpErrors_CS1922, 1922);
                return _CS1922;
            }
        }
        private static ICompilerReferenceError _CS1922;

        /// <summary><para>C&#9839; compiler error &#35;1925:</para><para>Cannot initialize object of type 'type' with a collection initializer.</para></summary>
        public static ICompilerReferenceError CS1925
        {
            get
            {
                if (_CS1925 == null)
                    _CS1925 = new CompilerReferenceError(Resources.CSharpErrors_CS1925, 1925);
                return _CS1925;
            }
        }
        private static ICompilerReferenceError _CS1925;

        /// <summary><para>C&#9839; compiler error &#35;1926:</para><para>Error reading Win32 manifest file 'filename' -- 'error'.</para></summary>
        public static ICompilerReferenceError CS1926
        {
            get
            {
                if (_CS1926 == null)
                    _CS1926 = new CompilerReferenceError(Resources.CSharpErrors_CS1926, 1926);
                return _CS1926;
            }
        }
        private static ICompilerReferenceError _CS1926;

        /// <summary><para>C&#9839; compiler error &#35;1928:</para><para>'Type' does not contain a definition for 'method' and the best extension method overload 'method' has some invalid arguments.</para></summary>
        public static ICompilerReferenceError CS1928
        {
            get
            {
                if (_CS1928 == null)
                    _CS1928 = new CompilerReferenceError(Resources.CSharpErrors_CS1928, 1928);
                return _CS1928;
            }
        }
        private static ICompilerReferenceError _CS1928;

        /// <summary><para>C&#9839; compiler error &#35;1929:</para><para>Instance argument: cannot convert from 'typeA' to 'typeB'.</para></summary>
        public static ICompilerReferenceError CS1929
        {
            get
            {
                if (_CS1929 == null)
                    _CS1929 = new CompilerReferenceError(Resources.CSharpErrors_CS1929, 1929);
                return _CS1929;
            }
        }
        private static ICompilerReferenceError _CS1929;

        /// <summary><para>C&#9839; compiler error &#35;1930:</para><para>The range variable 'name' has already been declared</para></summary>
        public static ICompilerReferenceError CS1930
        {
            get
            {
                if (_CS1930 == null)
                    _CS1930 = new CompilerReferenceError(Resources.CSharpErrors_CS1930, 1930);
                return _CS1930;
            }
        }
        private static ICompilerReferenceError _CS1930;

        /// <summary><para>C&#9839; compiler error &#35;1931:</para><para>The range variable 'variable' conflicts with a previous declaration of 'variable'.</para></summary>
        public static ICompilerReferenceError CS1931
        {
            get
            {
                if (_CS1931 == null)
                    _CS1931 = new CompilerReferenceError(Resources.CSharpErrors_CS1931, 1931);
                return _CS1931;
            }
        }
        private static ICompilerReferenceError _CS1931;

        /// <summary><para>C&#9839; compiler error &#35;1932:</para><para>Cannot assign 'expression' to a range variable.</para></summary>
        public static ICompilerReferenceError CS1932
        {
            get
            {
                if (_CS1932 == null)
                    _CS1932 = new CompilerReferenceError(Resources.CSharpErrors_CS1932, 1932);
                return _CS1932;
            }
        }
        private static ICompilerReferenceError _CS1932;

        /// <summary><para>C&#9839; compiler error &#35;1933:</para><para>Expression cannot contain query expressions</para></summary>
        public static ICompilerReferenceError CS1933
        {
            get
            {
                if (_CS1933 == null)
                    _CS1933 = new CompilerReferenceError(Resources.CSharpErrors_CS1933, 1933);
                return _CS1933;
            }
        }
        private static ICompilerReferenceError _CS1933;

        /// <summary><para>C&#9839; compiler error &#35;1934:</para><para>Could not find an implementation of the query pattern for source type 'type'. 'method' not found. Consider explicitly specifying the type of the range variable 'name'.</para></summary>
        public static ICompilerReferenceError CS1934
        {
            get
            {
                if (_CS1934 == null)
                    _CS1934 = new CompilerReferenceError(Resources.CSharpErrors_CS1934, 1934);
                return _CS1934;
            }
        }
        private static ICompilerReferenceError _CS1934;

        /// <summary><para>C&#9839; compiler error &#35;1935:</para><para>Could not find an implementation of the query pattern for source type 'type'. 'method' not found. Are you missing a reference to 'System.Core.dll' or a using directive for 'System.Linq'?</para></summary>
        public static ICompilerReferenceError CS1935
        {
            get
            {
                if (_CS1935 == null)
                    _CS1935 = new CompilerReferenceError(Resources.CSharpErrors_CS1935, 1935);
                return _CS1935;
            }
        }
        private static ICompilerReferenceError _CS1935;

        /// <summary><para>C&#9839; compiler error &#35;1936:</para><para>Could not find an implementation of the query pattern for source type 'type'. 'method' not found.</para></summary>
        public static ICompilerReferenceError CS1936
        {
            get
            {
                if (_CS1936 == null)
                    _CS1936 = new CompilerReferenceError(Resources.CSharpErrors_CS1936, 1936);
                return _CS1936;
            }
        }
        private static ICompilerReferenceError _CS1936;

        /// <summary><para>C&#9839; compiler error &#35;1937:</para><para>The name 'name' is not in scope on the left side of 'equals'. Consider swapping the expressions on either side of 'equals'.</para></summary>
        public static ICompilerReferenceError CS1937
        {
            get
            {
                if (_CS1937 == null)
                    _CS1937 = new CompilerReferenceError(Resources.CSharpErrors_CS1937, 1937);
                return _CS1937;
            }
        }
        private static ICompilerReferenceError _CS1937;

        /// <summary><para>C&#9839; compiler error &#35;1938:</para><para>The name 'name' is not in scope on the right side of 'equals'. Consider swapping the expressions on either side of 'equals'.</para></summary>
        public static ICompilerReferenceError CS1938
        {
            get
            {
                if (_CS1938 == null)
                    _CS1938 = new CompilerReferenceError(Resources.CSharpErrors_CS1938, 1938);
                return _CS1938;
            }
        }
        private static ICompilerReferenceError _CS1938;

        /// <summary><para>C&#9839; compiler error &#35;1939:</para><para>Cannot pass the range variable 'name' as an out or ref parameter.</para></summary>
        public static ICompilerReferenceError CS1939
        {
            get
            {
                if (_CS1939 == null)
                    _CS1939 = new CompilerReferenceError(Resources.CSharpErrors_CS1939, 1939);
                return _CS1939;
            }
        }
        private static ICompilerReferenceError _CS1939;

        /// <summary><para>C&#9839; compiler error &#35;1940:</para><para>Multiple implementations of the query pattern were found for source type 'type'. Ambiguous call to 'method'.</para></summary>
        public static ICompilerReferenceError CS1940
        {
            get
            {
                if (_CS1940 == null)
                    _CS1940 = new CompilerReferenceError(Resources.CSharpErrors_CS1940, 1940);
                return _CS1940;
            }
        }
        private static ICompilerReferenceError _CS1940;

        /// <summary><para>C&#9839; compiler error &#35;1941:</para><para>The type of one of the expressions in the 'clause' clause is incorrect. Type inference failed in the call to 'method'.</para></summary>
        public static ICompilerReferenceError CS1941
        {
            get
            {
                if (_CS1941 == null)
                    _CS1941 = new CompilerReferenceError(Resources.CSharpErrors_CS1941, 1941);
                return _CS1941;
            }
        }
        private static ICompilerReferenceError _CS1941;

        /// <summary><para>C&#9839; compiler error &#35;1942:</para><para>The type of the expression in the 'clause' clause is incorrect. Type inference failed in the call to 'method'.</para></summary>
        public static ICompilerReferenceError CS1942
        {
            get
            {
                if (_CS1942 == null)
                    _CS1942 = new CompilerReferenceError(Resources.CSharpErrors_CS1942, 1942);
                return _CS1942;
            }
        }
        private static ICompilerReferenceError _CS1942;

        /// <summary><para>C&#9839; compiler error &#35;1943:</para><para>An expression of type 'type' is not allowed in a subsequent from clause in a query expression with source type 'type'. Type inference failed in the call to 'method'.</para></summary>
        public static ICompilerReferenceError CS1943
        {
            get
            {
                if (_CS1943 == null)
                    _CS1943 = new CompilerReferenceError(Resources.CSharpErrors_CS1943, 1943);
                return _CS1943;
            }
        }
        private static ICompilerReferenceError _CS1943;

        /// <summary><para>C&#9839; compiler error &#35;1944:</para><para>An expression tree may not contain an unsafe pointer operation</para></summary>
        public static ICompilerReferenceError CS1944
        {
            get
            {
                if (_CS1944 == null)
                    _CS1944 = new CompilerReferenceError(Resources.CSharpErrors_CS1944, 1944);
                return _CS1944;
            }
        }
        private static ICompilerReferenceError _CS1944;

        /// <summary><para>C&#9839; compiler error &#35;1945:</para><para>An expression tree may not contain an anonymous method expression.</para></summary>
        public static ICompilerReferenceError CS1945
        {
            get
            {
                if (_CS1945 == null)
                    _CS1945 = new CompilerReferenceError(Resources.CSharpErrors_CS1945, 1945);
                return _CS1945;
            }
        }
        private static ICompilerReferenceError _CS1945;

        /// <summary><para>C&#9839; compiler error &#35;1946:</para><para>An anonymous method expression cannot be converted to an expression tree.</para></summary>
        public static ICompilerReferenceError CS1946
        {
            get
            {
                if (_CS1946 == null)
                    _CS1946 = new CompilerReferenceError(Resources.CSharpErrors_CS1946, 1946);
                return _CS1946;
            }
        }
        private static ICompilerReferenceError _CS1946;

        /// <summary><para>C&#9839; compiler error &#35;1947:</para><para>Range variable 'variable name' cannot be assigned to -- it is read only.</para></summary>
        public static ICompilerReferenceError CS1947
        {
            get
            {
                if (_CS1947 == null)
                    _CS1947 = new CompilerReferenceError(Resources.CSharpErrors_CS1947, 1947);
                return _CS1947;
            }
        }
        private static ICompilerReferenceError _CS1947;

        /// <summary><para>C&#9839; compiler error &#35;1948:</para><para>The range variable 'name' cannot have the same name as a method type parameter</para></summary>
        public static ICompilerReferenceError CS1948
        {
            get
            {
                if (_CS1948 == null)
                    _CS1948 = new CompilerReferenceError(Resources.CSharpErrors_CS1948, 1948);
                return _CS1948;
            }
        }
        private static ICompilerReferenceError _CS1948;

        /// <summary><para>C&#9839; compiler error &#35;1949:</para><para>The contextual keyword 'var' cannot be used in a range variable declaration.</para></summary>
        public static ICompilerReferenceError CS1949
        {
            get
            {
                if (_CS1949 == null)
                    _CS1949 = new CompilerReferenceError(Resources.CSharpErrors_CS1949, 1949);
                return _CS1949;
            }
        }
        private static ICompilerReferenceError _CS1949;

        /// <summary><para>C&#9839; compiler error &#35;1950:</para><para>The best overloaded Add method 'name' for the collection initializer has some invalid arguments.</para></summary>
        public static ICompilerReferenceError CS1950
        {
            get
            {
                if (_CS1950 == null)
                    _CS1950 = new CompilerReferenceError(Resources.CSharpErrors_CS1950, 1950);
                return _CS1950;
            }
        }
        private static ICompilerReferenceError _CS1950;

        /// <summary><para>C&#9839; compiler error &#35;1951:</para><para>An expression tree lambda may not contain an out or ref parameter.</para></summary>
        public static ICompilerReferenceError CS1951
        {
            get
            {
                if (_CS1951 == null)
                    _CS1951 = new CompilerReferenceError(Resources.CSharpErrors_CS1951, 1951);
                return _CS1951;
            }
        }
        private static ICompilerReferenceError _CS1951;

        /// <summary><para>C&#9839; compiler error &#35;1952:</para><para>An expression tree lambda may not contain a method with variable arguments</para></summary>
        public static ICompilerReferenceError CS1952
        {
            get
            {
                if (_CS1952 == null)
                    _CS1952 = new CompilerReferenceError(Resources.CSharpErrors_CS1952, 1952);
                return _CS1952;
            }
        }
        private static ICompilerReferenceError _CS1952;

        /// <summary><para>C&#9839; compiler error &#35;1953:</para><para>An expression tree lambda may not contain a method group.</para></summary>
        public static ICompilerReferenceError CS1953
        {
            get
            {
                if (_CS1953 == null)
                    _CS1953 = new CompilerReferenceError(Resources.CSharpErrors_CS1953, 1953);
                return _CS1953;
            }
        }
        private static ICompilerReferenceError _CS1953;

        /// <summary><para>C&#9839; compiler error &#35;1954:</para><para>The best overloaded method match 'method' for the collection initializer element cannot be used. Collection initializer 'Add' methods cannot have ref or out parameters.</para></summary>
        public static ICompilerReferenceError CS1954
        {
            get
            {
                if (_CS1954 == null)
                    _CS1954 = new CompilerReferenceError(Resources.CSharpErrors_CS1954, 1954);
                return _CS1954;
            }
        }
        private static ICompilerReferenceError _CS1954;

        /// <summary><para>C&#9839; compiler error &#35;1955:</para><para>Non-invocable member 'name' cannot be used like a method.</para></summary>
        public static ICompilerReferenceError CS1955
        {
            get
            {
                if (_CS1955 == null)
                    _CS1955 = new CompilerReferenceError(Resources.CSharpErrors_CS1955, 1955);
                return _CS1955;
            }
        }
        private static ICompilerReferenceError _CS1955;

        /// <summary><para>C&#9839; compiler error &#35;1958:</para><para>Object and collection initializer expressions may not be applied to a delegate creation expression,</para></summary>
        public static ICompilerReferenceError CS1958
        {
            get
            {
                if (_CS1958 == null)
                    _CS1958 = new CompilerReferenceError(Resources.CSharpErrors_CS1958, 1958);
                return _CS1958;
            }
        }
        private static ICompilerReferenceError _CS1958;

        /// <summary><para>C&#9839; compiler error &#35;1959:</para><para>'name' is of type 'type'. The type specified in a constant declaration must be sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, bool, string, an enum-type, or a reference-type.</para></summary>
        public static ICompilerReferenceError CS1959
        {
            get
            {
                if (_CS1959 == null)
                    _CS1959 = new CompilerReferenceError(Resources.CSharpErrors_CS1959, 1959);
                return _CS1959;
            }
        }
        private static ICompilerReferenceError _CS1959;

        /// <summary><para>C&#9839; compiler error &#35;2001:</para><para>Source file 'file' could not be found</para></summary>
        public static ICompilerReferenceError CS2001
        {
            get
            {
                if (_CS2001 == null)
                    _CS2001 = new CompilerReferenceError(Resources.CSharpErrors_CS2001, 2001);
                return _CS2001;
            }
        }
        private static ICompilerReferenceError _CS2001;

        /// <summary><para>C&#9839; compiler error &#35;2003:</para><para>Response file 'file' included multiple times</para></summary>
        public static ICompilerReferenceError CS2003
        {
            get
            {
                if (_CS2003 == null)
                    _CS2003 = new CompilerReferenceError(Resources.CSharpErrors_CS2003, 2003);
                return _CS2003;
            }
        }
        private static ICompilerReferenceError _CS2003;

        /// <summary><para>C&#9839; compiler error &#35;2005:</para><para>Missing file specification for 'option' option</para></summary>
        public static ICompilerReferenceError CS2005
        {
            get
            {
                if (_CS2005 == null)
                    _CS2005 = new CompilerReferenceError(Resources.CSharpErrors_CS2005, 2005);
                return _CS2005;
            }
        }
        private static ICompilerReferenceError _CS2005;

        /// <summary><para>C&#9839; compiler error &#35;2006:</para><para>Command-line syntax error: Missing 'text' for 'option' option</para></summary>
        public static ICompilerReferenceError CS2006
        {
            get
            {
                if (_CS2006 == null)
                    _CS2006 = new CompilerReferenceError(Resources.CSharpErrors_CS2006, 2006);
                return _CS2006;
            }
        }
        private static ICompilerReferenceError _CS2006;

        /// <summary><para>C&#9839; compiler error &#35;2007:</para><para>Unrecognized command-line option: 'option'</para></summary>
        public static ICompilerReferenceError CS2007
        {
            get
            {
                if (_CS2007 == null)
                    _CS2007 = new CompilerReferenceError(Resources.CSharpErrors_CS2007, 2007);
                return _CS2007;
            }
        }
        private static ICompilerReferenceError _CS2007;

        /// <summary><para>C&#9839; compiler error &#35;2008:</para><para>No inputs specified</para></summary>
        public static ICompilerReferenceError CS2008
        {
            get
            {
                if (_CS2008 == null)
                    _CS2008 = new CompilerReferenceError(Resources.CSharpErrors_CS2008, 2008);
                return _CS2008;
            }
        }
        private static ICompilerReferenceError _CS2008;

        /// <summary><para>C&#9839; compiler error &#35;2011:</para><para>Unable to open response file 'file'</para></summary>
        public static ICompilerReferenceError CS2011
        {
            get
            {
                if (_CS2011 == null)
                    _CS2011 = new CompilerReferenceError(Resources.CSharpErrors_CS2011, 2011);
                return _CS2011;
            }
        }
        private static ICompilerReferenceError _CS2011;

        /// <summary><para>C&#9839; compiler error &#35;2012:</para><para>Cannot open 'file' for writing</para></summary>
        public static ICompilerReferenceError CS2012
        {
            get
            {
                if (_CS2012 == null)
                    _CS2012 = new CompilerReferenceError(Resources.CSharpErrors_CS2012, 2012);
                return _CS2012;
            }
        }
        private static ICompilerReferenceError _CS2012;

        /// <summary><para>C&#9839; compiler error &#35;2013:</para><para>Invalid image base number 'value'</para></summary>
        public static ICompilerReferenceError CS2013
        {
            get
            {
                if (_CS2013 == null)
                    _CS2013 = new CompilerReferenceError(Resources.CSharpErrors_CS2013, 2013);
                return _CS2013;
            }
        }
        private static ICompilerReferenceError _CS2013;

        /// <summary><para>C&#9839; compiler error &#35;2015:</para><para>'file' is a binary file instead of a text file</para></summary>
        public static ICompilerReferenceError CS2015
        {
            get
            {
                if (_CS2015 == null)
                    _CS2015 = new CompilerReferenceError(Resources.CSharpErrors_CS2015, 2015);
                return _CS2015;
            }
        }
        private static ICompilerReferenceError _CS2015;

        /// <summary><para>C&#9839; compiler error &#35;2016:</para><para>Code page 'codepage' is invalid or not installed</para></summary>
        public static ICompilerReferenceError CS2016
        {
            get
            {
                if (_CS2016 == null)
                    _CS2016 = new CompilerReferenceError(Resources.CSharpErrors_CS2016, 2016);
                return _CS2016;
            }
        }
        private static ICompilerReferenceError _CS2016;

        /// <summary><para>C&#9839; compiler error &#35;2017:</para><para>Cannot specify /main if building a module or library</para></summary>
        public static ICompilerReferenceError CS2017
        {
            get
            {
                if (_CS2017 == null)
                    _CS2017 = new CompilerReferenceError(Resources.CSharpErrors_CS2017, 2017);
                return _CS2017;
            }
        }
        private static ICompilerReferenceError _CS2017;

        /// <summary><para>C&#9839; compiler error &#35;2018:</para><para>Unable to find messages file 'cscmsgs.dll'</para></summary>
        public static ICompilerReferenceError CS2018
        {
            get
            {
                if (_CS2018 == null)
                    _CS2018 = new CompilerReferenceError(Resources.CSharpErrors_CS2018, 2018);
                return _CS2018;
            }
        }
        private static ICompilerReferenceError _CS2018;

        /// <summary><para>C&#9839; compiler error &#35;2019:</para><para>Invalid target type for /target: must specify 'exe', 'winexe', 'library', or 'module'</para></summary>
        public static ICompilerReferenceError CS2019
        {
            get
            {
                if (_CS2019 == null)
                    _CS2019 = new CompilerReferenceError(Resources.CSharpErrors_CS2019, 2019);
                return _CS2019;
            }
        }
        private static ICompilerReferenceError _CS2019;

        /// <summary><para>C&#9839; compiler error &#35;2020:</para><para>Only the first set of input files can build a target other than 'module'</para></summary>
        public static ICompilerReferenceError CS2020
        {
            get
            {
                if (_CS2020 == null)
                    _CS2020 = new CompilerReferenceError(Resources.CSharpErrors_CS2020, 2020);
                return _CS2020;
            }
        }
        private static ICompilerReferenceError _CS2020;

        /// <summary><para>C&#9839; compiler error &#35;2021:</para><para>File name 'file' is too long or invalid</para></summary>
        public static ICompilerReferenceError CS2021
        {
            get
            {
                if (_CS2021 == null)
                    _CS2021 = new CompilerReferenceError(Resources.CSharpErrors_CS2021, 2021);
                return _CS2021;
            }
        }
        private static ICompilerReferenceError _CS2021;

        /// <summary><para>C&#9839; compiler error &#35;2022:</para><para>Options '/out' and '/target' must appear before source file names</para></summary>
        public static ICompilerReferenceError CS2022
        {
            get
            {
                if (_CS2022 == null)
                    _CS2022 = new CompilerReferenceError(Resources.CSharpErrors_CS2022, 2022);
                return _CS2022;
            }
        }
        private static ICompilerReferenceError _CS2022;

        /// <summary><para>C&#9839; compiler error &#35;2024:</para><para>Invalid file section alignment number '#'</para></summary>
        public static ICompilerReferenceError CS2024
        {
            get
            {
                if (_CS2024 == null)
                    _CS2024 = new CompilerReferenceError(Resources.CSharpErrors_CS2024, 2024);
                return _CS2024;
            }
        }
        private static ICompilerReferenceError _CS2024;

        /// <summary><para>C&#9839; compiler error &#35;2032:</para><para>Character 'character' is not allowed on the command-line or in response files</para></summary>
        public static ICompilerReferenceError CS2032
        {
            get
            {
                if (_CS2032 == null)
                    _CS2032 = new CompilerReferenceError(Resources.CSharpErrors_CS2032, 2032);
                return _CS2032;
            }
        }
        private static ICompilerReferenceError _CS2032;

        /// <summary><para>C&#9839; compiler error &#35;2033:</para><para>Cannot create short filename 'filename' when a long filename with the same short filename already exists </para></summary>
        public static ICompilerReferenceError CS2033
        {
            get
            {
                if (_CS2033 == null)
                    _CS2033 = new CompilerReferenceError(Resources.CSharpErrors_CS2033, 2033);
                return _CS2033;
            }
        }
        private static ICompilerReferenceError _CS2033;

        /// <summary><para>C&#9839; compiler error &#35;2034:</para><para>A /reference option that declares an extern alias can only have one filename. To specify multiple aliases or filenames, use multiple /reference options.</para></summary>
        public static ICompilerReferenceError CS2034
        {
            get
            {
                if (_CS2034 == null)
                    _CS2034 = new CompilerReferenceError(Resources.CSharpErrors_CS2034, 2034);
                return _CS2034;
            }
        }
        private static ICompilerReferenceError _CS2034;

        /// <summary><para>C&#9839; compiler error &#35;2035:</para><para>Command-line syntax error: Missing ':&lt;number&gt;' for 'compiler_option' option</para></summary>
        public static ICompilerReferenceError CS2035
        {
            get
            {
                if (_CS2035 == null)
                    _CS2035 = new CompilerReferenceError(Resources.CSharpErrors_CS2035, 2035);
                return _CS2035;
            }
        }
        private static ICompilerReferenceError _CS2035;

        /// <summary><para>C&#9839; compiler error &#35;2036:</para><para>The /pdb option requires that the /debug option also be used.</para></summary>
        public static ICompilerReferenceError CS2036
        {
            get
            {
                if (_CS2036 == null)
                    _CS2036 = new CompilerReferenceError(Resources.CSharpErrors_CS2036, 2036);
                return _CS2036;
            }
        }
        private static ICompilerReferenceError _CS2036;

        /// <summary><para>C&#9839; compiler error &#35;5001:</para><para>Program 'program' does not contain a static 'Main' method suitable for an entry point</para></summary>
        public static ICompilerReferenceError CS5001
        {
            get
            {
                if (_CS5001 == null)
                    _CS5001 = new CompilerReferenceError(Resources.CSharpErrors_CS5001, 5001);
                return _CS5001;
            }
        }
        private static ICompilerReferenceError _CS5001;

    }
}
