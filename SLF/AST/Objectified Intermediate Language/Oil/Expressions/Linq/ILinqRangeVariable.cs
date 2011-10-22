using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions.Linq
{
    /// <summary>
    /// Defines properties and methods for working with a range
    /// variable within a language integrated query.
    /// </summary>
    public interface ILinqRangeVariable :
        IIntermediateMember<IGeneralMemberUniqueIdentifier, ILinqClause, ILinqClause>,
        IIntermediateMember
    {
        /// <summary>
        /// Returns the <see cref="ILinqClause"/> which defined the
        /// <see cref="ILinqRangeVariable"/>.
        /// </summary>
        new ILinqClause Parent { get; }
        /// <summary>
        /// Obtains a <see cref="ILinqRangeVariableReference"/> which
        /// refers back to the <see cref="ILinqRangeVariable"/>
        /// as an expression.
        /// </summary>
        /// <returns>A <see cref="ILinqRangeVariableReference"/>
        /// associated to the current <see cref="ILinqRangeVariable"/>.</returns>
        ILinqRangeVariableReference GetReference();
    }
}
