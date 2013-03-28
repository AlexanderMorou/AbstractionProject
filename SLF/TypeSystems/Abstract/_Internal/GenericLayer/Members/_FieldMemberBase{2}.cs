using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _FieldMemberBase<TField, TFieldParent> :
        _MemberBase<IGeneralMemberUniqueIdentifier, TField, TFieldParent>,
        IFieldMember<TField, TFieldParent>
        where TField :
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {
        private IGeneralMemberUniqueIdentifier member;
        private IGeneralMemberUniqueIdentifier uniqueIdentifier;
        protected _FieldMemberBase(TField original, TFieldParent parent)
            : base(original, parent)
        {

        }

        public override IGeneralMemberUniqueIdentifier UniqueIdentifier
        {
            get
            {
                if (this.uniqueIdentifier == null)
                    this.uniqueIdentifier = TypeSystemIdentifiers.GetMemberIdentifier(this.Name);
                return this.uniqueIdentifier;
            }
        }
        #region IFieldMember Members

        public IType FieldType
        {
            get
            {
                if (Parent is IGenericType)
                {
                    IGenericType parent = ((IGenericType)(this.Parent));
                    if (parent.IsGenericConstruct && !parent.IsGenericDefinition)
                        return this.Original.FieldType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                }
                return this.Original.FieldType;
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("field {0}::{1}", this.Parent, this.UniqueIdentifier);
        }
    }
}
