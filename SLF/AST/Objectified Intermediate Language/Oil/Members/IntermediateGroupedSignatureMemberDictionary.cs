using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Events;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public class IntermediateGroupedSignatureMemberDictionary<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> :
        IntermediateGroupedMemberDictionary<TSignatureParent, TIntermediateSignatureParent, TSignatureIdentifier, TSignature, TIntermediateSignature>,
        IIntermediateSignatureMemberDictionary<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
        IIntermediateSignatureMemberDictionary
        where TSignatureIdentifier :
            ISignatureMemberUniqueIdentifier<TSignatureIdentifier>,
            IGeneralMemberUniqueIdentifier
        where TSignature :
            ISignatureMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignature :
            IIntermediateSignatureMember<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
            TSignature
        where TSignatureParameter :
            ISignatureParameterMember<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParameter :
            IIntermediateSignatureParameterMember<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParameter
        where TSignatureParent :
            ISignatureParent<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>
        where TIntermediateSignatureParent :
            class,
            IIntermediateSignatureParent<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent>,
            TSignatureParent
    {

        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedSignatureMemberDictionary{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> with the 
        /// <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateSignatureParent"/>
        /// which contains the <see cref="IntermediateGroupedSignatureMemberDictionary{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>.</param>
        public IntermediateGroupedSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent)
            : base(master, parent)
        {
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedSignatureMemberDictionary{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/> and <paramref name="root"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateSignatureParent"/>
        /// which contains the <see cref="IntermediateGroupedSignatureMemberDictionary{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>.</param>
        /// <param name="root">The <see cref="IntermediateGroupedSignatureMemberDictionary{TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent}"/>
        /// which the current is based upon.</param>
        public IntermediateGroupedSignatureMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateSignatureParent parent, IntermediateGroupedSignatureMemberDictionary<TSignatureIdentifier, TSignature, TIntermediateSignature, TSignatureParameter, TIntermediateSignatureParameter, TSignatureParent, TIntermediateSignatureParent> root)
            : base(master, parent, root)
        {
        }

        #region ISignatureMemberDictionary<TSignature,TSignatureParameter,TSignatureParent> Members

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        public IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, ITypeCollectionBase search)
        {
            return CLICommon.FindCache<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(((ISignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>)this).Values, search, strict);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="search">The <see cref="ITypeCollection"/> that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(bool, ITypeCollectionBase)"/> where strict is true.</remarks>
        public IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(ITypeCollectionBase search)
        {
            return this.Find(true, search);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="strict">Whether to adhere to the <paramref name="search"/> criteria
        /// strictly.</param>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        public IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(bool strict, params IType[] search)
        {
            return CLICommon.FindCache<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>(((ISignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent>)this).Values, search, strict);
        }

        /// <summary>
        /// Searches for <typeparamref name="TSignature"/> instances that match the <paramref name="search"/> criteria.
        /// </summary>
        /// <param name="search">The <see cref="IType"/> array that designates the signature to look for.</param>
        /// <returns>A new <see cref="IFilteredSignatureMemberDictionary{TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent}"/> of 
        /// <typeparamref name="TSignature"/> instances that matched the <paramref name="search"/> criteria.</returns>
        /// <exception cref="System.ArgumentNullException">thrown when <paramref name="search"/> is null.</exception>
        /// <remarks>Alias for <see cref="Find(bool, IType[])"/> where strict is true.</remarks>
        public IFilteredSignatureMemberDictionary<TSignatureIdentifier, TSignature, TSignatureParameter, TSignatureParent> Find(params IType[] search)
        {
            return this.Find(true, search);
        }

        #endregion

        #region ISignatureMemberDictionary Members

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(bool strict, ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(strict, search);
        }

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(ITypeCollection search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(search);
        }

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(bool strict, params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(strict, search);
        }

        IFilteredSignatureMemberDictionary ISignatureMemberDictionary.Find(params IType[] search)
        {
            return (IFilteredSignatureMemberDictionary)this.Find(search);
        }

        #endregion


        protected override void OnItemAdded(EventArgsR1<TIntermediateSignature> e)
        {
            if (e != null && e.Arg1 != null)
            {
                e.Arg1.Renamed += new EventHandler<DeclarationNameChangedEventArgs>(SignatureItem_Renamed);
                e.Arg1.ParameterAdded += new EventHandler<EventArgsR1<TIntermediateSignatureParameter>>(SignatureItem_ParameterChange);
                e.Arg1.ParameterRemoved += new EventHandler<EventArgsR1<TIntermediateSignatureParameter>>(SignatureItem_ParameterChange);
            }
            base.OnItemAdded(e);
        }

        protected void RekeyElement(TIntermediateSignature signature)
        {
            int valueIndex = this.Values.IndexOf(signature);
            if (valueIndex != -1)
                this.Keys[valueIndex] = signature.UniqueIdentifier;
        }

        void SignatureItem_Renamed(object sender, DeclarationNameChangedEventArgs e)
        {
            if (sender is TIntermediateSignature)
                this.RekeyElement((TIntermediateSignature)sender);
        }

        void SignatureItem_ParameterChange(object sender, EventArgsR1<TIntermediateSignatureParameter> e)
        {
            if (sender is TIntermediateSignature)
                this.RekeyElement((TIntermediateSignature)sender);
        }

        protected override void OnItemRemoved(EventArgsR1<TIntermediateSignature> e)
        {
            if (e != null && e.Arg1 != null)
            {
                e.Arg1.Renamed -= new EventHandler<DeclarationNameChangedEventArgs>(SignatureItem_Renamed);
                e.Arg1.ParameterAdded -= new EventHandler<EventArgsR1<TIntermediateSignatureParameter>>(SignatureItem_ParameterChange);
                e.Arg1.ParameterRemoved -= new EventHandler<EventArgsR1<TIntermediateSignatureParameter>>(SignatureItem_ParameterChange);
            }
            base.OnItemRemoved(e);
        }
    }
}
