using System;
using System.Collections.Generic;
using System.Text;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Abstract
{
    /// <summary>
    /// Provides a base implementation of an assembly workspace which
    /// unifies multiple assemblies under a common instance.
    /// </summary>
    public partial class AssemblyWorkspace :
        IAssemblyWorkspace
    {
        private bool normalTypesCheck = false;
        /// <summary>
        /// Data member for <see cref="Types"/>.
        /// </summary>
        private FullTypesMasterDictionary types;
        /// <summary>
        /// Data member representing the assembly the <see cref="AssemblyWorkspace"/> 
        /// is focused upon.
        /// </summary>
        private IAssembly assembly;
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
        private CombinedNamespaceDictionary namespaces;
        private ReferenceCollection references;

        /// <summary>
        /// Creates a new <see cref="IAssembly"/> which denotes the assembly that the 
        /// workspace is centralized around.
        /// </summary>
        /// <param name="rootAssembly">The <see cref="IAssembly"/> the <see cref="AssemblyWorkspace"/>
        /// is centered around.</param>
        public AssemblyWorkspace(IAssembly rootAssembly)
        {
            this.assembly = rootAssembly;
        }

        #region INamespaceParent Members

        /// <summary>
        /// Returns the namespace dictionary associated to the
        /// <see cref="AssemblyWorkspace"/>.
        /// </summary>
        public INamespaceDictionary Namespaces
        {
            get {
                this.CheckNamespaces();
                return this.namespaces;
            }
        }

        #endregion
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
                this.types = new FullTypesMasterDictionary(this, this.References.ToArray());
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

        #region Member Initializers
        private CombinedNamespaceDictionary InitializeNamespaces()
        {
            return new CombinedNamespaceDictionary(this, this.References.ToArray());
        }


        private ClassTypeDictionary InitializeClasses()
        {
            return new ClassTypeDictionary(this._Types, this, this.References.ToArray());
        }

        private DelegateTypeDictionary InitializeDelegates()
        {
            return new DelegateTypeDictionary(this._Types, this, this.References.ToArray());
        }

        private EnumTypeDictionary InitializeEnums()
        {
            return new EnumTypeDictionary(this._Types, this, this.References.ToArray());
        }

        private InterfaceTypeDictionary InitializeInterfaces()
        {
            return new InterfaceTypeDictionary(this._Types, this, this.References.ToArray());
        }

        private StructTypeDictionary InitializeStructs()
        {
            return new StructTypeDictionary(this._Types, this, this.References.ToArray());
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

        public IAssembly Assembly
        {
            get { return this.assembly; }
        }

        #endregion

        #region IAssemblyWorkspace Members

        /// <summary>
        /// Returns the series of <see cref="IAssembly"/> instances
        /// referenced by the workspace.
        /// </summary>
        public IAssemblyReferenceCollection References
        {
            get {
                if (this.references == null)
                    this.references = new ReferenceCollection(this);
                return this.references;
            }
        }

        #endregion

        public static AssemblyWorkspace operator |(AssemblyWorkspace left, AssemblyWorkspace right)
        {
            var result = new AssemblyWorkspace(left.Assembly);
            foreach (var @ref in left.References)
                if (@ref == left.assembly)
                    continue;
                else
                    result.References.Add(@ref);
            foreach (var @ref in right.References)
                result.References.Add(@ref);
            return result;
        }

        public static AssemblyWorkspace operator ^(AssemblyWorkspace left, AssemblyWorkspace right)
        {
            throw new NotImplementedException();
        }

        public static AssemblyWorkspace operator &(AssemblyWorkspace left, AssemblyWorkspace right)
        {
            throw new NotImplementedException();
        }

        public static AssemblyWorkspace operator +(AssemblyWorkspace left, IAssembly right)
        {
            AssemblyWorkspace result = new AssemblyWorkspace(left.Assembly);
            foreach (var @ref in left.References)
                if (@ref == left.Assembly)
                    continue;
                else
                    result.References.Add(@ref);
            result.References.Add(right);
            return result;
        }

        public static AssemblyWorkspace operator -(AssemblyWorkspace left, IAssembly right)
        {
            AssemblyWorkspace result = null;
            if (left.Assembly == right)
            {
                if (left.References.Count <= 1)
                    return null;
                else
                    result = new AssemblyWorkspace(left.References[1]);
                foreach (var @ref in left.References)
                    if (@ref == right ||
                        @ref == result.Assembly)
                        continue;
                    else
                        result.References.Add(@ref);
            }
            else
            {
                result = new AssemblyWorkspace(left.Assembly);
                foreach (var @ref in left.References)
                    if (@ref == right ||
                        @ref == left.Assembly)
                        continue;
                    else
                        result.References.Add(@ref);
            }
            return result;
        }
    }
}
