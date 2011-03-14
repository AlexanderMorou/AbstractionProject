using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;
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
    /// Defiens properties and methods for working with an owner drawn
    /// menu item.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnItem{TDrawnItem}"/>
    /// that is represented by the menu.</typeparam>
    public interface IOwnerDrawnMenuItem<TDrawnItem> : 
        IOwnerDrawnCheckableItem<TDrawnItem>,
        IOwnerDrawnMenu<TDrawnItem>,
        IOwnerDrawnStatedItem
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// Returns whether the <see cref="IOwnerDrawnMenuItem{TDrawnItem}"/> is
        /// a top-level menu item.
        /// </summary>
        bool TopMost { get; }
        /// <summary>
        /// Returns the <see cref="IOwnerDrawnMenu{TDrawnItem}"/> in which the
        /// <see cref="IOwnerDrawnMenuItem{TDrawnItem}"/> is contained.
        /// </summary>
        new IOwnerDrawnMenu<TDrawnItem> Parent { get; }
        /// <summary>
        /// Returns/sets the <see cref="Shortcut"/> which activates
        /// the commands associated to the <see cref="IOwnerDrawnMenuItem{TDrawnItem}"/>
        /// and appears next to the item text when drawn.
        /// </summary>
        Shortcut Shortcut { get; set; }
    }
    /// <summary>
    /// Defines generic properties and methods for working with an
    /// owner drawn menu that contains <see cref="IOwnerDrawnMenuItem{TDrawnItem}"/> 
    /// instances.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawnItem{TDrawnItem}"/>
    /// that is represented by the menu.</typeparam>
    public interface IOwnerDrawnMenu<TDrawnItem> : IOwnerDrawn<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// Returns the <see cref="Menu.MenuItemCollection"/> associated to the
        /// <see cref="IOwnerDrawnMenu{TDrawnItem}"/>.
        /// </summary>
        Menu.MenuItemCollection MenuItems { get; }
        /// <summary>
        /// Returns the <see cref="ImageMenuItemCollection"/> associated to the
        /// <see cref="IOwnerDrawnMenu{TDrawnItem}"/>
        /// </summary>
        ImageMenuItemCollection Items { get; }
        /// <summary>
        /// Returns the <see cref="ImageList"/> associated to the
        /// <see cref="IOwnerDrawnMenu{TDrawnItem}"/>.
        /// </summary>
        ImageList ImageList { get; }
    }
}
