using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    partial class AssemblyWorkspace
    {
        /// <summary>
        /// Provides a combined namespace implementation which
        /// merges the types of a namespace common throughout
        /// a series of assemblies.
        /// </summary>
        public class CombinedNamespace :
            INamespaceDeclaration//,
        //ICombinedNamespaceParent
        {
            private bool normalTypesCheck;
            /// <summary>
            /// Data member for <see cref="Classes"/>.
            /// </summary>
            private ClassTypeDictionary classes;
            /// <summary>
            /// Data member for <see cref="Delegates"/>.
            /// </summary>
            private DelegateTypeDictionary delegates;
            /// <summary>
            /// Data member for <see cref="Enums"/>.
            /// </summary>
            private EnumTypeDictionary enums;
            /// <summary>
            /// Data member for <see cref="Interfaces"/>.
            /// </summary>
            private InterfaceTypeDictionary interfaces;
            /// <summary>
            /// Data member for <see cref="Structs"/>.
            /// </summary>
            private StructTypeDictionary structs;
            /// <summary>
            /// Data member for <see cref="Namespaces"/>.
            /// </summary>
            private CombinedNamespaceDictionary namespaces;

            /// <summary>
            /// Data member for <see cref="Types"/>.
            /// </summary>
            private FullTypesMasterDictionary types;
            /// <summary>
            /// The <see cref="INamespaceParent"/> which contains the 
            /// <see cref="CombinedNamespace"/>.
            /// </summary>
            private INamespaceParent parent;
            /// <summary>
            /// Data member representing the <see cref="INamespaceDeclaration"/> series
            /// from which the <see cref="CombinedNamespace"/> is derived.
            /// </summary>
            private INamespaceDeclaration[] sources;
            /// <summary>
            /// Data member for <see cref="Name"/>.
            /// </summary>
            private string name;
            /// <summary>
            /// Creates a new <see cref="CombinedNamespace"/> from the 
            /// <paramref name="parent"/> and <paramref name="sources"/>.
            /// </summary>
            /// <param name="parent">The <see cref="INamespaceParent"/> which contains the 
            /// <see cref="CombinedNamespace"/>.</param>
            /// <param name="sources">The <see cref="INamespaceDeclaration"/> array from which
            /// the <see cref="CombinedNamespace"/> is derived.</param>
            public CombinedNamespace(string name, INamespaceParent parent, INamespaceDeclaration[] sources)
            {
                this.name = name;
                this.parent = parent;
                this.sources = sources;
            }

            #region INamespaceDeclaration Members

            public IAssembly Assembly
            {
                get
                {
                    if (this.Parent is IAssembly)
                        return (IAssembly)this.Parent;
                    else
                        return this.Parent.Assembly;
                }
            }

            public INamespaceParent Parent
            {
                get { return this.parent; }
            }

            public string FullName
            {
                get { return this.GetFullName(); }
            }

            #endregion

            #region INamespaceParent Members

            public INamespaceDictionary Namespaces
            {
                get
                {
                    this.CheckNamespaces();
                    return this.namespaces;
                }
            }

            #endregion

            #region ITypeParent Members

            public IClassTypeDictionary Classes
            {
                get
                {
                    this.CheckClasses();
                    return this.classes;
                }
            }

            public IDelegateTypeDictionary Delegates
            {
                get
                {
                    this.CheckDelegates();
                    return this.delegates;
                }
            }

            public IEnumTypeDictionary Enums
            {
                get
                {
                    this.CheckEnums();
                    return this.enums;
                }
            }

            public IInterfaceTypeDictionary Interfaces
            {
                get
                {
                    this.CheckInterfaces();
                    return this.interfaces;
                }
            }

            public IStructTypeDictionary Structs
            {
                get
                {
                    this.CheckStructs();
                    return this.structs;
                }
            }

            public IFullTypeDictionary Types
            {
                get
                {
                    this.CheckTypes();
                    return this.types;
                }
            }

            #endregion

            #region IDeclaration Members

            public string Name
            {
                get { return this.name; }
            }

            public string UniqueIdentifier
            {
                get { return this.FullName; }
            }

            /// <summary>
            /// Occurs when the <see cref="CombinedNamespace"/> is disposed.
            /// </summary>
            public event EventHandler Disposed;

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                //Dispose the individual elements of the combined namespace.
                try
                {
                    this.name = null;
                    //Classes
                    if (this.classes != null)
                    {
                        this.classes.Dispose();
                        this.classes = null;
                    }
                    //Delegates
                    if (this.delegates != null)
                    {
                        this.delegates.Dispose();
                        this.delegates = null;
                    }
                    //Enums
                    if (this.enums != null)
                    {
                        this.enums.Dispose();
                        this.enums = null;
                    }
                    //Interfaces
                    if (this.interfaces != null)
                    {
                        this.interfaces.Dispose();
                        this.interfaces = null;
                    }
                    //Structures
                    if (this.structs != null)
                    {
                        this.structs.Dispose();
                        this.structs = null;
                    }
                    //The parent
                    this.parent = null;
                    //The full types dictionary.
                    if (this.types != null)
                    {
                        this.types.Dispose();
                        this.types = null;
                    }
                    //The sub-namespaces.
                    if (this.namespaces != null)
                    {
                        this.namespaces.Dispose();
                        this.namespaces = null;
                    }
                }
                finally
                {
                    this.OnDisposed();
                    if (this.Disposed != null)
                        this.Disposed = null;
                }
            }

            /// <summary>
            /// Invokes the <see cref="Disposed"/> event.
            /// </summary>
            protected virtual void OnDisposed()
            {
                if (this.Disposed != null)
                    this.Disposed(this, EventArgs.Empty);
            }

            #endregion

            /// <summary>
            /// The full types master dictionary which, when accessed,
            /// only instantiates the master, instead of all of its
            /// subordinates as well.
            /// </summary>
            private FullTypesMasterDictionary _Types
            {
                get
                {
                    this.Check_Types();
                    return this.types;
                }
            }

            #region Member Checks

            private void CheckNamespaces()
            {
                if (this.namespaces == null)
                    this.namespaces = this.InitializeNamespaces();
            }

            private void CheckClasses()
            {
                if (this.classes == null)
                    this.classes = this.InitializeClasses();
            }

            private void CheckDelegates()
            {
                if (this.delegates == null)
                    this.delegates = this.InitializeDelegates();
            }

            private void CheckEnums()
            {
                if (this.enums == null)
                    this.enums = this.InitializeEnums();
            }

            private void CheckInterfaces()
            {
                if (this.interfaces == null)
                    this.interfaces = this.InitializeInterfaces();
            }

            private void CheckStructs()
            {
                if (this.structs == null)
                    this.structs = this.InitializeStructs();
            }

            private void CheckTypes()
            {
                if (!normalTypesCheck)
                {
                    this.Check_Types();
                    this.CheckClasses();
                    this.CheckDelegates();
                    this.CheckEnums();
                    this.CheckInterfaces();
                    this.CheckStructs();
                    this.normalTypesCheck = true;
                }
            }

            private void Check_Types()
            {
                if (this.types == null)
                    this.types = new FullTypesMasterDictionary(this, this.sources);
            }
            #endregion

            #region Member Initializers
            private CombinedNamespaceDictionary InitializeNamespaces()
            {
                return new CombinedNamespaceDictionary(this, this.sources);
            }

            private ClassTypeDictionary InitializeClasses()
            {
                return new ClassTypeDictionary(this._Types, this, this.sources);
            }

            private DelegateTypeDictionary InitializeDelegates()
            {
                return new DelegateTypeDictionary(this._Types, this, this.sources);
            }

            private EnumTypeDictionary InitializeEnums()
            {
                return new EnumTypeDictionary(this._Types, this, this.sources);
            }

            private InterfaceTypeDictionary InitializeInterfaces()
            {
                return new InterfaceTypeDictionary(this._Types, this, this.sources);
            }

            private StructTypeDictionary InitializeStructs()
            {
                return new StructTypeDictionary(this._Types, this, this.sources);
            }

            #endregion


            internal void Added(INamespaceDeclaration[] parents)
            {
                if (this.namespaces != null)
                    this.namespaces.Added(parents);
                this.sources = this.sources.Concat(parents).Distinct().ToArray();
                //ToDo: Add code here to update the type dictionaries.
            }
        }
    }
}
