using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2011 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public partial class ImplementedInterfacesDictionary<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>
        where TCtor :
            IConstructorMember<TCtor, TType>
        where TIntermediateCtor :
            TCtor,
            IIntermediateConstructorMember<TCtor, TIntermediateCtor, TType, TIntermediateType>
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            TEvent,
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            TField,
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>
        where TType :
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TIntermediateType :
            TType,
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>
    {
        internal class LockedVariant :
            ILockedTypeCollection
        {
            private ImplementedInterfacesDictionary<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> parent;

            public LockedVariant(ImplementedInterfacesDictionary<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> parent)
            {
                this.parent = parent;
            }

            #region ILockedTypeCollection Members

            public bool IsDisposed
            {
                get { return this.parent == null; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.parent = null;
            }

            #endregion

            #region ITypeCollectionBase Members

            public int IndexOf(IType item)
            {
                return this.parent.IndexOf(item);
            }

            #endregion

            #region IControlledStateCollection<IType> Members

            public int Count
            {
                get { return this.parent.Count; }
            }

            public bool Contains(IType item)
            {
                return this.parent.Contains(item);
            }

            public void CopyTo(IType[] array, int arrayIndex = 0)
            {
                ((ITypeCollectionBase)this.parent).CopyTo(array, arrayIndex);
            }

            public IType this[int index]
            {
                get { return ((ITypeCollectionBase)this.parent)[index]; }
            }

            public IType[] ToArray()
            {
                return ((ITypeCollectionBase)this.parent).ToArray();
            }

            #endregion

            #region IEnumerable<IType> Members

            public IEnumerator<IType> GetEnumerator()
            {
                return ((ITypeCollectionBase)this.parent).GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
        }

    }
    partial class ImplementedInterfacesCollection
    {
        internal class LockedVariant :
            ILockedTypeCollection
        {
            private ImplementedInterfacesCollection parent;

            public LockedVariant(ImplementedInterfacesCollection parent)
            {
                this.parent = parent;
            }

            #region ILockedTypeCollection Members

            public bool IsDisposed
            {
                get { return this.parent == null; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.parent = null;
            }

            #endregion

            #region ITypeCollectionBase Members

            public int IndexOf(IType item)
            {
                return this.parent.IndexOf(item);
            }

            #endregion

            #region IControlledStateCollection<IType> Members

            public int Count
            {
                get { return this.parent.Count; }
            }

            public bool Contains(IType item)
            {
                return this.parent.Contains(item);
            }

            public void CopyTo(IType[] array, int arrayIndex = 0)
            {
                ((ITypeCollectionBase)this.parent).CopyTo(array, arrayIndex);
            }

            public IType this[int index]
            {
                get { return ((ITypeCollectionBase)this.parent)[index]; }
            }

            public IType[] ToArray()
            {
                return ((ITypeCollectionBase)this.parent).ToArray();
            }

            #endregion

            #region IEnumerable<IType> Members

            public IEnumerator<IType> GetEnumerator()
            {
                return ((ITypeCollectionBase)this.parent).GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion
        }
    }
}