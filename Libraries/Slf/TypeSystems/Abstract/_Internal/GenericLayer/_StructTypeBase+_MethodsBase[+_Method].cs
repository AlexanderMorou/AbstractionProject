﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllenCopeland.Abstraction.Slf._Internal.GenericLayer.Members;
using AllenCopeland.Abstraction.Slf.Abstract;
using AllenCopeland.Abstraction.Slf.Abstract.Members;
 /*---------------------------------------------------------------------\
 | Copyright © 2008-2016 Allen C. [Alexander Morou] Copeland Jr.        |
 |----------------------------------------------------------------------|
 | The Abstraction Project's code is provided under a contract-release  |
 | basis.  DO NOT DISTRIBUTE and do not use beyond the contract terms.  |
 \-------------------------------------------------------------------- */

namespace AllenCopeland.Abstraction.Slf._Internal.GenericLayer
{
    partial class _StructTypeBase
    {
        internal class _MethodsBase :
            _MethodMembersBase<IStructMethodMember, IStructType>
        {
            internal _MethodsBase(_FullMembersBase master, IMethodMemberDictionary<IStructMethodMember, IStructType> originalSet, _StructTypeBase parent)
                : base(master, originalSet, parent)
            {
            }

            protected override IStructMethodMember ObtainWrapper(IStructMethodMember item)
            {
                return new _Method(this.Parent, item);
            }
            internal class _Method :
                _MethodMemberBase<IStructMethodMember, IStructType>,
                IStructMethodMember
            {
                internal _Method(IStructType parent, IStructMethodMember original)
                    : base(parent, original)
                {
                }

                internal _Method(IStructMethodMember original, IControlledTypeCollection genericParameters)
                    : base(original, genericParameters)
                {

                }

                public ExtendedMethodAttributes Attributes
                {
                    get
                    {
                        return this.Original.Attributes;
                    }
                }

                #region IExtendedInstanceMember Members

                public bool IsAsynchronous
                {
                    get
                    {
                        return this.Original.IsAsynchronous;
                    }
                }

                ExtendedMemberAttributes IExtendedInstanceMember.Attributes
                {
                    get { return (ExtendedMemberAttributes)this.Attributes ^ ExtendedMemberAttributes.FlagsMask; }
                }

                public bool IsStatic
                {
                    get { return this.Original.IsStatic; }
                }

                public bool IsAbstract
                {
                    get { return this.Original.IsAbstract; }
                }

                public bool IsVirtual
                {
                    get { return this.Original.IsVirtual; }
                }

                public bool IsHideBySignature
                {
                    get { return this.Original.IsHideBySignature; }
                }

                public bool IsFinal
                {
                    get { return this.Original.IsFinal; }
                }

                public bool IsOverride
                {
                    get { return this.Original.IsOverride; }
                }

                #endregion

                protected override IStructMethodMember OnMakeGenericMethod(IControlledTypeCollection genericReplacements)
                {
                    return new _Method(this, genericReplacements);
                }

                #region IInstanceMember Members

                InstanceMemberAttributes IInstanceMember.Attributes
                {
                    get {
                        return ((InstanceMemberAttributes)this.Original.Attributes) & InstanceMemberAttributes.FlagsMask;
                    }
                }

                #endregion


                #region IStructMethodMember Members
                /* *
                 * Easier than the class version of the same since there's guarantee
                 * that the parent definition has no generic type-parameters, and must
                 * be the same as the original definition's base.
                 * *
                 * Struct method members that have a base definition have to be one of these three:
                 * GetHashCode, Equals or ToString.
                 * */
                public IClassMethodMember BaseDefinition
                {
                    get {
                        return this.Original.BaseDefinition;
                    }
                }

                #endregion


                public IEnumerable<IInterfaceType> Implementations
                {
                    get
                    {
                        foreach (var impl in this.Original.Implementations)
                            yield return (IInterfaceType)ResolveType(impl);
                    }
                }
            }
        }
    }
}
