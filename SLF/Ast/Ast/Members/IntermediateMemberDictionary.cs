using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
/*---------------------------------------------------------------------\
| Copyright © 2008-2015 Allen C. [Alexander Morou] Copeland Jr.        |
|----------------------------------------------------------------------|
| The Abstraction Project's code is provided under a contract-release  |
| basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
\-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a base dictionary for intermediate members.
    /// </summary>
    /// <typeparam name="TParent">The type of parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of parent in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TMemberIdentifier">The kind of identifier used to differentiate the
    /// <typeparamref name="TIntermediateMember"/> instances from one another.</typeparam>
    /// <typeparam name="TMember">The type of member in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMember">The type of member in the intermediate abstract
    /// syntax tree.</typeparam>
    public abstract class IntermediateMemberDictionary<TParent, TIntermediateParent, TMemberIdentifier, TMember, TIntermediateMember> :
        IntermediateDeclarationDictionary<TMemberIdentifier, TMember, TIntermediateMember>,
        IIntermediateMemberDictionary<TParent, TIntermediateParent, TMemberIdentifier, TMember, TIntermediateMember>,
        IIntermediateMemberDictionary
        where TParent :
            IMemberParent
        where TIntermediateParent :
            IIntermediateMemberParent,
            TParent
        where TMemberIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TMember :
            IMember<TMemberIdentifier, TParent>
        where TIntermediateMember :
            IIntermediateMember<TMemberIdentifier, TParent, TIntermediateParent>,
            TMember
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateMemberDictionary{TParent, TIntermediateParent, TMemberIdentifier, TMember, TIntermediateMember}"/>
        /// initialized to its default state.
        /// </summary>
        public IntermediateMemberDictionary(TIntermediateParent parent) :
            base()
        {
            this.Parent = parent;
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateMemberDictionary{TParent, TIntermediateParent, TMemberIdentifier, TMember, TIntermediateMember}"/> 
        /// with the <see cref="Dictionary{TKey, TValue}"/> <paramref name="toWrap"/>.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/>
        /// which contains the <see cref="IntermediateMemberDictionary{TParent, TIntermediateParent, TMemberIdentifier, TMember, TIntermediateMember}"/>.</param>
        /// <param name="toWrap">The <see cref="Dictionary{TKey, TValue}"/> to encapsulate.</param>
        public IntermediateMemberDictionary(TIntermediateParent parent, IntermediateMemberDictionary<TParent, TIntermediateParent, TMemberIdentifier, TMember, TIntermediateMember> toWrap) :
            base(toWrap)
        {
            this.Parent = parent;
        }

        #region IIntermediateMemberDictionary<TParent,TIntermediateParent,TMember,TIntermediateMember> Members

        public TIntermediateParent Parent { get; private set; }

        #endregion


        #region IMemberDictionary<TParent,TMember> Members

        TParent IMemberDictionary<TParent, TMemberIdentifier, TMember>.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IMemberDictionary Members

        IMemberParent IMemberDictionary.Parent
        {
            get { return this.Parent; }
        }
        int IMemberDictionary.IndexOf(IMember decl)
        {
            if (!(decl is TMember))
                return -1;
            return this.IndexOf(((TMember)(decl)));
        }

        #endregion


        #region IIntermediateMemberDictionary Members

        bool IIntermediateMemberDictionary.Remove(IGeneralMemberUniqueIdentifier uniqueId)
        {
            if (uniqueId == null)
                throw new ArgumentNullException(ThrowHelper.GetArgumentName(ArgumentWithException.uniqueId));
            if (uniqueId is TMemberIdentifier)
                return this.Remove((TMemberIdentifier)uniqueId);
            throw ThrowHelper.ObtainArgumentException(ArgumentWithException.uniqueId, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.uniqueId), uniqueId.GetType().ToString(), typeof(TMemberIdentifier).ToString());
        }

        public bool Remove(TMemberIdentifier uniqueId)
        {
            if (!this.ContainsKey(uniqueId))
                throw new KeyNotFoundException();
            var dummy = this[uniqueId];
            base._Remove(uniqueId);
            dummy.Dispose();
            dummy = default(TIntermediateMember);
            return true;
        }

        public bool Remove(TIntermediateMember member)
        {
            if (member == null)
                throw new ArgumentNullException(ThrowHelper.GetArgumentName(ArgumentWithException.member));
            if (!this.Values.Contains(member))
                throw ThrowHelper.ObtainArgumentException(ArgumentWithException.member, ExceptionMessageId.Remove_ValueNotFound, ThrowHelper.GetArgumentName(ArgumentWithException.member));
            var key = this.Keys[this.Values.IndexOf(member)];
            return this.Remove(key);
        }

        bool IIntermediateMemberDictionary.Remove(IIntermediateMember member)
        {
            if (!(member is TIntermediateMember))
                return false;
            return this.Remove((TIntermediateMember)(member));
        }

        #endregion


        public override IEnumerable<KeyValuePair<TMemberIdentifier, TIntermediateMember>> ExclusivelyOnParent()
        {
            foreach (var memberKVP in this)
                if (object.ReferenceEquals(memberKVP.Value.Parent, this.Parent))
                    yield return memberKVP;
                else if (memberKVP.Value is IIntermediateSegmentableDeclaration && ((IIntermediateSegmentableDeclaration)(memberKVP.Value)).Parts.Count > 0)
                    foreach (TIntermediateMember partial in ((IIntermediateSegmentableDeclaration)(memberKVP.Value)).Parts)
                        if (object.ReferenceEquals(partial.Parent, this.Parent))
                            yield return new KeyValuePair<TMemberIdentifier, TIntermediateMember>(memberKVP.Key, partial);
        }
    }
}
