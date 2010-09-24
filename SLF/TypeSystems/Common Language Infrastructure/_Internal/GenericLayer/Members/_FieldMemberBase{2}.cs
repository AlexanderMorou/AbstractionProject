using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2010 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    internal abstract class _FieldMemberBase<TField, TFieldParent> :
        _MemberBase<TField, TFieldParent>,
        IFieldMember<TField, TFieldParent>
        where TField :
            IFieldMember<TField, TFieldParent>
        where TFieldParent :
            IFieldParent<TField, TFieldParent>
    {
        protected _FieldMemberBase(TField original, TFieldParent parent)
            : base(original, parent)
        {

        }

        #region IFieldMember Members

        public IType FieldType
        {
            get
            {
                if (Parent is IGenericType)
                {
                    IGenericType parent = ((IGenericType)(this.Parent));
                    if (parent.IsGenericType && !parent.IsGenericTypeDefinition)
                        return this.Original.FieldType.Disambiguify(parent.GenericParameters, null, TypeParameterSources.Type);
                }
                return this.Original.FieldType;
            }
        }

        #endregion
    }
}
