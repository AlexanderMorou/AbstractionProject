﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    /// <summary>
    /// Provides a base implementation of an intermediate namespace
    /// dictionary.
    /// </summary>
    [DebuggerDisplay("Namespaces: {Count}")]
    public class IntermediateNamespaceDictionary :
        IntermediateDeclarationDictionary<IGeneralDeclarationUniqueIdentifier, INamespaceDeclaration, IIntermediateNamespaceDeclaration>,
        IIntermediateNamespaceDictionary
    {
        private IIntermediateNamespaceParent parent;
        /// <summary>
        /// Creates a new instance of an <see cref="IntermediateNamespaceDictionary"/> with
        /// the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateNamespaceParent"/>
        /// which contains the <see cref="IntermediateNamespaceDictionary"/> 
        /// and its elements.</param>
        public IntermediateNamespaceDictionary(IIntermediateNamespaceParent parent) :
            base()
        {
            this.parent = parent;
        }
        /// <summary>
        /// Creates a new instance of an <see cref="IntermediateNamespaceDictionary"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateNamespaceParent"/>
        /// which contains the <see cref="IntermediateNamespaceDictionary"/>
        /// and its elements.</param>
        /// <param name="toWrap">The <see cref="IntermediateNamespaceDictionary"/> 
        /// to wrap the elements of.</param>
        public IntermediateNamespaceDictionary(IIntermediateNamespaceParent parent, IntermediateNamespaceDictionary toWrap) :
            base(toWrap)
        {
            this.parent = parent;
        }

        #region IIntermediateNamespaceDictionary Members

        public IIntermediateNamespaceDeclaration this[string path]
        {
            get
            {
                return (IIntermediateNamespaceDeclaration)this.GetThis(path);
            }
        }

        protected override INamespaceDeclaration OnGetThis(IGeneralDeclarationUniqueIdentifier key)
        {
            return this[key.Name];
        }

        /// <summary>
        /// Returns the <see cref="IIntermediateNamespaceParent"/>
        /// which contains the 
        /// <see cref="IntermediateNamespaceDictionary"/>.
        /// </summary>
        public IIntermediateNamespaceParent Parent
        {
            get { return this.parent; }
        }

        /// <summary>
        /// Adds a new <see cref="IIntermediateNamespaceDeclaration"/> to the
        /// <see cref="IntermediateNamespaceDictionary"/>.
        /// </summary>
        /// <param name="path">The <see cref="String"/> representing the namespace's
        /// fully qualified path.</param>
        /// <returns>A new <see cref="IIntermediateNamespaceDeclaration"/>
        /// instance that results from the operation.</returns>
        /// <remarks>The <paramref name="name"/> is segmented and delimited by periods (Full Stops, U+002E)
        /// which make up the invidual sub-namespaces of the <see cref="IIntermediateNamespaceDeclaration"/>
        /// that results.</remarks>
        /// <exception cref="System.ArgumentException"><paramref name="path"/> exists
        /// already; <paramref name="path"/> is <see cref="String.Empty"/>; or
        /// <paramref name="path"/> consists of periods (Full Stops, U+002E) only.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        public IIntermediateNamespaceDeclaration Add(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            if (path == string.Empty)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.path, ExceptionMessageId.ArgumentCannotBeEmpty, ThrowHelper.GetArgumentName(ArgumentWithException.path));
            if (path.Contains("."))
            {
                IGeneralDeclarationUniqueIdentifier[] names =
                    (from subKey in path.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                     select AstIdentifier.Declaration(subKey)).ToArray();
                //It was comprised of dots only.
                if (names.Length == 0)
                    throw ThrowHelper.ObtainArgumentException(ArgumentWithException.path, ExceptionMessageId.PathCannotBeDotsOnly);
                if (names.Length == 1)
                    return this.Add(names[0].Name);
                IIntermediateNamespaceParent parent = this.Parent;
                bool hadNonExistant = false;
                for (int i = 0; i < names.Length; i++)
                    if (parent.Namespaces.ContainsKey(names[i]))
                        parent = parent.Namespaces[names[i]];
                    else
                    {
                        if (!hadNonExistant)
                            hadNonExistant = true;
                        parent = parent.Namespaces.Add(names[i].Name);
                    }
                //The path already exists.
                if (!hadNonExistant)
                    throw new ArgumentException("path");
                return (IIntermediateNamespaceDeclaration)parent;
            }
            else
            {
                if (this.ContainsKey(path))
                    throw new ArgumentException(string.Format("The provided path {0} already exists.", path), "path");
                IntermediateNamespaceDeclaration ind = new IntermediateNamespaceDeclaration(path, this.Parent);
                this._Add(ind.UniqueIdentifier, ind);
                return ind;
            }
        }

        public bool ContainsKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (key == string.Empty)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.key, ExceptionMessageId.ArgumentCannotBeEmpty, ThrowHelper.GetArgumentName(ArgumentWithException.key));
            return this.ContainsKey(AstIdentifier.Declaration(key));
        }

        public override bool ContainsKey(IGeneralDeclarationUniqueIdentifier key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (key.Name == string.Empty)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.key, ExceptionMessageId.ArgumentCannotBeEmpty, ThrowHelper.GetArgumentName(ArgumentWithException.key));
            if (!key.Name.Contains("."))
                return base.ContainsKey(key);
            else
            {
                IGeneralDeclarationUniqueIdentifier[] keys =
                    (from subKey in key.Name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)
                     select AstIdentifier.Declaration(subKey)).ToArray();
                if (keys.Length == 0)
                    throw new ArgumentException("keys");
                if (keys.Length == 1)
                    return this.ContainsKey(keys[0]);
                IIntermediateNamespaceParent parent = this.parent;
                for (int i = 0; i < keys.Length; i++)
                    if (!parent.Namespaces.ContainsKey(keys[i]))
                        return false;
                    else
                        parent = parent.Namespaces[keys[i]];
                return true;
            }
        }

        /// <summary>
        /// Adds an existing <see cref="IIntermediateNamespaceDeclaration"/>
        /// instance to the <see cref="IIntermediateNamespaceDictionary"/>.
        /// </summary>
        /// <param name="ns">the <see cref="IIntermediateNamespaceDeclaration"/> to add.</param>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="ns"/> is null.</exception>
        public void Add(IIntermediateNamespaceDeclaration ns)
        {
            if (ns.Parent == this.Parent)
                this._Add(ns.UniqueIdentifier, ns);
            else
                throw new ArgumentException("namespace parent must be equal to the namespace dictionary parent.", "ns");
        }

        #endregion

        #region INamespaceDictionary Members

        INamespaceParent INamespaceDictionary.Parent
        {
            get { return this.Parent; }
        }

        public bool PathExists(string path)
        {
            return this.ContainsKey(path);
        }

        INamespaceDeclaration INamespaceDictionary.this[string path]
        {
            get
            {
                return this[path];
            }
        }

        #endregion

        protected override bool ShouldDispose(IIntermediateNamespaceDeclaration declaration)
        {
            return declaration.Parent == parent;
        }
    }
}