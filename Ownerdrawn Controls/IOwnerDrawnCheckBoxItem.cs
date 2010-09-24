using System;
using System.Collections.Generic;
using System.Text;
 /*----------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.     |
 |-----------------------------------------|
 | The Abstraction Project's code is prov- |
 | -ided under a contract-release basis.   |
 | DO NOT DISTRIBUTE and do not use beyond |
 | the contract terms.                     |
 \--------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Defines generic peroperties and methods for working with an 
    /// owner drawn item which can be checked or toggled with an active
    /// client area to alter the checked status of the item.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnCheckBoxItem{TDrawnItem, TParent}"/>
    /// which can be checked.</typeparam>
    /// <typeparam name="TParent">The <see cref="IOwnerDrawn{TDrawnItem, TParent}"/>
    /// that can have the checkable items.</typeparam>
    public interface IOwnerDrawnCheckBoxItem<TDrawnItem, TParent> : 
        IOwnerDrawnCheckableItem<TDrawnItem, TParent>,
        IOwnerDrawnCheckBoxItem<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnCheckBoxItem<TDrawnItem, TParent>
        where TParent :
            IOwnerDrawn<TDrawnItem, TParent>
    {
    }
    /// <summary>
    /// Defines generic properties and methods for working with an
    /// owner drawn item which can be checked or toggled with an active
    /// client area to alter the checked status of the item.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnItem{TDrawnItem}"/>
    /// which can be checked.</typeparam>
    public interface IOwnerDrawnCheckBoxItem<TDrawnItem> : 
        IOwnerDrawnCheckableItem<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// Returns whether the check area of the <see cref="IOwnerDrawnCheckBoxItem{TDrawnItem}"/>
        /// has the mouse over it.
        /// </summary>
        bool CheckAreaHovered { get; }
    }
}
