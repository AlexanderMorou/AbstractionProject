﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Utilities.Collections;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledGenericParameterConstructorMemberBase<TGenericParameter>
    {
        private new class Parameters :
            ParameterMembersBase<IGenericParameterConstructorMember<TGenericParameter>, IConstructorParameterMember<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>>,
            IParameterMemberDictionary<IGenericParameterConstructorMember<TGenericParameter>, IConstructorParameterMember<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>>
        {
            public Parameters(ParameterInfo[] dataSeries, CompiledGenericParameterConstructorMemberBase<TGenericParameter> parent)
                : base(parent, dataSeries.OnAll<ParameterInfo, IConstructorParameterMember<IGenericParameterConstructorMember<TGenericParameter>, TGenericParameter>>(f => new Parameter(f, parent)).ToArray())
            {

            }
        }
    }
}