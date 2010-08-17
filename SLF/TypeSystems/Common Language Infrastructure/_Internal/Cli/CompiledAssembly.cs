using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Modules;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using AllenCopeland.Abstraction.Utilities.Common;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CompiledAssembly :
        AssemblyBase,
        _ICompiledAssembly,
        ICompiledTypeParent
    {
        private CompiledFullTypeDictionary _types;

        private bool initializedCustomAttributes;
        private Type[] assemblyTypes;
        private Type[] assemblyLevelTypes;
        private byte[] publicKeyToken = null;
        /// <summary>
        /// Data member for <see cref="AssemblyBase.AssemblyInformation"/>.
        /// </summary>
        private IAssemblyInformation assemblyInfo = null;
        private IList<string> fullNamespaceNames;
        private IList<string> namespaceNames;
        /// <summary>
        /// Data member storing the modules that have been
        /// wrapped thus far.
        /// </summary>
        private IDictionary<Module, ICompiledModule> internalModuleCache;
        /// <summary>
        /// Data member storing the <see cref="UnderlyingAssembly"/>.
        /// </summary>
        private Assembly underlyingAssembly;
        /// <summary>
        /// Creates a new <see cref="CompiledAssembly"/> with the <paramref name="underlyingAssembly"/> 
        /// provided.
        /// </summary>
        /// <param name="underlyingAssembly">The <see cref="Assembly"/> from which the <see cref="CompiledAssembly"/>
        /// relates to.</param>
        internal CompiledAssembly(Assembly underlyingAssembly)
        {
            this.underlyingAssembly = underlyingAssembly;
        }
        #region ICompiledAssembly Members

        /// <summary>
        /// Returns the <see cref="System.Reflection.Assembly"/> that the <see cref="CompiledAssembly"/>
        /// relates to.
        /// </summary>
        public Assembly UnderlyingAssembly
        {
            get { return this.underlyingAssembly; }
        }

        #endregion

        protected override IAssemblyInformation OnGetAssemblyInformation()
        {
            if (this.assemblyInfo == null)
                this.assemblyInfo = new AssemblyInformation(this.underlyingAssembly);
            return this.assemblyInfo;
        }

        protected override IFullTypeDictionary InitializeTypes()
        {
            this.CheckClasses();
            this.CheckDelegates();
            this.CheckEnumerators();
            this.CheckInterfaces();
            this.CheckStructs();
            return this._Types;
        }

        protected override IStructTypeDictionary InitializeStructs()
        {
            return new CompiledStructTypeDictionary(this, this._Types); 
        }

        protected override IInterfaceTypeDictionary InitializeInterfaces()
        {
            return new CompiledInterfaceTypeDictionary(this, this._Types);
        }

        protected override IEnumTypeDictionary InitializeEnums()
        {
            return new CompiledEnumTypeDictionary(this, this._Types);
        }

        protected override IDelegateTypeDictionary InitializeDelegates()
        {
            return new CompiledDelegateTypeDictionary(this, this._Types);
        }

        protected override IClassTypeDictionary InitializeClasses()
        {
            return new CompiledClassTypeDictionary(this, this._Types);
        }

        protected override IModule OnGetManifestModule()
        {
            return GetCompiledModule(this.UnderlyingAssembly.ManifestModule);
        }

        protected override bool CanCacheManifestModule
        {
            get { return true; }
        }

        private ICompiledModule GetCompiledModule(Module m)
        {
            if (this.internalModuleCache == null)
                this.internalModuleCache = new Dictionary<Module, ICompiledModule>();
            if (this.internalModuleCache.ContainsKey(m))
                return this.internalModuleCache[m];
            ICompiledModule icm = null;
            icm = new CompiledModule(this, m);
            this.internalModuleCache.Add(m, icm);
            return icm;
        }
        /// <summary>
        /// Returns a <see cref="System.String"/> which 
        /// represents the current <see cref="CompiledAssembly"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/>which 
        /// represents the current <see cref="CompiledAssembly"/>.</returns>
        public override string ToString()
        {
            var pkt = this.PublicKeyToken;
            string pktStr = pkt.Length == 0 ? "null" : pkt.FormatHexadecimal().ToLower();
            return string.Format("{0}, Version={1}, Culture={2}, PublicKeyToken={3}", this.AssemblyInformation.AssemblyName, this.AssemblyInformation.AssemblyVersion, string.IsNullOrEmpty(this.AssemblyInformation.Culture.Name) ? "neutral" : this.AssemblyInformation.Culture.Name, pktStr);
        }

        private byte[] PublicKeyToken
        {
            get
            {
                if (this.publicKeyToken == null)
                    this.publicKeyToken = this.UnderlyingAssembly.GetName().GetPublicKeyToken();
                return this.publicKeyToken;
            }
        }

        protected override INamespaceDictionary InitializeNamespaces()
        {
            return new CompiledNamespaceDeclarations(this);
        }

        #region _ICompiledAssembly Members
        public Type[] AssemblyTypes
        {
            get
            {
                if (this.assemblyTypes == null)
                {
                    this.assemblyTypes = (from t in (this.UnderlyingAssembly.GetTypes().Filter(t =>
                    {
                        var accessLevelModifiers = t.GetAccessModifiers();
                        switch (accessLevelModifiers)
                        {
                            case AccessLevelModifiers.Private:
                            case AccessLevelModifiers.PrivateScope:
                                return false;
                            case AccessLevelModifiers.InternalProtected:
                            case AccessLevelModifiers.Internal:
                            case AccessLevelModifiers.Public:
                            case AccessLevelModifiers.Protected:
                            case AccessLevelModifiers.ProtectedInternal:
                            default:
                                return t.DeclaringType == null;
                        }
                    }))
                                          orderby ((
                                            t.IsClass && !typeof(Delegate).IsAssignableFrom(t)) ?
                                                0 :
                                            t.IsEnum ?
                                                2 :
                                            t.IsSubclassOf(typeof(Delegate)) ?
                                                1 :
                                            t.IsInterface ?
                                                3 :
                                            t.IsValueType && !t.IsEnum ?
                                                4 :
                                                5),
                                            t.Name
                                          select t).ToArray();
                }
                return this.assemblyTypes;
            }
        }

        #endregion

        #region _ICompiledNamespaceParent Members

        _ICompiledAssembly _ICompiledNamespaceParent.Assembly
        {
            get { return this; }
        }

        public new _ICompiledNamespaceDeclarations Namespaces
        {
            get { return (_ICompiledNamespaceDeclarations)base.Namespaces; }
        }

        public IList<string> NamespaceNames
        {
            get {
                if (this.namespaceNames == null)
                    this.namespaceNames = InitializeNamespaceNames();
                return this.namespaceNames;
            }
        }

        private IList<string> InitializeNamespaceNames()
        {
            IList<string> result = new List<string>();
            foreach (string s in this.FullNamespaceNames)
                if (s.Contains('.'))
                {
                    var topLevel = s.Substring(0, s.IndexOf('.'));
                    if (!result.Contains(topLevel))
                        result.Add(topLevel);
                }
                else if (!result.Contains(s))
                        result.Add(s);
            return result;
        }

        #endregion

        #region _ICompiledAssembly Members


        public IList<string> FullNamespaceNames
        {
            get {
                if (fullNamespaceNames == null)
                    this.fullNamespaceNames = InitializeFullNamespaceNames();
                return this.fullNamespaceNames;
            }
        }

        private IList<string> InitializeFullNamespaceNames()
        {
            List<string> result = new List<string>();
            Type[] types = this.UnderlyingAssembly.GetTypes();

            foreach (var t in types)
            {
                var ns = t.Namespace;
                if (string.IsNullOrEmpty(ns))
                    continue;
                else if (t.DeclaringType == null &&
                    !result.Contains(ns))
                    result.Add(ns);
            }
            result.Sort();
            return result;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    this.RemoveFromCache();
                    if (this.namespaceNames != null)
                    {
                        this.namespaceNames.Clear();
                        this.namespaceNames = null;
                    }
                    if (this.publicKeyToken != null)
                        this.publicKeyToken = null;
                    if (this.types != null)
                    {
                        ((CompiledFullTypeDictionary)this.types).Dispose();
                        this.types = null;
                    }
                    if (this.assemblyInfo != null)
                        this.assemblyInfo = null;
                    if (this.assemblyTypes != null)
                        this.assemblyTypes = null;
                    if (this.initializedCustomAttributes)
                        this.CustomAttributes.Dispose();
                    this.underlyingAssembly = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region ICompiledTypeParent Members

        public Type[] UnderlyingSystemTypes
        {
            get {
                if (assemblyLevelTypes == null)
                    assemblyLevelTypes = this.InitializeAssemblyLevelTypes();
                return this.assemblyLevelTypes;
            }
        }

        private Type[] InitializeAssemblyLevelTypes()
        {
            return this.AssemblyTypes.Filter(assemblyLevelType =>
                string.IsNullOrEmpty(assemblyLevelType.Namespace));
        }

        #endregion


        #region ITypeParent Members

        IAssembly ITypeParent.Assembly
        {
            get { return this; }
        }

        #endregion

        private CompiledFullTypeDictionary _Types
        {
            get
            {
                if (this._types == null)
                    this._types = new CompiledFullTypeDictionary(this);
                return this._types;
           
            }
        }

    }
}
