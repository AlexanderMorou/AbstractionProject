using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Modules;
using AllenCopeland.Abstraction.Utilities.Collections;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Modules
{
    [DebuggerDisplay("Modules: {Count}")]
    public partial class IntermediateModuleDictionary :
        ControlledDictionary<IGeneralDeclarationUniqueIdentifier, IIntermediateModule>,
        IIntermediateModuleDictionary,
        IModuleDictionary
    {

        private IIntermediateAssembly assembly;
        private AbstractValueCollection abstractValueCollection;
        #region IIntermediateModuleDictionary Members

        public IIntermediateModule Add(string moduleName)
        {
            var result = new IntermediateModule(moduleName, this.assembly);
            this._Add(result.UniqueIdentifier, result);
            return result;
        }

        #endregion

        internal IntermediateModuleDictionary(IIntermediateAssembly assembly)
        {
            this.assembly = assembly;
            this.Add("RootModule");
        }

        #region IModuleDictionary Members

        IAssembly IModuleDictionary.Parent
        {
            get { return this.assembly; }
        }

        #endregion


        #region IControlledDictionary<IGeneralDeclarationUniqueIdentifier,IModule> Members

        IControlledCollection<IGeneralDeclarationUniqueIdentifier> IControlledDictionary<IGeneralDeclarationUniqueIdentifier, IModule>.Keys
        {
            get { return this.Keys; }
        }
        IControlledCollection<IModule> IControlledDictionary<IGeneralDeclarationUniqueIdentifier, IModule>.Values
        {
            get {
                if (abstractValueCollection == null)
                    abstractValueCollection = new AbstractValueCollection(this);
                return this.abstractValueCollection;
            }
        }

        IModule IControlledDictionary<IGeneralDeclarationUniqueIdentifier,IModule>.this[IGeneralDeclarationUniqueIdentifier key]
        {
            get { return this[key]; }
        }

        bool IControlledDictionary<IGeneralDeclarationUniqueIdentifier,IModule>.TryGetValue(IGeneralDeclarationUniqueIdentifier key, out IModule value)
        {
            IIntermediateModule iValue;
            this.TryGetValue(key, out iValue);
            value = iValue;
            return iValue != null;
        }

        #endregion

        #region IControlledCollection<KeyValuePair<IGeneralDeclarationUniqueIdentifier,IModule>> Members


        bool IControlledCollection<KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>>.Contains(KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule> item)
        {
            throw new NotImplementedException();
        }

        void IControlledCollection<KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>>.CopyTo(KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            ((IControlledDictionary<IGeneralDeclarationUniqueIdentifier, IModule>)(this)).ToArray().CopyTo(array, arrayIndex);
        }

        KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule> IControlledCollection<KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>>.this[int index]
        {
            get {
                var currentElement = this[index];
                return new KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>(currentElement.Key, currentElement.Value);
            }
        }

        KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>[] IControlledCollection<KeyValuePair<IGeneralDeclarationUniqueIdentifier,IModule>>.ToArray()
        {
            KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>[] result = new KeyValuePair<IGeneralDeclarationUniqueIdentifier,IModule>[this.Count];
            var currentSet = this.ToArray();
            for (int i = 0; i < this.Count; i++)
                result[i] = new KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>(currentSet[i].Key, currentSet[i].Value);
            return result;
        }

        int IControlledCollection<KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>>.IndexOf(KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule> element)
        {
            int kIndex = this.Keys.IndexOf(element.Key);
            if (kIndex == -1)
                return -1;
            if (this.Values[kIndex] == element.Value)
                return kIndex;
            return -1;
        }

        #endregion

        #region IEnumerable<KeyValuePair<IGeneralDeclarationUniqueIdentifier,IModule>> Members

        IEnumerator<KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>> IEnumerable<KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>>.GetEnumerator()
        {
            foreach (var kvp in this)
                yield return new KeyValuePair<IGeneralDeclarationUniqueIdentifier, IModule>(kvp.Key, kvp.Value);
        }

        #endregion
    }
}
