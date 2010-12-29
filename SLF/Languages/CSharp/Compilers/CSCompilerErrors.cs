using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Compilers
{
    public static class CSCompilerMessages
    {
        public static ICompilerReferenceWarning CS0028
        {
            get
            {
                if (_CS0028 == null)
                    _CS0028 = new CompilerReferenceWarning(@"{0} has the wrong signature to be an entry point ", 4, 28);
                return _CS0028;
            }
        }
        private static ICompilerReferenceWarning _CS0028;
        public static ICompilerReferenceWarning CS0067
        {
            get
            {
                if (_CS0067 == null)
                    _CS0067 = new CompilerReferenceWarning(@"The event {0} is never used", 3, 67);
                return _CS0067;
            }
        }
        private static ICompilerReferenceWarning _CS0067;
        public static ICompilerReferenceWarning CS0078
        {
            get
            {
                if (_CS0078 == null)
                    _CS0078 = new CompilerReferenceWarning(@"The 'l' suffix is easily confused with the digit '1' -- use 'L' for clarity", 4, 78);
                return _CS0078;
            }
        }
        private static ICompilerReferenceWarning _CS0078;
        public static ICompilerReferenceWarning CS0105
        {
            get
            {
                if (_CS0105 == null)
                    _CS0105 = new CompilerReferenceWarning(@"The using directive for {0} appeared previously in this namespace", 3, 105);
                return _CS0105;
            }
        }
        private static ICompilerReferenceWarning _CS0105;
        public static ICompilerReferenceWarning CS0108
        {
            get
            {
                if (_CS0108 == null)
                    _CS0108 = new CompilerReferenceWarning(@"{0} hides inherited member {1}. Use the new keyword if hiding was intended.", 2, 108);
                return _CS0108;
            }
        }
        private static ICompilerReferenceWarning _CS0108;
        public static ICompilerReferenceWarning CS0109
        {
            get
            {
                if (_CS0109 == null)
                    _CS0109 = new CompilerReferenceWarning(@"The member {0} does not hide an inherited member. The new keyword is not required", 4, 109);
                return _CS0109;
            }
        }
        private static ICompilerReferenceWarning _CS0109;
        public static ICompilerReferenceWarning CS0114
        {
            get
            {
                if (_CS0114 == null)
                    _CS0114 = new CompilerReferenceWarning(@"{0} hides inherited member {1}. To make the current method override that implementation, add the override keyword. Otherwise add the new keyword.", 2, 114);
                return _CS0114;
            }
        }
        private static ICompilerReferenceWarning _CS0114;
        public static ICompilerReferenceWarning CS0162
        {
            get
            {
                if (_CS0162 == null)
                    _CS0162 = new CompilerReferenceWarning(@"Unreachable code detected", 2, 162);
                return _CS0162;
            }
        }
        private static ICompilerReferenceWarning _CS0162;
        public static ICompilerReferenceWarning CS0164
        {
            get
            {
                if (_CS0164 == null)
                    _CS0164 = new CompilerReferenceWarning(@"This label has not been referenced", 2, 164);
                return _CS0164;
            }
        }
        private static ICompilerReferenceWarning _CS0164;
        public static ICompilerReferenceWarning CS0168
        {
            get
            {
                if (_CS0168 == null)
                    _CS0168 = new CompilerReferenceWarning(@"The variable {0} is assigned but its value is never used", 3, 168);
                return _CS0168;
            }
        }
        private static ICompilerReferenceWarning _CS0168;
        public static ICompilerReferenceWarning CS0169
        {
            get
            {
                if (_CS0169 == null)
                    _CS0169 = new CompilerReferenceWarning(@"The private field {0} is never used", 3, 169);
                return _CS0169;
            }
        }
        private static ICompilerReferenceWarning _CS0169;
        public static ICompilerReferenceWarning CS0183
        {
            get
            {
                if (_CS0183 == null)
                    _CS0183 = new CompilerReferenceWarning(@"The given expression is always of the provided ({0}) type", 1, 183);
                return _CS0183;
            }
        }
        private static ICompilerReferenceWarning _CS0183;
        public static ICompilerReferenceWarning CS0184
        {
            get
            {
                if (_CS0184 == null)
                    _CS0184 = new CompilerReferenceWarning(@"The given expression is never of the provided ({0}) type", 1, 184);
                return _CS0184;
            }
        }
        private static ICompilerReferenceWarning _CS0184;
        public static ICompilerReferenceWarning CS0197
        {
            get
            {
                if (_CS0197 == null)
                    _CS0197 = new CompilerReferenceWarning(@"Passing {0} as ref or out or taking its address may cause a runtime exception because it is a field of a marshal-by-reference class", 1, 197);
                return _CS0197;
            }
        }
        private static ICompilerReferenceWarning _CS0197;
        public static ICompilerReferenceWarning CS0219
        {
            get
            {
                if (_CS0219 == null)
                    _CS0219 = new CompilerReferenceWarning(@"The variable {0} is assigned but its value is never used", 3, 219);
                return _CS0219;
            }
        }
        private static ICompilerReferenceWarning _CS0219;
        public static ICompilerReferenceWarning CS0251
        {
            get
            {
                if (_CS0251 == null)
                    _CS0251 = new CompilerReferenceWarning(@"Indexing an array with a negative index (array indices always start at zero)", 2, 251);
                return _CS0251;
            }
        }
        private static ICompilerReferenceWarning _CS0251;
        public static ICompilerReferenceWarning CS0252
        {
            get
            {
                if (_CS0252 == null)
                    _CS0252 = new CompilerReferenceWarning(@"Possible unintended reference comparison; to get a value comparison, cast the left hand side to type {0}", 2, 252);
                return _CS0252;
            }
        }
        private static ICompilerReferenceWarning _CS0252;
        public static ICompilerReferenceWarning CS0253
        {
            get
            {
                if (_CS0253 == null)
                    _CS0253 = new CompilerReferenceWarning(@"Possible unintended reference comparison; to get a value comparison, cast the right hand side to type {0}", 2, 253);
                return _CS0253;
            }
        }
        private static ICompilerReferenceWarning _CS0253;
        public static ICompilerReferenceWarning CS0278
        {
            get
            {
                if (_CS0278 == null)
                    _CS0278 = new CompilerReferenceWarning(@"{0} does not implement the {1} pattern. {2} is ambiguous with {2}.", 2, 278);
                return _CS0278;
            }
        }
        private static ICompilerReferenceWarning _CS0278;
        public static ICompilerReferenceWarning CS0279
        {
            get
            {
                if (_CS0279 == null)
                    _CS0279 = new CompilerReferenceWarning(@"{0} does not implement the {1} pattern. {2} is either static or not public.", 2, 279);
                return _CS0279;
            }
        }
        private static ICompilerReferenceWarning _CS0279;
        public static ICompilerReferenceWarning CS0280
        {
            get
            {
                if (_CS0280 == null)
                    _CS0280 = new CompilerReferenceWarning(@"{0} does not implement the {1} pattern. {2} has the wrong signature.", 2, 280);
                return _CS0280;
            }
        }
        private static ICompilerReferenceWarning _CS0280;
        public static ICompilerReferenceWarning CS0282
        {
            get
            {
                if (_CS0282 == null)
                    _CS0282 = new CompilerReferenceWarning(@"There is no defined ordering between fields in multiple declarations of partial class or struct {0}. To specify an ordering, all instance fields must be in the same declaration.", 3, 282);
                return _CS0282;
            }
        }
        private static ICompilerReferenceWarning _CS0282;
        public static ICompilerReferenceWarning CS0402
        {
            get
            {
                if (_CS0402 == null)
                    _CS0402 = new CompilerReferenceWarning(@"{0} : an entry point cannot be generic or in a generic type", 4, 402);
                return _CS0402;
            }
        }
        private static ICompilerReferenceWarning _CS0402;
        public static ICompilerReferenceWarning CS0414
        {
            get
            {
                if (_CS0414 == null)
                    _CS0414 = new CompilerReferenceWarning(@"The private field {0} is assigned but its value is never used", 3, 414);
                return _CS0414;
            }
        }
        private static ICompilerReferenceWarning _CS0414;
        public static ICompilerReferenceWarning CS0419
        {
            get
            {
                if (_CS0419 == null)
                    _CS0419 = new CompilerReferenceWarning(@"Ambiguous reference in cref attribute: {0}. Assuming {1}, but could have also matched other overloads including {2}.", 3, 419);
                return _CS0419;
            }
        }
        private static ICompilerReferenceWarning _CS0419;
        public static ICompilerReferenceWarning CS0420
        {
            get
            {
                if (_CS0420 == null)
                    _CS0420 = new CompilerReferenceWarning(@"{0}: a reference to a volatile field will not be treated as volatile", 1, 420);
                return _CS0420;
            }
        }
        private static ICompilerReferenceWarning _CS0420;
        public static ICompilerReferenceWarning CS0422
        {
            get
            {
                if (_CS0422 == null)
                    _CS0422 = new CompilerReferenceWarning(@"The /incremental option is no longer supported", 4, 422);
                return _CS0422;
            }
        }
        private static ICompilerReferenceWarning _CS0422;
        public static ICompilerReferenceWarning CS0429
        {
            get
            {
                if (_CS0429 == null)
                    _CS0429 = new CompilerReferenceWarning(@"Unreachable expression code detected ", 4, 429);
                return _CS0429;
            }
        }
        private static ICompilerReferenceWarning _CS0429;
        public static ICompilerReferenceWarning CS0435
        {
            get
            {
                if (_CS0435 == null)
                    _CS0435 = new CompilerReferenceWarning(@"The namespace {0} in {1} conflicts with the imported type {2} in {3}. Using the namespace defined in {1}..", 2, 435);
                return _CS0435;
            }
        }
        private static ICompilerReferenceWarning _CS0435;
        public static ICompilerReferenceWarning CS0436
        {
            get
            {
                if (_CS0436 == null)
                    _CS0436 = new CompilerReferenceWarning(@"The type {0} in {1} conflicts with the imported type {2} in {3}. Using the type defined in {1}.", 2, 436);
                return _CS0436;
            }
        }
        private static ICompilerReferenceWarning _CS0436;
        public static ICompilerReferenceWarning CS0437
        {
            get
            {
                if (_CS0437 == null)
                    _CS0437 = new CompilerReferenceWarning(@"The type {0} in {1} conflicts with the imported namespace {2} in {3}. Using the type defined in {1}.", 2, 437);
                return _CS0437;
            }
        }
        private static ICompilerReferenceWarning _CS0437;
        public static ICompilerReferenceWarning CS0440
        {
            get
            {
                if (_CS0440 == null)
                    _CS0440 = new CompilerReferenceWarning(@"Defining an alias named 'global' is ill-advised since 'global::' always references the global namespace and not an alias", 2, 440);
                return _CS0440;
            }
        }
        private static ICompilerReferenceWarning _CS0440;
        public static ICompilerReferenceWarning CS0444
        {
            get
            {
                if (_CS0444 == null)
                    _CS0444 = new CompilerReferenceWarning(@"Predefined type {0} was not found in {1} but was found in {2}", 2, 444);
                return _CS0444;
            }
        }
        private static ICompilerReferenceWarning _CS0444;
        public static ICompilerReferenceWarning CS0458
        {
            get
            {
                if (_CS0458 == null)
                    _CS0458 = new CompilerReferenceWarning(@"The result of the expression is always 'null' of type {0}", 2, 458);
                return _CS0458;
            }
        }
        private static ICompilerReferenceWarning _CS0458;
        public static ICompilerReferenceWarning CS0464
        {
            get
            {
                if (_CS0464 == null)
                    _CS0464 = new CompilerReferenceWarning(@"Comparing with null of type {0} always produces 'false'", 2, 464);
                return _CS0464;
            }
        }
        private static ICompilerReferenceWarning _CS0464;
        public static ICompilerReferenceWarning CS0465
        {
            get
            {
                if (_CS0465 == null)
                    _CS0465 = new CompilerReferenceWarning(@"Introducing a 'Finalize' method can interfere with destructor invocation. Did you intend to declare a destructor?", 1, 465);
                return _CS0465;
            }
        }
        private static ICompilerReferenceWarning _CS0465;
        public static ICompilerReferenceWarning CS0467
        {
            get
            {
                if (_CS0467 == null)
                    _CS0467 = new CompilerReferenceWarning(@"Ambiguity between method {0} and non-method {1}. Using method group.", 2, 467);
                return _CS0467;
            }
        }
        private static ICompilerReferenceWarning _CS0467;
        public static ICompilerReferenceWarning CS0469
        {
            get
            {
                if (_CS0469 == null)
                    _CS0469 = new CompilerReferenceWarning(@"The {0} value is not implicitly convertible to type {1}", 2, 469);
                return _CS0469;
            }
        }
        private static ICompilerReferenceWarning _CS0469;
        public static ICompilerReferenceWarning CS0472
        {
            get
            {
                if (_CS0472 == null)
                    _CS0472 = new CompilerReferenceWarning(@"The result of the expression is always {0} since a value of type {1} is never equal to 'null' of type {1}", 2, 472);
                return _CS0472;
            }
        }
        private static ICompilerReferenceWarning _CS0472;
        public static ICompilerReferenceWarning CS0602
        {
            get
            {
                if (_CS0602 == null)
                    _CS0602 = new CompilerReferenceWarning(@"The feature {0} is deprecated. Please use {1} instead", 1, 602);
                return _CS0602;
            }
        }
        private static ICompilerReferenceWarning _CS0602;
        public static ICompilerReferenceWarning CS0612
        {
            get
            {
                if (_CS0612 == null)
                    _CS0612 = new CompilerReferenceWarning(@"{0} is obsolete", 1, 612);
                return _CS0612;
            }
        }
        private static ICompilerReferenceWarning _CS0612;
        public static ICompilerReferenceWarning CS0618
        {
            get
            {
                if (_CS0618 == null)
                    _CS0618 = new CompilerReferenceWarning(@"{0} is obsolete: {1}", 2, 618);
                return _CS0618;
            }
        }
        private static ICompilerReferenceWarning _CS0618;
        public static ICompilerReferenceWarning CS0626
        {
            get
            {
                if (_CS0626 == null)
                    _CS0626 = new CompilerReferenceWarning(@"Method, operator, or accessor {0} is marked external and has no attributes on it. Consider adding a DllImport attribute to specify the external implementation", 1, 626);
                return _CS0626;
            }
        }
        private static ICompilerReferenceWarning _CS0626;
        public static ICompilerReferenceWarning CS0628
        {
            get
            {
                if (_CS0628 == null)
                    _CS0628 = new CompilerReferenceWarning(@"{0} : new protected member declared in sealed class", 4, 628);
                return _CS0628;
            }
        }
        private static ICompilerReferenceWarning _CS0628;
        public static ICompilerReferenceWarning CS0642
        {
            get
            {
                if (_CS0642 == null)
                    _CS0642 = new CompilerReferenceWarning(@"Possible mistaken empty statement", 3, 642);
                return _CS0642;
            }
        }
        private static ICompilerReferenceWarning _CS0642;
        public static ICompilerReferenceWarning CS0649
        {
            get
            {
                if (_CS0649 == null)
                    _CS0649 = new CompilerReferenceWarning(@"Field {0} is never assigned to, and will always have its default value {1}", 4, 649);
                return _CS0649;
            }
        }
        private static ICompilerReferenceWarning _CS0649;
        public static ICompilerReferenceWarning CS0652
        {
            get
            {
                if (_CS0652 == null)
                    _CS0652 = new CompilerReferenceWarning(@"Comparison to integral constant is useless; the constant is outside the range of type {0}", 2, 652);
                return _CS0652;
            }
        }
        private static ICompilerReferenceWarning _CS0652;
        public static ICompilerReferenceWarning CS0657
        {
            get
            {
                if (_CS0657 == null)
                    _CS0657 = new CompilerReferenceWarning(@"{0} is not a valid attribute location for this declaration. Valid attribute locations for this declaration are {1}. All attributes in this block will be ignored.", 1, 657);
                return _CS0657;
            }
        }
        private static ICompilerReferenceWarning _CS0657;
        public static ICompilerReferenceWarning CS0658
        {
            get
            {
                if (_CS0658 == null)
                    _CS0658 = new CompilerReferenceWarning(@"{0} is not a recognized attribute location. All attributes in this block will be ignored.", 1, 658);
                return _CS0658;
            }
        }
        private static ICompilerReferenceWarning _CS0658;
        public static ICompilerReferenceWarning CS0659
        {
            get
            {
                if (_CS0659 == null)
                    _CS0659 = new CompilerReferenceWarning(@"{0} overrides Object.Equals(object o) but does not override Object.GetHashCode()", 3, 659);
                return _CS0659;
            }
        }
        private static ICompilerReferenceWarning _CS0659;
        public static ICompilerReferenceWarning CS0660
        {
            get
            {
                if (_CS0660 == null)
                    _CS0660 = new CompilerReferenceWarning(@"{0} defines operator == or operator != but does not override Object.Equals(object o)", 3, 660);
                return _CS0660;
            }
        }
        private static ICompilerReferenceWarning _CS0660;
        public static ICompilerReferenceWarning CS0661
        {
            get
            {
                if (_CS0661 == null)
                    _CS0661 = new CompilerReferenceWarning(@"{0} defines operator == or operator != but does not override Object.GetHashCode()", 3, 661);
                return _CS0661;
            }
        }
        private static ICompilerReferenceWarning _CS0661;
        public static ICompilerReferenceWarning CS0665
        {
            get
            {
                if (_CS0665 == null)
                    _CS0665 = new CompilerReferenceWarning(@"Assignment in conditional expression is always constant; did you mean to use '==' instead of '='?", 3, 665);
                return _CS0665;
            }
        }
        private static ICompilerReferenceWarning _CS0665;
        public static ICompilerReferenceWarning CS0672
        {
            get
            {
                if (_CS0672 == null)
                    _CS0672 = new CompilerReferenceWarning(@"Member {0} overrides obsolete member '{1}. Add the Obsolete attribute to {0}", 1, 672);
                return _CS0672;
            }
        }
        private static ICompilerReferenceWarning _CS0672;
        public static ICompilerReferenceWarning CS0675
        {
            get
            {
                if (_CS0675 == null)
                    _CS0675 = new CompilerReferenceWarning(@"Bitwise-or operator used on a sign-extended operand; consider casting to a smaller unsigned type first", 3, 675);
                return _CS0675;
            }
        }
        private static ICompilerReferenceWarning _CS0675;
        public static ICompilerReferenceWarning CS0693
        {
            get
            {
                if (_CS0693 == null)
                    _CS0693 = new CompilerReferenceWarning(@"Type parameter {0} has the same name as the type parameter from outer type {1}", 3, 693);
                return _CS0693;
            }
        }
        private static ICompilerReferenceWarning _CS0693;
        public static ICompilerReferenceWarning CS0728
        {
            get
            {
                if (_CS0728 == null)
                    _CS0728 = new CompilerReferenceWarning(@"Possibly incorrect assignment to local {0} which is the argument to a using or lock statement. The Dispose call or unlocking will happen on the original value of the local.", 2, 728);
                return _CS0728;
            }
        }
        private static ICompilerReferenceWarning _CS0728;
        public static ICompilerReferenceWarning CS0809
        {
            get
            {
                if (_CS0809 == null)
                    _CS0809 = new CompilerReferenceWarning(@"Obsolete member {0} overrides non-obsolete member {1}.", 1, 809);
                return _CS0809;
            }
        }
        private static ICompilerReferenceWarning _CS0809;
        public static ICompilerReferenceWarning CS0824
        {
            get
            {
                if (_CS0824 == null)
                    _CS0824 = new CompilerReferenceWarning(@"Constructor {0} is marked external.", 1, 824);
                return _CS0824;
            }
        }
        private static ICompilerReferenceWarning _CS0824;
        public static ICompilerReferenceWarning CS1030
        {
            get
            {
                if (_CS1030 == null)
                    _CS1030 = new CompilerReferenceWarning(@"#warning: {0}", 1, 1030);
                return _CS1030;
            }
        }
        private static ICompilerReferenceWarning _CS1030;
        public static ICompilerReferenceWarning CS1058
        {
            get
            {
                if (_CS1058 == null)
                    _CS1058 = new CompilerReferenceWarning(@"A previous catch clause already catches all exceptions. All exceptions thrown will be wrapped in a System.Runtime.CompilerServices.RuntimeWrappedException", 1, 1058);
                return _CS1058;
            }
        }
        private static ICompilerReferenceWarning _CS1058;
        public static ICompilerReferenceWarning CS1060
        {
            get
            {
                if (_CS1060 == null)
                    _CS1060 = new CompilerReferenceWarning(@"Use of possibly unassigned field 'name'. Struct instance variables are initially unassigned if struct is unassigned.", 1, 1060);
                return _CS1060;
            }
        }
        private static ICompilerReferenceWarning _CS1060;
        public static ICompilerReferenceWarning CS1522
        {
            get
            {
                if (_CS1522 == null)
                    _CS1522 = new CompilerReferenceWarning(@"Empty switch block", 1, 1522);
                return _CS1522;
            }
        }
        private static ICompilerReferenceWarning _CS1522;
        public static ICompilerReferenceWarning CS1570
        {
            get
            {
                if (_CS1570 == null)
                    _CS1570 = new CompilerReferenceWarning(@"XML comment on {0} has badly formed XML — {1}", 1, 1570);
                return _CS1570;
            }
        }
        private static ICompilerReferenceWarning _CS1570;
        public static ICompilerReferenceWarning CS1571
        {
            get
            {
                if (_CS1571 == null)
                    _CS1571 = new CompilerReferenceWarning(@"XML comment on {0} has a duplicate param tag for {1}", 2, 1571);
                return _CS1571;
            }
        }
        private static ICompilerReferenceWarning _CS1571;
        public static ICompilerReferenceWarning CS1572
        {
            get
            {
                if (_CS1572 == null)
                    _CS1572 = new CompilerReferenceWarning(@"XML comment on {0} has a param tag for {1}, but there is no parameter by that name", 2, 1572);
                return _CS1572;
            }
        }
        private static ICompilerReferenceWarning _CS1572;
        public static ICompilerReferenceWarning CS1573
        {
            get
            {
                if (_CS1573 == null)
                    _CS1573 = new CompilerReferenceWarning(@"Parameter {0} has no matching param tag in the XML comment for {0} (but other parameters do)", 4, 1573);
                return _CS1573;
            }
        }
        private static ICompilerReferenceWarning _CS1573;
        public static ICompilerReferenceWarning CS1574
        {
            get
            {
                if (_CS1574 == null)
                    _CS1574 = new CompilerReferenceWarning(@"XML comment on {0} has syntactically incorrect cref attribute {1}", 1, 1574);
                return _CS1574;
            }
        }
        private static ICompilerReferenceWarning _CS1574;
        public static ICompilerReferenceWarning CS1580
        {
            get
            {
                if (_CS1580 == null)
                    _CS1580 = new CompilerReferenceWarning(@"Invalid type for parameter {0} in XML comment cref attribute", 1, 1580);
                return _CS1580;
            }
        }
        private static ICompilerReferenceWarning _CS1580;
        public static ICompilerReferenceWarning CS1581
        {
            get
            {
                if (_CS1581 == null)
                    _CS1581 = new CompilerReferenceWarning(@"Invalid return type in XML comment cref attribute", 1, 1581);
                return _CS1581;
            }
        }
        private static ICompilerReferenceWarning _CS1581;
        public static ICompilerReferenceWarning CS1584
        {
            get
            {
                if (_CS1584 == null)
                    _CS1584 = new CompilerReferenceWarning(@"XML comment on {0} has syntactically incorrect cref attribute {1}", 1, 1584);
                return _CS1584;
            }
        }
        private static ICompilerReferenceWarning _CS1584;
        public static ICompilerReferenceWarning CS1587
        {
            get
            {
                if (_CS1587 == null)
                    _CS1587 = new CompilerReferenceWarning(@"XML comment is not placed on a valid language element", 2, 1587);
                return _CS1587;
            }
        }
        private static ICompilerReferenceWarning _CS1587;
        public static ICompilerReferenceWarning CS1589
        {
            get
            {
                if (_CS1589 == null)
                    _CS1589 = new CompilerReferenceWarning(@"Unable to include XML fragment {0} of file {1} -- {1}", 1, 1589);
                return _CS1589;
            }
        }
        private static ICompilerReferenceWarning _CS1589;
        public static ICompilerReferenceWarning CS1590
        {
            get
            {
                if (_CS1590 == null)
                    _CS1590 = new CompilerReferenceWarning(@"Invalid XML include element -- Missing file attribute", 1, 1590);
                return _CS1590;
            }
        }
        private static ICompilerReferenceWarning _CS1590;
        public static ICompilerReferenceWarning CS1591
        {
            get
            {
                if (_CS1591 == null)
                    _CS1591 = new CompilerReferenceWarning(@"Missing XML comment for publicly visible type or member {0}", 4, 1591);
                return _CS1591;
            }
        }
        private static ICompilerReferenceWarning _CS1591;
        public static ICompilerReferenceWarning CS1592
        {
            get
            {
                if (_CS1592 == null)
                    _CS1592 = new CompilerReferenceWarning(@"Badly formed XML in included comments file -- {0}", 1, 1592);
                return _CS1592;
            }
        }
        private static ICompilerReferenceWarning _CS1592;
        public static ICompilerReferenceWarning CS1598
        {
            get
            {
                if (_CS1598 == null)
                    _CS1598 = new CompilerReferenceWarning(@"XML parser could not be loaded for the following reason: {0}. The XML documentation file {1} will not be generated.", 1, 1598);
                return _CS1598;
            }
        }
        private static ICompilerReferenceWarning _CS1598;
        public static ICompilerReferenceWarning CS1607
        {
            get
            {
                if (_CS1607 == null)
                    _CS1607 = new CompilerReferenceWarning(@"Assembly generation -- {0}", 1, 1607);
                return _CS1607;
            }
        }
        private static ICompilerReferenceWarning _CS1607;
        public static ICompilerReferenceWarning CS1610
        {
            get
            {
                if (_CS1610 == null)
                    _CS1610 = new CompilerReferenceWarning(@"Unable to delete temporary file {0} used for default Win32 resource -- {1}", 4, 1610);
                return _CS1610;
            }
        }
        private static ICompilerReferenceWarning _CS1610;
        public static ICompilerReferenceWarning CS1616
        {
            get
            {
                if (_CS1616 == null)
                    _CS1616 = new CompilerReferenceWarning(@"Option {0} overrides attribute {1} given in a source file or added module", 1, 1616);
                return _CS1616;
            }
        }
        private static ICompilerReferenceWarning _CS1616;
        public static ICompilerReferenceWarning CS1633
        {
            get
            {
                if (_CS1633 == null)
                    _CS1633 = new CompilerReferenceWarning(@"Unrecognized #pragma directive", 1, 1633);
                return _CS1633;
            }
        }
        private static ICompilerReferenceWarning _CS1633;
        public static ICompilerReferenceWarning CS1634
        {
            get
            {
                if (_CS1634 == null)
                    _CS1634 = new CompilerReferenceWarning(@"Expected disable or restore", 1, 1634);
                return _CS1634;
            }
        }
        private static ICompilerReferenceWarning _CS1634;
        public static ICompilerReferenceWarning CS1635
        {
            get
            {
                if (_CS1635 == null)
                    _CS1635 = new CompilerReferenceWarning(@"Cannot restore warning {0} because it was disabled globally", 1, 1635);
                return _CS1635;
            }
        }
        private static ICompilerReferenceWarning _CS1635;
        public static ICompilerReferenceWarning CS1645
        {
            get
            {
                if (_CS1645 == null)
                    _CS1645 = new CompilerReferenceWarning(@"Feature {0} is not part of the standardized ISO C# language specification, and may not be accepted by other compilers", 1, 1645);
                return _CS1645;
            }
        }
        private static ICompilerReferenceWarning _CS1645;
        public static ICompilerReferenceWarning CS1658
        {
            get
            {
                if (_CS1658 == null)
                    _CS1658 = new CompilerReferenceWarning(@"{0}. See also error: {1}", 1, 1658);
                return _CS1658;
            }
        }
        private static ICompilerReferenceWarning _CS1658;
        public static ICompilerReferenceWarning CS1668
        {
            get
            {
                if (_CS1668 == null)
                    _CS1668 = new CompilerReferenceWarning(@"Invalid search path 'path' specified in {0} -- {1}", 2, 1668);
                return _CS1668;
            }
        }
        private static ICompilerReferenceWarning _CS1668;
        public static ICompilerReferenceWarning CS1682
        {
            get
            {
                if (_CS1682 == null)
                    _CS1682 = new CompilerReferenceWarning(@"Reference to type {0} claims it is nested within {1}, but it could not be found", 1, 1682);
                return _CS1682;
            }
        }
        private static ICompilerReferenceWarning _CS1682;
        public static ICompilerReferenceWarning CS1683
        {
            get
            {
                if (_CS1683 == null)
                    _CS1683 = new CompilerReferenceWarning(@"Reference to type {0} claims it is defined in this assembly, but it is not defined in source or any added modules", 1, 1683);
                return _CS1683;
            }
        }
        private static ICompilerReferenceWarning _CS1683;
        public static ICompilerReferenceWarning CS1684
        {
            get
            {
                if (_CS1684 == null)
                    _CS1684 = new CompilerReferenceWarning(@"Reference to type {0} claims it is defined in {1}, but it could not be found", 1, 1684);
                return _CS1684;
            }
        }
        private static ICompilerReferenceWarning _CS1684;
        //System variation of CS0436
        public static ICompilerReferenceWarning CS1685
        {
            get
            {
                if (_CS1685 == null)
                    _CS1685 = new CompilerReferenceWarning(@"The predefined type {0} is defined in multiple assemblies in the global alias; using definition from {1}", 1, 1685);
                return _CS1685;
            }
        }
        private static ICompilerReferenceWarning _CS1685;
        public static ICompilerReferenceWarning CS1687
        {
            get
            {
                if (_CS1687 == null)
                    _CS1687 = new CompilerReferenceWarning(@"Source file has exceeded the limit of 16,707,565 lines representable in the PDB, debug information will be incorrect", 1, 1687);
                return _CS1687;
            }
        }
        private static ICompilerReferenceWarning _CS1687;
        public static ICompilerReferenceWarning CS1690
        {
            get
            {
                if (_CS1690 == null)
                    _CS1690 = new CompilerReferenceWarning(@"Accessing a member on {0} may cause a runtime exception because it is a field of a marshal-by-reference class", 1, 1690);
                return _CS1690;
            }
        }
        private static ICompilerReferenceWarning _CS1690;
        public static ICompilerReferenceWarning CS1691
        {
            get
            {
                if (_CS1691 == null)
                    _CS1691 = new CompilerReferenceWarning(@"{0} is not a valid warning number", 1, 1691);
                return _CS1691;
            }
        }
        private static ICompilerReferenceWarning _CS1691;
        public static ICompilerReferenceWarning CS1692
        {
            get
            {
                if (_CS1692 == null)
                    _CS1692 = new CompilerReferenceWarning(@"Invalid number", 1, 1692);
                return _CS1692;
            }
        }
        private static ICompilerReferenceWarning _CS1692;
        public static ICompilerReferenceWarning CS1694
        {
            get
            {
                if (_CS1694 == null)
                    _CS1694 = new CompilerReferenceWarning(@"Invalid filename specified for preprocessor directive. Filename is too long or not a valid filename.", 1, 1694);
                return _CS1694;
            }
        }
        private static ICompilerReferenceWarning _CS1694;
        public static ICompilerReferenceWarning CS1695
        {
            get
            {
                if (_CS1695 == null)
                    _CS1695 = new CompilerReferenceWarning(@"Invalid #pragma checksum syntax; should be #pragma checksum ""filename"" ""{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}"" ""XXXX...""", 1, 1695);
                return _CS1695;
            }
        }
        private static ICompilerReferenceWarning _CS1695;
        public static ICompilerReferenceWarning CS1696
        {
            get
            {
                if (_CS1696 == null)
                    _CS1696 = new CompilerReferenceWarning(@"Single-line comment or end-of-line expected", 1, 1696);
                return _CS1696;
            }
        }
        private static ICompilerReferenceWarning _CS1696;
        public static ICompilerReferenceWarning CS1697
        {
            get
            {
                if (_CS1697 == null)
                    _CS1697 = new CompilerReferenceWarning(@"Different checksum values given for {0}", 1, 1697);
                return _CS1697;
            }
        }
        private static ICompilerReferenceWarning _CS1697;
        public static ICompilerReferenceWarning CS1698
        {
            get
            {
                if (_CS1698 == null)
                    _CS1698 = new CompilerReferenceWarning(@"Circular assembly reference {0} does not match the output assembly name {1}. Try adding a reference to {0} or changing the output assembly name to match.", 2, 1698);
                return _CS1698;
            }
        }
        private static ICompilerReferenceWarning _CS1698;
        public static ICompilerReferenceWarning CS1699
        {
            get
            {
                if (_CS1699 == null)
                    _CS1699 = new CompilerReferenceWarning(@"Use command line option {0} or appropriate project settings instead of {1}", 1, 1699);
                return _CS1699;
            }
        }
        private static ICompilerReferenceWarning _CS1699;
        public static ICompilerReferenceWarning CS1700
        {
            get
            {
                if (_CS1700 == null)
                    _CS1700 = new CompilerReferenceWarning(@"Assembly reference Assembly Name is invalid and cannot be resolved", 3, 1700);
                return _CS1700;
            }
        }
        private static ICompilerReferenceWarning _CS1700;
        public static ICompilerReferenceWarning CS1701
        {
            get
            {
                if (_CS1701 == null)
                    _CS1701 = new CompilerReferenceWarning(@"Assuming assembly reference {0} matches {1}, you may need to supply runtime policy ", 2, 1701);
                return _CS1701;
            }
        }
        private static ICompilerReferenceWarning _CS1701;
        public static ICompilerReferenceWarning CS1702
        {
            get
            {
                if (_CS1702 == null)
                    _CS1702 = new CompilerReferenceWarning(@"Assuming assembly reference {0} matches {1}, you may need to supply runtime policy", 3, 1702);
                return _CS1702;
            }
        }
        private static ICompilerReferenceWarning _CS1702;
        public static ICompilerReferenceWarning CS1707
        {
            get
            {
                if (_CS1707 == null)
                    _CS1707 = new CompilerReferenceWarning(@"Delegate {0} bound to {1} instead of {2} because of new language rules", 1, 1707);
                return _CS1707;
            }
        }
        private static ICompilerReferenceWarning _CS1707;
        public static ICompilerReferenceWarning CS1709
        {
            get
            {
                if (_CS1709 == null)
                    _CS1709 = new CompilerReferenceWarning(@"Filename specified for preprocessor directive is empty", 1, 1709);
                return _CS1709;
            }
        }
        private static ICompilerReferenceWarning _CS1709;
        public static ICompilerReferenceWarning CS1710
        {
            get
            {
                if (_CS1710 == null)
                    _CS1710 = new CompilerReferenceWarning(@"XML comment on {0} has a duplicate typeparam tag for {1}", 2, 1710);
                return _CS1710;
            }
        }
        private static ICompilerReferenceWarning _CS1710;
        public static ICompilerReferenceWarning CS1711
        {
            get
            {
                if (_CS1711 == null)
                    _CS1711 = new CompilerReferenceWarning(@"XML comment on {0} has a typeparam tag for {1}, but there is no type parameter by that name", 2, 1711);
                return _CS1711;
            }
        }
        private static ICompilerReferenceWarning _CS1711;
        public static ICompilerReferenceWarning CS1712
        {
            get
            {
                if (_CS1712 == null)
                    _CS1712 = new CompilerReferenceWarning(@"Type parameter {0} has no matching typeparam tag in the XML comment on {1} (but other type parameters do)", 4, 1712);
                return _CS1712;
            }
        }
        private static ICompilerReferenceWarning _CS1712;
        public static ICompilerReferenceWarning CS1717
        {
            get
            {
                if (_CS1717 == null)
                    _CS1717 = new CompilerReferenceWarning(@"Assignment made to same variable; did you mean to assign something else?", 3, 1717);
                return _CS1717;
            }
        }
        private static ICompilerReferenceWarning _CS1717;
        public static ICompilerReferenceWarning CS1718
        {
            get
            {
                if (_CS1718 == null)
                    _CS1718 = new CompilerReferenceWarning(@"Comparison made to same variable; did you mean to compare something else?", 3, 1718);
                return _CS1718;
            }
        }
        private static ICompilerReferenceWarning _CS1718;
        public static ICompilerReferenceWarning CS1720
        {
            get
            {
                if (_CS1720 == null)
                    _CS1720 = new CompilerReferenceWarning(@"Expression will always cause a System.NullReferenceException because the default value of {0} is null", 1, 1720);
                return _CS1720;
            }
        }
        private static ICompilerReferenceWarning _CS1720;
        public static ICompilerReferenceWarning CS1723
        {
            get
            {
                if (_CS1723 == null)
                    _CS1723 = new CompilerReferenceWarning(@"XML comment on {0} has cref attribute {1} that refers to a type parameter", 1, 1723);
                return _CS1723;
            }
        }
        private static ICompilerReferenceWarning _CS1723;
        public static ICompilerReferenceWarning CS1911
        {
            get
            {
                if (_CS1911 == null)
                    _CS1911 = new CompilerReferenceWarning(@"Access to member {0} through a 'base' keyword from an anonymous method, lambda expression, query expression, or iterator results in unverifiable code. Consider moving the access into a helper method on the containing type.", 1, 1911);
                return _CS1911;
            }
        }
        private static ICompilerReferenceWarning _CS1911;
        public static ICompilerReferenceWarning CS1927
        {
            get
            {
                if (_CS1927 == null)
                    _CS1927 = new CompilerReferenceWarning(@"Ignoring /win32manifest for module because it only applies to assemblies.", 2, 1927);
                return _CS1927;
            }
        }
        private static ICompilerReferenceWarning _CS1927;
        public static ICompilerReferenceWarning CS1956
        {
            get
            {
                if (_CS1956 == null)
                    _CS1956 = new CompilerReferenceWarning(@"Member {0} implements interface member {0} in type {1}. There are multiple matches for the interface member at run-time. It is implementation dependent which method will be called.", 1, 1956);
                return _CS1956;
            }
        }
        private static ICompilerReferenceWarning _CS1956;
        public static ICompilerReferenceWarning CS1957
        {
            get
            {
                if (_CS1957 == null)
                    _CS1957 = new CompilerReferenceWarning(@"Member {0} overrides {1}. There are multiple override candidates at run-time. It is implementation dependent which method will be called.", 1, 1957);
                return _CS1957;
            }
        }
        private static ICompilerReferenceWarning _CS1957;
        public static ICompilerReferenceWarning CS2002
        {
            get
            {
                if (_CS2002 == null)
                    _CS2002 = new CompilerReferenceWarning(@"Source file {0} specified multiple times", 1, 2002);
                return _CS2002;
            }
        }
        private static ICompilerReferenceWarning _CS2002;
        public static ICompilerReferenceWarning CS2014
        {
            get
            {
                if (_CS2014 == null)
                    _CS2014 = new CompilerReferenceWarning(@"Compiler option {0} is obsolete, please use {1} instead", 1, 2014);
                return _CS2014;
            }
        }
        private static ICompilerReferenceWarning _CS2014;
        public static ICompilerReferenceWarning CS2023
        {
            get
            {
                if (_CS2023 == null)
                    _CS2023 = new CompilerReferenceWarning(@"Ignoring /noconfig option because it was specified in a response file", 1, 2023);
                return _CS2023;
            }
        }
        private static ICompilerReferenceWarning _CS2023;
        public static ICompilerReferenceWarning CS2029
        {
            get
            {
                if (_CS2029 == null)
                    _CS2029 = new CompilerReferenceWarning(@"Invalid value for '/define'; {0} is not a valid identifier", 1, 2029);
                return _CS2029;
            }
        }
        private static ICompilerReferenceWarning _CS2029;
        public static ICompilerReferenceWarning CS3000
        {
            get
            {
                if (_CS3000 == null)
                    _CS3000 = new CompilerReferenceWarning(@"Methods with variable arguments are not CLS-compliant", 1, 3000);
                return _CS3000;
            }
        }
        private static ICompilerReferenceWarning _CS3000;
        public static ICompilerReferenceWarning CS3001
        {
            get
            {
                if (_CS3001 == null)
                    _CS3001 = new CompilerReferenceWarning(@"Argument type {0} is not CLS-compliant", 1, 3001);
                return _CS3001;
            }
        }
        private static ICompilerReferenceWarning _CS3001;
        public static ICompilerReferenceWarning CS3002
        {
            get
            {
                if (_CS3002 == null)
                    _CS3002 = new CompilerReferenceWarning(@"Return type of {0} is not CLS-compliant", 1, 3002);
                return _CS3002;
            }
        }
        private static ICompilerReferenceWarning _CS3002;
        public static ICompilerReferenceWarning CS3003
        {
            get
            {
                if (_CS3003 == null)
                    _CS3003 = new CompilerReferenceWarning(@"Type of {0} is not CLS-compliant", 1, 3003);
                return _CS3003;
            }
        }
        private static ICompilerReferenceWarning _CS3003;
        public static ICompilerReferenceWarning CS3004
        {
            get
            {
                if (_CS3004 == null)
                    _CS3004 = new CompilerReferenceWarning(@"Mixed and decomposed Unicode characters are not CLS-compliant", 1, 3004);
                return _CS3004;
            }
        }
        private static ICompilerReferenceWarning _CS3004;
        public static ICompilerReferenceWarning CS3005
        {
            get
            {
                if (_CS3005 == null)
                    _CS3005 = new CompilerReferenceWarning(@"Identifier {0} differing only in case is not CLS-compliant", 1, 3005);
                return _CS3005;
            }
        }
        private static ICompilerReferenceWarning _CS3005;
        public static ICompilerReferenceWarning CS3006
        {
            get
            {
                if (_CS3006 == null)
                    _CS3006 = new CompilerReferenceWarning(@"Overloaded method {0} differing only in ref or out, or in array rank, is not CLS-compliant", 1, 3006);
                return _CS3006;
            }
        }
        private static ICompilerReferenceWarning _CS3006;
        public static ICompilerReferenceWarning CS3007
        {
            get
            {
                if (_CS3007 == null)
                    _CS3007 = new CompilerReferenceWarning(@"Overloaded method {0} differing only by unnamed array types is not CLS-compliant", 1, 3007);
                return _CS3007;
            }
        }
        private static ICompilerReferenceWarning _CS3007;
        public static ICompilerReferenceWarning CS3008
        {
            get
            {
                if (_CS3008 == null)
                    _CS3008 = new CompilerReferenceWarning(@"Identifier {0} differing only in case is not CLS-compliant", 1, 3008);
                return _CS3008;
            }
        }
        private static ICompilerReferenceWarning _CS3008;
        public static ICompilerReferenceWarning CS3009
        {
            get
            {
                if (_CS3009 == null)
                    _CS3009 = new CompilerReferenceWarning(@"{0}: base type {0} is not CLS-compliant", 1, 3009);
                return _CS3009;
            }
        }
        private static ICompilerReferenceWarning _CS3009;
        public static ICompilerReferenceWarning CS3010
        {
            get
            {
                if (_CS3010 == null)
                    _CS3010 = new CompilerReferenceWarning(@"{0}: CLS-compliant interfaces must have only CLS-compliant members", 1, 3010);
                return _CS3010;
            }
        }
        private static ICompilerReferenceWarning _CS3010;
        public static ICompilerReferenceWarning CS3011
        {
            get
            {
                if (_CS3011 == null)
                    _CS3011 = new CompilerReferenceWarning(@"{0}: only CLS-compliant members can be abstract", 1, 3011);
                return _CS3011;
            }
        }
        private static ICompilerReferenceWarning _CS3011;
        public static ICompilerReferenceWarning CS3012
        {
            get
            {
                if (_CS3012 == null)
                    _CS3012 = new CompilerReferenceWarning(@"You cannot specify the CLSCompliant attribute on a module that differs from the CLSCompliant attribute on the assembly", 1, 3012);
                return _CS3012;
            }
        }
        private static ICompilerReferenceWarning _CS3012;
        public static ICompilerReferenceWarning CS3013
        {
            get
            {
                if (_CS3013 == null)
                    _CS3013 = new CompilerReferenceWarning(@"Added modules must be marked with the CLSCompliant attribute to match the assembly", 1, 3013);
                return _CS3013;
            }
        }
        private static ICompilerReferenceWarning _CS3013;
        public static ICompilerReferenceWarning CS3014
        {
            get
            {
                if (_CS3014 == null)
                    _CS3014 = new CompilerReferenceWarning(@"{0} does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute", 1, 3014);
                return _CS3014;
            }
        }
        private static ICompilerReferenceWarning _CS3014;
        public static ICompilerReferenceWarning CS3015
        {
            get
            {
                if (_CS3015 == null)
                    _CS3015 = new CompilerReferenceWarning(@"{0} has no accessible constructors which use only CLS-compliant types", 1, 3015);
                return _CS3015;
            }
        }
        private static ICompilerReferenceWarning _CS3015;
        public static ICompilerReferenceWarning CS3016
        {
            get
            {
                if (_CS3016 == null)
                    _CS3016 = new CompilerReferenceWarning(@"Arrays as attribute arguments is not CLS-compliant", 1, 3016);
                return _CS3016;
            }
        }
        private static ICompilerReferenceWarning _CS3016;
        public static ICompilerReferenceWarning CS3017
        {
            get
            {
                if (_CS3017 == null)
                    _CS3017 = new CompilerReferenceWarning(@"You cannot specify the CLSCompliant attribute on a module that differs from the CLSCompliant attribute on the assembly", 1, 3017);
                return _CS3017;
            }
        }
        private static ICompilerReferenceWarning _CS3017;
        public static ICompilerReferenceWarning CS3018
        {
            get
            {
                if (_CS3018 == null)
                    _CS3018 = new CompilerReferenceWarning(@"{0} cannot be marked as CLS-Compliant because it is a member of non CLS-compliant type {1}", 1, 3018);
                return _CS3018;
            }
        }
        private static ICompilerReferenceWarning _CS3018;
        public static ICompilerReferenceWarning CS3019
        {
            get
            {
                if (_CS3019 == null)
                    _CS3019 = new CompilerReferenceWarning(@"CLS compliance checking will not be performed on {0} because it is not visible from outside this assembly.", 2, 3019);
                return _CS3019;
            }
        }
        private static ICompilerReferenceWarning _CS3019;
        public static ICompilerReferenceWarning CS3021
        {
            get
            {
                if (_CS3021 == null)
                    _CS3021 = new CompilerReferenceWarning(@"{0} does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute", 2, 3021);
                return _CS3021;
            }
        }
        private static ICompilerReferenceWarning _CS3021;
        public static ICompilerReferenceWarning CS3022
        {
            get
            {
                if (_CS3022 == null)
                    _CS3022 = new CompilerReferenceWarning(@"CLSCompliant attribute has no meaning when applied to parameters. Try putting it on the method instead.", 1, 3022);
                return _CS3022;
            }
        }
        private static ICompilerReferenceWarning _CS3022;
        public static ICompilerReferenceWarning CS3023
        {
            get
            {
                if (_CS3023 == null)
                    _CS3023 = new CompilerReferenceWarning(@"CLSCompliant attribute has no meaning when applied to return types. Try putting it on the method instead.", 1, 3023);
                return _CS3023;
            }
        }
        private static ICompilerReferenceWarning _CS3023;
        public static ICompilerReferenceWarning CS3026
        {
            get
            {
                if (_CS3026 == null)
                    _CS3026 = new CompilerReferenceWarning(@"CLS-compliant field {0} cannot be volatile", 1, 3026);
                return _CS3026;
            }
        }
        private static ICompilerReferenceWarning _CS3026;
        public static ICompilerReferenceWarning CS3027
        {
            get
            {
                if (_CS3027 == null)
                    _CS3027 = new CompilerReferenceWarning(@"{0} is not CLS-compliant because base interface {1} is not CLS-compliant", 1, 3027);
                return _CS3027;
            }
        }
        private static ICompilerReferenceWarning _CS3027;
        public static ICompilerReferenceWarning CS5000
        {
            get
            {
                if (_CS5000 == null)
                    _CS5000 = new CompilerReferenceWarning(@"Unknown compiler option {0}", 1, 5000);
                return _CS5000;
            }
        }
        private static ICompilerReferenceWarning _CS5000;
        public static ICompilerReferenceError CS0001
        {
            get
            {
                if (_CS0001 == null)
                    _CS0001 = new CompilerReferenceError(@"Internal compiler error", 1);
                return _CS0001;
            }
        }
        private static ICompilerReferenceError _CS0001;
        public static ICompilerReferenceError CS0003
        {
            get
            {
                if (_CS0003 == null)
                    _CS0003 = new CompilerReferenceError(@"Out of memory", 3);
                return _CS0003;
            }
        }
        private static ICompilerReferenceError _CS0003;
        public static ICompilerReferenceError CS0004
        {
            get
            {
                if (_CS0004 == null)
                    _CS0004 = new CompilerReferenceError(@"Warning treated as error", 4);
                return _CS0004;
            }
        }
        private static ICompilerReferenceError _CS0004;
        public static ICompilerReferenceError CS0005
        {
            get
            {
                if (_CS0005 == null)
                    _CS0005 = new CompilerReferenceError(@"Compiler option {0} must be followed by an argument", 5);
                return _CS0005;
            }
        }
        private static ICompilerReferenceError _CS0005;
        public static ICompilerReferenceError CS0006
        {
            get
            {
                if (_CS0006 == null)
                    _CS0006 = new CompilerReferenceError(@"Metadata file {0} could not be found", 6);
                return _CS0006;
            }
        }
        private static ICompilerReferenceError _CS0006;
        public static ICompilerReferenceError CS0007
        {
            get
            {
                if (_CS0007 == null)
                    _CS0007 = new CompilerReferenceError(@"Unexpected common language runtime initialization error — {0}", 7);
                return _CS0007;
            }
        }
        private static ICompilerReferenceError _CS0007;
        public static ICompilerReferenceError CS0008
        {
            get
            {
                if (_CS0008 == null)
                    _CS0008 = new CompilerReferenceError(@"Unexpected error reading metadata from file 'file' — {0}", 8);
                return _CS0008;
            }
        }
        private static ICompilerReferenceError _CS0008;
        public static ICompilerReferenceError CS0009
        {
            get
            {
                if (_CS0009 == null)
                    _CS0009 = new CompilerReferenceError(@"Metadata file {0} could not be opened — {1}", 9);
                return _CS0009;
            }
        }
        private static ICompilerReferenceError _CS0009;
        public static ICompilerReferenceError CS0010
        {
            get
            {
                if (_CS0010 == null)
                    _CS0010 = new CompilerReferenceError(@"Unexpected fatal error -- {0}.", 10);
                return _CS0010;
            }
        }
        private static ICompilerReferenceError _CS0010;
        public static ICompilerReferenceError CS0011
        {
            get
            {
                if (_CS0011 == null)
                    _CS0011 = new CompilerReferenceError(@"The base class or interface {0} in assembly {1} referenced by type {2} could not be resolved", 11);
                return _CS0011;
            }
        }
        private static ICompilerReferenceError _CS0011;
        public static ICompilerReferenceError CS0012
        {
            get
            {
                if (_CS0012 == null)
                    _CS0012 = new CompilerReferenceError(@"The type {0} is defined in an assembly that is not referenced. You must add a reference to assembly {1}.", 12);
                return _CS0012;
            }
        }
        private static ICompilerReferenceError _CS0012;
        public static ICompilerReferenceError CS0013
        {
            get
            {
                if (_CS0013 == null)
                    _CS0013 = new CompilerReferenceError(@"Unexpected error writing metadata to file {0} -- {1}", 13);
                return _CS0013;
            }
        }
        private static ICompilerReferenceError _CS0013;
        public static ICompilerReferenceError CS0014
        {
            get
            {
                if (_CS0014 == null)
                    _CS0014 = new CompilerReferenceError(@"Required file {0} could not be found", 14);
                return _CS0014;
            }
        }
        private static ICompilerReferenceError _CS0014;
        public static ICompilerReferenceError CS0015
        {
            get
            {
                if (_CS0015 == null)
                    _CS0015 = new CompilerReferenceError(@"The name of type {0} is too long", 15);
                return _CS0015;
            }
        }
        private static ICompilerReferenceError _CS0015;
        public static ICompilerReferenceError CS0016
        {
            get
            {
                if (_CS0016 == null)
                    _CS0016 = new CompilerReferenceError(@"Could not write to output file {0} — {1}", 16);
                return _CS0016;
            }
        }
        private static ICompilerReferenceError _CS0016;
        public static ICompilerReferenceError CS0017
        {
            get
            {
                if (_CS0017 == null)
                    _CS0017 = new CompilerReferenceError(@"Program {0} has more than one entry point defined. Compile with /main to specify the type that contains the entry point.", 17);
                return _CS0017;
            }
        }
        private static ICompilerReferenceError _CS0017;
        public static ICompilerReferenceError CS0019
        {
            get
            {
                if (_CS0019 == null)
                    _CS0019 = new CompilerReferenceError(@"Operator {0} cannot be applied to operands of type {1} and {2}", 19);
                return _CS0019;
            }
        }
        private static ICompilerReferenceError _CS0019;
        public static ICompilerReferenceError CS0020
        {
            get
            {
                if (_CS0020 == null)
                    _CS0020 = new CompilerReferenceError(@"Division by constant zero", 20);
                return _CS0020;
            }
        }
        private static ICompilerReferenceError _CS0020;
        public static ICompilerReferenceError CS0021
        {
            get
            {
                if (_CS0021 == null)
                    _CS0021 = new CompilerReferenceError(@"Cannot apply indexing with [] to an expression of type {0}", 21);
                return _CS0021;
            }
        }
        private static ICompilerReferenceError _CS0021;
        public static ICompilerReferenceError CS0022
        {
            get
            {
                if (_CS0022 == null)
                    _CS0022 = new CompilerReferenceError(@"Wrong number of indices inside [], expected {0}", 22);
                return _CS0022;
            }
        }
        private static ICompilerReferenceError _CS0022;
        public static ICompilerReferenceError CS0023
        {
            get
            {
                if (_CS0023 == null)
                    _CS0023 = new CompilerReferenceError(@"Operator {0} cannot be applied to operand of type {1}", 23);
                return _CS0023;
            }
        }
        private static ICompilerReferenceError _CS0023;
        public static ICompilerReferenceError CS0025
        {
            get
            {
                if (_CS0025 == null)
                    _CS0025 = new CompilerReferenceError(@"Standard library file {0} could not be found", 25);
                return _CS0025;
            }
        }
        private static ICompilerReferenceError _CS0025;
        public static ICompilerReferenceError CS0026
        {
            get
            {
                if (_CS0026 == null)
                    _CS0026 = new CompilerReferenceError(@"Keyword 'this' is not valid in a static property, static method, or static field initializer", 26);
                return _CS0026;
            }
        }
        private static ICompilerReferenceError _CS0026;
        public static ICompilerReferenceError CS0027
        {
            get
            {
                if (_CS0027 == null)
                    _CS0027 = new CompilerReferenceError(@"Keyword 'this' is not available in the current context", 27);
                return _CS0027;
            }
        }
        private static ICompilerReferenceError _CS0027;
        public static ICompilerReferenceError CS0029
        {
            get
            {
                if (_CS0029 == null)
                    _CS0029 = new CompilerReferenceError(@"Cannot implicitly convert type {0} to {1}", 29);
                return _CS0029;
            }
        }
        private static ICompilerReferenceError _CS0029;
        public static ICompilerReferenceError CS0030
        {
            get
            {
                if (_CS0030 == null)
                    _CS0030 = new CompilerReferenceError(@"Cannot convert type {0} to {1}", 30);
                return _CS0030;
            }
        }
        private static ICompilerReferenceError _CS0030;
        public static ICompilerReferenceError CS0031
        {
            get
            {
                if (_CS0031 == null)
                    _CS0031 = new CompilerReferenceError(@"Constant value {0} cannot be converted to a {1}. (use 'unchecked' syntax to override)", 31);
                return _CS0031;
            }
        }
        private static ICompilerReferenceError _CS0031;
        public static ICompilerReferenceError CS0034
        {
            get
            {
                if (_CS0034 == null)
                    _CS0034 = new CompilerReferenceError(@"Operator {0} is ambiguous on operands of type {1} and {2}", 34);
                return _CS0034;
            }
        }
        private static ICompilerReferenceError _CS0034;
        public static ICompilerReferenceError CS0035
        {
            get
            {
                if (_CS0035 == null)
                    _CS0035 = new CompilerReferenceError(@"Operator {0} is ambiguous on an operand of type {1}", 35);
                return _CS0035;
            }
        }
        private static ICompilerReferenceError _CS0035;
        public static ICompilerReferenceError CS0036
        {
            get
            {
                if (_CS0036 == null)
                    _CS0036 = new CompilerReferenceError(@"An out parameter cannot have the '[In]' attribute", 36);
                return _CS0036;
            }
        }
        private static ICompilerReferenceError _CS0036;
        public static ICompilerReferenceError CS0037
        {
            get
            {
                if (_CS0037 == null)
                    _CS0037 = new CompilerReferenceError(@"Cannot convert null to {0} because it is a non-nullable value type", 37);
                return _CS0037;
            }
        }
        private static ICompilerReferenceError _CS0037;
        public static ICompilerReferenceError CS0038
        {
            get
            {
                if (_CS0038 == null)
                    _CS0038 = new CompilerReferenceError(@"Cannot access a nonstatic member of outer type {0} via nested type {1}", 38);
                return _CS0038;
            }
        }
        private static ICompilerReferenceError _CS0038;
        public static ICompilerReferenceError CS0039
        {
            get
            {
                if (_CS0039 == null)
                    _CS0039 = new CompilerReferenceError(@"Cannot convert type {0} to {1} via a reference conversion, boxing conversion, unboxing conversion, wrapping conversion, or null type conversion", 39);
                return _CS0039;
            }
        }
        private static ICompilerReferenceError _CS0039;
        public static ICompilerReferenceError CS0040
        {
            get
            {
                if (_CS0040 == null)
                    _CS0040 = new CompilerReferenceError(@"Unexpected error creating debug information file — {0}", 40);
                return _CS0040;
            }
        }
        private static ICompilerReferenceError _CS0040;
        public static ICompilerReferenceError CS0041
        {
            get
            {
                if (_CS0041 == null)
                    _CS0041 = new CompilerReferenceError(@"The fully qualified name for {0} is too long for debug information. Compile without '/debug' option.", 41);
                return _CS0041;
            }
        }
        private static ICompilerReferenceError _CS0041;
        public static ICompilerReferenceError CS0042
        {
            get
            {
                if (_CS0042 == null)
                    _CS0042 = new CompilerReferenceError(@"Unexpected error creating debug information file {0} — {1}", 42);
                return _CS0042;
            }
        }
        private static ICompilerReferenceError _CS0042;
        public static ICompilerReferenceError CS0043
        {
            get
            {
                if (_CS0043 == null)
                    _CS0043 = new CompilerReferenceError(@"PDB file {0} has an incorrect or out-of-date format. Delete it and rebuild.", 43);
                return _CS0043;
            }
        }
        private static ICompilerReferenceError _CS0043;
        public static ICompilerReferenceError CS0050
        {
            get
            {
                if (_CS0050 == null)
                    _CS0050 = new CompilerReferenceError(@"Inconsistent accessibility: return type {0} is less accessible than method {1}", 50);
                return _CS0050;
            }
        }
        private static ICompilerReferenceError _CS0050;
        public static ICompilerReferenceError CS0051
        {
            get
            {
                if (_CS0051 == null)
                    _CS0051 = new CompilerReferenceError(@"Inconsistent accessibility: parameter type {0} is less accessible than method {1}", 51);
                return _CS0051;
            }
        }
        private static ICompilerReferenceError _CS0051;
        public static ICompilerReferenceError CS0052
        {
            get
            {
                if (_CS0052 == null)
                    _CS0052 = new CompilerReferenceError(@"Inconsistent accessibility: field type {0} is less accessible than field {1}", 52);
                return _CS0052;
            }
        }
        private static ICompilerReferenceError _CS0052;
        public static ICompilerReferenceError CS0053
        {
            get
            {
                if (_CS0053 == null)
                    _CS0053 = new CompilerReferenceError(@"Inconsistent accessibility: property type {0} is less accessible than property {1}", 53);
                return _CS0053;
            }
        }
        private static ICompilerReferenceError _CS0053;
        public static ICompilerReferenceError CS0054
        {
            get
            {
                if (_CS0054 == null)
                    _CS0054 = new CompilerReferenceError(@"Inconsistent accessibility: indexer return type {0} is less accessible than indexer {1}", 54);
                return _CS0054;
            }
        }
        private static ICompilerReferenceError _CS0054;
        public static ICompilerReferenceError CS0055
        {
            get
            {
                if (_CS0055 == null)
                    _CS0055 = new CompilerReferenceError(@"Inconsistent accessibility: parameter type {0} is less accessible than indexer {1}", 55);
                return _CS0055;
            }
        }
        private static ICompilerReferenceError _CS0055;
        public static ICompilerReferenceError CS0056
        {
            get
            {
                if (_CS0056 == null)
                    _CS0056 = new CompilerReferenceError(@"Inconsistent accessibility: return type {0} is less accessible than operator {1}", 56);
                return _CS0056;
            }
        }
        private static ICompilerReferenceError _CS0056;
        public static ICompilerReferenceError CS0057
        {
            get
            {
                if (_CS0057 == null)
                    _CS0057 = new CompilerReferenceError(@"Inconsistent accessibility: parameter type {0} is less accessible than operator {1}", 57);
                return _CS0057;
            }
        }
        private static ICompilerReferenceError _CS0057;
        public static ICompilerReferenceError CS0058
        {
            get
            {
                if (_CS0058 == null)
                    _CS0058 = new CompilerReferenceError(@"Inconsistent accessibility: return type {0} is less accessible than delegate {1}", 58);
                return _CS0058;
            }
        }
        private static ICompilerReferenceError _CS0058;
        public static ICompilerReferenceError CS0059
        {
            get
            {
                if (_CS0059 == null)
                    _CS0059 = new CompilerReferenceError(@"Inconsistent accessibility: parameter type {0} is less accessible than delegate {1}", 59);
                return _CS0059;
            }
        }
        private static ICompilerReferenceError _CS0059;
        public static ICompilerReferenceError CS0060
        {
            get
            {
                if (_CS0060 == null)
                    _CS0060 = new CompilerReferenceError(@"Inconsistent accessibility: base class {0} is less accessible than class {1}", 60);
                return _CS0060;
            }
        }
        private static ICompilerReferenceError _CS0060;
        public static ICompilerReferenceError CS0061
        {
            get
            {
                if (_CS0061 == null)
                    _CS0061 = new CompilerReferenceError(@"Inconsistent accessibility: base interface {0} is less accessible than interface {1}", 61);
                return _CS0061;
            }
        }
        private static ICompilerReferenceError _CS0061;
        public static ICompilerReferenceError CS0065
        {
            get
            {
                if (_CS0065 == null)
                    _CS0065 = new CompilerReferenceError(@"{0}: event property must have both add and remove accessors", 65);
                return _CS0065;
            }
        }
        private static ICompilerReferenceError _CS0065;
        public static ICompilerReferenceError CS0066
        {
            get
            {
                if (_CS0066 == null)
                    _CS0066 = new CompilerReferenceError(@"{0}: event must be of a delegate type", 66);
                return _CS0066;
            }
        }
        private static ICompilerReferenceError _CS0066;
        public static ICompilerReferenceError CS0068
        {
            get
            {
                if (_CS0068 == null)
                    _CS0068 = new CompilerReferenceError(@"{0}: event in interface cannot have initializer", 68);
                return _CS0068;
            }
        }
        private static ICompilerReferenceError _CS0068;
        public static ICompilerReferenceError CS0069
        {
            get
            {
                if (_CS0069 == null)
                    _CS0069 = new CompilerReferenceError(@"An event in an interface cannot have add or remove accessors", 69);
                return _CS0069;
            }
        }
        private static ICompilerReferenceError _CS0069;
        public static ICompilerReferenceError CS0070
        {
            get
            {
                if (_CS0070 == null)
                    _CS0070 = new CompilerReferenceError(@"The event {0} can only appear on the left hand side of += or -= (except when used from within the type {1})", 70);
                return _CS0070;
            }
        }
        private static ICompilerReferenceError _CS0070;
        public static ICompilerReferenceError CS0071
        {
            get
            {
                if (_CS0071 == null)
                    _CS0071 = new CompilerReferenceError(@"An explicit interface implementation of an event must use event accessor syntax", 71);
                return _CS0071;
            }
        }
        private static ICompilerReferenceError _CS0071;
        public static ICompilerReferenceError CS0072
        {
            get
            {
                if (_CS0072 == null)
                    _CS0072 = new CompilerReferenceError(@"{0} : cannot override; {1} is not an event", 72);
                return _CS0072;
            }
        }
        private static ICompilerReferenceError _CS0072;
        public static ICompilerReferenceError CS0073
        {
            get
            {
                if (_CS0073 == null)
                    _CS0073 = new CompilerReferenceError(@"An add or remove accessor must have a body", 73);
                return _CS0073;
            }
        }
        private static ICompilerReferenceError _CS0073;
        public static ICompilerReferenceError CS0074
        {
            get
            {
                if (_CS0074 == null)
                    _CS0074 = new CompilerReferenceError(@"{0}: abstract event cannot have initializer", 74);
                return _CS0074;
            }
        }
        private static ICompilerReferenceError _CS0074;
        public static ICompilerReferenceError CS0075
        {
            get
            {
                if (_CS0075 == null)
                    _CS0075 = new CompilerReferenceError(@"To cast a negative value, you must enclose the value in parentheses", 75);
                return _CS0075;
            }
        }
        private static ICompilerReferenceError _CS0075;
        public static ICompilerReferenceError CS0076
        {
            get
            {
                if (_CS0076 == null)
                    _CS0076 = new CompilerReferenceError(@"The enumerator name 'value__' is reserved and cannot be used", 76);
                return _CS0076;
            }
        }
        private static ICompilerReferenceError _CS0076;
        public static ICompilerReferenceError CS0077
        {
            get
            {
                if (_CS0077 == null)
                    _CS0077 = new CompilerReferenceError(@"The as operator must be used with a reference type or nullable type ({0} is a non-nullable value type).", 77);
                return _CS0077;
            }
        }
        private static ICompilerReferenceError _CS0077;
        public static ICompilerReferenceError CS0079
        {
            get
            {
                if (_CS0079 == null)
                    _CS0079 = new CompilerReferenceError(@"The event {0} can only appear on the left hand side of += or -=", 79);
                return _CS0079;
            }
        }
        private static ICompilerReferenceError _CS0079;
        public static ICompilerReferenceError CS0080
        {
            get
            {
                if (_CS0080 == null)
                    _CS0080 = new CompilerReferenceError(@"Constraints are not allowed on non-generic declarations", 80);
                return _CS0080;
            }
        }
        private static ICompilerReferenceError _CS0080;
        public static ICompilerReferenceError CS0081
        {
            get
            {
                if (_CS0081 == null)
                    _CS0081 = new CompilerReferenceError(@"Type parameter declaration must be an identifier not a type", 81);
                return _CS0081;
            }
        }
        private static ICompilerReferenceError _CS0081;
        public static ICompilerReferenceError CS0082
        {
            get
            {
                if (_CS0082 == null)
                    _CS0082 = new CompilerReferenceError(@"Type {0} already reserves a member called {1} with the same parameter types", 82);
                return _CS0082;
            }
        }
        private static ICompilerReferenceError _CS0082;
        public static ICompilerReferenceError CS0100
        {
            get
            {
                if (_CS0100 == null)
                    _CS0100 = new CompilerReferenceError(@"The parameter name {0} is a duplicate", 100);
                return _CS0100;
            }
        }
        private static ICompilerReferenceError _CS0100;
        public static ICompilerReferenceError CS0101
        {
            get
            {
                if (_CS0101 == null)
                    _CS0101 = new CompilerReferenceError(@"The namespace {0} already contains a definition for {1}", 101);
                return _CS0101;
            }
        }
        private static ICompilerReferenceError _CS0101;
        public static ICompilerReferenceError CS0102
        {
            get
            {
                if (_CS0102 == null)
                    _CS0102 = new CompilerReferenceError(@"The type {0} already contains a definition for {1}", 102);
                return _CS0102;
            }
        }
        private static ICompilerReferenceError _CS0102;
        public static ICompilerReferenceError CS0103
        {
            get
            {
                if (_CS0103 == null)
                    _CS0103 = new CompilerReferenceError(@"The name {0} does not exist in the current context", 103);
                return _CS0103;
            }
        }
        private static ICompilerReferenceError _CS0103;
        public static ICompilerReferenceError CS0104
        {
            get
            {
                if (_CS0104 == null)
                    _CS0104 = new CompilerReferenceError(@"{0} is an ambiguous reference between {1} and {2}", 104);
                return _CS0104;
            }
        }
        private static ICompilerReferenceError _CS0104;
        public static ICompilerReferenceError CS0106
        {
            get
            {
                if (_CS0106 == null)
                    _CS0106 = new CompilerReferenceError(@"The modifier {0} is not valid for this item", 106);
                return _CS0106;
            }
        }
        private static ICompilerReferenceError _CS0106;
        public static ICompilerReferenceError CS0107
        {
            get
            {
                if (_CS0107 == null)
                    _CS0107 = new CompilerReferenceError(@"More than one protection modifier", 107);
                return _CS0107;
            }
        }
        private static ICompilerReferenceError _CS0107;
        public static ICompilerReferenceError CS0110
        {
            get
            {
                if (_CS0110 == null)
                    _CS0110 = new CompilerReferenceError(@"The evaluation of the constant value for {0} involves a circular definition", 110);
                return _CS0110;
            }
        }
        private static ICompilerReferenceError _CS0110;
        public static ICompilerReferenceError CS0111
        {
            get
            {
                if (_CS0111 == null)
                    _CS0111 = new CompilerReferenceError(@"Type {0} already defines a member called {1} with the same parameter types", 111);
                return _CS0111;
            }
        }
        private static ICompilerReferenceError _CS0111;
        public static ICompilerReferenceError CS0112
        {
            get
            {
                if (_CS0112 == null)
                    _CS0112 = new CompilerReferenceError(@"A static member {0} cannot be marked as override, virtual or abstract", 112);
                return _CS0112;
            }
        }
        private static ICompilerReferenceError _CS0112;
        public static ICompilerReferenceError CS0113
        {
            get
            {
                if (_CS0113 == null)
                    _CS0113 = new CompilerReferenceError(@"A member {0} marked as override cannot be marked as new or virtual", 113);
                return _CS0113;
            }
        }
        private static ICompilerReferenceError _CS0113;
        public static ICompilerReferenceError CS0115
        {
            get
            {
                if (_CS0115 == null)
                    _CS0115 = new CompilerReferenceError(@"{0} : no suitable method found to override", 115);
                return _CS0115;
            }
        }
        private static ICompilerReferenceError _CS0115;
        public static ICompilerReferenceError CS0116
        {
            get
            {
                if (_CS0116 == null)
                    _CS0116 = new CompilerReferenceError(@"A namespace does not directly contain members such as fields or methods", 116);
                return _CS0116;
            }
        }
        private static ICompilerReferenceError _CS0116;
        public static ICompilerReferenceError CS0117
        {
            get
            {
                if (_CS0117 == null)
                    _CS0117 = new CompilerReferenceError(@"{0} does not contain a definition for 'identifier'", 117);
                return _CS0117;
            }
        }
        private static ICompilerReferenceError _CS0117;
        public static ICompilerReferenceError CS0118
        {
            get
            {
                if (_CS0118 == null)
                    _CS0118 = new CompilerReferenceError(@"{0} is a {1} but is used like a {2}", 118);
                return _CS0118;
            }
        }
        private static ICompilerReferenceError _CS0118;
        public static ICompilerReferenceError CS0119
        {
            get
            {
                if (_CS0119 == null)
                    _CS0119 = new CompilerReferenceError(@"{0} is a {1}, which is not valid in the given context.", 119);
                return _CS0119;
            }
        }
        private static ICompilerReferenceError _CS0119;
        public static ICompilerReferenceError CS0120
        {
            get
            {
                if (_CS0120 == null)
                    _CS0120 = new CompilerReferenceError(@"An object reference is required for the nonstatic field, method, or property {0}", 120);
                return _CS0120;
            }
        }
        private static ICompilerReferenceError _CS0120;
        public static ICompilerReferenceError CS0121
        {
            get
            {
                if (_CS0121 == null)
                    _CS0121 = new CompilerReferenceError(@"The call is ambiguous between the following methods or properties: {0} and {1}", 121);
                return _CS0121;
            }
        }
        private static ICompilerReferenceError _CS0121;
        public static ICompilerReferenceError CS0122
        {
            get
            {
                if (_CS0122 == null)
                    _CS0122 = new CompilerReferenceError(@"{0} is inaccessible due to its protection level", 122);
                return _CS0122;
            }
        }
        private static ICompilerReferenceError _CS0122;
        public static ICompilerReferenceError CS0123
        {
            get
            {
                if (_CS0123 == null)
                    _CS0123 = new CompilerReferenceError(@"No overload for {0} matches delegate {1}", 123);
                return _CS0123;
            }
        }
        private static ICompilerReferenceError _CS0123;
        public static ICompilerReferenceError CS0126
        {
            get
            {
                if (_CS0126 == null)
                    _CS0126 = new CompilerReferenceError(@"An object of a type convertible to {0} is required", 126);
                return _CS0126;
            }
        }
        private static ICompilerReferenceError _CS0126;
        public static ICompilerReferenceError CS0127
        {
            get
            {
                if (_CS0127 == null)
                    _CS0127 = new CompilerReferenceError(@"Since {0} returns void, a return keyword must not be followed by an object expression", 127);
                return _CS0127;
            }
        }
        private static ICompilerReferenceError _CS0127;
        public static ICompilerReferenceError CS0128
        {
            get
            {
                if (_CS0128 == null)
                    _CS0128 = new CompilerReferenceError(@"A local variable named {0} is already defined in this scope", 128);
                return _CS0128;
            }
        }
        private static ICompilerReferenceError _CS0128;
        public static ICompilerReferenceError CS0131
        {
            get
            {
                if (_CS0131 == null)
                    _CS0131 = new CompilerReferenceError(@"The left-hand side of an assignment must be a variable, property or indexer", 131);
                return _CS0131;
            }
        }
        private static ICompilerReferenceError _CS0131;
        public static ICompilerReferenceError CS0132
        {
            get
            {
                if (_CS0132 == null)
                    _CS0132 = new CompilerReferenceError(@"{0} : a static constructor must be parameterless", 132);
                return _CS0132;
            }
        }
        private static ICompilerReferenceError _CS0132;
        public static ICompilerReferenceError CS0133
        {
            get
            {
                if (_CS0133 == null)
                    _CS0133 = new CompilerReferenceError(@"The expression being assigned to {0} must be constant", 133);
                return _CS0133;
            }
        }
        private static ICompilerReferenceError _CS0133;
        public static ICompilerReferenceError CS0134
        {
            get
            {
                if (_CS0134 == null)
                    _CS0134 = new CompilerReferenceError(@"{0} is of type {1}. A const field of a reference type other than string can only be initialized with null.", 134);
                return _CS0134;
            }
        }
        private static ICompilerReferenceError _CS0134;
        public static ICompilerReferenceError CS0135
        {
            get
            {
                if (_CS0135 == null)
                    _CS0135 = new CompilerReferenceError(@"{0} conflicts with the declaration {1}", 135);
                return _CS0135;
            }
        }
        private static ICompilerReferenceError _CS0135;
        public static ICompilerReferenceError CS0136
        {
            get
            {
                if (_CS0136 == null)
                    _CS0136 = new CompilerReferenceError(@"A local variable named {0} cannot be declared in this scope because it would give a different meaning to {0}, which is already used in a 'parent or current/child' scope to denote something else", 136);
                return _CS0136;
            }
        }
        private static ICompilerReferenceError _CS0136;
        public static ICompilerReferenceError CS0138
        {
            get
            {
                if (_CS0138 == null)
                    _CS0138 = new CompilerReferenceError(@"A using namespace directive can only be applied to namespaces; {0} is a type not a namespace", 138);
                return _CS0138;
            }
        }
        private static ICompilerReferenceError _CS0138;
        public static ICompilerReferenceError CS0139
        {
            get
            {
                if (_CS0139 == null)
                    _CS0139 = new CompilerReferenceError(@"No enclosing loop out of which to break or continue", 139);
                return _CS0139;
            }
        }
        private static ICompilerReferenceError _CS0139;
        public static ICompilerReferenceError CS0140
        {
            get
            {
                if (_CS0140 == null)
                    _CS0140 = new CompilerReferenceError(@"The label {0} is a duplicate", 140);
                return _CS0140;
            }
        }
        private static ICompilerReferenceError _CS0140;
        public static ICompilerReferenceError CS0143
        {
            get
            {
                if (_CS0143 == null)
                    _CS0143 = new CompilerReferenceError(@"The type {0} has no constructors defined", 143);
                return _CS0143;
            }
        }
        private static ICompilerReferenceError _CS0143;
        public static ICompilerReferenceError CS0144
        {
            get
            {
                if (_CS0144 == null)
                    _CS0144 = new CompilerReferenceError(@"Cannot create an instance of the abstract class or interface {0}", 144);
                return _CS0144;
            }
        }
        private static ICompilerReferenceError _CS0144;
        public static ICompilerReferenceError CS0145
        {
            get
            {
                if (_CS0145 == null)
                    _CS0145 = new CompilerReferenceError(@"A const field requires a value to be provided", 145);
                return _CS0145;
            }
        }
        private static ICompilerReferenceError _CS0145;
        public static ICompilerReferenceError CS0146
        {
            get
            {
                if (_CS0146 == null)
                    _CS0146 = new CompilerReferenceError(@"Circular base class dependency involving {0} and {1}", 146);
                return _CS0146;
            }
        }
        private static ICompilerReferenceError _CS0146;
        public static ICompilerReferenceError CS0148
        {
            get
            {
                if (_CS0148 == null)
                    _CS0148 = new CompilerReferenceError(@"The delegate {0} does not have a valid constructor", 148);
                return _CS0148;
            }
        }
        private static ICompilerReferenceError _CS0148;
        public static ICompilerReferenceError CS0149
        {
            get
            {
                if (_CS0149 == null)
                    _CS0149 = new CompilerReferenceError(@"Method name expected", 149);
                return _CS0149;
            }
        }
        private static ICompilerReferenceError _CS0149;
        public static ICompilerReferenceError CS0150
        {
            get
            {
                if (_CS0150 == null)
                    _CS0150 = new CompilerReferenceError(@"A constant value is expected", 150);
                return _CS0150;
            }
        }
        private static ICompilerReferenceError _CS0150;
        public static ICompilerReferenceError CS0151
        {
            get
            {
                if (_CS0151 == null)
                    _CS0151 = new CompilerReferenceError(@"A value of an integral type expected", 151);
                return _CS0151;
            }
        }
        private static ICompilerReferenceError _CS0151;
        public static ICompilerReferenceError CS0152
        {
            get
            {
                if (_CS0152 == null)
                    _CS0152 = new CompilerReferenceError(@"The label {0} already occurs in this switch statement", 152);
                return _CS0152;
            }
        }
        private static ICompilerReferenceError _CS0152;
        public static ICompilerReferenceError CS0153
        {
            get
            {
                if (_CS0153 == null)
                    _CS0153 = new CompilerReferenceError(@"A goto case is only valid inside a switch statement", 153);
                return _CS0153;
            }
        }
        private static ICompilerReferenceError _CS0153;
        public static ICompilerReferenceError CS0154
        {
            get
            {
                if (_CS0154 == null)
                    _CS0154 = new CompilerReferenceError(@"The property or indexer 'property' cannot be used in this context because it lacks the get accessor", 154);
                return _CS0154;
            }
        }
        private static ICompilerReferenceError _CS0154;
        public static ICompilerReferenceError CS0155
        {
            get
            {
                if (_CS0155 == null)
                    _CS0155 = new CompilerReferenceError(@"The type caught or thrown must be derived from System.Exception", 155);
                return _CS0155;
            }
        }
        private static ICompilerReferenceError _CS0155;
        public static ICompilerReferenceError CS0156
        {
            get
            {
                if (_CS0156 == null)
                    _CS0156 = new CompilerReferenceError(@"A throw statement with no arguments is not allowed in a finally clause that is nested inside the nearest enclosing catch clause", 156);
                return _CS0156;
            }
        }
        private static ICompilerReferenceError _CS0156;
        public static ICompilerReferenceError CS0157
        {
            get
            {
                if (_CS0157 == null)
                    _CS0157 = new CompilerReferenceError(@"Control cannot leave the body of a finally clause", 157);
                return _CS0157;
            }
        }
        private static ICompilerReferenceError _CS0157;
        public static ICompilerReferenceError CS0158
        {
            get
            {
                if (_CS0158 == null)
                    _CS0158 = new CompilerReferenceError(@"The label {0} shadows another label by the same name in a contained scope", 158);
                return _CS0158;
            }
        }
        private static ICompilerReferenceError _CS0158;
        public static ICompilerReferenceError CS0159
        {
            get
            {
                if (_CS0159 == null)
                    _CS0159 = new CompilerReferenceError(@"No such label {0} within the scope of the goto statement", 159);
                return _CS0159;
            }
        }
        private static ICompilerReferenceError _CS0159;
        public static ICompilerReferenceError CS0160
        {
            get
            {
                if (_CS0160 == null)
                    _CS0160 = new CompilerReferenceError(@"A previous catch clause already catches all exceptions of this or of a super type ({0})", 160);
                return _CS0160;
            }
        }
        private static ICompilerReferenceError _CS0160;
        public static ICompilerReferenceError CS0161
        {
            get
            {
                if (_CS0161 == null)
                    _CS0161 = new CompilerReferenceError(@"{0}: not all code paths return a value", 161);
                return _CS0161;
            }
        }
        private static ICompilerReferenceError _CS0161;
        public static ICompilerReferenceError CS0163
        {
            get
            {
                if (_CS0163 == null)
                    _CS0163 = new CompilerReferenceError(@"Control cannot fall through from one case label ({0}) to another", 163);
                return _CS0163;
            }
        }
        private static ICompilerReferenceError _CS0163;
        public static ICompilerReferenceError CS0165
        {
            get
            {
                if (_CS0165 == null)
                    _CS0165 = new CompilerReferenceError(@"Use of unassigned local variable {0}", 165);
                return _CS0165;
            }
        }
        private static ICompilerReferenceError _CS0165;
        public static ICompilerReferenceError CS0167
        {
            get
            {
                if (_CS0167 == null)
                    _CS0167 = new CompilerReferenceError(@"The delegate {0} is missing the Invoke method", 167);
                return _CS0167;
            }
        }
        private static ICompilerReferenceError _CS0167;
        public static ICompilerReferenceError CS0170
        {
            get
            {
                if (_CS0170 == null)
                    _CS0170 = new CompilerReferenceError(@"Use of possibly unassigned field {0}", 170);
                return _CS0170;
            }
        }
        private static ICompilerReferenceError _CS0170;
        public static ICompilerReferenceError CS0171
        {
            get
            {
                if (_CS0171 == null)
                    _CS0171 = new CompilerReferenceError(@"Backing field for automatically implemented property {0} must be fully assigned before control is returned to the caller. Consider calling the default constructor from a constructor initializer.", 171);
                return _CS0171;
            }
        }
        private static ICompilerReferenceError _CS0171;
        public static ICompilerReferenceError CS0172
        {
            get
            {
                if (_CS0172 == null)
                    _CS0172 = new CompilerReferenceError(@"Type of conditional expression cannot be determined because {0} and {1} implicitly convert to one another", 172);
                return _CS0172;
            }
        }
        private static ICompilerReferenceError _CS0172;
        public static ICompilerReferenceError CS0173
        {
            get
            {
                if (_CS0173 == null)
                    _CS0173 = new CompilerReferenceError(@"Type of conditional expression cannot be determined because there is no implicit conversion between {0} and {1}", 173);
                return _CS0173;
            }
        }
        private static ICompilerReferenceError _CS0173;
        public static ICompilerReferenceError CS0174
        {
            get
            {
                if (_CS0174 == null)
                    _CS0174 = new CompilerReferenceError(@"A base class is required for a 'base' reference", 174);
                return _CS0174;
            }
        }
        private static ICompilerReferenceError _CS0174;
        public static ICompilerReferenceError CS0175
        {
            get
            {
                if (_CS0175 == null)
                    _CS0175 = new CompilerReferenceError(@"Use of keyword 'base' is not valid in this context", 175);
                return _CS0175;
            }
        }
        private static ICompilerReferenceError _CS0175;
        public static ICompilerReferenceError CS0176
        {
            get
            {
                if (_CS0176 == null)
                    _CS0176 = new CompilerReferenceError(@"Static member {0} cannot be accessed with an instance reference; qualify it with a type name instead", 176);
                return _CS0176;
            }
        }
        private static ICompilerReferenceError _CS0176;
        public static ICompilerReferenceError CS0177
        {
            get
            {
                if (_CS0177 == null)
                    _CS0177 = new CompilerReferenceError(@"The out parameter {0} must be assigned to before control leaves the current method", 177);
                return _CS0177;
            }
        }
        private static ICompilerReferenceError _CS0177;
        public static ICompilerReferenceError CS0178
        {
            get
            {
                if (_CS0178 == null)
                    _CS0178 = new CompilerReferenceError(@"Invalid rank specifier: expected ',' or ']'", 178);
                return _CS0178;
            }
        }
        private static ICompilerReferenceError _CS0178;
        public static ICompilerReferenceError CS0179
        {
            get
            {
                if (_CS0179 == null)
                    _CS0179 = new CompilerReferenceError(@"{0} cannot be extern and declare a body", 179);
                return _CS0179;
            }
        }
        private static ICompilerReferenceError _CS0179;
        public static ICompilerReferenceError CS0180
        {
            get
            {
                if (_CS0180 == null)
                    _CS0180 = new CompilerReferenceError(@"{0} cannot be both extern and abstract", 180);
                return _CS0180;
            }
        }
        private static ICompilerReferenceError _CS0180;
        public static ICompilerReferenceError CS0182
        {
            get
            {
                if (_CS0182 == null)
                    _CS0182 = new CompilerReferenceError(@"An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type", 182);
                return _CS0182;
            }
        }
        private static ICompilerReferenceError _CS0182;
        public static ICompilerReferenceError CS0185
        {
            get
            {
                if (_CS0185 == null)
                    _CS0185 = new CompilerReferenceError(@"{0} is not a reference type as required by the lock statement", 185);
                return _CS0185;
            }
        }
        private static ICompilerReferenceError _CS0185;
        public static ICompilerReferenceError CS0186
        {
            get
            {
                if (_CS0186 == null)
                    _CS0186 = new CompilerReferenceError(@"Use of null is not valid in this context ", 186);
                return _CS0186;
            }
        }
        private static ICompilerReferenceError _CS0186;
        public static ICompilerReferenceError CS0188
        {
            get
            {
                if (_CS0188 == null)
                    _CS0188 = new CompilerReferenceError(@"The 'this' object cannot be used before all of its fields are assigned to", 188);
                return _CS0188;
            }
        }
        private static ICompilerReferenceError _CS0188;
        public static ICompilerReferenceError CS0191
        {
            get
            {
                if (_CS0191 == null)
                    _CS0191 = new CompilerReferenceError(@"Property or indexer {0} cannot be assigned to -- it is read only", 191);
                return _CS0191;
            }
        }
        private static ICompilerReferenceError _CS0191;
        public static ICompilerReferenceError CS0192
        {
            get
            {
                if (_CS0192 == null)
                    _CS0192 = new CompilerReferenceError(@"Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)", 192);
                return _CS0192;
            }
        }
        private static ICompilerReferenceError _CS0192;
        public static ICompilerReferenceError CS0193
        {
            get
            {
                if (_CS0193 == null)
                    _CS0193 = new CompilerReferenceError(@"The * or -> operator must be applied to a pointer", 193);
                return _CS0193;
            }
        }
        private static ICompilerReferenceError _CS0193;
        public static ICompilerReferenceError CS0196
        {
            get
            {
                if (_CS0196 == null)
                    _CS0196 = new CompilerReferenceError(@"A pointer must be indexed by only one value", 196);
                return _CS0196;
            }
        }
        private static ICompilerReferenceError _CS0196;
        public static ICompilerReferenceError CS0198
        {
            get
            {
                if (_CS0198 == null)
                    _CS0198 = new CompilerReferenceError(@"Fields of static readonly field {0} cannot be assigned to (except in a static constructor or a variable initializer)", 198);
                return _CS0198;
            }
        }
        private static ICompilerReferenceError _CS0198;
        public static ICompilerReferenceError CS0199
        {
            get
            {
                if (_CS0199 == null)
                    _CS0199 = new CompilerReferenceError(@"Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)", 199);
                return _CS0199;
            }
        }
        private static ICompilerReferenceError _CS0199;
        public static ICompilerReferenceError CS0200
        {
            get
            {
                if (_CS0200 == null)
                    _CS0200 = new CompilerReferenceError(@"Property or indexer {0} cannot be assigned to — it is read only", 200);
                return _CS0200;
            }
        }
        private static ICompilerReferenceError _CS0200;
        public static ICompilerReferenceError CS0201
        {
            get
            {
                if (_CS0201 == null)
                    _CS0201 = new CompilerReferenceError(@"Only assignment, call, increment, decrement, and new object expressions can be used as a statement", 201);
                return _CS0201;
            }
        }
        private static ICompilerReferenceError _CS0201;
        public static ICompilerReferenceError CS0202
        {
            get
            {
                if (_CS0202 == null)
                    _CS0202 = new CompilerReferenceError(@"foreach requires that the return type {0} of '{1}.GetEnumerator()' must have a suitable public MoveNext method and public Current property", 202);
                return _CS0202;
            }
        }
        private static ICompilerReferenceError _CS0202;
        public static ICompilerReferenceError CS0204
        {
            get
            {
                if (_CS0204 == null)
                    _CS0204 = new CompilerReferenceError(@"Only 65534 locals are allowed", 204);
                return _CS0204;
            }
        }
        private static ICompilerReferenceError _CS0204;
        public static ICompilerReferenceError CS0205
        {
            get
            {
                if (_CS0205 == null)
                    _CS0205 = new CompilerReferenceError(@"Cannot call an abstract base member: {0}", 205);
                return _CS0205;
            }
        }
        private static ICompilerReferenceError _CS0205;
        public static ICompilerReferenceError CS0206
        {
            get
            {
                if (_CS0206 == null)
                    _CS0206 = new CompilerReferenceError(@"A property or indexer may not be passed as an out or ref parameter", 206);
                return _CS0206;
            }
        }
        private static ICompilerReferenceError _CS0206;
        public static ICompilerReferenceError CS0208
        {
            get
            {
                if (_CS0208 == null)
                    _CS0208 = new CompilerReferenceError(@"Cannot take the address of, get the size of, or declare a pointer to a managed type ({0})", 208);
                return _CS0208;
            }
        }
        private static ICompilerReferenceError _CS0208;
        public static ICompilerReferenceError CS0209
        {
            get
            {
                if (_CS0209 == null)
                    _CS0209 = new CompilerReferenceError(@"The type of local declared in a fixed statement must be a pointer type", 209);
                return _CS0209;
            }
        }
        private static ICompilerReferenceError _CS0209;
        public static ICompilerReferenceError CS0210
        {
            get
            {
                if (_CS0210 == null)
                    _CS0210 = new CompilerReferenceError(@"You must provide an initializer in a fixed or using statement declaration", 210);
                return _CS0210;
            }
        }
        private static ICompilerReferenceError _CS0210;
        public static ICompilerReferenceError CS0211
        {
            get
            {
                if (_CS0211 == null)
                    _CS0211 = new CompilerReferenceError(@"Cannot take the address of the given expression", 211);
                return _CS0211;
            }
        }
        private static ICompilerReferenceError _CS0211;
        public static ICompilerReferenceError CS0212
        {
            get
            {
                if (_CS0212 == null)
                    _CS0212 = new CompilerReferenceError(@"You can only take the address of an unfixed expression inside of a fixed statement initializer", 212);
                return _CS0212;
            }
        }
        private static ICompilerReferenceError _CS0212;
        public static ICompilerReferenceError CS0213
        {
            get
            {
                if (_CS0213 == null)
                    _CS0213 = new CompilerReferenceError(@"You cannot use the fixed statement to take the address of an already fixed expression", 213);
                return _CS0213;
            }
        }
        private static ICompilerReferenceError _CS0213;
        public static ICompilerReferenceError CS0214
        {
            get
            {
                if (_CS0214 == null)
                    _CS0214 = new CompilerReferenceError(@"Pointers and fixed size buffers may only be used in an unsafe context", 214);
                return _CS0214;
            }
        }
        private static ICompilerReferenceError _CS0214;
        public static ICompilerReferenceError CS0215
        {
            get
            {
                if (_CS0215 == null)
                    _CS0215 = new CompilerReferenceError(@"The return type of operator True or False must be bool", 215);
                return _CS0215;
            }
        }
        private static ICompilerReferenceError _CS0215;
        public static ICompilerReferenceError CS0216
        {
            get
            {
                if (_CS0216 == null)
                    _CS0216 = new CompilerReferenceError(@"The operator {0} requires a matching operator {1} to also be defined", 216);
                return _CS0216;
            }
        }
        private static ICompilerReferenceError _CS0216;
        public static ICompilerReferenceError CS0217
        {
            get
            {
                if (_CS0217 == null)
                    _CS0217 = new CompilerReferenceError(@"In order to be applicable as a short circuit operator a user-defined logical operator ({0}) must have the same return type as the type of its 2 parameters.", 217);
                return _CS0217;
            }
        }
        private static ICompilerReferenceError _CS0217;
        public static ICompilerReferenceError CS0218
        {
            get
            {
                if (_CS0218 == null)
                    _CS0218 = new CompilerReferenceError(@"The type ({0}) must contain declarations of operator true and operator false", 218);
                return _CS0218;
            }
        }
        private static ICompilerReferenceError _CS0218;
        public static ICompilerReferenceError CS0220
        {
            get
            {
                if (_CS0220 == null)
                    _CS0220 = new CompilerReferenceError(@"The operation overflows at compile time in checked mode", 220);
                return _CS0220;
            }
        }
        private static ICompilerReferenceError _CS0220;
        public static ICompilerReferenceError CS0221
        {
            get
            {
                if (_CS0221 == null)
                    _CS0221 = new CompilerReferenceError(@"Constant value {0} cannot be converted to a {1} (use 'unchecked' syntax to override)", 221);
                return _CS0221;
            }
        }
        private static ICompilerReferenceError _CS0221;
        public static ICompilerReferenceError CS0225
        {
            get
            {
                if (_CS0225 == null)
                    _CS0225 = new CompilerReferenceError(@"The params parameter must be a single dimensional array", 225);
                return _CS0225;
            }
        }
        private static ICompilerReferenceError _CS0225;
        public static ICompilerReferenceError CS0226
        {
            get
            {
                if (_CS0226 == null)
                    _CS0226 = new CompilerReferenceError(@"An __arglist expression may only appear inside of a call or new expression.", 226);
                return _CS0226;
            }
        }
        private static ICompilerReferenceError _CS0226;
        public static ICompilerReferenceError CS0227
        {
            get
            {
                if (_CS0227 == null)
                    _CS0227 = new CompilerReferenceError(@"Unsafe code may only appear if compiling with /unsafe", 227);
                return _CS0227;
            }
        }
        private static ICompilerReferenceError _CS0227;
        public static ICompilerReferenceError CS0228
        {
            get
            {
                if (_CS0228 == null)
                    _CS0228 = new CompilerReferenceError(@"{0} does not contain a definition for {1}, or it is not accessible", 228);
                return _CS0228;
            }
        }
        private static ICompilerReferenceError _CS0228;
        public static ICompilerReferenceError CS0229
        {
            get
            {
                if (_CS0229 == null)
                    _CS0229 = new CompilerReferenceError(@"Ambiguity between {0} and {1}", 229);
                return _CS0229;
            }
        }
        private static ICompilerReferenceError _CS0229;
        public static ICompilerReferenceError CS0230
        {
            get
            {
                if (_CS0230 == null)
                    _CS0230 = new CompilerReferenceError(@"Type and identifier are both required in a foreach statement", 230);
                return _CS0230;
            }
        }
        private static ICompilerReferenceError _CS0230;
        public static ICompilerReferenceError CS0231
        {
            get
            {
                if (_CS0231 == null)
                    _CS0231 = new CompilerReferenceError(@"A params parameter must be the last parameter in a formal parameter list.", 231);
                return _CS0231;
            }
        }
        private static ICompilerReferenceError _CS0231;
        public static ICompilerReferenceError CS0233
        {
            get
            {
                if (_CS0233 == null)
                    _CS0233 = new CompilerReferenceError(@"{0} does not have a predefined size, therefore sizeof can only be used in an unsafe context (consider using System.Runtime.InteropServices.Marshal.SizeOf)", 233);
                return _CS0233;
            }
        }
        private static ICompilerReferenceError _CS0233;
        public static ICompilerReferenceError CS0234
        {
            get
            {
                if (_CS0234 == null)
                    _CS0234 = new CompilerReferenceError(@"The type or namespace name {0} does not exist in the namespace {1} (are you missing an assembly reference?)", 234);
                return _CS0234;
            }
        }
        private static ICompilerReferenceError _CS0234;
        public static ICompilerReferenceError CS0236
        {
            get
            {
                if (_CS0236 == null)
                    _CS0236 = new CompilerReferenceError(@"A field initializer cannot reference the nonstatic field, method, or property {0}", 236);
                return _CS0236;
            }
        }
        private static ICompilerReferenceError _CS0236;
        public static ICompilerReferenceError CS0238
        {
            get
            {
                if (_CS0238 == null)
                    _CS0238 = new CompilerReferenceError(@"{0} cannot be sealed because it is not an override", 238);
                return _CS0238;
            }
        }
        private static ICompilerReferenceError _CS0238;
        public static ICompilerReferenceError CS0239
        {
            get
            {
                if (_CS0239 == null)
                    _CS0239 = new CompilerReferenceError(@"{0} : cannot override inherited member {1} because it is sealed", 239);
                return _CS0239;
            }
        }
        private static ICompilerReferenceError _CS0239;
        public static ICompilerReferenceError CS0241
        {
            get
            {
                if (_CS0241 == null)
                    _CS0241 = new CompilerReferenceError(@"Default parameter specifiers are not permitted", 241);
                return _CS0241;
            }
        }
        private static ICompilerReferenceError _CS0241;
        public static ICompilerReferenceError CS0242
        {
            get
            {
                if (_CS0242 == null)
                    _CS0242 = new CompilerReferenceError(@"The operation in question is undefined on void pointers", 242);
                return _CS0242;
            }
        }
        private static ICompilerReferenceError _CS0242;
        public static ICompilerReferenceError CS0243
        {
            get
            {
                if (_CS0243 == null)
                    _CS0243 = new CompilerReferenceError(@"The Conditional attribute is not valid on 'method' because it is an override method", 243);
                return _CS0243;
            }
        }
        private static ICompilerReferenceError _CS0243;
        public static ICompilerReferenceError CS0244
        {
            get
            {
                if (_CS0244 == null)
                    _CS0244 = new CompilerReferenceError(@"Neither 'is' nor 'as' is valid on pointer types", 244);
                return _CS0244;
            }
        }
        private static ICompilerReferenceError _CS0244;
        public static ICompilerReferenceError CS0245
        {
            get
            {
                if (_CS0245 == null)
                    _CS0245 = new CompilerReferenceError(@"Destructors and object.Finalize cannot be called directly. Consider calling IDisposable.Dispose if available.", 245);
                return _CS0245;
            }
        }
        private static ICompilerReferenceError _CS0245;
        public static ICompilerReferenceError CS0246
        {
            get
            {
                if (_CS0246 == null)
                    _CS0246 = new CompilerReferenceError(@"The type or namespace name {0} could not be found (are you missing a using directive or an assembly reference?)", 246);
                return _CS0246;
            }
        }
        private static ICompilerReferenceError _CS0246;
        public static ICompilerReferenceError CS0247
        {
            get
            {
                if (_CS0247 == null)
                    _CS0247 = new CompilerReferenceError(@"Cannot use a negative size with stackalloc", 247);
                return _CS0247;
            }
        }
        private static ICompilerReferenceError _CS0247;
        public static ICompilerReferenceError CS0248
        {
            get
            {
                if (_CS0248 == null)
                    _CS0248 = new CompilerReferenceError(@"Cannot create an array with a negative size", 248);
                return _CS0248;
            }
        }
        private static ICompilerReferenceError _CS0248;
        public static ICompilerReferenceError CS0249
        {
            get
            {
                if (_CS0249 == null)
                    _CS0249 = new CompilerReferenceError(@"Do not override object.Finalize. Instead, provide a destructor.", 249);
                return _CS0249;
            }
        }
        private static ICompilerReferenceError _CS0249;
        public static ICompilerReferenceError CS0250
        {
            get
            {
                if (_CS0250 == null)
                    _CS0250 = new CompilerReferenceError(@"Do not directly call your base class Finalize method. It is called automatically from your destructor.", 250);
                return _CS0250;
            }
        }
        private static ICompilerReferenceError _CS0250;
        public static ICompilerReferenceError CS0254
        {
            get
            {
                if (_CS0254 == null)
                    _CS0254 = new CompilerReferenceError(@"The right hand side of a fixed statement assignment may not be a cast expression", 254);
                return _CS0254;
            }
        }
        private static ICompilerReferenceError _CS0254;
        public static ICompilerReferenceError CS0255
        {
            get
            {
                if (_CS0255 == null)
                    _CS0255 = new CompilerReferenceError(@"stackalloc may not be used in a catch or finally block", 255);
                return _CS0255;
            }
        }
        private static ICompilerReferenceError _CS0255;
        public static ICompilerReferenceError CS0260
        {
            get
            {
                if (_CS0260 == null)
                    _CS0260 = new CompilerReferenceError(@"Missing partial modifier on declaration of type {0}; another partial declaration of this type exists", 260);
                return _CS0260;
            }
        }
        private static ICompilerReferenceError _CS0260;
        public static ICompilerReferenceError CS0261
        {
            get
            {
                if (_CS0261 == null)
                    _CS0261 = new CompilerReferenceError(@"Partial declarations of {0} must be all classes, all structs, or all interfaces", 261);
                return _CS0261;
            }
        }
        private static ICompilerReferenceError _CS0261;
        public static ICompilerReferenceError CS0262
        {
            get
            {
                if (_CS0262 == null)
                    _CS0262 = new CompilerReferenceError(@"Partial declarations of {0} have conflicting accessibility modifiers", 262);
                return _CS0262;
            }
        }
        private static ICompilerReferenceError _CS0262;
        public static ICompilerReferenceError CS0263
        {
            get
            {
                if (_CS0263 == null)
                    _CS0263 = new CompilerReferenceError(@"Partial declarations of {0} must not specify different base classes", 263);
                return _CS0263;
            }
        }
        private static ICompilerReferenceError _CS0263;
        public static ICompilerReferenceError CS0264
        {
            get
            {
                if (_CS0264 == null)
                    _CS0264 = new CompilerReferenceError(@"Partial declarations of {0} must have the same type parameter names in the same order", 264);
                return _CS0264;
            }
        }
        private static ICompilerReferenceError _CS0264;
        public static ICompilerReferenceError CS0265
        {
            get
            {
                if (_CS0265 == null)
                    _CS0265 = new CompilerReferenceError(@"Partial declarations of {0} have inconsistent constraints for type parameter {1}", 265);
                return _CS0265;
            }
        }
        private static ICompilerReferenceError _CS0265;
        public static ICompilerReferenceError CS0266
        {
            get
            {
                if (_CS0266 == null)
                    _CS0266 = new CompilerReferenceError(@"Cannot implicitly convert type {0} to {1}. An explicit conversion exists (are you missing a cast?)", 266);
                return _CS0266;
            }
        }
        private static ICompilerReferenceError _CS0266;
        public static ICompilerReferenceError CS0267
        {
            get
            {
                if (_CS0267 == null)
                    _CS0267 = new CompilerReferenceError(@"The partial modifier can only appear immediately before 'class', 'struct', or 'interface'", 267);
                return _CS0267;
            }
        }
        private static ICompilerReferenceError _CS0267;
        public static ICompilerReferenceError CS0268
        {
            get
            {
                if (_CS0268 == null)
                    _CS0268 = new CompilerReferenceError(@"Imported type {0} is invalid. It contains a circular base class dependency.", 268);
                return _CS0268;
            }
        }
        private static ICompilerReferenceError _CS0268;
        public static ICompilerReferenceError CS0269
        {
            get
            {
                if (_CS0269 == null)
                    _CS0269 = new CompilerReferenceError(@"Use of unassigned out parameter {0}", 269);
                return _CS0269;
            }
        }
        private static ICompilerReferenceError _CS0269;
        public static ICompilerReferenceError CS0270
        {
            get
            {
                if (_CS0270 == null)
                    _CS0270 = new CompilerReferenceError(@"Array size cannot be specified in a variable declaration (try initializing with a 'new' expression)", 270);
                return _CS0270;
            }
        }
        private static ICompilerReferenceError _CS0270;
        public static ICompilerReferenceError CS0271
        {
            get
            {
                if (_CS0271 == null)
                    _CS0271 = new CompilerReferenceError(@"The property or indexer {0} cannot be used in this context because the get accessor is inaccessible", 271);
                return _CS0271;
            }
        }
        private static ICompilerReferenceError _CS0271;
        public static ICompilerReferenceError CS0272
        {
            get
            {
                if (_CS0272 == null)
                    _CS0272 = new CompilerReferenceError(@"The property or indexer {0} cannot be used in this context because the set accessor is inaccessible", 272);
                return _CS0272;
            }
        }
        private static ICompilerReferenceError _CS0272;
        public static ICompilerReferenceError CS0273
        {
            get
            {
                if (_CS0273 == null)
                    _CS0273 = new CompilerReferenceError(@"The accessibility modifier of the {0} accessor must be more restrictive than the property or indexer {1}", 273);
                return _CS0273;
            }
        }
        private static ICompilerReferenceError _CS0273;
        public static ICompilerReferenceError CS0274
        {
            get
            {
                if (_CS0274 == null)
                    _CS0274 = new CompilerReferenceError(@"Cannot specify accessibility modifiers for both accessors of the property or indexer {0}", 274);
                return _CS0274;
            }
        }
        private static ICompilerReferenceError _CS0274;
        public static ICompilerReferenceError CS0275
        {
            get
            {
                if (_CS0275 == null)
                    _CS0275 = new CompilerReferenceError(@"{0}: accessibility modifiers may not be used on accessors in an interface", 275);
                return _CS0275;
            }
        }
        private static ICompilerReferenceError _CS0275;
        public static ICompilerReferenceError CS0276
        {
            get
            {
                if (_CS0276 == null)
                    _CS0276 = new CompilerReferenceError(@"{0}: accessibility modifiers on accessors may only be used if the property or indexer has both a get and a set accessor", 276);
                return _CS0276;
            }
        }
        private static ICompilerReferenceError _CS0276;
        public static ICompilerReferenceError CS0277
        {
            get
            {
                if (_CS0277 == null)
                    _CS0277 = new CompilerReferenceError(@"{0} does not implement interface member {1}. {2} is not public", 277);
                return _CS0277;
            }
        }
        private static ICompilerReferenceError _CS0277;
        public static ICompilerReferenceError CS0281
        {
            get
            {
                if (_CS0281 == null)
                    _CS0281 = new CompilerReferenceError(@"Friend access was granted to {0}, but the output assembly is named {1}. Try adding a reference to {0} or changing the output assembly name to match.", 281);
                return _CS0281;
            }
        }
        private static ICompilerReferenceError _CS0281;
        public static ICompilerReferenceError CS0283
        {
            get
            {
                if (_CS0283 == null)
                    _CS0283 = new CompilerReferenceError(@"The type {0} cannot be declared const", 283);
                return _CS0283;
            }
        }
        private static ICompilerReferenceError _CS0283;
        public static ICompilerReferenceError CS0304
        {
            get
            {
                if (_CS0304 == null)
                    _CS0304 = new CompilerReferenceError(@"Cannot create an instance of the variable type {0} because it does not have the new() constraint", 304);
                return _CS0304;
            }
        }
        private static ICompilerReferenceError _CS0304;
        public static ICompilerReferenceError CS0305
        {
            get
            {
                if (_CS0305 == null)
                    _CS0305 = new CompilerReferenceError(@"Using the generic type {0} requires {1} type arguments", 305);
                return _CS0305;
            }
        }
        private static ICompilerReferenceError _CS0305;
        public static ICompilerReferenceError CS0306
        {
            get
            {
                if (_CS0306 == null)
                    _CS0306 = new CompilerReferenceError(@"The type {0} may not be used as a type argument", 306);
                return _CS0306;
            }
        }
        private static ICompilerReferenceError _CS0306;
        public static ICompilerReferenceError CS0307
        {
            get
            {
                if (_CS0307 == null)
                    _CS0307 = new CompilerReferenceError(@"The {0} {1} is not a generic method. If you intended an expression list, use parentheses around the < expression.", 307);
                return _CS0307;
            }
        }
        private static ICompilerReferenceError _CS0307;
        public static ICompilerReferenceError CS0308
        {
            get
            {
                if (_CS0308 == null)
                    _CS0308 = new CompilerReferenceError(@"The non-generic type-or-method {0} cannot be used with type arguments.", 308);
                return _CS0308;
            }
        }
        private static ICompilerReferenceError _CS0308;
        public static ICompilerReferenceError CS0310
        {
            get
            {
                if (_CS0310 == null)
                    _CS0310 = new CompilerReferenceError(@"The type {0} must be a non-abstract type with a public parameterless constructor in order to use it as parameter {1} in the generic type or method {2}", 310);
                return _CS0310;
            }
        }
        private static ICompilerReferenceError _CS0310;
        public static ICompilerReferenceError CS0311
        {
            get
            {
                if (_CS0311 == null)
                    _CS0311 = new CompilerReferenceError(@"The type {0} cannot be used as type parameter {2} in the generic type or method {3}. There is no implicit reference conversion from {0} to {1}.", 311);
                return _CS0311;
            }
        }
        private static ICompilerReferenceError _CS0311;
        public static ICompilerReferenceError CS0312
        {
            get
            {
                if (_CS0312 == null)
                    _CS0312 = new CompilerReferenceError(@"The type {0} cannot be used as type parameter 'name' in the generic type or method 'name'. The nullable type 'type1' does not satisfy the constraint of 'type2'.", 312);
                return _CS0312;
            }
        }
        private static ICompilerReferenceError _CS0312;
        public static ICompilerReferenceError CS0313
        {
            get
            {
                if (_CS0313 == null)
                    _CS0313 = new CompilerReferenceError(@"The type {0} cannot be used as type parameter 'parameter name' in the generic type or method 'type2'. The nullable type 'type1' does not satisfy the constraint of 'type2'. Nullable types cannot satisfy any interface constraints.", 313);
                return _CS0313;
            }
        }
        private static ICompilerReferenceError _CS0313;
        public static ICompilerReferenceError CS0314
        {
            get
            {
                if (_CS0314 == null)
                    _CS0314 = new CompilerReferenceError(@"The type {0} cannot be used as type parameter 'name' in the generic type or method 'name'. There is no boxing conversion or type parameter conversion from 'type1' to 'type2'.", 314);
                return _CS0314;
            }
        }
        private static ICompilerReferenceError _CS0314;
        public static ICompilerReferenceError CS0315
        {
            get
            {
                if (_CS0315 == null)
                    _CS0315 = new CompilerReferenceError(@"The type {0} cannot be used as type parameter 'T' in the generic type or method 'TypeorMethod<T>'. There is no boxing conversion from 'valueType' to 'referenceType'.", 315);
                return _CS0315;
            }
        }
        private static ICompilerReferenceError _CS0315;
        public static ICompilerReferenceError CS0316
        {
            get
            {
                if (_CS0316 == null)
                    _CS0316 = new CompilerReferenceError(@"The parameter name {0} conflicts with an automatically-generated parameter name.", 316);
                return _CS0316;
            }
        }
        private static ICompilerReferenceError _CS0316;
        public static ICompilerReferenceError CS0400
        {
            get
            {
                if (_CS0400 == null)
                    _CS0400 = new CompilerReferenceError(@"The type or namespace name {0} could not be found in the global namespace (are you missing an assembly reference?)", 400);
                return _CS0400;
            }
        }
        private static ICompilerReferenceError _CS0400;
        public static ICompilerReferenceError CS0401
        {
            get
            {
                if (_CS0401 == null)
                    _CS0401 = new CompilerReferenceError(@"The new() constraint must be the last constraint specified", 401);
                return _CS0401;
            }
        }
        private static ICompilerReferenceError _CS0401;
        public static ICompilerReferenceError CS0403
        {
            get
            {
                if (_CS0403 == null)
                    _CS0403 = new CompilerReferenceError(@"Cannot convert null to type parameter {0} because it could be a non-nullable value type. Consider using default({0}) instead.", 403);
                return _CS0403;
            }
        }
        private static ICompilerReferenceError _CS0403;
        public static ICompilerReferenceError CS0404
        {
            get
            {
                if (_CS0404 == null)
                    _CS0404 = new CompilerReferenceError(@"'<' unexpected : attributes cannot be generic", 404);
                return _CS0404;
            }
        }
        private static ICompilerReferenceError _CS0404;
        public static ICompilerReferenceError CS0405
        {
            get
            {
                if (_CS0405 == null)
                    _CS0405 = new CompilerReferenceError(@"Duplicate constraint {0} for type parameter {1}", 405);
                return _CS0405;
            }
        }
        private static ICompilerReferenceError _CS0405;
        public static ICompilerReferenceError CS0406
        {
            get
            {
                if (_CS0406 == null)
                    _CS0406 = new CompilerReferenceError(@"The class type constraint 'constraint' must come before any other constraints", 406);
                return _CS0406;
            }
        }
        private static ICompilerReferenceError _CS0406;
        public static ICompilerReferenceError CS0407
        {
            get
            {
                if (_CS0407 == null)
                    _CS0407 = new CompilerReferenceError(@"'return-type method' has the wrong return type", 407);
                return _CS0407;
            }
        }
        private static ICompilerReferenceError _CS0407;
        public static ICompilerReferenceError CS0409
        {
            get
            {
                if (_CS0409 == null)
                    _CS0409 = new CompilerReferenceError(@"A constraint clause has already been specified for type parameter 'type parameter'. All of the constraints for a type parameter must be specified in a single where clause.", 409);
                return _CS0409;
            }
        }
        private static ICompilerReferenceError _CS0409;
        public static ICompilerReferenceError CS0410
        {
            get
            {
                if (_CS0410 == null)
                    _CS0410 = new CompilerReferenceError(@"No overload for 'method' has the correct parameter and return types", 410);
                return _CS0410;
            }
        }
        private static ICompilerReferenceError _CS0410;
        public static ICompilerReferenceError CS0411
        {
            get
            {
                if (_CS0411 == null)
                    _CS0411 = new CompilerReferenceError(@"The type arguments for method 'method' cannot be inferred from the usage. Try specifying the type arguments explicitly.", 411);
                return _CS0411;
            }
        }
        private static ICompilerReferenceError _CS0411;
        public static ICompilerReferenceError CS0412
        {
            get
            {
                if (_CS0412 == null)
                    _CS0412 = new CompilerReferenceError(@"'generic': a parameter or local variable cannot have the same name as a method type parameter", 412);
                return _CS0412;
            }
        }
        private static ICompilerReferenceError _CS0412;
        public static ICompilerReferenceError CS0413
        {
            get
            {
                if (_CS0413 == null)
                    _CS0413 = new CompilerReferenceError(@"The type parameter 'type parameter' cannot be used with the 'as' operator because it does not have a class type constraint nor a 'class' constraint", 413);
                return _CS0413;
            }
        }
        private static ICompilerReferenceError _CS0413;
        public static ICompilerReferenceError CS0415
        {
            get
            {
                if (_CS0415 == null)
                    _CS0415 = new CompilerReferenceError(@"The 'IndexerName' attribute is valid only on an indexer that is not an explicit interface member declaration", 415);
                return _CS0415;
            }
        }
        private static ICompilerReferenceError _CS0415;
        public static ICompilerReferenceError CS0416
        {
            get
            {
                if (_CS0416 == null)
                    _CS0416 = new CompilerReferenceError(@"'type parameter': an attribute argument cannot use type parameters", 416);
                return _CS0416;
            }
        }
        private static ICompilerReferenceError _CS0416;
        public static ICompilerReferenceError CS0417
        {
            get
            {
                if (_CS0417 == null)
                    _CS0417 = new CompilerReferenceError(@"'identifier': cannot provide arguments when creating an instance of a variable type", 417);
                return _CS0417;
            }
        }
        private static ICompilerReferenceError _CS0417;
        public static ICompilerReferenceError CS0418
        {
            get
            {
                if (_CS0418 == null)
                    _CS0418 = new CompilerReferenceError(@"'class name': an abstract class cannot be sealed or static", 418);
                return _CS0418;
            }
        }
        private static ICompilerReferenceError _CS0418;
        public static ICompilerReferenceError CS0423
        {
            get
            {
                if (_CS0423 == null)
                    _CS0423 = new CompilerReferenceError(@"Since 'class' has the ComImport attribute, 'method' must be extern or abstract", 423);
                return _CS0423;
            }
        }
        private static ICompilerReferenceError _CS0423;
        public static ICompilerReferenceError CS0424
        {
            get
            {
                if (_CS0424 == null)
                    _CS0424 = new CompilerReferenceError(@"'class': a class with the ComImport attribute cannot specify a base class", 424);
                return _CS0424;
            }
        }
        private static ICompilerReferenceError _CS0424;
        public static ICompilerReferenceError CS0425
        {
            get
            {
                if (_CS0425 == null)
                    _CS0425 = new CompilerReferenceError(@"The constraints for type parameter 'type parameter' of method 'method' must match the constraints for type parameter 'type parameter' of interface method 'method'. Consider using an explicit interface implementation instead.", 425);
                return _CS0425;
            }
        }
        private static ICompilerReferenceError _CS0425;
        public static ICompilerReferenceError CS0426
        {
            get
            {
                if (_CS0426 == null)
                    _CS0426 = new CompilerReferenceError(@"The type name 'identifier' does not exist in the type 'type'", 426);
                return _CS0426;
            }
        }
        private static ICompilerReferenceError _CS0426;
        public static ICompilerReferenceError CS0428
        {
            get
            {
                if (_CS0428 == null)
                    _CS0428 = new CompilerReferenceError(@"Cannot convert method group 'Identifier' to non-delegate type 'type'. Did you intend to invoke the method?", 428);
                return _CS0428;
            }
        }
        private static ICompilerReferenceError _CS0428;
        public static ICompilerReferenceError CS0430
        {
            get
            {
                if (_CS0430 == null)
                    _CS0430 = new CompilerReferenceError(@"The extern alias 'alias' was not specified in a /reference option", 430);
                return _CS0430;
            }
        }
        private static ICompilerReferenceError _CS0430;
        public static ICompilerReferenceError CS0431
        {
            get
            {
                if (_CS0431 == null)
                    _CS0431 = new CompilerReferenceError(@"Cannot use alias 'identifier' with '::' since the alias references a type. Use '.' instead.", 431);
                return _CS0431;
            }
        }
        private static ICompilerReferenceError _CS0431;
        public static ICompilerReferenceError CS0432
        {
            get
            {
                if (_CS0432 == null)
                    _CS0432 = new CompilerReferenceError(@"Alias 'identifier' not found", 432);
                return _CS0432;
            }
        }
        private static ICompilerReferenceError _CS0432;
        public static ICompilerReferenceError CS0433
        {
            get
            {
                if (_CS0433 == null)
                    _CS0433 = new CompilerReferenceError(@"The type TypeName1 exists in both TypeName2 and TypeName3", 433);
                return _CS0433;
            }
        }
        private static ICompilerReferenceError _CS0433;
        public static ICompilerReferenceError CS0434
        {
            get
            {
                if (_CS0434 == null)
                    _CS0434 = new CompilerReferenceError(@"The namespace NamespaceName1 in NamespaceName2 conflicts with the type TypeName1 in NamespaceName3", 434);
                return _CS0434;
            }
        }
        private static ICompilerReferenceError _CS0434;
        public static ICompilerReferenceError CS0438
        {
            get
            {
                if (_CS0438 == null)
                    _CS0438 = new CompilerReferenceError(@"The type 'type' in 'module_1' conflicts with the namespace 'namespace' in 'module_2'.", 438);
                return _CS0438;
            }
        }
        private static ICompilerReferenceError _CS0438;
        public static ICompilerReferenceError CS0439
        {
            get
            {
                if (_CS0439 == null)
                    _CS0439 = new CompilerReferenceError(@"An extern alias declaration must precede all other elements defined in the namespace", 439);
                return _CS0439;
            }
        }
        private static ICompilerReferenceError _CS0439;
        public static ICompilerReferenceError CS0441
        {
            get
            {
                if (_CS0441 == null)
                    _CS0441 = new CompilerReferenceError(@"'class': a class cannot be both static and sealed", 441);
                return _CS0441;
            }
        }
        private static ICompilerReferenceError _CS0441;
        public static ICompilerReferenceError CS0442
        {
            get
            {
                if (_CS0442 == null)
                    _CS0442 = new CompilerReferenceError(@"'Property': abstract properties cannot have private accessors", 442);
                return _CS0442;
            }
        }
        private static ICompilerReferenceError _CS0442;
        public static ICompilerReferenceError CS0443
        {
            get
            {
                if (_CS0443 == null)
                    _CS0443 = new CompilerReferenceError(@"Syntax error, value expected", 443);
                return _CS0443;
            }
        }
        private static ICompilerReferenceError _CS0443;
        public static ICompilerReferenceError CS0445
        {
            get
            {
                if (_CS0445 == null)
                    _CS0445 = new CompilerReferenceError(@"Cannot modify the result of an unboxing conversion", 445);
                return _CS0445;
            }
        }
        private static ICompilerReferenceError _CS0445;
        public static ICompilerReferenceError CS0446
        {
            get
            {
                if (_CS0446 == null)
                    _CS0446 = new CompilerReferenceError(@"Foreach cannot operate on a 'Method or Delegate'. Did you intend to invoke the 'Method or Delegate'?", 446);
                return _CS0446;
            }
        }
        private static ICompilerReferenceError _CS0446;
        public static ICompilerReferenceError CS0447
        {
            get
            {
                if (_CS0447 == null)
                    _CS0447 = new CompilerReferenceError(@"Attributes cannot be used on type arguments, only on type parameters", 447);
                return _CS0447;
            }
        }
        private static ICompilerReferenceError _CS0447;
        public static ICompilerReferenceError CS0448
        {
            get
            {
                if (_CS0448 == null)
                    _CS0448 = new CompilerReferenceError(@"The return type for ++ or -- operator must be the containing type or derived from the containing type", 448);
                return _CS0448;
            }
        }
        private static ICompilerReferenceError _CS0448;
        public static ICompilerReferenceError CS0449
        {
            get
            {
                if (_CS0449 == null)
                    _CS0449 = new CompilerReferenceError(@"The 'class' or 'struct' constraint must come before any other constraints", 449);
                return _CS0449;
            }
        }
        private static ICompilerReferenceError _CS0449;
        public static ICompilerReferenceError CS0450
        {
            get
            {
                if (_CS0450 == null)
                    _CS0450 = new CompilerReferenceError(@"'Type Parameter Name': cannot specify both a constraint class and the 'class' or 'struct' constraint", 450);
                return _CS0450;
            }
        }
        private static ICompilerReferenceError _CS0450;
        public static ICompilerReferenceError CS0451
        {
            get
            {
                if (_CS0451 == null)
                    _CS0451 = new CompilerReferenceError(@"The 'new()' constraint cannot be used with the 'struct' constraint", 451);
                return _CS0451;
            }
        }
        private static ICompilerReferenceError _CS0451;
        public static ICompilerReferenceError CS0452
        {
            get
            {
                if (_CS0452 == null)
                    _CS0452 = new CompilerReferenceError(@"The type 'type name' must be a reference type in order to use it as parameter 'parameter name' in the generic type or method 'identifier of generic'", 452);
                return _CS0452;
            }
        }
        private static ICompilerReferenceError _CS0452;
        public static ICompilerReferenceError CS0453
        {
            get
            {
                if (_CS0453 == null)
                    _CS0453 = new CompilerReferenceError(@"The type 'Type Name' must be a non-nullable value type in order to use it as parameter 'Parameter Name' in the generic type or method 'Generic Identifier'", 453);
                return _CS0453;
            }
        }
        private static ICompilerReferenceError _CS0453;
        public static ICompilerReferenceError CS0454
        {
            get
            {
                if (_CS0454 == null)
                    _CS0454 = new CompilerReferenceError(@"Circular constraint dependency involving 'Type Parameter 1' and 'Type Parameter 2'", 454);
                return _CS0454;
            }
        }
        private static ICompilerReferenceError _CS0454;
        public static ICompilerReferenceError CS0455
        {
            get
            {
                if (_CS0455 == null)
                    _CS0455 = new CompilerReferenceError(@"Type parameter 'Type Parameter Name' inherits conflicting constraints 'Constraint Name 1' and 'Constraint Name 2'", 455);
                return _CS0455;
            }
        }
        private static ICompilerReferenceError _CS0455;
        public static ICompilerReferenceError CS0456
        {
            get
            {
                if (_CS0456 == null)
                    _CS0456 = new CompilerReferenceError(@"Type parameter 'Type Parameter Name 1' has the 'struct' constraint so 'Type Parameter Name 1' cannot be used as a constraint for 'Type Parameter Name 2'", 456);
                return _CS0456;
            }
        }
        private static ICompilerReferenceError _CS0456;
        public static ICompilerReferenceError CS0457
        {
            get
            {
                if (_CS0457 == null)
                    _CS0457 = new CompilerReferenceError(@"Ambiguous user defined conversions 'Conversion method name 1' and 'Conversion method name 2' when converting from 'type name 1' to 'type name 2'", 457);
                return _CS0457;
            }
        }
        private static ICompilerReferenceError _CS0457;
        public static ICompilerReferenceError CS0459
        {
            get
            {
                if (_CS0459 == null)
                    _CS0459 = new CompilerReferenceError(@"Cannot take the address of a read-only local variable", 459);
                return _CS0459;
            }
        }
        private static ICompilerReferenceError _CS0459;
        public static ICompilerReferenceError CS0460
        {
            get
            {
                if (_CS0460 == null)
                    _CS0460 = new CompilerReferenceError(@"Constraints for override and explicit interface implementation methods are inherited from the base method, so they cannot be specified directly", 460);
                return _CS0460;
            }
        }
        private static ICompilerReferenceError _CS0460;
        public static ICompilerReferenceError CS0462
        {
            get
            {
                if (_CS0462 == null)
                    _CS0462 = new CompilerReferenceError(@"The inherited members 'member1' and 'member2' have the same signature in type 'type', so they cannot be overridden", 462);
                return _CS0462;
            }
        }
        private static ICompilerReferenceError _CS0462;
        public static ICompilerReferenceError CS0463
        {
            get
            {
                if (_CS0463 == null)
                    _CS0463 = new CompilerReferenceError(@"Evaluation of the decimal constant expression failed with error: 'error'", 463);
                return _CS0463;
            }
        }
        private static ICompilerReferenceError _CS0463;
        public static ICompilerReferenceError CS0466
        {
            get
            {
                if (_CS0466 == null)
                    _CS0466 = new CompilerReferenceError(@"'method1' should not have a params parameter since 'method2' does not", 466);
                return _CS0466;
            }
        }
        private static ICompilerReferenceError _CS0466;
        public static ICompilerReferenceError CS0468
        {
            get
            {
                if (_CS0468 == null)
                    _CS0468 = new CompilerReferenceError(@"Ambiguity between type 'type1' and type 'type2'", 468);
                return _CS0468;
            }
        }
        private static ICompilerReferenceError _CS0468;
        public static ICompilerReferenceError CS0470
        {
            get
            {
                if (_CS0470 == null)
                    _CS0470 = new CompilerReferenceError(@"Method 'method' cannot implement interface accessor 'accessor' for type 'type'. Use an explicit interface implementation.", 470);
                return _CS0470;
            }
        }
        private static ICompilerReferenceError _CS0470;
        public static ICompilerReferenceError CS0471
        {
            get
            {
                if (_CS0471 == null)
                    _CS0471 = new CompilerReferenceError(@"The method 'name' is not a generic method. If you intended an expression list, use parentheses around the < expression.", 471);
                return _CS0471;
            }
        }
        private static ICompilerReferenceError _CS0471;
        public static ICompilerReferenceError CS0473
        {
            get
            {
                if (_CS0473 == null)
                    _CS0473 = new CompilerReferenceError(@"Explicit interface implementation 'method name' matches more than one interface member. Which interface member is actually chosen is implementation-dependent. Consider using a non-explicit implementation instead.", 473);
                return _CS0473;
            }
        }
        private static ICompilerReferenceError _CS0473;
        public static ICompilerReferenceError CS0500
        {
            get
            {
                if (_CS0500 == null)
                    _CS0500 = new CompilerReferenceError(@"'class member' cannot declare a body because it is marked abstract", 500);
                return _CS0500;
            }
        }
        private static ICompilerReferenceError _CS0500;
        public static ICompilerReferenceError CS0501
        {
            get
            {
                if (_CS0501 == null)
                    _CS0501 = new CompilerReferenceError(@"'member function' must declare a body because it is not marked abstract, extern, or partial", 501);
                return _CS0501;
            }
        }
        private static ICompilerReferenceError _CS0501;
        public static ICompilerReferenceError CS0502
        {
            get
            {
                if (_CS0502 == null)
                    _CS0502 = new CompilerReferenceError(@"'member' cannot be both abstract and sealed", 502);
                return _CS0502;
            }
        }
        private static ICompilerReferenceError _CS0502;
        public static ICompilerReferenceError CS0503
        {
            get
            {
                if (_CS0503 == null)
                    _CS0503 = new CompilerReferenceError(@"The abstract method 'method' cannot be marked virtual", 503);
                return _CS0503;
            }
        }
        private static ICompilerReferenceError _CS0503;
        public static ICompilerReferenceError CS0504
        {
            get
            {
                if (_CS0504 == null)
                    _CS0504 = new CompilerReferenceError(@"The constant 'variable' cannot be marked static", 504);
                return _CS0504;
            }
        }
        private static ICompilerReferenceError _CS0504;
        public static ICompilerReferenceError CS0505
        {
            get
            {
                if (_CS0505 == null)
                    _CS0505 = new CompilerReferenceError(@"'member1': cannot override because 'member2' is not a function", 505);
                return _CS0505;
            }
        }
        private static ICompilerReferenceError _CS0505;
        public static ICompilerReferenceError CS0506
        {
            get
            {
                if (_CS0506 == null)
                    _CS0506 = new CompilerReferenceError(@"'function1' : cannot override inherited member 'function2' because it is not marked ""virtual"", ""abstract"", or ""override""", 506);
                return _CS0506;
            }
        }
        private static ICompilerReferenceError _CS0506;
        public static ICompilerReferenceError CS0507
        {
            get
            {
                if (_CS0507 == null)
                    _CS0507 = new CompilerReferenceError(@"'function1' : cannot change access modifiers when overriding 'access' inherited member 'function2'", 507);
                return _CS0507;
            }
        }
        private static ICompilerReferenceError _CS0507;
        public static ICompilerReferenceError CS0508
        {
            get
            {
                if (_CS0508 == null)
                    _CS0508 = new CompilerReferenceError(@"'Type 1': return type must be 'Type 2' to match overridden member 'Member Name'", 508);
                return _CS0508;
            }
        }
        private static ICompilerReferenceError _CS0508;
        public static ICompilerReferenceError CS0509
        {
            get
            {
                if (_CS0509 == null)
                    _CS0509 = new CompilerReferenceError(@"'class1' : cannot derive from sealed type 'class2'", 509);
                return _CS0509;
            }
        }
        private static ICompilerReferenceError _CS0509;
        public static ICompilerReferenceError CS0513
        {
            get
            {
                if (_CS0513 == null)
                    _CS0513 = new CompilerReferenceError(@"'function' is abstract but it is contained in nonabstract class 'class'", 513);
                return _CS0513;
            }
        }
        private static ICompilerReferenceError _CS0513;
        public static ICompilerReferenceError CS0514
        {
            get
            {
                if (_CS0514 == null)
                    _CS0514 = new CompilerReferenceError(@"'constructor' : static constructor cannot have an explicit 'this' or 'base' constructor call", 514);
                return _CS0514;
            }
        }
        private static ICompilerReferenceError _CS0514;
        public static ICompilerReferenceError CS0515
        {
            get
            {
                if (_CS0515 == null)
                    _CS0515 = new CompilerReferenceError(@"'function' : access modifiers are not allowed on static constructors", 515);
                return _CS0515;
            }
        }
        private static ICompilerReferenceError _CS0515;
        public static ICompilerReferenceError CS0516
        {
            get
            {
                if (_CS0516 == null)
                    _CS0516 = new CompilerReferenceError(@"Constructor 'constructor' can not call itself", 516);
                return _CS0516;
            }
        }
        private static ICompilerReferenceError _CS0516;
        public static ICompilerReferenceError CS0517
        {
            get
            {
                if (_CS0517 == null)
                    _CS0517 = new CompilerReferenceError(@"'class' has no base class and cannot call a base constructor", 517);
                return _CS0517;
            }
        }
        private static ICompilerReferenceError _CS0517;
        public static ICompilerReferenceError CS0518
        {
            get
            {
                if (_CS0518 == null)
                    _CS0518 = new CompilerReferenceError(@"Predefined type 'type' is not defined or imported", 518);
                return _CS0518;
            }
        }
        private static ICompilerReferenceError _CS0518;
        public static ICompilerReferenceError CS0520
        {
            get
            {
                if (_CS0520 == null)
                    _CS0520 = new CompilerReferenceError(@"Predefined type 'name' is declared incorrectly", 520);
                return _CS0520;
            }
        }
        private static ICompilerReferenceError _CS0520;
        public static ICompilerReferenceError CS0522
        {
            get
            {
                if (_CS0522 == null)
                    _CS0522 = new CompilerReferenceError(@"'constructor' : structs cannot call base class constructors", 522);
                return _CS0522;
            }
        }
        private static ICompilerReferenceError _CS0522;
        public static ICompilerReferenceError CS0523
        {
            get
            {
                if (_CS0523 == null)
                    _CS0523 = new CompilerReferenceError(@"Struct member 'struct2 field' of type 'struct1' causes a cycle in the struct layout", 523);
                return _CS0523;
            }
        }
        private static ICompilerReferenceError _CS0523;
        public static ICompilerReferenceError CS0524
        {
            get
            {
                if (_CS0524 == null)
                    _CS0524 = new CompilerReferenceError(@"'type' : interfaces cannot declare types", 524);
                return _CS0524;
            }
        }
        private static ICompilerReferenceError _CS0524;
        public static ICompilerReferenceError CS0525
        {
            get
            {
                if (_CS0525 == null)
                    _CS0525 = new CompilerReferenceError(@"Interfaces cannot contain fields", 525);
                return _CS0525;
            }
        }
        private static ICompilerReferenceError _CS0525;
        public static ICompilerReferenceError CS0526
        {
            get
            {
                if (_CS0526 == null)
                    _CS0526 = new CompilerReferenceError(@"Interfaces cannot contain constructors", 526);
                return _CS0526;
            }
        }
        private static ICompilerReferenceError _CS0526;
        public static ICompilerReferenceError CS0527
        {
            get
            {
                if (_CS0527 == null)
                    _CS0527 = new CompilerReferenceError(@"Type 'type' in interface list is not an interface", 527);
                return _CS0527;
            }
        }
        private static ICompilerReferenceError _CS0527;
        public static ICompilerReferenceError CS0528
        {
            get
            {
                if (_CS0528 == null)
                    _CS0528 = new CompilerReferenceError(@"'interface' is already listed in interface list", 528);
                return _CS0528;
            }
        }
        private static ICompilerReferenceError _CS0528;
        public static ICompilerReferenceError CS0529
        {
            get
            {
                if (_CS0529 == null)
                    _CS0529 = new CompilerReferenceError(@"Inherited interface 'interface1' causes a cycle in the interface hierarchy of 'interface2'", 529);
                return _CS0529;
            }
        }
        private static ICompilerReferenceError _CS0529;
        public static ICompilerReferenceError CS0531
        {
            get
            {
                if (_CS0531 == null)
                    _CS0531 = new CompilerReferenceError(@"'member' : interface members cannot have a definition", 531);
                return _CS0531;
            }
        }
        private static ICompilerReferenceError _CS0531;
        public static ICompilerReferenceError CS0533
        {
            get
            {
                if (_CS0533 == null)
                    _CS0533 = new CompilerReferenceError(@"'derived-class member' hides inherited abstract member 'base-class member'", 533);
                return _CS0533;
            }
        }
        private static ICompilerReferenceError _CS0533;
        public static ICompilerReferenceError CS0534
        {
            get
            {
                if (_CS0534 == null)
                    _CS0534 = new CompilerReferenceError(@"'function1' does not implement inherited abstract member 'function2'", 534);
                return _CS0534;
            }
        }
        private static ICompilerReferenceError _CS0534;
        public static ICompilerReferenceError CS0535
        {
            get
            {
                if (_CS0535 == null)
                    _CS0535 = new CompilerReferenceError(@"'class' does not implement interface member 'member'", 535);
                return _CS0535;
            }
        }
        private static ICompilerReferenceError _CS0535;
        public static ICompilerReferenceError CS0537
        {
            get
            {
                if (_CS0537 == null)
                    _CS0537 = new CompilerReferenceError(@"The class System.Object cannot have a base class or implement an interface", 537);
                return _CS0537;
            }
        }
        private static ICompilerReferenceError _CS0537;
        public static ICompilerReferenceError CS0538
        {
            get
            {
                if (_CS0538 == null)
                    _CS0538 = new CompilerReferenceError(@"'name' in explicit interface declaration is not an interface", 538);
                return _CS0538;
            }
        }
        private static ICompilerReferenceError _CS0538;
        public static ICompilerReferenceError CS0539
        {
            get
            {
                if (_CS0539 == null)
                    _CS0539 = new CompilerReferenceError(@"'member' in explicit interface declaration is not a member of interface", 539);
                return _CS0539;
            }
        }
        private static ICompilerReferenceError _CS0539;
        public static ICompilerReferenceError CS0540
        {
            get
            {
                if (_CS0540 == null)
                    _CS0540 = new CompilerReferenceError(@"'interface member' : containing type does not implement interface 'interface'", 540);
                return _CS0540;
            }
        }
        private static ICompilerReferenceError _CS0540;
        public static ICompilerReferenceError CS0541
        {
            get
            {
                if (_CS0541 == null)
                    _CS0541 = new CompilerReferenceError(@"'declaration' : explicit interface declaration can only be declared in a class or struct", 541);
                return _CS0541;
            }
        }
        private static ICompilerReferenceError _CS0541;
        public static ICompilerReferenceError CS0542
        {
            get
            {
                if (_CS0542 == null)
                    _CS0542 = new CompilerReferenceError(@"'user-defined type' : member names cannot be the same as their enclosing type", 542);
                return _CS0542;
            }
        }
        private static ICompilerReferenceError _CS0542;
        public static ICompilerReferenceError CS0543
        {
            get
            {
                if (_CS0543 == null)
                    _CS0543 = new CompilerReferenceError(@"'enumeration' : the enumerator value is too large to fit in its type", 543);
                return _CS0543;
            }
        }
        private static ICompilerReferenceError _CS0543;
        public static ICompilerReferenceError CS0544
        {
            get
            {
                if (_CS0544 == null)
                    _CS0544 = new CompilerReferenceError(@"'property override': cannot override because 'non-property' is not a property", 544);
                return _CS0544;
            }
        }
        private static ICompilerReferenceError _CS0544;
        public static ICompilerReferenceError CS0545
        {
            get
            {
                if (_CS0545 == null)
                    _CS0545 = new CompilerReferenceError(@"'function' : cannot override because 'property' does not have an overridable get accessor", 545);
                return _CS0545;
            }
        }
        private static ICompilerReferenceError _CS0545;
        public static ICompilerReferenceError CS0546
        {
            get
            {
                if (_CS0546 == null)
                    _CS0546 = new CompilerReferenceError(@"'accessor' : cannot override because 'property' does not have an overridable set accessor", 546);
                return _CS0546;
            }
        }
        private static ICompilerReferenceError _CS0546;
        public static ICompilerReferenceError CS0547
        {
            get
            {
                if (_CS0547 == null)
                    _CS0547 = new CompilerReferenceError(@"'property' : property or indexer cannot have void type", 547);
                return _CS0547;
            }
        }
        private static ICompilerReferenceError _CS0547;
        public static ICompilerReferenceError CS0548
        {
            get
            {
                if (_CS0548 == null)
                    _CS0548 = new CompilerReferenceError(@"'property' : property or indexer must have at least one accessor", 548);
                return _CS0548;
            }
        }
        private static ICompilerReferenceError _CS0548;
        public static ICompilerReferenceError CS0549
        {
            get
            {
                if (_CS0549 == null)
                    _CS0549 = new CompilerReferenceError(@"'function' is a new virtual member in sealed class 'class'", 549);
                return _CS0549;
            }
        }
        private static ICompilerReferenceError _CS0549;
        public static ICompilerReferenceError CS0550
        {
            get
            {
                if (_CS0550 == null)
                    _CS0550 = new CompilerReferenceError(@"'accessor' adds an accessor not found in interface member 'property'", 550);
                return _CS0550;
            }
        }
        private static ICompilerReferenceError _CS0550;
        public static ICompilerReferenceError CS0551
        {
            get
            {
                if (_CS0551 == null)
                    _CS0551 = new CompilerReferenceError(@"Explicit interface implementation 'implementation' is missing accessor 'accessor'", 551);
                return _CS0551;
            }
        }
        private static ICompilerReferenceError _CS0551;
        public static ICompilerReferenceError CS0552
        {
            get
            {
                if (_CS0552 == null)
                    _CS0552 = new CompilerReferenceError(@"'conversion routine' : user defined conversion to/from interface", 552);
                return _CS0552;
            }
        }
        private static ICompilerReferenceError _CS0552;
        public static ICompilerReferenceError CS0553
        {
            get
            {
                if (_CS0553 == null)
                    _CS0553 = new CompilerReferenceError(@"'conversion routine' : user defined conversion to/from base class", 553);
                return _CS0553;
            }
        }
        private static ICompilerReferenceError _CS0553;
        public static ICompilerReferenceError CS0554
        {
            get
            {
                if (_CS0554 == null)
                    _CS0554 = new CompilerReferenceError(@"'conversion routine' : user defined conversion to/from derived class", 554);
                return _CS0554;
            }
        }
        private static ICompilerReferenceError _CS0554;
        public static ICompilerReferenceError CS0555
        {
            get
            {
                if (_CS0555 == null)
                    _CS0555 = new CompilerReferenceError(@"User-defined operator cannot take an object of the enclosing type and convert to an object of the enclosing type", 555);
                return _CS0555;
            }
        }
        private static ICompilerReferenceError _CS0555;
        public static ICompilerReferenceError CS0556
        {
            get
            {
                if (_CS0556 == null)
                    _CS0556 = new CompilerReferenceError(@"User-defined conversion must convert to or from the enclosing type", 556);
                return _CS0556;
            }
        }
        private static ICompilerReferenceError _CS0556;
        public static ICompilerReferenceError CS0557
        {
            get
            {
                if (_CS0557 == null)
                    _CS0557 = new CompilerReferenceError(@"Duplicate user-defined conversion in type 'class'", 557);
                return _CS0557;
            }
        }
        private static ICompilerReferenceError _CS0557;
        public static ICompilerReferenceError CS0558
        {
            get
            {
                if (_CS0558 == null)
                    _CS0558 = new CompilerReferenceError(@"User-defined operator 'operator' must be declared static and public", 558);
                return _CS0558;
            }
        }
        private static ICompilerReferenceError _CS0558;
        public static ICompilerReferenceError CS0559
        {
            get
            {
                if (_CS0559 == null)
                    _CS0559 = new CompilerReferenceError(@"The parameter type for ++ or -- operator must be the containing type", 559);
                return _CS0559;
            }
        }
        private static ICompilerReferenceError _CS0559;
        public static ICompilerReferenceError CS0562
        {
            get
            {
                if (_CS0562 == null)
                    _CS0562 = new CompilerReferenceError(@"The parameter of a unary operator must be the containing type", 562);
                return _CS0562;
            }
        }
        private static ICompilerReferenceError _CS0562;
        public static ICompilerReferenceError CS0563
        {
            get
            {
                if (_CS0563 == null)
                    _CS0563 = new CompilerReferenceError(@"One of the parameters of a binary operator must be the containing type", 563);
                return _CS0563;
            }
        }
        private static ICompilerReferenceError _CS0563;
        public static ICompilerReferenceError CS0564
        {
            get
            {
                if (_CS0564 == null)
                    _CS0564 = new CompilerReferenceError(@"The first operand of an overloaded shift operator must have the same type as the containing type, and the type of the second operand must be int", 564);
                return _CS0564;
            }
        }
        private static ICompilerReferenceError _CS0564;
        public static ICompilerReferenceError CS0567
        {
            get
            {
                if (_CS0567 == null)
                    _CS0567 = new CompilerReferenceError(@"Interfaces cannot contain operators", 567);
                return _CS0567;
            }
        }
        private static ICompilerReferenceError _CS0567;
        public static ICompilerReferenceError CS0568
        {
            get
            {
                if (_CS0568 == null)
                    _CS0568 = new CompilerReferenceError(@"Structs cannot contain explicit parameterless constructors", 568);
                return _CS0568;
            }
        }
        private static ICompilerReferenceError _CS0568;
        public static ICompilerReferenceError CS0569
        {
            get
            {
                if (_CS0569 == null)
                    _CS0569 = new CompilerReferenceError(@"'method2' : cannot override 'method1' because it is not supported by the language", 569);
                return _CS0569;
            }
        }
        private static ICompilerReferenceError _CS0569;
        public static ICompilerReferenceError CS0570
        {
            get
            {
                if (_CS0570 == null)
                    _CS0570 = new CompilerReferenceError(@"Property, indexer, or event 'name' is not supported by the language; try directly calling accessor method 'name!'", 570);
                return _CS0570;
            }
        }
        private static ICompilerReferenceError _CS0570;
        public static ICompilerReferenceError CS0571
        {
            get
            {
                if (_CS0571 == null)
                    _CS0571 = new CompilerReferenceError(@"'function' : cannot explicitly call operator or accessor", 571);
                return _CS0571;
            }
        }
        private static ICompilerReferenceError _CS0571;
        public static ICompilerReferenceError CS0572
        {
            get
            {
                if (_CS0572 == null)
                    _CS0572 = new CompilerReferenceError(@"'type' : cannot reference a type through an expression; try 'path_to_type' instead", 572);
                return _CS0572;
            }
        }
        private static ICompilerReferenceError _CS0572;
        public static ICompilerReferenceError CS0573
        {
            get
            {
                if (_CS0573 == null)
                    _CS0573 = new CompilerReferenceError(@"'field declaration' : cannot have instance field initializers in structs", 573);
                return _CS0573;
            }
        }
        private static ICompilerReferenceError _CS0573;
        public static ICompilerReferenceError CS0574
        {
            get
            {
                if (_CS0574 == null)
                    _CS0574 = new CompilerReferenceError(@"Name of destructor must match name of class", 574);
                return _CS0574;
            }
        }
        private static ICompilerReferenceError _CS0574;
        public static ICompilerReferenceError CS0575
        {
            get
            {
                if (_CS0575 == null)
                    _CS0575 = new CompilerReferenceError(@"Only class types can contain destructors", 575);
                return _CS0575;
            }
        }
        private static ICompilerReferenceError _CS0575;
        public static ICompilerReferenceError CS0576
        {
            get
            {
                if (_CS0576 == null)
                    _CS0576 = new CompilerReferenceError(@"Namespace 'namespace' contains a definition conflicting with alias 'identifier'", 576);
                return _CS0576;
            }
        }
        private static ICompilerReferenceError _CS0576;
        public static ICompilerReferenceError CS0577
        {
            get
            {
                if (_CS0577 == null)
                    _CS0577 = new CompilerReferenceError(@"The Conditional attribute is not valid on 'function' because it is a constructor, destructor, operator, or explicit interface implementation", 577);
                return _CS0577;
            }
        }
        private static ICompilerReferenceError _CS0577;
        public static ICompilerReferenceError CS0578
        {
            get
            {
                if (_CS0578 == null)
                    _CS0578 = new CompilerReferenceError(@"The Conditional attribute is not valid on 'function' because its return type is not void", 578);
                return _CS0578;
            }
        }
        private static ICompilerReferenceError _CS0578;
        public static ICompilerReferenceError CS0579
        {
            get
            {
                if (_CS0579 == null)
                    _CS0579 = new CompilerReferenceError(@"Duplicate 'attribute' attribute", 579);
                return _CS0579;
            }
        }
        private static ICompilerReferenceError _CS0579;
        public static ICompilerReferenceError CS0582
        {
            get
            {
                if (_CS0582 == null)
                    _CS0582 = new CompilerReferenceError(@"The Conditional not valid on interface members", 582);
                return _CS0582;
            }
        }
        private static ICompilerReferenceError _CS0582;
        public static ICompilerReferenceError CS0583
        {
            get
            {
                if (_CS0583 == null)
                    _CS0583 = new CompilerReferenceError(@"Internal Compiler Error. An internal error has occurred in the compiler. To work around this problem, try simplifying or changing the program near the locations listed below. Locations at the top of the list are closer to the point at which the internal error occurred. Errors such as this can be reported to Microsoft by using the /errorreport option.", 583);
                return _CS0583;
            }
        }
        private static ICompilerReferenceError _CS0583;
        public static ICompilerReferenceError CS0584
        {
            get
            {
                if (_CS0584 == null)
                    _CS0584 = new CompilerReferenceError(@"Internal Compiler Error: stage 'stage' symbol 'symbol'", 584);
                return _CS0584;
            }
        }
        private static ICompilerReferenceError _CS0584;
        public static ICompilerReferenceError CS0585
        {
            get
            {
                if (_CS0585 == null)
                    _CS0585 = new CompilerReferenceError(@"Internal Compiler Error: stage 'stage'", 585);
                return _CS0585;
            }
        }
        private static ICompilerReferenceError _CS0585;
        public static ICompilerReferenceError CS0586
        {
            get
            {
                if (_CS0586 == null)
                    _CS0586 = new CompilerReferenceError(@"Internal Compiler Error: stage 'stage'", 586);
                return _CS0586;
            }
        }
        private static ICompilerReferenceError _CS0586;
        public static ICompilerReferenceError CS0587
        {
            get
            {
                if (_CS0587 == null)
                    _CS0587 = new CompilerReferenceError(@"Internal Compiler Error: stage 'stage'", 587);
                return _CS0587;
            }
        }
        private static ICompilerReferenceError _CS0587;
        public static ICompilerReferenceError CS0588
        {
            get
            {
                if (_CS0588 == null)
                    _CS0588 = new CompilerReferenceError(@"Internal Compiler Error: stage 'LEX'", 588);
                return _CS0588;
            }
        }
        private static ICompilerReferenceError _CS0588;
        public static ICompilerReferenceError CS0589
        {
            get
            {
                if (_CS0589 == null)
                    _CS0589 = new CompilerReferenceError(@"Internal Compiler Error: stage 'PARSE'", 589);
                return _CS0589;
            }
        }
        private static ICompilerReferenceError _CS0589;
        public static ICompilerReferenceError CS0590
        {
            get
            {
                if (_CS0590 == null)
                    _CS0590 = new CompilerReferenceError(@"User-defined operators cannot return void", 590);
                return _CS0590;
            }
        }
        private static ICompilerReferenceError _CS0590;
        public static ICompilerReferenceError CS0591
        {
            get
            {
                if (_CS0591 == null)
                    _CS0591 = new CompilerReferenceError(@"Invalid value for argument to 'attribute' attribute", 591);
                return _CS0591;
            }
        }
        private static ICompilerReferenceError _CS0591;
        public static ICompilerReferenceError CS0592
        {
            get
            {
                if (_CS0592 == null)
                    _CS0592 = new CompilerReferenceError(@"Attribute 'attribute' is not valid on this declaration type. It is valid on 'type' declarations only.", 592);
                return _CS0592;
            }
        }
        private static ICompilerReferenceError _CS0592;
        public static ICompilerReferenceError CS0594
        {
            get
            {
                if (_CS0594 == null)
                    _CS0594 = new CompilerReferenceError(@"Floating-point constant is outside the range of type 'type'", 594);
                return _CS0594;
            }
        }
        private static ICompilerReferenceError _CS0594;
        public static ICompilerReferenceError CS0596
        {
            get
            {
                if (_CS0596 == null)
                    _CS0596 = new CompilerReferenceError(@"The Guid attribute must be specified with the ComImport attribute", 596);
                return _CS0596;
            }
        }
        private static ICompilerReferenceError _CS0596;
        public static ICompilerReferenceError CS0599
        {
            get
            {
                if (_CS0599 == null)
                    _CS0599 = new CompilerReferenceError(@"Invalid value for named attribute argument 'argument'", 599);
                return _CS0599;
            }
        }
        private static ICompilerReferenceError _CS0599;
        public static ICompilerReferenceError CS0601
        {
            get
            {
                if (_CS0601 == null)
                    _CS0601 = new CompilerReferenceError(@"The DllImport attribute must be specified on a method marked 'static' and 'extern'", 601);
                return _CS0601;
            }
        }
        private static ICompilerReferenceError _CS0601;
        public static ICompilerReferenceError CS0609
        {
            get
            {
                if (_CS0609 == null)
                    _CS0609 = new CompilerReferenceError(@"Cannot set the IndexerName attribute on an indexer marked override", 609);
                return _CS0609;
            }
        }
        private static ICompilerReferenceError _CS0609;
        public static ICompilerReferenceError CS0610
        {
            get
            {
                if (_CS0610 == null)
                    _CS0610 = new CompilerReferenceError(@"Field or property cannot be of type 'type'", 610);
                return _CS0610;
            }
        }
        private static ICompilerReferenceError _CS0610;
        public static ICompilerReferenceError CS0611
        {
            get
            {
                if (_CS0611 == null)
                    _CS0611 = new CompilerReferenceError(@"Array elements cannot be of type 'type'", 611);
                return _CS0611;
            }
        }
        private static ICompilerReferenceError _CS0611;
        public static ICompilerReferenceError CS0616
        {
            get
            {
                if (_CS0616 == null)
                    _CS0616 = new CompilerReferenceError(@"'class' is not an attribute class", 616);
                return _CS0616;
            }
        }
        private static ICompilerReferenceError _CS0616;
        public static ICompilerReferenceError CS0617
        {
            get
            {
                if (_CS0617 == null)
                    _CS0617 = new CompilerReferenceError(@"'reference' is not a valid named attribute argument because it is not a valid attribute parameter type", 617);
                return _CS0617;
            }
        }
        private static ICompilerReferenceError _CS0617;
        public static ICompilerReferenceError CS0619
        {
            get
            {
                if (_CS0619 == null)
                    _CS0619 = new CompilerReferenceError(@"'member' is obsolete: 'text'", 619);
                return _CS0619;
            }
        }
        private static ICompilerReferenceError _CS0619;
        public static ICompilerReferenceError CS0620
        {
            get
            {
                if (_CS0620 == null)
                    _CS0620 = new CompilerReferenceError(@"Indexers cannot have void type", 620);
                return _CS0620;
            }
        }
        private static ICompilerReferenceError _CS0620;
        public static ICompilerReferenceError CS0621
        {
            get
            {
                if (_CS0621 == null)
                    _CS0621 = new CompilerReferenceError(@"'member' : virtual or abstract members cannot be private", 621);
                return _CS0621;
            }
        }
        private static ICompilerReferenceError _CS0621;
        public static ICompilerReferenceError CS0622
        {
            get
            {
                if (_CS0622 == null)
                    _CS0622 = new CompilerReferenceError(@"Can only use array initializer expressions to assign to array types. Try using a new expression instead.", 622);
                return _CS0622;
            }
        }
        private static ICompilerReferenceError _CS0622;
        public static ICompilerReferenceError CS0623
        {
            get
            {
                if (_CS0623 == null)
                    _CS0623 = new CompilerReferenceError(@"Array initializers can only be used in a variable or field initializer. Try using a new expression instead.", 623);
                return _CS0623;
            }
        }
        private static ICompilerReferenceError _CS0623;
        public static ICompilerReferenceError CS0625
        {
            get
            {
                if (_CS0625 == null)
                    _CS0625 = new CompilerReferenceError(@"'field': instance field types marked with StructLayout(LayoutKind.Explicit) must have a FieldOffset attribute", 625);
                return _CS0625;
            }
        }
        private static ICompilerReferenceError _CS0625;
        public static ICompilerReferenceError CS0629
        {
            get
            {
                if (_CS0629 == null)
                    _CS0629 = new CompilerReferenceError(@"Conditional member 'member' cannot implement interface member 'base class member' in type 'Type Name'", 629);
                return _CS0629;
            }
        }
        private static ICompilerReferenceError _CS0629;
        public static ICompilerReferenceError CS0631
        {
            get
            {
                if (_CS0631 == null)
                    _CS0631 = new CompilerReferenceError(@"ref and out are not valid in this context", 631);
                return _CS0631;
            }
        }
        private static ICompilerReferenceError _CS0631;
        public static ICompilerReferenceError CS0633
        {
            get
            {
                if (_CS0633 == null)
                    _CS0633 = new CompilerReferenceError(@"The argument to the 'attribute' attribute must be a valid identifier", 633);
                return _CS0633;
            }
        }
        private static ICompilerReferenceError _CS0633;
        public static ICompilerReferenceError CS0635
        {
            get
            {
                if (_CS0635 == null)
                    _CS0635 = new CompilerReferenceError(@"'attribute' : System.Interop.UnmanagedType.CustomMarshaller requires named arguments ComType and Marshal", 635);
                return _CS0635;
            }
        }
        private static ICompilerReferenceError _CS0635;
        public static ICompilerReferenceError CS0636
        {
            get
            {
                if (_CS0636 == null)
                    _CS0636 = new CompilerReferenceError(@"The FieldOffset attribute can only be placed on members of types marked with the StructLayout(LayoutKind.Explicit)", 636);
                return _CS0636;
            }
        }
        private static ICompilerReferenceError _CS0636;
        public static ICompilerReferenceError CS0637
        {
            get
            {
                if (_CS0637 == null)
                    _CS0637 = new CompilerReferenceError(@"The FieldOffset attribute is not allowed on static or const fields", 637);
                return _CS0637;
            }
        }
        private static ICompilerReferenceError _CS0637;
        public static ICompilerReferenceError CS0641
        {
            get
            {
                if (_CS0641 == null)
                    _CS0641 = new CompilerReferenceError(@"'attribute' : attribute is only valid on classes derived from System.Attribute", 641);
                return _CS0641;
            }
        }
        private static ICompilerReferenceError _CS0641;
        public static ICompilerReferenceError CS0643
        {
            get
            {
                if (_CS0643 == null)
                    _CS0643 = new CompilerReferenceError(@"'arg' duplicate named attribute argument", 643);
                return _CS0643;
            }
        }
        private static ICompilerReferenceError _CS0643;
        public static ICompilerReferenceError CS0644
        {
            get
            {
                if (_CS0644 == null)
                    _CS0644 = new CompilerReferenceError(@"'class1' cannot derive from special class 'class2'", 644);
                return _CS0644;
            }
        }
        private static ICompilerReferenceError _CS0644;
        public static ICompilerReferenceError CS0645
        {
            get
            {
                if (_CS0645 == null)
                    _CS0645 = new CompilerReferenceError(@"Identifier too long", 645);
                return _CS0645;
            }
        }
        private static ICompilerReferenceError _CS0645;
        public static ICompilerReferenceError CS0646
        {
            get
            {
                if (_CS0646 == null)
                    _CS0646 = new CompilerReferenceError(@"Cannot specify the DefaultMember attribute on a type containing an indexer", 646);
                return _CS0646;
            }
        }
        private static ICompilerReferenceError _CS0646;
        public static ICompilerReferenceError CS0647
        {
            get
            {
                if (_CS0647 == null)
                    _CS0647 = new CompilerReferenceError(@"Error emitting 'attribute' attribute -- 'reason'", 647);
                return _CS0647;
            }
        }
        private static ICompilerReferenceError _CS0647;
        public static ICompilerReferenceError CS0648
        {
            get
            {
                if (_CS0648 == null)
                    _CS0648 = new CompilerReferenceError(@"'type' is a type not supported by the language", 648);
                return _CS0648;
            }
        }
        private static ICompilerReferenceError _CS0648;
        public static ICompilerReferenceError CS0650
        {
            get
            {
                if (_CS0650 == null)
                    _CS0650 = new CompilerReferenceError(@"Bad array declarator: To declare a managed array the rank specifier precedes the variable's identifier. To declare a fixed size buffer field, use the fixed keyword before the field type.", 650);
                return _CS0650;
            }
        }
        private static ICompilerReferenceError _CS0650;
        public static ICompilerReferenceError CS0653
        {
            get
            {
                if (_CS0653 == null)
                    _CS0653 = new CompilerReferenceError(@"Cannot apply attribute class 'class' because it is abstract", 653);
                return _CS0653;
            }
        }
        private static ICompilerReferenceError _CS0653;
        public static ICompilerReferenceError CS0655
        {
            get
            {
                if (_CS0655 == null)
                    _CS0655 = new CompilerReferenceError(@"'parameter' is not a valid named attribute argument because it is not a valid attribute parameter type", 655);
                return _CS0655;
            }
        }
        private static ICompilerReferenceError _CS0655;
        public static ICompilerReferenceError CS0656
        {
            get
            {
                if (_CS0656 == null)
                    _CS0656 = new CompilerReferenceError(@"Missing compiler required member 'object.member'", 656);
                return _CS0656;
            }
        }
        private static ICompilerReferenceError _CS0656;
        public static ICompilerReferenceError CS0662
        {
            get
            {
                if (_CS0662 == null)
                    _CS0662 = new CompilerReferenceError(@"'method' cannot specify only Out attribute on a ref parameter. Use both In and Out attributes, or neither.", 662);
                return _CS0662;
            }
        }
        private static ICompilerReferenceError _CS0662;
        public static ICompilerReferenceError CS0663
        {
            get
            {
                if (_CS0663 == null)
                    _CS0663 = new CompilerReferenceError(@"Cannot define overloaded methods that differ only on ref and out.", 663);
                return _CS0663;
            }
        }
        private static ICompilerReferenceError _CS0663;
        public static ICompilerReferenceError CS0664
        {
            get
            {
                if (_CS0664 == null)
                    _CS0664 = new CompilerReferenceError(@"Literal of type double cannot be implicitly converted to type 'type'; use an 'suffix' suffix to create a literal of this type", 664);
                return _CS0664;
            }
        }
        private static ICompilerReferenceError _CS0664;
        public static ICompilerReferenceError CS0666
        {
            get
            {
                if (_CS0666 == null)
                    _CS0666 = new CompilerReferenceError(@"'member' : new protected member declared in struct", 666);
                return _CS0666;
            }
        }
        private static ICompilerReferenceError _CS0666;
        public static ICompilerReferenceError CS0667
        {
            get
            {
                if (_CS0667 == null)
                    _CS0667 = new CompilerReferenceError(@"The feature 'invalid feature' is deprecated. Please use 'valid feature' instead'.", 667);
                return _CS0667;
            }
        }
        private static ICompilerReferenceError _CS0667;
        public static ICompilerReferenceError CS0668
        {
            get
            {
                if (_CS0668 == null)
                    _CS0668 = new CompilerReferenceError(@"Two indexers have different names; the IndexerName attribute must be used with the same name on every indexer within a type", 668);
                return _CS0668;
            }
        }
        private static ICompilerReferenceError _CS0668;
        public static ICompilerReferenceError CS0669
        {
            get
            {
                if (_CS0669 == null)
                    _CS0669 = new CompilerReferenceError(@"A class with the ComImport attribute cannot have a user-defined constructor", 669);
                return _CS0669;
            }
        }
        private static ICompilerReferenceError _CS0669;
        public static ICompilerReferenceError CS0670
        {
            get
            {
                if (_CS0670 == null)
                    _CS0670 = new CompilerReferenceError(@"Field cannot have void type", 670);
                return _CS0670;
            }
        }
        private static ICompilerReferenceError _CS0670;
        public static ICompilerReferenceError CS0673
        {
            get
            {
                if (_CS0673 == null)
                    _CS0673 = new CompilerReferenceError(@"System.Void cannot be used from C# -- use typeof(void) to get the void type object.", 673);
                return _CS0673;
            }
        }
        private static ICompilerReferenceError _CS0673;
        public static ICompilerReferenceError CS0674
        {
            get
            {
                if (_CS0674 == null)
                    _CS0674 = new CompilerReferenceError(@"Do not use 'System.ParamArrayAttribute'. Use the 'params' keyword instead.", 674);
                return _CS0674;
            }
        }
        private static ICompilerReferenceError _CS0674;
        public static ICompilerReferenceError CS0677
        {
            get
            {
                if (_CS0677 == null)
                    _CS0677 = new CompilerReferenceError(@"'variable': a volatile field cannot be of the type 'type'", 677);
                return _CS0677;
            }
        }
        private static ICompilerReferenceError _CS0677;
        public static ICompilerReferenceError CS0678
        {
            get
            {
                if (_CS0678 == null)
                    _CS0678 = new CompilerReferenceError(@"'variable': a field can not be both volatile and readonly", 678);
                return _CS0678;
            }
        }
        private static ICompilerReferenceError _CS0678;
        public static ICompilerReferenceError CS0681
        {
            get
            {
                if (_CS0681 == null)
                    _CS0681 = new CompilerReferenceError(@"The modifier 'abstract' is not valid on fields. Try using a property instead", 681);
                return _CS0681;
            }
        }
        private static ICompilerReferenceError _CS0681;
        public static ICompilerReferenceError CS0682
        {
            get
            {
                if (_CS0682 == null)
                    _CS0682 = new CompilerReferenceError(@"'type1' cannot implement 'type2' because it is not supported by the language", 682);
                return _CS0682;
            }
        }
        private static ICompilerReferenceError _CS0682;
        public static ICompilerReferenceError CS0683
        {
            get
            {
                if (_CS0683 == null)
                    _CS0683 = new CompilerReferenceError(@"'explicitmethod' explicit method implementation cannot implement 'method' because it is an accessor", 683);
                return _CS0683;
            }
        }
        private static ICompilerReferenceError _CS0683;
        public static ICompilerReferenceError CS0685
        {
            get
            {
                if (_CS0685 == null)
                    _CS0685 = new CompilerReferenceError(@"Conditional member 'member' cannot have an out parameter", 685);
                return _CS0685;
            }
        }
        private static ICompilerReferenceError _CS0685;
        public static ICompilerReferenceError CS0686
        {
            get
            {
                if (_CS0686 == null)
                    _CS0686 = new CompilerReferenceError(@"Accessor 'accessor' cannot implement interface member 'member' for type 'type'. Use an explicit interface implementation.", 686);
                return _CS0686;
            }
        }
        private static ICompilerReferenceError _CS0686;
        public static ICompilerReferenceError CS0687
        {
            get
            {
                if (_CS0687 == null)
                    _CS0687 = new CompilerReferenceError(@"The namespace alias qualifier '::' always resolves to a type or namespace so is illegal here. Consider using '.' instead.", 687);
                return _CS0687;
            }
        }
        private static ICompilerReferenceError _CS0687;
        public static ICompilerReferenceError CS0689
        {
            get
            {
                if (_CS0689 == null)
                    _CS0689 = new CompilerReferenceError(@"Cannot derive from 'identifier' because it is a type parameter", 689);
                return _CS0689;
            }
        }
        private static ICompilerReferenceError _CS0689;
        public static ICompilerReferenceError CS0690
        {
            get
            {
                if (_CS0690 == null)
                    _CS0690 = new CompilerReferenceError(@"Input file 'file' contains invalid metadata.", 690);
                return _CS0690;
            }
        }
        private static ICompilerReferenceError _CS0690;
        public static ICompilerReferenceError CS0692
        {
            get
            {
                if (_CS0692 == null)
                    _CS0692 = new CompilerReferenceError(@"Duplicate type parameter 'identifier'", 692);
                return _CS0692;
            }
        }
        private static ICompilerReferenceError _CS0692;
        public static ICompilerReferenceError CS0694
        {
            get
            {
                if (_CS0694 == null)
                    _CS0694 = new CompilerReferenceError(@"Type parameter 'identifier' has the same name as the containing type, or method", 694);
                return _CS0694;
            }
        }
        private static ICompilerReferenceError _CS0694;
        public static ICompilerReferenceError CS0695
        {
            get
            {
                if (_CS0695 == null)
                    _CS0695 = new CompilerReferenceError(@"'generic type' cannot implement both 'generic interface' and 'generic interface' because they may unify for some type parameter substitutions", 695);
                return _CS0695;
            }
        }
        private static ICompilerReferenceError _CS0695;
        public static ICompilerReferenceError CS0698
        {
            get
            {
                if (_CS0698 == null)
                    _CS0698 = new CompilerReferenceError(@"A generic type cannot derive from 'class' because it is an attribute class", 698);
                return _CS0698;
            }
        }
        private static ICompilerReferenceError _CS0698;
        public static ICompilerReferenceError CS0699
        {
            get
            {
                if (_CS0699 == null)
                    _CS0699 = new CompilerReferenceError(@"'generic' does not define type parameter 'identifier'", 699);
                return _CS0699;
            }
        }
        private static ICompilerReferenceError _CS0699;
        public static ICompilerReferenceError CS0701
        {
            get
            {
                if (_CS0701 == null)
                    _CS0701 = new CompilerReferenceError(@"'identifier' is not a valid constraint. A type used as a constraint must be an interface, a non-sealed class or a type parameter.", 701);
                return _CS0701;
            }
        }
        private static ICompilerReferenceError _CS0701;
        public static ICompilerReferenceError CS0702
        {
            get
            {
                if (_CS0702 == null)
                    _CS0702 = new CompilerReferenceError(@"Constraint cannot be special class 'identifier'", 702);
                return _CS0702;
            }
        }
        private static ICompilerReferenceError _CS0702;
        public static ICompilerReferenceError CS0703
        {
            get
            {
                if (_CS0703 == null)
                    _CS0703 = new CompilerReferenceError(@"Inconsistent accessibility: constraint type 'identifier' is less accessible than 'identifier'", 703);
                return _CS0703;
            }
        }
        private static ICompilerReferenceError _CS0703;
        public static ICompilerReferenceError CS0704
        {
            get
            {
                if (_CS0704 == null)
                    _CS0704 = new CompilerReferenceError(@"Cannot do member lookup in 'type' because it is a type parameter", 704);
                return _CS0704;
            }
        }
        private static ICompilerReferenceError _CS0704;
        public static ICompilerReferenceError CS0706
        {
            get
            {
                if (_CS0706 == null)
                    _CS0706 = new CompilerReferenceError(@"Invalid constraint type. A type used as a constraint must be an interface, a non-sealed class or a type parameter.", 706);
                return _CS0706;
            }
        }
        private static ICompilerReferenceError _CS0706;
        public static ICompilerReferenceError CS0708
        {
            get
            {
                if (_CS0708 == null)
                    _CS0708 = new CompilerReferenceError(@"'field': cannot declare instance members in a static class", 708);
                return _CS0708;
            }
        }
        private static ICompilerReferenceError _CS0708;
        public static ICompilerReferenceError CS0709
        {
            get
            {
                if (_CS0709 == null)
                    _CS0709 = new CompilerReferenceError(@"'derived class': cannot derive from static class 'base class'", 709);
                return _CS0709;
            }
        }
        private static ICompilerReferenceError _CS0709;
        public static ICompilerReferenceError CS0710
        {
            get
            {
                if (_CS0710 == null)
                    _CS0710 = new CompilerReferenceError(@"Static classes cannot have instance constructors", 710);
                return _CS0710;
            }
        }
        private static ICompilerReferenceError _CS0710;
        public static ICompilerReferenceError CS0711
        {
            get
            {
                if (_CS0711 == null)
                    _CS0711 = new CompilerReferenceError(@"Static classes cannot contain destructors", 711);
                return _CS0711;
            }
        }
        private static ICompilerReferenceError _CS0711;
        public static ICompilerReferenceError CS0712
        {
            get
            {
                if (_CS0712 == null)
                    _CS0712 = new CompilerReferenceError(@"Cannot create an instance of the static class 'static class'", 712);
                return _CS0712;
            }
        }
        private static ICompilerReferenceError _CS0712;
        public static ICompilerReferenceError CS0713
        {
            get
            {
                if (_CS0713 == null)
                    _CS0713 = new CompilerReferenceError(@"Static class 'static type' cannot derive from type 'type'. Static classes must derive from object.", 713);
                return _CS0713;
            }
        }
        private static ICompilerReferenceError _CS0713;
        public static ICompilerReferenceError CS0714
        {
            get
            {
                if (_CS0714 == null)
                    _CS0714 = new CompilerReferenceError(@"'static type' : static classes cannot implement interfaces", 714);
                return _CS0714;
            }
        }
        private static ICompilerReferenceError _CS0714;
        public static ICompilerReferenceError CS0715
        {
            get
            {
                if (_CS0715 == null)
                    _CS0715 = new CompilerReferenceError(@"'static class' : static classes cannot contain user defined operators", 715);
                return _CS0715;
            }
        }
        private static ICompilerReferenceError _CS0715;
        public static ICompilerReferenceError CS0716
        {
            get
            {
                if (_CS0716 == null)
                    _CS0716 = new CompilerReferenceError(@"Cannot convert to static type 'type'", 716);
                return _CS0716;
            }
        }
        private static ICompilerReferenceError _CS0716;
        public static ICompilerReferenceError CS0717
        {
            get
            {
                if (_CS0717 == null)
                    _CS0717 = new CompilerReferenceError(@"'static class': static classes cannot be used as constraints", 717);
                return _CS0717;
            }
        }
        private static ICompilerReferenceError _CS0717;
        public static ICompilerReferenceError CS0718
        {
            get
            {
                if (_CS0718 == null)
                    _CS0718 = new CompilerReferenceError(@"'type': static types cannot be used as type arguments", 718);
                return _CS0718;
            }
        }
        private static ICompilerReferenceError _CS0718;
        public static ICompilerReferenceError CS0719
        {
            get
            {
                if (_CS0719 == null)
                    _CS0719 = new CompilerReferenceError(@"'type': array elements cannot be of static type", 719);
                return _CS0719;
            }
        }
        private static ICompilerReferenceError _CS0719;
        public static ICompilerReferenceError CS0720
        {
            get
            {
                if (_CS0720 == null)
                    _CS0720 = new CompilerReferenceError(@"'static class': cannot declare indexers in a static class", 720);
                return _CS0720;
            }
        }
        private static ICompilerReferenceError _CS0720;
        public static ICompilerReferenceError CS0721
        {
            get
            {
                if (_CS0721 == null)
                    _CS0721 = new CompilerReferenceError(@"'type': static types cannot be used as parameters", 721);
                return _CS0721;
            }
        }
        private static ICompilerReferenceError _CS0721;
        public static ICompilerReferenceError CS0722
        {
            get
            {
                if (_CS0722 == null)
                    _CS0722 = new CompilerReferenceError(@"'type': static types cannot be used as return types", 722);
                return _CS0722;
            }
        }
        private static ICompilerReferenceError _CS0722;
        public static ICompilerReferenceError CS0723
        {
            get
            {
                if (_CS0723 == null)
                    _CS0723 = new CompilerReferenceError(@"Cannot declare variable of static type 'type'", 723);
                return _CS0723;
            }
        }
        private static ICompilerReferenceError _CS0723;
        public static ICompilerReferenceError CS0724
        {
            get
            {
                if (_CS0724 == null)
                    _CS0724 = new CompilerReferenceError(@"does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute", 724);
                return _CS0724;
            }
        }
        private static ICompilerReferenceError _CS0724;
        public static ICompilerReferenceError CS0726
        {
            get
            {
                if (_CS0726 == null)
                    _CS0726 = new CompilerReferenceError(@"'format specifier' is not a valid format specifier", 726);
                return _CS0726;
            }
        }
        private static ICompilerReferenceError _CS0726;
        public static ICompilerReferenceError CS0727
        {
            get
            {
                if (_CS0727 == null)
                    _CS0727 = new CompilerReferenceError(@"Invalid format specifier", 727);
                return _CS0727;
            }
        }
        private static ICompilerReferenceError _CS0727;
        public static ICompilerReferenceError CS0729
        {
            get
            {
                if (_CS0729 == null)
                    _CS0729 = new CompilerReferenceError(@"Type 'type' is defined in this assembly, but a type forwarder is specified for it", 729);
                return _CS0729;
            }
        }
        private static ICompilerReferenceError _CS0729;
        public static ICompilerReferenceError CS0730
        {
            get
            {
                if (_CS0730 == null)
                    _CS0730 = new CompilerReferenceError(@"Cannot forward type 'type' because it is a nested type of 'type'", 730);
                return _CS0730;
            }
        }
        private static ICompilerReferenceError _CS0730;
        public static ICompilerReferenceError CS0731
        {
            get
            {
                if (_CS0731 == null)
                    _CS0731 = new CompilerReferenceError(@"The type forwarder for type 'type' in assembly 'assembly' causes a cycle", 731);
                return _CS0731;
            }
        }
        private static ICompilerReferenceError _CS0731;
        public static ICompilerReferenceError CS0733
        {
            get
            {
                if (_CS0733 == null)
                    _CS0733 = new CompilerReferenceError(@"Cannot forward generic type, 'GenericType<>'", 733);
                return _CS0733;
            }
        }
        private static ICompilerReferenceError _CS0733;
        public static ICompilerReferenceError CS0734
        {
            get
            {
                if (_CS0734 == null)
                    _CS0734 = new CompilerReferenceError(@"The /moduleassemblyname option may only be specified when building a target type of 'module'", 734);
                return _CS0734;
            }
        }
        private static ICompilerReferenceError _CS0734;
        public static ICompilerReferenceError CS0735
        {
            get
            {
                if (_CS0735 == null)
                    _CS0735 = new CompilerReferenceError(@"Invalid type specified as an argument for TypeForwardedTo attribute", 735);
                return _CS0735;
            }
        }
        private static ICompilerReferenceError _CS0735;
        public static ICompilerReferenceError CS0736
        {
            get
            {
                if (_CS0736 == null)
                    _CS0736 = new CompilerReferenceError(@"'type name' does not implement interface member 'member name'. 'method name' cannot implement an interface member because it is static.", 736);
                return _CS0736;
            }
        }
        private static ICompilerReferenceError _CS0736;
        public static ICompilerReferenceError CS0737
        {
            get
            {
                if (_CS0737 == null)
                    _CS0737 = new CompilerReferenceError(@"'type name' does not implement interface member 'member name'. 'method name' cannot implement an interface member because it is not public.", 737);
                return _CS0737;
            }
        }
        private static ICompilerReferenceError _CS0737;
        public static ICompilerReferenceError CS0738
        {
            get
            {
                if (_CS0738 == null)
                    _CS0738 = new CompilerReferenceError(@"'type name' does not implement interface member 'member name'. 'method name' cannot implement 'interface member' because it does not have the matching return type of ' type name'.", 738);
                return _CS0738;
            }
        }
        private static ICompilerReferenceError _CS0738;
        public static ICompilerReferenceError CS0739
        {
            get
            {
                if (_CS0739 == null)
                    _CS0739 = new CompilerReferenceError(@"'type name' duplicate TypeForwardedToAttribute.", 739);
                return _CS0739;
            }
        }
        private static ICompilerReferenceError _CS0739;
        public static ICompilerReferenceError CS0742
        {
            get
            {
                if (_CS0742 == null)
                    _CS0742 = new CompilerReferenceError(@"A query body must end with a select clause or a group clause", 742);
                return _CS0742;
            }
        }
        private static ICompilerReferenceError _CS0742;
        public static ICompilerReferenceError CS0743
        {
            get
            {
                if (_CS0743 == null)
                    _CS0743 = new CompilerReferenceError(@"Expected contextual keyword 'on'", 743);
                return _CS0743;
            }
        }
        private static ICompilerReferenceError _CS0743;
        public static ICompilerReferenceError CS0744
        {
            get
            {
                if (_CS0744 == null)
                    _CS0744 = new CompilerReferenceError(@"Expected contextual keyword 'equals'", 744);
                return _CS0744;
            }
        }
        private static ICompilerReferenceError _CS0744;
        public static ICompilerReferenceError CS0745
        {
            get
            {
                if (_CS0745 == null)
                    _CS0745 = new CompilerReferenceError(@"Expected contextual keyword 'by'", 745);
                return _CS0745;
            }
        }
        private static ICompilerReferenceError _CS0745;
        public static ICompilerReferenceError CS0746
        {
            get
            {
                if (_CS0746 == null)
                    _CS0746 = new CompilerReferenceError(@"Invalid anonymous type member declarator. Anonymous type members must be declared with a member assignment, simple name or member access.", 746);
                return _CS0746;
            }
        }
        private static ICompilerReferenceError _CS0746;
        public static ICompilerReferenceError CS0747
        {
            get
            {
                if (_CS0747 == null)
                    _CS0747 = new CompilerReferenceError(@"Invalid initializer member declarator.", 747);
                return _CS0747;
            }
        }
        private static ICompilerReferenceError _CS0747;
        public static ICompilerReferenceError CS0748
        {
            get
            {
                if (_CS0748 == null)
                    _CS0748 = new CompilerReferenceError(@"Inconsistent lambda parameter usage; all parameter types must either be explicit or implicit.", 748);
                return _CS0748;
            }
        }
        private static ICompilerReferenceError _CS0748;
        public static ICompilerReferenceError CS0750
        {
            get
            {
                if (_CS0750 == null)
                    _CS0750 = new CompilerReferenceError(@"A partial method cannot have access modifiers or the virtual, abstract, override, new, sealed, or extern modifiers.", 750);
                return _CS0750;
            }
        }
        private static ICompilerReferenceError _CS0750;
        public static ICompilerReferenceError CS0751
        {
            get
            {
                if (_CS0751 == null)
                    _CS0751 = new CompilerReferenceError(@"A partial method must be declared in a partial class or partial struct", 751);
                return _CS0751;
            }
        }
        private static ICompilerReferenceError _CS0751;
        public static ICompilerReferenceError CS0752
        {
            get
            {
                if (_CS0752 == null)
                    _CS0752 = new CompilerReferenceError(@"A partial method cannot have out parameters", 752);
                return _CS0752;
            }
        }
        private static ICompilerReferenceError _CS0752;
        public static ICompilerReferenceError CS0753
        {
            get
            {
                if (_CS0753 == null)
                    _CS0753 = new CompilerReferenceError(@"Only methods, classes, structs, or interfaces may be partial.", 753);
                return _CS0753;
            }
        }
        private static ICompilerReferenceError _CS0753;
        public static ICompilerReferenceError CS0754
        {
            get
            {
                if (_CS0754 == null)
                    _CS0754 = new CompilerReferenceError(@"A partial method may not explicitly implement an interface method.", 754);
                return _CS0754;
            }
        }
        private static ICompilerReferenceError _CS0754;
        public static ICompilerReferenceError CS0755
        {
            get
            {
                if (_CS0755 == null)
                    _CS0755 = new CompilerReferenceError(@"Both partial method declarations must be extension methods or neither may be an extension method.", 755);
                return _CS0755;
            }
        }
        private static ICompilerReferenceError _CS0755;
        public static ICompilerReferenceError CS0756
        {
            get
            {
                if (_CS0756 == null)
                    _CS0756 = new CompilerReferenceError(@"A partial method may not have multiple defining declarations.", 756);
                return _CS0756;
            }
        }
        private static ICompilerReferenceError _CS0756;
        public static ICompilerReferenceError CS0757
        {
            get
            {
                if (_CS0757 == null)
                    _CS0757 = new CompilerReferenceError(@"A partial method may not have multiple implementing declarations.", 757);
                return _CS0757;
            }
        }
        private static ICompilerReferenceError _CS0757;
        public static ICompilerReferenceError CS0758
        {
            get
            {
                if (_CS0758 == null)
                    _CS0758 = new CompilerReferenceError(@"Both partial method declarations must use a params parameter or neither may use a params parameter", 758);
                return _CS0758;
            }
        }
        private static ICompilerReferenceError _CS0758;
        public static ICompilerReferenceError CS0759
        {
            get
            {
                if (_CS0759 == null)
                    _CS0759 = new CompilerReferenceError(@"No defining declaration found for implementing declaration of partial method 'method'.", 759);
                return _CS0759;
            }
        }
        private static ICompilerReferenceError _CS0759;
        public static ICompilerReferenceError CS0761
        {
            get
            {
                if (_CS0761 == null)
                    _CS0761 = new CompilerReferenceError(@"Partial method declarations of 'method<T>' have inconsistent type parameter constraints.", 761);
                return _CS0761;
            }
        }
        private static ICompilerReferenceError _CS0761;
        public static ICompilerReferenceError CS0762
        {
            get
            {
                if (_CS0762 == null)
                    _CS0762 = new CompilerReferenceError(@"Cannot create delegate from method 'method' because it is a partial method without an implementing declaration", 762);
                return _CS0762;
            }
        }
        private static ICompilerReferenceError _CS0762;
        public static ICompilerReferenceError CS0763
        {
            get
            {
                if (_CS0763 == null)
                    _CS0763 = new CompilerReferenceError(@"Both partial method declarations must be static or neither may be static.", 763);
                return _CS0763;
            }
        }
        private static ICompilerReferenceError _CS0763;
        public static ICompilerReferenceError CS0764
        {
            get
            {
                if (_CS0764 == null)
                    _CS0764 = new CompilerReferenceError(@"Both partial method declarations must be unsafe or neither may be unsafe", 764);
                return _CS0764;
            }
        }
        private static ICompilerReferenceError _CS0764;
        public static ICompilerReferenceError CS0765
        {
            get
            {
                if (_CS0765 == null)
                    _CS0765 = new CompilerReferenceError(@"Partial methods with only a defining declaration or removed conditional methods cannot be used in expression trees", 765);
                return _CS0765;
            }
        }
        private static ICompilerReferenceError _CS0765;
        public static ICompilerReferenceError CS0766
        {
            get
            {
                if (_CS0766 == null)
                    _CS0766 = new CompilerReferenceError(@"Partial methods must have a void return type.", 766);
                return _CS0766;
            }
        }
        private static ICompilerReferenceError _CS0766;
        public static ICompilerReferenceError CS0811
        {
            get
            {
                if (_CS0811 == null)
                    _CS0811 = new CompilerReferenceError(@"The fully qualified name for 'name' is too long for debug information. Compile without '/debug' option.", 811);
                return _CS0811;
            }
        }
        private static ICompilerReferenceError _CS0811;
        public static ICompilerReferenceError CS0815
        {
            get
            {
                if (_CS0815 == null)
                    _CS0815 = new CompilerReferenceError(@"Cannot assign 'expression' to an implicitly typed local", 815);
                return _CS0815;
            }
        }
        private static ICompilerReferenceError _CS0815;
        public static ICompilerReferenceError CS0818
        {
            get
            {
                if (_CS0818 == null)
                    _CS0818 = new CompilerReferenceError(@"Implicitly typed locals must be initialized", 818);
                return _CS0818;
            }
        }
        private static ICompilerReferenceError _CS0818;
        public static ICompilerReferenceError CS0819
        {
            get
            {
                if (_CS0819 == null)
                    _CS0819 = new CompilerReferenceError(@"Implicitly typed locals cannot have multiple declarators.", 819);
                return _CS0819;
            }
        }
        private static ICompilerReferenceError _CS0819;
        public static ICompilerReferenceError CS0820
        {
            get
            {
                if (_CS0820 == null)
                    _CS0820 = new CompilerReferenceError(@"Cannot assign array initializer to an implicitly typed local", 820);
                return _CS0820;
            }
        }
        private static ICompilerReferenceError _CS0820;
        public static ICompilerReferenceError CS0821
        {
            get
            {
                if (_CS0821 == null)
                    _CS0821 = new CompilerReferenceError(@"Implicitly typed locals cannot be fixed", 821);
                return _CS0821;
            }
        }
        private static ICompilerReferenceError _CS0821;
        public static ICompilerReferenceError CS0822
        {
            get
            {
                if (_CS0822 == null)
                    _CS0822 = new CompilerReferenceError(@"Implicitly typed locals cannot be const", 822);
                return _CS0822;
            }
        }
        private static ICompilerReferenceError _CS0822;
        public static ICompilerReferenceError CS0825
        {
            get
            {
                if (_CS0825 == null)
                    _CS0825 = new CompilerReferenceError(@"The contextual keyword 'var' may only appear within a local variable declaration.", 825);
                return _CS0825;
            }
        }
        private static ICompilerReferenceError _CS0825;
        public static ICompilerReferenceError CS0826
        {
            get
            {
                if (_CS0826 == null)
                    _CS0826 = new CompilerReferenceError(@"No best type found for implicitly typed array.", 826);
                return _CS0826;
            }
        }
        private static ICompilerReferenceError _CS0826;
        public static ICompilerReferenceError CS0828
        {
            get
            {
                if (_CS0828 == null)
                    _CS0828 = new CompilerReferenceError(@"Cannot assign 'expression' to anonymous type property.", 828);
                return _CS0828;
            }
        }
        private static ICompilerReferenceError _CS0828;
        public static ICompilerReferenceError CS0831
        {
            get
            {
                if (_CS0831 == null)
                    _CS0831 = new CompilerReferenceError(@"An expression tree may not contain a base access.", 831);
                return _CS0831;
            }
        }
        private static ICompilerReferenceError _CS0831;
        public static ICompilerReferenceError CS0832
        {
            get
            {
                if (_CS0832 == null)
                    _CS0832 = new CompilerReferenceError(@"An expression tree may not contain an assignment operator.", 832);
                return _CS0832;
            }
        }
        private static ICompilerReferenceError _CS0832;
        public static ICompilerReferenceError CS0833
        {
            get
            {
                if (_CS0833 == null)
                    _CS0833 = new CompilerReferenceError(@"An anonymous type cannot have multiple properties with the same name.", 833);
                return _CS0833;
            }
        }
        private static ICompilerReferenceError _CS0833;
        public static ICompilerReferenceError CS0834
        {
            get
            {
                if (_CS0834 == null)
                    _CS0834 = new CompilerReferenceError(@"A lambda expression must have an expression body to be converted to an expression tree.", 834);
                return _CS0834;
            }
        }
        private static ICompilerReferenceError _CS0834;
        public static ICompilerReferenceError CS0835
        {
            get
            {
                if (_CS0835 == null)
                    _CS0835 = new CompilerReferenceError(@"Cannot convert lambda to an expression tree whose type argument 'type' is not a delegate type.", 835);
                return _CS0835;
            }
        }
        private static ICompilerReferenceError _CS0835;
        public static ICompilerReferenceError CS0836
        {
            get
            {
                if (_CS0836 == null)
                    _CS0836 = new CompilerReferenceError(@"Cannot use anonymous type in a constant expression.", 836);
                return _CS0836;
            }
        }
        private static ICompilerReferenceError _CS0836;
        public static ICompilerReferenceError CS0837
        {
            get
            {
                if (_CS0837 == null)
                    _CS0837 = new CompilerReferenceError(@"The first operand of an ""is"" or ""as"" operator may not be a lambda expression or anonymous method.", 837);
                return _CS0837;
            }
        }
        private static ICompilerReferenceError _CS0837;
        public static ICompilerReferenceError CS0838
        {
            get
            {
                if (_CS0838 == null)
                    _CS0838 = new CompilerReferenceError(@"An expression tree may not contain a multidimensional array initializer.", 838);
                return _CS0838;
            }
        }
        private static ICompilerReferenceError _CS0838;
        public static ICompilerReferenceError CS0839
        {
            get
            {
                if (_CS0839 == null)
                    _CS0839 = new CompilerReferenceError(@"Argument missing.", 839);
                return _CS0839;
            }
        }
        private static ICompilerReferenceError _CS0839;
        public static ICompilerReferenceError CS0840
        {
            get
            {
                if (_CS0840 == null)
                    _CS0840 = new CompilerReferenceError(@"'Property name' must declare a body because it is not marked abstract or extern. Automatically implemented properties must define both get and set accessors.", 840);
                return _CS0840;
            }
        }
        private static ICompilerReferenceError _CS0840;
        public static ICompilerReferenceError CS0841
        {
            get
            {
                if (_CS0841 == null)
                    _CS0841 = new CompilerReferenceError(@"Cannot use variable 'name' before it is declared.", 841);
                return _CS0841;
            }
        }
        private static ICompilerReferenceError _CS0841;
        public static ICompilerReferenceError CS0842
        {
            get
            {
                if (_CS0842 == null)
                    _CS0842 = new CompilerReferenceError(@"Automatically implemented properties cannot be used inside a type marked with StructLayout(LayoutKind.Explicit).", 842);
                return _CS0842;
            }
        }
        private static ICompilerReferenceError _CS0842;
        public static ICompilerReferenceError CS0843
        {
            get
            {
                if (_CS0843 == null)
                    _CS0843 = new CompilerReferenceError(@"Backing field for automatically implemented property 'name' must be fully assigned before control is returned to the caller. Consider calling the default constructor from a constructor initializer.", 843);
                return _CS0843;
            }
        }
        private static ICompilerReferenceError _CS0843;
        public static ICompilerReferenceError CS0844
        {
            get
            {
                if (_CS0844 == null)
                    _CS0844 = new CompilerReferenceError(@"Cannot use local variable 'name' before it is declared. The declaration of the local variable hides the field 'name'.", 844);
                return _CS0844;
            }
        }
        private static ICompilerReferenceError _CS0844;
        public static ICompilerReferenceError CS0845
        {
            get
            {
                if (_CS0845 == null)
                    _CS0845 = new CompilerReferenceError(@"An expression tree lambda may not contain a coalescing operator with a null literal left-hand side.", 845);
                return _CS0845;
            }
        }
        private static ICompilerReferenceError _CS0845;
        public static ICompilerReferenceError CS1001
        {
            get
            {
                if (_CS1001 == null)
                    _CS1001 = new CompilerReferenceError(@"Identifier expected", 1001);
                return _CS1001;
            }
        }
        private static ICompilerReferenceError _CS1001;
        public static ICompilerReferenceError CS1002
        {
            get
            {
                if (_CS1002 == null)
                    _CS1002 = new CompilerReferenceError(@"; expected", 1002);
                return _CS1002;
            }
        }
        private static ICompilerReferenceError _CS1002;
        public static ICompilerReferenceError CS1003
        {
            get
            {
                if (_CS1003 == null)
                    _CS1003 = new CompilerReferenceError(@"Syntax error, 'char' expected", 1003);
                return _CS1003;
            }
        }
        private static ICompilerReferenceError _CS1003;
        public static ICompilerReferenceError CS1004
        {
            get
            {
                if (_CS1004 == null)
                    _CS1004 = new CompilerReferenceError(@"Duplicate 'modifier' modifier", 1004);
                return _CS1004;
            }
        }
        private static ICompilerReferenceError _CS1004;
        public static ICompilerReferenceError CS1007
        {
            get
            {
                if (_CS1007 == null)
                    _CS1007 = new CompilerReferenceError(@"Property accessor already defined", 1007);
                return _CS1007;
            }
        }
        private static ICompilerReferenceError _CS1007;
        public static ICompilerReferenceError CS1008
        {
            get
            {
                if (_CS1008 == null)
                    _CS1008 = new CompilerReferenceError(@"Type byte, sbyte, short, ushort, int, uint, long, or ulong expected", 1008);
                return _CS1008;
            }
        }
        private static ICompilerReferenceError _CS1008;
        public static ICompilerReferenceError CS1009
        {
            get
            {
                if (_CS1009 == null)
                    _CS1009 = new CompilerReferenceError(@"Unrecognized escape sequence", 1009);
                return _CS1009;
            }
        }
        private static ICompilerReferenceError _CS1009;
        public static ICompilerReferenceError CS1010
        {
            get
            {
                if (_CS1010 == null)
                    _CS1010 = new CompilerReferenceError(@"Newline in constant", 1010);
                return _CS1010;
            }
        }
        private static ICompilerReferenceError _CS1010;
        public static ICompilerReferenceError CS1011
        {
            get
            {
                if (_CS1011 == null)
                    _CS1011 = new CompilerReferenceError(@"Empty character literal", 1011);
                return _CS1011;
            }
        }
        private static ICompilerReferenceError _CS1011;
        public static ICompilerReferenceError CS1012
        {
            get
            {
                if (_CS1012 == null)
                    _CS1012 = new CompilerReferenceError(@"Too many characters in character literal", 1012);
                return _CS1012;
            }
        }
        private static ICompilerReferenceError _CS1012;
        public static ICompilerReferenceError CS1013
        {
            get
            {
                if (_CS1013 == null)
                    _CS1013 = new CompilerReferenceError(@"Invalid number", 1013);
                return _CS1013;
            }
        }
        private static ICompilerReferenceError _CS1013;
        public static ICompilerReferenceError CS1014
        {
            get
            {
                if (_CS1014 == null)
                    _CS1014 = new CompilerReferenceError(@"A get or set accessor expected", 1014);
                return _CS1014;
            }
        }
        private static ICompilerReferenceError _CS1014;
        public static ICompilerReferenceError CS1015
        {
            get
            {
                if (_CS1015 == null)
                    _CS1015 = new CompilerReferenceError(@"An object, string, or class type expected", 1015);
                return _CS1015;
            }
        }
        private static ICompilerReferenceError _CS1015;
        public static ICompilerReferenceError CS1016
        {
            get
            {
                if (_CS1016 == null)
                    _CS1016 = new CompilerReferenceError(@"Named attribute argument expected", 1016);
                return _CS1016;
            }
        }
        private static ICompilerReferenceError _CS1016;
        public static ICompilerReferenceError CS1017
        {
            get
            {
                if (_CS1017 == null)
                    _CS1017 = new CompilerReferenceError(@"Catch clauses cannot follow the general catch clause of a try statement", 1017);
                return _CS1017;
            }
        }
        private static ICompilerReferenceError _CS1017;
        public static ICompilerReferenceError CS1018
        {
            get
            {
                if (_CS1018 == null)
                    _CS1018 = new CompilerReferenceError(@"Keyword 'this' or 'base' expected", 1018);
                return _CS1018;
            }
        }
        private static ICompilerReferenceError _CS1018;
        public static ICompilerReferenceError CS1019
        {
            get
            {
                if (_CS1019 == null)
                    _CS1019 = new CompilerReferenceError(@"Overloadable unary operator expected", 1019);
                return _CS1019;
            }
        }
        private static ICompilerReferenceError _CS1019;
        public static ICompilerReferenceError CS1020
        {
            get
            {
                if (_CS1020 == null)
                    _CS1020 = new CompilerReferenceError(@"Overloadable binary operator expected", 1020);
                return _CS1020;
            }
        }
        private static ICompilerReferenceError _CS1020;
        public static ICompilerReferenceError CS1021
        {
            get
            {
                if (_CS1021 == null)
                    _CS1021 = new CompilerReferenceError(@"Integral constant is too large", 1021);
                return _CS1021;
            }
        }
        private static ICompilerReferenceError _CS1021;
        public static ICompilerReferenceError CS1022
        {
            get
            {
                if (_CS1022 == null)
                    _CS1022 = new CompilerReferenceError(@"Type or namespace definition, or end-of-file expected", 1022);
                return _CS1022;
            }
        }
        private static ICompilerReferenceError _CS1022;
        public static ICompilerReferenceError CS1023
        {
            get
            {
                if (_CS1023 == null)
                    _CS1023 = new CompilerReferenceError(@"Embedded statement cannot be a declaration or labeled statement", 1023);
                return _CS1023;
            }
        }
        private static ICompilerReferenceError _CS1023;
        public static ICompilerReferenceError CS1024
        {
            get
            {
                if (_CS1024 == null)
                    _CS1024 = new CompilerReferenceError(@"Preprocessor directive expected", 1024);
                return _CS1024;
            }
        }
        private static ICompilerReferenceError _CS1024;
        public static ICompilerReferenceError CS1025
        {
            get
            {
                if (_CS1025 == null)
                    _CS1025 = new CompilerReferenceError(@"Single-line comment or end-of-line expected", 1025);
                return _CS1025;
            }
        }
        private static ICompilerReferenceError _CS1025;
        public static ICompilerReferenceError CS1026
        {
            get
            {
                if (_CS1026 == null)
                    _CS1026 = new CompilerReferenceError(@") expected", 1026);
                return _CS1026;
            }
        }
        private static ICompilerReferenceError _CS1026;
        public static ICompilerReferenceError CS1027
        {
            get
            {
                if (_CS1027 == null)
                    _CS1027 = new CompilerReferenceError(@"#endif directive expected", 1027);
                return _CS1027;
            }
        }
        private static ICompilerReferenceError _CS1027;
        public static ICompilerReferenceError CS1028
        {
            get
            {
                if (_CS1028 == null)
                    _CS1028 = new CompilerReferenceError(@"Unexpected preprocessor directive", 1028);
                return _CS1028;
            }
        }
        private static ICompilerReferenceError _CS1028;
        public static ICompilerReferenceError CS1029
        {
            get
            {
                if (_CS1029 == null)
                    _CS1029 = new CompilerReferenceError(@"#error: 'text'", 1029);
                return _CS1029;
            }
        }
        private static ICompilerReferenceError _CS1029;
        public static ICompilerReferenceError CS1031
        {
            get
            {
                if (_CS1031 == null)
                    _CS1031 = new CompilerReferenceError(@"Type expected", 1031);
                return _CS1031;
            }
        }
        private static ICompilerReferenceError _CS1031;
        public static ICompilerReferenceError CS1032
        {
            get
            {
                if (_CS1032 == null)
                    _CS1032 = new CompilerReferenceError(@"Cannot define/undefine preprocessor symbols after first token in file", 1032);
                return _CS1032;
            }
        }
        private static ICompilerReferenceError _CS1032;
        public static ICompilerReferenceError CS1033
        {
            get
            {
                if (_CS1033 == null)
                    _CS1033 = new CompilerReferenceError(@"Source file has exceeded the limit of 16,707,565 lines representable in the PDB; debug information will be incorrect", 1033);
                return _CS1033;
            }
        }
        private static ICompilerReferenceError _CS1033;
        public static ICompilerReferenceError CS1034
        {
            get
            {
                if (_CS1034 == null)
                    _CS1034 = new CompilerReferenceError(@"Compiler limit exceeded: Line cannot exceed 'number' characters", 1034);
                return _CS1034;
            }
        }
        private static ICompilerReferenceError _CS1034;
        public static ICompilerReferenceError CS1035
        {
            get
            {
                if (_CS1035 == null)
                    _CS1035 = new CompilerReferenceError(@"End-of-file found, '*/' expected", 1035);
                return _CS1035;
            }
        }
        private static ICompilerReferenceError _CS1035;
        public static ICompilerReferenceError CS1036
        {
            get
            {
                if (_CS1036 == null)
                    _CS1036 = new CompilerReferenceError(@"( or . expected", 1036);
                return _CS1036;
            }
        }
        private static ICompilerReferenceError _CS1036;
        public static ICompilerReferenceError CS1037
        {
            get
            {
                if (_CS1037 == null)
                    _CS1037 = new CompilerReferenceError(@"Overloadable operator expected", 1037);
                return _CS1037;
            }
        }
        private static ICompilerReferenceError _CS1037;
        public static ICompilerReferenceError CS1038
        {
            get
            {
                if (_CS1038 == null)
                    _CS1038 = new CompilerReferenceError(@"#endregion directive expected", 1038);
                return _CS1038;
            }
        }
        private static ICompilerReferenceError _CS1038;
        public static ICompilerReferenceError CS1039
        {
            get
            {
                if (_CS1039 == null)
                    _CS1039 = new CompilerReferenceError(@"Unterminated string literal", 1039);
                return _CS1039;
            }
        }
        private static ICompilerReferenceError _CS1039;
        public static ICompilerReferenceError CS1040
        {
            get
            {
                if (_CS1040 == null)
                    _CS1040 = new CompilerReferenceError(@"Preprocessor directives must appear as the first non-whitespace character on a line", 1040);
                return _CS1040;
            }
        }
        private static ICompilerReferenceError _CS1040;
        public static ICompilerReferenceError CS1041
        {
            get
            {
                if (_CS1041 == null)
                    _CS1041 = new CompilerReferenceError(@"Identifier expected, 'keyword' is a keyword", 1041);
                return _CS1041;
            }
        }
        private static ICompilerReferenceError _CS1041;
        public static ICompilerReferenceError CS1043
        {
            get
            {
                if (_CS1043 == null)
                    _CS1043 = new CompilerReferenceError(@"{ or ; expected", 1043);
                return _CS1043;
            }
        }
        private static ICompilerReferenceError _CS1043;
        public static ICompilerReferenceError CS1044
        {
            get
            {
                if (_CS1044 == null)
                    _CS1044 = new CompilerReferenceError(@"Cannot use more than one type in a for, using, fixed, or declaration statement", 1044);
                return _CS1044;
            }
        }
        private static ICompilerReferenceError _CS1044;
        public static ICompilerReferenceError CS1055
        {
            get
            {
                if (_CS1055 == null)
                    _CS1055 = new CompilerReferenceError(@"An add or remove accessor expected", 1055);
                return _CS1055;
            }
        }
        private static ICompilerReferenceError _CS1055;
        public static ICompilerReferenceError CS1056
        {
            get
            {
                if (_CS1056 == null)
                    _CS1056 = new CompilerReferenceError(@"Unexpected character 'character'", 1056);
                return _CS1056;
            }
        }
        private static ICompilerReferenceError _CS1056;
        public static ICompilerReferenceError CS1057
        {
            get
            {
                if (_CS1057 == null)
                    _CS1057 = new CompilerReferenceError(@"'member': static classes cannot contain protected members", 1057);
                return _CS1057;
            }
        }
        private static ICompilerReferenceError _CS1057;
        public static ICompilerReferenceError CS1059
        {
            get
            {
                if (_CS1059 == null)
                    _CS1059 = new CompilerReferenceError(@"The operand of an increment or decrement operator must be a variable, property or indexer.", 1059);
                return _CS1059;
            }
        }
        private static ICompilerReferenceError _CS1059;
        public static ICompilerReferenceError CS1061
        {
            get
            {
                if (_CS1061 == null)
                    _CS1061 = new CompilerReferenceError(@"'type' does not contain a definition for 'member' and no extension method 'name' accepting a first argument of type 'type' could be found (are you missing a using directive or an assembly reference?).", 1061);
                return _CS1061;
            }
        }
        private static ICompilerReferenceError _CS1061;
        public static ICompilerReferenceError CS1100
        {
            get
            {
                if (_CS1100 == null)
                    _CS1100 = new CompilerReferenceError(@"Method 'name' has a parameter modifier 'this' which is not on the first parameter.", 1100);
                return _CS1100;
            }
        }
        private static ICompilerReferenceError _CS1100;
        public static ICompilerReferenceError CS1101
        {
            get
            {
                if (_CS1101 == null)
                    _CS1101 = new CompilerReferenceError(@"The parameter modifier 'ref' cannot be used with 'this'.", 1101);
                return _CS1101;
            }
        }
        private static ICompilerReferenceError _CS1101;
        public static ICompilerReferenceError CS1102
        {
            get
            {
                if (_CS1102 == null)
                    _CS1102 = new CompilerReferenceError(@"The parameter modifier 'out' cannot be used with 'this'.", 1102);
                return _CS1102;
            }
        }
        private static ICompilerReferenceError _CS1102;
        public static ICompilerReferenceError CS1103
        {
            get
            {
                if (_CS1103 == null)
                    _CS1103 = new CompilerReferenceError(@"The first parameter of an extension method cannot be of type 'type'.", 1103);
                return _CS1103;
            }
        }
        private static ICompilerReferenceError _CS1103;
        public static ICompilerReferenceError CS1104
        {
            get
            {
                if (_CS1104 == null)
                    _CS1104 = new CompilerReferenceError(@"A parameter array cannot be used with 'this' modifier on an extension method.", 1104);
                return _CS1104;
            }
        }
        private static ICompilerReferenceError _CS1104;
        public static ICompilerReferenceError CS1105
        {
            get
            {
                if (_CS1105 == null)
                    _CS1105 = new CompilerReferenceError(@"Extension methods must be static.", 1105);
                return _CS1105;
            }
        }
        private static ICompilerReferenceError _CS1105;
        public static ICompilerReferenceError CS1106
        {
            get
            {
                if (_CS1106 == null)
                    _CS1106 = new CompilerReferenceError(@"Extension methods must be defined in a non generic static class.", 1106);
                return _CS1106;
            }
        }
        private static ICompilerReferenceError _CS1106;
        public static ICompilerReferenceError CS1107
        {
            get
            {
                if (_CS1107 == null)
                    _CS1107 = new CompilerReferenceError(@"A parameter can only have one 'modifier name' modifier.", 1107);
                return _CS1107;
            }
        }
        private static ICompilerReferenceError _CS1107;
        public static ICompilerReferenceError CS1108
        {
            get
            {
                if (_CS1108 == null)
                    _CS1108 = new CompilerReferenceError(@"A parameter cannot have all the specified modifiers; there are too many modifiers on the parameter.", 1108);
                return _CS1108;
            }
        }
        private static ICompilerReferenceError _CS1108;
        public static ICompilerReferenceError CS1109
        {
            get
            {
                if (_CS1109 == null)
                    _CS1109 = new CompilerReferenceError(@"Extension Methods must be defined on top level static classes, 'name' is a nested class.", 1109);
                return _CS1109;
            }
        }
        private static ICompilerReferenceError _CS1109;
        public static ICompilerReferenceError CS1110
        {
            get
            {
                if (_CS1110 == null)
                    _CS1110 = new CompilerReferenceError(@"Cannot use 'this' modifier on first parameter of method declaration without a reference to System.Core.dll. Add a reference to System.Core.dll or remove 'this' modifier from the method declaration.", 1110);
                return _CS1110;
            }
        }
        private static ICompilerReferenceError _CS1110;
        public static ICompilerReferenceError CS1112
        {
            get
            {
                if (_CS1112 == null)
                    _CS1112 = new CompilerReferenceError(@"Do not use 'System.Runtime.CompilerServices.ExtensionAttribute'. Use the 'this' keyword instead.", 1112);
                return _CS1112;
            }
        }
        private static ICompilerReferenceError _CS1112;
        public static ICompilerReferenceError CS1113
        {
            get
            {
                if (_CS1113 == null)
                    _CS1113 = new CompilerReferenceError(@"Extension methods 'name' defined on value type 'name' cannot be used to create delegates.", 1113);
                return _CS1113;
            }
        }
        private static ICompilerReferenceError _CS1113;
        public static ICompilerReferenceError CS1501
        {
            get
            {
                if (_CS1501 == null)
                    _CS1501 = new CompilerReferenceError(@"No overload for method 'method' takes 'number' arguments", 1501);
                return _CS1501;
            }
        }
        private static ICompilerReferenceError _CS1501;
        public static ICompilerReferenceError CS1502
        {
            get
            {
                if (_CS1502 == null)
                    _CS1502 = new CompilerReferenceError(@"The best overloaded Add method 'name' for the collection initializer has some invalid arguments", 1502);
                return _CS1502;
            }
        }
        private static ICompilerReferenceError _CS1502;
        public static ICompilerReferenceError CS1503
        {
            get
            {
                if (_CS1503 == null)
                    _CS1503 = new CompilerReferenceError(@"The best overloaded Add method 'name for the collection initializer has some invalid arguments", 1503);
                return _CS1503;
            }
        }
        private static ICompilerReferenceError _CS1503;
        public static ICompilerReferenceError CS1504
        {
            get
            {
                if (_CS1504 == null)
                    _CS1504 = new CompilerReferenceError(@"Source file 'file' could not be opened ('reason')", 1504);
                return _CS1504;
            }
        }
        private static ICompilerReferenceError _CS1504;
        public static ICompilerReferenceError CS1507
        {
            get
            {
                if (_CS1507 == null)
                    _CS1507 = new CompilerReferenceError(@"Cannot link resource file 'file' when building a module", 1507);
                return _CS1507;
            }
        }
        private static ICompilerReferenceError _CS1507;
        public static ICompilerReferenceError CS1508
        {
            get
            {
                if (_CS1508 == null)
                    _CS1508 = new CompilerReferenceError(@"Resource identifier 'identifier' has already been used in this assembly", 1508);
                return _CS1508;
            }
        }
        private static ICompilerReferenceError _CS1508;
        public static ICompilerReferenceError CS1509
        {
            get
            {
                if (_CS1509 == null)
                    _CS1509 = new CompilerReferenceError(@"Referenced file 'file' is not an assembly; use '/addmodule' option instead", 1509);
                return _CS1509;
            }
        }
        private static ICompilerReferenceError _CS1509;
        public static ICompilerReferenceError CS1510
        {
            get
            {
                if (_CS1510 == null)
                    _CS1510 = new CompilerReferenceError(@"A ref or out argument must be an assignable variable", 1510);
                return _CS1510;
            }
        }
        private static ICompilerReferenceError _CS1510;
        public static ICompilerReferenceError CS1511
        {
            get
            {
                if (_CS1511 == null)
                    _CS1511 = new CompilerReferenceError(@"Keyword 'base' is not available in a static method", 1511);
                return _CS1511;
            }
        }
        private static ICompilerReferenceError _CS1511;
        public static ICompilerReferenceError CS1512
        {
            get
            {
                if (_CS1512 == null)
                    _CS1512 = new CompilerReferenceError(@"Keyword 'base' is not available in the current context", 1512);
                return _CS1512;
            }
        }
        private static ICompilerReferenceError _CS1512;
        public static ICompilerReferenceError CS1513
        {
            get
            {
                if (_CS1513 == null)
                    _CS1513 = new CompilerReferenceError(@"} expected", 1513);
                return _CS1513;
            }
        }
        private static ICompilerReferenceError _CS1513;
        public static ICompilerReferenceError CS1514
        {
            get
            {
                if (_CS1514 == null)
                    _CS1514 = new CompilerReferenceError(@"{ expected", 1514);
                return _CS1514;
            }
        }
        private static ICompilerReferenceError _CS1514;
        public static ICompilerReferenceError CS1515
        {
            get
            {
                if (_CS1515 == null)
                    _CS1515 = new CompilerReferenceError(@"'in' expected", 1515);
                return _CS1515;
            }
        }
        private static ICompilerReferenceError _CS1515;
        public static ICompilerReferenceError CS1517
        {
            get
            {
                if (_CS1517 == null)
                    _CS1517 = new CompilerReferenceError(@"Invalid preprocessor expression", 1517);
                return _CS1517;
            }
        }
        private static ICompilerReferenceError _CS1517;
        public static ICompilerReferenceError CS1518
        {
            get
            {
                if (_CS1518 == null)
                    _CS1518 = new CompilerReferenceError(@"Expected class, delegate, enum, interface, or struct", 1518);
                return _CS1518;
            }
        }
        private static ICompilerReferenceError _CS1518;
        public static ICompilerReferenceError CS1519
        {
            get
            {
                if (_CS1519 == null)
                    _CS1519 = new CompilerReferenceError(@"Invalid token 'token' in class, struct, or interface member declaration", 1519);
                return _CS1519;
            }
        }
        private static ICompilerReferenceError _CS1519;
        public static ICompilerReferenceError CS1520
        {
            get
            {
                if (_CS1520 == null)
                    _CS1520 = new CompilerReferenceError(@"Method must have a return type", 1520);
                return _CS1520;
            }
        }
        private static ICompilerReferenceError _CS1520;
        public static ICompilerReferenceError CS1521
        {
            get
            {
                if (_CS1521 == null)
                    _CS1521 = new CompilerReferenceError(@"Invalid base type", 1521);
                return _CS1521;
            }
        }
        private static ICompilerReferenceError _CS1521;
        public static ICompilerReferenceError CS1524
        {
            get
            {
                if (_CS1524 == null)
                    _CS1524 = new CompilerReferenceError(@"Expected catch or finally", 1524);
                return _CS1524;
            }
        }
        private static ICompilerReferenceError _CS1524;
        public static ICompilerReferenceError CS1525
        {
            get
            {
                if (_CS1525 == null)
                    _CS1525 = new CompilerReferenceError(@"Invalid expression term 'character'", 1525);
                return _CS1525;
            }
        }
        private static ICompilerReferenceError _CS1525;
        public static ICompilerReferenceError CS1526
        {
            get
            {
                if (_CS1526 == null)
                    _CS1526 = new CompilerReferenceError(@"A new expression requires (), [], or {} after type", 1526);
                return _CS1526;
            }
        }
        private static ICompilerReferenceError _CS1526;
        public static ICompilerReferenceError CS1527
        {
            get
            {
                if (_CS1527 == null)
                    _CS1527 = new CompilerReferenceError(@"Elements defined in a namespace cannot be explicitly declared as private, protected, or protected internal", 1527);
                return _CS1527;
            }
        }
        private static ICompilerReferenceError _CS1527;
        public static ICompilerReferenceError CS1528
        {
            get
            {
                if (_CS1528 == null)
                    _CS1528 = new CompilerReferenceError(@"Expected ; or = (cannot specify constructor arguments in declaration)", 1528);
                return _CS1528;
            }
        }
        private static ICompilerReferenceError _CS1528;
        public static ICompilerReferenceError CS1529
        {
            get
            {
                if (_CS1529 == null)
                    _CS1529 = new CompilerReferenceError(@"A using clause must precede all other elements defined in the namespace except extern alias declarations", 1529);
                return _CS1529;
            }
        }
        private static ICompilerReferenceError _CS1529;
        public static ICompilerReferenceError CS1530
        {
            get
            {
                if (_CS1530 == null)
                    _CS1530 = new CompilerReferenceError(@"Keyword 'new' is not allowed on elements defined in a namespace", 1530);
                return _CS1530;
            }
        }
        private static ICompilerReferenceError _CS1530;
        public static ICompilerReferenceError CS1534
        {
            get
            {
                if (_CS1534 == null)
                    _CS1534 = new CompilerReferenceError(@"Overloaded binary operator 'operator' takes two parameters", 1534);
                return _CS1534;
            }
        }
        private static ICompilerReferenceError _CS1534;
        public static ICompilerReferenceError CS1535
        {
            get
            {
                if (_CS1535 == null)
                    _CS1535 = new CompilerReferenceError(@"Overloaded unary operator 'operator' takes one parameter", 1535);
                return _CS1535;
            }
        }
        private static ICompilerReferenceError _CS1535;
        public static ICompilerReferenceError CS1536
        {
            get
            {
                if (_CS1536 == null)
                    _CS1536 = new CompilerReferenceError(@"Invalid parameter type void", 1536);
                return _CS1536;
            }
        }
        private static ICompilerReferenceError _CS1536;
        public static ICompilerReferenceError CS1537
        {
            get
            {
                if (_CS1537 == null)
                    _CS1537 = new CompilerReferenceError(@"The using alias 'alias' appeared previously in this namespace", 1537);
                return _CS1537;
            }
        }
        private static ICompilerReferenceError _CS1537;
        public static ICompilerReferenceError CS1540
        {
            get
            {
                if (_CS1540 == null)
                    _CS1540 = new CompilerReferenceError(@"Cannot access protected member 'member' via a qualifier of type 'type1'; the qualifier must be of type 'type2' (or derived from it)", 1540);
                return _CS1540;
            }
        }
        private static ICompilerReferenceError _CS1540;
        public static ICompilerReferenceError CS1541
        {
            get
            {
                if (_CS1541 == null)
                    _CS1541 = new CompilerReferenceError(@"Invalid reference option: 'symbol' — cannot reference directories", 1541);
                return _CS1541;
            }
        }
        private static ICompilerReferenceError _CS1541;
        public static ICompilerReferenceError CS1542
        {
            get
            {
                if (_CS1542 == null)
                    _CS1542 = new CompilerReferenceError(@"'dll' cannot be added to this assembly because it already is an assembly; use '/R' option instead", 1542);
                return _CS1542;
            }
        }
        private static ICompilerReferenceError _CS1542;
        public static ICompilerReferenceError CS1545
        {
            get
            {
                if (_CS1545 == null)
                    _CS1545 = new CompilerReferenceError(@"Property, indexer, or event 'property' is not supported by the language; try directly calling accessor methods 'set accessor' or 'get accessor'", 1545);
                return _CS1545;
            }
        }
        private static ICompilerReferenceError _CS1545;
        public static ICompilerReferenceError CS1546
        {
            get
            {
                if (_CS1546 == null)
                    _CS1546 = new CompilerReferenceError(@"Property, indexer, or event 'property' is not supported by the language; try directly calling accessor method 'accessor'", 1546);
                return _CS1546;
            }
        }
        private static ICompilerReferenceError _CS1546;
        public static ICompilerReferenceError CS1547
        {
            get
            {
                if (_CS1547 == null)
                    _CS1547 = new CompilerReferenceError(@"Keyword 'void' cannot be used in this context", 1547);
                return _CS1547;
            }
        }
        private static ICompilerReferenceError _CS1547;
        public static ICompilerReferenceError CS1548
        {
            get
            {
                if (_CS1548 == null)
                    _CS1548 = new CompilerReferenceError(@"Cryptographic failure while signing assembly 'assembly' — 'reason'", 1548);
                return _CS1548;
            }
        }
        private static ICompilerReferenceError _CS1548;
        public static ICompilerReferenceError CS1549
        {
            get
            {
                if (_CS1549 == null)
                    _CS1549 = new CompilerReferenceError(@"Appropriate cryptographic service not found", 1549);
                return _CS1549;
            }
        }
        private static ICompilerReferenceError _CS1549;
        public static ICompilerReferenceError CS1551
        {
            get
            {
                if (_CS1551 == null)
                    _CS1551 = new CompilerReferenceError(@"Indexers must have at least one parameter", 1551);
                return _CS1551;
            }
        }
        private static ICompilerReferenceError _CS1551;
        public static ICompilerReferenceError CS1552
        {
            get
            {
                if (_CS1552 == null)
                    _CS1552 = new CompilerReferenceError(@"Array type specifier, [], must appear before parameter name", 1552);
                return _CS1552;
            }
        }
        private static ICompilerReferenceError _CS1552;
        public static ICompilerReferenceError CS1553
        {
            get
            {
                if (_CS1553 == null)
                    _CS1553 = new CompilerReferenceError(@"Declaration is not valid; use 'modifier operator <dest-type> (...' instead", 1553);
                return _CS1553;
            }
        }
        private static ICompilerReferenceError _CS1553;
        public static ICompilerReferenceError CS1554
        {
            get
            {
                if (_CS1554 == null)
                    _CS1554 = new CompilerReferenceError(@"Declaration is not valid; use '<type> operator op (...' instead", 1554);
                return _CS1554;
            }
        }
        private static ICompilerReferenceError _CS1554;
        public static ICompilerReferenceError CS1555
        {
            get
            {
                if (_CS1555 == null)
                    _CS1555 = new CompilerReferenceError(@"Could not find 'class' specified for Main method", 1555);
                return _CS1555;
            }
        }
        private static ICompilerReferenceError _CS1555;
        public static ICompilerReferenceError CS1556
        {
            get
            {
                if (_CS1556 == null)
                    _CS1556 = new CompilerReferenceError(@"'construct' specified for Main method must be a valid class or struct", 1556);
                return _CS1556;
            }
        }
        private static ICompilerReferenceError _CS1556;
        public static ICompilerReferenceError CS1557
        {
            get
            {
                if (_CS1557 == null)
                    _CS1557 = new CompilerReferenceError(@"Cannot use 'class' for Main method because it is in a different output file", 1557);
                return _CS1557;
            }
        }
        private static ICompilerReferenceError _CS1557;
        public static ICompilerReferenceError CS1558
        {
            get
            {
                if (_CS1558 == null)
                    _CS1558 = new CompilerReferenceError(@"'class' does not have a suitable static Main method", 1558);
                return _CS1558;
            }
        }
        private static ICompilerReferenceError _CS1558;
        public static ICompilerReferenceError CS1559
        {
            get
            {
                if (_CS1559 == null)
                    _CS1559 = new CompilerReferenceError(@"Cannot use 'object' for Main method because it is imported", 1559);
                return _CS1559;
            }
        }
        private static ICompilerReferenceError _CS1559;
        public static ICompilerReferenceError CS1560
        {
            get
            {
                if (_CS1560 == null)
                    _CS1560 = new CompilerReferenceError(@"Invalid filename specified for preprocessor directive. Filename is too long or not a valid filename", 1560);
                return _CS1560;
            }
        }
        private static ICompilerReferenceError _CS1560;
        public static ICompilerReferenceError CS1561
        {
            get
            {
                if (_CS1561 == null)
                    _CS1561 = new CompilerReferenceError(@"Output filename is too long or invalid", 1561);
                return _CS1561;
            }
        }
        private static ICompilerReferenceError _CS1561;
        public static ICompilerReferenceError CS1562
        {
            get
            {
                if (_CS1562 == null)
                    _CS1562 = new CompilerReferenceError(@"Outputs without source must have the /out option specified", 1562);
                return _CS1562;
            }
        }
        private static ICompilerReferenceError _CS1562;
        public static ICompilerReferenceError CS1563
        {
            get
            {
                if (_CS1563 == null)
                    _CS1563 = new CompilerReferenceError(@"Output 'output file' does not have any source files", 1563);
                return _CS1563;
            }
        }
        private static ICompilerReferenceError _CS1563;
        public static ICompilerReferenceError CS1564
        {
            get
            {
                if (_CS1564 == null)
                    _CS1564 = new CompilerReferenceError(@"Conflicting options specified: Win32 resource file; Win32 manifest.", 1564);
                return _CS1564;
            }
        }
        private static ICompilerReferenceError _CS1564;
        public static ICompilerReferenceError CS1565
        {
            get
            {
                if (_CS1565 == null)
                    _CS1565 = new CompilerReferenceError(@"Conflicting options specified: Win32 resource file; Win32 icon", 1565);
                return _CS1565;
            }
        }
        private static ICompilerReferenceError _CS1565;
        public static ICompilerReferenceError CS1566
        {
            get
            {
                if (_CS1566 == null)
                    _CS1566 = new CompilerReferenceError(@"Error reading resource file 'file' — 'reason'", 1566);
                return _CS1566;
            }
        }
        private static ICompilerReferenceError _CS1566;
        public static ICompilerReferenceError CS1567
        {
            get
            {
                if (_CS1567 == null)
                    _CS1567 = new CompilerReferenceError(@"Error generating Win32 resource: 'file'", 1567);
                return _CS1567;
            }
        }
        private static ICompilerReferenceError _CS1567;
        public static ICompilerReferenceError CS1569
        {
            get
            {
                if (_CS1569 == null)
                    _CS1569 = new CompilerReferenceError(@"Error generating XML documentation file 'Filename' ('reason')", 1569);
                return _CS1569;
            }
        }
        private static ICompilerReferenceError _CS1569;
        public static ICompilerReferenceError CS1575
        {
            get
            {
                if (_CS1575 == null)
                    _CS1575 = new CompilerReferenceError(@"A stackalloc expression requires [] after type", 1575);
                return _CS1575;
            }
        }
        private static ICompilerReferenceError _CS1575;
        public static ICompilerReferenceError CS1576
        {
            get
            {
                if (_CS1576 == null)
                    _CS1576 = new CompilerReferenceError(@"The line number specified for #line directive is missing or invalid", 1576);
                return _CS1576;
            }
        }
        private static ICompilerReferenceError _CS1576;
        public static ICompilerReferenceError CS1577
        {
            get
            {
                if (_CS1577 == null)
                    _CS1577 = new CompilerReferenceError(@"Assembly generation failed — reason", 1577);
                return _CS1577;
            }
        }
        private static ICompilerReferenceError _CS1577;
        public static ICompilerReferenceError CS1578
        {
            get
            {
                if (_CS1578 == null)
                    _CS1578 = new CompilerReferenceError(@"Filename, single-line comment or end-of-line expected", 1578);
                return _CS1578;
            }
        }
        private static ICompilerReferenceError _CS1578;
        public static ICompilerReferenceError CS1579
        {
            get
            {
                if (_CS1579 == null)
                    _CS1579 = new CompilerReferenceError(@"foreach statement cannot operate on variables of type 'type1' because 'type2' does not contain a public definition for 'identifier'", 1579);
                return _CS1579;
            }
        }
        private static ICompilerReferenceError _CS1579;
        public static ICompilerReferenceError CS1583
        {
            get
            {
                if (_CS1583 == null)
                    _CS1583 = new CompilerReferenceError(@"'file' is not a valid Win32 resource file", 1583);
                return _CS1583;
            }
        }
        private static ICompilerReferenceError _CS1583;
        public static ICompilerReferenceError CS1585
        {
            get
            {
                if (_CS1585 == null)
                    _CS1585 = new CompilerReferenceError(@"Member modifier 'keyword' must precede the member type and name", 1585);
                return _CS1585;
            }
        }
        private static ICompilerReferenceError _CS1585;
        public static ICompilerReferenceError CS1586
        {
            get
            {
                if (_CS1586 == null)
                    _CS1586 = new CompilerReferenceError(@"Array creation must have array size or array initializer", 1586);
                return _CS1586;
            }
        }
        private static ICompilerReferenceError _CS1586;
        public static ICompilerReferenceError CS1588
        {
            get
            {
                if (_CS1588 == null)
                    _CS1588 = new CompilerReferenceError(@"Cannot determine common language runtime directory -- 'reason'", 1588);
                return _CS1588;
            }
        }
        private static ICompilerReferenceError _CS1588;
        public static ICompilerReferenceError CS1593
        {
            get
            {
                if (_CS1593 == null)
                    _CS1593 = new CompilerReferenceError(@"Delegate 'del' does not take 'number' arguments", 1593);
                return _CS1593;
            }
        }
        private static ICompilerReferenceError _CS1593;
        public static ICompilerReferenceError CS1594
        {
            get
            {
                if (_CS1594 == null)
                    _CS1594 = new CompilerReferenceError(@"Delegate 'delegate' has some invalid arguments", 1594);
                return _CS1594;
            }
        }
        private static ICompilerReferenceError _CS1594;
        public static ICompilerReferenceError CS1597
        {
            get
            {
                if (_CS1597 == null)
                    _CS1597 = new CompilerReferenceError(@"Semicolon after method or accessor block is not valid", 1597);
                return _CS1597;
            }
        }
        private static ICompilerReferenceError _CS1597;
        public static ICompilerReferenceError CS1599
        {
            get
            {
                if (_CS1599 == null)
                    _CS1599 = new CompilerReferenceError(@"Method or delegate cannot return type 'type'", 1599);
                return _CS1599;
            }
        }
        private static ICompilerReferenceError _CS1599;
        public static ICompilerReferenceError CS1600
        {
            get
            {
                if (_CS1600 == null)
                    _CS1600 = new CompilerReferenceError(@"Compilation cancelled by user", 1600);
                return _CS1600;
            }
        }
        private static ICompilerReferenceError _CS1600;
        public static ICompilerReferenceError CS1601
        {
            get
            {
                if (_CS1601 == null)
                    _CS1601 = new CompilerReferenceError(@"Method or delegate parameter cannot be of type 'type'", 1601);
                return _CS1601;
            }
        }
        private static ICompilerReferenceError _CS1601;
        public static ICompilerReferenceError CS1604
        {
            get
            {
                if (_CS1604 == null)
                    _CS1604 = new CompilerReferenceError(@"Cannot assign to 'variable' because it is read-only", 1604);
                return _CS1604;
            }
        }
        private static ICompilerReferenceError _CS1604;
        public static ICompilerReferenceError CS1605
        {
            get
            {
                if (_CS1605 == null)
                    _CS1605 = new CompilerReferenceError(@"Cannot pass 'var' as a ref or out argument because it is read-only", 1605);
                return _CS1605;
            }
        }
        private static ICompilerReferenceError _CS1605;
        public static ICompilerReferenceError CS1606
        {
            get
            {
                if (_CS1606 == null)
                    _CS1606 = new CompilerReferenceError(@"Assembly signing failed; output may not be signed -- reason", 1606);
                return _CS1606;
            }
        }
        private static ICompilerReferenceError _CS1606;
        public static ICompilerReferenceError CS1608
        {
            get
            {
                if (_CS1608 == null)
                    _CS1608 = new CompilerReferenceError(@"The Required attribute is not permitted on C# types", 1608);
                return _CS1608;
            }
        }
        private static ICompilerReferenceError _CS1608;
        public static ICompilerReferenceError CS1609
        {
            get
            {
                if (_CS1609 == null)
                    _CS1609 = new CompilerReferenceError(@"Modifiers cannot be placed on event accessor declarations", 1609);
                return _CS1609;
            }
        }
        private static ICompilerReferenceError _CS1609;
        public static ICompilerReferenceError CS1611
        {
            get
            {
                if (_CS1611 == null)
                    _CS1611 = new CompilerReferenceError(@"The params parameter cannot be declared as ref or out", 1611);
                return _CS1611;
            }
        }
        private static ICompilerReferenceError _CS1611;
        public static ICompilerReferenceError CS1612
        {
            get
            {
                if (_CS1612 == null)
                    _CS1612 = new CompilerReferenceError(@"Cannot modify the return value of 'expression' because it is not a variable", 1612);
                return _CS1612;
            }
        }
        private static ICompilerReferenceError _CS1612;
        public static ICompilerReferenceError CS1613
        {
            get
            {
                if (_CS1613 == null)
                    _CS1613 = new CompilerReferenceError(@"The managed coclass wrapper class 'class' for interface 'interface' cannot be found (are you missing an assembly reference?)", 1613);
                return _CS1613;
            }
        }
        private static ICompilerReferenceError _CS1613;
        public static ICompilerReferenceError CS1614
        {
            get
            {
                if (_CS1614 == null)
                    _CS1614 = new CompilerReferenceError(@"'name' is ambiguous; between 'attribute1' and 'attribute2'. use either '@attribute' or 'attributeAttribute'", 1614);
                return _CS1614;
            }
        }
        private static ICompilerReferenceError _CS1614;
        public static ICompilerReferenceError CS1615
        {
            get
            {
                if (_CS1615 == null)
                    _CS1615 = new CompilerReferenceError(@"Argument 'number' should not be passed with the 'keyword' keyword", 1615);
                return _CS1615;
            }
        }
        private static ICompilerReferenceError _CS1615;
        public static ICompilerReferenceError CS1617
        {
            get
            {
                if (_CS1617 == null)
                    _CS1617 = new CompilerReferenceError(@"Invalid option 'option' for /langversion; must be ISO-1, ISO-2 or Default", 1617);
                return _CS1617;
            }
        }
        private static ICompilerReferenceError _CS1617;
        public static ICompilerReferenceError CS1618
        {
            get
            {
                if (_CS1618 == null)
                    _CS1618 = new CompilerReferenceError(@"Cannot create delegate with 'method' because it has a Conditional attribute", 1618);
                return _CS1618;
            }
        }
        private static ICompilerReferenceError _CS1618;
        public static ICompilerReferenceError CS1619
        {
            get
            {
                if (_CS1619 == null)
                    _CS1619 = new CompilerReferenceError(@"Cannot create temporary file 'filename' -- reason", 1619);
                return _CS1619;
            }
        }
        private static ICompilerReferenceError _CS1619;
        public static ICompilerReferenceError CS1620
        {
            get
            {
                if (_CS1620 == null)
                    _CS1620 = new CompilerReferenceError(@"Argument 'number' must be passed with the 'keyword' keyword", 1620);
                return _CS1620;
            }
        }
        private static ICompilerReferenceError _CS1620;
        public static ICompilerReferenceError CS1621
        {
            get
            {
                if (_CS1621 == null)
                    _CS1621 = new CompilerReferenceError(@"The yield statement cannot be used inside an anonymous method or lambda expression", 1621);
                return _CS1621;
            }
        }
        private static ICompilerReferenceError _CS1621;
        public static ICompilerReferenceError CS1622
        {
            get
            {
                if (_CS1622 == null)
                    _CS1622 = new CompilerReferenceError(@"Cannot return a value from an iterator. Use the yield return statement to return a value, or yield break to end the iteration.", 1622);
                return _CS1622;
            }
        }
        private static ICompilerReferenceError _CS1622;
        public static ICompilerReferenceError CS1623
        {
            get
            {
                if (_CS1623 == null)
                    _CS1623 = new CompilerReferenceError(@"Iterators cannot have ref or out parameters", 1623);
                return _CS1623;
            }
        }
        private static ICompilerReferenceError _CS1623;
        public static ICompilerReferenceError CS1624
        {
            get
            {
                if (_CS1624 == null)
                    _CS1624 = new CompilerReferenceError(@"The body of 'accessor' cannot be an iterator block because 'type' is not an iterator interface type", 1624);
                return _CS1624;
            }
        }
        private static ICompilerReferenceError _CS1624;
        public static ICompilerReferenceError CS1625
        {
            get
            {
                if (_CS1625 == null)
                    _CS1625 = new CompilerReferenceError(@"Cannot yield in the body of a finally clause", 1625);
                return _CS1625;
            }
        }
        private static ICompilerReferenceError _CS1625;
        public static ICompilerReferenceError CS1626
        {
            get
            {
                if (_CS1626 == null)
                    _CS1626 = new CompilerReferenceError(@"Cannot yield a value in the body of a try block with a catch clause", 1626);
                return _CS1626;
            }
        }
        private static ICompilerReferenceError _CS1626;
        public static ICompilerReferenceError CS1627
        {
            get
            {
                if (_CS1627 == null)
                    _CS1627 = new CompilerReferenceError(@"Expression expected after yield return", 1627);
                return _CS1627;
            }
        }
        private static ICompilerReferenceError _CS1627;
        public static ICompilerReferenceError CS1628
        {
            get
            {
                if (_CS1628 == null)
                    _CS1628 = new CompilerReferenceError(@"Cannot use ref or out parameter 'parameter' inside an anonymous method, lambda expression, or query expression", 1628);
                return _CS1628;
            }
        }
        private static ICompilerReferenceError _CS1628;
        public static ICompilerReferenceError CS1629
        {
            get
            {
                if (_CS1629 == null)
                    _CS1629 = new CompilerReferenceError(@"Unsafe code may not appear in iterators", 1629);
                return _CS1629;
            }
        }
        private static ICompilerReferenceError _CS1629;
        public static ICompilerReferenceError CS1630
        {
            get
            {
                if (_CS1630 == null)
                    _CS1630 = new CompilerReferenceError(@"Invalid option 'option' for /errorreport; must be prompt, send, queue, or none", 1630);
                return _CS1630;
            }
        }
        private static ICompilerReferenceError _CS1630;
        public static ICompilerReferenceError CS1631
        {
            get
            {
                if (_CS1631 == null)
                    _CS1631 = new CompilerReferenceError(@"Cannot yield a value in the body of a catch clause", 1631);
                return _CS1631;
            }
        }
        private static ICompilerReferenceError _CS1631;
        public static ICompilerReferenceError CS1632
        {
            get
            {
                if (_CS1632 == null)
                    _CS1632 = new CompilerReferenceError(@"Control cannot leave the body of an anonymous method or lambda expression", 1632);
                return _CS1632;
            }
        }
        private static ICompilerReferenceError _CS1632;
        public static ICompilerReferenceError CS1637
        {
            get
            {
                if (_CS1637 == null)
                    _CS1637 = new CompilerReferenceError(@"Iterators cannot have unsafe parameters or yield types", 1637);
                return _CS1637;
            }
        }
        private static ICompilerReferenceError _CS1637;
        public static ICompilerReferenceError CS1638
        {
            get
            {
                if (_CS1638 == null)
                    _CS1638 = new CompilerReferenceError(@"'identifier' is a reserved identifier and cannot be used when ISO language version mode is used", 1638);
                return _CS1638;
            }
        }
        private static ICompilerReferenceError _CS1638;
        public static ICompilerReferenceError CS1639
        {
            get
            {
                if (_CS1639 == null)
                    _CS1639 = new CompilerReferenceError(@"The managed coclass wrapper class signature 'signature' for interface 'interface' is not a valid class name signature", 1639);
                return _CS1639;
            }
        }
        private static ICompilerReferenceError _CS1639;
        public static ICompilerReferenceError CS1640
        {
            get
            {
                if (_CS1640 == null)
                    _CS1640 = new CompilerReferenceError(@"foreach statement cannot operate on variables of type 'type' because it implements multiple instantiations of 'interface', try casting to a specific interface instantiation", 1640);
                return _CS1640;
            }
        }
        private static ICompilerReferenceError _CS1640;
        public static ICompilerReferenceError CS1641
        {
            get
            {
                if (_CS1641 == null)
                    _CS1641 = new CompilerReferenceError(@"A fixed size buffer field must have the array size specifier after the field name", 1641);
                return _CS1641;
            }
        }
        private static ICompilerReferenceError _CS1641;
        public static ICompilerReferenceError CS1642
        {
            get
            {
                if (_CS1642 == null)
                    _CS1642 = new CompilerReferenceError(@"Fixed size buffer fields may only be members of structs.", 1642);
                return _CS1642;
            }
        }
        private static ICompilerReferenceError _CS1642;
        public static ICompilerReferenceError CS1643
        {
            get
            {
                if (_CS1643 == null)
                    _CS1643 = new CompilerReferenceError(@"Not all code paths return a value in method of type 'type!'", 1643);
                return _CS1643;
            }
        }
        private static ICompilerReferenceError _CS1643;
        public static ICompilerReferenceError CS1644
        {
            get
            {
                if (_CS1644 == null)
                    _CS1644 = new CompilerReferenceError(@"Feature 'feature' is not part of the standardized ISO C# language specification, and may not be accepted by other compilers", 1644);
                return _CS1644;
            }
        }
        private static ICompilerReferenceError _CS1644;
        public static ICompilerReferenceError CS1646
        {
            get
            {
                if (_CS1646 == null)
                    _CS1646 = new CompilerReferenceError(@"Keyword, identifier, or string expected after verbatim specifier: @", 1646);
                return _CS1646;
            }
        }
        private static ICompilerReferenceError _CS1646;
        public static ICompilerReferenceError CS1647
        {
            get
            {
                if (_CS1647 == null)
                    _CS1647 = new CompilerReferenceError(@"An expression is too long or complex to compile near 'code'", 1647);
                return _CS1647;
            }
        }
        private static ICompilerReferenceError _CS1647;
        public static ICompilerReferenceError CS1648
        {
            get
            {
                if (_CS1648 == null)
                    _CS1648 = new CompilerReferenceError(@"Members of readonly field 'identifier' cannot be modified (except in a constructor or a variable initializer)", 1648);
                return _CS1648;
            }
        }
        private static ICompilerReferenceError _CS1648;
        public static ICompilerReferenceError CS1649
        {
            get
            {
                if (_CS1649 == null)
                    _CS1649 = new CompilerReferenceError(@"Members of readonly field 'identifier' cannot be passed ref or out (except in a constructor)", 1649);
                return _CS1649;
            }
        }
        private static ICompilerReferenceError _CS1649;
        public static ICompilerReferenceError CS1650
        {
            get
            {
                if (_CS1650 == null)
                    _CS1650 = new CompilerReferenceError(@"Fields of static readonly field 'identifier' cannot be assigned to (except in a static constructor or a variable initializer)", 1650);
                return _CS1650;
            }
        }
        private static ICompilerReferenceError _CS1650;
        public static ICompilerReferenceError CS1651
        {
            get
            {
                if (_CS1651 == null)
                    _CS1651 = new CompilerReferenceError(@"Fields of static readonly field 'identifier' cannot be passed ref or out (except in a static constructor)", 1651);
                return _CS1651;
            }
        }
        private static ICompilerReferenceError _CS1651;
        public static ICompilerReferenceError CS1654
        {
            get
            {
                if (_CS1654 == null)
                    _CS1654 = new CompilerReferenceError(@"Cannot modify members of 'variable' because it is a 'read-only variable type'", 1654);
                return _CS1654;
            }
        }
        private static ICompilerReferenceError _CS1654;
        public static ICompilerReferenceError CS1655
        {
            get
            {
                if (_CS1655 == null)
                    _CS1655 = new CompilerReferenceError(@"Cannot pass fields of 'variable' as a ref or out argument because it is a 'readonly variable type'", 1655);
                return _CS1655;
            }
        }
        private static ICompilerReferenceError _CS1655;
        public static ICompilerReferenceError CS1656
        {
            get
            {
                if (_CS1656 == null)
                    _CS1656 = new CompilerReferenceError(@"Cannot assign to 'variable' because it is a 'read-only variable type'", 1656);
                return _CS1656;
            }
        }
        private static ICompilerReferenceError _CS1656;
        public static ICompilerReferenceError CS1657
        {
            get
            {
                if (_CS1657 == null)
                    _CS1657 = new CompilerReferenceError(@"Cannot pass 'parameter' as a ref or out argument because 'reason''", 1657);
                return _CS1657;
            }
        }
        private static ICompilerReferenceError _CS1657;
        public static ICompilerReferenceError CS1660
        {
            get
            {
                if (_CS1660 == null)
                    _CS1660 = new CompilerReferenceError(@"Cannot convert anonymous method block to type 'type' because it is not a delegate type", 1660);
                return _CS1660;
            }
        }
        private static ICompilerReferenceError _CS1660;
        public static ICompilerReferenceError CS1661
        {
            get
            {
                if (_CS1661 == null)
                    _CS1661 = new CompilerReferenceError(@"Cannot convert anonymous method block to delegate type 'delegate type' because the specified block's parameter types do not match the delegate parameter types", 1661);
                return _CS1661;
            }
        }
        private static ICompilerReferenceError _CS1661;
        public static ICompilerReferenceError CS1662
        {
            get
            {
                if (_CS1662 == null)
                    _CS1662 = new CompilerReferenceError(@"Cannot convert anonymous method block to delegate type 'delegate type' because some of the return types in the block are not implicitly convertible to the delegate return type", 1662);
                return _CS1662;
            }
        }
        private static ICompilerReferenceError _CS1662;
        public static ICompilerReferenceError CS1663
        {
            get
            {
                if (_CS1663 == null)
                    _CS1663 = new CompilerReferenceError(@"Fixed size buffer type must be one of the following: bool, byte, short, int, long, char, sbyte, ushort, uint, ulong, float or double", 1663);
                return _CS1663;
            }
        }
        private static ICompilerReferenceError _CS1663;
        public static ICompilerReferenceError CS1664
        {
            get
            {
                if (_CS1664 == null)
                    _CS1664 = new CompilerReferenceError(@"Fixed size buffer of length 'length' and type 'type' is too big", 1664);
                return _CS1664;
            }
        }
        private static ICompilerReferenceError _CS1664;
        public static ICompilerReferenceError CS1665
        {
            get
            {
                if (_CS1665 == null)
                    _CS1665 = new CompilerReferenceError(@"Fixed size buffers must have a length greater than zero", 1665);
                return _CS1665;
            }
        }
        private static ICompilerReferenceError _CS1665;
        public static ICompilerReferenceError CS1666
        {
            get
            {
                if (_CS1666 == null)
                    _CS1666 = new CompilerReferenceError(@"You cannot use fixed size buffers contained in unfixed expressions. Try using the fixed statement.", 1666);
                return _CS1666;
            }
        }
        private static ICompilerReferenceError _CS1666;
        public static ICompilerReferenceError CS1667
        {
            get
            {
                if (_CS1667 == null)
                    _CS1667 = new CompilerReferenceError(@"Attribute 'attribute' is not valid on property or event accessors. It is valid on 'declaration type' declarations only.", 1667);
                return _CS1667;
            }
        }
        private static ICompilerReferenceError _CS1667;
        public static ICompilerReferenceError CS1670
        {
            get
            {
                if (_CS1670 == null)
                    _CS1670 = new CompilerReferenceError(@"params is not valid in this context", 1670);
                return _CS1670;
            }
        }
        private static ICompilerReferenceError _CS1670;
        public static ICompilerReferenceError CS1671
        {
            get
            {
                if (_CS1671 == null)
                    _CS1671 = new CompilerReferenceError(@"A namespace declaration cannot have modifiers or attributes", 1671);
                return _CS1671;
            }
        }
        private static ICompilerReferenceError _CS1671;
        public static ICompilerReferenceError CS1672
        {
            get
            {
                if (_CS1672 == null)
                    _CS1672 = new CompilerReferenceError(@"Invalid option 'option' for /platform; must be anycpu, x86, Itanium or x64", 1672);
                return _CS1672;
            }
        }
        private static ICompilerReferenceError _CS1672;
        public static ICompilerReferenceError CS1673
        {
            get
            {
                if (_CS1673 == null)
                    _CS1673 = new CompilerReferenceError(@"Anonymous methods, lambda expressions, and query expressions inside structs cannot access instance members of 'this'. Consider copying 'this' to a local variable outside the anonymous method, lambda expression or query expression and using the local instead.", 1673);
                return _CS1673;
            }
        }
        private static ICompilerReferenceError _CS1673;
        public static ICompilerReferenceError CS1674
        {
            get
            {
                if (_CS1674 == null)
                    _CS1674 = new CompilerReferenceError(@"'T': type used in a using statement must be implicitly convertible to 'System.IDisposable'", 1674);
                return _CS1674;
            }
        }
        private static ICompilerReferenceError _CS1674;
        public static ICompilerReferenceError CS1675
        {
            get
            {
                if (_CS1675 == null)
                    _CS1675 = new CompilerReferenceError(@"Enums cannot have type parameters", 1675);
                return _CS1675;
            }
        }
        private static ICompilerReferenceError _CS1675;
        public static ICompilerReferenceError CS1676
        {
            get
            {
                if (_CS1676 == null)
                    _CS1676 = new CompilerReferenceError(@"Parameter 'number' must be declared with the 'keyword' keyword", 1676);
                return _CS1676;
            }
        }
        private static ICompilerReferenceError _CS1676;
        public static ICompilerReferenceError CS1677
        {
            get
            {
                if (_CS1677 == null)
                    _CS1677 = new CompilerReferenceError(@"Parameter 'number' should not be declared with the 'keyword' keyword", 1677);
                return _CS1677;
            }
        }
        private static ICompilerReferenceError _CS1677;
        public static ICompilerReferenceError CS1678
        {
            get
            {
                if (_CS1678 == null)
                    _CS1678 = new CompilerReferenceError(@"Parameter 'number' is declared as type 'type1' but should be 'type2'", 1678);
                return _CS1678;
            }
        }
        private static ICompilerReferenceError _CS1678;
        public static ICompilerReferenceError CS1679
        {
            get
            {
                if (_CS1679 == null)
                    _CS1679 = new CompilerReferenceError(@"Invalid extern alias for '/reference'; 'identifier' is not a valid identifier", 1679);
                return _CS1679;
            }
        }
        private static ICompilerReferenceError _CS1679;
        public static ICompilerReferenceError CS1680
        {
            get
            {
                if (_CS1680 == null)
                    _CS1680 = new CompilerReferenceError(@"Invalid reference alias option: 'alias=' -- missing filename.", 1680);
                return _CS1680;
            }
        }
        private static ICompilerReferenceError _CS1680;
        public static ICompilerReferenceError CS1681
        {
            get
            {
                if (_CS1681 == null)
                    _CS1681 = new CompilerReferenceError(@"You cannot redefine the global extern alias", 1681);
                return _CS1681;
            }
        }
        private static ICompilerReferenceError _CS1681;
        public static ICompilerReferenceError CS1686
        {
            get
            {
                if (_CS1686 == null)
                    _CS1686 = new CompilerReferenceError(@"Local 'variable' or its members cannot have their address taken and be used inside an anonymous method or lambda expression", 1686);
                return _CS1686;
            }
        }
        private static ICompilerReferenceError _CS1686;
        public static ICompilerReferenceError CS1688
        {
            get
            {
                if (_CS1688 == null)
                    _CS1688 = new CompilerReferenceError(@"Cannot convert anonymous method block without a parameter list to delegate type 'delegate' because it has one or more out parameters", 1688);
                return _CS1688;
            }
        }
        private static ICompilerReferenceError _CS1688;
        public static ICompilerReferenceError CS1689
        {
            get
            {
                if (_CS1689 == null)
                    _CS1689 = new CompilerReferenceError(@"Attribute 'Attribute Name' is only valid on methods or attribute classes", 1689);
                return _CS1689;
            }
        }
        private static ICompilerReferenceError _CS1689;
        public static ICompilerReferenceError CS1703
        {
            get
            {
                if (_CS1703 == null)
                    _CS1703 = new CompilerReferenceError(@"An assembly with the same simple name 'name' has already been imported. Try removing one of the references or sign them to enable side-by-side.", 1703);
                return _CS1703;
            }
        }
        private static ICompilerReferenceError _CS1703;
        public static ICompilerReferenceError CS1704
        {
            get
            {
                if (_CS1704 == null)
                    _CS1704 = new CompilerReferenceError(@"An assembly with the same simple name 'Assembly Name' has already been imported. Try removing one of the references or sign them to enable side-by-side.", 1704);
                return _CS1704;
            }
        }
        private static ICompilerReferenceError _CS1704;
        public static ICompilerReferenceError CS1705
        {
            get
            {
                if (_CS1705 == null)
                    _CS1705 = new CompilerReferenceError(@"Assembly 'AssemblyName1' uses 'TypeName' which has a higher version than referenced assembly 'AssemblyName2'", 1705);
                return _CS1705;
            }
        }
        private static ICompilerReferenceError _CS1705;
        public static ICompilerReferenceError CS1706
        {
            get
            {
                if (_CS1706 == null)
                    _CS1706 = new CompilerReferenceError(@"Expression cannot contain anonymous methods or lambda expressions", 1706);
                return _CS1706;
            }
        }
        private static ICompilerReferenceError _CS1706;
        public static ICompilerReferenceError CS1708
        {
            get
            {
                if (_CS1708 == null)
                    _CS1708 = new CompilerReferenceError(@"Fixed size buffers can only be accessed through locals or fields", 1708);
                return _CS1708;
            }
        }
        private static ICompilerReferenceError _CS1708;
        public static ICompilerReferenceError CS1713
        {
            get
            {
                if (_CS1713 == null)
                    _CS1713 = new CompilerReferenceError(@"Unexpected error building metadata name for type Typename1—'Reason'", 1713);
                return _CS1713;
            }
        }
        private static ICompilerReferenceError _CS1713;
        public static ICompilerReferenceError CS1714
        {
            get
            {
                if (_CS1714 == null)
                    _CS1714 = new CompilerReferenceError(@"The base class or interface of TypeName1 could not be resolved or is invalid", 1714);
                return _CS1714;
            }
        }
        private static ICompilerReferenceError _CS1714;
        public static ICompilerReferenceError CS1715
        {
            get
            {
                if (_CS1715 == null)
                    _CS1715 = new CompilerReferenceError(@"'Type1': type must be 'Type2' to match overridden member 'MemberName'", 1715);
                return _CS1715;
            }
        }
        private static ICompilerReferenceError _CS1715;
        public static ICompilerReferenceError CS1716
        {
            get
            {
                if (_CS1716 == null)
                    _CS1716 = new CompilerReferenceError(@"Do not use 'System.Runtime.CompilerServices.FixedBuffer' attribute. Use the 'fixed' field modifier instead.", 1716);
                return _CS1716;
            }
        }
        private static ICompilerReferenceError _CS1716;
        public static ICompilerReferenceError CS1719
        {
            get
            {
                if (_CS1719 == null)
                    _CS1719 = new CompilerReferenceError(@"Error reading Win32 resource file 'File Name' -- 'reason'", 1719);
                return _CS1719;
            }
        }
        private static ICompilerReferenceError _CS1719;
        public static ICompilerReferenceError CS1721
        {
            get
            {
                if (_CS1721 == null)
                    _CS1721 = new CompilerReferenceError(@"Class 'class' cannot have multiple base classes: 'class_1' and 'class_2'", 1721);
                return _CS1721;
            }
        }
        private static ICompilerReferenceError _CS1721;
        public static ICompilerReferenceError CS1722
        {
            get
            {
                if (_CS1722 == null)
                    _CS1722 = new CompilerReferenceError(@"Base class 'class' must come before any interfaces", 1722);
                return _CS1722;
            }
        }
        private static ICompilerReferenceError _CS1722;
        public static ICompilerReferenceError CS1724
        {
            get
            {
                if (_CS1724 == null)
                    _CS1724 = new CompilerReferenceError(@"Value specified for the argument to 'System.Runtime.InteropServices.DefaultCharSetAttribute' is not valid", 1724);
                return _CS1724;
            }
        }
        private static ICompilerReferenceError _CS1724;
        public static ICompilerReferenceError CS1725
        {
            get
            {
                if (_CS1725 == null)
                    _CS1725 = new CompilerReferenceError(@"Friend assembly reference 'reference' is invalid. InternalsVisibleTo declarations cannot have a version, culture, public key token, or processor architecture specified.", 1725);
                return _CS1725;
            }
        }
        private static ICompilerReferenceError _CS1725;
        public static ICompilerReferenceError CS1726
        {
            get
            {
                if (_CS1726 == null)
                    _CS1726 = new CompilerReferenceError(@"Friend assembly reference 'reference' is invalid. Strong-name signed assemblies must specify a public key in their InternalsVisibleTo declarations.", 1726);
                return _CS1726;
            }
        }
        private static ICompilerReferenceError _CS1726;
        public static ICompilerReferenceError CS1727
        {
            get
            {
                if (_CS1727 == null)
                    _CS1727 = new CompilerReferenceError(@"Cannot send error report automatically without authorization. Please visit '' to authorize sending error report.", 1727);
                return _CS1727;
            }
        }
        private static ICompilerReferenceError _CS1727;
        public static ICompilerReferenceError CS1728
        {
            get
            {
                if (_CS1728 == null)
                    _CS1728 = new CompilerReferenceError(@"Cannot bind delegate to 'member' because it is a member of 'type'", 1728);
                return _CS1728;
            }
        }
        private static ICompilerReferenceError _CS1728;
        public static ICompilerReferenceError CS1729
        {
            get
            {
                if (_CS1729 == null)
                    _CS1729 = new CompilerReferenceError(@"'type' does not contain a constructor that takes 'number' arguments.", 1729);
                return _CS1729;
            }
        }
        private static ICompilerReferenceError _CS1729;
        public static ICompilerReferenceError CS1730
        {
            get
            {
                if (_CS1730 == null)
                    _CS1730 = new CompilerReferenceError(@"Assembly and module attributes must precede all other elements defined in a file except using clauses and extern alias declarations.", 1730);
                return _CS1730;
            }
        }
        private static ICompilerReferenceError _CS1730;
        public static ICompilerReferenceError CS1731
        {
            get
            {
                if (_CS1731 == null)
                    _CS1731 = new CompilerReferenceError(@"Cannot convert 'expression' to delegate because some of the return types in the block are not implicitly convertible to the delegate return type.", 1731);
                return _CS1731;
            }
        }
        private static ICompilerReferenceError _CS1731;
        public static ICompilerReferenceError CS1732
        {
            get
            {
                if (_CS1732 == null)
                    _CS1732 = new CompilerReferenceError(@"Expected parameter.", 1732);
                return _CS1732;
            }
        }
        private static ICompilerReferenceError _CS1732;
        public static ICompilerReferenceError CS1733
        {
            get
            {
                if (_CS1733 == null)
                    _CS1733 = new CompilerReferenceError(@"Expected expression.", 1733);
                return _CS1733;
            }
        }
        private static ICompilerReferenceError _CS1733;
        public static ICompilerReferenceError CS1900
        {
            get
            {
                if (_CS1900 == null)
                    _CS1900 = new CompilerReferenceError(@"Warning level must be in the range 0-4", 1900);
                return _CS1900;
            }
        }
        private static ICompilerReferenceError _CS1900;
        public static ICompilerReferenceError CS1902
        {
            get
            {
                if (_CS1902 == null)
                    _CS1902 = new CompilerReferenceError(@"Invalid option 'option' for /debug; must be full or pdbonly", 1902);
                return _CS1902;
            }
        }
        private static ICompilerReferenceError _CS1902;
        public static ICompilerReferenceError CS1906
        {
            get
            {
                if (_CS1906 == null)
                    _CS1906 = new CompilerReferenceError(@"Invalid option 'option'; Resource visibility must be either 'public' or 'private'", 1906);
                return _CS1906;
            }
        }
        private static ICompilerReferenceError _CS1906;
        public static ICompilerReferenceError CS1908
        {
            get
            {
                if (_CS1908 == null)
                    _CS1908 = new CompilerReferenceError(@"The type of the argument to the DefaultValue attribute must match the parameter type", 1908);
                return _CS1908;
            }
        }
        private static ICompilerReferenceError _CS1908;
        public static ICompilerReferenceError CS1909
        {
            get
            {
                if (_CS1909 == null)
                    _CS1909 = new CompilerReferenceError(@"The DefaultValue attribute is not applicable on parameters of type 'type'", 1909);
                return _CS1909;
            }
        }
        private static ICompilerReferenceError _CS1909;
        public static ICompilerReferenceError CS1910
        {
            get
            {
                if (_CS1910 == null)
                    _CS1910 = new CompilerReferenceError(@"Argument of type 'type' is not applicable for the DefaultValue attribute", 1910);
                return _CS1910;
            }
        }
        private static ICompilerReferenceError _CS1910;
        public static ICompilerReferenceError CS1912
        {
            get
            {
                if (_CS1912 == null)
                    _CS1912 = new CompilerReferenceError(@"Duplicate initialization of member 'name'.", 1912);
                return _CS1912;
            }
        }
        private static ICompilerReferenceError _CS1912;
        public static ICompilerReferenceError CS1913
        {
            get
            {
                if (_CS1913 == null)
                    _CS1913 = new CompilerReferenceError(@"Member 'name' cannot be initialized. It is not a field or property.", 1913);
                return _CS1913;
            }
        }
        private static ICompilerReferenceError _CS1913;
        public static ICompilerReferenceError CS1914
        {
            get
            {
                if (_CS1914 == null)
                    _CS1914 = new CompilerReferenceError(@"Static field 'name' cannot be assigned in an object initializer", 1914);
                return _CS1914;
            }
        }
        private static ICompilerReferenceError _CS1914;
        public static ICompilerReferenceError CS1917
        {
            get
            {
                if (_CS1917 == null)
                    _CS1917 = new CompilerReferenceError(@"Members of read-only field 'name' of type 'struct name' cannot be assigned with an object initializer because it is of a value type.", 1917);
                return _CS1917;
            }
        }
        private static ICompilerReferenceError _CS1917;
        public static ICompilerReferenceError CS1918
        {
            get
            {
                if (_CS1918 == null)
                    _CS1918 = new CompilerReferenceError(@"Members of property 'name' of type 'type' cannot be assigned with an object initializer because it is of a value type.", 1918);
                return _CS1918;
            }
        }
        private static ICompilerReferenceError _CS1918;
        public static ICompilerReferenceError CS1919
        {
            get
            {
                if (_CS1919 == null)
                    _CS1919 = new CompilerReferenceError(@"Unsafe type 'type name' cannot be used in object creation.", 1919);
                return _CS1919;
            }
        }
        private static ICompilerReferenceError _CS1919;
        public static ICompilerReferenceError CS1920
        {
            get
            {
                if (_CS1920 == null)
                    _CS1920 = new CompilerReferenceError(@"Element initializer cannot be empty.", 1920);
                return _CS1920;
            }
        }
        private static ICompilerReferenceError _CS1920;
        public static ICompilerReferenceError CS1921
        {
            get
            {
                if (_CS1921 == null)
                    _CS1921 = new CompilerReferenceError(@"The best overloaded method match for 'method' has wrong signature for the initializer element. The initializable Add must be an accessible instance method.", 1921);
                return _CS1921;
            }
        }
        private static ICompilerReferenceError _CS1921;
        public static ICompilerReferenceError CS1922
        {
            get
            {
                if (_CS1922 == null)
                    _CS1922 = new CompilerReferenceError(@"Collection initializer requires its type 'type' to implement System.Collections.IEnumerable.", 1922);
                return _CS1922;
            }
        }
        private static ICompilerReferenceError _CS1922;
        public static ICompilerReferenceError CS1925
        {
            get
            {
                if (_CS1925 == null)
                    _CS1925 = new CompilerReferenceError(@"Cannot initialize object of type 'type' with a collection initializer.", 1925);
                return _CS1925;
            }
        }
        private static ICompilerReferenceError _CS1925;
        public static ICompilerReferenceError CS1926
        {
            get
            {
                if (_CS1926 == null)
                    _CS1926 = new CompilerReferenceError(@"Error reading Win32 manifest file 'filename' -- 'error'.", 1926);
                return _CS1926;
            }
        }
        private static ICompilerReferenceError _CS1926;
        public static ICompilerReferenceError CS1928
        {
            get
            {
                if (_CS1928 == null)
                    _CS1928 = new CompilerReferenceError(@"'Type' does not contain a definition for 'method' and the best extension method overload 'method' has some invalid arguments.", 1928);
                return _CS1928;
            }
        }
        private static ICompilerReferenceError _CS1928;
        public static ICompilerReferenceError CS1929
        {
            get
            {
                if (_CS1929 == null)
                    _CS1929 = new CompilerReferenceError(@"Instance argument: cannot convert from 'typeA' to 'typeB'.", 1929);
                return _CS1929;
            }
        }
        private static ICompilerReferenceError _CS1929;
        public static ICompilerReferenceError CS1930
        {
            get
            {
                if (_CS1930 == null)
                    _CS1930 = new CompilerReferenceError(@"The range variable 'name' has already been declared", 1930);
                return _CS1930;
            }
        }
        private static ICompilerReferenceError _CS1930;
        public static ICompilerReferenceError CS1931
        {
            get
            {
                if (_CS1931 == null)
                    _CS1931 = new CompilerReferenceError(@"The range variable 'variable' conflicts with a previous declaration of 'variable'.", 1931);
                return _CS1931;
            }
        }
        private static ICompilerReferenceError _CS1931;
        public static ICompilerReferenceError CS1932
        {
            get
            {
                if (_CS1932 == null)
                    _CS1932 = new CompilerReferenceError(@"Cannot assign 'expression' to a range variable.", 1932);
                return _CS1932;
            }
        }
        private static ICompilerReferenceError _CS1932;
        public static ICompilerReferenceError CS1933
        {
            get
            {
                if (_CS1933 == null)
                    _CS1933 = new CompilerReferenceError(@"Expression cannot contain query expressions", 1933);
                return _CS1933;
            }
        }
        private static ICompilerReferenceError _CS1933;
        public static ICompilerReferenceError CS1934
        {
            get
            {
                if (_CS1934 == null)
                    _CS1934 = new CompilerReferenceError(@"Could not find an implementation of the query pattern for source type 'type'. 'method' not found. Consider explicitly specifying the type of the range variable 'name'.", 1934);
                return _CS1934;
            }
        }
        private static ICompilerReferenceError _CS1934;
        public static ICompilerReferenceError CS1935
        {
            get
            {
                if (_CS1935 == null)
                    _CS1935 = new CompilerReferenceError(@"Could not find an implementation of the query pattern for source type 'type'. 'method' not found. Are you missing a reference to 'System.Core.dll' or a using directive for 'System.Linq'?", 1935);
                return _CS1935;
            }
        }
        private static ICompilerReferenceError _CS1935;
        public static ICompilerReferenceError CS1936
        {
            get
            {
                if (_CS1936 == null)
                    _CS1936 = new CompilerReferenceError(@"Could not find an implementation of the query pattern for source type 'type'. 'method' not found.", 1936);
                return _CS1936;
            }
        }
        private static ICompilerReferenceError _CS1936;
        public static ICompilerReferenceError CS1937
        {
            get
            {
                if (_CS1937 == null)
                    _CS1937 = new CompilerReferenceError(@"The name 'name' is not in scope on the left side of 'equals'. Consider swapping the expressions on either side of 'equals'.", 1937);
                return _CS1937;
            }
        }
        private static ICompilerReferenceError _CS1937;
        public static ICompilerReferenceError CS1938
        {
            get
            {
                if (_CS1938 == null)
                    _CS1938 = new CompilerReferenceError(@"The name 'name' is not in scope on the right side of 'equals'. Consider swapping the expressions on either side of 'equals'.", 1938);
                return _CS1938;
            }
        }
        private static ICompilerReferenceError _CS1938;
        public static ICompilerReferenceError CS1939
        {
            get
            {
                if (_CS1939 == null)
                    _CS1939 = new CompilerReferenceError(@"Cannot pass the range variable 'name' as an out or ref parameter.", 1939);
                return _CS1939;
            }
        }
        private static ICompilerReferenceError _CS1939;
        public static ICompilerReferenceError CS1940
        {
            get
            {
                if (_CS1940 == null)
                    _CS1940 = new CompilerReferenceError(@"Multiple implementations of the query pattern were found for source type 'type'. Ambiguous call to 'method'.", 1940);
                return _CS1940;
            }
        }
        private static ICompilerReferenceError _CS1940;
        public static ICompilerReferenceError CS1941
        {
            get
            {
                if (_CS1941 == null)
                    _CS1941 = new CompilerReferenceError(@"The type of one of the expressions in the 'clause' clause is incorrect. Type inference failed in the call to 'method'.", 1941);
                return _CS1941;
            }
        }
        private static ICompilerReferenceError _CS1941;
        public static ICompilerReferenceError CS1942
        {
            get
            {
                if (_CS1942 == null)
                    _CS1942 = new CompilerReferenceError(@"The type of the expression in the 'clause' clause is incorrect. Type inference failed in the call to 'method'.", 1942);
                return _CS1942;
            }
        }
        private static ICompilerReferenceError _CS1942;
        public static ICompilerReferenceError CS1943
        {
            get
            {
                if (_CS1943 == null)
                    _CS1943 = new CompilerReferenceError(@"An expression of type 'type' is not allowed in a subsequent from clause in a query expression with source type 'type'. Type inference failed in the call to 'method'.", 1943);
                return _CS1943;
            }
        }
        private static ICompilerReferenceError _CS1943;
        public static ICompilerReferenceError CS1944
        {
            get
            {
                if (_CS1944 == null)
                    _CS1944 = new CompilerReferenceError(@"An expression tree may not contain an unsafe pointer operation", 1944);
                return _CS1944;
            }
        }
        private static ICompilerReferenceError _CS1944;
        public static ICompilerReferenceError CS1945
        {
            get
            {
                if (_CS1945 == null)
                    _CS1945 = new CompilerReferenceError(@"An expression tree may not contain an anonymous method expression.", 1945);
                return _CS1945;
            }
        }
        private static ICompilerReferenceError _CS1945;
        public static ICompilerReferenceError CS1946
        {
            get
            {
                if (_CS1946 == null)
                    _CS1946 = new CompilerReferenceError(@"An anonymous method expression cannot be converted to an expression tree.", 1946);
                return _CS1946;
            }
        }
        private static ICompilerReferenceError _CS1946;
        public static ICompilerReferenceError CS1947
        {
            get
            {
                if (_CS1947 == null)
                    _CS1947 = new CompilerReferenceError(@"Range variable 'variable name' cannot be assigned to -- it is read only.", 1947);
                return _CS1947;
            }
        }
        private static ICompilerReferenceError _CS1947;
        public static ICompilerReferenceError CS1948
        {
            get
            {
                if (_CS1948 == null)
                    _CS1948 = new CompilerReferenceError(@"The range variable 'name' cannot have the same name as a method type parameter", 1948);
                return _CS1948;
            }
        }
        private static ICompilerReferenceError _CS1948;
        public static ICompilerReferenceError CS1949
        {
            get
            {
                if (_CS1949 == null)
                    _CS1949 = new CompilerReferenceError(@"The contextual keyword 'var' cannot be used in a range variable declaration.", 1949);
                return _CS1949;
            }
        }
        private static ICompilerReferenceError _CS1949;
        public static ICompilerReferenceError CS1950
        {
            get
            {
                if (_CS1950 == null)
                    _CS1950 = new CompilerReferenceError(@"The best overloaded Add method 'name' for the collection initializer has some invalid arguments.", 1950);
                return _CS1950;
            }
        }
        private static ICompilerReferenceError _CS1950;
        public static ICompilerReferenceError CS1951
        {
            get
            {
                if (_CS1951 == null)
                    _CS1951 = new CompilerReferenceError(@"An expression tree lambda may not contain an out or ref parameter.", 1951);
                return _CS1951;
            }
        }
        private static ICompilerReferenceError _CS1951;
        public static ICompilerReferenceError CS1952
        {
            get
            {
                if (_CS1952 == null)
                    _CS1952 = new CompilerReferenceError(@"An expression tree lambda may not contain a method with variable arguments", 1952);
                return _CS1952;
            }
        }
        private static ICompilerReferenceError _CS1952;
        public static ICompilerReferenceError CS1953
        {
            get
            {
                if (_CS1953 == null)
                    _CS1953 = new CompilerReferenceError(@"An expression tree lambda may not contain a method group.", 1953);
                return _CS1953;
            }
        }
        private static ICompilerReferenceError _CS1953;
        public static ICompilerReferenceError CS1954
        {
            get
            {
                if (_CS1954 == null)
                    _CS1954 = new CompilerReferenceError(@"The best overloaded method match 'method' for the collection initializer element cannot be used. Collection initializer 'Add' methods cannot have ref or out parameters.", 1954);
                return _CS1954;
            }
        }
        private static ICompilerReferenceError _CS1954;
        public static ICompilerReferenceError CS1955
        {
            get
            {
                if (_CS1955 == null)
                    _CS1955 = new CompilerReferenceError(@"Non-invocable member 'name' cannot be used like a method.", 1955);
                return _CS1955;
            }
        }
        private static ICompilerReferenceError _CS1955;
        public static ICompilerReferenceError CS1958
        {
            get
            {
                if (_CS1958 == null)
                    _CS1958 = new CompilerReferenceError(@"Object and collection initializer expressions may not be applied to a delegate creation expression,", 1958);
                return _CS1958;
            }
        }
        private static ICompilerReferenceError _CS1958;
        public static ICompilerReferenceError CS1959
        {
            get
            {
                if (_CS1959 == null)
                    _CS1959 = new CompilerReferenceError(@"'name' is of type 'type'. The type specified in a constant declaration must be sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, bool, string, an enum-type, or a reference-type.", 1959);
                return _CS1959;
            }
        }
        private static ICompilerReferenceError _CS1959;
        public static ICompilerReferenceError CS2001
        {
            get
            {
                if (_CS2001 == null)
                    _CS2001 = new CompilerReferenceError(@"Source file 'file' could not be found", 2001);
                return _CS2001;
            }
        }
        private static ICompilerReferenceError _CS2001;
        public static ICompilerReferenceError CS2003
        {
            get
            {
                if (_CS2003 == null)
                    _CS2003 = new CompilerReferenceError(@"Response file 'file' included multiple times", 2003);
                return _CS2003;
            }
        }
        private static ICompilerReferenceError _CS2003;
        public static ICompilerReferenceError CS2005
        {
            get
            {
                if (_CS2005 == null)
                    _CS2005 = new CompilerReferenceError(@"Missing file specification for 'option' option", 2005);
                return _CS2005;
            }
        }
        private static ICompilerReferenceError _CS2005;
        public static ICompilerReferenceError CS2006
        {
            get
            {
                if (_CS2006 == null)
                    _CS2006 = new CompilerReferenceError(@"Command-line syntax error: Missing 'text' for 'option' option", 2006);
                return _CS2006;
            }
        }
        private static ICompilerReferenceError _CS2006;
        public static ICompilerReferenceError CS2007
        {
            get
            {
                if (_CS2007 == null)
                    _CS2007 = new CompilerReferenceError(@"Unrecognized command-line option: 'option'", 2007);
                return _CS2007;
            }
        }
        private static ICompilerReferenceError _CS2007;
        public static ICompilerReferenceError CS2008
        {
            get
            {
                if (_CS2008 == null)
                    _CS2008 = new CompilerReferenceError(@"No inputs specified", 2008);
                return _CS2008;
            }
        }
        private static ICompilerReferenceError _CS2008;
        public static ICompilerReferenceError CS2011
        {
            get
            {
                if (_CS2011 == null)
                    _CS2011 = new CompilerReferenceError(@"Unable to open response file 'file'", 2011);
                return _CS2011;
            }
        }
        private static ICompilerReferenceError _CS2011;
        public static ICompilerReferenceError CS2012
        {
            get
            {
                if (_CS2012 == null)
                    _CS2012 = new CompilerReferenceError(@"Cannot open 'file' for writing", 2012);
                return _CS2012;
            }
        }
        private static ICompilerReferenceError _CS2012;
        public static ICompilerReferenceError CS2013
        {
            get
            {
                if (_CS2013 == null)
                    _CS2013 = new CompilerReferenceError(@"Invalid image base number 'value'", 2013);
                return _CS2013;
            }
        }
        private static ICompilerReferenceError _CS2013;
        public static ICompilerReferenceError CS2015
        {
            get
            {
                if (_CS2015 == null)
                    _CS2015 = new CompilerReferenceError(@"'file' is a binary file instead of a text file", 2015);
                return _CS2015;
            }
        }
        private static ICompilerReferenceError _CS2015;
        public static ICompilerReferenceError CS2016
        {
            get
            {
                if (_CS2016 == null)
                    _CS2016 = new CompilerReferenceError(@"Code page 'codepage' is invalid or not installed", 2016);
                return _CS2016;
            }
        }
        private static ICompilerReferenceError _CS2016;
        public static ICompilerReferenceError CS2017
        {
            get
            {
                if (_CS2017 == null)
                    _CS2017 = new CompilerReferenceError(@"Cannot specify /main if building a module or library", 2017);
                return _CS2017;
            }
        }
        private static ICompilerReferenceError _CS2017;
        public static ICompilerReferenceError CS2018
        {
            get
            {
                if (_CS2018 == null)
                    _CS2018 = new CompilerReferenceError(@"Unable to find messages file 'cscmsgs.dll'", 2018);
                return _CS2018;
            }
        }
        private static ICompilerReferenceError _CS2018;
        public static ICompilerReferenceError CS2019
        {
            get
            {
                if (_CS2019 == null)
                    _CS2019 = new CompilerReferenceError(@"Invalid target type for /target: must specify 'exe', 'winexe', 'library', or 'module'", 2019);
                return _CS2019;
            }
        }
        private static ICompilerReferenceError _CS2019;
        public static ICompilerReferenceError CS2020
        {
            get
            {
                if (_CS2020 == null)
                    _CS2020 = new CompilerReferenceError(@"Only the first set of input files can build a target other than 'module'", 2020);
                return _CS2020;
            }
        }
        private static ICompilerReferenceError _CS2020;
        public static ICompilerReferenceError CS2021
        {
            get
            {
                if (_CS2021 == null)
                    _CS2021 = new CompilerReferenceError(@"File name 'file' is too long or invalid", 2021);
                return _CS2021;
            }
        }
        private static ICompilerReferenceError _CS2021;
        public static ICompilerReferenceError CS2022
        {
            get
            {
                if (_CS2022 == null)
                    _CS2022 = new CompilerReferenceError(@"Options '/out' and '/target' must appear before source file names", 2022);
                return _CS2022;
            }
        }
        private static ICompilerReferenceError _CS2022;
        public static ICompilerReferenceError CS2024
        {
            get
            {
                if (_CS2024 == null)
                    _CS2024 = new CompilerReferenceError(@"Invalid file section alignment number '#'", 2024);
                return _CS2024;
            }
        }
        private static ICompilerReferenceError _CS2024;
        public static ICompilerReferenceError CS2032
        {
            get
            {
                if (_CS2032 == null)
                    _CS2032 = new CompilerReferenceError(@"Character 'character' is not allowed on the command-line or in response files", 2032);
                return _CS2032;
            }
        }
        private static ICompilerReferenceError _CS2032;
        public static ICompilerReferenceError CS2033
        {
            get
            {
                if (_CS2033 == null)
                    _CS2033 = new CompilerReferenceError(@"Cannot create short filename 'filename' when a long filename with the same short filename already exists ", 2033);
                return _CS2033;
            }
        }
        private static ICompilerReferenceError _CS2033;
        public static ICompilerReferenceError CS2034
        {
            get
            {
                if (_CS2034 == null)
                    _CS2034 = new CompilerReferenceError(@"A /reference option that declares an extern alias can only have one filename. To specify multiple aliases or filenames, use multiple /reference options.", 2034);
                return _CS2034;
            }
        }
        private static ICompilerReferenceError _CS2034;
        public static ICompilerReferenceError CS2035
        {
            get
            {
                if (_CS2035 == null)
                    _CS2035 = new CompilerReferenceError(@"Command-line syntax error: Missing ':<number>' for 'compiler_option' option", 2035);
                return _CS2035;
            }
        }
        private static ICompilerReferenceError _CS2035;
        public static ICompilerReferenceError CS2036
        {
            get
            {
                if (_CS2036 == null)
                    _CS2036 = new CompilerReferenceError(@"The /pdb option requires that the /debug option also be used.", 2036);
                return _CS2036;
            }
        }
        private static ICompilerReferenceError _CS2036;
        public static ICompilerReferenceError CS5001
        {
            get
            {
                if (_CS5001 == null)
                    _CS5001 = new CompilerReferenceError(@"Program 'program' does not contain a static 'Main' method suitable for an entry point", 5001);
                return _CS5001;
            }
        }
        private static ICompilerReferenceError _CS5001;
    }
}
