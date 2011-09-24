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
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    public partial class ImplementedInterfacesDictionary<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType> :
        ControlledStateDictionary<IInterfaceType, IIntermediateInterfaceMemberMapping<TEvent, IInterfaceEventMember, TIntermediateEvent, IIntermediateInterfaceEventMember, TIndexer, IInterfaceIndexerMember, TIntermediateIndexer, IIntermediateInterfaceIndexerMember, TMethod, IInterfaceMethodMember, TIntermediateMethod, IIntermediateInterfaceMethodMember, TProperty, IInterfacePropertyMember, TIntermediateProperty, IIntermediateInterfacePropertyMember, TType, IInterfaceType, TIntermediateType, IIntermediateInterfaceType>>,
        IIntermediateInstantiableTypeImplementedInterfaces<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>,
        ITypeCollectionBase
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
        private TIntermediateType parent;
        private ITypeCollection copy;
        private LockedVariant locked;
        internal ImplementedInterfacesDictionary(TIntermediateType parent)
        {
            this.parent = parent;
        }

        #region IIntermediateInstantiableTypeImplementedInterfaces<TCtor,TIntermediateCtor,TEvent,TIntermediateEvent,TField,TIntermediateField,TIndexer,TIntermediateIndexer,TMethod,TIntermediateMethod,TProperty,TIntermediateProperty,TType,TIntermediateType> Members

        public void ImplementInterfaceQuick(IInterfaceType @interface)
        {
            this.Copy.Add(@interface);
        }
        public IIntermediateInterfaceMemberMapping<TEvent, IInterfaceEventMember, TIntermediateEvent, IIntermediateInterfaceEventMember, TIndexer, IInterfaceIndexerMember, TIntermediateIndexer, IIntermediateInterfaceIndexerMember, TMethod, IInterfaceMethodMember, TIntermediateMethod, IIntermediateInterfaceMethodMember, TProperty, IInterfacePropertyMember, TIntermediateProperty, IIntermediateInterfacePropertyMember, TType, IInterfaceType, TIntermediateType, IIntermediateInterfaceType> ImplementInterface(IInterfaceType @interface, bool insertPlaceholders = false)
        {
            throw new NotImplementedException();
        }

        #endregion
        
        private ITypeCollection Copy
        {
            get
            {
                if (this.copy == null)
                    this.copy = new TypeCollection();
                return this.copy;
            }
        }
        #region ITypeCollectionBase Members

        public int IndexOf(IType item)
        {
            return this.GetEnumerableInternal().GetIndexOf(item);
        }

        #endregion

        #region IControlledStateCollection<IType> Members

        int IControlledStateCollection<IType>.Count
        {
            get
            {
                return this.GetEnumerableInternal().Count();
            }
        }

        bool IControlledStateCollection<IType>.Contains(IType item)
        {
            return this.GetEnumerableInternal().Contains(item);
        }

        void IControlledStateCollection<IType>.CopyTo(IType[] array, int arrayIndex)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }

        IType IControlledStateCollection<IType>.this[int index]
        {
            get
            {
                return GetEnumerableInternal().ElementAt(index);
            }
        }

        IType[] IControlledStateCollection<IType>.ToArray()
        {
            return GetEnumerableInternal().ToArray();
        }

        #endregion

        #region IEnumerable<IType> Members

        public new IEnumerator<IType> GetEnumerator()
        {
            return GetEnumerableInternal().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        private IEnumerable<IType> GetEnumerableInternal()
        {
            HashSet<IType> yielded = new HashSet<IType>();
            var grandParent = parent.BaseType;
            if (grandParent != null)
                foreach (var type in grandParent.ImplementedInterfaces)
                    if (yielded.Add(type))
                        yield return type;
            foreach (var type in this.Copy)
                if (yielded.Add(type))
                {
                    yield return type;
                    foreach (var subType in type.ImplementedInterfaces)
                        if (yielded.Add(subType))
                            yield return subType;
                }
        }

        internal LockedVariant GetLocked()
        {
            if (this.locked == null)
                this.locked = new LockedVariant(this);
            return this.locked;
        }

        #region IEquatable<ITypeCollectionBase> Members

        public bool Equals(ITypeCollectionBase other)
        {
            if (other == null)
                return false;
            if (object.ReferenceEquals(this, other))
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
    public partial class ImplementedInterfacesCollection :
        ITypeCollectionBase
    {
        private ITypeCollection copy;
        private LockedVariant locked;
        private IType parent;

        /// <summary>
        /// Creates a new ImplementedInterfacesCollection with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <see cref="IntermediateGenericSegmentableInstantiableType{TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType}"/>
        /// which owns the <see cref="ImplementedInterfacesCollection"/></param>
        protected internal ImplementedInterfacesCollection(IType parent)
        {
            this.parent = parent;
        }

        private ITypeCollection Copy
        {
            get
            {
                if (this.copy == null)
                    this.copy = new TypeCollection();
                return this.copy;
            }
        }

        #region ITypeCollectionBase Members

        public int IndexOf(IType item)
        {
            return this.GetEnumerableInternal().GetIndexOf(item);
        }

        #endregion

        #region IControlledStateCollection<IType> Members

        public int Count
        {
            get
            {
                return this.GetEnumerableInternal().Count();
            }
        }

        public bool Contains(IType item)
        {
            return this.GetEnumerableInternal().Contains(item);
        }

        void IControlledStateCollection<IType>.CopyTo(IType[] array, int arrayIndex)
        {
            this.ToArray().CopyTo(array, arrayIndex);
        }

        IType IControlledStateCollection<IType>.this[int index]
        {
            get
            {
                return GetEnumerableInternal().ElementAt(index);
            }
        }

        IType[] IControlledStateCollection<IType>.ToArray()
        {
            return GetEnumerableInternal().ToArray();
        }

        #endregion

        #region IEnumerable<IType> Members

        public IEnumerator<IType> GetEnumerator()
        {
            return GetEnumerableInternal().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        private IEnumerable<IType> GetEnumerableInternal()
        {
            HashSet<IType> yielded = new HashSet<IType>();
            var grandParent = parent.BaseType;
            if (grandParent != null)
                foreach (var type in grandParent.ImplementedInterfaces)
                    if (yielded.Add(type))
                        yield return type;
            foreach (var type in this.Copy)
                if (yielded.Add(type))
                {
                    yield return type;
                    foreach (var subType in type.ImplementedInterfaces)
                        if (yielded.Add(subType))
                            yield return subType;
                }
        }

        internal LockedVariant GetLocked()
        {
            if (this.locked == null)
                this.locked = new LockedVariant(this);
            return this.locked;
        }

        #region IEquatable<ITypeCollectionBase> Members

        public bool Equals(ITypeCollectionBase other)
        {
            if (other == null)
                return false;
            if (object.ReferenceEquals(this, other))
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
