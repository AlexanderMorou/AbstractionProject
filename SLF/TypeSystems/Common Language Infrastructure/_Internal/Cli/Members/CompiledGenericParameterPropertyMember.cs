using System;
using System.Collections.Generic;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Abstract;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using System.Reflection;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    internal sealed class CompiledGenericParameterPropertyMember<TGenericParameter> :
        CompiledPropertySignatureMemberBase<IGenericParameterPropertyMember<TGenericParameter>, TGenericParameter, IGenericParameterPropertyMethodMember<TGenericParameter>, IGenericParameterMethodMember<TGenericParameter>, TGenericParameter>,
        IGenericParameterPropertyMember<TGenericParameter>
        where TGenericParameter :
            IGenericParameter<TGenericParameter>
    {
        /// <summary>
        /// Creates a new <see cref="CompiledGenericParameterPropertyMember{TGenericParameter}"/>
        /// with the <paramref name="parent"/> provided.
        /// </summary>
        /// <param name="parent">The <typeparamref name="TGenericParameter"/> which owns the
        /// <see cref="CompiledGenericParameterPropertyMember{TGenericParameter}"/>.</param>
        internal CompiledGenericParameterPropertyMember(TGenericParameter parent, PropertyInfo memberInfo)
            : base(parent, memberInfo)
        {
        }

        private sealed class MethodMember :
            CompiledGenericParameterMethodMember<TGenericParameter>,
            IGenericParameterPropertyMethodMember<TGenericParameter>
        {
            internal MethodMember(PropertyMethodType methodType, TGenericParameter parent, MethodInfo memberInfo)
                : base(parent, memberInfo)
            {
                this.MethodType = methodType;
            }

            #region IPropertySignatureMethodMember Members

            public PropertyMethodType MethodType { get; private set; }

            #endregion
        }

        protected override IGenericParameterPropertyMethodMember<TGenericParameter> OnGetMethod(PropertyMethodType methodType, MethodInfo memberInfo)
        {
            return new MethodMember(methodType, Parent, memberInfo);
        }

    }
}
