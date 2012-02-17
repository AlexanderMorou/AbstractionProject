using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Languages.CSharp
{
    public enum CSharpErrorIdentifiers
    {
        /// <summary>
        /// Internal compiler error
        /// </summary>
        CS0001 = 0001,
        /// <summary>
        /// Out of memory
        /// </summary>
        CS0003 = 0003,
        /// <summary>
        /// Warning treated as error
        /// </summary>
        CS0004 = 0004,
        /// <summary>
        /// Compiler option {0} must be followed by an argument
        /// </summary>
        CS0005 = 0005,
        /// <summary>
        /// Metadata file {0} could not be found
        /// </summary>
        CS0006 = 0006,
        /// <summary>
        /// Unexpected common language runtime initialization error — {0}
        /// </summary>
        CS0007 = 0007,
        /// <summary>
        /// Unexpected error reading metadata from file 'file' — {0}
        /// </summary>
        CS0008 = 0008,
        /// <summary>
        /// Metadata file {0} could not be opened — {1}
        /// </summary>
        CS0009 = 0009,
        /// <summary>
        /// Unexpected fatal error -- {0}.
        /// </summary>
        CS0010 = 0010,
        /// <summary>
        /// The base class or interface {0} in assembly {1} referenced by type {2} could not be resolved
        /// </summary>
        CS0011 = 0011,
        /// <summary>
        /// The type {0} is defined in an assembly that is not referenced. You must add a reference to assembly {1}.
        /// </summary>
        CS0012 = 0012,
        /// <summary>
        /// Unexpected error writing metadata to file {0} -- {1}
        /// </summary>
        CS0013 = 0013,
        /// <summary>
        /// Required file {0} could not be found
        /// </summary>
        CS0014 = 0014,
        /// <summary>
        /// The name of type {0} is too long
        /// </summary>
        CS0015 = 0015,
        /// <summary>
        /// Could not write to output file {0} — {1}
        /// </summary>
        CS0016 = 0016,
        /// <summary>
        /// Program {0} has more than one entry point defined. Compile with /main to specify the type that contains the entry point.
        /// </summary>
        CS0017 = 0017,
        /// <summary>
        /// Operator {0} cannot be applied to operands of type {1} and {2}
        /// </summary>
        CS0019 = 0019,
        /// <summary>
        /// Division by constant zero
        /// </summary>
        CS0020 = 0020,
        /// <summary>
        /// Cannot apply indexing with [] to an expression of type {0}
        /// </summary>
        CS0021 = 0021,
        /// <summary>
        /// Wrong number of indices inside [], expected {0}
        /// </summary>
        CS0022 = 0022,
        /// <summary>
        /// Operator {0} cannot be applied to operand of type {1}
        /// </summary>
        CS0023 = 0023,
        /// <summary>
        /// Standard library file {0} could not be found
        /// </summary>
        CS0025 = 0025,
        /// <summary>
        /// Keyword 'this' is not valid in a static property, static method, or static field initializer
        /// </summary>
        CS0026 = 0026,
        /// <summary>
        /// Keyword 'this' is not available in the current context
        /// </summary>
        CS0027 = 0027,
        /// <summary>
        /// Cannot implicitly convert type {0} to {1}
        /// </summary>
        CS0029 = 0029,
        /// <summary>
        /// Cannot convert type {0} to {1}
        /// </summary>
        CS0030 = 0030,
        /// <summary>
        /// Constant value {0} cannot be converted to a {1}. (use 'unchecked' syntax to override)
        /// </summary>
        CS0031 = 0031,
        /// <summary>
        /// Operator {0} is ambiguous on operands of type {1} and {2}
        /// </summary>
        CS0034 = 0034,
        /// <summary>
        /// Operator {0} is ambiguous on an operand of type {1}
        /// </summary>
        CS0035 = 0035,
        /// <summary>
        /// An out parameter cannot have the '[In]' attribute
        /// </summary>
        CS0036 = 0036,
        /// <summary>
        /// Cannot convert null to {0} because it is a non-nullable value type
        /// </summary>
        CS0037 = 0037,
        /// <summary>
        /// Cannot access a nonstatic member of outer type {0} via nested type {1}
        /// </summary>
        CS0038 = 0038,
        /// <summary>
        /// Cannot convert type {0} to {1} via a reference conversion, boxing conversion, unboxing conversion, wrapping conversion, or null type conversion
        /// </summary>
        CS0039 = 0039,
        /// <summary>
        /// Unexpected error creating debug information file — {0}
        /// </summary>
        CS0040 = 0040,
        /// <summary>
        /// The fully qualified name for {0} is too long for debug information. Compile without '/debug' option.
        /// </summary>
        CS0041 = 0041,
        /// <summary>
        /// Unexpected error creating debug information file {0} — {1}
        /// </summary>
        CS0042 = 0042,
        /// <summary>
        /// PDB file {0} has an incorrect or out-of-date format. Delete it and rebuild.
        /// </summary>
        CS0043 = 0043,
        /// <summary>
        /// Inconsistent accessibility: return type {0} is less accessible than method {1}
        /// </summary>
        CS0050 = 0050,
        /// <summary>
        /// Inconsistent accessibility: parameter type {0} is less accessible than method {1}
        /// </summary>
        CS0051 = 0051,
        /// <summary>
        /// Inconsistent accessibility: field type {0} is less accessible than field {1}
        /// </summary>
        CS0052 = 0052,
        /// <summary>
        /// Inconsistent accessibility: property type {0} is less accessible than property {1}
        /// </summary>
        CS0053 = 0053,
        /// <summary>
        /// Inconsistent accessibility: indexer return type {0} is less accessible than indexer {1}
        /// </summary>
        CS0054 = 0054,
        /// <summary>
        /// Inconsistent accessibility: parameter type {0} is less accessible than indexer {1}
        /// </summary>
        CS0055 = 0055,
        /// <summary>
        /// Inconsistent accessibility: return type {0} is less accessible than operator {1}
        /// </summary>
        CS0056 = 0056,
        /// <summary>
        /// Inconsistent accessibility: parameter type {0} is less accessible than operator {1}
        /// </summary>
        CS0057 = 0057,
        /// <summary>
        /// Inconsistent accessibility: return type {0} is less accessible than delegate {1}
        /// </summary>
        CS0058 = 0058,
        /// <summary>
        /// Inconsistent accessibility: parameter type {0} is less accessible than delegate {1}
        /// </summary>
        CS0059 = 0059,
        /// <summary>
        /// Inconsistent accessibility: base class {0} is less accessible than class {1}
        /// </summary>
        CS0060 = 0060,
        /// <summary>
        /// Inconsistent accessibility: base interface {0} is less accessible than interface {1}
        /// </summary>
        CS0061 = 0061,
        /// <summary>
        /// {0}: event property must have both add and remove accessors
        /// </summary>
        CS0065 = 0065,
        /// <summary>
        /// {0}: event must be of a delegate type
        /// </summary>
        CS0066 = 0066,
        /// <summary>
        /// {0}: event in interface cannot have initializer
        /// </summary>
        CS0068 = 0068,
        /// <summary>
        /// An event in an interface cannot have add or remove accessors
        /// </summary>
        CS0069 = 0069,
        /// <summary>
        /// The event {0} can only appear on the left hand side of += or -= (except when used from within the type {1})
        /// </summary>
        CS0070 = 0070,
        /// <summary>
        /// An explicit interface implementation of an event must use event accessor syntax
        /// </summary>
        CS0071 = 0071,
        /// <summary>
        /// {0} : cannot override; {1} is not an event
        /// </summary>
        CS0072 = 0072,
        /// <summary>
        /// An add or remove accessor must have a body
        /// </summary>
        CS0073 = 0073,
        /// <summary>
        /// {0}: abstract event cannot have initializer
        /// </summary>
        CS0074 = 0074,
        /// <summary>
        /// To cast a negative value, you must enclose the value in parentheses
        /// </summary>
        CS0075 = 0075,
        /// <summary>
        /// The enumerator name 'value__' is reserved and cannot be used
        /// </summary>
        CS0076 = 0076,
        /// <summary>
        /// The as operator must be used with a reference type or nullable type ({0} is a non-nullable value type).
        /// </summary>
        CS0077 = 0077,
        /// <summary>
        /// The event {0} can only appear on the left hand side of += or -=
        /// </summary>
        CS0079 = 0079,
        /// <summary>
        /// Constraints are not allowed on non-generic declarations
        /// </summary>
        CS0080 = 0080,
        /// <summary>
        /// Type parameter declaration must be an identifier not a type
        /// </summary>
        CS0081 = 0081,
        /// <summary>
        /// Type {0} already reserves a member called {1} with the same parameter types
        /// </summary>
        CS0082 = 0082,
        /// <summary>
        /// The parameter name {0} is a duplicate
        /// </summary>
        CS0100 = 0100,
        /// <summary>
        /// The namespace {0} already contains a definition for {1}
        /// </summary>
        CS0101 = 0101,
        /// <summary>
        /// The type {0} already contains a definition for {1}
        /// </summary>
        CS0102 = 0102,
        /// <summary>
        /// The name {0} does not exist in the current context
        /// </summary>
        CS0103 = 0103,
        /// <summary>
        /// {0} is an ambiguous reference between {1} and {2}
        /// </summary>
        CS0104 = 0104,
        /// <summary>
        /// The modifier {0} is not valid for this item
        /// </summary>
        CS0106 = 0106,
        /// <summary>
        /// More than one protection modifier
        /// </summary>
        CS0107 = 0107,
        /// <summary>
        /// The evaluation of the constant value for {0} involves a circular definition
        /// </summary>
        CS0110 = 0110,
        /// <summary>
        /// Type {0} already defines a member called {1} with the same parameter types
        /// </summary>
        CS0111 = 0111,
        /// <summary>
        /// A static member {0} cannot be marked as override, virtual or abstract
        /// </summary>
        CS0112 = 0112,
        /// <summary>
        /// A member {0} marked as override cannot be marked as new or virtual
        /// </summary>
        CS0113 = 0113,
        /// <summary>
        /// {0} : no suitable method found to override
        /// </summary>
        CS0115 = 0115,
        /// <summary>
        /// A namespace does not directly contain members such as fields or methods
        /// </summary>
        CS0116 = 0116,
        /// <summary>
        /// {0} does not contain a definition for 'identifier'
        /// </summary>
        CS0117 = 0117,
        /// <summary>
        /// {0} is a {1} but is used like a {2}
        /// </summary>
        CS0118 = 0118,
        /// <summary>
        /// {0} is a {1}, which is not valid in the given context.
        /// </summary>
        CS0119 = 0119,
        /// <summary>
        /// An object reference is required for the nonstatic field, method, or property {0}
        /// </summary>
        CS0120 = 0120,
        /// <summary>
        /// The call is ambiguous between the following methods or properties: {0} and {1}
        /// </summary>
        CS0121 = 0121,
        /// <summary>
        /// {0} is inaccessible due to its protection level
        /// </summary>
        CS0122 = 0122,
        /// <summary>
        /// No overload for {0} matches delegate {1}
        /// </summary>
        CS0123 = 0123,
        /// <summary>
        /// An object of a type convertible to {0} is required
        /// </summary>
        CS0126 = 0126,
        /// <summary>
        /// Since {0} returns void, a return keyword must not be followed by an object expression
        /// </summary>
        CS0127 = 0127,
        /// <summary>
        /// A local variable named {0} is already defined in this scope
        /// </summary>
        CS0128 = 0128,
        /// <summary>
        /// The left-hand side of an assignment must be a variable, property or indexer
        /// </summary>
        CS0131 = 0131,
        /// <summary>
        /// {0} : a static constructor must be parameterless
        /// </summary>
        CS0132 = 0132,
        /// <summary>
        /// The expression being assigned to {0} must be constant
        /// </summary>
        CS0133 = 0133,
        /// <summary>
        /// {0} is of type {1}. A const field of a reference type other than string can only be initialized with null.
        /// </summary>
        CS0134 = 0134,
        /// <summary>
        /// {0} conflicts with the declaration {1}
        /// </summary>
        CS0135 = 0135,
        /// <summary>
        /// A local variable named {0} cannot be declared in this scope because it would give a different meaning to {0}, which is already used in a 'parent or current/child' scope to denote something else
        /// </summary>
        CS0136 = 0136,
        /// <summary>
        /// A using namespace directive can only be applied to namespaces; {0} is a type not a namespace
        /// </summary>
        CS0138 = 0138,
        /// <summary>
        /// No enclosing loop out of which to break or continue
        /// </summary>
        CS0139 = 0139,
        /// <summary>
        /// The label {0} is a duplicate
        /// </summary>
        CS0140 = 0140,
        /// <summary>
        /// The type {0} has no constructors defined
        /// </summary>
        CS0143 = 0143,
        /// <summary>
        /// Cannot create an instance of the abstract class or interface {0}
        /// </summary>
        CS0144 = 0144,
        /// <summary>
        /// A const field requires a value to be provided
        /// </summary>
        CS0145 = 0145,
        /// <summary>
        /// Circular base class dependency involving {0} and {1}
        /// </summary>
        CS0146 = 0146,
        /// <summary>
        /// The delegate {0} does not have a valid constructor
        /// </summary>
        CS0148 = 0148,
        /// <summary>
        /// Method name expected
        /// </summary>
        CS0149 = 0149,
        /// <summary>
        /// A constant value is expected
        /// </summary>
        CS0150 = 0150,
        /// <summary>
        /// A value of an integral type expected
        /// </summary>
        CS0151 = 0151,
        /// <summary>
        /// The label {0} already occurs in this switch statement
        /// </summary>
        CS0152 = 0152,
        /// <summary>
        /// A goto case is only valid inside a switch statement
        /// </summary>
        CS0153 = 0153,
        /// <summary>
        /// The property or indexer 'property' cannot be used in this context because it lacks the get accessor
        /// </summary>
        CS0154 = 0154,
        /// <summary>
        /// The type caught or thrown must be derived from System.Exception
        /// </summary>
        CS0155 = 0155,
        /// <summary>
        /// A throw statement with no arguments is not allowed in a finally clause that is nested inside the nearest enclosing catch clause
        /// </summary>
        CS0156 = 0156,
        /// <summary>
        /// Control cannot leave the body of a finally clause
        /// </summary>
        CS0157 = 0157,
        /// <summary>
        /// The label {0} shadows another label by the same name in a contained scope
        /// </summary>
        CS0158 = 0158,
        /// <summary>
        /// No such label {0} within the scope of the goto statement
        /// </summary>
        CS0159 = 0159,
        /// <summary>
        /// A previous catch clause already catches all exceptions of this or of a super type ({0})
        /// </summary>
        CS0160 = 0160,
        /// <summary>
        /// {0}: not all code paths return a value
        /// </summary>
        CS0161 = 0161,
        /// <summary>
        /// Control cannot fall through from one case label ({0}) to another
        /// </summary>
        CS0163 = 0163,
        /// <summary>
        /// Use of unassigned local variable {0}
        /// </summary>
        CS0165 = 0165,
        /// <summary>
        /// The delegate {0} is missing the Invoke method
        /// </summary>
        CS0167 = 0167,
        /// <summary>
        /// Use of possibly unassigned field {0}
        /// </summary>
        CS0170 = 0170,
        /// <summary>
        /// Backing field for automatically implemented property {0} must be fully assigned before control is returned to the caller. Consider calling the default constructor from a constructor initializer.
        /// </summary>
        CS0171 = 0171,
        /// <summary>
        /// Type of conditional expression cannot be determined because {0} and {1} implicitly convert to one another
        /// </summary>
        CS0172 = 0172,
        /// <summary>
        /// Type of conditional expression cannot be determined because there is no implicit conversion between {0} and {1}
        /// </summary>
        CS0173 = 0173,
        /// <summary>
        /// A base class is required for a 'base' reference
        /// </summary>
        CS0174 = 0174,
        /// <summary>
        /// Use of keyword 'base' is not valid in this context
        /// </summary>
        CS0175 = 0175,
        /// <summary>
        /// Static member {0} cannot be accessed with an instance reference; qualify it with a type name instead
        /// </summary>
        CS0176 = 0176,
        /// <summary>
        /// The out parameter {0} must be assigned to before control leaves the current method
        /// </summary>
        CS0177 = 0177,
        /// <summary>
        /// Invalid rank specifier: expected ',' or ']'
        /// </summary>
        CS0178 = 0178,
        /// <summary>
        /// {0} cannot be extern and declare a body
        /// </summary>
        CS0179 = 0179,
        /// <summary>
        /// {0} cannot be both extern and abstract
        /// </summary>
        CS0180 = 0180,
        /// <summary>
        /// An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type
        /// </summary>
        CS0182 = 0182,
        /// <summary>
        /// {0} is not a reference type as required by the lock statement
        /// </summary>
        CS0185 = 0185,
        /// <summary>
        /// Use of null is not valid in this context 
        /// </summary>
        CS0186 = 0186,
        /// <summary>
        /// The 'this' object cannot be used before all of its fields are assigned to
        /// </summary>
        CS0188 = 0188,
        /// <summary>
        /// Property or indexer {0} cannot be assigned to -- it is read only
        /// </summary>
        CS0191 = 0191,
        /// <summary>
        /// Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)
        /// </summary>
        CS0192 = 0192,
        /// <summary>
        /// The * or -> operator must be applied to a pointer
        /// </summary>
        CS0193 = 0193,
        /// <summary>
        /// A pointer must be indexed by only one value
        /// </summary>
        CS0196 = 0196,
        /// <summary>
        /// Fields of static readonly field {0} cannot be assigned to (except in a static constructor or a variable initializer)
        /// </summary>
        CS0198 = 0198,
        /// <summary>
        /// Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)
        /// </summary>
        CS0199 = 0199,
        /// <summary>
        /// Property or indexer {0} cannot be assigned to — it is read only
        /// </summary>
        CS0200 = 0200,
        /// <summary>
        /// Only assignment, call, increment, decrement, and new object expressions can be used as a statement
        /// </summary>
        CS0201 = 0201,
        /// <summary>
        /// foreach requires that the return type {0} of '{1}.GetEnumerator()' must have a suitable public MoveNext method and public Current property
        /// </summary>
        CS0202 = 0202,
        /// <summary>
        /// Only 65534 locals are allowed
        /// </summary>
        CS0204 = 0204,
        /// <summary>
        /// Cannot call an abstract base member: {0}
        /// </summary>
        CS0205 = 0205,
        /// <summary>
        /// A property or indexer may not be passed as an out or ref parameter
        /// </summary>
        CS0206 = 0206,
        /// <summary>
        /// Cannot take the address of, get the size of, or declare a pointer to a managed type ({0})
        /// </summary>
        CS0208 = 0208,
        /// <summary>
        /// The type of local declared in a fixed statement must be a pointer type
        /// </summary>
        CS0209 = 0209,
        /// <summary>
        /// You must provide an initializer in a fixed or using statement declaration
        /// </summary>
        CS0210 = 0210,
        /// <summary>
        /// Cannot take the address of the given expression
        /// </summary>
        CS0211 = 0211,
        /// <summary>
        /// You can only take the address of an unfixed expression inside of a fixed statement initializer
        /// </summary>
        CS0212 = 0212,
        /// <summary>
        /// You cannot use the fixed statement to take the address of an already fixed expression
        /// </summary>
        CS0213 = 0213,
        /// <summary>
        /// Pointers and fixed size buffers may only be used in an unsafe context
        /// </summary>
        CS0214 = 0214,
        /// <summary>
        /// The return type of operator True or False must be bool
        /// </summary>
        CS0215 = 0215,
        /// <summary>
        /// The operator {0} requires a matching operator {1} to also be defined
        /// </summary>
        CS0216 = 0216,
        /// <summary>
        /// In order to be applicable as a short circuit operator a user-defined logical operator ({0}) must have the same return type as the type of its 2 parameters.
        /// </summary>
        CS0217 = 0217,
        /// <summary>
        /// The type ({0}) must contain declarations of operator true and operator false
        /// </summary>
        CS0218 = 0218,
        /// <summary>
        /// The operation overflows at compile time in checked mode
        /// </summary>
        CS0220 = 0220,
        /// <summary>
        /// Constant value {0} cannot be converted to a {1} (use 'unchecked' syntax to override)
        /// </summary>
        CS0221 = 0221,
        /// <summary>
        /// The params parameter must be a single dimensional array
        /// </summary>
        CS0225 = 0225,
        /// <summary>
        /// An __arglist expression may only appear inside of a call or new expression.
        /// </summary>
        CS0226 = 0226,
        /// <summary>
        /// Unsafe code may only appear if compiling with /unsafe
        /// </summary>
        CS0227 = 0227,
        /// <summary>
        /// {0} does not contain a definition for {1}, or it is not accessible
        /// </summary>
        CS0228 = 0228,
        /// <summary>
        /// Ambiguity between {0} and {1}
        /// </summary>
        CS0229 = 0229,
        /// <summary>
        /// Type and identifier are both required in a foreach statement
        /// </summary>
        CS0230 = 0230,
        /// <summary>
        /// A params parameter must be the last parameter in a formal parameter list.
        /// </summary>
        CS0231 = 0231,
        /// <summary>
        /// {0} does not have a predefined size, therefore sizeof can only be used in an unsafe context (consider using System.Runtime.InteropServices.Marshal.SizeOf)
        /// </summary>
        CS0233 = 0233,
        /// <summary>
        /// The type or namespace name {0} does not exist in the namespace {1} (are you missing an assembly reference?)
        /// </summary>
        CS0234 = 0234,
        /// <summary>
        /// A field initializer cannot reference the nonstatic field, method, or property {0}
        /// </summary>
        CS0236 = 0236,
        /// <summary>
        /// {0} cannot be sealed because it is not an override
        /// </summary>
        CS0238 = 0238,
        /// <summary>
        /// {0} : cannot override inherited member {1} because it is sealed
        /// </summary>
        CS0239 = 0239,
        /// <summary>
        /// Default parameter specifiers are not permitted
        /// </summary>
        CS0241 = 0241,
        /// <summary>
        /// The operation in question is undefined on void pointers
        /// </summary>
        CS0242 = 0242,
        /// <summary>
        /// The Conditional attribute is not valid on 'method' because it is an override method
        /// </summary>
        CS0243 = 0243,
        /// <summary>
        /// Neither 'is' nor 'as' is valid on pointer types
        /// </summary>
        CS0244 = 0244,
        /// <summary>
        /// Destructors and object.Finalize cannot be called directly. Consider calling IDisposable.Dispose if available.
        /// </summary>
        CS0245 = 0245,
        /// <summary>
        /// The type or namespace name {0} could not be found (are you missing a using directive or an assembly reference?)
        /// </summary>
        CS0246 = 0246,
        /// <summary>
        /// Cannot use a negative size with stackalloc
        /// </summary>
        CS0247 = 0247,
        /// <summary>
        /// Cannot create an array with a negative size
        /// </summary>
        CS0248 = 0248,
        /// <summary>
        /// Do not override object.Finalize. Instead, provide a destructor.
        /// </summary>
        CS0249 = 0249,
        /// <summary>
        /// Do not directly call your base class Finalize method. It is called automatically from your destructor.
        /// </summary>
        CS0250 = 0250,
        /// <summary>
        /// The right hand side of a fixed statement assignment may not be a cast expression
        /// </summary>
        CS0254 = 0254,
        /// <summary>
        /// stackalloc may not be used in a catch or finally block
        /// </summary>
        CS0255 = 0255,
        /// <summary>
        /// Missing partial modifier on declaration of type {0}; another partial declaration of this type exists
        /// </summary>
        CS0260 = 0260,
        /// <summary>
        /// Partial declarations of {0} must be all classes, all structs, or all interfaces
        /// </summary>
        CS0261 = 0261,
        /// <summary>
        /// Partial declarations of {0} have conflicting accessibility modifiers
        /// </summary>
        CS0262 = 0262,
        /// <summary>
        /// Partial declarations of {0} must not specify different base classes
        /// </summary>
        CS0263 = 0263,
        /// <summary>
        /// Partial declarations of {0} must have the same type parameter names in the same order
        /// </summary>
        CS0264 = 0264,
        /// <summary>
        /// Partial declarations of {0} have inconsistent constraints for type parameter {1}
        /// </summary>
        CS0265 = 0265,
        /// <summary>
        /// Cannot implicitly convert type {0} to {1}. An explicit conversion exists (are you missing a cast?)
        /// </summary>
        CS0266 = 0266,
        /// <summary>
        /// The partial modifier can only appear immediately before 'class', 'struct', or 'interface'
        /// </summary>
        CS0267 = 0267,
        /// <summary>
        /// Imported type {0} is invalid. It contains a circular base class dependency.
        /// </summary>
        CS0268 = 0268,
        /// <summary>
        /// Use of unassigned out parameter {0}
        /// </summary>
        CS0269 = 0269,
        /// <summary>
        /// Array size cannot be specified in a variable declaration (try initializing with a 'new' expression)
        /// </summary>
        CS0270 = 0270,
        /// <summary>
        /// The property or indexer {0} cannot be used in this context because the get accessor is inaccessible
        /// </summary>
        CS0271 = 0271,
        /// <summary>
        /// The property or indexer {0} cannot be used in this context because the set accessor is inaccessible
        /// </summary>
        CS0272 = 0272,
        /// <summary>
        /// The accessibility modifier of the {0} accessor must be more restrictive than the property or indexer {1}
        /// </summary>
        CS0273 = 0273,
        /// <summary>
        /// Cannot specify accessibility modifiers for both accessors of the property or indexer {0}
        /// </summary>
        CS0274 = 0274,
        /// <summary>
        /// {0}: accessibility modifiers may not be used on accessors in an interface
        /// </summary>
        CS0275 = 0275,
        /// <summary>
        /// {0}: accessibility modifiers on accessors may only be used if the property or indexer has both a get and a set accessor
        /// </summary>
        CS0276 = 0276,
        /// <summary>
        /// {0} does not implement interface member {1}. {2} is not public
        /// </summary>
        CS0277 = 0277,
        /// <summary>
        /// Friend access was granted to {0}, but the output assembly is named {1}. Try adding a reference to {0} or changing the output assembly name to match.
        /// </summary>
        CS0281 = 0281,
        /// <summary>
        /// The type {0} cannot be declared const
        /// </summary>
        CS0283 = 0283,
        /// <summary>
        /// Cannot create an instance of the variable type {0} because it does not have the new() constraint
        /// </summary>
        CS0304 = 0304,
        /// <summary>
        /// Using the generic type {0} requires {1} type arguments
        /// </summary>
        CS0305 = 0305,
        /// <summary>
        /// The type {0} may not be used as a type argument
        /// </summary>
        CS0306 = 0306,
        /// <summary>
        /// The {0} {1} is not a generic method. If you intended an expression list, use parentheses around the < expression.
        /// </summary>
        CS0307 = 0307,
        /// <summary>
        /// The non-generic type-or-method {0} cannot be used with type arguments.
        /// </summary>
        CS0308 = 0308,
        /// <summary>
        /// The type {0} must be a non-abstract type with a public parameterless constructor in order to use it as parameter {1} in the generic type or method {2}
        /// </summary>
        CS0310 = 0310,
        /// <summary>
        /// The type {0} cannot be used as type parameter {2} in the generic type or method {3}. There is no implicit reference conversion from {0} to {1}.
        /// </summary>
        CS0311 = 0311,
        /// <summary>
        /// The type {0} cannot be used as type parameter {1} in the generic type or method {2}. The nullable type {0} does not satisfy the constraint of {3}.
        /// </summary>
        CS0312 = 0312,
        /// <summary>
        /// The type {0} cannot be used as type parameter {1} in the generic type or method {2}. The nullable type {0} does not satisfy the constraint of {2}. Nullable types cannot satisfy any interface constraints.
        /// </summary>
        CS0313 = 0313,
        /// <summary>
        /// The type {0} cannot be used as type parameter {1} in the generic type or method {2}. There is no boxing conversion or type parameter conversion from {3} to {4}.
        /// </summary>
        CS0314 = 0314,
        /// <summary>
        /// The type {0} cannot be used as type parameter '{1}' in the generic type or method '{2}'. There is no boxing conversion from '{3}' to '{4}'.
        /// </summary>
        CS0315 = 0315,
        /// <summary>
        /// The parameter name {0} conflicts with an automatically-generated parameter name.
        /// </summary>
        CS0316 = 0316,
        /// <summary>
        /// The type or namespace name {0} could not be found in the global namespace (are you missing an assembly reference?)
        /// </summary>
        CS0400 = 0400,
        /// <summary>
        /// The new() constraint must be the last constraint specified
        /// </summary>
        CS0401 = 0401,
        /// <summary>
        /// Cannot convert null to type parameter {0} because it could be a non-nullable value type. Consider using default({0}) instead.
        /// </summary>
        CS0403 = 0403,
        /// <summary>
        /// '<' unexpected : attributes cannot be generic
        /// </summary>
        CS0404 = 0404,
        /// <summary>
        /// Duplicate constraint {0} for type parameter {1}
        /// </summary>
        CS0405 = 0405,
        /// <summary>
        /// The class type constraint {0} must come before any other constraints
        /// </summary>
        CS0406 = 0406,
        /// <summary>
        /// {0} has the wrong return type
        /// </summary>
        CS0407 = 0407,
        /// <summary>
        /// A constraint clause has already been specified for type parameter {0}. All of the constraints for a type parameter must be specified in a single where clause.
        /// </summary>
        CS0409 = 0409,
        /// <summary>
        /// No overload for {0} has the correct parameter and return types
        /// </summary>
        CS0410 = 0410,
        /// <summary>
        /// The type arguments for method {0} cannot be inferred from the usage. Try specifying the type arguments explicitly.
        /// </summary>
        CS0411 = 0411,
        /// <summary>
        /// {0}: a parameter or local variable cannot have the same name as a method type parameter
        /// </summary>
        CS0412 = 0412,
        /// <summary>
        /// The type parameter {0} cannot be used with the 'as' operator because it does not have a class type constraint nor a constraint that is a class
        /// </summary>
        CS0413 = 0413,
        /// <summary>
        /// The {0} attribute is valid only on an indexer that is not an explicit interface member declaration
        /// </summary>
        CS0415 = 0415,
        /// <summary>
        /// {0}: an attribute argument cannot use type parameters
        /// </summary>
        CS0416 = 0416,
        /// <summary>
        /// {0}: cannot provide arguments when creating an instance of a variable type
        /// </summary>
        CS0417 = 0417,
        /// <summary>
        /// {0}: an abstract class cannot be sealed or static
        /// </summary>
        CS0418 = 0418,
        /// <summary>
        /// Since {0} has the ComImport attribute, {1} must be extern or abstract
        /// </summary>
        CS0423 = 0423,
        /// <summary>
        /// {0}: a class with the ComImport attribute cannot specify a base class
        /// </summary>
        CS0424 = 0424,
        /// <summary>
        /// The constraints for type parameter {0} of method {1} must match the constraints for type parameter {2} of interface method {3}. Consider using an explicit interface implementation instead.
        /// </summary>
        CS0425 = 0425,
        /// <summary>
        /// The type name {0} does not exist in the type {1}
        /// </summary>
        CS0426 = 0426,
        /// <summary>
        /// Cannot convert method group {0} to non-delegate type {1}. Did you intend to invoke the method?
        /// </summary>
        CS0428 = 0428,
        /// <summary>
        /// The extern alias {0} was not specified in a /reference option
        /// </summary>
        CS0430 = 0430,
        /// <summary>
        /// Cannot use alias {0} with '::' since the alias references a type. Use '.' instead.
        /// </summary>
        CS0431 = 0431,
        /// <summary>
        /// Alias {0} not found
        /// </summary>
        CS0432 = 0432,
        /// <summary>
        /// The type {0} exists in both {1} and {2}
        /// </summary>
        CS0433 = 0433,
        /// <summary>
        /// The namespace {0} in {1} conflicts with the type {2} in {3}
        /// </summary>
        CS0434 = 0434,
        /// <summary>
        /// The type {0} in {1} conflicts with the namespace {2} in {3}.
        /// </summary>
        CS0438 = 0438,
        /// <summary>
        /// An extern alias declaration must precede all other elements defined in the namespace
        /// </summary>
        CS0439 = 0439,
        /// <summary>
        /// {0}: a class cannot be both static and sealed
        /// </summary>
        CS0441 = 0441,
        /// <summary>
        /// {0}: abstract properties cannot have private accessors
        /// </summary>
        CS0442 = 0442,
        /// <summary>
        /// Syntax error, value expected
        /// </summary>
        CS0443 = 0443,
        /// <summary>
        /// Cannot modify the result of an unboxing conversion
        /// </summary>
        CS0445 = 0445,
        /// <summary>
        /// Foreach cannot operate on a {0}. Did you intend to invoke the {0}?
        /// </summary>
        CS0446 = 0446,
        /// <summary>
        /// Attributes cannot be used on type arguments, only on type parameters
        /// </summary>
        CS0447 = 0447,
        /// <summary>
        /// The return type for ++ or -- operator must be the containing type or derived from the containing type
        /// </summary>
        CS0448 = 0448,
        /// <summary>
        /// The 'class' or 'struct' constraint must come before any other constraints
        /// </summary>
        CS0449 = 0449,
        /// <summary>
        /// {0}: cannot specify both a constraint class and the 'class' or 'struct' constraint
        /// </summary>
        CS0450 = 0450,
        /// <summary>
        /// The 'new()' constraint cannot be used with the 'struct' constraint
        /// </summary>
        CS0451 = 0451,
        /// <summary>
        /// The type {0} must be a reference type in order to use it as parameter {1} in the generic type or method {2}.
        /// </summary>
        CS0452 = 0452,
        /// <summary>
        /// The type {0} must be a non-nullable value type in order to use it as parameter {1} in the generic type or method {2}
        /// </summary>
        CS0453 = 0453,
        /// <summary>
        /// Circular constraint dependency involving {0} and {1}
        /// </summary>
        CS0454 = 0454,
        /// <summary>
        /// Type parameter {0} inherits conflicting constraints {1} and {2}
        /// </summary>
        CS0455 = 0455,
        /// <summary>
        /// Type parameter {0} has the 'struct' constraint so {0} cannot be used as a constraint for {1}
        /// </summary>
        CS0456 = 0456,
        /// <summary>
        /// Ambiguous user defined conversions {0} and {1} when converting from {2} to {3}
        /// </summary>
        CS0457 = 0457,
        /// <summary>
        /// Cannot take the address of a read-only local variable
        /// </summary>
        CS0459 = 0459,
        /// <summary>
        /// Constraints for override and explicit interface implementation methods are inherited from the base method, so they cannot be specified directly
        /// </summary>
        CS0460 = 0460,
        /// <summary>
        /// The inherited members {0} and {1} have the same signature in type {1}, so they cannot be overridden
        /// </summary>
        CS0462 = 0462,
        /// <summary>
        /// Evaluation of the decimal constant expression failed with error: {0}
        /// </summary>
        CS0463 = 0463,
        /// <summary>
        /// {0} should not have a params parameter since {1} does not
        /// </summary>
        CS0466 = 0466,
        /// <summary>
        /// Ambiguity between type {0} and type {1}
        /// </summary>
        CS0468 = 0468,
        /// <summary>
        /// Method {0} cannot implement interface accessor {1} for type {2}. Use an explicit interface implementation.
        /// </summary>
        CS0470 = 0470,
        /// <summary>
        /// The method [0} is not a generic method. If you intended an expression list, use parentheses around the &lt; expression.
        /// </summary>
        CS0471 = 0471,
        /// <summary>
        /// Explicit interface implementation {0} matches more than one interface member. Which interface member is actually chosen is implementation-dependent. Consider using a non-explicit implementation instead.
        /// </summary>
        CS0473 = 0473,
        /// <summary>
        /// {0} cannot declare a body because it is marked abstract
        /// </summary>
        CS0500 = 0500,
        /// <summary>
        /// {0} must declare a body because it is not marked abstract, extern, or partial
        /// </summary>
        CS0501 = 0501,
        /// <summary>
        /// {0} cannot be both abstract and sealed
        /// </summary>
        CS0502 = 0502,
        /// <summary>
        /// The abstract method {0} cannot be marked virtual
        /// </summary>
        CS0503 = 0503,
        /// <summary>
        /// The constant {0} cannot be marked static
        /// </summary>
        CS0504 = 0504,
        /// <summary>
        /// {0}: cannot override because {1} is not a function
        /// </summary>
        CS0505 = 0505,
        /// <summary>
        /// {0} : cannot override inherited member {1} because it is not marked 'virtual', 'abstract', or 'override'
        /// </summary>
        CS0506 = 0506,
        /// <summary>
        /// {0} : cannot change access modifiers when overriding {1} inherited member {2}
        /// </summary>
        CS0507 = 0507,
        /// <summary>
        /// {0}: return type must be {1} to match overridden member {2}
        /// </summary>
        CS0508 = 0508,
        /// <summary>
        /// {0} : cannot derive from sealed type {1}
        /// </summary>
        CS0509 = 0509,
        /// <summary>
        /// {0} is abstract but it is contained in nonabstract class {1}
        /// </summary>
        CS0513 = 0513,
        /// <summary>
        /// {0} : static constructor cannot have an explicit 'this' or 'base' constructor call
        /// </summary>
        CS0514 = 0514,
        /// <summary>
        /// {0} : access modifiers are not allowed on static constructors
        /// </summary>
        CS0515 = 0515,
        /// <summary>
        /// Constructor {0} can not call itself
        /// </summary>
        CS0516 = 0516,
        /// <summary>
        /// {0} has no base class and cannot call a base constructor
        /// </summary>
        CS0517 = 0517,
        /// <summary>
        /// Predefined type {0} is not defined or imported
        /// </summary>
        CS0518 = 0518,
        /// <summary>
        /// Predefined type {0} is declared incorrectly
        /// </summary>
        CS0520 = 0520,
        /// <summary>
        /// {0} : structs cannot call base class constructors
        /// </summary>
        CS0522 = 0522,
        /// <summary>
        /// Struct member {0} of type {1} causes a cycle in the struct layout
        /// </summary>
        CS0523 = 0523,
        /// <summary>
        /// {0} : interfaces cannot declare types
        /// </summary>
        CS0524 = 0524,
        /// <summary>
        /// Interfaces cannot contain fields
        /// </summary>
        CS0525 = 0525,
        /// <summary>
        /// Interfaces cannot contain constructors
        /// </summary>
        CS0526 = 0526,
        /// <summary>
        /// Type {0} in interface list is not an interface
        /// </summary>
        CS0527 = 0527,
        /// <summary>
        /// {0} is already listed in interface list
        /// </summary>
        CS0528 = 0528,
        /// <summary>
        /// Inherited interface {0} causes a cycle in the interface hierarchy of {1}
        /// </summary>
        CS0529 = 0529,
        /// <summary>
        /// {0} : interface members cannot have a definition
        /// </summary>
        CS0531 = 0531,
        /// <summary>
        /// {0} hides inherited abstract member {1}
        /// </summary>
        CS0533 = 0533,
        /// <summary>
        /// {0} does not implement inherited abstract member {1}
        /// </summary>
        CS0534 = 0534,
        /// <summary>
        /// {0} does not implement interface member {1}
        /// </summary>
        CS0535 = 0535,
        /// <summary>
        /// The class System.Object cannot have a base class or implement an interface
        /// </summary>
        CS0537 = 0537,
        /// <summary>
        /// {0} in explicit interface declaration is not an interface
        /// </summary>
        CS0538 = 0538,
        /// <summary>
        /// {0} in explicit interface declaration is not a member of interface
        /// </summary>
        CS0539 = 0539,
        /// <summary>
        /// {0} : containing type does not implement interface {1}
        /// </summary>
        CS0540 = 0540,
        /// <summary>
        /// {0} : explicit interface declaration can only be declared in a class or struct
        /// </summary>
        CS0541 = 0541,
        /// <summary>
        /// {0} : member names cannot be the same as their enclosing type
        /// </summary>
        CS0542 = 0542,
        /// <summary>
        /// {0} : the enumerator value is too large to fit in its type
        /// </summary>
        CS0543 = 0543,
        /// <summary>
        /// {0}: cannot override because {1} is not a property
        /// </summary>
        CS0544 = 0544,
        /// <summary>
        /// {0} : cannot override because {1} does not have an overridable get accessor
        /// </summary>
        CS0545 = 0545,
        /// <summary>
        /// {0} : cannot override because {1} does not have an overridable set accessor
        /// </summary>
        CS0546 = 0546,
        /// <summary>
        /// {0} : property or indexer cannot have void type
        /// </summary>
        CS0547 = 0547,
        /// <summary>
        /// {0} : property or indexer must have at least one accessor
        /// </summary>
        CS0548 = 0548,
        /// <summary>
        /// {0} is a new virtual member in sealed class {1}
        /// </summary>
        CS0549 = 0549,
        /// <summary>
        /// {0} adds an accessor not found in interface member {1}
        /// </summary>
        CS0550 = 0550,
        /// <summary>
        /// Explicit interface implementation {0} is missing accessor {1}
        /// </summary>
        CS0551 = 0551,
        /// <summary>
        /// {0} : user defined conversion to/from interface
        /// </summary>
        CS0552 = 0552,
        /// <summary>
        /// {0} : user defined conversion to/from base class
        /// </summary>
        CS0553 = 0553,
        /// <summary>
        /// {0} : user defined conversion to/from derived class
        /// </summary>
        CS0554 = 0554,
        /// <summary>
        /// User-defined operator cannot take an object of the enclosing type and convert to an object of the enclosing type
        /// </summary>
        CS0555 = 0555,
        /// <summary>
        /// User-defined conversion must convert to or from the enclosing type
        /// </summary>
        CS0556 = 0556,
        /// <summary>
        /// Duplicate user-defined conversion in type {0}
        /// </summary>
        CS0557 = 0557,
        /// <summary>
        /// User-defined operator {0} must be declared static and public
        /// </summary>
        CS0558 = 0558,
        /// <summary>
        /// The parameter type for ++ or -- operator must be the containing type
        /// </summary>
        CS0559 = 0559,
        /// <summary>
        /// The parameter of a unary operator must be the containing type
        /// </summary>
        CS0562 = 0562,
        /// <summary>
        /// One of the parameters of a binary operator must be the containing type
        /// </summary>
        CS0563 = 0563,
        /// <summary>
        /// The first operand of an overloaded shift operator must have the same type as the containing type, and the type of the second operand must be int
        /// </summary>
        CS0564 = 0564,
        /// <summary>
        /// Interfaces cannot contain operators
        /// </summary>
        CS0567 = 0567,
        /// <summary>
        /// Structs cannot contain explicit parameterless constructors
        /// </summary>
        CS0568 = 0568,
        /// <summary>
        /// {1} : cannot override {0} because it is not supported by the language
        /// </summary>
        CS0569 = 0569,
        /// <summary>
        /// Property, indexer, or event {0} is not supported by the language; try directly calling accessor method {1}!
        /// </summary>
        CS0570 = 0570,
        /// <summary>
        /// {0} : cannot explicitly call operator or accessor
        /// </summary>
        CS0571 = 0571,
        /// <summary>
        /// {0} : cannot reference a type through an expression; try {1} instead
        /// </summary>
        CS0572 = 0572,
        /// <summary>
        /// {0} : cannot have instance field initializers in structs
        /// </summary>
        CS0573 = 0573,
        /// <summary>
        /// Name of destructor must match name of class
        /// </summary>
        CS0574 = 0574,
        /// <summary>
        /// Only class types can contain destructors
        /// </summary>
        CS0575 = 0575,
        /// <summary>
        /// Namespace {0} contains a definition conflicting with alias {1}
        /// </summary>
        CS0576 = 0576,
        /// <summary>
        /// The Conditional attribute is not valid on {0} because it is a constructor, destructor, operator, or explicit interface implementation
        /// </summary>
        CS0577 = 0577,
        /// <summary>
        /// The Conditional attribute is not valid on {0} because its return type is not void
        /// </summary>
        CS0578 = 0578,
        /// <summary>
        /// Duplicate {0} attribute
        /// </summary>
        CS0579 = 0579,
        /// <summary>
        /// The Conditional not valid on interface members
        /// </summary>
        CS0582 = 0582,
        /// <summary>
        /// Internal Compiler Error. An internal error has occurred in the compiler. To work around this problem, try simplifying or changing the program near the locations listed below. Locations at the top of the list are closer to the point at which the internal error occurred. Errors such as this can be reported to Microsoft by using the /errorreport option.
        /// </summary>
        CS0583 = 0583,
        /// <summary>
        /// Internal Compiler Error: stage {0} symbol {1}
        /// </summary>
        CS0584 = 0584,
        /// <summary>
        /// Internal Compiler Error: stage {0}
        /// </summary>
        CS0585 = 0585,
        /// <summary>
        /// Internal Compiler Error: stage {0}
        /// </summary>
        CS0586 = 0586,
        /// <summary>
        /// Internal Compiler Error: stage {0}
        /// </summary>
        CS0587 = 0587,
        /// <summary>
        /// Internal Compiler Error: stage 'LEX'
        /// </summary>
        CS0588 = 0588,
        /// <summary>
        /// Internal Compiler Error: stage 'PARSE'
        /// </summary>
        CS0589 = 0589,
        /// <summary>
        /// User-defined operators cannot return void
        /// </summary>
        CS0590 = 0590,
        /// <summary>
        /// Invalid value for argument to {0} attribute
        /// </summary>
        CS0591 = 0591,
        /// <summary>
        /// Attribute {0} is not valid on this declaration type. It is valid on {1} declarations only.
        /// </summary>
        CS0592 = 0592,
        /// <summary>
        /// Floating-point constant is outside the range of type {0}
        /// </summary>
        CS0594 = 0594,
        /// <summary>
        /// The Guid attribute must be specified with the ComImport attribute
        /// </summary>
        CS0596 = 0596,
        /// <summary>
        /// Invalid value for named attribute argument {0}
        /// </summary>
        CS0599 = 0599,
        /// <summary>
        /// The DllImport attribute must be specified on a method marked 'static' and 'extern'
        /// </summary>
        CS0601 = 0601,
        /// <summary>
        /// Cannot set the IndexerName attribute on an indexer marked override
        /// </summary>
        CS0609 = 0609,
        /// <summary>
        /// Field or property cannot be of type {0}
        /// </summary>
        CS0610 = 0610,
        /// <summary>
        /// Array elements cannot be of type {0}
        /// </summary>
        CS0611 = 0611,
        /// <summary>
        /// {0} is not an attribute class
        /// </summary>
        CS0616 = 0616,
        /// <summary>
        /// {0} is not a valid named attribute argument because it is not a valid attribute parameter type
        /// </summary>
        CS0617 = 0617,
        /// <summary>
        /// {0} is obsolete: {1}
        /// </summary>
        CS0619 = 0619,
        /// <summary>
        /// Indexers cannot have void type
        /// </summary>
        CS0620 = 0620,
        /// <summary>
        /// {0} : virtual or abstract members cannot be private
        /// </summary>
        CS0621 = 0621,
        /// <summary>
        /// Can only use array initializer expressions to assign to array types. Try using a new expression instead.
        /// </summary>
        CS0622 = 0622,
        /// <summary>
        /// Array initializers can only be used in a variable or field initializer. Try using a new expression instead.
        /// </summary>
        CS0623 = 0623,
        /// <summary>
        /// {0}: instance field types marked with StructLayout(LayoutKind.Explicit) must have a FieldOffset attribute
        /// </summary>
        CS0625 = 0625,
        /// <summary>
        /// Conditional member {0} cannot implement interface member {1} in type {2}
        /// </summary>
        CS0629 = 0629,
        /// <summary>
        /// ref and out are not valid in this context
        /// </summary>
        CS0631 = 0631,
        /// <summary>
        /// The argument to the {0} attribute must be a valid identifier
        /// </summary>
        CS0633 = 0633,
        /// <summary>
        /// {0} : System.Interop.UnmanagedType.CustomMarshaller requires named arguments ComType and Marshal
        /// </summary>
        CS0635 = 0635,
        /// <summary>
        /// The FieldOffset attribute can only be placed on members of types marked with the StructLayout(LayoutKind.Explicit)
        /// </summary>
        CS0636 = 0636,
        /// <summary>
        /// The FieldOffset attribute is not allowed on static or const fields
        /// </summary>
        CS0637 = 0637,
        /// <summary>
        /// {0} : attribute is only valid on classes derived from System.Attribute
        /// </summary>
        CS0641 = 0641,
        /// <summary>
        /// {0} duplicate named attribute argument
        /// </summary>
        CS0643 = 0643,
        /// <summary>
        /// {0} cannot derive from special class {1}
        /// </summary>
        CS0644 = 0644,
        /// <summary>
        /// Identifier too long
        /// </summary>
        CS0645 = 0645,
        /// <summary>
        /// Cannot specify the DefaultMember attribute on a type containing an indexer
        /// </summary>
        CS0646 = 0646,
        /// <summary>
        /// Error emitting {0} attribute -- {1}
        /// </summary>
        CS0647 = 0647,
        /// <summary>
        /// {0} is a type not supported by the language
        /// </summary>
        CS0648 = 0648,
        /// <summary>
        /// Bad array declarator: To declare a managed array the rank specifier precedes the variable's identifier. To declare a fixed size buffer field, use the fixed keyword before the field type.
        /// </summary>
        CS0650 = 0650,
        /// <summary>
        /// Cannot apply attribute class {0} because it is abstract
        /// </summary>
        CS0653 = 0653,
        /// <summary>
        /// {0} is not a valid named attribute argument because it is not a valid attribute parameter type
        /// </summary>
        CS0655 = 0655,
        /// <summary>
        /// Missing compiler required member {0}
        /// </summary>
        CS0656 = 0656,
        /// <summary>
        /// {0} cannot specify only Out attribute on a ref parameter. Use both In and Out attributes, or neither.
        /// </summary>
        CS0662 = 0662,
        /// <summary>
        /// Cannot define overloaded methods that differ only on ref and out.
        /// </summary>
        CS0663 = 0663,
        /// <summary>
        /// Literal of type double cannot be implicitly converted to type {0}; use an {1} suffix to create a literal of this type
        /// </summary>
        CS0664 = 0664,
        /// <summary>
        /// {0} : new protected member declared in struct
        /// </summary>
        CS0666 = 0666,
        /// <summary>
        /// The feature {0} is deprecated. Please use {1} instead.
        /// </summary>
        CS0667 = 0667,
        /// <summary>
        /// Two indexers have different names; the IndexerName attribute must be used with the same name on every indexer within a type
        /// </summary>
        CS0668 = 0668,
        /// <summary>
        /// A class with the ComImport attribute cannot have a user-defined constructor
        /// </summary>
        CS0669 = 0669,
        /// <summary>
        /// Field cannot have void type
        /// </summary>
        CS0670 = 0670,
        /// <summary>
        /// System.Void cannot be used from C# -- use typeof(void) to get the void type object.
        /// </summary>
        CS0673 = 0673,
        /// <summary>
        /// Do not use 'System.ParamArrayAttribute'. Use the 'params' keyword instead.
        /// </summary>
        CS0674 = 0674,
        /// <summary>
        /// {0}: a volatile field cannot be of the type {1}
        /// </summary>
        CS0677 = 0677,
        /// <summary>
        /// {0}: a field can not be both volatile and readonly
        /// </summary>
        CS0678 = 0678,
        /// <summary>
        /// The modifier 'abstract' is not valid on fields. Try using a property instead
        /// </summary>
        CS0681 = 0681,
        /// <summary>
        /// {0} cannot implement {1} because it is not supported by the language
        /// </summary>
        CS0682 = 0682,
        /// <summary>
        /// {0} explicit method implementation cannot implement {1} because it is an accessor
        /// </summary>
        CS0683 = 0683,
        /// <summary>
        /// Conditional member {0} cannot have an out parameter
        /// </summary>
        CS0685 = 0685,
        /// <summary>
        /// Accessor {0} cannot implement interface member {1} for type {2}. Use an explicit interface implementation.
        /// </summary>
        CS0686 = 0686,
        /// <summary>
        /// The namespace alias qualifier '::' always resolves to a type or namespace so is illegal here. Consider using '.' instead.
        /// </summary>
        CS0687 = 0687,
        /// <summary>
        /// Cannot derive from {0} because it is a type parameter
        /// </summary>
        CS0689 = 0689,
        /// <summary>
        /// Input file {0} contains invalid metadata.
        /// </summary>
        CS0690 = 0690,
        /// <summary>
        /// Duplicate type parameter {0}
        /// </summary>
        CS0692 = 0692,
        /// <summary>
        /// Type parameter {0} has the same name as the containing type, or method
        /// </summary>
        CS0694 = 0694,
        /// <summary>
        /// {0} cannot implement both {1} and {1} because they may unify for some type parameter substitutions
        /// </summary>
        CS0695 = 0695,
        /// <summary>
        /// A generic type cannot derive from {0} because it is an attribute class
        /// </summary>
        CS0698 = 0698,
        /// <summary>
        /// {0} does not define type parameter {1}
        /// </summary>
        CS0699 = 0699,
        /// <summary>
        /// {0} is not a valid constraint. A type used as a constraint must be an interface, a non-sealed class or a type parameter.
        /// </summary>
        CS0701 = 0701,
        /// <summary>
        /// Constraint cannot be special class {0}
        /// </summary>
        CS0702 = 0702,
        /// <summary>
        /// Inconsistent accessibility: constraint type {0} is less accessible than {1}
        /// </summary>
        CS0703 = 0703,
        /// <summary>
        /// Cannot do member lookup in {0} because it is a type parameter
        /// </summary>
        CS0704 = 0704,
        /// <summary>
        /// Invalid constraint type. A type used as a constraint must be an interface, a non-sealed class or a type parameter.
        /// </summary>
        CS0706 = 0706,
        /// <summary>
        /// {0}: cannot declare instance members in a static class
        /// </summary>
        CS0708 = 0708,
        /// <summary>
        /// {0}: cannot derive from static class {1}
        /// </summary>
        CS0709 = 0709,
        /// <summary>
        /// Static classes cannot have instance constructors
        /// </summary>
        CS0710 = 0710,
        /// <summary>
        /// Static classes cannot contain destructors
        /// </summary>
        CS0711 = 0711,
        /// <summary>
        /// Cannot create an instance of the static class {0}
        /// </summary>
        CS0712 = 0712,
        /// <summary>
        /// Static class {0} cannot derive from type {1}. Static classes must derive from object.
        /// </summary>
        CS0713 = 0713,
        /// <summary>
        /// {0} : static classes cannot implement interfaces
        /// </summary>
        CS0714 = 0714,
        /// <summary>
        /// {0} : static classes cannot contain user defined operators
        /// </summary>
        CS0715 = 0715,
        /// <summary>
        /// Cannot convert to static type {0}
        /// </summary>
        CS0716 = 0716,
        /// <summary>
        /// {0}: static classes cannot be used as constraints
        /// </summary>
        CS0717 = 0717,
        /// <summary>
        /// {0}: static types cannot be used as type arguments
        /// </summary>
        CS0718 = 0718,
        /// <summary>
        /// {0}: array elements cannot be of static type
        /// </summary>
        CS0719 = 0719,
        /// <summary>
        /// {0}: cannot declare indexers in a static class
        /// </summary>
        CS0720 = 0720,
        /// <summary>
        /// {0}: static types cannot be used as parameters
        /// </summary>
        CS0721 = 0721,
        /// <summary>
        /// {0}: static types cannot be used as return types
        /// </summary>
        CS0722 = 0722,
        /// <summary>
        /// Cannot declare variable of static type {0}
        /// </summary>
        CS0723 = 0723,
        /// <summary>
        /// does not need a CLSCompliant attribute because the assembly does not have a CLSCompliant attribute
        /// </summary>
        CS0724 = 0724,
        /// <summary>
        /// Type {0} is defined in this assembly, but a type forwarder is specified for it
        /// </summary>
        CS0729 = 0729,
        /// <summary>
        /// Cannot forward type {0} because it is a nested type of {1}
        /// </summary>
        CS0730 = 0730,
        /// <summary>
        /// The type forwarder for type {0} in assembly {1} causes a cycle
        /// </summary>
        CS0731 = 0731,
        /// <summary>
        /// Cannot forward generic type, {0}
        /// </summary>
        CS0733 = 0733,
        /// <summary>
        /// The /moduleassemblyname option may only be specified when building a target type of 'module'
        /// </summary>
        CS0734 = 0734,
        /// <summary>
        /// Invalid type specified as an argument for TypeForwardedTo attribute
        /// </summary>
        CS0735 = 0735,
        /// <summary>
        /// {0} does not implement interface member {1}. {2} cannot implement an interface member because it is static.
        /// </summary>
        CS0736 = 0736,
        /// <summary>
        /// {0} does not implement interface member {1}. {2} cannot implement an interface member because it is not public.
        /// </summary>
        CS0737 = 0737,
        /// <summary>
        /// {0} does not implement interface member {1}. {2} cannot implement {1} because it does not have the matching return type of {3}.
        /// </summary>
        CS0738 = 0738,
        /// <summary>
        /// {0} duplicate TypeForwardedToAttribute.
        /// </summary>
        CS0739 = 0739,
        /// <summary>
        /// A query body must end with a select clause or a group clause
        /// </summary>
        CS0742 = 0742,
        /// <summary>
        /// Expected contextual keyword 'on'
        /// </summary>
        CS0743 = 0743,
        /// <summary>
        /// Expected contextual keyword 'equals'
        /// </summary>
        CS0744 = 0744,
        /// <summary>
        /// Expected contextual keyword 'by'
        /// </summary>
        CS0745 = 0745,
        /// <summary>
        /// Invalid anonymous type member declarator. Anonymous type members must be declared with a member assignment, simple name or member access.
        /// </summary>
        CS0746 = 0746,
        /// <summary>
        /// Invalid initializer member declarator.
        /// </summary>
        CS0747 = 0747,
        /// <summary>
        /// Inconsistent lambda parameter usage; all parameter types must either be explicit or implicit.
        /// </summary>
        CS0748 = 0748,
        /// <summary>
        /// A partial method cannot have access modifiers or the virtual, abstract, override, new, sealed, or extern modifiers.
        /// </summary>
        CS0750 = 0750,
        /// <summary>
        /// A partial method must be declared in a partial class or partial struct
        /// </summary>
        CS0751 = 0751,
        /// <summary>
        /// A partial method cannot have out parameters
        /// </summary>
        CS0752 = 0752,
        /// <summary>
        /// Only methods, classes, structs, or interfaces may be partial.
        /// </summary>
        CS0753 = 0753,
        /// <summary>
        /// A partial method may not explicitly implement an interface method.
        /// </summary>
        CS0754 = 0754,
        /// <summary>
        /// Both partial method declarations must be extension methods or neither may be an extension method.
        /// </summary>
        CS0755 = 0755,
        /// <summary>
        /// A partial method may not have multiple defining declarations.
        /// </summary>
        CS0756 = 0756,
        /// <summary>
        /// A partial method may not have multiple implementing declarations.
        /// </summary>
        CS0757 = 0757,
        /// <summary>
        /// Both partial method declarations must use a params parameter or neither may use a params parameter
        /// </summary>
        CS0758 = 0758,
        /// <summary>
        /// No defining declaration found for implementing declaration of partial method {0}.
        /// </summary>
        CS0759 = 0759,
        /// <summary>
        /// Partial method declarations of {0} have inconsistent type parameter constraints.
        /// </summary>
        CS0761 = 0761,
        /// <summary>
        /// Cannot create delegate from method {0} because it is a partial method without an implementing declaration
        /// </summary>
        CS0762 = 0762,
        /// <summary>
        /// Both partial method declarations must be static or neither may be static.
        /// </summary>
        CS0763 = 0763,
        /// <summary>
        /// Both partial method declarations must be unsafe or neither may be unsafe
        /// </summary>
        CS0764 = 0764,
        /// <summary>
        /// Partial methods with only a defining declaration or removed conditional methods cannot be used in expression trees
        /// </summary>
        CS0765 = 0765,
        /// <summary>
        /// Partial methods must have a void return type.
        /// </summary>
        CS0766 = 0766,
        /// <summary>
        /// The fully qualified name for {0} is too long for debug information. Compile without '/debug' option.
        /// </summary>
        CS0811 = 0811,
        /// <summary>
        /// Cannot assign {0} to an implicitly typed local
        /// </summary>
        CS0815 = 0815,
        /// <summary>
        /// Implicitly typed locals must be initialized
        /// </summary>
        CS0818 = 0818,
        /// <summary>
        /// Implicitly typed locals cannot have multiple declarators.
        /// </summary>
        CS0819 = 0819,
        /// <summary>
        /// Cannot assign array initializer to an implicitly typed local
        /// </summary>
        CS0820 = 0820,
        /// <summary>
        /// Implicitly typed locals cannot be fixed
        /// </summary>
        CS0821 = 0821,
        /// <summary>
        /// Implicitly typed locals cannot be const
        /// </summary>
        CS0822 = 0822,
        /// <summary>
        /// The contextual keyword 'var' may only appear within a local variable declaration.
        /// </summary>
        CS0825 = 0825,
        /// <summary>
        /// No best type found for implicitly typed array.
        /// </summary>
        CS0826 = 0826,
        /// <summary>
        /// Cannot assign {0} to anonymous type property.
        /// </summary>
        CS0828 = 0828,
        /// <summary>
        /// An expression tree may not contain a base access.
        /// </summary>
        CS0831 = 0831,
        /// <summary>
        /// An expression tree may not contain an assignment operator.
        /// </summary>
        CS0832 = 0832,
        /// <summary>
        /// An anonymous type cannot have multiple properties with the same name.
        /// </summary>
        CS0833 = 0833,
        /// <summary>
        /// A lambda expression must have an expression body to be converted to an expression tree.
        /// </summary>
        CS0834 = 0834,
        /// <summary>
        /// Cannot convert lambda to an expression tree whose type argument {0} is not a delegate type.
        /// </summary>
        CS0835 = 0835,
        /// <summary>
        /// Cannot use anonymous type in a constant expression.
        /// </summary>
        CS0836 = 0836,
        /// <summary>
        /// The first operand of an 'is' or 'as' operator may not be a lambda expression or anonymous method.
        /// </summary>
        CS0837 = 0837,
        /// <summary>
        /// An expression tree may not contain a multidimensional array initializer.
        /// </summary>
        CS0838 = 0838,
        /// <summary>
        /// Argument missing.
        /// </summary>
        CS0839 = 0839,
        /// <summary>
        /// {0} must declare a body because it is not marked abstract or extern. Automatically implemented properties must define both get and set accessors.
        /// </summary>
        CS0840 = 0840,
        /// <summary>
        /// Cannot use variable {0} before it is declared.
        /// </summary>
        CS0841 = 0841,
        /// <summary>
        /// Automatically implemented properties cannot be used inside a type marked with StructLayout(LayoutKind.Explicit).
        /// </summary>
        CS0842 = 0842,
        /// <summary>
        /// Backing field for automatically implemented property {0} must be fully assigned before control is returned to the caller. Consider calling the default constructor from a constructor initializer.
        /// </summary>
        CS0843 = 0843,
        /// <summary>
        /// Cannot use local variable {0} before it is declared. The declaration of the local variable hides the field {0}.
        /// </summary>
        CS0844 = 0844,
        /// <summary>
        /// An expression tree lambda may not contain a coalescing operator with a null literal left-hand side.
        /// </summary>
        CS0845 = 0845,
        /// <summary>
        /// Identifier expected
        /// </summary>
        CS1001 = 1001,
        /// <summary>
        /// ; expected
        /// </summary>
        CS1002 = 1002,
        /// <summary>
        /// Syntax error, 'char' expected
        /// </summary>
        CS1003 = 1003,
        /// <summary>
        /// Duplicate {0} modifier
        /// </summary>
        CS1004 = 1004,
        /// <summary>
        /// Property accessor already defined
        /// </summary>
        CS1007 = 1007,
        /// <summary>
        /// Type byte, sbyte, short, ushort, int, uint, long, or ulong expected
        /// </summary>
        CS1008 = 1008,
        /// <summary>
        /// Unrecognized escape sequence
        /// </summary>
        CS1009 = 1009,
        /// <summary>
        /// Newline in constant
        /// </summary>
        CS1010 = 1010,
        /// <summary>
        /// Empty character literal
        /// </summary>
        CS1011 = 1011,
        /// <summary>
        /// Too many characters in character literal
        /// </summary>
        CS1012 = 1012,
        /// <summary>
        /// Invalid number
        /// </summary>
        CS1013 = 1013,
        /// <summary>
        /// A get or set accessor expected
        /// </summary>
        CS1014 = 1014,
        /// <summary>
        /// An object, string, or class type expected
        /// </summary>
        CS1015 = 1015,
        /// <summary>
        /// Named attribute argument expected
        /// </summary>
        CS1016 = 1016,
        /// <summary>
        /// Catch clauses cannot follow the general catch clause of a try statement
        /// </summary>
        CS1017 = 1017,
        /// <summary>
        /// Keyword 'this' or 'base' expected
        /// </summary>
        CS1018 = 1018,
        /// <summary>
        /// Overloadable unary operator expected
        /// </summary>
        CS1019 = 1019,
        /// <summary>
        /// Overloadable binary operator expected
        /// </summary>
        CS1020 = 1020,
        /// <summary>
        /// Integral constant is too large
        /// </summary>
        CS1021 = 1021,
        /// <summary>
        /// Type or namespace definition, or end-of-file expected
        /// </summary>
        CS1022 = 1022,
        /// <summary>
        /// Embedded statement cannot be a declaration or labeled statement
        /// </summary>
        CS1023 = 1023,
        /// <summary>
        /// Preprocessor directive expected
        /// </summary>
        CS1024 = 1024,
        /// <summary>
        /// Single-line comment or end-of-line expected
        /// </summary>
        CS1025 = 1025,
        /// <summary>
        /// ) expected
        /// </summary>
        CS1026 = 1026,
        /// <summary>
        /// #endif directive expected
        /// </summary>
        CS1027 = 1027,
        /// <summary>
        /// Unexpected preprocessor directive
        /// </summary>
        CS1028 = 1028,
        /// <summary>
        /// #error: {0}
        /// </summary>
        CS1029 = 1029,
        /// <summary>
        /// Type expected
        /// </summary>
        CS1031 = 1031,
        /// <summary>
        /// Cannot define/undefine preprocessor symbols after first token in file
        /// </summary>
        CS1032 = 1032,
        /// <summary>
        /// Source file has exceeded the limit of 16,707,565 lines representable in the PDB; debug information will be incorrect
        /// </summary>
        CS1033 = 1033,
        /// <summary>
        /// Compiler limit exceeded: Line cannot exceed 'number' characters
        /// </summary>
        CS1034 = 1034,
        /// <summary>
        /// End-of-file found, '*/' expected
        /// </summary>
        CS1035 = 1035,
        /// <summary>
        /// ( or . expected
        /// </summary>
        CS1036 = 1036,
        /// <summary>
        /// Overloadable operator expected
        /// </summary>
        CS1037 = 1037,
        /// <summary>
        /// #endregion directive expected
        /// </summary>
        CS1038 = 1038,
        /// <summary>
        /// Unterminated string literal
        /// </summary>
        CS1039 = 1039,
        /// <summary>
        /// Preprocessor directives must appear as the first non-whitespace character on a line
        /// </summary>
        CS1040 = 1040,
        /// <summary>
        /// Identifier expected, 'keyword' is a keyword
        /// </summary>
        CS1041 = 1041,
        /// <summary>
        /// { or ; expected
        /// </summary>
        CS1043 = 1043,
        /// <summary>
        /// Cannot use more than one type in a for, using, fixed, or declaration statement
        /// </summary>
        CS1044 = 1044,
        /// <summary>
        /// An add or remove accessor expected
        /// </summary>
        CS1055 = 1055,
        /// <summary>
        /// Unexpected character 'character'
        /// </summary>
        CS1056 = 1056,
        /// <summary>
        /// {0}: static classes cannot contain protected members
        /// </summary>
        CS1057 = 1057,
        /// <summary>
        /// The operand of an increment or decrement operator must be a variable, property or indexer.
        /// </summary>
        CS1059 = 1059,
        /// <summary>
        /// {0} does not contain a definition for {1} and no extension method {2} accepting a first argument of type {0} could be found (are you missing a using directive or an assembly reference?).
        /// </summary>
        CS1061 = 1061,
        /// <summary>
        /// Method {0} has a parameter modifier 'this' which is not on the first parameter.
        /// </summary>
        CS1100 = 1100,
        /// <summary>
        /// The parameter modifier 'ref' cannot be used with 'this'.
        /// </summary>
        CS1101 = 1101,
        /// <summary>
        /// The parameter modifier 'out' cannot be used with 'this'.
        /// </summary>
        CS1102 = 1102,
        /// <summary>
        /// The first parameter of an extension method cannot be of type {0}.
        /// </summary>
        CS1103 = 1103,
        /// <summary>
        /// A parameter array cannot be used with 'this' modifier on an extension method.
        /// </summary>
        CS1104 = 1104,
        /// <summary>
        /// Extension methods must be static.
        /// </summary>
        CS1105 = 1105,
        /// <summary>
        /// Extension methods must be defined in a non generic static class.
        /// </summary>
        CS1106 = 1106,
        /// <summary>
        /// A parameter can only have one {0} modifier.
        /// </summary>
        CS1107 = 1107,
        /// <summary>
        /// A parameter cannot have all the specified modifiers; there are too many modifiers on the parameter.
        /// </summary>
        CS1108 = 1108,
        /// <summary>
        /// Extension Methods must be defined on top level static classes, {0} is a nested class.
        /// </summary>
        CS1109 = 1109,
        /// <summary>
        /// Cannot use 'this' modifier on first parameter of method declaration without a reference to System.Core.dll. Add a reference to System.Core.dll or remove 'this' modifier from the method declaration.
        /// </summary>
        CS1110 = 1110,
        /// <summary>
        /// Do not use 'System.Runtime.CompilerServices.ExtensionAttribute'. Use the 'this' keyword instead.
        /// </summary>
        CS1112 = 1112,
        /// <summary>
        /// Extension methods {0} defined on value type {1} cannot be used to create delegates.
        /// </summary>
        CS1113 = 1113,
        /// <summary>
        /// No overload for method {0} takes {1} arguments
        /// </summary>
        CS1501 = 1501,
        /// <summary>
        /// The best overloaded Add method {0} for the collection initializer has some invalid arguments
        /// </summary>
        CS1502 = 1502,
        /// <summary>
        /// The best overloaded Add method {0} for the collection initializer has some invalid arguments
        /// </summary>
        CS1503 = 1503,
        /// <summary>
        /// Source file {0} could not be opened ({1})
        /// </summary>
        CS1504 = 1504,
        /// <summary>
        /// Cannot link resource file {0} when building a module
        /// </summary>
        CS1507 = 1507,
        /// <summary>
        /// Resource identifier {0} has already been used in this assembly
        /// </summary>
        CS1508 = 1508,
        /// <summary>
        /// Referenced file {0} is not an assembly; use '/addmodule' option instead
        /// </summary>
        CS1509 = 1509,
        /// <summary>
        /// A ref or out argument must be an assignable variable
        /// </summary>
        CS1510 = 1510,
        /// <summary>
        /// Keyword 'base' is not available in a static method
        /// </summary>
        CS1511 = 1511,
        /// <summary>
        /// Keyword 'base' is not available in the current context
        /// </summary>
        CS1512 = 1512,
        /// <summary>
        /// } expected
        /// </summary>
        CS1513 = 1513,
        /// <summary>
        /// { expected
        /// </summary>
        CS1514 = 1514,
        /// <summary>
        /// 'in' expected
        /// </summary>
        CS1515 = 1515,
        /// <summary>
        /// Invalid preprocessor expression
        /// </summary>
        CS1517 = 1517,
        /// <summary>
        /// Expected class, delegate, enum, interface, or struct
        /// </summary>
        CS1518 = 1518,
        /// <summary>
        /// Invalid token {0} in class, struct, or interface member declaration
        /// </summary>
        CS1519 = 1519,
        /// <summary>
        /// Method must have a return type
        /// </summary>
        CS1520 = 1520,
        /// <summary>
        /// Invalid base type
        /// </summary>
        CS1521 = 1521,
        /// <summary>
        /// Expected catch or finally
        /// </summary>
        CS1524 = 1524,
        /// <summary>
        /// Invalid expression term {0}
        /// </summary>
        CS1525 = 1525,
        /// <summary>
        /// A new expression requires (), [], or {} after type
        /// </summary>
        CS1526 = 1526,
        /// <summary>
        /// Elements defined in a namespace cannot be explicitly declared as private, protected, or protected internal
        /// </summary>
        CS1527 = 1527,
        /// <summary>
        /// Expected ; or = (cannot specify constructor arguments in declaration)
        /// </summary>
        CS1528 = 1528,
        /// <summary>
        /// A using clause must precede all other elements defined in the namespace except extern alias declarations
        /// </summary>
        CS1529 = 1529,
        /// <summary>
        /// Keyword 'new' is not allowed on elements defined in a namespace
        /// </summary>
        CS1530 = 1530,
        /// <summary>
        /// Overloaded binary operator {0} takes two parameters
        /// </summary>
        CS1534 = 1534,
        /// <summary>
        /// Overloaded unary operator {0} takes one parameter
        /// </summary>
        CS1535 = 1535,
        /// <summary>
        /// Invalid parameter type void
        /// </summary>
        CS1536 = 1536,
        /// <summary>
        /// The using alias {0} appeared previously in this namespace
        /// </summary>
        CS1537 = 1537,
        /// <summary>
        /// Cannot access protected member {0} via a qualifier of type {1}; the qualifier must be of type {2} (or derived from it)
        /// </summary>
        CS1540 = 1540,
        /// <summary>
        /// Invalid reference option: {0} — cannot reference directories
        /// </summary>
        CS1541 = 1541,
        /// <summary>
        /// {0} cannot be added to this assembly because it already is an assembly; use '/R' option instead
        /// </summary>
        CS1542 = 1542,
        /// <summary>
        /// Property, indexer, or event {0} is not supported by the language; try directly calling accessor methods {1} or {2}
        /// </summary>
        CS1545 = 1545,
        /// <summary>
        /// Property, indexer, or event {0} is not supported by the language; try directly calling accessor method {1}
        /// </summary>
        CS1546 = 1546,
        /// <summary>
        /// Keyword 'void' cannot be used in this context
        /// </summary>
        CS1547 = 1547,
        /// <summary>
        /// Cryptographic failure while signing assembly {0} — {1}
        /// </summary>
        CS1548 = 1548,
        /// <summary>
        /// Appropriate cryptographic service not found
        /// </summary>
        CS1549 = 1549,
        /// <summary>
        /// Indexers must have at least one parameter
        /// </summary>
        CS1551 = 1551,
        /// <summary>
        /// Array type specifier, [], must appear before parameter name
        /// </summary>
        CS1552 = 1552,
        /// <summary>
        /// Declaration is not valid; use '{0} operator {1} (...' instead
        /// </summary>
        CS1553 = 1553,
        /// <summary>
        /// Declaration is not valid; use '{0} operator {1} (...' instead
        /// </summary>
        CS1554 = 1554,
        /// <summary>
        /// Could not find {0} specified for Main method
        /// </summary>
        CS1555 = 1555,
        /// <summary>
        /// {0} specified for Main method must be a valid class or struct
        /// </summary>
        CS1556 = 1556,
        /// <summary>
        /// Cannot use {0} for Main method because it is in a different output file
        /// </summary>
        CS1557 = 1557,
        /// <summary>
        /// {0} does not have a suitable static Main method
        /// </summary>
        CS1558 = 1558,
        /// <summary>
        /// Cannot use {0} for Main method because it is imported
        /// </summary>
        CS1559 = 1559,
        /// <summary>
        /// Invalid filename specified for preprocessor directive. Filename is too long or not a valid filename
        /// </summary>
        CS1560 = 1560,
        /// <summary>
        /// Output filename is too long or invalid
        /// </summary>
        CS1561 = 1561,
        /// <summary>
        /// Outputs without source must have the /out option specified
        /// </summary>
        CS1562 = 1562,
        /// <summary>
        /// Output {0} does not have any source files
        /// </summary>
        CS1563 = 1563,
        /// <summary>
        /// Conflicting options specified: Win32 resource file; Win32 manifest.
        /// </summary>
        CS1564 = 1564,
        /// <summary>
        /// Conflicting options specified: Win32 resource file; Win32 icon
        /// </summary>
        CS1565 = 1565,
        /// <summary>
        /// Error reading resource file {0} — {1}
        /// </summary>
        CS1566 = 1566,
        /// <summary>
        /// Error generating Win32 resource: {0}
        /// </summary>
        CS1567 = 1567,
        /// <summary>
        /// Error generating XML documentation file {0} ({1})
        /// </summary>
        CS1569 = 1569,
        /// <summary>
        /// A stackalloc expression requires [] after type
        /// </summary>
        CS1575 = 1575,
        /// <summary>
        /// The line number specified for #line directive is missing or invalid
        /// </summary>
        CS1576 = 1576,
        /// <summary>
        /// Assembly generation failed — reason
        /// </summary>
        CS1577 = 1577,
        /// <summary>
        /// Filename, single-line comment or end-of-line expected
        /// </summary>
        CS1578 = 1578,
        /// <summary>
        /// foreach statement cannot operate on variables of type {0} because {1} does not contain a public definition for {2}
        /// </summary>
        CS1579 = 1579,
        /// <summary>
        /// {0} is not a valid Win32 resource file
        /// </summary>
        CS1583 = 1583,
        /// <summary>
        /// Member modifier {0} must precede the member type and name
        /// </summary>
        CS1585 = 1585,
        /// <summary>
        /// Array creation must have array size or array initializer
        /// </summary>
        CS1586 = 1586,
        /// <summary>
        /// Cannot determine common language runtime directory -- {0}
        /// </summary>
        CS1588 = 1588,
        /// <summary>
        /// Delegate {0} does not take {1} arguments
        /// </summary>
        CS1593 = 1593,
        /// <summary>
        /// Delegate {0} has some invalid arguments
        /// </summary>
        CS1594 = 1594,
        /// <summary>
        /// Semicolon after method or accessor block is not valid
        /// </summary>
        CS1597 = 1597,
        /// <summary>
        /// Method or delegate cannot return type {0}
        /// </summary>
        CS1599 = 1599,
        /// <summary>
        /// Compilation cancelled by user
        /// </summary>
        CS1600 = 1600,
        /// <summary>
        /// Method or delegate parameter cannot be of type {0}
        /// </summary>
        CS1601 = 1601,
        /// <summary>
        /// Cannot assign to {0} because it is read-only
        /// </summary>
        CS1604 = 1604,
        /// <summary>
        /// Cannot pass {0} as a ref or out argument because it is read-only
        /// </summary>
        CS1605 = 1605,
        /// <summary>
        /// Assembly signing failed; output may not be signed -- {0}
        /// </summary>
        CS1606 = 1606,
        /// <summary>
        /// The Required attribute is not permitted on C# types
        /// </summary>
        CS1608 = 1608,
        /// <summary>
        /// Modifiers cannot be placed on event accessor declarations
        /// </summary>
        CS1609 = 1609,
        /// <summary>
        /// The params parameter cannot be declared as ref or out
        /// </summary>
        CS1611 = 1611,
        /// <summary>
        /// Cannot modify the return value of {0} because it is not a variable
        /// </summary>
        CS1612 = 1612,
        /// <summary>
        /// The managed coclass wrapper class {0} for interface {1} cannot be found (are you missing an assembly reference?)
        /// </summary>
        CS1613 = 1613,
        /// <summary>
        /// {0} is ambiguous; between {1} and {2}. use either '@{0}' or '{0}Attribute'
        /// </summary>
        CS1614 = 1614,
        /// <summary>
        /// Argument {0} should not be passed with the {1} keyword
        /// </summary>
        CS1615 = 1615,
        /// <summary>
        /// Invalid option {0} for /langversion; must be ISO-1, ISO-2 or Default
        /// </summary>
        CS1617 = 1617,
        /// <summary>
        /// Cannot create delegate with {0} because it has a Conditional attribute
        /// </summary>
        CS1618 = 1618,
        /// <summary>
        /// Cannot create temporary file {0} -- {1}
        /// </summary>
        CS1619 = 1619,
        /// <summary>
        /// Argument {0} must be passed with the {1} keyword
        /// </summary>
        CS1620 = 1620,
        /// <summary>
        /// The yield statement cannot be used inside an anonymous method or lambda expression
        /// </summary>
        CS1621 = 1621,
        /// <summary>
        /// Cannot return a value from an iterator. Use the yield return statement to return a value, or yield break to end the iteration.
        /// </summary>
        CS1622 = 1622,
        /// <summary>
        /// Iterators cannot have ref or out parameters
        /// </summary>
        CS1623 = 1623,
        /// <summary>
        /// The body of {0} cannot be an iterator block because {1} is not an iterator interface type
        /// </summary>
        CS1624 = 1624,
        /// <summary>
        /// Cannot yield in the body of a finally clause
        /// </summary>
        CS1625 = 1625,
        /// <summary>
        /// Cannot yield a value in the body of a try block with a catch clause
        /// </summary>
        CS1626 = 1626,
        /// <summary>
        /// Expression expected after yield return
        /// </summary>
        CS1627 = 1627,
        /// <summary>
        /// Cannot use ref or out parameter {0} inside an anonymous method, lambda expression, or query expression
        /// </summary>
        CS1628 = 1628,
        /// <summary>
        /// Unsafe code may not appear in iterators
        /// </summary>
        CS1629 = 1629,
        /// <summary>
        /// Invalid option {0} for /errorreport; must be prompt, send, queue, or none
        /// </summary>
        CS1630 = 1630,
        /// <summary>
        /// Cannot yield a value in the body of a catch clause
        /// </summary>
        CS1631 = 1631,
        /// <summary>
        /// Control cannot leave the body of an anonymous method or lambda expression
        /// </summary>
        CS1632 = 1632,
        /// <summary>
        /// Iterators cannot have unsafe parameters or yield types
        /// </summary>
        CS1637 = 1637,
        /// <summary>
        /// {0} is a reserved identifier and cannot be used when ISO language version mode is used
        /// </summary>
        CS1638 = 1638,
        /// <summary>
        /// The managed coclass wrapper class signature {0} for interface {1} is not a valid class name signature
        /// </summary>
        CS1639 = 1639,
        /// <summary>
        /// foreach statement cannot operate on variables of type {0} because it implements multiple instantiations of {1}, try casting to a specific interface instantiation
        /// </summary>
        CS1640 = 1640,
        /// <summary>
        /// A fixed size buffer field must have the array size specifier after the field name
        /// </summary>
        CS1641 = 1641,
        /// <summary>
        /// Fixed size buffer fields may only be members of structs.
        /// </summary>
        CS1642 = 1642,
        /// <summary>
        /// Not all code paths return a value in method of type {0}
        /// </summary>
        CS1643 = 1643,
        /// <summary>
        /// Feature {0} is not part of the standardized ISO C# language specification, and may not be accepted by other compilers
        /// </summary>
        CS1644 = 1644,
        /// <summary>
        /// Keyword, identifier, or string expected after verbatim specifier: 
        /// </summary>
        CS1646 = 1646,
        /// <summary>
        /// An expression is too long or complex to compile near {0}
        /// </summary>
        CS1647 = 1647,
        /// <summary>
        /// Members of readonly field {0} cannot be modified (except in a constructor or a variable initializer)
        /// </summary>
        CS1648 = 1648,
        /// <summary>
        /// Members of readonly field {0} cannot be passed ref or out (except in a constructor)
        /// </summary>
        CS1649 = 1649,
        /// <summary>
        /// Fields of static readonly field {0} cannot be assigned to (except in a static constructor or a variable initializer)
        /// </summary>
        CS1650 = 1650,
        /// <summary>
        /// Fields of static readonly field {0} cannot be passed ref or out (except in a static constructor)
        /// </summary>
        CS1651 = 1651,
        /// <summary>
        /// Cannot modify members of {0} because it is a {1}
        /// </summary>
        CS1654 = 1654,
        /// <summary>
        /// Cannot pass fields of {0} as a ref or out argument because it is a {1}
        /// </summary>
        CS1655 = 1655,
        /// <summary>
        /// Cannot assign to {0} because it is a {1}
        /// </summary>
        CS1656 = 1656,
        /// <summary>
        /// Cannot pass {0} as a ref or out argument because {1}
        /// </summary>
        CS1657 = 1657,
        /// <summary>
        /// Cannot convert anonymous method block to type {0} because it is not a delegate type
        /// </summary>
        CS1660 = 1660,
        /// <summary>
        /// Cannot convert anonymous method block to delegate type {0} because the specified block's parameter types do not match the delegate parameter types
        /// </summary>
        CS1661 = 1661,
        /// <summary>
        /// Cannot convert anonymous method block to delegate type {0} because some of the return types in the block are not implicitly convertible to the delegate return type
        /// </summary>
        CS1662 = 1662,
        /// <summary>
        /// Fixed size buffer type must be one of the following: bool, byte, short, int, long, char, sbyte, ushort, uint, ulong, float or double
        /// </summary>
        CS1663 = 1663,
        /// <summary>
        /// Fixed size buffer of length {0} and type {1} is too big
        /// </summary>
        CS1664 = 1664,
        /// <summary>
        /// Fixed size buffers must have a length greater than zero
        /// </summary>
        CS1665 = 1665,
        /// <summary>
        /// You cannot use fixed size buffers contained in unfixed expressions. Try using the fixed statement.
        /// </summary>
        CS1666 = 1666,
        /// <summary>
        /// Attribute {0} is not valid on property or event accessors. It is valid on {1} declarations only.
        /// </summary>
        CS1667 = 1667,
        /// <summary>
        /// params is not valid in this context
        /// </summary>
        CS1670 = 1670,
        /// <summary>
        /// A namespace declaration cannot have modifiers or attributes
        /// </summary>
        CS1671 = 1671,
        /// <summary>
        /// Invalid option {0} for /platform; must be anycpu, x86, Itanium or x64
        /// </summary>
        CS1672 = 1672,
        /// <summary>
        /// Anonymous methods, lambda expressions, and query expressions inside structs cannot access instance members of 'this'. Consider copying 'this' to a local variable outside the anonymous method, lambda expression or query expression and using the local instead.
        /// </summary>
        CS1673 = 1673,
        /// <summary>
        /// {0}: type used in a using statement must be implicitly convertible to 'System.IDisposable'
        /// </summary>
        CS1674 = 1674,
        /// <summary>
        /// Enums cannot have type parameters
        /// </summary>
        CS1675 = 1675,
        /// <summary>
        /// Parameter {0} must be declared with the {1} keyword
        /// </summary>
        CS1676 = 1676,
        /// <summary>
        /// Parameter {0} should not be declared with the {1} keyword
        /// </summary>
        CS1677 = 1677,
        /// <summary>
        /// Parameter {0} is declared as type {1} but should be {2}
        /// </summary>
        CS1678 = 1678,
        /// <summary>
        /// Invalid extern alias for {0}; {1} is not a valid identifier
        /// </summary>
        CS1679 = 1679,
        /// <summary>
        /// Invalid reference alias option: 'alias=' -- missing filename.
        /// </summary>
        CS1680 = 1680,
        /// <summary>
        /// You cannot redefine the global extern alias
        /// </summary>
        CS1681 = 1681,
        /// <summary>
        /// Local {0} or its members cannot have their address taken and be used inside an anonymous method or lambda expression
        /// </summary>
        CS1686 = 1686,
        /// <summary>
        /// Cannot convert anonymous method block without a parameter list to delegate type {0} because it has one or more out parameters
        /// </summary>
        CS1688 = 1688,
        /// <summary>
        /// Attribute {0} is only valid on methods or attribute classes
        /// </summary>
        CS1689 = 1689,
        /// <summary>
        /// An assembly with the same simple name {0} has already been imported. Try removing one of the references or sign them to enable side-by-side.
        /// </summary>
        CS1703 = 1703,
        /// <summary>
        /// An assembly with the same simple name {0} has already been imported. Try removing one of the references or sign them to enable side-by-side.
        /// </summary>
        CS1704 = 1704,
        /// <summary>
        /// Assembly {0} uses {1} which has a higher version than referenced assembly {2}
        /// </summary>
        CS1705 = 1705,
        /// <summary>
        /// Expression cannot contain anonymous methods or lambda expressions
        /// </summary>
        CS1706 = 1706,
        /// <summary>
        /// Fixed size buffers can only be accessed through locals or fields
        /// </summary>
        CS1708 = 1708,
        /// <summary>
        /// Unexpected error building metadata name for type {0} — {1}
        /// </summary>
        CS1713 = 1713,
        /// <summary>
        /// The base class or interface of {0} could not be resolved or is invalid
        /// </summary>
        CS1714 = 1714,
        /// <summary>
        /// {0}: type must be {1} to match overridden member {2}
        /// </summary>
        CS1715 = 1715,
        /// <summary>
        /// Do not use 'System.Runtime.CompilerServices.FixedBuffer' attribute. Use the 'fixed' field modifier instead.
        /// </summary>
        CS1716 = 1716,
        /// <summary>
        /// Error reading Win32 resource file {0} -- {1}
        /// </summary>
        CS1719 = 1719,
        /// <summary>
        /// Class {0} cannot have multiple base classes: {1} and {2}
        /// </summary>
        CS1721 = 1721,
        /// <summary>
        /// Base class {0} must come before any interfaces
        /// </summary>
        CS1722 = 1722,
        /// <summary>
        /// Value specified for the argument to 'System.Runtime.InteropServices.DefaultCharSetAttribute' is not valid
        /// </summary>
        CS1724 = 1724,
        /// <summary>
        /// Friend assembly reference {0} is invalid. InternalsVisibleTo declarations cannot have a version, culture, public key token, or processor architecture specified.
        /// </summary>
        CS1725 = 1725,
        /// <summary>
        /// Friend assembly reference {0} is invalid. Strong-name signed assemblies must specify a public key in their InternalsVisibleTo declarations.
        /// </summary>
        CS1726 = 1726,
        /// <summary>
        /// Cannot send error report automatically without authorization. Please visit {0} to authorize sending error report.
        /// </summary>
        CS1727 = 1727,
        /// <summary>
        /// Cannot bind delegate to {0} because it is a member of {1}
        /// </summary>
        CS1728 = 1728,
        /// <summary>
        /// {0} does not contain a constructor that takes {1} arguments.
        /// </summary>
        CS1729 = 1729,
        /// <summary>
        /// Assembly and module attributes must precede all other elements defined in a file except using clauses and extern alias declarations.
        /// </summary>
        CS1730 = 1730,
        /// <summary>
        /// Cannot convert {0} to delegate because some of the return types in the block are not implicitly convertible to the delegate return type.
        /// </summary>
        CS1731 = 1731,
        /// <summary>
        /// Expected parameter.
        /// </summary>
        CS1732 = 1732,
        /// <summary>
        /// Expected expression.
        /// </summary>
        CS1733 = 1733,
        /// <summary>
        /// Warning level must be in the range 0-4
        /// </summary>
        CS1900 = 1900,
        /// <summary>
        /// Invalid option {0} for /debug; must be full or pdbonly
        /// </summary>
        CS1902 = 1902,
        /// <summary>
        /// Invalid option {0}; Resource visibility must be either 'public' or 'private'
        /// </summary>
        CS1906 = 1906,
        /// <summary>
        /// The type of the argument to the DefaultValue attribute must match the parameter type
        /// </summary>
        CS1908 = 1908,
        /// <summary>
        /// The DefaultValue attribute is not applicable on parameters of type {0}
        /// </summary>
        CS1909 = 1909,
        /// <summary>
        /// Argument of type {0} is not applicable for the DefaultValue attribute
        /// </summary>
        CS1910 = 1910,
        /// <summary>
        /// Duplicate initialization of member {0}
        /// </summary>
        CS1912 = 1912,
        /// <summary>
        /// Member {0} cannot be initialized. It is not a field or property.
        /// </summary>
        CS1913 = 1913,
        /// <summary>
        /// Static field {0} cannot be assigned in an object initializer
        /// </summary>
        CS1914 = 1914,
        /// <summary>
        /// Members of read-only field {0} of type {1} cannot be assigned with an object initializer because it is of a value type.
        /// </summary>
        CS1917 = 1917,
        /// <summary>
        /// Members of property {0} of type {1} cannot be assigned with an object initializer because it is of a value type.
        /// </summary>
        CS1918 = 1918,
        /// <summary>
        /// Unsafe type {0} cannot be used in object creation.
        /// </summary>
        CS1919 = 1919,
        /// <summary>
        /// Element initializer cannot be empty.
        /// </summary>
        CS1920 = 1920,
        /// <summary>
        /// The best overloaded method match for {0} has wrong signature for the initializer element. The initializable Add must be an accessible instance method.
        /// </summary>
        CS1921 = 1921,
        /// <summary>
        /// Collection initializer requires its type {0} to implement System.Collections.IEnumerable.
        /// </summary>
        CS1922 = 1922,
        /// <summary>
        /// Cannot initialize object of type {0} with a collection initializer.
        /// </summary>
        CS1925 = 1925,
        /// <summary>
        /// Error reading Win32 manifest file {0} -- {1}.
        /// </summary>
        CS1926 = 1926,
        /// <summary>
        /// {0} does not contain a definition for {1} and the best extension method overload {2} has some invalid arguments.
        /// </summary>
        CS1928 = 1928,
        /// <summary>
        /// Instance argument: cannot convert from {0} to {1}.
        /// </summary>
        CS1929 = 1929,
        /// <summary>
        /// The range variable {0} has already been declared
        /// </summary>
        CS1930 = 1930,
        /// <summary>
        /// The range variable {0} conflicts with a previous declaration of {1}.
        /// </summary>
        CS1931 = 1931,
        /// <summary>
        /// Cannot assign {0} to a range variable.
        /// </summary>
        CS1932 = 1932,
        /// <summary>
        /// Expression cannot contain query expressions
        /// </summary>
        CS1933 = 1933,
        /// <summary>
        /// Could not find an implementation of the query pattern for source type {0}. {1} not found. Consider explicitly specifying the type of the range variable {2}.
        /// </summary>
        CS1934 = 1934,
        /// <summary>
        /// Could not find an implementation of the query pattern for source type {0}. {1} not found. Are you missing a reference to 'System.Core.dll' or a using directive for 'System.Linq'?
        /// </summary>
        CS1935 = 1935,
        /// <summary>
        /// Could not find an implementation of the query pattern for source type {0}. {1} not found.
        /// </summary>
        CS1936 = 1936,
        /// <summary>
        /// The name {0} is not in scope on the left side of 'equals'. Consider swapping the expressions on either side of 'equals'.
        /// </summary>
        CS1937 = 1937,
        /// <summary>
        /// The name {0} is not in scope on the right side of 'equals'. Consider swapping the expressions on either side of 'equals'.
        /// </summary>
        CS1938 = 1938,
        /// <summary>
        /// Cannot pass the range variable {0} as an out or ref parameter.
        /// </summary>
        CS1939 = 1939,
        /// <summary>
        /// Multiple implementations of the query pattern were found for source type {0}. Ambiguous call to {1}.
        /// </summary>
        CS1940 = 1940,
        /// <summary>
        /// The type of one of the expressions in the {0} clause is incorrect. Type inference failed in the call to {1}.
        /// </summary>
        CS1941 = 1941,
        /// <summary>
        /// The type of the expression in the {0} clause is incorrect. Type inference failed in the call to {1}.
        /// </summary>
        CS1942 = 1942,
        /// <summary>
        /// An expression of type {0} is not allowed in a subsequent from clause in a query expression with source type {1}. Type inference failed in the call to {2}.
        /// </summary>
        CS1943 = 1943,
        /// <summary>
        /// An expression tree may not contain an unsafe pointer operation
        /// </summary>
        CS1944 = 1944,
        /// <summary>
        /// An expression tree may not contain an anonymous method expression.
        /// </summary>
        CS1945 = 1945,
        /// <summary>
        /// An anonymous method expression cannot be converted to an expression tree.
        /// </summary>
        CS1946 = 1946,
        /// <summary>
        /// Range variable {0} cannot be assigned to -- it is read only.
        /// </summary>
        CS1947 = 1947,
        /// <summary>
        /// The range variable {0} cannot have the same name as a method type parameter
        /// </summary>
        CS1948 = 1948,
        /// <summary>
        /// The contextual keyword 'var' cannot be used in a range variable declaration.
        /// </summary>
        CS1949 = 1949,
        /// <summary>
        /// The best overloaded Add method {0} for the collection initializer has some invalid arguments.
        /// </summary>
        CS1950 = 1950,
        /// <summary>
        /// An expression tree lambda may not contain an out or ref parameter.
        /// </summary>
        CS1951 = 1951,
        /// <summary>
        /// An expression tree lambda may not contain a method with variable arguments
        /// </summary>
        CS1952 = 1952,
        /// <summary>
        /// An expression tree lambda may not contain a method group.
        /// </summary>
        CS1953 = 1953,
        /// <summary>
        /// The best overloaded method match {0} for the collection initializer element cannot be used. Collection initializer 'Add' methods cannot have ref or out parameters.
        /// </summary>
        CS1954 = 1954,
        /// <summary>
        /// Non-invocable member {0} cannot be used like a method.
        /// </summary>
        CS1955 = 1955,
        /// <summary>
        /// Object and collection initializer expressions may not be applied to a delegate creation expression,
        /// </summary>
        CS1958 = 1958,
        /// <summary>
        /// {0} is of type {1}. The type specified in a constant declaration must be sbyte, byte, short, ushort, int, uint, long, ulong, char, float, double, decimal, bool, string, an enum-type, or a reference-type.
        /// </summary>
        CS1959 = 1959,
        /// <summary>
        /// Source file {0} could not be found
        /// </summary>
        CS2001 = 2001,
        /// <summary>
        /// Response file {0} included multiple times
        /// </summary>
        CS2003 = 2003,
        /// <summary>
        /// Missing file specification for {0} option
        /// </summary>
        CS2005 = 2005,
        /// <summary>
        /// Command-line syntax error: Missing {0} for {1} option
        /// </summary>
        CS2006 = 2006,
        /// <summary>
        /// Unrecognized command-line option: {0}
        /// </summary>
        CS2007 = 2007,
        /// <summary>
        /// No inputs specified
        /// </summary>
        CS2008 = 2008,
        /// <summary>
        /// Unable to open response file {0}
        /// </summary>
        CS2011 = 2011,
        /// <summary>
        /// Cannot open {0} for writing
        /// </summary>
        CS2012 = 2012,
        /// <summary>
        /// Invalid image base number {0}
        /// </summary>
        CS2013 = 2013,
        /// <summary>
        /// {0} is a binary file instead of a text file
        /// </summary>
        CS2015 = 2015,
        /// <summary>
        /// Code page {0} is invalid or not installed
        /// </summary>
        CS2016 = 2016,
        /// <summary>
        /// Cannot specify /main if building a module or library
        /// </summary>
        CS2017 = 2017,
        /// <summary>
        /// Unable to find messages file 'cscmsgs.dll'
        /// </summary>
        CS2018 = 2018,
        /// <summary>
        /// Invalid target type for /target: must specify 'exe', 'winexe', 'library', or 'module'
        /// </summary>
        CS2019 = 2019,
        /// <summary>
        /// Only the first set of input files can build a target other than 'module'
        /// </summary>
        CS2020 = 2020,
        /// <summary>
        /// File name {0} is too long or invalid
        /// </summary>
        CS2021 = 2021,
        /// <summary>
        /// Options '/out' and '/target' must appear before source file names
        /// </summary>
        CS2022 = 2022,
        /// <summary>
        /// Invalid file section alignment number '#'
        /// </summary>
        CS2024 = 2024,
        /// <summary>
        /// Character {0} is not allowed on the command-line or in response files
        /// </summary>
        CS2032 = 2032,
        /// <summary>
        /// Cannot create short filename {0} when a long filename with the same short filename already exists
        /// </summary>
        CS2033 = 2033,
        /// <summary>
        /// A /reference option that declares an extern alias can only have one filename. To specify multiple aliases or filenames, use multiple /reference options.
        /// </summary>
        CS2034 = 2034,
        /// <summary>
        /// >Command-line syntax error: Missing ':&lt;number&gt;' for {0} option
        /// </summary>
        CS2035 = 2035,
        /// <summary>
        /// The /pdb option requires that the /debug option also be used.
        /// </summary>
        CS2036 = 2036,
        /// <summary>
        /// Program {0} does not contain a static 'Main' method suitable for an entry point
        /// </summary>
        CS5001 = 5001,
    }
}
