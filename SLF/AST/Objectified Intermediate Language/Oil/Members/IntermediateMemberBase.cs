﻿using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf.Oil.Members
{
    public class IntermediateMemberBase<TParent, TIntermediateParent> :
        IntermediateDeclarationBase,
        IIntermediateMember<TParent, TIntermediateParent>
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
        /// Creates a new <see cref="IntermediateMemberBase{TParent, TIntermediateParent}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which
        /// contains the <see cref="IntermediateMemberBase{TParent, TIntermediateParent}"/></param>
        public IntermediateMemberBase(TIntermediateParent parent)
            : base()
        {
            this.parent = parent;
        }

        /// <summary>
        /// Creates a new <see cref="IntermediateMemberBase{TParent, TIntermediateParent}"/>
        /// with the <paramref name="name"/> and <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="name">The <see cref="String"/> representing the unique identifier of the 
        /// <see cref="IntermediateMemberBase{TParent, TIntermediateParent}"/>.</param>
        /// <param name="parent">The <typeparamref name="TIntermediateParent"/> which
        /// contains the <see cref="IntermediateMemberBase{TParent, TIntermediateParent}"/></param>
        public IntermediateMemberBase(string name, TIntermediateParent parent)
            : base()
        {
            base.OnSetName(name);
            this.parent = parent;
        }

        #region IIntermediateMember<TParent,TIntermediateParent> Members

        /// <summary>
        /// Returns the parent of the 
        /// <see cref="IntermediateMemberBase{TParent, TIntermediateParent}"/>.
        /// </summary>
        public TIntermediateParent Parent
        {
            get
            {
                return this.ParentImpl;
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

        TParent IMember<TParent>.Parent
        {
            get { return this.Parent; }
        }

        #endregion

        public override string UniqueIdentifier
        {
            get { return this.Name; }
        }
    }
}