﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;

 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
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
        ICompiledTypeParent
    {
        private _ICompiledNamespaceParent parent;
        private string name;
        private IList<string> namespaceNames;
        private Type[] namespaceTypes;
        private IClassTypeDictionary classes;
        private IDelegateTypeDictionary delegates;
        private IEnumTypeDictionary enums;
        private IInterfaceTypeDictionary interfaces;
        private IStructTypeDictionary structs;
        private _ICompiledNamespaceDeclarations namespaces;
        private CompiledFullTypeDictionary types;
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
    }
}