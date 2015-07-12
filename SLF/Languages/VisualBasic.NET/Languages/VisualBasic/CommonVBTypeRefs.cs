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
        private enum LibraryTarget
        {
            System,
            SystemCore,
            MSVB,
        }
        #region Data Members

        /// <summary>
        /// Data member for <see cref="HideModuleNameAttribute"/>
        /// </summary>
        private IClassType hideModuleNameAttribute;

        /// <summary>
        /// Data member maintaining internal associations for type/member
        /// identity resolution.
        /// </summary>
        private ICliManager manager;
        /// <summary>
        /// Data member for <see cref="StandardModuleAttribute"/>
        /// </summary>
        private IClassType standardModuleAttribute;
        /// <summary>
        /// Data member for <see cref="ApplicationBase"/>.
        /// </summary>
        private IClassType applicationBase;
        /// <summary>
        /// Data member for <see cref="ConsoleApplicationBase"/>.
        /// </summary>
        private IClassType consoleApplicationBase;
        /// <summary>
        /// Data member for <see cref="GeneratedCodeAttribute"/>.
        /// </summary>
        private IClassType generatedCodeAttribute;
        /// <summary>
        /// Data member for <see cref="ComputerBase"/>.
        /// </summary>
        private IClassType computerBase;
        /// <summary>
        /// Data member for <see cref="EditorBrowsableAttribute"/>.
        /// </summary>
        private IClassType editorBrowsableAttribute;
        /// <summary>
        /// Data member for <see cref="EditorBrowsableState"/>.
        /// </summary>
        private IEnumType editorBrowsableState;
        /// <summary>
        /// Data member for <see cref="ExtensionAttribute"/>.
        /// </summary>
        private IClassType extensionAttribute;
        private static Dictionary<ITypeIdentityManager, CommonVBTypeRefs> managerCache = new Dictionary<ITypeIdentityManager, CommonVBTypeRefs>();
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
                    this.applicationBase = (IClassType)GetTypeFromCommonLibrary("Microsoft.VisualBasic.ApplicationServices", "ApplicationBase");
                    this.applicationBase.Disposed += new EventHandler(applicationBase_Disposed);
                }
                return this.applicationBase;
            }
        }

        public IClassType Computer
        {
            get
            {
                if (this.computerBase == null)
                {
#if DEBUG
                    Debug.WriteLine("Computer initialized.");
#endif

                    this.computerBase = (IClassType)GetTypeFromCommonLibrary("Microsoft.VisualBasic.Devices", "Computer");
                    this.computerBase.Disposed += new EventHandler(computerBase_Disposed);
                }
                return this.computerBase;
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

                    this.consoleApplicationBase = (IClassType)GetTypeFromCommonLibrary("Microsoft.VisualBasic.ApplicationServices", "ConsoleApplicationBase");
                    this.consoleApplicationBase.Disposed += new EventHandler(consoleApplicationBase_Disposed);
                }
                return this.consoleApplicationBase;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="Microsoft.VisualBasic.HideModuleNameAttribute"/>
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
                    this.hideModuleNameAttribute = (IClassType)GetTypeFromCommonLibrary("Microsoft.VisualBasic", "HideModuleNameAttribute");
                    this.hideModuleNameAttribute.Disposed += new EventHandler(hideModuleNameAttribute_Disposed);
                }
                return this.hideModuleNameAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the <see cref="System.Runtime.CompilerServices.ExtensionAttribute"/>
        /// system type.
        /// </summary>
        /// <remarks>In .NET 3.5 through .NET 4.0 this was part of the System.Core library; after that it was
        /// a part of the CommonLanguageRuntime library.</remarks>
        public IClassType ExtensionAttribute
        {
            get
            {
                if (this.extensionAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("ExtensionAttribute initialized.");
#endif
                    switch (this.manager.RuntimeEnvironment.Version & ~CliFrameworkVersion.ClientProfile)
                    {
                        case CliFrameworkVersion.v1_0_3705:
                        case CliFrameworkVersion.v1_1_4322:
                        case CliFrameworkVersion.v2_0_50727:
                        case CliFrameworkVersion.v3_0:
                            return null;
                        case CliFrameworkVersion.v3_5:
                        case CliFrameworkVersion.v4_0_30319:
                            this.extensionAttribute = (IClassType)GetTypeFromCommonLibrary("System.Runtime.CompilerServices", "ExtensionAttribute", library: LibraryTarget.SystemCore);
                            break;
                        case CliFrameworkVersion.v4_5:
                            this.extensionAttribute = (IClassType)GetTypeFromCommonLibrary("System.Runtime.CompilerServices", "ExtensionAttribute", library: LibraryTarget.SystemCore);
                            break;
                        default:
                            break;
                    }
                    this.extensionAttribute.Disposed += new EventHandler(extensionAttribute_Disposed);
                }
                return this.extensionAttribute;
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
                    this.standardModuleAttribute = (IClassType)GetTypeFromCommonLibrary("Microsoft.VisualBasic.CompilerServices", "StandardModuleAttribute");
                    this.standardModuleAttribute.Disposed += this.standardModuleAttribute_Disposed;
                }
                return this.standardModuleAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the 
        /// <see cref="System.ComponentModel.EditorBrowsableAttribute"/>
        /// system type.
        /// </summary>
        public IClassType EditorBrowsableAttribute
        {
            get
            {
                if (this.editorBrowsableAttribute == null)
                {
#if DEBUG
                    Debug.WriteLine("EditorBrowsableAttribute initialized.");
#endif
                    this.editorBrowsableAttribute = (IClassType)GetTypeFromCommonLibrary("System.ComponentModel", "EditorBrowsableAttribute", library: LibraryTarget.System);
                    this.editorBrowsableAttribute.Disposed += this.editorBrowsableAttribute_Disposed;
                }
                return this.editorBrowsableAttribute;
            }
        }

        /// <summary>
        /// Returns the <see cref="IClassType"/> reference wrapper for the 
        /// <see cref="System.ComponentModel.EditorBrowsableState"/>
        /// system type.
        /// </summary>
        public IEnumType EditorBrowsableState
        {
            get
            {
                if (this.editorBrowsableState == null)
                {
#if DEBUG
                    Debug.WriteLine("EditorBrowsableState initialized.");
#endif
                    this.editorBrowsableState = (IEnumType)GetTypeFromCommonLibrary("System.ComponentModel", "EditorBrowsableState", library: LibraryTarget.System);
                    this.editorBrowsableState.Disposed += this.editorBrowsableState_Disposed;
                }
                return this.editorBrowsableState;
            }
        }

        private IType GetTypeFromCommonLibrary(string @namespace, string typeName, int typeParameters = 0, LibraryTarget library = LibraryTarget.MSVB)
        {
            IAssemblyUniqueIdentifier assemUniqueId;
            switch (library)
            {
                case LibraryTarget.System:
                    assemUniqueId = MicrosoftLanguageVendor.StandardLibraryIdentifiers.GetVersionedSystemLibrary(this.manager.RuntimeEnvironment.Version);
                    break;
                case LibraryTarget.MSVB:
                    assemUniqueId = MicrosoftLanguageVendor.StandardLibraryIdentifiers.GetVersionedMicrosoftVisualBasicLibrary(this.manager.RuntimeEnvironment.Version);
                    break;
                case LibraryTarget.SystemCore:
                    assemUniqueId = MicrosoftLanguageVendor.StandardLibraryIdentifiers.GetVersionedSystemCoreLibrary(this.manager.RuntimeEnvironment.Version);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("library");
            }
            return this.manager.ObtainTypeReference(assemUniqueId.GetTypeIdentifier(@namespace, typeName, typeParameters));
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
                    this.generatedCodeAttribute = (IClassType)GetTypeFromCommonLibrary("System.CodeDom.Compiler", "GeneratedCodeAttribute", library: LibraryTarget.System);
                    this.generatedCodeAttribute.Disposed += new EventHandler(generatedCodeAttribute_Disposed);
                }
                return this.generatedCodeAttribute;
            }
        }

        #endregion

        #region Dispose Methods

        private void editorBrowsableAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.editorBrowsableAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("EditorBrowsableAttribute Disposed.");
#endif
                this.editorBrowsableAttribute.Disposed -= editorBrowsableAttribute_Disposed;
                this.editorBrowsableAttribute = null;
            }
        }

        private void editorBrowsableState_Disposed(object sender, EventArgs e)
        {
            if (this.editorBrowsableState != null)
            {
#if DEBUG
                Debug.WriteLine("EditorBrowsableState Disposed.");
#endif
                this.editorBrowsableState.Disposed -= editorBrowsableState_Disposed;
                this.editorBrowsableState = null;
            }
        }

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

        private void computerBase_Disposed(object sender, EventArgs e)
        {
            if (this.computerBase != null)
            {
#if DEBUG
                Debug.WriteLine("ConsoleApplicationBase Disposed.");
#endif
                this.computerBase.Disposed -= computerBase_Disposed;
                this.computerBase = null;
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

        private void extensionAttribute_Disposed(object sender, EventArgs e)
        {
            if (this.extensionAttribute != null)
            {
#if DEBUG
                Debug.WriteLine("ExtensionAttribute Disposed.");
#endif
                this.extensionAttribute.Disposed -= extensionAttribute_Disposed;
                this.extensionAttribute = null;
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
