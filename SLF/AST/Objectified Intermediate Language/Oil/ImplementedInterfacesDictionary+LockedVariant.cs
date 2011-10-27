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
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public partial class ImplementedInterfacesDictionary<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>
        where TEvent :
            IEventMember<TEvent, TType>
        where TIntermediateEvent :
            IIntermediateEventMember<TEvent, TIntermediateEvent, TType, TIntermediateType>,
            TEvent
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>,
            TIndexer
        where TMethod :
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>,
            TMethod
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TProperty
        where TType :
            IEventParent<TEvent, TType>,
            IIndexerParent<TIndexer, TType>,
            IMethodParent<TMethod, TType>,
            IPropertyParent<TProperty, TType>
        where TIntermediateType :
            IIntermediateEventParent<TEvent, TIntermediateEvent, TType, TIntermediateType>,
            IIntermediateIndexerParent<TIndexer, TIntermediateIndexer, TType, TIntermediateType>,
            IIntermediateMethodParent<TMethod, TIntermediateMethod, TType, TIntermediateType>,
            IIntermediatePropertyParent<TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TType
    {
        internal class LockedVariant :
            ILockedTypeCollection
        {

            public LockedVariant(ImplementedInterfacesDictionary<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> parent)
            {
                this.Parent = parent;
            }

            public ImplementedInterfacesDictionary<TEvent, TIntermediateEvent, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> Parent { get; private set; }

            #region ILockedTypeCollection Members

            public bool IsDisposed
            {
                get { return this.Parent == null; }
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

            #region IEquatable<ITypeCollectionBase> Members

            public bool Equals(ITypeCollectionBase other)
            {
                if (other == null)
                    return false;
                if (object.ReferenceEquals(other, this))
                    return true;
                return this.SequenceEqual(other);
            }

            #endregion

            public override bool Equals(object obj)
            {
                if (obj is ITypeCollectionBase)
                    return this.Equals((ITypeCollectionBase)(obj));
                return false;
            }

            public override int GetHashCode()
            {
                return this.Count.GetHashCode();
            }
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

            #region IEquatable<ITypeCollectionBase> Members

            public bool Equals(ITypeCollectionBase other)
            {
                if (other == null)
                    return false;
                if (object.ReferenceEquals(other, this))
                    return true;
                return this.SequenceEqual(other);
            }

            #endregion

            public override bool Equals(object obj)
            {
                if (obj is ITypeCollectionBase)
                    return this.Equals((ITypeCollectionBase)(obj));
                return false;
            }

            public override int GetHashCode()
            {
                return this.Count.GetHashCode();
            }
        }
    }
}