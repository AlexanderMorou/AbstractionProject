using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
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
    /// Defines generic peroperties and methods for working with an 
    /// owner drawn item which can be checked or toggled.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnCheckableItem{TDrawnItem, TParent}"/>
    /// which can be checked.</typeparam>
    /// <typeparam name="TParent">The <see cref="IOwnerDrawn{TDrawnItem, TParent}"/>
    /// that can have the checkable items.</typeparam>
    public interface IOwnerDrawnCheckableItem<TDrawnItem, TParent> :
        IOwnerDrawnItem<TDrawnItem, TParent>,
        IOwnerDrawnCheckableItem<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnCheckableItem<TDrawnItem, TParent>
        where TParent :
            IOwnerDrawn<TDrawnItem, TParent>
    {
    }

    /// <summary>
    /// Defines generic properties and methods for working with an
    /// owner drawn item which can be checked or toggled.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnItem{TDrawnItem}"/>
    /// which can be checked.</typeparam>
    public interface IOwnerDrawnCheckableItem<TDrawnItem> :
        IOwnerDrawnItem<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// Returns/sets whether the <see cref="IOwnerDrawnCheckableItem{TDrawnItem}"/>
        /// is checked.
        /// </summary>
        bool Checked { get; set; }
        /// <summary>
        /// Returns the <see cref="Bitmap"/> associated to the
        /// <see cref="IOwnerDrawnCheckableItem{TDrawnItem}"/> in its active state.
        /// </summary>
        Bitmap CheckedImage { get; }
        /// <summary>
        /// Returns the <see cref="Bitmap"/> associated to the
        /// <see cref="IOwnerDrawnCheckableItem{TDrawnItem}"/> in its deactivated
        /// state.
        /// </summary>
        Bitmap CheckedDisabledImage { get; }
        /// <summary>
        /// Returns the <see cref="Bitmap"/> associated to the
        /// <see cref="IOwnerDrawnCheckableItem{TDrawnItem}"/> 
        /// when the item has keyboard focus or is hovered, depending
        /// on the implementation.
        /// </summary>
        Bitmap CheckedShadowImage { get; }
    }
}
