using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Languages;
//using AllenCopeland.Abstraction.Slf.Cli;
/*---------------------------------------------------------------------\
| Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
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
        public virtual IIntermediateNamespaceDeclaration Add(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            if (path == string.Empty)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.path, ExceptionMessageId.ArgumentCannotBeEmpty, ThrowHelper.GetArgumentName(ArgumentWithException.path));
            IIntermediateNamespaceCtorLanguageService ctorService = null;
            if (this.Parent != null && this.Parent.Assembly != null && this.Parent.Assembly.Provider != null)
                this.Parent.Assembly.Provider.TryGetService(LanguageGuids.Services.IntermediateNamespaceCreatorService, out ctorService);
            string separator = ctorService == null
                               ? "."
                               : ctorService.IdentitySeparator;

            if (path.Contains(separator))
            {
                var pathVariants = path.NamespacePathBreakdown();
                if (this.Parent is INamespaceDeclaration)
                    pathVariants = pathVariants.Except(((INamespaceDeclaration)(this.Parent)).FullName.NamespacePathBreakdown(separator)).ToArray();
                var names = pathVariants.ToArray();
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
                        parent = parent.Namespaces.Add(names[i].Name.GetNamespaceFinalPathPart(separator));
                    }
                //The path already exists.
                if (!hadNonExistant)
                    throw new ArgumentException(string.Format("The provided path '{0}' already exists at this level.", path), "path");
                return (IIntermediateNamespaceDeclaration)parent;
            }
            else
            {
                if (this.ContainsKey(path))
                    throw new ArgumentException(string.Format("The provided path {0} already exists at this level.", path), "path");
                IIntermediateNamespaceDeclaration newNamespace = 
                    ctorService != null
                    ? ctorService.New(path, this.Parent)
                    : new IntermediateNamespaceDeclaration(path, this.Parent);
                this._Add(newNamespace.UniqueIdentifier, newNamespace);
                return newNamespace;
            }
        }

        public bool ContainsKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (key == string.Empty)
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.key, ExceptionMessageId.ArgumentCannotBeEmpty, ThrowHelper.GetArgumentName(ArgumentWithException.key));
            return this.ContainsKey(TypeSystemIdentifiers.GetDeclarationIdentifier(key));
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
            var pathVariants = path.NamespacePathBreakdown();
            if (this.Parent is INamespaceDeclaration)
                pathVariants = pathVariants.Except(((INamespaceDeclaration)(this.Parent)).FullName.NamespacePathBreakdown()).ToArray();
            IIntermediateNamespaceDictionary current = this;
            foreach (var currentPath in pathVariants)
            {
                if (!current.ContainsKey(currentPath))
                    return false;
                current = current[currentPath].Namespaces;
            }
            return true;
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

        #region IIntermediateNamespaceDictionary Members


        public int GetCountFor(IIntermediateNamespaceParent parent)
        {
            int result = 0;
            foreach (var element in this.Values)
                if (element.Parent == parent)
                    result++;
                else
                    foreach (var part in element.Parts)
                        if (part.Parent == parent)
                            result++;
            return result;
        }

        #endregion

        public override IEnumerable<KeyValuePair<IGeneralDeclarationUniqueIdentifier, IIntermediateNamespaceDeclaration>> ExclusivelyOnParent()
        {
            foreach (var nsKVP in this)
                if (nsKVP.Value.Parent == this.Parent)
                    yield return nsKVP;
                else if (nsKVP.Value.Parts.Count > 0)
                    foreach (var partial in nsKVP.Value.Parts)
                        if (partial.Parent == this.Parent)
                            yield return new KeyValuePair<IGeneralDeclarationUniqueIdentifier, IIntermediateNamespaceDeclaration>(nsKVP.Key, partial);
        }
    }
}
