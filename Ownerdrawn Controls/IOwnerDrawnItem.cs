using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
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
    /// Defines generic properties and methods for working with an owner drawn item.
    /// </summary>
    /// <typeparam name="TParent">The parent type</typeparam>
    /// <typeparam name="TDrawnItem">The drawn item represented by the ownerdrawn system.</typeparam>
    public interface IOwnerDrawnItem<TDrawnItem, TParent> : IOwnerDrawnItem<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem, TParent>
        where TParent :
            IOwnerDrawn<TDrawnItem, TParent>
    {
        /// <summary>
        /// Returns the <typeparamref name="TParent"/>
        /// which contains the <see cref="IOwnerDrawnItem{TDrawnItem, TParent}"/>.
        /// </summary>
        new TParent Parent { get; }
    }

    /// <summary>
    /// Defines generic properties and methods for working with a specific kind
    /// of owner drawn item.
    /// </summary>
    /// <typeparam name="TDrawnItem">The drawn item represented by the ownerdrawn system.</typeparam>
    public interface IOwnerDrawnItem<TDrawnItem> :
        IOwnerDrawnItem
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// Returns the <see cref="Int32"/> value associated to the
        /// <see cref="IOwnerDrawnItem{TDrawnItem}"/> which designates
        /// the ordinal index of the item relative to its siblings within
        /// <see cref="IOwnerDrawnItem.Parent"/>.
        /// </summary>
        int Index { get; }
    }

    /// <summary>
    /// Defines properties and methods for working with an owner drawn item.
    /// </summary>
    public interface IOwnerDrawnItem
    {
        /// <summary>
        /// Returns the <see cref="Bitmap"/> relative to the default 
        /// enabled state of the  <see cref="IOwnerDrawnItem"/>.
        /// </summary>
        Bitmap Image { get; }
        /// <summary>
        /// Returns the <see cref="Bitmap"/> relative to the 
        /// disabled image of the <see cref="IOwnerDrawnItem"/>.
        /// </summary>
        Bitmap DisabledImage { get; }
        /// <summary>
        /// Returns the <see cref="Bitmap"/> relative to the
        /// shadowed state of the <see cref="IOwnerDrawnItem"/> which
        /// is displayed underneath the <see cref="Image"/>.
        /// </summary>
        Bitmap ShadowImage { get; }
        /// <summary>
        /// Returns the <see cref="Object"/> relative to the 
        /// item that the <see cref="IOwnerDrawnItem"/> exists
        /// to represent.
        /// </summary>
        object Item { get; }
        /// <summary>
        /// Returns the <see cref="String"/> value representing the
        /// text to display on the <see cref="IOwnerDrawnItem"/>.
        /// </summary>
        string Text { get; }
        /// <summary>
        /// Returns the <see cref="Object"/> which contains the 
        /// <see cref="IOwnerDrawnItem"/>.
        /// </summary>
        object Parent { get; }
    }

}
