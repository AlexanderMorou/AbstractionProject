using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Ast;
using AllenCopeland.Abstraction.Utilities.Collections;
/*---------------------------------------------------------------------\
| Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Ast
{
    /* *
     * Provides a base collection for a set of metadata in relationship to an intermediate 
     * type.
     * */
    internal partial class IntermediateTypeMetadataCollection :
        IControlledCollection<IMetadatum>,
        IMetadataCollection,
        IControlledCollection
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
        /// Creates a new <see cref="IntermediateTypeMetadataCollection"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IIntermediateType"/> which needs
        /// a merged series of custom attributes.</param>
        public IntermediateTypeMetadataCollection(IIntermediateType parent)
        {
            this.parent = parent;
        }

        #region IMetadataCollection Members
        public bool Contains(IType metadatumType)
        {
            return ((IEnumerable<IMetadatum>)(this)).Any(i => i.Type.IsAssignableFrom(metadatumType));
        }

        public IMetadataEntity Parent
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

        #region IControlledCollection<IMetadatum> Members

        public int Count
        {
            get { return this.Count(); }
        }

        public bool Contains(IMetadatum item)
        {
            return ((IEnumerable<IMetadatum>)(this)).Contains(item);
        }

        public void CopyTo(IMetadatum[] array, int arrayIndex = 0)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }

        public IMetadatum this[int index]
        {
            get
            {
                int i = 0;
                if (index < 0)
                    throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
                foreach (var item in this)
                    if (i == index)
                        return item;
                //index > Count
                throw ThrowHelper.ObtainArgumentOutOfRangeException(ArgumentWithException.index);
            }
        }

        public IMetadatum[] ToArray()
        {
            return new List<IMetadatum>(this).ToArray();
        }

        public int IndexOf(IMetadatum element)
        {
            return this.GetIndexOf(element);
        }

        #endregion

        #region IEnumerable<IMetadatum> Members

        /* *
         * To note: this doesn't consider types that are neither
         * compiled nor a part of the intermediate type system.
         * */
        public IEnumerator<IMetadatum> GetEnumerator()
        {
            return new Enumerator(this.parent).GetEnumerator();
            //HashList<ICompiledType> noMultipleEncounters = new HashList<ICompiledType>();
            //HashList<IIntermediateType> noMultipleEncountersInter = new HashList<IIntermediateType>();
            //ICompiledClassType attrUType = (ICompiledClassType)typeof(AttributeUsageAttribute).GetTypeReference();
            //for (IType current = this.parent; current != null; current = current.BaseType)
            //    if (current is IIntermediateType)
            //    {
            //        foreach (var item in ((IIntermediateType)(current)).Metadata.Flatten())
            //            if (current == this.parent)
            //                yield return item;
            //            else if (CheckAttribute(noMultipleEncounters, noMultipleEncountersInter, attrUType, item))
            //                yield return item;
            //    }
            //    else if (current is ICompiledType)
            //    {
            //        foreach (var item in current.Metadata)
            //            if (CheckAttribute(noMultipleEncounters, noMultipleEncountersInter, attrUType, item))
            //                yield return item;
            //        /* *
            //         * Custom Attributes of a compiled type are all inclusive, so it's not
            //         * necessary to delve further.
            //         * */
            //        break;
            //    }
            //attrUType = null;
            //noMultipleEncounters.Clear();
            //noMultipleEncountersInter.Clear();
            //noMultipleEncounters = null;
            //noMultipleEncountersInter = null;
            //yield break;
        }
        /* *
         * Determines whether an attribute, based upon the current hierarchy structure of the 
         * parent, should be included in the series.
         * */
        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IControlledCollection Members

        int IControlledCollection.Count
        {
            get { return this.Count; }
        }

        bool IControlledCollection.Contains(object item)
        {
            if (item is IType)
                return this.Contains((IType)item);
            else if (item is IMetadatum)
                return this.Contains((IMetadatum)item);
            return false;
        }

        void IControlledCollection.CopyTo(Array array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            IMetadatum[] insts = this.ToArray();
            if (insts.Length + arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");
            for (int i = 0; i < insts.Length; i++)
                array.SetValue(insts[i], i + arrayIndex);

        }

        object IControlledCollection.this[int index]
        {
            get
            {
                return this[index];
            }
        }

        int IControlledCollection.IndexOf(object element)
        {
            if (element is IMetadatum)
                return this.IndexOf((IMetadatum)element);
            return -1;
        }

        #endregion

        #region IMetadataCollection Members

        /// <summary>
        /// Returns the <see cref="IMetadatum"/> of the
        /// <paramref name="metadatumType"/> provided.
        /// </summary>
        /// <param name="metadatumType">The <see cref="IType"/>
        /// of the metadatum to return the <see cref="IMetadatum"/>
        /// of.</param>
        /// <returns>The <see cref="IMetadatum"/> of the
        /// <paramref name="metadatumType"/> provided.</returns>
        public IMetadatum this[IType metadatumType]
        {
            get
            {
                IMetadatum closeMatch = null;
                foreach (var item in this)
                    if (item.Type.Equals(metadatumType))
                        return item;
                    else if (metadatumType.IsAssignableFrom(item.Type))
                        closeMatch = item;
                return closeMatch;
            }
        }

        #endregion


    }
}
