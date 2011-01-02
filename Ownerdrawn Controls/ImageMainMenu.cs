using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
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
    /// Provides a base implementation of a main menu which can contain
    /// menus with images.
    /// </summary>
    [ToolboxItemFilter("AllenCopeland.Abstraction.OwnerDrawnControls.ImageMainMenu")]
    [ToolboxItem(true)]
    public partial class ImageMainMenu : MainMenu,
        IOwnerDrawnMenu<ImageMenuItem>
    {
        private ImageMenuItemCollection menuItems;
        private ImageList imageList;
        private OwnerDrawnStyle<ImageMenuItem> style;
        private OwnerDrawnStyleSource source;
        private System.Drawing.Color looseTransparencyColor;
        /// <summary>
        /// Creates a new <see cref="ImageMainMenu"/> with the
        /// <paramref name="components"/> provided.
        /// </summary>
        /// <param name="components">The <see cref="IContainer"/>
        /// which contains the <see cref="ImageMainMenu"/>.</param>
        public ImageMainMenu(IContainer components)
        {
            InitializeComponent();
            this.menuItems = new ImageMenuItemCollection(this);
        }

        /// <summary>
        /// Creates a new <see cref="ImageMainMenu"/> initialized
        /// to its default state.
        /// </summary>
        public ImageMainMenu()
        {
            InitializeComponent();
            this.menuItems = new ImageMenuItemCollection(this);
        }
        #region IOwnerDrawn<object,ImageMenuItem> Members

        [Browsable(false)]
        IOwnerDrawnStyle<ImageMenuItem> IOwnerDrawn<ImageMenuItem>.Style
        {
            [DebuggerHidden]
            get
            {
                return this.Style;
            }
        }
        #endregion

        #region IOwnerDrawnMenu<object,ImageMenuItem> Members

        Menu.MenuItemCollection IOwnerDrawnMenu<ImageMenuItem>.MenuItems
        {
            get
            {
                return base.MenuItems;
            }
        }

        ImageList IOwnerDrawnMenu<ImageMenuItem>.ImageList
        {
            get
            {
                return this.ImageList;
            }
        }

        /// <summary>
        /// Returns the <see cref="ImageMenuItemCollection"/> which alters
        /// the imaged menu items of the <see cref="ImageMainMenu"/>.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ImageMenuItemCollection Items
        {
            get
            {
                return this.menuItems;
            }
        }
        /// <summary>
        /// Returns a <see cref="Menu.MenuItemCollection"/> associated to the original
        /// <see cref="MainMenu"/>'s items.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        new public MenuItemCollection MenuItems
        {
            get
            {
                return base.MenuItems;
            }
        }
        #endregion

        /// <summary>
        /// Returns/sets the <see cref="ImageList"/> associated to the
        /// <see cref="ImageContextMenu"/>.
        /// </summary>
        public virtual ImageList ImageList
        {
            [DebuggerHidden]
            get
            {
                return this.imageList;
            }
            set
            {
                this.imageList = value;
            }
        }
        /// <summary>
        /// The Style instance that's responsible for rendering the items.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OwnerDrawnStyle<ImageMenuItem> Style
        {
            [DebuggerHidden]
            get
            {
                switch (this.source)
                {
                    case OwnerDrawnStyleSource.GradatedSource:
                        return OwnerDrawnManager<ImageMenuItem>.GradatedStyle;
                    case OwnerDrawnStyleSource.SimpleSource:
                        return OwnerDrawnManager<ImageMenuItem>.SimpleStyle;
                    case OwnerDrawnStyleSource.ManagerSource:
                        return OwnerDrawnManager<ImageMenuItem>.Style;
                    case OwnerDrawnStyleSource.CustomSource:
                        return this.style;
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    if (value == OwnerDrawnManager<ImageMenuItem>.GradatedStyle)
                    {
                        this.style = null;
                        this.source = OwnerDrawnStyleSource.GradatedSource;
                    }
                    else if (value == OwnerDrawnManager<ImageMenuItem>.SimpleStyle)
                    {
                        this.style = null;
                        this.source = OwnerDrawnStyleSource.SimpleSource;
                    }
                    else if (value == OwnerDrawnManager<ImageMenuItem>.Style)
                    {
                        this.style = null;
                        this.source = OwnerDrawnStyleSource.ManagerSource;
                    }
                    else
                    {
                        this.style = value;
                        this.source = OwnerDrawnStyleSource.CustomSource;
                    }
                }
                else
                    throw new NullReferenceException("value cannot be null.");
            }
        }
        /// <summary>
        /// Returns/sets the <see cref="Color"/> assoicated to the <see cref="ImageMainMenu"/>
        /// which sets the transparent color for loose images for the <see cref="ImageMenuItem"/> 
        /// elements contained within.
        /// </summary>
        public Color LooseTransparencyColor
        {
            [DebuggerHidden]
            get
            {
                return this.looseTransparencyColor;
            }
            set
            {
                this.looseTransparencyColor = value;
            }
        }

        /// <summary>
        /// The place that the Style is sourced from.
        /// </summary>
        [Browsable(true)]
        [DefaultValue(OwnerDrawnStyleSource.ManagerSource)]
        public OwnerDrawnStyleSource StyleSource
        {
            [DebuggerHidden]
            get
            {
                return this.source;
            }
            set
            {
                switch (this.source)
                {
                    case OwnerDrawnStyleSource.GradatedSource:
                    case OwnerDrawnStyleSource.SimpleSource:
                    case OwnerDrawnStyleSource.ManagerSource:
                        this.source = value;
                        break;
                    case OwnerDrawnStyleSource.CustomSource:
                        throw new ArgumentException("Value cannot set directly to 'OwnerDrawnStyleSource.CustomSource', you must set the style reference to a custom derived OwnerDrawnStyle object instance.");
                }
            }
        }

    }
}
