using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Provides event arguments for an imaged checked list box
    /// when the checked state of an item changes.
    /// </summary>
    public class ImageCheckedListBoxItemCheckChangedEventArgs :
        EventArgs
    {

        /// <summary>
        /// Creates a new <see cref="ImageCheckedListBoxItemCheckChangedEventArgs"/> 
        /// with the <paramref name="item"/> provided.
        /// </summary>
        /// <param name="item">The <see cref="ImageCheckedListBox.ImageObjectItem"/>
        /// item whose checked state changed.</param>
        public ImageCheckedListBoxItemCheckChangedEventArgs(ImageCheckedListBox.ImageObjectItem item)
        {
            this.Item = item;
        }

        /// <summary>
        /// Returns the <see cref="ImageCheckedListBox.ImageObjectItem"/> whose 
        /// checked state changed.
        /// </summary>
        public ImageCheckedListBox.ImageObjectItem Item {get;private set;}
    }
}
