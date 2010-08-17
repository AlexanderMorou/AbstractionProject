using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
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
    /// Enumeration that determines the origin of Handler's style instance.
    /// </summary>
    public enum OwnerDrawnStyleSource
    {
        /// <summary>
        /// The Manager determines the source of the style.
        /// </summary>
        ManagerSource,
        /// <summary>
        /// Uses a simpler rendering approach.
        /// </summary>
        SimpleSource,
        /// <summary>
        /// Default handler, uses gradients as a style standard.
        /// </summary>
        GradatedSource,
        /// <summary>
        /// The source is custom, don't set this directly, this is set 
        /// internally by altering the Handler's Style property.
        /// </summary>
        [Browsable(false)]
        CustomSource
    }
    /// <summary>
    /// Provides a static class for assisting owner drawn styling.
    /// </summary>
    /// <typeparam name="TDrawnItem">The type of <see cref="IOwnerDrawn{TDrawnItem}"/></typeparam>
    public static class OwnerDrawnManager<TDrawnItem>
        where TDrawnItem :
            IOwnerDrawnItem<TDrawnItem>
    {
        /// <summary>
        /// The SimpleStyle instance.
        /// </summary>
        private static OwnerDrawnSimpleStyle<TDrawnItem> styleSimpleDefault = null;
        /// <summary>
        /// The GradatedStyle instance.
        /// </summary>
        private static OwnerDrawnGradatedStyle<TDrawnItem> styleGradatedDefault = null;
        /// <summary>
        /// The custom style HandlerManager reference.
        /// </summary>
        private static OwnerDrawnStyle<TDrawnItem> customStyle;
        /// <summary>
        /// The default style
        /// </summary>
        public static readonly OwnerDrawnStyle<TDrawnItem> DefaultStyle = GradatedStyle;
        /// <summary>
        /// The default style source.
        /// </summary>
        public static readonly OwnerDrawnStyleSource DefaultSource = OwnerDrawnStyleSource.GradatedSource;
        /// <summary>
        /// The present source of the style.
        /// </summary>
        private static OwnerDrawnStyleSource source = DefaultSource;
        internal static OwnerDrawnSimpleStyle<TDrawnItem> SimpleStyle
        {
            [DebuggerHidden]
            get
            {
                if (styleSimpleDefault == null)
                    styleSimpleDefault = new OwnerDrawnSimpleStyle<TDrawnItem>();
                return styleSimpleDefault;
            }
        }
        /// <summary>
        /// The GradatedStyle property, accessed by instances of the 
        /// OwnerDrawnMenuHandler.
        /// </summary>
        /// <value>Accesses the value of the styleGradatedDefault data member.  Instanciated on first access</value>
        internal static OwnerDrawnGradatedStyle<TDrawnItem> GradatedStyle
        {
            [DebuggerHidden]
            get
            {
                //Determine if the Gradated style has been instanciated, 
                //if it hasn'TItem create it; finally, return.
                if (styleGradatedDefault == null)
                    styleGradatedDefault = new OwnerDrawnGradatedStyle<TDrawnItem>();
                return styleGradatedDefault;
            }
        }
        /// <summary>
        /// The Manager style used by OwnerDrawnMenuHandler instances who's Source
        /// is set to OwnerDrawnMenuStyleSource.ManagerSource
        /// </summary>
        public static OwnerDrawnStyle<TDrawnItem> Style
        {
            [DebuggerHidden]
            get
            {
                //Determine which style we're using, then return the appropriate 
                //one.
                //This doesn'TItem handle 'OwnerDrawnMenuStyleSource.ManagerSource',
                //since this is the Manager class.
                switch (source)
                {
                    case OwnerDrawnStyleSource.GradatedSource:
                        return GradatedStyle;
                    case OwnerDrawnStyleSource.SimpleSource:
                        return SimpleStyle;
                    case OwnerDrawnStyleSource.CustomSource:
                        return customStyle;
                }
                //If the value was set inappropriately then return null...
                return null;
            }
            set
            {
                /* *
                 * Check to see if the source isn't one of our internal
                 * styles, if it is, then set the Source accordingly,
                 * otherwise set it to OwnerDrawnMenuStyleSource.CustomSource
                 * */
                if (value == GradatedStyle)
                {
                    customStyle = null;
                    source = OwnerDrawnStyleSource.GradatedSource;
                }
                else if (value == SimpleStyle)
                {
                    customStyle = null;
                    source = OwnerDrawnStyleSource.SimpleSource;
                }
                else if (value != null)
                {
                    customStyle = value;
                    source = OwnerDrawnStyleSource.CustomSource;
                }
                else
                {
                    //Special case, if they provided a null value, then we
                    //Set it to the default source,
                    //it's necessary to nullify the custom style, so it can be
                    //GC'ed at a later time if need be.
                    source = DefaultSource;
                    customStyle = null;
                }
            }
        }
        /// <summary>
        /// The Source that the manager determines the style from,
        /// based upon this value the instance of Style can change.
        /// </summary>
        /// <exception cref="ArgumentException">If the style is OwnerDrawnMenuStyleSource.Custom or another
        /// unsupported value.</exception>
        /// <value>Accesses the static source data member.</value>
        public static OwnerDrawnStyleSource Source
        {
            [DebuggerHidden]
            get
            {
                return source;
            }
            set
            {
                switch (value)
                {
                    case OwnerDrawnStyleSource.SimpleSource:
                    case OwnerDrawnStyleSource.GradatedSource:
                        source = value;
                        break;
                    default:
                        //Unsupported style source.
                        throw new ArgumentException("value cannot be 'ManagerSource', 'CustomSource' or unknown");
                }
            }
        }
    }
}
