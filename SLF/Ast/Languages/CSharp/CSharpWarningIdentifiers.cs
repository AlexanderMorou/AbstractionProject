using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    public enum CSharpWarningIdentifiers
    {
        /// <summary>
        /// {0} has the wrong signature to be an entry point.
        /// </summary>
        CS0028 = 0028,
        /// <summary>
        /// The event {0} is never used.
        /// </summary>
        CS0067 = 0067,
        /// <summary>
        /// The 'l' suffix is easily confused with the digit '1' -- use 'L' for clarity.
        /// </summary>
        CS0078 = 0078,
        /// <summary>
        /// The using directive for {0} appeared previously in this namespace.
        /// </summary>
        CS0105 = 0105,
        /// <summary>
        /// {0} hides inherited member {1}. Use the new keyword if hiding was intended.
        /// </summary>
        CS0108 = 0108,
        /// <summary>
        /// The member {0} does not hide an inherited member. The new keyword is not required.
        /// </summary>
        CS0109 = 0109,
        /// <summary>
        /// {0} hides inherited member {1}. To make the current method override that implementation, add the override keyword. Otherwise add the new keyword.
        /// </summary>
        CS0114 = 0114,
        /// <summary>
        /// Unreachable code detected
        /// </summary>
        CS0162 = 0162,
        /// <summary>
        /// This label has not been referenced
        /// </summary>
        CS0164 = 0164,
        /// <summary>
        /// The variable {0} is assigned but its value is never used
        /// </summary>
        CS0168 = 0168,
        /// <summary>
        /// The private field {0} is never used
        /// </summary>
        CS0169 = 0169,
        /// <summary>
        /// The given expression is always of the provided ({0}) type.
        /// </summary>
        CS0183 = 0183,
        /// <summary>
        /// The given expression is never of the provided ({0}) type.
        /// </summary>
        CS0184 = 0184,
        /// <summary>
        /// Passing {0} as ref or out or taking its address may cause a runtime exception because it is a field of a marshal-by-reference class
        /// </summary>
        CS0197 = 0197,
        /// <summary>
        /// The variable {0} is assigned but its value is never used
        /// </summary>
        CS0219 = 0219,
        /// <summary>
        /// Indexing an array with a negative index (array indices always start at zero)
        /// </summary>
        CS0251 = 0251,
        /// <summary>
        /// Possible unintended reference comparison; to get a value comparison, cast the left hand side to type {0}
        /// </summary>
        CS0252 = 0252,
        /// <summary>
        /// Possible unintended reference comparison; to get a value comparison, cast the right hand side to type {0}
        /// </summary>
        CS0253 = 0253,
        /// <summary>
        /// {0} does not implement the {1} pattern. {2} is ambiguous with {2}.
        /// </summary>
        CS0278 = 0278,
        /// <summary>
        /// {0} does not implement the {1} pattern. {2} is either static or not public.
        /// </summary>
        CS0279 = 0279,
        /// <summary>
        /// {0} does not implement the {1} pattern. {2} has the wrong signature.
        /// </summary>
        CS0280 = 0280,
        /// <summary>
        /// There is no defined ordering between fields in multiple declarations of partial class or struct {0}. To specify an ordering, all instance fields must be in the same declaration.
        /// </summary>
        CS0282 = 0282,
        /// <summary>
        /// {0} : an entry point cannot be generic or in a generic type
        /// </summary>
        CS0402 = 0402,
        /// <summary>
        /// The private field {0} is assigned but its value is never used
        /// </summary>
        CS0414 = 0414,
        /// <summary>
        /// Ambiguous reference in cref attribute: {0}. Assuming {1}, but could have also matched other overloads including {2}.
        /// </summary>
        CS0419 = 0419,
        /// <summary>
        /// {0}: a reference to a volatile field will not be treated as volatile
        /// </summary>
        CS0420 = 0420,
        /// <summary>
        /// The /incremental option is no longer supported
        /// </summary>
        CS0422 = 0422,
        /// <summary>
        /// Unreachable expression code detected 
        /// </summary>
        CS0429 = 0429,
        /// <summary>
        /// The namespace {0} in {1} conflicts with the imported type {2} in {3}. Using the namespace defined in {1}..
        /// </summary>
        CS0435 = 0435,
        /// <summary>
        /// The type {0} in {1} conflicts with the imported type {2} in {3}. Using the type defined in {1}.
        /// </summary>
        CS0436 = 0436,
        /// <summary>
        /// The type {0} in {1} conflicts with the imported namespace {2} in {3}. Using the type defined in {1}.
        /// </summary>
        CS0437 = 0437,
        /// <summary>
        /// Defining an alias named 'global' is ill-advised since 'global::' always references the global namespace and not an alias
        /// </summary>
        CS0440 = 0440,
        /// <summary>
        /// Predefined type {0} was not found in {1} but was found in {2}
        /// </summary>
        CS0444 = 0444,
        /// <summary>
        /// The result of the expression is always 'null' of type {0}
        /// </summary>
        CS0458 = 0458,
        /// <summary>
        /// Comparing with null of type {0} always produces 'false'
        /// </summary>
        CS0464 = 0464,
        /// <summary>
        /// Introducing a 'Finalize' method can interfere with destructor invocation. Did you intend to declare a destructor?
        /// </summary>
        CS0465 = 0465,
        /// <summary>
        /// Ambiguity between method {0} and non-method {1}. Using method group.
        /// </summary>
        CS0467 = 0467,
        /// <summary>
        /// The {0} value is not implicitly convertible to type {1}
        /// </summary>
        CS0469 = 0469,
        /// <summary>
        /// The result of the expression is always {0} since a value of type {1} is never equal to 'null' of type {1}
        /// </summary>
        CS0472 = 0472,
        /// <summary>
        /// The feature {0} is deprecated. Please use {1} instead
        /// </summary>
        CS0602 = 0602,
        /// <summary>
        /// {0} is obsolete
        /// </summary>
        CS0612 = 0612,
        /// <summary>
        /// {0} is obsolete: {1}
        /// </summary>
        CS0618 = 0618,
        /// <summary>
        /// Method, operator, or accessor {0} is marked external and has no attributes on it. Consider adding a DllImport attribute to specify the external implementation
        /// </summary>
        CS0626 = 0626,
        /// <summary>
        /// {0} : new protected member declared in sealed class
        /// </summary>
        CS0628 = 0628,
        /// <summary>
        /// Possible mistaken empty statement
        /// </summary>
        CS0642 = 0642,
        /// <summary>
        /// Field {0} is never assigned to, and will always have its default value {1}
        /// </summary>
        CS0649 = 0649,
        /// <summary>
        /// Comparison to integral constant is useless; the constant is outside the range of type {0}
        /// </summary>
        CS0652 = 0652,
        /// <summary>
        /// {0} is not a valid attribute location for this declaration. Valid attribute locations for this declaration are {1}. All attributes in this block will be ignored.
        /// </summary>
        CS0657 = 0657,
        /// <summary>
        /// {0} is not a recognized attribute location. All attributes in this block will be ignored.
        /// </summary>
        CS0658 = 0658,
        /// <summary>
        /// {0} overrides Object.Equals(object o) but does not override Object.GetHashCode()
        /// </summary>
        CS0659 = 0659,
        /// <summary>
        /// {0} defines operator == or operator != but does not override Object.Equals(object o)
        /// </summary>
        CS0660 = 0660,
        /// <summary>
        /// {0} defines operator == or operator != but does not override Object.GetHashCode()
        /// </summary>
        CS0661 = 0661,
        /// <summary>
        /// Assignment in conditional expression is always constant; did you mean to use '==' instead of '='?
        /// </summary>
        CS0665 = 0665,
        /// <summary>
        /// Member {0} overrides obsolete member '{1}. Add the Obsolete attribute to {0}
        /// </summary>
        CS0672 = 0672,
        /// <summary>
        /// Bitwise-or operator used on a sign-extended operand; consider casting to a smaller unsigned type first
        /// </summary>
        CS0675 = 0675,
        /// <summary>
        /// Type parameter {0} has the same name as the type parameter from outer type {1}
        /// </summary>
        CS0693 = 0693,
        /// <summary>
        /// Possibly incorrect assignment to local {0} which is the argument to a using or lock statement. The Dispose call or unlocking will happen on the original value of the local.
        /// </summary>
        CS0728 = 0728,
        /// <summary>
        /// Obsolete member {0} overrides non-obsolete member {1}.
        /// </summary>
        CS0809 = 0809,
        /// <summary>
        /// Constructor {0} is marked external.
        /// </summary>
        CS0824 = 0824,
        /// <summary>
        /// #warning: {0}
        /// </summary>
        CS1030 = 1030,
        /// <summary>
        /// A previous catch clause already catches all exceptions. All exceptions thrown will be wrapped in a System.Runtime.CompilerServices.RuntimeWrappedException
        /// </summary>
        CS1058 = 1058,
        /// <summary>
        /// Use of possibly unassigned field 'name'. Struct instance variables are initially unassigned if struct is unassigned.
        /// </summary>
        CS1060 = 1060,
        /// <summary>
        /// Empty switch block
        /// </summary>
        CS1522 = 1522,
        /// <summary>
        /// XML comment on {0} has badly formed XML — {1}
        /// </summary>
        CS1570 = 1570,
        /// <summary>
        /// XML comment on {0} has a duplicate param tag for {1}
        /// </summary>
        CS1571 = 1571,
        /// <summary>
        /// XML comment on {0} has a param tag for {1}, but there is no parameter by that name
        /// </summary>
        CS1572 = 1572,
        /// <summary>
        /// Parameter {0} has no matching param tag in the XML comment for {0} (but other parameters do)
        /// </summary>
        CS1573 = 1573,
        /// <summary>
        /// XML comment on {0} has cref attribute {1} that could not be resolved.
        /// </summary>
        CS1574 = 1574,
        /// <summary>
        /// Invalid type for parameter {0} in XML comment cref attribute
        /// </summary>
        CS1580 = 1580,
        /// <summary>
        /// Invalid return type in XML comment cref attribute
        /// </summary>
        CS1581 = 1581,
        /// <summary>
        /// XML comment on {0} has syntactically incorrect cref attribute {1}
        /// </summary>
        CS1584 = 1584,
        /// <summary>
        /// XML comment is not placed on a valid language element
        /// </summary>
        CS1587 = 1587,
        /// <summary>
        /// Unable to include XML fragment {0} of file {1} -- {1}
        /// </summary>
        CS1589 = 1589,
        /// <summary>
        /// Invalid XML include element -- Missing file attribute
        /// </summary>
        CS1590 = 1590,
        /// <summary>
        /// Missing XML comment for publicly visible type or member {0}
        /// </summary>
        CS1591 = 1591,
        /// <summary>
        /// Badly formed XML in included comments file -- {0}
        /// </summary>
        CS1592 = 1592,
        /// <summary>
        /// XML parser could not be loaded for the following reason: {0}. The XML documentation file {1} will not be generated.
        /// </summary>
        CS1598 = 1598,
        /// <summary>
        /// Assembly generation -- {0}
        /// </summary>
        CS1607 = 1607,
        /// <summary>
        /// Unable to delete temporary file {0} used for default Win32 resource -- {1}
        /// </summary>
        CS1610 = 1610,
        /// <summary>
        /// Option {0} overrides attribute {1} given in a source file or added module
        /// </summary>
        CS1616 = 1616,
        /// <summary>
        /// Unrecognized #pragma directive
        /// </summary>
        CS1633 = 1633,
        /// <summary>
        /// Expected disable or restore
        /// </summary>
        CS1634 = 1634,
        /// <summary>
        /// Cannot restore warning {0} because it was disabled globally
        /// </summary>
        CS1635 = 1635,
        /// <summary>
        /// Feature {0} is not part of the standardized ISO C# language specification, and may not be accepted by other compilers
        /// </summary>
        CS1645 = 1645,
        /// <summary>
        /// {0}. See also error: {1}
        /// </summary>
        CS1658 = 1658,
        /// <summary>
        /// Invalid search path 'path' specified in {0} -- {1}
        /// </summary>
        CS1668 = 1668,
        /// <summary>
        /// Reference to type {0} claims it is nested within {1}, but it could not be found
        /// </summary>
        CS1682 = 1682,
        /// <summary>
        /// Reference to type {0} claims it is defined in this assembly, but it is not defined in source or any added modules
        /// </summary>
        CS1683 = 1683,
        /// <summary>
        /// Reference to type {0} claims it is defined in {1}, but it could not be found
        /// </summary>
        CS1684 = 1684,
        /// <summary>
        /// The predefined type {0} is defined in multiple assemblies in the global alias; using definition from {1}
        /// </summary>
        CS1685 = 1685,
        /// <summary>
        /// Source file has exceeded the limit of 16,707,565 lines representable in the PDB, debug information will be incorrect
        /// </summary>
        CS1687 = 1687,
        /// <summary>
        /// Accessing a member on {0} may cause a runtime exception because it is a field of a marshal-by-reference class
        /// </summary>
        CS1690 = 1690,
        /// <summary>
        /// {0} is not a valid warning number
        /// </summary>
        CS1691 = 1691,
        /// <summary>
        /// Invalid number
        /// </summary>
        CS1692 = 1692,
        /// <summary>
        /// Invalid filename specified for preprocessor directive. Filename is too long or not a valid filename.
        /// </summary>
        CS1694 = 1694,
        /// <summary>
        /// Invalid #pragma checksum syntax; should be #pragma checksum ""filename"" ""{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}"" ""XXXX...""
        /// </summary>
        CS1695 = 1695,
        /// <summary>
        /// Single-line comment or end-of-line expected
        /// </summary>
        CS1696 = 1696,
        /// <summary>
        /// Different checksum values given for {0}
        /// </summary>
        CS1697 = 1697,
        /// <summary>
        /// Circular assembly reference {0} does not match the output assembly name {1}. Try adding a reference to {0} or changing the output assembly name to match.
        /// </summary>
        CS1698 = 1698,
        /// <summary>
        /// Use command line option {0} or appropriate project settings instead of {1}
        /// </summary>
        CS1699 = 1699,
        /// <summary>
        /// Assembly reference Assembly Name is invalid and cannot be resolved
        /// </summary>
        CS1700 = 1700,
        /// <summary>
        /// Assuming assembly reference {0} matches {1}, you may need to supply runtime policy 
        /// </summary>
        CS1701 = 1701,
        /// <summary>
        /// Assuming assembly reference {0} matches {1}, you may need to supply runtime policy
        /// </summary>
        CS1702 = 1702,
        /// <summary>
        /// Delegate {0} bound to {1} instead of {2} because of new language rules
        /// </summary>
        CS1707 = 1707,
        /// <summary>
        /// Filename specified for preprocessor directive is empty
        /// </summary>
        CS1709 = 1709,
        /// <summary>
        /// XML comment on {0} has a duplicate typeparam tag for {1}
        /// </summary>
        CS1710 = 1710,
        /// <summary>
        /// XML comment on {0} has a typeparam tag for {1}, but there is no type parameter by that name
        /// </summary>
        CS1711 = 1711,
        /// <summary>
        /// Type parameter {0} has no matching typeparam tag in the XML comment on {1} (but other type parameters do)
        /// </summary>
        CS1712 = 1712,
        /// <summary>
        /// Assignment made to same variable; did you mean to assign something else?
        /// </summary>
        CS1717 = 1717,
        /// <summary>
        /// Comparison made to same variable; did you mean to compare something else?
        /// </summary>
        CS1718 = 1718,
        /// <summary>
        /// Expression will always cause a System.NullReferenceException because the default value of {0} is null
        /// </summary>
        CS1720 = 1720,
        /// <summary>
        /// XML comment on {0} has cref attribute {1} that refers to a type parameter
        /// </summary>
        CS1723 = 1723,
        /// <summary>
        /// Access to member {0} through a 'base' keyword from an anonymous method, lambda expression, query expression, or iterator results in unverifiable code. Consider moving the access into a helper method on the containing type.
        /// </summary>
        CS1911 = 1911,
        /// <summary>
        /// Ignoring /win32manifest for module because it only applies to assemblies.
        /// </summary>
        CS1927 = 1927,
        /// <summary>
        /// Member {0} implements interface member {0} in type {1}. There are multiple matches for the interface member at run-time. It is implementation dependent which method will be called.
        /// </summary>
        CS1956 = 1956,
        /// <summary>
        /// Member {0} overrides {1}. There are multiple override candidates at run-time. It is implementation dependent which method will be called.
        /// </summary>
        CS1957 = 1957,
        /// <summary>
        /// Source file {0} specified multiple times
        /// </summary>
        CS2002 = 2002,
        /// <summary>
        /// Compiler option {0} is obsolete, please use {1} instead
        /// </summary>
        CS2014 = 2014,
        /// <summary>
        /// Ignoring /noconfig option because it was specified in a response file
        /// </summary>
        CS2023 = 2023,
        /// <summary>
        /// Invalid value for '/define'; {0} is not a valid identifier
        /// </summary>
        CS2029 = 2029,
        /// <summary>
        /// Methods with variable arguments are not CLS-compliant
        /// </summary>
        CS3000 = 3000,
        /// <summary>
        /// Argument type {0} is not CLS-compliant
        /// </summary>
        CS3001 = 3001,
        /// <summary>
        /// Return type of {0} is not CLS-compliant
        /// </summary>
        CS3002 = 3002,
        /// <summary>
        /// Type of {0} is not CLS-compliant
        /// </summary>
        CS3003 = 3003,
        /// <summary>
        /// Mixed and decomposed Unicode characters are not CLS-compliant
        /// </summary>
        CS3004 = 3004,
        /// <summary>
        /// Identifier {0} differing only in case is not CLS-compliant
        /// </summary>
        CS3005 = 3005,
        /// <summary>
        /// Overloaded method {0} differing only in ref or out, or in array rank, is not CLS-compliant
        /// </summary>
        CS3006 = 3006,
        /// <summary>
        /// Overloaded method {0} differing only by unnamed array types is not CLS-compliant
        /// </summary>
        CS3007 = 3007,
        /// <summary>
        /// Identifier {0} differing only in case is not CLS-compliant
        /// </summary>
        CS3008 = 3008,
        /// <summary>
        /// {0}: base type {0} is not CLS-compliant
        /// </summary>
        CS3009 = 3009,
        /// <summary>
        /// {0}: CLS-compliant interfaces must have only CLS-compliant members
        /// </summary>
        CS3010 = 3010,
        /// <summary>
        /// {0}: only CLS-compliant members can be abstract
        /// </summary>
        CS3011 = 3011,
        /// <summary>
        /// You cannot specify the CLSCompliant attribute on a module that differs from the CLSCompliant attribute on the assembly
        /// </summary>
        CS3012 = 3012,
        /// <summary>
        /// Added modules must be marked with the CLSCompliant attribute to match the assembly
        /// </summary>
        CS3013 = 3013,
        /// <summary>
        /// {0} does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute
        /// </summary>
        CS3014 = 3014,
        /// <summary>
        /// {0} has no accessible constructors which use only CLS-compliant types
        /// </summary>
        CS3015 = 3015,
        /// <summary>
        /// Arrays as attribute arguments is not CLS-compliant
        /// </summary>
        CS3016 = 3016,
        /// <summary>
        /// You cannot specify the CLSCompliant attribute on a module that differs from the CLSCompliant attribute on the assembly
        /// </summary>
        CS3017 = 3017,
        /// <summary>
        /// {0} cannot be marked as CLS-Compliant because it is a member of non CLS-compliant type {1}
        /// </summary>
        CS3018 = 3018,
        /// <summary>
        /// CLS compliance checking will not be performed on {0} because it is not visible from outside this assembly.
        /// </summary>
        CS3019 = 3019,
        /// <summary>
        /// {0} does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute
        /// </summary>
        CS3021 = 3021,
        /// <summary>
        /// CLSCompliant attribute has no meaning when applied to parameters. Try putting it on the method instead.
        /// </summary>
        CS3022 = 3022,
        /// <summary>
        /// CLSCompliant attribute has no meaning when applied to return types. Try putting it on the method instead.
        /// </summary>
        CS3023 = 3023,
        /// <summary>
        /// CLS-compliant field {0} cannot be volatile
        /// </summary>
        CS3026 = 3026,
        /// <summary>
        /// {0} is not CLS-compliant because base interface {1} is not CLS-compliant
        /// </summary>
        CS3027 = 3027,
        /// <summary>
        /// Unknown compiler option {0}
        /// </summary>
        CS5000 = 5000,
    }
}
