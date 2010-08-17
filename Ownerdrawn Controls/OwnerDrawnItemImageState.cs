using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
 /*----------------------------------------\
 | Copyright © 2008 Allen Copeland Jr.     |
 |-----------------------------------------|
 | The Abstraction Project's code is prov- |
 | -ided under a contract-release basis.   |
 | DO NOT DISTRIBUTE and do not use beyond |
 | the contract terms.                     |
 \--------------------------------------- */

namespace AllenCopeland.Abstraction.OwnerDrawnControls
{
    /// <summary>
    /// Provides a class which represents the triple-state 
    /// of an image.
    /// </summary>
    public class OwnerDrawnItemImageState :
        IDisposable
    {
        /// <summary>
        /// Returns the <see cref="Bitmap"/> associated to the normal
        /// enabled state of the image.
        /// </summary>
        public Bitmap Normal { get; private set; }
        /// <summary>
        /// Returns the <see cref="Bitmap"/> associated to the 
        /// shadowed state of the image, underneath the <see cref="Normal"/>
        /// image when the item is hovered/selected.
        /// </summary>
        public Bitmap Shadowed { get; private set; }
        /// <summary>
        /// Returns the <see cref="Bitmap"/> associated to the disabled
        /// state of the image.
        /// </summary>
        public Bitmap Disabled { get; private set; }
        /// <summary>
        /// Creates a new <see cref="OwnerDrawnItemImageState"/> with the
        /// <paramref name="normal"/>, <paramref name="disabled"/> and
        /// <paramref name="shadowed"/> states of the image state.
        /// </summary>
        /// <param name="normal">The <see cref="Bitmap"/> associated to the normal
        /// enabled state of the image.</param>
        /// <param name="disabled">The <see cref="Bitmap"/> associated to the disabled
        /// state of the image.</param>
        /// <param name="shadowed">The <see cref="Bitmap"/> associated to the 
        /// shadowed state of the image, underneath the <see cref="Normal"/>
        /// image when the item is hovered/selected.</param>
        internal OwnerDrawnItemImageState(Bitmap normal, Bitmap disabled, Bitmap shadowed)
        {
            this.Normal = normal;
            this.Shadowed = shadowed;
            this.Disabled = disabled;
        }

        /// <summary>
        /// Creates a new <see cref="OwnerDrawnItemImageState"/> initialized to 
        /// its default state, with all images null.
        /// </summary>
        internal OwnerDrawnItemImageState() { }

        #region IDisposable Members

        /// <summary>
        /// Disposes the <see cref="OwnerDrawnItemImageState"/>.
        /// </summary>
        public void Dispose()
        {
            if (Normal != null)
                Normal.Dispose();
            if (Disabled != null)
                Disabled.Dispose();
            if (Shadowed != null)
                Shadowed.Dispose();
            this.Normal = null;
            this.Shadowed = null;
            this.Disabled = null;
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
