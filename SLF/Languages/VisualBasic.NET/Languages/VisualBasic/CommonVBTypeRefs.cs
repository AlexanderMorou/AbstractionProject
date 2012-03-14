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
        private IClassType hideModuleNameAttribute;

        /// <summary>
        /// Data member for <see cref="StandardModuleAttribute"/>
        /// </summary>
        private IClassType standardModuleAttribute;
        private IClassType applicationBase;
        private IClassType consoleApplicationBase;
        private IClassType generatedCodeAttribute;
        private ICliManager manager;

        private static Dictionary<ICliManager, CommonVBTypeRefs> managerCache;
        #endregion

        internal static CommonVBTypeRefs GetCommonTypeRefs(ICliManager manager)
        {
            if (!managerCache.ContainsKey(manager))
                managerCache.Add(manager, new CommonVBTypeRefs(manager));
            return managerCache[manager];
        }

        private CommonVBTypeRefs(ICliManager manager)
        {
            this.manager = manager;
        }

        #region Properties

        public IClassType ApplicationBase
        {
            get
            {
                if (this.applicationBase == null)
                {
#if DEBUG
                    Debug.WriteLine("ApplicationBase initialized.");
#endif
                    this.applicationBase = (IClassType) this.manager.ObtainTypeReference(typeof(ApplicationBase));
                    this.applicationBase.Disposed += new EventHandler(applicationBase_Disposed);
                }
                return this.applicationBase;
            }
        }

        public IClassType ConsoleApplicationBase
        {
            get
            {
                if (this.consoleApplicationBase == null)
                {
#if DEBUG
                    Debug.WriteLine("ConsoleApplicationBase initialized.");
#endif
                    this.consoleApplicationBase = (IClassType) this.manager.ObtainTypeReference(typeof(ConsoleApplicationBase));
                    this.consoleApplicationBase.Disposed += new EventHandler(consoleApplicationBase_Disposed);
                }
                return this.consoleApplicationBase;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="HideModuleNameAttribute"/>
        /// system type.
        /// </summary>
        public IClassType HideModuleNameAttribute
        {
            get
            {
                if (this.hideModuleNameAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("HideModuleNameAttribute initialized.");
#endif
                    this.hideModuleNameAttribute = (IClassType) this.manager.ObtainTypeReference(typeof(HideModuleNameAttribute));
                    this.hideModuleNameAttribute.Disposed += new EventHandler(hideModuleNameAttribute_Disposed);
                }
                return this.hideModuleNameAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the 
        /// <see cref="Microsoft.VisualBasic.CompilerServices.StandardModuleAttribute"/>
        /// system type.
        /// </summary>
        public IClassType StandardModuleAttribute
        {
            get
            {
                if (this.standardModuleAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("StandardModuleAttribute initialized.");
#endif
                    this.standardModuleAttribute = (IClassType) this.manager.ObtainTypeReference(typeof(StandardModuleAttribute));
                    this.standardModuleAttribute.Disposed += this.standardModuleAttribute_Disposed;
                }
                return this.standardModuleAttribute;
            }
        }

        public IClassType GeneratedCodeAttribute
        {
            get
            {
                if (this.generatedCodeAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("GeneratedCodeAttribute initialized");
#endif
                    this.generatedCodeAttribute = (IClassType) this.manager.ObtainTypeReference(typeof(GeneratedCodeAttribute));
                    this.generatedCodeAttribute.Disposed += new EventHandler(generatedCodeAttribute_Disposed);
                }
                return this.generatedCodeAttribute;
            }
        }

        #endregion

        #region Dispose Methods

        private void applicationBase_Disposed(object sender, EventArgs e)
        {
            if (this.applicationBase != null)
            {
#if DEBUG
                Debug.WriteLine("ApplicationBase Disposed.");
#endif
                this.applicationBase.Disposed -= applicationBase_Disposed;
                this.applicationBase = null;
            }
        }

        private void consoleApplicationBase_Disposed(object sender, EventArgs e)
        {
            if (this.consoleApplicationBase != null)
            {
#if DEBUG
                Debug.WriteLine("ConsoleApplicationBase Disposed.");
#endif
                this.consoleApplicationBase.Disposed -= consoleApplicationBase_Disposed;
                this.consoleApplicationBase = null;
            }
        }

        private void hideModuleNameAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.hideModuleNameAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("HideModuleNameAttribute Disposed.");
#endif
                this.hideModuleNameAttribute.Disposed -= hideModuleNameAttribute_Disposed;
                this.hideModuleNameAttribute = null;
            }
        }

        private void standardModuleAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.standardModuleAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("StandardModuleAttribute Disposed.");
#endif
                this.standardModuleAttribute.Disposed -= standardModuleAttribute_Disposed;
                this.standardModuleAttribute = null;
            }
        }

        private void generatedCodeAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.generatedCodeAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("GeneratedCodeAttribute Disposed.");
#endif
                this.generatedCodeAttribute.Disposed -= generatedCodeAttribute_Disposed;
                this.generatedCodeAttribute = null;
            }
        }
        #endregion
    }
}
