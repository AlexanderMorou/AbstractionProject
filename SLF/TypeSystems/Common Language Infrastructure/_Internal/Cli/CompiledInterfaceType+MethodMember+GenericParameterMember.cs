using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledInterfaceType
    {
        partial class MethodMember
        {
            private sealed class GenericParameterMember :
                CompiledGenericParameterMemberBase<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>,
                IMethodSignatureGenericTypeParameterMember
            {
                internal GenericParameterMember(IInterfaceMethodMember parent, Type underlyingSystemType)
                    : base(parent, underlyingSystemType)
                {

                }

                /// <summary>
                /// Makes the current <see cref="GenericParameterMember"/> as a 
                /// nullable <see cref="IType"/>.
                /// </summary>
                /// <returns>A <see cref="IType"/> instance
                /// which represents the current <see cref="GenericParameterMember"/>
                /// as a nullable type.</returns>
                protected override IType OnMakeNullable()
                {
                    if (this.SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct)
                        return base.OnMakeNullable();
                    throw new InvalidOperationException(string.Format("Only structures are nullable, no constraint to certify {0} as a struct.", this.Name));
                }

                #region IGenericParameter Members

                IGenericParamParent IGenericParameter.Parent
                {
                    get { return this.Parent; }
                }

                #endregion

                protected override TypeKind TypeImpl
                {
                    get { return TypeKind.Class; }
                }
            }
        }
    }
}
