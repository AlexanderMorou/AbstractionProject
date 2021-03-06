﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members
{
    partial class _ParametersBase<TParent, TParameter>
        where TParent :
            IParameterParent<TParent, TParameter>
        where TParameter :
            class,
            IParameterMember<TParent>
    {

        protected internal class _Parameter :
            _MemberBase<IGeneralMemberUniqueIdentifier, TParameter, TParent>,
            IParameterMember<TParent>
        {

            internal _Parameter(TParameter original, TParent adjustedParent)
                : base(original, adjustedParent)
            {
            }

            #region IParameterMember Members

            IParameterParent IParameterMember.Parent
            {
                get { return this.Parent; }
            }

            public IType ParameterType
            {
                get
                {
                    return this.ParameterTypeImpl;
                }
            }

            protected virtual IType ParameterTypeImpl
            {
                get
                {
                    IType originalType = this.Original.ParameterType;
                    if (originalType.ContainsGenericParameters())
                    {
                        if (this.Parent is IGenericType)
                        {
                            return originalType.Disambiguify(((IGenericType)(this.Parent)).GenericParameters, null, TypeParameterSources.Type);
                        }
                        else if (this.Parent is IMember)
                        {
                            IMember parent = ((IMember)(this.Parent));
                            if (parent.Parent is IGenericType)
                                return originalType.Disambiguify(((IGenericType)(parent.Parent)).GenericParameters, null, TypeParameterSources.Type);
                        }
                    }
                    return originalType;
                }
            }

            public ParameterCoercionDirection Direction
            {
                get { return this.Original.Direction; }
            }

            public IMetadataCollection Metadata
            {
                get
                {
                    return this.Original.Metadata;
                }
            }

            #endregion

            public override IGeneralMemberUniqueIdentifier UniqueIdentifier
            {
                get { return this.Original.UniqueIdentifier; }
            }

            public bool IsDefined(IType metadatumType)
            {
                return this.Original.IsDefined(metadatumType);
            }
        }
    }
}
