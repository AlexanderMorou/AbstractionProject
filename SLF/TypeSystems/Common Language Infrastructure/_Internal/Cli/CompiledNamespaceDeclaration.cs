using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Reflection;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;

 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal partial class CompiledNamespaceDeclaration :
        DeclarationBase,
        INamespaceDeclaration,
        _ICompiledNamespaceParent,
        _ICompiledTypeParent
    {
        private _ICompiledNamespaceParent parent;
        private string name;
        private IList<string> namespaceNames;
        private Type[] namespaceTypes;
        private MethodInfo[] namespaceMethods;
        private FieldInfo[] namespaceFields;
        private IMethodMemberDictionary<ITopLevelMethod, INamespaceParent> methods;
        private IFieldMemberDictionary<ITopLevelField, INamespaceParent> fields;
        private IClassTypeDictionary classes;
        private IDelegateTypeDictionary delegates;
        private IEnumTypeDictionary enums;
        private IInterfaceTypeDictionary interfaces;
        private IStructTypeDictionary structs;
        private _ICompiledNamespaceDeclarations namespaces;
        private CompiledFullTypeDictionary types;
        private LockedFullMembersBase _members;
        public CompiledNamespaceDeclaration(string name, _ICompiledNamespaceParent parent)
        {
            this.name = name;
            this.parent = parent;
        }
        public _ICompiledNamespaceParent Parent
        {
            get
            {
                return this.parent;
            }
        }

        #region _ICompiledNamespaceParent Members

        public _ICompiledAssembly Assembly
        {
            get { return this.Parent.Assembly; }
        }

        IAssembly ITypeParent.Assembly
        {
            get
            {
                return this.Parent.Assembly;
            }
        }

        public _ICompiledNamespaceDeclarations Namespaces
        {
            get
            {
                if (this.namespaces == null)
                    this.namespaces = new CompiledNamespaceDeclarations(this);
                return this.namespaces;
            }
        }

        #endregion

        #region INamespaceParent Members

        public IFullMemberDictionary Members
        {
            get
            {
                this.CheckFields();
                this.CheckMethods();
                return this._Members;
            }
        }

        INamespaceDictionary INamespaceParent.Namespaces
        {
            get { return this.Namespaces; }
        }

        #endregion

        #region ITypeParent Members

        public IClassTypeDictionary Classes
        {
            get
            {
                CheckClasses();
                return this.classes;
            }
        }

        public IDelegateTypeDictionary Delegates
        {
            get
            {
                CheckDelegates();
                return this.delegates;
            }
        }

        public IEnumTypeDictionary Enums
        {
            get
            {
                CheckEnumerators();
                return this.enums;
            }
        }

        public IInterfaceTypeDictionary Interfaces
        {
            get
            {
                CheckInterfaces();
                return this.interfaces;
            }
        }

        public IStructTypeDictionary Structs
        {
            get
            {
                CheckStructs();
                return this.structs;
            }
        }

        public IFullTypeDictionary Types
        {
            get {
                this.CheckClasses();
                this.CheckDelegates();
                this.CheckEnumerators();
                this.CheckInterfaces();
                this.CheckStructs();
                return this._Types;
            }
        }

        private CompiledFullTypeDictionary _Types
        {
            get
            {
                if (this.types == null)
                    this.types = new CompiledFullTypeDictionary(this);
                return this.types;
            }
        }

        #endregion
        #region Initialization Members

        private void CheckClasses()
        {
            if (this.classes == null)
                this.classes = new CompiledClassTypeDictionary(this, this._Types);
        }

        private void CheckDelegates()
        {
            if (this.delegates == null)
                this.delegates = new CompiledDelegateTypeDictionary(this, this._Types);
        }

        private void CheckEnumerators()
        {
            if (this.enums == null)
                this.enums = new CompiledEnumTypeDictionary(this, this._Types);
        }

        private void CheckInterfaces()
        {
            if (this.interfaces == null)
                this.interfaces = new CompiledInterfaceTypeDictionary(this, this._Types);
        }

        private void CheckStructs()
        {
            if (this.structs == null)
                this.structs = new CompiledStructTypeDictionary(this, this._Types);
        }
        #endregion
        protected override string OnGetName()
        {
            return this.name;
        }

        public override string UniqueIdentifier
        {
            get { return this.GetFullName(); }
        }

        public override void Dispose()
        {
            try
            {
                this.name = null;
                if (this.classes != null)
                {
                    this.classes.Dispose();
                    this.classes = null;
                }
                if (this.delegates != null)
                {
                    this.delegates.Dispose();
                    this.delegates = null;
                }
                if (this.enums != null)
                {
                    this.enums.Dispose();
                    this.enums = null;
                }
                if (this.interfaces != null)
                {
                    this.interfaces.Dispose();
                    this.interfaces = null;
                }
                if (this.structs != null)
                {
                    this.structs.Dispose();
                    this.structs = null;
                }
                this.parent = null;
                if (this.types != null)
                {
                    this.types.Dispose();
                    this.types = null;
                }
                if (this.namespaces != null)
                {
                    this.namespaces.Dispose();
                    this.namespaces = null;
                }
                if (this.namespaceNames != null)
                {
                    this.namespaceNames.Clear();
                    this.namespaceNames = null;
                }
                if (this.namespaceTypes != null)
                    this.namespaceTypes = null;
            }
            finally
            {
                this.OnDisposed();
            }
        }

        #region _ICompiledNamespaceParent Members


        public IList<string> NamespaceNames
        {
            get
            {
                if (this.namespaceNames == null)
                    this.namespaceNames = this.InitializeNamespaceNames();
                return this.namespaceNames;
            }
        }

        private IList<string> InitializeNamespaceNames()
        {
            var thisFullName = this.GetFullName();
            var depth = thisFullName.Count('.');
            var result = ((IList<string>)(new List<string>()));
            foreach (var thatFullName in this.Assembly.FullNamespaceNames)
            {
                /* *
                 * No need to check known mismatches.
                 * Its length should always be longer,
                 * and should always contain exactly
                 * one more period than the current 
                 * level.
                 * */
                if (thatFullName.Length <= thisFullName.Length)
                    continue;
                bool match = true;
                if (thatFullName.Count('.') > depth + 1)
                {
                    match = InitialPartsMatch(thisFullName, thatFullName);
                    var rightSide = thatFullName.Substring(thisFullName.Length + 1);
                    if (match)
                    {
                        var leftSide = rightSide.Substring(0, rightSide.IndexOf('.'));
                        if (!result.Contains(leftSide))
                            result.Add(leftSide);
                    }
                    continue;
                }
                else if (thatFullName.Count('.') <= depth)
                    continue;
                else
                    match = InitialPartsMatch(thisFullName, thatFullName);
                if (!match)
                    continue;
                else
                {
                    var rightSide = thatFullName.Substring(thisFullName.Length + 1);
                    if (!(result.Contains(rightSide)))
                        result.Add(rightSide);
                }
            }
            return result;
        }

        private static bool InitialPartsMatch(string thisFullName, string thatFullName)
        {
            bool match = true;
            for (int i = 0; i < thisFullName.Length; i++)
            {
                if (thatFullName[i] != thisFullName[i])
                {
                    match = false;
                    break;
                }
            }
            return match;
        }

        #endregion

        public Type[] UnderlyingSystemTypes
        {
            get
            {
                if (this.namespaceTypes == null)
                    this.namespaceTypes = this.InitializeNamespaceTypes();
                return this.namespaceTypes;
            }
        }

        private Type[] InitializeNamespaceTypes()
        {
            string fullName = this.GetFullName();
            IList<Type> result = new List<Type>();
            foreach (var v in this.Assembly.AssemblyTypes)
                if (v.Namespace == fullName)
                    result.Add(v);
            return result.ToArray();
        }

        public MethodInfo[] UnderlyingGlobalMethods
        {
            get
            {
                if (this.namespaceMethods == null)
                    this.namespaceMethods = this.InitializeNamespaceMethods();
                return this.namespaceMethods;
            }
        }
        public FieldInfo[] UnderlyingGlobalFields
        {
            get
            {
                if (this.namespaceFields == null)
                    this.namespaceFields = this.InitializeNamespaceFields();
                return this.namespaceFields;
            }
        }

        private MethodInfo[] InitializeNamespaceMethods()
        {
            string fullName = this.GetFullName();
            int fullLength = fullName.Length;
            /* *
             * Filter the methods from the global methods based off of
             * the prefix being identical to the fullname of the current
             * namespace.
             * */
            return (from m in this.Assembly.AssemblyGlobalMethods
                    let mName = m.Name
                    where mName.Length > fullLength
                    let lName = mName.Substring(0, fullLength)
                    where lName == fullName
                    let dot = mName[fullLength]
                    let rName = mName.Substring(fullLength + 1)
                    where dot == '.' &&
                        !(string.IsNullOrEmpty(rName) ||
                            rName.Contains('.'))
                    select m).ToArray();
        }

        private FieldInfo[] InitializeNamespaceFields()
        {
            string fullName = this.GetFullName();
            int fullLength = fullName.Length;
            /* *
             * Filter the fields from the global fields based off of
             * the prefix being identical to the fullname of the current
             * namespace.
             * */
            return (from m in this.Assembly.AssemblyGlobalFields
                    let mName = m.Name
                    where mName.Length > fullLength
                    let lName = mName.Substring(0, fullLength)
                    where lName == fullName
                    let dot = mName[fullLength]
                    let rName = mName.Substring(fullLength + 1)
                    where dot == '.' &&
                        !(string.IsNullOrEmpty(rName) ||
                            rName.Contains('.'))
                    select m).ToArray();
        }
        #region INamespaceDeclaration Members

        IAssembly INamespaceDeclaration.Assembly
        {
            get { return this.Assembly; }
        }

        INamespaceParent INamespaceDeclaration.Parent
        {
            get { return this.parent; }
        }

        public string FullName
        {
            get { return this.GetFullName(); }
        }

        #endregion

        public override string ToString()
        {
            return this.FullName;
        }

        public IEnumerable<string> AggregateIdentifiers
        {
            get
            {

                return this.GetAggregateIdentifiers();
            }
        }

        #region IMethodParent<ITopLevelMethod,INamespaceParent> Members

        public IMethodMemberDictionary<ITopLevelMethod, INamespaceParent> Methods
        {
            get {
                CheckMethods();
                return this.methods;
            }
        }

        private void CheckMethods()
        {
            if (this.methods == null)
                this.methods = this.InitializeMethods();
        }

        private IMethodMemberDictionary<ITopLevelMethod, INamespaceParent> InitializeMethods()
        {
            return new LockedMethodMembersBase<ITopLevelMethod, INamespaceParent>(this._Members, this, this.UnderlyingGlobalMethods, this.GetMethod);
        }

        #endregion

        #region IMethodParent Members

        IMethodMemberDictionary IMethodParent.Methods
        {
            get { return (IMethodMemberDictionary)this.Methods; }
        }

        #endregion

        public LockedFullMembersBase _Members
        {
            get
            {
                if (this._members == null)
                    this._members = new LockedFullMembersBase();
                return this._members;
            }
        }


        #region IFieldParent<ITopLevelField,INamespaceParent> Members

        public IFieldMemberDictionary<ITopLevelField, INamespaceParent> Fields
        {
            get {
                this.CheckFields();
                return this.fields;
            }
        }

        private void CheckFields()
        {
            if (this.fields == null)
                this.fields = this.InitializeFields();
        }

        private IFieldMemberDictionary<ITopLevelField, INamespaceParent> InitializeFields()
        {
            return new LockedFieldMembersBase<ITopLevelField, INamespaceParent>(this._Members, this, this.UnderlyingGlobalFields, this.GetField);
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return (IFieldMemberDictionary)this.Fields; }
        }

        #endregion

        private ITopLevelField GetField(FieldInfo memberInfo)
        {
            return new CompiledTopLevelField(memberInfo, this);
        }

        private ITopLevelMethod GetMethod(MethodInfo memberInfo)
        {
            return new CompiledTopLevelMethod(memberInfo, this);
        }
    }
}
