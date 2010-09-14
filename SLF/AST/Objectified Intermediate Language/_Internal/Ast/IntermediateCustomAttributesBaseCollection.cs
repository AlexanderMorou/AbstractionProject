using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Oil;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    /* *
     * Provides a base collection for custom attributes in relationship to an intermediate class
     * type.
     * */
    internal class IntermediateCustomAttributesBaseCollection :
        IReadOnlyCollection<ICustomAttributeInstance>,
        ICustomAttributeCollection,
        IReadOnlyCollection
    {
        /* *
         * No fancy trickery here, just brute force.
         * The parent is malleable and can change at any moment, its hierarchy is 
         * much the same.
         * *
         * Because of this, it's not practical to cache information about the parent
         * and its attributes.
         * */
        private IIntermediateType parent;
        /// <summary>
        /// Creates a new <see cref="IntermediateCustomAttributesBaseCollection"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateType"/> which needs
        /// a merged series of custom attributes.</param>
        public IntermediateCustomAttributesBaseCollection(IIntermediateType parent)
        {
            this.parent = parent;
        }

        #region ICustomAttributeCollection Members
        public bool Contains(IType attributeType)
        {
            return ((IEnumerable<ICustomAttributeInstance>)(this)).Any(i => i.Type.IsAssignableFrom(attributeType));
        }

        public ICustomAttributedDeclaration Parent
        {
            get { return parent; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.parent = null;
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IControlledStateCollection<ICustomAttributeInstance> Members

        public int Count
        {
            get { return this.Count(); }
        }

        public bool Contains(ICustomAttributeInstance item)
        {
            return ((IEnumerable<ICustomAttributeInstance>)(this)).Contains(item);
        }

        public void CopyTo(ICustomAttributeInstance[] array, int arrayIndex)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }

        public ICustomAttributeInstance this[int index]
        {
            get {
                int i = 0;
                if (index < 0)
                    throw new ArgumentOutOfRangeException("index");
                foreach (var item in this)
                    if (i == index)
                        return item;
                //index > Count
                throw new ArgumentOutOfRangeException("index");
            }
        }

        public ICustomAttributeInstance[] ToArray()
        {
            return new List<ICustomAttributeInstance>(this).ToArray();
        }

        public int IndexOf(ICustomAttributeInstance element)
        {
            return this.GetIndexOf(element);
        }

        #endregion

        #region IEnumerable<ICustomAttributeInstance> Members
        /* *
         * To note: this doesn't consider types that are neither
         * compiled nor a part of the intermediate system built.
         * */
        public IEnumerator<ICustomAttributeInstance> GetEnumerator()
        {
            HashList<ICompiledType> noMultipleEncounters = new HashList<ICompiledType>();
            HashList<IIntermediateType> noMultipleEncountersInter = new HashList<IIntermediateType>();
            ICompiledClassType attrUType = (ICompiledClassType)typeof(AttributeUsageAttribute).GetTypeReference();
            for (IType current = this.parent; current != null; current = current.BaseType)
                if (current is IIntermediateType)
                {
                    foreach (var item in ((IIntermediateType)(current)).CustomAttributes.Flatten())
                        if (current == this.parent)
                            yield return item;
                        else if (CheckAttribute(noMultipleEncounters, noMultipleEncountersInter, attrUType, item))
                            yield return item;
                }
                else if (current is ICompiledType)
                {
                    foreach (var item in current.CustomAttributes)
                        if (CheckAttribute(noMultipleEncounters, noMultipleEncountersInter, attrUType, item))
                            yield return item;
                    /* *
                     * Custom Attributes of a compiled type are all inclusive, so it's not
                     * necessary to delve further.
                     * */
                    break;
                }
            attrUType = null;
            noMultipleEncounters.Clear();
            noMultipleEncountersInter.Clear();
            noMultipleEncounters = null;
            noMultipleEncountersInter = null;
            yield break;
        }
        /* *
         * Determines whether an attribute, based upon the current hierarchy structure of the 
         * parent, should be included in the series.
         * */
        private bool CheckAttribute(HashList<ICompiledType> noMultipleEncounters, HashList<IIntermediateType> noMultipleEncountersInter, ICompiledClassType attrUType, ICustomAttributeInstance item)
        {
            ICompiledType k = null;
            if (item.Type is ICompiledType && (!(noMultipleEncounters.Contains(k = (ICompiledType)item.Type))))
            {
                /* *
                 * If it's a compiled attribute, this is eaiser.
                 * Use the method compiled classes uses to determine attribute usage
                 * and use that to determine inclusion.
                 * */
                AttributeUsage kUsage = k.UnderlyingSystemType.GetAttributeUsage();
                if (!kUsage.Inherited)
                    return false;
                if (!kUsage.AllowMultiple)
                    noMultipleEncounters.Add(k);
            }
            else
            {
                IIntermediateType l = null;
                if (item.Type is IIntermediateType && (!(noMultipleEncountersInter.Contains(l = (IIntermediateType)item.Type))))
                {
                    IType attrType = l;
                    bool attrTypeIsIntermediate = false;
                    IIntermediateType attrTypeI = null;
                    /* *
                     * The scan is ugly, but necessary.
                     * *
                     * The intermediate form of attributes doesn't merge the series
                     * for simplicity reasons.  Anyone wanting the merged version gets
                     * this set, so this set can't utilize itself to do the scan.
                     * *
                     * Would result in a StackOverflow in cases where the attribute 
                     * is applied to itself or one of its children.
                     * */
                    for (; attrType != null; attrType = attrType.BaseType)
                        if (attrTypeIsIntermediate = (attrType is IIntermediateType))
                            (attrTypeI = ((IIntermediateType)(attrType))).CustomAttributes.Contains(attrUType);
                        else if (attrType is ICompiledType)
                        {
                            attrTypeI = null;
                            /* *
                             * Compiled types can't very well derive from an intermediate type,
                             * since they're not compiled.
                             * *
                             * All compiled type attributes contain an AttributeUsageAttribute.  
                             * Since it's assigned to the base type of all attributes: Attribute.
                             * */
                            break;
                        }
                    AttributeUsage lUsage = new AttributeUsage((attrTypeIsIntermediate ? (attrTypeI.CustomAttributes[typeof(AttributeUsageAttribute).GetTypeReference()].WrappedAttribute) : (attrType.CustomAttributes[typeof(AttributeUsageAttribute).GetTypeReference()].WrappedAttribute)) as AttributeUsageAttribute);
                    if (!lUsage.Inherited)
                        return false;
                    if (!lUsage.AllowMultiple)
                        noMultipleEncountersInter.Add(l);
                }
            }
            return true;
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IControlledStateCollection Members

        int IControlledStateCollection.Count
        {
            get { return this.Count; }
        }

        bool IControlledStateCollection.Contains(object item)
        {
            if (item is IType)
                return this.Contains((IType)item);
            else if (item is ICustomAttributeInstance)
                return this.Contains((ICustomAttributeInstance)item);
            return false;
        }

        void IControlledStateCollection.CopyTo(Array array, int arrayIndex)
        {
            if (array == null) 
                throw new ArgumentNullException("array");

            ICustomAttributeInstance[] insts = this.ToArray();
            if (insts.Length + arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            for (int i = 0; i < insts.Length; i++)
                array.SetValue(insts[i], i + arrayIndex);
            
        }

        object IControlledStateCollection.this[int index]
        {
            get {
                return this[index];
            }
        }

        int IControlledStateCollection.IndexOf(object element)
        {
            if (element is ICustomAttributeInstance)
                return this.IndexOf((ICustomAttributeInstance)element);
            return -1;
        }

        #endregion

        #region ICollection Members

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            ((IControlledStateCollection)(this)).CopyTo(array, arrayIndex);
        }

        int ICollection.Count
        {
            get { return this.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return null; }
        }

        #endregion

        #region ICustomAttributeCollection Members


        public ICustomAttributeInstance this[IType attributeType]
        {
            get {
                ICustomAttributeInstance closeMatch = null;
                foreach (var item in this)
                    if (item.Type.Equals(attributeType))
                        return item;
                    else if (attributeType.IsAssignableFrom(item.Type))
                        closeMatch=item;
                return closeMatch;
            }
        }

        #endregion


    }
}
