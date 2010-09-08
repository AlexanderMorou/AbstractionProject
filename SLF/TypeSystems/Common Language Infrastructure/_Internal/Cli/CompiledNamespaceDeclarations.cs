using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    /// <summary>
    /// Provides an internal namespaces series for compiled 
    /// namespace parent, such as another compiled namespace 
    /// or a compiled assembly.
    /// </summary>
    internal partial class CompiledNamespaceDeclarations :
        NamespaceCollectionBase,
        _ICompiledNamespaceDeclarations
    {
        private string[] baseData;
        /// <summary>
        /// Creates a new <see cref="CompiledNamespaceDeclarations"/>
        /// instance with the <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="parent">The <see cref="_ICompiledNamespaceParent"/>
        /// which contains the <see cref="CompiledNamespaceDeclarations"/>.</param>
        public  CompiledNamespaceDeclarations(_ICompiledNamespaceParent parent)
            : base(parent)
        {
            baseData = parent.NamespaceNames.ToArray();
        }

        #region _ICompiledNamespaceDeclarations Members

        public new _ICompiledNamespaceParent Parent
        {
            get { return (_ICompiledNamespaceParent)base.Parent; }
        }

        #endregion

        public override bool Contains(KeyValuePair<string, INamespaceDeclaration> item)
        {
            if (this.Keys.Contains(item.Key))
                return this.Values.Contains(item.Value);
            return false;
        }

        public override int Count
        {
            get
            {
                return this.baseData.Length;
            }
        }

        protected override ControlledStateDictionary<string, INamespaceDeclaration>.KeysCollection InitializeKeysCollection()
        {
            return new _KeysCollection(this);
        }
        protected override ControlledStateDictionary<string, INamespaceDeclaration>.ValuesCollection InitializeValuesCollection()
        {
            return new _ValuesCollection(this);
        }

        public override IEnumerator<KeyValuePair<string, INamespaceDeclaration>> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
                yield return new KeyValuePair<string, INamespaceDeclaration>(this.Keys[i], this.Values[i]);
            yield break;
        }

        public override void Dispose()
        {
            if (this.valuesInstance != null)
                ((_ValuesCollection)this.Values).Dispose();
            this.baseData = null;
            base.Dispose();
        }
        public override void CopyTo(KeyValuePair<string, INamespaceDeclaration>[] array, int arrayIndex)
        {
            for (int i = 0; i < this.Count; i++)
                array[i + arrayIndex] = new KeyValuePair<string, INamespaceDeclaration>(this.Keys[i], this.Values[i]);
            base.CopyTo(array, arrayIndex);
        }

        protected override INamespaceDeclaration OnGetThis(string key)
        {
            return this.GetThis(key);
        }

        protected override KeyValuePair<string, INamespaceDeclaration> OnGetThis(int index)
        {
            return new KeyValuePair<string, INamespaceDeclaration>(this.Keys[index], this.Values[index]);
        }

        public override KeyValuePair<string, INamespaceDeclaration>[] ToArray()
        {
            var result = new KeyValuePair<string, INamespaceDeclaration>[this.Count];
            for (int i = 0; i < this.Count; i++)
                result[i] = new KeyValuePair<string, INamespaceDeclaration>(this.Keys[i], this.Values[i]);
            return result;
        }

        public override bool ContainsKey(string key)
        {
            return this.Keys.Contains(key);
        }
    }
}
