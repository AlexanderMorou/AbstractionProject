using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>
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
        where TIntermediateEventMethod :
            class,
            TIntermediateMethod,
            IIntermediateEventMethodMember
        where TField :
            IFieldMember<TField, TType>,
            IInstanceMember
        where TIntermediateField :
            TField,
            IIntermediateFieldMember<TField, TIntermediateField, TType, TIntermediateType>,
            IIntermediateInstanceMember
        where TIndexer :
            IIndexerMember<TIndexer, TType>
        where TIntermediateIndexer :
            TIndexer,
            IIntermediateIndexerMember<TIndexer, TIntermediateIndexer, TType, TIntermediateType>
        where TIntermediateIndexerMethod :
            class,
            TMethod,
            IIntermediatePropertyMethodMember
        where TMethod :
            class,
            IMethodMember<TMethod, TType>,
            IExtendedInstanceMember
        where TIntermediateMethod :
            class,
            TMethod,
            IIntermediateMethodMember<TMethod, TIntermediateMethod, TType, TIntermediateType>
        where TProperty :
            IPropertyMember<TProperty, TType>
        where TIntermediateProperty :
            TProperty,
            IIntermediatePropertyMember<TProperty, TIntermediateProperty, TType, TIntermediateType>
        where TIntermediatePropertyMethod :
            class,
            TMethod,
            IIntermediatePropertyMethodMember
        where TType :
            class,
            IGenericType<TType>,
            IInstantiableType<TCtor, TEvent, TField, TIndexer, TMethod, TProperty, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TType, TIntermediateType>,
            IIntermediateSegmentableType<TType, TIntermediateType>,
            IIntermediateInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>,
            TType
        where TInstanceIntermediateType :
            IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType>,
            TIntermediateType
    {
        protected class ImplementedInterfacesDictionary :
            ControlledStateDictionary<IType, IIntermediateInterfaceMemberMapping<TEvent, IInterfaceEventMember, TIntermediateEvent,IIntermediateInterfaceEventMember, TIndexer, IInterfaceIndexerMember, TIntermediateIndexer, IIntermediateInterfaceIndexerMember, TMethod, IInterfaceMethodMember, TIntermediateMethod, IIntermediateInterfaceMethodMember, TProperty, IInterfacePropertyMember, TIntermediateProperty, IIntermediateInterfacePropertyMember, TType, IInterfaceType, TIntermediateType, IIntermediateInterfaceType>>,
            IIntermediateInstantiableTypeImplementedInterfaces<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TType, TIntermediateType>,
            ITypeCollectionBase
        {
            private ITypeCollection copy;
            private IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent;
            protected internal ImplementedInterfacesDictionary(IntermediateGenericSegmentableInstantiableType<TCtor, TIntermediateCtor, TEvent, TIntermediateEvent, TIntermediateEventMethod, TField, TIntermediateField, TIndexer, TIntermediateIndexer, TIntermediateIndexerMethod, TMethod, TIntermediateMethod, TProperty, TIntermediateProperty, TIntermediatePropertyMethod, TType, TIntermediateType, TInstanceIntermediateType> parent)
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
                return this.GetEnumerableFinal().GetIndexOf(item);
            }

            #endregion

            #region IControlledStateCollection<IType> Members

            int IControlledStateCollection<IType>.Count
            {
                get
                {
                    return this.GetEnumerableFinal().Count();
                }
            }

            bool IControlledStateCollection<IType>.Contains(IType item)
            {
                return this.GetEnumerableFinal().Contains(item);
            }

            void IControlledStateCollection<IType>.CopyTo(IType[] array, int arrayIndex)
            {
                this.ToArray().CopyTo(array, arrayIndex);
            }

            IType IControlledStateCollection<IType>.this[int index]
            {
                get {
                    return GetEnumerableFinal().ElementAt(index);
                }
            }

            IType[] IControlledStateCollection<IType>.ToArray()
            {
                return GetEnumerableFinal().ToArray();
            }

            #endregion

            #region IEnumerable<IType> Members

            IEnumerator<IType> IEnumerable<IType>.GetEnumerator()
            {
                return GetEnumerableFinal().GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            private IEnumerable<IType> GetEnumerableFinal()
            {
                return GetEnumerableInternal().Distinct();
            }

            private IEnumerable<IType> GetEnumerableInternal()
            {
                var grandParent = parent.BaseType;
                if (grandParent != null)
                    foreach (var type in grandParent.ImplementedInterfaces)
                        yield return type;
                foreach (var type in this.Copy)
                    yield return type;
            }

        }
    }
}
