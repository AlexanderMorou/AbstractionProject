﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
using ParametersBase = AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members._ParametersBase<AllenCopeland.Abstraction.Slf.Abstract.IDelegateType, AllenCopeland.Abstraction.Slf.Abstract.Members.IDelegateTypeParameterMember>;

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
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