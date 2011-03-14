using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    /// Provides a class which groups a series of <see cref="ToolBarButton"/>
    /// instances to construct a toggleable set from them.
    /// </summary>
    public class ToolbarButtonGroup : Component
    {
        private ToolBar toolBar;
        private GroupItemCollection items;

        /// <summary>
        /// Creates a new <see cref="ToolbarButtonGroup"/> with the
        /// <paramref name="components"/> provided.
        /// </summary>
        /// <param name="components">The <see cref="IContainer"/> which
        /// contains the <see cref="ToolbarButtonGroup"/>.</param>
        public ToolbarButtonGroup(IContainer components) : this()
        {
            components.Add(this);
        }

        /// <summary>
        /// Creates a new <see cref="ToolbarButtonGroup"/> 
        /// initialized to its default state.
        /// </summary>
        public ToolbarButtonGroup()
        {
            this.toolBar = null;
            this.items = new GroupItemCollection();
        }

        /// <summary>
        /// Returns the <see cref="GroupItemCollection"/> which contains the
        /// <see cref="GroupItem"/> elements which relate back to a specific 
        /// <see cref="ToolBarButton"/>.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GroupItemCollection GroupItems
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// Returns/sets the <see cref="ToolBar"/> on which the grouping should occur.
        /// </summary>
        public ToolBar ToolBar
        {
            get
            {
                return this.toolBar;
            }
            set
            {
                this.toolBar = value;
                if ((value != null) && (!(this.DesignMode)))
                    value.ButtonClick += new ToolBarButtonClickEventHandler(ToolBar_ButtonClick);
            }
        }

        void ToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == null)
                return;
            //Check to see whether the button even exists in this grouping.
            if (this.items.Where(k => k.Button == e.Button).Count() == 0)
                return;
            foreach (GroupItem gi in this.items)
            {
                if (((gi.Button != null && gi.Button != e.Button) && gi.Button.Pushed))
                    gi.Button.Pushed = false;
                else if (gi.Button == e.Button && !gi.Button.Pushed)
                    gi.Button.Pushed = true;
            }
        }
        /// <summary>
        /// The series of <see cref="GroupItem"/> instances
        /// contained by the <see cref="ToolbarButtonGroup"/>
        /// </summary>
        public class GroupItemCollection :
            Collection<GroupItem>
        {
            /// <summary>
            /// Initializes the <see cref="GroupItemCollection"/>
            /// to its default state.
            /// </summary>
            public GroupItemCollection()
            {
            }
        }
        /// <summary>
        /// Provides a group item implementation which references
        /// back to the original <see cref="ToolBarButton"/> that it
        /// wraps.
        /// </summary>
        [DefaultProperty("Button")]
        public class GroupItem
        {
            private ToolBarButton button;
            /// <summary>
            /// Creates a new <see cref="GroupItem"/> with the <paramref name="button"/>
            /// provided.
            /// </summary>
            /// <param name="button"></param>
            public GroupItem(ToolBarButton button) : this()
            {
                this.button = button;
            }
            /// <summary>
            /// Creates a new <see cref="GroupItem"/> initialized 
            /// to its default state.
            /// </summary>
            public GroupItem() { }
            /// <summary>
            /// Returns/sets the <see cref="ToolBarButton"/> associated to the
            /// <see cref="GroupItem"/>.
            /// </summary>
            public ToolBarButton Button
            {
                get
                {
                    return button;
                }
                set
                {
                    this.button = value;
                    if ((value != null) && (value.Style != ToolBarButtonStyle.ToggleButton))
                        value.Style = ToolBarButtonStyle.ToggleButton;
                }
            }
        }
    }
}
