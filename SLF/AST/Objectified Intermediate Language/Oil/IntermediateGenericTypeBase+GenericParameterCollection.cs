using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Oil.Members;
using System.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil
{
    partial class IntermediateGenericTypeBase<TType, TIntermediateType>
        where TType :
            class,
            IGenericType<TType>
        where TIntermediateType :
            class,
            TType,
            IIntermediateGenericType<TType, TIntermediateType>
    {
        protected sealed class GenericParameterCollection :
            ILockedTypeCollection
        {
            private IntermediateGenericTypeBase<TType, TIntermediateType> owner;
            public GenericParameterCollection(IntermediateGenericTypeBase<TType, TIntermediateType> owner)
            {
                this.owner = owner;
            }

            #region ITypeCollectionBase Members

            public int IndexOf(IType item)
            {
                if (item is IGenericParameter)
                    return ((IGenericParameter)(item)).Position;
                return -1;
            }

            #endregion

            #region IControlledStateCollection<IType> Members

            public int Count
            {
                get
                {
                    if (this.owner.Parent is IGenericType)
                    {
                        var genericParent = ((IGenericType)(this.owner.Parent));
                        if (genericParent.IsGenericType)
                            return genericParent.GenericParameters.Count + this.owner.TypeParameters.Count;
                    }
                    return this.owner.TypeParameters.Count;
                }
            }

            public bool Contains(IType item)
            {
                if (item is IIntermediateGenericTypeParameter<TType, TIntermediateType>)
                    return this.owner.TypeParameters.Values.Contains(((IIntermediateGenericTypeParameter<TType, TIntermediateType>)(item)));
                return false;
            }

            public void CopyTo(IType[] array, int arrayIndex)
            {
                if (this.Count + arrayIndex >= array.Length)
                    throw new ArgumentException("array");
                for (int i = 0; i < this.Count; i++)
                    array[arrayIndex + i] = this[i];
            }

            public IType this[int index]
            {
                get {
                    if (this.owner.Parent is IGenericType)
                    {
                        var genericParent = ((IGenericType)(this.owner.Parent));
                        if (genericParent.IsGenericType)
                        {
                            var baseLine = genericParent.GenericParameters.Count;
                            if (index > baseLine)
                                return this.owner.TypeParameters.Values[index - baseLine];
                            else
                                return genericParent.GenericParameters[index];
                        }
                    }
                    return this.owner.TypeParameters.Values[index];
                }
            }

            public IType[] ToArray()
            {
                IType[] result = new IType[this.Count];
                if (this.owner.Parent is IGenericType)
                {
                    var genericParent = ((IGenericType)(this.owner.Parent));
                    if (genericParent.IsGenericType)
                    {
                        var baseLine = genericParent.GenericParameters.Count;
                        this.owner.GenericParameters.CopyTo(result, 0);
                        int count = this.Count;
                        for (int i = baseLine; i < count; i++)
                            result[i] = this.owner.TypeParameters.Values[i];
                    }
                }
                else
                    for (int i = 0; i < this.Count; i++)
                        result[i] = this.owner.TypeParameters.Values[i];
                return result;
            }

            #endregion

            #region IEnumerable<IType> Members

            public IEnumerator<IType> GetEnumerator()
            {
                if (this.owner.Parent is IGenericType)
                {
                    var genericParent = ((IGenericType)(this.owner.Parent));
                    if (genericParent.IsGenericType)
                        foreach (var gParam in genericParent.GenericParameters)
                            yield return gParam;
                }
                foreach (var value in this.owner.TypeParameters.Values)
                    yield return value;
            }

            #endregion

            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                this.owner = null;
            }

            #endregion

            #region ILockedTypeCollection Members

            public bool IsDisposed
            {
                get { return this.owner == null; }
            }

            #endregion
        }
    }
}
