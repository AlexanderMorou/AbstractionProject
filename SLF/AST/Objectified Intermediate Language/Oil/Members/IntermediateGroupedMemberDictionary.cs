using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base grouped member dictionary.
    /// </summary>
    /// <typeparam name="TMemberParent">The type of <see cref="IMemberParent"/>
    /// used in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMemberParent">The type of
    /// <see cref="IIntermediateMemberParent"/> in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TMember">The type of <see cref="IMember{TParent}"/> 
    /// used in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMember">The type of 
    /// <see cref="IIntermediateMember{TIdentifier, TParent, TIntermediateParent}"/>
    /// used in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateGroupedMemberDictionary<TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember> :
        IntermediateGroupedDeclarationDictionary<TMemberIdentifier, TMember, IGeneralMemberUniqueIdentifier, IMember, TIntermediateMember>,
        IIntermediateGroupedMemberDictionary<TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember>,
        IIntermediateGroupedMemberDictionary
        where TMemberParent :
            IMemberParent
        where TIntermediateMemberParent :
            class,
            IIntermediateMemberParent,
            TMemberParent
        where TMemberIdentifier :
            IMemberUniqueIdentifier<TMemberIdentifier>
        where TMember :
            IMember<TMemberIdentifier, TMemberParent>
        where TIntermediateMember :
            TMember,
            IIntermediateMember<TMemberIdentifier, TMemberParent, TIntermediateMemberParent>
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/> with the 
        /// <paramref name="master"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateMemberParent"/>
        /// which contains the <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>.</param>
        public IntermediateGroupedMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateMemberParent parent)
            : base(master)
        {
            this.Parent = parent;
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/> with the 
        /// <paramref name="master"/>, <paramref name="parent"/>, and <paramref name="root"/> provided.
        /// </summary>
        /// <param name="master">The <see cref="IntermediateFullMemberDictionary"/>
        /// which contains the current instance's members as well as those of other <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>
        /// instances of varying types.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateMemberParent"/>
        /// which contains the <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>.</param>
        /// <param name="root">The root <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>
        /// which the current is based upon.</param>
        public IntermediateGroupedMemberDictionary(IntermediateFullMemberDictionary master, TIntermediateMemberParent parent, IntermediateGroupedMemberDictionary<TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember> root)
            : base(master, root)
        {
            this.Parent = parent;
        }

        #region IIntermediateMemberDictionary<TMemberParent,TIntermediateMemberParent,TMember,TIntermediateMember> Members

        /// <summary>
        /// Returns the <typeparamref name="TIntermediateMemberParent"/> which contains the <see cref="IntermediateGroupedMemberDictionary{TMemberParent, TIntermediateMemberParent, TMemberIdentifier, TMember, TIntermediateMember}"/>.
        /// </summary>
        public TIntermediateMemberParent Parent { get; private set; }

        #endregion

        #region IMemberDictionary<TMemberParent,TMember> Members

        TMemberParent IMemberDictionary<TMemberParent, TMember>.Parent
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

        public bool Remove(string uniqueId)
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
                throw new ArgumentNullException("member");
            if (!this.Values.Contains(member))
                throw new ArgumentException("member");
            return this.Remove(member.UniqueIdentifier);
        }

        bool IIntermediateMemberDictionary.Remove(IIntermediateMember member)
        {
            if (!(member is TIntermediateMember))
                return false;
            return this.Remove((TIntermediateMember)(member));
        }

        #endregion


        protected override bool ShouldDispose(TIntermediateMember v)
        {
            return v.Parent == this.Parent;
        }
    }
}
