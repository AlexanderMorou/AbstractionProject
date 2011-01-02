using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using AllenCopeland.Abstraction.Utilities.Collections;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2011 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    internal sealed partial class CompiledEnumType :
        CompiledTypeBase<IEnumType>,
        ICompiledEnumType
    {
        /// <summary>
        /// Data member for <see cref="Fields"/>.
        /// </summary>
        private IFieldMemberDictionary<IEnumFieldMember, IEnumType> fields;

        internal CompiledEnumType(Type underlyingSystemType)
            : base(underlyingSystemType)
        {
        }

        private IFieldMemberDictionary<IEnumFieldMember, IEnumType> InitializeFields()
        {
            return new LockedFieldMembersBase<IEnumFieldMember, IEnumType>(this._Members, this, this.UnderlyingSystemType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly).Filter(field =>
                !(field.IsSpecialName || field.IsDefined(typeof(CompilerGeneratedAttribute), true))), GetField);
        }
        protected override IFullMemberDictionary OnGetMembers()
        {
            this.CheckFields();
            return base.OnGetMembers();
        }
        private IEnumFieldMember GetField(FieldInfo source)
        {
            return new FieldMember(source, this);
        }

        #region IFieldParent<IEnumFieldMember,IEnumType> Members

        public IFieldMemberDictionary<IEnumFieldMember, IEnumType> Fields
        {
            get {
                CheckFields();
                return this.fields;
            }
        }

        private void CheckFields()
        {
            if (this.fields == null)
                this.fields = this.InitializeFields();
        }

        #endregion

        #region IFieldParent Members

        IFieldMemberDictionary IFieldParent.Fields
        {
            get { return (IFieldMemberDictionary)this.Fields; }
        }

        #endregion


        protected override TypeKind TypeImpl
        {
            get { return TypeKind.Enumerator; }
        }
    }
}
