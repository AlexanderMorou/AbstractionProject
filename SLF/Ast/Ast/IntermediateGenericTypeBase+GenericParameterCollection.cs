using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast
{
    partial class IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType>
        where TTypeIdentifier :
            IGenericTypeUniqueIdentifier,
            IGeneralDeclarationUniqueIdentifier
        where TType :
            class,
            IGenericType<TTypeIdentifier, TType>
        where TIntermediateType :
            class,
            IIntermediateGenericType<TTypeIdentifier, TType, TIntermediateType>,
            TType
    {
        protected sealed class GenericParameterCollection :
            ILockedTypeCollection
        {
            private IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType> owner;
            public GenericParameterCollection(IntermediateGenericTypeBase<TTypeIdentifier, TType, TIntermediateType> owner)
            {
                this.owner = owner;
            }

            #region IControlledTypeCollection Members

            public int IndexOf(IType type)
            {
                if (type is IGenericParameter)
                    return ((IGenericParameter)(type)).Position;
                return -1;
            }

            #endregion

            #region IControlledCollection<IType> Members

            public int Count
            {
                get
                {
                    if (this.owner.Parent is IGenericType)
                    {
                        var genericParent = ((IGenericType)(this.owner.Parent));
                        if (genericParent.IsGenericConstruct)
                            return genericParent.GenericParameters.Count + this.owner.TypeParameters.Count;
                    }
                    return this.owner.TypeParameters.Count;
                }
            }

            public bool Contains(IType item)
            {
                if (item is IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>)
                    return this.owner.TypeParameters.Values.Contains(((IIntermediateGenericTypeParameter<TTypeIdentifier, TType, TIntermediateType>)(item)));
                return false;
            }

            public void CopyTo(IType[] array, int arrayIndex = 0)
            {
                if (arrayIndex < 0 || this.Count + arrayIndex > array.Length)
                    throw new ArgumentOutOfRangeException("arrayIndex");
                for (int i = 0; i < this.Count; i++)
                    array[arrayIndex + i] = this[i];
            }

            public IType this[int index]
            {
                get {
                    if (this.owner.Parent is IGenericType)
                    {
                        var genericParent = ((IGenericType)(this.owner.Parent));
                        if (genericParent.IsGenericConstruct)
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
                    if (genericParent.IsGenericConstruct)
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
                    if (genericParent.IsGenericConstruct)
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

            #region IEquatable<IControlledTypeCollection> Members

            public bool Equals(IControlledTypeCollection other)
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
                if (obj is IControlledTypeCollection)
                    return this.Equals((IControlledTypeCollection)(obj));
                return false;
            }

            public override int GetHashCode()
            {
                return this.Count.GetHashCode();
            }
        }
    }
}
