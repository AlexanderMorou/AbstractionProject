using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    /// <summary>
    /// Provides a base implementation of an 
    /// <see cref="ICompiledFieldMember"/> which 
    /// is a child to a 
    /// <typeparamref name="TFieldParent"/>.
    /// </summary>
    /// <typeparam name="TField">The type of field 
    /// used in the current implementation.</typeparam>
    /// <typeparam name="TFieldParent">The type 
    /// of parent used to contain the 
    /// <typeparamref name="TField"/> instances
    /// in the current implementation.</typeparam>
    internal abstract class CompiledFieldMemberBase<TField, TFieldParent> :
        FieldMemberBase<TField, TFieldParent>,
        ICompiledFieldMember
        where TField :
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {
        /// <summary>
        /// Data member for <see cref="FieldInfo"/>.
        /// </summary>
        private FieldInfo memberInfo;

        /// <summary>
        /// Creates a new <see cref="CompiledFieldMemberBase{TField, TFieldBase}"/>
        /// with the <paramref name="memberInfo"/> and <paramref name="parent"/>
        /// provided.
        /// </summary>
        /// <param name="memberInfo">The <see cref="FieldInfo"/> related to the
        /// <see cref="CompiledFieldMemberBase{TField, TFieldBase}"/>'s
        /// data.</param>
        /// <param name="parent">The <typeparamref name="TFieldParent"/> which
        /// contains the <see cref="CompiledFieldMemberBase{TField, TFieldBase}"/>.</param>
        protected CompiledFieldMemberBase(FieldInfo memberInfo, TFieldParent parent)
            : base(parent)
        {
            this.memberInfo = memberInfo;
        }

        /// <summary>
        /// Obtains the <see cref="DeclarationBase.Name"/> 
        /// for the <see cref="CompiledFieldMemberBase{TField, TFieldParent}"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that 
        /// contains the name of the <see cref="DeclarationBase"/>.
        /// </returns>
        /// <remarks>Returns <see cref="System.Reflection.MemberInfo.Name"/></remarks>
        protected override string OnGetName()
        {
            return this.MemberInfo.Name;
        }

        /// <summary>
        /// Returns the <see cref="IType"/> related to the <see cref="FieldMemberBase{TField, TFieldParent}.FieldType"/> of the
        /// current <see cref="CompiledFieldMemberBase{TField, TFieldParent}"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="IType"/> which relates to
        /// the type of the
        /// <see cref="CompiledFieldMemberBase{TField, TFieldParent}"/>.</returns>
        protected override IType OnGetFieldType()
        {
            return this.MemberInfo.FieldType.GetTypeReference();
        }

        #region ICompiledFieldMember Members

        /// <summary>
        /// Returns the <see cref="FieldInfo"/> associated 
        /// to the <see cref="CompiledFieldMemberBase{TField, TFieldBase}"/>.
        /// </summary>
        public FieldInfo MemberInfo
        {
            get { return this.memberInfo; }
        }

        #endregion

        #region ICompiledMember Members

        MemberInfo ICompiledMember.MemberInfo
        {
            get { return this.MemberInfo; }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} {1}", this.FieldType.FullName, this.Name);
        }

        public override void Dispose()
        {
            this.memberInfo = null;
        }
    }
}
