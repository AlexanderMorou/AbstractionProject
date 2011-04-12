using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Oil.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    partial class IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignatureParameter :
            IMethodSignatureParameterMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignatureParameter :
            TSignatureParameter,
            IIntermediateMethodSignatureParameterMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TSignature :
            class,
            IMethodSignatureMember<TSignatureParameter, TSignature, TParent>
        where TIntermediateSignature :
            TSignature,
            IIntermediateMethodSignatureMember<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent>
        where TParent :
            ISignatureParent<TSignature, TSignatureParameter, TParent>
        where TIntermediateParent :
            TParent,
            IIntermediateSignatureParent<TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TParent, TIntermediateParent>
    {
        protected sealed class GenericParameterCollection :
            ILockedTypeCollection
        {
            private IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> owner;
            public GenericParameterCollection(IntermediateMethodSignatureMemberBase<TSignatureParameter, TIntermediateSignatureParameter, TSignature, TIntermediateSignature, TParent, TIntermediateParent> owner)
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
                if (this.Count + arrayIndex >= array.Length)
                    throw new ArgumentException("array");
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
        }
    }
}
