using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Abstract.Members
{
    internal abstract class FieldMemberBase<TField, TFieldParent> :
        MemberBase<TFieldParent>,
        IFieldMember<TField, TFieldParent>
        where TField :
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {

        /// <summary>
        /// Creates a new <see cref="FieldMemberBase{TField, TParent}"/> instance
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TFieldParent"/>
        /// which contains the new <see cref="FieldMemberBase{TField, TParent}"/>.</param>
        protected FieldMemberBase(TFieldParent parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Returns the unique identifier for the current 
        /// <see cref="FieldMemberBase{TField, TFieldParent}"/> 
        /// where  <see cref="DeclarationBase.Name"/> is not enough to 
        /// distinguish between two <typeparamref name="TField"/> 
        /// entities.
        /// </summary>
        /// <remarks>Returns <see cref="DeclarationBase.Name"/>.</remarks>
        public override string UniqueIdentifier
        {
            get { return this.Name; }
        }

        #region IFieldMember Members

        public IType FieldType
        {
            get { return this.OnGetFieldType(); }
        }

        #endregion

        /// <summary>
        /// Returns the <see cref="IType"/> related to the <see cref="FieldType"/> of the
        /// current <see cref="FieldMemberBase{TField, TFieldParent}"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="IType"/> which relates to
        /// the type of the
        /// <see cref="FieldMemberBase{TField, TFieldParent}"/>.</returns>
        protected abstract IType OnGetFieldType();
    }
}
