using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
    /// The Default MDIChild Windows Dialog.
    /// </summary>
    public sealed partial class MDIChildWindowsDefaultDialog : 
        MDIChildWindowsDialog
    {
        /// <summary>
        /// Standard public .ctor.
        /// </summary>
        public MDIChildWindowsDefaultDialog() : base()
        {
            InitializeComponent();
        }
    }
}