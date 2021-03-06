﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Ast.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    partial class IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignatureParameter
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>,
            TSignature
        where TParent :
            ISignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            IIntermediateSignatureParent<IGeneralGenericSignatureMemberUniqueIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>,
            TParent
    {
        protected sealed class GenericParameterCollection :
            ILockedTypeCollection
        {
            private IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> owner;
            public GenericParameterCollection(IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> owner)
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
                get { return this.owner.TypeParameters.Count; }
            }

            public bool Contains(IType item)
            {
                if (item is IIntermediateMethodSignatureGenericTypeParameterMember)
                    return this.owner.TypeParameters.Values.Contains(((IIntermediateMethodSignatureGenericTypeParameterMember)(item)));
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
                    return this.owner.TypeParameters.Values[index];
                }
            }

            public IType[] ToArray()
            {
                IType[] result = new IType[this.Count];
                for (int i = 0; i < this.Count; i++)
                    result[i] = this[i];
                return result;
            }

            #endregion

            #region IEnumerable<IType> Members

            public IEnumerator<IType> GetEnumerator()
            {
                if (this.owner == null)
                    yield break;
                foreach (var value in this.owner.TypeParameters.Values)
                    yield return value;
                yield break;
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
