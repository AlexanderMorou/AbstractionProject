using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllenCopeland.Abstraction.Slf.Oil.Expressions
{
    /// <summary>
    /// Defines properties and methods for working with a reference which refers to an event.
    /// </summary>
    public interface IEventReferenceExpression :
        IExpression
    {
        /// <summary>
        /// Returns the <see cref="Name"/> 
        /// of the expression the <see cref="IEventReferenceExpression"/>
        /// points to.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the <see cref="IMemberParentReferenceExpression"/> 
        /// which contains the source information to accessing
        /// the event.
        /// </summary>
        IMemberParentReferenceExpression Source { get; }
    }
}
