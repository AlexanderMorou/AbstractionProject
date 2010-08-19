using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Ast;
using System.Diagnostics;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    [DebuggerDisplay("Namespaces: {Count}")]
    public class IntermediateNamespaceDictionary :
        IntermediateDeclarationDictionary<INamespaceDeclaration, IIntermediateNamespaceDeclaration>,
        IIntermediateNamespaceDictionary
    {
        private IIntermediateNamespaceParent parent;
        public IntermediateNamespaceDictionary(IIntermediateNamespaceParent parent) :
            base()
        {
            this.parent = parent;
        }
        public IntermediateNamespaceDictionary(IIntermediateNamespaceParent parent, IntermediateNamespaceDictionary toWrap) :
            base(toWrap)
        {
            this.parent = parent;
        }

        #region IIntermediateNamespaceDictionary Members

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
        /// <see cref="IIntermediateNamespaceDictionary"/>.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the namespace's
        /// fully qualified path.</param>
        /// <returns>A new <see cref="IIntermediateNamespaceDeclaration"/>
        /// instance that results from the operation.</returns>
        /// <remarks>The <paramref name="name"/> is segmented and delimited by periods (Full Stops, U+002E)
        /// which make up the invidual sub-namespaces of the <see cref="IIntermediateNamespaceDeclaration"/>
        /// that results.</remarks>
        /// <exception cref="System.ArgumentException"><paramref name="name"/> exists
        /// already; <paramref name="name"/> is <see cref="String.Empty"/>; or
        /// <paramref name="name"/> consists of periods (Full Stops, U+002E) only.</exception>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="name"/>
        /// is null.</exception>
        public IIntermediateNamespaceDeclaration Add(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (name == string.Empty)
                throw new ArgumentException("name");
            if (name.Contains("."))
            {
                string[] names = name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                //It was comprised of dots only.
                if (names.Length == 0)
                    throw new ArgumentException("name");
                if (names.Length == 1)
                    return this.Add(names[0]);
                IIntermediateNamespaceParent parent = this.Parent;
                bool hadNonExistant = false;
                for (int i = 0; i < names.Length; i++)
                    if (parent.Namespaces.ContainsKey(names[i]))
                        parent = parent.Namespaces[names[i]];
                    else
                    {
                        if (!hadNonExistant)
                            hadNonExistant = true;
                        parent = parent.Namespaces.Add(names[i]);
                    }
                //The path already exists.
                if (!hadNonExistant)
                    throw new ArgumentException("name");
                return (IIntermediateNamespaceDeclaration)parent;
            }
            else
            {
                if (this.ContainsKey(name))
                    throw new ArgumentException(string.Format("The provided name {0} already exists.", name),"name");
                IntermediateNamespaceDeclaration ind = new IntermediateNamespaceDeclaration(name, this.Parent);
                base.Add(name, ind);
                return ind;
            }
        }

        public override bool ContainsKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            if (key == string.Empty)
                throw new ArgumentException("key");
            if (!key.Contains("."))
                return base.ContainsKey(key);
            else
            {
                string[] keys = key.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (keys.Length == 0)
                    throw new ArgumentException("key");
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
            throw new NotImplementedException();
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

        #endregion
    }
}
