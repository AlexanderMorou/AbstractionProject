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

namespace AllenCopeland.Abstraction.Slf.Oil.Modules
{
    [DebuggerDisplay("Modules: {Count}")]
    public partial class IntermediateModuleDictionary :
        ControlledStateDictionary<string, IIntermediateModule>,
        IIntermediateModuleDictionary,
        IModuleDictionary
    {

        private IIntermediateAssembly assembly;
        private AbstractValueCollection abstractValueCollection;
        #region IIntermediateModuleDictionary Members

        public IIntermediateModule Add(string moduleName)
        {
            var result = new IntermediateModule(moduleName, this.assembly);
            this._Add(moduleName, result);
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


        #region IControlledStateDictionary<string,IModule> Members

        IControlledStateCollection<string> IControlledStateDictionary<string, IModule>.Keys
        {
            get { return this.Keys; }
        }
        IControlledStateCollection<IModule> IControlledStateDictionary<string, IModule>.Values
        {
            get {
                if (abstractValueCollection == null)
                    abstractValueCollection = new AbstractValueCollection(this);
                return this.abstractValueCollection;
            }
        }

        IModule IControlledStateDictionary<string,IModule>.this[string key]
        {
            get { return this[key]; }
        }

        bool IControlledStateDictionary<string,IModule>.TryGetValue(string key, out IModule value)
        {
            IIntermediateModule iValue;
            this.TryGetValue(key, out iValue);
            value = iValue;
            return iValue != null;
        }

        #endregion

        #region IControlledStateCollection<KeyValuePair<string,IModule>> Members


        bool IControlledStateCollection<KeyValuePair<string, IModule>>.Contains(KeyValuePair<string, IModule> item)
        {
            throw new NotImplementedException();
        }

        void IControlledStateCollection<KeyValuePair<string, IModule>>.CopyTo(KeyValuePair<string, IModule>[] array, int arrayIndex)
        {
            if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            ((IControlledStateDictionary<string, IModule>)(this)).ToArray().CopyTo(array, arrayIndex);
        }

        KeyValuePair<string, IModule> IControlledStateCollection<KeyValuePair<string, IModule>>.this[int index]
        {
            get {
                var currentElement = this[index];
                return new KeyValuePair<string, IModule>(currentElement.Key, currentElement.Value);
            }
        }

        KeyValuePair<string, IModule>[] IControlledStateCollection<KeyValuePair<string,IModule>>.ToArray()
        {
            KeyValuePair<string, IModule>[] result = new KeyValuePair<string,IModule>[this.Count];
            var currentSet = this.ToArray();
            for (int i = 0; i < this.Count; i++)
                result[i] = new KeyValuePair<string, IModule>(currentSet[i].Key, currentSet[i].Value);
            return result;
        }

        int IControlledStateCollection<KeyValuePair<string, IModule>>.IndexOf(KeyValuePair<string, IModule> element)
        {
            int kIndex = this.Keys.IndexOf(element.Key);
            if (kIndex == -1)
                return -1;
            if (this.Values[kIndex] == element.Value)
                return kIndex;
            return -1;
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,IModule>> Members

        IEnumerator<KeyValuePair<string, IModule>> IEnumerable<KeyValuePair<string, IModule>>.GetEnumerator()
        {
            foreach (var kvp in this)
                yield return new KeyValuePair<string, IModule>(kvp.Key, kvp.Value);
        }

        #endregion
    }
}
