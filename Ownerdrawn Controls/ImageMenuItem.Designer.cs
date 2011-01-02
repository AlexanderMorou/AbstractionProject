using System;
using System.Collections.Generic;
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
    partial class ImageMenuItem
    {
        /// <summary>
        /// Disposes the resources (other than non-managed resources) used by the <see cref="ImageMenuItem"/>.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.mdiHandler != null)
                    this.mdiHandler.Dispose();
                this.mdiHandler = null;
            }
            base.Dispose(disposing);
        }
        #region Windows Control Designer generated code
        private void InitializeComponent()
        {
            // 
            // ImageMenuItem
            // 
            this.OwnerDraw = true;
        }
        #endregion
    }
}
