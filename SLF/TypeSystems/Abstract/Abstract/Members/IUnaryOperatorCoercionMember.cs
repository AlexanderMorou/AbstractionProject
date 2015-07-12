using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Abstract.Members
{
    /// <summary>
    /// The operation coerced by the 
    /// <see cref="IUnaryOperatorCoercionMember"/>.
    /// </summary>
    public enum CoercibleUnaryOperators :
        byte
    {
        /// <summary>
        /// Plus unary operator, often '+'.
        /// </summary>
        /// <remarks>CLI method: op_UnaryPlus</remarks>
        Plus,
        /// <summary>
        /// Negation unary operator, often '-'.
        /// </summary>
        /// <remarks>CLI method: op_UnaryNegation</remarks>
        Negation,
        /// <summary>
        /// 'Evaluates to false' unary operator, often 
        /// 'false' or 'IsFalse'.
        /// </summary>
        /// <remarks>CLI method: op_False</remarks>
        EvaluatesToFalse,
        /// <summary>
        /// 'Evaluates to true' unary operator, often 
        /// 'true' or 'IsTrue'.
        /// </summary>
        /// <remarks>CLI method: op_True</remarks>
        EvaluatesToTrue,
        /// <summary>
        /// Logical inversion unary operator, often 
        /// '!' or 'Not'.
        /// </summary>
        /// <remarks>CLI method: op_LogicalNot</remarks>
        LogicalInvert,
        /// <summary>
        /// Ones complement operator, often '~' 
        /// or 'Not'.
        /// </summary>
        /// <remarks>CLI Method: op_OnesComplement</remarks>
        Complement,
        /// <summary>
        /// Increment unary operator, often '++'.
        /// </summary>
        /// <remarks>CLI method: op_Increment</remarks>
        Increment,
        /// <summary>
        /// Decrement unary operator, often '--'.
        /// </summary>
        /// <remarks>CLI method: op_Decrement</remarks>
        Decrement,
    }

    /// <summary>
    /// Defines properties and methods for a member which 
    /// coerces the interpretation of the containing type 
    /// with regards to unary operator expressions.
    /// </summary>
    public interface IUnaryOperatorCoercionMember :
        ICoercionMember
    { 
        /// <summary>
        /// The operator coerced.
        /// </summary>
        CoercibleUnaryOperators Operator { get; }

        /// <summary>
        /// Returns the <see cref="IType"/> which results from the
        /// unary operation.
        /// </summary>
        IType ResultedType { get; }
    }
}
