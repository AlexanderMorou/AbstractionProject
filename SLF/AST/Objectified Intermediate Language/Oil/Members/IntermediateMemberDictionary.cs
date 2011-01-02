using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    /// <summary>
    /// Provides a base dictionary for intermediate members.
    /// </summary>
    /// <typeparam name="TParent">The type of parent in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of parent in the intermediate abstract syntax tree.</typeparam>
    /// <typeparam name="TMember">The type of member in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateMember">The type of member in the intermediate abstract
    /// syntax tree.</typeparam>
    public abstract class IntermediateMemberDictionary<TParent, TIntermediateParent, TMember, TIntermediateMember> :
        IntermediateDeclarationDictionary<TMember, TIntermediateMember>,
        IIntermediateMemberDictionary<TParent, TIntermediateParent, TMember, TIntermediateMember>,
        IIntermediateMemberDictionary
        where TParent :
            IMemberParent
        where TIntermediateParent :
            IIntermediateMemberParent,
            TParent
        where TMember :
            IMember<TParent>
        where TIntermediateMember :
            IIntermediateMember<TParent, TIntermediateParent>,
            TMember
    {
        /// <summary>
        /// Creates a new <see cref="IntermediateMemberDictionary{TParent, TIntermediateParent, TMember, TIntermediateMember}"/>
        /// initialized to its default state.
        /// </summary>
        public IntermediateMemberDictionary(TIntermediateParent parent) :
            base()
        {
            this.Parent = parent;
        }
        /// <summary>
        /// Creates a new <see cref="IntermediateMemberDictionary{TParent, TIntermediateParent, TMember, TIntermediateMember}"/> 
        /// with the <see cref="Dictionary{TKey, TValue}"/> <paramref name="toWrap"/>.
        /// </summary>
        /// <param name="toWrap">The <see cref="Dictionary{TKey, TValue}"/> to encapsulate.</param>
        public IntermediateMemberDictionary(TIntermediateParent parent, IntermediateMemberDictionary<TParent, TIntermediateParent, TMember, TIntermediateMember> toWrap) :
            base(toWrap)
        {
            this.Parent = parent;
        }

        #region IIntermediateMemberDictionary<TParent,TIntermediateParent,TMember,TIntermediateMember> Members

        public TIntermediateParent Parent { get; private set; }

        #endregion


        #region IMemberDictionary<TParent,TMember> Members

        TParent IMemberDictionary<TParent, TMember>.Parent
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

        public new bool Remove(string uniqueId)
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

    }
}
