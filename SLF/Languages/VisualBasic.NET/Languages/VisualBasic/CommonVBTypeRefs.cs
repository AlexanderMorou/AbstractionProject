using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.ApplicationServices;
using System.CodeDom.Compiler;

namespace AllenCopeland.Abstraction.Slf.Languages.VisualBasic
{
    public class CommonVBTypeRefs
    {
        #region Data Members

        /// <summary>
        /// Data member for <see cref="HideModuleNameAttribute"/>
        /// </summary>
        private static IClassType hideModuleNameAttribute;

        /// <summary>
        /// Data member for <see cref="StandardModuleAttribute"/>
        /// </summary>
        private static IClassType standardModuleAttribute;
        private static IClassType applicationBase;
        private static IClassType consoleApplicationBase;
        private static IClassType generatedCodeAttribute;
        #endregion 

        #region Properties

        public static IClassType ApplicationBase
        {
            get
            {
                if (CommonVBTypeRefs.applicationBase == null)
                {
#if DEBUG
                    Debug.WriteLine("ApplicationBase initialized.");
#endif
                    CommonVBTypeRefs.applicationBase = typeof(ApplicationBase).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonVBTypeRefs.applicationBase.Disposed += new EventHandler(applicationBase_Disposed);
                }
                return CommonVBTypeRefs.applicationBase;
            }
        }

        public static IClassType ConsoleApplicationBase
        {
            get
            {
                if (CommonVBTypeRefs.consoleApplicationBase == null)
                {
#if DEBUG
                    Debug.WriteLine("ConsoleApplicationBase initialized.");
#endif
                    CommonVBTypeRefs.consoleApplicationBase = typeof(ConsoleApplicationBase).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonVBTypeRefs.consoleApplicationBase.Disposed += new EventHandler(consoleApplicationBase_Disposed);
                }
                return CommonVBTypeRefs.consoleApplicationBase;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="HideModuleNameAttribute"/>
        /// system type.
        /// </summary>
        public static IClassType HideModuleNameAttribute
        {
            get
            {
                if (CommonVBTypeRefs.hideModuleNameAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("HideModuleNameAttribute initialized.");
#endif
                    CommonVBTypeRefs.hideModuleNameAttribute = typeof(HideModuleNameAttribute).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonVBTypeRefs.hideModuleNameAttribute.Disposed += new EventHandler(hideModuleNameAttribute_Disposed);
                }
                return CommonVBTypeRefs.hideModuleNameAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the 
        /// <see cref="Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute"/>
        /// system type.
        /// </summary>
        public static IClassType StandardModuleAttribute
        {
            get
            {
                if (CommonVBTypeRefs.standardModuleAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("StandardModuleAttribute initialized.");
#endif
                    CommonVBTypeRefs.standardModuleAttribute = typeof(StandardModuleAttribute).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonVBTypeRefs.standardModuleAttribute.Disposed += CommonVBTypeRefs.standardModuleAttribute_Disposed;
                }
                return CommonVBTypeRefs.standardModuleAttribute;
            }
        }

        public static IClassType GeneratedCodeAttribute
        {
            get
            {
                if (CommonVBTypeRefs.generatedCodeAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("GeneratedCodeAttribute initialized");
#endif
                    CommonVBTypeRefs.generatedCodeAttribute = typeof(GeneratedCodeAttribute).GetTypeReference<IGeneralGenericTypeUniqueIdentifier, IClassType>();
                    CommonVBTypeRefs.generatedCodeAttribute.Disposed += new EventHandler(generatedCodeAttribute_Disposed);
                }
                return CommonVBTypeRefs.generatedCodeAttribute;
            }
        }

        #endregion

        #region Dispose Methods

        static void applicationBase_Disposed(object sender, EventArgs e)
        {
            if (CommonVBTypeRefs.applicationBase != null)
            {
#if DEBUG
                Debug.WriteLine("ApplicationBase Disposed.");
#endif
                CommonVBTypeRefs.applicationBase.Disposed -= applicationBase_Disposed;
                CommonVBTypeRefs.applicationBase = null;
            }
        }

        static void consoleApplicationBase_Disposed(object sender, EventArgs e)
        {
            if (CommonVBTypeRefs.consoleApplicationBase != null)
            {
#if DEBUG
                Debug.WriteLine("ConsoleApplicationBase Disposed.");
#endif
                CommonVBTypeRefs.consoleApplicationBase.Disposed -= consoleApplicationBase_Disposed;
                CommonVBTypeRefs.consoleApplicationBase = null;
            }
        }

        static void hideModuleNameAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonVBTypeRefs.hideModuleNameAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("HideModuleNameAttribute Disposed.");
#endif
                CommonVBTypeRefs.hideModuleNameAttribute.Disposed -= hideModuleNameAttribute_Disposed;
                CommonVBTypeRefs.hideModuleNameAttribute = null;
            }
        }

        static void standardModuleAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonVBTypeRefs.standardModuleAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("StandardModuleAttribute Disposed.");
#endif
                CommonVBTypeRefs.standardModuleAttribute.Disposed -= standardModuleAttribute_Disposed;
                CommonVBTypeRefs.standardModuleAttribute = null;
            }
        }

        static void generatedCodeAttribute_Disposed(object sender, EventArgs e)
        {
            if (CommonVBTypeRefs.generatedCodeAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("GeneratedCodeAttribute Disposed.");
#endif
                CommonVBTypeRefs.generatedCodeAttribute.Disposed -= generatedCodeAttribute_Disposed;
                CommonVBTypeRefs.generatedCodeAttribute = null;
            }
        }
        #endregion
    }
}
