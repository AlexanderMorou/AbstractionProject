using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2013 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */
namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    using ParametersBase = _ParametersBase<IDelegateType, IDelegateTypeParameterMember>;
    partial class _DelegateTypeBase
    {
        private class _Parameters :
            ParametersBase,
            IDelegateTypeParameterDictionary
        {
            internal _Parameters(_DelegateTypeBase parent, IDelegateTypeParameterDictionary original)
                : base(parent, original)
            {
            }
            protected override IDelegateTypeParameterMember GetWrapper(IDelegateTypeParameterMember parameter, IDelegateType parent)
            {
                return new _Parameter(parameter, (_DelegateTypeBase)parent);
            }
            private new class _Parameter :
                ParametersBase._Parameter,
                IDelegateTypeParameterMember
            {
                internal _Parameter(IDelegateTypeParameterMember original, _DelegateTypeBase parent)
                    : base(original, parent)
                {
                }
            }

        }
    }
}
