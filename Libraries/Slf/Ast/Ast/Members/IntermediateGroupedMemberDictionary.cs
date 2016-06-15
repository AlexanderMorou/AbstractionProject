using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a base grouped member dictionary.
    /// </summary>
    /// <typeparam name="TMemberParent">The type of <see cref="IMemberParent"/>
    /// used in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMemberParent">The type of
    /// <see cref="IIntermediateMemberParent"/> in the intermediate
    /// abstract syntax tree.</typeparam>
    /// <typeparam name="TMemberIdentifier">The kind of identifier used to differentiate the 
    /// <typeparamref name="TIntermediateMember"/> instances from one another.</typeparam>
    /// <typeparam name="TMember">The type of <see cref="IMember{TMemberIdentifier, TParent}"/> 
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
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
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

        TMemberParent IMemberDictionary<TMemberParent, TMemberIdentifier, TMember>.Parent
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
        bool IIntermediateMemberDictionary.Remove(IGeneralMemberUniqueIdentifier uniqueId, bool dispose)
        {
            if (uniqueId == null)
                throw new ArgumentNullException("uniqueId");
            if (uniqueId is TMemberIdentifier)
                return this.Remove((TMemberIdentifier)uniqueId, dispose);
            throw ThrowHelper.ObtainArgumentException(ArgumentWithException.uniqueId, ExceptionMessageId.ValueIsWrongType, ThrowHelper.GetArgumentName(ArgumentWithException.uniqueId), uniqueId.GetType().ToString(), typeof(TMemberIdentifier).ToString());
        }
        public bool Remove(TMemberIdentifier uniqueId, bool dispose = true)
        {
            if (uniqueId == null)
                throw new ArgumentNullException("uniqueId");
            if (!this.ContainsKey(uniqueId))
                throw new KeyNotFoundException();
            var dummy = this[uniqueId];
            base._Remove(uniqueId);
            if (dispose)
            {
                dummy.Dispose();
                dummy = default(TIntermediateMember);
            }
            return true;
        }

        public bool Remove(TIntermediateMember member, bool dispose = true)
        {
            if (member == null)
                throw new ArgumentNullException("member");
            if (!this.Values.Contains(member))
                throw new ArgumentException("member");
            var key = this.Keys[this.Values.IndexOf(member)];
            return this.Remove(key, dispose);
        }

        bool IIntermediateMemberDictionary.Remove(IIntermediateMember member, bool dispose)
        {
            if (!(member is TIntermediateMember))
                return false;
            return this.Remove((TIntermediateMember)(member), dispose);
        }

        #endregion


        protected override bool ShouldDispose(TIntermediateMember v)
        {
            return v.Parent == this.Parent;
        }


        public override IEnumerable<KeyValuePair<TMemberIdentifier, TIntermediateMember>> ExclusivelyOnParent()
        {
            foreach (var element in this)
                if (element.Value.Parent == this.Parent)
                    yield return element;
                else if (element.Value is IIntermediateSegmentableDeclaration && ((IIntermediateSegmentableDeclaration)(element.Value)).Parts.Count > 0)
                    foreach (TIntermediateMember partial in ((IIntermediateSegmentableDeclaration)(element.Value)).Parts)
                        if (partial.Parent == this.Parent)
                            yield return new KeyValuePair<TMemberIdentifier, TIntermediateMember>(element.Key, partial);
        }
    }
}
