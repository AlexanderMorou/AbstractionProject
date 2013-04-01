using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Ast.Members
{
    /// <summary>
    /// Provides a base member class which provides the underlying functionality of an
    /// intermediate member.
    /// </summary>
    /// <typeparam name="TIdentifier">The kind of identifier used to differentiate
    /// the members of the same kind, and of varying kinds, from one another.</typeparam>
    /// <typeparam name="TParent">The type of <see cref="IMemberParent"/>
    /// which contains the <see cref="IntermediateMemberBase{TIdentifier, TParent, TIntermediateParent}"/>
    /// in the abstract type system.</typeparam>
    /// <typeparam name="TIntermediateParent">The type of <see cref="IMemberParent"/>
    /// which contains the <see cref="IntermediateMemberBase{TIdentifier, TParent, TIntermediateParent}"/>
    /// in the intermediate abstract syntax tree.</typeparam>
    public abstract class IntermediateMemberBase<TIdentifier, TParent, TIntermediateParent> :
        IntermediateDeclarationBase<TIdentifier>,
        IIntermediateMember<TIdentifier, TParent, TIntermediateParent>
        where TIdentifier :
            IMemberUniqueIdentifier,
            IGeneralMemberUniqueIdentifier
        where TParent :
            IMemberParent
        where TIntermediateParent :
            IIntermediateMemberParent,
            TParent
    {
        /// <summary>
        /// Data member for <see cref="Parent"/>.
        /// </summary>
        private TIntermediateParent parent;

        /// <summary>
        /// Creates a new <see cref="IntermediateMemberBase{TIdentifier, TParent, TIntermediateParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which
        /// contains the <see cref="IntermediateMemberBase{TIdentifier, TParent, TIntermediateParent}"/></param>
        public IntermediateMemberBase(TIntermediateParent parent)
            : base()
        {
            this.parent = parent;
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateMemberBase{TIdentifier, TParent, TIntermediateParent}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the unique identifier of the 
        /// <see cref="IntermediateMemberBase{TIdentifier, TParent, TIntermediateParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which
        /// contains the <see cref="IntermediateMemberBase{TIdentifier, TParent, TIntermediateParent}"/></param>
        public IntermediateMemberBase(string name, TIntermediateParent parent)
            : base()
        {
            base.AssignName(name);
            this.parent = parent;
        }

        #region IIntermediateMember<TParent,TIntermediateParent> Members

        /// <summary>
        /// Returns the parent of the 
        /// <see cref="IntermediateMemberBase{TIdentifier, TParent, TIntermediateParent}"/>.
        /// </summary>
        public TIntermediateParent Parent
        {
            get
            {
                return this.ParentImpl;
            }
        }

        #endregion

        #region IIntermediateMember
        IIntermediateMemberParent IIntermediateMember.Parent
        {
            get
            {
                return this.Parent;
            }
        }
        #endregion

        /// <summary>
        /// Returns/sets the parent locally.
        /// </summary>
        protected virtual TIntermediateParent ParentImpl
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        #region IMember Members

        IMemberParent IMember.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IMember<TParent> Members

        TParent IMember<TIdentifier, TParent>.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        #region IIntermediateMember Members

        /// <summary>
        /// Visits the <paramref name="visitor"/> provided.
        /// </summary>
        /// <param name="visitor">The <see cref="IIntermediateMemberVisitor"/>
        /// to observe the proper overload of.</param>
        public abstract void Visit(IIntermediateMemberVisitor visitor);

        public abstract TResult Visit<TResult, TContext>(IIntermediateMemberVisitor<TResult, TContext> visitor, TContext context);

        #endregion

        /// <summary>
        /// Disposes the <see cref="IntermediateMemberBase{TIdentifier, TParent, TIntermediateParent}"/>
        /// </summary>
        /// <param name="disposing">whether to dispose the managed 
        /// resources as well as the unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                this.parent = default(TIntermediateParent);
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
    }
}
