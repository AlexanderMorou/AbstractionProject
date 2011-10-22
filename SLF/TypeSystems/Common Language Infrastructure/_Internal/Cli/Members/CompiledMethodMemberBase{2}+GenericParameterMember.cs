using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Abstract.Properties;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2012 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli.Members
{
    partial class CompiledMethodMemberBase<TMethod, TMethodParent>
        where TMethod :
            IMethodMember<TMethod, TMethodParent>
        where TMethodParent :
            IMethodParent<TMethod, TMethodParent>
    {
        private sealed class GenericParameterMember :
            CompiledGenericParameterMemberBase<IMethodSignatureGenericTypeParameterMember, IMethodSignatureMember>,
            IMethodSignatureGenericTypeParameterMember
            //ICompiledGenericTypeParameter<TMethod>
        {
            internal GenericParameterMember(TMethod parent, Type underlyingSystemType)
                : base(parent, underlyingSystemType)
            {

            }

            /// <summary>
            /// Creates a new nullable type as the current <see cref="GenericParameterMember"/>.
            /// </summary>
            /// <returns>A new <see cref="NullableType"/> on the current <see cref="GenericParameterMember"/>.</returns>
            protected override IType OnMakeNullable()
            {
                if (this.SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct)
                    return base.OnMakeNullable();
                throw new InvalidOperationException(Resources.TypeConstraintFailure_ValueType);
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

            protected override IArrayType OnMakeArray(int rank)
            {
                return new ArrayType(this, rank);
            }

            protected override IArrayType OnMakeArray(params int[] lowerBounds)
            {
                return new ArrayType(this, lowerBounds);
            }

            protected override IType OnMakeByReference()
            {
                return new ByRefType(this);
            }

            protected override IType OnMakePointer()
            {
                return new PointerType(this);
            }
        }
    }
}
