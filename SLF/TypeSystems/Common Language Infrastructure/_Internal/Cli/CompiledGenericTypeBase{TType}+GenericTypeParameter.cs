﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.Abstract.Members;
using AllenCopeland.Abstraction.Slf._Internal.Cli.Members;
using AllenCopeland.Abstraction.Slf.CompilerServices;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
using AllenCopeland.Abstraction.Slf.Cli;
using AllenCopeland.Abstraction.Slf.Cli.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2009 Allen Copeland Jr.                                  |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */


namespace AllenCopeland.Abstraction.Slf._Internal.Cli
{
    partial class CompiledGenericTypeBase<TType>
    {
        protected internal sealed class GenericTypeParameter :
            CompiledGenericTypeParameter<TType>,
            IGenericTypeParameter<TType>
        {

            public GenericTypeParameter(TType parent, Type underlyingSystemType)
                : base(parent, underlyingSystemType)
            {

            }

            protected override IType OnMakeNullable()
            {
                if (this.SpecialConstraint == GenericTypeParameterSpecialConstraint.Struct)
                    return base.OnMakeNullable();
                throw new InvalidOperationException(string.Format("Only structures are nullable, no constraint to certify {0} as a struct.", this.Name));
            }

            protected override AccessLevelModifiers OnGetAccessLevel()
            {
                return AccessLevelModifiers.Public;
            }

            protected override IType BaseTypeImpl
            {
                get { return null; }
            }

            /// <summary>
            /// Returns a collection of <see cref="IType"/> instances that are implemented by the current
            /// <see cref="CompiledGenericTypeBase{TType}.GenericTypeParameter"/>.
            /// </summary>
            protected override ILockedTypeCollection OnGetImplementedInterfaces()
            {
                return LockedTypeCollection.Empty;
            }

            protected override IFullMemberDictionary OnGetMembers()
            {
                return null;
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