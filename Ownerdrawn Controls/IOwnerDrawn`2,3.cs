using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
 /*----------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.     |
 |-----------------------------------------|
 | The Abstraction Project's code is prov- |
 | -ided under a contract-release basis.   |
 | DO NOT DISTRIBUTE and do not use beyond |
 | the contract terms.                     |
 \--------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Defines generic properties and methods for working with an item
    /// which is owner drawn.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnItem{TDrawnItem, TParent}"/>
    /// represented by the current implementation.</typeparam>
    /// <typeparam name="TParent">The type of <see cref="IOwnerDrawn{TDrawnItem, TParent}"/>
    /// which contains the <typeparamref name="TDrawnItem"/>
    /// elements.</typeparam>
    public interface IOwnerDrawn<TDrawnItem, TParent> :
        IOwnerDrawn<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem, TParent>
        where TParent :
            IOwnerDrawn<TDrawnItem, TParent>
    {

    }
    /// <summary>
    /// Defines generic properties and methods for working with an item
    /// which is owner drawn.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnItem{TDrawnItem}"/>
    /// represented by the current implementation.</typeparam>
    public interface IOwnerDrawn<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// Returns the <see cref="IOwnerDrawnStyle{TDrawnItem}"/> which handles
        /// metric calculations/drawing operations.
        /// </summary>
        IOwnerDrawnStyle<TDrawnItem> Style { get; }
    }
}
