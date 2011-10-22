using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Expressions;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf.Languages.CSharp.Expressions
{
    public enum CSharpRelationalOperation
    {
        /// <summary>
        /// Represents an operation where left side of the <see cref="ICSharpRelationalExpression"/>
        /// is checked on whether it is less than the right side.
        /// </summary>
        LessThan,
        /// <summary>
        /// Represents an operation where left side of the <see cref="ICSharpRelationalExpression"/>
        /// is checked on whether it is less than or equal to the right side.
        /// </summary>
        LessThanOrEqualTo,
        /// <summary>
        /// Represents an operation where left side of the <see cref="ICSharpRelationalExpression"/>
        /// is checked on whether it is greater than the right side.
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Represents an operation where left side of the <see cref="ICSharpRelationalExpression"/>
        /// is checked on whether it is greater than or equal to the right side.
        /// </summary>
        GreaterThanOrEqualTo,
        /// <summary>
        /// Represents an operation where the left side of the <see cref="ICSharpRelationalExpression"/>
        /// is type-checked against another type; true is returned if it is the type
        /// provided; false, otherwise.
        /// </summary>
        TypeCheck,
        /// <summary>
        /// The <see cref="ICSharpRelationalExpression"/> represents a 
        /// type-cast or null expression.
        /// </summary>
        /// <remarks>
        ///     C&#9839;: Term "as" TypeIdentifier
        ///     VB: "Dim" Temp "As" TermType '=' Term
        ///         ... IIf(Temp TypeOf Type, Temp, Null) ...
        /// </remarks>
        TypeCastOrNull,
        /// <summary>
        /// The relaitonal operation is a non-binary operation and is therefore a term containing
        /// only the <see cref="IBinaryOperationExpression{TLeft, TRight}.RightSide"/>.
        /// </summary>
        Term,
    }
    /// <summary>
    /// Defines properties and methods for working with a relational check operation.
    /// </summary>
    public interface ICSharpRelationalExpression :
        IBinaryOperationExpression<ICSharpRelationalExpression, ICSharpShiftExpression>,
        ICSharpExpression
    {
        /// <summary>
        /// Returns the type of <see cref="CSharpRelationalOperation"/> the <see cref="ICSharpRelationalExpression"/>
        /// is.
        /// </summary>
        CSharpRelationalOperation Operation { get; set; }
    }
}
